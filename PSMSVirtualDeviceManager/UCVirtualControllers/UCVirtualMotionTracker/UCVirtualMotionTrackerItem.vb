Imports System.Numerics
Imports System.Text
Imports Rug.Osc
Imports PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants

Public Class UCVirtualMotionTrackerItem
    Const MAX_DRIVER_TIMEOUT As Integer = 5000
    Const MAX_CONTROLLER_TIMEOUT As Integer = 5000

    Shared _ThreadLock As New Object

    Public g_mUCVirtualMotionTracker As UCVirtualMotionTracker

    Public g_mClassIO As ClassIO
    Public g_mClassConfig As ClassConfig

    Private g_sTrackerName As String = ""
    Private g_sNickname As String = ""

    Private g_bIgnoreEvents As Boolean = False
    Private g_bIgnoreUnsaved As Boolean = False


    Private g_iStatusHideHeight As Integer = 0
    Private g_iStatusShowHeight As Integer = g_iStatusHideHeight

    Public g_mDriverLastResponse As New Stopwatch
    Public g_mControllerLastResponse As New Stopwatch
    Public g_sDriverLastResponseCode As Integer = 0
    Public g_sDriverLastResponseMessage As String = ""
    Public g_sDriverVersion As String = ""
    Public g_sDriverPath As String = ""

    Public Sub New(iControllerID As Integer, _UCVirtualMotionTracker As UCVirtualMotionTracker)
        g_mUCVirtualMotionTracker = _UCVirtualMotionTracker

        If (iControllerID < 0 OrElse iControllerID > ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1) Then
            iControllerID = -1
        End If

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.   
        Try
            g_bIgnoreEvents = True

            ComboBox_ControllerID.Items.Clear()
            For i = -1 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                ComboBox_ControllerID.Items.Add(CStr(i))
            Next
            ComboBox_ControllerID.SelectedIndex = 0

            If (iControllerID > -1) Then
                ComboBox_ControllerID.SelectedIndex = iControllerID + 1
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_VMTTrackerID.Items.Clear()
            For i = -1 To ClassVmtConst.VMT_TRACKER_MAX
                ComboBox_VMTTrackerID.Items.Add(CStr(i))
            Next
            ComboBox_VMTTrackerID.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_VMTTrackerRole.Items.Clear()
            ComboBox_VMTTrackerRole.Items.Add("Generic Tracker")
            ComboBox_VMTTrackerRole.Items.Add("Generic Left Controller")
            ComboBox_VMTTrackerRole.Items.Add("Generic Right Controller")
            ComboBox_VMTTrackerRole.Items.Add("HTC Vive Tracker")
            ComboBox_VMTTrackerRole.Items.Add("HTC Vive Left Controller")
            ComboBox_VMTTrackerRole.Items.Add("HTC Vive Right Controller")

            If (ComboBox_VMTTrackerRole.Items.Count <> ClassIO.ENUM_TRACKER_ROLE.__MAX) Then
                Throw New ArgumentException("Size not equal")
            End If

            ComboBox_VMTTrackerRole.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_SteamTrackerRole.Items.Clear()
            Dim mConfig As New ClassSteamVRConfig
            For i = ClassSteamVRConfig.ClassTrackerRoles.ENUM_TRACKER_ROLE_TYPE.INVALID To ClassSteamVRConfig.ClassTrackerRoles.ENUM_TRACKER_ROLE_TYPE.__MAX - 1
                ComboBox_SteamTrackerRole.Items.Add(mConfig.m_ClassTrackerRoles.GetTrackerRoleReadableName(CType(i, ClassSteamVRConfig.ClassTrackerRoles.ENUM_TRACKER_ROLE_TYPE)))
            Next
            ComboBox_SteamTrackerRole.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        g_mClassIO = New ClassIO(Me)
        g_mClassConfig = New ClassConfig(Me)

        g_mClassIO.m_Index = CInt(ComboBox_ControllerID.SelectedItem)
        g_mClassIO.Enable()

        g_mDriverLastResponse.Start()
        g_mControllerLastResponse.Start()

        SetUnsavedState(False)

        AddHandler g_mUCVirtualMotionTracker.g_ClassOscServer.OnOscProcessMessage, AddressOf OnOscProcessMessage
        AddHandler g_mUCVirtualMotionTracker.g_ClassOscServer.OnSuspendChanged, AddressOf OnOscSuspendChanged

        CreateControl()

        OnOscSuspendChanged()

        ' Hide timeout error
        Panel_Status.Visible = False
        g_iStatusHideHeight = (Me.Height - Panel_Status.Height - Panel_Status.Margin.Top)
        g_iStatusShowHeight = Me.Height
        Me.Height = g_iStatusHideHeight
    End Sub

    Private Sub OnOscSuspendChanged()
        If (g_mUCVirtualMotionTracker Is Nothing OrElse g_mUCVirtualMotionTracker.g_ClassOscServer Is Nothing) Then
            Return
        End If

        Dim bEnabled As Boolean = (Not g_mUCVirtualMotionTracker.g_ClassOscServer.IsRunning OrElse g_mUCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests)

        ComboBox_ControllerID.Enabled = bEnabled
        ComboBox_VMTTrackerID.Enabled = bEnabled
        ComboBox_VMTTrackerRole.Enabled = bEnabled
    End Sub

    Private Sub OnOscProcessMessage(mMessage As OscMessage)
        Try
            Select Case (mMessage.Address)
                Case "/VMT/Out/Log"
                    If (mMessage.Count < 2) Then
                        Return
                    End If

                    Dim iNum As Integer = CInt(mMessage(0))
                    Dim sMessage As String = CStr(mMessage(1))

                    ' #TODO Add log

                Case "/VMT/Out/Alive"
                    If (mMessage.Count < 1) Then
                        Return
                    End If

                    Dim sDriverVersion As String = CStr(mMessage(0))
                    Dim sDriverPath As String = ""
                    If (mMessage.Count > 1) Then
                        sDriverPath = CStr(mMessage(1))
                    End If

                    Me.BeginInvoke(Sub()
                                       g_mDriverLastResponse.Restart()

                                       g_sDriverVersion = sDriverVersion
                                       g_sDriverPath = sDriverPath
                                   End Sub)
                Case "/VMT/Out/Haptic"
                    ' No Le Hep
                Case "/VMT/Out/Unavailable"
                    If (mMessage.Count < 2) Then
                        Return
                    End If

                    Dim iCode As Integer = CInt(mMessage(0))
                    Dim sReason As String = CStr(mMessage(1))

                    Select Case (iCode)
                        Case 1 'Room matrix not setup
                            If (g_mUCVirtualMotionTracker.g_ClassOscServer.IsRunning) Then
                                g_mUCVirtualMotionTracker.g_ClassOscServer.Send(New OscMessage(
                                    "/VMT/SetRoomMatrix", New Object() {
                                        1.0F, 0F, 0F, 0F,
                                        0F, 1.0F, 0F, 0F,
                                        0F, 0F, 1.0F, 0F
                                    }))
                            End If
                    End Select

                    If (iCode = 0) Then
                        Me.BeginInvoke(Sub()
                                           g_mDriverLastResponse.Restart()

                                           g_sDriverLastResponseCode = iCode
                                           g_sDriverLastResponseMessage = sReason
                                       End Sub)
                    Else
                        Me.BeginInvoke(Sub()
                                           g_mDriverLastResponse.Restart()

                                           g_sDriverLastResponseCode = iCode
                                           g_sDriverLastResponseMessage = sReason
                                       End Sub)
                    End If

            End Select
        Catch ex As Exception
            ' Something sussy is going on...
        End Try
    End Sub

    Private Sub SetUnsavedState(bIsUnsaved As Boolean)
        If (g_bIgnoreUnsaved) Then
            Return
        End If

        If (bIsUnsaved) Then
            Button_SaveSettings.Text = String.Format("Save Settings*")
            Button_SaveSettings.Font = New Font(Button_SaveSettings.Font, FontStyle.Bold)
        Else
            Button_SaveSettings.Text = String.Format("Save Settings")
            Button_SaveSettings.Font = New Font(Button_SaveSettings.Font, FontStyle.Regular)
        End If
    End Sub

    Private Sub UpdateTrackerRoleComboBox()
        Try
            g_bIgnoreEvents = True

            If (g_mClassIO.m_VmtTracker < 0) Then
                ComboBox_SteamTrackerRole.SelectedIndex = 0
                Return
            End If

            Dim mConfig As New ClassSteamVRConfig
            If (mConfig.LoadConfig()) Then
                Dim sTrackerName As String = (ClassVmtConst.VMT_DEVICE_SERIAL & g_mClassIO.m_VmtTracker)
                Dim sTrackerRole As String = mConfig.m_ClassTrackerRoles.GetTrackerRole(sTrackerName)
                If (sTrackerRole Is Nothing) Then
                    ComboBox_SteamTrackerRole.SelectedIndex = 0
                    Return
                End If

                ComboBox_SteamTrackerRole.SelectedIndex = mConfig.m_ClassTrackerRoles.GetTrackerRoleFromName(sTrackerRole) + 1
            Else
                ComboBox_SteamTrackerRole.SelectedIndex = 0
            End If
        Catch ex As Exception
            ComboBox_SteamTrackerRole.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub UCRemoteDeviceItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Try
                g_bIgnoreUnsaved = True
                g_mClassConfig.LoadConfig()
            Finally
                g_bIgnoreUnsaved = False
            End Try

            UpdateTrackerRoleComboBox()

            SetUnsavedState(False)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboBox_ControllerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_ControllerID.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Try
            g_bIgnoreUnsaved = True
            g_mClassConfig.LoadConfig()
        Finally
            g_bIgnoreUnsaved = False
        End Try

        g_mClassIO.m_Index = CInt(ComboBox_ControllerID.SelectedItem)
        g_mClassIO.Enable()

        UpdateTrackerRoleComboBox()

        SetUnsavedState(False)
    End Sub

    Private Sub ComboBox_VMTTrackerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_VMTTrackerID.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassIO.m_VmtTracker = CInt(ComboBox_VMTTrackerID.SelectedItem)
        SetUnsavedState(True)

        UpdateTrackerRoleComboBox()
    End Sub

    Private Sub ComboBox_VMTTrackerRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_VMTTrackerRole.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassIO.m_VmtTrackerRole = CType(ComboBox_VMTTrackerRole.SelectedIndex, ClassIO.ENUM_TRACKER_ROLE)
        SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_TrackerRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_SteamTrackerRole.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        SetUnsavedState(True)
    End Sub

    Private Sub Button_SaveSettings_Click(sender As Object, e As EventArgs) Handles Button_SaveSettings.Click
        Try
            g_mClassConfig.SaveConfig()
            SetUnsavedState(False)

            Dim mConfig As New ClassSteamVRConfig
            If (mConfig.LoadConfig()) Then
                Dim sTrackerName As String = (ClassVmtConst.VMT_DEVICE_SERIAL & g_mClassIO.m_VmtTracker)

                If (ComboBox_SteamTrackerRole.SelectedIndex - 1 <= ClassSteamVRConfig.ClassTrackerRoles.ENUM_TRACKER_ROLE_TYPE.INVALID) Then
                    mConfig.m_ClassTrackerRoles.RemoveTrackerRole(sTrackerName)
                Else
                    mConfig.m_ClassTrackerRoles.SetTrackerRole(sTrackerName, CType(ComboBox_SteamTrackerRole.SelectedIndex - 1, ClassSteamVRConfig.ClassTrackerRoles.ENUM_TRACKER_ROLE_TYPE))
                End If

                mConfig.SaveConfig()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TimerFPS_Tick(sender As Object, e As EventArgs) Handles TimerFPS.Tick
        Try
            TimerFPS.Stop()

            SyncLock _ThreadLock
                TextBox_Fps.Text = String.Format("OSC IO: {0}/s", g_mClassIO.m_FpsOscCounter)

                If (g_mClassIO.m_FpsOscCounter > 0) Then
                    g_mControllerLastResponse.Restart()
                End If

                g_mClassIO.m_FpsOscCounter = 0
            End SyncLock
        Finally
            TimerFPS.Start()
        End Try
    End Sub

    Private Sub TimerPose_Tick(sender As Object, e As EventArgs) Handles TimerPose.Tick
        Try
            TimerPose.Stop()

            SyncLock _ThreadLock
                If (g_mClassIO.m_ControllerData IsNot Nothing) Then
                    TextBox_Pos.Text = String.Format("Pos X: {0}{3}Pos Y: {1}{3}Pos Z: {2}", Math.Floor(g_mClassIO.m_ControllerData.m_Position.X), Math.Floor(g_mClassIO.m_ControllerData.m_Position.Y), Math.Floor(g_mClassIO.m_ControllerData.m_Position.Z), Environment.NewLine)

                    Dim iAng = ClassQuaternionTools.FromQ2(g_mClassIO.m_ControllerData.m_Orientation)
                    TextBox_Gyro.Text = String.Format("Ang X: {0}{3}Ang Y: {1}{3}Ang Z: {2}", Math.Floor(iAng.X), Math.Floor(iAng.Y), Math.Floor(iAng.Z), Environment.NewLine)
                Else
                    TextBox_Pos.Text = String.Format("Pos X: {0}{3}Pos Y: {1}{3}Pos Z: {2}", "N/A", "N/A", "N/A", Environment.NewLine)
                    TextBox_Gyro.Text = String.Format("Ang X: {0}{3}Ang Y: {1}{3}Ang Z: {2}", "N/A", "N/A", "N/A", Environment.NewLine)
                End If

            End SyncLock
        Finally
            TimerPose.Start()
        End Try
    End Sub


    Private Sub Timer_Status_Tick(sender As Object, e As EventArgs) Handles Timer_Status.Tick
        Try
            Timer_Status.Stop()

            Dim sTitle As String = ""
            Dim sMessage As String = ""
            Dim iStatusType As Integer = -1 ' -1 Hide, 0 Info, 1 Warn, 2 Error

            While True
                If (g_mUCVirtualMotionTracker.g_ClassOscServer.IsRunning) Then
                    ' Show driver timeouts
                    If (g_mDriverLastResponse.ElapsedMilliseconds > MAX_DRIVER_TIMEOUT) Then
                        sTitle = "Driver not is responding!"

                        Dim sText As New Text.StringBuilder
                        sText.AppendLine("The driver is not responding! Make sure the OSC server is running and the driver installed correctly.")

                        If (Not String.IsNullOrEmpty(g_sDriverVersion)) Then
                            sText.AppendFormat("VMT driver version: {0}", g_sDriverVersion).AppendLine()
                        End If
                        If (Not String.IsNullOrEmpty(g_sDriverPath)) Then
                            sText.AppendFormat("VMT driver path: {0}", g_sDriverPath).AppendLine()
                        End If

                        sMessage = sText.ToString
                        iStatusType = 2

                        Exit While
                    End If


                    If (g_sDriverLastResponseCode <> 0) Then
                        sTitle = String.Format("Driver responded with an error (Code: {0})!", g_sDriverLastResponseCode)

                        Dim sText As New Text.StringBuilder
                        sText.AppendLine(g_sDriverLastResponseMessage)

                        If (Not String.IsNullOrEmpty(g_sDriverVersion)) Then
                            sText.AppendFormat("VMT driver version: {0}", g_sDriverVersion).AppendLine()
                        End If
                        If (Not String.IsNullOrEmpty(g_sDriverPath)) Then
                            sText.AppendFormat("VMT driver path: {0}", g_sDriverPath).AppendLine()
                        End If

                        sMessage = sText.ToString
                        iStatusType = 2

                        Exit While
                    End If

                    ' Show user wrong driver version
                    If (Not String.IsNullOrEmpty(g_sDriverVersion) AndAlso g_sDriverVersion <> ClassVmtConst.VMT_DRIVER_VERSION_EXPECT) Then
                        sTitle = "Driver running but might be incompatible"


                        Dim sText As New Text.StringBuilder
                        sText.AppendFormat("The driver reported version '{0}' but required is {1}! This may cause problems.", g_sDriverVersion, ClassVmtConst.VMT_DRIVER_VERSION_EXPECT).AppendLine()

                        If (Not String.IsNullOrEmpty(g_sDriverVersion)) Then
                            sText.AppendFormat("VMT driver version: {0}", g_sDriverVersion).AppendLine()
                        End If
                        If (Not String.IsNullOrEmpty(g_sDriverPath)) Then
                            sText.AppendFormat("VMT driver path: {0}", g_sDriverPath).AppendLine()
                        End If

                        sMessage = sText.ToString
                        iStatusType = 1

                        Exit While
                    End If

                    ' Show tracker not working
                    If (g_mControllerLastResponse.ElapsedMilliseconds > MAX_CONTROLLER_TIMEOUT) Then
                        sTitle = "Controller is not responding!"

                        Dim sText As New Text.StringBuilder
                        sText.AppendLine("There are no new incoming pose data. Make sure PSMoveServiceEx is running.")

                        sMessage = sText.ToString
                        iStatusType = 2

                        Exit While
                    End If
                End If

                Exit While
            End While

            If (Label_StatusTitle.Text <> sTitle OrElse Label_StatusMessage.Text <> sMessage) Then
                Label_StatusTitle.Text = sTitle
                Label_StatusMessage.Text = sMessage
            End If

            If (iStatusType < 0) Then
                If (Panel_Status.Visible) Then
                    Panel_Status.Visible = False

                    Me.Height = g_iStatusHideHeight
                End If
            Else
                If (Not Panel_Status.Visible) Then
                    Panel_Status.Visible = True

                    Me.Height = g_iStatusShowHeight
                End If
            End If
        Finally
            Timer_Status.Start()
        End Try
    End Sub

    Private Sub CleanUp()
        If (g_mUCVirtualMotionTracker IsNot Nothing AndAlso g_mUCVirtualMotionTracker.g_ClassOscServer IsNot Nothing) Then
            RemoveHandler g_mUCVirtualMotionTracker.g_ClassOscServer.OnOscProcessMessage, AddressOf OnOscProcessMessage
        End If

        If (g_mUCVirtualMotionTracker IsNot Nothing AndAlso g_mUCVirtualMotionTracker.g_ClassOscServer IsNot Nothing) Then
            RemoveHandler g_mUCVirtualMotionTracker.g_ClassOscServer.OnSuspendChanged, AddressOf OnOscSuspendChanged
        End If

        If (g_mClassIO IsNot Nothing) Then
            g_mClassIO.Dispose()
            g_mClassIO = Nothing
        End If
    End Sub

    Public Class ClassIO
        Implements IDisposable

        Public _ThreadLock As New Object
        Public g_UCVirtualMotionTrackerItem As UCVirtualMotionTrackerItem

        Enum ENUM_TRACKER_ROLE
            GENERIC_TRACKER
            GENERIC_LEFT_CONTROLLER
            GENERIC_RIGHT_CONTROLLER
            HTC_VIVE_TRACKER
            HTC_VIVE_LEFT_CONTROLLER
            HTC_VIVE_RIGHT_CONTROLLER

            __MAX
        End Enum

        Private g_iIndex As Integer = -1
        Private g_iVmtTracker As Integer = -1
        Private g_iVmtTrackerRole As ENUM_TRACKER_ROLE = ENUM_TRACKER_ROLE.GENERIC_TRACKER
        Private g_mOscThread As Threading.Thread = Nothing

        Private g_mJointOffset As New Vector3(0, 0, 0)
        Private g_mControllerOffset As New Vector3(0, 0, 0)
        Private g_iJointYawCorrection As Integer = 0
        Private g_iControllerYawCorrection As Integer = 0
        Private g_bOnlyJointOffset As Boolean = False

        Private g_iFpsOscCounter As Integer = 0
        Private g_mControllerData As ClassServiceClient.IControllerData
        Private g_mTrackerData As New Dictionary(Of Integer, ClassServiceClient.STRUC_TRACKER_DATA)

        Private g_mOscDataPack As New STRUC_OSC_DATA_PACK()

        Class STRUC_OSC_DATA_PACK
            Public mPosition As New Vector3(0, 0, 0)
            Public mOrientation As New Quaternion(0, 0, 0, 1)

            Public mButtons As New Dictionary(Of Integer, Boolean)
            Public mTrigger As New Dictionary(Of Integer, Single)
            Public mJoyStick As New Vector2()
        End Class

        Public Sub New(_UCVirtualMotionTrackerItem As UCVirtualMotionTrackerItem)
            g_UCVirtualMotionTrackerItem = _UCVirtualMotionTrackerItem
        End Sub

        Property m_Index As Integer
            Get
                Return g_iIndex
            End Get
            Set(value As Integer)
                If (g_mOscThread IsNot Nothing AndAlso g_mOscThread.IsAlive) Then
                    Disable()
                    g_iIndex = value
                    Enable()
                Else
                    g_iIndex = value
                End If
            End Set
        End Property

        Property m_VmtTracker As Integer
            Get
                SyncLock _ThreadLock
                    Return g_iVmtTracker
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock _ThreadLock
                    g_iVmtTracker = value
                End SyncLock
            End Set
        End Property

        Property m_VmtTrackerRole As ENUM_TRACKER_ROLE
            Get
                SyncLock _ThreadLock
                    Return g_iVmtTrackerRole
                End SyncLock
            End Get
            Set(value As ENUM_TRACKER_ROLE)
                SyncLock _ThreadLock
                    g_iVmtTrackerRole = value
                End SyncLock
            End Set
        End Property

        Public Sub Enable()
            If (g_iIndex < 0) Then
                Return
            End If

            If (g_mOscThread IsNot Nothing AndAlso g_mOscThread.IsAlive) Then
                Return
            End If

            g_mOscThread = New Threading.Thread(AddressOf ThreadOsc)
            g_mOscThread.IsBackground = True
            g_mOscThread.Start()
        End Sub

        Property m_FpsOscCounter As Integer
            Get
                SyncLock _ThreadLock
                    Return g_iFpsOscCounter
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock _ThreadLock
                    g_iFpsOscCounter = value
                End SyncLock
            End Set
        End Property

        Property m_ControllerData As ClassServiceClient.IControllerData
            Get
                SyncLock _ThreadLock
                    Return g_mControllerData
                End SyncLock
            End Get
            Set(value As ClassServiceClient.IControllerData)
                SyncLock _ThreadLock
                    g_mControllerData = value
                End SyncLock
            End Set
        End Property

        Property m_TrackerData(iIndex As Integer) As ClassServiceClient.STRUC_TRACKER_DATA
            Get
                SyncLock _ThreadLock
                    Return g_mTrackerData(iIndex)
                End SyncLock
            End Get
            Set(value As ClassServiceClient.STRUC_TRACKER_DATA)
                SyncLock _ThreadLock
                    g_mTrackerData(iIndex) = value
                End SyncLock
            End Set
        End Property

        Public Sub Disable()
            If (g_mOscThread Is Nothing OrElse Not g_mOscThread.IsAlive) Then
                Return
            End If

            g_mOscThread.Abort()
            g_mOscThread.Join()
            g_mOscThread = Nothing
        End Sub

        Private Sub ThreadOsc()
            Const TOUCHPAD_AXIS_UNITS As Single = 7.5F

            Dim iLastOutputSeqNum As Integer = 0
            Dim bJoystickButtonPressed As Boolean = False
            Dim bGripButtonPressed As Boolean = False
            Dim mJoystickButtonPressedTime As New Stopwatch
            Dim mJoystickPressedLastOrientation As New Quaternion
            Dim mJoystickPressedLastPosition As New Vector3
            Dim mJoystickShortcuts As New Dictionary(Of Integer, Vector2)
            Dim bGripToggled As Boolean = False

            Dim mTrackerDataUpdate As New Stopwatch
            mTrackerDataUpdate.Restart()

            Const ENABLE_TRACKER As Integer = 1
            Const ENABLE_CONTROLLER_L As Integer = 2
            Const ENABLE_CONTROLLER_R As Integer = 3
            Const ENABLE_TRACKINGREFECNCE As Integer = 4
            Const ENABLE_HTC_VIVE_TRACKER As Integer = 5
            Const ENABLE_HTC_VIVE_CONTROLLER_L As Integer = 6
            Const ENABLE_HTC_VIVE_CONTROLLER_R As Integer = 7
            Const ENABLE_HTC_TRACKINGREFERENCE As Integer = 8

            Const GEN_BUTTON_MOVE = 0
            Const GEN_BUTTON_MENU = 1
            Const GEN_BUTTON_START = 2
            Const GEN_BUTTON_SEELCT = 3
            Const GEN_BUTTON_SQUARE = 4
            Const GEN_BUTTON_CROSS = 5
            Const GEN_BUTTON_CIRCLE = 6
            Const GEN_BUTTON_TRIANGLE = 7

            Const HTC_VIVE_BUTTON_SYSTEM_CLICK = 0
            Const HTC_VIVE_BUTTON_TRIGGER_CLICK = 1
            Const HTC_VIVE_BUTTON_TRACKPAD_TOUCH = 2
            Const HTC_VIVE_BUTTON_TRACKPAD_CLICK = 3
            Const HTC_VIVE_BUTTON_GRIP_CLICK = 4
            Const HTC_VIVE_BUTTON_MENU_CLICK = 5

            While True
                Try
                    If (g_iIndex < 0) Then
                        Return
                    End If

                    If (Not g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.IsRunning) Then
                        Throw New ArgumentException("OSC server is not running")
                    End If

                    If (g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
                        Throw New ArgumentException("OSC server is suspended")
                    End If

                    ' Get controller data
                    m_ControllerData = ClassServiceClient.m_ControllerData(g_iIndex)

                    If (m_ControllerData IsNot Nothing) Then
                        ' We got any new data?
                        If (iLastOutputSeqNum <> m_ControllerData.m_OutputSeqNum) Then
                            iLastOutputSeqNum = m_ControllerData.m_OutputSeqNum

                            ' Get controller settings
                            Dim mClassControllerSettings As UCVirtualMotionTracker.ClassControllerSettings = g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassControllerSettings
                            Dim g_bJoystickShortcutBinding As Boolean = mClassControllerSettings.m_JoystickShortcutBinding
                            Dim g_bJoystickShortcutTouchpadClick As Boolean = mClassControllerSettings.m_JoystickShortcutTouchpadClick
                            Dim g_iHtcTouchpadEmulationClickMethod = mClassControllerSettings.m_HtcTouchpadEmulationClickMethod
                            Dim g_iHtcGripButtonMethod = mClassControllerSettings.m_HtcGripButtonMethod

                            SyncLock _ThreadLock
                                g_mOscDataPack.mOrientation = m_ControllerData.m_Orientation
                                g_mOscDataPack.mPosition = m_ControllerData.m_Position * CSng(PSM_CENTIMETERS_TO_METERS)

                                Select Case (m_VmtTrackerRole)
                                    Case ENUM_TRACKER_ROLE.GENERIC_LEFT_CONTROLLER,
                                            ENUM_TRACKER_ROLE.GENERIC_RIGHT_CONTROLLER,
                                            ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER,
                                            ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER

                                        Select Case (True)
                                            Case (TypeOf m_ControllerData Is ClassServiceClient.STRUC_PSMOVE_CONTROLLER_DATA)
                                                Dim m_PSMoveData = DirectCast(m_ControllerData, ClassServiceClient.STRUC_PSMOVE_CONTROLLER_DATA)

                                                Dim mButtons As Boolean() = New Boolean() {
                                                    m_PSMoveData.m_MoveButton,
                                                    m_PSMoveData.m_PSButton,
                                                    m_PSMoveData.m_StartButton,
                                                    m_PSMoveData.m_SelectButton,
                                                    m_PSMoveData.m_SquareButton,
                                                    m_PSMoveData.m_CrossButton,
                                                    m_PSMoveData.m_CircleButton,
                                                    m_PSMoveData.m_TriangleButton
                                                }

                                                Dim bJoystickTrigger As Boolean = m_PSMoveData.m_MoveButton

                                                ' Send buttons
                                                Select Case (m_VmtTrackerRole)
                                                    Case ENUM_TRACKER_ROLE.GENERIC_LEFT_CONTROLLER,
                                                           ENUM_TRACKER_ROLE.GENERIC_RIGHT_CONTROLLER
                                                        For i = 0 To mButtons.Length - 1
                                                            g_mOscDataPack.mButtons(i) = mButtons(i)
                                                        Next

                                                    Case ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER,
                                                          ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER

                                                        g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_SYSTEM_CLICK) = m_PSMoveData.m_StartButton
                                                        g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRIGGER_CLICK) = ((m_PSMoveData.m_TriggerValue / 255.0F) > 0.75F)
                                                        g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_TOUCH) = bJoystickTrigger
                                                        g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = False
                                                        g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = False
                                                        g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_MENU_CLICK) = m_PSMoveData.m_PSButton

                                                        ' Do grip button
                                                        Select Case (g_iHtcGripButtonMethod)
                                                            Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_HOLDING_MIRRORED
                                                                If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER) Then
                                                                    bGripButtonPressed = m_PSMoveData.m_CircleButton
                                                                    g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = m_PSMoveData.m_CircleButton
                                                                Else
                                                                    bGripButtonPressed = m_PSMoveData.m_CrossButton
                                                                    g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = m_PSMoveData.m_CrossButton
                                                                End If

                                                            Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_HOLDING_STRICT_CIRCLE
                                                                bGripButtonPressed = m_PSMoveData.m_CircleButton
                                                                g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = m_PSMoveData.m_CircleButton

                                                            Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_HOLDING_STRICT_CROSS
                                                                bGripButtonPressed = m_PSMoveData.m_CrossButton
                                                                g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = m_PSMoveData.m_CrossButton

                                                            Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_MIRRORED
                                                                If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER) Then
                                                                    If (m_PSMoveData.m_CircleButton) Then
                                                                        If (Not bGripButtonPressed) Then
                                                                            bGripButtonPressed = True

                                                                            bGripToggled = Not bGripToggled
                                                                        End If
                                                                    Else
                                                                        If (bGripButtonPressed) Then
                                                                            bGripButtonPressed = False
                                                                        End If
                                                                    End If
                                                                Else
                                                                    If (m_PSMoveData.m_CrossButton) Then
                                                                        If (Not bGripButtonPressed) Then
                                                                            bGripButtonPressed = True

                                                                            bGripToggled = Not bGripToggled
                                                                        End If
                                                                    Else
                                                                        If (bGripButtonPressed) Then
                                                                            bGripButtonPressed = False
                                                                        End If
                                                                    End If
                                                                End If

                                                                g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = bGripToggled

                                                            Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_STRICT_CIRCLE
                                                                If (m_PSMoveData.m_CircleButton) Then
                                                                    If (Not bGripButtonPressed) Then
                                                                        bGripButtonPressed = True

                                                                        bGripToggled = Not bGripToggled
                                                                    End If
                                                                Else
                                                                    If (bGripButtonPressed) Then
                                                                        bGripButtonPressed = False
                                                                    End If
                                                                End If

                                                                g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = bGripToggled

                                                            Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_STRICT_CROSS
                                                                If (m_PSMoveData.m_CrossButton) Then
                                                                    If (Not bGripButtonPressed) Then
                                                                        bGripButtonPressed = True

                                                                        bGripToggled = Not bGripToggled
                                                                    End If
                                                                Else
                                                                    If (bGripButtonPressed) Then
                                                                        bGripButtonPressed = False
                                                                    End If
                                                                End If

                                                                g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = bGripToggled

                                                        End Select

                                                        ' Do touchpad click
                                                        Select Case (g_iHtcTouchpadEmulationClickMethod)
                                                            Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED
                                                                If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER) Then
                                                                    If (m_PSMoveData.m_TriangleButton) Then
                                                                        bJoystickTrigger = True
                                                                    End If
                                                                Else
                                                                    If (m_PSMoveData.m_SquareButton) Then
                                                                        bJoystickTrigger = True
                                                                    End If
                                                                End If

                                                            Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_SQUARE
                                                                If (m_PSMoveData.m_SquareButton) Then
                                                                    bJoystickTrigger = True
                                                                End If

                                                            Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_TRIANGLE
                                                                If (m_PSMoveData.m_TriangleButton) Then
                                                                    bJoystickTrigger = True
                                                                End If
                                                        End Select
                                                End Select

                                                g_mOscDataPack.mTrigger(0) = (m_PSMoveData.m_TriggerValue / 255.0F)

                                                ' Joystick emulation
                                                If (bJoystickTrigger) Then
                                                    If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER OrElse
                                                        m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER) Then

                                                        g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_TOUCH) = True
                                                    End If

                                                    ' Just pressed
                                                    If (Not bJoystickButtonPressed) Then
                                                        bJoystickButtonPressed = True

                                                        mJoystickButtonPressedTime.Restart()

                                                        mJoystickPressedLastOrientation = m_PSMoveData.m_Orientation
                                                        mJoystickPressedLastPosition = ClassQuaternionTools.GetPositionInRotationSpace(mJoystickPressedLastOrientation, m_PSMoveData.m_Position)
                                                    End If

                                                    Dim mNewPos As Vector3 = ClassQuaternionTools.GetPositionInRotationSpace(mJoystickPressedLastOrientation, m_PSMoveData.m_Position)

                                                    mNewPos = ((mNewPos - mJoystickPressedLastPosition) / TOUCHPAD_AXIS_UNITS)
                                                    mNewPos.X = Math.Min(Math.Max(mNewPos.X, -1.0F), 1.0F)
                                                    mNewPos.Z = Math.Min(Math.Max(mNewPos.Z, -1.0F), 1.0F)

                                                    g_mOscDataPack.mJoyStick = New Vector2(mNewPos.X, -mNewPos.Z)

                                                    If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER OrElse
                                                        m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER) Then

                                                        ' Only start pressing when we moved a distance
                                                        If (Math.Abs(mNewPos.X) > 0.25F OrElse Math.Abs(mNewPos.Y) > 0.25F) Then
                                                            Select Case (g_iHtcTouchpadEmulationClickMethod)
                                                                Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_TOUCHPAD_CLICK_METHOD.MOVE_ALWAYS
                                                                    g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True

                                                                Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED
                                                                    If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER) Then
                                                                        If (m_PSMoveData.m_TriangleButton) Then
                                                                            g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                                                        End If
                                                                    Else
                                                                        If (m_PSMoveData.m_SquareButton) Then
                                                                            g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                                                        End If
                                                                    End If

                                                                Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_SQUARE
                                                                    If (m_PSMoveData.m_SquareButton) Then
                                                                        g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                                                    End If

                                                                Case UCVirtualMotionTracker.ClassControllerSettings.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_TRIANGLE
                                                                    If (m_PSMoveData.m_TriangleButton) Then
                                                                        g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                                                    End If
                                                            End Select
                                                        End If
                                                    End If

                                                    If (g_bJoystickShortcutBinding AndAlso m_PSMoveData.m_MoveButton) Then
                                                        ' Record joystick shortcut while MOVE button is pressed 
                                                        ' Also skip MOVE button
                                                        For i = 1 To mButtons.Length - 1
                                                            If (mButtons(i)) Then
                                                                If (Math.Abs(mNewPos.X) < 0.25F AndAlso Math.Abs(mNewPos.Z) < 0.25F) Then
                                                                    ' Remove shortcut
                                                                    If (mJoystickShortcuts.ContainsKey(i)) Then
                                                                        mJoystickShortcuts.Remove(i)
                                                                    End If
                                                                Else
                                                                    ' Create shortcut
                                                                    mJoystickShortcuts(i) = g_mOscDataPack.mJoyStick
                                                                End If
                                                            End If
                                                        Next
                                                    End If
                                                Else
                                                    If (bJoystickButtonPressed) Then
                                                        bJoystickButtonPressed = False
                                                    End If

                                                    g_mOscDataPack.mJoyStick = New Vector2(0.0F, 0.0F)

                                                    If (g_bJoystickShortcutBinding) Then
                                                        ' Record joystick shortcut while MOVE button is pressed
                                                        For i = 1 To mButtons.Length - 1
                                                            If (mButtons(i)) Then
                                                                If (mJoystickShortcuts.ContainsKey(i)) Then
                                                                    g_mOscDataPack.mJoyStick = mJoystickShortcuts(i)

                                                                    If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER OrElse
                                                                            m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER) Then

                                                                        g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_TOUCH) = True

                                                                        If (g_bJoystickShortcutTouchpadClick) Then
                                                                            g_mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                                                        End If
                                                                    End If

                                                                    ' Never press the shortcut button, just in case its mapped
                                                                    ' g_mOscDataPack.mButtons(i) = False
                                                                End If
                                                            End If
                                                        Next
                                                    End If
                                                End If
                                        End Select
                                End Select

                            End SyncLock

                            Select Case (m_VmtTrackerRole)
                                Case ENUM_TRACKER_ROLE.GENERIC_TRACKER
                                    'Use Right-Handed space for SteamVR 
                                    g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                        New OscMessage(
                                            "/VMT/Room/Driver",
                                            m_VmtTracker, ENABLE_TRACKER, 0.0F,
                                            g_mOscDataPack.mPosition.X,
                                            g_mOscDataPack.mPosition.Y,
                                            g_mOscDataPack.mPosition.Z,
                                            g_mOscDataPack.mOrientation.X,
                                            g_mOscDataPack.mOrientation.Y,
                                            g_mOscDataPack.mOrientation.Z,
                                            g_mOscDataPack.mOrientation.W
                                        ))

                                Case ENUM_TRACKER_ROLE.GENERIC_LEFT_CONTROLLER,
                                     ENUM_TRACKER_ROLE.GENERIC_RIGHT_CONTROLLER

                                    Dim iController As Integer = ENABLE_TRACKER
                                    Select Case (m_VmtTrackerRole)
                                        Case ENUM_TRACKER_ROLE.GENERIC_LEFT_CONTROLLER
                                            iController = ENABLE_CONTROLLER_L

                                        Case ENUM_TRACKER_ROLE.GENERIC_RIGHT_CONTROLLER
                                            iController = ENABLE_CONTROLLER_R
                                    End Select

                                    For Each mButton In g_mOscDataPack.mButtons
                                        g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                            New OscMessage(
                                                "/VMT/Input/Button",
                                                m_VmtTracker, mButton.Key, 0.0F, CInt(mButton.Value)
                                            ))
                                    Next

                                    For Each mTrigger In g_mOscDataPack.mTrigger
                                        g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                           New OscMessage(
                                               "/VMT/Input/Trigger",
                                               m_VmtTracker, mTrigger.Key, 0.0F, mTrigger.Value
                                           ))
                                    Next

                                    g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                        New OscMessage(
                                            "/VMT/Input/Joystick",
                                            m_VmtTracker, 0, 0.0F, g_mOscDataPack.mJoyStick.X, g_mOscDataPack.mJoyStick.Y
                                        ))

                                    'Use Right-Handed space for SteamVR 
                                    g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                        New OscMessage(
                                            "/VMT/Room/Driver",
                                            m_VmtTracker, iController, 0.0F,
                                            g_mOscDataPack.mPosition.X,
                                            g_mOscDataPack.mPosition.Y,
                                            g_mOscDataPack.mPosition.Z,
                                            g_mOscDataPack.mOrientation.X,
                                            g_mOscDataPack.mOrientation.Y,
                                            g_mOscDataPack.mOrientation.Z,
                                            g_mOscDataPack.mOrientation.W
                                        ))

                                Case ENUM_TRACKER_ROLE.HTC_VIVE_TRACKER
                                    'Use Right-Handed space for SteamVR 
                                    g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                        New OscMessage(
                                            "/VMT/Room/Driver",
                                            m_VmtTracker, ENABLE_HTC_VIVE_TRACKER, 0.0F,
                                            g_mOscDataPack.mPosition.X,
                                            g_mOscDataPack.mPosition.Y,
                                            g_mOscDataPack.mPosition.Z,
                                            g_mOscDataPack.mOrientation.X,
                                            g_mOscDataPack.mOrientation.Y,
                                            g_mOscDataPack.mOrientation.Z,
                                            g_mOscDataPack.mOrientation.W
                                        ))

                                Case ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER,
                                     ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER

                                    Dim iController As Integer = ENABLE_HTC_VIVE_TRACKER
                                    Select Case (m_VmtTrackerRole)
                                        Case ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER
                                            iController = ENABLE_HTC_VIVE_CONTROLLER_L

                                        Case ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER
                                            iController = ENABLE_HTC_VIVE_CONTROLLER_R
                                    End Select

                                    For Each mButton In g_mOscDataPack.mButtons
                                        g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                            New OscMessage(
                                                "/VMT/Input/Button",
                                                m_VmtTracker, mButton.Key, 0.0F, CInt(mButton.Value)
                                            ))
                                    Next

                                    For Each mTrigger In g_mOscDataPack.mTrigger
                                        g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                           New OscMessage(
                                               "/VMT/Input/Trigger",
                                               m_VmtTracker, mTrigger.Key, 0.0F, mTrigger.Value
                                           ))
                                    Next

                                    g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                        New OscMessage(
                                            "/VMT/Input/Joystick",
                                            m_VmtTracker, 0, 0.0F, g_mOscDataPack.mJoyStick.X, g_mOscDataPack.mJoyStick.Y
                                        ))

                                    'Use Right-Handed space for SteamVR 
                                    g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                        New OscMessage(
                                            "/VMT/Room/Driver",
                                            m_VmtTracker, iController, 0.0F,
                                            g_mOscDataPack.mPosition.X,
                                            g_mOscDataPack.mPosition.Y,
                                            g_mOscDataPack.mPosition.Z,
                                            g_mOscDataPack.mOrientation.X,
                                            g_mOscDataPack.mOrientation.Y,
                                            g_mOscDataPack.mOrientation.Z,
                                            g_mOscDataPack.mOrientation.W
                                        ))

                            End Select

                            m_FpsOscCounter += 1
                        End If
                    End If

                    ' Update tracker references
                    ' $TODO Add them into their own thread?
                    If (mTrackerDataUpdate.Elapsed > New TimeSpan(0, 0, 10)) Then
                        mTrackerDataUpdate.Restart()

                        For i = 0 To PSMOVESERVICE_MAX_TRACKER_COUNT - 1
                            m_TrackerData(i) = ClassServiceClient.m_TrackerData(i)

                            Dim mPosition As Vector3 = m_TrackerData(i).m_Position * CSng(PSM_CENTIMETERS_TO_METERS)

                            ' Cameras are flipped, flip them correctly
                            Dim mFlippedQ As Quaternion = m_TrackerData(i).m_Orientation * Quaternion.CreateFromAxisAngle(Vector3.UnitY, 180.0F * (Math.PI / 180.0F))

                            'Use Right-Handed space for SteamVR 
                            g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                New OscMessage(
                                    "/VMT/Room/Driver",
                                    ClassVmtConst.VMT_TRACKER_MAX + i + 1, ENABLE_HTC_TRACKINGREFERENCE, 0.0F,
                                    mPosition.X,
                                    mPosition.Y,
                                    mPosition.Z,
                                    mFlippedQ.X,
                                    mFlippedQ.Y,
                                    mFlippedQ.Z,
                                    mFlippedQ.W
                                ))
                        Next
                    End If


                    ClassPrecisionSleep.Sleep(1)
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    Threading.Thread.Sleep(1000)
                End Try
            End While
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).

                    If (g_mOscThread IsNot Nothing AndAlso g_mOscThread.IsAlive) Then
                        g_mOscThread.Abort()
                        g_mOscThread.Join()
                        g_mOscThread = Nothing
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

    Class ClassConfig
        Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "vmt_devices.ini")

        Private g_mUCRemoteDeviceItem As UCVirtualMotionTrackerItem

        Public Sub New(_UCRemoteDeviceItem As UCVirtualMotionTrackerItem)
            g_mUCRemoteDeviceItem = _UCRemoteDeviceItem
        End Sub

        Public Sub SaveConfig()
            If (CInt(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedItem) < 0) Then
                Return
            End If

            Dim sDevicePath As String = CType(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedItem, String)

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    SyncLock _ThreadLock
                        Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "VMTTrackerID", CStr(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerID.SelectedIndex)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "VMTTrackerRole", CStr(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerRole.SelectedIndex)))

                        mIni.WriteKeyValue(mIniContent.ToArray)
                    End SyncLock
                End Using
            End Using
        End Sub

        Public Sub LoadConfig()
            If (CInt(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedItem) < 0) Then
                Return
            End If

            Dim sDevicePath As String = CType(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedItem, String)

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    SetComboBoxClamp(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerID, CInt(mIni.ReadKeyValue(sDevicePath, "VMTTrackerID", "-1")))
                    SetComboBoxClamp(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerRole, CInt(mIni.ReadKeyValue(sDevicePath, "VMTTrackerRole", "0")))
                End Using
            End Using
        End Sub

        Private Sub SetNumericUpDownClamp(mControl As NumericUpDown, iValue As Single)
            mControl.Value = CDec(Math.Max(mControl.Minimum, Math.Min(mControl.Maximum, iValue)))
        End Sub

        Private Sub SetComboBoxClamp(mControl As ComboBox, iIndex As Integer)
            If (mControl.Items.Count = 0) Then
                Return
            End If

            mControl.SelectedIndex = Math.Max(0, Math.Min(mControl.Items.Count - 1, iIndex))
        End Sub
    End Class

    Private Sub Label_Close_Click(sender As Object, e As EventArgs) Handles Label_Close.Click
        Me.Dispose()
    End Sub
End Class
