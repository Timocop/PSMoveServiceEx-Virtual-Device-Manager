Imports Microsoft.Win32

Public Class FormMain

#Const ALWAYS_SHOW_UPDATE = False

    Public g_mUCStartPage As UCStartPage
    Public g_mUCPlaystationVR As UCPlaystationVR
    Public g_mUCVirtualControllers As UCVirtualControllers
    Public g_mUCVirtualHMDs As UCVirtualHMDs
    Public g_mUCVirtualTrackers As UCVirtualTrackers
    Public g_mUCVirtualMotionTracker As UCVirtualMotionTracker

    Public g_UCRestartPsms As UCRestartProcessWarning
    Public g_UCRestartSteamVR As UCRestartProcessWarning

    Public g_mPSMoveServiceCAPI As ClassServiceClient
    Public g_mClassUpdateChecker As ClassUpdateChecker
    Public g_mClassCameraFirmwareWatchdog As ClassCameraFirmwareWatchdog

    Private g_mAutoCloseThread As Threading.Thread = Nothing

    Private g_bIgnoreEvents As Boolean = False
    Private g_bAutoClose As Boolean = False
    Private g_bAllowRestartPrompt As Boolean = False
    Private g_bInit As Boolean = False

    Private g_mMutex As Threading.Mutex
    Private Const MUTEX_NAME As String = "PSMoveServiceEx_VDM_Mutex"

    Public Const COMMANDLINE_RUNAS_SYSTEM As String = "-runas-system"
    Public Const COMMANDLINE_PATCH_PSVR_MONITOR_MULTI As String = "-patch-psvr-monitor-multi"
    Public Const COMMANDLINE_PATCH_PSVR_MONITOR_DIRECT As String = "-patch-psvr-monitor-direct"
    Public Const COMMANDLINE_PATCH_PSVR_MONITOR_REMOVE As String = "-patch-psvr-monitor-remove"
    Public Const COMMANDLINE_INSTALL_PSVR_DRIVERS As String = "-install-psvr-drivers"
    Public Const COMMANDLINE_INSTALL_PSEYE_DRIVERS As String = "-install-pseye-drivers"
    Public Const COMMANDLINE_INSTALL_PS4CAM_DRIVERS As String = "-install-ps4cam-drivers"
    Public Const COMMANDLINE_UNINSTALL_PSVR As String = "-uninstall-psvr"
    Public Const COMMANDLINE_UNINSTALL_PSEYE As String = "-uninstall-pseye"
    Public Const COMMANDLINE_UNINSTALL_PS4CAM As String = "-uninstall-ps4cam"
    Public Const COMMANDLINE_VERBOSE As String = "-verbose"
    Public Const COMMANDLINE_START_STEAMVR As String = "-steamvr"
    Public Const COMMANDLINE_START_SERVICE As String = "-run-service"
    Public Const COMMANDLINE_START_REMOTEDEVICES As String = "-run-remote-devices"
    Public Const COMMANDLINE_START_OSCSERVER As String = "-run-osc-server"

    Enum ENUM_PAGE
        STARTPAGE
        PLAYSTATION_VR
        VIRTUAL_CONTROLLERS
        VIRTUAL_HMDS
        VIRTUAL_TRACKERS
        VIRTUAL_MOTION_TRACKERS
    End Enum

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
            g_mUpdaterThread.Priority = Threading.ThreadPriority.Lowest
            g_mUpdaterThread.IsBackground = True
            g_mUpdaterThread.Start()
        End Sub

        Private Sub UpdateCheckThread()
            Try
                Threading.Thread.Sleep(2500)

                Dim sLocationInfo As String = ""

                If (True) Then
#If DEBUG AndAlso ALWAYS_SHOW_UPDATE Then
                    ClassUtils.AsyncInvoke(g_mFormMain,
                                            Sub()
                                                g_mFormMain.LinkLabel_Updates.Text = "New Update Available!"
                                                g_mFormMain.LinkLabel_Updates.Font = New Font(g_mFormMain.LinkLabel_Updates.Font, FontStyle.Bold)

                                                g_mFormMain.g_mUCStartPage.Panel_VdmUpdate.Visible = True
                                            End Sub)
#Else
                    If (ClassUpdate.ClassVdm.CheckUpdateAvailable(Application.ExecutablePath, sLocationInfo)) Then
                        ClassUtils.AsyncInvoke(Sub()
                                                   g_mFormMain.Button_NavUpdate.Text = "New Update Available!"
                                                   g_mFormMain.Button_NavUpdate.Font = New Font(g_mFormMain.Button_NavUpdate.Font, FontStyle.Bold)

                                                   g_mFormMain.g_mUCStartPage.Panel_VdmUpdate.Visible = True
                                               End Sub)
                    End If
#End If
                End If

                If (True) Then
                    Dim mConfig As New ClassServiceInfo
                    mConfig.LoadConfig()

                    If (mConfig.FileExist) Then
