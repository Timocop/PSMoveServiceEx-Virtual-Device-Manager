Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class FormMain
    Public g_mUCVirtualControllers As UCVirtualControllers
    Public g_mUCVirtualHMDs As UCVirtualHMDs
    Public g_mUCVirtualTrackers As UCVirtualTrackers
    Public g_mPSMoveServiceCAPI As ClassServiceClient

    Private g_bIgnoreEvents As Boolean = False

    Enum ENUM_PAGE
        VIRTUAL_CONTROLLERS
        VIRTUAL_HMDS
        VIRTUAL_TRACKERS
    End Enum

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
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
                g_mPSMoveServiceCAPI.TheadStart()
                Exit While
            Catch ex As Exception
                Dim sMsg As New Text.StringBuilder
                sMsg.AppendLine("Unable to create the PSMoveServiceEx client with the following error")
                sMsg.AppendLine(ex.Message)
                If (MessageBox.Show(sMsg.ToString, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = DialogResult.Cancel) Then
                    Exit While
                End If
            End Try
        End While

        Label_Version.Text = String.Format("Version: {0}", Application.ProductVersion.ToString)
    End Sub

    Public Sub SelectPage(iPage As ENUM_PAGE)
        TableLayoutPanel_Title.Visible = False

        Select Case (iPage)
            Case ENUM_PAGE.VIRTUAL_CONTROLLERS
                g_mUCVirtualControllers.Visible = True
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False

            Case ENUM_PAGE.VIRTUAL_HMDS
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = True
                g_mUCVirtualTrackers.Visible = False

            Case ENUM_PAGE.VIRTUAL_TRACKERS
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = True
        End Select
    End Sub

    Private Sub CleanUp()
        Try
            If (g_mPSMoveServiceCAPI IsNot Nothing) Then
                g_mPSMoveServiceCAPI.Dispose()
                g_mPSMoveServiceCAPI = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

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

    Private Sub LinkLabel_Controllers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Controllers.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_HMDs.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_HMDS)
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Trackers.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_TRACKERS)
    End Sub

    Private Sub LinkLabel_ControllersGeneral_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ControllersGeneral.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_General
    End Sub

    Private Sub LinkLabel_ControllersRemote_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ControllersRemote.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_RemoteSettings
    End Sub

    Private Sub LinkLabel_ControllersAttachments_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ControllersAttachments.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_ControllerAttachments
    End Sub

    Private Sub LinkLabel_ControllersVMT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ControllersVMT.LinkClicked
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_VMT
    End Sub

    Private Sub LinkLabel_InstallCameraDrivers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_InstallCameraDrivers.LinkClicked
        Try
            If (Process.GetProcessesByName("PSMoveService").Count > 0) Then
                Throw New ArgumentException("PSMoveServiceEx is running. Please close PSMoveServiceEx!")
            End If

            Dim sMessage As New Text.StringBuilder
            sMessage.AppendLine("You are about to install LibUSB drivers for Playstation Eye Cameras.")
            sMessage.AppendLine("Already existing drivers will be replaced!")
            sMessage.AppendLine()
            sMessage.AppendLine("Do you want to continue?")
            If (MessageBox.Show(sMessage.ToString, "Driver Installation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                Return
            End If

            Dim mDriverInstaller As New ClassLibusbDriver
            mDriverInstaller.InstallDriver64()

            MessageBox.Show("Drivers installed successfully!", "Driver Installation", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_UninstallCameraDrivers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_UninstallCameraDrivers.LinkClicked
        Try
            If (Process.GetProcessesByName("PSMoveService").Count > 0) Then
                Throw New ArgumentException("PSMoveServiceEx is running. Please close PSMoveServiceEx!")
            End If

            Dim sMessage As New Text.StringBuilder
            sMessage.AppendLine("You are about to uninstall LibUSB drivers for Playstation Eye Cameras.")
            sMessage.AppendLine()
            sMessage.AppendLine("WARNING:")
            sMessage.AppendLine("All USB Controllers and USB Hubs will be restarted while the uninstallation process!")
            sMessage.AppendLine("USB input devices (such as keyboard and mouse) might not respond during the uninstallation process!")
            sMessage.AppendLine()
            sMessage.AppendLine("Do you want to continue?")
            If (MessageBox.Show(sMessage.ToString, "Driver Uninstallation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                Return
            End If

            Dim mDriverInstaller As New ClassLibusbDriver
            mDriverInstaller.UninstallDriver64()

            MessageBox.Show("Drivers uninstalled successfully!", "Driver Uninstallation", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_FactoryResetService_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_FactoryResetService.LinkClicked
        Try
            If (Process.GetProcessesByName("PSMoveService").Count > 0) Then
                Throw New ArgumentException("PSMoveServiceEx is running. Please close PSMoveServiceEx!")
            End If

            Dim sMessage As New Text.StringBuilder
            sMessage.AppendLine("You are about to remove all PSMoveServiceEx configurations.")
            sMessage.AppendLine("THIS CAN NOT BE UNDONE!")
            sMessage.AppendLine()
            sMessage.AppendLine("Do you want to continue?")
            If (MessageBox.Show(sMessage.ToString, "Factory Reset", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                Return
            End If

            Dim sConfigFolder As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PSMoveService")

            If (IO.Directory.Exists(sConfigFolder)) Then
                IO.Directory.Delete(sConfigFolder, True)
            End If

            MessageBox.Show("All config have been removed!", "Factory Reset", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_RunPSMS_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_RunPSMS.LinkClicked
        Try
            If (Process.GetProcessesByName("PSMoveService").Count > 0) Then
                Throw New ArgumentException("PSMoveServiceEx is already running!")
            End If

            Dim mConfig As New ClassServiceInfo
            mConfig.LoadConfig()

            If (Not mConfig.FileExist()) Then
                If (mConfig.FindByProcess()) Then
                    mConfig.SaveConfig()
                Else
                    If (mConfig.SearchForService) Then
                        mConfig.SaveConfig()
                    Else
                        Return
                    End If
                End If
            End If

            Using mProcess As New Process
                mProcess.StartInfo.FileName = mConfig.m_FileName
                mProcess.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(mConfig.m_FileName)
                mProcess.StartInfo.UseShellExecute = False

                mProcess.Start()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_StopPSMS_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_StopPSMS.LinkClicked
        Try
            Dim mProcesses As Process() = Process.GetProcessesByName("PSMoveService")
            If (mProcesses Is Nothing OrElse mProcesses.Length < 1) Then
                Throw New ArgumentException("PSMoveServiceEx is not running!")
            End If

            For Each mProcess In mProcesses
                If (mProcess.CloseMainWindow()) Then
                    mProcess.WaitForExit(10000)
                Else
                    mProcess.Kill()
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_RestartPSMS_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_RestartPSMS.LinkClicked
        Try
            Dim sPSMSPath As String = Nothing

            Dim mProcesses As Process() = Process.GetProcessesByName("PSMoveService")
            If (mProcesses Is Nothing OrElse mProcesses.Length < 1) Then
                Throw New ArgumentException("PSMoveServiceEx not running!")
            End If

            For Each mProcess In mProcesses
                If (sPSMSPath Is Nothing) Then
                    sPSMSPath = mProcess.MainModule.FileName
                End If

                If (mProcess.CloseMainWindow()) Then
                    mProcess.WaitForExit(10000)
                Else
                    mProcess.Kill()
                End If
            Next

            If (sPSMSPath Is Nothing OrElse Not IO.File.Exists(sPSMSPath)) Then
                Throw New ArgumentException("PSMoveServiceEx executable not found. Please start manualy.")
            End If

            Using mProcess As New Process
                mProcess.StartInfo.FileName = sPSMSPath
                mProcess.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(sPSMSPath)
                mProcess.StartInfo.UseShellExecute = False

                mProcess.Start()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_RunPSMSTool_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_RunPSMSTool.LinkClicked
        Try
            If (Process.GetProcessesByName("PSMoveConfigTool").Count > 0) Then
                Throw New ArgumentException("PSMoveConfigTool is already running!")
            End If

            Dim mConfig As New ClassServiceInfo
            mConfig.LoadConfig()

            If (Not mConfig.FileExist()) Then
                If (mConfig.FindByProcess()) Then
                    mConfig.SaveConfig()
                Else
                    If (mConfig.SearchForService) Then
                        mConfig.SaveConfig()
                    Else
                        Return
                    End If
                End If
            End If

            Dim sFilePath As String = IO.Path.Combine(IO.Path.GetDirectoryName(mConfig.m_FileName), "PSMoveConfigTool.exe")
            If (Not IO.File.Exists(sFilePath)) Then
                Throw New ArgumentException("PSMoveConfigTool.exe does not exist!")
            End If

            Using mProcess As New Process
                mProcess.StartInfo.FileName = sFilePath
                mProcess.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(sFilePath)
                mProcess.StartInfo.Arguments = "\autoConnect"
                mProcess.StartInfo.UseShellExecute = False

                mProcess.Start()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    Private Sub LinkLabel_SetServicePath_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_SetServicePath.LinkClicked
        Try
            Dim mConfig As New ClassServiceInfo
            mConfig.LoadConfig()

            If (mConfig.SearchForService) Then
                mConfig.SaveConfig()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (e.CloseReason <> CloseReason.UserClosing) Then
            Return
        End If

        If (Process.GetProcessesByName("PSMoveService").Count > 0) Then
            Dim sMsg As New Text.StringBuilder
            sMsg.AppendLine("PSMoveServiceEx is currently running.")
            sMsg.AppendLine()
            sMsg.AppendLine("Do you want to close PSMoveServiceEx?")
            Select Case (MessageBox.Show(sMsg.ToString, "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                Case DialogResult.Cancel
                    e.Cancel = True

                Case DialogResult.Yes
                    Dim mProcesses As Process() = Process.GetProcessesByName("PSMoveService")
                    If (mProcesses IsNot Nothing AndAlso mProcesses.Length > 0) Then
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

    Class ClassServiceInfo
        Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "settings.ini")
        Private g_sFileName As String = ""

        Private g_bConfigsLoaded As Boolean = False

        Public Sub New()
        End Sub

        Property m_FileName As String
            Get
                Return g_sFileName
            End Get
            Set(value As String)
                g_sFileName = value
            End Set
        End Property

        Public Function FileExist() As Boolean
            If (String.IsNullOrEmpty(m_FileName) OrElse Not IO.File.Exists(m_FileName)) Then
                Return False
            End If

            Return True
        End Function

        Public Function FindByProcess() As Boolean
            Dim pProcesses As Process() = Process.GetProcessesByName("PSMoveService")
            If (pProcesses Is Nothing OrElse pProcesses.Length < 1) Then
                Return False
            End If

            For Each mProcess In pProcesses
                m_FileName = mProcess.MainModule.FileName
                Return True
            Next

            Return False
        End Function

        Public Function SearchForService() As Boolean
            Using mFileSearch As New OpenFileDialog()
                mFileSearch.Title = "Find PSMoveServiceEx..."
                mFileSearch.Filter = "PSMoveService|PSMoveService.exe"
                mFileSearch.Multiselect = False
                mFileSearch.CheckFileExists = True

                If (mFileSearch.ShowDialog() = DialogResult.OK) Then
                    m_FileName = mFileSearch.FileName

                    Return True
                End If

                Return False
            End Using
        End Function

        Public Sub SaveConfig()
            If (Not g_bConfigsLoaded) Then
                Return
            End If

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                    mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("Settings", "PSMoveServiceLocation", m_FileName))

                    mIni.WriteKeyValue(mIniContent.ToArray)
                End Using
            End Using
        End Sub

        Public Sub LoadConfig()
            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    m_FileName = mIni.ReadKeyValue("Settings", "PSMoveServiceLocation", "")
                End Using
            End Using

            g_bConfigsLoaded = True
        End Sub
    End Class
End Class
