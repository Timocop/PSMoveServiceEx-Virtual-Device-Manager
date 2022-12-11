Imports System.Runtime.InteropServices

Public Class FormMain
    Public g_mUCVirtualControllers As UCVirtualControllers
    Public g_mUCVirtualHMDs As UCVirtualHMDs
    Public g_mUCVirtualTrackers As UCVirtualTrackers
    Public g_mPSMoveServiceCAPI As ClassServiceClient

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

    Private Sub Button_RestartPSMS_Click(sender As Object, e As EventArgs) Handles Button_RestartPSMS.Click
        Try
            Dim sPSMSPath As String = Nothing

            Dim pProcesses As Process() = Process.GetProcessesByName("PSMoveService")
            If (pProcesses Is Nothing OrElse pProcesses.Length < 1) Then
                Throw New ArgumentException("PSMoveService not running.")
            End If

            For Each mProcess In pProcesses
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
                Throw New ArgumentException("PSMoveService executable not found. Please start manualy.")
            End If

            Dim mNewProcess As New Process()
            mNewProcess.StartInfo.FileName = sPSMSPath
            mNewProcess.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(sPSMSPath)
            mNewProcess.StartInfo.UseShellExecute = False
            mNewProcess.Start()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

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
End Class