#If DEBUG AndAlso ALWAYS_SHOW_UPDATE Then
                        ClassUtils.AsyncInvoke(g_mFormMain,
                                                Sub()
                                                    g_mFormMain.g_mUCStartPage.Panel_PsmsxUpdate.Visible = True
                                                End Sub)

                        ClassUtils.AsyncInvoke(g_mFormMain,
                                                 Sub()
                                                     g_mFormMain.g_mUCStartPage.Panel_PsmsxInstall.Visible = True
                                                 End Sub)
#Else
                        If (ClassUpdate.ClassPsms.CheckUpdateAvailable(mConfig.m_FileName, sLocationInfo)) Then
                            ClassUtils.AsyncInvoke(Sub()
                                                       g_mFormMain.g_mUCStartPage.Panel_PsmsxUpdate.Visible = True
                                                   End Sub)
                        End If
#End If
                    Else
                        ClassUtils.AsyncInvoke(Sub()
                                                   g_mFormMain.g_mUCStartPage.Panel_PsmsxInstall.Visible = True
                                               End Sub)
                    End If
                End If

            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLog(ex)
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

    Public Sub New()
        Try
            ProcessCommandline(False)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

        If (Not IsSingleInstance()) Then
            Environment.Exit(0)
            End
        End If

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 

#If DEBUG Then
        Me.Text &= " (DEBUGGING)"
