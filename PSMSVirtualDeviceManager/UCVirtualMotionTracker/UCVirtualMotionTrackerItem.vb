Imports System.Numerics
Imports System.Text
Imports Rug.Osc
Imports PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants

Public Class UCVirtualMotionTrackerItem
    Const MAX_DRIVER_TIMEOUT As Integer = 5000
    Const MAX_CONTROLLER_TIMEOUT As Integer = 5000

    Const VMT_LIGHTHOUSE_BEGIN_INDEX As Integer = (ClassVmtConst.VMT_TRACKER_MAX + 1)

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
    Public g_bIsHMD As Boolean = False

    Public Sub New(iDeviceID As Integer, bIsHMD As Boolean, _UCVirtualMotionTracker As UCVirtualMotionTracker)
        g_mUCVirtualMotionTracker = _UCVirtualMotionTracker

        g_bIsHMD = bIsHMD

        If (bIsHMD) Then
            If (iDeviceID < 0 OrElse iDeviceID > ClassSerivceConst.PSMOVESERVICE_MAX_HMD_COUNT - 1) Then
                iDeviceID = -1
            End If
        Else
            If (iDeviceID < 0 OrElse iDeviceID > ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1) Then
                iDeviceID = -1
            End If
        End If

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.   

        If (g_bIsHMD) Then
            Label_DeviceID.Text = "HMD ID:"
        Else
            Label_DeviceID.Text = "Controller ID:"
        End If

        Try
            g_bIgnoreEvents = True

            If (bIsHMD) Then
                ComboBox_DeviceID.Items.Clear()
                For i = -1 To ClassSerivceConst.PSMOVESERVICE_MAX_HMD_COUNT - 1
                    ComboBox_DeviceID.Items.Add(CStr(i))
                Next
                ComboBox_DeviceID.SelectedIndex = 0
            Else
                ComboBox_DeviceID.Items.Clear()
                For i = -1 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                    ComboBox_DeviceID.Items.Add(CStr(i))
                Next
                ComboBox_DeviceID.SelectedIndex = 0
            End If

            If (iDeviceID > -1) Then
                ComboBox_DeviceID.SelectedIndex = iDeviceID + 1
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_VMTTrackerID.Items.Clear()

            If (bIsHMD) Then
                ComboBox_VMTTrackerID.Items.Add("-1")
            Else
                ComboBox_VMTTrackerID.Items.Add("-1")

                For i = 0 To ClassVmtConst.VMT_TRACKER_MAX
                    ComboBox_VMTTrackerID.Items.Add(CStr(i))
                Next
            End If
            ComboBox_VMTTrackerID.SelectedIndex = 0

            If (bIsHMD) Then
                ComboBox_VMTTrackerID.Enabled = False
            End If
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

            If (bIsHMD) Then
                ComboBox_VMTTrackerRole.Enabled = False
            End If
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

            If (bIsHMD) Then
                ComboBox_SteamTrackerRole.Enabled = False
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        g_mClassIO = New ClassIO(Me)
        g_mClassConfig = New ClassConfig(Me)

        g_mClassIO.m_Index = CInt(ComboBox_DeviceID.SelectedItem)
        g_mClassIO.m_IsHMD = bIsHMD
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

    Private Sub UpdateTrackerTitle()
        Dim iDeviceID As Integer = CInt(ComboBox_DeviceID.SelectedItem)
        Dim iVmtTrackerID As Integer = CInt(ComboBox_VMTTrackerID.SelectedItem)
        Dim sVmtTrackerRole As String = CStr(ComboBox_VMTTrackerRole.SelectedItem)
        Dim sTrackerRole As String = CStr(ComboBox_SteamTrackerRole.SelectedItem)

        If (g_bIsHMD) Then
            If (iDeviceID < 0) Then
                Label_TrackerName.Text = "HMD Name: Invalid"
            Else
                Label_TrackerName.Text = String.Format("HMD Name: {0}{1}", ClassVmtConst.VMT_DEVICE_NAME, "HMD")
            End If
        Else
            If (iVmtTrackerID < 0 OrElse iDeviceID < 0) Then
                Label_TrackerName.Text = "Tracker Name: Invalid"
            Else
                Label_TrackerName.Text = String.Format("Tracker Name: {0}{1} - {2} ({3})", ClassVmtConst.VMT_DEVICE_NAME, iVmtTrackerID, sVmtTrackerRole, sTrackerRole)
            End If
        End If


    End Sub

    Private Sub OnOscSuspendChanged()
        If (g_mUCVirtualMotionTracker Is Nothing OrElse g_mUCVirtualMotionTracker.g_ClassOscServer Is Nothing) Then
            Return
        End If

        Dim bEnabled As Boolean = (Not g_mUCVirtualMotionTracker.g_ClassOscServer.IsRunning OrElse g_mUCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests)

        ComboBox_DeviceID.Enabled = bEnabled
        ComboBox_VMTTrackerID.Enabled = (bEnabled AndAlso Not g_bIsHMD)
        ComboBox_VMTTrackerRole.Enabled = (bEnabled AndAlso Not g_bIsHMD)
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

                    ClassUtils.AsyncInvoke(Me, Sub()
                                                   g_mDriverLastResponse.Restart()

                                                   g_sDriverVersion = sDriverVersion
                                                   g_sDriverPath = sDriverPath
                                               End Sub)
                Case "/VMT/Out/Haptic"
                    If (mMessage.Count < 4) Then
                        Return
                    End If

                    Dim iIndex As Integer = CInt(mMessage(0))
                    Dim fFreqency As Single = CSng(mMessage(1))
                    Dim fAmplitude As Single = CSng(mMessage(2))
                    Dim fDuration As Single = CSng(mMessage(3))

                    If (g_mClassIO Is Nothing) Then
                        Return
                    End If

                    If (g_mClassIO.m_VmtTracker <> iIndex) Then
                        Return
                    End If

                    g_mClassIO.SetHepticFeedback(fFreqency, fAmplitude, fDuration)

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
                        ClassUtils.AsyncInvoke(Me, Sub()
                                                       g_mDriverLastResponse.Restart()

                                                       g_sDriverLastResponseCode = iCode
                                                       g_sDriverLastResponseMessage = sReason
                                                   End Sub)
                    Else
                        ClassUtils.AsyncInvoke(Me, Sub()
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

    Private Sub ComboBox_ControllerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_DeviceID.SelectedIndexChanged
        UpdateTrackerTitle()

        If (g_bIgnoreEvents) Then
            Return
        End If

        Try
            g_bIgnoreUnsaved = True
            g_mClassConfig.LoadConfig()
        Finally
            g_bIgnoreUnsaved = False
        End Try

        g_mClassIO.m_Index = CInt(ComboBox_DeviceID.SelectedItem)
        g_mClassIO.Enable()

        UpdateTrackerRoleComboBox()

        SetUnsavedState(False)
    End Sub

    Private Sub ComboBox_VMTTrackerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_VMTTrackerID.SelectedIndexChanged
        UpdateTrackerTitle()

        If (g_bIgnoreEvents) Then
            Return
        End If

        If (g_bIsHMD) Then
            Return
        End If

        g_mClassIO.m_VmtTracker = CInt(ComboBox_VMTTrackerID.SelectedItem)
        SetUnsavedState(True)

        UpdateTrackerRoleComboBox()
    End Sub

    Private Sub ComboBox_VMTTrackerRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_VMTTrackerRole.SelectedIndexChanged
        UpdateTrackerTitle()

        If (g_bIgnoreEvents) Then
            Return
        End If

        If (g_bIsHMD) Then
            Return
        End If

        g_mClassIO.m_VmtTrackerRole = CType(ComboBox_VMTTrackerRole.SelectedIndex, ClassIO.ENUM_TRACKER_ROLE)
        SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_TrackerRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_SteamTrackerRole.SelectedIndexChanged
        UpdateTrackerTitle()

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
        Catch ex As Exception
        Finally
            TimerFPS.Start()
        End Try
    End Sub

    Private Sub TimerPose_Tick(sender As Object, e As EventArgs) Handles TimerPose.Tick
        Try
            TimerPose.Stop()

            SyncLock _ThreadLock
                Dim mPosition As New Vector3(0, 0, 0)
                Dim mOrientation As Quaternion = Quaternion.Identity
                Dim bValid As Boolean = False

                Select Case (True)
                    Case (g_mClassIO.m_ControllerData IsNot Nothing)
                        mPosition = g_mClassIO.m_ControllerData.m_Position
                        mOrientation = g_mClassIO.m_ControllerData.m_Orientation
                        bValid = True

                    Case (g_mClassIO.m_HmdData IsNot Nothing)
                        mPosition = g_mClassIO.m_HmdData.m_Position
                        mOrientation = g_mClassIO.m_HmdData.m_Orientation
                        bValid = True

                End Select

                If (bValid) Then
                    If (Me.Visible) Then
                        TextBox_Pos.Text = String.Format("Pos X: {0}{3}Pos Y: {1}{3}Pos Z: {2}", Math.Floor(mPosition.X), Math.Floor(mPosition.Y), Math.Floor(mPosition.Z), Environment.NewLine)

                        Dim iAng = ClassQuaternionTools.FromQ(mOrientation)
                        TextBox_Gyro.Text = String.Format("Ang X: {0}{3}Ang Y: {1}{3}Ang Z: {2}", Math.Floor(iAng.X), Math.Floor(iAng.Y), Math.Floor(iAng.Z), Environment.NewLine)
                    End If
                Else
                    If (Me.Visible) Then
                        TextBox_Pos.Text = String.Format("Pos X: {0}{3}Pos Y: {1}{3}Pos Z: {2}", "N/A", "N/A", "N/A", Environment.NewLine)
                        TextBox_Gyro.Text = String.Format("Ang X: {0}{3}Ang Y: {1}{3}Ang Z: {2}", "N/A", "N/A", "N/A", Environment.NewLine)
                    End If
                End If

            End SyncLock
        Catch ex As Exception
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
                If (g_mUCVirtualMotionTracker.g_ClassOscServer.IsRunning AndAlso Not g_mUCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
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

                    If (Me.Height <> g_iStatusHideHeight) Then
                        Me.Height = g_iStatusHideHeight
                    End If
                End If
            Else
                If (Not Panel_Status.Visible) Then
                    Panel_Status.Visible = True

                    If (Me.Height <> g_iStatusShowHeight) Then
                        Me.Height = g_iStatusShowHeight
                    End If
                End If
            End If
        Catch ex As Exception
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

        Const TOUCHPAD_CLAMP_BUFFER = 2.5F
        Const TOUCHPAD_GYRO_MULTI = 2.5F

        Const ENABLE_TRACKER As Integer = 1
        Const ENABLE_CONTROLLER_L As Integer = 2
        Const ENABLE_CONTROLLER_R As Integer = 3
        Const ENABLE_TRACKINGREFECNCE As Integer = 4
        Const ENABLE_HTC_VIVE_TRACKER As Integer = 5
        Const ENABLE_HTC_VIVE_CONTROLLER_L As Integer = 6
        Const ENABLE_HTC_VIVE_CONTROLLER_R As Integer = 7
        Const ENABLE_HTC_VIVE_LIGHTHOUSE As Integer = 8
        Const ENABLE_HMD As Integer = 9

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
        Private g_bIsHMD As Boolean = False
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
        Private g_mHmdData As ClassServiceClient.IHmdData
        Private g_mTrackerData As New Dictionary(Of Integer, ClassServiceClient.ITrackerData)

        Private g_mOscDataPack As New STRUC_OSC_DATA_PACK()
        Private g_mHeptic As New STRUC_HEPTIC_ITEM
        Private g_mResetRecenter As Boolean = False

        Enum ENUM_PLAYSPACE_CALIBRATION_STATUS
            FAILED = -1
            INACTIVE
            PSMOVE_RUNNING
            MANUAL_RUNNING
            DONE
        End Enum
        Private g_iPlayCalibStatus As ENUM_PLAYSPACE_CALIBRATION_STATUS = ENUM_PLAYSPACE_CALIBRATION_STATUS.INACTIVE
        Private g_bPlayCalibAllowSave As Boolean = False

        Class STRUC_HEPTIC_ITEM
            Const DEFAULT_HAPTIC_FREQUENCY As Single = 200.0F
            Const DEFAULT_HAPTIC_AMPLITUDE As Single = 1.0F
            Const DEFAULT_HAPTIC_DURATION As Single = 0.0F

            Public fFrequency As Single = DEFAULT_HAPTIC_FREQUENCY
            Public fAmplitude As Single = DEFAULT_HAPTIC_AMPLITUDE
            Public fDuration As Single = DEFAULT_HAPTIC_DURATION

            Public Sub Clear()
                fFrequency = DEFAULT_HAPTIC_FREQUENCY
                fAmplitude = DEFAULT_HAPTIC_AMPLITUDE
                fDuration = DEFAULT_HAPTIC_DURATION
            End Sub
        End Class

        Class STRUC_OSC_DATA_PACK
            Const EQUAL_TOLERANCE As Single = 0.0001F

            Public mPosition As New Vector3(0.0F, 0.0F, 0.0F)
            Public mOrientation As New Quaternion(0.0F, 0.0F, 0.0F, 1.0F)

            Public mButtons As New Dictionary(Of Integer, Boolean)
            Public mTrigger As New Dictionary(Of Integer, Single)
            Public mJoyStick As New Vector2(0.0F, 0.0F)

            Public Sub New()
            End Sub

            Public Sub New(_Pack As STRUC_OSC_DATA_PACK)
                mPosition = _Pack.mPosition
                mOrientation = _Pack.mOrientation

                mButtons = New Dictionary(Of Integer, Boolean)(_Pack.mButtons)
                mTrigger = New Dictionary(Of Integer, Single)(_Pack.mTrigger)
                mJoyStick = _Pack.mJoyStick
            End Sub

            Public Function IsPositionEqual(mOther As STRUC_OSC_DATA_PACK) As Boolean
                Return IsPositionEqual(mOther.mPosition)
            End Function

            Public Function IsPositionEqual(vec As Vector3) As Boolean
                If (Math.Abs(mPosition.X - vec.X) < EQUAL_TOLERANCE AndAlso
                        Math.Abs(mPosition.Y - vec.Y) < EQUAL_TOLERANCE AndAlso
                        Math.Abs(mPosition.Z - vec.Z) < EQUAL_TOLERANCE) Then
                    Return True
                End If

                Return False
            End Function

            Public Function IsQuaternionEqual(mOther As STRUC_OSC_DATA_PACK) As Boolean
                Return IsQuaternionEqual(mOther.mOrientation)
            End Function

            Public Function IsQuaternionEqual(quat As Quaternion) As Boolean
                If (Math.Abs(mOrientation.X - quat.X) < EQUAL_TOLERANCE AndAlso
                        Math.Abs(mOrientation.Y - quat.Y) < EQUAL_TOLERANCE AndAlso
                        Math.Abs(mOrientation.Z - quat.Z) < EQUAL_TOLERANCE AndAlso
                        Math.Abs(mOrientation.W - quat.W) < EQUAL_TOLERANCE) Then
                    Return True
                End If

                Return False
            End Function

            Public Function IsInputEqual(mOther As STRUC_OSC_DATA_PACK) As Boolean
                If (mButtons.Keys.Count = mOther.mButtons.Keys.Count) Then
                    Dim iTotalKeys As New List(Of Integer)
                    iTotalKeys.AddRange(mButtons.Keys.ToArray)
                    iTotalKeys.AddRange(mOther.mButtons.Keys.ToArray)

                    For Each i In iTotalKeys
                        Dim v1 As Boolean
                        Dim v2 As Boolean
                        If (Not mButtons.TryGetValue(i, v1) OrElse Not mOther.mButtons.TryGetValue(i, v2)) Then
                            Return False
                        End If

                        If (v1 <> v2) Then
                            Return False
                        End If
                    Next
                Else
                    Return False
                End If

                If (mTrigger.Keys.Count = mOther.mTrigger.Keys.Count) Then
                    Dim iTotalKeys As New List(Of Integer)
                    iTotalKeys.AddRange(mTrigger.Keys.ToArray)
                    iTotalKeys.AddRange(mOther.mTrigger.Keys.ToArray)

                    For Each i In iTotalKeys
                        Dim v1 As Single
                        Dim v2 As Single
                        If (Not mTrigger.TryGetValue(i, v1) OrElse Not mOther.mTrigger.TryGetValue(i, v2)) Then
                            Return False
                        End If

                        If (v1 <> v2) Then
                            Return False
                        End If
                    Next
                Else
                    Return False
                End If

                If (mJoyStick <> mOther.mJoyStick) Then
                    Return False
                End If

                Return True
            End Function

            Public Overrides Function Equals(obj As Object) As Boolean
                Dim mOther = TryCast(obj, STRUC_OSC_DATA_PACK)
                If (mOther Is Nothing) Then
                    Return MyBase.Equals(obj)
                End If

                If (Not IsPositionEqual(mOther)) Then
                    Return False
                End If

                If (Not IsQuaternionEqual(mOther)) Then
                    Return False
                End If

                If (Not IsInputEqual(mOther)) Then
                    Return False
                End If

                Return True
            End Function
        End Class

        Public Sub New(_UCVirtualMotionTrackerItem As UCVirtualMotionTrackerItem)
            g_UCVirtualMotionTrackerItem = _UCVirtualMotionTrackerItem
        End Sub

        Property m_Index As Integer
            Get
                SyncLock _ThreadLock
                    Return g_iIndex
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock _ThreadLock
                    If (g_mOscThread IsNot Nothing AndAlso g_mOscThread.IsAlive) Then
                        Disable()
                        g_iIndex = value
                        Enable()
                    Else
                        g_iIndex = value
                    End If
                End SyncLock
            End Set
        End Property

        Property m_IsHMD As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bIsHMD
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bIsHMD = value
                End SyncLock
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

        Property m_HmdData As ClassServiceClient.IHmdData
            Get
                SyncLock _ThreadLock
                    Return g_mHmdData
                End SyncLock
            End Get
            Set(value As ClassServiceClient.IHmdData)
                SyncLock _ThreadLock
                    g_mHmdData = value
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

        Property m_TrackerData(iIndex As Integer) As ClassServiceClient.ITrackerData
            Get
                SyncLock _ThreadLock
                    Return g_mTrackerData(iIndex)
                End SyncLock
            End Get
            Set(value As ClassServiceClient.ITrackerData)
                SyncLock _ThreadLock
                    g_mTrackerData(iIndex) = value
                End SyncLock
            End Set
        End Property

        Public Sub SetHepticFeedback(fFrequency As Single, fAmplitude As Single, fDuration As Single)
            Dim mClassSettings = g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassSettings
            If (Not mClassSettings.m_MiscSettings.m_EnableHepticFeedback) Then
                Return
            End If

            SyncLock _ThreadLock
                g_mHeptic.fFrequency = fFrequency
                g_mHeptic.fAmplitude = fAmplitude
                g_mHeptic.fDuration = fDuration
            End SyncLock
        End Sub

        Public Sub ResetRecenter()
            SyncLock _ThreadLock
                g_mResetRecenter = True
            End SyncLock
        End Sub

        Public Sub StartManualPlayspaceCalibration()
            SyncLock _ThreadLock
                g_iPlayCalibStatus = ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING
                g_bPlayCalibAllowSave = False
            End SyncLock
        End Sub

        Public Sub StopManualPlayspaceCalibration()
            SyncLock _ThreadLock
                If (g_iPlayCalibStatus <> ENUM_PLAYSPACE_CALIBRATION_STATUS.DONE) Then
                    g_iPlayCalibStatus = ENUM_PLAYSPACE_CALIBRATION_STATUS.FAILED
                End If
                g_bPlayCalibAllowSave = False
            End SyncLock
        End Sub

        Public ReadOnly Property m_ManualPlayspaceCalibrationStatus As ENUM_PLAYSPACE_CALIBRATION_STATUS
            Get
                SyncLock _ThreadLock
                    Return g_iPlayCalibStatus
                End SyncLock
            End Get
        End Property

        Property m_ManualPlayspaceCalibrationAllowSave As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bPlayCalibAllowSave
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bPlayCalibAllowSave = value
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
            Dim iLastOutputSeqNum As Integer = 0

            Dim mOscDataPack As New STRUC_OSC_DATA_PACK()
            Dim mEnforcePacketUpdate As New Stopwatch

            ' Controller
            Dim bJoystickButtonPressed As Boolean = False
            Dim bGripButtonPressed As Boolean = False
            Dim mJoystickButtonPressedTime As New Stopwatch
            Dim mJoystickPressedLastOrientation As New Quaternion
            Dim mJoystickPressedLastPosition As New Vector3
            Dim mJoystickShortcuts As New Dictionary(Of Integer, Vector2)
            Dim bGripToggled As Boolean = False
            Dim mLastBatteryReport As New Stopwatch
            Dim mLastRecenterTime As New Stopwatch
            Dim mLastHmdRecenterTime As New Stopwatch
            Dim mLastPlayspaceRecenterTime As New Stopwatch
            Dim mRecenterButtonPressed As Boolean = False
            Dim mHmdRecenterButtonPressed As Boolean = False
            Dim mPlayspaceRecenterButtonPressed As Boolean = False
            Dim mPlayspaceRecenterButtonHolding As Boolean = False
            Dim mPlayspaceRecenterLastHmdSerial As String = ""
            Dim mPlayspaceRecenterCalibrationRunning As Boolean = False
            Dim mPlayspaceRecenterCalibrationSave As Boolean = False

            Dim iDisplayX As Integer = 0
            Dim iDisplayY As Integer = 0
            Dim iDisplayW As Integer = 0
            Dim iDisplayH As Integer = 0
            Dim iRenderW As Integer = 0
            Dim iRenderH As Integer = 0
            Dim iFrameRate As Integer = 0
            Dim bDisplaySuccess As Boolean = False
            Dim mDisplayNextUpdate As New Stopwatch
            Dim mDisplaySetupUpdate As New Stopwatch

            Dim bFirstEnabled As Boolean = False
            Dim mTrackerDataUpdate As New Stopwatch

            Dim mRumbleLastTimeSend As Date = Now
            Dim mRumbleLastTimeSendValid As Boolean = False

            Dim mRecenterQuat = Quaternion.Identity

            While True
                Dim bExceptionSleep As Boolean = False

                Try
                    If (g_iIndex < 0) Then
                        Return
                    End If

                    Dim mUCVirtualMotionTracker = g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker

                    If (Not mUCVirtualMotionTracker.g_ClassOscServer.IsRunning) Then
                        Throw New ArgumentException("OSC server is not running")
                    End If

                    If (mUCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
                        Throw New ArgumentException("OSC server is suspended")
                    End If

                    If (Not bFirstEnabled) Then
                        bFirstEnabled = True

                        mTrackerDataUpdate.Restart()
                        mLastBatteryReport.Restart()
                        mEnforcePacketUpdate.Restart()
                    End If

                    SyncLock _ThreadLock
                        If (g_mResetRecenter) Then
                            g_mResetRecenter = False

                            mRecenterQuat = Quaternion.Identity
                        End If
                    End SyncLock

                    Dim mClassSettings = g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassSettings

                    ' Get controller settings
                    Dim bJoystickShortcutBinding = mClassSettings.m_ControllerSettings.m_JoystickShortcutBinding
                    Dim bJoystickShortcutTouchpadClick = mClassSettings.m_ControllerSettings.m_JoystickShortcutTouchpadClick
                    Dim iHtcTouchpadEmulationClickMethod = mClassSettings.m_ControllerSettings.m_HtcTouchpadEmulationClickMethod
                    Dim iHtcGripButtonMethod = mClassSettings.m_ControllerSettings.m_HtcGripButtonMethod
                    Dim bClampTouchpadToBounds = mClassSettings.m_ControllerSettings.m_HtcClampTouchpadToBounds
                    Dim iHtcTouchpadMethod = mClassSettings.m_ControllerSettings.m_HtcTouchpadMethod
                    Dim bEnableControllerRecenter = mClassSettings.m_ControllerSettings.m_EnableControllerRecenter
                    Dim iRecenterMethod = mClassSettings.m_ControllerSettings.m_ControllerRecenterMethod
                    Dim sRecenterFromDeviceName = mClassSettings.m_ControllerSettings.m_ControllerRecenterFromDeviceName
                    Dim bEnableHmdRecenter = mClassSettings.m_ControllerSettings.m_EnableHmdRecenter
                    Dim iHmdRecenterMethod = mClassSettings.m_ControllerSettings.m_HmdRecenterMethod
                    Dim sHmdRecenterFromDeviceName = mClassSettings.m_ControllerSettings.m_HmdRecenterFromDeviceName
                    Dim iRecenterButtonTimeMs = mClassSettings.m_ControllerSettings.m_RecenterButtonTimeMs
                    Dim iTouchpadTouchAreaCm = mClassSettings.m_ControllerSettings.m_HtcTouchpadTouchAreaCm
                    Dim iTouchpadClickDeadzone = mClassSettings.m_ControllerSettings.m_HtcTouchpadClickDeadzone
                    Dim bEnabledPlayspaceRecenter = mClassSettings.m_ControllerSettings.m_EnablePlayspaceRecenter

                    ' Get HMD settings
                    Dim bUseCustomDistortion = mClassSettings.m_HmdSettings.m_UseCustomDistortion
                    Dim iHmdDistortK0 = mClassSettings.m_HmdSettings.m_DistortionK0(bUseCustomDistortion)
                    Dim iHmdDistortK1 = mClassSettings.m_HmdSettings.m_DistortionK1(bUseCustomDistortion)
                    Dim iHmdDistortScale = mClassSettings.m_HmdSettings.m_DistortionScale(bUseCustomDistortion)
                    Dim iHmdDistortRedOffset = mClassSettings.m_HmdSettings.m_DistortionRedOffset(bUseCustomDistortion)
                    Dim iHmdDistortGreenOffset = mClassSettings.m_HmdSettings.m_DistortionGreenOffset(bUseCustomDistortion)
                    Dim iHmdDistortBlueOffset = mClassSettings.m_HmdSettings.m_DistortionBlueOffset(bUseCustomDistortion)
                    Dim iHmdHFov = mClassSettings.m_HmdSettings.m_HFov(bUseCustomDistortion)
                    Dim iHmdVFov = mClassSettings.m_HmdSettings.m_VFov(bUseCustomDistortion)
                    Dim iHmdIPD = (mClassSettings.m_HmdSettings.m_IPD / 1000.0F) ' To meters
                    Dim iHmdRenderScale = mClassSettings.m_HmdSettings.m_RenderScale

                    ' Get misc settings
                    Dim bDisableBaseStationSpawning As Boolean = mClassSettings.m_MiscSettings.m_DisableBaseStationSpawning
                    Dim bEnableHepticFeedback As Boolean = mClassSettings.m_MiscSettings.m_EnableHepticFeedback
                    Dim bOptimizeTransportPackets As Boolean = mClassSettings.m_MiscSettings.m_OptimizeTransportPackets


                    Dim bEnfocePacketUpdate As Boolean = False
                    If (mEnforcePacketUpdate.ElapsedMilliseconds > 1000) Then
                        mEnforcePacketUpdate.Restart()

                        bEnfocePacketUpdate = True
                    End If

                    Dim mServiceClient = mUCVirtualMotionTracker.g_mFormMain.g_mPSMoveServiceCAPI


                    If (m_IsHMD) Then
                        ' Get hmd data
                        g_mHmdData = mServiceClient.m_HmdData(m_Index)

                        If (g_mHmdData IsNot Nothing) Then
                            ' We got any new data?
                            If (iLastOutputSeqNum <> g_mHmdData.m_OutputSeqNum) Then
                                iLastOutputSeqNum = g_mHmdData.m_OutputSeqNum

                                SyncLock _ThreadLock
                                    Dim mRawOrientation = g_mHmdData.m_Orientation
                                    Dim mCalibratedOrientation = mRawOrientation

                                    Dim mRawPosition = g_mHmdData.m_Position
                                    Dim mCalibratedPosition = mRawPosition

                                    ' Playspace offsets, used for playspace calibration
                                    If (Not mPlayspaceRecenterCalibrationRunning) Then
                                        InternalApplyPlayspaceCalibrationLogic(mClassSettings.m_PlayspaceSettings, mCalibratedPosition, mCalibratedOrientation)
                                    End If

                                    Dim mOrientation = mRecenterQuat * mCalibratedOrientation
                                    Dim mPosition = mCalibratedPosition

                                    mOscDataPack.mOrientation = mOrientation
                                    mOscDataPack.mPosition = mPosition * CSng(PSM_CENTIMETERS_TO_METERS)

                                    ' $TODO Do something cool?

                                    'Do Hmd/remote recenter
                                    InternalRecenterHmd(bEnableHmdRecenter,
                                                            mServiceClient,
                                                            mHmdRecenterButtonPressed,
                                                            mLastHmdRecenterTime,
                                                            iRecenterButtonTimeMs,
                                                            iHmdRecenterMethod,
                                                            sHmdRecenterFromDeviceName,
                                                            mCalibratedPosition,
                                                            mCalibratedOrientation,
                                                            mRecenterQuat,
                                                            mUCVirtualMotionTracker,
                                                            True)

                                    If (Not mDisplayNextUpdate.IsRunning OrElse mDisplayNextUpdate.ElapsedMilliseconds > 5000) Then
                                        mDisplayNextUpdate.Restart()

                                        Dim mClassMonitor As New ClassMonitor
                                        Dim mDevMode As ClassMonitor.DEVMODE = Nothing
                                        If (mClassMonitor.FindPlaystationVrMonitor(mDevMode, Nothing) = ClassMonitor.ENUM_PSVR_MONITOR_STATUS.SUCCESS) Then
                                            If (Not String.IsNullOrEmpty(mDevMode.dmDeviceName)) Then
                                                iDisplayX = mDevMode.dmPositionX
                                                iDisplayY = mDevMode.dmPositionY
                                                iDisplayW = mDevMode.dmPelsWidth
                                                iDisplayH = mDevMode.dmPelsHeight
                                                iRenderW = CInt((iDisplayW * iHmdRenderScale))
                                                iRenderH = CInt((iDisplayH * iHmdRenderScale))
                                                iFrameRate = mDevMode.dmDisplayFrequency

                                                bDisplaySuccess = True
                                            End If
                                        End If
                                    End If

                                    If (bDisplaySuccess AndAlso iDisplayW > 0 AndAlso iDisplayH > 0) Then
                                        ' Setup the HMD
                                        ' $TODO Make this less retarded. Get status from the driver if something isnt set up properly.
                                        If (Not mDisplaySetupUpdate.IsRunning OrElse mDisplaySetupUpdate.ElapsedMilliseconds > 500) Then
                                            mDisplaySetupUpdate.Restart()

                                            mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                New OscMessage(
                                                    "/VMT/HMD/SetupDisplay",
                                                    iDisplayX, iDisplayY,
                                                    iDisplayW, iDisplayH,
                                                    iRenderW, iRenderH,
                                                    iFrameRate
                                                ))
                                            m_FpsOscCounter += 1

                                            mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                New OscMessage(
                                                    "/VMT/HMD/SetupRender",
                                                    iHmdDistortK0, iHmdDistortK1, iHmdDistortScale,
                                                    -iHmdDistortRedOffset, -iHmdDistortGreenOffset, -iHmdDistortBlueOffset,
                                                    iHmdHFov, iHmdVFov
                                                ))
                                            m_FpsOscCounter += 1

                                            mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                New OscMessage(
                                                    "/VMT/HMD/SetIpdMeters",
                                                    iHmdIPD
                                                ))
                                            m_FpsOscCounter += 1
                                        End If

                                        If (bEnfocePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                Not g_mOscDataPack.IsPositionEqual(mOscDataPack) OrElse Not g_mOscDataPack.IsQuaternionEqual(mOscDataPack)) Then
                                            mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                New OscMessage(
                                                    "/VMT/HMD/Room/Driver",
                                                    0.0F,
                                                    mOscDataPack.mPosition.X,
                                                    mOscDataPack.mPosition.Y,
                                                    mOscDataPack.mPosition.Z,
                                                    mOscDataPack.mOrientation.X,
                                                    mOscDataPack.mOrientation.Y,
                                                    mOscDataPack.mOrientation.Z,
                                                    mOscDataPack.mOrientation.W
                                                ))
                                            m_FpsOscCounter += 1
                                        End If
                                    End If
                                End SyncLock
                            End If
                        End If
                    Else
                        ' Get controller data
                        m_ControllerData = mServiceClient.m_ControllerData(m_Index)

                        If (m_ControllerData IsNot Nothing) Then
                            ' Set controller rumble
                            Select Case (True)
                                Case (TypeOf m_ControllerData Is ClassServiceClient.STRUC_PSMOVE_CONTROLLER_DATA)
                                    InternalHepticFeedbackLogic(bEnableHepticFeedback,
                                                            mRumbleLastTimeSendValid,
                                                            mRumbleLastTimeSend,
                                                            mServiceClient)
                            End Select

                            ' We got any new data?
                            If (iLastOutputSeqNum <> m_ControllerData.m_OutputSeqNum) Then
                                iLastOutputSeqNum = m_ControllerData.m_OutputSeqNum

                                Dim iBatteryValue As Single = m_ControllerData.m_BatteryLevel
                                Dim bIsVirtualCOntroller As Boolean = m_ControllerData.m_Serial.StartsWith("VirtualController")



                                SyncLock _ThreadLock
                                    Dim mRawOrientation = m_ControllerData.m_Orientation
                                    Dim mCalibratedOrientation = mRawOrientation

                                    Dim mRawPosition = m_ControllerData.m_Position
                                    Dim mCalibratedPosition = mRawPosition

                                    ' Playspace offsets, used for playspace calibration
                                    If (Not mPlayspaceRecenterCalibrationRunning) Then
                                        InternalApplyPlayspaceCalibrationLogic(mClassSettings.m_PlayspaceSettings, mCalibratedPosition, mCalibratedOrientation)
                                    End If

                                    Dim mOrientation = mRecenterQuat * mCalibratedOrientation
                                    Dim mPosition = mCalibratedPosition

                                    mOscDataPack.mOrientation = mOrientation
                                    mOscDataPack.mPosition = mPosition * CSng(PSM_CENTIMETERS_TO_METERS)

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

                                            'Do playspace recenter
                                            InternalPlayspaceRecenterLogic(bEnabledPlayspaceRecenter,
                                                                            m_PSMoveData.m_SelectButton AndAlso m_PSMoveData.m_StartButton,
                                                                            mRawPosition,
                                                                            mPlayspaceRecenterButtonPressed,
                                                                            mPlayspaceRecenterButtonHolding,
                                                                            mLastPlayspaceRecenterTime,
                                                                            iRecenterButtonTimeMs,
                                                                            mPlayspaceRecenterCalibrationRunning,
                                                                            mPlayspaceRecenterLastHmdSerial,
                                                                            mPlayspaceRecenterCalibrationSave,
                                                                            mClassSettings,
                                                                            mUCVirtualMotionTracker)

                                            'Do controller recenter
                                            InternalRecenterControllerLogic(bEnableControllerRecenter,
                                                                            m_PSMoveData.m_SelectButton AndAlso Not m_PSMoveData.m_StartButton,
                                                                            mRecenterButtonPressed,
                                                                            mLastRecenterTime,
                                                                            iRecenterButtonTimeMs,
                                                                            iRecenterMethod,
                                                                            sRecenterFromDeviceName,
                                                                            mRecenterQuat,
                                                                            mCalibratedPosition,
                                                                            mCalibratedOrientation,
                                                                            mUCVirtualMotionTracker)

                                            'Do Hmd/remote recenter
                                            InternalRecenterHmd(bEnableHmdRecenter,
                                                                mServiceClient,
                                                                mHmdRecenterButtonPressed,
                                                                mLastHmdRecenterTime,
                                                                iRecenterButtonTimeMs,
                                                                iHmdRecenterMethod,
                                                                sHmdRecenterFromDeviceName,
                                                                mCalibratedPosition,
                                                                mCalibratedOrientation,
                                                                mRecenterQuat,
                                                                mUCVirtualMotionTracker,
                                                                False)

                                            'Send buttons
                                            InternalButtonsLogic(mOscDataPack,
                                                                mButtons,
                                                                m_PSMoveData,
                                                                bJoystickTrigger,
                                                                iHtcGripButtonMethod,
                                                                bGripButtonPressed,
                                                                bGripToggled,
                                                                iHtcTouchpadEmulationClickMethod)

                                            'Joystick emulation
                                            InternalJoystickEmulationLogic(mOscDataPack,
                                                                    bJoystickTrigger,
                                                                    iHtcTouchpadMethod,
                                                                    bJoystickButtonPressed,
                                                                    mJoystickButtonPressedTime,
                                                                    mJoystickPressedLastOrientation,
                                                                    mRecenterQuat,
                                                                    m_PSMoveData,
                                                                    mJoystickPressedLastPosition,
                                                                    bClampTouchpadToBounds,
                                                                    iTouchpadTouchAreaCm,
                                                                    iTouchpadClickDeadzone,
                                                                    iHtcTouchpadEmulationClickMethod,
                                                                    bJoystickShortcutBinding,
                                                                    mButtons,
                                                                    mJoystickShortcuts,
                                                                    bJoystickShortcutTouchpadClick)
                                    End Select

                                    'Send battery level to
                                    If (mLastBatteryReport.Elapsed > New TimeSpan(0, 0, 1)) Then
                                        mLastBatteryReport.Restart()

                                        mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                            New OscMessage(
                                                "/VMT/Property/Battery",
                                                m_VmtTracker,
                                                iBatteryValue
                                            ))
                                        m_FpsOscCounter += 1
                                    End If

                                    Select Case (m_VmtTrackerRole)
                                        Case ENUM_TRACKER_ROLE.GENERIC_TRACKER

                                            If (bEnfocePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                    Not g_mOscDataPack.IsPositionEqual(mOscDataPack) OrElse Not g_mOscDataPack.IsQuaternionEqual(mOscDataPack)) Then
                                                mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                    New OscMessage(
                                                        "/VMT/Room/Driver",
                                                        m_VmtTracker, ENABLE_TRACKER, 0.0F,
                                                        mOscDataPack.mPosition.X,
                                                        mOscDataPack.mPosition.Y,
                                                        mOscDataPack.mPosition.Z,
                                                        mOscDataPack.mOrientation.X,
                                                        mOscDataPack.mOrientation.Y,
                                                        mOscDataPack.mOrientation.Z,
                                                        mOscDataPack.mOrientation.W
                                                    ))
                                                m_FpsOscCounter += 1
                                            End If

                                            g_mOscDataPack = New STRUC_OSC_DATA_PACK(mOscDataPack)

                                        Case ENUM_TRACKER_ROLE.GENERIC_LEFT_CONTROLLER, ENUM_TRACKER_ROLE.GENERIC_RIGHT_CONTROLLER

                                            Dim iController As Integer = ENABLE_TRACKER
                                            Select Case (m_VmtTrackerRole)
                                                Case ENUM_TRACKER_ROLE.GENERIC_LEFT_CONTROLLER
                                                    iController = ENABLE_CONTROLLER_L

                                                Case ENUM_TRACKER_ROLE.GENERIC_RIGHT_CONTROLLER
                                                    iController = ENABLE_CONTROLLER_R
                                            End Select

                                            If (bEnfocePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                    Not g_mOscDataPack.IsInputEqual(mOscDataPack)) Then
                                                For Each mButton In mOscDataPack.mButtons
                                                    mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                        New OscMessage(
                                                            "/VMT/Input/Button",
                                                            m_VmtTracker, mButton.Key, 0.0F, CInt(mButton.Value)
                                                        ))
                                                    m_FpsOscCounter += 1
                                                Next

                                                For Each mTrigger In mOscDataPack.mTrigger
                                                    mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                       New OscMessage(
                                                           "/VMT/Input/Trigger",
                                                           m_VmtTracker, mTrigger.Key, 0.0F, mTrigger.Value
                                                       ))
                                                    m_FpsOscCounter += 1
                                                Next

                                                mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                    New OscMessage(
                                                        "/VMT/Input/Joystick",
                                                        m_VmtTracker, 0, 0.0F, mOscDataPack.mJoyStick.X, mOscDataPack.mJoyStick.Y
                                                    ))
                                                m_FpsOscCounter += 1

                                            End If

                                            If (bEnfocePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                    Not g_mOscDataPack.IsPositionEqual(mOscDataPack) OrElse Not g_mOscDataPack.IsQuaternionEqual(mOscDataPack)) Then
                                                mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                    New OscMessage(
                                                        "/VMT/Room/Driver",
                                                        m_VmtTracker, iController, 0.0F,
                                                        mOscDataPack.mPosition.X,
                                                        mOscDataPack.mPosition.Y,
                                                        mOscDataPack.mPosition.Z,
                                                        mOscDataPack.mOrientation.X,
                                                        mOscDataPack.mOrientation.Y,
                                                        mOscDataPack.mOrientation.Z,
                                                        mOscDataPack.mOrientation.W
                                                    ))
                                                m_FpsOscCounter += 1
                                            End If

                                            g_mOscDataPack = New STRUC_OSC_DATA_PACK(mOscDataPack)

                                        Case ENUM_TRACKER_ROLE.HTC_VIVE_TRACKER

                                            If (bEnfocePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                    Not g_mOscDataPack.IsPositionEqual(mOscDataPack) OrElse Not g_mOscDataPack.IsQuaternionEqual(mOscDataPack)) Then
                                                mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                    New OscMessage(
                                                        "/VMT/Room/Driver",
                                                        m_VmtTracker, ENABLE_HTC_VIVE_TRACKER, 0.0F,
                                                        mOscDataPack.mPosition.X,
                                                        mOscDataPack.mPosition.Y,
                                                        mOscDataPack.mPosition.Z,
                                                        mOscDataPack.mOrientation.X,
                                                        mOscDataPack.mOrientation.Y,
                                                        mOscDataPack.mOrientation.Z,
                                                        mOscDataPack.mOrientation.W
                                                    ))
                                                m_FpsOscCounter += 1
                                            End If

                                            g_mOscDataPack = New STRUC_OSC_DATA_PACK(mOscDataPack)

                                        Case ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER, ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER

                                            Dim iController As Integer = ENABLE_HTC_VIVE_TRACKER
                                            Select Case (m_VmtTrackerRole)
                                                Case ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER
                                                    iController = ENABLE_HTC_VIVE_CONTROLLER_L

                                                Case ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER
                                                    iController = ENABLE_HTC_VIVE_CONTROLLER_R
                                            End Select

                                            If (bEnfocePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                    Not g_mOscDataPack.IsInputEqual(mOscDataPack)) Then
                                                For Each mButton In mOscDataPack.mButtons
                                                    mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                        New OscMessage(
                                                            "/VMT/Input/Button",
                                                            m_VmtTracker, mButton.Key, 0.0F, CInt(mButton.Value)
                                                        ))
                                                    m_FpsOscCounter += 1
                                                Next

                                                For Each mTrigger In mOscDataPack.mTrigger
                                                    mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                       New OscMessage(
                                                           "/VMT/Input/Trigger",
                                                           m_VmtTracker, mTrigger.Key, 0.0F, mTrigger.Value
                                                       ))
                                                    m_FpsOscCounter += 1
                                                Next

                                                mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                    New OscMessage(
                                                        "/VMT/Input/Joystick",
                                                        m_VmtTracker, 0, 0.0F, mOscDataPack.mJoyStick.X, mOscDataPack.mJoyStick.Y
                                                    ))
                                                m_FpsOscCounter += 1
                                            End If

                                            If (bEnfocePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                    Not g_mOscDataPack.IsPositionEqual(mOscDataPack) OrElse Not g_mOscDataPack.IsQuaternionEqual(mOscDataPack)) Then
                                                mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                                    New OscMessage(
                                                        "/VMT/Room/Driver",
                                                        m_VmtTracker, iController, 0.0F,
                                                        mOscDataPack.mPosition.X,
                                                        mOscDataPack.mPosition.Y,
                                                        mOscDataPack.mPosition.Z,
                                                        mOscDataPack.mOrientation.X,
                                                        mOscDataPack.mOrientation.Y,
                                                        mOscDataPack.mOrientation.Z,
                                                        mOscDataPack.mOrientation.W
                                                    ))
                                                m_FpsOscCounter += 1
                                            End If

                                            g_mOscDataPack = New STRUC_OSC_DATA_PACK(mOscDataPack)
                                    End Select
                                End SyncLock
                            End If
                        End If
                    End If

                    If (Not bDisableBaseStationSpawning) Then
                        ' Update tracker references
                        ' $TODO Add them into their own thread?
                        If (mTrackerDataUpdate.Elapsed > New TimeSpan(0, 0, 10)) Then
                            mTrackerDataUpdate.Restart()

                            For i = 0 To PSMOVESERVICE_MAX_TRACKER_COUNT - 1
                                m_TrackerData(i) = mServiceClient.m_TrackerData(i)

                                If (m_TrackerData(i) IsNot Nothing) Then
                                    Dim mRawOrientation = m_TrackerData(i).m_Orientation
                                    Dim mCalibratedOrientation = mRawOrientation

                                    Dim mRawPosition = m_TrackerData(i).m_Position
                                    Dim mCalibratedPosition = mRawPosition

                                    ' Playspace offsets, used for playspace calibration
                                    InternalApplyPlayspaceCalibrationLogic(mClassSettings.m_PlayspaceSettings, mCalibratedPosition, mCalibratedOrientation)

                                    Dim mOrientation As Quaternion = mCalibratedOrientation
                                    Dim mPosition As Vector3 = mCalibratedPosition * CSng(PSM_CENTIMETERS_TO_METERS)

                                    ' Cameras are flipped, flip them correctly
                                    Dim mFlippedQ As Quaternion = mOrientation * Quaternion.CreateFromAxisAngle(Vector3.UnitY, 180.0F * (Math.PI / 180.0F))

                                    'Use Right-Handed space for SteamVR 
                                    mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                        New OscMessage(
                                            "/VMT/Room/Driver",
                                            VMT_LIGHTHOUSE_BEGIN_INDEX + i, ENABLE_HTC_VIVE_LIGHTHOUSE, 0.0F,
                                            mPosition.X,
                                            mPosition.Y,
                                            mPosition.Z,
                                            mFlippedQ.X,
                                            mFlippedQ.Y,
                                            mFlippedQ.Z,
                                            mFlippedQ.W
                                        ))
                                End If
                            Next
                        End If
                    End If


                    ClassPrecisionSleep.Sleep(CInt(mUCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_OscThreadSleepMs))
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    bExceptionSleep = True
                End Try

                ' Thread.Abort will not trigger inside a Try/Catch
                If (bExceptionSleep) Then
                    bExceptionSleep = False
                    Threading.Thread.Sleep(1000)
                End If
            End While
        End Sub

        Private Sub InternalHepticFeedbackLogic(ByRef bEnableHepticFeedback As Boolean,
                                                ByRef mRumbleLastTimeSendValid As Boolean,
                                                ByRef mRumbleLastTimeSend As Date,
                                                ByRef mServiceClient As ClassServiceClient)
            If (bEnableHepticFeedback) Then
                Const MAX_RUMBLE_UPODATE_RATE As Single = 33.0F
                Const MAX_PULSE_MICROSECONDS As Single = 5000.0F

                Dim fHepticDuraction As Single
                Dim fHelpticAmplitude As Single

                SyncLock _ThreadLock
                    fHepticDuraction = g_mHeptic.fDuration
                    fHelpticAmplitude = g_mHeptic.fAmplitude
                End SyncLock

                Dim fHapticPulseDurationMicroSec As Single = (fHepticDuraction * 1000000.0F)

                Dim bTimoutElapsed As Boolean = True

                If (mRumbleLastTimeSendValid) Then
                    Dim mLastSend As TimeSpan = (Now - mRumbleLastTimeSend)

                    bTimoutElapsed = (mLastSend.TotalMilliseconds > MAX_RUMBLE_UPODATE_RATE)
                End If

                If (bTimoutElapsed) Then
                    Dim fRumble As Single = (fHapticPulseDurationMicroSec / MAX_PULSE_MICROSECONDS) * fHelpticAmplitude

                    If (fHepticDuraction > 0.0F) Then
                        If (fRumble < 0.35F) Then
                            fRumble = 0.35F
                        End If
                    End If

                    If (fRumble < 0.0F) Then
                        fRumble = 0.0F
                    End If

                    If (fRumble > 1.0F) Then
                        fRumble = 1.0F
                    End If

                    mServiceClient.SetControllerRumble(g_iIndex, fRumble)

                    mRumbleLastTimeSend = Now
                    mRumbleLastTimeSendValid = True

                    SyncLock _ThreadLock
                        g_mHeptic.Clear()
                    End SyncLock
                End If
            Else
                SyncLock _ThreadLock
                    g_mHeptic.Clear()
                End SyncLock
            End If
        End Sub

        Private Sub InternalApplyPlayspaceCalibrationLogic(ByRef m_PlayspaceSettings As UCVirtualMotionTracker.ClassSettings.STRUC_PLAYSPACE_SETTINGS,
                                                           ByRef mPosition As Vector3,
                                                           ByRef mOrientation As Quaternion)
            If (m_PlayspaceSettings.m_Valid) Then
                Dim mCalibrationForward As Quaternion
                Dim mForward As Vector3
                If (m_PlayspaceSettings.m_ForwardMethod = UCVirtualMotionTracker.ClassSettings.STRUC_PLAYSPACE_SETTINGS.ENUM_FORWARD_METHOD.USE_HMD_FORWARD) Then
                    mCalibrationForward = ClassQuaternionTools.ExtractYawQuaternion(m_PlayspaceSettings.m_HmdAngOffset, -Vector3.UnitZ)
                    mForward = Vector3.UnitZ * m_PlayspaceSettings.m_ForwardOffset
                Else
                    mCalibrationForward = ClassQuaternionTools.LookRotation(
                        m_PlayspaceSettings.m_PointHmdEndPos - m_PlayspaceSettings.m_PointHmdBeginPos, Vector3.UnitY)
                    mForward = Vector3.UnitY * m_PlayspaceSettings.m_ForwardOffset
                End If

                Dim mOffsetForward = ClassQuaternionTools.RotateVector(mCalibrationForward, mForward)

                Dim mPlayspaceCalibPointsRotated = ClassQuaternionTools.RotateVector(
                    Quaternion.Conjugate(m_PlayspaceSettings.m_AngOffset), m_PlayspaceSettings.m_PointControllerBeginPos)

                mPlayspaceCalibPointsRotated = (m_PlayspaceSettings.m_PointHmdBeginPos + mOffsetForward) - mPlayspaceCalibPointsRotated

                Dim mPlayspaceRotated = ClassQuaternionTools.RotateVector(
                    Quaternion.Conjugate(m_PlayspaceSettings.m_AngOffset), mPosition)

                Dim mHeightOffset = New Vector3(0.0F, m_PlayspaceSettings.m_HeightOffset, 0.0F)

                mPosition = mPlayspaceRotated + mPlayspaceCalibPointsRotated + mHeightOffset
                mOrientation = Quaternion.Conjugate(m_PlayspaceSettings.m_AngOffset) * mOrientation
            End If
        End Sub

        Private Sub InternalPlayspaceRecenterLogic(ByRef bEnabledPlayspaceRecenter As Boolean,
                                                  ByRef bHoldingRecenterButtons As Boolean,
                                                  ByRef mRawPosition As Vector3,
                                                  ByRef mPlayspaceRecenterButtonPressed As Boolean,
                                                  ByRef mPlayspaceRecenterButtonHolding As Boolean,
                                                  ByRef mLastPlayspaceRecenterTime As Stopwatch,
                                                  ByRef iRecenterButtonTimeMs As Long,
                                                  ByRef mPlayspaceRecenterCalibrationRunning As Boolean,
                                                  ByRef mPlayspaceRecenterLastHmdSerial As String,
                                                  ByRef mPlayspaceRecenterCalibrationSave As Boolean,
                                                  ByRef mClassControllerSettings As UCVirtualMotionTracker.ClassSettings,
                                                  ByRef mUCVirtualMotionTracker As UCVirtualMotionTracker)
            If ((bEnabledPlayspaceRecenter AndAlso bHoldingRecenterButtons) OrElse g_iPlayCalibStatus = ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING) Then
                If (Not mPlayspaceRecenterButtonPressed) Then
                    mPlayspaceRecenterButtonPressed = True
                    mPlayspaceRecenterButtonHolding = False

                    mLastPlayspaceRecenterTime.Restart()
                End If

                If (mLastPlayspaceRecenterTime.ElapsedMilliseconds > iRecenterButtonTimeMs OrElse g_iPlayCalibStatus = ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING) Then
                    If (Not mPlayspaceRecenterButtonHolding) Then
                        mPlayspaceRecenterButtonHolding = True

                        If (g_iPlayCalibStatus <> ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING) Then
                            g_iPlayCalibStatus = ENUM_PLAYSPACE_CALIBRATION_STATUS.PSMOVE_RUNNING
                        End If
                        mPlayspaceRecenterCalibrationRunning = True

                        mClassControllerSettings.m_PlayspaceSettings.Reset()
                        mPlayspaceRecenterLastHmdSerial = ""

                        Dim sCurrentRecenterDeviceName As String = ""

                        For Each mDevice In mUCVirtualMotionTracker.g_ClassOscDevices.GetDevices()
                            If (mDevice.iType = UCVirtualMotionTracker.ClassOscDevices.STRUC_DEVICE.ENUM_DEVICE_TYPE.HMD) Then
                                sCurrentRecenterDeviceName = mDevice.sSerial
                            End If
                        Next

                        Dim mFoundDevice As UCVirtualMotionTracker.ClassOscDevices.STRUC_DEVICE = Nothing
                        If (mUCVirtualMotionTracker.g_ClassOscDevices.GetDeviceBySerial(sCurrentRecenterDeviceName, mFoundDevice)) Then
                            mClassControllerSettings.m_PlayspaceSettings.m_PointControllerBeginPos = mRawPosition ' Dont use calibrated position. It can cause bad offsets.
                            mClassControllerSettings.m_PlayspaceSettings.m_PointHmdBeginPos = mFoundDevice.GetPosCm()
                            mPlayspaceRecenterLastHmdSerial = sCurrentRecenterDeviceName
                        End If
                    End If

                    If (Not String.IsNullOrEmpty(mPlayspaceRecenterLastHmdSerial)) Then
                        Dim mFoundDevice As UCVirtualMotionTracker.ClassOscDevices.STRUC_DEVICE = Nothing
                        If (mUCVirtualMotionTracker.g_ClassOscDevices.GetDeviceBySerial(mPlayspaceRecenterLastHmdSerial, mFoundDevice)) Then
                            Dim iMinDistance As Single = 10.0F

                            Dim mControllerPosBegin As Vector3 = mClassControllerSettings.m_PlayspaceSettings.m_PointControllerBeginPos
                            Dim mControllerPosEnd As Vector3 = mRawPosition ' Dont use calibrated position. It can cause bad offsets.
                            Dim mFromDevicePosBegin As Vector3 = mClassControllerSettings.m_PlayspaceSettings.m_PointHmdBeginPos
                            Dim mFromDevicePosEnd As Vector3 = mFoundDevice.GetPosCm()
                            Dim mFromDeviceOrientation As Quaternion = mFoundDevice.mOrientation

                            Dim bAllowSave As Boolean = True

                            If (g_iPlayCalibStatus = ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING) Then
                                bAllowSave = g_bPlayCalibAllowSave
                            End If

                            If (bAllowSave AndAlso
                                    Math.Abs(Vector3.Distance(mControllerPosBegin, mControllerPosEnd)) > iMinDistance AndAlso
                                    Math.Abs(Vector3.Distance(mFromDevicePosBegin, mFromDevicePosEnd)) > iMinDistance) Then
                                mControllerPosBegin.Y = 0.0F
                                mControllerPosEnd.Y = 0.0F
                                mFromDevicePosBegin.Y = 0.0F
                                mFromDevicePosEnd.Y = 0.0F

                                Dim mRelControllerVec = ClassQuaternionTools.LookRotation(mControllerPosEnd - mControllerPosBegin, Vector3.UnitY)
                                Dim mRelDeviceVec = ClassQuaternionTools.LookRotation(mFromDevicePosEnd - mFromDevicePosBegin, Vector3.UnitY)
                                Dim mVecDiff = Quaternion.Conjugate(mRelDeviceVec) * mRelControllerVec

                                mClassControllerSettings.m_PlayspaceSettings.m_PosOffset = mFromDevicePosEnd - mControllerPosEnd
                                mClassControllerSettings.m_PlayspaceSettings.m_AngOffset = mVecDiff
                                mClassControllerSettings.m_PlayspaceSettings.m_HmdAngOffset = mFromDeviceOrientation

                                mClassControllerSettings.m_PlayspaceSettings.m_PointControllerEndPos = mControllerPosEnd
                                mClassControllerSettings.m_PlayspaceSettings.m_PointHmdEndPos = mFromDevicePosEnd
                                mClassControllerSettings.m_PlayspaceSettings.m_Valid = True

                                'Save settings after calibration is done 
                                mPlayspaceRecenterCalibrationSave = True

                                g_bPlayCalibAllowSave = False
                                g_iPlayCalibStatus = ENUM_PLAYSPACE_CALIBRATION_STATUS.DONE

                                'Dont triggers this again, we are done here.
                                mLastPlayspaceRecenterTime.Reset()
                            End If
                        Else
                            g_iPlayCalibStatus = ENUM_PLAYSPACE_CALIBRATION_STATUS.FAILED
                        End If
                    Else
                        g_iPlayCalibStatus = ENUM_PLAYSPACE_CALIBRATION_STATUS.FAILED
                    End If
                End If
            Else
                If (mPlayspaceRecenterButtonPressed) Then
                    mPlayspaceRecenterButtonPressed = False
                End If
                If (mPlayspaceRecenterButtonHolding) Then
                    mPlayspaceRecenterButtonHolding = False
                End If
                If (g_iPlayCalibStatus = ENUM_PLAYSPACE_CALIBRATION_STATUS.PSMOVE_RUNNING) Then
                    g_iPlayCalibStatus = ENUM_PLAYSPACE_CALIBRATION_STATUS.INACTIVE
                End If
                If (mPlayspaceRecenterCalibrationRunning) Then
                    mPlayspaceRecenterCalibrationRunning = False
                End If
                If (mPlayspaceRecenterCalibrationSave) Then
                    mPlayspaceRecenterCalibrationSave = False

                    'Save settings after calibration is done
                    mClassControllerSettings.SaveSettings(UCVirtualMotionTracker.ENUM_SETTINGS_SAVE_TYPE_FLAGS.PLAYSPACE_CALIBRATION)
                End If
            End If
        End Sub

        Private Sub InternalRecenterControllerLogic(ByRef bEnableControllerRecenter As Boolean,
                                                   ByRef bHoldingRecenterButtons As Boolean,
                                                   ByRef mRecenterButtonPressed As Boolean,
                                                   ByRef mLastRecenterTime As Stopwatch,
                                                   ByRef iRecenterButtonTimeMs As Long,
                                                   ByRef iRecenterMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD,
                                                   ByRef sRecenterFromDeviceName As String,
                                                   ByRef mRecenterQuat As Quaternion,
                                                   ByRef mCalibratedPosition As Vector3,
                                                   ByRef mCalibratedOrientation As Quaternion,
                                                   ByRef mUCVirtualMotionTracker As UCVirtualMotionTracker)
            If (bEnableControllerRecenter AndAlso bHoldingRecenterButtons) Then
                If (Not mRecenterButtonPressed) Then
                    mRecenterButtonPressed = True

                    mLastRecenterTime.Restart()
                End If

                If (mLastRecenterTime.ElapsedMilliseconds > iRecenterButtonTimeMs) Then
                    mLastRecenterTime.Stop()
                    mLastRecenterTime.Reset()

                    Dim bDoFactoryRecenter As Boolean = True

                    Select Case (iRecenterMethod)
                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD.USE_DEVICE
                            Dim sCurrentRecenterDeviceName As String = sRecenterFromDeviceName

                            ' If empty, do a autoamtic search and get any available HMD
                            If (String.IsNullOrEmpty(sCurrentRecenterDeviceName) OrElse sCurrentRecenterDeviceName.TrimEnd.Length = 0) Then
                                For Each mDevice In mUCVirtualMotionTracker.g_ClassOscDevices.GetDevices()
                                    If (mDevice.iType = UCVirtualMotionTracker.ClassOscDevices.STRUC_DEVICE.ENUM_DEVICE_TYPE.HMD) Then
                                        sCurrentRecenterDeviceName = mDevice.sSerial
                                    End If
                                Next
                            End If

                            Dim mFoundDevice As UCVirtualMotionTracker.ClassOscDevices.STRUC_DEVICE = Nothing
                            If (mUCVirtualMotionTracker.g_ClassOscDevices.GetDeviceBySerial(sCurrentRecenterDeviceName, mFoundDevice)) Then
                                Dim mControllerPos As Vector3 = mCalibratedPosition
                                Dim mFromDevicePos As Vector3 = mFoundDevice.GetPosCm()

                                mControllerPos.Y = 0.0F
                                mFromDevicePos.Y = 0.0F

                                Dim mQuatDirection = ClassQuaternionTools.FromVectorToVector(mFromDevicePos, mControllerPos)
                                Dim mControllerYaw = ClassQuaternionTools.ExtractYawQuaternion(mCalibratedOrientation, New Vector3(0F, 0.0F, -1.0F))

                                mRecenterQuat = mQuatDirection * Quaternion.Conjugate(mControllerYaw)

                                bDoFactoryRecenter = False
                            End If
                    End Select

                    If (bDoFactoryRecenter) Then
                        Dim mControllerYaw = ClassQuaternionTools.ExtractYawQuaternion(mCalibratedOrientation, New Vector3(0F, 0.0F, -1.0F))

                        mRecenterQuat = Quaternion.Conjugate(mControllerYaw) * ClassQuaternionTools.LookRotation(Vector3.UnitX, Vector3.UnitY)
                    End If
                End If
            Else
                If (mRecenterButtonPressed) Then
                    mRecenterButtonPressed = False
                End If
            End If
        End Sub

        Private Sub InternalRecenterHmd(ByRef bEnableHmdRecenter As Boolean,
                                        ByRef mServiceClient As ClassServiceClient,
                                        ByRef mHmdRecenterButtonPressed As Boolean,
                                        ByRef mLastHmdRecenterTime As Stopwatch,
                                        ByRef iRecenterButtonTimeMs As Long,
                                        ByRef iHmdRecenterMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD,
                                        ByRef sHmdRecenterFromDeviceName As String,
                                        ByRef mCalibratedPosition As Vector3,
                                        ByRef mCalibratedOrientation As Quaternion,
                                        ByRef mRecenterQuat As Quaternion,
                                        ByRef mUCVirtualMotionTracker As UCVirtualMotionTracker,
                                        ByRef bIsHmd As Boolean)
            Dim bOtherControllerRecenterButtonPressed As Boolean = False
            Dim bOtherControllerPos As New Vector3

            If (bEnableHmdRecenter) Then
                For Each mControllerDataSearch In mServiceClient.GetControllersData()
                    If (Not bIsHmd) Then
                        If (mControllerDataSearch.m_Id = m_ControllerData.m_Id) Then
                            Continue For
                        End If
                    End If

                    Select Case (True)
                        Case (TypeOf mControllerDataSearch Is ClassServiceClient.STRUC_PSMOVE_CONTROLLER_DATA)
                            Dim m_PSMoveDataSearch = DirectCast(mControllerDataSearch, ClassServiceClient.STRUC_PSMOVE_CONTROLLER_DATA)

                            If (Not m_PSMoveDataSearch.m_IsTracking) Then
                                Continue For
                            End If

                            ' Only recenter when only select is pressed
                            If (m_PSMoveDataSearch.m_StartButton AndAlso Not m_PSMoveDataSearch.m_SelectButton) Then
                                bOtherControllerPos = m_PSMoveDataSearch.m_Position
                                bOtherControllerRecenterButtonPressed = True

                                Exit For
                            End If

                    End Select
                Next
            End If

            If (bEnableHmdRecenter AndAlso bOtherControllerRecenterButtonPressed) Then
                If (Not mHmdRecenterButtonPressed) Then
                    mHmdRecenterButtonPressed = True

                    mLastHmdRecenterTime.Restart()
                End If

                If (mLastHmdRecenterTime.ElapsedMilliseconds > iRecenterButtonTimeMs) Then
                    mLastHmdRecenterTime.Stop()
                    mLastHmdRecenterTime.Reset()

                    Dim bDoFactoryRecenter As Boolean = True

                    Select Case (iHmdRecenterMethod)
                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD.USE_DEVICE
                            Dim sCurrentRecenterDeviceName As String = sHmdRecenterFromDeviceName

                            ' If empty, do a autoamtic search and get any available HMD
                            If (String.IsNullOrEmpty(sCurrentRecenterDeviceName) OrElse sCurrentRecenterDeviceName.TrimEnd.Length = 0) Then
                                For Each mDevice In mUCVirtualMotionTracker.g_ClassOscDevices.GetDevices()
                                    If (mDevice.iType = UCVirtualMotionTracker.ClassOscDevices.STRUC_DEVICE.ENUM_DEVICE_TYPE.HMD) Then
                                        sCurrentRecenterDeviceName = mDevice.sSerial

                                        Exit For
                                    End If
                                Next
                            End If

                            ' Check if we are the target device.
                            Dim bIsTarget As Boolean = False

                            If (bIsHmd) Then
                                bIsTarget = ((ClassVmtConst.VMT_DEVICE_NAME & "HMD") = sCurrentRecenterDeviceName)
                            Else
                                bIsTarget = ((ClassVmtConst.VMT_DEVICE_NAME & m_VmtTracker) = sCurrentRecenterDeviceName)
                            End If

                            If (bIsTarget) Then
                                Dim mControllerPos As Vector3 = bOtherControllerPos
                                Dim mFromDevicePos As Vector3 = mCalibratedPosition

                                mControllerPos.Y = 0.0F
                                mFromDevicePos.Y = 0.0F

                                Dim mQuatDirection = ClassQuaternionTools.FromVectorToVector(mFromDevicePos, mControllerPos)
                                Dim mControllerYaw = ClassQuaternionTools.ExtractYawQuaternion(mCalibratedOrientation, New Vector3(0F, 0.0F, -1.0F))

                                mRecenterQuat = Quaternion.Conjugate(mControllerYaw) * mQuatDirection

                                bDoFactoryRecenter = False
                            Else
                                bDoFactoryRecenter = False
                            End If
                    End Select

                    If (bDoFactoryRecenter) Then
                        Dim mControllerYaw = ClassQuaternionTools.ExtractYawQuaternion(mCalibratedOrientation, New Vector3(0F, 0.0F, -1.0F))

                        mRecenterQuat = Quaternion.Conjugate(mControllerYaw) * ClassQuaternionTools.LookRotation(Vector3.UnitX, Vector3.UnitY)
                    End If
                End If
            Else
                If (mHmdRecenterButtonPressed) Then
                    mHmdRecenterButtonPressed = False
                End If
            End If
        End Sub

        Private Sub InternalJoystickEmulationLogic(ByRef mOscDataPack As STRUC_OSC_DATA_PACK,
                                                   ByRef bJoystickTrigger As Boolean,
                                                   ByRef iHtcTouchpadMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_METHOD,
                                                   ByRef bJoystickButtonPressed As Boolean,
                                                   ByRef mJoystickButtonPressedTime As Stopwatch,
                                                   ByRef mJoystickPressedLastOrientation As Quaternion,
                                                   ByRef mRecenterQuat As Quaternion,
                                                   ByRef m_PSMoveData As ClassServiceClient.STRUC_PSMOVE_CONTROLLER_DATA,
                                                   ByRef mJoystickPressedLastPosition As Vector3,
                                                   ByRef bClampTouchpadToBounds As Boolean,
                                                   ByRef iTouchpadTouchAreaCm As Single,
                                                   ByRef iTouchpadClickDeadzone As Single,
                                                   ByRef iHtcTouchpadEmulationClickMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD,
                                                   ByRef bJoystickShortcutBinding As Boolean,
                                                   ByRef mButtons As Boolean(),
                                                   ByRef mJoystickShortcuts As Dictionary(Of Integer, Vector2),
                                                   ByRef bJoystickShortcutTouchpadClick As Boolean)
            If (bJoystickTrigger) Then
                If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER OrElse
                        m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER) Then

                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_TOUCH) = True
                End If

                ' Just pressed
                Dim mNewPos As Vector3

                If (iHtcTouchpadMethod = UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_METHOD.USE_ORIENTATION) Then
                    If (Not bJoystickButtonPressed) Then
                        bJoystickButtonPressed = True

                        mJoystickButtonPressedTime.Restart()

                        mJoystickPressedLastOrientation = mRecenterQuat * m_PSMoveData.m_Orientation
                    End If

                    mJoystickPressedLastPosition = New Vector3(0, 0, 0)

                    mNewPos = ClassQuaternionTools.GetPositionInRotationSpace(Quaternion.Conjugate(mJoystickPressedLastOrientation) * mRecenterQuat * m_PSMoveData.m_Orientation, New Vector3(0, -1, 0))
                Else
                    If (Not bJoystickButtonPressed) Then
                        bJoystickButtonPressed = True

                        mJoystickButtonPressedTime.Restart()

                        mJoystickPressedLastOrientation = mRecenterQuat * m_PSMoveData.m_Orientation
                        mJoystickPressedLastPosition = ClassQuaternionTools.GetPositionInRotationSpace(mJoystickPressedLastOrientation, m_PSMoveData.m_Position)
                    End If

                    mNewPos = ClassQuaternionTools.GetPositionInRotationSpace(mJoystickPressedLastOrientation, m_PSMoveData.m_Position)
                End If

                Dim mNewPosDiff As New Vector3(
                    mNewPos.X - mJoystickPressedLastPosition.X,
                    mNewPos.Y - mJoystickPressedLastPosition.Y,
                    mNewPos.Z - mJoystickPressedLastPosition.Z
                )

                If (bClampTouchpadToBounds) Then
                    ' Clamp new position to iTouchpadTouchAreaCm
                    If (Math.Abs(mNewPosDiff.X) > (iTouchpadTouchAreaCm + TOUCHPAD_CLAMP_BUFFER)) Then
                        mJoystickPressedLastPosition.X -= Math.Sign(mNewPosDiff.X) * ((iTouchpadTouchAreaCm + TOUCHPAD_CLAMP_BUFFER) - Math.Abs(mNewPosDiff.X))
                    End If

                    If (Math.Abs(mNewPosDiff.Y) > iTouchpadTouchAreaCm + TOUCHPAD_CLAMP_BUFFER) Then
                        mJoystickPressedLastPosition.Y -= Math.Sign(mNewPosDiff.Y) * ((iTouchpadTouchAreaCm + TOUCHPAD_CLAMP_BUFFER) - Math.Abs(mNewPosDiff.Y))
                    End If

                    If (Math.Abs(mNewPosDiff.Z) > iTouchpadTouchAreaCm + TOUCHPAD_CLAMP_BUFFER) Then
                        mJoystickPressedLastPosition.Z -= Math.Sign(mNewPosDiff.Z) * ((iTouchpadTouchAreaCm + TOUCHPAD_CLAMP_BUFFER) - Math.Abs(mNewPosDiff.Z))
                    End If
                End If

                If (iHtcTouchpadMethod = UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_METHOD.USE_ORIENTATION) Then
                    mNewPos = (mNewPos - mJoystickPressedLastPosition) * TOUCHPAD_GYRO_MULTI
                    mNewPos.X = Math.Min(Math.Max(mNewPos.X, -1.0F), 1.0F)
                    mNewPos.Z = Math.Min(Math.Max(mNewPos.Z, -1.0F), 1.0F)
                Else
                    mNewPos = ((mNewPos - mJoystickPressedLastPosition) / iTouchpadTouchAreaCm)
                    mNewPos.X = Math.Min(Math.Max(mNewPos.X, -1.0F), 1.0F)
                    mNewPos.Z = Math.Min(Math.Max(mNewPos.Z, -1.0F), 1.0F)
                End If

                Dim mJoystickVec = New Vector2(mNewPos.X, -mNewPos.Z)

                mOscDataPack.mJoyStick = mJoystickVec

                If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER OrElse m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER) Then
                    ' Only start pressing when we moved a distance
                    If (Math.Abs(mJoystickVec.X) >= iTouchpadClickDeadzone OrElse Math.Abs(mJoystickVec.Y) >= iTouchpadClickDeadzone) Then
                        Select Case (iHtcTouchpadEmulationClickMethod)
                            Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.MOVE_ALWAYS
                                mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True

                            Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED
                                If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER) Then
                                    If (m_PSMoveData.m_TriangleButton) Then
                                        mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                    End If
                                Else
                                    If (m_PSMoveData.m_SquareButton) Then
                                        mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                    End If
                                End If

                            Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_SQUARE
                                If (m_PSMoveData.m_SquareButton) Then
                                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                End If

                            Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_TRIANGLE
                                If (m_PSMoveData.m_TriangleButton) Then
                                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                End If
                        End Select
                    End If
                End If

                If (bJoystickShortcutBinding AndAlso m_PSMoveData.m_MoveButton) Then
                    ' Record joystick shortcut while MOVE button is pressed 
                    ' Also skip MOVE button
                    For i = 1 To mButtons.Length - 1
                        If (mButtons(i)) Then
                            If (Math.Abs(mJoystickVec.X) < 0.5F AndAlso Math.Abs(mJoystickVec.Y) < 0.5F) Then
                                ' Remove shortcut
                                If (mJoystickShortcuts.ContainsKey(i)) Then
                                    mJoystickShortcuts.Remove(i)
                                End If
                            Else
                                ' Create shortcut
                                mJoystickShortcuts(i) = mOscDataPack.mJoyStick
                            End If
                        End If
                    Next
                End If
            Else
                If (bJoystickButtonPressed) Then
                    bJoystickButtonPressed = False
                End If

                mOscDataPack.mJoyStick = New Vector2(0.0F, 0.0F)

                If (bJoystickShortcutBinding) Then
                    ' Record joystick shortcut while MOVE button is pressed
                    For i = 1 To mButtons.Length - 1
                        If (mButtons(i)) Then
                            If (mJoystickShortcuts.ContainsKey(i)) Then
                                mOscDataPack.mJoyStick = mJoystickShortcuts(i)

                                If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER OrElse
                                                m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER) Then

                                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_TOUCH) = True

                                    If (bJoystickShortcutTouchpadClick) Then
                                        mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                    End If
                                End If

                                ' Never press the shortcut button, just in case its mapped
                                ' mOscDataPack.mButtons(i) = False
                            End If
                        End If
                    Next
                End If
            End If
        End Sub

        Private Sub InternalButtonsLogic(ByRef mOscDataPack As STRUC_OSC_DATA_PACK,
                                         ByRef mButtons As Boolean(),
                                         ByRef m_PSMoveData As ClassServiceClient.STRUC_PSMOVE_CONTROLLER_DATA,
                                         ByRef bJoystickTrigger As Boolean,
                                         ByRef iHtcGripButtonMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD,
                                         ByRef bGripButtonPressed As Boolean,
                                         ByRef bGripToggled As Boolean,
                                         ByRef iHtcTouchpadEmulationClickMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD)
            Select Case (m_VmtTrackerRole)
                Case ENUM_TRACKER_ROLE.GENERIC_LEFT_CONTROLLER, ENUM_TRACKER_ROLE.GENERIC_RIGHT_CONTROLLER
                    For i = 0 To mButtons.Length - 1
                        mOscDataPack.mButtons(i) = mButtons(i)
                    Next

                Case ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER, ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_SYSTEM_CLICK) = m_PSMoveData.m_StartButton
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRIGGER_CLICK) = ((m_PSMoveData.m_TriggerValue / 255.0F) > 0.75F)
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_TOUCH) = bJoystickTrigger
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = False
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = False
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_MENU_CLICK) = m_PSMoveData.m_PSButton

                    ' Do grip button
                    Select Case (iHtcGripButtonMethod)
                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_HOLDING_MIRRORED
                            If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER) Then
                                bGripButtonPressed = m_PSMoveData.m_CircleButton
                                mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = m_PSMoveData.m_CircleButton
                            Else
                                bGripButtonPressed = m_PSMoveData.m_CrossButton
                                mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = m_PSMoveData.m_CrossButton
                            End If

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_HOLDING_STRICT_CIRCLE
                            bGripButtonPressed = m_PSMoveData.m_CircleButton
                            mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = m_PSMoveData.m_CircleButton

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_HOLDING_STRICT_CROSS
                            bGripButtonPressed = m_PSMoveData.m_CrossButton
                            mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = m_PSMoveData.m_CrossButton

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_MIRRORED
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

                            mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = bGripToggled

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_STRICT_CIRCLE
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

                            mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = bGripToggled

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_STRICT_CROSS
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

                            mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = bGripToggled

                    End Select

                    ' Do touchpad click
                    Select Case (iHtcTouchpadEmulationClickMethod)
                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED
                            If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER) Then
                                If (m_PSMoveData.m_TriangleButton) Then
                                    bJoystickTrigger = True
                                End If
                            Else
                                If (m_PSMoveData.m_SquareButton) Then
                                    bJoystickTrigger = True
                                End If
                            End If

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_SQUARE
                            If (m_PSMoveData.m_SquareButton) Then
                                bJoystickTrigger = True
                            End If

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_TRIANGLE
                            If (m_PSMoveData.m_TriangleButton) Then
                                bJoystickTrigger = True
                            End If
                    End Select
            End Select

            mOscDataPack.mTrigger(0) = (m_PSMoveData.m_TriggerValue / 255.0F)

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
            If (CInt(g_mUCRemoteDeviceItem.ComboBox_DeviceID.SelectedItem) < 0) Then
                Return
            End If

            Dim iDeviceID As Integer = CInt(g_mUCRemoteDeviceItem.ComboBox_DeviceID.SelectedItem)

            If (g_mUCRemoteDeviceItem.g_bIsHMD) Then
                ' For HMDs 
                Dim sDevicePath As String = CStr(iDeviceID + ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT)

                Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)
                        SyncLock _ThreadLock
                            Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                            ' Nothing?

                            mIni.WriteKeyValue(mIniContent.ToArray)
                        End SyncLock
                    End Using
                End Using
            Else
                ' For Controllers
                Dim sDevicePath As String = CStr(iDeviceID)

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
            End If


        End Sub

        Public Sub LoadConfig()
            If (CInt(g_mUCRemoteDeviceItem.ComboBox_DeviceID.SelectedItem) < 0) Then
                Return
            End If

            Dim iDeviceID As Integer = CInt(g_mUCRemoteDeviceItem.ComboBox_DeviceID.SelectedItem)

            If (g_mUCRemoteDeviceItem.g_bIsHMD) Then
                ' For HMDs 
                Dim sDevicePath As String = CStr(iDeviceID + ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT)

                Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)

                        ' Nothing?

                    End Using
                End Using
            Else
                ' For Controllers
                Dim sDevicePath As String = CStr(iDeviceID)

                Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)
                        SetComboBoxClamp(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerID, CInt(mIni.ReadKeyValue(sDevicePath, "VMTTrackerID", "0")))
                        SetComboBoxClamp(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerRole, CInt(mIni.ReadKeyValue(sDevicePath, "VMTTrackerRole", "0")))
                    End Using
                End Using
            End If

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
End Class
