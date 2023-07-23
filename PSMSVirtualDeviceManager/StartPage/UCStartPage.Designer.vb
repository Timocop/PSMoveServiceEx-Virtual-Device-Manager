<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCStartPage
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LinkLabel_ServicePath = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.LinkLabel_ServiceRun = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ServiceRestart = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ServiceStop = New System.Windows.Forms.LinkLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LinkLabel_ConfigToolClose = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality2 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.LinkLabel_ConfigToolRun = New System.Windows.Forms.LinkLabel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.LinkLabel_ServiceFactory = New System.Windows.Forms.LinkLabel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality3 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.LinkLabel_InstallDrivers = New System.Windows.Forms.LinkLabel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label_PsmsxStatus = New System.Windows.Forms.Label()
        Me.Panel_PsmsxStatus = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.ToolTip_Service = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        CType(Me.ClassPictureBoxQuality3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Controls.Add(Me.Panel12)
        Me.Panel1.Location = New System.Drawing.Point(16, 83)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(768, 268)
        Me.Panel1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel4, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel5, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 42)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(766, 224)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.LinkLabel_ServicePath)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.ClassPictureBoxQuality1)
        Me.Panel4.Controls.Add(Me.LinkLabel_ServiceRun)
        Me.Panel4.Controls.Add(Me.LinkLabel_ServiceRestart)
        Me.Panel4.Controls.Add(Me.LinkLabel_ServiceStop)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(382, 112)
        Me.Panel4.TabIndex = 1
        '
        'LinkLabel_ServicePath
        '
        Me.LinkLabel_ServicePath.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServicePath.AutoSize = True
        Me.LinkLabel_ServicePath.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServicePath.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServicePath.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServicePath.Location = New System.Drawing.Point(103, 91)
        Me.LinkLabel_ServicePath.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ServicePath.Name = "LinkLabel_ServicePath"
        Me.LinkLabel_ServicePath.Size = New System.Drawing.Size(96, 13)
        Me.LinkLabel_ServicePath.TabIndex = 19
        Me.LinkLabel_ServicePath.TabStop = True
        Me.LinkLabel_ServicePath.Text = "Set Service Path..."
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ServicePath, "Tooltip")
        Me.LinkLabel_ServicePath.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(102, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 21)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Service Control"
        '
        'ClassPictureBoxQuality1
        '
        Me.ClassPictureBoxQuality1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.devmgr_201_256x256_32
        Me.ClassPictureBoxQuality1.Location = New System.Drawing.Point(32, 19)
        Me.ClassPictureBoxQuality1.m_HighQuality = True
        Me.ClassPictureBoxQuality1.Margin = New System.Windows.Forms.Padding(32, 16, 3, 3)
        Me.ClassPictureBoxQuality1.Name = "ClassPictureBoxQuality1"
        Me.ClassPictureBoxQuality1.Size = New System.Drawing.Size(64, 64)
        Me.ClassPictureBoxQuality1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality1.TabIndex = 10
        Me.ClassPictureBoxQuality1.TabStop = False
        '
        'LinkLabel_ServiceRun
        '
        Me.LinkLabel_ServiceRun.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServiceRun.AutoSize = True
        Me.LinkLabel_ServiceRun.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRun.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServiceRun.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRun.Location = New System.Drawing.Point(103, 43)
        Me.LinkLabel_ServiceRun.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ServiceRun.Name = "LinkLabel_ServiceRun"
        Me.LinkLabel_ServiceRun.Size = New System.Drawing.Size(66, 13)
        Me.LinkLabel_ServiceRun.TabIndex = 17
        Me.LinkLabel_ServiceRun.TabStop = True
        Me.LinkLabel_ServiceRun.Text = "Run Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ServiceRun, "Tooltip")
        Me.LinkLabel_ServiceRun.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_ServiceRestart
        '
        Me.LinkLabel_ServiceRestart.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServiceRestart.AutoSize = True
        Me.LinkLabel_ServiceRestart.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRestart.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServiceRestart.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRestart.Location = New System.Drawing.Point(103, 59)
        Me.LinkLabel_ServiceRestart.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ServiceRestart.Name = "LinkLabel_ServiceRestart"
        Me.LinkLabel_ServiceRestart.Size = New System.Drawing.Size(81, 13)
        Me.LinkLabel_ServiceRestart.TabIndex = 11
        Me.LinkLabel_ServiceRestart.TabStop = True
        Me.LinkLabel_ServiceRestart.Text = "Restart Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ServiceRestart, "Tooltip")
        Me.LinkLabel_ServiceRestart.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_ServiceStop
        '
        Me.LinkLabel_ServiceStop.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServiceStop.AutoSize = True
        Me.LinkLabel_ServiceStop.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceStop.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServiceStop.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceStop.Location = New System.Drawing.Point(103, 75)
        Me.LinkLabel_ServiceStop.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ServiceStop.Name = "LinkLabel_ServiceStop"
        Me.LinkLabel_ServiceStop.Size = New System.Drawing.Size(69, 13)
        Me.LinkLabel_ServiceStop.TabIndex = 12
        Me.LinkLabel_ServiceStop.TabStop = True
        Me.LinkLabel_ServiceStop.Text = "Stop Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ServiceStop, "Tooltip")
        Me.LinkLabel_ServiceStop.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.LinkLabel_ConfigToolClose)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.ClassPictureBoxQuality2)
        Me.Panel2.Controls.Add(Me.LinkLabel_ConfigToolRun)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(382, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(384, 112)
        Me.Panel2.TabIndex = 0
        '
        'LinkLabel_ConfigToolClose
        '
        Me.LinkLabel_ConfigToolClose.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ConfigToolClose.AutoSize = True
        Me.LinkLabel_ConfigToolClose.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolClose.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ConfigToolClose.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolClose.Location = New System.Drawing.Point(103, 59)
        Me.LinkLabel_ConfigToolClose.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ConfigToolClose.Name = "LinkLabel_ConfigToolClose"
        Me.LinkLabel_ConfigToolClose.Size = New System.Drawing.Size(98, 13)
        Me.LinkLabel_ConfigToolClose.TabIndex = 24
        Me.LinkLabel_ConfigToolClose.TabStop = True
        Me.LinkLabel_ConfigToolClose.Text = "Close Config Tool"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ConfigToolClose, "Tooltip")
        Me.LinkLabel_ConfigToolClose.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(102, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 21)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Configuration"
        '
        'ClassPictureBoxQuality2
        '
        Me.ClassPictureBoxQuality2.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5364_64x64_32
        Me.ClassPictureBoxQuality2.Location = New System.Drawing.Point(32, 19)
        Me.ClassPictureBoxQuality2.m_HighQuality = True
        Me.ClassPictureBoxQuality2.Margin = New System.Windows.Forms.Padding(32, 16, 3, 3)
        Me.ClassPictureBoxQuality2.Name = "ClassPictureBoxQuality2"
        Me.ClassPictureBoxQuality2.Size = New System.Drawing.Size(64, 64)
        Me.ClassPictureBoxQuality2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality2.TabIndex = 19
        Me.ClassPictureBoxQuality2.TabStop = False
        '
        'LinkLabel_ConfigToolRun
        '
        Me.LinkLabel_ConfigToolRun.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ConfigToolRun.AutoSize = True
        Me.LinkLabel_ConfigToolRun.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolRun.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ConfigToolRun.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolRun.Location = New System.Drawing.Point(103, 43)
        Me.LinkLabel_ConfigToolRun.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ConfigToolRun.Name = "LinkLabel_ConfigToolRun"
        Me.LinkLabel_ConfigToolRun.Size = New System.Drawing.Size(91, 13)
        Me.LinkLabel_ConfigToolRun.TabIndex = 22
        Me.LinkLabel_ConfigToolRun.TabStop = True
        Me.LinkLabel_ConfigToolRun.Text = "Run Config Tool"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ConfigToolRun, "Tooltip")
        Me.LinkLabel_ConfigToolRun.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.LinkLabel_ServiceFactory)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.ClassPictureBoxQuality3)
        Me.Panel5.Controls.Add(Me.LinkLabel_InstallDrivers)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 112)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(382, 112)
        Me.Panel5.TabIndex = 6
        '
        'LinkLabel_ServiceFactory
        '
        Me.LinkLabel_ServiceFactory.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServiceFactory.AutoSize = True
        Me.LinkLabel_ServiceFactory.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceFactory.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServiceFactory.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceFactory.Location = New System.Drawing.Point(103, 56)
        Me.LinkLabel_ServiceFactory.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ServiceFactory.Name = "LinkLabel_ServiceFactory"
        Me.LinkLabel_ServiceFactory.Size = New System.Drawing.Size(164, 13)
        Me.LinkLabel_ServiceFactory.TabIndex = 27
        Me.LinkLabel_ServiceFactory.TabStop = True
        Me.LinkLabel_ServiceFactory.Text = "Factory Reset PSMoveServiceEx"
        Me.LinkLabel_ServiceFactory.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(102, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 21)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Troubleshooting"
        '
        'ClassPictureBoxQuality3
        '
        Me.ClassPictureBoxQuality3.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Faultrep_5201_256x256_32
        Me.ClassPictureBoxQuality3.Location = New System.Drawing.Point(32, 16)
        Me.ClassPictureBoxQuality3.m_HighQuality = True
        Me.ClassPictureBoxQuality3.Margin = New System.Windows.Forms.Padding(32, 16, 3, 3)
        Me.ClassPictureBoxQuality3.Name = "ClassPictureBoxQuality3"
        Me.ClassPictureBoxQuality3.Size = New System.Drawing.Size(64, 64)
        Me.ClassPictureBoxQuality3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality3.TabIndex = 24
        Me.ClassPictureBoxQuality3.TabStop = False
        '
        'LinkLabel_InstallDrivers
        '
        Me.LinkLabel_InstallDrivers.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_InstallDrivers.AutoSize = True
        Me.LinkLabel_InstallDrivers.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_InstallDrivers.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_InstallDrivers.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_InstallDrivers.Location = New System.Drawing.Point(103, 40)
        Me.LinkLabel_InstallDrivers.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_InstallDrivers.Name = "LinkLabel_InstallDrivers"
        Me.LinkLabel_InstallDrivers.Size = New System.Drawing.Size(152, 13)
        Me.LinkLabel_InstallDrivers.TabIndex = 25
        Me.LinkLabel_InstallDrivers.TabStop = True
        Me.LinkLabel_InstallDrivers.Text = "Install PlaystationEye Drivers"
        Me.LinkLabel_InstallDrivers.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Panel12
        '
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Controls.Add(Me.Label_PsmsxStatus)
        Me.Panel12.Controls.Add(Me.Panel_PsmsxStatus)
        Me.Panel12.Controls.Add(Me.Panel3)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(766, 42)
        Me.Panel12.TabIndex = 0
        '
        'Label_PsmsxStatus
        '
        Me.Label_PsmsxStatus.BackColor = System.Drawing.Color.Transparent
        Me.Label_PsmsxStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PsmsxStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_PsmsxStatus.ForeColor = System.Drawing.Color.Navy
        Me.Label_PsmsxStatus.Location = New System.Drawing.Point(19, 0)
        Me.Label_PsmsxStatus.Name = "Label_PsmsxStatus"
        Me.Label_PsmsxStatus.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label_PsmsxStatus.Size = New System.Drawing.Size(747, 41)
        Me.Label_PsmsxStatus.TabIndex = 1
        Me.Label_PsmsxStatus.Text = "PSMoveServiceEx Disconnected."
        Me.Label_PsmsxStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel_PsmsxStatus
        '
        Me.Panel_PsmsxStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel_PsmsxStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel_PsmsxStatus.Location = New System.Drawing.Point(0, 0)
        Me.Panel_PsmsxStatus.Name = "Panel_PsmsxStatus"
        Me.Panel_PsmsxStatus.Size = New System.Drawing.Size(19, 41)
        Me.Panel_PsmsxStatus.TabIndex = 2
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Gray
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 41)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(766, 1)
        Me.Panel3.TabIndex = 0
        '
        'ToolTip_Service
        '
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.PictureBox1)
        Me.Panel6.Controls.Add(Me.Label4)
        Me.Panel6.Controls.Add(Me.Label3)
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(800, 64)
        Me.Panel6.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.mmcshext_128_256x256_32
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.m_HighQuality = True
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(57, 57)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(66, 30)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(731, 30)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Manage service and configurations here."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(66, 3)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(172, 21)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Service Management"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(0, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(800, 1)
        Me.Label5.TabIndex = 0
        '
        'UCStartPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCStartPage"
        Me.Size = New System.Drawing.Size(800, 600)
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.ClassPictureBoxQuality3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel12.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label_PsmsxStatus As Label
    Friend WithEvents ToolTip_Service As ToolTip
    Friend WithEvents ClassPictureBoxQuality1 As ClassPictureBoxQuality
    Friend WithEvents LinkLabel_ServiceRestart As LinkLabel
    Friend WithEvents LinkLabel_ServiceStop As LinkLabel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents LinkLabel_ServiceRun As LinkLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents ClassPictureBoxQuality2 As ClassPictureBoxQuality
    Friend WithEvents LinkLabel_ConfigToolRun As LinkLabel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents PictureBox1 As ClassPictureBoxQuality
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents ClassPictureBoxQuality3 As ClassPictureBoxQuality
    Friend WithEvents LinkLabel_InstallDrivers As LinkLabel
    Friend WithEvents LinkLabel_ServiceFactory As LinkLabel
    Friend WithEvents LinkLabel_ServicePath As LinkLabel
    Friend WithEvents Panel_PsmsxStatus As Panel
    Friend WithEvents LinkLabel_ConfigToolClose As LinkLabel
End Class
