Imports System.Numerics
Imports System.Text
Imports Rug.Osc
Imports PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants

Public Class UCVirtualMotionTrackerItem
    Const HMD_DIRECT_MODE_FRAMERATE As Integer = 120
    Const MAX_DRIVER_TIMEOUT As Integer = 5000
    Const MAX_CONTROLLER_TIMEOUT As Integer = 5000

    Const VMT_LIGHTHOUSE_BEGIN_INDEX As Integer = (ClassVmtConst.VMT_TRACKER_MAX + 1)

    Public g_UCVirtualMotionTracker As UCVirtualMotionTracker

    Public g_mClassIO As ClassIO
    Public g_mClassConfig As ClassConfig

    Private g_bIgnoreEvents As Boolean = False
    Private g_bIgnoreUnsaved As Boolean = False
    Private g_bInit As Boolean = False


    Private g_iStatusHideHeight As Integer = 0
    Private g_iStatusShowHeight As Integer = g_iStatusHideHeight
    Private g_bHasStatusError As Boolean = False
    Private g_sHasStatusErrormessage As New KeyValuePair(Of String, String)("", "")

    Public g_mDriverLastResponse As New Stopwatch
    Public g_mControllerLastResponse As New Stopwatch
    Public g_sDriverLastResponseCode As Integer = 0
    Public g_sDriverLastResponseMessage As String = ""
    Public g_sDriverVersion As String = ""
    Public g_sDriverPath As String = ""
    Public g_bIsHMD As Boolean = False
    Public g_bIsHmdDirectMode As Boolean = False
    Public g_iHmdFramerate As Integer = -1

    Public Sub New(iDeviceID As Integer, bIsHMD As Boolean, _UCVirtualMotionTracker As UCVirtualMotionTracker)
        g_UCVirtualMotionTracker = _UCVirtualMotionTracker

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
            Me.ToolTip1.SetToolTip(ComboBox_DeviceID, "The PSMoveServiceEx head-mounted display id that will be used for this tracker.")
        Else
            Label_DeviceID.Text = "Controller ID:"
            Me.ToolTip1.SetToolTip(ComboBox_DeviceID, "The PSMoveServiceEx controller id that will be used for this tracker.")
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

            ComboBox_HmdViewPointOffset.Items.Clear()
            ComboBox_HmdViewPointOffset.Items.Add("Yes")
            ComboBox_HmdViewPointOffset.Items.Add("No")

            If (bIsHMD) Then
                ComboBox_HmdViewPointOffset.SelectedIndex = 0
                ComboBox_HmdViewPointOffset.Enabled = False
            Else
                ComboBox_HmdViewPointOffset.SelectedIndex = 1
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
            ComboBox_VMTTrackerRole.Items.Add("Oculus Touch Left Controller")
            ComboBox_VMTTrackerRole.Items.Add("Oculus Touch Right Controller")

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

        CreateControl()

        ' Hide timeout error
        Panel_Status.Visible = False
        g_iStatusHideHeight = (Me.Height - Panel_Status.Height - Panel_Status.Margin.Top)
        g_iStatusShowHeight = Me.Height
        Me.Height = g_iStatusHideHeight
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

        AddHandler g_UCVirtualMotionTracker.g_ClassOscServer.OnOscProcessMessage, AddressOf OnOscProcessMessage
        AddHandler g_UCVirtualMotionTracker.g_ClassOscServer.OnSuspendChanged, AddressOf OnOscSuspendChanged

        Try
            Try
                g_bIgnoreUnsaved = True
                g_mClassConfig.LoadConfig()
            Finally
                g_bIgnoreUnsaved = False
            End Try

            UpdateTrackerRoleComboBox()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

        SetUnsavedState(False)
        OnOscSuspendChanged()
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
        If (g_UCVirtualMotionTracker Is Nothing OrElse g_UCVirtualMotionTracker.g_ClassOscServer Is Nothing) Then
            Return
        End If

        Dim bEnabled As Boolean = (Not g_UCVirtualMotionTracker.g_ClassOscServer.IsRunning OrElse g_UCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests)

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

                    ClassUtils.AsyncInvoke(Sub()
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
                            If (g_UCVirtualMotionTracker.g_ClassOscServer.IsRunning) Then
                                g_UCVirtualMotionTracker.g_ClassOscServer.SendRoomMatrixIdentity()
                            End If
                    End Select

                    If (iCode = 0) Then
                        ClassUtils.AsyncInvoke(Sub()
                                                   g_mDriverLastResponse.Restart()

                                                   g_sDriverLastResponseCode = iCode
                                                   g_sDriverLastResponseMessage = sReason
                                               End Sub)
                    Else
                        ClassUtils.AsyncInvoke(Sub()
                                                   g_mDriverLastResponse.Restart()

                                                   g_sDriverLastResponseCode = iCode
                                                   g_sDriverLastResponseMessage = sReason
                                               End Sub)
                    End If

                Case "/VMT/Out/HmdInfo"
                    If (mMessage.Count < 4) Then
                        Return
                    End If

                    Dim iFramerate As Integer = CInt(mMessage(0))
                    Dim iScreenWdith As Integer = CInt(mMessage(1))
                    Dim iScreenHeight As Integer = CInt(mMessage(2))
                    Dim bDirectMode = CBool(mMessage(3))

                    ClassUtils.AsyncInvoke(Sub()
                                               g_mDriverLastResponse.Restart()

                                               g_iHmdFramerate = iFramerate
                                               g_bIsHmdDirectMode = bDirectMode
                                           End Sub)
            End Select
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
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
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        Finally
            g_bIgnoreEvents = False
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
        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()

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
        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
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

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub ComboBox_TrackerRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_SteamTrackerRole.SelectedIndexChanged
        UpdateTrackerTitle()

        If (g_bIgnoreEvents) Then
            Return
        End If

        SetUnsavedState(True)

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub ComboBox_HmdViewPointOffset_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_HmdViewPointOffset.SelectedIndexChanged
        UpdateTrackerTitle()

        If (g_bIgnoreEvents) Then
            Return
        End If

        If (g_bIsHMD) Then
            Return
        End If

        g_mClassIO.m_UsbHmdViewPointOffset = (ComboBox_HmdViewPointOffset.SelectedIndex = 0)
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
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub TimerFPS_Tick(sender As Object, e As EventArgs) Handles TimerFPS.Tick
        TimerFPS.Stop()

        Try
            Dim iFpsCoutner As Integer = g_mClassIO.m_FpsOscCounter
            If (iFpsCoutner > 0) Then
                g_mControllerLastResponse.Restart()
            End If

            If (Me.Visible) Then
                TextBox_Fps.Text = String.Format("OSC IO: {0}/s", iFpsCoutner)
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try

        TimerFPS.Start()
    End Sub

    Private Sub TimerPose_Tick(sender As Object, e As EventArgs) Handles TimerPose.Tick
        Try
            TimerPose.Stop()

            Dim mPosition As Vector3 = Vector3.Zero
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

            If (Me.Visible) Then
                If (bValid) Then
                    TextBox_Pos.Text = String.Format("Pos X: {0}{3}Pos Y: {1}{3}Pos Z: {2}", Math.Floor(mPosition.X), Math.Floor(mPosition.Y), Math.Floor(mPosition.Z), Environment.NewLine)

                    Dim iAng = ClassMathUtils.FromQ(mOrientation)
                    TextBox_Gyro.Text = String.Format("Ang X: {0}{3}Ang Y: {1}{3}Ang Z: {2}", Math.Floor(iAng.X), Math.Floor(iAng.Y), Math.Floor(iAng.Z), Environment.NewLine)
                Else
                    TextBox_Pos.Text = String.Format("Pos X: {0}{3}Pos Y: {1}{3}Pos Z: {2}", "N/A", "N/A", "N/A", Environment.NewLine)
                    TextBox_Gyro.Text = String.Format("Ang X: {0}{3}Ang Y: {1}{3}Ang Z: {2}", "N/A", "N/A", "N/A", Environment.NewLine)
                End If
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try

        TimerPose.Start()
    End Sub


    Private Sub Timer_Status_Tick(sender As Object, e As EventArgs) Handles Timer_Status.Tick
        Timer_Status.Stop()

        Try
            Dim sTitle As String = ""
            Dim sMessage As String = ""
            Dim iStatusType As Integer = -1 ' -1 Hide, 0 Info, 1 Warn, 2 Error

            While True


                ' Check controller id
                If (g_mClassIO IsNot Nothing AndAlso g_mClassIO.m_Index < 0) Then
                    sTitle = "Tracker is disabled"

                    Dim sText As New Text.StringBuilder

                    If (g_mClassIO.m_IsHMD) Then
                        sText.AppendLine("The HMD id has not been set.")
                    Else
                        sText.AppendLine("The controller id has not been set.")
                    End If

                    sMessage = sText.ToString
                    iStatusType = 2

                    Exit While
                End If

                ' Check VMT id
                If (g_mClassIO IsNot Nothing AndAlso g_mClassIO.m_VmtTracker < 0) Then
                    If (Not g_mClassIO.m_IsHMD) Then
                        sTitle = "Tracker is disabled"

                        Dim sText As New Text.StringBuilder
                        sText.AppendLine("The VMT tracker id has not been set. Please choose a free index slot.")

                        sMessage = sText.ToString
                        iStatusType = 2

                        Exit While
                    End If
                End If

                If (g_UCVirtualMotionTracker.g_ClassOscServer.IsRunning AndAlso Not g_UCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
                    ' Show driver timeouts
                    If (g_mDriverLastResponse.ElapsedMilliseconds > MAX_DRIVER_TIMEOUT) Then
                        sTitle = "Driver not is responding"

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
                        sTitle = String.Format("Driver responded with an error (Code: {0})", g_sDriverLastResponseCode)

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
                        sTitle = "Driver version incompatible"

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

                    ' Show HMD bad direct-mode
                    If (g_bIsHMD AndAlso g_bIsHmdDirectMode AndAlso g_iHmdFramerate <> HMD_DIRECT_MODE_FRAMERATE) Then
                        sTitle = "Driver is unable to load PlayStation VR display configuration"


                        Dim sText As New Text.StringBuilder
                        sText.AppendLine("Go 'SteamVR > Developer > Developer Settings' and click 'Disable Direct Display Mode'. Restart SteamVR and click 'Enable Direct Display Mode' to fix this issue.")

                        sMessage = sText.ToString
                        iStatusType = 2

                        Exit While
                    End If

                    ' Show tracker not working
                    If (g_mControllerLastResponse.ElapsedMilliseconds > MAX_CONTROLLER_TIMEOUT) Then
                        sTitle = "Device is not responding"

                        Dim sText As New Text.StringBuilder
                        sText.AppendLine("There are no new incoming pose data. Make sure PSMoveServiceEx is running.")

                        sMessage = sText.ToString
                        iStatusType = 2

                        Exit While
                    End If
                End If

                Exit While
            End While

            g_bHasStatusError = (iStatusType > -1)
            g_sHasStatusErrormessage = New KeyValuePair(Of String, String)(sTitle, sMessage)

            If (Me.Visible) Then
                If (Label_StatusTitle.Text <> g_sHasStatusErrormessage.Key OrElse Label_StatusMessage.Text <> g_sHasStatusErrormessage.Value) Then
                    Label_StatusTitle.Text = g_sHasStatusErrormessage.Key
                    Label_StatusMessage.Text = g_sHasStatusErrormessage.Value
                End If

                If (g_bHasStatusError) Then
                    If (Not Panel_Status.Visible) Then
                        Panel_Status.Visible = True

                        If (Me.Height <> g_iStatusShowHeight) Then
                            Me.Height = g_iStatusShowHeight
                        End If
                    End If
                Else
                    If (Panel_Status.Visible) Then
                        Panel_Status.Visible = False

                        If (Me.Height <> g_iStatusHideHeight) Then
                            Me.Height = g_iStatusHideHeight
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try

        Timer_Status.Start()
    End Sub

    ReadOnly Property m_HasStatusError As Boolean
        Get
            Return g_bHasStatusError
        End Get
    End Property

    ReadOnly Property m_HasStatusErrorMessage As KeyValuePair(Of String, String)
        Get
            Return g_sHasStatusErrormessage
        End Get
    End Property

    Private Sub CleanUp()
        If (g_UCVirtualMotionTracker IsNot Nothing AndAlso g_UCVirtualMotionTracker.g_ClassOscServer IsNot Nothing) Then
            RemoveHandler g_UCVirtualMotionTracker.g_ClassOscServer.OnOscProcessMessage, AddressOf OnOscProcessMessage
        End If

        If (g_UCVirtualMotionTracker IsNot Nothing AndAlso g_UCVirtualMotionTracker.g_ClassOscServer IsNot Nothing) Then
            RemoveHandler g_UCVirtualMotionTracker.g_ClassOscServer.OnSuspendChanged, AddressOf OnOscSuspendChanged
        End If

        If (g_mClassIO IsNot Nothing) Then
            g_mClassIO.Dispose()
            g_mClassIO = Nothing
        End If
    End Sub

    Public Class ClassIO
        Implements IDisposable

        Private g_mThreadLock As New Object
        Public g_UCVirtualMotionTrackerItem As UCVirtualMotionTrackerItem

        Const TOUCHPAD_CLAMP_BUFFER = 2.5F
        Const TOUCHPAD_GYRO_MULTI = 25.0F

        Const ENABLE_GENERIC_TRACKER = 1
        Const ENABLE_GENERIC_CONTROLLER_L = 2
        Const ENABLE_GENERIC_CONTROLLER_R = 3
        Const ENABLE_TRACKINGREFECNCE = 4
        Const ENABLE_HTC_VIVE_TRACKER = 5
        Const ENABLE_HTC_VIVE_CONTROLLER_L = 6
        Const ENABLE_HTC_VIVE_CONTROLLER_R = 7
        Const ENABLE_OCULUS_TOUCH_CONTROLLER_L = 8
        Const ENABLE_OCULUS_TOUCH_CONTROLLER_R = 9
        Const ENABLE_HTC_VIVE_LIGHTHOUSE = 10
        Const ENABLE_HMD = 11

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

        Const OCULUS_TOUCH_BUTTON_AX_CLICK = 0
        Const OCULUS_TOUCH_BUTTON_AX_TOUCH = 1
        Const OCULUS_TOUCH_BUTTON_BY_CLICK = 2
        Const OCULUS_TOUCH_BUTTON_BY_TOUCH = 3
        Const OCULUS_TOUCH_BUTTON_SYSTEM_CLICK = 4 'OCULUS_TOUCH_BUTTON_BACK_CLICK
        Const OCULUS_TOUCH_BUTTON_SYSTEM_TOUCH = 5 'OCULUS_TOUCH_BUTTON_GUIDE_CLICK
        Const OCULUS_TOUCH_BUTTON_GRIP_CLICK = 6
        Const OCULUS_TOUCH_BUTTON_GRIP_TOUCH = 7
        Const OCULUS_TOUCH_BUTTON_JOYSTICK_CLICK = 8
        Const OCULUS_TOUCH_BUTTON_JOYSTICK_TOUCH = 9
        Const OCULUS_TOUCH_BUTTON_START_CLICK = 10
        Const OCULUS_TOUCH_BUTTON_TRIGGER_CLICK = 11
        Const OCULUS_TOUCH_BUTTON_TRIGGER_TOUCH = 12

        Enum ENUM_TRACKER_ROLE
            GENERIC_TRACKER
            GENERIC_LEFT_CONTROLLER
            GENERIC_RIGHT_CONTROLLER
            HTC_VIVE_TRACKER
            HTC_VIVE_LEFT_CONTROLLER
            HTC_VIVE_RIGHT_CONTROLLER
            OCULUS_TOUCH_LEFT_CONTROLLER
            OCULUS_TOUCH_RIGHT_CONTROLLER

            __MAX
        End Enum

        Private g_iIndex As Integer = -1
        Private g_bIsHMD As Boolean = False
        Private g_iVmtTracker As Integer = -1
        Private g_iVmtTrackerRole As ENUM_TRACKER_ROLE = ENUM_TRACKER_ROLE.GENERIC_TRACKER
        Private g_bUsbHmdViewPointOffset As Boolean = False
        Private g_mOscThread As Threading.Thread = Nothing

        Private g_mJointOffset As Vector3 = Vector3.Zero
        Private g_mControllerOffset As Vector3 = Vector3.Zero
        Private g_iJointYawCorrection As Integer = 0
        Private g_iControllerYawCorrection As Integer = 0
        Private g_bOnlyJointOffset As Boolean = False

        Private g_mFpsOscCounter As New Queue(Of Date)
        Private g_mControllerData As ClassServiceClient.IControllerData
        Private g_mHmdData As ClassServiceClient.IHmdData
        Private g_mTrackerData As New Dictionary(Of Integer, ClassServiceClient.ITrackerData)

        Private g_mOscDataPack As New STRUC_OSC_DATA_PACK()
        Private g_mHeptic As New STRUC_HEPTIC_ITEM
        Private g_mResetRecenter As Boolean = False

        Class STURC_PLAYSPACE_CALIBRATION_STATUS
            Public Enum ENUM_PLAYSPACE_CALIBRATION_STATUS
                FAILED = -1
                INACTIVE
                PSMOVE_RUNNING
                MANUAL_RUNNING
                DONE
            End Enum

            Private g_iStatus As ENUM_PLAYSPACE_CALIBRATION_STATUS = ENUM_PLAYSPACE_CALIBRATION_STATUS.INACTIVE

            Property m_Status As ENUM_PLAYSPACE_CALIBRATION_STATUS
                Get
                    Return g_iStatus
                End Get
                Set(value As ENUM_PLAYSPACE_CALIBRATION_STATUS)
                    g_iStatus = value
                End Set
            End Property
        End Class

        Class STURC_PLAYSPACE_CALIBRATION
            Private g_mPlayspaceCalibrationState As STURC_PLAYSPACE_CALIBRATION_STATUS
            Private g_iMinDistance As Single = 10.0F
            Private g_bFinishCalibration As Boolean = False

            Public Sub New(_PlayspaceCalibrationState As STURC_PLAYSPACE_CALIBRATION_STATUS)
                g_mPlayspaceCalibrationState = _PlayspaceCalibrationState
            End Sub

            Public Sub StartCalibration(Optional _MinDistance As Single = 10.0F)
                g_mPlayspaceCalibrationState.m_Status = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING
                g_bFinishCalibration = False
                g_iMinDistance = _MinDistance
            End Sub

            Public Sub StopCalibration()
                If (g_mPlayspaceCalibrationState.m_Status <> STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.DONE) Then
                    g_mPlayspaceCalibrationState.m_Status = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.FAILED
                End If
                g_bFinishCalibration = False
            End Sub

            Public Property m_FinishCalibration As Boolean
                Get
                    Return g_bFinishCalibration
                End Get
                Set(value As Boolean)
                    g_bFinishCalibration = value
                End Set
            End Property

            Public Property m_MinDistance As Single
                Get
                    Return g_iMinDistance
                End Get
                Set(value As Single)
                    g_iMinDistance = value
                End Set
            End Property

            Public Function GetStatus() As STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS
                Return g_mPlayspaceCalibrationState.m_Status
            End Function
        End Class
        Private g_mPlayspaceCalibrationState As STURC_PLAYSPACE_CALIBRATION_STATUS = Nothing
        Private g_mPlayspaceCalibration As STURC_PLAYSPACE_CALIBRATION = Nothing

        Private g_bManualControllerRecenter As Boolean = False
        Private g_bManualHmdRecenter As Boolean = False

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
            Const POS_EQUAL_TOLERANCE As Single = 0.01F
            Const ROT_EQUAL_TOLERANCE As Single = 0.0001F

            Public mPosition As Vector3 = Vector3.Zero
            Public mOrientation As Quaternion = Quaternion.Identity
            Public mPositionVelocity As Vector3 = Vector3.Zero
            Public mOrientationVelocity As Vector3 = Vector3.Zero

            Public mButtons As New Dictionary(Of Integer, Boolean)
            Public mTrigger As New Dictionary(Of Integer, Single)
            Public mJoyStick As Vector2 = Vector2.Zero

            Public Sub New()
            End Sub

            Public Sub New(_Pack As STRUC_OSC_DATA_PACK)
                mPosition = _Pack.mPosition
                mOrientation = _Pack.mOrientation
                mPositionVelocity = _Pack.mPositionVelocity
                mOrientationVelocity = _Pack.mOrientationVelocity

                mButtons = New Dictionary(Of Integer, Boolean)(_Pack.mButtons)
                mTrigger = New Dictionary(Of Integer, Single)(_Pack.mTrigger)
                mJoyStick = _Pack.mJoyStick
            End Sub

            Public Function IsPositionEqual(mOther As STRUC_OSC_DATA_PACK) As Boolean
                Return IsPositionEqual(mOther.mPosition)
            End Function

            Public Function IsPositionEqual(vec As Vector3) As Boolean
                If (Math.Abs(mPosition.X - vec.X) < POS_EQUAL_TOLERANCE AndAlso
                        Math.Abs(mPosition.Y - vec.Y) < POS_EQUAL_TOLERANCE AndAlso
                        Math.Abs(mPosition.Z - vec.Z) < POS_EQUAL_TOLERANCE) Then
                    Return True
                End If

                Return False
            End Function

            Public Function IsQuaternionEqual(mOther As STRUC_OSC_DATA_PACK) As Boolean
                Return IsQuaternionEqual(mOther.mOrientation)
            End Function

            Public Function IsQuaternionEqual(quat As Quaternion) As Boolean
                If (Math.Abs(mOrientation.X - quat.X) < ROT_EQUAL_TOLERANCE AndAlso
                        Math.Abs(mOrientation.Y - quat.Y) < ROT_EQUAL_TOLERANCE AndAlso
                        Math.Abs(mOrientation.Z - quat.Z) < ROT_EQUAL_TOLERANCE AndAlso
                        Math.Abs(mOrientation.W - quat.W) < ROT_EQUAL_TOLERANCE) Then
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

            g_mPlayspaceCalibrationState = New STURC_PLAYSPACE_CALIBRATION_STATUS
            g_mPlayspaceCalibration = New STURC_PLAYSPACE_CALIBRATION(g_mPlayspaceCalibrationState)
        End Sub

        Property m_Index As Integer
            Get
                SyncLock g_mThreadLock
                    Return g_iIndex
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock g_mThreadLock
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
                SyncLock g_mThreadLock
                    Return g_bIsHMD
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock g_mThreadLock
                    g_bIsHMD = value
                End SyncLock
            End Set
        End Property

        Property m_VmtTracker As Integer
            Get
                SyncLock g_mThreadLock
                    Return g_iVmtTracker
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock g_mThreadLock
                    g_iVmtTracker = value
                End SyncLock
            End Set
        End Property

        Property m_VmtTrackerRole As ENUM_TRACKER_ROLE
            Get
                SyncLock g_mThreadLock
                    Return g_iVmtTrackerRole
                End SyncLock
            End Get
            Set(value As ENUM_TRACKER_ROLE)
                SyncLock g_mThreadLock
                    g_iVmtTrackerRole = value
                End SyncLock
            End Set
        End Property

        Property m_UsbHmdViewPointOffset As Boolean
            Get
                SyncLock g_mThreadLock
                    If (g_bIsHMD) Then
                        Return True
                    End If

                    Return g_bUsbHmdViewPointOffset
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock g_mThreadLock
                    g_bUsbHmdViewPointOffset = value
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

        ReadOnly Property m_FpsOscCounter As Integer
            Get
                SyncLock g_mThreadLock
                    Dim mNow As Date = Now

                    While (g_mFpsOscCounter.Count > 0 AndAlso g_mFpsOscCounter.Peek() + New TimeSpan(0, 0, 1) < mNow)
                        g_mFpsOscCounter.Dequeue()
                    End While

                    Return g_mFpsOscCounter.Count
                End SyncLock
            End Get
        End Property

        Public Sub AddFpsOscCounter()
            SyncLock g_mThreadLock
                Dim mNow As Date = Now

                While (g_mFpsOscCounter.Count > 0 AndAlso g_mFpsOscCounter.Peek() + New TimeSpan(0, 0, 1) < mNow)
                    g_mFpsOscCounter.Dequeue()
                End While

                g_mFpsOscCounter.Enqueue(mNow)
            End SyncLock
        End Sub

        Property m_HmdData As ClassServiceClient.IHmdData
            Get
                SyncLock g_mThreadLock
                    Return g_mHmdData
                End SyncLock
            End Get
            Set(value As ClassServiceClient.IHmdData)
                SyncLock g_mThreadLock
                    g_mHmdData = value
                End SyncLock
            End Set
        End Property

        Property m_ControllerData As ClassServiceClient.IControllerData
            Get
                SyncLock g_mThreadLock
                    Return g_mControllerData
                End SyncLock
            End Get
            Set(value As ClassServiceClient.IControllerData)
                SyncLock g_mThreadLock
                    g_mControllerData = value
                End SyncLock
            End Set
        End Property

        Property m_TrackerData(iIndex As Integer) As ClassServiceClient.ITrackerData
            Get
                SyncLock g_mThreadLock
                    Return g_mTrackerData(iIndex)
                End SyncLock
            End Get
            Set(value As ClassServiceClient.ITrackerData)
                SyncLock g_mThreadLock
                    g_mTrackerData(iIndex) = value
                End SyncLock
            End Set
        End Property

        Public Sub SetHepticFeedback(fFrequency As Single, fAmplitude As Single, fDuration As Single)
            Dim mClassSettings = g_UCVirtualMotionTrackerItem.g_UCVirtualMotionTracker.g_ClassSettings
            If (Not mClassSettings.m_MiscSettings.m_EnableHepticFeedback) Then
                Return
            End If

            SyncLock g_mThreadLock
                g_mHeptic.fFrequency = fFrequency
                g_mHeptic.fAmplitude = fAmplitude
                g_mHeptic.fDuration = fDuration
            End SyncLock
        End Sub

        Public Sub ResetRecenter()
            SyncLock g_mThreadLock
                g_mResetRecenter = True
            End SyncLock
        End Sub

        Public Sub StartControllerRecenter()
            SyncLock g_mThreadLock
                g_bManualControllerRecenter = True
            End SyncLock
        End Sub

        Public Sub StartHmdRecenter()
            SyncLock g_mThreadLock
                g_bManualHmdRecenter = True
            End SyncLock
        End Sub

        ReadOnly Property m_PlayspaceCalibration As STURC_PLAYSPACE_CALIBRATION
            Get
                SyncLock g_mThreadLock
                    Return g_mPlayspaceCalibration
                End SyncLock
            End Get
        End Property

        Private ReadOnly Property m_PlayspaceCalibrationState As STURC_PLAYSPACE_CALIBRATION_STATUS
            Get
                SyncLock g_mThreadLock
                    Return g_mPlayspaceCalibrationState
                End SyncLock
            End Get
        End Property

        Public Sub Disable()
            If (g_mOscThread Is Nothing OrElse Not g_mOscThread.IsAlive) Then
                Return
            End If

            g_mOscThread.Abort()
            g_mOscThread.Join()
            g_mOscThread = Nothing
        End Sub

        Class STRUC_HMD_DISPLAY_DATA
            Public iDisplayX As Integer
            Public iDisplayY As Integer
            Public iDisplayW As Integer
            Public iDisplayH As Integer
            Public iRenderW As Integer
            Public iRenderH As Integer
            Public iFrameRate As Integer
            Public bDirectMode As Boolean
            Public iVendorId As Integer
            Public iProductId As Integer
            Public bDisplaySuccess As Boolean
            Public mDisplayNextUpdate As Stopwatch
            Public mDisplaySetupUpdate As Stopwatch

            Public Sub New()
                iDisplayX = 0
                iDisplayY = 0
                iDisplayW = 0
                iDisplayH = 0
                iRenderW = 0
                iRenderH = 0
                iFrameRate = 0
                bDirectMode = False
                iVendorId = 0
                iProductId = 0
                bDisplaySuccess = False
                mDisplayNextUpdate = New Stopwatch
                mDisplaySetupUpdate = New Stopwatch
            End Sub
        End Class

        Private Sub ThreadOsc()
            Dim iLastOutputSeqNum As Integer = 0
            Dim iLastOutputSeqNumFailures As Integer = 0

            Dim mOscDataPack As New STRUC_OSC_DATA_PACK()
            Dim mEnforcePosePacketUpdate As New Stopwatch
            Dim mEnforceInputPacketUpdate As New Stopwatch

            ' Controller  
            Dim mLastBatteryReport As New Stopwatch


            Dim bFirstEnabled As Boolean = False
            Dim mTrackerDataUpdate As New Stopwatch

            Dim mHmdDisplayData As New STRUC_HMD_DISPLAY_DATA
            Dim mCalculateVelocityManualData As New STRUC_CALCULATE_VELOCITY_MANUAL_DATA
            Dim mCalculateVelocityData As New STRUC_CALCULATE_VELOCITY_DATA
            Dim mHepticFeedbackData As New STRUC_HEPTIC_FEEDBACK_DATA
            Dim mPlayspaceRecenterData As New STRUC_PLAYSPACE_RECENTER_DATA
            Dim mRecenterControllerData As New STRUC_RECENTER_CONTROLLER_DATA
            Dim mRecenterHmdData As New STRUC_RECENTER_HMD_DATA
            Dim mJoystickEmulationData As New STRUC_JOYSTICK_EMULATION_DATA
            Dim mButtonsData As New STRUC_BUTTONS_DATA

            Dim mRecenterQuat = Quaternion.Identity

            Dim mClampedExecution As New ClassPrecisionSleep.ClassFrameTimed()

            While True
                Dim bExceptionSleep As Boolean = False

                Try
                    If (g_iIndex < 0) Then
                        Return
                    End If

                    Dim mUCVirtualMotionTracker = g_UCVirtualMotionTrackerItem.g_UCVirtualMotionTracker

                    If (mClampedExecution.CanExecute()) Then
                        mClampedExecution.m_Framerate = mUCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_OscMaxThreadFps

                        If (Not mUCVirtualMotionTracker.g_ClassOscServer.IsRunning) Then
                            Throw New ArgumentException("OSC server is not running")
                        End If

                        If (mUCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
                            Throw New ArgumentException("OSC server is suspended")
                        End If

                        Dim mClassSettings = mUCVirtualMotionTracker.g_ClassSettings

                        If (Not bFirstEnabled) Then
                            bFirstEnabled = True

                            mTrackerDataUpdate.Restart()
                            mLastBatteryReport.Restart()
                            mEnforcePosePacketUpdate.Restart()
                            mEnforceInputPacketUpdate.Restart()

                            'Load recentering
                            If (Not m_IsHMD) Then
                                mClassSettings.m_ControllerSettings.m_ControllerRecenter.TryGetValue(g_iIndex, mRecenterQuat)
                            End If
                        End If

                        SyncLock g_mThreadLock
                            If (g_mResetRecenter) Then
                                g_mResetRecenter = False

                                mRecenterQuat = Quaternion.Identity

                                If (Not m_IsHMD) Then
                                    mClassSettings.m_ControllerSettings.m_ControllerRecenter(g_iIndex) = mRecenterQuat
                                    mClassSettings.SaveSettings(UCVirtualMotionTracker.ENUM_SETTINGS_SAVE_TYPE_FLAGS.DEVICE_RECENTER)
                                End If
                            End If
                        End SyncLock

                        ' Get controller settings
                        Dim bHtcTouchpadShortcutBinding = mClassSettings.m_ControllerSettings.m_HtcTouchpadShortcutBinding
                        Dim bHtcTouchpadShortcutTouchpadClick = mClassSettings.m_ControllerSettings.m_HtcTouchpadShortcutTouchpadClick
                        Dim iHtcTouchpadEmulationClickMethod = mClassSettings.m_ControllerSettings.m_HtcTouchpadEmulationClickMethod
                        Dim iOculusButtonMethod = mClassSettings.m_ControllerSettings.m_OculusButtonMethod
                        Dim bOculusGripToggle = mClassSettings.m_ControllerSettings.m_OculusGripToggle
                        Dim bHybridGripToggle = mClassSettings.m_ControllerSettings.m_HybridGripToggle
                        Dim iHtcGripButtonMethod = mClassSettings.m_ControllerSettings.m_HtcGripButtonMethod
                        Dim iControllerJoystickMethod = mClassSettings.m_ControllerSettings.m_ControllerJoystickMethod
                        Dim bEnableControllerRecenter = mClassSettings.m_ControllerSettings.m_EnableControllerRecenter
                        Dim iControllerRecenterMethod = mClassSettings.m_ControllerSettings.m_ControllerRecenterMethod
                        Dim sControllerRecenterFromDeviceName = mClassSettings.m_ControllerSettings.m_ControllerRecenterFromDeviceName
                        Dim bEnableHmdRecenter = mClassSettings.m_ControllerSettings.m_EnableHmdRecenter
                        Dim iHmdRecenterMethod = mClassSettings.m_ControllerSettings.m_HmdRecenterMethod
                        Dim sHmdRecenterFromDeviceName = mClassSettings.m_ControllerSettings.m_HmdRecenterFromDeviceName
                        Dim iRecenterButtonTimeMs = mClassSettings.m_ControllerSettings.m_RecenterButtonTimeMs
                        Dim iControllerJoystickAreaCm = mClassSettings.m_ControllerSettings.m_ControllerJoystickAreaCm
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
                        Dim mViewPositionOffset = mClassSettings.m_HmdSettings.m_ViewPositionOffset
                        Dim mViewRotationOffset = mClassSettings.m_HmdSettings.m_ViewRotationOffset

                        ' Get misc settings
                        Dim bDisableBaseStationSpawning As Boolean = mClassSettings.m_MiscSettings.m_DisableBaseStationSpawning
                        Dim bEnableHepticFeedback As Boolean = mClassSettings.m_MiscSettings.m_EnableHepticFeedback
                        Dim bOptimizeTransportPackets As Boolean = mClassSettings.m_MiscSettings.m_OptimizeTransportPackets
                        Dim bEnableVelocityHmd As Boolean = mClassSettings.m_MiscSettings.m_EnableVelocityHmd
                        Dim bEnableVelocityController As Boolean = mClassSettings.m_MiscSettings.m_EnableVelocityController
                        Dim bEnableVelocityTracker As Boolean = mClassSettings.m_MiscSettings.m_EnableVelocityTracker
                        Dim bEnableManualVelocity As Boolean = mClassSettings.m_MiscSettings.m_EnableManualVelocity


                        Dim bEnfocePosePacketUpdate As Boolean = False
                        Dim bEnfoceInputPacketUpdate As Boolean = False
                        If (mEnforcePosePacketUpdate.ElapsedMilliseconds > 250) Then
                            mEnforcePosePacketUpdate.Restart()

                            bEnfocePosePacketUpdate = True
                        End If
                        If (mEnforceInputPacketUpdate.ElapsedMilliseconds > 250) Then
                            mEnforceInputPacketUpdate.Restart()

                            bEnfoceInputPacketUpdate = True
                        End If

                        Dim mServiceClient = mUCVirtualMotionTracker.g_mFormMain.g_mPSMoveServiceCAPI


                        If (m_IsHMD) Then
                            ' Get hmd data
                            g_mHmdData = mServiceClient.m_HmdData(m_Index)

                            If (g_mHmdData IsNot Nothing) Then
                                ' We got any new data?
                                If (iLastOutputSeqNumFailures > 250 OrElse iLastOutputSeqNum < g_mHmdData.m_OutputSeqNum) Then
                                    iLastOutputSeqNumFailures = 0
                                    iLastOutputSeqNum = g_mHmdData.m_OutputSeqNum

                                    SyncLock g_mThreadLock
                                        Dim mRawPosition = g_mHmdData.m_Position
                                        Dim mRawOrientation = g_mHmdData.m_Orientation
                                        Dim mRawPositionVelocity = g_mHmdData.m_PositionVelocity
                                        Dim mRawOrientationVelocity = g_mHmdData.m_OrientationVelocity

                                        Dim mCalibratedPosition = mRawPosition
                                        Dim mCalibratedOrientation = mRawOrientation
                                        Dim mCalibratedPositionVelocity = mRawPositionVelocity
                                        Dim mCalibratedOrientationVelocity = mRawOrientationVelocity

                                        ' Playspace offsets, used for playspace calibration
                                        InternalApplyPlayspaceCalibrationLogic(mClassSettings.m_PlayspaceSettings,
                                                                               mCalibratedPosition,
                                                                               mCalibratedOrientation,
                                                                               mCalibratedPositionVelocity,
                                                                               mCalibratedOrientationVelocity,
                                                                               True)

                                        Dim mPosition = mCalibratedPosition
                                        Dim mOrientation = mRecenterQuat * mCalibratedOrientation
                                        Dim mPositionVelocity = mCalibratedPositionVelocity
                                        Dim mOrientationVelocity = mCalibratedOrientationVelocity

                                        InternalApplyViewPointOffsetLogic(mViewPositionOffset,
                                                                          mViewRotationOffset,
                                                                          mPosition,
                                                                          mOrientation,
                                                                          mOrientationVelocity)

                                        mOscDataPack.mPosition = mPosition
                                        mOscDataPack.mOrientation = mOrientation
                                        mOscDataPack.mPositionVelocity = mPositionVelocity
                                        mOscDataPack.mOrientationVelocity = mOrientationVelocity

                                        ' $TODO Do something cool?

                                        'Do Hmd/remote recenter 
                                        InternalRecenterHmd(bEnableHmdRecenter,
                                                            mServiceClient,
                                                            iRecenterButtonTimeMs,
                                                            iHmdRecenterMethod,
                                                            sHmdRecenterFromDeviceName,
                                                            mCalibratedPosition,
                                                            mCalibratedOrientation,
                                                            mRecenterQuat,
                                                            mUCVirtualMotionTracker,
                                                            True,
                                                            mClassSettings,
                                                            mRecenterHmdData)

                                        If (mHmdDisplayData.bDisplaySuccess AndAlso mHmdDisplayData.iDisplayW > 0 AndAlso mHmdDisplayData.iDisplayH > 0) Then
                                            Dim bSetPack As Boolean = False

                                            ' Setup the HMD
                                            ' $TODO Make this less retarded. Get status from the driver if something isnt set up properly.
                                            If (Not mHmdDisplayData.mDisplaySetupUpdate.IsRunning OrElse mHmdDisplayData.mDisplaySetupUpdate.ElapsedMilliseconds > 500) Then
                                                mHmdDisplayData.mDisplaySetupUpdate.Restart()

                                                If (mHmdDisplayData.bDirectMode) Then
                                                    mUCVirtualMotionTracker.g_ClassOscServer.SendHmdSetupDisplayDirect(
                                                        mHmdDisplayData.iDisplayX, mHmdDisplayData.iDisplayY,
                                                        mHmdDisplayData.iDisplayW, mHmdDisplayData.iDisplayH,
                                                        mHmdDisplayData.iRenderW, mHmdDisplayData.iRenderH,
                                                        mHmdDisplayData.iFrameRate,
                                                        mHmdDisplayData.iVendorId, mHmdDisplayData.iProductId)
                                                    AddFpsOscCounter()
                                                Else
                                                    mUCVirtualMotionTracker.g_ClassOscServer.SendHmdSetupDisplay(
                                                        mHmdDisplayData.iDisplayX, mHmdDisplayData.iDisplayY,
                                                        mHmdDisplayData.iDisplayW, mHmdDisplayData.iDisplayH,
                                                        mHmdDisplayData.iRenderW, mHmdDisplayData.iRenderH,
                                                        mHmdDisplayData.iFrameRate)
                                                    AddFpsOscCounter()
                                                End If

                                                mUCVirtualMotionTracker.g_ClassOscServer.SendHmdSetupRender(
                                                    iHmdDistortK0, iHmdDistortK1, iHmdDistortScale,
                                                    -iHmdDistortRedOffset, -iHmdDistortGreenOffset, -iHmdDistortBlueOffset,
                                                    iHmdHFov, iHmdVFov)
                                                AddFpsOscCounter()

                                                mUCVirtualMotionTracker.g_ClassOscServer.SendHmdSetIpdMeters(iHmdIPD)
                                                AddFpsOscCounter()
                                            End If

                                            If (bEnfocePosePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                    Not g_mOscDataPack.IsPositionEqual(mOscDataPack) OrElse Not g_mOscDataPack.IsQuaternionEqual(mOscDataPack)) Then
                                                mEnforcePosePacketUpdate.Restart()

                                                Dim mCalcPosition As Vector3 = mOscDataPack.mPosition
                                                Dim mCalcOrientation As Quaternion = mOscDataPack.mOrientation
                                                Dim mVelocityPosition As Vector3 = Vector3.Zero
                                                Dim mVelocityOrientation As Vector3 = Vector3.Zero
                                                Dim iVelocityTimeOffset As Single = 0.0F

                                                If (bEnableVelocityHmd) Then
                                                    If (bEnableManualVelocity) Then
                                                        InternalCalculateVelocityManual(
                                                            mCalcPosition, mCalcOrientation,
                                                            mVelocityPosition, mVelocityOrientation,
                                                            iVelocityTimeOffset,
                                                            mCalculateVelocityManualData
                                                        )
                                                    Else
                                                        mVelocityPosition = mOscDataPack.mPositionVelocity
                                                        mVelocityOrientation = mOscDataPack.mOrientationVelocity

                                                        InternalCalculateVelocity(
                                                            mRawPosition, mRawOrientation,
                                                            mCalcPosition, mCalcOrientation,
                                                            mVelocityPosition, mVelocityOrientation,
                                                            iVelocityTimeOffset,
                                                            mCalculateVelocityData
                                                        )
                                                    End If
                                                End If

                                                mUCVirtualMotionTracker.g_ClassOscServer.SendHmdPose(
                                                    iLastOutputSeqNum, 0.0,
                                                    mCalcPosition * PSM_CENTIMETERS_TO_METERS,
                                                    mCalcOrientation,
                                                    mVelocityPosition * PSM_CENTIMETERS_TO_METERS,
                                                    mVelocityOrientation)
                                                AddFpsOscCounter()
                                                bSetPack = True
                                            End If

                                            If (bSetPack) Then
                                                g_mOscDataPack = New STRUC_OSC_DATA_PACK(mOscDataPack)
                                            End If
                                        End If

                                        ' May cause thread delay, put this in the end so we send pose without delay just in case.
                                        If (Not mHmdDisplayData.mDisplayNextUpdate.IsRunning OrElse mHmdDisplayData.mDisplayNextUpdate.ElapsedMilliseconds > 5000) Then
                                            mHmdDisplayData.mDisplayNextUpdate.Restart()

                                            Dim mClassMonitor As New ClassMonitor
                                            Dim mDevMode As ClassMonitor.DEVMODE = Nothing
                                            Dim mDisplayInfo As KeyValuePair(Of ClassMonitor.DISPLAY_DEVICE, ClassMonitor.MONITOR_DEVICE) = Nothing

                                            Select Case (mClassMonitor.FindPlaystationVrMonitor(mDevMode, mDisplayInfo))
                                                Case ClassMonitor.ENUM_PSVR_MONITOR_STATUS.SUCCESS
                                                    ' If we found a monitor, its probably in virtual-mode.

                                                    If (Not String.IsNullOrEmpty(mDevMode.dmDeviceName)) Then
                                                        If (mClassMonitor.IsPlaystationVrMonitorPatched() = ClassMonitor.ENUM_PATCHED_RESGITRY_STATE.PATCHED_MULTI) Then
                                                            mHmdDisplayData.iDisplayX = mDevMode.dmPositionX
                                                            mHmdDisplayData.iDisplayY = mDevMode.dmPositionY
                                                            mHmdDisplayData.iDisplayW = mDevMode.dmPelsWidth
                                                            mHmdDisplayData.iDisplayH = mDevMode.dmPelsHeight
                                                            mHmdDisplayData.iRenderW = CInt((mHmdDisplayData.iDisplayW * iHmdRenderScale) / 2)
                                                            mHmdDisplayData.iRenderH = CInt((mHmdDisplayData.iDisplayH * iHmdRenderScale))
                                                            mHmdDisplayData.iFrameRate = mDevMode.dmDisplayFrequency
                                                            mHmdDisplayData.bDirectMode = False

                                                            mHmdDisplayData.bDisplaySuccess = True
                                                        End If
                                                    End If

                                                Case ClassMonitor.ENUM_PSVR_MONITOR_STATUS.ERROR_NOT_ACTIVE,
                                                        ClassMonitor.ENUM_PSVR_MONITOR_STATUS.ERROR_NOT_FOUND
                                                    ' If display is not active or not found, its probably direct-mode
                                                    If (mClassMonitor.IsPlaystationVrMonitorPatched() = ClassMonitor.ENUM_PATCHED_RESGITRY_STATE.PATCHED_DIRECT) Then
                                                        '$TODO: Use settings to adjust properties like framerate.
                                                        mHmdDisplayData.iDisplayX = 0
                                                        mHmdDisplayData.iDisplayY = 0
                                                        mHmdDisplayData.iDisplayW = 1920
                                                        mHmdDisplayData.iDisplayH = 1080
                                                        mHmdDisplayData.iRenderW = CInt((mHmdDisplayData.iDisplayW * iHmdRenderScale) / 2)
                                                        mHmdDisplayData.iRenderH = CInt((mHmdDisplayData.iDisplayH * iHmdRenderScale))
                                                        mHmdDisplayData.iFrameRate = HMD_DIRECT_MODE_FRAMERATE
                                                        mHmdDisplayData.bDirectMode = True

                                                        Dim sMonitorName As String = mClassMonitor.GetPlaystationVrInstalledMonitorName()

                                                        For l = 0 To ClassMonitor.PSVR_MONITOR_IDS.Length - 1
                                                            Dim mPsvrMonitor = ClassMonitor.PSVR_MONITOR_IDS(l)

                                                            If (mPsvrMonitor.GetMonitorNameLong().ToUpperInvariant.EndsWith(sMonitorName.ToUpperInvariant)) Then
                                                                mHmdDisplayData.iVendorId = mPsvrMonitor.GetVID()
                                                                mHmdDisplayData.iProductId = mPsvrMonitor.GetPID()

                                                                mHmdDisplayData.bDisplaySuccess = True
                                                            End If
                                                        Next
                                                    End If
                                            End Select
                                        End If
                                    End SyncLock
                                ElseIf (iLastOutputSeqNum <> g_mHmdData.m_OutputSeqNum) Then
                                    iLastOutputSeqNumFailures += 1
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
                                                                    mServiceClient,
                                                                    mHepticFeedbackData)
                                End Select

                                ' We got any new data?
                                If (iLastOutputSeqNumFailures > 250 OrElse iLastOutputSeqNum < m_ControllerData.m_OutputSeqNum) Then
                                    iLastOutputSeqNumFailures = 0
                                    iLastOutputSeqNum = m_ControllerData.m_OutputSeqNum

                                    Dim iBatteryValue As Single = m_ControllerData.m_BatteryLevel
                                    Dim bIsVirtualCOntroller As Boolean = m_ControllerData.m_Serial.StartsWith("VirtualController")

                                    SyncLock g_mThreadLock
                                        Dim mRawPosition = m_ControllerData.m_Position
                                        Dim mRawOrientation = m_ControllerData.m_Orientation
                                        Dim mRawPositionVelocity = m_ControllerData.m_PositionVelocity
                                        Dim mRawOrientationVelocity = m_ControllerData.m_OrientationVelocity

                                        Dim mCalibratedPosition = mRawPosition
                                        Dim mCalibratedOrientation = mRawOrientation
                                        Dim mCalibratedPositionVelocity = mRawPositionVelocity
                                        Dim mCalibratedOrientationVelocity = mRawOrientationVelocity

                                        ' Playspace offsets, used for playspace calibration
                                        InternalApplyPlayspaceCalibrationLogic(mClassSettings.m_PlayspaceSettings,
                                                                               mCalibratedPosition,
                                                                               mCalibratedOrientation,
                                                                               mCalibratedPositionVelocity,
                                                                               mCalibratedOrientationVelocity,
                                                                               True)

                                        Dim mPosition = mCalibratedPosition
                                        Dim mOrientation = mRecenterQuat * mCalibratedOrientation
                                        Dim mPositionVelocity = mCalibratedPositionVelocity
                                        Dim mOrientationVelocity = mCalibratedOrientationVelocity

                                        If (m_UsbHmdViewPointOffset) Then
                                            InternalApplyViewPointOffsetLogic(mViewPositionOffset,
                                                                          mViewRotationOffset,
                                                                          mPosition,
                                                                          mOrientation,
                                                                          mOrientationVelocity)
                                        End If

                                        mOscDataPack.mPosition = mPosition
                                        mOscDataPack.mOrientation = mOrientation
                                        mOscDataPack.mPositionVelocity = mPositionVelocity
                                        mOscDataPack.mOrientationVelocity = mOrientationVelocity

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
                                                                                iRecenterButtonTimeMs,
                                                                                mClassSettings,
                                                                                mUCVirtualMotionTracker,
                                                                                mPlayspaceRecenterData)

                                                'Do controller recenter
                                                InternalRecenterControllerLogic(bEnableControllerRecenter,
                                                                                m_PSMoveData.m_SelectButton AndAlso Not m_PSMoveData.m_StartButton,
                                                                                iRecenterButtonTimeMs,
                                                                                iControllerRecenterMethod,
                                                                                sControllerRecenterFromDeviceName,
                                                                                mRecenterQuat,
                                                                                mCalibratedPosition,
                                                                                mCalibratedOrientation,
                                                                                mUCVirtualMotionTracker,
                                                                                mClassSettings,
                                                                                mRecenterControllerData)

                                                'Do Hmd/remote recenter
                                                InternalRecenterHmd(bEnableHmdRecenter,
                                                                    mServiceClient,
                                                                    iRecenterButtonTimeMs,
                                                                    iHmdRecenterMethod,
                                                                    sHmdRecenterFromDeviceName,
                                                                    mCalibratedPosition,
                                                                    mCalibratedOrientation,
                                                                    mRecenterQuat,
                                                                    mUCVirtualMotionTracker,
                                                                    False,
                                                                    mClassSettings,
                                                                    mRecenterHmdData)

                                                'Send buttons
                                                InternalButtonsLogic(mOscDataPack,
                                                                    mButtons,
                                                                    m_PSMoveData,
                                                                    bJoystickTrigger,
                                                                    iHtcGripButtonMethod,
                                                                    iHtcTouchpadEmulationClickMethod,
                                                                    bOculusGripToggle,
                                                                    iOculusButtonMethod,
                                                                    bHybridGripToggle,
                                                                    mButtonsData)

                                                'Joystick emulation
                                                InternalJoystickEmulationLogic(mOscDataPack,
                                                                        bJoystickTrigger,
                                                                        iControllerJoystickMethod,
                                                                        mRecenterQuat,
                                                                        m_PSMoveData,
                                                                        iControllerJoystickAreaCm,
                                                                        iTouchpadClickDeadzone,
                                                                        iHtcTouchpadEmulationClickMethod,
                                                                        bHtcTouchpadShortcutBinding,
                                                                        mButtons,
                                                                        bHtcTouchpadShortcutTouchpadClick,
                                                                        mJoystickEmulationData)
                                        End Select

                                        'Send battery level to
                                        If (mLastBatteryReport.Elapsed > New TimeSpan(0, 0, 1)) Then
                                            mLastBatteryReport.Restart()

                                            mUCVirtualMotionTracker.g_ClassOscServer.SendDeviceBattery(
                                                    m_VmtTracker,
                                                    iBatteryValue)
                                            AddFpsOscCounter()
                                        End If

                                        Select Case (m_VmtTrackerRole)
                                            Case ENUM_TRACKER_ROLE.GENERIC_TRACKER
                                                Dim bSetPack As Boolean = False

                                                If (bEnfocePosePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                        Not g_mOscDataPack.IsPositionEqual(mOscDataPack) OrElse Not g_mOscDataPack.IsQuaternionEqual(mOscDataPack)) Then
                                                    mEnforcePosePacketUpdate.Restart()

                                                    Dim mCalcPosition As Vector3 = mOscDataPack.mPosition
                                                    Dim mCalcOrientation As Quaternion = mOscDataPack.mOrientation
                                                    Dim mVelocityPosition As Vector3 = Vector3.Zero
                                                    Dim mVelocityOrientation As Vector3 = Vector3.Zero
                                                    Dim iVelocityTimeOffset As Single = 0.0F

                                                    If (bEnableVelocityTracker) Then
                                                        If (bEnableManualVelocity) Then
                                                            InternalCalculateVelocityManual(
                                                                mCalcPosition, mCalcOrientation,
                                                                mVelocityPosition, mVelocityOrientation,
                                                                iVelocityTimeOffset,
                                                                mCalculateVelocityManualData
                                                            )
                                                        Else
                                                            mVelocityPosition = mOscDataPack.mPositionVelocity
                                                            mVelocityOrientation = mOscDataPack.mOrientationVelocity

                                                            InternalCalculateVelocity(
                                                                mRawPosition, mRawOrientation,
                                                                mCalcPosition, mCalcOrientation,
                                                                mVelocityPosition, mVelocityOrientation,
                                                                iVelocityTimeOffset,
                                                                mCalculateVelocityData
                                                            )
                                                        End If
                                                    End If

                                                    mUCVirtualMotionTracker.g_ClassOscServer.SendDevicePose(
                                                        iLastOutputSeqNum,
                                                        m_VmtTracker, ENABLE_GENERIC_TRACKER, iVelocityTimeOffset,
                                                        mCalcPosition * PSM_CENTIMETERS_TO_METERS,
                                                        mCalcOrientation,
                                                        mVelocityPosition * PSM_CENTIMETERS_TO_METERS,
                                                        mVelocityOrientation)
                                                    AddFpsOscCounter()
                                                    bSetPack = True
                                                End If

                                                If (bSetPack) Then
                                                    g_mOscDataPack = New STRUC_OSC_DATA_PACK(mOscDataPack)
                                                End If

                                            Case ENUM_TRACKER_ROLE.GENERIC_LEFT_CONTROLLER, ENUM_TRACKER_ROLE.GENERIC_RIGHT_CONTROLLER
                                                Dim bSetPack As Boolean = False

                                                Dim iController As Integer = ENABLE_GENERIC_TRACKER
                                                Select Case (m_VmtTrackerRole)
                                                    Case ENUM_TRACKER_ROLE.GENERIC_LEFT_CONTROLLER
                                                        iController = ENABLE_GENERIC_CONTROLLER_L

                                                    Case Else
                                                        iController = ENABLE_GENERIC_CONTROLLER_R
                                                End Select

                                                If (bEnfoceInputPacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                        Not g_mOscDataPack.IsInputEqual(mOscDataPack)) Then
                                                    mEnforceInputPacketUpdate.Restart()

                                                    For Each mButton In mOscDataPack.mButtons
                                                        mUCVirtualMotionTracker.g_ClassOscServer.SendDeviceInputButton(
                                                                m_VmtTracker, mButton.Key, mButton.Value)
                                                        AddFpsOscCounter()
                                                        bSetPack = True
                                                    Next

                                                    For Each mTrigger In mOscDataPack.mTrigger
                                                        mUCVirtualMotionTracker.g_ClassOscServer.SendDeviceInputTrigger(
                                                               m_VmtTracker, mTrigger.Key, mTrigger.Value)
                                                        AddFpsOscCounter()
                                                        bSetPack = True
                                                    Next

                                                    mUCVirtualMotionTracker.g_ClassOscServer.SendDeviceInputJoystick(
                                                        m_VmtTracker, 0,
                                                        mOscDataPack.mJoyStick.X, mOscDataPack.mJoyStick.Y)
                                                    AddFpsOscCounter()
                                                    bSetPack = True

                                                End If

                                                If (bEnfocePosePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                        Not g_mOscDataPack.IsPositionEqual(mOscDataPack) OrElse Not g_mOscDataPack.IsQuaternionEqual(mOscDataPack)) Then
                                                    mEnforcePosePacketUpdate.Restart()

                                                    Dim mCalcPosition As Vector3 = mOscDataPack.mPosition
                                                    Dim mCalcOrientation As Quaternion = mOscDataPack.mOrientation
                                                    Dim mVelocityPosition As Vector3 = Vector3.Zero
                                                    Dim mVelocityOrientation As Vector3 = Vector3.Zero
                                                    Dim iVelocityTimeOffset As Single = 0.0F

                                                    If (bEnableVelocityController) Then
                                                        If (bEnableManualVelocity) Then
                                                            InternalCalculateVelocityManual(
                                                                mCalcPosition, mCalcOrientation,
                                                                mVelocityPosition, mVelocityOrientation,
                                                                iVelocityTimeOffset,
                                                                mCalculateVelocityManualData
                                                            )
                                                        Else
                                                            mVelocityPosition = mOscDataPack.mPositionVelocity
                                                            mVelocityOrientation = mOscDataPack.mOrientationVelocity

                                                            InternalCalculateVelocity(
                                                                mRawPosition, mRawOrientation,
                                                                mCalcPosition, mCalcOrientation,
                                                                mVelocityPosition, mVelocityOrientation,
                                                                iVelocityTimeOffset,
                                                                mCalculateVelocityData
                                                            )
                                                        End If
                                                    End If

                                                    mUCVirtualMotionTracker.g_ClassOscServer.SendDevicePose(
                                                        iLastOutputSeqNum,
                                                        m_VmtTracker, iController, iVelocityTimeOffset,
                                                        mCalcPosition * PSM_CENTIMETERS_TO_METERS,
                                                        mCalcOrientation,
                                                        mVelocityPosition * PSM_CENTIMETERS_TO_METERS,
                                                        mVelocityOrientation)
                                                    AddFpsOscCounter()
                                                    bSetPack = True
                                                End If

                                                If (bSetPack) Then
                                                    g_mOscDataPack = New STRUC_OSC_DATA_PACK(mOscDataPack)
                                                End If

                                            Case ENUM_TRACKER_ROLE.HTC_VIVE_TRACKER
                                                Dim bSetPack As Boolean = False

                                                If (bEnfocePosePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                        Not g_mOscDataPack.IsPositionEqual(mOscDataPack) OrElse Not g_mOscDataPack.IsQuaternionEqual(mOscDataPack)) Then
                                                    mEnforcePosePacketUpdate.Restart()

                                                    Dim mCalcPosition As Vector3 = mOscDataPack.mPosition
                                                    Dim mCalcOrientation As Quaternion = mOscDataPack.mOrientation
                                                    Dim mVelocityPosition As Vector3 = Vector3.Zero
                                                    Dim mVelocityOrientation As Vector3 = Vector3.Zero
                                                    Dim iVelocityTimeOffset As Single = 0.0F

                                                    If (bEnableVelocityTracker) Then
                                                        If (bEnableManualVelocity) Then
                                                            InternalCalculateVelocityManual(
                                                                mCalcPosition, mCalcOrientation,
                                                                mVelocityPosition, mVelocityOrientation,
                                                                iVelocityTimeOffset,
                                                                mCalculateVelocityManualData
                                                            )
                                                        Else
                                                            mVelocityPosition = mOscDataPack.mPositionVelocity
                                                            mVelocityOrientation = mOscDataPack.mOrientationVelocity

                                                            InternalCalculateVelocity(
                                                                mRawPosition, mRawOrientation,
                                                                mCalcPosition, mCalcOrientation,
                                                                mVelocityPosition, mVelocityOrientation,
                                                                iVelocityTimeOffset,
                                                                mCalculateVelocityData
                                                            )
                                                        End If
                                                    End If

                                                    mUCVirtualMotionTracker.g_ClassOscServer.SendDevicePose(
                                                        iLastOutputSeqNum,
                                                        m_VmtTracker, ENABLE_HTC_VIVE_TRACKER, iVelocityTimeOffset,
                                                        mCalcPosition * PSM_CENTIMETERS_TO_METERS,
                                                        mCalcOrientation,
                                                        mVelocityPosition * PSM_CENTIMETERS_TO_METERS,
                                                        mVelocityOrientation)
                                                    AddFpsOscCounter()
                                                    bSetPack = True
                                                End If

                                                If (bSetPack) Then
                                                    g_mOscDataPack = New STRUC_OSC_DATA_PACK(mOscDataPack)
                                                End If

                                            Case ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER, ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER,
                                                 ENUM_TRACKER_ROLE.OCULUS_TOUCH_LEFT_CONTROLLER, ENUM_TRACKER_ROLE.OCULUS_TOUCH_RIGHT_CONTROLLER
                                                Dim bSetPack As Boolean = False

                                                Dim iController As Integer = ENABLE_GENERIC_TRACKER
                                                Select Case (m_VmtTrackerRole)
                                                    Case ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER
                                                        iController = ENABLE_HTC_VIVE_CONTROLLER_L

                                                    Case ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER
                                                        iController = ENABLE_HTC_VIVE_CONTROLLER_R

                                                    Case ENUM_TRACKER_ROLE.OCULUS_TOUCH_LEFT_CONTROLLER
                                                        iController = ENABLE_OCULUS_TOUCH_CONTROLLER_L

                                                    Case ENUM_TRACKER_ROLE.OCULUS_TOUCH_RIGHT_CONTROLLER
                                                        iController = ENABLE_OCULUS_TOUCH_CONTROLLER_R
                                                End Select

                                                If (bEnfoceInputPacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                        Not g_mOscDataPack.IsInputEqual(mOscDataPack)) Then
                                                    mEnforceInputPacketUpdate.Restart()

                                                    For Each mButton In mOscDataPack.mButtons
                                                        mUCVirtualMotionTracker.g_ClassOscServer.SendDeviceInputButton(
                                                            m_VmtTracker, mButton.Key, mButton.Value)
                                                        AddFpsOscCounter()
                                                        bSetPack = True
                                                    Next

                                                    For Each mTrigger In mOscDataPack.mTrigger
                                                        mUCVirtualMotionTracker.g_ClassOscServer.SendDeviceInputTrigger(
                                                            m_VmtTracker, mTrigger.Key, mTrigger.Value)
                                                        AddFpsOscCounter()
                                                        bSetPack = True
                                                    Next

                                                    mUCVirtualMotionTracker.g_ClassOscServer.SendDeviceInputJoystick(
                                                        m_VmtTracker, 0,
                                                        mOscDataPack.mJoyStick.X, mOscDataPack.mJoyStick.Y)
                                                    AddFpsOscCounter()
                                                    bSetPack = True
                                                End If

                                                If (bEnfocePosePacketUpdate OrElse Not bOptimizeTransportPackets OrElse
                                                        Not g_mOscDataPack.IsPositionEqual(mOscDataPack) OrElse Not g_mOscDataPack.IsQuaternionEqual(mOscDataPack)) Then
                                                    mEnforcePosePacketUpdate.Restart()

                                                    Dim mCalcPosition As Vector3 = mOscDataPack.mPosition
                                                    Dim mCalcOrientation As Quaternion = mOscDataPack.mOrientation
                                                    Dim mVelocityPosition As Vector3 = Vector3.Zero
                                                    Dim mVelocityOrientation As Vector3 = Vector3.Zero
                                                    Dim iVelocityTimeOffset As Single = 0.0F

                                                    If (bEnableVelocityController) Then
                                                        If (bEnableManualVelocity) Then
                                                            InternalCalculateVelocityManual(
                                                                mCalcPosition, mCalcOrientation,
                                                                mVelocityPosition, mVelocityOrientation,
                                                                iVelocityTimeOffset,
                                                                mCalculateVelocityManualData
                                                            )
                                                        Else
                                                            mVelocityPosition = mOscDataPack.mPositionVelocity
                                                            mVelocityOrientation = mOscDataPack.mOrientationVelocity

                                                            InternalCalculateVelocity(
                                                                mRawPosition, mRawOrientation,
                                                                mCalcPosition, mCalcOrientation,
                                                                mVelocityPosition, mVelocityOrientation,
                                                                iVelocityTimeOffset,
                                                                mCalculateVelocityData
                                                            )
                                                        End If
                                                    End If

                                                    mUCVirtualMotionTracker.g_ClassOscServer.SendDevicePose(
                                                        iLastOutputSeqNum,
                                                        m_VmtTracker, iController, iVelocityTimeOffset,
                                                        mCalcPosition * PSM_CENTIMETERS_TO_METERS,
                                                        mCalcOrientation,
                                                        mVelocityPosition * PSM_CENTIMETERS_TO_METERS,
                                                        mVelocityOrientation)
                                                    AddFpsOscCounter()
                                                    bSetPack = True
                                                End If

                                                If (bSetPack) Then
                                                    g_mOscDataPack = New STRUC_OSC_DATA_PACK(mOscDataPack)
                                                End If

                                        End Select
                                    End SyncLock
                                ElseIf (iLastOutputSeqNum <> m_ControllerData.m_OutputSeqNum) Then
                                    iLastOutputSeqNumFailures += 1
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
                                        Dim mRawPosition = m_TrackerData(i).m_Position

                                        Dim mCalibratedOrientation = mRawOrientation
                                        Dim mCalibratedPosition = mRawPosition

                                        ' Playspace offsets, used for playspace calibration
                                        InternalApplyPlayspaceCalibrationLogic(mClassSettings.m_PlayspaceSettings,
                                                                               mCalibratedPosition,
                                                                               mCalibratedOrientation,
                                                                               Vector3.Zero,
                                                                               Vector3.Zero,
                                                                               False)

                                        Dim mOrientation As Quaternion = mCalibratedOrientation
                                        Dim mPosition As Vector3 = mCalibratedPosition

                                        ' Cameras are flipped, flip them correctly
                                        Dim mFlippedQ As Quaternion = mOrientation * Quaternion.CreateFromAxisAngle(Vector3.UnitY, 180.0F * (Math.PI / 180.0F))

                                        'Use Right-Handed space for SteamVR 
                                        mUCVirtualMotionTracker.g_ClassOscServer.SendDevicePose(
                                            iLastOutputSeqNum,
                                            VMT_LIGHTHOUSE_BEGIN_INDEX + i, ENABLE_HTC_VIVE_LIGHTHOUSE, 0.0F,
                                            mPosition * PSM_CENTIMETERS_TO_METERS,
                                            mFlippedQ,
                                            Vector3.Zero,
                                            Vector3.Zero)
                                    End If
                                Next
                            End If
                        End If
                    End If

                    ClassPrecisionSleep.Sleep(mUCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_OscThreadSleepMs)
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    bExceptionSleep = True
                    ClassAdvancedExceptionLogging.WriteToLog(ex)
                End Try

                ' Thread.Abort will not trigger inside a Try/Catch
                If (bExceptionSleep) Then
                    bExceptionSleep = False
                    Threading.Thread.Sleep(1000)
                End If
            End While
        End Sub

        Class STRUC_CALCULATE_VELOCITY_MANUAL_DATA
            Public mLastPosition As Vector3
            Public mLastOrientation As Quaternion
            Public mLastVelocityPosition As Vector3
            Public mLastVelocityOrientation As Vector3
            Public mLastPositionTime As Date
            Public mLastOrientationTime As Date
            Public mNormalizedPositionDelta As Queue(Of Double)
            Public mNormalizedOrientationDelta As Queue(Of Double)
            Public iVelocityPositionDelta As Double
            Public iVelocityOrientationDelta As Double

            Public Sub New()
                mLastPosition = Vector3.Zero
                mLastOrientation = Quaternion.Identity
                mLastVelocityPosition = Vector3.Zero
                mLastVelocityOrientation = Vector3.Zero
                mLastPositionTime = Now
                mLastOrientationTime = Now
                mNormalizedPositionDelta = New Queue(Of Double)
                mNormalizedOrientationDelta = New Queue(Of Double)
                iVelocityPositionDelta = 0.0
                iVelocityOrientationDelta = 0.0
            End Sub
        End Class

        Private Sub InternalCalculateVelocityManual(ByRef mPosition As Vector3, ByRef mOrientation As Quaternion,
                                                    ByRef mVelocityPosition As Vector3, ByRef mVelocityOrientation As Vector3,
                                                    ByRef iVelocityTimeOffset As Single,
                                                    ByRef mData As STRUC_CALCULATE_VELOCITY_MANUAL_DATA)
            Const MIN_VELOCITY_FREQ = (1.0 / 10.0)
            Const MAX_VELOCITY_FREQ = (1.0 / 2500.0)
            Const VELOCITY_POSITION_SMOOTHING = 0.4
            Const VELOCITY_ORIENTATION_SMOOTHING = 0.2

            Dim mNow As Date = Now

            ' Linear Velocity
            If (True) Then
                Dim mDelta As TimeSpan = (mNow - mData.mLastPositionTime)
                Dim iDeltaTime As Double = mDelta.TotalSeconds

                If (mPosition <> mData.mLastPosition) Then
                    mData.mLastPositionTime = mNow

                    mData.mNormalizedPositionDelta.Enqueue(iDeltaTime)
                    If (mData.mNormalizedPositionDelta.Count > 30) Then
                        mData.mNormalizedPositionDelta.Dequeue()
                    End If
                    Dim iAvgiDeltaTime = mData.mNormalizedPositionDelta.Average()

                    Dim mNewVelocity = New Vector3(
                        mPosition.X - mData.mLastPosition.X,
                        mPosition.Y - mData.mLastPosition.Y,
                        mPosition.Z - mData.mLastPosition.Z
                    )
                    mData.mLastVelocityPosition = ClassMathUtils.ExponentialLowpassFilter(VELOCITY_POSITION_SMOOTHING, mNewVelocity, mData.mLastVelocityPosition)

                    mData.mLastPosition = mPosition
                    mData.iVelocityPositionDelta = iAvgiDeltaTime
                End If

                If (mData.iVelocityPositionDelta > Double.Epsilon AndAlso mData.iVelocityPositionDelta <= MIN_VELOCITY_FREQ AndAlso iDeltaTime <= MIN_VELOCITY_FREQ) Then
                    mVelocityPosition = New Vector3(
                        CSng(mData.mLastVelocityPosition.X / mData.iVelocityPositionDelta),
                        CSng(mData.mLastVelocityPosition.Y / mData.iVelocityPositionDelta),
                        CSng(mData.mLastVelocityPosition.Z / mData.iVelocityPositionDelta)
                    )
                    mPosition = New Vector3(
                        CSng(mData.mLastPosition.X - (mVelocityPosition.X * mData.iVelocityPositionDelta)),
                        CSng(mData.mLastPosition.Y - (mVelocityPosition.Y * mData.iVelocityPositionDelta)),
                        CSng(mData.mLastPosition.Z - (mVelocityPosition.Z * mData.iVelocityPositionDelta))
                    )
                End If
            End If

            ' Angular Velocity
            If (True) Then
                Dim mDelta As TimeSpan = (mNow - mData.mLastOrientationTime)
                Dim iDeltaTime As Double = mDelta.TotalSeconds

                If (mOrientation <> mData.mLastOrientation) Then
                    mData.mLastOrientationTime = mNow

                    mData.mNormalizedOrientationDelta.Enqueue(iDeltaTime)
                    If (mData.mNormalizedOrientationDelta.Count > 30) Then
                        mData.mNormalizedOrientationDelta.Dequeue()
                    End If
                    Dim iAvgDeltaTime = mData.mNormalizedOrientationDelta.Average()

                    Dim mNewVelocity = ClassMathUtils.AngularVelocityBetweenQuats(
                        mData.mLastOrientation, mOrientation, 1.0F
                    )
                    mData.mLastVelocityOrientation = ClassMathUtils.ExponentialLowpassFilter(VELOCITY_ORIENTATION_SMOOTHING, mNewVelocity, mData.mLastVelocityOrientation)

                    mData.mLastOrientation = mOrientation
                    mData.iVelocityOrientationDelta = iAvgDeltaTime
                End If

                If (mData.iVelocityOrientationDelta > Double.Epsilon AndAlso mData.iVelocityOrientationDelta <= MIN_VELOCITY_FREQ AndAlso iDeltaTime <= MIN_VELOCITY_FREQ) Then
                    mVelocityOrientation = New Vector3(
                        CSng(mData.mLastVelocityOrientation.X / mData.iVelocityOrientationDelta),
                        CSng(mData.mLastVelocityOrientation.Y / mData.iVelocityOrientationDelta),
                        CSng(mData.mLastVelocityOrientation.Z / mData.iVelocityOrientationDelta)
                    )
                    mOrientation = mData.mLastOrientation * Quaternion.Conjugate(ClassMathUtils.QuaternionFromAngularVelocity(mVelocityOrientation, mData.iVelocityOrientationDelta))
                End If
            End If
        End Sub

        Class STRUC_CALCULATE_VELOCITY_DATA
            Public mLastRawPosition As Vector3
            Public mLastRawOrientation As Quaternion
            Public mLastVelocityPosition As Vector3
            Public mLastVelocityOrientation As Vector3
            Public mLastPositionTime As Date
            Public mLastOrientationTime As Date
            Public mNormalizedPositionDelta As Queue(Of Double)
            Public mNormalizedOrientationDelta As Queue(Of Double)
            Public iVelocityPositionDelta As Double
            Public iVelocityOrientationDelta As Double

            Public Sub New()
                mLastRawPosition = Vector3.Zero
                mLastRawOrientation = Quaternion.Identity
                mLastVelocityPosition = Vector3.Zero
                mLastVelocityOrientation = Vector3.Zero
                mLastPositionTime = Now
                mLastOrientationTime = Now
                mNormalizedPositionDelta = New Queue(Of Double)
                mNormalizedOrientationDelta = New Queue(Of Double)
                iVelocityPositionDelta = 0.0
                iVelocityOrientationDelta = 0.0
            End Sub
        End Class

        Private Sub InternalCalculateVelocity(ByRef mRawPosition As Vector3, ByRef mRawOrientation As Quaternion,
                                                ByRef mPosition As Vector3, ByRef mOrientation As Quaternion,
                                                ByRef mVelocityPosition As Vector3, ByRef mVelocityOrientation As Vector3,
                                                ByRef iVelocityTimeOffset As Single,
                                                ByRef mData As STRUC_CALCULATE_VELOCITY_DATA)
            Const MIN_VELOCITY_FREQ = (1.0 / 10.0)
            Const MAX_VELOCITY_FREQ = (1.0 / 2500.0)

            Dim mNow As Date = Now

            ' Linear Velocity
            If (True) Then
                Dim mDelta As TimeSpan = (mNow - mData.mLastPositionTime)
                Dim iDeltaTime As Double = mDelta.TotalSeconds

                If (mData.mLastRawPosition <> mRawPosition) Then
                    mData.mLastPositionTime = mNow

                    mData.mNormalizedPositionDelta.Enqueue(iDeltaTime)
                    If (mData.mNormalizedPositionDelta.Count > 30) Then
                        mData.mNormalizedPositionDelta.Dequeue()
                    End If
                    Dim iAvgDeltaTime = mData.mNormalizedPositionDelta.Average()

                    mData.mLastRawPosition = mRawPosition
                    mData.iVelocityPositionDelta = iAvgDeltaTime
                End If

                If (mData.iVelocityPositionDelta > Double.Epsilon AndAlso mData.iVelocityPositionDelta <= MIN_VELOCITY_FREQ AndAlso iDeltaTime <= MIN_VELOCITY_FREQ) Then
                    mPosition = New Vector3(
                        CSng(mPosition.X - (mVelocityPosition.X * mData.iVelocityPositionDelta)),
                        CSng(mPosition.Y - (mVelocityPosition.Y * mData.iVelocityPositionDelta)),
                        CSng(mPosition.Z - (mVelocityPosition.Z * mData.iVelocityPositionDelta))
                    )
                Else
                    mVelocityPosition = Vector3.Zero
                End If
            End If

            ' Angular Velocity
            If (True) Then
                Dim mDelta As TimeSpan = (mNow - mData.mLastOrientationTime)
                Dim iDeltaTime As Double = mDelta.TotalSeconds

                If (mData.mLastRawOrientation <> mRawOrientation) Then
                    mData.mLastOrientationTime = mNow

                    mData.mNormalizedOrientationDelta.Enqueue(iDeltaTime)
                    If (mData.mNormalizedOrientationDelta.Count > 30) Then
                        mData.mNormalizedOrientationDelta.Dequeue()
                    End If
                    Dim iAvgDeltaTime = mData.mNormalizedOrientationDelta.Average()

                    mData.mLastRawOrientation = mRawOrientation
                    mData.iVelocityOrientationDelta = iAvgDeltaTime
                End If

                If (mData.iVelocityOrientationDelta > Double.Epsilon AndAlso mData.iVelocityOrientationDelta <= MIN_VELOCITY_FREQ AndAlso iDeltaTime <= MIN_VELOCITY_FREQ) Then
                    mOrientation = mOrientation * Quaternion.Conjugate(ClassMathUtils.QuaternionFromAngularVelocity(mVelocityOrientation, mData.iVelocityOrientationDelta))
                Else
                    mVelocityOrientation = Vector3.Zero
                End If
            End If
        End Sub

        Class STRUC_HEPTIC_FEEDBACK_DATA
            Public mRumbleLastTimeSendValid As Boolean
            Public mRumbleLastTimeSend As Date

            Public Sub New()
                mRumbleLastTimeSendValid = False
                mRumbleLastTimeSend = Now
            End Sub
        End Class

        Private Sub InternalHepticFeedbackLogic(ByRef bEnableHepticFeedback As Boolean,
                                                ByRef mServiceClient As ClassServiceClient,
                                                ByRef mData As STRUC_HEPTIC_FEEDBACK_DATA)
            If (bEnableHepticFeedback) Then
                Const MAX_RUMBLE_UPODATE_RATE As Single = 33.0F
                Const MAX_PULSE_MICROSECONDS As Single = 5000.0F

                Dim fHepticDuraction As Single
                Dim fHelpticAmplitude As Single

                SyncLock g_mThreadLock
                    fHepticDuraction = g_mHeptic.fDuration
                    fHelpticAmplitude = g_mHeptic.fAmplitude
                End SyncLock

                Dim fHapticPulseDurationMicroSec As Single = (fHepticDuraction * 1000000.0F)

                Dim bTimoutElapsed As Boolean = True

                If (mData.mRumbleLastTimeSendValid) Then
                    Dim mLastSend As TimeSpan = (Now - mData.mRumbleLastTimeSend)

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

                    mData.mRumbleLastTimeSend = Now
                    mData.mRumbleLastTimeSendValid = True

                    SyncLock g_mThreadLock
                        g_mHeptic.Clear()
                    End SyncLock
                End If
            Else
                SyncLock g_mThreadLock
                    g_mHeptic.Clear()
                End SyncLock
            End If
        End Sub

        Private Sub InternalApplyViewPointOffsetLogic(ByRef mViewPositionOffset As Vector3,
                                                      ByRef mViewRotationOffset As Vector3,
                                                      ByRef mPosition As Vector3,
                                                      ByRef mOrientation As Quaternion,
                                                      ByRef mOrientationVelocity As Vector3)

            Dim mRotationOffsetDeg = mViewRotationOffset
            While (mRotationOffsetDeg.X < 0.0F)
                mRotationOffsetDeg.X += 360.0F
            End While

            While (mRotationOffsetDeg.Y < 0.0F)
                mRotationOffsetDeg.Y += 360.0F
            End While

            While (mRotationOffsetDeg.Z < 0.0F)
                mRotationOffsetDeg.Z += 360.0F
            End While

            Dim mRotationOffsetRad = New Vector3(
                CSng(mViewRotationOffset.X * (Math.PI / 180.0F)),
                CSng(mViewRotationOffset.Y * (Math.PI / 180.0F)),
                CSng(mViewRotationOffset.Z * (Math.PI / 180.0F)))

            ' Adjut orientation
            Dim mOffsetQuat = Quaternion.CreateFromYawPitchRoll(mRotationOffsetRad.Z, mRotationOffsetRad.Y, mRotationOffsetRad.X)
            mOrientation = mOrientation * mOffsetQuat

            ' Adjust orientation velocity
            mOrientationVelocity = ClassMathUtils.RotateVector(Quaternion.Conjugate(mOffsetQuat), mOrientationVelocity)

            ' Adjust position
            Dim mOffsetForward = ClassMathUtils.RotateVector(mOrientation, -mViewPositionOffset)
            mPosition += mOffsetForward

        End Sub

        Private Sub InternalApplyPlayspaceCalibrationLogic(ByRef mPlayspaceSettings As UCVirtualMotionTracker.ClassSettings.STRUC_PLAYSPACE_SETTINGS,
                                                           ByRef mPosition As Vector3,
                                                           ByRef mOrientation As Quaternion,
                                                           ByRef mPositionVelocity As Vector3,
                                                           ByRef mOrientationVelocity As Vector3,
                                                           ByRef bEnableAutoScale As Boolean)
            ' Dont use offsets while we calibrate.
            If (m_PlayspaceCalibration.GetStatus = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING OrElse
                m_PlayspaceCalibration.GetStatus = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.PSMOVE_RUNNING) Then
                Return
            End If

            If (mPlayspaceSettings.m_Valid) Then
                Dim mScale = Vector3.One

                Dim mPointHmdBeginPosXZ = mPlayspaceSettings.m_PointHmdBeginPos
                Dim mPointHmdEndPosXZ = mPlayspaceSettings.m_PointHmdEndPos
                Dim mPointControllerBeginPosXZ = mPlayspaceSettings.m_PointControllerBeginPos
                Dim mPointControllerEndPosXZ = mPlayspaceSettings.m_PointControllerEndPos

                mPointHmdBeginPosXZ.Y = 0.0
                mPointHmdEndPosXZ.Y = 0.0
                mPointControllerBeginPosXZ.Y = 0.0
                mPointControllerEndPosXZ.Y = 0.0

                ' Auto scale playspace by points distance difference
                If (bEnableAutoScale AndAlso mPlayspaceSettings.m_AutoScale) Then
                    Dim iScale As Single = 1.0F

                    Dim iPointHmdDistance As Single = Math.Abs(Vector3.Distance(mPointHmdBeginPosXZ, mPointHmdEndPosXZ))
                    Dim iPointControllerDistance As Single = Math.Abs(Vector3.Distance(mPointControllerBeginPosXZ, mPointControllerEndPosXZ))
                    If (iPointHmdDistance > Single.Epsilon AndAlso iPointControllerDistance > Single.Epsilon) Then
                        iScale = (iPointHmdDistance / iPointControllerDistance)

                        If (iScale > 2.0F) Then
                            iScale = 2.0F
                        End If

                        If (iScale < 0.1F) Then
                            iScale = 0.1F
                        End If
                    End If

                    mScale = New Vector3(iScale, iScale, iScale)
                End If

                Dim mCalibrationForward As Quaternion
                Dim mForward As Vector3
                Dim mSideways As Vector3
                If (mPlayspaceSettings.m_ForwardMethod = UCVirtualMotionTracker.ClassSettings.STRUC_PLAYSPACE_SETTINGS.ENUM_FORWARD_METHOD.USE_HMD_FORWARD) Then
                    mCalibrationForward = ClassMathUtils.ExtractYawQuaternion(mPlayspaceSettings.m_HmdAngOffset, -Vector3.UnitZ)
                    mForward = Vector3.UnitZ * mPlayspaceSettings.m_ForwardOffset
                    mSideways = Vector3.UnitX * mPlayspaceSettings.m_SideOffset
                Else
                    mCalibrationForward = ClassMathUtils.LookRotation(
                        mPointHmdEndPosXZ - mPointHmdBeginPosXZ, Vector3.UnitY)
                    mForward = Vector3.UnitY * mPlayspaceSettings.m_ForwardOffset
                    mSideways = Vector3.UnitX * mPlayspaceSettings.m_SideOffset
                End If

                Dim mOffsetForward = ClassMathUtils.RotateVector(mCalibrationForward, mForward)
                Dim mOffsetSideways = ClassMathUtils.RotateVector(mCalibrationForward, mSideways)

                Dim mPlayspaceCalibPointsRotated = ClassMathUtils.RotateVector(
                    Quaternion.Conjugate(mPlayspaceSettings.m_AngOffset), mPlayspaceSettings.m_PointControllerBeginPos * mScale)

                mPlayspaceCalibPointsRotated = (mPlayspaceSettings.m_PointHmdBeginPos + mOffsetForward + mOffsetSideways) - mPlayspaceCalibPointsRotated

                Dim mPlayspaceRotated = ClassMathUtils.RotateVector(
                    Quaternion.Conjugate(mPlayspaceSettings.m_AngOffset), mPosition * mScale)
                Dim mPlayspaceRotatedVelocity = ClassMathUtils.RotateVector(
                    Quaternion.Conjugate(mPlayspaceSettings.m_AngOffset), mPositionVelocity)

                Dim mOffsetHeight = New Vector3(0.0F, mPlayspaceSettings.m_HeightOffset, 0.0F)

                mPosition = mPlayspaceRotated + mPlayspaceCalibPointsRotated + mOffsetHeight
                mPositionVelocity = mPlayspaceRotatedVelocity
                mOrientation = Quaternion.Conjugate(mPlayspaceSettings.m_AngOffset) * mOrientation
            End If
        End Sub

        Class STRUC_PLAYSPACE_RECENTER_DATA
            Public mPlayspaceRecenterButtonPressed As Boolean
            Public mPlayspaceRecenterButtonHolding As Boolean
            Public mLastPlayspaceRecenterTime As Stopwatch
            Public mPlayspaceRecenterLastHmdSerial As String
            Public mPlayspaceRecenterCalibrationSave As Boolean

            Public Sub New()
                mPlayspaceRecenterButtonPressed = False
                mPlayspaceRecenterButtonHolding = False
                mLastPlayspaceRecenterTime = New Stopwatch
                mPlayspaceRecenterLastHmdSerial = ""
                mPlayspaceRecenterCalibrationSave = False
            End Sub
        End Class

        Private Sub InternalPlayspaceRecenterLogic(ByRef bEnabledPlayspaceRecenter As Boolean,
                                                  ByRef bHoldingRecenterButtons As Boolean,
                                                  ByRef mRawPosition As Vector3,
                                                  ByRef iRecenterButtonTimeMs As Long,
                                                  ByRef mClassControllerSettings As UCVirtualMotionTracker.ClassSettings,
                                                  ByRef mUCVirtualMotionTracker As UCVirtualMotionTracker,
                                                  ByRef mData As STRUC_PLAYSPACE_RECENTER_DATA)
            If ((bEnabledPlayspaceRecenter AndAlso bHoldingRecenterButtons) OrElse
                    m_PlayspaceCalibration.GetStatus = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING) Then
                If (Not mData.mPlayspaceRecenterButtonPressed) Then
                    mData.mPlayspaceRecenterButtonPressed = True
                    mData.mPlayspaceRecenterButtonHolding = False

                    mData.mLastPlayspaceRecenterTime.Restart()
                End If

                If (mData.mLastPlayspaceRecenterTime.ElapsedMilliseconds > iRecenterButtonTimeMs OrElse
                        m_PlayspaceCalibration.GetStatus = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING) Then
                    If (Not mData.mPlayspaceRecenterButtonHolding) Then
                        mData.mPlayspaceRecenterButtonHolding = True

                        ' If caibration is not triggered by manual calibration. Change to calibration via psmove.
                        If (m_PlayspaceCalibration.GetStatus() <> STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING) Then
                            m_PlayspaceCalibrationState.m_Status = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.PSMOVE_RUNNING
                        End If

                        mClassControllerSettings.m_PlayspaceSettings.Reset()
                        mData.mPlayspaceRecenterLastHmdSerial = ""

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
                            mData.mPlayspaceRecenterLastHmdSerial = sCurrentRecenterDeviceName
                        End If
                    End If

                    ' Do not allow playspace recenter on itself
                    If (Not String.IsNullOrEmpty(mData.mPlayspaceRecenterLastHmdSerial) AndAlso Not mData.mPlayspaceRecenterLastHmdSerial.StartsWith(ClassVmtConst.VMT_DEVICE_NAME)) Then
                        Dim mFoundDevice As UCVirtualMotionTracker.ClassOscDevices.STRUC_DEVICE = Nothing
                        If (mUCVirtualMotionTracker.g_ClassOscDevices.GetDeviceBySerial(mData.mPlayspaceRecenterLastHmdSerial, mFoundDevice)) Then
                            Dim mControllerPosBegin As Vector3 = mClassControllerSettings.m_PlayspaceSettings.m_PointControllerBeginPos
                            Dim mControllerPosEnd As Vector3 = mRawPosition ' Dont use calibrated position. It can cause bad offsets.
                            Dim mFromDevicePosBegin As Vector3 = mClassControllerSettings.m_PlayspaceSettings.m_PointHmdBeginPos
                            Dim mFromDevicePosEnd As Vector3 = mFoundDevice.GetPosCm()
                            Dim mFromDeviceOrientation As Quaternion = mFoundDevice.mOrientation

                            Dim bAllowFinishCalibration As Boolean = True
                            Dim iMinDistance As Single = 10.0F

                            If (m_PlayspaceCalibration.GetStatus = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING) Then
                                bAllowFinishCalibration = m_PlayspaceCalibration.m_FinishCalibration
                                iMinDistance = m_PlayspaceCalibration.m_MinDistance
                            End If

                            Dim mControllerPosBeginXZ = mControllerPosBegin
                            Dim mControllerPosEndXZ = mControllerPosEnd
                            Dim mFromDevicePosBeginXZ = mFromDevicePosBegin
                            Dim mFromDevicePosEndXZ = mFromDevicePosEnd

                            mControllerPosBeginXZ.Y = 0.0F
                            mControllerPosEndXZ.Y = 0.0F
                            mFromDevicePosBeginXZ.Y = 0.0F
                            mFromDevicePosEndXZ.Y = 0.0F

                            If (bAllowFinishCalibration AndAlso
                                    Math.Abs(Vector3.Distance(mControllerPosBeginXZ, mControllerPosEndXZ)) > iMinDistance AndAlso
                                    Math.Abs(Vector3.Distance(mFromDevicePosBeginXZ, mFromDevicePosEndXZ)) > iMinDistance) Then

                                Dim mRelControllerVec = ClassMathUtils.LookRotation(mControllerPosEndXZ - mControllerPosBeginXZ, Vector3.UnitY)
                                Dim mRelDeviceVec = ClassMathUtils.LookRotation(mFromDevicePosEndXZ - mFromDevicePosBeginXZ, Vector3.UnitY)
                                Dim mVecDiff = Quaternion.Conjugate(mRelDeviceVec) * mRelControllerVec

                                mClassControllerSettings.m_PlayspaceSettings.m_PosOffset = mFromDevicePosEnd - mControllerPosEnd
                                mClassControllerSettings.m_PlayspaceSettings.m_AngOffset = mVecDiff
                                mClassControllerSettings.m_PlayspaceSettings.m_HmdAngOffset = mFromDeviceOrientation

                                mClassControllerSettings.m_PlayspaceSettings.m_PointControllerEndPos = mControllerPosEnd
                                mClassControllerSettings.m_PlayspaceSettings.m_PointHmdEndPos = mFromDevicePosEnd
                                mClassControllerSettings.m_PlayspaceSettings.m_Valid = True

                                'Save settings after calibration is done 
                                mData.mPlayspaceRecenterCalibrationSave = True

                                ' Stop when manual calibration when done. PSmove calibration should continue when the button is stopped pressed.
                                If (m_PlayspaceCalibration.GetStatus = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.MANUAL_RUNNING) Then
                                    m_PlayspaceCalibrationState.m_Status = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.DONE
                                End If
                            End If
                        Else
                            m_PlayspaceCalibrationState.m_Status = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.FAILED
                        End If
                    Else
                        m_PlayspaceCalibrationState.m_Status = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.FAILED
                    End If
                End If
            Else
                If (mData.mPlayspaceRecenterButtonPressed) Then
                    mData.mPlayspaceRecenterButtonPressed = False
                End If
                If (mData.mPlayspaceRecenterButtonHolding) Then
                    mData.mPlayspaceRecenterButtonHolding = False
                End If
                If (m_PlayspaceCalibration.GetStatus = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.PSMOVE_RUNNING) Then
                    m_PlayspaceCalibrationState.m_Status = STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.INACTIVE
                End If
                If (mData.mPlayspaceRecenterCalibrationSave) Then
                    mData.mPlayspaceRecenterCalibrationSave = False

                    'Save settings after calibration is done
                    mClassControllerSettings.SaveSettings(UCVirtualMotionTracker.ENUM_SETTINGS_SAVE_TYPE_FLAGS.PLAYSPACE_CALIBRATION)
                End If
            End If
        End Sub

        Class STRUC_RECENTER_CONTROLLER_DATA
            Public mRecenterButtonPressed As Boolean
            Public mLastRecenterTime As Stopwatch

            Public Sub New()
                mRecenterButtonPressed = False
                mLastRecenterTime = New Stopwatch
            End Sub
        End Class

        Private Sub InternalRecenterControllerLogic(ByRef bEnableControllerRecenter As Boolean,
                                                    ByRef bHoldingRecenterButtons As Boolean,
                                                    ByRef iRecenterButtonTimeMs As Long,
                                                    ByRef iRecenterMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD,
                                                    ByRef sRecenterFromDeviceName As String,
                                                    ByRef mRecenterQuat As Quaternion,
                                                    ByRef mCalibratedPosition As Vector3,
                                                    ByRef mCalibratedOrientation As Quaternion,
                                                    ByRef mUCVirtualMotionTracker As UCVirtualMotionTracker,
                                                    ByRef mClassControllerSettings As UCVirtualMotionTracker.ClassSettings,
                                                    ByRef mData As STRUC_RECENTER_CONTROLLER_DATA)
            If ((bEnableControllerRecenter AndAlso bHoldingRecenterButtons) OrElse g_bManualControllerRecenter) Then
                If (Not mData.mRecenterButtonPressed) Then
                    mData.mRecenterButtonPressed = True

                    mData.mLastRecenterTime.Restart()
                End If

                If (g_bManualControllerRecenter OrElse mData.mLastRecenterTime.ElapsedMilliseconds > iRecenterButtonTimeMs) Then
                    mData.mLastRecenterTime.Stop()
                    mData.mLastRecenterTime.Reset()

                    g_bManualControllerRecenter = False

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

                                ' Make sure the distance is big enough to get the angle.
                                If (Math.Abs(Vector3.Distance(mControllerPos, mFromDevicePos)) > 1.0F) Then
                                    Dim mQuatDirection = ClassMathUtils.FromVectorToVector(mFromDevicePos, mControllerPos)
                                    Dim mControllerYaw = ClassMathUtils.ExtractYawQuaternion(mCalibratedOrientation, New Vector3(0, 0, -1))

                                    mRecenterQuat = mQuatDirection * Quaternion.Conjugate(mControllerYaw)

                                    mClassControllerSettings.m_ControllerSettings.m_ControllerRecenter(g_iIndex) = mRecenterQuat
                                    mClassControllerSettings.SaveSettings(UCVirtualMotionTracker.ENUM_SETTINGS_SAVE_TYPE_FLAGS.DEVICE_RECENTER)

                                    bDoFactoryRecenter = False
                                End If
                            End If
                    End Select

                    If (bDoFactoryRecenter) Then
                        Dim mControllerYaw = ClassMathUtils.ExtractYawQuaternion(mCalibratedOrientation, New Vector3(0, 0, -1))

                        mRecenterQuat = Quaternion.Conjugate(mControllerYaw) * ClassMathUtils.LookRotation(Vector3.UnitX, Vector3.UnitY)

                        mClassControllerSettings.m_ControllerSettings.m_ControllerRecenter(g_iIndex) = mRecenterQuat
                        mClassControllerSettings.SaveSettings(UCVirtualMotionTracker.ENUM_SETTINGS_SAVE_TYPE_FLAGS.DEVICE_RECENTER)
                    End If
                End If
            Else
                If (mData.mRecenterButtonPressed) Then
                    mData.mRecenterButtonPressed = False
                End If
            End If
        End Sub

        Class STRUC_RECENTER_HMD_DATA
            Public mHmdRecenterButtonPressed As Boolean
            Public mLastHmdRecenterTime As Stopwatch

            Public Sub New()
                mHmdRecenterButtonPressed = False
                mLastHmdRecenterTime = New Stopwatch
            End Sub
        End Class

        Private Sub InternalRecenterHmd(ByRef bEnableHmdRecenter As Boolean,
                                        ByRef mServiceClient As ClassServiceClient,
                                        ByRef iRecenterButtonTimeMs As Long,
                                        ByRef iHmdRecenterMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD,
                                        ByRef sHmdRecenterFromDeviceName As String,
                                        ByRef mCalibratedPosition As Vector3,
                                        ByRef mCalibratedOrientation As Quaternion,
                                        ByRef mRecenterQuat As Quaternion,
                                        ByRef mUCVirtualMotionTracker As UCVirtualMotionTracker,
                                        ByRef bIsHmd As Boolean,
                                        ByRef mClassControllerSettings As UCVirtualMotionTracker.ClassSettings,
                                        ByRef mData As STRUC_RECENTER_HMD_DATA)
            Dim bOtherControllerRecenterButtonPressed As Boolean = False
            Dim bOtherControllerPos As Vector3 = Vector3.Zero

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

            If ((bEnableHmdRecenter AndAlso bOtherControllerRecenterButtonPressed) OrElse g_bManualHmdRecenter) Then
                If (Not mData.mHmdRecenterButtonPressed) Then
                    mData.mHmdRecenterButtonPressed = True

                    mData.mLastHmdRecenterTime.Restart()
                End If

                If (g_bManualHmdRecenter OrElse mData.mLastHmdRecenterTime.ElapsedMilliseconds > iRecenterButtonTimeMs) Then
                    mData.mLastHmdRecenterTime.Stop()
                    mData.mLastHmdRecenterTime.Reset()

                    g_bManualHmdRecenter = False

                    Dim bDoFactoryRecenter As Boolean = True

                    Select Case (iHmdRecenterMethod)
                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD.USE_DEVICE
                            ' Make sure the controller is valid for this operation.
                            If (bOtherControllerRecenterButtonPressed) Then
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

                                    ' Make sure the distance is big enough to get the angle.
                                    If (Math.Abs(Vector3.Distance(mControllerPos, mFromDevicePos)) > 1.0F) Then
                                        Dim mQuatDirection = ClassMathUtils.FromVectorToVector(mFromDevicePos, mControllerPos)
                                        Dim mControllerYaw = ClassMathUtils.ExtractYawQuaternion(mCalibratedOrientation, New Vector3(0, 0, -1))

                                        mRecenterQuat = Quaternion.Conjugate(mControllerYaw) * mQuatDirection

                                        If (Not bIsHmd) Then
                                            mClassControllerSettings.m_ControllerSettings.m_ControllerRecenter(g_iIndex) = mRecenterQuat
                                            mClassControllerSettings.SaveSettings(UCVirtualMotionTracker.ENUM_SETTINGS_SAVE_TYPE_FLAGS.DEVICE_RECENTER)
                                        End If

                                        bDoFactoryRecenter = False
                                    End If
                                Else
                                    bDoFactoryRecenter = False
                                End If
                            End If
                    End Select

                    If (bDoFactoryRecenter) Then
                        Dim mControllerYaw = ClassMathUtils.ExtractYawQuaternion(mCalibratedOrientation, New Vector3(0, 0, -1))

                        mRecenterQuat = Quaternion.Conjugate(mControllerYaw) * ClassMathUtils.LookRotation(Vector3.UnitX, Vector3.UnitY)

                        If (Not bIsHmd) Then
                            mClassControllerSettings.m_ControllerSettings.m_ControllerRecenter(g_iIndex) = mRecenterQuat
                            mClassControllerSettings.SaveSettings(UCVirtualMotionTracker.ENUM_SETTINGS_SAVE_TYPE_FLAGS.DEVICE_RECENTER)
                        End If
                    End If
                End If
            Else
                If (mData.mHmdRecenterButtonPressed) Then
                    mData.mHmdRecenterButtonPressed = False
                End If
            End If
        End Sub

        Class STRUC_JOYSTICK_EMULATION_DATA
            Public bJoystickButtonPressed As Boolean
            Public mJoystickPressedLastOrientation As Quaternion
            Public mJoystickPostion As Vector3
            Public mJoystickPressedLastPosition As Vector3
            Public mJoystickShortcuts As Dictionary(Of Integer, Vector2)

            Public Sub New()
                bJoystickButtonPressed = False
                mJoystickPressedLastOrientation = Quaternion.Identity
                mJoystickPostion = Vector3.Zero
                mJoystickPressedLastPosition = Vector3.Zero
                mJoystickShortcuts = New Dictionary(Of Integer, Vector2)
            End Sub
        End Class

        Private Sub InternalJoystickEmulationLogic(ByRef mOscDataPack As STRUC_OSC_DATA_PACK,
                                                   ByRef bJoystickTrigger As Boolean,
                                                   ByRef iControllerJoystickMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_CONTROLLER_JOYSTICK_METHOD,
                                                   ByRef mRecenterQuat As Quaternion,
                                                   ByRef mPSMoveData As ClassServiceClient.STRUC_PSMOVE_CONTROLLER_DATA,
                                                   ByRef iControllerJoystickAreaCm As Single,
                                                   ByRef iTouchpadClickDeadzone As Single,
                                                   ByRef iHtcTouchpadEmulationClickMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD,
                                                   ByRef bHtcTouchpadShortcutBinding As Boolean,
                                                   ByRef mButtons As Boolean(),
                                                   ByRef bHtcTouchpadShortcutTouchpadClick As Boolean,
                                                   ByRef mData As STRUC_JOYSTICK_EMULATION_DATA)

            If (bJoystickTrigger) Then
                'This should not be possible. Just in case
                If (iControllerJoystickAreaCm < 0.1) Then
                    iControllerJoystickAreaCm = 0.1
                End If

                If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER OrElse
                        m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER) Then
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_TOUCH) = True
                End If

                If (True) Then
                    Dim mCurrentOrientation = mRecenterQuat * mPSMoveData.m_Orientation
                    Dim mControllerYaw = ClassMathUtils.ExtractYawQuaternion(mCurrentOrientation, New Vector3(0, 0, -1))
                    Dim mCurrentOrientationRelative = Quaternion.Conjugate(mControllerYaw) * mCurrentOrientation

                    If (iControllerJoystickMethod = UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_CONTROLLER_JOYSTICK_METHOD.USE_ORIENTATION) Then

                        If (Not mData.bJoystickButtonPressed) Then
                            mData.bJoystickButtonPressed = True

                            mData.mJoystickPostion = Vector3.Zero

                            mData.mJoystickPressedLastOrientation = mCurrentOrientationRelative
                            mData.mJoystickPressedLastPosition = Vector3.Zero
                        End If

                        Dim mNewJoystickPosition = ClassMathUtils.RotateVector(
                            Quaternion.Conjugate(Quaternion.Conjugate(mData.mJoystickPressedLastOrientation) * mCurrentOrientationRelative),
                            New Vector3(0, -1, 0))

                        mData.mJoystickPostion = mData.mJoystickPostion - (mNewJoystickPosition / (iControllerJoystickAreaCm / TOUCHPAD_GYRO_MULTI))

                        mData.mJoystickPressedLastOrientation = mCurrentOrientationRelative
                        mData.mJoystickPressedLastPosition = mNewJoystickPosition

                    Else
                        If (Not mData.bJoystickButtonPressed) Then
                            mData.bJoystickButtonPressed = True

                            mData.mJoystickPostion = Vector3.Zero

                            mData.mJoystickPressedLastOrientation = mCurrentOrientation
                            mData.mJoystickPressedLastPosition = mPSMoveData.m_Position
                        End If

                        Dim mNewJoystickPosition = ClassMathUtils.RotateVector(
                            Quaternion.Conjugate(mData.mJoystickPressedLastOrientation),
                            mPSMoveData.m_Position - mData.mJoystickPressedLastPosition)

                        mData.mJoystickPostion = mData.mJoystickPostion - (mNewJoystickPosition / iControllerJoystickAreaCm)

                        mData.mJoystickPressedLastOrientation = mCurrentOrientation
                        mData.mJoystickPressedLastPosition = mPSMoveData.m_Position
                    End If
                End If

                mData.mJoystickPostion.X = Math.Min(Math.Max(mData.mJoystickPostion.X, -1.0F), 1.0F)
                mData.mJoystickPostion.Z = Math.Min(Math.Max(mData.mJoystickPostion.Z, -1.0F), 1.0F)

                Dim mJoystickVec = New Vector2(
                    -mData.mJoystickPostion.X,
                    mData.mJoystickPostion.Z
                )

                mOscDataPack.mJoyStick = mJoystickVec

                If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER OrElse
                        m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER) Then
                    ' Only start pressing when we moved a distance
                    If (Math.Abs(mJoystickVec.X) >= iTouchpadClickDeadzone OrElse Math.Abs(mJoystickVec.Y) >= iTouchpadClickDeadzone) Then
                        Select Case (iHtcTouchpadEmulationClickMethod)
                            Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.MOVE_ALWAYS
                                mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True

                            Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED
                                If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER) Then
                                    If (mPSMoveData.m_TriangleButton) Then
                                        mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                    End If
                                Else
                                    If (mPSMoveData.m_SquareButton) Then
                                        mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                    End If
                                End If

                            Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_SQUARE
                                If (mPSMoveData.m_SquareButton) Then
                                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                End If

                            Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_TRIANGLE
                                If (mPSMoveData.m_TriangleButton) Then
                                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                End If
                        End Select
                    End If

                    If (bHtcTouchpadShortcutBinding AndAlso mPSMoveData.m_MoveButton) Then
                        ' Record joystick shortcut while MOVE button is pressed 
                        ' Also skip MOVE button
                        For i = 1 To mButtons.Length - 1
                            If (mButtons(i)) Then
                                If (Math.Abs(mJoystickVec.X) < 0.5F AndAlso Math.Abs(mJoystickVec.Y) < 0.5F) Then
                                    ' Remove shortcut
                                    If (mData.mJoystickShortcuts.ContainsKey(i)) Then
                                        mData.mJoystickShortcuts.Remove(i)
                                    End If
                                Else
                                    ' Create shortcut
                                    mData.mJoystickShortcuts(i) = mOscDataPack.mJoyStick
                                End If
                            End If
                        Next
                    End If
                End If
            Else
                If (mData.bJoystickButtonPressed) Then
                    mData.bJoystickButtonPressed = False
                End If

                mOscDataPack.mJoyStick = Vector2.Zero

                If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER OrElse
                        m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER) Then
                    If (bHtcTouchpadShortcutBinding) Then
                        ' Record joystick shortcut while MOVE button is pressed
                        For i = 1 To mButtons.Length - 1
                            If (mButtons(i)) Then
                                If (mData.mJoystickShortcuts.ContainsKey(i)) Then
                                    mOscDataPack.mJoyStick = mData.mJoystickShortcuts(i)

                                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_TOUCH) = True

                                    If (bHtcTouchpadShortcutTouchpadClick) Then
                                        mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = True
                                    End If

                                    ' Never press the shortcut button, just in case its mapped
                                    ' mOscDataPack.mButtons(i) = False
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End Sub

        Class STRUC_BUTTONS_DATA
            Public bGripButtonPressed As Boolean
            Public bGripToggled As Boolean
            Public mGripPressTime As Stopwatch

            Public Sub New()
                bGripButtonPressed = False
                bGripToggled = False
                mGripPressTime = New Stopwatch
            End Sub
        End Class

        Private Sub InternalButtonsLogic(ByRef mOscDataPack As STRUC_OSC_DATA_PACK,
                                         ByRef mButtons As Boolean(),
                                         ByRef mPSMoveData As ClassServiceClient.STRUC_PSMOVE_CONTROLLER_DATA,
                                         ByRef bJoystickTrigger As Boolean,
                                         ByRef iHtcGripButtonMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD,
                                         ByRef iHtcTouchpadEmulationClickMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD,
                                         ByRef bOculusGripToggle As Boolean,
                                         ByRef iOculusButtonMethod As UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_OCULUS_BUTTON_METHOD,
                                         ByRef bHybridGripToggle As Boolean,
                                         ByRef mData As STRUC_BUTTONS_DATA)
            Select Case (m_VmtTrackerRole)
                Case ENUM_TRACKER_ROLE.GENERIC_LEFT_CONTROLLER, ENUM_TRACKER_ROLE.GENERIC_RIGHT_CONTROLLER
                    For i = 0 To mButtons.Length - 1
                        mOscDataPack.mButtons(i) = mButtons(i)
                    Next

                Case ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER, ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_SYSTEM_CLICK) = mPSMoveData.m_StartButton
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRIGGER_CLICK) = ((mPSMoveData.m_TriggerValue / 255.0F) > 0.75F)
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_TOUCH) = bJoystickTrigger
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_TRACKPAD_CLICK) = False
                    mOscDataPack.mButtons(HTC_VIVE_BUTTON_MENU_CLICK) = mPSMoveData.m_PSButton

                    ' Do grip button
                    Dim bGripJustPressed As Boolean = False
                    Dim bIsGripToggle As Boolean = False

                    Select Case (iHtcGripButtonMethod)
                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_HOLDING_MIRRORED
                            If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER) Then
                                bGripJustPressed = mPSMoveData.m_CircleButton
                            Else
                                bGripJustPressed = mPSMoveData.m_CrossButton
                            End If

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_HOLDING_STRICT_CIRCLE
                            bGripJustPressed = mPSMoveData.m_CircleButton

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_HOLDING_STRICT_CROSS
                            bGripJustPressed = mPSMoveData.m_CrossButton

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_MIRRORED
                            bIsGripToggle = True

                            If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER) Then
                                bGripJustPressed = mPSMoveData.m_CircleButton
                            Else
                                bGripJustPressed = mPSMoveData.m_CrossButton
                            End If

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_STRICT_CIRCLE
                            bIsGripToggle = True
                            bGripJustPressed = mPSMoveData.m_CircleButton

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_STRICT_CROSS
                            bIsGripToggle = True
                            bGripJustPressed = mPSMoveData.m_CrossButton

                    End Select

                    Dim bHybridGrip As Boolean = False

                    ' Hybrid grip toggle 
                    If (bHybridGripToggle AndAlso bIsGripToggle) Then
                        If (bGripJustPressed) Then
                            If (Not mData.mGripPressTime.IsRunning) Then
                                mData.mGripPressTime.Restart()
                            End If

                            If (mData.mGripPressTime.ElapsedMilliseconds > 500) Then
                                mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = True

                                bHybridGrip = True
                            End If
                        Else
                            If (mData.mGripPressTime.ElapsedMilliseconds > 500) Then
                                mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = False

                                bHybridGrip = True
                            End If

                            mData.mGripPressTime.Reset()
                        End If
                    Else
                        mData.mGripPressTime.Reset()
                    End If

                    ' Grip toggle
                    If (bHybridGrip) Then
                        mData.bGripToggled = False
                        mData.bGripButtonPressed = False
                    Else
                        If (Not bIsGripToggle) Then
                            mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = bGripJustPressed
                        Else
                            If (bGripJustPressed) Then
                                If (Not mData.bGripButtonPressed) Then
                                    mData.bGripButtonPressed = True

                                    mData.bGripToggled = Not mData.bGripToggled
                                End If
                            Else
                                If (mData.bGripButtonPressed) Then
                                    mData.bGripButtonPressed = False
                                End If
                            End If

                            mOscDataPack.mButtons(HTC_VIVE_BUTTON_GRIP_CLICK) = mData.bGripToggled
                        End If
                    End If

                    ' Do touchpad click
                    Select Case (iHtcTouchpadEmulationClickMethod)
                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED
                            If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER) Then
                                If (mPSMoveData.m_TriangleButton) Then
                                    bJoystickTrigger = True
                                End If
                            Else
                                If (mPSMoveData.m_SquareButton) Then
                                    bJoystickTrigger = True
                                End If
                            End If

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_SQUARE
                            If (mPSMoveData.m_SquareButton) Then
                                bJoystickTrigger = True
                            End If

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_STRICT_TRIANGLE
                            If (mPSMoveData.m_TriangleButton) Then
                                bJoystickTrigger = True
                            End If
                    End Select

                Case ENUM_TRACKER_ROLE.OCULUS_TOUCH_LEFT_CONTROLLER, ENUM_TRACKER_ROLE.OCULUS_TOUCH_RIGHT_CONTROLLER
                    Dim bGripJustPressed As Boolean = False

                    Select Case (iOculusButtonMethod)
                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_OCULUS_BUTTON_METHOD.BUTTON_LEFT_CIRCLE_TRIANGLE_CROSS_SQUARE
                            If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.OCULUS_TOUCH_LEFT_CONTROLLER) Then
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_CLICK) = mPSMoveData.m_CircleButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_TOUCH) = mPSMoveData.m_CircleButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_CLICK) = mPSMoveData.m_TriangleButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_TOUCH) = mPSMoveData.m_TriangleButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_CLICK) = mPSMoveData.m_SquareButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_TOUCH) = mPSMoveData.m_SquareButton
                                bGripJustPressed = mPSMoveData.m_CrossButton
                            Else
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_CLICK) = mPSMoveData.m_CrossButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_TOUCH) = mPSMoveData.m_CrossButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_CLICK) = mPSMoveData.m_SquareButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_TOUCH) = mPSMoveData.m_SquareButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_CLICK) = mPSMoveData.m_TriangleButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_TOUCH) = mPSMoveData.m_TriangleButton
                                bGripJustPressed = mPSMoveData.m_CircleButton
                            End If

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_OCULUS_BUTTON_METHOD.BUTTON_LEFT_CROSS_SQUARE_CIRCLE_TRIANGLE
                            If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.OCULUS_TOUCH_LEFT_CONTROLLER) Then
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_CLICK) = mPSMoveData.m_CrossButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_TOUCH) = mPSMoveData.m_CrossButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_CLICK) = mPSMoveData.m_SquareButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_TOUCH) = mPSMoveData.m_SquareButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_CLICK) = mPSMoveData.m_TriangleButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_TOUCH) = mPSMoveData.m_TriangleButton
                                bGripJustPressed = mPSMoveData.m_CircleButton
                            Else
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_CLICK) = mPSMoveData.m_CircleButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_TOUCH) = mPSMoveData.m_CircleButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_CLICK) = mPSMoveData.m_TriangleButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_TOUCH) = mPSMoveData.m_TriangleButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_CLICK) = mPSMoveData.m_SquareButton
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_TOUCH) = mPSMoveData.m_SquareButton
                                bGripJustPressed = mPSMoveData.m_CrossButton
                            End If

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_OCULUS_BUTTON_METHOD.BUTTON_BOTH_CIRCLE_TRIANGLE_CROSS_SQUARE
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_CLICK) = mPSMoveData.m_CircleButton
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_TOUCH) = mPSMoveData.m_CircleButton
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_CLICK) = mPSMoveData.m_TriangleButton
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_TOUCH) = mPSMoveData.m_TriangleButton
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_CLICK) = mPSMoveData.m_SquareButton
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_TOUCH) = mPSMoveData.m_SquareButton
                            bGripJustPressed = mPSMoveData.m_CrossButton

                        Case UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_OCULUS_BUTTON_METHOD.BUTTON_BOTH_CROSS_SQUARE_CIRCLE_TRIANGLE
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_CLICK) = mPSMoveData.m_CrossButton
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_AX_TOUCH) = mPSMoveData.m_CrossButton
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_CLICK) = mPSMoveData.m_SquareButton
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_BY_TOUCH) = mPSMoveData.m_SquareButton
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_CLICK) = mPSMoveData.m_TriangleButton
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_TOUCH) = mPSMoveData.m_TriangleButton
                            bGripJustPressed = mPSMoveData.m_CircleButton

                    End Select

                    Dim bHybridGrip As Boolean = False

                    ' Hybrid grip toggle 
                    If (bHybridGripToggle AndAlso bOculusGripToggle) Then
                        If (bGripJustPressed) Then
                            If (Not mData.mGripPressTime.IsRunning) Then
                                mData.mGripPressTime.Restart()
                            End If

                            If (mData.mGripPressTime.ElapsedMilliseconds > 500) Then
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_GRIP_CLICK) = True
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_GRIP_TOUCH) = True
                                mOscDataPack.mTrigger(1) = 1.0F

                                bHybridGrip = True
                            End If
                        Else
                            If (mData.mGripPressTime.ElapsedMilliseconds > 500) Then
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_GRIP_CLICK) = False
                                mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_GRIP_TOUCH) = False
                                mOscDataPack.mTrigger(1) = 0.0F

                                bHybridGrip = True
                            End If

                            mData.mGripPressTime.Reset()
                        End If
                    Else
                        mData.mGripPressTime.Reset()
                    End If

                    ' Grip toggle
                    If (bHybridGrip) Then
                        mData.bGripToggled = False
                        mData.bGripButtonPressed = False
                    Else
                        If (Not bOculusGripToggle) Then
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_GRIP_CLICK) = bGripJustPressed
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_GRIP_TOUCH) = bGripJustPressed
                            mOscDataPack.mTrigger(1) = If(bGripJustPressed, 1.0F, 0.0F)
                        Else
                            If (bGripJustPressed) Then
                                If (Not mData.bGripButtonPressed) Then
                                    mData.bGripButtonPressed = True

                                    mData.bGripToggled = Not mData.bGripToggled
                                End If
                            Else
                                If (mData.bGripButtonPressed) Then
                                    mData.bGripButtonPressed = False
                                End If
                            End If

                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_GRIP_CLICK) = mData.bGripToggled
                            mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_GRIP_TOUCH) = mData.bGripToggled
                            mOscDataPack.mTrigger(1) = If(mData.bGripToggled, 1.0F, 0.0F)
                        End If
                    End If

                    mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_JOYSTICK_TOUCH) = mPSMoveData.m_MoveButton

                    ' System click only works on the left controller
                    If (m_VmtTrackerRole = ENUM_TRACKER_ROLE.OCULUS_TOUCH_LEFT_CONTROLLER) Then
                        mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_SYSTEM_CLICK) = mPSMoveData.m_StartButton
                    Else
                        mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_SYSTEM_CLICK) = False
                    End If

                    mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_START_CLICK) = mPSMoveData.m_PSButton
                    mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_TRIGGER_CLICK) = ((mPSMoveData.m_TriggerValue / 255.0F) > 0.75F)
                    mOscDataPack.mButtons(OCULUS_TOUCH_BUTTON_TRIGGER_TOUCH) = ((mPSMoveData.m_TriggerValue / 255.0F) > 0.25F)
            End Select

            mOscDataPack.mTrigger(0) = (mPSMoveData.m_TriggerValue / 255.0F)

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
        Private g_mUCRemoteDeviceItem As UCVirtualMotionTrackerItem
        Private Shared g_mThreadLock As New Object

        Public Sub New(_UCRemoteDeviceItem As UCVirtualMotionTrackerItem)
            g_mUCRemoteDeviceItem = _UCRemoteDeviceItem
        End Sub

        Public Sub SaveConfig()
            SyncLock g_mThreadLock
                If (CInt(g_mUCRemoteDeviceItem.ComboBox_DeviceID.SelectedItem) < 0) Then
                    Return
                End If

                Dim iDeviceID As Integer = CInt(g_mUCRemoteDeviceItem.ComboBox_DeviceID.SelectedItem)

                If (g_mUCRemoteDeviceItem.g_bIsHMD) Then
                    ' For HMDs 
                    Dim sDevicePath As String = CStr(iDeviceID + ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT)

                    Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_VMT, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                        Using mIni As New ClassIni(mStream)
                            Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                            ' Nothing?

                            mIni.WriteKeyValue(mIniContent.ToArray)
                        End Using
                    End Using
                Else
                    ' For Controllers
                    Dim sDevicePath As String = CStr(iDeviceID)

                    Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_VMT, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                        Using mIni As New ClassIni(mStream)
                            Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "VMTTrackerID", CStr(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerID.SelectedIndex)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "VMTTrackerRole", CStr(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerRole.SelectedIndex)))
                            mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "HmdViewPointOffset", CStr(g_mUCRemoteDeviceItem.ComboBox_HmdViewPointOffset.SelectedIndex)))

                            mIni.WriteKeyValue(mIniContent.ToArray)
                        End Using
                    End Using
                End If
            End SyncLock
        End Sub

        Public Sub LoadConfig()
            SyncLock g_mThreadLock
                If (CInt(g_mUCRemoteDeviceItem.ComboBox_DeviceID.SelectedItem) < 0) Then
                    Return
                End If

                Dim iDeviceID As Integer = CInt(g_mUCRemoteDeviceItem.ComboBox_DeviceID.SelectedItem)

                If (g_mUCRemoteDeviceItem.g_bIsHMD) Then
                    ' For HMDs 
                    Dim sDevicePath As String = CStr(iDeviceID + ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT)

                    Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_VMT, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                        Using mIni As New ClassIni(mStream)

                            ' Nothing?

                        End Using
                    End Using
                Else
                    ' For Controllers
                    Dim sDevicePath As String = CStr(iDeviceID)

                    Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_VMT, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                        Using mIni As New ClassIni(mStream)
                            SetComboBoxClamp(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerID, CInt(mIni.ReadKeyValue(sDevicePath, "VMTTrackerID", "0")))
                            SetComboBoxClamp(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerRole, CInt(mIni.ReadKeyValue(sDevicePath, "VMTTrackerRole", "0")))
                            SetComboBoxClamp(g_mUCRemoteDeviceItem.ComboBox_HmdViewPointOffset, CInt(mIni.ReadKeyValue(sDevicePath, "HmdViewPointOffset", "1")))
                        End Using
                    End Using
                End If
            End SyncLock
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

    Private Sub Button_TrackerRecenter_Click(sender As Object, e As EventArgs) Handles Button_TrackerRecenter.Click
        ContextMenuStrip_TrackerRecenter.Show(Button_TrackerRecenter, New Point(0, Button_TrackerRecenter.Height))
    End Sub

    Private Sub ToolStripMenuItem_TrackerRecenterNow_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_TrackerRecenterNow.Click
        Timer_RecenterTimer.Stop()
        Timer_RecenterTimer.Interval = 1
        Timer_RecenterTimer.Start()
    End Sub

    Private Sub ToolStripMenuItem_TrackerRecenterDelayed_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_TrackerRecenterDelayed.Click
        Timer_RecenterTimer.Stop()
        Timer_RecenterTimer.Interval = 5000
        Timer_RecenterTimer.Start()
    End Sub

    Private Sub Timer_RecenterTimer_Tick(sender As Object, e As EventArgs) Handles Timer_RecenterTimer.Tick
        Timer_RecenterTimer.Stop()

        Try
            If (Not g_UCVirtualMotionTracker.g_ClassOscServer.IsRunning) Then
                Throw New ArgumentException("OSC server is not running")
            End If

            If (g_UCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
                Throw New ArgumentException("OSC server is suspended")
            End If

            If (g_mClassIO Is Nothing) Then
                Return
            End If

            If (g_mClassIO.m_IsHMD) Then
                g_mClassIO.StartHmdRecenter()
            Else
                g_mClassIO.StartControllerRecenter()
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ToolStripMenuItem_TrackerRecenterClear_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_TrackerRecenterClear.Click
        Try
            If (Not g_UCVirtualMotionTracker.g_ClassOscServer.IsRunning) Then
                Throw New ArgumentException("OSC server is not running")
            End If

            If (g_UCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
                Throw New ArgumentException("OSC server is suspended")
            End If

            If (g_mClassIO Is Nothing) Then
                Return
            End If

            g_mClassIO.ResetRecenter()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub
End Class
