Imports System.ComponentModel
Imports System.Numerics
Imports Rug.Osc

Public Class UCVirtualMotionTracker
    Public g_mUCVirtualControllers As UCVirtualControllers

    Private g_mAutostartMenuStrips As New Dictionary(Of Integer, ToolStripMenuItem)
    Private g_mVMTControllers As New List(Of UCVirtualMotionTrackerItem)
    Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "vmt_devices.ini")

    Public g_ClassOscServer As ClassOscServer
    Public g_ClassControllerSettings As ClassControllerSettings
    Public g_ClassOscDevices As ClassOscDevices

    Private g_bIgnoreEvents As Boolean = True
    Private g_mOscStatusThread As Threading.Thread = Nothing
    Private g_mOscDeviceStatusThread As Threading.Thread = Nothing

    Public Sub New(_mUCVirtualControllers As UCVirtualControllers)
        g_mUCVirtualControllers = _mUCVirtualControllers

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
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
            ComboBox_RecenterMethod.Items.Add("Use Specific Device")
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
            ComboBox_RecenterFromDevice.Items.Add(New ClassRecenterDeviceItem("", "Any HMD"))

            ComboBox_RecenterFromDevice.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_HmdRecenterMethod.Items.Clear()
            ComboBox_HmdRecenterMethod.Items.Add("Use Active Controller")
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

        For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
            Dim mItem As New ToolStripMenuItem("Controller ID: " & CStr(i))

            g_mAutostartMenuStrips(i) = mItem

            mItem.Tag = i

            ContextMenuStrip_Autostart.Items.Add(mItem)
        Next

        CreateControl()

        Panel_SteamVRRestart.Visible = False

        g_mOscStatusThread = New Threading.Thread(AddressOf OscStatusThread)
        g_mOscStatusThread.IsBackground = True
        g_mOscStatusThread.Start()

        g_mOscDeviceStatusThread = New Threading.Thread(AddressOf OscDeviceStatusThread)
        g_mOscDeviceStatusThread.IsBackground = True
        g_mOscDeviceStatusThread.Start()
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
                ComboBox_TouchpadClickMethod.SelectedIndex = g_ClassControllerSettings.m_HtcTouchpadEmulationClickMethod
                ComboBox_GrabButtonMethod.SelectedIndex = g_ClassControllerSettings.m_HtcGripButtonMethod
                CheckBox_TouchpadClampBounds.Checked = g_ClassControllerSettings.m_HtcClampTouchpadToBounds
                ComboBox_TouchpadMethod.SelectedIndex = g_ClassControllerSettings.m_HtcTouchpadMethod

                CheckBox_ControllerRecenterEnabled.Checked = g_ClassControllerSettings.m_EnableControllerRecenter
                ComboBox_RecenterMethod.SelectedIndex = g_ClassControllerSettings.m_ControllerRecenterMethod

                ComboBox_RecenterFromDevice.Items.Clear()
                ComboBox_RecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(g_ClassControllerSettings.m_ControllerRecenterFromDeviceName, "Any HMD"))
                ComboBox_RecenterFromDevice.SelectedIndex = 0

                CheckBox_HmdRecenterEnabled.Checked = g_ClassControllerSettings.m_EnableHmdRecenter
                ComboBox_HmdRecenterMethod.SelectedIndex = g_ClassControllerSettings.m_HmdRecenterMethod

                ComboBox_HmdRecenterFromDevice.Items.Clear()
                ComboBox_HmdRecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(g_ClassControllerSettings.m_HmdRecenterFromDeviceName, "No Device Selected"))
                ComboBox_HmdRecenterFromDevice.SelectedIndex = 0

                NumericUpDown_RecenterButtonTime.Value = Math.Max(NumericUpDown_RecenterButtonTime.Minimum, Math.Min(NumericUpDown_RecenterButtonTime.Maximum, g_ClassControllerSettings.m_RecenterButtonTimeMs))
                NumericUpDown_OscThreadSleep.Value = Math.Max(NumericUpDown_OscThreadSleep.Minimum, Math.Min(NumericUpDown_OscThreadSleep.Maximum, g_ClassControllerSettings.m_OscThreadSleepMs))

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
        ' TODO: Add help
    End Sub

    Private Sub CleanUp()
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

        For Each mItem In g_mVMTControllers
            If (mItem IsNot Nothing AndAlso Not mItem.IsDisposed) Then
                mItem.Dispose()
            End If
        Next
        g_mVMTControllers.Clear()

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
        Private g_iRecenterButtonTimeMs As Long
        Private g_iOscThreadSleepMs As Long

        Public Sub New(_UCVirtualMotionTracker As UCVirtualMotionTracker)
            g_UCVirtualMotionTracker = _UCVirtualMotionTracker
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
                    g_iOscThreadSleepMs = value
                End SyncLock
            End Set
        End Property

        Public Sub LoadSettings()
            g_bSettingsLoaded = True

            Dim tmpInt As Integer
            Dim tmpLng As Long

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
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
                End Using
            End Using
        End Sub

        Public Sub SaveSettings()
            If (Not g_bSettingsLoaded) Then
                Return
            End If

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

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
                Return ClassQuaternionTools.FromQ2(mOrientation)
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

                ClassPrecisionSleep.Sleep(1)
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

                            mDeviceList.Add(New STRUC_DEVICE(iIndex, CType(iType, STRUC_DEVICE.ENUM_DEVICE_TYPE), sSerial))
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
                                mDevice.mPos = New Vector3(iPosX, iPosY, iPosZ)
                                mDevice.mOrientation = New Quaternion(iQuatX, iQuatY, iQuatZ, iQuatW)

                                mDevice.mLastPoseTimestamp = Now

                                g_mDevicesDic(sSerial) = mDevice

                                RaiseEvent OnDevicePose(mDevice)
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
