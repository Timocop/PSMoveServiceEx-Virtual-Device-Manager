Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class FormMain
    Public g_mUCStartPage As UCStartPage
    Public g_mUCVirtualControllers As UCVirtualControllers
    Public g_mUCVirtualHMDs As UCVirtualHMDs
    Public g_mUCVirtualTrackers As UCVirtualTrackers
    Public g_mPSMoveServiceCAPI As ClassServiceClient

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

        Label_Version.Text = String.Format("Version: {0}", Application.ProductVersion.ToString)

        SelectPage(ENUM_PAGE.STARTPAGE)
    End Sub

    Public Sub SelectPage(iPage As ENUM_PAGE)
        Select Case (iPage)
            Case ENUM_PAGE.STARTPAGE
                g_mUCStartPage.Visible = True
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False

            Case ENUM_PAGE.VIRTUAL_CONTROLLERS
                g_mUCStartPage.Visible = False
                g_mUCVirtualControllers.Visible = True
                g_mUCVirtualHMDs.Visible = False
                g_mUCVirtualTrackers.Visible = False

            Case ENUM_PAGE.VIRTUAL_HMDS
                g_mUCStartPage.Visible = False
                g_mUCVirtualControllers.Visible = False
                g_mUCVirtualHMDs.Visible = True
                g_mUCVirtualTrackers.Visible = False

            Case ENUM_PAGE.VIRTUAL_TRACKERS
                g_mUCStartPage.Visible = False
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
        g_mUCVirtualControllers.g_mUCVirtualMotionTracker.TabControl1.SelectedTab = g_mUCVirtualControllers.g_mUCVirtualMotionTracker.TabPage_Management

        g_mUCVirtualControllers.g_mUCVirtualMotionTracker.LinkLabel_OscRun_Click()
    End Sub

    Public Sub LinkLabel1LinkLabel_VMTPauseOscServer_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1LinkLabel_VMTPauseOscServer.LinkClicked
        If (g_mUCVirtualControllers.g_mUCVirtualMotionTracker Is Nothing OrElse g_mUCVirtualControllers.g_mUCVirtualMotionTracker.IsDisposed) Then
            Return
        End If

        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
        g_mUCVirtualControllers.TabControl1.SelectedTab = g_mUCVirtualControllers.TabPage_VMT
        g_mUCVirtualControllers.g_mUCVirtualMotionTracker.TabControl1.SelectedTab = g_mUCVirtualControllers.g_mUCVirtualMotionTracker.TabPage_Management

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
End Class
