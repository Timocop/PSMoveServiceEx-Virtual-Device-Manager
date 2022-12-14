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
        Me.TableLayoutPanel_Title = New System.Windows.Forms.TableLayoutPanel()
        Me.Label_MainTitle = New System.Windows.Forms.Label()
        Me.Label_MainText = New System.Windows.Forms.Label()
        Me.Label_Version = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LinkLabel_SetServicePath = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_RestartPSMS = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_StopPSMS = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_UninstallCameraDrivers = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_FactoryResetService = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LinkLabel_InstallCameraDrivers = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_RunPSMSTool = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_RunPSMS = New System.Windows.Forms.LinkLabel()
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
        Me.ToolTip_Service = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel_Pages.SuspendLayout()
        Me.TableLayoutPanel_Title.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Pages
        '
        Me.Panel_Pages.AutoScroll = True
        Me.Panel_Pages.Controls.Add(Me.TableLayoutPanel_Title)
        Me.Panel_Pages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Pages.Location = New System.Drawing.Point(229, 0)
        Me.Panel_Pages.Name = "Panel_Pages"
        Me.Panel_Pages.Size = New System.Drawing.Size(826, 761)
        Me.Panel_Pages.TabIndex = 1
        '
        'TableLayoutPanel_Title
        '
        Me.TableLayoutPanel_Title.ColumnCount = 3
        Me.TableLayoutPanel_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 500.0!))
        Me.TableLayoutPanel_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Title.Controls.Add(Me.Label_MainTitle, 1, 1)
        Me.TableLayoutPanel_Title.Controls.Add(Me.Label_MainText, 1, 2)
        Me.TableLayoutPanel_Title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Title.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Title.Name = "TableLayoutPanel_Title"
        Me.TableLayoutPanel_Title.RowCount = 4
        Me.TableLayoutPanel_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Title.Size = New System.Drawing.Size(826, 761)
        Me.TableLayoutPanel_Title.TabIndex = 2
        '
        'Label_MainTitle
        '
        Me.Label_MainTitle.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_MainTitle.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label_MainTitle.Location = New System.Drawing.Point(166, 280)
        Me.Label_MainTitle.Name = "Label_MainTitle"
        Me.Label_MainTitle.Size = New System.Drawing.Size(494, 100)
        Me.Label_MainTitle.TabIndex = 0
        Me.Label_MainTitle.Text = "Welcome to the Virtual Device Manager"
        Me.Label_MainTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label_MainText
        '
        Me.Label_MainText.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_MainText.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label_MainText.Location = New System.Drawing.Point(166, 380)
        Me.Label_MainText.Name = "Label_MainText"
        Me.Label_MainText.Size = New System.Drawing.Size(494, 100)
        Me.Label_MainText.TabIndex = 1
        Me.Label_MainText.Text = "Use the sidebar on the left for navigation."
        Me.Label_MainText.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label_Version
        '
        Me.Label_Version.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_Version.AutoSize = True
        Me.Label_Version.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_Version.Location = New System.Drawing.Point(17, 723)
        Me.Label_Version.Margin = New System.Windows.Forms.Padding(8, 3, 3, 16)
        Me.Label_Version.Name = "Label_Version"
        Me.Label_Version.Size = New System.Drawing.Size(66, 13)
        Me.Label_Version.TabIndex = 2
        Me.Label_Version.Text = "Version: 1.0"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.LinkLabel_SetServicePath)
        Me.Panel1.Controls.Add(Me.Label_Version)
        Me.Panel1.Controls.Add(Me.LinkLabel_RestartPSMS)
        Me.Panel1.Controls.Add(Me.LinkLabel_StopPSMS)
        Me.Panel1.Controls.Add(Me.LinkLabel_UninstallCameraDrivers)
        Me.Panel1.Controls.Add(Me.LinkLabel_FactoryResetService)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.LinkLabel_InstallCameraDrivers)
        Me.Panel1.Controls.Add(Me.LinkLabel_RunPSMSTool)
        Me.Panel1.Controls.Add(Me.LinkLabel_RunPSMS)
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
        Me.Panel1.Size = New System.Drawing.Size(229, 761)
        Me.Panel1.TabIndex = 2
        '
        'LinkLabel_SetServicePath
        '
        Me.LinkLabel_SetServicePath.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_SetServicePath.AutoSize = True
        Me.LinkLabel_SetServicePath.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_SetServicePath.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_SetServicePath.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5332_16x16_32
        Me.LinkLabel_SetServicePath.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_SetServicePath.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_SetServicePath.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_SetServicePath.Location = New System.Drawing.Point(17, 149)
        Me.LinkLabel_SetServicePath.Margin = New System.Windows.Forms.Padding(8, 3, 3, 0)
        Me.LinkLabel_SetServicePath.Name = "LinkLabel_SetServicePath"
        Me.LinkLabel_SetServicePath.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_SetServicePath.Size = New System.Drawing.Size(114, 19)
        Me.LinkLabel_SetServicePath.TabIndex = 17
        Me.LinkLabel_SetServicePath.TabStop = True
        Me.LinkLabel_SetServicePath.Text = "      Set Service Path..."
        Me.LinkLabel_SetServicePath.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_RestartPSMS
        '
        Me.LinkLabel_RestartPSMS.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_RestartPSMS.AutoSize = True
        Me.LinkLabel_RestartPSMS.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_RestartPSMS.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_RestartPSMS.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16739_16x16_32
        Me.LinkLabel_RestartPSMS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_RestartPSMS.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_RestartPSMS.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RestartPSMS.Location = New System.Drawing.Point(17, 105)
        Me.LinkLabel_RestartPSMS.Margin = New System.Windows.Forms.Padding(8, 3, 3, 0)
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
        Me.LinkLabel_StopPSMS.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_StopPSMS.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_StopPSMS.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5337_16x16_32
        Me.LinkLabel_StopPSMS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_StopPSMS.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_StopPSMS.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_StopPSMS.Location = New System.Drawing.Point(17, 83)
        Me.LinkLabel_StopPSMS.Margin = New System.Windows.Forms.Padding(8, 3, 3, 0)
        Me.LinkLabel_StopPSMS.Name = "LinkLabel_StopPSMS"
        Me.LinkLabel_StopPSMS.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_StopPSMS.Size = New System.Drawing.Size(87, 19)
        Me.LinkLabel_StopPSMS.TabIndex = 15
        Me.LinkLabel_StopPSMS.TabStop = True
        Me.LinkLabel_StopPSMS.Text = "      Stop Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_StopPSMS, "ToolTip")
        Me.LinkLabel_StopPSMS.VisitedLinkColor = System.Drawing.Color.Navy
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
        Me.LinkLabel_UninstallCameraDrivers.Location = New System.Drawing.Point(17, 491)
        Me.LinkLabel_UninstallCameraDrivers.Margin = New System.Windows.Forms.Padding(8, 3, 3, 0)
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
        Me.LinkLabel_FactoryResetService.Location = New System.Drawing.Point(17, 513)
        Me.LinkLabel_FactoryResetService.Margin = New System.Windows.Forms.Padding(8, 3, 3, 0)
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
        Me.Label4.Location = New System.Drawing.Point(17, 440)
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
        Me.LinkLabel_InstallCameraDrivers.Location = New System.Drawing.Point(17, 469)
        Me.LinkLabel_InstallCameraDrivers.Margin = New System.Windows.Forms.Padding(8, 16, 3, 0)
        Me.LinkLabel_InstallCameraDrivers.Name = "LinkLabel_InstallCameraDrivers"
        Me.LinkLabel_InstallCameraDrivers.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_InstallCameraDrivers.Size = New System.Drawing.Size(173, 19)
        Me.LinkLabel_InstallCameraDrivers.TabIndex = 11
        Me.LinkLabel_InstallCameraDrivers.TabStop = True
        Me.LinkLabel_InstallCameraDrivers.Text = "      Install Playstation Eye Drivers"
        Me.LinkLabel_InstallCameraDrivers.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel_RunPSMSTool
        '
        Me.LinkLabel_RunPSMSTool.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_RunPSMSTool.AutoSize = True
        Me.LinkLabel_RunPSMSTool.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_RunPSMSTool.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunPSMSTool.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.mmcshext_128_16x16_32
        Me.LinkLabel_RunPSMSTool.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_RunPSMSTool.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_RunPSMSTool.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunPSMSTool.Location = New System.Drawing.Point(17, 127)
        Me.LinkLabel_RunPSMSTool.Margin = New System.Windows.Forms.Padding(8, 3, 3, 0)
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
        Me.LinkLabel_RunPSMS.DisabledLinkColor = System.Drawing.Color.Gray
        Me.LinkLabel_RunPSMS.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunPSMS.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.LinkLabel_RunPSMS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_RunPSMS.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_RunPSMS.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_RunPSMS.Location = New System.Drawing.Point(17, 61)
        Me.LinkLabel_RunPSMS.Margin = New System.Windows.Forms.Padding(8, 16, 3, 0)
        Me.LinkLabel_RunPSMS.Name = "LinkLabel_RunPSMS"
        Me.LinkLabel_RunPSMS.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.LinkLabel_RunPSMS.Size = New System.Drawing.Size(84, 19)
        Me.LinkLabel_RunPSMS.TabIndex = 8
        Me.LinkLabel_RunPSMS.TabStop = True
        Me.LinkLabel_RunPSMS.Text = "      Run Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_RunPSMS, "ToolTip")
        Me.LinkLabel_RunPSMS.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(17, 32)
        Me.Label3.Margin = New System.Windows.Forms.Padding(8, 32, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "PSMoveServiceEx"
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
        Me.LinkLabel_ControllersVMT.Location = New System.Drawing.Point(32, 326)
        Me.LinkLabel_ControllersVMT.Margin = New System.Windows.Forms.Padding(32, 3, 3, 3)
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
        Me.LinkLabel_ControllersAttachments.Location = New System.Drawing.Point(32, 301)
        Me.LinkLabel_ControllersAttachments.Margin = New System.Windows.Forms.Padding(32, 3, 3, 3)
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
        Me.LinkLabel_ControllersRemote.Location = New System.Drawing.Point(32, 276)
        Me.LinkLabel_ControllersRemote.Margin = New System.Windows.Forms.Padding(32, 3, 3, 3)
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
        Me.LinkLabel_ControllersGeneral.Location = New System.Drawing.Point(32, 251)
        Me.LinkLabel_ControllersGeneral.Margin = New System.Windows.Forms.Padding(32, 3, 3, 3)
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
        Me.LinkLabel_Trackers.Location = New System.Drawing.Point(17, 389)
        Me.LinkLabel_Trackers.Margin = New System.Windows.Forms.Padding(8, 3, 3, 0)
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
        Me.LinkLabel_HMDs.Location = New System.Drawing.Point(17, 351)
        Me.LinkLabel_HMDs.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
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
        Me.Panel3.Size = New System.Drawing.Size(1, 761)
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
        Me.LinkLabel_Controllers.Location = New System.Drawing.Point(17, 229)
        Me.LinkLabel_Controllers.Margin = New System.Windows.Forms.Padding(8, 16, 3, 0)
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
        Me.Label2.Location = New System.Drawing.Point(17, 200)
        Me.Label2.Margin = New System.Windows.Forms.Padding(8, 32, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(144, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Virtual Devices Navigation"
        '
        'ToolTip_Service
        '
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
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PSMoveServiceEx - Virtual Device Manager"
        Me.Panel_Pages.ResumeLayout(False)
        Me.TableLayoutPanel_Title.ResumeLayout(False)
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
    Friend WithEvents Label3 As Label
    Friend WithEvents LinkLabel_RunPSMSTool As LinkLabel
    Friend WithEvents LinkLabel_InstallCameraDrivers As LinkLabel
    Friend WithEvents Label4 As Label
    Friend WithEvents LinkLabel_FactoryResetService As LinkLabel
    Friend WithEvents LinkLabel_UninstallCameraDrivers As LinkLabel
    Friend WithEvents LinkLabel_RestartPSMS As LinkLabel
    Friend WithEvents LinkLabel_StopPSMS As LinkLabel
    Friend WithEvents Label_MainText As Label
    Friend WithEvents Label_MainTitle As Label
    Friend WithEvents TableLayoutPanel_Title As TableLayoutPanel
    Friend WithEvents LinkLabel_SetServicePath As LinkLabel
    Friend WithEvents ToolTip_Service As ToolTip
End Class
