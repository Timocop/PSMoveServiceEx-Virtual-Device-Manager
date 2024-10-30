Imports System.ComponentModel
Imports System.Numerics
Imports Rug.Osc

Public Class UCVirtualMotionTracker
    Public g_mFormMain As FormMain

    Public g_ClassOscServer As ClassOscServer
    Public g_ClassSettings As ClassSettings
    Public g_ClassOscDevices As ClassOscDevices

    Public g_UCVmtManagement As UCVmtManagement
    Public g_UCVmtTrackers As UCVmtTrackers
    Public g_UCVmtSettings As UCVmtSettings
    Public g_UCVmtPlayspaceCalib As UCVmtPlayspaceCalib
    Public g_UCVmtOverrides As UCVmtOverrides
    Public g_mClassSteamVRRenderWatchdog As ClassSteamVRRenderWatchdog

    Private g_bIgnoreEvents As Boolean = True

    Enum ENUM_SETTINGS_SAVE_TYPE_FLAGS
        ALL = -1
        DEVICE = (1 << 0)
        DEVICE_RECENTER = (1 << 1)
        PLAYSPACE_CALIBRATION = (1 << 2)
        PLAYSPACE = (1 << 3)
        PLAYSPACE_CALIB_CONTROLLER = (1 << 4)
    End Enum

    Public Sub New(_mFormMain As FormMain)
        g_mFormMain = _mFormMain

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 

        g_bIgnoreEvents = False

        g_ClassOscServer = New ClassOscServer
        g_ClassSettings = New ClassSettings(Me)
        g_ClassOscDevices = New ClassOscDevices(Me)

        g_UCVmtManagement = New UCVmtManagement(Me)
        g_UCVmtManagement.Parent = TabPage_Management
        g_UCVmtManagement.Dock = DockStyle.Top

        g_UCVmtTrackers = New UCVmtTrackers(Me)
        g_UCVmtTrackers.Parent = TabPage_Trackers
        g_UCVmtTrackers.Dock = DockStyle.Fill

        g_UCVmtSettings = New UCVmtSettings(Me)
        g_UCVmtSettings.Parent = TabPage_Settings
        g_UCVmtSettings.Dock = DockStyle.Fill

        g_UCVmtPlayspaceCalib = New UCVmtPlayspaceCalib(Me)
        g_UCVmtPlayspaceCalib.Parent = TabPage_PlayspaceCalib
        g_UCVmtPlayspaceCalib.Dock = DockStyle.Top

        g_UCVmtOverrides = New UCVmtOverrides(Me)
        g_UCVmtOverrides.Parent = TabPage_Overrides
        g_UCVmtOverrides.Dock = DockStyle.Fill

        CreateControl()

        g_mClassSteamVRRenderWatchdog = New ClassSteamVRRenderWatchdog(Me)
        g_mClassSteamVRRenderWatchdog.Init()
    End Sub

    Private Sub UCControllerAttachments_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            g_UCVmtSettings.LoadSettings()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

        Try
            g_UCVmtTrackers.AutostartLoad()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

        Try
            g_UCVmtOverrides.RefreshOverrides()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub LinkLabel_ReadMore_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ReadMore.LinkClicked
        Dim mMsg As New FormRtfHelp
        mMsg.RichTextBox_Help.Rtf = My.Resources.HelpVirtualMotionTracker
        mMsg.ShowDialog(g_mFormMain)
    End Sub

    Private Sub CleanUp()
        If (g_mClassSteamVRRenderWatchdog IsNot Nothing) Then
            g_mClassSteamVRRenderWatchdog.Dispose()
            g_mClassSteamVRRenderWatchdog = Nothing
        End If

        If (g_UCVmtManagement IsNot Nothing AndAlso Not g_UCVmtManagement.IsDisposed) Then
            g_UCVmtManagement.Dispose()
            g_UCVmtManagement = Nothing
        End If

        If (g_UCVmtTrackers IsNot Nothing AndAlso Not g_UCVmtTrackers.IsDisposed) Then
            g_UCVmtTrackers.Dispose()
            g_UCVmtTrackers = Nothing
        End If

        If (g_UCVmtSettings IsNot Nothing AndAlso Not g_UCVmtSettings.IsDisposed) Then
            g_UCVmtSettings.Dispose()
            g_UCVmtSettings = Nothing
        End If

        If (g_UCVmtPlayspaceCalib IsNot Nothing AndAlso Not g_UCVmtPlayspaceCalib.IsDisposed) Then
            g_UCVmtPlayspaceCalib.Dispose()
            g_UCVmtPlayspaceCalib = Nothing
        End If

        If (g_UCVmtOverrides IsNot Nothing AndAlso Not g_UCVmtOverrides.IsDisposed) Then
            g_UCVmtOverrides.Dispose()
            g_UCVmtOverrides = Nothing
        End If

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
            StartServer(Nothing)
        End Sub

        Public Sub StartServer(sAddress As String)
            SyncLock _ThreadLock
                If (g_VmtOsc IsNot Nothing) Then
                    Return
                End If

                If (String.IsNullOrEmpty(sAddress) OrElse sAddress.Trim.Length = 0) Then
                    sAddress = ClassVmtConst.VMT_IP
                End If

                g_VmtOsc = New ClassOSC(sAddress, ClassVmtConst.VMT_PORT_RECEIVE, ClassVmtConst.VMT_PORT_SEND, AddressOf __OnOscProcessBundle, AddressOf __OnOscProcessMessage)
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

    Class ClassSettings
        Private g_UCVirtualMotionTracker As UCVirtualMotionTracker
        Private Shared _ThreadLock As New Object

        Private g_bSettingsLoaded As Boolean = False

        Private g_mControllerSettings As STRUC_CONTROLLER_SETTINGS
        Private g_mHmdSettings As STRUC_HMD_SETTINGS
        Private g_mMiscSettings As STRUC_MISC_SETTINGS
        Private g_mPlaySpaceSettings As STRUC_PLAYSPACE_SETTINGS

        Public Const DISPLAY_DISTORTION_K0 As Single = 0.75F
        Public Const DISPLAY_DISTORTION_K1 As Single = 4.0F
        Public Const DISPLAY_DISTORTION_SCALE As Single = 0.7F
        Public Const DISPLAY_DISTORTION_RED_OFFSET As Single = 0.0F
        Public Const DISPLAY_DISTORTION_GREEN_OFFSET As Single = 0.006F
        Public Const DISPLAY_DISTORTION_BLUE_OFFSET As Single = 0.012F
        Public Const DISPLAY_HFOV As Single = 105.0F
        Public Const DISPLAY_VFOV As Single = 110.0F

        Class STRUC_HMD_SETTINGS
            Private g_bUseCustomDistortion As Boolean = False
            Private g_iDistortionK0 As Single = DISPLAY_DISTORTION_K0
            Private g_iDistortionK1 As Single = DISPLAY_DISTORTION_K1
            Private g_iDistortionScale As Single = DISPLAY_DISTORTION_SCALE
            Private g_iDistortionRedOffset As Single = DISPLAY_DISTORTION_RED_OFFSET
            Private g_iDistortionGreenOffset As Single = DISPLAY_DISTORTION_GREEN_OFFSET
            Private g_iDistortionBlueOffset As Single = DISPLAY_DISTORTION_BLUE_OFFSET
            Private g_iHFov As Single = DISPLAY_HFOV
            Private g_iVFov As Single = DISPLAY_VFOV
            Private g_iIPD As Single = 67.0F
            Private g_iRenderScale As Single = 1.3F '130% for 0.7 distortion scale compensation

            Public Property m_UseCustomDistortion As Boolean
                Get
                    Return g_bUseCustomDistortion
                End Get
                Set(value As Boolean)
                    g_bUseCustomDistortion = value
                End Set
            End Property

            Public Property m_DistortionK0(bUseCustomDistortion As Boolean) As Single
                Get
                    If (Not bUseCustomDistortion) Then
                        Return DISPLAY_DISTORTION_K0
                    End If

                    Return g_iDistortionK0
                End Get
                Set(value As Single)
                    If (value < -100.0F) Then
                        value = -100.0F
                    End If

                    If (value > 100.0F) Then
                        value = 100.0F
                    End If

                    g_iDistortionK0 = value
                End Set
            End Property

            Public Property m_DistortionK1(bUseCustomDistortion As Boolean) As Single
                Get
                    If (Not bUseCustomDistortion) Then
                        Return DISPLAY_DISTORTION_K1
                    End If

                    Return g_iDistortionK1
                End Get
                Set(value As Single)
                    If (value < -100.0F) Then
                        value = -100.0F
                    End If

                    If (value > 100.0F) Then
                        value = 100.0F
                    End If

                    g_iDistortionK1 = value
                End Set
            End Property

            Public Property m_DistortionScale(bUseCustomDistortion As Boolean) As Single
                Get
                    If (Not bUseCustomDistortion) Then
                        Return DISPLAY_DISTORTION_SCALE
                    End If

                    Return g_iDistortionScale
                End Get
                Set(value As Single)
                    If (value < -100.0F) Then
                        value = -100.0F
                    End If

                    If (value > 100.0F) Then
                        value = 100.0F
                    End If

                    g_iDistortionScale = value
                End Set
            End Property

            Public Property m_DistortionRedOffset(bUseCustomDistortion As Boolean) As Single
                Get
                    If (Not bUseCustomDistortion) Then
                        Return DISPLAY_DISTORTION_RED_OFFSET
                    End If

                    Return g_iDistortionRedOffset
                End Get
                Set(value As Single)
                    If (value < -100.0F) Then
                        value = -100.0F
                    End If

                    If (value > 100.0F) Then
                        value = 100.0F
                    End If

                    g_iDistortionRedOffset = value
                End Set
            End Property

            Public Property m_DistortionGreenOffset(bUseCustomDistortion As Boolean) As Single
                Get
                    If (Not bUseCustomDistortion) Then
                        Return DISPLAY_DISTORTION_GREEN_OFFSET
                    End If

                    Return g_iDistortionGreenOffset
                End Get
                Set(value As Single)
                    If (value < -100.0F) Then
                        value = -100.0F
                    End If

                    If (value > 100.0F) Then
                        value = 100.0F
                    End If

                    g_iDistortionGreenOffset = value
                End Set
            End Property

            Public Property m_DistortionBlueOffset(bUseCustomDistortion As Boolean) As Single
                Get
                    If (Not bUseCustomDistortion) Then
                        Return DISPLAY_DISTORTION_BLUE_OFFSET
                    End If

                    Return g_iDistortionBlueOffset
                End Get
                Set(value As Single)
                    If (value < -100.0F) Then
                        value = -100.0F
                    End If

                    If (value > 100.0F) Then
                        value = 100.0F
                    End If

                    g_iDistortionBlueOffset = value
                End Set
            End Property

            Public Property m_HFov(bUseCustomDistortion As Boolean) As Single
                Get
                    If (Not bUseCustomDistortion) Then
                        Return DISPLAY_HFOV
                    End If

                    Return g_iHFov
                End Get
                Set(value As Single)
                    If (value < 10) Then
                        value = 10
                    End If

                    If (value > 180.0F) Then
                        value = 180.0F
                    End If

                    g_iHFov = value
                End Set
            End Property

            Public Property m_VFov(bUseCustomDistortion As Boolean) As Single
                Get
                    If (Not bUseCustomDistortion) Then
                        Return DISPLAY_VFOV
                    End If

                    Return g_iVFov
                End Get
                Set(value As Single)
                    If (value < 10) Then
                        value = 10
                    End If

                    If (value > 180.0F) Then
                        value = 180.0F
                    End If

                    g_iVFov = value
                End Set
            End Property

            Public Property m_IPD As Single
                Get
                    Return g_iIPD
                End Get
                Set(value As Single)
                    If (value < 50) Then
                        value = 50
                    End If

                    If (value > 100.0F) Then
                        value = 100.0F
                    End If

                    g_iIPD = value
                End Set
            End Property

            Public Property m_RenderScale As Single
                Get
                    Return g_iRenderScale
                End Get
                Set(value As Single)
                    If (value < 0.2F) Then
                        value = 0.2F
                    End If

                    If (value > 5.0F) Then
                        value = 5.0F
                    End If

                    g_iRenderScale = value
                End Set
            End Property
        End Class

        Class STRUC_PLAYSPACE_SETTINGS
            Enum ENUM_FORWARD_METHOD
                USE_HMD_FORWARD
                USE_PLAYSPACE_FORRWARD
            End Enum

            Public Property m_PosOffset As Vector3
            Public Property m_AngOffset As Quaternion
            Public Property m_HmdAngOffset As Quaternion
            Public Property m_ScaleOffset As Single

            Public Property m_PointControllerBeginPos As Vector3
            Public Property m_PointControllerEndPos As Vector3
            Public Property m_PointHmdBeginPos As Vector3
            Public Property m_PointHmdEndPos As Vector3

            Public Property m_Valid As Boolean

            Public Property m_ForwardOffset As Single
            Public Property m_SideOffset As Single
            Public Property m_HeightOffset As Single
            Public Property m_ForwardMethod As ENUM_FORWARD_METHOD
            Public Property m_CalibrationControllerId As Integer
            Public Property m_AutoScale As Boolean

            Public Sub Reset()
                m_Valid = False

                m_PosOffset = Vector3.Zero
                m_AngOffset = Quaternion.Identity
                m_HmdAngOffset = Quaternion.Identity
                m_ScaleOffset = 1.0F

                m_PointControllerBeginPos = Vector3.Zero
                m_PointControllerEndPos = Vector3.Zero
                m_PointHmdBeginPos = Vector3.Zero
                m_PointHmdEndPos = Vector3.Zero
            End Sub
        End Class

        Class STRUC_CONTROLLER_SETTINGS
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

            Enum ENUM_OCULUS_BUTTON_METHOD
                BUTTON_LEFT_CIRCLE_TRIANGLE_CROSS_SQUARE
                BUTTON_LEFT_CROSS_SQUARE_CIRCLE_TRIANGLE
                BUTTON_BOTH_CIRCLE_TRIANGLE_CROSS_SQUARE
                BUTTON_BOTH_CROSS_SQUARE_CIRCLE_TRIANGLE

                __MAX
            End Enum

            Enum ENUM_CONTROLLER_JOYSTICK_METHOD
                USE_POSITION
                USE_ORIENTATION

                __MAX
            End Enum

            Enum ENUM_DEVICE_RECENTER_METHOD
                USE_DEVICE
                USE_PLAYSPACE

                __MAX
            End Enum

            Private g_bHtcTouchpadShortcutBinding As Boolean = False
            Private g_bHtcTouchpadShortcutTouchpadClick As Boolean = False
            Private g_iHtcTouchpadEmulationClickMethod As ENUM_HTC_TOUCHPAD_CLICK_METHOD = ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED
            Private g_iHtcGripButtonMethod As ENUM_HTC_GRIP_BUTTON_METHOD = ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_MIRRORED
            Private g_iTouchpadClickDeadzone As Single = 0.25F

            Private g_iOculusButtonMethod As ENUM_OCULUS_BUTTON_METHOD = ENUM_OCULUS_BUTTON_METHOD.BUTTON_LEFT_CIRCLE_TRIANGLE_CROSS_SQUARE
            Private g_bOculusGripToggle As Boolean = True
            Private g_bHybridGripToggle As Boolean = True

            Private g_iControllerJoystickMethod As ENUM_CONTROLLER_JOYSTICK_METHOD = ENUM_CONTROLLER_JOYSTICK_METHOD.USE_ORIENTATION
            Private g_iControllerJoystickAreaCm As Single = 7.5F
            Private g_bControllerClampJoystickToBounds As Boolean = True

            Private g_bEnableControllerRecenter As Boolean = True
            Private g_iControllerRecenterMethod As ENUM_DEVICE_RECENTER_METHOD = ENUM_DEVICE_RECENTER_METHOD.USE_DEVICE
            Private g_sControllerRecenterFromDeviceName As String = ""
            Private g_bEnableHmdRecenter As Boolean = True
            Private g_iHmdRecenterMethod As ENUM_DEVICE_RECENTER_METHOD = ENUM_DEVICE_RECENTER_METHOD.USE_DEVICE
            Private g_sHmdRecenterFromDeviceName As String = ""
            Private g_iRecenterButtonTimeMs As Long = 500
            Private g_iOscThreadSleepMs As Integer = 1
            Private g_iOscMaxThreadFps As Integer = 200
            Private g_bEnablePlayspaceRecenter As Boolean

            Public Property m_ControllerRecenter As New Dictionary(Of Integer, Quaternion)

            Property m_HtcTouchpadShortcutBinding As Boolean
                Get
                    Return g_bHtcTouchpadShortcutBinding
                End Get
                Set(value As Boolean)
                    g_bHtcTouchpadShortcutBinding = value
                End Set
            End Property

            Property m_HtcTouchpadShortcutTouchpadClick As Boolean
                Get
                    Return g_bHtcTouchpadShortcutTouchpadClick
                End Get
                Set(value As Boolean)
                    g_bHtcTouchpadShortcutTouchpadClick = value
                End Set
            End Property

            Property m_HtcTouchpadEmulationClickMethod As ENUM_HTC_TOUCHPAD_CLICK_METHOD
                Get
                    Return g_iHtcTouchpadEmulationClickMethod
                End Get
                Set(value As ENUM_HTC_TOUCHPAD_CLICK_METHOD)
                    If (value < 0) Then
                        value = 0
                    End If

                    If (value > ENUM_HTC_TOUCHPAD_CLICK_METHOD.__MAX - 1) Then
                        value = CType(ENUM_HTC_TOUCHPAD_CLICK_METHOD.__MAX - 1, ENUM_HTC_TOUCHPAD_CLICK_METHOD)
                    End If

                    g_iHtcTouchpadEmulationClickMethod = value
                End Set
            End Property

            Property m_HtcGripButtonMethod As ENUM_HTC_GRIP_BUTTON_METHOD
                Get
                    Return g_iHtcGripButtonMethod
                End Get
                Set(value As ENUM_HTC_GRIP_BUTTON_METHOD)
                    If (value < 0) Then
                        value = 0
                    End If

                    If (value > ENUM_HTC_GRIP_BUTTON_METHOD.__MAX - 1) Then
                        value = CType(ENUM_HTC_GRIP_BUTTON_METHOD.__MAX - 1, ENUM_HTC_GRIP_BUTTON_METHOD)
                    End If

                    g_iHtcGripButtonMethod = value
                End Set
            End Property

            Property m_OculusButtonMethod As ENUM_OCULUS_BUTTON_METHOD
                Get
                    Return g_iOculusButtonMethod
                End Get
                Set(value As ENUM_OCULUS_BUTTON_METHOD)
                    If (value < 0) Then
                        value = 0
                    End If

                    If (value > ENUM_OCULUS_BUTTON_METHOD.__MAX - 1) Then
                        value = CType(ENUM_OCULUS_BUTTON_METHOD.__MAX - 1, ENUM_OCULUS_BUTTON_METHOD)
                    End If

                    g_iOculusButtonMethod = value
                End Set
            End Property

            Property m_OculusGripToggle As Boolean
                Get
                    Return g_bOculusGripToggle
                End Get
                Set(value As Boolean)
                    g_bOculusGripToggle = value
                End Set
            End Property

            Property m_HybridGripToggle As Boolean
                Get
                    Return g_bHybridGripToggle
                End Get
                Set(value As Boolean)
                    g_bHybridGripToggle = value
                End Set
            End Property

            Property m_ControllerJoystickMethod As ENUM_CONTROLLER_JOYSTICK_METHOD
                Get
                    Return g_iControllerJoystickMethod
                End Get
                Set(value As ENUM_CONTROLLER_JOYSTICK_METHOD)
                    g_iControllerJoystickMethod = value
                End Set
            End Property

            Property m_EnableControllerRecenter As Boolean
                Get
                    Return g_bEnableControllerRecenter
                End Get
                Set(value As Boolean)
                    g_bEnableControllerRecenter = value
                End Set
            End Property

            Property m_ControllerRecenterMethod As ENUM_DEVICE_RECENTER_METHOD
                Get
                    Return g_iControllerRecenterMethod
                End Get
                Set(value As ENUM_DEVICE_RECENTER_METHOD)
                    g_iControllerRecenterMethod = value
                End Set
            End Property

            Property m_ControllerRecenterFromDeviceName As String
                Get
                    Return g_sControllerRecenterFromDeviceName
                End Get
                Set(value As String)
                    g_sControllerRecenterFromDeviceName = value
                End Set
            End Property

            Property m_EnableHmdRecenter As Boolean
                Get
                    Return g_bEnableHmdRecenter
                End Get
                Set(value As Boolean)
                    g_bEnableHmdRecenter = value
                End Set
            End Property

            Property m_HmdRecenterMethod As ENUM_DEVICE_RECENTER_METHOD
                Get
                    Return g_iHmdRecenterMethod
                End Get
                Set(value As ENUM_DEVICE_RECENTER_METHOD)
                    g_iHmdRecenterMethod = value
                End Set
            End Property

            Property m_HmdRecenterFromDeviceName As String
                Get
                    Return g_sHmdRecenterFromDeviceName
                End Get
                Set(value As String)
                    g_sHmdRecenterFromDeviceName = value
                End Set
            End Property

            Property m_RecenterButtonTimeMs As Long
                Get
                    Return g_iRecenterButtonTimeMs
                End Get
                Set(value As Long)
                    If (value < 1) Then
                        value = 1
                    End If
                    If (value > 10000) Then
                        value = 10000
                    End If

                    g_iRecenterButtonTimeMs = value
                End Set
            End Property

            Property m_OscThreadSleepMs As Integer
                Get
                    Return g_iOscThreadSleepMs
                End Get
                Set(value As Integer)
                    If (value < 1) Then
                        value = 1
                    End If
                    If (value > 100) Then
                        value = 100
                    End If

                    g_iOscThreadSleepMs = value
                End Set
            End Property

            Property m_OscMaxThreadFps As Integer
                Get
                    Return g_iOscMaxThreadFps
                End Get
                Set(value As Integer)
                    If (value < 30) Then
                        value = 30
                    End If
                    If (value > Short.MaxValue) Then
                        value = Short.MaxValue
                    End If

                    g_iOscMaxThreadFps = value
                End Set
            End Property

            Property m_ControllerJoystickAreaCm As Single
                Get
                    Return g_iControllerJoystickAreaCm
                End Get
                Set(value As Single)
                    If (value < 1.0F) Then
                        value = 1.0F
                    End If
                    If (value > 100.0F) Then
                        value = 100.0F
                    End If

                    g_iControllerJoystickAreaCm = value
                End Set
            End Property

            Property m_HtcTouchpadClickDeadzone As Single
                Get
                    Return g_iTouchpadClickDeadzone
                End Get
                Set(value As Single)
                    If (value < 0.0F) Then
                        value = 0.0F
                    End If
                    If (value > 1.0F) Then
                        value = 1.0F
                    End If

                    g_iTouchpadClickDeadzone = value
                End Set
            End Property

            Property m_EnablePlayspaceRecenter As Boolean
                Get
                    Return g_bEnablePlayspaceRecenter
                End Get
                Set(value As Boolean)
                    g_bEnablePlayspaceRecenter = value
                End Set
            End Property

        End Class

        Class STRUC_MISC_SETTINGS
            Private g_bDisableBaseStationSpawning As Boolean = False
            Private g_bEnableHepticFeedback As Boolean = True
            Private g_bOptimizeTransportPackets As Boolean = True
            Private g_bRenderWindowFix As Boolean = True
            Private g_bEnableVelocityHmd As Boolean = True
            Private g_bEnableVelocityController As Boolean = True
            Private g_bEnableVelocityTracker As Boolean = True
            Private g_bEnableVelocityInterpolation As Boolean = False
            Private g_bEnableVelocityControllerOnly As Boolean = True
            Private g_sOscRemoteIP As String = ""

            Property m_DisableBaseStationSpawning As Boolean
                Get
                    Return g_bDisableBaseStationSpawning
                End Get
                Set(value As Boolean)
                    g_bDisableBaseStationSpawning = value
                End Set
            End Property

            Property m_EnableHepticFeedback As Boolean
                Get
                    Return g_bEnableHepticFeedback
                End Get
                Set(value As Boolean)
                    g_bEnableHepticFeedback = value
                End Set
            End Property

            Property m_OptimizeTransportPackets As Boolean
                Get
                    Return g_bOptimizeTransportPackets
                End Get
                Set(value As Boolean)
                    g_bOptimizeTransportPackets = value
                End Set
            End Property

            Property m_RenderWindowFix As Boolean
                Get
                    Return g_bRenderWindowFix
                End Get
                Set(value As Boolean)
                    g_bRenderWindowFix = value
                End Set
            End Property

            Property m_EnableVelocityHmd As Boolean
                Get
                    Return g_bEnableVelocityHmd
                End Get
                Set(value As Boolean)
                    g_bEnableVelocityHmd = value
                End Set
            End Property

            Property m_EnableVelocityController As Boolean
                Get
                    Return g_bEnableVelocityController
                End Get
                Set(value As Boolean)
                    g_bEnableVelocityController = value
                End Set
            End Property

            Property m_EnableVelocityTracker As Boolean
                Get
                    Return g_bEnableVelocityTracker
                End Get
                Set(value As Boolean)
                    g_bEnableVelocityTracker = value
                End Set
            End Property

            Property m_EnableVelocityInterpoliation As Boolean
                Get
                    Return g_bEnableVelocityInterpolation
                End Get
                Set(value As Boolean)
                    g_bEnableVelocityInterpolation = value
                End Set
            End Property

            Property m_OscRemoteIP As String
                Get
                    Return g_sOscRemoteIP
                End Get
                Set(value As String)
                    g_sOscRemoteIP = value
                End Set
            End Property
        End Class

        Public Sub New(_UCVirtualMotionTracker As UCVirtualMotionTracker)
            g_UCVirtualMotionTracker = _UCVirtualMotionTracker

            g_mControllerSettings = New STRUC_CONTROLLER_SETTINGS()
            g_mHmdSettings = New STRUC_HMD_SETTINGS()
            g_mMiscSettings = New STRUC_MISC_SETTINGS()

            g_mPlaySpaceSettings = New STRUC_PLAYSPACE_SETTINGS()
            g_mPlaySpaceSettings.Reset()
        End Sub

        ReadOnly Property m_ControllerSettings As STRUC_CONTROLLER_SETTINGS
            Get
                SyncLock _ThreadLock
                    Return g_mControllerSettings
                End SyncLock
            End Get
        End Property

        ReadOnly Property m_HmdSettings As STRUC_HMD_SETTINGS
            Get
                SyncLock _ThreadLock
                    Return g_mHmdSettings
                End SyncLock
            End Get
        End Property

        ReadOnly Property m_MiscSettings As STRUC_MISC_SETTINGS
            Get
                SyncLock _ThreadLock
                    Return g_mMiscSettings
                End SyncLock
            End Get
        End Property

        ReadOnly Property m_PlayspaceSettings As STRUC_PLAYSPACE_SETTINGS
            Get
                SyncLock _ThreadLock
                    Return g_mPlaySpaceSettings
                End SyncLock
            End Get
        End Property

        Public Sub LoadSettings()
            SyncLock _ThreadLock
                g_bSettingsLoaded = True

                Dim tmpVec3 As Vector3
                Dim tmpQuat As Quaternion

                Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_VMT, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)

                        ' Controller Settings
                        m_ControllerSettings.m_HtcTouchpadShortcutBinding =
                            (mIni.ReadKeyValue("ControllerSettings", "HtcTouchpadShortcutBinding", "false") = "true")
                        m_ControllerSettings.m_HtcTouchpadShortcutTouchpadClick =
                            (mIni.ReadKeyValue("ControllerSettings", "HtcTouchpadShortcutTouchpadClick", "false") = "true")
                        m_ControllerSettings.m_HtcTouchpadEmulationClickMethod =
                                CType(Integer.Parse(mIni.ReadKeyValue("ControllerSettings", "HtcTouchpadEmulationClickMethod", CStr(CInt(STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED)))), STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD)
                        m_ControllerSettings.m_HtcGripButtonMethod =
                            CType(Integer.Parse(mIni.ReadKeyValue("ControllerSettings", "HtcGripButtonMethod", CStr(CInt(STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_MIRRORED)))), STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD)
                        m_ControllerSettings.m_OculusButtonMethod =
                            CType(Integer.Parse(mIni.ReadKeyValue("ControllerSettings", "OculusButtonMethod", CStr(CInt(STRUC_CONTROLLER_SETTINGS.ENUM_OCULUS_BUTTON_METHOD.BUTTON_LEFT_CIRCLE_TRIANGLE_CROSS_SQUARE)))), STRUC_CONTROLLER_SETTINGS.ENUM_OCULUS_BUTTON_METHOD)
                        m_ControllerSettings.m_OculusGripToggle =
                            (mIni.ReadKeyValue("ControllerSettings", "OculusGripToggle", "true") = "true")
                        m_ControllerSettings.m_HybridGripToggle =
                            (mIni.ReadKeyValue("ControllerSettings", "HybridGripToggle", "true") = "true")
                        m_ControllerSettings.m_ControllerJoystickMethod =
                            CType(Integer.Parse(mIni.ReadKeyValue("ControllerSettings", "ControllerJoystickMethod", CStr(CInt(STRUC_CONTROLLER_SETTINGS.ENUM_CONTROLLER_JOYSTICK_METHOD.USE_ORIENTATION)))), STRUC_CONTROLLER_SETTINGS.ENUM_CONTROLLER_JOYSTICK_METHOD)
                        m_ControllerSettings.m_EnableControllerRecenter =
                            (mIni.ReadKeyValue("ControllerSettings", "EnableControllerRecenter", "true") = "true")
                        m_ControllerSettings.m_ControllerRecenterMethod =
                            CType(Integer.Parse(mIni.ReadKeyValue("ControllerSettings", "ControllerRecenterMethod", CStr(CInt(STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD.USE_DEVICE)))), STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD)
                        m_ControllerSettings.m_ControllerRecenterFromDeviceName =
                            mIni.ReadKeyValue("ControllerSettings", "ControllerRecenterFromDeviceName", "")
                        m_ControllerSettings.m_EnableHmdRecenter =
                            (mIni.ReadKeyValue("ControllerSettings", "EnableHmdRecenter", "true") = "true")
                        m_ControllerSettings.m_HmdRecenterMethod =
                            CType(Integer.Parse(mIni.ReadKeyValue("ControllerSettings", "HmdRecenterMethod", CStr(CInt(STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD.USE_DEVICE)))), STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD)
                        m_ControllerSettings.m_HmdRecenterFromDeviceName =
                            mIni.ReadKeyValue("ControllerSettings", "HmdRecenterFromDeviceName", "")
                        m_ControllerSettings.m_RecenterButtonTimeMs =
                            Long.Parse(mIni.ReadKeyValue("ControllerSettings", "RecenterButtonTimeMs", "500"))
                        m_ControllerSettings.m_OscThreadSleepMs =
                            Integer.Parse(mIni.ReadKeyValue("ControllerSettings", "OscThreadSleepMs", "1"))
                        m_ControllerSettings.m_OscMaxThreadFps =
                            Integer.Parse(mIni.ReadKeyValue("ControllerSettings", "OscMaxThreadFps", "200"))
                        m_ControllerSettings.m_ControllerJoystickAreaCm =
                            Single.Parse(mIni.ReadKeyValue("ControllerSettings", "ControllerJoystickAreaCm", "7.5"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_ControllerSettings.m_HtcTouchpadClickDeadzone =
                            Single.Parse(mIni.ReadKeyValue("ControllerSettings", "HtcTouchpadClickDeadzone", "0.25"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_ControllerSettings.m_EnablePlayspaceRecenter =
                            (mIni.ReadKeyValue("ControllerSettings", "EnablePlayspaceRecenter", "true") = "true")

                        ' Per Controller Settings
                        For j = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                            tmpQuat = Quaternion.Identity
                            tmpQuat.X = Single.Parse(mIni.ReadKeyValue(CStr(j), "Recenter.X", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                            tmpQuat.Y = Single.Parse(mIni.ReadKeyValue(CStr(j), "Recenter.Y", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                            tmpQuat.Z = Single.Parse(mIni.ReadKeyValue(CStr(j), "Recenter.Z", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                            tmpQuat.W = Single.Parse(mIni.ReadKeyValue(CStr(j), "Recenter.W", "1.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                            m_ControllerSettings.m_ControllerRecenter(j) = tmpQuat
                        Next

                        ' Hmd Settings
                        m_HmdSettings.m_UseCustomDistortion =
                            (mIni.ReadKeyValue("HmdSettings", "UseCustomDistortion", "false") = "true")
                        m_HmdSettings.m_DistortionK0(True) =
                            Single.Parse(mIni.ReadKeyValue("HmdSettings", "DistortionK0", DISPLAY_DISTORTION_K0.ToString(Globalization.CultureInfo.InvariantCulture)), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_HmdSettings.m_DistortionK1(True) =
                            Single.Parse(mIni.ReadKeyValue("HmdSettings", "DistortionK1", DISPLAY_DISTORTION_K1.ToString(Globalization.CultureInfo.InvariantCulture)), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_HmdSettings.m_DistortionScale(True) =
                             Single.Parse(mIni.ReadKeyValue("HmdSettings", "DistortionScale", DISPLAY_DISTORTION_SCALE.ToString(Globalization.CultureInfo.InvariantCulture)), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_HmdSettings.m_DistortionRedOffset(True) =
                            Single.Parse(mIni.ReadKeyValue("HmdSettings", "DistortionRedOffset", DISPLAY_DISTORTION_RED_OFFSET.ToString(Globalization.CultureInfo.InvariantCulture)), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_HmdSettings.m_DistortionGreenOffset(True) =
                            Single.Parse(mIni.ReadKeyValue("HmdSettings", "DistortionGreenOffset", DISPLAY_DISTORTION_GREEN_OFFSET.ToString(Globalization.CultureInfo.InvariantCulture)), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_HmdSettings.m_DistortionBlueOffset(True) =
                            Single.Parse(mIni.ReadKeyValue("HmdSettings", "DistortionBlueOffset", DISPLAY_DISTORTION_BLUE_OFFSET.ToString(Globalization.CultureInfo.InvariantCulture)), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_HmdSettings.m_HFov(True) =
                            Single.Parse(mIni.ReadKeyValue("HmdSettings", "HFov", DISPLAY_HFOV.ToString(Globalization.CultureInfo.InvariantCulture)), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_HmdSettings.m_VFov(True) =
                            Single.Parse(mIni.ReadKeyValue("HmdSettings", "VFov", DISPLAY_VFOV.ToString(Globalization.CultureInfo.InvariantCulture)), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_HmdSettings.m_IPD =
                            Single.Parse(mIni.ReadKeyValue("HmdSettings", "IPD", "67.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_HmdSettings.m_RenderScale =
                            Single.Parse(mIni.ReadKeyValue("HmdSettings", "RenderScale", "1.3"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

                        ' Misc Settings 
                        g_mMiscSettings.m_DisableBaseStationSpawning =
                            (mIni.ReadKeyValue("MiscSettings", "DisableBaseStationSpawning", "false") = "true")
                        g_mMiscSettings.m_EnableHepticFeedback =
                            (mIni.ReadKeyValue("MiscSettings", "EnableHepticFeedback", "true") = "true")
                        g_mMiscSettings.m_OptimizeTransportPackets =
                            (mIni.ReadKeyValue("MiscSettings", "OptimizeTransportPackets", "true") = "true")
                        g_mMiscSettings.m_RenderWindowFix =
                            (mIni.ReadKeyValue("MiscSettings", "RenderWindowFix", "true") = "true")
                        g_mMiscSettings.m_EnableVelocityHmd =
                            (mIni.ReadKeyValue("MiscSettings", "EnableVelocityHmd", "true") = "true")
                        g_mMiscSettings.m_EnableVelocityController =
                            (mIni.ReadKeyValue("MiscSettings", "EnableVelocityController", "true") = "true")
                        g_mMiscSettings.m_EnableVelocityTracker =
                            (mIni.ReadKeyValue("MiscSettings", "EnableVelocityTracker", "true") = "true")
                        g_mMiscSettings.m_EnableVelocityInterpoliation =
                            (mIni.ReadKeyValue("MiscSettings", "EnableVelocityInterpoliation", "false") = "true")
                        g_mMiscSettings.m_OscRemoteIP =
                            mIni.ReadKeyValue("MiscSettings", "OscRemoteIP", "")

                        ' Playspace Settings
                        m_PlayspaceSettings.m_ForwardOffset =
                            Single.Parse(mIni.ReadKeyValue("PlayspaceSettings", "ForwardOffset", "10.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_PlayspaceSettings.m_SideOffset =
                            Single.Parse(mIni.ReadKeyValue("PlayspaceSettings", "SideOffset", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_PlayspaceSettings.m_HeightOffset =
                            Single.Parse(mIni.ReadKeyValue("PlayspaceSettings", "HeightOffset", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_PlayspaceSettings.m_ForwardMethod =
                            CType(Integer.Parse(mIni.ReadKeyValue("PlayspaceSettings", "ForwardMethod", "0")), STRUC_PLAYSPACE_SETTINGS.ENUM_FORWARD_METHOD)
                        m_PlayspaceSettings.m_CalibrationControllerId =
                            Integer.Parse(mIni.ReadKeyValue("PlayspaceSettings", "CalibControllerId", "0"))
                        m_PlayspaceSettings.m_AutoScale =
                            (mIni.ReadKeyValue("PlayspaceSettings", "AutoScale", "true") = "true")

                        ' Playspace Calibration Settings
                        tmpVec3 = Vector3.Zero
                        tmpVec3.X = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PosOffsetX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpVec3.Y = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PosOffsetY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpVec3.Z = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PosOffsetZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_PlayspaceSettings.m_PosOffset = tmpVec3

                        tmpQuat = Quaternion.Identity
                        tmpQuat.X = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "AngOffsetX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpQuat.Y = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "AngOffsetY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpQuat.Z = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "AngOffsetZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpQuat.W = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "AngOffsetW", "1.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_PlayspaceSettings.m_AngOffset = tmpQuat

                        tmpVec3 = Vector3.Zero
                        tmpVec3.X = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerBeginPosX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpVec3.Y = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerBeginPosY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpVec3.Z = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerBeginPosZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_PlayspaceSettings.m_PointControllerBeginPos = tmpVec3

                        tmpVec3 = Vector3.Zero
                        tmpVec3.X = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerEndPosX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpVec3.Y = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerEndPosY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpVec3.Z = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointControllerEndPosZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_PlayspaceSettings.m_PointControllerEndPos = tmpVec3

                        tmpVec3 = Vector3.Zero
                        tmpVec3.X = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdBeginPosX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpVec3.Y = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdBeginPosY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpVec3.Z = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdBeginPosZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_PlayspaceSettings.m_PointHmdBeginPos = tmpVec3

                        tmpVec3 = Vector3.Zero
                        tmpVec3.X = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdEndPosX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpVec3.Y = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdEndPosY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpVec3.Z = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "PointHmdEndPosZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_PlayspaceSettings.m_PointHmdEndPos = tmpVec3

                        tmpQuat = Quaternion.Identity
                        tmpQuat.X = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "HmdAngOffsetX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpQuat.Y = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "HmdAngOffsetY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpQuat.Z = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "HmdAngOffsetZ", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        tmpQuat.W = Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "HmdAngOffsetW", "1.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        m_PlayspaceSettings.m_HmdAngOffset = tmpQuat

                        m_PlayspaceSettings.m_ScaleOffset =
                            Single.Parse(mIni.ReadKeyValue("PlayspaceCalibrationSettings", "ScaleOffset", "1.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

                        m_PlayspaceSettings.m_Valid = (mIni.ReadKeyValue("PlayspaceCalibrationSettings", "Valid", "false") = "true")

                    End Using
                End Using
            End SyncLock
        End Sub

        Public Sub SaveSettings(iSaveFlags As ENUM_SETTINGS_SAVE_TYPE_FLAGS)
            SyncLock _ThreadLock
                If (Not g_bSettingsLoaded) Then
                    Return
                End If

                Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_VMT, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)
                        Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                        If ((iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.DEVICE_RECENTER) <> 0 OrElse (iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.ALL) <> 0) Then
                            For Each mItem In m_ControllerSettings.m_ControllerRecenter
                                mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(CStr(mItem.Key), "Recenter.X", mItem.Value.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                                mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(CStr(mItem.Key), "Recenter.Y", mItem.Value.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                                mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(CStr(mItem.Key), "Recenter.Z", mItem.Value.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                                mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(CStr(mItem.Key), "Recenter.W", mItem.Value.W.ToString(Globalization.CultureInfo.InvariantCulture)))
                            Next
                        End If

                        If ((iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.DEVICE) <> 0 OrElse (iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.ALL) <> 0) Then
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcTouchpadShortcutBinding", If(m_ControllerSettings.m_HtcTouchpadShortcutBinding, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcTouchpadShortcutTouchpadClick", If(m_ControllerSettings.m_HtcTouchpadShortcutTouchpadClick, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcTouchpadEmulationClickMethod", CStr(CInt(m_ControllerSettings.m_HtcTouchpadEmulationClickMethod))))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcGripButtonMethod", CStr(CInt(m_ControllerSettings.m_HtcGripButtonMethod))))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "OculusButtonMethod", CStr(CInt(m_ControllerSettings.m_OculusButtonMethod))))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "OculusGripToggle", If(m_ControllerSettings.m_OculusGripToggle, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HybridGripToggle", If(m_ControllerSettings.m_HybridGripToggle, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "ControllerJoystickMethod", CStr(CInt(m_ControllerSettings.m_ControllerJoystickMethod))))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "EnableControllerRecenter", If(m_ControllerSettings.m_EnableControllerRecenter, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "ControllerRecenterMethod", CStr(CInt(m_ControllerSettings.m_ControllerRecenterMethod))))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "ControllerRecenterFromDeviceName", m_ControllerSettings.m_ControllerRecenterFromDeviceName))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "EnableHmdRecenter", If(m_ControllerSettings.m_EnableHmdRecenter, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HmdRecenterMethod", CStr(CInt(m_ControllerSettings.m_HmdRecenterMethod))))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HmdRecenterFromDeviceName", m_ControllerSettings.m_HmdRecenterFromDeviceName))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "RecenterButtonTimeMs", CStr(m_ControllerSettings.m_RecenterButtonTimeMs)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "OscThreadSleepMs", CStr(m_ControllerSettings.m_OscThreadSleepMs)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "OscMaxThreadFps", CStr(m_ControllerSettings.m_OscMaxThreadFps)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "ControllerJoystickAreaCm", m_ControllerSettings.m_ControllerJoystickAreaCm.ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcTouchpadClickDeadzone", m_ControllerSettings.m_HtcTouchpadClickDeadzone.ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "EnablePlayspaceRecenter", If(m_ControllerSettings.m_EnablePlayspaceRecenter, "true", "false")))

                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("HmdSettings", "UseCustomDistortion", If(m_HmdSettings.m_UseCustomDistortion, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("HmdSettings", "DistortionK0", m_HmdSettings.m_DistortionK0(True).ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("HmdSettings", "DistortionK1", m_HmdSettings.m_DistortionK1(True).ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("HmdSettings", "DistortionScale", m_HmdSettings.m_DistortionScale(True).ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("HmdSettings", "DistortionRedOffset", m_HmdSettings.m_DistortionRedOffset(True).ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("HmdSettings", "DistortionGreenOffset", m_HmdSettings.m_DistortionGreenOffset(True).ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("HmdSettings", "DistortionBlueOffset", m_HmdSettings.m_DistortionBlueOffset(True).ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("HmdSettings", "HFov", m_HmdSettings.m_HFov(True).ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("HmdSettings", "VFov", m_HmdSettings.m_VFov(True).ToString(Globalization.CultureInfo.InvariantCulture)))

                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("HmdSettings", "IPD", m_HmdSettings.m_IPD.ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("HmdSettings", "RenderScale", m_HmdSettings.m_RenderScale.ToString(Globalization.CultureInfo.InvariantCulture)))

                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("MiscSettings", "DisableBaseStationSpawning", If(g_mMiscSettings.m_DisableBaseStationSpawning, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("MiscSettings", "EnableHepticFeedback", If(g_mMiscSettings.m_EnableHepticFeedback, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("MiscSettings", "OptimizeTransportPackets", If(g_mMiscSettings.m_OptimizeTransportPackets, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("MiscSettings", "RenderWindowFix", If(g_mMiscSettings.m_RenderWindowFix, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("MiscSettings", "EnableVelocityHmd", If(g_mMiscSettings.m_EnableVelocityHmd, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("MiscSettings", "EnableVelocityController", If(g_mMiscSettings.m_EnableVelocityController, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("MiscSettings", "EnableVelocityTracker", If(g_mMiscSettings.m_EnableVelocityTracker, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("MiscSettings", "EnableVelocityInterpoliation", If(g_mMiscSettings.m_EnableVelocityInterpoliation, "true", "false")))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("MiscSettings", "OscRemoteIP", g_mMiscSettings.m_OscRemoteIP))
                        End If

                        If ((iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.PLAYSPACE) <> 0 OrElse (iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.ALL) <> 0) Then
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceSettings", "ForwardOffset", m_PlayspaceSettings.m_ForwardOffset.ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceSettings", "SideOffset", m_PlayspaceSettings.m_SideOffset.ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceSettings", "HeightOffset", m_PlayspaceSettings.m_HeightOffset.ToString(Globalization.CultureInfo.InvariantCulture)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceSettings", "ForwardMethod", CStr(CInt(m_PlayspaceSettings.m_ForwardMethod))))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceSettings", "AutoScale", If(m_PlayspaceSettings.m_AutoScale, "true", "false")))
                        End If

                        If ((iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.PLAYSPACE_CALIB_CONTROLLER) <> 0 OrElse (iSaveFlags And ENUM_SETTINGS_SAVE_TYPE_FLAGS.ALL) <> 0) Then
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceSettings", "CalibControllerId", CStr(m_PlayspaceSettings.m_CalibrationControllerId)))
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

                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "ScaleOffset", m_PlayspaceSettings.m_ScaleOffset.ToString(Globalization.CultureInfo.InvariantCulture)))

                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("PlayspaceCalibrationSettings", "Valid", If(m_PlayspaceSettings.m_Valid, "true", "false")))
                        End If

                        mIni.WriteKeyValue(mIniContent.ToArray)
                    End Using
                End Using
            End SyncLock
        End Sub

        Public Sub SetUnsavedState(bIsUnsaved As Boolean)
            If (bIsUnsaved) Then
                g_UCVirtualMotionTracker.g_UCVmtSettings.Button_SaveControllerSettings.Text = String.Format("Save Settings*")
                g_UCVirtualMotionTracker.g_UCVmtSettings.Button_SaveControllerSettings.Font = New Font(g_UCVirtualMotionTracker.g_UCVmtSettings.Button_SaveControllerSettings.Font, FontStyle.Bold)
            Else
                g_UCVirtualMotionTracker.g_UCVmtSettings.Button_SaveControllerSettings.Text = String.Format("Save Settings")
                g_UCVirtualMotionTracker.g_UCVmtSettings.Button_SaveControllerSettings.Font = New Font(g_UCVirtualMotionTracker.g_UCVmtSettings.Button_SaveControllerSettings.Font, FontStyle.Regular)
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
                mPos = Vector3.Zero
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

            Dim mClampedExecution As New ClassPrecisionSleep.ClassFrameTimed()

            While True
                Try
                    If (mClampedExecution.CanExecute()) Then
                        mClampedExecution.m_Framerate = g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_OscMaxThreadFps

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
                    End If
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLog(ex)
                End Try

                ClassPrecisionSleep.Sleep(g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_OscThreadSleepMs)
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
                            Dim mSerialRemove As New List(Of String)
                            For Each mDevice In g_mDevicesDic
                                If (mDeviceList.Exists(Function(x As STRUC_DEVICE) x.sSerial = mDevice.Value.sSerial)) Then
                                    Continue For
                                End If

                                RaiseEvent OnDeviceRemoved(mDevice.Value)

                                mSerialRemove.Add(mDevice.Value.sSerial)
                            Next

                            For Each sSerial As String In mSerialRemove
                                g_mDevicesDic.Remove(sSerial)
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
                ClassAdvancedExceptionLogging.WriteToLog(ex)
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
