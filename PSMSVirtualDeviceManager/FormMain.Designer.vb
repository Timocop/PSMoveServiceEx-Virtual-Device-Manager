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
        Me.Label_PsvrStatus = New System.Windows.Forms.LinkLabel()
        Me.Label_RemoteDeviceStatus = New System.Windows.Forms.LinkLabel()
        Me.Label_ServiceStatus = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_PSVR = New System.Windows.Forms.LinkLabel()
        Me.Label_VmtStatus = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_VMT = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_StartPage = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_RunSteamVR = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1LinkLabel_VMTPauseOscServer = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_RemoteStartSocket = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_VMTStartOscServer = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_Updates = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_Github = New System.Windows.Forms.LinkLabel()
        Me.Label_Version = New System.Windows.Forms.Label()
        Me.LinkLabel_Trackers = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_HMDs = New System.Windows.Forms.LinkLabel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LinkLabel_Controllers = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LinkLabel_PlayCalibStatus = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_PlayCalibStart = New System.Windows.Forms.LinkLabel()
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
        Me.LinkLabel_RestartPSMS.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_RestartPSMS.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RestartPSMS.Location = New System.Drawing.Point(48, 178)
        Me.LinkLabel_RestartPSMS.Margin = New System.Windows.Forms.Padding(48, 8, 3, 0)
        Me.LinkLabel_RestartPSMS.Name = "LinkLabel_RestartPSMS"
        Me.LinkLabel_RestartPSMS.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_RestartPSMS.Size = New System.Drawing.Size(99, 19)
        Me.LinkLabel_RestartPSMS.TabIndex = 16
        Me.LinkLabel_RestartPSMS.TabStop = True
        Me.LinkLabel_RestartPSMS.Text = "Restart Service"
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
        Me.LinkLabel_StopPSMS.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_StopPSMS.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_StopPSMS.Location = New System.Drawing.Point(48, 151)
        Me.LinkLabel_StopPSMS.Margin = New System.Windows.Forms.Padding(48, 8, 3, 0)
        Me.LinkLabel_StopPSMS.Name = "LinkLabel_StopPSMS"
        Me.LinkLabel_StopPSMS.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_StopPSMS.Size = New System.Drawing.Size(87, 19)
        Me.LinkLabel_StopPSMS.TabIndex = 15
        Me.LinkLabel_StopPSMS.TabStop = True
        Me.LinkLabel_StopPSMS.Text = "Stop Service"
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
        Me.LinkLabel_RunPSMSTool.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_RunPSMSTool.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunPSMSTool.Location = New System.Drawing.Point(48, 205)
        Me.LinkLabel_RunPSMSTool.Margin = New System.Windows.Forms.Padding(48, 8, 3, 0)
        Me.LinkLabel_RunPSMSTool.Name = "LinkLabel_RunPSMSTool"
        Me.LinkLabel_RunPSMSTool.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_RunPSMSTool.Size = New System.Drawing.Size(147, 19)
        Me.LinkLabel_RunPSMSTool.TabIndex = 10
        Me.LinkLabel_RunPSMSTool.TabStop = True
        Me.LinkLabel_RunPSMSTool.Text = "Run Service Config Tool"
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
        Me.LinkLabel_RunPSMS.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_RunPSMS.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunPSMS.Location = New System.Drawing.Point(48, 124)
        Me.LinkLabel_RunPSMS.Margin = New System.Windows.Forms.Padding(48, 8, 3, 0)
        Me.LinkLabel_RunPSMS.Name = "LinkLabel_RunPSMS"
        Me.LinkLabel_RunPSMS.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_RunPSMS.Size = New System.Drawing.Size(84, 19)
        Me.LinkLabel_RunPSMS.TabIndex = 8
        Me.LinkLabel_RunPSMS.TabStop = True
        Me.LinkLabel_RunPSMS.Text = "Run Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_RunPSMS, "ToolTip")
        Me.LinkLabel_RunPSMS.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.AutoScrollMinSize = New System.Drawing.Size(0, 750)
        Me.Panel1.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.LinkLabel_PlayCalibStatus)
        Me.Panel1.Controls.Add(Me.LinkLabel_PlayCalibStart)
        Me.Panel1.Controls.Add(Me.Label_PsvrStatus)
        Me.Panel1.Controls.Add(Me.Label_RemoteDeviceStatus)
        Me.Panel1.Controls.Add(Me.Label_ServiceStatus)
        Me.Panel1.Controls.Add(Me.LinkLabel_PSVR)
        Me.Panel1.Controls.Add(Me.Label_VmtStatus)
        Me.Panel1.Controls.Add(Me.LinkLabel_VMT)
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
        'Label_PsvrStatus
        '
        Me.Label_PsvrStatus.ActiveLinkColor = System.Drawing.Color.Black
        Me.Label_PsvrStatus.AutoSize = True
        Me.Label_PsvrStatus.BackColor = System.Drawing.Color.Transparent
        Me.Label_PsvrStatus.DisabledLinkColor = System.Drawing.Color.Black
        Me.Label_PsvrStatus.ForeColor = System.Drawing.Color.Black
        Me.Label_PsvrStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Status_WHITE_16
        Me.Label_PsvrStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label_PsvrStatus.LinkArea = New System.Windows.Forms.LinkArea(0, 0)
        Me.Label_PsvrStatus.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Label_PsvrStatus.LinkColor = System.Drawing.Color.Black
        Me.Label_PsvrStatus.Location = New System.Drawing.Point(32, 267)
        Me.Label_PsvrStatus.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.Label_PsvrStatus.Name = "Label_PsvrStatus"
        Me.Label_PsvrStatus.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.Label_PsvrStatus.Size = New System.Drawing.Size(128, 19)
        Me.Label_PsvrStatus.TabIndex = 34
        Me.Label_PsvrStatus.Text = "Management Status"
        Me.Label_PsvrStatus.VisitedLinkColor = System.Drawing.Color.Black
        '
        'Label_RemoteDeviceStatus
        '
        Me.Label_RemoteDeviceStatus.ActiveLinkColor = System.Drawing.Color.Black
        Me.Label_RemoteDeviceStatus.AutoSize = True
        Me.Label_RemoteDeviceStatus.BackColor = System.Drawing.Color.Transparent
        Me.Label_RemoteDeviceStatus.DisabledLinkColor = System.Drawing.Color.Black
        Me.Label_RemoteDeviceStatus.ForeColor = System.Drawing.Color.Black
        Me.Label_RemoteDeviceStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Status_WHITE_16
        Me.Label_RemoteDeviceStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label_RemoteDeviceStatus.LinkArea = New System.Windows.Forms.LinkArea(0, 0)
        Me.Label_RemoteDeviceStatus.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Label_RemoteDeviceStatus.LinkColor = System.Drawing.Color.Black
        Me.Label_RemoteDeviceStatus.Location = New System.Drawing.Point(32, 329)
        Me.Label_RemoteDeviceStatus.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.Label_RemoteDeviceStatus.Name = "Label_RemoteDeviceStatus"
        Me.Label_RemoteDeviceStatus.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.Label_RemoteDeviceStatus.Size = New System.Drawing.Size(140, 19)
        Me.Label_RemoteDeviceStatus.TabIndex = 33
        Me.Label_RemoteDeviceStatus.Text = "Remote Devices Status"
        Me.Label_RemoteDeviceStatus.VisitedLinkColor = System.Drawing.Color.Black
        '
        'Label_ServiceStatus
        '
        Me.Label_ServiceStatus.ActiveLinkColor = System.Drawing.Color.Black
        Me.Label_ServiceStatus.AutoSize = True
        Me.Label_ServiceStatus.BackColor = System.Drawing.Color.Transparent
        Me.Label_ServiceStatus.DisabledLinkColor = System.Drawing.Color.Black
        Me.Label_ServiceStatus.ForeColor = System.Drawing.Color.Black
        Me.Label_ServiceStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Status_WHITE_16
        Me.Label_ServiceStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label_ServiceStatus.LinkArea = New System.Windows.Forms.LinkArea(0, 0)
        Me.Label_ServiceStatus.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Label_ServiceStatus.LinkColor = System.Drawing.Color.Black
        Me.Label_ServiceStatus.Location = New System.Drawing.Point(32, 97)
        Me.Label_ServiceStatus.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.Label_ServiceStatus.Name = "Label_ServiceStatus"
        Me.Label_ServiceStatus.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.Label_ServiceStatus.Size = New System.Drawing.Size(128, 19)
        Me.Label_ServiceStatus.TabIndex = 32
        Me.Label_ServiceStatus.Text = "Management Status"
        Me.Label_ServiceStatus.VisitedLinkColor = System.Drawing.Color.Black
        '
        'LinkLabel_PSVR
        '
        Me.LinkLabel_PSVR.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_PSVR.AutoSize = True
        Me.LinkLabel_PSVR.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_PSVR.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_PSVR.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_PSVR.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.LinkLabel_PSVR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_PSVR.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_PSVR.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_PSVR.Location = New System.Drawing.Point(17, 240)
        Me.LinkLabel_PSVR.Margin = New System.Windows.Forms.Padding(8, 16, 3, 0)
        Me.LinkLabel_PSVR.Name = "LinkLabel_PSVR"
        Me.LinkLabel_PSVR.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_PSVR.Size = New System.Drawing.Size(170, 19)
        Me.LinkLabel_PSVR.TabIndex = 31
        Me.LinkLabel_PSVR.TabStop = True
        Me.LinkLabel_PSVR.Text = "PlayStation VR Management"
        Me.LinkLabel_PSVR.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Label_VmtStatus
        '
        Me.Label_VmtStatus.ActiveLinkColor = System.Drawing.Color.Black
        Me.Label_VmtStatus.AutoSize = True
        Me.Label_VmtStatus.BackColor = System.Drawing.Color.Transparent
        Me.Label_VmtStatus.DisabledLinkColor = System.Drawing.Color.Black
        Me.Label_VmtStatus.ForeColor = System.Drawing.Color.Black
        Me.Label_VmtStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Status_WHITE_16
        Me.Label_VmtStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label_VmtStatus.LinkArea = New System.Windows.Forms.LinkArea(0, 0)
        Me.Label_VmtStatus.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Label_VmtStatus.LinkColor = System.Drawing.Color.Black
        Me.Label_VmtStatus.Location = New System.Drawing.Point(32, 472)
        Me.Label_VmtStatus.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.Label_VmtStatus.Name = "Label_VmtStatus"
        Me.Label_VmtStatus.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.Label_VmtStatus.Size = New System.Drawing.Size(128, 19)
        Me.Label_VmtStatus.TabIndex = 26
        Me.Label_VmtStatus.Text = "Management Status"
        Me.Label_VmtStatus.VisitedLinkColor = System.Drawing.Color.Black
        '
        'LinkLabel_VMT
        '
        Me.LinkLabel_VMT.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_VMT.AutoSize = True
        Me.LinkLabel_VMT.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_VMT.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_VMT.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_VMT.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.LinkLabel_VMT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_VMT.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_VMT.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_VMT.Location = New System.Drawing.Point(17, 445)
        Me.LinkLabel_VMT.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel_VMT.Name = "LinkLabel_VMT"
        Me.LinkLabel_VMT.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_VMT.Size = New System.Drawing.Size(139, 19)
        Me.LinkLabel_VMT.TabIndex = 25
        Me.LinkLabel_VMT.TabStop = True
        Me.LinkLabel_VMT.Text = "Virtual Motion Tracker"
        Me.LinkLabel_VMT.VisitedLinkColor = System.Drawing.Color.Navy
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
        Me.LinkLabel_StartPage.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_StartPage.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_StartPage.Location = New System.Drawing.Point(17, 70)
        Me.LinkLabel_StartPage.Margin = New System.Windows.Forms.Padding(8, 16, 3, 0)
        Me.LinkLabel_StartPage.Name = "LinkLabel_StartPage"
        Me.LinkLabel_StartPage.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_StartPage.Size = New System.Drawing.Size(131, 19)
        Me.LinkLabel_StartPage.TabIndex = 24
        Me.LinkLabel_StartPage.TabStop = True
        Me.LinkLabel_StartPage.Text = "Service Management"
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
        Me.LinkLabel_RunSteamVR.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_RunSteamVR.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunSteamVR.Location = New System.Drawing.Point(17, 654)
        Me.LinkLabel_RunSteamVR.Margin = New System.Windows.Forms.Padding(8, 3, 3, 0)
        Me.LinkLabel_RunSteamVR.Name = "LinkLabel_RunSteamVR"
        Me.LinkLabel_RunSteamVR.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_RunSteamVR.Size = New System.Drawing.Size(110, 19)
        Me.LinkLabel_RunSteamVR.TabIndex = 23
        Me.LinkLabel_RunSteamVR.TabStop = True
        Me.LinkLabel_RunSteamVR.Text = "Launch SteamVR"
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
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Location = New System.Drawing.Point(48, 526)
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Margin = New System.Windows.Forms.Padding(48, 8, 3, 0)
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Name = "LinkLabel1LinkLabel_VMTPauseOscServer"
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Size = New System.Drawing.Size(114, 19)
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.TabIndex = 22
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.TabStop = True
        Me.LinkLabel1LinkLabel_VMTPauseOscServer.Text = "Pause OSC Server"
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
        Me.LinkLabel_RemoteStartSocket.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_RemoteStartSocket.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RemoteStartSocket.Location = New System.Drawing.Point(48, 356)
        Me.LinkLabel_RemoteStartSocket.Margin = New System.Windows.Forms.Padding(48, 8, 3, 0)
        Me.LinkLabel_RemoteStartSocket.Name = "LinkLabel_RemoteStartSocket"
        Me.LinkLabel_RemoteStartSocket.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_RemoteStartSocket.Size = New System.Drawing.Size(86, 19)
        Me.LinkLabel_RemoteStartSocket.TabIndex = 21
        Me.LinkLabel_RemoteStartSocket.TabStop = True
        Me.LinkLabel_RemoteStartSocket.Text = "Start Socket"
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
        Me.LinkLabel_VMTStartOscServer.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_VMTStartOscServer.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_VMTStartOscServer.Location = New System.Drawing.Point(48, 499)
        Me.LinkLabel_VMTStartOscServer.Margin = New System.Windows.Forms.Padding(48, 8, 3, 0)
        Me.LinkLabel_VMTStartOscServer.Name = "LinkLabel_VMTStartOscServer"
        Me.LinkLabel_VMTStartOscServer.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_VMTStartOscServer.Size = New System.Drawing.Size(108, 19)
        Me.LinkLabel_VMTStartOscServer.TabIndex = 20
        Me.LinkLabel_VMTStartOscServer.TabStop = True
        Me.LinkLabel_VMTStartOscServer.Text = "Start OSC Server"
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
        Me.LinkLabel_Updates.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_Updates.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_Updates.Location = New System.Drawing.Point(17, 698)
        Me.LinkLabel_Updates.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
        Me.LinkLabel_Updates.Name = "LinkLabel_Updates"
        Me.LinkLabel_Updates.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_Updates.Size = New System.Drawing.Size(122, 19)
        Me.LinkLabel_Updates.TabIndex = 19
        Me.LinkLabel_Updates.TabStop = True
        Me.LinkLabel_Updates.Text = "Check For Updates"
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
        Me.LinkLabel_Github.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_Github.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_Github.Location = New System.Drawing.Point(17, 676)
        Me.LinkLabel_Github.Margin = New System.Windows.Forms.Padding(8, 3, 3, 0)
        Me.LinkLabel_Github.Name = "LinkLabel_Github"
        Me.LinkLabel_Github.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_Github.Size = New System.Drawing.Size(62, 19)
        Me.LinkLabel_Github.TabIndex = 18
        Me.LinkLabel_Github.TabStop = True
        Me.LinkLabel_Github.Text = "GitHub"
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
        'LinkLabel_Trackers
        '
        Me.LinkLabel_Trackers.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_Trackers.AutoSize = True
        Me.LinkLabel_Trackers.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_Trackers.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_Trackers.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_Trackers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.LinkLabel_Trackers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_Trackers.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_Trackers.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_Trackers.Location = New System.Drawing.Point(17, 418)
        Me.LinkLabel_Trackers.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel_Trackers.Name = "LinkLabel_Trackers"
        Me.LinkLabel_Trackers.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_Trackers.Size = New System.Drawing.Size(103, 19)
        Me.LinkLabel_Trackers.TabIndex = 2
        Me.LinkLabel_Trackers.TabStop = True
        Me.LinkLabel_Trackers.Text = "Virtual Trackers"
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
        Me.LinkLabel_HMDs.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_HMDs.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_HMDs.Location = New System.Drawing.Point(17, 391)
        Me.LinkLabel_HMDs.Margin = New System.Windows.Forms.Padding(8, 16, 3, 0)
        Me.LinkLabel_HMDs.Name = "LinkLabel_HMDs"
        Me.LinkLabel_HMDs.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_HMDs.Size = New System.Drawing.Size(182, 19)
        Me.LinkLabel_HMDs.TabIndex = 1
        Me.LinkLabel_HMDs.TabStop = True
        Me.LinkLabel_HMDs.Text = "Virtual Head-Mounted Devices"
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
        Me.LinkLabel_Controllers.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_Controllers.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_Controllers.Location = New System.Drawing.Point(17, 302)
        Me.LinkLabel_Controllers.Margin = New System.Windows.Forms.Padding(8, 16, 3, 0)
        Me.LinkLabel_Controllers.Name = "LinkLabel_Controllers"
        Me.LinkLabel_Controllers.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_Controllers.Size = New System.Drawing.Size(119, 19)
        Me.LinkLabel_Controllers.TabIndex = 0
        Me.LinkLabel_Controllers.TabStop = True
        Me.LinkLabel_Controllers.Text = "Virtual Controllers"
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
        'LinkLabel_PlayCalibStatus
        '
        Me.LinkLabel_PlayCalibStatus.ActiveLinkColor = System.Drawing.Color.Black
        Me.LinkLabel_PlayCalibStatus.AutoSize = True
        Me.LinkLabel_PlayCalibStatus.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_PlayCalibStatus.DisabledLinkColor = System.Drawing.Color.Black
        Me.LinkLabel_PlayCalibStatus.ForeColor = System.Drawing.Color.Black
        Me.LinkLabel_PlayCalibStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Status_WHITE_16
        Me.LinkLabel_PlayCalibStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_PlayCalibStatus.LinkArea = New System.Windows.Forms.LinkArea(0, 0)
        Me.LinkLabel_PlayCalibStatus.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_PlayCalibStatus.LinkColor = System.Drawing.Color.Black
        Me.LinkLabel_PlayCalibStatus.Location = New System.Drawing.Point(32, 553)
        Me.LinkLabel_PlayCalibStatus.Margin = New System.Windows.Forms.Padding(32, 8, 3, 0)
        Me.LinkLabel_PlayCalibStatus.Name = "LinkLabel_PlayCalibStatus"
        Me.LinkLabel_PlayCalibStatus.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_PlayCalibStatus.Size = New System.Drawing.Size(128, 19)
        Me.LinkLabel_PlayCalibStatus.TabIndex = 36
        Me.LinkLabel_PlayCalibStatus.Text = "Management Status"
        Me.LinkLabel_PlayCalibStatus.VisitedLinkColor = System.Drawing.Color.Black
        '
        'LinkLabel_PlayCalibStart
        '
        Me.LinkLabel_PlayCalibStart.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_PlayCalibStart.AutoSize = True
        Me.LinkLabel_PlayCalibStart.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel_PlayCalibStart.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_PlayCalibStart.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_PlayCalibStart.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.LinkLabel_PlayCalibStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_PlayCalibStart.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_PlayCalibStart.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_PlayCalibStart.Location = New System.Drawing.Point(48, 580)
        Me.LinkLabel_PlayCalibStart.Margin = New System.Windows.Forms.Padding(48, 8, 3, 0)
        Me.LinkLabel_PlayCalibStart.Name = "LinkLabel_PlayCalibStart"
        Me.LinkLabel_PlayCalibStart.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_PlayCalibStart.Size = New System.Drawing.Size(109, 19)
        Me.LinkLabel_PlayCalibStart.TabIndex = 35
        Me.LinkLabel_PlayCalibStart.TabStop = True
        Me.LinkLabel_PlayCalibStart.Text = "Start Calibration"
        Me.LinkLabel_PlayCalibStart.VisitedLinkColor = System.Drawing.Color.Navy
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
    Friend WithEvents LinkLabel_RunPSMS As LinkLabel
    Friend WithEvents LinkLabel_RunPSMSTool As LinkLabel
    Friend WithEvents LinkLabel_RestartPSMS As LinkLabel
    Friend WithEvents LinkLabel_StopPSMS As LinkLabel
    Friend WithEvents ToolTip_Service As ToolTip
    Friend WithEvents LinkLabel_Github As LinkLabel
    Friend WithEvents LinkLabel_Updates As LinkLabel
    Friend WithEvents LinkLabel_RemoteStartSocket As LinkLabel
    Friend WithEvents LinkLabel_RunSteamVR As LinkLabel
    Friend WithEvents LinkLabel_StartPage As LinkLabel
    Friend WithEvents LinkLabel_VMT As LinkLabel
    Friend WithEvents Label_VmtStatus As LinkLabel
    Friend WithEvents LinkLabel1LinkLabel_VMTPauseOscServer As LinkLabel
    Friend WithEvents LinkLabel_VMTStartOscServer As LinkLabel
    Friend WithEvents LinkLabel_PSVR As LinkLabel
    Friend WithEvents Label_RemoteDeviceStatus As LinkLabel
    Friend WithEvents Label_ServiceStatus As LinkLabel
    Friend WithEvents Label_PsvrStatus As LinkLabel
    Friend WithEvents LinkLabel_PlayCalibStatus As LinkLabel
    Friend WithEvents LinkLabel_PlayCalibStart As LinkLabel
End Class
