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
        Me.TabControl_Vmt = New System.Windows.Forms.TabControl()
        Me.TabPage_Management = New System.Windows.Forms.TabPage()
        Me.Panel_AvailableDevices = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel_Status = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.LinkLabel_OscRun = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_OscPause = New System.Windows.Forms.LinkLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LinkLabel_SteamSettings = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_SteamRun = New System.Windows.Forms.LinkLabel()
        Me.Label9 = New System.Windows.Forms.Label()
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
        Me.TabControl_SettingsDevices = New System.Windows.Forms.TabControl()
        Me.TabPage_SettingsPSmove = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label_ScrollFocus = New System.Windows.Forms.Label()
        Me.NumericUpDown_PlayCalibForwardOffset = New System.Windows.Forms.NumericUpDown()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Button_PlayCalibReset = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.CheckBox_PlayCalibEnabled = New System.Windows.Forms.CheckBox()
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
        Me.Label_TouchpadTouchAreaDeg = New System.Windows.Forms.Label()
        Me.NumericUpDown_TouchpadTouchArea = New System.Windows.Forms.NumericUpDown()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.NumericUpDown_TouchpadClickDeadzone = New System.Windows.Forms.NumericUpDown()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.LinkLabel_TouchpadShortcutHelp = New System.Windows.Forms.LinkLabel()
        Me.ComboBox_TouchpadMethod = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CheckBox_TouchpadShortcutClick = New System.Windows.Forms.CheckBox()
        Me.CheckBox_TouchpadClampBounds = New System.Windows.Forms.CheckBox()
        Me.CheckBox_TouchpadShortcuts = New System.Windows.Forms.CheckBox()
        Me.ComboBox_GrabButtonMethod = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox_TouchpadClickMethod = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPage_SettingsOther = New System.Windows.Forms.TabPage()
        Me.Button_OscThreadSleepReset = New System.Windows.Forms.Button()
        Me.NumericUpDown_OscThreadSleep = New System.Windows.Forms.NumericUpDown()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CheckBox_EnableHeptics = New System.Windows.Forms.CheckBox()
        Me.CheckBox_DisableBasestations = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button_SaveControllerSettings = New System.Windows.Forms.Button()
        Me.TabPage_PlayspaceCalib = New System.Windows.Forms.TabPage()
        Me.Panel_PlayCalibSteps = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.ProgressBar_PlayCalibStep5 = New System.Windows.Forms.ProgressBar()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.ProgressBar_PlayCalibStep4 = New System.Windows.Forms.ProgressBar()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.ProgressBar_PlayCalibStep3 = New System.Windows.Forms.ProgressBar()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.ProgressBar_PlayCalibStep2 = New System.Windows.Forms.ProgressBar()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.ProgressBar_PlayCalibStep1 = New System.Windows.Forms.ProgressBar()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label_PlayCalibTitle = New System.Windows.Forms.Label()
        Me.Panel_PlayCalibStatus = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.ComboBox_PlayCalibControllerID = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.LinkLabel_PlayCalibShowSettings = New System.Windows.Forms.LinkLabel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Button_PlaySpaceManualCalib = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.TabPage_Overrides = New System.Windows.Forms.TabPage()
        Me.Panel_SteamVRRestart = New System.Windows.Forms.Panel()
        Me.LinkLabel_SteamVRRestartOff = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button_Refresh = New System.Windows.Forms.Button()
        Me.Button_Remove = New System.Windows.Forms.Button()
        Me.Button_Add = New System.Windows.Forms.Button()
        Me.ContextMenuStrip_Autostart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ListView_OscDevices = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader_Type = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Serial = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Position = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Orientation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ClassPictureBoxQuality1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality2 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality_CalibStep5 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality_CalibStep4 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality_CalibStep3 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality_CalibStep2 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality_CalibStep1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality3 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.PictureBox3 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.PictureBox2 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ListView_Overrides = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.TabControl_Vmt.SuspendLayout()
        Me.TabPage_Management.SuspendLayout()
        Me.Panel_AvailableDevices.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel_Status.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.TabPage_Trackers.SuspendLayout()
        Me.TabPage_Settings.SuspendLayout()
        Me.TabControl_SettingsDevices.SuspendLayout()
        Me.TabPage_SettingsPSmove.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumericUpDown_PlayCalibForwardOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_RecenterButtonTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.NumericUpDown_TouchpadTouchArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_TouchpadClickDeadzone, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_SettingsOther.SuspendLayout()
        CType(Me.NumericUpDown_OscThreadSleep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_PlayspaceCalib.SuspendLayout()
        Me.Panel_PlayCalibSteps.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TabPage_Overrides.SuspendLayout()
        Me.Panel_SteamVRRestart.SuspendLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality_CalibStep5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality_CalibStep4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality_CalibStep3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality_CalibStep2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality_CalibStep1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LinkLabel_ReadMore
        '
        Me.LinkLabel_ReadMore.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ReadMore.AutoSize = True
        Me.LinkLabel_ReadMore.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ReadMore.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ReadMore.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ReadMore.Location = New System.Drawing.Point(38, 46)
        Me.LinkLabel_ReadMore.Name = "LinkLabel_ReadMore"
        Me.LinkLabel_ReadMore.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.LinkLabel_ReadMore.Size = New System.Drawing.Size(62, 16)
        Me.LinkLabel_ReadMore.TabIndex = 21
        Me.LinkLabel_ReadMore.TabStop = True
        Me.LinkLabel_ReadMore.Text = "Read more"
        Me.LinkLabel_ReadMore.VisitedLinkColor = System.Drawing.Color.RoyalBlue
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
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'TabControl_Vmt
        '
        Me.TabControl_Vmt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl_Vmt.Controls.Add(Me.TabPage_Management)
        Me.TabControl_Vmt.Controls.Add(Me.TabPage_Trackers)
        Me.TabControl_Vmt.Controls.Add(Me.TabPage_Settings)
        Me.TabControl_Vmt.Controls.Add(Me.TabPage_PlayspaceCalib)
        Me.TabControl_Vmt.Controls.Add(Me.TabPage_Overrides)
        Me.TabControl_Vmt.Location = New System.Drawing.Point(16, 78)
        Me.TabControl_Vmt.Margin = New System.Windows.Forms.Padding(16)
        Me.TabControl_Vmt.Name = "TabControl_Vmt"
        Me.TabControl_Vmt.SelectedIndex = 0
        Me.TabControl_Vmt.Size = New System.Drawing.Size(768, 1277)
        Me.TabControl_Vmt.TabIndex = 22
        '
        'TabPage_Management
        '
        Me.TabPage_Management.AutoScroll = True
        Me.TabPage_Management.BackColor = System.Drawing.Color.White
        Me.TabPage_Management.Controls.Add(Me.Panel_AvailableDevices)
        Me.TabPage_Management.Controls.Add(Me.Panel_Status)
        Me.TabPage_Management.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Management.Name = "TabPage_Management"
        Me.TabPage_Management.Size = New System.Drawing.Size(760, 1251)
        Me.TabPage_Management.TabIndex = 4
        Me.TabPage_Management.Text = "Management"
        '
        'Panel_AvailableDevices
        '
        Me.Panel_AvailableDevices.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_AvailableDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_AvailableDevices.Controls.Add(Me.ListView_OscDevices)
        Me.Panel_AvailableDevices.Controls.Add(Me.Panel8)
        Me.Panel_AvailableDevices.Location = New System.Drawing.Point(16, 222)
        Me.Panel_AvailableDevices.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_AvailableDevices.Name = "Panel_AvailableDevices"
        Me.Panel_AvailableDevices.Size = New System.Drawing.Size(728, 250)
        Me.Panel_AvailableDevices.TabIndex = 2
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
        'Panel_Status
        '
        Me.Panel_Status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Status.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel_Status.Controls.Add(Me.Panel12)
        Me.Panel_Status.Location = New System.Drawing.Point(16, 16)
        Me.Panel_Status.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_Status.Name = "Panel_Status"
        Me.Panel_Status.Size = New System.Drawing.Size(728, 174)
        Me.Panel_Status.TabIndex = 1
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(726, 130)
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
        Me.Panel4.Size = New System.Drawing.Size(362, 130)
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
        Me.Panel2.Location = New System.Drawing.Point(362, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(364, 130)
        Me.Panel2.TabIndex = 2
        '
        'LinkLabel_SteamSettings
        '
        Me.LinkLabel_SteamSettings.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_SteamSettings.AutoSize = True
        Me.LinkLabel_SteamSettings.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_SteamSettings.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_SteamSettings.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_SteamSettings.Location = New System.Drawing.Point(103, 91)
        Me.LinkLabel_SteamSettings.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
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
        Me.TabPage_Trackers.Size = New System.Drawing.Size(760, 1251)
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
        Me.Panel_VMTTrackers.Size = New System.Drawing.Size(722, 1174)
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
        Me.TabPage_Settings.Controls.Add(Me.TabControl_SettingsDevices)
        Me.TabPage_Settings.Controls.Add(Me.Button_SaveControllerSettings)
        Me.TabPage_Settings.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Settings.Name = "TabPage_Settings"
        Me.TabPage_Settings.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Settings.Size = New System.Drawing.Size(760, 1251)
        Me.TabPage_Settings.TabIndex = 3
        Me.TabPage_Settings.Text = "Settings"
        Me.TabPage_Settings.UseVisualStyleBackColor = True
        '
        'TabControl_SettingsDevices
        '
        Me.TabControl_SettingsDevices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl_SettingsDevices.Controls.Add(Me.TabPage_SettingsPSmove)
        Me.TabControl_SettingsDevices.Controls.Add(Me.TabPage_SettingsOther)
        Me.TabControl_SettingsDevices.Location = New System.Drawing.Point(6, 6)
        Me.TabControl_SettingsDevices.Name = "TabControl_SettingsDevices"
        Me.TabControl_SettingsDevices.SelectedIndex = 0
        Me.TabControl_SettingsDevices.Size = New System.Drawing.Size(748, 1184)
        Me.TabControl_SettingsDevices.TabIndex = 47
        '
        'TabPage_SettingsPSmove
        '
        Me.TabPage_SettingsPSmove.AutoScroll = True
        Me.TabPage_SettingsPSmove.Controls.Add(Me.GroupBox1)
        Me.TabPage_SettingsPSmove.Controls.Add(Me.GroupBox2)
        Me.TabPage_SettingsPSmove.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_SettingsPSmove.Name = "TabPage_SettingsPSmove"
        Me.TabPage_SettingsPSmove.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_SettingsPSmove.Size = New System.Drawing.Size(740, 1158)
        Me.TabPage_SettingsPSmove.TabIndex = 0
        Me.TabPage_SettingsPSmove.Text = "PSMove Controller"
        Me.TabPage_SettingsPSmove.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label_ScrollFocus)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown_PlayCalibForwardOffset)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.Button_PlayCalibReset)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.CheckBox_PlayCalibEnabled)
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
        Me.GroupBox1.Location = New System.Drawing.Point(3, 241)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(734, 657)
        Me.GroupBox1.TabIndex = 47
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Recenter Settings"
        '
        'Label_ScrollFocus
        '
        Me.Label_ScrollFocus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_ScrollFocus.AutoSize = True
        Me.Label_ScrollFocus.Location = New System.Drawing.Point(636, 641)
        Me.Label_ScrollFocus.Name = "Label_ScrollFocus"
        Me.Label_ScrollFocus.Size = New System.Drawing.Size(92, 13)
        Me.Label_ScrollFocus.TabIndex = 70
        Me.Label_ScrollFocus.Text = "<<FOR FOCUS>"
        Me.Label_ScrollFocus.Visible = False
        '
        'NumericUpDown_PlayCalibForwardOffset
        '
        Me.NumericUpDown_PlayCalibForwardOffset.Location = New System.Drawing.Point(236, 532)
        Me.NumericUpDown_PlayCalibForwardOffset.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.NumericUpDown_PlayCalibForwardOffset.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown_PlayCalibForwardOffset.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.NumericUpDown_PlayCalibForwardOffset.Name = "NumericUpDown_PlayCalibForwardOffset"
        Me.NumericUpDown_PlayCalibForwardOffset.Size = New System.Drawing.Size(95, 22)
        Me.NumericUpDown_PlayCalibForwardOffset.TabIndex = 69
        Me.NumericUpDown_PlayCalibForwardOffset.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(19, 534)
        Me.Label26.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(211, 13)
        Me.Label26.TabIndex = 68
        Me.Label26.Text = "Playspace Recenter Forward Offset (cm):"
        '
        'Button_PlayCalibReset
        '
        Me.Button_PlayCalibReset.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.Button_PlayCalibReset.Location = New System.Drawing.Point(19, 573)
        Me.Button_PlayCalibReset.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Button_PlayCalibReset.Name = "Button_PlayCalibReset"
        Me.Button_PlayCalibReset.Size = New System.Drawing.Size(214, 23)
        Me.Button_PlayCalibReset.TabIndex = 67
        Me.Button_PlayCalibReset.Text = "Reset playspace calibration"
        Me.Button_PlayCalibReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_PlayCalibReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_PlayCalibReset.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label25.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label25.Location = New System.Drawing.Point(19, 470)
        Me.Label25.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label25.Name = "Label25"
        Me.Label25.Padding = New System.Windows.Forms.Padding(3)
        Me.Label25.Size = New System.Drawing.Size(504, 45)
        Me.Label25.TabIndex = 66
        Me.Label25.Text = resources.GetString("Label25.Text")
        '
        'CheckBox_PlayCalibEnabled
        '
        Me.CheckBox_PlayCalibEnabled.AutoSize = True
        Me.CheckBox_PlayCalibEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_PlayCalibEnabled.Location = New System.Drawing.Point(19, 447)
        Me.CheckBox_PlayCalibEnabled.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.CheckBox_PlayCalibEnabled.Name = "CheckBox_PlayCalibEnabled"
        Me.CheckBox_PlayCalibEnabled.Size = New System.Drawing.Size(182, 18)
        Me.CheckBox_PlayCalibEnabled.TabIndex = 65
        Me.CheckBox_PlayCalibEnabled.Text = "Enable playspace recentering"
        Me.CheckBox_PlayCalibEnabled.UseVisualStyleBackColor = True
        '
        'Button_RecenterButtonTimeReset
        '
        Me.Button_RecenterButtonTimeReset.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5315_16x16_32
        Me.Button_RecenterButtonTimeReset.Location = New System.Drawing.Point(298, 612)
        Me.Button_RecenterButtonTimeReset.Name = "Button_RecenterButtonTimeReset"
        Me.Button_RecenterButtonTimeReset.Size = New System.Drawing.Size(23, 23)
        Me.Button_RecenterButtonTimeReset.TabIndex = 62
        Me.Button_RecenterButtonTimeReset.UseVisualStyleBackColor = True
        '
        'NumericUpDown_RecenterButtonTime
        '
        Me.NumericUpDown_RecenterButtonTime.Location = New System.Drawing.Point(197, 613)
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
        Me.Label13.Location = New System.Drawing.Point(19, 615)
        Me.Label13.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(172, 13)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "Recenter button press time (ms):"
        '
        'Button_ResetRecenter
        '
        Me.Button_ResetRecenter.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.Button_ResetRecenter.Location = New System.Drawing.Point(19, 405)
        Me.Button_ResetRecenter.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Button_ResetRecenter.Name = "Button_ResetRecenter"
        Me.Button_ResetRecenter.Size = New System.Drawing.Size(214, 23)
        Me.Button_ResetRecenter.TabIndex = 59
        Me.Button_ResetRecenter.Text = "Reset recenter on all trackers"
        Me.Button_ResetRecenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.ComboBox_HmdRecenterFromDevice.Location = New System.Drawing.Point(191, 340)
        Me.ComboBox_HmdRecenterFromDevice.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_HmdRecenterFromDevice.Name = "ComboBox_HmdRecenterFromDevice"
        Me.ComboBox_HmdRecenterFromDevice.Size = New System.Drawing.Size(492, 21)
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
        Me.ComboBox_HmdRecenterMethod.Location = New System.Drawing.Point(191, 313)
        Me.ComboBox_HmdRecenterMethod.Margin = New System.Windows.Forms.Padding(3, 16, 48, 3)
        Me.ComboBox_HmdRecenterMethod.Name = "ComboBox_HmdRecenterMethod"
        Me.ComboBox_HmdRecenterMethod.Size = New System.Drawing.Size(492, 21)
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
        Me.Label17.Size = New System.Drawing.Size(509, 58)
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
        Me.CheckBox_HmdRecenterEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_HmdRecenterEnabled.Location = New System.Drawing.Point(19, 239)
        Me.CheckBox_HmdRecenterEnabled.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.CheckBox_HmdRecenterEnabled.Name = "CheckBox_HmdRecenterEnabled"
        Me.CheckBox_HmdRecenterEnabled.Size = New System.Drawing.Size(229, 18)
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
        Me.ComboBox_RecenterFromDevice.Location = New System.Drawing.Point(191, 199)
        Me.ComboBox_RecenterFromDevice.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_RecenterFromDevice.Name = "ComboBox_RecenterFromDevice"
        Me.ComboBox_RecenterFromDevice.Size = New System.Drawing.Size(492, 21)
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
        Me.ComboBox_RecenterMethod.Location = New System.Drawing.Point(191, 172)
        Me.ComboBox_RecenterMethod.Margin = New System.Windows.Forms.Padding(3, 16, 48, 3)
        Me.ComboBox_RecenterMethod.Name = "ComboBox_RecenterMethod"
        Me.ComboBox_RecenterMethod.Size = New System.Drawing.Size(492, 21)
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
        Me.CheckBox_ControllerRecenterEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_ControllerRecenterEnabled.Location = New System.Drawing.Point(22, 98)
        Me.CheckBox_ControllerRecenterEnabled.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.CheckBox_ControllerRecenterEnabled.Name = "CheckBox_ControllerRecenterEnabled"
        Me.CheckBox_ControllerRecenterEnabled.Size = New System.Drawing.Size(190, 18)
        Me.CheckBox_ControllerRecenterEnabled.TabIndex = 0
        Me.CheckBox_ControllerRecenterEnabled.Text = "Enable orientation recentering"
        Me.CheckBox_ControllerRecenterEnabled.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label_TouchpadTouchAreaDeg)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_TouchpadTouchArea)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_TouchpadClickDeadzone)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.LinkLabel_TouchpadShortcutHelp)
        Me.GroupBox2.Controls.Add(Me.ComboBox_TouchpadMethod)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.CheckBox_TouchpadShortcutClick)
        Me.GroupBox2.Controls.Add(Me.CheckBox_TouchpadClampBounds)
        Me.GroupBox2.Controls.Add(Me.CheckBox_TouchpadShortcuts)
        Me.GroupBox2.Controls.Add(Me.ComboBox_GrabButtonMethod)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.ComboBox_TouchpadClickMethod)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(734, 238)
        Me.GroupBox2.TabIndex = 46
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "HTC Vive Emulation Settings"
        '
        'Label_TouchpadTouchAreaDeg
        '
        Me.Label_TouchpadTouchAreaDeg.AutoSize = True
        Me.Label_TouchpadTouchAreaDeg.Location = New System.Drawing.Point(317, 98)
        Me.Label_TouchpadTouchAreaDeg.Name = "Label_TouchpadTouchAreaDeg"
        Me.Label_TouchpadTouchAreaDeg.Size = New System.Drawing.Size(41, 13)
        Me.Label_TouchpadTouchAreaDeg.TabIndex = 53
        Me.Label_TouchpadTouchAreaDeg.Text = "cm / 0°"
        '
        'NumericUpDown_TouchpadTouchArea
        '
        Me.NumericUpDown_TouchpadTouchArea.DecimalPlaces = 2
        Me.NumericUpDown_TouchpadTouchArea.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.NumericUpDown_TouchpadTouchArea.Location = New System.Drawing.Point(191, 96)
        Me.NumericUpDown_TouchpadTouchArea.Name = "NumericUpDown_TouchpadTouchArea"
        Me.NumericUpDown_TouchpadTouchArea.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_TouchpadTouchArea.TabIndex = 52
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(19, 98)
        Me.Label23.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(166, 13)
        Me.Label23.TabIndex = 51
        Me.Label23.Text = "Touchpad touch area (cm/deg):"
        '
        'NumericUpDown_TouchpadClickDeadzone
        '
        Me.NumericUpDown_TouchpadClickDeadzone.DecimalPlaces = 2
        Me.NumericUpDown_TouchpadClickDeadzone.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.NumericUpDown_TouchpadClickDeadzone.Location = New System.Drawing.Point(191, 151)
        Me.NumericUpDown_TouchpadClickDeadzone.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_TouchpadClickDeadzone.Name = "NumericUpDown_TouchpadClickDeadzone"
        Me.NumericUpDown_TouchpadClickDeadzone.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_TouchpadClickDeadzone.TabIndex = 50
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(19, 153)
        Me.Label22.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(144, 13)
        Me.Label22.TabIndex = 49
        Me.Label22.Text = "Touchpad click dead-zone:"
        '
        'LinkLabel_TouchpadShortcutHelp
        '
        Me.LinkLabel_TouchpadShortcutHelp.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_TouchpadShortcutHelp.AutoSize = True
        Me.LinkLabel_TouchpadShortcutHelp.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_TouchpadShortcutHelp.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_102_16x16_32
        Me.LinkLabel_TouchpadShortcutHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel_TouchpadShortcutHelp.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_TouchpadShortcutHelp.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_TouchpadShortcutHelp.Location = New System.Drawing.Point(251, 20)
        Me.LinkLabel_TouchpadShortcutHelp.Name = "LinkLabel_TouchpadShortcutHelp"
        Me.LinkLabel_TouchpadShortcutHelp.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_TouchpadShortcutHelp.Size = New System.Drawing.Size(91, 19)
        Me.LinkLabel_TouchpadShortcutHelp.TabIndex = 48
        Me.LinkLabel_TouchpadShortcutHelp.TabStop = True
        Me.LinkLabel_TouchpadShortcutHelp.Text = "What is this?"
        Me.LinkLabel_TouchpadShortcutHelp.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'ComboBox_TouchpadMethod
        '
        Me.ComboBox_TouchpadMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_TouchpadMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_TouchpadMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_TouchpadMethod.FormattingEnabled = True
        Me.ComboBox_TouchpadMethod.Location = New System.Drawing.Point(191, 69)
        Me.ComboBox_TouchpadMethod.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_TouchpadMethod.Name = "ComboBox_TouchpadMethod"
        Me.ComboBox_TouchpadMethod.Size = New System.Drawing.Size(492, 21)
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
        Me.CheckBox_TouchpadClampBounds.Location = New System.Drawing.Point(19, 206)
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
        'ComboBox_GrabButtonMethod
        '
        Me.ComboBox_GrabButtonMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_GrabButtonMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_GrabButtonMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_GrabButtonMethod.FormattingEnabled = True
        Me.ComboBox_GrabButtonMethod.Location = New System.Drawing.Point(191, 179)
        Me.ComboBox_GrabButtonMethod.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_GrabButtonMethod.Name = "ComboBox_GrabButtonMethod"
        Me.ComboBox_GrabButtonMethod.Size = New System.Drawing.Size(492, 21)
        Me.ComboBox_GrabButtonMethod.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 182)
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
        Me.ComboBox_TouchpadClickMethod.Location = New System.Drawing.Point(191, 124)
        Me.ComboBox_TouchpadClickMethod.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_TouchpadClickMethod.Name = "ComboBox_TouchpadClickMethod"
        Me.ComboBox_TouchpadClickMethod.Size = New System.Drawing.Size(492, 21)
        Me.ComboBox_TouchpadClickMethod.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 127)
        Me.Label4.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(129, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Touchpad click method:"
        '
        'TabPage_SettingsOther
        '
        Me.TabPage_SettingsOther.Controls.Add(Me.Button_OscThreadSleepReset)
        Me.TabPage_SettingsOther.Controls.Add(Me.NumericUpDown_OscThreadSleep)
        Me.TabPage_SettingsOther.Controls.Add(Me.Label21)
        Me.TabPage_SettingsOther.Controls.Add(Me.Label7)
        Me.TabPage_SettingsOther.Controls.Add(Me.CheckBox_EnableHeptics)
        Me.TabPage_SettingsOther.Controls.Add(Me.CheckBox_DisableBasestations)
        Me.TabPage_SettingsOther.Controls.Add(Me.Label6)
        Me.TabPage_SettingsOther.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_SettingsOther.Name = "TabPage_SettingsOther"
        Me.TabPage_SettingsOther.Size = New System.Drawing.Size(740, 1158)
        Me.TabPage_SettingsOther.TabIndex = 1
        Me.TabPage_SettingsOther.Text = "Other"
        Me.TabPage_SettingsOther.UseVisualStyleBackColor = True
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
        Me.Button_SaveControllerSettings.Location = New System.Drawing.Point(624, 1209)
        Me.Button_SaveControllerSettings.Margin = New System.Windows.Forms.Padding(16)
        Me.Button_SaveControllerSettings.Name = "Button_SaveControllerSettings"
        Me.Button_SaveControllerSettings.Size = New System.Drawing.Size(120, 23)
        Me.Button_SaveControllerSettings.TabIndex = 45
        Me.Button_SaveControllerSettings.Text = "Save settings"
        Me.Button_SaveControllerSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_SaveControllerSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_SaveControllerSettings.UseVisualStyleBackColor = True
        '
        'TabPage_PlayspaceCalib
        '
        Me.TabPage_PlayspaceCalib.AutoScroll = True
        Me.TabPage_PlayspaceCalib.Controls.Add(Me.Panel_PlayCalibSteps)
        Me.TabPage_PlayspaceCalib.Controls.Add(Me.ComboBox_PlayCalibControllerID)
        Me.TabPage_PlayspaceCalib.Controls.Add(Me.Label28)
        Me.TabPage_PlayspaceCalib.Controls.Add(Me.LinkLabel_PlayCalibShowSettings)
        Me.TabPage_PlayspaceCalib.Controls.Add(Me.Label27)
        Me.TabPage_PlayspaceCalib.Controls.Add(Me.Button_PlaySpaceManualCalib)
        Me.TabPage_PlayspaceCalib.Controls.Add(Me.Label24)
        Me.TabPage_PlayspaceCalib.Controls.Add(Me.ClassPictureBoxQuality3)
        Me.TabPage_PlayspaceCalib.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_PlayspaceCalib.Name = "TabPage_PlayspaceCalib"
        Me.TabPage_PlayspaceCalib.Size = New System.Drawing.Size(760, 1251)
        Me.TabPage_PlayspaceCalib.TabIndex = 5
        Me.TabPage_PlayspaceCalib.Text = "Playspace Calibration"
        Me.TabPage_PlayspaceCalib.UseVisualStyleBackColor = True
        '
        'Panel_PlayCalibSteps
        '
        Me.Panel_PlayCalibSteps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_PlayCalibSteps.AutoSize = True
        Me.Panel_PlayCalibSteps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel16)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel15)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel14)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel17)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel13)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel18)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel11)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel9)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel7)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel5)
        Me.Panel_PlayCalibSteps.Location = New System.Drawing.Point(16, 182)
        Me.Panel_PlayCalibSteps.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_PlayCalibSteps.Name = "Panel_PlayCalibSteps"
        Me.Panel_PlayCalibSteps.Size = New System.Drawing.Size(728, 367)
        Me.Panel_PlayCalibSteps.TabIndex = 69
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.LightGray
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(0, 301)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(726, 1)
        Me.Panel16.TabIndex = 7
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.Label38)
        Me.Panel15.Controls.Add(Me.Label39)
        Me.Panel15.Controls.Add(Me.ClassPictureBoxQuality_CalibStep5)
        Me.Panel15.Controls.Add(Me.ProgressBar_PlayCalibStep5)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel15.Location = New System.Drawing.Point(0, 301)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(726, 64)
        Me.Panel15.TabIndex = 6
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(59, 22)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(240, 13)
        Me.Label38.TabIndex = 5
        Me.Label38.Text = "Playspace has been successfuly synchronized!"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(59, 6)
        Me.Label39.Margin = New System.Windows.Forms.Padding(3)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(162, 13)
        Me.Label39.TabIndex = 1
        Me.Label39.Text = "Step 5: Calibration Completed"
        '
        'ProgressBar_PlayCalibStep5
        '
        Me.ProgressBar_PlayCalibStep5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar_PlayCalibStep5.Location = New System.Drawing.Point(0, 54)
        Me.ProgressBar_PlayCalibStep5.Name = "ProgressBar_PlayCalibStep5"
        Me.ProgressBar_PlayCalibStep5.Size = New System.Drawing.Size(726, 10)
        Me.ProgressBar_PlayCalibStep5.TabIndex = 4
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.Label36)
        Me.Panel14.Controls.Add(Me.Label37)
        Me.Panel14.Controls.Add(Me.ClassPictureBoxQuality_CalibStep4)
        Me.Panel14.Controls.Add(Me.ProgressBar_PlayCalibStep4)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(0, 237)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(726, 64)
        Me.Panel14.TabIndex = 5
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(59, 22)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(205, 13)
        Me.Label36.TabIndex = 5
        Me.Label36.Text = "Stand still to sample the ending point."
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(59, 6)
        Me.Label37.Margin = New System.Windows.Forms.Padding(3)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(148, 13)
        Me.Label37.TabIndex = 1
        Me.Label37.Text = "Step 4: Sampling End Point"
        '
        'ProgressBar_PlayCalibStep4
        '
        Me.ProgressBar_PlayCalibStep4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar_PlayCalibStep4.Location = New System.Drawing.Point(0, 54)
        Me.ProgressBar_PlayCalibStep4.Name = "ProgressBar_PlayCalibStep4"
        Me.ProgressBar_PlayCalibStep4.Size = New System.Drawing.Size(726, 10)
        Me.ProgressBar_PlayCalibStep4.TabIndex = 4
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.LightGray
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17.Location = New System.Drawing.Point(0, 236)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(726, 1)
        Me.Panel17.TabIndex = 6
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.Label34)
        Me.Panel13.Controls.Add(Me.Label32)
        Me.Panel13.Controls.Add(Me.ClassPictureBoxQuality_CalibStep3)
        Me.Panel13.Controls.Add(Me.ProgressBar_PlayCalibStep3)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 172)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(726, 64)
        Me.Panel13.TabIndex = 4
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(59, 22)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(240, 13)
        Me.Label34.TabIndex = 5
        Me.Label34.Text = "Take some steps forward and then stand still."
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(59, 6)
        Me.Label32.Margin = New System.Windows.Forms.Padding(3)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(121, 13)
        Me.Label32.TabIndex = 1
        Me.Label32.Text = "Step 3: Move Forward"
        '
        'ProgressBar_PlayCalibStep3
        '
        Me.ProgressBar_PlayCalibStep3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar_PlayCalibStep3.Location = New System.Drawing.Point(0, 54)
        Me.ProgressBar_PlayCalibStep3.Name = "ProgressBar_PlayCalibStep3"
        Me.ProgressBar_PlayCalibStep3.Size = New System.Drawing.Size(726, 10)
        Me.ProgressBar_PlayCalibStep3.TabIndex = 4
        '
        'Panel18
        '
        Me.Panel18.BackColor = System.Drawing.Color.LightGray
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel18.Location = New System.Drawing.Point(0, 171)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(726, 1)
        Me.Panel18.TabIndex = 8
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Label35)
        Me.Panel11.Controls.Add(Me.Label33)
        Me.Panel11.Controls.Add(Me.ClassPictureBoxQuality_CalibStep2)
        Me.Panel11.Controls.Add(Me.ProgressBar_PlayCalibStep2)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 107)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(726, 64)
        Me.Panel11.TabIndex = 3
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(59, 22)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(208, 13)
        Me.Label35.TabIndex = 6
        Me.Label35.Text = "Stand still to sample the starting point."
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(59, 6)
        Me.Label33.Margin = New System.Windows.Forms.Padding(3)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(152, 13)
        Me.Label33.TabIndex = 1
        Me.Label33.Text = "Step 2: Sampling Start Point"
        '
        'ProgressBar_PlayCalibStep2
        '
        Me.ProgressBar_PlayCalibStep2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar_PlayCalibStep2.Location = New System.Drawing.Point(0, 54)
        Me.ProgressBar_PlayCalibStep2.Name = "ProgressBar_PlayCalibStep2"
        Me.ProgressBar_PlayCalibStep2.Size = New System.Drawing.Size(726, 10)
        Me.ProgressBar_PlayCalibStep2.TabIndex = 4
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.LightGray
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 106)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(726, 1)
        Me.Panel9.TabIndex = 2
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label31)
        Me.Panel7.Controls.Add(Me.Label30)
        Me.Panel7.Controls.Add(Me.ClassPictureBoxQuality_CalibStep1)
        Me.Panel7.Controls.Add(Me.ProgressBar_PlayCalibStep1)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 42)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(726, 64)
        Me.Panel7.TabIndex = 1
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(59, 22)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(562, 26)
        Me.Label31.TabIndex = 2
        Me.Label31.Text = "Hold the controller bulb in front of your head mount display." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Make sure you have" &
    " the controller bulb always in front of your head mount display while doing thes" &
    "e steps!"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(59, 6)
        Me.Label30.Margin = New System.Windows.Forms.Padding(3)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(106, 13)
        Me.Label30.TabIndex = 1
        Me.Label30.Text = "Step 1: Preparation"
        '
        'ProgressBar_PlayCalibStep1
        '
        Me.ProgressBar_PlayCalibStep1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar_PlayCalibStep1.Location = New System.Drawing.Point(0, 54)
        Me.ProgressBar_PlayCalibStep1.Name = "ProgressBar_PlayCalibStep1"
        Me.ProgressBar_PlayCalibStep1.Size = New System.Drawing.Size(726, 10)
        Me.ProgressBar_PlayCalibStep1.TabIndex = 3
        '
        'Panel5
        '
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Label_PlayCalibTitle)
        Me.Panel5.Controls.Add(Me.Panel_PlayCalibStatus)
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(726, 42)
        Me.Panel5.TabIndex = 0
        '
        'Label_PlayCalibTitle
        '
        Me.Label_PlayCalibTitle.BackColor = System.Drawing.Color.Transparent
        Me.Label_PlayCalibTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PlayCalibTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_PlayCalibTitle.ForeColor = System.Drawing.Color.Navy
        Me.Label_PlayCalibTitle.Location = New System.Drawing.Point(19, 0)
        Me.Label_PlayCalibTitle.Name = "Label_PlayCalibTitle"
        Me.Label_PlayCalibTitle.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label_PlayCalibTitle.Size = New System.Drawing.Size(707, 41)
        Me.Label_PlayCalibTitle.TabIndex = 1
        Me.Label_PlayCalibTitle.Text = "Playspace Calibration Steps"
        Me.Label_PlayCalibTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel_PlayCalibStatus
        '
        Me.Panel_PlayCalibStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel_PlayCalibStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel_PlayCalibStatus.Location = New System.Drawing.Point(0, 0)
        Me.Panel_PlayCalibStatus.Name = "Panel_PlayCalibStatus"
        Me.Panel_PlayCalibStatus.Size = New System.Drawing.Size(19, 41)
        Me.Panel_PlayCalibStatus.TabIndex = 2
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Gray
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 41)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(726, 1)
        Me.Panel6.TabIndex = 0
        '
        'ComboBox_PlayCalibControllerID
        '
        Me.ComboBox_PlayCalibControllerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_PlayCalibControllerID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_PlayCalibControllerID.FormattingEnabled = True
        Me.ComboBox_PlayCalibControllerID.Location = New System.Drawing.Point(98, 104)
        Me.ComboBox_PlayCalibControllerID.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_PlayCalibControllerID.Name = "ComboBox_PlayCalibControllerID"
        Me.ComboBox_PlayCalibControllerID.Size = New System.Drawing.Size(219, 21)
        Me.ComboBox_PlayCalibControllerID.TabIndex = 68
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(16, 107)
        Me.Label28.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(76, 13)
        Me.Label28.TabIndex = 67
        Me.Label28.Text = "Controller ID:"
        '
        'LinkLabel_PlayCalibShowSettings
        '
        Me.LinkLabel_PlayCalibShowSettings.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_PlayCalibShowSettings.AutoSize = True
        Me.LinkLabel_PlayCalibShowSettings.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_PlayCalibShowSettings.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_PlayCalibShowSettings.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_PlayCalibShowSettings.Location = New System.Drawing.Point(236, 70)
        Me.LinkLabel_PlayCalibShowSettings.Name = "LinkLabel_PlayCalibShowSettings"
        Me.LinkLabel_PlayCalibShowSettings.Size = New System.Drawing.Size(81, 13)
        Me.LinkLabel_PlayCalibShowSettings.TabIndex = 23
        Me.LinkLabel_PlayCalibShowSettings.TabStop = True
        Me.LinkLabel_PlayCalibShowSettings.Text = "Show Settings"
        Me.LinkLabel_PlayCalibShowSettings.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label27.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label27.Location = New System.Drawing.Point(13, 131)
        Me.Label27.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.Label27.Name = "Label27"
        Me.Label27.Padding = New System.Windows.Forms.Padding(3)
        Me.Label27.Size = New System.Drawing.Size(643, 32)
        Me.Label27.TabIndex = 66
        Me.Label27.Text = "        PSMove controllers do not need manual calibration, use the buttons SELECT" &
    "+START to recenter the playspace instead." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "        See settings for more details" &
    "."
        '
        'Button_PlaySpaceManualCalib
        '
        Me.Button_PlaySpaceManualCalib.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.Button_PlaySpaceManualCalib.Location = New System.Drawing.Point(16, 65)
        Me.Button_PlaySpaceManualCalib.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Button_PlaySpaceManualCalib.Name = "Button_PlaySpaceManualCalib"
        Me.Button_PlaySpaceManualCalib.Size = New System.Drawing.Size(214, 23)
        Me.Button_PlaySpaceManualCalib.TabIndex = 65
        Me.Button_PlaySpaceManualCalib.Text = "Start Manual Calibration"
        Me.Button_PlaySpaceManualCalib.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_PlaySpaceManualCalib.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_PlaySpaceManualCalib.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label24.Location = New System.Drawing.Point(38, 16)
        Me.Label24.Margin = New System.Windows.Forms.Padding(3, 16, 16, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(706, 33)
        Me.Label24.TabIndex = 27
        Me.Label24.Text = "Playspace calibrator synchronizes the PSMoveService tracking universe with anothe" &
    "r." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "If you use third party head mount displays this step is required to use both" &
    " tracking methods at the same time."
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
        Me.TabPage_Overrides.Size = New System.Drawing.Size(760, 1251)
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
        'ContextMenuStrip_Autostart
        '
        Me.ContextMenuStrip_Autostart.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip_Autostart.Size = New System.Drawing.Size(61, 4)
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
        'ClassPictureBoxQuality_CalibStep5
        '
        Me.ClassPictureBoxQuality_CalibStep5.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClassPictureBoxQuality_CalibStep5.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_32x32_32
        Me.ClassPictureBoxQuality_CalibStep5.Location = New System.Drawing.Point(0, 0)
        Me.ClassPictureBoxQuality_CalibStep5.m_HighQuality = True
        Me.ClassPictureBoxQuality_CalibStep5.Name = "ClassPictureBoxQuality_CalibStep5"
        Me.ClassPictureBoxQuality_CalibStep5.Size = New System.Drawing.Size(53, 54)
        Me.ClassPictureBoxQuality_CalibStep5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ClassPictureBoxQuality_CalibStep5.TabIndex = 0
        Me.ClassPictureBoxQuality_CalibStep5.TabStop = False
        '
        'ClassPictureBoxQuality_CalibStep4
        '
        Me.ClassPictureBoxQuality_CalibStep4.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClassPictureBoxQuality_CalibStep4.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_32x32_32
        Me.ClassPictureBoxQuality_CalibStep4.Location = New System.Drawing.Point(0, 0)
        Me.ClassPictureBoxQuality_CalibStep4.m_HighQuality = True
        Me.ClassPictureBoxQuality_CalibStep4.Name = "ClassPictureBoxQuality_CalibStep4"
        Me.ClassPictureBoxQuality_CalibStep4.Size = New System.Drawing.Size(53, 54)
        Me.ClassPictureBoxQuality_CalibStep4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ClassPictureBoxQuality_CalibStep4.TabIndex = 0
        Me.ClassPictureBoxQuality_CalibStep4.TabStop = False
        '
        'ClassPictureBoxQuality_CalibStep3
        '
        Me.ClassPictureBoxQuality_CalibStep3.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClassPictureBoxQuality_CalibStep3.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_32x32_32
        Me.ClassPictureBoxQuality_CalibStep3.Location = New System.Drawing.Point(0, 0)
        Me.ClassPictureBoxQuality_CalibStep3.m_HighQuality = True
        Me.ClassPictureBoxQuality_CalibStep3.Name = "ClassPictureBoxQuality_CalibStep3"
        Me.ClassPictureBoxQuality_CalibStep3.Size = New System.Drawing.Size(53, 54)
        Me.ClassPictureBoxQuality_CalibStep3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ClassPictureBoxQuality_CalibStep3.TabIndex = 0
        Me.ClassPictureBoxQuality_CalibStep3.TabStop = False
        '
        'ClassPictureBoxQuality_CalibStep2
        '
        Me.ClassPictureBoxQuality_CalibStep2.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClassPictureBoxQuality_CalibStep2.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_32x32_32
        Me.ClassPictureBoxQuality_CalibStep2.Location = New System.Drawing.Point(0, 0)
        Me.ClassPictureBoxQuality_CalibStep2.m_HighQuality = True
        Me.ClassPictureBoxQuality_CalibStep2.Name = "ClassPictureBoxQuality_CalibStep2"
        Me.ClassPictureBoxQuality_CalibStep2.Size = New System.Drawing.Size(53, 54)
        Me.ClassPictureBoxQuality_CalibStep2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ClassPictureBoxQuality_CalibStep2.TabIndex = 0
        Me.ClassPictureBoxQuality_CalibStep2.TabStop = False
        '
        'ClassPictureBoxQuality_CalibStep1
        '
        Me.ClassPictureBoxQuality_CalibStep1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClassPictureBoxQuality_CalibStep1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_32x32_32
        Me.ClassPictureBoxQuality_CalibStep1.Location = New System.Drawing.Point(0, 0)
        Me.ClassPictureBoxQuality_CalibStep1.m_HighQuality = True
        Me.ClassPictureBoxQuality_CalibStep1.Name = "ClassPictureBoxQuality_CalibStep1"
        Me.ClassPictureBoxQuality_CalibStep1.Size = New System.Drawing.Size(53, 54)
        Me.ClassPictureBoxQuality_CalibStep1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ClassPictureBoxQuality_CalibStep1.TabIndex = 0
        Me.ClassPictureBoxQuality_CalibStep1.TabStop = False
        '
        'ClassPictureBoxQuality3
        '
        Me.ClassPictureBoxQuality3.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.ClassPictureBoxQuality3.Location = New System.Drawing.Point(16, 16)
        Me.ClassPictureBoxQuality3.m_HighQuality = False
        Me.ClassPictureBoxQuality3.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.ClassPictureBoxQuality3.Name = "ClassPictureBoxQuality3"
        Me.ClassPictureBoxQuality3.Size = New System.Drawing.Size(16, 16)
        Me.ClassPictureBoxQuality3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality3.TabIndex = 26
        Me.ClassPictureBoxQuality3.TabStop = False
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
        Me.Controls.Add(Me.TabControl_Vmt)
        Me.Controls.Add(Me.LinkLabel_ReadMore)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVirtualMotionTracker"
        Me.Size = New System.Drawing.Size(800, 1371)
        Me.TabControl_Vmt.ResumeLayout(False)
        Me.TabPage_Management.ResumeLayout(False)
        Me.Panel_AvailableDevices.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel_Status.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.TabPage_Trackers.ResumeLayout(False)
        Me.TabPage_Settings.ResumeLayout(False)
        Me.TabControl_SettingsDevices.ResumeLayout(False)
        Me.TabPage_SettingsPSmove.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumericUpDown_PlayCalibForwardOffset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_RecenterButtonTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.NumericUpDown_TouchpadTouchArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_TouchpadClickDeadzone, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_SettingsOther.ResumeLayout(False)
        Me.TabPage_SettingsOther.PerformLayout()
        CType(Me.NumericUpDown_OscThreadSleep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_PlayspaceCalib.ResumeLayout(False)
        Me.TabPage_PlayspaceCalib.PerformLayout()
        Me.Panel_PlayCalibSteps.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.TabPage_Overrides.ResumeLayout(False)
        Me.Panel_SteamVRRestart.ResumeLayout(False)
        Me.Panel_SteamVRRestart.PerformLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality_CalibStep5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality_CalibStep4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality_CalibStep3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality_CalibStep2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality_CalibStep1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LinkLabel_ReadMore As LinkLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As ClassPictureBoxQuality
    Friend WithEvents TabControl_Vmt As TabControl
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
    Friend WithEvents CheckBox_TouchpadShortcutClick As CheckBox
    Friend WithEvents Button_SaveControllerSettings As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ComboBox_TouchpadClickMethod As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ComboBox_GrabButtonMethod As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TabControl_SettingsDevices As TabControl
    Friend WithEvents TabPage_SettingsPSmove As TabPage
    Friend WithEvents TabPage_SettingsOther As TabPage
    Friend WithEvents CheckBox_DisableBasestations As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CheckBox_EnableHeptics As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TabPage_Management As TabPage
    Friend WithEvents Panel_Status As Panel
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
    Friend WithEvents Panel_AvailableDevices As Panel
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
    Friend WithEvents LinkLabel_SteamSettings As LinkLabel
    Friend WithEvents LinkLabel_TouchpadShortcutHelp As LinkLabel
    Friend WithEvents NumericUpDown_TouchpadClickDeadzone As NumericUpDown
    Friend WithEvents Label22 As Label
    Friend WithEvents NumericUpDown_TouchpadTouchArea As NumericUpDown
    Friend WithEvents Label23 As Label
    Friend WithEvents Label_TouchpadTouchAreaDeg As Label
    Friend WithEvents TabPage_PlayspaceCalib As TabPage
    Friend WithEvents Label24 As Label
    Friend WithEvents ClassPictureBoxQuality3 As ClassPictureBoxQuality
    Friend WithEvents Button_PlaySpaceManualCalib As Button
    Friend WithEvents NumericUpDown_PlayCalibForwardOffset As NumericUpDown
    Friend WithEvents Label26 As Label
    Friend WithEvents Button_PlayCalibReset As Button
    Friend WithEvents Label25 As Label
    Friend WithEvents CheckBox_PlayCalibEnabled As CheckBox
    Friend WithEvents LinkLabel_PlayCalibShowSettings As LinkLabel
    Friend WithEvents Label27 As Label
    Friend WithEvents ComboBox_PlayCalibControllerID As ComboBox
    Friend WithEvents Label28 As Label
    Friend WithEvents Panel_PlayCalibSteps As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents ClassPictureBoxQuality_CalibStep1 As ClassPictureBoxQuality
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label_PlayCalibTitle As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Label31 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents Panel11 As Panel
    Friend WithEvents ProgressBar_PlayCalibStep2 As ProgressBar
    Friend WithEvents Label33 As Label
    Friend WithEvents ClassPictureBoxQuality_CalibStep2 As ClassPictureBoxQuality
    Friend WithEvents ProgressBar_PlayCalibStep1 As ProgressBar
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Label38 As Label
    Friend WithEvents ProgressBar_PlayCalibStep5 As ProgressBar
    Friend WithEvents Label39 As Label
    Friend WithEvents ClassPictureBoxQuality_CalibStep5 As ClassPictureBoxQuality
    Friend WithEvents Panel14 As Panel
    Friend WithEvents Label36 As Label
    Friend WithEvents ProgressBar_PlayCalibStep4 As ProgressBar
    Friend WithEvents Label37 As Label
    Friend WithEvents ClassPictureBoxQuality_CalibStep4 As ClassPictureBoxQuality
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Label34 As Label
    Friend WithEvents ProgressBar_PlayCalibStep3 As ProgressBar
    Friend WithEvents Label32 As Label
    Friend WithEvents ClassPictureBoxQuality_CalibStep3 As ClassPictureBoxQuality
    Friend WithEvents Label35 As Label
    Friend WithEvents Panel_PlayCalibStatus As Panel
    Friend WithEvents Panel16 As Panel
    Friend WithEvents Panel17 As Panel
    Friend WithEvents Panel18 As Panel
    Friend WithEvents Label_ScrollFocus As Label
End Class
