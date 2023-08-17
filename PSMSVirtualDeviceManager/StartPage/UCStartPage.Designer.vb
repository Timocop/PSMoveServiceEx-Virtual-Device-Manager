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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCStartPage))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.LinkLabel_Updates = New System.Windows.Forms.LinkLabel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality4 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.LinkLabel_Github = New System.Windows.Forms.LinkLabel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LinkLabel_ServiceRunCmd = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ServicePath = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.LinkLabel_ServiceRun = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ServiceRestart = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ServiceStop = New System.Windows.Forms.LinkLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LinkLabel_ConfigToolRunCmd = New System.Windows.Forms.LinkLabel()
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
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.ListView_ServiceDevices = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader_Type = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Color = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Serial = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Pos = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Orientation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Battery = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Button_PsmsUpdateIgnore = New System.Windows.Forms.Button()
        Me.Button_PsmsxUpdateDownload = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality5 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel_PsmsxUpdate = New System.Windows.Forms.Panel()
        Me.Panel_VdmUpdate = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Button_VdmUpdateIgnore = New System.Windows.Forms.Button()
        Me.Button_VdmUpdateDownload = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality6 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.Timer_RestartPsms = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.ClassPictureBoxQuality4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        CType(Me.ClassPictureBoxQuality3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.ClassPictureBoxQuality5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        Me.Panel_PsmsxUpdate.SuspendLayout()
        Me.Panel_VdmUpdate.SuspendLayout()
        Me.Panel17.SuspendLayout()
        CType(Me.ClassPictureBoxQuality6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel18.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Controls.Add(Me.Panel12)
        Me.Panel1.Location = New System.Drawing.Point(16, 19)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(861, 278)
        Me.Panel1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel7, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel4, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel5, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 42)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.11966!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.88034!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(859, 234)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.LinkLabel_Updates)
        Me.Panel7.Controls.Add(Me.Label7)
        Me.Panel7.Controls.Add(Me.ClassPictureBoxQuality4)
        Me.Panel7.Controls.Add(Me.LinkLabel_Github)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(429, 136)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(430, 98)
        Me.Panel7.TabIndex = 7
        '
        'LinkLabel_Updates
        '
        Me.LinkLabel_Updates.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_Updates.AutoSize = True
        Me.LinkLabel_Updates.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_Updates.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_Updates.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_Updates.Location = New System.Drawing.Point(103, 56)
        Me.LinkLabel_Updates.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_Updates.Name = "LinkLabel_Updates"
        Me.LinkLabel_Updates.Size = New System.Drawing.Size(102, 13)
        Me.LinkLabel_Updates.TabIndex = 27
        Me.LinkLabel_Updates.TabStop = True
        Me.LinkLabel_Updates.Text = "Check for Updates"
        Me.LinkLabel_Updates.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(102, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(157, 21)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Support and Updates"
        '
        'ClassPictureBoxQuality4
        '
        Me.ClassPictureBoxQuality4.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.connect_10101_256x256_32
        Me.ClassPictureBoxQuality4.Location = New System.Drawing.Point(32, 16)
        Me.ClassPictureBoxQuality4.m_HighQuality = True
        Me.ClassPictureBoxQuality4.Margin = New System.Windows.Forms.Padding(32, 16, 3, 3)
        Me.ClassPictureBoxQuality4.Name = "ClassPictureBoxQuality4"
        Me.ClassPictureBoxQuality4.Size = New System.Drawing.Size(64, 64)
        Me.ClassPictureBoxQuality4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality4.TabIndex = 24
        Me.ClassPictureBoxQuality4.TabStop = False
        '
        'LinkLabel_Github
        '
        Me.LinkLabel_Github.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_Github.AutoSize = True
        Me.LinkLabel_Github.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_Github.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_Github.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_Github.Location = New System.Drawing.Point(103, 40)
        Me.LinkLabel_Github.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_Github.Name = "LinkLabel_Github"
        Me.LinkLabel_Github.Size = New System.Drawing.Size(69, 13)
        Me.LinkLabel_Github.TabIndex = 25
        Me.LinkLabel_Github.TabStop = True
        Me.LinkLabel_Github.Text = "Visit GitHub"
        Me.LinkLabel_Github.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.LinkLabel_ServiceRunCmd)
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
        Me.Panel4.Size = New System.Drawing.Size(429, 136)
        Me.Panel4.TabIndex = 1
        '
        'LinkLabel_ServiceRunCmd
        '
        Me.LinkLabel_ServiceRunCmd.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServiceRunCmd.AutoSize = True
        Me.LinkLabel_ServiceRunCmd.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRunCmd.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServiceRunCmd.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRunCmd.Location = New System.Drawing.Point(103, 59)
        Me.LinkLabel_ServiceRunCmd.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ServiceRunCmd.Name = "LinkLabel_ServiceRunCmd"
        Me.LinkLabel_ServiceRunCmd.Size = New System.Drawing.Size(80, 13)
        Me.LinkLabel_ServiceRunCmd.TabIndex = 20
        Me.LinkLabel_ServiceRunCmd.TabStop = True
        Me.LinkLabel_ServiceRunCmd.Text = "Debug Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ServiceRunCmd, "Tooltip")
        Me.LinkLabel_ServiceRunCmd.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_ServicePath
        '
        Me.LinkLabel_ServicePath.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServicePath.AutoSize = True
        Me.LinkLabel_ServicePath.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServicePath.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServicePath.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServicePath.Location = New System.Drawing.Point(103, 107)
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
        Me.LinkLabel_ServiceRestart.Location = New System.Drawing.Point(103, 75)
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
        Me.LinkLabel_ServiceStop.Location = New System.Drawing.Point(103, 91)
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
        Me.Panel2.Controls.Add(Me.LinkLabel_ConfigToolRunCmd)
        Me.Panel2.Controls.Add(Me.LinkLabel_ConfigToolClose)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.ClassPictureBoxQuality2)
        Me.Panel2.Controls.Add(Me.LinkLabel_ConfigToolRun)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(429, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(430, 136)
        Me.Panel2.TabIndex = 0
        '
        'LinkLabel_ConfigToolRunCmd
        '
        Me.LinkLabel_ConfigToolRunCmd.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ConfigToolRunCmd.AutoSize = True
        Me.LinkLabel_ConfigToolRunCmd.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolRunCmd.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ConfigToolRunCmd.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolRunCmd.Location = New System.Drawing.Point(103, 59)
        Me.LinkLabel_ConfigToolRunCmd.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ConfigToolRunCmd.Name = "LinkLabel_ConfigToolRunCmd"
        Me.LinkLabel_ConfigToolRunCmd.Size = New System.Drawing.Size(105, 13)
        Me.LinkLabel_ConfigToolRunCmd.TabIndex = 25
        Me.LinkLabel_ConfigToolRunCmd.TabStop = True
        Me.LinkLabel_ConfigToolRunCmd.Text = "Debug Config Tool"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ConfigToolRunCmd, "Tooltip")
        Me.LinkLabel_ConfigToolRunCmd.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_ConfigToolClose
        '
        Me.LinkLabel_ConfigToolClose.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ConfigToolClose.AutoSize = True
        Me.LinkLabel_ConfigToolClose.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolClose.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ConfigToolClose.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolClose.Location = New System.Drawing.Point(103, 75)
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
        Me.Panel5.Location = New System.Drawing.Point(0, 136)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(429, 98)
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
        Me.LinkLabel_ServiceFactory.Size = New System.Drawing.Size(153, 13)
        Me.LinkLabel_ServiceFactory.TabIndex = 27
        Me.LinkLabel_ServiceFactory.TabStop = True
        Me.LinkLabel_ServiceFactory.Text = "Factory Reset PSMoveService"
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
        Me.Panel12.Size = New System.Drawing.Size(859, 42)
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
        Me.Label_PsmsxStatus.Size = New System.Drawing.Size(840, 41)
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
        Me.Panel3.Size = New System.Drawing.Size(859, 1)
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
        Me.Panel6.Size = New System.Drawing.Size(893, 64)
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
        Me.Label4.Size = New System.Drawing.Size(824, 30)
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
        Me.Label5.Size = New System.Drawing.Size(893, 1)
        Me.Label5.TabIndex = 0
        '
        'Panel8
        '
        Me.Panel8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.ListView_ServiceDevices)
        Me.Panel8.Controls.Add(Me.Panel14)
        Me.Panel8.Location = New System.Drawing.Point(16, 16)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(861, 268)
        Me.Panel8.TabIndex = 2
        '
        'ListView_ServiceDevices
        '
        Me.ListView_ServiceDevices.BackColor = System.Drawing.Color.White
        Me.ListView_ServiceDevices.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView_ServiceDevices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader_Type, Me.ColumnHeader_Color, Me.ColumnHeader_ID, Me.ColumnHeader_Serial, Me.ColumnHeader_Pos, Me.ColumnHeader_Orientation, Me.ColumnHeader_Battery})
        Me.ListView_ServiceDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_ServiceDevices.FullRowSelect = True
        Me.ListView_ServiceDevices.HideSelection = False
        Me.ListView_ServiceDevices.Location = New System.Drawing.Point(0, 42)
        Me.ListView_ServiceDevices.Name = "ListView_ServiceDevices"
        Me.ListView_ServiceDevices.Size = New System.Drawing.Size(859, 224)
        Me.ListView_ServiceDevices.TabIndex = 1
        Me.ListView_ServiceDevices.UseCompatibleStateImageBehavior = False
        Me.ListView_ServiceDevices.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader_Type
        '
        Me.ColumnHeader_Type.Text = "Type"
        Me.ColumnHeader_Type.Width = 100
        '
        'ColumnHeader_Color
        '
        Me.ColumnHeader_Color.Text = "Color"
        Me.ColumnHeader_Color.Width = 75
        '
        'ColumnHeader_ID
        '
        Me.ColumnHeader_ID.Text = "ID"
        Me.ColumnHeader_ID.Width = 30
        '
        'ColumnHeader_Serial
        '
        Me.ColumnHeader_Serial.Text = "Serial"
        Me.ColumnHeader_Serial.Width = 250
        '
        'ColumnHeader_Pos
        '
        Me.ColumnHeader_Pos.Text = "Position"
        Me.ColumnHeader_Pos.Width = 125
        '
        'ColumnHeader_Orientation
        '
        Me.ColumnHeader_Orientation.Text = "Orientation"
        Me.ColumnHeader_Orientation.Width = 125
        '
        'ColumnHeader_Battery
        '
        Me.ColumnHeader_Battery.Text = "Battery"
        Me.ColumnHeader_Battery.Width = 50
        '
        'Panel14
        '
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.Label12)
        Me.Panel14.Controls.Add(Me.Panel16)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(0, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(859, 42)
        Me.Panel14.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Navy
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label12.Size = New System.Drawing.Size(859, 41)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Available Service Devices"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.Gray
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel16.Location = New System.Drawing.Point(0, 41)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(859, 1)
        Me.Panel16.TabIndex = 0
        '
        'Panel9
        '
        Me.Panel9.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.Button_PsmsUpdateIgnore)
        Me.Panel9.Controls.Add(Me.Button_PsmsxUpdateDownload)
        Me.Panel9.Controls.Add(Me.Label10)
        Me.Panel9.Controls.Add(Me.ClassPictureBoxQuality5)
        Me.Panel9.Controls.Add(Me.Panel10)
        Me.Panel9.Location = New System.Drawing.Point(16, 19)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(861, 140)
        Me.Panel9.TabIndex = 3
        '
        'Button_PsmsUpdateIgnore
        '
        Me.Button_PsmsUpdateIgnore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_PsmsUpdateIgnore.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_PsmsUpdateIgnore.Location = New System.Drawing.Point(215, 99)
        Me.Button_PsmsUpdateIgnore.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.Button_PsmsUpdateIgnore.Name = "Button_PsmsUpdateIgnore"
        Me.Button_PsmsUpdateIgnore.Size = New System.Drawing.Size(95, 23)
        Me.Button_PsmsUpdateIgnore.TabIndex = 4
        Me.Button_PsmsUpdateIgnore.Text = "Ignore"
        Me.Button_PsmsUpdateIgnore.UseVisualStyleBackColor = True
        '
        'Button_PsmsxUpdateDownload
        '
        Me.Button_PsmsxUpdateDownload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_PsmsxUpdateDownload.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_PsmsxUpdateDownload.Location = New System.Drawing.Point(70, 99)
        Me.Button_PsmsxUpdateDownload.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.Button_PsmsxUpdateDownload.Name = "Button_PsmsxUpdateDownload"
        Me.Button_PsmsxUpdateDownload.Size = New System.Drawing.Size(139, 23)
        Me.Button_PsmsxUpdateDownload.TabIndex = 3
        Me.Button_PsmsxUpdateDownload.Text = "Download Now"
        Me.Button_PsmsxUpdateDownload.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(67, 49)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(511, 39)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = resources.GetString("Label10.Text")
        '
        'ClassPictureBoxQuality5
        '
        Me.ClassPictureBoxQuality5.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.connect_10101_256x256_32
        Me.ClassPictureBoxQuality5.Location = New System.Drawing.Point(8, 49)
        Me.ClassPictureBoxQuality5.m_HighQuality = True
        Me.ClassPictureBoxQuality5.Margin = New System.Windows.Forms.Padding(8)
        Me.ClassPictureBoxQuality5.Name = "ClassPictureBoxQuality5"
        Me.ClassPictureBoxQuality5.Size = New System.Drawing.Size(48, 48)
        Me.ClassPictureBoxQuality5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality5.TabIndex = 1
        Me.ClassPictureBoxQuality5.TabStop = False
        '
        'Panel10
        '
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label8)
        Me.Panel10.Controls.Add(Me.Panel11)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(859, 42)
        Me.Panel10.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Lavender
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Navy
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label8.Size = New System.Drawing.Size(859, 41)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "PSMoveServiceEx Software Update Available"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.Gray
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel11.Location = New System.Drawing.Point(0, 41)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(859, 1)
        Me.Panel11.TabIndex = 0
        '
        'Panel_PsmsxUpdate
        '
        Me.Panel_PsmsxUpdate.Controls.Add(Me.Panel9)
        Me.Panel_PsmsxUpdate.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel_PsmsxUpdate.Location = New System.Drawing.Point(0, 64)
        Me.Panel_PsmsxUpdate.Name = "Panel_PsmsxUpdate"
        Me.Panel_PsmsxUpdate.Size = New System.Drawing.Size(893, 175)
        Me.Panel_PsmsxUpdate.TabIndex = 4
        Me.Panel_PsmsxUpdate.Visible = False
        '
        'Panel_VdmUpdate
        '
        Me.Panel_VdmUpdate.Controls.Add(Me.Panel17)
        Me.Panel_VdmUpdate.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel_VdmUpdate.Location = New System.Drawing.Point(0, 239)
        Me.Panel_VdmUpdate.Name = "Panel_VdmUpdate"
        Me.Panel_VdmUpdate.Size = New System.Drawing.Size(893, 175)
        Me.Panel_VdmUpdate.TabIndex = 5
        Me.Panel_VdmUpdate.Visible = False
        '
        'Panel17
        '
        Me.Panel17.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel17.Controls.Add(Me.Button_VdmUpdateIgnore)
        Me.Panel17.Controls.Add(Me.Button_VdmUpdateDownload)
        Me.Panel17.Controls.Add(Me.Label11)
        Me.Panel17.Controls.Add(Me.ClassPictureBoxQuality6)
        Me.Panel17.Controls.Add(Me.Panel18)
        Me.Panel17.Location = New System.Drawing.Point(16, 19)
        Me.Panel17.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(861, 140)
        Me.Panel17.TabIndex = 3
        '
        'Button_VdmUpdateIgnore
        '
        Me.Button_VdmUpdateIgnore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_VdmUpdateIgnore.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_VdmUpdateIgnore.Location = New System.Drawing.Point(215, 99)
        Me.Button_VdmUpdateIgnore.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.Button_VdmUpdateIgnore.Name = "Button_VdmUpdateIgnore"
        Me.Button_VdmUpdateIgnore.Size = New System.Drawing.Size(95, 23)
        Me.Button_VdmUpdateIgnore.TabIndex = 7
        Me.Button_VdmUpdateIgnore.Text = "Ignore"
        Me.Button_VdmUpdateIgnore.UseVisualStyleBackColor = True
        '
        'Button_VdmUpdateDownload
        '
        Me.Button_VdmUpdateDownload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_VdmUpdateDownload.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_VdmUpdateDownload.Location = New System.Drawing.Point(70, 99)
        Me.Button_VdmUpdateDownload.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.Button_VdmUpdateDownload.Name = "Button_VdmUpdateDownload"
        Me.Button_VdmUpdateDownload.Size = New System.Drawing.Size(139, 23)
        Me.Button_VdmUpdateDownload.TabIndex = 6
        Me.Button_VdmUpdateDownload.Text = "Download Now"
        Me.Button_VdmUpdateDownload.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(67, 49)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(511, 39)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = resources.GetString("Label11.Text")
        '
        'ClassPictureBoxQuality6
        '
        Me.ClassPictureBoxQuality6.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.connect_10101_256x256_32
        Me.ClassPictureBoxQuality6.Location = New System.Drawing.Point(8, 49)
        Me.ClassPictureBoxQuality6.m_HighQuality = True
        Me.ClassPictureBoxQuality6.Margin = New System.Windows.Forms.Padding(8)
        Me.ClassPictureBoxQuality6.Name = "ClassPictureBoxQuality6"
        Me.ClassPictureBoxQuality6.Size = New System.Drawing.Size(48, 48)
        Me.ClassPictureBoxQuality6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality6.TabIndex = 4
        Me.ClassPictureBoxQuality6.TabStop = False
        '
        'Panel18
        '
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel18.Controls.Add(Me.Label9)
        Me.Panel18.Controls.Add(Me.Panel19)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel18.Location = New System.Drawing.Point(0, 0)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(859, 42)
        Me.Panel18.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Lavender
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Navy
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label9.Size = New System.Drawing.Size(859, 41)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Virtual Device Manager Software Update Available"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel19
        '
        Me.Panel19.BackColor = System.Drawing.Color.Gray
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel19.Location = New System.Drawing.Point(0, 41)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(859, 1)
        Me.Panel19.TabIndex = 0
        '
        'Panel20
        '
        Me.Panel20.Controls.Add(Me.Panel1)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel20.Location = New System.Drawing.Point(0, 414)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(893, 313)
        Me.Panel20.TabIndex = 6
        '
        'Panel21
        '
        Me.Panel21.Controls.Add(Me.Panel8)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel21.Location = New System.Drawing.Point(0, 727)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(893, 300)
        Me.Panel21.TabIndex = 7
        '
        'Timer_RestartPsms
        '
        Me.Timer_RestartPsms.Interval = 1000
        '
        'UCStartPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel21)
        Me.Controls.Add(Me.Panel20)
        Me.Controls.Add(Me.Panel_VdmUpdate)
        Me.Controls.Add(Me.Panel_PsmsxUpdate)
        Me.Controls.Add(Me.Panel6)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCStartPage"
        Me.Size = New System.Drawing.Size(893, 1075)
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.ClassPictureBoxQuality4, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.Panel8.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.ClassPictureBoxQuality5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel_PsmsxUpdate.ResumeLayout(False)
        Me.Panel_VdmUpdate.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        CType(Me.ClassPictureBoxQuality6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel18.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel21.ResumeLayout(False)
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
    Friend WithEvents Panel7 As Panel
    Friend WithEvents LinkLabel_Updates As LinkLabel
    Friend WithEvents Label7 As Label
    Friend WithEvents ClassPictureBoxQuality4 As ClassPictureBoxQuality
    Friend WithEvents LinkLabel_Github As LinkLabel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents ListView_ServiceDevices As ClassListViewEx
    Friend WithEvents ColumnHeader_Type As ColumnHeader
    Friend WithEvents ColumnHeader_Serial As ColumnHeader
    Friend WithEvents ColumnHeader_Pos As ColumnHeader
    Friend WithEvents ColumnHeader_Orientation As ColumnHeader
    Friend WithEvents ColumnHeader_Battery As ColumnHeader
    Friend WithEvents Panel14 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel16 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Panel_PsmsxUpdate As Panel
    Friend WithEvents Panel_VdmUpdate As Panel
    Friend WithEvents Panel17 As Panel
    Friend WithEvents Panel18 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel19 As Panel
    Friend WithEvents Panel20 As Panel
    Friend WithEvents Panel21 As Panel
    Friend WithEvents Button_PsmsxUpdateDownload As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents ClassPictureBoxQuality5 As ClassPictureBoxQuality
    Friend WithEvents Button_VdmUpdateDownload As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents ClassPictureBoxQuality6 As ClassPictureBoxQuality
    Friend WithEvents Button_PsmsUpdateIgnore As Button
    Friend WithEvents Button_VdmUpdateIgnore As Button
    Friend WithEvents ColumnHeader_ID As ColumnHeader
    Friend WithEvents ColumnHeader_Color As ColumnHeader
    Friend WithEvents LinkLabel_ServiceRunCmd As LinkLabel
    Friend WithEvents LinkLabel_ConfigToolRunCmd As LinkLabel
    Friend WithEvents Timer_RestartPsms As Timer
End Class
