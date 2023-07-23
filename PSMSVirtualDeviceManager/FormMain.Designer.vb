<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                CleanUp()
            End If

            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.Panel_Pages = New System.Windows.Forms.Panel()
        Me.ToolTip_Service = New System.Windows.Forms.ToolTip(Me.components)
        Me.LinkLabel_RestartPSMS = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_StopPSMS = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_RunPSMSTool = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_RunPSMS = New System.Windows.Forms.LinkLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LinkLabel_StartPage = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_RunSteamVR = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1LinkLabel_VMTPauseOscServer = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_RemoteStartSocket = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_VMTStartOscServer = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_Updates = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_Github = New System.Windows.Forms.LinkLabel()
        Me.Label_Version = New System.Windows.Forms.Label()
        Me.LinkLabel_FactoryResetService = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LinkLabel_InstallCameraDrivers = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ControllersVMT = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ControllersAttachments = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ControllersRemote = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ControllersGeneral = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_Trackers = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_HMDs = New System.Windows.Forms.LinkLabel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LinkLabel_Controllers = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Pages
        '
        Me.Panel_Pages.AutoScroll = True
        Me.Panel_Pages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Pages.Location = New System.Drawing.Point(229, 0)
        Me.Panel_Pages.Name = "Panel_Pages"
        Me.Panel_Pages.Size = New System.Drawing.Size(826, 761)
        Me.Panel_Pages.TabIndex = 1
        '
        'ToolTip_Service
        '
        '
        'LinkLabel_RestartPSMS
        '
        Me.LinkLabel_RestartPSMS.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_RestartPSMS.AutoSize = True
        Me.LinkLabel_RestartPSMS.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_RestartPSMS.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_RestartPSMS.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_RestartPSMS.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16739_16x16_32
        Me.LinkLabel_RestartPSMS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_RestartPSMS.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_RestartPSMS.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RestartPSMS.Location = New System.Drawing.Point(41, 151)
        Me.LinkLabel_RestartPSMS.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.LinkLabel_RestartPSMS.Name = "LinkLabel_RestartPSMS"
        Me.LinkLabel_RestartPSMS.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_RestartPSMS.Size = New System.Drawing.Size(99, 19)
        Me.LinkLabel_RestartPSMS.TabIndex = 16
        Me.LinkLabel_RestartPSMS.TabStop = True
        Me.LinkLabel_RestartPSMS.Text = "      Restart Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_RestartPSMS, "ToolTip")
        Me.LinkLabel_RestartPSMS.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_StopPSMS
        '
        Me.LinkLabel_StopPSMS.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_StopPSMS.AutoSize = True
        Me.LinkLabel_StopPSMS.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_StopPSMS.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_StopPSMS.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_StopPSMS.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5337_16x16_32
        Me.LinkLabel_StopPSMS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_StopPSMS.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_StopPSMS.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_StopPSMS.Location = New System.Drawing.Point(41, 124)
        Me.LinkLabel_StopPSMS.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.LinkLabel_StopPSMS.Name = "LinkLabel_StopPSMS"
        Me.LinkLabel_StopPSMS.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_StopPSMS.Size = New System.Drawing.Size(87, 19)
        Me.LinkLabel_StopPSMS.TabIndex = 15
        Me.LinkLabel_StopPSMS.TabStop = True
        Me.LinkLabel_StopPSMS.Text = "      Stop Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_StopPSMS, "ToolTip")
        Me.LinkLabel_StopPSMS.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_RunPSMSTool
        '
        Me.LinkLabel_RunPSMSTool.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_RunPSMSTool.AutoSize = True
        Me.LinkLabel_RunPSMSTool.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_RunPSMSTool.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_RunPSMSTool.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunPSMSTool.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.mmcshext_128_16x16_32
        Me.LinkLabel_RunPSMSTool.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_RunPSMSTool.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_RunPSMSTool.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunPSMSTool.Location = New System.Drawing.Point(41, 178)
        Me.LinkLabel_RunPSMSTool.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.LinkLabel_RunPSMSTool.Name = "LinkLabel_RunPSMSTool"
        Me.LinkLabel_RunPSMSTool.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_RunPSMSTool.Size = New System.Drawing.Size(147, 19)
        Me.LinkLabel_RunPSMSTool.TabIndex = 10
        Me.LinkLabel_RunPSMSTool.TabStop = True
        Me.LinkLabel_RunPSMSTool.Text = "      Run Service Config Tool"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_RunPSMSTool, "ToolTip")
        Me.LinkLabel_RunPSMSTool.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_RunPSMS
        '
        Me.LinkLabel_RunPSMS.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_RunPSMS.AutoSize = True
        Me.LinkLabel_RunPSMS.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_RunPSMS.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_RunPSMS.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunPSMS.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.LinkLabel_RunPSMS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_RunPSMS.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_RunPSMS.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunPSMS.Location = New System.Drawing.Point(41, 97)
        Me.LinkLabel_RunPSMS.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.LinkLabel_RunPSMS.Name = "LinkLabel_RunPSMS"
        Me.LinkLabel_RunPSMS.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_RunPSMS.Size = New System.Drawing.Size(84, 19)
        Me.LinkLabel_RunPSMS.TabIndex = 8
        Me.LinkLabel_RunPSMS.TabStop = True
        Me.LinkLabel_RunPSMS.Text = "      Run Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_RunPSMS, "ToolTip")
        Me.LinkLabel_RunPSMS.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.AutoScrollMinSize = New System.Drawing.Size(0, 700)
        Me.Panel1.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.LinkLabel_RunPSMS)
        Me.Panel1.Controls.Add(Me.LinkLabel_RunPSMSTool)
        Me.Panel1.Controls.Add(Me.LinkLabel_StartPage)
        Me.Panel1.Controls.Add(Me.LinkLabel_StopPSMS)
        Me.Panel1.Controls.Add(Me.LinkLabel_RunSteamVR)
        Me.Panel1.Controls.Add(Me.LinkLabel_RestartPSMS)
        Me.Panel1.Controls.Add(Me.LinkLabel1LinkLabel_VMTPauseOscServer)
        Me.Panel1.Controls.Add(Me.LinkLabel_RemoteStartSocket)
        Me.Panel1.Controls.Add(Me.LinkLabel_VMTStartOscServer)
        Me.Panel1.Controls.Add(Me.LinkLabel_Updates)
        Me.Panel1.Controls.Add(Me.LinkLabel_Github)
        Me.Panel1.Controls.Add(Me.Label_Version)
        Me.Panel1.Controls.Add(Me.LinkLabel_FactoryResetService)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.LinkLabel_InstallCameraDrivers)
        Me.Panel1.Controls.Add(Me.LinkLabel_ControllersVMT)
        Me.Panel1.Controls.Add(Me.LinkLabel_ControllersAttachments)
        Me.Panel1.Controls.Add(Me.LinkLabel_ControllersRemote)
        Me.Panel1.Controls.Add(Me.LinkLabel_ControllersGeneral)
        Me.Panel1.Controls.Add(Me.LinkLabel_Trackers)
        Me.Panel1.Controls.Add(Me.LinkLabel_HMDs)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.LinkLabel_Controllers)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(229, 761)
        Me.Panel1.TabIndex = 2
        '
        'LinkLabel_StartPage
        '
        Me.LinkLabel_StartPage.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_StartPage.AutoSize = True
        Me.LinkLabel_StartPage.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_StartPage.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_StartPage.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_StartPage.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.LinkLabel_StartPage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_StartPage.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_StartPage.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_StartPage.Location = New System.Drawing.Point(17, 70)
        Me.LinkLabel_StartPage.Margin = New System.Windows.Forms.Padding(8, 16, 3, 0)
        Me.LinkLabel_StartPage.Name = "LinkLabel_StartPage"
        Me.LinkLabel_StartPage.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_StartPage.Size = New System.Drawing.Size(131, 19)
        Me.LinkLabel_StartPage.TabIndex = 24
        Me.LinkLabel_StartPage.TabStop = True
        Me.LinkLabel_StartPage.Text = "      Service Management"
        Me.LinkLabel_StartPage.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_RunSteamVR
        '
        Me.LinkLabel_RunSteamVR.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_RunSteamVR.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel_RunSteamVR.AutoSize = True
        Me.LinkLabel_RunSteamVR.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_RunSteamVR.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_RunSteamVR.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunSteamVR.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5323_16x16_32
        Me.LinkLabel_RunSteamVR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_RunSteamVR.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_RunSteamVR.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunSteamVR.Location = New System.Drawing.Point(17, 654)
        Me.LinkLabel_RunSteamVR.Margin = New System.Windows.Forms.Padding(8, 3, 3, 0)
        Me.LinkLabel_RunSteamVR.Name = "LinkLabel_RunSteamVR"
        Me.LinkLabel_RunSteamVR.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_RunSteamVR.Size = New System.Drawing.Size(110, 19)
        Me.LinkLabel_RunSteamVR.TabIndex = 23
        Me.LinkLabel_RunSteamVR.TabStop = True
        Me.LinkLabel_RunSteamVR.Text = "      Launch SteamVR"
        Me.LinkLabel_RunSteamVR.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel1LinkLabel_VMTPauseOscServer
        '
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.AutoSize = True
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5315_16x16_32
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Location = New System.Drawing.Point(57, 394)
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Margin = New System.Windows.Forms.Padding(48, 8, 3, 0)
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Name = "LinkLabel1LinkLabel_VMTPauseOscServer"
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Size = New System.Drawing.Size(114, 19)
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.TabIndex = 22
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.TabStop = True
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Text = "      Pause OSC Server"
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_RemoteStartSocket
        '
        Me.LinkLabel_RemoteStartSocket.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_RemoteStartSocket.AutoSize = True
        Me.LinkLabel_RemoteStartSocket.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_RemoteStartSocket.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_RemoteStartSocket.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_RemoteStartSocket.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.LinkLabel_RemoteStartSocket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_RemoteStartSocket.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_RemoteStartSocket.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RemoteStartSocket.Location = New System.Drawing.Point(57, 286)
        Me.LinkLabel_RemoteStartSocket.Margin = New System.Windows.Forms.Padding(48, 8, 3, 0)
        Me.LinkLabel_RemoteStartSocket.Name = "LinkLabel_RemoteStartSocket"
        Me.LinkLabel_RemoteStartSocket.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_RemoteStartSocket.Size = New System.Drawing.Size(86, 19)
        Me.LinkLabel_RemoteStartSocket.TabIndex = 21
        Me.LinkLabel_RemoteStartSocket.TabStop = True
        Me.LinkLabel_RemoteStartSocket.Text = "      Start Socket"
        Me.LinkLabel_RemoteStartSocket.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_VMTStartOscServer
        '
        Me.LinkLabel_VMTStartOscServer.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_VMTStartOscServer.AutoSize = True
        Me.LinkLabel_VMTStartOscServer.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_VMTStartOscServer.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_VMTStartOscServer.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_VMTStartOscServer.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.LinkLabel_VMTStartOscServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_VMTStartOscServer.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_VMTStartOscServer.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_VMTStartOscServer.Location = New System.Drawing.Point(57, 367)
        Me.LinkLabel_VMTStartOscServer.Margin = New System.Windows.Forms.Padding(48, 8, 3, 0)
        Me.LinkLabel_VMTStartOscServer.Name = "LinkLabel_VMTStartOscServer"
        Me.LinkLabel_VMTStartOscServer.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_VMTStartOscServer.Size = New System.Drawing.Size(108, 19)
        Me.LinkLabel_VMTStartOscServer.TabIndex = 20
        Me.LinkLabel_VMTStartOscServer.TabStop = True
        Me.LinkLabel_VMTStartOscServer.Text = "      Start OSC Server"
        Me.LinkLabel_VMTStartOscServer.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_Updates
        '
        Me.LinkLabel_Updates.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_Updates.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel_Updates.AutoSize = True
        Me.LinkLabel_Updates.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_Updates.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_Updates.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_Updates.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_16x16_32
        Me.LinkLabel_Updates.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_Updates.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_Updates.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_Updates.Location = New System.Drawing.Point(17, 698)
        Me.LinkLabel_Updates.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
        Me.LinkLabel_Updates.Name = "LinkLabel_Updates"
        Me.LinkLabel_Updates.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_Updates.Size = New System.Drawing.Size(122, 19)
        Me.LinkLabel_Updates.TabIndex = 19
        Me.LinkLabel_Updates.TabStop = True
        Me.LinkLabel_Updates.Text = "      Check For Updates"
        Me.LinkLabel_Updates.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_Github
        '
        Me.LinkLabel_Github.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_Github.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel_Github.AutoSize = True
        Me.LinkLabel_Github.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_Github.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_Github.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_Github.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netcenter_7_16x16_32
        Me.LinkLabel_Github.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_Github.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_Github.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_Github.Location = New System.Drawing.Point(17, 676)
        Me.LinkLabel_Github.Margin = New System.Windows.Forms.Padding(8, 3, 3, 0)
        Me.LinkLabel_Github.Name = "LinkLabel_Github"
        Me.LinkLabel_Github.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_Github.Size = New System.Drawing.Size(62, 19)
        Me.LinkLabel_Github.TabIndex = 18
        Me.LinkLabel_Github.TabStop = True
        Me.LinkLabel_Github.Text = "      GitHub"
        Me.LinkLabel_Github.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Label_Version
        '
        Me.Label_Version.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_Version.AutoSize = True
        Me.Label_Version.BackColor = System.Drawing.Color.Transparent
        Me.Label_Version.ForeColor = System.Drawing.Color.Black
        Me.Label_Version.Location = New System.Drawing.Point(17, 723)
        Me.Label_Version.Margin = New System.Windows.Forms.Padding(8, 3, 3, 16)
        Me.Label_Version.Name = "Label_Version"
        Me.Label_Version.Size = New System.Drawing.Size(66, 13)
        Me.Label_Version.TabIndex = 2
        Me.Label_Version.Text = "Version: 1.0"
        '
        'LinkLabel_FactoryResetService
        '
        Me.LinkLabel_FactoryResetService.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_FactoryResetService.AutoSize = True
        Me.LinkLabel_FactoryResetService.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_FactoryResetService.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_FactoryResetService.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_FactoryResetService.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.LinkLabel_FactoryResetService.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_FactoryResetService.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_FactoryResetService.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_FactoryResetService.Location = New System.Drawing.Point(17, 555)
        Me.LinkLabel_FactoryResetService.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel_FactoryResetService.Name = "LinkLabel_FactoryResetService"
        Me.LinkLabel_FactoryResetService.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_FactoryResetService.Size = New System.Drawing.Size(182, 19)
        Me.LinkLabel_FactoryResetService.TabIndex = 13
        Me.LinkLabel_FactoryResetService.TabStop = True
        Me.LinkLabel_FactoryResetService.Text = "      Factory Reset PSMoveServiceEx"
        Me.LinkLabel_FactoryResetService.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(17, 499)
        Me.Label4.Margin = New System.Windows.Forms.Padding(8, 32, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Troubleshooting"
        '
        'LinkLabel_InstallCameraDrivers
        '
        Me.LinkLabel_InstallCameraDrivers.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_InstallCameraDrivers.AutoSize = True
        Me.LinkLabel_InstallCameraDrivers.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_InstallCameraDrivers.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_InstallCameraDrivers.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_InstallCameraDrivers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.DevicePairing_6101_16x16_32
        Me.LinkLabel_InstallCameraDrivers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_InstallCameraDrivers.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_InstallCameraDrivers.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_InstallCameraDrivers.Location = New System.Drawing.Point(17, 528)
        Me.LinkLabel_InstallCameraDrivers.Margin = New System.Windows.Forms.Padding(8, 16, 3, 0)
        Me.LinkLabel_InstallCameraDrivers.Name = "LinkLabel_InstallCameraDrivers"
        Me.LinkLabel_InstallCameraDrivers.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_InstallCameraDrivers.Size = New System.Drawing.Size(173, 19)
        Me.LinkLabel_InstallCameraDrivers.TabIndex = 11
        Me.LinkLabel_InstallCameraDrivers.TabStop = True
        Me.LinkLabel_InstallCameraDrivers.Text = "      Install Playstation Eye Drivers"
        Me.LinkLabel_InstallCameraDrivers.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_ControllersVMT
        '
        Me.LinkLabel_ControllersVMT.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ControllersVMT.AutoSize = True
        Me.LinkLabel_ControllersVMT.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_ControllersVMT.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_ControllersVMT.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersVMT.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Unpin_16x16_32
        Me.LinkLabel_ControllersVMT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_ControllersVMT.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_ControllersVMT.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersVMT.Location = New System.Drawing.Point(32, 340)
        Me.LinkLabel_ControllersVMT.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.LinkLabel_ControllersVMT.Name = "LinkLabel_ControllersVMT"
        Me.LinkLabel_ControllersVMT.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_ControllersVMT.Size = New System.Drawing.Size(139, 19)
        Me.LinkLabel_ControllersVMT.TabIndex = 6
        Me.LinkLabel_ControllersVMT.TabStop = True
        Me.LinkLabel_ControllersVMT.Text = "      Virtual Motion Tracker"
        Me.LinkLabel_ControllersVMT.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_ControllersAttachments
        '
        Me.LinkLabel_ControllersAttachments.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ControllersAttachments.AutoSize = True
        Me.LinkLabel_ControllersAttachments.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_ControllersAttachments.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_ControllersAttachments.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersAttachments.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Unpin_16x16_32
        Me.LinkLabel_ControllersAttachments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_ControllersAttachments.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_ControllersAttachments.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersAttachments.Location = New System.Drawing.Point(32, 313)
        Me.LinkLabel_ControllersAttachments.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.LinkLabel_ControllersAttachments.Name = "LinkLabel_ControllersAttachments"
        Me.LinkLabel_ControllersAttachments.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_ControllersAttachments.Size = New System.Drawing.Size(144, 19)
        Me.LinkLabel_ControllersAttachments.TabIndex = 5
        Me.LinkLabel_ControllersAttachments.TabStop = True
        Me.LinkLabel_ControllersAttachments.Text = "      Controller Attachments"
        Me.LinkLabel_ControllersAttachments.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_ControllersRemote
        '
        Me.LinkLabel_ControllersRemote.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ControllersRemote.AutoSize = True
        Me.LinkLabel_ControllersRemote.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_ControllersRemote.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_ControllersRemote.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersRemote.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Unpin_16x16_32
        Me.LinkLabel_ControllersRemote.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_ControllersRemote.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_ControllersRemote.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersRemote.Location = New System.Drawing.Point(32, 259)
        Me.LinkLabel_ControllersRemote.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.LinkLabel_ControllersRemote.Name = "LinkLabel_ControllersRemote"
        Me.LinkLabel_ControllersRemote.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_ControllersRemote.Size = New System.Drawing.Size(105, 19)
        Me.LinkLabel_ControllersRemote.TabIndex = 4
        Me.LinkLabel_ControllersRemote.TabStop = True
        Me.LinkLabel_ControllersRemote.Text = "      Remote Devices"
        Me.LinkLabel_ControllersRemote.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_ControllersGeneral
        '
        Me.LinkLabel_ControllersGeneral.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ControllersGeneral.AutoSize = True
        Me.LinkLabel_ControllersGeneral.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_ControllersGeneral.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_ControllersGeneral.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersGeneral.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Unpin_16x16_32
        Me.LinkLabel_ControllersGeneral.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_ControllersGeneral.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_ControllersGeneral.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersGeneral.Location = New System.Drawing.Point(32, 232)
        Me.LinkLabel_ControllersGeneral.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.LinkLabel_ControllersGeneral.Name = "LinkLabel_ControllersGeneral"
        Me.LinkLabel_ControllersGeneral.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_ControllersGeneral.Size = New System.Drawing.Size(65, 19)
        Me.LinkLabel_ControllersGeneral.TabIndex = 3
        Me.LinkLabel_ControllersGeneral.TabStop = True
        Me.LinkLabel_ControllersGeneral.Text = "      General"
        Me.LinkLabel_ControllersGeneral.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_Trackers
        '
        Me.LinkLabel_Trackers.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_Trackers.AutoSize = True
        Me.LinkLabel_Trackers.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_Trackers.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_Trackers.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_Trackers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.LinkLabel_Trackers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_Trackers.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_Trackers.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_Trackers.Location = New System.Drawing.Point(17, 448)
        Me.LinkLabel_Trackers.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel_Trackers.Name = "LinkLabel_Trackers"
        Me.LinkLabel_Trackers.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_Trackers.Size = New System.Drawing.Size(103, 19)
        Me.LinkLabel_Trackers.TabIndex = 2
        Me.LinkLabel_Trackers.TabStop = True
        Me.LinkLabel_Trackers.Text = "      Virtual Trackers"
        Me.LinkLabel_Trackers.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_HMDs
        '
        Me.LinkLabel_HMDs.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_HMDs.AutoSize = True
        Me.LinkLabel_HMDs.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_HMDs.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_HMDs.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_HMDs.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.LinkLabel_HMDs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_HMDs.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_HMDs.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_HMDs.Location = New System.Drawing.Point(17, 421)
        Me.LinkLabel_HMDs.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel_HMDs.Name = "LinkLabel_HMDs"
        Me.LinkLabel_HMDs.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_HMDs.Size = New System.Drawing.Size(168, 19)
        Me.LinkLabel_HMDs.TabIndex = 1
        Me.LinkLabel_HMDs.TabStop = True
        Me.LinkLabel_HMDs.Text = "      Virtual Head Mount Devices"
        Me.LinkLabel_HMDs.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(228, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1, 761)
        Me.Panel3.TabIndex = 0
        '
        'LinkLabel_Controllers
        '
        Me.LinkLabel_Controllers.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_Controllers.AutoSize = True
        Me.LinkLabel_Controllers.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_Controllers.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_Controllers.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_Controllers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.LinkLabel_Controllers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_Controllers.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_Controllers.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_Controllers.Location = New System.Drawing.Point(17, 205)
        Me.LinkLabel_Controllers.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel_Controllers.Name = "LinkLabel_Controllers"
        Me.LinkLabel_Controllers.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_Controllers.Size = New System.Drawing.Size(119, 19)
        Me.LinkLabel_Controllers.TabIndex = 0
        Me.LinkLabel_Controllers.TabStop = True
        Me.LinkLabel_Controllers.Text = "      Virtual Controllers"
        Me.LinkLabel_Controllers.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(17, 41)
        Me.Label2.Margin = New System.Windows.Forms.Padding(8, 32, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Navigation"
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1055, 761)
        Me.Controls.Add(Me.Panel_Pages)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1000, 600)
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PSMoveServiceEx - Virtual Device Manager"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Pages As Panel
    Friend WithEvents Label_Version As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents LinkLabel_Controllers As LinkLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents LinkLabel_HMDs As LinkLabel
    Friend WithEvents LinkLabel_Trackers As LinkLabel
    Friend WithEvents LinkLabel_ControllersRemote As LinkLabel
    Friend WithEvents LinkLabel_ControllersGeneral As LinkLabel
    Friend WithEvents LinkLabel_ControllersVMT As LinkLabel
    Friend WithEvents LinkLabel_ControllersAttachments As LinkLabel
    Friend WithEvents LinkLabel_RunPSMS As LinkLabel
    Friend WithEvents LinkLabel_RunPSMSTool As LinkLabel
    Friend WithEvents LinkLabel_InstallCameraDrivers As LinkLabel
    Friend WithEvents Label4 As Label
    Friend WithEvents LinkLabel_FactoryResetService As LinkLabel
    Friend WithEvents LinkLabel_RestartPSMS As LinkLabel
    Friend WithEvents LinkLabel_StopPSMS As LinkLabel
    Friend WithEvents ToolTip_Service As ToolTip
    Friend WithEvents LinkLabel_Github As LinkLabel
    Friend WithEvents LinkLabel_Updates As LinkLabel
    Friend WithEvents LinkLabel_VMTStartOscServer As LinkLabel
    Friend WithEvents LinkLabel_RemoteStartSocket As LinkLabel
    Friend WithEvents LinkLabel1LinkLabel_VMTPauseOscServer As LinkLabel
    Friend WithEvents LinkLabel_RunSteamVR As LinkLabel
    Friend WithEvents LinkLabel_StartPage As LinkLabel
End Class
