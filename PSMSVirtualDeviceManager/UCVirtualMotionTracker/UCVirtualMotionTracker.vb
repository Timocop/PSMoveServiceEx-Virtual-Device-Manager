﻿Imports System.ComponentModel
Imports System.Numerics
Imports Rug.Osc

Public Class UCVirtualMotionTracker
    Public g_mFormMain As FormMain

    Private g_mAutostartMenuStrips As New Dictionary(Of Integer, ToolStripMenuItem)
    Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "vmt_devices.ini")

    Public g_ClassOscServer As ClassOscServer
    Public g_ClassControllerSettings As ClassControllerSettings
    Public g_ClassOscDevices As ClassOscDevices

    Private g_bIgnoreEvents As Boolean = True
    Private g_mOscStatusThread As Threading.Thread = Nothing
    Private g_mOscDeviceStatusThread As Threading.Thread = Nothing
    Private g_mPlayspaceCalibrationThread As Threading.Thread = Nothing

    Enum ENUM_SETTINGS_SAVE_TYPE_FLAGS
        ALL = -1
        CONTROLLER = (1 << 0)
        PLAYSPACE_CALIBRATION = (1 << 1)
        PLAYSPACE = (1 << 2)
    End Enum

    Class ClassTrackersListViewItem
        Inherits ListViewItem
        Implements IDisposable

        Private g_UCVirtualMotionTrackerItem As UCVirtualMotionTrackerItem
        Private g_UCVirtualMotionTracker As UCVirtualMotionTracker

        Public Sub New(iControllerID As Integer, _UCVirtualMotionTracker As UCVirtualMotionTracker)
            MyBase.New(New String() {"", "", ""})

            g_UCVirtualMotionTracker = _UCVirtualMotionTracker
            g_UCVirtualMotionTrackerItem = New UCVirtualMotionTrackerItem(iControllerID, _UCVirtualMotionTracker)

            UpdateItem()
        End Sub

        Public Sub UpdateItem()
            If (g_UCVirtualMotionTrackerItem Is Nothing OrElse g_UCVirtualMotionTrackerItem.IsDisposed) Then
                Return
            End If

            If (g_UCVirtualMotionTrackerItem.g_mClassIO Is Nothing) Then
                Return
            End If

            Me.SubItems(0).Text = CStr(g_UCVirtualMotionTrackerItem.g_mClassIO.m_Index)
            Me.SubItems(1).Text = CStr(g_UCVirtualMotionTrackerItem.g_mClassIO.m_VmtTracker)

            If (g_UCVirtualMotionTrackerItem.ComboBox_VMTTrackerRole.SelectedItem IsNot Nothing AndAlso g_UCVirtualMotionTrackerItem.ComboBox_SteamTrackerRole.SelectedItem IsNot Nothing) Then
                Me.SubItems(2).Text = String.Format("{0} ({1})", CStr(g_UCVirtualMotionTrackerItem.ComboBox_VMTTrackerRole.SelectedItem), CStr(g_UCVirtualMotionTrackerItem.ComboBox_SteamTrackerRole.SelectedItem))
            Else
                Me.SubItems(2).Text = ""
            End If

            'Is there any error?
            If (g_UCVirtualMotionTrackerItem.Panel_Status.Visible) Then
                Me.BackColor = Color.FromArgb(255, 192, 192)
            Else
                Me.BackColor = Color.FromArgb(255, 255, 255)
            End If
        End Sub

        ReadOnly Property m_UCVirtualMotionTrackerItem As UCVirtualMotionTrackerItem
            Get
                Return g_UCVirtualMotionTrackerItem
            End Get
        End Property

        Property m_Visible As Boolean
            Get
                If (g_UCVirtualMotionTrackerItem Is Nothing OrElse g_UCVirtualMotionTrackerItem.IsDisposed) Then
                    Return False
                End If

                Return (g_UCVirtualMotionTrackerItem.Parent Is g_UCVirtualMotionTracker.Panel_VMTTrackers)
            End Get
            Set(value As Boolean)
                If (value) Then
                    If (g_UCVirtualMotionTrackerItem Is Nothing OrElse g_UCVirtualMotionTrackerItem.IsDisposed) Then
                        Return
                    End If

                    g_UCVirtualMotionTrackerItem.Parent = g_UCVirtualMotionTracker.Panel_VMTTrackers
                    g_UCVirtualMotionTrackerItem.Dock = DockStyle.Top
                Else
                    g_UCVirtualMotionTrackerItem.Parent = Nothing
                End If
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    If (g_UCVirtualMotionTrackerItem IsNot Nothing AndAlso Not g_UCVirtualMotionTrackerItem.IsDisposed) Then
                        g_UCVirtualMotionTrackerItem.Dispose()
                        g_UCVirtualMotionTrackerItem = Nothing
                    End If
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

    Public Sub New(_mFormMain As FormMain)
        g_mFormMain = _mFormMain

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        Label_ScrollFocus.Text = ""

        g_bIgnoreEvents = False

        g_ClassOscServer = New ClassOscServer
        g_ClassControllerSettings = New ClassControllerSettings(Me)
        g_ClassOscDevices = New ClassOscDevices(Me)

        Try
            g_bIgnoreEvents = True

            ComboBox_TouchpadClickMethod.Items.Clear()
            ComboBox_TouchpadClickMethod.Items.Add("Button Drag (Left Controller: TRIANGLE [▲] / Right Controller: SQUARE [■])")
            ComboBox_TouchpadClickMethod.Items.Add("Button Drag (Both Controllers: SQUARE [■])")
            ComboBox_TouchpadClickMethod.Items.Add("Button Drag (Both Controllers: TRIANGLE [▲])")
            ComboBox_TouchpadClickMethod.Items.Add("While holding MOVE [~] button")

            ComboBox_TouchpadClickMethod.SelectedIndex = 0

            If (ComboBox_TouchpadClickMethod.Items.Count <> ClassControllerSettings.ENUM_HTC_TOUCHPAD_CLICK_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_GrabButtonMethod.Items.Clear()
            ComboBox_GrabButtonMethod.Items.Add("Button Toggle (Left Controller: CIRCLE [O] / Right Controller: CROSS [X])")
            ComboBox_GrabButtonMethod.Items.Add("Button Toggle (Both Controllers: CROSS [X])")
            ComboBox_GrabButtonMethod.Items.Add("Button Toggle (Both Controllers: CIRCLE [O])")
            ComboBox_GrabButtonMethod.Items.Add("Button Holding (Left Controller: CIRCLE [O] / Right Controller: CROSS [X])")
            ComboBox_GrabButtonMethod.Items.Add("Button Holding (Both Controllers: CROSS [X])")
            ComboBox_GrabButtonMethod.Items.Add("Button Holding (Both Controllers: CIRCLE [O])")

            ComboBox_GrabButtonMethod.SelectedIndex = 0

            If (ComboBox_GrabButtonMethod.Items.Count <> ClassControllerSettings.ENUM_HTC_GRIP_BUTTON_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_TouchpadMethod.Items.Clear()
            ComboBox_TouchpadMethod.Items.Add("Use Controller Position")
            ComboBox_TouchpadMethod.Items.Add("Use Controller Orientation")

            ComboBox_TouchpadMethod.SelectedIndex = 0

            If (ComboBox_TouchpadMethod.Items.Count <> ClassControllerSettings.ENUM_HTC_TOUCHPAD_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_RecenterMethod.Items.Clear()
            ComboBox_RecenterMethod.Items.Add("Use Current Controller")
            ComboBox_RecenterMethod.Items.Add("Use PSMoveServiceEx Playspace Orientation")

            ComboBox_RecenterMethod.SelectedIndex = 0

            If (ComboBox_RecenterMethod.Items.Count <> ClassControllerSettings.ENUM_DEVICE_RECENTER_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_RecenterFromDevice.Items.Clear()
            ComboBox_RecenterFromDevice.Items.Add(New ClassRecenterDeviceItem("", "Any Head-Mounted Display"))

            ComboBox_RecenterFromDevice.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_HmdRecenterMethod.Items.Clear()
            ComboBox_HmdRecenterMethod.Items.Add("Use Current Controller")
            ComboBox_HmdRecenterMethod.Items.Add("Use PSMoveServiceEx Playspace Orientation")

            ComboBox_HmdRecenterMethod.SelectedIndex = 0

            If (ComboBox_HmdRecenterMethod.Items.Count <> ClassControllerSettings.ENUM_DEVICE_RECENTER_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_HmdRecenterFromDevice.Items.Clear()
            ComboBox_HmdRecenterFromDevice.Items.Add(New ClassRecenterDeviceItem("", "No Device Selected"))

            ComboBox_HmdRecenterFromDevice.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_PlayCalibForwardMethod.Items.Clear()
            ComboBox_PlayCalibForwardMethod.Items.Add("Use Head-Mounted Display Forward")
            ComboBox_PlayCalibForwardMethod.Items.Add("Use Calibrated Playspace Forward")

            ComboBox_PlayCalibForwardMethod.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
            Dim mItem As New ToolStripMenuItem("Controller ID: " & CStr(i))

            g_mAutostartMenuStrips(i) = mItem

            mItem.Tag = i

            ContextMenuStrip_Autostart.Items.Add(mItem)
        Next

        Try
            g_bIgnoreEvents = True

            ComboBox_PlayCalibControllerID.Items.Clear()
            For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                ComboBox_PlayCalibControllerID.Items.Add(i)
            Next

            ComboBox_PlayCalibControllerID.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        CreateControl()

        Panel_SteamVRRestart.Visible = False

        g_mOscStatusThread = New Threading.Thread(AddressOf OscStatusThread)
        g_mOscStatusThread.IsBackground = True
        g_mOscStatusThread.Start()

        g_mOscDeviceStatusThread = New Threading.Thread(AddressOf OscDeviceStatusThread)
        g_mOscDeviceStatusThread.IsBackground = True
        g_mOscDeviceStatusThread.Start()

        SetPlayspaceCalibrationStatus(ENUM_PLAYSPACE_CALIBRATION_STATUS.IDLE, 0)
    End Sub

    Private Sub UCControllerAttachments_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            g_ClassControllerSettings.LoadSettings()

            Try
                g_bIgnoreEvents = True

                CheckBox_TouchpadShortcuts.Checked = g_ClassControllerSettings.m_JoystickShortcutBinding
                CheckBox_TouchpadShortcutClick.Checked = g_ClassControllerSettings.m_JoystickShortcutTouchpadClick
                CheckBox_DisableBasestations.Checked = g_ClassControllerSettings.m_DisableBaseStationSpawning
                CheckBox_EnableHeptics.Checked = g_ClassControllerSettings.m_EnableHepticFeedback
                ComboBox_TouchpadClickMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_TouchpadClickMethod.Items.Count - 1, g_ClassControllerSettings.m_HtcTouchpadEmulationClickMethod))
                ComboBox_GrabButtonMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_GrabButtonMethod.Items.Count - 1, g_ClassControllerSettings.m_HtcGripButtonMethod))
                CheckBox_TouchpadClampBounds.Checked = g_ClassControllerSettings.m_HtcClampTouchpadToBounds
                ComboBox_TouchpadMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_TouchpadMethod.Items.Count - 1, g_ClassControllerSettings.m_HtcTouchpadMethod))

                CheckBox_ControllerRecenterEnabled.Checked = g_ClassControllerSettings.m_EnableControllerRecenter
                ComboBox_RecenterMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_RecenterMethod.Items.Count - 1, g_ClassControllerSettings.m_ControllerRecenterMethod))

                ComboBox_RecenterFromDevice.Items.Clear()
                ComboBox_RecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(g_ClassControllerSettings.m_ControllerRecenterFromDeviceName, "Any Head-Mounted Display"))
                ComboBox_RecenterFromDevice.SelectedIndex = 0

                CheckBox_HmdRecenterEnabled.Checked = g_ClassControllerSettings.m_EnableHmdRecenter
                ComboBox_HmdRecenterMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_HmdRecenterMethod.Items.Count - 1, g_ClassControllerSettings.m_HmdRecenterMethod))

                ComboBox_HmdRecenterFromDevice.Items.Clear()
                ComboBox_HmdRecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(g_ClassControllerSettings.m_HmdRecenterFromDeviceName, "No Device Selected"))
                ComboBox_HmdRecenterFromDevice.SelectedIndex = 0

                NumericUpDown_RecenterButtonTime.Value = Math.Max(NumericUpDown_RecenterButtonTime.Minimum, Math.Min(NumericUpDown_RecenterButtonTime.Maximum, g_ClassControllerSettings.m_RecenterButtonTimeMs))
                NumericUpDown_OscThreadSleep.Value = Math.Max(NumericUpDown_OscThreadSleep.Minimum, Math.Min(NumericUpDown_OscThreadSleep.Maximum, g_ClassControllerSettings.m_OscThreadSleepMs))

                NumericUpDown_TouchpadClickDeadzone.Value = CDec(Math.Max(NumericUpDown_TouchpadClickDeadzone.Minimum, Math.Min(NumericUpDown_TouchpadClickDeadzone.Maximum, g_ClassControllerSettings.m_HtcTouchpadClickDeadzone)))
                NumericUpDown_TouchpadTouchArea.Value = CDec(Math.Max(NumericUpDown_TouchpadTouchArea.Minimum, Math.Min(NumericUpDown_TouchpadTouchArea.Maximum, g_ClassControllerSettings.m_HtcTouchpadTouchAreaCm)))

                CheckBox_PlayCalibEnabled.Checked = g_ClassControllerSettings.m_EnablePlayspaceRecenter
                NumericUpDown_PlayCalibForwardOffset.Value = CDec(Math.Max(NumericUpDown_PlayCalibForwardOffset.Minimum, Math.Min(NumericUpDown_PlayCalibForwardOffset.Maximum, g_ClassControllerSettings.m_PlayspaceSettings.m_ForwardOffset)))
                NumericUpDown_PlayCalibHeightOffset.Value = CDec(Math.Max(NumericUpDown_PlayCalibHeightOffset.Minimum, Math.Min(NumericUpDown_PlayCalibHeightOffset.Maximum, g_ClassControllerSettings.m_PlayspaceSettings.m_HeightOffset)))
                ComboBox_PlayCalibForwardMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_PlayCalibForwardMethod.Items.Count - 1, g_ClassControllerSettings.m_PlayspaceSettings.m_ForwardMethod))

                g_ClassControllerSettings.SetUnsavedState(False)
            Finally
                g_bIgnoreEvents = False
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            AutostartLoad()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            RefreshOverrides()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub LinkLabel_ReadMore_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ReadMore.LinkClicked
        Dim mMsg As New FormRtfHelp
        mMsg.RichTextBox_Help.Rtf = My.Resources.HelpVirtualMotionTracker
        mMsg.ShowDialog(Me)
    End Sub

    Private Sub CleanUp()
        If (g_mPlayspaceCalibrationThread IsNot Nothing AndAlso g_mPlayspaceCalibrationThread.IsAlive) Then
            g_mPlayspaceCalibrationThread.Abort()
            g_mPlayspaceCalibrationThread.Join()
            g_mPlayspaceCalibrationThread = Nothing
        End If

        If (g_mOscDeviceStatusThread IsNot Nothing AndAlso g_mOscDeviceStatusThread.IsAlive) Then
            g_mOscDeviceStatusThread.Abort()
            g_mOscDeviceStatusThread.Join()
            g_mOscDeviceStatusThread = Nothing
        End If

        If (g_mOscStatusThread IsNot Nothing AndAlso g_mOscStatusThread.IsAlive) Then
            g_mOscStatusThread.Abort()
            g_mOscStatusThread.Join()
            g_mOscStatusThread = Nothing
        End If

        For Each mItem As ListViewItem In ListView_Trackers.Items
            Dim mTrackerItem = DirectCast(mItem, ClassTrackersListViewItem)

            mTrackerItem.Dispose()
        Next
        ListView_Trackers.Items.Clear()

        For Each mItem In g_mAutostartMenuStrips
            If (mItem.Value IsNot Nothing AndAlso Not mItem.Value.IsDisposed) Then
                mItem.Value.Dispose()
            End If
        Next
        g_mAutostartMenuStrips.Clear()

        If (g_ClassOscDevices IsNot Nothing) Then
            g_ClassOscDevices.Dispose()
            g_ClassOscDevices = Nothing
        End If

        If (g_ClassOscServer IsNot Nothing) Then
            g_ClassOscServer.Dispose()
            g_ClassOscServer = Nothing
        End If
    End Sub

    Class ClassOscServer
        Implements IDisposable

        Shared _ThreadLock As New Object
        Private g_VmtOsc As ClassOSC = Nothing

        Public Event OnOscProcessBundle(mBundle As OscBundle)
        Public Event OnOscProcessMessage(mMessage As OscMessage)

        Public Event OnSuspendChanged()

        Private g_bSuspendRequest As Boolean = False
        Private g_mLastResponse As Date


        Public Sub New()
        End Sub

        Public Sub StartServer()
            SyncLock _ThreadLock
                If (g_VmtOsc IsNot Nothing) Then
                    Return
                End If

                g_VmtOsc = New ClassOSC(ClassVmtConst.VMT_IP, ClassVmtConst.VMT_PORT_RECEIVE, ClassVmtConst.VMT_PORT_SEND, AddressOf __OnOscProcessBundle, AddressOf __OnOscProcessMessage)
            End SyncLock

            RaiseEvent OnSuspendChanged()
        End Sub

        Property m_SuspendRequests As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bSuspendRequest
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bSuspendRequest = value
                End SyncLock

                RaiseEvent OnSuspendChanged()
            End Set
        End Property

        ReadOnly Property m_LastResponse As Date
            Get
                SyncLock _ThreadLock
                    Return g_mLastResponse
                End SyncLock
            End Get
        End Property

        Public Sub Send(mPacket As OscPacket)
            If (m_SuspendRequests) Then
                Return
            End If

            g_VmtOsc.Send(mPacket)
        End Sub

        Public Function IsRunning() As Boolean
            SyncLock _ThreadLock
                Return (g_VmtOsc IsNot Nothing)
            End SyncLock
        End Function

        Private Sub __OnOscProcessBundle(mBundle As OscBundle)
            If (m_SuspendRequests) Then
                Return
            End If

            SyncLock _ThreadLock
                g_mLastResponse = Now
            End SyncLock

            RaiseEvent OnOscProcessBundle(mBundle)
        End Sub

        Private Sub __OnOscProcessMessage(mMessage As OscMessage)
            If (m_SuspendRequests) Then
                Return
            End If

            SyncLock _ThreadLock
                g_mLastResponse = Now
            End SyncLock

            RaiseEvent OnOscProcessMessage(mMessage)
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    If (g_VmtOsc IsNot Nothing) Then
                        g_VmtOsc.Dispose()
                        g_VmtOsc = Nothing
                    End If

                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    Class ClassControllerSettings
        Private g_UCVirtualMotionTracker As UCVirtualMotionTracker
        Private Shared _ThreadLock As New Object

        Private g_bSettingsLoaded As Boolean = False

        Enum ENUM_HTC_TOUCHPAD_CLICK_METHOD
            BUTTON_MIRRORED
            BUTTON_STRICT_SQUARE
            BUTTON_STRICT_TRIANGLE
            MOVE_ALWAYS

            __MAX
        End Enum

        Enum ENUM_HTC_GRIP_BUTTON_METHOD
            BUTTON_TOGGLE_MIRRORED
            BUTTON_TOGGLE_STRICT_CROSS
            BUTTON_TOGGLE_STRICT_CIRCLE
            BUTTON_HOLDING_MIRRORED
            BUTTON_HOLDING_STRICT_CROSS
            BUTTON_HOLDING_STRICT_CIRCLE

            __MAX
        End Enum

        Enum ENUM_HTC_TOUCHPAD_METHOD
            USE_POSITION
            USE_ORIENTATION

            __MAX
        End Enum

        Enum ENUM_DEVICE_RECENTER_METHOD
            USE_DEVICE
            USE_PLAYSPACE

            __MAX
        End Enum

        Private g_bTouchpadShortcutBinding As Boolean = False
        Private g_bTouchpadShortcutTouchpadClick As Boolean = False
        Private g_iHtcTouchpadEmulationClickMethod As ENUM_HTC_TOUCHPAD_CLICK_METHOD = ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED
        Private g_iHtcGripButtonMethod As ENUM_HTC_GRIP_BUTTON_METHOD = ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_MIRRORED
        Private g_bDisableBaseStationSpawning As Boolean = False
        Private g_bEnableHepticFeedback As Boolean = True
        Private g_bHtcClampTouchpadToBounds As Boolean = True
        Private g_iHtcTouchpadMethod As ENUM_HTC_TOUCHPAD_METHOD = ENUM_HTC_TOUCHPAD_METHOD.USE_POSITION

        Private g_bEnableControllerRecenter As Boolean = True
        Private g_iControllerRecenterMethod As ENUM_DEVICE_RECENTER_METHOD = ENUM_DEVICE_RECENTER_METHOD.USE_DEVICE
        Private g_sControllerRecenterFromDeviceName As String = ""
        Private g_bEnableHmdRecenter As Boolean = True
        Private g_iHmdRecenterMethod As ENUM_DEVICE_RECENTER_METHOD = ENUM_DEVICE_RECENTER_METHOD.USE_DEVICE
        Private g_sHmdRecenterFromDeviceName As String = ""
        Private g_iRecenterButtonTimeMs As Long = 500
        Private g_iOscThreadSleepMs As Long = 1
        Private g_iTouchpadTouchAreaCm As Single = 7.5F
        Private g_iTouchpadClickDeadzone As Single = 0.25F
        Private g_mPlaySpaceSettings As STRUC_PLAYSPACE_SETTINGS
        Private g_bEnablePlayspaceRecenter As Boolean

        Class STRUC_PLAYSPACE_SETTINGS
            Enum ENUM_FORWARD_METHOD
                USE_HMD_FORWARD
                USE_PLAYSPACE_FORRWARD
            End Enum

            Public Property m_PosOffset As Vector3
            Public Property m_AngOffset As Quaternion
            Public Property m_HmdAngOffset As Quaternion

            Public Property m_PointControllerBeginPos As Vector3
            Public Property m_PointControllerEndPos As Vector3
            Public Property m_PointHmdBeginPos As Vector3
            Public Property m_PointHmdEndPos As Vector3

            Public Property m_Valid As Boolean

            Public Property m_ForwardOffset As Single
            Public Property m_HeightOffset As Single
            Public Property m_ForwardMethod As ENUM_FORWARD_METHOD

            Public Sub Reset()
                m_Valid = False

                m_PosOffset = New Vector3
                m_AngOffset = Quaternion.Identity
                m_HmdAngOffset = Quaternion.Identity

                m_PointControllerBeginPos = New Vector3
                m_PointControllerEndPos = New Vector3
                m_PointHmdBeginPos = New Vector3
                m_PointHmdEndPos = New Vector3
            End Sub
        End Class

        Public Sub New(_UCVirtualMotionTracker As UCVirtualMotionTracker)
            g_UCVirtualMotionTracker = _UCVirtualMotionTracker

            g_mPlaySpaceSettings = New STRUC_PLAYSPACE_SETTINGS()
            g_mPlaySpaceSettings.Reset()
        End Sub

        Property m_JoystickShortcutBinding As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bTouchpadShortcutBinding
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bTouchpadShortcutBinding = value
                End SyncLock
            End Set
        End Property

        Property m_JoystickShortcutTouchpadClick As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bTouchpadShortcutTouchpadClick
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bTouchpadShortcutTouchpadClick = value
                End SyncLock
            End Set
        End Property

        Property m_HtcTouchpadEmulationClickMethod As ENUM_HTC_TOUCHPAD_CLICK_METHOD
            Get
                SyncLock _ThreadLock
                    Return g_iHtcTouchpadEmulationClickMethod
                End SyncLock
            End Get
            Set(value As ENUM_HTC_TOUCHPAD_CLICK_METHOD)
                If (value < 0) Then
                    value = 0
                End If

                If (value > ENUM_HTC_TOUCHPAD_CLICK_METHOD.__MAX - 1) Then
                    value = CType(ENUM_HTC_TOUCHPAD_CLICK_METHOD.__MAX - 1, ENUM_HTC_TOUCHPAD_CLICK_METHOD)
                End If

                SyncLock _ThreadLock
                    g_iHtcTouchpadEmulationClickMethod = value
                End SyncLock
            End Set
        End Property

        Property m_HtcGripButtonMethod As ENUM_HTC_GRIP_BUTTON_METHOD
            Get
                SyncLock _ThreadLock
                    Return g_iHtcGripButtonMethod
                End SyncLock
            End Get
            Set(value As ENUM_HTC_GRIP_BUTTON_METHOD)
                If (value < 0) Then
                    value = 0
                End If

                If (value > ENUM_HTC_GRIP_BUTTON_METHOD.__MAX - 1) Then
                    value = CType(ENUM_HTC_GRIP_BUTTON_METHOD.__MAX - 1, ENUM_HTC_GRIP_BUTTON_METHOD)
                End If

                SyncLock _ThreadLock
                    g_iHtcGripButtonMethod = value
                End SyncLock
            End Set
        End Property

        Property m_DisableBaseStationSpawning As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bDisableBaseStationSpawning
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bDisableBaseStationSpawning = value
                End SyncLock
            End Set
        End Property

        Property m_EnableHepticFeedback As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bEnableHepticFeedback
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bEnableHepticFeedback = value
                End SyncLock
            End Set
        End Property

        Property m_HtcClampTouchpadToBounds As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bHtcClampTouchpadToBounds
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bHtcClampTouchpadToBounds = value
                End SyncLock
            End Set
        End Property

        Property m_HtcTouchpadMethod As ENUM_HTC_TOUCHPAD_METHOD
            Get
                SyncLock _ThreadLock
                    Return g_iHtcTouchpadMethod
                End SyncLock
            End Get
            Set(value As ENUM_HTC_TOUCHPAD_METHOD)
                SyncLock _ThreadLock
                    g_iHtcTouchpadMethod = value
                End SyncLock
            End Set
        End Property

        Property m_EnableControllerRecenter As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bEnableControllerRecenter
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bEnableControllerRecenter = value
                End SyncLock
            End Set
        End Property

        Property m_ControllerRecenterMethod As ENUM_DEVICE_RECENTER_METHOD
            Get
                SyncLock _ThreadLock
                    Return g_iControllerRecenterMethod
                End SyncLock
            End Get
            Set(value As ENUM_DEVICE_RECENTER_METHOD)
                SyncLock _ThreadLock
                    g_iControllerRecenterMethod = value
                End SyncLock
            End Set
        End Property

        Property m_ControllerRecenterFromDeviceName As String
            Get
                SyncLock _ThreadLock
                    Return g_sControllerRecenterFromDeviceName
                End SyncLock
            End Get
            Set(value As String)
                SyncLock _ThreadLock
                    g_sControllerRecenterFromDeviceName = value
                End SyncLock
            End Set
        End Property

        Property m_EnableHmdRecenter As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bEnableHmdRecenter
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bEnableHmdRecenter = value
                End SyncLock
            End Set
        End Property

        Property m_HmdRecenterMethod As ENUM_DEVICE_RECENTER_METHOD
            Get
                SyncLock _ThreadLock
                    Return g_iHmdRecenterMethod
                End SyncLock
            End Get
            Set(value As ENUM_DEVICE_RECENTER_METHOD)
                SyncLock _ThreadLock
                    g_iHmdRecenterMethod = value
                End SyncLock
            End Set
        End Property

        Property m_HmdRecenterFromDeviceName As String
            Get
                SyncLock _ThreadLock
                    Return g_sHmdRecenterFromDeviceName
                End SyncLock
            End Get
            Set(value As String)
                SyncLock _ThreadLock
                    g_sHmdRecenterFromDeviceName = value
                End SyncLock
            End Set
        End Property

        Property m_RecenterButtonTimeMs As Long
            Get
                SyncLock _ThreadLock
                    Return g_iRecenterButtonTimeMs
                End SyncLock
            End Get
            Set(value As Long)
                SyncLock _ThreadLock
                    If (value < 1) Then
                        value = 1
                    End If
                    If (value > 10000) Then
                        value = 10000
                    End If

                    g_iRecenterButtonTimeMs = value
                End SyncLock
            End Set
        End Property

        Property m_OscThreadSleepMs As Long
            Get
                SyncLock _ThreadLock
                    Return g_iOscThreadSleepMs
                End SyncLock
            End Get
            Set(value As Long)
                SyncLock _ThreadLock
                    If (value < 1) Then
                        value = 1
                    End If
                    If (value > 100) Then
                        value = 100
                    End If

                    g_iOscThreadSleepMs = value
                End SyncLock
            End Set
        End Property

        Property m_HtcTouchpadTouchAreaCm As Single
            Get
                SyncLock _ThreadLock
                    Return g_iTouchpadTouchAreaCm
                End SyncLock
            End Get
            Set(value As Single)
                SyncLock _ThreadLock
                    If (value < 1.0F) Then
                        value = 1.0F
                    End If
                    If (value > 100.0F) Then
                        value = 100.0F
                    End If

                    g_iTouchpadTouchAreaCm = value
                End SyncLock
            End Set
        End Property

        Property m_HtcTouchpadClickDeadzone As Single
            Get
                SyncLock _ThreadLock
                    Return g_iTouchpadClickDeadzone
                End SyncLock
            End Get
            Set(value As Single)
                SyncLock _ThreadLock
                    If (value < 0.0F) Then
                        value = 0.0F
                    End If
                    If (value > 1.0F) Then
                        value = 1.0F
                    End If

                    g_iTouchpadClickDeadzone = value
                End SyncLock
            End Set
        End Property

        Property m_EnablePlayspaceRecenter As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bEnablePlayspaceRecenter
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bEnablePlayspaceRecenter = value
                End SyncLock
            End Set
        End Property

        ReadOnly Property m_PlayspaceSettings As STRUC_PLAYSPACE_SETTINGS
            Get
                SyncLock _ThreadLock
                    Return g_mPlaySpaceSettings
                End SyncLock
            End Get
        End Property

        Public Sub LoadSettings()
            g_bSettingsLoaded = True

            Dim tmpSng As Single
            Dim tmpInt As Integer
            Dim tmpLng As Long
            Dim tmpVec3 As Vector3
            Dim tmpQuat As Quaternion

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)

                    ' Controller Settings
                    m_JoystickShortcutBinding = (mIni.ReadKeyValue("ControllerSettings", "JoystickShortcutBindings", "false") = "true")
                    m_JoystickShortcutTouchpadClick = (mIni.ReadKeyValue("ControllerSettings", "JoystickShortcutTouchpadClick", "false") = "true")
                    m_DisableBaseStationSpawning = (mIni.ReadKeyValue("ControllerSettings", "DisableBaseStationSpawning", "false") = "true")
                    m_EnableHepticFeedback = (mIni.ReadKeyValue("ControllerSettings", "EnableHepticFeedback", "true") = "true")

                    If (Integer.TryParse(mIni.ReadKeyValue("ControllerSettings", "HtcTouchpadEmulationClickMethod", CStr(CInt(ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED))), tmpInt)) Then
                        m_HtcTouchpadEmulationClickMethod = CType(tmpInt, ENUM_HTC_TOUCHPAD_CLICK_METHOD)
                    End If

                    If (Integer.TryParse(mIni.ReadKeyValue("ControllerSettings", "HtcGripButtonMethod", CStr(CInt(ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_MIRRORED))), tmpInt)) Then
                        m_HtcGripButtonMethod = CType(tmpInt, ENUM_HTC_GRIP_BUTTON_METHOD)
                    End If

                    m_HtcClampTouchpadToBounds = (mIni.ReadKeyValue("ControllerSettings", "HtcClampTouchpadToBounds", "true") = "true")

                    If (Integer.TryParse(mIni.ReadKeyValue("ControllerSettings", "HtcTouchpadMethod", CStr(CInt(ENUM_HTC_TOUCHPAD_METHOD.USE_POSITION))), tmpInt)) Then
                        m_HtcTouchpadMethod = CType(tmpInt, ENUM_HTC_TOUCHPAD_METHOD)
                    End If

                    m_EnableControllerRecenter = (mIni.ReadKeyValue("ControllerSettings", "EnableControllerRecenter", "true") = "true")

                    If (Integer.TryParse(mIni.ReadKeyValue("ControllerSettings", "ControllerRecenterMethod", CStr(CInt(ENUM_DEVICE_RECENTER_METHOD.USE_DEVICE))), tmpInt)) Then
                        m_ControllerRecenterMethod = CType(tmpInt, ENUM_DEVICE_RECENTER_METHOD)
                    End If

                    m_ControllerRecenterFromDeviceName = mIni.ReadKeyValue("ControllerSettings", "ControllerRecenterFromDeviceName", "")

                    m_EnableHmdRecenter = (mIni.ReadKeyValue("ControllerSettings", "EnableHmdRecenter", "true") = "true")

                    If (Integer.TryParse(mIni.ReadKeyValue("ControllerSettings", "HmdRecenterMethod", CStr(CInt(ENUM_DEVICE_RECENTER_METHOD.USE_DEVICE))), tmpInt)) Then
                        m_HmdRecenterMethod = CType(tmpInt, ENUM_DEVICE_RECENTER_METHOD)
                    End If

                    m_HmdRecenterFromDeviceName = mIni.ReadKeyValue("ControllerSettings", "HmdRecenterFromDeviceName", "")

                    If (Long.TryParse(mIni.ReadKeyValue("ControllerSettings", "RecenterButtonTimeMs", "500"), tmpLng)) Then
                        m_RecenterButtonTimeMs = tmpLng
                    End If

                    If (Long.TryParse(mIni.ReadKeyValue("ControllerSettings", "OscThreadSleepMs", "1"), tmpLng)) Then
                        m_OscThreadSleepMs = tmpLng
                    End If

                    If (Single.TryParse(mIni.ReadKeyValue("ControllerSettings", "HtcTouchpadTouchAreaCm", "7.5"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        m_HtcTouchpadTouchAreaCm = tmpSng
                    End If

                    If (Single.TryParse(mIni.ReadKeyValue("ControllerSettings", "HtcTouchpadClickDeadzone", "0.25"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        m_HtcTouchpadClickDeadzone = tmpSng
                    End If

                    m_EnablePlayspaceRecenter = (mIni.ReadKeyValue("ControllerSettings", "EnablePlayspaceRecenter", "true") = "true")

                    ' Playspace Settings
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceSettings", "ForwardOffset", "10.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        m_PlayspaceSettings.m_ForwardOffset = tmpSng
                    End If

                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceSettings", "HeightOffset", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        m_PlayspaceSettings.m_HeightOffset = tmpSng
                    End If

                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceSettings", "ForwardMethod", "0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        m_PlayspaceSettings.m_ForwardMethod = CType(tmpSng, STRUC_PLAYSPACE_SETTINGS.ENUM_FORWARD_METHOD)
                    End If


                    ' Playspace Calibration Settings
                    tmpVec3 = New Vector3
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PosOffsetX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.X = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PosOffsetY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.Y = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PosOffsetZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.Z = tmpSng
                    End If
                    m_PlayspaceSettings.m_PosOffset = tmpVec3

                    tmpQuat = New Quaternion
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "AngOffsetX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpQuat.X = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "AngOffsetY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpQuat.Y = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "AngOffsetZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpQuat.Z = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "AngOffsetW", "1.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpQuat.W = tmpSng
                    End If
                    m_PlayspaceSettings.m_AngOffset = tmpQuat

                    tmpVec3 = New Vector3
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerBeginPosX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.X = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerBeginPosY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.Y = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerBeginPosZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.Z = tmpSng
                    End If
                    m_PlayspaceSettings.m_PointControllerBeginPos = tmpVec3

                    tmpVec3 = New Vector3
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerEndPosX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.X = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerEndPosY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.Y = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerEndPosZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.Z = tmpSng
                    End If
                    m_PlayspaceSettings.m_PointControllerEndPos = tmpVec3

                    tmpVec3 = New Vector3
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdBeginPosX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.X = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdBeginPosY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.Y = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdBeginPosZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.Z = tmpSng
                    End If
                    m_PlayspaceSettings.m_PointHmdBeginPos = tmpVec3

                    tmpVec3 = New Vector3
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdEndPosX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.X = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdEndPosY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.Y = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdEndPosZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpVec3.Z = tmpSng
                    End If
                    m_PlayspaceSettings.m_PointHmdEndPos = tmpVec3

                    tmpQuat = New Quaternion
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "HmdAngOffsetX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpQuat.X = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "HmdAngOffsetY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpQuat.Y = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "HmdAngOffsetZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpQuat.Z = tmpSng
                    End If
                    If (Single.TryParse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "HmdAngOffsetW", "1.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, tmpSng)) Then
                        tmpQuat.W = tmpSng
                    End If
                    m_PlayspaceSettings.m_HmdAngOffset = tmpQuat

                    m_PlayspaceSettings.m_Valid = (mIni.ReadKeyValue("PlayspaceCalibrationSettings", "Valid", "false") = "true")

                End Using
            End Using
        End Sub

        Public Sub SaveSettings(iSaveFlags As ENUM_SETTINGS_SAVE_TYPE_FLAGS)
            If (Not g_bSettingsLoaded) Then
                Return
            End If

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                    If ((iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.CONTROLLER) <> 0 OrElse (iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.ALL) <> 0) Then
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "JoystickShortcutBindings", If(m_JoystickShortcutBinding, "true", "false")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "JoystickShortcutTouchpadClick", If(m_JoystickShortcutTouchpadClick, "true", "false")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "DisableBaseStationSpawning", If(m_DisableBaseStationSpawning, "true", "false")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "EnableHepticFeedback", If(m_EnableHepticFeedback, "true", "false")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcTouchpadEmulationClickMethod", CStr(CInt(m_HtcTouchpadEmulationClickMethod))))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcGripButtonMethod", CStr(CInt(m_HtcGripButtonMethod))))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcClampTouchpadToBounds", If(m_HtcClampTouchpadToBounds, "true", "false")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcTouchpadMethod", CStr(CInt(m_HtcTouchpadMethod))))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "EnableControllerRecenter", If(m_EnableControllerRecenter, "true", "false")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "ControllerRecenterMethod", CStr(CInt(m_ControllerRecenterMethod))))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "ControllerRecenterFromDeviceName", m_ControllerRecenterFromDeviceName))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "EnableHmdRecenter", If(m_EnableHmdRecenter, "true", "false")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HmdRecenterMethod", CStr(CInt(m_HmdRecenterMethod))))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HmdRecenterFromDeviceName", m_HmdRecenterFromDeviceName))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "RecenterButtonTimeMs", CStr(m_RecenterButtonTimeMs)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "OscThreadSleepMs", CStr(m_OscThreadSleepMs)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcTouchpadTouchAreaCm", m_HtcTouchpadTouchAreaCm.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcTouchpadClickDeadzone", m_HtcTouchpadClickDeadzone.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "EnablePlayspaceRecenter", If(m_EnablePlayspaceRecenter, "true", "false")))
                    End If

                    If ((iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.PLAYSPACE) <> 0 OrElse (iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.ALL) <> 0) Then
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceSettings", "ForwardOffset", m_PlayspaceSettings.m_ForwardOffset.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceSettings", "HeightOffset", m_PlayspaceSettings.m_HeightOffset.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceSettings", "ForwardMethod", CStr(CInt(m_PlayspaceSettings.m_ForwardMethod))))
                    End If

                    If ((iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.PLAYSPACE_CALIBRATION) <> 0 OrElse (iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.ALL) <> 0) Then
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PosOffsetX", m_PlayspaceSettings.m_PosOffset.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PosOffsetY", m_PlayspaceSettings.m_PosOffset.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PosOffsetZ", m_PlayspaceSettings.m_PosOffset.Z.ToString(Globalization.CultureInfo.InvariantCulture)))

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "AngOffsetX", m_PlayspaceSettings.m_AngOffset.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "AngOffsetY", m_PlayspaceSettings.m_AngOffset.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "AngOffsetZ", m_PlayspaceSettings.m_AngOffset.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "AngOffsetW", m_PlayspaceSettings.m_AngOffset.W.ToString(Globalization.CultureInfo.InvariantCulture)))

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointControllerBeginPosX", m_PlayspaceSettings.m_PointControllerBeginPos.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointControllerBeginPosY", m_PlayspaceSettings.m_PointControllerBeginPos.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointControllerBeginPosZ", m_PlayspaceSettings.m_PointControllerBeginPos.Z.ToString(Globalization.CultureInfo.InvariantCulture)))

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointControllerEndPosX", m_PlayspaceSettings.m_PointControllerEndPos.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointControllerEndPosY", m_PlayspaceSettings.m_PointControllerEndPos.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointControllerEndPosZ", m_PlayspaceSettings.m_PointControllerEndPos.Z.ToString(Globalization.CultureInfo.InvariantCulture)))

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointHmdBeginPosX", m_PlayspaceSettings.m_PointHmdBeginPos.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointHmdBeginPosY", m_PlayspaceSettings.m_PointHmdBeginPos.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointHmdBeginPosZ", m_PlayspaceSettings.m_PointHmdBeginPos.Z.ToString(Globalization.CultureInfo.InvariantCulture)))

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointHmdEndPosX", m_PlayspaceSettings.m_PointHmdEndPos.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointHmdEndPosY", m_PlayspaceSettings.m_PointHmdEndPos.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "PointHmdEndPosZ", m_PlayspaceSettings.m_PointHmdEndPos.Z.ToString(Globalization.CultureInfo.InvariantCulture)))

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "HmdAngOffsetX", m_PlayspaceSettings.m_HmdAngOffset.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "HmdAngOffsetY", m_PlayspaceSettings.m_HmdAngOffset.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "HmdAngOffsetZ", m_PlayspaceSettings.m_HmdAngOffset.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "HmdAngOffsetW", m_PlayspaceSettings.m_HmdAngOffset.W.ToString(Globalization.CultureInfo.InvariantCulture)))

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "Valid", If(m_PlayspaceSettings.m_Valid, "true", "false")))
                    End If

                    mIni.WriteKeyValue(mIniContent.ToArray)
                End Using
            End Using
        End Sub

        Public Sub SetUnsavedState(bIsUnsaved As Boolean)
            If (bIsUnsaved) Then
                g_UCVirtualMotionTracker.Button_SaveControllerSettings.Text = String.Format("Save Settings*")
                g_UCVirtualMotionTracker.Button_SaveControllerSettings.Font = New Font(g_UCVirtualMotionTracker.Button_SaveControllerSettings.Font, FontStyle.Bold)
            Else
                g_UCVirtualMotionTracker.Button_SaveControllerSettings.Text = String.Format("Save Settings")
                g_UCVirtualMotionTracker.Button_SaveControllerSettings.Font = New Font(g_UCVirtualMotionTracker.Button_SaveControllerSettings.Font, FontStyle.Regular)
            End If
        End Sub
    End Class

    Class ClassOscDevices
        Implements IDisposable

        Shared _ThreadLock As New Object
        Private g_UCVirtualMotionTracker As UCVirtualMotionTracker
        Private g_mDevicesThread As Threading.Thread = Nothing

        Event OnDeviceArrived(mDevice As STRUC_DEVICE)
        Event OnDeviceRemoved(mDevice As STRUC_DEVICE)
        Event OnDevicePose(mDevice As STRUC_DEVICE)

        Structure STRUC_DEVICE
            Enum ENUM_DEVICE_TYPE
                INVALID
                HMD
                CONTROLLER
                TRACKER
                REFERENCE
                REDIRECT
            End Enum

            Public Sub New(_Index As Integer, _Type As ENUM_DEVICE_TYPE, _Serial As String)
                iIndex = _Index
                iType = _Type
                sSerial = _Serial
                mPos = New Vector3()
                mOrientation = Quaternion.Identity
            End Sub

            Public Sub New(_Index As Integer, _Type As ENUM_DEVICE_TYPE, _Serial As String, _Pos As Vector3, _Orientation As Quaternion)
                iIndex = _Index
                iType = _Type
                sSerial = _Serial
                mPos = _Pos
                mOrientation = _Orientation
            End Sub

            Dim iIndex As Integer
            Dim iType As ENUM_DEVICE_TYPE
            Dim sSerial As String
            Dim mPos As Vector3
            Dim mOrientation As Quaternion

            Public Function GetPosCm() As Vector3
                Return New Vector3(
                    mPos.X * CSng(PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSM_METERS_TO_CENTIMETERS),
                    mPos.Y * CSng(PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSM_METERS_TO_CENTIMETERS),
                    mPos.Z * CSng(PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSM_METERS_TO_CENTIMETERS))
            End Function

            Public Function GetOrientationEuler() As Vector3
                Return ClassQuaternionTools.FromQ(mOrientation)
            End Function

            Dim mLastPoseTimestamp As Date
        End Structure

        Private g_mDevicesDic As New Dictionary(Of String, STRUC_DEVICE)

        Public Sub New(mUCVirtualMotionTracker As UCVirtualMotionTracker)
            g_UCVirtualMotionTracker = mUCVirtualMotionTracker

            AddHandler g_UCVirtualMotionTracker.g_ClassOscServer.OnOscProcessMessage, AddressOf OnOscMessage
        End Sub

        Public Sub StartThread()
            If (g_mDevicesThread IsNot Nothing AndAlso g_mDevicesThread.IsAlive) Then
                Return
            End If

            g_mDevicesThread = New Threading.Thread(AddressOf DevicesThread)
            g_mDevicesThread.IsBackground = True
            g_mDevicesThread.Start()
        End Sub

        Private Sub DevicesThread()
            Dim mLastDeviceList As Date = Now

            While True
                Try
                    If (Not g_UCVirtualMotionTracker.g_ClassOscServer.IsRunning OrElse g_UCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
                        Threading.Thread.Sleep(1000)
                        Continue While
                    End If

                    ' Request Device List
                    Dim mLastDeviceListTimespan = Now - mLastDeviceList

                    If (mLastDeviceListTimespan.TotalMilliseconds > 1000) Then
                        mLastDeviceList = Now

                        g_UCVirtualMotionTracker.g_ClassOscServer.Send(New OscMessage("/VMT/GetDevicesList"))
                    End If

                    ' Request Device Pose 
                    SyncLock _ThreadLock
                        For Each mDevice In g_mDevicesDic
                            g_UCVirtualMotionTracker.g_ClassOscServer.Send(New OscMessage("/VMT/GetDevicePose", New Object() {mDevice.Value.sSerial}))
                        Next
                    End SyncLock
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception

                End Try

                ClassPrecisionSleep.Sleep(CInt(g_UCVirtualMotionTracker.g_ClassControllerSettings.m_OscThreadSleepMs))
            End While
        End Sub

        Private Sub OnOscMessage(mMessage As OscMessage)
            Try
                Select Case (mMessage.Address)
                    Case "/VMT/Out/DevicesList"
                        If (mMessage.Count < 1) Then
                            Return
                        End If

                        Dim sDevices As String = CStr(mMessage(0))

                        Dim mDeviceList As New List(Of STRUC_DEVICE)

                        For Each sDevice As String In sDevices.Split(New String() {vbCrLf, vbLf}, 0)
                            Dim sInfos As String() = sDevice.Split(":"c)
                            If (sInfos.Length < 3) Then
                                Continue For
                            End If

                            Dim iIndex As Integer = -1
                            Dim iType As Integer = -1
                            Dim sSerial As String = ""

                            For i = 0 To sInfos.Length - 1
                                Select Case (i)
                                    Case 0
                                        iIndex = CInt(sInfos(i))
                                    Case 1
                                        iType = CInt(sInfos(i))
                                    Case Else
                                        sSerial &= sInfos(i)
                                End Select
                            Next

                            Dim mDevice As New STRUC_DEVICE(iIndex, CType(iType, STRUC_DEVICE.ENUM_DEVICE_TYPE), sSerial)

                            Select Case (mDevice.iType)
                                Case STRUC_DEVICE.ENUM_DEVICE_TYPE.CONTROLLER,
                                        STRUC_DEVICE.ENUM_DEVICE_TYPE.HMD,
                                        STRUC_DEVICE.ENUM_DEVICE_TYPE.TRACKER

                                    mDeviceList.Add(mDevice)
                            End Select
                        Next


                        SyncLock _ThreadLock
                            ' Add missing devices
                            For Each mDevice As STRUC_DEVICE In mDeviceList
                                If (Not g_mDevicesDic.ContainsKey(mDevice.sSerial)) Then
                                    g_mDevicesDic(mDevice.sSerial) = mDevice

                                    RaiseEvent OnDeviceArrived(mDevice)
                                End If
                            Next

                            ' Remove devices that do not exist anymore
                            For Each mDevice In g_mDevicesDic
                                If (mDeviceList.Exists(Function(x As STRUC_DEVICE) x.sSerial = mDevice.Value.sSerial)) Then
                                    Continue For
                                End If

                                RaiseEvent OnDeviceRemoved(mDevice.Value)

                                g_mDevicesDic.Remove(mDevice.Value.sSerial)
                            Next
                        End SyncLock

                    Case "/VMT/Out/DevicePose"
                        If (mMessage.Count < 8) Then
                            Return
                        End If

                        Dim sSerial As String = CStr(mMessage(0))
                        Dim iPosX As Single = CSng(mMessage(1))
                        Dim iPosY As Single = CSng(mMessage(2))
                        Dim iPosZ As Single = CSng(mMessage(3))
                        Dim iQuatX As Single = CSng(mMessage(4))
                        Dim iQuatY As Single = CSng(mMessage(5))
                        Dim iQuatZ As Single = CSng(mMessage(6))
                        Dim iQuatW As Single = CSng(mMessage(7))

                        SyncLock _ThreadLock
                            Dim mDevice As STRUC_DEVICE = Nothing

                            If (g_mDevicesDic.TryGetValue(sSerial, mDevice)) Then
                                Select Case (mDevice.iType)
                                    Case STRUC_DEVICE.ENUM_DEVICE_TYPE.CONTROLLER,
                                            STRUC_DEVICE.ENUM_DEVICE_TYPE.HMD,
                                            STRUC_DEVICE.ENUM_DEVICE_TYPE.TRACKER

                                        mDevice.mPos = New Vector3(iPosX, iPosY, iPosZ)
                                        mDevice.mOrientation = New Quaternion(iQuatX, iQuatY, iQuatZ, iQuatW)

                                        mDevice.mLastPoseTimestamp = Now

                                        g_mDevicesDic(sSerial) = mDevice

                                        RaiseEvent OnDevicePose(mDevice)
                                End Select
                            End If
                        End SyncLock
                End Select
            Catch ex As Exception
                ' Something sussy is going on...
            End Try
        End Sub

        Public Function GetDevices() As STRUC_DEVICE()
            SyncLock _ThreadLock
                Dim mDeviceList As New List(Of STRUC_DEVICE)

                For Each mDevice In g_mDevicesDic
                    mDeviceList.Add(mDevice.Value)
                Next

                Return mDeviceList.ToArray
            End SyncLock
        End Function

        Public Function GetDeviceBySerial(sSerial As String, ByRef mDevice As STRUC_DEVICE) As Boolean
            SyncLock _ThreadLock
                If (g_mDevicesDic.TryGetValue(sSerial, mDevice)) Then
                    Return True
                End If

                Return False
            End SyncLock
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    If (g_UCVirtualMotionTracker IsNot Nothing AndAlso g_UCVirtualMotionTracker.g_ClassOscServer IsNot Nothing) Then
                        RemoveHandler g_UCVirtualMotionTracker.g_ClassOscServer.OnOscProcessMessage, AddressOf OnOscMessage
                    End If

                    If (g_mDevicesThread IsNot Nothing AndAlso g_mDevicesThread.IsAlive) Then
                        g_mDevicesThread.Abort()
                        g_mDevicesThread.Join()
                        g_mDevicesThread = Nothing
                    End If
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
End Class