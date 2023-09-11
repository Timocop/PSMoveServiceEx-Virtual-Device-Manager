Imports System.Resources
Public Class FormMain
    Public g_mUCStartPage As UCStartPage
    Public g_mUCVirtualControllers As UCVirtualControllers
    Public g_mUCVirtualHMDs As UCVirtualHMDs
    Public g_mUCVirtualTrackers As UCVirtualTrackers
    Public g_mPSMoveServiceCAPI As ClassServiceClient
    Public g_mClassUpdateChecker As ClassUpdateChecker
    Dim mConfig As New ClassServiceInfo
    Public rm As New ResourceManager("PSMSVirtualDeviceManager.Localization", GetType(FormMain).Assembly)
    Private g_bIgnoreEvents As Boolean = False
    Enum ENUM_PAGE
        STARTPAGE
        VIRTUAL_CONTROLLERS
        VIRTUAL_HMDS
        VIRTUAL_TRACKERS
    End Enum

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 

        g_mUCStartPage = New UCStartPage(Me)
        g_mUCStartPage.SuspendLayout()
        g_mUCStartPage.Parent = Panel_Pages
        g_mUCStartPage.Dock = DockStyle.Fill
        g_mUCStartPage.Visible = False
        g_mUCStartPage.ResumeLayout()

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

        Label_Version.Text = String.Format("{0}: {1}", rm.GetString("Version"), Application.ProductVersion.ToString())


        g_mClassUpdateChecker = New ClassUpdateChecker(Me)
        g_mClassUpdateChecker.StartUpdateCheck()

        SelectPage(ENUM_PAGE.STARTPAGE)

        AddHandler g_mPSMoveServiceCAPI.OnConnectionStatusChanged, AddressOf OnServiceConnectionStatusChanged
    End Sub

    Public Sub SelectPage(iPage As ENUM_PAGE)
        Select Case (iPage)
            Case ENUM_PAGE.STARTPAGE
                g_mUCStartPage.Visible = True
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False

                LinkLabel_StartPage.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Bold)
                LinkLabel_Controllers.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_HMDs.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_Trackers.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)

            Case ENUM_PAGE.VIRTUAL_CONTROLLERS
                g_mUCStartPage.Visible = False
                g_mUCVirtualControllers.Visible = True
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False

                LinkLabel_StartPage.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_Controllers.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Bold)
                LinkLabel_HMDs.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_Trackers.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)

            Case ENUM_PAGE.VIRTUAL_HMDS
                g_mUCStartPage.Visible = False
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = True
                g_mUCVirtualTrackers.Visible = False

                LinkLabel_StartPage.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_Controllers.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_HMDs.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Bold)
                LinkLabel_Trackers.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)

            Case ENUM_PAGE.VIRTUAL_TRACKERS
                g_mUCStartPage.Visible = False
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = True

                LinkLabel_StartPage.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_Controllers.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_HMDs.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Regular)
                LinkLabel_Trackers.Font = New Font(LinkLabel_StartPage.Font, FontStyle.Bold)
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

    Public Sub LinkLabel_Controllers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Controllers.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
    End Sub

    Public Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_HMDs.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_HMDS)
    End Sub

    Public Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Trackers.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_TRACKERS)
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

    Public Sub LinkLabel_ControllersVMT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ControllersVMT.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_VMT
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
        If (g_mUCVirtualControllers.g_mUCVirtualMotionTracker Is Nothing OrElse g_mUCVirtualControllers.g_mUCVirtualMotionTracker.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_VMT
        g_mUCVirtualControllers.g_mUCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_mUCVirtualControllers.g_mUCVirtualMotionTracker.TabPage_Management

        g_mUCVirtualControllers.g_mUCVirtualMotionTracker.LinkLabel_OscRun_Click()
    End Sub

    Public Sub LinkLabel1LinkLabel_VMTPauseOscServer_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1LinkLabel_VMTPauseOscServer.LinkClicked
        If (g_mUCVirtualControllers.g_mUCVirtualMotionTracker Is Nothing OrElse g_mUCVirtualControllers.g_mUCVirtualMotionTracker.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_VMT
        g_mUCVirtualControllers.g_mUCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_mUCVirtualControllers.g_mUCVirtualMotionTracker.TabPage_Management

        g_mUCVirtualControllers.g_mUCVirtualMotionTracker.LinkLabel_OscPause_Click()
    End Sub

    Public Sub LinkLabel_InstallCameraDrivers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_InstallCameraDrivers.LinkClicked
        If (g_mUCStartPage Is Nothing OrElse g_mUCStartPage.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.STARTPAGE)
        g_mUCStartPage.LinkLabel_InstallDrivers_Click()
    End Sub

    Public Sub LinkLabel_FactoryResetService_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_FactoryResetService.LinkClicked
        If (g_mUCStartPage Is Nothing OrElse g_mUCStartPage.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.STARTPAGE)
        g_mUCStartPage.LinkLabel_ServiceFactory_Click()
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

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        mConfig.LoadConfig()
        ComboBox1.SelectedIndex = mConfig.lastselectedlanguage
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 1
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("pl-PL")
                mConfig.LastSelectedLanguage = 1
                mConfig.SaveConfig()
            Case 2
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("es")
                mConfig.LastSelectedLanguage = 2
                mConfig.SaveConfig()
            Case 3
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("de")
                mConfig.LastSelectedLanguage = 3
                mConfig.SaveConfig()
            Case Else ' default
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US")
                mConfig.LastSelectedLanguage = 0
                mConfig.SaveConfig()
        End Select

        ' Navigation bar
        Label2.Text = rm.GetString("Navigation")
        LinkLabel_StartPage.Text = rm.GetString("ServiceManagement")
        LinkLabel_RunPSMS.Text = rm.GetString("RunPSMS")
        LinkLabel_StopPSMS.Text = rm.GetString("StopPSMS")
        LinkLabel_RestartPSMS.Text = rm.GetString("RestartPSMS")
        LinkLabel_RunPSMSTool.Text = rm.GetString("RunPSMS")
        LinkLabel_Controllers.Text = rm.GetString("Controllers")
        LinkLabel_ControllersGeneral.Text = rm.GetString("ControllersGeneral")
        LinkLabel_ControllersRemote.Text = rm.GetString("ControllersRemote")
        LinkLabel_RemoteStartSocket.Text = rm.GetString("RemoteStartSocket")
        LinkLabel_ControllersAttachments.Text = rm.GetString("ControllerAttachements")
        LinkLabel_ControllersVMT.Text = rm.GetString("ControllersVMT")
        LinkLabel_VMTStartOscServer.Text = rm.GetString("VMTStartOscServer")
        LinkLabel1LinkLabel_VMTPauseOscServer.Text = rm.GetString("VMTPauseOscServer")
        LinkLabel_HMDs.Text = rm.GetString("HMDs")
        LinkLabel_Trackers.Text = rm.GetString("Trackers")
        Label4.Text = rm.GetString("Troubleshooting")
        LinkLabel_InstallCameraDrivers.Text = rm.GetString("InstallCameraDrivers")
        LinkLabel_FactoryResetService.Text = rm.GetString("FactoryResetService")
        Label1.Text = rm.GetString("Language")
        ComboBox1.Items.Insert(0, rm.GetString("English")) ' i have to do it like this because if you do ComboBox1.Items(1) = (rm.GetString("English") its gonna detect a selectedindex
        ComboBox1.Items.Insert(1, rm.GetString("Polish"))  ' change and make an infinite loop so now if you select an language its gonna work now but your selection will disappear.
        ComboBox1.Items.Insert(2, rm.GetString("Spanish"))
        ComboBox1.Items.Insert(3, rm.GetString("German"))
        For i As Integer = ComboBox1.Items.Count - 1 To 4 Step -1
            ComboBox1.Items.RemoveAt(i)
        Next

        ToolTip1.SetToolTip(LanguageT, rm.GetString("LanguageT"))
        LinkLabel_RunSteamVR.Text = rm.GetString("RunSteamVR")
        LinkLabel_Github.Text = rm.GetString("GitHub")
        LinkLabel_Updates.Text = rm.GetString("Updates")
        Label_Version.Text = rm.GetString("Version")
        ' UCStartPage
        g_mUCStartPage.Label3.Text = rm.GetString("ServiceManagement")
        g_mUCStartPage.Label4.Text = rm.GetString("Label3")
        g_mUCStartPage.Label8.Text = rm.GetString("PSMSUpdateAvailable")
        g_mUCStartPage.Label10.Text = rm.GetString("PSMSUpdateText")
        g_mUCStartPage.Button_PsmsxUpdateDownload.Text = rm.GetString("DownloadNow")
        g_mUCStartPage.Button_PsmsUpdateIgnore.Text = rm.GetString("Ignore")
        g_mUCStartPage.Label9.Text = rm.GetString("VDMUpdateAvailable")
        g_mUCStartPage.Label11.Text = rm.GetString("VDMUpdateText")
        g_mUCStartPage.Button_VdmUpdateDownload.Text = rm.GetString("DownloadNow")
        g_mUCStartPage.Button_VdmUpdateIgnore.Text = rm.GetString("DownloadNow")
        g_mUCStartPage.Label1.Text = rm.GetString("ServiceControl")
        g_mUCStartPage.LinkLabel_ServiceRun.Text = rm.GetString("RunPSMS")
        g_mUCStartPage.LinkLabel_ServiceRunCmd.Text = rm.GetString("DebugService")
        g_mUCStartPage.LinkLabel_ServiceRestart.Text = rm.GetString("RestartPSMS")
        g_mUCStartPage.LinkLabel_ServiceStop.Text = rm.GetString("StopPSMS")
        g_mUCStartPage.LinkLabel_ServicePath.Text = rm.GetString("SetServicePath")
        g_mUCStartPage.Label6.Text = rm.GetString("Troubleshooting")
        g_mUCStartPage.LinkLabel_InstallDrivers.Text = rm.GetString("InstallCameraDrivers")
        g_mUCStartPage.LinkLabel_ServiceFactory.Text = rm.GetString("FactoryResetService")
        g_mUCStartPage.Label2.Text = rm.GetString("Configuration")
        g_mUCStartPage.LinkLabel_ConfigToolRun.Text = rm.GetString("RunPSMSTool")
        g_mUCStartPage.LinkLabel_ConfigToolRunCmd.Text = rm.GetString("RunPSMSToolCmd")
        g_mUCStartPage.LinkLabel_ConfigToolClose.Text = rm.GetString("ClosePSMSTool")
        g_mUCStartPage.Label7.Text = rm.GetString("SupportAndUpdates")
        g_mUCStartPage.LinkLabel_Github.Text = rm.GetString("VisitGithub")
        g_mUCStartPage.LinkLabel_Updates.Text = rm.GetString("Updates")
        g_mUCStartPage.Label12.Text = rm.GetString("AvailableServiceDevices")
        g_mUCStartPage.ColumnHeader_Type.Text = rm.GetString("Type")
        g_mUCStartPage.ColumnHeader_Color.Text = rm.GetString("Color")
        g_mUCStartPage.ColumnHeader_ID.Text = rm.GetString("ID")
        g_mUCStartPage.ColumnHeader_Serial.Text = rm.GetString("Serial")
        g_mUCStartPage.ColumnHeader_Pos.Text = rm.GetString("Position")
        g_mUCStartPage.ColumnHeader_Orientation.Text = rm.GetString("Orientation")
        g_mUCStartPage.ColumnHeader_Battery.Text = rm.GetString("Battery")

    End Sub

End Class
