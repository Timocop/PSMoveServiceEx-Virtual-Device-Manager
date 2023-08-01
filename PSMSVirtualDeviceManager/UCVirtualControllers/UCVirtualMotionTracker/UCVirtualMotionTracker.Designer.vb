<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCVirtualMotionTracker
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCVirtualMotionTracker))
        Me.LinkLabel_ReadMore = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage_Management = New System.Windows.Forms.TabPage()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.ListView_OscDevices = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader_Type = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Serial = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Position = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Orientation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.LinkLabel_OscRun = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_OscPause = New System.Windows.Forms.LinkLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LinkLabel_SteamRun = New System.Windows.Forms.LinkLabel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality2 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.LinkLabel_DriverInstall = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_DriverUninstall = New System.Windows.Forms.LinkLabel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label_OscStatus = New System.Windows.Forms.Label()
        Me.Panel_OscStatus = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TabPage_Trackers = New System.Windows.Forms.TabPage()
        Me.Panel_VMTTrackers = New System.Windows.Forms.Panel()
        Me.Button_VMTControllers = New System.Windows.Forms.Button()
        Me.Button_AddVMTController = New System.Windows.Forms.Button()
        Me.TabPage_Settings = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button_RecenterButtonTimeReset = New System.Windows.Forms.Button()
        Me.NumericUpDown_RecenterButtonTime = New System.Windows.Forms.NumericUpDown()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Button_ResetRecenter = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.ComboBox_HmdRecenterFromDevice = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.ComboBox_HmdRecenterMethod = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.CheckBox_HmdRecenterEnabled = New System.Windows.Forms.CheckBox()
        Me.ComboBox_RecenterFromDevice = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.ComboBox_RecenterMethod = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.CheckBox_ControllerRecenterEnabled = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ComboBox_TouchpadMethod = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CheckBox_TouchpadShortcutClick = New System.Windows.Forms.CheckBox()
        Me.CheckBox_TouchpadClampBounds = New System.Windows.Forms.CheckBox()
        Me.CheckBox_TouchpadShortcuts = New System.Windows.Forms.CheckBox()
        Me.LinkLabel_JoystickShortcutsInfo = New System.Windows.Forms.LinkLabel()
        Me.ComboBox_GrabButtonMethod = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox_TouchpadClickMethod = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Button_OscThreadSleepReset = New System.Windows.Forms.Button()
        Me.NumericUpDown_OscThreadSleep = New System.Windows.Forms.NumericUpDown()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CheckBox_EnableHeptics = New System.Windows.Forms.CheckBox()
        Me.CheckBox_DisableBasestations = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button_SaveControllerSettings = New System.Windows.Forms.Button()
        Me.TabPage_Overrides = New System.Windows.Forms.TabPage()
        Me.Panel_SteamVRRestart = New System.Windows.Forms.Panel()
        Me.LinkLabel_SteamVRRestartOff = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button_Refresh = New System.Windows.Forms.Button()
        Me.Button_Remove = New System.Windows.Forms.Button()
        Me.Button_Add = New System.Windows.Forms.Button()
        Me.PictureBox2 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ListView_Overrides = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip_Autostart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.PictureBox1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.TabControl1.SuspendLayout()
        Me.TabPage_Management.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.TabPage_Trackers.SuspendLayout()
        Me.TabPage_Settings.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumericUpDown_RecenterButtonTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.NumericUpDown_OscThreadSleep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_Overrides.SuspendLayout()
        Me.Panel_SteamVRRestart.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LinkLabel_ReadMore
        '
        Me.LinkLabel_ReadMore.AutoSize = True
        Me.LinkLabel_ReadMore.Enabled = False
        Me.LinkLabel_ReadMore.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_ReadMore.Location = New System.Drawing.Point(38, 49)
        Me.LinkLabel_ReadMore.Name = "LinkLabel_ReadMore"
        Me.LinkLabel_ReadMore.Size = New System.Drawing.Size(62, 13)
        Me.LinkLabel_ReadMore.TabIndex = 21
        Me.LinkLabel_ReadMore.TabStop = True
        Me.LinkLabel_ReadMore.Text = "Read more"
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Location = New System.Drawing.Point(38, 16)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 16, 16, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(746, 33)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Control Virtual Motion Trackers (VMT) with PSMoveServiceEx controllers." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage_Management)
        Me.TabControl1.Controls.Add(Me.TabPage_Trackers)
        Me.TabControl1.Controls.Add(Me.TabPage_Settings)
        Me.TabControl1.Controls.Add(Me.TabPage_Overrides)
        Me.TabControl1.Location = New System.Drawing.Point(16, 78)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(16)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(768, 946)
        Me.TabControl1.TabIndex = 22
        '
        'TabPage_Management
        '
        Me.TabPage_Management.AutoScroll = True
        Me.TabPage_Management.BackColor = System.Drawing.Color.White
        Me.TabPage_Management.Controls.Add(Me.Panel5)
        Me.TabPage_Management.Controls.Add(Me.Panel1)
        Me.TabPage_Management.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Management.Name = "TabPage_Management"
        Me.TabPage_Management.Size = New System.Drawing.Size(760, 920)
        Me.TabPage_Management.TabIndex = 4
        Me.TabPage_Management.Text = "Management"
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.ListView_OscDevices)
        Me.Panel5.Controls.Add(Me.Panel8)
        Me.Panel5.Location = New System.Drawing.Point(16, 188)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(728, 250)
        Me.Panel5.TabIndex = 2
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
        Me.ListView_OscDevices.Size = New System.Drawing.Size(726, 206)
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
        Me.Panel8.Size = New System.Drawing.Size(726, 42)
        Me.Panel8.TabIndex = 0
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
        Me.Label12.Size = New System.Drawing.Size(726, 41)
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
        Me.Panel10.Size = New System.Drawing.Size(726, 1)
        Me.Panel10.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Controls.Add(Me.Panel12)
        Me.Panel1.Location = New System.Drawing.Point(16, 16)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(728, 157)
        Me.Panel1.TabIndex = 1
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(726, 113)
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
        Me.Panel4.Size = New System.Drawing.Size(362, 113)
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
        Me.Panel2.Controls.Add(Me.LinkLabel_SteamRun)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.ClassPictureBoxQuality2)
        Me.Panel2.Controls.Add(Me.LinkLabel_DriverInstall)
        Me.Panel2.Controls.Add(Me.LinkLabel_DriverUninstall)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(362, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(364, 113)
        Me.Panel2.TabIndex = 2
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
        Me.LinkLabel_DriverInstall.Location = New System.Drawing.Point(103, 59)
        Me.LinkLabel_DriverInstall.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
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
        Me.LinkLabel_DriverUninstall.Location = New System.Drawing.Point(103, 75)
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
        Me.Panel12.Size = New System.Drawing.Size(726, 42)
        Me.Panel12.TabIndex = 0
        '
        'Label_OscStatus
        '
        Me.Label_OscStatus.BackColor = System.Drawing.Color.Transparent
        Me.Label_OscStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_OscStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_OscStatus.ForeColor = System.Drawing.Color.Navy
        Me.Label_OscStatus.Location = New System.Drawing.Point(19, 0)
        Me.Label_OscStatus.Name = "Label_OscStatus"
        Me.Label_OscStatus.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label_OscStatus.Size = New System.Drawing.Size(707, 41)
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
        Me.Panel3.Size = New System.Drawing.Size(726, 1)
        Me.Panel3.TabIndex = 0
        '
        'TabPage_Trackers
        '
        Me.TabPage_Trackers.Controls.Add(Me.Panel_VMTTrackers)
        Me.TabPage_Trackers.Controls.Add(Me.Button_VMTControllers)
        Me.TabPage_Trackers.Controls.Add(Me.Button_AddVMTController)
        Me.TabPage_Trackers.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Trackers.Name = "TabPage_Trackers"
        Me.TabPage_Trackers.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Trackers.Size = New System.Drawing.Size(760, 920)
        Me.TabPage_Trackers.TabIndex = 0
        Me.TabPage_Trackers.Text = "Trackers"
        Me.TabPage_Trackers.UseVisualStyleBackColor = True
        '
        'Panel_VMTTrackers
        '
        Me.Panel_VMTTrackers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_VMTTrackers.AutoScroll = True
        Me.Panel_VMTTrackers.Location = New System.Drawing.Point(19, 58)
        Me.Panel_VMTTrackers.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_VMTTrackers.Name = "Panel_VMTTrackers"
        Me.Panel_VMTTrackers.Size = New System.Drawing.Size(722, 748)
        Me.Panel_VMTTrackers.TabIndex = 15
        '
        'Button_VMTControllers
        '
        Me.Button_VMTControllers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5353_16x16_32
        Me.Button_VMTControllers.Location = New System.Drawing.Point(16, 16)
        Me.Button_VMTControllers.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Button_VMTControllers.Name = "Button_VMTControllers"
        Me.Button_VMTControllers.Size = New System.Drawing.Size(162, 23)
        Me.Button_VMTControllers.TabIndex = 14
        Me.Button_VMTControllers.Text = "Autostart trackers..."
        Me.Button_VMTControllers.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_VMTControllers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_VMTControllers.UseVisualStyleBackColor = True
        '
        'Button_AddVMTController
        '
        Me.Button_AddVMTController.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_AddVMTController.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.DevicePairing_6101_16x16_32
        Me.Button_AddVMTController.Location = New System.Drawing.Point(621, 16)
        Me.Button_AddVMTController.Margin = New System.Windows.Forms.Padding(3, 3, 16, 3)
        Me.Button_AddVMTController.Name = "Button_AddVMTController"
        Me.Button_AddVMTController.Size = New System.Drawing.Size(120, 23)
        Me.Button_AddVMTController.TabIndex = 13
        Me.Button_AddVMTController.Text = "Add tracker"
        Me.Button_AddVMTController.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_AddVMTController.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_AddVMTController.UseVisualStyleBackColor = True
        '
        'TabPage_Settings
        '
        Me.TabPage_Settings.Controls.Add(Me.TabControl2)
        Me.TabPage_Settings.Controls.Add(Me.Button_SaveControllerSettings)
        Me.TabPage_Settings.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Settings.Name = "TabPage_Settings"
        Me.TabPage_Settings.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Settings.Size = New System.Drawing.Size(760, 920)
        Me.TabPage_Settings.TabIndex = 3
        Me.TabPage_Settings.Text = "Settings"
        Me.TabPage_Settings.UseVisualStyleBackColor = True
        '
        'TabControl2
        '
        Me.TabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Controls.Add(Me.TabPage5)
        Me.TabControl2.Location = New System.Drawing.Point(6, 6)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(748, 853)
        Me.TabControl2.TabIndex = 47
        '
        'TabPage4
        '
        Me.TabPage4.AutoScroll = True
        Me.TabPage4.Controls.Add(Me.GroupBox1)
        Me.TabPage4.Controls.Add(Me.GroupBox2)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(740, 827)
        Me.TabPage4.TabIndex = 0
        Me.TabPage4.Text = "PSMove Controller"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button_RecenterButtonTimeReset)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown_RecenterButtonTime)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Button_ResetRecenter)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.ComboBox_HmdRecenterFromDevice)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.ComboBox_HmdRecenterMethod)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.CheckBox_HmdRecenterEnabled)
        Me.GroupBox1.Controls.Add(Me.ComboBox_RecenterFromDevice)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.ComboBox_RecenterMethod)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.CheckBox_ControllerRecenterEnabled)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(3, 186)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(734, 485)
        Me.GroupBox1.TabIndex = 47
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Recenter Settings"
        '
        'Button_RecenterButtonTimeReset
        '
        Me.Button_RecenterButtonTimeReset.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5315_16x16_32
        Me.Button_RecenterButtonTimeReset.Location = New System.Drawing.Point(300, 404)
        Me.Button_RecenterButtonTimeReset.Name = "Button_RecenterButtonTimeReset"
        Me.Button_RecenterButtonTimeReset.Size = New System.Drawing.Size(23, 23)
        Me.Button_RecenterButtonTimeReset.TabIndex = 62
        Me.Button_RecenterButtonTimeReset.UseVisualStyleBackColor = True
        '
        'NumericUpDown_RecenterButtonTime
        '
        Me.NumericUpDown_RecenterButtonTime.Location = New System.Drawing.Point(199, 405)
        Me.NumericUpDown_RecenterButtonTime.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.NumericUpDown_RecenterButtonTime.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown_RecenterButtonTime.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.NumericUpDown_RecenterButtonTime.Name = "NumericUpDown_RecenterButtonTime"
        Me.NumericUpDown_RecenterButtonTime.Size = New System.Drawing.Size(95, 22)
        Me.NumericUpDown_RecenterButtonTime.TabIndex = 61
        Me.NumericUpDown_RecenterButtonTime.Value = New Decimal(New Integer() {500, 0, 0, 0})
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(21, 407)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(172, 13)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "Recenter button press time (ms):"
        '
        'Button_ResetRecenter
        '
        Me.Button_ResetRecenter.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.Button_ResetRecenter.Location = New System.Drawing.Point(19, 446)
        Me.Button_ResetRecenter.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Button_ResetRecenter.Name = "Button_ResetRecenter"
        Me.Button_ResetRecenter.Size = New System.Drawing.Size(214, 23)
        Me.Button_ResetRecenter.TabIndex = 59
        Me.Button_ResetRecenter.Text = "Reset recenter on all trackers"
        Me.Button_ResetRecenter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_ResetRecenter.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label20.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_101_16x16_32
        Me.Label20.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label20.Location = New System.Drawing.Point(51, 367)
        Me.Label20.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Padding = New System.Windows.Forms.Padding(3)
        Me.Label20.Size = New System.Drawing.Size(348, 19)
        Me.Label20.TabIndex = 58
        Me.Label20.Text = "        Only PSMoveServiceEx supported trackers can be recentered!"
        '
        'ComboBox_HmdRecenterFromDevice
        '
        Me.ComboBox_HmdRecenterFromDevice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_HmdRecenterFromDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_HmdRecenterFromDevice.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_HmdRecenterFromDevice.FormattingEnabled = True
        Me.ComboBox_HmdRecenterFromDevice.Location = New System.Drawing.Point(179, 340)
        Me.ComboBox_HmdRecenterFromDevice.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_HmdRecenterFromDevice.Name = "ComboBox_HmdRecenterFromDevice"
        Me.ComboBox_HmdRecenterFromDevice.Size = New System.Drawing.Size(504, 21)
        Me.ComboBox_HmdRecenterFromDevice.TabIndex = 57
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(51, 343)
        Me.Label18.Margin = New System.Windows.Forms.Padding(48, 3, 3, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(81, 13)
        Me.Label18.TabIndex = 56
        Me.Label18.Text = "Target Tracker:"
        '
        'ComboBox_HmdRecenterMethod
        '
        Me.ComboBox_HmdRecenterMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_HmdRecenterMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_HmdRecenterMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_HmdRecenterMethod.FormattingEnabled = True
        Me.ComboBox_HmdRecenterMethod.Location = New System.Drawing.Point(179, 313)
        Me.ComboBox_HmdRecenterMethod.Margin = New System.Windows.Forms.Padding(3, 16, 48, 3)
        Me.ComboBox_HmdRecenterMethod.Name = "ComboBox_HmdRecenterMethod"
        Me.ComboBox_HmdRecenterMethod.Size = New System.Drawing.Size(504, 21)
        Me.ComboBox_HmdRecenterMethod.TabIndex = 55
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(51, 316)
        Me.Label19.Margin = New System.Windows.Forms.Padding(48, 3, 3, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(98, 13)
        Me.Label19.TabIndex = 54
        Me.Label19.Text = "Recenter method:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label17.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label17.Location = New System.Drawing.Point(15, 21)
        Me.Label17.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Padding = New System.Windows.Forms.Padding(3)
        Me.Label17.Size = New System.Drawing.Size(494, 58)
        Me.Label17.TabIndex = 53
        Me.Label17.Text = resources.GetString("Label17.Text")
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label16.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label16.Location = New System.Drawing.Point(51, 262)
        Me.Label16.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Padding = New System.Windows.Forms.Padding(3)
        Me.Label16.Size = New System.Drawing.Size(534, 32)
        Me.Label16.TabIndex = 52
        Me.Label16.Text = resources.GetString("Label16.Text")
        '
        'CheckBox_HmdRecenterEnabled
        '
        Me.CheckBox_HmdRecenterEnabled.AutoSize = True
        Me.CheckBox_HmdRecenterEnabled.Location = New System.Drawing.Point(19, 239)
        Me.CheckBox_HmdRecenterEnabled.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.CheckBox_HmdRecenterEnabled.Name = "CheckBox_HmdRecenterEnabled"
        Me.CheckBox_HmdRecenterEnabled.Size = New System.Drawing.Size(223, 17)
        Me.CheckBox_HmdRecenterEnabled.TabIndex = 51
        Me.CheckBox_HmdRecenterEnabled.Text = "Enable remote orientation recentering"
        Me.CheckBox_HmdRecenterEnabled.UseVisualStyleBackColor = True
        '
        'ComboBox_RecenterFromDevice
        '
        Me.ComboBox_RecenterFromDevice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_RecenterFromDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_RecenterFromDevice.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_RecenterFromDevice.FormattingEnabled = True
        Me.ComboBox_RecenterFromDevice.Location = New System.Drawing.Point(179, 199)
        Me.ComboBox_RecenterFromDevice.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_RecenterFromDevice.Name = "ComboBox_RecenterFromDevice"
        Me.ComboBox_RecenterFromDevice.Size = New System.Drawing.Size(504, 21)
        Me.ComboBox_RecenterFromDevice.TabIndex = 50
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(51, 202)
        Me.Label15.Margin = New System.Windows.Forms.Padding(48, 3, 3, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 13)
        Me.Label15.TabIndex = 49
        Me.Label15.Text = "From Tracker:"
        '
        'ComboBox_RecenterMethod
        '
        Me.ComboBox_RecenterMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_RecenterMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_RecenterMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_RecenterMethod.FormattingEnabled = True
        Me.ComboBox_RecenterMethod.Location = New System.Drawing.Point(179, 172)
        Me.ComboBox_RecenterMethod.Margin = New System.Windows.Forms.Padding(3, 16, 48, 3)
        Me.ComboBox_RecenterMethod.Name = "ComboBox_RecenterMethod"
        Me.ComboBox_RecenterMethod.Size = New System.Drawing.Size(504, 21)
        Me.ComboBox_RecenterMethod.TabIndex = 48
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(51, 175)
        Me.Label14.Margin = New System.Windows.Forms.Padding(48, 3, 3, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(98, 13)
        Me.Label14.TabIndex = 47
        Me.Label14.Text = "Recenter method:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label11.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label11.Location = New System.Drawing.Point(51, 121)
        Me.Label11.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Padding = New System.Windows.Forms.Padding(3)
        Me.Label11.Size = New System.Drawing.Size(520, 32)
        Me.Label11.TabIndex = 45
        Me.Label11.Text = resources.GetString("Label11.Text")
        '
        'CheckBox_ControllerRecenterEnabled
        '
        Me.CheckBox_ControllerRecenterEnabled.AutoSize = True
        Me.CheckBox_ControllerRecenterEnabled.Location = New System.Drawing.Point(22, 98)
        Me.CheckBox_ControllerRecenterEnabled.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.CheckBox_ControllerRecenterEnabled.Name = "CheckBox_ControllerRecenterEnabled"
        Me.CheckBox_ControllerRecenterEnabled.Size = New System.Drawing.Size(184, 17)
        Me.CheckBox_ControllerRecenterEnabled.TabIndex = 0
        Me.CheckBox_ControllerRecenterEnabled.Text = "Enable orientation recentering"
        Me.CheckBox_ControllerRecenterEnabled.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ComboBox_TouchpadMethod)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.CheckBox_TouchpadShortcutClick)
        Me.GroupBox2.Controls.Add(Me.CheckBox_TouchpadClampBounds)
        Me.GroupBox2.Controls.Add(Me.CheckBox_TouchpadShortcuts)
        Me.GroupBox2.Controls.Add(Me.LinkLabel_JoystickShortcutsInfo)
        Me.GroupBox2.Controls.Add(Me.ComboBox_GrabButtonMethod)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.ComboBox_TouchpadClickMethod)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(734, 183)
        Me.GroupBox2.TabIndex = 46
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "HTC Vive Emulation Settings"
        '
        'ComboBox_TouchpadMethod
        '
        Me.ComboBox_TouchpadMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_TouchpadMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_TouchpadMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_TouchpadMethod.FormattingEnabled = True
        Me.ComboBox_TouchpadMethod.Location = New System.Drawing.Point(179, 69)
        Me.ComboBox_TouchpadMethod.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_TouchpadMethod.Name = "ComboBox_TouchpadMethod"
        Me.ComboBox_TouchpadMethod.Size = New System.Drawing.Size(504, 21)
        Me.ComboBox_TouchpadMethod.TabIndex = 47
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(19, 72)
        Me.Label10.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(137, 13)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Touchpad touch method:"
        '
        'CheckBox_TouchpadShortcutClick
        '
        Me.CheckBox_TouchpadShortcutClick.AutoSize = True
        Me.CheckBox_TouchpadShortcutClick.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_TouchpadShortcutClick.Location = New System.Drawing.Point(51, 45)
        Me.CheckBox_TouchpadShortcutClick.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.CheckBox_TouchpadShortcutClick.Name = "CheckBox_TouchpadShortcutClick"
        Me.CheckBox_TouchpadShortcutClick.Size = New System.Drawing.Size(224, 18)
        Me.CheckBox_TouchpadShortcutClick.TabIndex = 44
        Me.CheckBox_TouchpadShortcutClick.Text = "Click touchpad when using shortcuts"
        Me.CheckBox_TouchpadShortcutClick.UseVisualStyleBackColor = True
        '
        'CheckBox_TouchpadClampBounds
        '
        Me.CheckBox_TouchpadClampBounds.AutoSize = True
        Me.CheckBox_TouchpadClampBounds.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_TouchpadClampBounds.Location = New System.Drawing.Point(19, 150)
        Me.CheckBox_TouchpadClampBounds.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_TouchpadClampBounds.Name = "CheckBox_TouchpadClampBounds"
        Me.CheckBox_TouchpadClampBounds.Size = New System.Drawing.Size(196, 18)
        Me.CheckBox_TouchpadClampBounds.TabIndex = 45
        Me.CheckBox_TouchpadClampBounds.Text = "Clamp touchpad axis to bounds"
        Me.CheckBox_TouchpadClampBounds.UseVisualStyleBackColor = True
        '
        'CheckBox_TouchpadShortcuts
        '
        Me.CheckBox_TouchpadShortcuts.AutoSize = True
        Me.CheckBox_TouchpadShortcuts.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_TouchpadShortcuts.Location = New System.Drawing.Point(19, 21)
        Me.CheckBox_TouchpadShortcuts.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_TouchpadShortcuts.Name = "CheckBox_TouchpadShortcuts"
        Me.CheckBox_TouchpadShortcuts.Size = New System.Drawing.Size(226, 18)
        Me.CheckBox_TouchpadShortcuts.TabIndex = 42
        Me.CheckBox_TouchpadShortcuts.Text = "Enable touchpad emulation shortcuts"
        Me.CheckBox_TouchpadShortcuts.UseVisualStyleBackColor = True
        '
        'LinkLabel_JoystickShortcutsInfo
        '
        Me.LinkLabel_JoystickShortcutsInfo.AutoSize = True
        Me.LinkLabel_JoystickShortcutsInfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel_JoystickShortcutsInfo.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_JoystickShortcutsInfo.Location = New System.Drawing.Point(251, 23)
        Me.LinkLabel_JoystickShortcutsInfo.Name = "LinkLabel_JoystickShortcutsInfo"
        Me.LinkLabel_JoystickShortcutsInfo.Size = New System.Drawing.Size(26, 13)
        Me.LinkLabel_JoystickShortcutsInfo.TabIndex = 43
        Me.LinkLabel_JoystickShortcutsInfo.TabStop = True
        Me.LinkLabel_JoystickShortcutsInfo.Text = "( ? )"
        '
        'ComboBox_GrabButtonMethod
        '
        Me.ComboBox_GrabButtonMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_GrabButtonMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_GrabButtonMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_GrabButtonMethod.FormattingEnabled = True
        Me.ComboBox_GrabButtonMethod.Location = New System.Drawing.Point(179, 123)
        Me.ComboBox_GrabButtonMethod.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_GrabButtonMethod.Name = "ComboBox_GrabButtonMethod"
        Me.ComboBox_GrabButtonMethod.Size = New System.Drawing.Size(504, 21)
        Me.ComboBox_GrabButtonMethod.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 126)
        Me.Label5.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(117, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Grab button method:"
        '
        'ComboBox_TouchpadClickMethod
        '
        Me.ComboBox_TouchpadClickMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_TouchpadClickMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_TouchpadClickMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_TouchpadClickMethod.FormattingEnabled = True
        Me.ComboBox_TouchpadClickMethod.Location = New System.Drawing.Point(179, 96)
        Me.ComboBox_TouchpadClickMethod.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_TouchpadClickMethod.Name = "ComboBox_TouchpadClickMethod"
        Me.ComboBox_TouchpadClickMethod.Size = New System.Drawing.Size(504, 21)
        Me.ComboBox_TouchpadClickMethod.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 99)
        Me.Label4.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(129, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Touchpad click method:"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Button_OscThreadSleepReset)
        Me.TabPage5.Controls.Add(Me.NumericUpDown_OscThreadSleep)
        Me.TabPage5.Controls.Add(Me.Label21)
        Me.TabPage5.Controls.Add(Me.Label7)
        Me.TabPage5.Controls.Add(Me.CheckBox_EnableHeptics)
        Me.TabPage5.Controls.Add(Me.CheckBox_DisableBasestations)
        Me.TabPage5.Controls.Add(Me.Label6)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(740, 827)
        Me.TabPage5.TabIndex = 1
        Me.TabPage5.Text = "Other"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Button_OscThreadSleepReset
        '
        Me.Button_OscThreadSleepReset.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5315_16x16_32
        Me.Button_OscThreadSleepReset.Location = New System.Drawing.Point(274, 177)
        Me.Button_OscThreadSleepReset.Name = "Button_OscThreadSleepReset"
        Me.Button_OscThreadSleepReset.Size = New System.Drawing.Size(23, 23)
        Me.Button_OscThreadSleepReset.TabIndex = 50
        Me.Button_OscThreadSleepReset.UseVisualStyleBackColor = True
        '
        'NumericUpDown_OscThreadSleep
        '
        Me.NumericUpDown_OscThreadSleep.Location = New System.Drawing.Point(177, 178)
        Me.NumericUpDown_OscThreadSleep.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_OscThreadSleep.Name = "NumericUpDown_OscThreadSleep"
        Me.NumericUpDown_OscThreadSleep.Size = New System.Drawing.Size(91, 22)
        Me.NumericUpDown_OscThreadSleep.TabIndex = 49
        Me.NumericUpDown_OscThreadSleep.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(16, 180)
        Me.Label21.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(155, 13)
        Me.Label21.TabIndex = 48
        Me.Label21.Text = "Processing thread sleep (ms):"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label7.Location = New System.Drawing.Point(16, 119)
        Me.Label7.Margin = New System.Windows.Forms.Padding(16, 0, 3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(3)
        Me.Label7.Size = New System.Drawing.Size(497, 45)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = resources.GetString("Label7.Text")
        '
        'CheckBox_EnableHeptics
        '
        Me.CheckBox_EnableHeptics.AutoSize = True
        Me.CheckBox_EnableHeptics.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_EnableHeptics.Location = New System.Drawing.Point(16, 98)
        Me.CheckBox_EnableHeptics.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.CheckBox_EnableHeptics.Name = "CheckBox_EnableHeptics"
        Me.CheckBox_EnableHeptics.Size = New System.Drawing.Size(338, 18)
        Me.CheckBox_EnableHeptics.TabIndex = 46
        Me.CheckBox_EnableHeptics.Text = "Enable haptic feedback (PSMove && DualShock 4 controllers)"
        Me.CheckBox_EnableHeptics.UseVisualStyleBackColor = True
        '
        'CheckBox_DisableBasestations
        '
        Me.CheckBox_DisableBasestations.AutoSize = True
        Me.CheckBox_DisableBasestations.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_DisableBasestations.Location = New System.Drawing.Point(16, 16)
        Me.CheckBox_DisableBasestations.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.CheckBox_DisableBasestations.Name = "CheckBox_DisableBasestations"
        Me.CheckBox_DisableBasestations.Size = New System.Drawing.Size(190, 18)
        Me.CheckBox_DisableBasestations.TabIndex = 43
        Me.CheckBox_DisableBasestations.Text = "Disable Base Station spawning"
        Me.CheckBox_DisableBasestations.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label6.Location = New System.Drawing.Point(16, 37)
        Me.Label6.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(3)
        Me.Label6.Size = New System.Drawing.Size(592, 45)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = resources.GetString("Label6.Text")
        '
        'Button_SaveControllerSettings
        '
        Me.Button_SaveControllerSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SaveControllerSettings.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16761_16x16_32
        Me.Button_SaveControllerSettings.Location = New System.Drawing.Point(624, 878)
        Me.Button_SaveControllerSettings.Margin = New System.Windows.Forms.Padding(16)
        Me.Button_SaveControllerSettings.Name = "Button_SaveControllerSettings"
        Me.Button_SaveControllerSettings.Size = New System.Drawing.Size(120, 23)
        Me.Button_SaveControllerSettings.TabIndex = 45
        Me.Button_SaveControllerSettings.Text = "Save settings"
        Me.Button_SaveControllerSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_SaveControllerSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_SaveControllerSettings.UseVisualStyleBackColor = True
        '
        'TabPage_Overrides
        '
        Me.TabPage_Overrides.Controls.Add(Me.Panel_SteamVRRestart)
        Me.TabPage_Overrides.Controls.Add(Me.Label2)
        Me.TabPage_Overrides.Controls.Add(Me.Button_Refresh)
        Me.TabPage_Overrides.Controls.Add(Me.Button_Remove)
        Me.TabPage_Overrides.Controls.Add(Me.Button_Add)
        Me.TabPage_Overrides.Controls.Add(Me.PictureBox2)
        Me.TabPage_Overrides.Controls.Add(Me.ListView_Overrides)
        Me.TabPage_Overrides.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Overrides.Name = "TabPage_Overrides"
        Me.TabPage_Overrides.Size = New System.Drawing.Size(760, 920)
        Me.TabPage_Overrides.TabIndex = 2
        Me.TabPage_Overrides.Text = "SteamVR Tracker Overrides"
        Me.TabPage_Overrides.UseVisualStyleBackColor = True
        '
        'Panel_SteamVRRestart
        '
        Me.Panel_SteamVRRestart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_SteamVRRestart.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel_SteamVRRestart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_SteamVRRestart.Controls.Add(Me.LinkLabel_SteamVRRestartOff)
        Me.Panel_SteamVRRestart.Controls.Add(Me.Label3)
        Me.Panel_SteamVRRestart.Controls.Add(Me.PictureBox3)
        Me.Panel_SteamVRRestart.Location = New System.Drawing.Point(16, 301)
        Me.Panel_SteamVRRestart.Name = "Panel_SteamVRRestart"
        Me.Panel_SteamVRRestart.Size = New System.Drawing.Size(728, 42)
        Me.Panel_SteamVRRestart.TabIndex = 28
        '
        'LinkLabel_SteamVRRestartOff
        '
        Me.LinkLabel_SteamVRRestartOff.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel_SteamVRRestartOff.AutoSize = True
        Me.LinkLabel_SteamVRRestartOff.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_SteamVRRestartOff.Location = New System.Drawing.Point(665, 13)
        Me.LinkLabel_SteamVRRestartOff.Margin = New System.Windows.Forms.Padding(16)
        Me.LinkLabel_SteamVRRestartOff.Name = "LinkLabel_SteamVRRestartOff"
        Me.LinkLabel_SteamVRRestartOff.Size = New System.Drawing.Size(45, 13)
        Me.LinkLabel_SteamVRRestartOff.TabIndex = 28
        Me.LinkLabel_SteamVRRestartOff.TabStop = True
        Me.LinkLabel_SteamVRRestartOff.Text = "Dismiss"
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(42, 0)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 16, 16, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(601, 40)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "SteamVR needs to be restarted for changes to take effect."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox3
        '
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox3.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_101_16x16_32
        Me.PictureBox3.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox3.m_HighQuality = False
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(42, 40)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 26
        Me.PictureBox3.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(38, 16)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 16, 16, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(706, 33)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Choose trackers you want to override position and orientation with." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Useful if yo" &
    "u, for example, use PhoneVR and want to enable 6-DoF using PSMoveSerivceEx contr" &
    "ollers."
        '
        'Button_Refresh
        '
        Me.Button_Refresh.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16739_16x16_32
        Me.Button_Refresh.Location = New System.Drawing.Point(16, 272)
        Me.Button_Refresh.Name = "Button_Refresh"
        Me.Button_Refresh.Size = New System.Drawing.Size(109, 23)
        Me.Button_Refresh.TabIndex = 3
        Me.Button_Refresh.Text = "Refresh"
        Me.Button_Refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_Refresh.UseVisualStyleBackColor = True
        '
        'Button_Remove
        '
        Me.Button_Remove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Remove.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.Button_Remove.Location = New System.Drawing.Point(520, 272)
        Me.Button_Remove.Name = "Button_Remove"
        Me.Button_Remove.Size = New System.Drawing.Size(109, 23)
        Me.Button_Remove.TabIndex = 2
        Me.Button_Remove.Text = "Remove"
        Me.Button_Remove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Remove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_Remove.UseVisualStyleBackColor = True
        '
        'Button_Add
        '
        Me.Button_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Add.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.wmploc_474_16x16_32
        Me.Button_Add.Location = New System.Drawing.Point(635, 272)
        Me.Button_Add.Name = "Button_Add"
        Me.Button_Add.Size = New System.Drawing.Size(109, 23)
        Me.Button_Add.TabIndex = 1
        Me.Button_Add.Text = "Add"
        Me.Button_Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_Add.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.PictureBox2.Location = New System.Drawing.Point(16, 16)
        Me.PictureBox2.m_HighQuality = False
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 24
        Me.PictureBox2.TabStop = False
        '
        'ListView_Overrides
        '
        Me.ListView_Overrides.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_Overrides.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView_Overrides.FullRowSelect = True
        Me.ListView_Overrides.HideSelection = False
        Me.ListView_Overrides.Location = New System.Drawing.Point(16, 81)
        Me.ListView_Overrides.Margin = New System.Windows.Forms.Padding(16, 32, 16, 3)
        Me.ListView_Overrides.Name = "ListView_Overrides"
        Me.ListView_Overrides.Size = New System.Drawing.Size(728, 185)
        Me.ListView_Overrides.TabIndex = 0
        Me.ListView_Overrides.UseCompatibleStateImageBehavior = False
        Me.ListView_Overrides.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Tracker"
        Me.ColumnHeader1.Width = 300
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Override"
        Me.ColumnHeader2.Width = 300
        '
        'ContextMenuStrip_Autostart
        '
        Me.ContextMenuStrip_Autostart.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip_Autostart.Size = New System.Drawing.Size(61, 4)
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.PictureBox1.Location = New System.Drawing.Point(16, 16)
        Me.PictureBox1.m_HighQuality = False
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 19
        Me.PictureBox1.TabStop = False
        '
        'UCVirtualMotionTracker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.LinkLabel_ReadMore)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVirtualMotionTracker"
        Me.Size = New System.Drawing.Size(800, 1040)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage_Management.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel12.ResumeLayout(False)
        Me.TabPage_Trackers.ResumeLayout(False)
        Me.TabPage_Settings.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumericUpDown_RecenterButtonTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        CType(Me.NumericUpDown_OscThreadSleep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_Overrides.ResumeLayout(False)
        Me.Panel_SteamVRRestart.ResumeLayout(False)
        Me.Panel_SteamVRRestart.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LinkLabel_ReadMore As LinkLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As ClassPictureBoxQuality
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage_Trackers As TabPage
    Friend WithEvents TabPage_Overrides As TabPage
    Friend WithEvents Panel_VMTTrackers As Panel
    Friend WithEvents Button_VMTControllers As Button
    Friend WithEvents Button_AddVMTController As Button
    Friend WithEvents ContextMenuStrip_Autostart As ContextMenuStrip
    Friend WithEvents ListView_Overrides As ClassListViewEx
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents Button_Refresh As Button
    Friend WithEvents Button_Remove As Button
    Friend WithEvents Button_Add As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox2 As ClassPictureBoxQuality
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox3 As ClassPictureBoxQuality
    Friend WithEvents Panel_SteamVRRestart As Panel
    Friend WithEvents LinkLabel_SteamVRRestartOff As LinkLabel
    Friend WithEvents TabPage_Settings As TabPage
    Friend WithEvents CheckBox_TouchpadShortcuts As CheckBox
    Friend WithEvents LinkLabel_JoystickShortcutsInfo As LinkLabel
    Friend WithEvents CheckBox_TouchpadShortcutClick As CheckBox
    Friend WithEvents Button_SaveControllerSettings As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ComboBox_TouchpadClickMethod As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ComboBox_GrabButtonMethod As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents CheckBox_DisableBasestations As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CheckBox_EnableHeptics As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TabPage_Management As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents ClassPictureBoxQuality1 As ClassPictureBoxQuality
    Friend WithEvents LinkLabel_OscRun As LinkLabel
    Friend WithEvents LinkLabel_OscPause As LinkLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents ClassPictureBoxQuality2 As ClassPictureBoxQuality
    Friend WithEvents LinkLabel_DriverInstall As LinkLabel
    Friend WithEvents LinkLabel_DriverUninstall As LinkLabel
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Label_OscStatus As Label
    Friend WithEvents Panel_OscStatus As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents LinkLabel_SteamRun As LinkLabel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents ListView_OscDevices As ClassListViewEx
    Friend WithEvents ColumnHeader_Type As ColumnHeader
    Friend WithEvents ColumnHeader_Serial As ColumnHeader
    Friend WithEvents ColumnHeader_Position As ColumnHeader
    Friend WithEvents ColumnHeader_Orientation As ColumnHeader
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents CheckBox_TouchpadClampBounds As CheckBox
    Friend WithEvents Label10 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents CheckBox_ControllerRecenterEnabled As CheckBox
    Friend WithEvents Label11 As Label
    Friend WithEvents ComboBox_RecenterMethod As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents ComboBox_RecenterFromDevice As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents ComboBox_TouchpadMethod As ComboBox
    Friend WithEvents ComboBox_HmdRecenterFromDevice As ComboBox
    Friend WithEvents Label18 As Label
    Friend WithEvents ComboBox_HmdRecenterMethod As ComboBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents CheckBox_HmdRecenterEnabled As CheckBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Button_ResetRecenter As Button
    Friend WithEvents NumericUpDown_RecenterButtonTime As NumericUpDown
    Friend WithEvents Label13 As Label
    Friend WithEvents NumericUpDown_OscThreadSleep As NumericUpDown
    Friend WithEvents Label21 As Label
    Friend WithEvents Button_OscThreadSleepReset As Button
    Friend WithEvents Button_RecenterButtonTimeReset As Button
End Class