#End If

        Try
            ClassAdvancedExceptionLogging.m_EnableLogging = True
            ClassAdvancedExceptionLogging.m_AutoFlushToFile = True
        Catch ex As Exception
            ' Do nothing
        End Try

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
                ClassAdvancedExceptionLogging.WriteToLog(ex)

                Dim sMsg As New Text.StringBuilder
                sMsg.AppendLine("Unable to create the PSMoveServiceEx client with the following error:")
                sMsg.AppendLine(ex.Message)
                If (MessageBox.Show(sMsg.ToString, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = DialogResult.Cancel) Then
                    Exit While
                End If
            End Try
        End While

        While True
            Try
                If (g_mClassCameraFirmwareWatchdog IsNot Nothing) Then
                    g_mClassCameraFirmwareWatchdog.Dispose()
                    g_mClassCameraFirmwareWatchdog = Nothing
                End If

                g_mClassCameraFirmwareWatchdog = New ClassCameraFirmwareWatchdog
                g_mClassCameraFirmwareWatchdog.Init()
                g_mClassCameraFirmwareWatchdog.UploadFirmware()
                Exit While
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLog(ex)

                Dim sMsg As New Text.StringBuilder
                sMsg.AppendLine("Unable to create camera firmware watchdog with the following error:")
                sMsg.AppendLine(ex.Message)
                If (MessageBox.Show(sMsg.ToString, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = DialogResult.Cancel) Then
                    Exit While
                End If
            End Try
        End While

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

        g_mUCVirtualHMDs = New UCVirtualHMDs(Me)
        g_mUCVirtualHMDs.SuspendLayout()
        g_mUCVirtualHMDs.Parent = Panel_Pages
        g_mUCVirtualHMDs.Dock = DockStyle.Fill
        g_mUCVirtualHMDs.Visible = False
        g_mUCVirtualHMDs.ResumeLayout()

        g_mUCVirtualTrackers = New UCVirtualTrackers(Me)
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

        g_UCRestartPsms = New UCRestartProcessWarning
        g_UCRestartPsms.m_Message = "PSMoveServiceEx needs to be restarted for changes to take effect."
        g_UCRestartPsms.m_ProcessName = "PSMoveService;PSMoveServiceAdmin"

        g_UCRestartPsms.Parent = Panel_Pages
        g_UCRestartPsms.Dock = DockStyle.Bottom
        g_UCRestartPsms.Visible = True
        g_UCRestartPsms.SendToBack()
        g_UCRestartPsms.Visible = False

        g_UCRestartSteamVR = New UCRestartProcessWarning
        g_UCRestartSteamVR.m_Message = "SteamVR needs to be restarted for changes to take effect."
        g_UCRestartSteamVR.m_ProcessName = "vrserver"

        g_UCRestartSteamVR.Parent = Panel_Pages
        g_UCRestartSteamVR.Dock = DockStyle.Bottom
        g_UCRestartSteamVR.Visible = True
        g_UCRestartSteamVR.SendToBack()
        g_UCRestartSteamVR.Visible = False

        Button_NavVersion.Text = String.Format("Version: {0}", Application.ProductVersion.ToString)

        CreateControl()
    End Sub

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Init()
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

        ClassUtils.m_InvokeControl = Me

        AddHandler g_mPSMoveServiceCAPI.OnConnectionStatusChanged, AddressOf OnServiceConnectionStatusChanged

        g_mUCStartPage.Init()
        g_mUCPlaystationVR.Init()
        g_mUCVirtualControllers.Init()
        g_mUCVirtualHMDs.Init()
        g_mUCVirtualTrackers.Init()
        g_mUCVirtualMotionTracker.Init()

        g_mClassUpdateChecker = New ClassUpdateChecker(Me)
        g_mClassUpdateChecker.StartUpdateCheck()

        SelectPage(ENUM_PAGE.STARTPAGE)

        Try
            ProcessCommandline(True)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

        g_bAllowRestartPrompt = True
    End Sub

    Public Sub PromptRestartPSMoveService()
        If (Not g_bAllowRestartPrompt) Then
            Return
        End If

        g_UCRestartPsms.ShowAndWait()
    End Sub

    Public Sub PromptRestartSteamVR()
        If (Not g_bAllowRestartPrompt) Then
            Return
        End If

        g_UCRestartSteamVR.ShowAndWait()
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

    Private Sub ProcessCommandline(bLateload As Boolean)
        Dim sCmdLines As String() = Environment.GetCommandLineArgs

        If (bLateload) Then
            For Each sCommand As String In sCmdLines
                While (sCommand = COMMANDLINE_START_STEAMVR)
                    ' Close with SteamVR
                    Me.Text &= " (STEAMVR)"

                    g_mAutoCloseThread = New Threading.Thread(
                        Sub()
                            Try
                                Threading.Thread.Sleep(5000)

                                Dim mProcesses As Process() = Process.GetProcessesByName("vrserver")
                                If (mProcesses.Count > 0) Then
                                    For Each mProcess As Process In mProcesses
                                        mProcess.WaitForExit()
                                    Next
                                End If

                                ClassUtils.AsyncInvoke(Sub()
                                                           g_bAutoClose = True
                                                           Me.Close()
                                                       End Sub)
                            Catch ex As Exception
                                ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                            End Try
                        End Sub)
                    g_mAutoCloseThread.Priority = Threading.ThreadPriority.Lowest
                    g_mAutoCloseThread.IsBackground = True
                    g_mAutoCloseThread.Start()

                    Exit While
                End While

                While (sCommand = COMMANDLINE_START_SERVICE)
                    If (g_mUCStartPage Is Nothing OrElse g_mUCStartPage.IsDisposed) Then
                        Exit While
                    End If

                    ' Run Service
                    g_mUCStartPage.LinkLabel_ServiceRun_Click()

                    Exit While
                End While

                While (sCommand = COMMANDLINE_START_REMOTEDEVICES)
                    If (g_mUCVirtualControllers.g_mUCRemoteDevices Is Nothing OrElse g_mUCVirtualControllers.g_mUCRemoteDevices.IsDisposed) Then
                        Exit While
                    End If

                    ' Run remote devices and allow new devices
                    g_mUCVirtualControllers.g_mUCRemoteDevices.Button_StartSocket_Click()
                    g_mUCVirtualControllers.g_mUCRemoteDevices.CheckBox_AllowNewDevices.Checked = True

                    Exit While
                End While

                While (sCommand = COMMANDLINE_START_OSCSERVER)
                    If (g_mUCVirtualMotionTracker Is Nothing OrElse g_mUCVirtualMotionTracker.IsDisposed) Then
                        Exit While
                    End If

                    ' Run OSC server
                    g_mUCVirtualMotionTracker.g_UCVmtManagement.LinkLabel_OscRun_Click()

                    Exit While
                End While
            Next
        Else
            Dim bExitOnSuccess As Boolean = False

            For Each sCommand As String In sCmdLines
                While (sCommand = COMMANDLINE_PATCH_PSVR_MONITOR_MULTI OrElse sCommand = COMMANDLINE_PATCH_PSVR_MONITOR_DIRECT OrElse sCommand = COMMANDLINE_PATCH_PSVR_MONITOR_REMOVE OrElse
                        sCommand = COMMANDLINE_INSTALL_PSVR_DRIVERS OrElse sCommand = COMMANDLINE_UNINSTALL_PSVR)
                    ' Patch the PSVR monitor registry to allow 120/90/60 Hz refresh rates
                    bExitOnSuccess = True

                    Try
                        Dim mClassMonitor As New ClassMonitor
                        Dim mDriverInstaller As New ClassLibusbDriver

                        ' Test if we have access to the display registry keys, or do we require SYSTEM priviliges
                        ' This should only trigger on older Windows 10 builds or Windows versions prior to Windows 10.
                        If (sCommand = COMMANDLINE_PATCH_PSVR_MONITOR_MULTI OrElse sCommand = COMMANDLINE_PATCH_PSVR_MONITOR_DIRECT) Then
                            If (Not sCmdLines.Contains(COMMANDLINE_RUNAS_SYSTEM)) Then
                                Dim bRequiresSystem As Boolean = False

                                Try
                                    If (Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Enum\DISPLAY", True) Is Nothing) Then
                                        bRequiresSystem = True
                                    End If
                                Catch ex As Exception
                                    bRequiresSystem = True
                                End Try

                                If (bRequiresSystem) Then
                                    Dim sCmdLinesSystem As New List(Of String)
                                    sCmdLinesSystem.AddRange(sCmdLines)
                                    sCmdLinesSystem.Add(COMMANDLINE_RUNAS_SYSTEM)

                                    ' Remove executablepath
                                    sCmdLinesSystem.RemoveAt(0)
                                    ClassUtils.RunAsSystem(sCmdLinesSystem.ToArray)

                                    Environment.Exit(1)
                                    End
                                End If
                            End If
                        End If

                        ' Remove PSVR monitors
                        If (True) Then
                            Dim bScanNewDevices As Boolean = False
                            Dim sDevicesToRemove As New List(Of String)
                            For Each mPsvrMonitor In ClassMonitor.PSVR_MONITOR_IDS
                                For Each mDisplayInfo In mDriverInstaller.GetDeviceProviderDISPLAY(mPsvrMonitor.GetMonitorName())
                                    ' Dont allow anything else than non-system drivers past here!
                                    If (Not String.IsNullOrEmpty(mDisplayInfo.sDriverInfPath) AndAlso mDisplayInfo.sDriverInfPath.ToLowerInvariant.StartsWith("oem")) Then
                                        mDriverInstaller.RemoveDriver(mDisplayInfo.sDriverInfPath)
                                    End If

                                    sDevicesToRemove.Add(mDisplayInfo.sDeviceID)
                                Next
                            Next

                            ' Remove devices after everything is done.
                            For Each mDeviceID As String In sDevicesToRemove
                                mDriverInstaller.RemoveDevice(mDeviceID, True)
                                bScanNewDevices = True
                            Next

                            If (bScanNewDevices) Then
                                mDriverInstaller.ScanDevices()
                            End If
                        End If

                        ' Just uninstall
                        If (sCommand = COMMANDLINE_INSTALL_PSVR_DRIVERS OrElse sCommand = COMMANDLINE_PATCH_PSVR_MONITOR_REMOVE OrElse sCommand = COMMANDLINE_UNINSTALL_PSVR) Then
                            Exit While
                        End If

                        Threading.Thread.Sleep(5000)

                        Dim bInstallDirectMode As Boolean = (sCommand = COMMANDLINE_PATCH_PSVR_MONITOR_DIRECT)
                        If (Not mClassMonitor.PatchPlaystationVrMonitor(bInstallDirectMode)) Then
                            Throw New ArgumentException("Unable to find PlayStation VR monitor")
                        End If

                        ' Restart monitors.
                        If (True) Then
                            Dim bScanNewDevices As Boolean = False
                            For Each mPsvrMonitor In ClassMonitor.PSVR_MONITOR_IDS
                                For Each mDisplayInfo In mDriverInstaller.GetDeviceProviderDISPLAY(mPsvrMonitor.GetMonitorName)
                                    mDriverInstaller.RestartDevice(mDisplayInfo.sDeviceID)
                                    bScanNewDevices = True
                                Next
                            Next

                            If (bScanNewDevices) Then
                                mDriverInstaller.ScanDevices()
                            End If
                        End If


                    Catch ex As Exception
                        ClassAdvancedExceptionLogging.WriteToLog(ex)

                        If (sCmdLines.Contains(COMMANDLINE_VERBOSE)) Then
                            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                        End If

                        Environment.Exit(-1)
                        End
                    End Try

                    Exit While
                End While

                While (sCommand = COMMANDLINE_INSTALL_PSVR_DRIVERS OrElse sCommand = COMMANDLINE_UNINSTALL_PSVR)
                    ' Un/Install PSVR libusb drivers.
                    bExitOnSuccess = True

                    Try
                        Dim mDriverInstaller As New ClassLibusbDriver

                        ' Unisntall drivers and device first
                        If (True) Then
                            Dim iRetries As Integer = 5
                            While True
                                ' Remove old drivers.
                                mDriverInstaller.UninstallPlaystationVrDriver64()

                                Threading.Thread.Sleep(5000)

                                Dim iDriverStatus = mDriverInstaller.VerifyPlaystationVrDriver64()
                                If (iDriverStatus <> ClassLibusbDriver.ENUM_DRIVER_STATE.NOT_INSTALLED) Then
                                    If (iRetries > 0) Then
                                        iRetries -= 1
                                        Continue While
                                    End If

                                    Throw New ArgumentException(String.Format("Driver uninstallation failed with error: {0} - {1}", CInt(ClassLibusbDriver.ENUM_WDI_ERROR.WDI_ERROR_OTHER), "Unable to uninstall drivers"))
                                End If

                                Exit While
                            End While
                        End If

                        ' Just uninstall
                        If (sCommand = COMMANDLINE_UNINSTALL_PSVR) Then
                            Exit While
                        End If

                        ' Install drivers
                        If (True) Then
                            Dim iRetries As Integer = 5
                            While (True)
                                Threading.Thread.Sleep(5000)

                                Dim iDrvierInstallExitCode = mDriverInstaller.InstallPlaystationVrDrvier64()

                                ' Just in case Windows installs default drivers after uninstallation.
                                If (iRetries > 0 AndAlso iDrvierInstallExitCode = ClassLibusbDriver.ENUM_WDI_ERROR.WDI_ERROR_PENDING_INSTALLATION) Then
                                    iRetries -= 1
                                    Continue While
                                End If

                                If (iDrvierInstallExitCode <> ClassLibusbDriver.ENUM_WDI_ERROR.WDI_SUCCESS) Then
                                    Throw New ArgumentException(String.Format("Driver installation failed with error: {0} - {1}", CInt(iDrvierInstallExitCode), iDrvierInstallExitCode.ToString))
                                End If

                                Exit While
                            End While
                        End If
                    Catch ex As Exception
                        ClassAdvancedExceptionLogging.WriteToLog(ex)

                        If (sCmdLines.Contains(COMMANDLINE_VERBOSE)) Then
                            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                        End If

                        Environment.Exit(-1)
                        End
                    End Try

                    Exit While
                End While

                While (sCommand = COMMANDLINE_INSTALL_PSEYE_DRIVERS OrElse sCommand = COMMANDLINE_UNINSTALL_PSEYE)
                    ' Un/Install PSEye libusb drivers.
                    bExitOnSuccess = True

                    Try
                        Dim mDriverInstaller As New ClassLibusbDriver

                        ' Unisntall drivers and device first
                        If (True) Then
                            Dim iRetries As Integer = 5
                            While True
                                ' Remove old drivers.
                                mDriverInstaller.UninstallPlaystationEyeDriver64()

                                Threading.Thread.Sleep(5000)

                                Dim iDriverStatus = mDriverInstaller.VerifyPlaystationEyeDriver64()
                                If (iDriverStatus <> ClassLibusbDriver.ENUM_DRIVER_STATE.NOT_INSTALLED) Then
                                    If (iRetries > 0) Then
                                        iRetries -= 1
                                        Continue While
                                    End If

                                    Throw New ArgumentException(String.Format("Driver uninstallation failed with error: {0} - {1}", CInt(ClassLibusbDriver.ENUM_WDI_ERROR.WDI_ERROR_OTHER), "Unable to uninstall drivers"))
                                End If

                                Exit While
                            End While
                        End If

                        ' Just uninstall
                        If (sCommand.ToLowerInvariant = COMMANDLINE_UNINSTALL_PSEYE) Then
                            Exit While
                        End If

                        ' Install drivers
                        If (True) Then
                            Dim iRetries As Integer = 5
                            While (True)
                                Threading.Thread.Sleep(5000)

                                Dim iDrvierInstallExitCode = mDriverInstaller.InstallPlaystationEyeDriver64()

                                ' Just in case Windows installs default drivers after uninstallation.
                                If (iRetries > 0 AndAlso iDrvierInstallExitCode = ClassLibusbDriver.ENUM_WDI_ERROR.WDI_ERROR_PENDING_INSTALLATION) Then
                                    iRetries -= 1
                                    Continue While
                                End If

                                If (iDrvierInstallExitCode <> ClassLibusbDriver.ENUM_WDI_ERROR.WDI_SUCCESS) Then
                                    Throw New ArgumentException(String.Format("Driver installation failed with error: {0} - {1}", CInt(iDrvierInstallExitCode), iDrvierInstallExitCode.ToString))
                                End If

                                Exit While
                            End While
                        End If

                    Catch ex As Exception
                        ClassAdvancedExceptionLogging.WriteToLog(ex)

                        If (sCmdLines.Contains(COMMANDLINE_VERBOSE)) Then
                            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                        End If

                        Environment.Exit(-1)
                        End
                    End Try

                    Exit While
                End While

                While (sCommand = COMMANDLINE_INSTALL_PS4CAM_DRIVERS OrElse sCommand = COMMANDLINE_UNINSTALL_PS4CAM)
                    ' Un/Install PSEye libusb drivers.
                    bExitOnSuccess = True

                    Try
                        Dim mDriverInstaller As New ClassLibusbDriver

                        ' Unisntall drivers and device first
                        If (True) Then
                            Dim iRetries As Integer = 5
                            While True
                                ' Remove old drivers.
                                mDriverInstaller.UninstallPlaystation4CamDriver64()

                                Threading.Thread.Sleep(5000)

                                Dim iDriverStatus = mDriverInstaller.VerifyPlaystation4CamDriver64()
                                If (iDriverStatus <> ClassLibusbDriver.ENUM_DRIVER_STATE.NOT_INSTALLED) Then
                                    If (iRetries > 0) Then
                                        iRetries -= 1
                                        Continue While
                                    End If

                                    Throw New ArgumentException(String.Format("Driver uninstallation failed with error: {0} - {1}", CInt(ClassLibusbDriver.ENUM_WDI_ERROR.WDI_ERROR_OTHER), "Unable to uninstall drivers"))
                                End If

                                Exit While
                            End While
                        End If

                        ' Just uninstall
                        If (sCommand.ToLowerInvariant = COMMANDLINE_UNINSTALL_PS4CAM) Then
                            Exit While
                        End If

                        ' Install drivers
                        If (True) Then
                            Dim iRetries As Integer = 5
                            While (True)
                                Threading.Thread.Sleep(5000)

                                Dim iDrvierInstallExitCode = mDriverInstaller.InstallPlaystation4CamDriver64()

                                ' Just in case Windows installs default drivers after uninstallation.
                                If (iRetries > 0 AndAlso iDrvierInstallExitCode = ClassLibusbDriver.ENUM_WDI_ERROR.WDI_ERROR_PENDING_INSTALLATION) Then
                                    iRetries -= 1
                                    Continue While
                                End If

                                If (iDrvierInstallExitCode <> ClassLibusbDriver.ENUM_WDI_ERROR.WDI_SUCCESS) Then
                                    Throw New ArgumentException(String.Format("Driver installation failed with error: {0} - {1}", CInt(iDrvierInstallExitCode), iDrvierInstallExitCode.ToString))
                                End If

                                Exit While
                            End While
                        End If

                        ' Upload firmware
                        Using mCameraFirmware As New ClassCameraFirmwareWatchdog
                            mCameraFirmware.UploadFirmware()
                        End Using

                        mDriverInstaller.ScanDevices()

                    Catch ex As Exception
                        ClassAdvancedExceptionLogging.WriteToLog(ex)

                        If (sCmdLines.Contains(COMMANDLINE_VERBOSE)) Then
                            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                        End If

                        Environment.Exit(-1)
                        End
                    End Try

                    Exit While
                End While
            Next

            If (bExitOnSuccess) Then
                Environment.Exit(0)
                End
            End If
        End If
    End Sub

    Public Sub SelectPage(iPage As ENUM_PAGE)
        Dim mSleectColor As Color = Color.Lavender
        Dim mNormalColor As Color = Color.GhostWhite

        Select Case (iPage)
            Case ENUM_PAGE.STARTPAGE
                g_mUCStartPage.Visible = True
                g_mUCPlaystationVR.Visible = False
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False
                g_mUCVirtualMotionTracker.Visible = False

                Button_NavServiceManagement.BackColor = mSleectColor
                Button_NavPsvrManagement.BackColor = mNormalColor
                Button_NavVirtualControllers.BackColor = mNormalColor
                Button_NavHeadMountedDisplay.BackColor = mNormalColor
                Button_NavVirtualTrackers.BackColor = mNormalColor
                Button_NavVirtualMotionTrackers.BackColor = mNormalColor

            Case ENUM_PAGE.PLAYSTATION_VR
                g_mUCStartPage.Visible = False
                g_mUCPlaystationVR.Visible = True
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False
                g_mUCVirtualMotionTracker.Visible = False

                Button_NavServiceManagement.BackColor = mNormalColor
                Button_NavPsvrManagement.BackColor = mSleectColor
                Button_NavVirtualControllers.BackColor = mNormalColor
                Button_NavHeadMountedDisplay.BackColor = mNormalColor
                Button_NavVirtualTrackers.BackColor = mNormalColor
                Button_NavVirtualMotionTrackers.BackColor = mNormalColor

            Case ENUM_PAGE.VIRTUAL_CONTROLLERS
                g_mUCStartPage.Visible = False
                g_mUCPlaystationVR.Visible = False
                g_mUCVirtualControllers.Visible = True
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False
                g_mUCVirtualMotionTracker.Visible = False

                Button_NavServiceManagement.BackColor = mNormalColor
                Button_NavPsvrManagement.BackColor = mNormalColor
                Button_NavVirtualControllers.BackColor = mSleectColor
                Button_NavHeadMountedDisplay.BackColor = mNormalColor
                Button_NavVirtualTrackers.BackColor = mNormalColor
                Button_NavVirtualMotionTrackers.BackColor = mNormalColor

            Case ENUM_PAGE.VIRTUAL_HMDS
                g_mUCStartPage.Visible = False
                g_mUCPlaystationVR.Visible = False
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = True
                g_mUCVirtualTrackers.Visible = False
                g_mUCVirtualMotionTracker.Visible = False

                Button_NavServiceManagement.BackColor = mNormalColor
                Button_NavPsvrManagement.BackColor = mNormalColor
                Button_NavVirtualControllers.BackColor = mNormalColor
                Button_NavHeadMountedDisplay.BackColor = mSleectColor
                Button_NavVirtualTrackers.BackColor = mNormalColor
                Button_NavVirtualMotionTrackers.BackColor = mNormalColor

            Case ENUM_PAGE.VIRTUAL_TRACKERS
                g_mUCStartPage.Visible = False
                g_mUCPlaystationVR.Visible = False
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = True
                g_mUCVirtualMotionTracker.Visible = False

                Button_NavServiceManagement.BackColor = mNormalColor
                Button_NavPsvrManagement.BackColor = mNormalColor
                Button_NavVirtualControllers.BackColor = mNormalColor
                Button_NavHeadMountedDisplay.BackColor = mNormalColor
                Button_NavVirtualTrackers.BackColor = mSleectColor
                Button_NavVirtualMotionTrackers.BackColor = mNormalColor

            Case ENUM_PAGE.VIRTUAL_MOTION_TRACKERS
                g_mUCStartPage.Visible = False
                g_mUCPlaystationVR.Visible = False
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False
                g_mUCVirtualMotionTracker.Visible = True

                Button_NavServiceManagement.BackColor = mNormalColor
                Button_NavPsvrManagement.BackColor = mNormalColor
                Button_NavVirtualControllers.BackColor = mNormalColor
                Button_NavHeadMountedDisplay.BackColor = mNormalColor
                Button_NavVirtualTrackers.BackColor = mNormalColor
                Button_NavVirtualMotionTrackers.BackColor = mSleectColor
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
            ClassAdvancedExceptionLogging.WriteToLog(ex)
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
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try
    End Sub

    Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (e.CloseReason <> CloseReason.UserClosing) Then
            Return
        End If

        Try
            Dim mProcesses As New List(Of Process)
            mProcesses.AddRange(Process.GetProcessesByName("PSMoveService"))
            mProcesses.AddRange(Process.GetProcessesByName("PSMoveServiceAdmin"))
            mProcesses.AddRange(Process.GetProcessesByName("PSMoveConfigTool"))

            If (mProcesses.Count > 0) Then
                Dim bCloseService As Boolean = False

                If (g_bAutoClose) Then
                    bCloseService = True
                Else
                    Dim sMsg As New Text.StringBuilder
                    sMsg.AppendLine("PSMoveServiceEx is currently running.")
                    sMsg.AppendLine()
                    sMsg.AppendLine("Do you want to close PSMoveServiceEx?")

                    Select Case (MessageBox.Show(sMsg.ToString, "PSMoveServiceEx is still running", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        Case DialogResult.Cancel
                            e.Cancel = True

                        Case DialogResult.Yes
                            bCloseService = True

                        Case DialogResult.No
                            ' Do nothing
                    End Select
                End If

                If (bCloseService) Then
                    If (mProcesses.Count > 0) Then
                        For Each mProcess In mProcesses
                            mProcess.Kill()
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

        Try
            ClassAdvancedExceptionLogging.m_AutoFlushToFile = False
        Catch ex As Exception
            ' Do nothing
        End Try
    End Sub

    Private Sub FormMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Using mCloseForm As New FormLoading
            mCloseForm.Text = "Closing established connections and cleaning up..."
            mCloseForm.m_ProgressBar.Style = ProgressBarStyle.Continuous
            mCloseForm.Show(Me)
            mCloseForm.Refresh()

            CleanUp()
        End Using
    End Sub

    Private Sub Button_NavServiceManagement_Click(sender As Object, e As EventArgs) Handles Button_NavServiceManagement.Click
        SelectPage(ENUM_PAGE.STARTPAGE)
    End Sub

    Private Sub Button_RunService_Click(sender As Object, e As EventArgs) Handles Button_NavRunService.Click
        If (g_mUCStartPage Is Nothing OrElse g_mUCStartPage.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.STARTPAGE)

        Dim mServiceInfo As New ClassServiceInfo
        If (mServiceInfo.IsServiceRunning <> ClassServiceInfo.ENUM_SERVICE_PROCESS_TYPE.NONE) Then
            g_mUCStartPage.LinkLabel_ServiceStop_Click()
        Else
            g_mUCStartPage.LinkLabel_ServiceRun_Click()
        End If
    End Sub

    Private Sub Button_RestartService_Click(sender As Object, e As EventArgs) Handles Button_NavRestartService.Click
        If (g_mUCStartPage Is Nothing OrElse g_mUCStartPage.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.STARTPAGE)
        g_mUCStartPage.LinkLabel_ServiceRestart_Click()
    End Sub

    Private Sub Button_RunServiceConfigTool_Click(sender As Object, e As EventArgs) Handles Button_NavRunServiceConfigTool.Click
        If (g_mUCStartPage Is Nothing OrElse g_mUCStartPage.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.STARTPAGE)
        g_mUCStartPage.LinkLabel_ConfigToolRun_Click()
    End Sub

    Private Sub Button_NavPsvrManagement_Click(sender As Object, e As EventArgs) Handles Button_NavPsvrManagement.Click
        SelectPage(ENUM_PAGE.PLAYSTATION_VR)
    End Sub

    Private Sub Button_NavVirtualControllers_Click(sender As Object, e As EventArgs) Handles Button_NavVirtualControllers.Click
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
    End Sub

    Private Sub Button_NavStartRemoteSocket_Click(sender As Object, e As EventArgs) Handles Button_NavStartRemoteSocket.Click
        If (g_mUCVirtualControllers.g_mUCRemoteDevices Is Nothing OrElse g_mUCVirtualControllers.g_mUCRemoteDevices.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_RemoteSettings

        g_mUCVirtualControllers.g_mUCRemoteDevices.Button_StartSocket_Click()
        g_mUCVirtualControllers.g_mUCRemoteDevices.CheckBox_AllowNewDevices.Checked = True
    End Sub

    Private Sub Button_NavHeadMountedDisplay_Click(sender As Object, e As EventArgs) Handles Button_NavHeadMountedDisplay.Click
        SelectPage(ENUM_PAGE.VIRTUAL_HMDS)
    End Sub

    Private Sub Button_NavVirtualTrackers_Click(sender As Object, e As EventArgs) Handles Button_NavVirtualTrackers.Click
        SelectPage(ENUM_PAGE.VIRTUAL_TRACKERS)
    End Sub

    Private Sub Button_NavVirtualMotionTrackers_Click(sender As Object, e As EventArgs) Handles Button_NavVirtualMotionTrackers.Click
        SelectPage(ENUM_PAGE.VIRTUAL_MOTION_TRACKERS)
    End Sub

    Private Sub Button_NavStartOsc_Click(sender As Object, e As EventArgs) Handles Button_NavStartOsc.Click
        If (g_mUCVirtualMotionTracker Is Nothing OrElse g_mUCVirtualMotionTracker.IsDisposed) Then
            Return
        End If

        If (g_mUCVirtualMotionTracker.g_ClassOscServer Is Nothing) Then
            Return
        End If

        SelectPage(ENUM_PAGE.VIRTUAL_MOTION_TRACKERS)
        g_mUCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_mUCVirtualMotionTracker.TabPage_Management

        If (g_mUCVirtualMotionTracker.g_ClassOscServer.IsRunning AndAlso Not g_mUCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
            g_mUCVirtualMotionTracker.g_UCVmtManagement.LinkLabel_OscPause_Click()
        Else
            g_mUCVirtualMotionTracker.g_UCVmtManagement.LinkLabel_OscRun_Click()
        End If
    End Sub

    Private Sub Button_NavStartPlayCalib_Click(sender As Object, e As EventArgs) Handles Button_NavStartPlayCalib.Click
        If (g_mUCVirtualMotionTracker Is Nothing OrElse g_mUCVirtualMotionTracker.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.VIRTUAL_MOTION_TRACKERS)
        g_mUCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_mUCVirtualMotionTracker.TabPage_PlayspaceCalib
        g_mUCVirtualMotionTracker.g_UCVmtPlayspaceCalib.Button_PlaySpaceManualCalib_Click()
    End Sub

    Private Sub Button_NavUpdate_Click(sender As Object, e As EventArgs) Handles Button_NavUpdate.Click
        Try
            Process.Start("https://github.com/Timocop/PSMoveServiceEx-Virtual-Device-Manager/releases")
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try
    End Sub

    Private Sub Button_NavGitHub_Click(sender As Object, e As EventArgs) Handles Button_NavGitHub.Click
        Try
            Process.Start("https://github.com/Timocop/PSMoveServiceEx-Virtual-Device-Manager")
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try
    End Sub

    Private Sub Button_NavRunSteamVR_Click(sender As Object, e As EventArgs) Handles Button_NavRunSteamVR.Click
        Try
            Process.Start("steam://rungameid/250820")
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
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

        If (g_UCRestartSteamVR IsNot Nothing AndAlso Not g_UCRestartSteamVR.IsDisposed) Then
            g_UCRestartSteamVR.Dispose()
            g_UCRestartSteamVR = Nothing
        End If

        If (g_UCRestartPsms IsNot Nothing AndAlso Not g_UCRestartPsms.IsDisposed) Then
            g_UCRestartPsms.Dispose()
            g_UCRestartPsms = Nothing
        End If

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

        Try
            If (g_mClassCameraFirmwareWatchdog IsNot Nothing) Then
                g_mClassCameraFirmwareWatchdog.Dispose()
                g_mClassCameraFirmwareWatchdog = Nothing
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

        Try
            If (g_mPSMoveServiceCAPI IsNot Nothing) Then
                g_mPSMoveServiceCAPI.Dispose()
                g_mPSMoveServiceCAPI = Nothing
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

        Try
            ClassAdvancedExceptionLogging.m_AutoFlushToFile = False
        Catch ex As Exception
            ' DO nothing
        End Try
    End Sub

End Class
