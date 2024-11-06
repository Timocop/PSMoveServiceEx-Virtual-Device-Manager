<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCVmtManagement
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Me.Panel_AvailableDevices = New System.Windows.Forms.Panel()
        Me.ListView_OscDevices = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader_Type = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Serial = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Position = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Orientation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel_Status = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.LinkLabel_OscRun = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_OscPause = New System.Windows.Forms.LinkLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LinkLabel_SteamSettings = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_SteamRun = New System.Windows.Forms.LinkLabel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality2 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.LinkLabel_DriverInstall = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_DriverUninstall = New System.Windows.Forms.LinkLabel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label_OscStatus = New System.Windows.Forms.Label()
        Me.Panel_OscStatus = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.ToolTip_Info = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip_Default = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel24 = New System.Windows.Forms.Panel()
        Me.Chart_VmtPerformance = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Panel25 = New System.Windows.Forms.Panel()
        Me.Button_ChartSettings = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel26 = New System.Windows.Forms.Panel()
        Me.ContextMenuStrip_Chart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_ChartEnabled = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_ChartClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripComboBox_ChartSamples = New System.Windows.Forms.ToolStripComboBox()
        Me.Panel_AvailableDevices.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel_Status.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel24.SuspendLayout()
        CType(Me.Chart_VmtPerformance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel25.SuspendLayout()
        Me.ContextMenuStrip_Chart.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_AvailableDevices
        '
        Me.Panel_AvailableDevices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_AvailableDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_AvailableDevices.Controls.Add(Me.ListView_OscDevices)
        Me.Panel_AvailableDevices.Controls.Add(Me.Panel8)
        Me.Panel_AvailableDevices.Location = New System.Drawing.Point(16, 16)
        Me.Panel_AvailableDevices.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_AvailableDevices.Name = "Panel_AvailableDevices"
        Me.Panel_AvailableDevices.Size = New System.Drawing.Size(768, 233)
        Me.Panel_AvailableDevices.TabIndex = 4
        '
        'ListView_OscDevices
        '
        Me.ListView_OscDevices.BackColor = System.Drawing.Color.White
        Me.ListView_OscDevices.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView_OscDevices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader_Type, Me.ColumnHeader_Serial, Me.ColumnHeader_Position, Me.ColumnHeader_Orientation})
        Me.ListView_OscDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_OscDevices.FullRowSelect = True
        Me.ListView_OscDevices.HideSelection = False
        Me.ListView_OscDevices.Location = New System.Drawing.Point(0, 42)
        Me.ListView_OscDevices.Name = "ListView_OscDevices"
        Me.ListView_OscDevices.Size = New System.Drawing.Size(766, 189)
        Me.ListView_OscDevices.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView_OscDevices.TabIndex = 1
        Me.ListView_OscDevices.UseCompatibleStateImageBehavior = False
        Me.ListView_OscDevices.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader_Type
        '
        Me.ColumnHeader_Type.Text = "Type"
        Me.ColumnHeader_Type.Width = 100
        '
        'ColumnHeader_Serial
        '
        Me.ColumnHeader_Serial.Text = "Serial"
        Me.ColumnHeader_Serial.Width = 250
        '
        'ColumnHeader_Position
        '
        Me.ColumnHeader_Position.Text = "Position"
        Me.ColumnHeader_Position.Width = 150
        '
        'ColumnHeader_Orientation
        '
        Me.ColumnHeader_Orientation.Text = "Orientation"
        Me.ColumnHeader_Orientation.Width = 150
        '
        'Panel8
        '
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.Label12)
        Me.Panel8.Controls.Add(Me.Panel10)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(766, 42)
        Me.Panel8.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Navy
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label12.Size = New System.Drawing.Size(766, 41)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Available OSC Devices"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Gray
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel10.Location = New System.Drawing.Point(0, 41)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(766, 1)
        Me.Panel10.TabIndex = 0
        '
        'Panel_Status
        '
        Me.Panel_Status.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Status.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel_Status.Controls.Add(Me.Panel12)
        Me.Panel_Status.Location = New System.Drawing.Point(16, 16)
        Me.Panel_Status.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_Status.Name = "Panel_Status"
        Me.Panel_Status.Size = New System.Drawing.Size(768, 178)
        Me.Panel_Status.TabIndex = 3
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel4, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 42)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(766, 134)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.ClassPictureBoxQuality1)
        Me.Panel4.Controls.Add(Me.LinkLabel_OscRun)
        Me.Panel4.Controls.Add(Me.LinkLabel_OscPause)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(382, 134)
        Me.Panel4.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Navy
        Me.Label8.Location = New System.Drawing.Point(102, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(111, 21)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Server Control"
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
        'LinkLabel_OscRun
        '
        Me.LinkLabel_OscRun.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_OscRun.AutoSize = True
        Me.LinkLabel_OscRun.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_OscRun.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_OscRun.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_OscRun.Location = New System.Drawing.Point(103, 43)
        Me.LinkLabel_OscRun.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_OscRun.Name = "LinkLabel_OscRun"
        Me.LinkLabel_OscRun.Size = New System.Drawing.Size(87, 13)
        Me.LinkLabel_OscRun.TabIndex = 17
        Me.LinkLabel_OscRun.TabStop = True
        Me.LinkLabel_OscRun.Text = "Run OSC Server"
        Me.LinkLabel_OscRun.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_OscPause
        '
        Me.LinkLabel_OscPause.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_OscPause.AutoSize = True
        Me.LinkLabel_OscPause.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_OscPause.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_OscPause.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_OscPause.Location = New System.Drawing.Point(103, 59)
        Me.LinkLabel_OscPause.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_OscPause.Name = "LinkLabel_OscPause"
        Me.LinkLabel_OscPause.Size = New System.Drawing.Size(96, 13)
        Me.LinkLabel_OscPause.TabIndex = 11
        Me.LinkLabel_OscPause.TabStop = True
        Me.LinkLabel_OscPause.Text = "Pause OSC Server"
        Me.LinkLabel_OscPause.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.LinkLabel_SteamSettings)
        Me.Panel2.Controls.Add(Me.LinkLabel_SteamRun)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.ClassPictureBoxQuality2)
        Me.Panel2.Controls.Add(Me.LinkLabel_DriverInstall)
        Me.Panel2.Controls.Add(Me.LinkLabel_DriverUninstall)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(382, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(384, 134)
        Me.Panel2.TabIndex = 2
        '
        'LinkLabel_SteamSettings
        '
        Me.LinkLabel_SteamSettings.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_SteamSettings.AutoSize = True
        Me.LinkLabel_SteamSettings.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_SteamSettings.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_SteamSettings.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_SteamSettings.Location = New System.Drawing.Point(103, 103)
        Me.LinkLabel_SteamSettings.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
        Me.LinkLabel_SteamSettings.Name = "LinkLabel_SteamSettings"
        Me.LinkLabel_SteamSettings.Size = New System.Drawing.Size(111, 13)
        Me.LinkLabel_SteamSettings.TabIndex = 20
        Me.LinkLabel_SteamSettings.TabStop = True
        Me.LinkLabel_SteamSettings.Text = "Advanced Settings..."
        Me.LinkLabel_SteamSettings.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_SteamRun
        '
        Me.LinkLabel_SteamRun.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_SteamRun.AutoSize = True
        Me.LinkLabel_SteamRun.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_SteamRun.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_SteamRun.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_SteamRun.Location = New System.Drawing.Point(103, 43)
        Me.LinkLabel_SteamRun.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_SteamRun.Name = "LinkLabel_SteamRun"
        Me.LinkLabel_SteamRun.Size = New System.Drawing.Size(92, 13)
        Me.LinkLabel_SteamRun.TabIndex = 19
        Me.LinkLabel_SteamRun.TabStop = True
        Me.LinkLabel_SteamRun.Text = "Launch SteamVR"
        Me.LinkLabel_SteamRun.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Navy
        Me.Label9.Location = New System.Drawing.Point(102, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(133, 21)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "SteamVR Support"
        '
        'ClassPictureBoxQuality2
        '
        Me.ClassPictureBoxQuality2.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.DevicePairing_6101_256x256_32
        Me.ClassPictureBoxQuality2.Location = New System.Drawing.Point(32, 19)
        Me.ClassPictureBoxQuality2.m_HighQuality = True
        Me.ClassPictureBoxQuality2.Margin = New System.Windows.Forms.Padding(32, 16, 3, 3)
        Me.ClassPictureBoxQuality2.Name = "ClassPictureBoxQuality2"
        Me.ClassPictureBoxQuality2.Size = New System.Drawing.Size(64, 64)
        Me.ClassPictureBoxQuality2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality2.TabIndex = 10
        Me.ClassPictureBoxQuality2.TabStop = False
        '
        'LinkLabel_DriverInstall
        '
        Me.LinkLabel_DriverInstall.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_DriverInstall.AutoSize = True
        Me.LinkLabel_DriverInstall.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_DriverInstall.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_DriverInstall.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_DriverInstall.Location = New System.Drawing.Point(103, 65)
        Me.LinkLabel_DriverInstall.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
        Me.LinkLabel_DriverInstall.Name = "LinkLabel_DriverInstall"
        Me.LinkLabel_DriverInstall.Size = New System.Drawing.Size(71, 13)
        Me.LinkLabel_DriverInstall.TabIndex = 17
        Me.LinkLabel_DriverInstall.TabStop = True
        Me.LinkLabel_DriverInstall.Text = "Install Driver"
        Me.LinkLabel_DriverInstall.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_DriverUninstall
        '
        Me.LinkLabel_DriverUninstall.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_DriverUninstall.AutoSize = True
        Me.LinkLabel_DriverUninstall.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_DriverUninstall.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_DriverUninstall.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_DriverUninstall.Location = New System.Drawing.Point(103, 81)
        Me.LinkLabel_DriverUninstall.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_DriverUninstall.Name = "LinkLabel_DriverUninstall"
        Me.LinkLabel_DriverUninstall.Size = New System.Drawing.Size(86, 13)
        Me.LinkLabel_DriverUninstall.TabIndex = 11
        Me.LinkLabel_DriverUninstall.TabStop = True
        Me.LinkLabel_DriverUninstall.Text = "Uninstall Driver"
        Me.LinkLabel_DriverUninstall.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Panel12
        '
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Controls.Add(Me.Label_OscStatus)
        Me.Panel12.Controls.Add(Me.Panel_OscStatus)
        Me.Panel12.Controls.Add(Me.Panel3)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(766, 42)
        Me.Panel12.TabIndex = 0
        '
        'Label_OscStatus
        '
        Me.Label_OscStatus.BackColor = System.Drawing.Color.White
        Me.Label_OscStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_OscStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_OscStatus.ForeColor = System.Drawing.Color.Navy
        Me.Label_OscStatus.Location = New System.Drawing.Point(19, 0)
        Me.Label_OscStatus.Name = "Label_OscStatus"
        Me.Label_OscStatus.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label_OscStatus.Size = New System.Drawing.Size(747, 41)
        Me.Label_OscStatus.TabIndex = 1
        Me.Label_OscStatus.Text = "OSC Disconnected"
        Me.Label_OscStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel_OscStatus
        '
        Me.Panel_OscStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel_OscStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel_OscStatus.Location = New System.Drawing.Point(0, 0)
        Me.Panel_OscStatus.Name = "Panel_OscStatus"
        Me.Panel_OscStatus.Size = New System.Drawing.Size(19, 41)
        Me.Panel_OscStatus.TabIndex = 2
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
        'ToolTip_Info
        '
        Me.ToolTip_Info.AutomaticDelay = 100
        Me.ToolTip_Info.AutoPopDelay = 30000
        Me.ToolTip_Info.InitialDelay = 100
        Me.ToolTip_Info.ReshowDelay = 20
        Me.ToolTip_Info.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip_Info.ToolTipTitle = "Information"
        '
        'ToolTip_Default
        '
        Me.ToolTip_Default.AutomaticDelay = 100
        Me.ToolTip_Default.AutoPopDelay = 30000
        Me.ToolTip_Default.InitialDelay = 100
        Me.ToolTip_Default.ReshowDelay = 20
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel_Status)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 207)
        Me.Panel1.TabIndex = 5
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel_AvailableDevices)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 207)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(800, 265)
        Me.Panel5.TabIndex = 6
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.Panel24)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 472)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(800, 396)
        Me.Panel13.TabIndex = 10
        '
        'Panel24
        '
        Me.Panel24.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel24.Controls.Add(Me.Chart_VmtPerformance)
        Me.Panel24.Controls.Add(Me.Panel25)
        Me.Panel24.Location = New System.Drawing.Point(16, 16)
        Me.Panel24.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(768, 364)
        Me.Panel24.TabIndex = 2
        '
        'Chart_VmtPerformance
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart_VmtPerformance.ChartAreas.Add(ChartArea1)
        Me.Chart_VmtPerformance.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.Chart_VmtPerformance.Legends.Add(Legend1)
        Me.Chart_VmtPerformance.Location = New System.Drawing.Point(0, 42)
        Me.Chart_VmtPerformance.Name = "Chart_VmtPerformance"
        Me.Chart_VmtPerformance.Size = New System.Drawing.Size(766, 320)
        Me.Chart_VmtPerformance.TabIndex = 1
        Me.Chart_VmtPerformance.Text = "Chart1"
        Title1.Alignment = System.Drawing.ContentAlignment.MiddleRight
        Title1.Name = "Title1"
        Title1.Text = "OSC device packets per second"
        Me.Chart_VmtPerformance.Titles.Add(Title1)
        '
        'Panel25
        '
        Me.Panel25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel25.Controls.Add(Me.Button_ChartSettings)
        Me.Panel25.Controls.Add(Me.Label15)
        Me.Panel25.Controls.Add(Me.Panel26)
        Me.Panel25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel25.Location = New System.Drawing.Point(0, 0)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(766, 42)
        Me.Panel25.TabIndex = 0
        '
        'Button_ChartSettings
        '
        Me.Button_ChartSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ChartSettings.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5321_16x16_32
        Me.Button_ChartSettings.Location = New System.Drawing.Point(623, 6)
        Me.Button_ChartSettings.Margin = New System.Windows.Forms.Padding(16, 6, 16, 6)
        Me.Button_ChartSettings.Name = "Button_ChartSettings"
        Me.Button_ChartSettings.Size = New System.Drawing.Size(127, 30)
        Me.Button_ChartSettings.TabIndex = 2
        Me.Button_ChartSettings.Text = "Graph Settings..."
        Me.Button_ChartSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_ChartSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_ChartSettings.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.White
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Navy
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label15.Size = New System.Drawing.Size(766, 41)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "OSC Performance Graph"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel26
        '
        Me.Panel26.BackColor = System.Drawing.Color.Gray
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel26.Location = New System.Drawing.Point(0, 41)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Size = New System.Drawing.Size(766, 1)
        Me.Panel26.TabIndex = 0
        '
        'ContextMenuStrip_Chart
        '
        Me.ContextMenuStrip_Chart.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_ChartEnabled, Me.ToolStripMenuItem_ChartClear, Me.ToolStripSeparator1, Me.ToolStripMenuItem1, Me.ToolStripComboBox_ChartSamples})
        Me.ContextMenuStrip_Chart.Name = "ContextMenuStrip_Chart"
        Me.ContextMenuStrip_Chart.Size = New System.Drawing.Size(182, 103)
        '
        'ToolStripMenuItem_ChartEnabled
        '
        Me.ToolStripMenuItem_ChartEnabled.CheckOnClick = True
        Me.ToolStripMenuItem_ChartEnabled.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.ToolStripMenuItem_ChartEnabled.Name = "ToolStripMenuItem_ChartEnabled"
        Me.ToolStripMenuItem_ChartEnabled.Size = New System.Drawing.Size(181, 22)
        Me.ToolStripMenuItem_ChartEnabled.Text = "Enabled"
        '
        'ToolStripMenuItem_ChartClear
        '
        Me.ToolStripMenuItem_ChartClear.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.ToolStripMenuItem_ChartClear.Name = "ToolStripMenuItem_ChartClear"
        Me.ToolStripMenuItem_ChartClear.Size = New System.Drawing.Size(181, 22)
        Me.ToolStripMenuItem_ChartClear.Text = "Clear"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(178, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Enabled = False
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(181, 22)
        Me.ToolStripMenuItem1.Text = "Maximum Samples:"
        '
        'ToolStripComboBox_ChartSamples
        '
        Me.ToolStripComboBox_ChartSamples.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ToolStripComboBox_ChartSamples.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ToolStripComboBox_ChartSamples.Name = "ToolStripComboBox_ChartSamples"
        Me.ToolStripComboBox_ChartSamples.Size = New System.Drawing.Size(121, 23)
        '
        'UCVmtManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel13)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVmtManagement"
        Me.Size = New System.Drawing.Size(800, 1004)
        Me.Panel_AvailableDevices.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel_Status.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel12.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.Panel24.ResumeLayout(False)
        CType(Me.Chart_VmtPerformance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel25.ResumeLayout(False)
        Me.ContextMenuStrip_Chart.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel_AvailableDevices As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Panel_Status As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents ClassPictureBoxQuality1 As ClassPictureBoxQuality
    Friend WithEvents LinkLabel_OscRun As LinkLabel
    Friend WithEvents LinkLabel_OscPause As LinkLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents LinkLabel_SteamSettings As LinkLabel
    Friend WithEvents LinkLabel_SteamRun As LinkLabel
    Friend WithEvents Label9 As Label
    Friend WithEvents ClassPictureBoxQuality2 As ClassPictureBoxQuality
    Friend WithEvents LinkLabel_DriverInstall As LinkLabel
    Friend WithEvents LinkLabel_DriverUninstall As LinkLabel
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Label_OscStatus As Label
    Friend WithEvents Panel_OscStatus As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ToolTip_Info As ToolTip
    Friend WithEvents ToolTip_Default As ToolTip
    Friend WithEvents ListView_OscDevices As ClassListViewEx
    Friend WithEvents ColumnHeader_Type As ColumnHeader
    Friend WithEvents ColumnHeader_Serial As ColumnHeader
    Friend WithEvents ColumnHeader_Position As ColumnHeader
    Friend WithEvents ColumnHeader_Orientation As ColumnHeader
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Panel24 As Panel
    Friend WithEvents Chart_VmtPerformance As DataVisualization.Charting.Chart
    Friend WithEvents Panel25 As Panel
    Friend WithEvents Button_ChartSettings As Button
    Friend WithEvents Label15 As Label
    Friend WithEvents Panel26 As Panel
    Friend WithEvents ContextMenuStrip_Chart As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_ChartEnabled As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ChartClear As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripComboBox_ChartSamples As ToolStripComboBox
End Class
