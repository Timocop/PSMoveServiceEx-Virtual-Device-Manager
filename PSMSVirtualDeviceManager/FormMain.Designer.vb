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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.Panel_Pages = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label_Version = New System.Windows.Forms.Label()
        Me.Button_RestartPSMS = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LinkLabel4 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_UninstallCameraDrivers = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_FactoryResetService = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LinkLabel_InstallCameraDrivers = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LinkLabel_ControllersVMT = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ControllersAttachments = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ControllersRemote = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ControllersGeneral = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_Trackers = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_HMDs = New System.Windows.Forms.LinkLabel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LinkLabel_Controllers = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Pages
        '
        Me.Panel_Pages.AutoScroll = True
        Me.Panel_Pages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Pages.Location = New System.Drawing.Point(229, 0)
        Me.Panel_Pages.Name = "Panel_Pages"
        Me.Panel_Pages.Size = New System.Drawing.Size(826, 697)
        Me.Panel_Pages.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.Controls.Add(Me.Label_Version)
        Me.Panel2.Controls.Add(Me.Button_RestartPSMS)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 697)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1055, 64)
        Me.Panel2.TabIndex = 2
        '
        'Label_Version
        '
        Me.Label_Version.AutoSize = True
        Me.Label_Version.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_Version.Location = New System.Drawing.Point(12, 26)
        Me.Label_Version.Name = "Label_Version"
        Me.Label_Version.Size = New System.Drawing.Size(66, 13)
        Me.Label_Version.TabIndex = 2
        Me.Label_Version.Text = "Version: 1.0"
        '
        'Button_RestartPSMS
        '
        Me.Button_RestartPSMS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_RestartPSMS.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_RestartPSMS.Location = New System.Drawing.Point(893, 16)
        Me.Button_RestartPSMS.Margin = New System.Windows.Forms.Padding(3, 16, 3, 16)
        Me.Button_RestartPSMS.Name = "Button_RestartPSMS"
        Me.Button_RestartPSMS.Size = New System.Drawing.Size(150, 32)
        Me.Button_RestartPSMS.TabIndex = 1
        Me.Button_RestartPSMS.Text = "Restart PSMoveService"
        Me.Button_RestartPSMS.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1055, 1)
        Me.Label1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.LinkLabel4)
        Me.Panel1.Controls.Add(Me.LinkLabel2)
        Me.Panel1.Controls.Add(Me.LinkLabel_UninstallCameraDrivers)
        Me.Panel1.Controls.Add(Me.LinkLabel_FactoryResetService)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.LinkLabel_InstallCameraDrivers)
        Me.Panel1.Controls.Add(Me.LinkLabel3)
        Me.Panel1.Controls.Add(Me.LinkLabel1)
        Me.Panel1.Controls.Add(Me.Label3)
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
        Me.Panel1.Size = New System.Drawing.Size(229, 697)
        Me.Panel1.TabIndex = 2
        '
        'LinkLabel4
        '
        Me.LinkLabel4.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel4.AutoSize = True
        Me.LinkLabel4.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel4.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel4.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16739_16x16_32
        Me.LinkLabel4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel4.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel4.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel4.Location = New System.Drawing.Point(17, 140)
        Me.LinkLabel4.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel4.Name = "LinkLabel4"
        Me.LinkLabel4.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel4.Size = New System.Drawing.Size(150, 19)
        Me.LinkLabel4.TabIndex = 16
        Me.LinkLabel4.TabStop = True
        Me.LinkLabel4.Text = "      Restart PSMoveServiceEx"
        Me.LinkLabel4.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel2
        '
        Me.LinkLabel2.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel2.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel2.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5337_16x16_32
        Me.LinkLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel2.Location = New System.Drawing.Point(17, 113)
        Me.LinkLabel2.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel2.Size = New System.Drawing.Size(138, 19)
        Me.LinkLabel2.TabIndex = 15
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "      Stop PSMoveServiceEx"
        Me.LinkLabel2.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_UninstallCameraDrivers
        '
        Me.LinkLabel_UninstallCameraDrivers.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_UninstallCameraDrivers.AutoSize = True
        Me.LinkLabel_UninstallCameraDrivers.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_UninstallCameraDrivers.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_UninstallCameraDrivers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5337_16x16_32
        Me.LinkLabel_UninstallCameraDrivers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_UninstallCameraDrivers.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_UninstallCameraDrivers.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_UninstallCameraDrivers.Location = New System.Drawing.Point(17, 576)
        Me.LinkLabel_UninstallCameraDrivers.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel_UninstallCameraDrivers.Name = "LinkLabel_UninstallCameraDrivers"
        Me.LinkLabel_UninstallCameraDrivers.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_UninstallCameraDrivers.Size = New System.Drawing.Size(188, 19)
        Me.LinkLabel_UninstallCameraDrivers.TabIndex = 14
        Me.LinkLabel_UninstallCameraDrivers.TabStop = True
        Me.LinkLabel_UninstallCameraDrivers.Text = "      Uninstall Playstation Eye Drivers"
        Me.LinkLabel_UninstallCameraDrivers.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_FactoryResetService
        '
        Me.LinkLabel_FactoryResetService.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_FactoryResetService.AutoSize = True
        Me.LinkLabel_FactoryResetService.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_FactoryResetService.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_FactoryResetService.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.LinkLabel_FactoryResetService.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_FactoryResetService.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_FactoryResetService.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_FactoryResetService.Location = New System.Drawing.Point(17, 603)
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
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(17, 504)
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
        Me.LinkLabel_InstallCameraDrivers.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_InstallCameraDrivers.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_InstallCameraDrivers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.DevicePairing_6101_16x16_32
        Me.LinkLabel_InstallCameraDrivers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_InstallCameraDrivers.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_InstallCameraDrivers.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_InstallCameraDrivers.Location = New System.Drawing.Point(17, 549)
        Me.LinkLabel_InstallCameraDrivers.Margin = New System.Windows.Forms.Padding(8, 32, 3, 0)
        Me.LinkLabel_InstallCameraDrivers.Name = "LinkLabel_InstallCameraDrivers"
        Me.LinkLabel_InstallCameraDrivers.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_InstallCameraDrivers.Size = New System.Drawing.Size(173, 19)
        Me.LinkLabel_InstallCameraDrivers.TabIndex = 11
        Me.LinkLabel_InstallCameraDrivers.TabStop = True
        Me.LinkLabel_InstallCameraDrivers.Text = "      Install Playstation Eye Drivers"
        Me.LinkLabel_InstallCameraDrivers.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel3
        '
        Me.LinkLabel3.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel3.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel3.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.mmcshext_128_16x16_32
        Me.LinkLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel3.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel3.Location = New System.Drawing.Point(17, 167)
        Me.LinkLabel3.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel3.Size = New System.Drawing.Size(160, 19)
        Me.LinkLabel3.TabIndex = 10
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "      Run PSMoveServiceEx Tool"
        Me.LinkLabel3.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel1
        '
        Me.LinkLabel1.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel1.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.LinkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel1.Location = New System.Drawing.Point(17, 86)
        Me.LinkLabel1.Margin = New System.Windows.Forms.Padding(8, 32, 3, 0)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel1.Size = New System.Drawing.Size(135, 19)
        Me.LinkLabel1.TabIndex = 8
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "      Run PSMoveServiceEx"
        Me.LinkLabel1.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(17, 41)
        Me.Label3.Margin = New System.Windows.Forms.Padding(8, 32, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Service"
        '
        'LinkLabel_ControllersVMT
        '
        Me.LinkLabel_ControllersVMT.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ControllersVMT.AutoSize = True
        Me.LinkLabel_ControllersVMT.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_ControllersVMT.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersVMT.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Unpin_16x16_32
        Me.LinkLabel_ControllersVMT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_ControllersVMT.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_ControllersVMT.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersVMT.Location = New System.Drawing.Point(32, 380)
        Me.LinkLabel_ControllersVMT.Margin = New System.Windows.Forms.Padding(32, 8, 3, 3)
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
        Me.LinkLabel_ControllersAttachments.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_ControllersAttachments.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersAttachments.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Unpin_16x16_32
        Me.LinkLabel_ControllersAttachments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_ControllersAttachments.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_ControllersAttachments.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersAttachments.Location = New System.Drawing.Point(32, 350)
        Me.LinkLabel_ControllersAttachments.Margin = New System.Windows.Forms.Padding(32, 8, 3, 3)
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
        Me.LinkLabel_ControllersRemote.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_ControllersRemote.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersRemote.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Unpin_16x16_32
        Me.LinkLabel_ControllersRemote.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_ControllersRemote.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_ControllersRemote.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersRemote.Location = New System.Drawing.Point(32, 320)
        Me.LinkLabel_ControllersRemote.Margin = New System.Windows.Forms.Padding(32, 8, 3, 3)
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
        Me.LinkLabel_ControllersGeneral.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_ControllersGeneral.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersGeneral.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Unpin_16x16_32
        Me.LinkLabel_ControllersGeneral.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_ControllersGeneral.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_ControllersGeneral.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_ControllersGeneral.Location = New System.Drawing.Point(32, 290)
        Me.LinkLabel_ControllersGeneral.Margin = New System.Windows.Forms.Padding(32, 8, 3, 3)
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
        Me.LinkLabel_Trackers.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_Trackers.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_Trackers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.LinkLabel_Trackers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_Trackers.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_Trackers.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_Trackers.Location = New System.Drawing.Point(17, 453)
        Me.LinkLabel_Trackers.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel_Trackers.Name = "LinkLabel_Trackers"
        Me.LinkLabel_Trackers.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_Trackers.Size = New System.Drawing.Size(148, 19)
        Me.LinkLabel_Trackers.TabIndex = 2
        Me.LinkLabel_Trackers.TabStop = True
        Me.LinkLabel_Trackers.Text = "      Manage Virtual Trackers"
        Me.LinkLabel_Trackers.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_HMDs
        '
        Me.LinkLabel_HMDs.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_HMDs.AutoSize = True
        Me.LinkLabel_HMDs.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_HMDs.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_HMDs.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.LinkLabel_HMDs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_HMDs.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_HMDs.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_HMDs.Location = New System.Drawing.Point(17, 410)
        Me.LinkLabel_HMDs.Margin = New System.Windows.Forms.Padding(8, 8, 3, 3)
        Me.LinkLabel_HMDs.Name = "LinkLabel_HMDs"
        Me.LinkLabel_HMDs.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_HMDs.Size = New System.Drawing.Size(131, 32)
        Me.LinkLabel_HMDs.TabIndex = 1
        Me.LinkLabel_HMDs.TabStop = True
        Me.LinkLabel_HMDs.Text = "      Manage Virtual" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "      Head Mount Devices"
        Me.LinkLabel_HMDs.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(228, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1, 697)
        Me.Panel3.TabIndex = 0
        '
        'LinkLabel_Controllers
        '
        Me.LinkLabel_Controllers.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_Controllers.AutoSize = True
        Me.LinkLabel_Controllers.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_Controllers.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_Controllers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.LinkLabel_Controllers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_Controllers.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_Controllers.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_Controllers.Location = New System.Drawing.Point(17, 263)
        Me.LinkLabel_Controllers.Margin = New System.Windows.Forms.Padding(8, 32, 3, 0)
        Me.LinkLabel_Controllers.Name = "LinkLabel_Controllers"
        Me.LinkLabel_Controllers.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_Controllers.Size = New System.Drawing.Size(164, 19)
        Me.LinkLabel_Controllers.TabIndex = 0
        Me.LinkLabel_Controllers.TabStop = True
        Me.LinkLabel_Controllers.Text = "      Manage Virtual Controllers"
        Me.LinkLabel_Controllers.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(17, 218)
        Me.Label2.Margin = New System.Windows.Forms.Padding(8, 32, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(144, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Virtual Devices Navigation"
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1055, 761)
        Me.Controls.Add(Me.Panel_Pages)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PSMoveServiceEx - Virtual Device Manager"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Pages As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Button_RestartPSMS As Button
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
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Label3 As Label
    Friend WithEvents LinkLabel3 As LinkLabel
    Friend WithEvents LinkLabel_InstallCameraDrivers As LinkLabel
    Friend WithEvents Label4 As Label
    Friend WithEvents LinkLabel_FactoryResetService As LinkLabel
    Friend WithEvents LinkLabel_UninstallCameraDrivers As LinkLabel
    Friend WithEvents LinkLabel4 As LinkLabel
    Friend WithEvents LinkLabel2 As LinkLabel
End Class
