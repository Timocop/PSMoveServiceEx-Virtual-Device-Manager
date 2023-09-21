Public Class FormMain
    Public g_mUCStartPage As UCStartPage
    Public g_mUCPlaystationVR As UCPlaystationVR
    Public g_mUCVirtualControllers As UCVirtualControllers
    Public g_mUCVirtualHMDs As UCVirtualHMDs
    Public g_mUCVirtualTrackers As UCVirtualTrackers
    Public g_mUCVirtualMotionTracker As UCVirtualMotionTracker

    Public g_mPSMoveServiceCAPI As ClassServiceClient
    Public g_mClassUpdateChecker As ClassUpdateChecker

    Private g_bIgnoreEvents As Boolean = False

    Private g_mMutex As Threading.Mutex
    Private Const MUTEX_NAME As String = "PSMoveServiceEx_VDM_Mutex"

    Public Const COMMANDLINE_PATCH_PSVR_MONITOR As String = "-patch-psvr-monitor"
    Public Const COMMANDLINE_INSTALL_PSVR_DRIVERS As String = "-install-psvr-drivers"
    Public Const COMMANDLINE_INSTALL_PSEYE_DRIVERS As String = "-install-pseye-drivers"
    Public Const COMMANDLINE_VERBOSE As String = "-verbose"

    Enum ENUM_PAGE
        STARTPAGE
        PLAYSTATION_VR
        VIRTUAL_CONTROLLERS
        VIRTUAL_HMDS
        VIRTUAL_TRACKERS
        VIRTUAL_MOTION_TRACKERS
    End Enum

    Public Sub New()

        ProcessCommandline()

        If (Not IsSingleInstance()) Then
            Environment.Exit(0)
            End
        End If

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 

        g_mUCStartPage = New UCStartPage(Me)
        g_mUCStartPage.SuspendLayout()
        g_mUCStartPage.Parent = Panel_Pages
        g_mUCStartPage.Dock = DockStyle.Fill
        g_mUCStartPage.Visible = False
        g_mUCStartPage.ResumeLayout()

        g_mUCPlaystationVR = New UCPlaystationVR(Me)
        g_mUCPlaystationVR.SuspendLayout()
        g_mUCPlaystationVR.Parent = Panel_Pages
        g_mUCPlaystationVR.Dock = DockStyle.Fill
        g_mUCPlaystationVR.Visible = False
        g_mUCPlaystationVR.ResumeLayout()

        g_mUCVirtualControllers = New UCVirtualControllers(Me)
        g_mUCVirtualControllers.SuspendLayout()
        g_mUCVirtualControllers.Parent = Panel_Pages
        g_mUCVirtualControllers.Dock = DockStyle.Fill
        g_mUCVirtualControllers.Visible = False
        g_mUCVirtualControllers.ResumeLayout()

        g_mUCVirtualHMDs = New UCVirtualHMDs()
        g_mUCVirtualHMDs.SuspendLayout()
        g_mUCVirtualHMDs.Parent = Panel_Pages
        g_mUCVirtualHMDs.Dock = DockStyle.Fill
        g_mUCVirtualHMDs.Visible = False
        g_mUCVirtualHMDs.ResumeLayout()

        g_mUCVirtualTrackers = New UCVirtualTrackers()
        g_mUCVirtualTrackers.SuspendLayout()
        g_mUCVirtualTrackers.Parent = Panel_Pages
        g_mUCVirtualTrackers.Dock = DockStyle.Fill
        g_mUCVirtualTrackers.Visible = False
        g_mUCVirtualTrackers.ResumeLayout()

        g_mUCVirtualMotionTracker = New UCVirtualMotionTracker(Me)
        g_mUCVirtualMotionTracker.SuspendLayout()
        g_mUCVirtualMotionTracker.Parent = Panel_Pages
        g_mUCVirtualMotionTracker.Dock = DockStyle.Fill
        g_mUCVirtualMotionTracker.Visible = False
        g_mUCVirtualMotionTracker.ResumeLayout()

        While True
            Try
                If (g_mPSMoveServiceCAPI IsNot Nothing) Then
                    g_mPSMoveServiceCAPI.Dispose()
                    g_mPSMoveServiceCAPI = Nothing
                End If

                g_mPSMoveServiceCAPI = New ClassServiceClient()
                g_mPSMoveServiceCAPI.ServiceStart()
                g_mPSMoveServiceCAPI.StartProcessing()
                Exit While
            Catch ex As Exception
                Dim sMsg As New Text.StringBuilder
                sMsg.AppendLine("Unable to create the PSMoveServiceEx client with the following error:")
                sMsg.AppendLine(ex.Message)
                If (MessageBox.Show(sMsg.ToString, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = DialogResult.Cancel) Then
                    Exit While
                End If
            End Try
        End While

        Label_Version.Text = String.Format("Version: {0}", Application.ProductVersion.ToString)

        g_mClassUpdateChecker = New ClassUpdateChecker(Me)
        g_mClassUpdateChecker.StartUpdateCheck()

        SelectPage(ENUM_PAGE.STARTPAGE)

        AddHandler g_mPSMoveServiceCAPI.OnConnectionStatusChanged, AddressOf OnServiceConnectionStatusChanged
    End Sub

    Private Function IsSingleInstance() As Boolean
        Try
            Threading.Mutex.OpenExisting(MUTEX_NAME)
        Catch ex As Exception
            g_mMutex = New Threading.Mutex(True, MUTEX_NAME)

            Return True
        End Try

        Return False
    End Function

    Private Sub ProcessCommandline()
        Dim sCmdLines As String() = Environment.GetCommandLineArgs

        For Each sCommand As String In sCmdLines
            Select Case (sCommand.ToLowerInvariant)
                Case COMMANDLINE_PATCH_PSVR_MONITOR
                    ' Patch the PSVR monitor registry to allow 120/90/60 Hz refresh rates

                    Try
                        Dim mClassMonitor As New ClassMonitor
                        Dim mDriverInstaller As New ClassLibusbDriver

                        Dim sDisplayNames As String() = New String() {
                            ClassMonitor.PSVR_MONITOR_GEN1_NAME.Replace("MONITOR\", ""),
                            ClassMonitor.PSVR_MONITOR_GEN2_NAME.Replace("MONITOR\", "")
                        }

                        ' Remove conflicting drivers (e.g libusb on hid).
                        If (True) Then
                            Dim bScanNewDevices As Boolean = False
                            For Each sMonitorSerial As String In sDisplayNames
                                For Each mDisplayInfo In mDriverInstaller.GetDeviceProviderDISPLAY(sMonitorSerial)
                                    ' Dont allow anything else than non-system drivers past here!
                                    If (Not String.IsNullOrEmpty(mDisplayInfo.sDriverInfPath) AndAlso mDisplayInfo.sDriverInfPath.ToLowerInvariant.StartsWith("oem")) Then
                                        mDriverInstaller.RemoveDriver(mDisplayInfo.sDriverInfPath)
                                    End If

                                    mDriverInstaller.RemoveDevice(mDisplayInfo.sDeviceID, True)
                                    bScanNewDevices = True
                                Next
                            Next

                            If (bScanNewDevices) Then
                                mDriverInstaller.ScanDevices()
                            End If
                        End If

                        mClassMonitor.PatchPlaystationVrMonitor()

                        ' Restart monitors.
                        If (True) Then
                            Dim bScanNewDevices As Boolean = False
                            For Each sMonitorSerial As String In sDisplayNames
                                For Each mDisplayInfo In mDriverInstaller.GetDeviceProviderDISPLAY(sMonitorSerial)
                                    mDriverInstaller.RestartDevice(mDisplayInfo.sDeviceID)
                                    bScanNewDevices = True
                                Next
                            Next

                            If (bScanNewDevices) Then
                                mDriverInstaller.ScanDevices()
                            End If
                        End If


                    Catch ex As Exception
                        If (sCmdLines.Contains(COMMANDLINE_VERBOSE)) Then
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        Environment.Exit(-1)
                        End
                    End Try

                    Environment.Exit(0)
                    End

                Case COMMANDLINE_INSTALL_PSVR_DRIVERS
                    ' Un/Install PSVR libusb drivers.

                    Try
                        Dim mDriverInstaller As New ClassLibusbDriver

                        ' Remove old drivers.
                        mDriverInstaller.UninstallPlaystationVrDriver64()

                        ' Install drivers
                        If (True) Then
                            Dim iDrvierInstallExitCode = mDriverInstaller.InstallPlaystationVrDrvier64()
                            If (iDrvierInstallExitCode <> ClassLibusbDriver.ENUM_WDI_ERROR.WDI_SUCCESS) Then
                                Throw New ArgumentException(String.Format("Driver installation failed with error: {0} - {1}", CInt(iDrvierInstallExitCode), iDrvierInstallExitCode.ToString))
                            End If
                        End If

                        ' Remove conflicting drivers (e.g libusb on hid).
                        If (True) Then
                            Dim bScanNewDevices As Boolean = False
                            For Each mInfo In mDriverInstaller.DRV_PSVR_HID_CONFIGS
                                For Each mUsbInfo In mDriverInstaller.GetDeviceProviderUSB(mInfo)
                                    If (mUsbInfo.iConfigFlags <> 0) Then
                                        Continue For
                                    End If

                                    If (Not mUsbInfo.HasDriverInstalled()) Then
                                        Continue For
                                    End If

                                    If (mUsbInfo.sService = ClassLibusbDriver.HID_SERVICE_NAME) Then
                                        Continue For
                                    End If

                                    ' Dont allow anything else than non-system drivers past here!
                                    If (String.IsNullOrEmpty(mUsbInfo.sDriverInfPath) OrElse Not mUsbInfo.sDriverInfPath.ToLowerInvariant.StartsWith("oem")) Then
                                        Continue For
                                    End If

                                    mDriverInstaller.RemoveDriver(mUsbInfo.sDriverInfPath)
                                    mDriverInstaller.RemoveDevice(mUsbInfo.sDeviceID, True)
                                    bScanNewDevices = True
                                Next
                            Next

                            If (bScanNewDevices) Then
                                mDriverInstaller.ScanDevices()
                            End If
                        End If

                    Catch ex As Exception
                        If (sCmdLines.Contains(COMMANDLINE_VERBOSE)) Then
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        Environment.Exit(-1)
                        End
                    End Try

                    Environment.Exit(0)
                    End
                Case COMMANDLINE_INSTALL_PSEYE_DRIVERS
                    ' Un/Install PSEye libusb drivers.

                    Try
                        Dim mDriverInstaller As New ClassLibusbDriver

                        ' Remove old drivers.
                        mDriverInstaller.UninstallPlaystationEyeDriver64()

                        ' Install drivers
                        If (True) Then
                            Dim iDrvierInstallExitCode = mDriverInstaller.InstallPlaystationEyeDriver64()
                            If (iDrvierInstallExitCode <> ClassLibusbDriver.ENUM_WDI_ERROR.WDI_SUCCESS) Then
                                Throw New ArgumentException(String.Format("Driver installation failed with error: {0} - {1}", CInt(iDrvierInstallExitCode), iDrvierInstallExitCode.ToString))
                            End If
                        End If

                    Catch ex As Exception
                        If (sCmdLines.Contains(COMMANDLINE_VERBOSE)) Then
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        Environment.Exit(-1)
                        End
                    End Try

                    Environment.Exit(0)
                    End
            End Select
        Next
    End Sub

    Public Sub SelectPage(iPage As ENUM_PAGE)
        Select Case (iPage)
            Case ENUM_PAGE.STARTPAGE
                g_mUCStartPage.Visible = True
                g_mUCPlaystationVR.Visible = False
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False
                g_mUCVirtualMotionTracker.Visible = False

                LinkLabel_StartPage.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Bold)
                LinkLabel_PSVR.Font = New Font(LinkLabel_PSVR.Font, FontStyle.Regular)
                LinkLabel_Controllers.Font = New Font(LinkLabel_Controllers.Font, FontStyle.Regular)
                LinkLabel_HMDs.Font = New Font(LinkLabel_HMDs.Font, FontStyle.Regular)
                LinkLabel_Trackers.Font = New Font(LinkLabel_Trackers.Font, FontStyle.Regular)
                LinkLabel_VMT.Font = New Font(LinkLabel_VMT.Font, FontStyle.Regular)

            Case ENUM_PAGE.PLAYSTATION_VR
                g_mUCStartPage.Visible = False
                g_mUCPlaystationVR.Visible = True
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False
                g_mUCVirtualMotionTracker.Visible = False

                LinkLabel_StartPage.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_PSVR.Font = New Font(LinkLabel_PSVR.Font, FontStyle.Bold)
                LinkLabel_Controllers.Font = New Font(LinkLabel_Controllers.Font, FontStyle.Regular)
                LinkLabel_HMDs.Font = New Font(LinkLabel_HMDs.Font, FontStyle.Regular)
                LinkLabel_Trackers.Font = New Font(LinkLabel_Trackers.Font, FontStyle.Regular)
                LinkLabel_VMT.Font = New Font(LinkLabel_VMT.Font, FontStyle.Regular)

            Case ENUM_PAGE.VIRTUAL_CONTROLLERS
                g_mUCStartPage.Visible = False
                g_mUCPlaystationVR.Visible = False
                g_mUCVirtualControllers.Visible = True
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False
                g_mUCVirtualMotionTracker.Visible = False

                LinkLabel_StartPage.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_PSVR.Font = New Font(LinkLabel_PSVR.Font, FontStyle.Regular)
                LinkLabel_Controllers.Font = New Font(LinkLabel_Controllers.Font, FontStyle.Bold)
                LinkLabel_HMDs.Font = New Font(LinkLabel_HMDs.Font, FontStyle.Regular)
                LinkLabel_Trackers.Font = New Font(LinkLabel_Trackers.Font, FontStyle.Regular)
                LinkLabel_VMT.Font = New Font(LinkLabel_VMT.Font, FontStyle.Regular)

            Case ENUM_PAGE.VIRTUAL_HMDS
                g_mUCStartPage.Visible = False
                g_mUCPlaystationVR.Visible = False
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = True
                g_mUCVirtualTrackers.Visible = False
                g_mUCVirtualMotionTracker.Visible = False

                LinkLabel_StartPage.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_PSVR.Font = New Font(LinkLabel_PSVR.Font, FontStyle.Regular)
                LinkLabel_Controllers.Font = New Font(LinkLabel_Controllers.Font, FontStyle.Regular)
                LinkLabel_HMDs.Font = New Font(LinkLabel_HMDs.Font, FontStyle.Bold)
                LinkLabel_Trackers.Font = New Font(LinkLabel_Trackers.Font, FontStyle.Regular)
                LinkLabel_VMT.Font = New Font(LinkLabel_VMT.Font, FontStyle.Regular)

            Case ENUM_PAGE.VIRTUAL_TRACKERS
                g_mUCStartPage.Visible = False
                g_mUCPlaystationVR.Visible = False
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = True
                g_mUCVirtualMotionTracker.Visible = False

                LinkLabel_StartPage.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_PSVR.Font = New Font(LinkLabel_PSVR.Font, FontStyle.Regular)
                LinkLabel_Controllers.Font = New Font(LinkLabel_Controllers.Font, FontStyle.Regular)
                LinkLabel_HMDs.Font = New Font(LinkLabel_HMDs.Font, FontStyle.Regular)
                LinkLabel_Trackers.Font = New Font(LinkLabel_Trackers.Font, FontStyle.Bold)
                LinkLabel_VMT.Font = New Font(LinkLabel_VMT.Font, FontStyle.Regular)

            Case ENUM_PAGE.VIRTUAL_MOTION_TRACKERS
                g_mUCStartPage.Visible = False
                g_mUCPlaystationVR.Visible = False
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False
                g_mUCVirtualMotionTracker.Visible = True

                LinkLabel_StartPage.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_PSVR.Font = New Font(LinkLabel_PSVR.Font, FontStyle.Regular)
                LinkLabel_Controllers.Font = New Font(LinkLabel_Controllers.Font, FontStyle.Regular)
                LinkLabel_HMDs.Font = New Font(LinkLabel_HMDs.Font, FontStyle.Regular)
                LinkLabel_Trackers.Font = New Font(LinkLabel_Trackers.Font, FontStyle.Regular)
                LinkLabel_VMT.Font = New Font(LinkLabel_VMT.Font, FontStyle.Bold)
        End Select
    End Sub

    Private Sub OnServiceConnectionStatusChanged()
        Try
            ' Lets find PSMS-EX when connection as been established and then save it to config.
            Dim mConfig As New ClassServiceInfo
            mConfig.LoadConfig()

            If (Not mConfig.FileExist) Then
                If (mConfig.FindByProcess()) Then
                    mConfig.SaveConfig()
                End If
            End If
        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetCameraNameById(iIndex As Integer) As String
        Dim sCameras As New List(Of String)
        Dim iCount As Integer = 0

        Dim sClasses = New String() {
            "Image",
            "Camera",
            "KinectSensor"
        }

        For i = 0 To sClasses.Length - 1
            sClasses(i) = String.Format("PNPClass = '{0}'", sClasses(i))
        Next

        Using searcher As New Management.ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE (" & String.Join(" OR ", sClasses) & ")")
            For Each device In searcher.[Get]()
                If (iCount = iIndex) Then
                    Return device("Caption").ToString()
                End If

                iCount += 1
            Next
        End Using

        Return Nothing
    End Function


    Public Sub LinkLabel_StartPage_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_StartPage.LinkClicked
        SelectPage(ENUM_PAGE.STARTPAGE)
    End Sub

    Private Sub LinkLabel_PSVR_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_PSVR.LinkClicked
        SelectPage(ENUM_PAGE.PLAYSTATION_VR)
    End Sub

    Public Sub LinkLabel_Controllers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Controllers.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
    End Sub

    Public Sub LinkLabel_HMDs_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_HMDs.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_HMDS)
    End Sub

    Public Sub LinkLabel_Trackers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Trackers.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_TRACKERS)
    End Sub

    Private Sub LinkLabel_VMT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_VMT.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_MOTION_TRACKERS)
    End Sub

    Public Sub LinkLabel_ControllersGeneral_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ControllersGeneral.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_General
    End Sub

    Public Sub LinkLabel_ControllersRemote_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ControllersRemote.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_RemoteSettings
    End Sub

    Public Sub LinkLabel_ControllersAttachments_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ControllersAttachments.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_ControllerAttachments
    End Sub

    Public Sub LinkLabel_RemoteStartSocket_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_RemoteStartSocket.LinkClicked
        If (g_mUCVirtualControllers.g_mUCRemoteDevices Is Nothing OrElse g_mUCVirtualControllers.g_mUCRemoteDevices.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_RemoteSettings

        g_mUCVirtualControllers.g_mUCRemoteDevices.Button_StartSocket.PerformClick()
        g_mUCVirtualControllers.g_mUCRemoteDevices.CheckBox_AllowNewDevices.Checked = True
    End Sub

    Public Sub LinkLabel_VMTStartOscServer_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_VMTStartOscServer.LinkClicked
        If (g_mUCVirtualMotionTracker Is Nothing OrElse g_mUCVirtualMotionTracker.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.VIRTUAL_MOTION_TRACKERS)
        g_mUCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_mUCVirtualMotionTracker.TabPage_Management

        g_mUCVirtualMotionTracker.LinkLabel_OscRun_Click()
    End Sub

    Public Sub LinkLabel1LinkLabel_VMTPauseOscServer_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1LinkLabel_VMTPauseOscServer.LinkClicked
        If (g_mUCVirtualMotionTracker Is Nothing OrElse g_mUCVirtualMotionTracker.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.VIRTUAL_MOTION_TRACKERS)
        g_mUCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_mUCVirtualMotionTracker.TabPage_Management

        g_mUCVirtualMotionTracker.LinkLabel_OscPause_Click()
    End Sub

    Private Sub LinkLabel_VmtManagement_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_VmtManagement.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_MOTION_TRACKERS)
        g_mUCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_mUCVirtualMotionTracker.TabPage_Management
    End Sub

    Private Sub LinkLabel_VmtTrackers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_VmtTrackers.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_MOTION_TRACKERS)
        g_mUCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_mUCVirtualMotionTracker.TabPage_Trackers
    End Sub

    Private Sub LinkLabel_VmtSettings_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_VmtSettings.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_MOTION_TRACKERS)
        g_mUCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_mUCVirtualMotionTracker.TabPage_Settings
    End Sub

    Private Sub LinkLabel_VmtPlayspaceCalib_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_VmtPlayspaceCalib.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_MOTION_TRACKERS)
        g_mUCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_mUCVirtualMotionTracker.TabPage_PlayspaceCalib
    End Sub

    Private Sub LinkLabel_VmtSteamVrOverrides_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_VmtSteamVrOverrides.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_MOTION_TRACKERS)
        g_mUCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_mUCVirtualMotionTracker.TabPage_Overrides
    End Sub

    Public Sub LinkLabel_RunPSMS_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_RunPSMS.LinkClicked
        If (g_mUCStartPage Is Nothing OrElse g_mUCStartPage.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.STARTPAGE)
        g_mUCStartPage.LinkLabel_ServiceRun_Click()
    End Sub

    Public Sub LinkLabel_StopPSMS_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_StopPSMS.LinkClicked
        If (g_mUCStartPage Is Nothing OrElse g_mUCStartPage.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.STARTPAGE)
        g_mUCStartPage.LinkLabel_ServiceStop_Click()
    End Sub

    Public Sub LinkLabel_RestartPSMS_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_RestartPSMS.LinkClicked
        If (g_mUCStartPage Is Nothing OrElse g_mUCStartPage.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.STARTPAGE)
        g_mUCStartPage.LinkLabel_ServiceRestart_Click()
    End Sub

    Public Sub LinkLabel_RunPSMSTool_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_RunPSMSTool.LinkClicked
        If (g_mUCStartPage Is Nothing OrElse g_mUCStartPage.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.STARTPAGE)
        g_mUCStartPage.LinkLabel_ConfigToolRun_Click()
    End Sub

    Private Sub ToolTip_Service_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip_Service.Popup
        Try
            If (g_bIgnoreEvents) Then
                Return
            End If

            Dim mConfig As New ClassServiceInfo
            mConfig.LoadConfig()

            Try
                g_bIgnoreEvents = True

                If (mConfig.FileExist) Then
                    ToolTip_Service.ToolTipTitle = "Service path:"
                    ToolTip_Service.SetToolTip(e.AssociatedControl, mConfig.m_FileName)
                Else
                    e.Cancel = True
                End If
            Finally
                g_bIgnoreEvents = False

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (e.CloseReason <> CloseReason.UserClosing) Then
            Return
        End If

        Dim mProcesses As New List(Of Process)
        mProcesses.AddRange(Process.GetProcessesByName("PSMoveService"))
        mProcesses.AddRange(Process.GetProcessesByName("PSMoveConfigTool"))


        If (mProcesses.Count > 0) Then
            Dim sMsg As New Text.StringBuilder
            sMsg.AppendLine("PSMoveServiceEx is currently running.")
            sMsg.AppendLine()
            sMsg.AppendLine("Do you want to close PSMoveServiceEx?")
            Select Case (MessageBox.Show(sMsg.ToString, "PSMoveServiceEx is still running", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                Case DialogResult.Cancel
                    e.Cancel = True

                Case DialogResult.Yes
                    If (mProcesses.Count > 0) Then
                        For Each mProcess In mProcesses
                            If (mProcess.CloseMainWindow()) Then
                                mProcess.WaitForExit(10000)
                            Else
                                mProcess.Kill()
                            End If
                        Next
                    End If

                Case DialogResult.No
                    ' Do nothing
            End Select
        End If
    End Sub

    Private Sub FormMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Using mCloseForm As New FormLoading
            mCloseForm.Text = "Closing established connections and cleaning up..."
            mCloseForm.ProgressBar1.Style = ProgressBarStyle.Continuous
            mCloseForm.Show()
            mCloseForm.Refresh()

            CleanUp()
        End Using
    End Sub

    Private Sub LinkLabel_Github_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Github.LinkClicked
        Try
            Process.Start("https://github.com/Timocop/PSMoveServiceEx-Virtual-Device-Manager")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LinkLabel_Updates_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Updates.LinkClicked
        Try
            Process.Start("https://github.com/Timocop/PSMoveServiceEx-Virtual-Device-Manager/releases")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LinkLabel_RunSteamVR_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_RunSteamVR.LinkClicked
        Try
            Process.Start("steam://rungameid/250820")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CleanUp()
        If (g_mPSMoveServiceCAPI IsNot Nothing) Then
            RemoveHandler g_mPSMoveServiceCAPI.OnConnectionStatusChanged, AddressOf OnServiceConnectionStatusChanged
        End If

        If (g_mClassUpdateChecker IsNot Nothing) Then
            g_mClassUpdateChecker.Dispose()
            g_mClassUpdateChecker = Nothing
        End If

        Try
            If (g_mPSMoveServiceCAPI IsNot Nothing) Then
                g_mPSMoveServiceCAPI.Dispose()
                g_mPSMoveServiceCAPI = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If (g_mUCStartPage IsNot Nothing AndAlso Not g_mUCStartPage.IsDisposed) Then
            g_mUCStartPage.Dispose()
            g_mUCStartPage = Nothing
        End If

        If (g_mUCPlaystationVR IsNot Nothing AndAlso Not g_mUCPlaystationVR.IsDisposed) Then
            g_mUCPlaystationVR.Dispose()
            g_mUCPlaystationVR = Nothing
        End If

        If (g_mUCVirtualControllers IsNot Nothing AndAlso Not g_mUCVirtualControllers.IsDisposed) Then
            g_mUCVirtualControllers.Dispose()
            g_mUCVirtualControllers = Nothing
        End If

        If (g_mUCVirtualHMDs IsNot Nothing AndAlso Not g_mUCVirtualHMDs.IsDisposed) Then
            g_mUCVirtualHMDs.Dispose()
            g_mUCVirtualHMDs = Nothing
        End If

        If (g_mUCVirtualTrackers IsNot Nothing AndAlso Not g_mUCVirtualTrackers.IsDisposed) Then
            g_mUCVirtualTrackers.Dispose()
            g_mUCVirtualTrackers = Nothing
        End If

        If (g_mUCVirtualMotionTracker IsNot Nothing AndAlso Not g_mUCVirtualMotionTracker.IsDisposed) Then
            g_mUCVirtualMotionTracker.Dispose()
            g_mUCVirtualMotionTracker = Nothing
        End If

    End Sub

    Class ClassUpdateChecker
        Implements IDisposable

        Private g_mUpdaterThread As Threading.Thread = Nothing

        Private g_mFormMain As FormMain

        Public Sub New(_mFormMain As FormMain)
            g_mFormMain = _mFormMain
        End Sub

        Public Sub StartUpdateCheck()
            If (g_mUpdaterThread IsNot Nothing AndAlso g_mUpdaterThread.IsAlive) Then
                Return
            End If

            g_mUpdaterThread = New Threading.Thread(AddressOf UpdateCheckThread)
            g_mUpdaterThread.IsBackground = True
            g_mUpdaterThread.Start()
        End Sub

        Private Sub UpdateCheckThread()
            Try
                Threading.Thread.Sleep(2500)

                Dim sLocationInfo As String = ""

                If (True) Then
                    If (ClassUpdate.ClassVdm.CheckUpdateAvailable(Application.ExecutablePath, sLocationInfo)) Then
                        ClassUtils.AsyncInvoke(g_mFormMain,
                                               Sub()
                                                   g_mFormMain.LinkLabel_Updates.Text = "New Update Available!"
                                                   g_mFormMain.LinkLabel_Updates.Font = New Font(g_mFormMain.LinkLabel_Updates.Font, FontStyle.Bold)

                                                   g_mFormMain.g_mUCStartPage.Panel_VdmUpdate.Visible = True
                                               End Sub)
                    End If
                End If

                If (True) Then
                    Dim mConfig As New ClassServiceInfo
                    mConfig.LoadConfig()

                    If (mConfig.FileExist) Then
                        If (ClassUpdate.ClassPsms.CheckUpdateAvailable(mConfig.m_FileName, sLocationInfo)) Then
                            ClassUtils.AsyncInvoke(g_mFormMain,
                                Sub()
                                    g_mFormMain.g_mUCStartPage.Panel_PsmsxUpdate.Visible = True
                                End Sub)
                        End If
                    End If
                End If

            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception

            End Try
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    If (g_mUpdaterThread IsNot Nothing AndAlso g_mUpdaterThread.IsAlive) Then
                        g_mUpdaterThread.Abort()
                        g_mUpdaterThread.Join()
                        g_mUpdaterThread = Nothing
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
