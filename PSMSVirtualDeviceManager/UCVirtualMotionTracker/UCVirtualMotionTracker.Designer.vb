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
        Me.TabControl_Vmt = New System.Windows.Forms.TabControl()
        Me.TabPage_Management = New System.Windows.Forms.TabPage()
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
        Me.TabPage_Trackers = New System.Windows.Forms.TabPage()
        Me.ListView_Trackers = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip_Trackers = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_TrackerRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel_VMTTrackers = New System.Windows.Forms.Panel()
        Me.Button_VMTControllers = New System.Windows.Forms.Button()
        Me.Button_AddVMTController = New System.Windows.Forms.Button()
        Me.TabPage_Settings = New System.Windows.Forms.TabPage()
        Me.TabControl_SettingsDevices = New System.Windows.Forms.TabControl()
        Me.TabPage_SettingsPSVR = New System.Windows.Forms.TabPage()
        Me.GroupBox_Distortion = New System.Windows.Forms.GroupBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.LinkLabel_PsvrDistReset = New System.Windows.Forms.LinkLabel()
        Me.NumericUpDown_PsvrVFov = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig14 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PsvrHFov = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig13 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PsvrDistBlueOffset = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig16 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PsvrDistGreenOffset = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig15 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PsvrDistRedOffset = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig11 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PsvrDistScale = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig12 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PsvrDistK1 = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig10 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PsvrDistK0 = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig9 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.CheckBox_ShowDistSettings = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown_PsvrIPD = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig8 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox_PsvrRenderResolution = New System.Windows.Forms.ComboBox()
        Me.TabPage_SettingsPSmove = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LinkLabel_PlayCalibShowSettings2 = New System.Windows.Forms.LinkLabel()
        Me.Label_ScrollFocus = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.CheckBox_PlayCalibEnabled = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown_RecenterButtonTime = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig7 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Button_ResetRecenter = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.ComboBox_HmdRecenterFromDevice = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.ComboBox_HmdRecenterMethod = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
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
        Me.UcNumericUpDownBig5 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.NumericUpDown_TouchpadClickDeadzone = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig6 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
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
        Me.TabPage_SettingsPlayspace = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PlayCalibSideOffset = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig3 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.ComboBox_PlayCalibForwardMethod = New System.Windows.Forms.ComboBox()
        Me.Button_PlayCalibReset = New System.Windows.Forms.Button()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PlayCalibHeightOffset = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig2 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PlayCalibForwardOffset = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig1 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.TabPage_SettingsOther = New System.Windows.Forms.TabPage()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.CheckBox_OptimizePackets = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown_OscThreadSleep = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig4 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CheckBox_EnableHeptics = New System.Windows.Forms.CheckBox()
        Me.CheckBox_DisableBasestations = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button_SaveControllerSettings = New System.Windows.Forms.Button()
        Me.TabPage_PlayspaceCalib = New System.Windows.Forms.TabPage()
        Me.NumericUpDown_PlayCalibPrepTime = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig17 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Panel_PlayCalibSteps = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality_CalibStep5 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ProgressBar_PlayCalibStep5 = New System.Windows.Forms.ProgressBar()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality_CalibStep4 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ProgressBar_PlayCalibStep4 = New System.Windows.Forms.ProgressBar()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality_CalibStep3 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ProgressBar_PlayCalibStep3 = New System.Windows.Forms.ProgressBar()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality_CalibStep2 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ProgressBar_PlayCalibStep2 = New System.Windows.Forms.ProgressBar()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality_CalibStep1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
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
        Me.ClassPictureBoxQuality3 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
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
        Me.ToolTip_Info = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer_VMTTrackers = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ClassPictureBoxQuality4 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip_AddTracker = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_AddTracker = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_AddHmd = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip_Default = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControl_Vmt.SuspendLayout()
        Me.TabPage_Management.SuspendLayout()
        Me.Panel_AvailableDevices.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel_Status.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.TabPage_Trackers.SuspendLayout()
        Me.ContextMenuStrip_Trackers.SuspendLayout()
        Me.TabPage_Settings.SuspendLayout()
        Me.TabControl_SettingsDevices.SuspendLayout()
        Me.TabPage_SettingsPSVR.SuspendLayout()
        Me.GroupBox_Distortion.SuspendLayout()
        CType(Me.NumericUpDown_PsvrVFov, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PsvrVFov.SuspendLayout()
        CType(Me.NumericUpDown_PsvrHFov, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PsvrHFov.SuspendLayout()
        CType(Me.NumericUpDown_PsvrDistBlueOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PsvrDistBlueOffset.SuspendLayout()
        CType(Me.NumericUpDown_PsvrDistGreenOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PsvrDistGreenOffset.SuspendLayout()
        CType(Me.NumericUpDown_PsvrDistRedOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PsvrDistRedOffset.SuspendLayout()
        CType(Me.NumericUpDown_PsvrDistScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PsvrDistScale.SuspendLayout()
        CType(Me.NumericUpDown_PsvrDistK1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PsvrDistK1.SuspendLayout()
        CType(Me.NumericUpDown_PsvrDistK0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PsvrDistK0.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.NumericUpDown_PsvrIPD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PsvrIPD.SuspendLayout()
        Me.TabPage_SettingsPSmove.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumericUpDown_RecenterButtonTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_RecenterButtonTime.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.NumericUpDown_TouchpadTouchArea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_TouchpadTouchArea.SuspendLayout()
        CType(Me.NumericUpDown_TouchpadClickDeadzone, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_TouchpadClickDeadzone.SuspendLayout()
        Me.TabPage_SettingsPlayspace.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.NumericUpDown_PlayCalibSideOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PlayCalibSideOffset.SuspendLayout()
        CType(Me.NumericUpDown_PlayCalibHeightOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PlayCalibHeightOffset.SuspendLayout()
        CType(Me.NumericUpDown_PlayCalibForwardOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PlayCalibForwardOffset.SuspendLayout()
        Me.TabPage_SettingsOther.SuspendLayout()
        CType(Me.NumericUpDown_OscThreadSleep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_OscThreadSleep.SuspendLayout()
        Me.TabPage_PlayspaceCalib.SuspendLayout()
        CType(Me.NumericUpDown_PlayCalibPrepTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PlayCalibPrepTime.SuspendLayout()
        Me.Panel_PlayCalibSteps.SuspendLayout()
        Me.Panel15.SuspendLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel14.SuspendLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel13.SuspendLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel11.SuspendLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        CType(Me.ClassPictureBoxQuality3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_Overrides.SuspendLayout()
        Me.Panel_SteamVRRestart.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.ClassPictureBoxQuality4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip_AddTracker.SuspendLayout()
        Me.SuspendLayout()
        '
        'LinkLabel_ReadMore
        '
        Me.LinkLabel_ReadMore.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ReadMore.AutoSize = True
        Me.LinkLabel_ReadMore.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ReadMore.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ReadMore.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ReadMore.Location = New System.Drawing.Point(67, 60)
        Me.LinkLabel_ReadMore.Margin = New System.Windows.Forms.Padding(3, 0, 3, 8)
        Me.LinkLabel_ReadMore.Name = "LinkLabel_ReadMore"
        Me.LinkLabel_ReadMore.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.LinkLabel_ReadMore.Size = New System.Drawing.Size(62, 16)
        Me.LinkLabel_ReadMore.TabIndex = 21
        Me.LinkLabel_ReadMore.TabStop = True
        Me.LinkLabel_ReadMore.Text = "Read more"
        Me.LinkLabel_ReadMore.VisitedLinkColor = System.Drawing.Color.RoyalBlue
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
        Me.TabControl_Vmt.Location = New System.Drawing.Point(16, 104)
        Me.TabControl_Vmt.Margin = New System.Windows.Forms.Padding(16)
        Me.TabControl_Vmt.Name = "TabControl_Vmt"
        Me.TabControl_Vmt.SelectedIndex = 0
        Me.TabControl_Vmt.Size = New System.Drawing.Size(768, 1251)
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
        Me.TabPage_Management.Size = New System.Drawing.Size(760, 1225)
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
        Me.Panel_AvailableDevices.Location = New System.Drawing.Point(16, 226)
        Me.Panel_AvailableDevices.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_AvailableDevices.Name = "Panel_AvailableDevices"
        Me.Panel_AvailableDevices.Size = New System.Drawing.Size(728, 250)
        Me.Panel_AvailableDevices.TabIndex = 2
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
        Me.Panel_Status.Size = New System.Drawing.Size(728, 178)
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(726, 134)
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
        Me.Panel4.Size = New System.Drawing.Size(362, 134)
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
        Me.Panel2.Location = New System.Drawing.Point(362, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(364, 134)
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
        Me.TabPage_Trackers.BackColor = System.Drawing.Color.White
        Me.TabPage_Trackers.Controls.Add(Me.ListView_Trackers)
        Me.TabPage_Trackers.Controls.Add(Me.Panel_VMTTrackers)
        Me.TabPage_Trackers.Controls.Add(Me.Button_VMTControllers)
        Me.TabPage_Trackers.Controls.Add(Me.Button_AddVMTController)
        Me.TabPage_Trackers.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Trackers.Name = "TabPage_Trackers"
        Me.TabPage_Trackers.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Trackers.Size = New System.Drawing.Size(760, 1225)
        Me.TabPage_Trackers.TabIndex = 0
        Me.TabPage_Trackers.Text = "Trackers"
        '
        'ListView_Trackers
        '
        Me.ListView_Trackers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_Trackers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.ListView_Trackers.ContextMenuStrip = Me.ContextMenuStrip_Trackers
        Me.ListView_Trackers.FullRowSelect = True
        Me.ListView_Trackers.HideSelection = False
        Me.ListView_Trackers.Location = New System.Drawing.Point(19, 58)
        Me.ListView_Trackers.Margin = New System.Windows.Forms.Padding(16)
        Me.ListView_Trackers.MultiSelect = False
        Me.ListView_Trackers.Name = "ListView_Trackers"
        Me.ListView_Trackers.Size = New System.Drawing.Size(722, 150)
        Me.ListView_Trackers.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView_Trackers.TabIndex = 21
        Me.ListView_Trackers.UseCompatibleStateImageBehavior = False
        Me.ListView_Trackers.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Type"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "ID"
        Me.ColumnHeader3.Width = 75
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "VMT ID"
        Me.ColumnHeader4.Width = 75
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Tracker Role"
        Me.ColumnHeader5.Width = 400
        '
        'ContextMenuStrip_Trackers
        '
        Me.ContextMenuStrip_Trackers.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_TrackerRemove})
        Me.ContextMenuStrip_Trackers.Name = "ContextMenuStrip_Trackers"
        Me.ContextMenuStrip_Trackers.Size = New System.Drawing.Size(118, 26)
        '
        'ToolStripMenuItem_TrackerRemove
        '
        Me.ToolStripMenuItem_TrackerRemove.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.ToolStripMenuItem_TrackerRemove.Name = "ToolStripMenuItem_TrackerRemove"
        Me.ToolStripMenuItem_TrackerRemove.Size = New System.Drawing.Size(117, 22)
        Me.ToolStripMenuItem_TrackerRemove.Text = "Remove"
        '
        'Panel_VMTTrackers
        '
        Me.Panel_VMTTrackers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_VMTTrackers.AutoScroll = True
        Me.Panel_VMTTrackers.Location = New System.Drawing.Point(19, 240)
        Me.Panel_VMTTrackers.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_VMTTrackers.Name = "Panel_VMTTrackers"
        Me.Panel_VMTTrackers.Size = New System.Drawing.Size(722, 966)
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
        Me.Button_AddVMTController.Text = "Add tracker..."
        Me.Button_AddVMTController.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_AddVMTController.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_AddVMTController.UseVisualStyleBackColor = True
        '
        'TabPage_Settings
        '
        Me.TabPage_Settings.BackColor = System.Drawing.Color.White
        Me.TabPage_Settings.Controls.Add(Me.TabControl_SettingsDevices)
        Me.TabPage_Settings.Controls.Add(Me.Button_SaveControllerSettings)
        Me.TabPage_Settings.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Settings.Name = "TabPage_Settings"
        Me.TabPage_Settings.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Settings.Size = New System.Drawing.Size(760, 1225)
        Me.TabPage_Settings.TabIndex = 3
        Me.TabPage_Settings.Text = "Settings"
        '
        'TabControl_SettingsDevices
        '
        Me.TabControl_SettingsDevices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl_SettingsDevices.Controls.Add(Me.TabPage_SettingsPSVR)
        Me.TabControl_SettingsDevices.Controls.Add(Me.TabPage_SettingsPSmove)
        Me.TabControl_SettingsDevices.Controls.Add(Me.TabPage_SettingsPlayspace)
        Me.TabControl_SettingsDevices.Controls.Add(Me.TabPage_SettingsOther)
        Me.TabControl_SettingsDevices.Location = New System.Drawing.Point(6, 6)
        Me.TabControl_SettingsDevices.Name = "TabControl_SettingsDevices"
        Me.TabControl_SettingsDevices.SelectedIndex = 0
        Me.TabControl_SettingsDevices.Size = New System.Drawing.Size(748, 1158)
        Me.TabControl_SettingsDevices.TabIndex = 47
        '
        'TabPage_SettingsPSVR
        '
        Me.TabPage_SettingsPSVR.BackColor = System.Drawing.Color.White
        Me.TabPage_SettingsPSVR.Controls.Add(Me.GroupBox_Distortion)
        Me.TabPage_SettingsPSVR.Controls.Add(Me.GroupBox4)
        Me.TabPage_SettingsPSVR.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_SettingsPSVR.Name = "TabPage_SettingsPSVR"
        Me.TabPage_SettingsPSVR.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_SettingsPSVR.Size = New System.Drawing.Size(740, 1132)
        Me.TabPage_SettingsPSVR.TabIndex = 3
        Me.TabPage_SettingsPSVR.Text = "PlayStation VR"
        '
        'GroupBox_Distortion
        '
        Me.GroupBox_Distortion.Controls.Add(Me.Label62)
        Me.GroupBox_Distortion.Controls.Add(Me.Label61)
        Me.GroupBox_Distortion.Controls.Add(Me.Label60)
        Me.GroupBox_Distortion.Controls.Add(Me.Label59)
        Me.GroupBox_Distortion.Controls.Add(Me.Label58)
        Me.GroupBox_Distortion.Controls.Add(Me.Label57)
        Me.GroupBox_Distortion.Controls.Add(Me.Label56)
        Me.GroupBox_Distortion.Controls.Add(Me.Label55)
        Me.GroupBox_Distortion.Controls.Add(Me.LinkLabel_PsvrDistReset)
        Me.GroupBox_Distortion.Controls.Add(Me.NumericUpDown_PsvrVFov)
        Me.GroupBox_Distortion.Controls.Add(Me.Label53)
        Me.GroupBox_Distortion.Controls.Add(Me.NumericUpDown_PsvrHFov)
        Me.GroupBox_Distortion.Controls.Add(Me.Label52)
        Me.GroupBox_Distortion.Controls.Add(Me.NumericUpDown_PsvrDistBlueOffset)
        Me.GroupBox_Distortion.Controls.Add(Me.Label51)
        Me.GroupBox_Distortion.Controls.Add(Me.NumericUpDown_PsvrDistGreenOffset)
        Me.GroupBox_Distortion.Controls.Add(Me.Label50)
        Me.GroupBox_Distortion.Controls.Add(Me.NumericUpDown_PsvrDistRedOffset)
        Me.GroupBox_Distortion.Controls.Add(Me.Label49)
        Me.GroupBox_Distortion.Controls.Add(Me.NumericUpDown_PsvrDistScale)
        Me.GroupBox_Distortion.Controls.Add(Me.Label48)
        Me.GroupBox_Distortion.Controls.Add(Me.NumericUpDown_PsvrDistK1)
        Me.GroupBox_Distortion.Controls.Add(Me.Label47)
        Me.GroupBox_Distortion.Controls.Add(Me.NumericUpDown_PsvrDistK0)
        Me.GroupBox_Distortion.Controls.Add(Me.Label46)
        Me.GroupBox_Distortion.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox_Distortion.Location = New System.Drawing.Point(3, 109)
        Me.GroupBox_Distortion.Name = "GroupBox_Distortion"
        Me.GroupBox_Distortion.Size = New System.Drawing.Size(734, 286)
        Me.GroupBox_Distortion.TabIndex = 4
        Me.GroupBox_Distortion.TabStop = False
        Me.GroupBox_Distortion.Text = "Distortion Settings"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.Color.Gray
        Me.Label62.Location = New System.Drawing.Point(334, 222)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(129, 13)
        Me.Label62.TabIndex = 26
        Me.Label62.Text = "(requires SteamVR restart)"
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.Color.Gray
        Me.Label61.Location = New System.Drawing.Point(334, 194)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(129, 13)
        Me.Label61.TabIndex = 25
        Me.Label61.Text = "(requires SteamVR restart)"
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.ForeColor = System.Drawing.Color.Gray
        Me.Label60.Location = New System.Drawing.Point(334, 166)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(129, 13)
        Me.Label60.TabIndex = 24
        Me.Label60.Text = "(requires SteamVR restart)"
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.Color.Gray
        Me.Label59.Location = New System.Drawing.Point(334, 138)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(129, 13)
        Me.Label59.TabIndex = 23
        Me.Label59.Text = "(requires SteamVR restart)"
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.ForeColor = System.Drawing.Color.Gray
        Me.Label58.Location = New System.Drawing.Point(334, 110)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(129, 13)
        Me.Label58.TabIndex = 22
        Me.Label58.Text = "(requires SteamVR restart)"
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.Color.Gray
        Me.Label57.Location = New System.Drawing.Point(334, 82)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(129, 13)
        Me.Label57.TabIndex = 21
        Me.Label57.Text = "(requires SteamVR restart)"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.Color.Gray
        Me.Label56.Location = New System.Drawing.Point(334, 54)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(129, 13)
        Me.Label56.TabIndex = 20
        Me.Label56.Text = "(requires SteamVR restart)"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.Color.Gray
        Me.Label55.Location = New System.Drawing.Point(334, 26)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(129, 13)
        Me.Label55.TabIndex = 6
        Me.Label55.Text = "(requires SteamVR restart)"
        '
        'LinkLabel_PsvrDistReset
        '
        Me.LinkLabel_PsvrDistReset.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_PsvrDistReset.AutoSize = True
        Me.LinkLabel_PsvrDistReset.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_PsvrDistReset.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_PsvrDistReset.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_PsvrDistReset.Location = New System.Drawing.Point(16, 254)
        Me.LinkLabel_PsvrDistReset.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.LinkLabel_PsvrDistReset.Name = "LinkLabel_PsvrDistReset"
        Me.LinkLabel_PsvrDistReset.Size = New System.Drawing.Size(124, 13)
        Me.LinkLabel_PsvrDistReset.TabIndex = 19
        Me.LinkLabel_PsvrDistReset.TabStop = True
        Me.LinkLabel_PsvrDistReset.Text = "Reset distortion values"
        Me.LinkLabel_PsvrDistReset.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'NumericUpDown_PsvrVFov
        '
        Me.NumericUpDown_PsvrVFov.Controls.Add(Me.UcNumericUpDownBig14)
        Me.NumericUpDown_PsvrVFov.Location = New System.Drawing.Point(146, 220)
        Me.NumericUpDown_PsvrVFov.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.NumericUpDown_PsvrVFov.Name = "NumericUpDown_PsvrVFov"
        Me.NumericUpDown_PsvrVFov.Size = New System.Drawing.Size(182, 22)
        Me.NumericUpDown_PsvrVFov.TabIndex = 18
        '
        'UcNumericUpDownBig14
        '
        Me.UcNumericUpDownBig14.AutoSize = True
        Me.UcNumericUpDownBig14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig14.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig14.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig14.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig14.Location = New System.Drawing.Point(138, 0)
        Me.UcNumericUpDownBig14.m_bDockOnControl = True
        Me.UcNumericUpDownBig14.m_NumericUpDown = Me.NumericUpDown_PsvrVFov
        Me.UcNumericUpDownBig14.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig14.m_ResetVisible = False
        Me.UcNumericUpDownBig14.Name = "UcNumericUpDownBig14"
        Me.UcNumericUpDownBig14.Size = New System.Drawing.Size(44, 22)
        Me.UcNumericUpDownBig14.TabIndex = 27
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(19, 222)
        Me.Label53.Margin = New System.Windows.Forms.Padding(16, 8, 3, 3)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(45, 13)
        Me.Label53.TabIndex = 17
        Me.Label53.Text = "V. FOV:"
        '
        'NumericUpDown_PsvrHFov
        '
        Me.NumericUpDown_PsvrHFov.Controls.Add(Me.UcNumericUpDownBig13)
        Me.NumericUpDown_PsvrHFov.Location = New System.Drawing.Point(146, 192)
        Me.NumericUpDown_PsvrHFov.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.NumericUpDown_PsvrHFov.Name = "NumericUpDown_PsvrHFov"
        Me.NumericUpDown_PsvrHFov.Size = New System.Drawing.Size(182, 22)
        Me.NumericUpDown_PsvrHFov.TabIndex = 16
        '
        'UcNumericUpDownBig13
        '
        Me.UcNumericUpDownBig13.AutoSize = True
        Me.UcNumericUpDownBig13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig13.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig13.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig13.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig13.Location = New System.Drawing.Point(138, 0)
        Me.UcNumericUpDownBig13.m_bDockOnControl = True
        Me.UcNumericUpDownBig13.m_NumericUpDown = Me.NumericUpDown_PsvrHFov
        Me.UcNumericUpDownBig13.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig13.m_ResetVisible = False
        Me.UcNumericUpDownBig13.Name = "UcNumericUpDownBig13"
        Me.UcNumericUpDownBig13.Size = New System.Drawing.Size(44, 22)
        Me.UcNumericUpDownBig13.TabIndex = 27
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(19, 194)
        Me.Label52.Margin = New System.Windows.Forms.Padding(16, 8, 3, 3)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(46, 13)
        Me.Label52.TabIndex = 15
        Me.Label52.Text = "H. FOV:"
        '
        'NumericUpDown_PsvrDistBlueOffset
        '
        Me.NumericUpDown_PsvrDistBlueOffset.Controls.Add(Me.UcNumericUpDownBig16)
        Me.NumericUpDown_PsvrDistBlueOffset.DecimalPlaces = 4
        Me.NumericUpDown_PsvrDistBlueOffset.Increment = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.NumericUpDown_PsvrDistBlueOffset.Location = New System.Drawing.Point(146, 164)
        Me.NumericUpDown_PsvrDistBlueOffset.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_PsvrDistBlueOffset.Name = "NumericUpDown_PsvrDistBlueOffset"
        Me.NumericUpDown_PsvrDistBlueOffset.Size = New System.Drawing.Size(182, 22)
        Me.NumericUpDown_PsvrDistBlueOffset.TabIndex = 14
        '
        'UcNumericUpDownBig16
        '
        Me.UcNumericUpDownBig16.AutoSize = True
        Me.UcNumericUpDownBig16.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig16.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig16.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig16.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig16.Location = New System.Drawing.Point(138, 0)
        Me.UcNumericUpDownBig16.m_bDockOnControl = True
        Me.UcNumericUpDownBig16.m_NumericUpDown = Me.NumericUpDown_PsvrDistBlueOffset
        Me.UcNumericUpDownBig16.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig16.m_ResetVisible = False
        Me.UcNumericUpDownBig16.Name = "UcNumericUpDownBig16"
        Me.UcNumericUpDownBig16.Size = New System.Drawing.Size(44, 22)
        Me.UcNumericUpDownBig16.TabIndex = 27
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(19, 166)
        Me.Label51.Margin = New System.Windows.Forms.Padding(16, 8, 3, 3)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(67, 13)
        Me.Label51.TabIndex = 13
        Me.Label51.Text = "Blue Offset:"
        '
        'NumericUpDown_PsvrDistGreenOffset
        '
        Me.NumericUpDown_PsvrDistGreenOffset.Controls.Add(Me.UcNumericUpDownBig15)
        Me.NumericUpDown_PsvrDistGreenOffset.DecimalPlaces = 4
        Me.NumericUpDown_PsvrDistGreenOffset.Increment = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.NumericUpDown_PsvrDistGreenOffset.Location = New System.Drawing.Point(146, 136)
        Me.NumericUpDown_PsvrDistGreenOffset.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_PsvrDistGreenOffset.Name = "NumericUpDown_PsvrDistGreenOffset"
        Me.NumericUpDown_PsvrDistGreenOffset.Size = New System.Drawing.Size(182, 22)
        Me.NumericUpDown_PsvrDistGreenOffset.TabIndex = 12
        '
        'UcNumericUpDownBig15
        '
        Me.UcNumericUpDownBig15.AutoSize = True
        Me.UcNumericUpDownBig15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig15.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig15.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig15.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig15.Location = New System.Drawing.Point(138, 0)
        Me.UcNumericUpDownBig15.m_bDockOnControl = True
        Me.UcNumericUpDownBig15.m_NumericUpDown = Me.NumericUpDown_PsvrDistGreenOffset
        Me.UcNumericUpDownBig15.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig15.m_ResetVisible = False
        Me.UcNumericUpDownBig15.Name = "UcNumericUpDownBig15"
        Me.UcNumericUpDownBig15.Size = New System.Drawing.Size(44, 22)
        Me.UcNumericUpDownBig15.TabIndex = 27
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(19, 138)
        Me.Label50.Margin = New System.Windows.Forms.Padding(16, 8, 3, 3)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(76, 13)
        Me.Label50.TabIndex = 11
        Me.Label50.Text = "Green Offset:"
        '
        'NumericUpDown_PsvrDistRedOffset
        '
        Me.NumericUpDown_PsvrDistRedOffset.Controls.Add(Me.UcNumericUpDownBig11)
        Me.NumericUpDown_PsvrDistRedOffset.DecimalPlaces = 4
        Me.NumericUpDown_PsvrDistRedOffset.Increment = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.NumericUpDown_PsvrDistRedOffset.Location = New System.Drawing.Point(146, 108)
        Me.NumericUpDown_PsvrDistRedOffset.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_PsvrDistRedOffset.Name = "NumericUpDown_PsvrDistRedOffset"
        Me.NumericUpDown_PsvrDistRedOffset.Size = New System.Drawing.Size(182, 22)
        Me.NumericUpDown_PsvrDistRedOffset.TabIndex = 10
        '
        'UcNumericUpDownBig11
        '
        Me.UcNumericUpDownBig11.AutoSize = True
        Me.UcNumericUpDownBig11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig11.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig11.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig11.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig11.Location = New System.Drawing.Point(138, 0)
        Me.UcNumericUpDownBig11.m_bDockOnControl = True
        Me.UcNumericUpDownBig11.m_NumericUpDown = Me.NumericUpDown_PsvrDistRedOffset
        Me.UcNumericUpDownBig11.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig11.m_ResetVisible = False
        Me.UcNumericUpDownBig11.Name = "UcNumericUpDownBig11"
        Me.UcNumericUpDownBig11.Size = New System.Drawing.Size(44, 22)
        Me.UcNumericUpDownBig11.TabIndex = 27
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(19, 110)
        Me.Label49.Margin = New System.Windows.Forms.Padding(16, 8, 3, 3)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(65, 13)
        Me.Label49.TabIndex = 9
        Me.Label49.Text = "Red Offset:"
        '
        'NumericUpDown_PsvrDistScale
        '
        Me.NumericUpDown_PsvrDistScale.Controls.Add(Me.UcNumericUpDownBig12)
        Me.NumericUpDown_PsvrDistScale.DecimalPlaces = 4
        Me.NumericUpDown_PsvrDistScale.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.NumericUpDown_PsvrDistScale.Location = New System.Drawing.Point(146, 80)
        Me.NumericUpDown_PsvrDistScale.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_PsvrDistScale.Name = "NumericUpDown_PsvrDistScale"
        Me.NumericUpDown_PsvrDistScale.Size = New System.Drawing.Size(182, 22)
        Me.NumericUpDown_PsvrDistScale.TabIndex = 7
        '
        'UcNumericUpDownBig12
        '
        Me.UcNumericUpDownBig12.AutoSize = True
        Me.UcNumericUpDownBig12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig12.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig12.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig12.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig12.Location = New System.Drawing.Point(138, 0)
        Me.UcNumericUpDownBig12.m_bDockOnControl = True
        Me.UcNumericUpDownBig12.m_NumericUpDown = Me.NumericUpDown_PsvrDistScale
        Me.UcNumericUpDownBig12.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig12.m_ResetVisible = False
        Me.UcNumericUpDownBig12.Name = "UcNumericUpDownBig12"
        Me.UcNumericUpDownBig12.Size = New System.Drawing.Size(44, 22)
        Me.UcNumericUpDownBig12.TabIndex = 27
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(19, 82)
        Me.Label48.Margin = New System.Windows.Forms.Padding(16, 8, 3, 3)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(91, 13)
        Me.Label48.TabIndex = 6
        Me.Label48.Text = "Distortion Scale:"
        '
        'NumericUpDown_PsvrDistK1
        '
        Me.NumericUpDown_PsvrDistK1.Controls.Add(Me.UcNumericUpDownBig10)
        Me.NumericUpDown_PsvrDistK1.DecimalPlaces = 4
        Me.NumericUpDown_PsvrDistK1.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.NumericUpDown_PsvrDistK1.Location = New System.Drawing.Point(146, 52)
        Me.NumericUpDown_PsvrDistK1.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_PsvrDistK1.Name = "NumericUpDown_PsvrDistK1"
        Me.NumericUpDown_PsvrDistK1.Size = New System.Drawing.Size(182, 22)
        Me.NumericUpDown_PsvrDistK1.TabIndex = 5
        '
        'UcNumericUpDownBig10
        '
        Me.UcNumericUpDownBig10.AutoSize = True
        Me.UcNumericUpDownBig10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig10.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig10.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig10.Location = New System.Drawing.Point(138, 0)
        Me.UcNumericUpDownBig10.m_bDockOnControl = True
        Me.UcNumericUpDownBig10.m_NumericUpDown = Me.NumericUpDown_PsvrDistK1
        Me.UcNumericUpDownBig10.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig10.m_ResetVisible = False
        Me.UcNumericUpDownBig10.Name = "UcNumericUpDownBig10"
        Me.UcNumericUpDownBig10.Size = New System.Drawing.Size(44, 22)
        Me.UcNumericUpDownBig10.TabIndex = 27
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(19, 54)
        Me.Label47.Margin = New System.Windows.Forms.Padding(16, 8, 3, 3)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(77, 13)
        Me.Label47.TabIndex = 4
        Me.Label47.Text = "Distortion K1:"
        '
        'NumericUpDown_PsvrDistK0
        '
        Me.NumericUpDown_PsvrDistK0.Controls.Add(Me.UcNumericUpDownBig9)
        Me.NumericUpDown_PsvrDistK0.DecimalPlaces = 4
        Me.NumericUpDown_PsvrDistK0.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.NumericUpDown_PsvrDistK0.Location = New System.Drawing.Point(146, 24)
        Me.NumericUpDown_PsvrDistK0.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_PsvrDistK0.Name = "NumericUpDown_PsvrDistK0"
        Me.NumericUpDown_PsvrDistK0.Size = New System.Drawing.Size(182, 22)
        Me.NumericUpDown_PsvrDistK0.TabIndex = 3
        '
        'UcNumericUpDownBig9
        '
        Me.UcNumericUpDownBig9.AutoSize = True
        Me.UcNumericUpDownBig9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig9.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig9.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig9.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig9.Location = New System.Drawing.Point(138, 0)
        Me.UcNumericUpDownBig9.m_bDockOnControl = True
        Me.UcNumericUpDownBig9.m_NumericUpDown = Me.NumericUpDown_PsvrDistK0
        Me.UcNumericUpDownBig9.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig9.m_ResetVisible = False
        Me.UcNumericUpDownBig9.Name = "UcNumericUpDownBig9"
        Me.UcNumericUpDownBig9.Size = New System.Drawing.Size(44, 22)
        Me.UcNumericUpDownBig9.TabIndex = 27
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(19, 26)
        Me.Label46.Margin = New System.Windows.Forms.Padding(16, 8, 3, 3)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(77, 13)
        Me.Label46.TabIndex = 2
        Me.Label46.Text = "Distortion K0:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label63)
        Me.GroupBox4.Controls.Add(Me.Label54)
        Me.GroupBox4.Controls.Add(Me.CheckBox_ShowDistSettings)
        Me.GroupBox4.Controls.Add(Me.NumericUpDown_PsvrIPD)
        Me.GroupBox4.Controls.Add(Me.Label45)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.ComboBox_PsvrRenderResolution)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(734, 106)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Render Settings"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(366, 52)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(25, 13)
        Me.Label63.TabIndex = 6
        Me.Label63.Text = "mm"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.Color.Gray
        Me.Label54.Location = New System.Drawing.Point(366, 26)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(129, 13)
        Me.Label54.TabIndex = 5
        Me.Label54.Text = "(requires SteamVR restart)"
        '
        'CheckBox_ShowDistSettings
        '
        Me.CheckBox_ShowDistSettings.AutoSize = True
        Me.CheckBox_ShowDistSettings.Location = New System.Drawing.Point(19, 78)
        Me.CheckBox_ShowDistSettings.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_ShowDistSettings.Name = "CheckBox_ShowDistSettings"
        Me.CheckBox_ShowDistSettings.Size = New System.Drawing.Size(314, 17)
        Me.CheckBox_ShowDistSettings.TabIndex = 4
        Me.CheckBox_ShowDistSettings.Text = "Use custom distortion settings (only for advanced users)"
        Me.CheckBox_ShowDistSettings.UseVisualStyleBackColor = True
        '
        'NumericUpDown_PsvrIPD
        '
        Me.NumericUpDown_PsvrIPD.Controls.Add(Me.UcNumericUpDownBig8)
        Me.NumericUpDown_PsvrIPD.Location = New System.Drawing.Point(174, 50)
        Me.NumericUpDown_PsvrIPD.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.NumericUpDown_PsvrIPD.Name = "NumericUpDown_PsvrIPD"
        Me.NumericUpDown_PsvrIPD.Size = New System.Drawing.Size(186, 22)
        Me.NumericUpDown_PsvrIPD.TabIndex = 3
        Me.NumericUpDown_PsvrIPD.Value = New Decimal(New Integer() {67, 0, 0, 0})
        '
        'UcNumericUpDownBig8
        '
        Me.UcNumericUpDownBig8.AutoSize = True
        Me.UcNumericUpDownBig8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig8.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig8.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig8.Location = New System.Drawing.Point(120, 0)
        Me.UcNumericUpDownBig8.m_bDockOnControl = True
        Me.UcNumericUpDownBig8.m_NumericUpDown = Me.NumericUpDown_PsvrIPD
        Me.UcNumericUpDownBig8.m_ResetValue = New Decimal(New Integer() {67, 0, 0, 0})
        Me.UcNumericUpDownBig8.m_ResetVisible = True
        Me.UcNumericUpDownBig8.Name = "UcNumericUpDownBig8"
        Me.UcNumericUpDownBig8.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig8.TabIndex = 7
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(16, 52)
        Me.Label45.Margin = New System.Windows.Forms.Padding(16, 8, 3, 3)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(152, 13)
        Me.Label45.TabIndex = 2
        Me.Label45.Text = "Interpupillary Distance (IPD):"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 26)
        Me.Label1.Margin = New System.Windows.Forms.Padding(16, 8, 3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Default render resolution:"
        '
        'ComboBox_PsvrRenderResolution
        '
        Me.ComboBox_PsvrRenderResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_PsvrRenderResolution.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_PsvrRenderResolution.FormattingEnabled = True
        Me.ComboBox_PsvrRenderResolution.Location = New System.Drawing.Point(174, 23)
        Me.ComboBox_PsvrRenderResolution.Name = "ComboBox_PsvrRenderResolution"
        Me.ComboBox_PsvrRenderResolution.Size = New System.Drawing.Size(186, 21)
        Me.ComboBox_PsvrRenderResolution.TabIndex = 1
        '
        'TabPage_SettingsPSmove
        '
        Me.TabPage_SettingsPSmove.AutoScroll = True
        Me.TabPage_SettingsPSmove.BackColor = System.Drawing.Color.White
        Me.TabPage_SettingsPSmove.Controls.Add(Me.GroupBox1)
        Me.TabPage_SettingsPSmove.Controls.Add(Me.GroupBox2)
        Me.TabPage_SettingsPSmove.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_SettingsPSmove.Name = "TabPage_SettingsPSmove"
        Me.TabPage_SettingsPSmove.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_SettingsPSmove.Size = New System.Drawing.Size(740, 1132)
        Me.TabPage_SettingsPSmove.TabIndex = 0
        Me.TabPage_SettingsPSmove.Text = "PSMove Controller"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LinkLabel_PlayCalibShowSettings2)
        Me.GroupBox1.Controls.Add(Me.Label_ScrollFocus)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.CheckBox_PlayCalibEnabled)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown_RecenterButtonTime)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Button_ResetRecenter)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.ComboBox_HmdRecenterFromDevice)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.ComboBox_HmdRecenterMethod)
        Me.GroupBox1.Controls.Add(Me.Label19)
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
        Me.GroupBox1.Size = New System.Drawing.Size(734, 518)
        Me.GroupBox1.TabIndex = 47
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Recenter Settings"
        '
        'LinkLabel_PlayCalibShowSettings2
        '
        Me.LinkLabel_PlayCalibShowSettings2.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_PlayCalibShowSettings2.AutoSize = True
        Me.LinkLabel_PlayCalibShowSettings2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_PlayCalibShowSettings2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_PlayCalibShowSettings2.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_PlayCalibShowSettings2.Location = New System.Drawing.Point(51, 444)
        Me.LinkLabel_PlayCalibShowSettings2.Margin = New System.Windows.Forms.Padding(48, 3, 3, 0)
        Me.LinkLabel_PlayCalibShowSettings2.Name = "LinkLabel_PlayCalibShowSettings2"
        Me.LinkLabel_PlayCalibShowSettings2.Size = New System.Drawing.Size(191, 13)
        Me.LinkLabel_PlayCalibShowSettings2.TabIndex = 71
        Me.LinkLabel_PlayCalibShowSettings2.TabStop = True
        Me.LinkLabel_PlayCalibShowSettings2.Text = "Show playspace calibration settings"
        Me.LinkLabel_PlayCalibShowSettings2.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label_ScrollFocus
        '
        Me.Label_ScrollFocus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_ScrollFocus.AutoSize = True
        Me.Label_ScrollFocus.Location = New System.Drawing.Point(636, 502)
        Me.Label_ScrollFocus.Name = "Label_ScrollFocus"
        Me.Label_ScrollFocus.Size = New System.Drawing.Size(92, 13)
        Me.Label_ScrollFocus.TabIndex = 70
        Me.Label_ScrollFocus.Text = "<<FOR FOCUS>"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label25.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label25.Location = New System.Drawing.Point(51, 393)
        Me.Label25.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.Label25.Name = "Label25"
        Me.Label25.Padding = New System.Windows.Forms.Padding(3)
        Me.Label25.Size = New System.Drawing.Size(574, 45)
        Me.Label25.TabIndex = 66
        Me.Label25.Text = resources.GetString("Label25.Text")
        '
        'CheckBox_PlayCalibEnabled
        '
        Me.CheckBox_PlayCalibEnabled.AutoSize = True
        Me.CheckBox_PlayCalibEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_PlayCalibEnabled.Location = New System.Drawing.Point(19, 370)
        Me.CheckBox_PlayCalibEnabled.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.CheckBox_PlayCalibEnabled.Name = "CheckBox_PlayCalibEnabled"
        Me.CheckBox_PlayCalibEnabled.Size = New System.Drawing.Size(228, 18)
        Me.CheckBox_PlayCalibEnabled.TabIndex = 65
        Me.CheckBox_PlayCalibEnabled.Text = "Enable playspace recentering shortcut"
        Me.CheckBox_PlayCalibEnabled.UseVisualStyleBackColor = True
        '
        'NumericUpDown_RecenterButtonTime
        '
        Me.NumericUpDown_RecenterButtonTime.Controls.Add(Me.UcNumericUpDownBig7)
        Me.NumericUpDown_RecenterButtonTime.Location = New System.Drawing.Point(197, 471)
        Me.NumericUpDown_RecenterButtonTime.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.NumericUpDown_RecenterButtonTime.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown_RecenterButtonTime.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.NumericUpDown_RecenterButtonTime.Name = "NumericUpDown_RecenterButtonTime"
        Me.NumericUpDown_RecenterButtonTime.Size = New System.Drawing.Size(114, 22)
        Me.NumericUpDown_RecenterButtonTime.TabIndex = 61
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_RecenterButtonTime, "Default: 500")
        Me.NumericUpDown_RecenterButtonTime.Value = New Decimal(New Integer() {500, 0, 0, 0})
        '
        'UcNumericUpDownBig7
        '
        Me.UcNumericUpDownBig7.AutoSize = True
        Me.UcNumericUpDownBig7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig7.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig7.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig7.Location = New System.Drawing.Point(48, 0)
        Me.UcNumericUpDownBig7.m_bDockOnControl = True
        Me.UcNumericUpDownBig7.m_NumericUpDown = Me.NumericUpDown_RecenterButtonTime
        Me.UcNumericUpDownBig7.m_ResetValue = New Decimal(New Integer() {500, 0, 0, 0})
        Me.UcNumericUpDownBig7.m_ResetVisible = True
        Me.UcNumericUpDownBig7.Name = "UcNumericUpDownBig7"
        Me.UcNumericUpDownBig7.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig7.TabIndex = 72
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(19, 473)
        Me.Label13.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(172, 13)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "Recenter button press time (ms):"
        '
        'Button_ResetRecenter
        '
        Me.Button_ResetRecenter.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.Button_ResetRecenter.Location = New System.Drawing.Point(19, 328)
        Me.Button_ResetRecenter.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Button_ResetRecenter.Name = "Button_ResetRecenter"
        Me.Button_ResetRecenter.Size = New System.Drawing.Size(214, 23)
        Me.Button_ResetRecenter.TabIndex = 59
        Me.Button_ResetRecenter.Text = "Reset recenter on all trackers"
        Me.Button_ResetRecenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_ResetRecenter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip_Info.SetToolTip(Me.Button_ResetRecenter, "This will reset recenter on all active trackers. Inactive trackers will not be re" &
        "set.")
        Me.Button_ResetRecenter.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label20.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_101_16x16_32
        Me.Label20.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label20.Location = New System.Drawing.Point(51, 290)
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
        Me.ComboBox_HmdRecenterFromDevice.Location = New System.Drawing.Point(191, 263)
        Me.ComboBox_HmdRecenterFromDevice.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_HmdRecenterFromDevice.Name = "ComboBox_HmdRecenterFromDevice"
        Me.ComboBox_HmdRecenterFromDevice.Size = New System.Drawing.Size(492, 21)
        Me.ComboBox_HmdRecenterFromDevice.TabIndex = 57
        Me.ToolTip_Info.SetToolTip(Me.ComboBox_HmdRecenterFromDevice, "The list might not be populated with trackers if the OSC Server is not running.")
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(51, 266)
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
        Me.ComboBox_HmdRecenterMethod.Location = New System.Drawing.Point(191, 236)
        Me.ComboBox_HmdRecenterMethod.Margin = New System.Windows.Forms.Padding(3, 16, 48, 3)
        Me.ComboBox_HmdRecenterMethod.Name = "ComboBox_HmdRecenterMethod"
        Me.ComboBox_HmdRecenterMethod.Size = New System.Drawing.Size(492, 21)
        Me.ComboBox_HmdRecenterMethod.TabIndex = 55
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(51, 239)
        Me.Label19.Margin = New System.Windows.Forms.Padding(48, 3, 3, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(98, 13)
        Me.Label19.TabIndex = 54
        Me.Label19.Text = "Recenter method:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label16.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label16.Location = New System.Drawing.Point(51, 185)
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
        Me.CheckBox_HmdRecenterEnabled.Location = New System.Drawing.Point(19, 162)
        Me.CheckBox_HmdRecenterEnabled.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.CheckBox_HmdRecenterEnabled.Name = "CheckBox_HmdRecenterEnabled"
        Me.CheckBox_HmdRecenterEnabled.Size = New System.Drawing.Size(275, 18)
        Me.CheckBox_HmdRecenterEnabled.TabIndex = 51
        Me.CheckBox_HmdRecenterEnabled.Text = "Enable remote orientation recentering shortcut"
        Me.CheckBox_HmdRecenterEnabled.UseVisualStyleBackColor = True
        '
        'ComboBox_RecenterFromDevice
        '
        Me.ComboBox_RecenterFromDevice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_RecenterFromDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_RecenterFromDevice.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_RecenterFromDevice.FormattingEnabled = True
        Me.ComboBox_RecenterFromDevice.Location = New System.Drawing.Point(191, 122)
        Me.ComboBox_RecenterFromDevice.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_RecenterFromDevice.Name = "ComboBox_RecenterFromDevice"
        Me.ComboBox_RecenterFromDevice.Size = New System.Drawing.Size(492, 21)
        Me.ComboBox_RecenterFromDevice.TabIndex = 50
        Me.ToolTip_Info.SetToolTip(Me.ComboBox_RecenterFromDevice, "The list might not be populated with trackers if the OSC Server is not running.")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(51, 125)
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
        Me.ComboBox_RecenterMethod.Location = New System.Drawing.Point(191, 95)
        Me.ComboBox_RecenterMethod.Margin = New System.Windows.Forms.Padding(3, 16, 48, 3)
        Me.ComboBox_RecenterMethod.Name = "ComboBox_RecenterMethod"
        Me.ComboBox_RecenterMethod.Size = New System.Drawing.Size(492, 21)
        Me.ComboBox_RecenterMethod.TabIndex = 48
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(51, 98)
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
        Me.Label11.Location = New System.Drawing.Point(51, 44)
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
        Me.CheckBox_ControllerRecenterEnabled.Location = New System.Drawing.Point(22, 21)
        Me.CheckBox_ControllerRecenterEnabled.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_ControllerRecenterEnabled.Name = "CheckBox_ControllerRecenterEnabled"
        Me.CheckBox_ControllerRecenterEnabled.Size = New System.Drawing.Size(236, 18)
        Me.CheckBox_ControllerRecenterEnabled.TabIndex = 0
        Me.CheckBox_ControllerRecenterEnabled.Text = "Enable orientation recentering shortcut"
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
        Me.NumericUpDown_TouchpadTouchArea.Controls.Add(Me.UcNumericUpDownBig5)
        Me.NumericUpDown_TouchpadTouchArea.DecimalPlaces = 2
        Me.NumericUpDown_TouchpadTouchArea.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.NumericUpDown_TouchpadTouchArea.Location = New System.Drawing.Point(191, 96)
        Me.NumericUpDown_TouchpadTouchArea.Name = "NumericUpDown_TouchpadTouchArea"
        Me.NumericUpDown_TouchpadTouchArea.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_TouchpadTouchArea.TabIndex = 52
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_TouchpadTouchArea, "Default: 7,50")
        Me.NumericUpDown_TouchpadTouchArea.Value = New Decimal(New Integer() {750, 0, 0, 131072})
        '
        'UcNumericUpDownBig5
        '
        Me.UcNumericUpDownBig5.AutoSize = True
        Me.UcNumericUpDownBig5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig5.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig5.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig5.Location = New System.Drawing.Point(54, 0)
        Me.UcNumericUpDownBig5.m_bDockOnControl = True
        Me.UcNumericUpDownBig5.m_NumericUpDown = Me.NumericUpDown_TouchpadTouchArea
        Me.UcNumericUpDownBig5.m_ResetValue = New Decimal(New Integer() {750, 0, 0, 131072})
        Me.UcNumericUpDownBig5.m_ResetVisible = True
        Me.UcNumericUpDownBig5.Name = "UcNumericUpDownBig5"
        Me.UcNumericUpDownBig5.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig5.TabIndex = 54
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
        Me.NumericUpDown_TouchpadClickDeadzone.Controls.Add(Me.UcNumericUpDownBig6)
        Me.NumericUpDown_TouchpadClickDeadzone.DecimalPlaces = 2
        Me.NumericUpDown_TouchpadClickDeadzone.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.NumericUpDown_TouchpadClickDeadzone.Location = New System.Drawing.Point(191, 151)
        Me.NumericUpDown_TouchpadClickDeadzone.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_TouchpadClickDeadzone.Name = "NumericUpDown_TouchpadClickDeadzone"
        Me.NumericUpDown_TouchpadClickDeadzone.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_TouchpadClickDeadzone.TabIndex = 50
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_TouchpadClickDeadzone, "Default: 0,25")
        Me.NumericUpDown_TouchpadClickDeadzone.Value = New Decimal(New Integer() {25, 0, 0, 131072})
        '
        'UcNumericUpDownBig6
        '
        Me.UcNumericUpDownBig6.AutoSize = True
        Me.UcNumericUpDownBig6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig6.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig6.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig6.Location = New System.Drawing.Point(54, 0)
        Me.UcNumericUpDownBig6.m_bDockOnControl = True
        Me.UcNumericUpDownBig6.m_NumericUpDown = Me.NumericUpDown_TouchpadClickDeadzone
        Me.UcNumericUpDownBig6.m_ResetValue = New Decimal(New Integer() {25, 0, 0, 131072})
        Me.UcNumericUpDownBig6.m_ResetVisible = True
        Me.UcNumericUpDownBig6.Name = "UcNumericUpDownBig6"
        Me.UcNumericUpDownBig6.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig6.TabIndex = 54
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
        'TabPage_SettingsPlayspace
        '
        Me.TabPage_SettingsPlayspace.BackColor = System.Drawing.Color.White
        Me.TabPage_SettingsPlayspace.Controls.Add(Me.GroupBox3)
        Me.TabPage_SettingsPlayspace.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_SettingsPlayspace.Name = "TabPage_SettingsPlayspace"
        Me.TabPage_SettingsPlayspace.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_SettingsPlayspace.Size = New System.Drawing.Size(740, 1132)
        Me.TabPage_SettingsPlayspace.TabIndex = 2
        Me.TabPage_SettingsPlayspace.Text = "Playspace"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.NumericUpDown_PlayCalibSideOffset)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Controls.Add(Me.ComboBox_PlayCalibForwardMethod)
        Me.GroupBox3.Controls.Add(Me.Button_PlayCalibReset)
        Me.GroupBox3.Controls.Add(Me.Label40)
        Me.GroupBox3.Controls.Add(Me.NumericUpDown_PlayCalibHeightOffset)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.NumericUpDown_PlayCalibForwardOffset)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(734, 252)
        Me.GroupBox3.TabIndex = 82
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Playspace Calibration Settings"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(19, 62)
        Me.Label17.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(88, 13)
        Me.Label17.TabIndex = 82
        Me.Label17.Text = "Side offset (cm):"
        '
        'NumericUpDown_PlayCalibSideOffset
        '
        Me.NumericUpDown_PlayCalibSideOffset.Controls.Add(Me.UcNumericUpDownBig3)
        Me.NumericUpDown_PlayCalibSideOffset.Location = New System.Drawing.Point(134, 60)
        Me.NumericUpDown_PlayCalibSideOffset.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown_PlayCalibSideOffset.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.NumericUpDown_PlayCalibSideOffset.Name = "NumericUpDown_PlayCalibSideOffset"
        Me.NumericUpDown_PlayCalibSideOffset.Size = New System.Drawing.Size(132, 22)
        Me.NumericUpDown_PlayCalibSideOffset.TabIndex = 83
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_PlayCalibSideOffset, "Default: 0")
        '
        'UcNumericUpDownBig3
        '
        Me.UcNumericUpDownBig3.AutoSize = True
        Me.UcNumericUpDownBig3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig3.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig3.Location = New System.Drawing.Point(66, 0)
        Me.UcNumericUpDownBig3.m_bDockOnControl = True
        Me.UcNumericUpDownBig3.m_NumericUpDown = Me.NumericUpDown_PlayCalibSideOffset
        Me.UcNumericUpDownBig3.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig3.m_ResetVisible = True
        Me.UcNumericUpDownBig3.Name = "UcNumericUpDownBig3"
        Me.UcNumericUpDownBig3.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig3.TabIndex = 85
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(19, 34)
        Me.Label26.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(109, 13)
        Me.Label26.TabIndex = 76
        Me.Label26.Text = "Forward offset (cm):"
        '
        'ComboBox_PlayCalibForwardMethod
        '
        Me.ComboBox_PlayCalibForwardMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_PlayCalibForwardMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_PlayCalibForwardMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_PlayCalibForwardMethod.FormattingEnabled = True
        Me.ComboBox_PlayCalibForwardMethod.Location = New System.Drawing.Point(134, 116)
        Me.ComboBox_PlayCalibForwardMethod.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_PlayCalibForwardMethod.Name = "ComboBox_PlayCalibForwardMethod"
        Me.ComboBox_PlayCalibForwardMethod.Size = New System.Drawing.Size(549, 21)
        Me.ComboBox_PlayCalibForwardMethod.TabIndex = 81
        '
        'Button_PlayCalibReset
        '
        Me.Button_PlayCalibReset.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.Button_PlayCalibReset.Location = New System.Drawing.Point(19, 156)
        Me.Button_PlayCalibReset.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Button_PlayCalibReset.Name = "Button_PlayCalibReset"
        Me.Button_PlayCalibReset.Size = New System.Drawing.Size(214, 23)
        Me.Button_PlayCalibReset.TabIndex = 75
        Me.Button_PlayCalibReset.Text = "Reset playspace calibration"
        Me.Button_PlayCalibReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_PlayCalibReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_PlayCalibReset.UseVisualStyleBackColor = True
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(19, 119)
        Me.Label40.Margin = New System.Windows.Forms.Padding(16, 3, 3, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(96, 13)
        Me.Label40.TabIndex = 80
        Me.Label40.Text = "Forward method:"
        '
        'NumericUpDown_PlayCalibHeightOffset
        '
        Me.NumericUpDown_PlayCalibHeightOffset.Controls.Add(Me.UcNumericUpDownBig2)
        Me.NumericUpDown_PlayCalibHeightOffset.Location = New System.Drawing.Point(134, 88)
        Me.NumericUpDown_PlayCalibHeightOffset.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown_PlayCalibHeightOffset.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.NumericUpDown_PlayCalibHeightOffset.Name = "NumericUpDown_PlayCalibHeightOffset"
        Me.NumericUpDown_PlayCalibHeightOffset.Size = New System.Drawing.Size(132, 22)
        Me.NumericUpDown_PlayCalibHeightOffset.TabIndex = 79
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_PlayCalibHeightOffset, "Default: 0")
        '
        'UcNumericUpDownBig2
        '
        Me.UcNumericUpDownBig2.AutoSize = True
        Me.UcNumericUpDownBig2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig2.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig2.Location = New System.Drawing.Point(66, 0)
        Me.UcNumericUpDownBig2.m_bDockOnControl = True
        Me.UcNumericUpDownBig2.m_NumericUpDown = Me.NumericUpDown_PlayCalibHeightOffset
        Me.UcNumericUpDownBig2.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig2.m_ResetVisible = True
        Me.UcNumericUpDownBig2.Name = "UcNumericUpDownBig2"
        Me.UcNumericUpDownBig2.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig2.TabIndex = 84
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(19, 90)
        Me.Label29.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(101, 13)
        Me.Label29.TabIndex = 78
        Me.Label29.Text = "Height offset (cm):"
        '
        'NumericUpDown_PlayCalibForwardOffset
        '
        Me.NumericUpDown_PlayCalibForwardOffset.Controls.Add(Me.UcNumericUpDownBig1)
        Me.NumericUpDown_PlayCalibForwardOffset.Location = New System.Drawing.Point(134, 32)
        Me.NumericUpDown_PlayCalibForwardOffset.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.NumericUpDown_PlayCalibForwardOffset.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown_PlayCalibForwardOffset.Minimum = New Decimal(New Integer() {10000, 0, 0, -2147483648})
        Me.NumericUpDown_PlayCalibForwardOffset.Name = "NumericUpDown_PlayCalibForwardOffset"
        Me.NumericUpDown_PlayCalibForwardOffset.Size = New System.Drawing.Size(132, 22)
        Me.NumericUpDown_PlayCalibForwardOffset.TabIndex = 77
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_PlayCalibForwardOffset, "Default: 10")
        Me.NumericUpDown_PlayCalibForwardOffset.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'UcNumericUpDownBig1
        '
        Me.UcNumericUpDownBig1.AutoSize = True
        Me.UcNumericUpDownBig1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig1.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig1.Location = New System.Drawing.Point(66, 0)
        Me.UcNumericUpDownBig1.m_bDockOnControl = True
        Me.UcNumericUpDownBig1.m_NumericUpDown = Me.NumericUpDown_PlayCalibForwardOffset
        Me.UcNumericUpDownBig1.m_ResetValue = New Decimal(New Integer() {10, 0, 0, 0})
        Me.UcNumericUpDownBig1.m_ResetVisible = True
        Me.UcNumericUpDownBig1.Name = "UcNumericUpDownBig1"
        Me.UcNumericUpDownBig1.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig1.TabIndex = 84
        '
        'TabPage_SettingsOther
        '
        Me.TabPage_SettingsOther.BackColor = System.Drawing.Color.White
        Me.TabPage_SettingsOther.Controls.Add(Me.Label64)
        Me.TabPage_SettingsOther.Controls.Add(Me.CheckBox_OptimizePackets)
        Me.TabPage_SettingsOther.Controls.Add(Me.NumericUpDown_OscThreadSleep)
        Me.TabPage_SettingsOther.Controls.Add(Me.Label21)
        Me.TabPage_SettingsOther.Controls.Add(Me.Label7)
        Me.TabPage_SettingsOther.Controls.Add(Me.CheckBox_EnableHeptics)
        Me.TabPage_SettingsOther.Controls.Add(Me.CheckBox_DisableBasestations)
        Me.TabPage_SettingsOther.Controls.Add(Me.Label6)
        Me.TabPage_SettingsOther.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_SettingsOther.Name = "TabPage_SettingsOther"
        Me.TabPage_SettingsOther.Size = New System.Drawing.Size(740, 1132)
        Me.TabPage_SettingsOther.TabIndex = 1
        Me.TabPage_SettingsOther.Text = "Other"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label64.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label64.Location = New System.Drawing.Point(16, 227)
        Me.Label64.Margin = New System.Windows.Forms.Padding(16, 0, 3, 0)
        Me.Label64.Name = "Label64"
        Me.Label64.Padding = New System.Windows.Forms.Padding(3)
        Me.Label64.Size = New System.Drawing.Size(420, 45)
        Me.Label64.TabIndex = 51
        Me.Label64.Text = resources.GetString("Label64.Text")
        '
        'CheckBox_OptimizePackets
        '
        Me.CheckBox_OptimizePackets.AutoSize = True
        Me.CheckBox_OptimizePackets.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_OptimizePackets.Location = New System.Drawing.Point(16, 206)
        Me.CheckBox_OptimizePackets.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_OptimizePackets.Name = "CheckBox_OptimizePackets"
        Me.CheckBox_OptimizePackets.Size = New System.Drawing.Size(208, 18)
        Me.CheckBox_OptimizePackets.TabIndex = 50
        Me.CheckBox_OptimizePackets.Text = "Optimize OSC packet transmission"
        Me.CheckBox_OptimizePackets.UseVisualStyleBackColor = True
        '
        'NumericUpDown_OscThreadSleep
        '
        Me.NumericUpDown_OscThreadSleep.Controls.Add(Me.UcNumericUpDownBig4)
        Me.NumericUpDown_OscThreadSleep.Location = New System.Drawing.Point(177, 178)
        Me.NumericUpDown_OscThreadSleep.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_OscThreadSleep.Name = "NumericUpDown_OscThreadSleep"
        Me.NumericUpDown_OscThreadSleep.Size = New System.Drawing.Size(118, 22)
        Me.NumericUpDown_OscThreadSleep.TabIndex = 49
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_OscThreadSleep, "Default: 1")
        Me.NumericUpDown_OscThreadSleep.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'UcNumericUpDownBig4
        '
        Me.UcNumericUpDownBig4.AutoSize = True
        Me.UcNumericUpDownBig4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig4.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig4.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig4.Location = New System.Drawing.Point(52, 0)
        Me.UcNumericUpDownBig4.m_bDockOnControl = True
        Me.UcNumericUpDownBig4.m_NumericUpDown = Me.NumericUpDown_OscThreadSleep
        Me.UcNumericUpDownBig4.m_ResetValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.UcNumericUpDownBig4.m_ResetVisible = True
        Me.UcNumericUpDownBig4.Name = "UcNumericUpDownBig4"
        Me.UcNumericUpDownBig4.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig4.TabIndex = 52
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
        Me.Label7.Size = New System.Drawing.Size(540, 45)
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
        Me.Label6.Size = New System.Drawing.Size(623, 45)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = resources.GetString("Label6.Text")
        '
        'Button_SaveControllerSettings
        '
        Me.Button_SaveControllerSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SaveControllerSettings.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16761_16x16_32
        Me.Button_SaveControllerSettings.Location = New System.Drawing.Point(624, 1183)
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
        Me.TabPage_PlayspaceCalib.BackColor = System.Drawing.Color.White
        Me.TabPage_PlayspaceCalib.Controls.Add(Me.NumericUpDown_PlayCalibPrepTime)
        Me.TabPage_PlayspaceCalib.Controls.Add(Me.Label41)
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
        Me.TabPage_PlayspaceCalib.Size = New System.Drawing.Size(760, 1225)
        Me.TabPage_PlayspaceCalib.TabIndex = 5
        Me.TabPage_PlayspaceCalib.Text = "Playspace Calibration"
        '
        'NumericUpDown_PlayCalibPrepTime
        '
        Me.NumericUpDown_PlayCalibPrepTime.Controls.Add(Me.UcNumericUpDownBig17)
        Me.NumericUpDown_PlayCalibPrepTime.Location = New System.Drawing.Point(168, 134)
        Me.NumericUpDown_PlayCalibPrepTime.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.NumericUpDown_PlayCalibPrepTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_PlayCalibPrepTime.Name = "NumericUpDown_PlayCalibPrepTime"
        Me.NumericUpDown_PlayCalibPrepTime.Size = New System.Drawing.Size(132, 22)
        Me.NumericUpDown_PlayCalibPrepTime.TabIndex = 71
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_PlayCalibPrepTime, "Default: 5")
        Me.NumericUpDown_PlayCalibPrepTime.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'UcNumericUpDownBig17
        '
        Me.UcNumericUpDownBig17.AutoSize = True
        Me.UcNumericUpDownBig17.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig17.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig17.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig17.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig17.Location = New System.Drawing.Point(66, 0)
        Me.UcNumericUpDownBig17.m_bDockOnControl = True
        Me.UcNumericUpDownBig17.m_NumericUpDown = Me.NumericUpDown_PlayCalibPrepTime
        Me.UcNumericUpDownBig17.m_ResetValue = New Decimal(New Integer() {5, 0, 0, 0})
        Me.UcNumericUpDownBig17.m_ResetVisible = True
        Me.UcNumericUpDownBig17.Name = "UcNumericUpDownBig17"
        Me.UcNumericUpDownBig17.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig17.TabIndex = 72
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(16, 136)
        Me.Label41.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(146, 13)
        Me.Label41.TabIndex = 70
        Me.Label41.Text = "Preparation time (seconds):"
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
        Me.Panel_PlayCalibSteps.Location = New System.Drawing.Point(16, 213)
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
        Me.Label31.Size = New System.Drawing.Size(553, 26)
        Me.Label31.TabIndex = 2
        Me.Label31.Text = "Hold the controller bulb in front of your head-mounted display. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Keep the contro" &
    "ller bulb consistently in front of your head-mounted display while performing th" &
    "ese steps!"
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
        Me.ComboBox_PlayCalibControllerID.Location = New System.Drawing.Point(168, 104)
        Me.ComboBox_PlayCalibControllerID.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_PlayCalibControllerID.Name = "ComboBox_PlayCalibControllerID"
        Me.ComboBox_PlayCalibControllerID.Size = New System.Drawing.Size(132, 21)
        Me.ComboBox_PlayCalibControllerID.TabIndex = 68
        Me.ToolTip_Info.SetToolTip(Me.ComboBox_PlayCalibControllerID, "The controller id you want to use for playspace calibration.")
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
        Me.LinkLabel_PlayCalibShowSettings.Size = New System.Drawing.Size(80, 13)
        Me.LinkLabel_PlayCalibShowSettings.TabIndex = 23
        Me.LinkLabel_PlayCalibShowSettings.TabStop = True
        Me.LinkLabel_PlayCalibShowSettings.Text = "Show settings"
        Me.LinkLabel_PlayCalibShowSettings.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label27.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label27.Location = New System.Drawing.Point(13, 162)
        Me.Label27.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.Label27.Name = "Label27"
        Me.Label27.Padding = New System.Windows.Forms.Padding(3)
        Me.Label27.Size = New System.Drawing.Size(646, 32)
        Me.Label27.TabIndex = 66
        Me.Label27.Text = "        PSMove controllers do not need manual calibration; use the buttons SELECT" &
    "+START to recenter the playspace instead. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "        See settings for more detail" &
    "s."
        '
        'Button_PlaySpaceManualCalib
        '
        Me.Button_PlaySpaceManualCalib.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.Button_PlaySpaceManualCalib.Location = New System.Drawing.Point(16, 65)
        Me.Button_PlaySpaceManualCalib.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Button_PlaySpaceManualCalib.Name = "Button_PlaySpaceManualCalib"
        Me.Button_PlaySpaceManualCalib.Size = New System.Drawing.Size(214, 23)
        Me.Button_PlaySpaceManualCalib.TabIndex = 65
        Me.Button_PlaySpaceManualCalib.Text = "Start Playspace Calibration"
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
        Me.Label24.Text = resources.GetString("Label24.Text")
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
        'TabPage_Overrides
        '
        Me.TabPage_Overrides.BackColor = System.Drawing.Color.White
        Me.TabPage_Overrides.Controls.Add(Me.Panel_SteamVRRestart)
        Me.TabPage_Overrides.Controls.Add(Me.Label2)
        Me.TabPage_Overrides.Controls.Add(Me.Button_Refresh)
        Me.TabPage_Overrides.Controls.Add(Me.Button_Remove)
        Me.TabPage_Overrides.Controls.Add(Me.Button_Add)
        Me.TabPage_Overrides.Controls.Add(Me.PictureBox2)
        Me.TabPage_Overrides.Controls.Add(Me.ListView_Overrides)
        Me.TabPage_Overrides.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Overrides.Name = "TabPage_Overrides"
        Me.TabPage_Overrides.Size = New System.Drawing.Size(760, 1225)
        Me.TabPage_Overrides.TabIndex = 2
        Me.TabPage_Overrides.Text = "SteamVR Tracker Overrides"
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
        Me.Label2.Size = New System.Drawing.Size(706, 43)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = resources.GetString("Label2.Text")
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
        Me.ListView_Overrides.Sorting = System.Windows.Forms.SortOrder.Ascending
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
        'ToolTip_Info
        '
        Me.ToolTip_Info.AutomaticDelay = 100
        Me.ToolTip_Info.AutoPopDelay = 30000
        Me.ToolTip_Info.InitialDelay = 100
        Me.ToolTip_Info.ReshowDelay = 20
        Me.ToolTip_Info.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip_Info.ToolTipTitle = "Information"
        '
        'Timer_VMTTrackers
        '
        Me.Timer_VMTTrackers.Enabled = True
        Me.Timer_VMTTrackers.Interval = 500
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LinkLabel_ReadMore)
        Me.Panel1.Controls.Add(Me.ClassPictureBoxQuality4)
        Me.Panel1.Controls.Add(Me.Label42)
        Me.Panel1.Controls.Add(Me.Label43)
        Me.Panel1.Controls.Add(Me.Label44)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 85)
        Me.Panel1.TabIndex = 23
        '
        'ClassPictureBoxQuality4
        '
        Me.ClassPictureBoxQuality4.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources._374
        Me.ClassPictureBoxQuality4.Location = New System.Drawing.Point(3, 3)
        Me.ClassPictureBoxQuality4.m_HighQuality = True
        Me.ClassPictureBoxQuality4.Name = "ClassPictureBoxQuality4"
        Me.ClassPictureBoxQuality4.Size = New System.Drawing.Size(57, 57)
        Me.ClassPictureBoxQuality4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality4.TabIndex = 16
        Me.ClassPictureBoxQuality4.TabStop = False
        '
        'Label42
        '
        Me.Label42.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label42.Location = New System.Drawing.Point(66, 30)
        Me.Label42.Margin = New System.Windows.Forms.Padding(3)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(731, 51)
        Me.Label42.TabIndex = 2
        Me.Label42.Text = resources.GetString("Label42.Text")
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(66, 3)
        Me.Label43.Margin = New System.Windows.Forms.Padding(3)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(188, 21)
        Me.Label43.TabIndex = 1
        Me.Label43.Text = "Virtual Motion Trackers"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label44.Location = New System.Drawing.Point(0, 84)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(800, 1)
        Me.Label44.TabIndex = 0
        '
        'ContextMenuStrip_AddTracker
        '
        Me.ContextMenuStrip_AddTracker.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_AddTracker, Me.ToolStripMenuItem_AddHmd})
        Me.ContextMenuStrip_AddTracker.Name = "ContextMenuStrip_AddTracker"
        Me.ContextMenuStrip_AddTracker.Size = New System.Drawing.Size(214, 48)
        '
        'ToolStripMenuItem_AddTracker
        '
        Me.ToolStripMenuItem_AddTracker.Name = "ToolStripMenuItem_AddTracker"
        Me.ToolStripMenuItem_AddTracker.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem_AddTracker.Text = "By Controller"
        '
        'ToolStripMenuItem_AddHmd
        '
        Me.ToolStripMenuItem_AddHmd.Name = "ToolStripMenuItem_AddHmd"
        Me.ToolStripMenuItem_AddHmd.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem_AddHmd.Text = "By Head-Mounted Display"
        '
        'ToolTip_Default
        '
        Me.ToolTip_Default.AutomaticDelay = 100
        Me.ToolTip_Default.AutoPopDelay = 30000
        Me.ToolTip_Default.InitialDelay = 100
        Me.ToolTip_Default.ReshowDelay = 20
        '
        'UCVirtualMotionTracker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl_Vmt)
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
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel12.ResumeLayout(False)
        Me.TabPage_Trackers.ResumeLayout(False)
        Me.ContextMenuStrip_Trackers.ResumeLayout(False)
        Me.TabPage_Settings.ResumeLayout(False)
        Me.TabControl_SettingsDevices.ResumeLayout(False)
        Me.TabPage_SettingsPSVR.ResumeLayout(False)
        Me.GroupBox_Distortion.ResumeLayout(False)
        Me.GroupBox_Distortion.PerformLayout()
        CType(Me.NumericUpDown_PsvrVFov, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PsvrVFov.ResumeLayout(False)
        Me.NumericUpDown_PsvrVFov.PerformLayout()
        CType(Me.NumericUpDown_PsvrHFov, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PsvrHFov.ResumeLayout(False)
        Me.NumericUpDown_PsvrHFov.PerformLayout()
        CType(Me.NumericUpDown_PsvrDistBlueOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PsvrDistBlueOffset.ResumeLayout(False)
        Me.NumericUpDown_PsvrDistBlueOffset.PerformLayout()
        CType(Me.NumericUpDown_PsvrDistGreenOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PsvrDistGreenOffset.ResumeLayout(False)
        Me.NumericUpDown_PsvrDistGreenOffset.PerformLayout()
        CType(Me.NumericUpDown_PsvrDistRedOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PsvrDistRedOffset.ResumeLayout(False)
        Me.NumericUpDown_PsvrDistRedOffset.PerformLayout()
        CType(Me.NumericUpDown_PsvrDistScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PsvrDistScale.ResumeLayout(False)
        Me.NumericUpDown_PsvrDistScale.PerformLayout()
        CType(Me.NumericUpDown_PsvrDistK1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PsvrDistK1.ResumeLayout(False)
        Me.NumericUpDown_PsvrDistK1.PerformLayout()
        CType(Me.NumericUpDown_PsvrDistK0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PsvrDistK0.ResumeLayout(False)
        Me.NumericUpDown_PsvrDistK0.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.NumericUpDown_PsvrIPD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PsvrIPD.ResumeLayout(False)
        Me.NumericUpDown_PsvrIPD.PerformLayout()
        Me.TabPage_SettingsPSmove.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumericUpDown_RecenterButtonTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_RecenterButtonTime.ResumeLayout(False)
        Me.NumericUpDown_RecenterButtonTime.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.NumericUpDown_TouchpadTouchArea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_TouchpadTouchArea.ResumeLayout(False)
        Me.NumericUpDown_TouchpadTouchArea.PerformLayout()
        CType(Me.NumericUpDown_TouchpadClickDeadzone, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_TouchpadClickDeadzone.ResumeLayout(False)
        Me.NumericUpDown_TouchpadClickDeadzone.PerformLayout()
        Me.TabPage_SettingsPlayspace.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.NumericUpDown_PlayCalibSideOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PlayCalibSideOffset.ResumeLayout(False)
        Me.NumericUpDown_PlayCalibSideOffset.PerformLayout()
        CType(Me.NumericUpDown_PlayCalibHeightOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PlayCalibHeightOffset.ResumeLayout(False)
        Me.NumericUpDown_PlayCalibHeightOffset.PerformLayout()
        CType(Me.NumericUpDown_PlayCalibForwardOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PlayCalibForwardOffset.ResumeLayout(False)
        Me.NumericUpDown_PlayCalibForwardOffset.PerformLayout()
        Me.TabPage_SettingsOther.ResumeLayout(False)
        Me.TabPage_SettingsOther.PerformLayout()
        CType(Me.NumericUpDown_OscThreadSleep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_OscThreadSleep.ResumeLayout(False)
        Me.NumericUpDown_OscThreadSleep.PerformLayout()
        Me.TabPage_PlayspaceCalib.ResumeLayout(False)
        Me.TabPage_PlayspaceCalib.PerformLayout()
        CType(Me.NumericUpDown_PlayCalibPrepTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PlayCalibPrepTime.ResumeLayout(False)
        Me.NumericUpDown_PlayCalibPrepTime.PerformLayout()
        Me.Panel_PlayCalibSteps.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        CType(Me.ClassPictureBoxQuality3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_Overrides.ResumeLayout(False)
        Me.Panel_SteamVRRestart.ResumeLayout(False)
        Me.Panel_SteamVRRestart.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ClassPictureBoxQuality4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip_AddTracker.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LinkLabel_ReadMore As LinkLabel
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
    Friend WithEvents Label16 As Label
    Friend WithEvents CheckBox_HmdRecenterEnabled As CheckBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Button_ResetRecenter As Button
    Friend WithEvents NumericUpDown_RecenterButtonTime As NumericUpDown
    Friend WithEvents Label13 As Label
    Friend WithEvents NumericUpDown_OscThreadSleep As NumericUpDown
    Friend WithEvents Label21 As Label
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
    Friend WithEvents ToolTip_Info As ToolTip
    Friend WithEvents NumericUpDown_PlayCalibPrepTime As NumericUpDown
    Friend WithEvents Label41 As Label
    Friend WithEvents TabPage_SettingsPlayspace As TabPage
    Friend WithEvents ComboBox_PlayCalibForwardMethod As ComboBox
    Friend WithEvents Label40 As Label
    Friend WithEvents NumericUpDown_PlayCalibHeightOffset As NumericUpDown
    Friend WithEvents Label29 As Label
    Friend WithEvents NumericUpDown_PlayCalibForwardOffset As NumericUpDown
    Friend WithEvents Label26 As Label
    Friend WithEvents Button_PlayCalibReset As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents ListView_Trackers As ClassListViewEx
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ContextMenuStrip_Trackers As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_TrackerRemove As ToolStripMenuItem
    Friend WithEvents Timer_VMTTrackers As Timer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ClassPictureBoxQuality4 As ClassPictureBoxQuality
    Friend WithEvents Label42 As Label
    Friend WithEvents Label43 As Label
    Friend WithEvents Label44 As Label
    Friend WithEvents ContextMenuStrip_AddTracker As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_AddTracker As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_AddHmd As ToolStripMenuItem
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents TabPage_SettingsPSVR As TabPage
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents NumericUpDown_PsvrIPD As NumericUpDown
    Friend WithEvents Label45 As Label
    Friend WithEvents GroupBox_Distortion As GroupBox
    Friend WithEvents NumericUpDown_PsvrDistK0 As NumericUpDown
    Friend WithEvents Label46 As Label
    Friend WithEvents NumericUpDown_PsvrDistScale As NumericUpDown
    Friend WithEvents Label48 As Label
    Friend WithEvents NumericUpDown_PsvrDistK1 As NumericUpDown
    Friend WithEvents Label47 As Label
    Friend WithEvents NumericUpDown_PsvrVFov As NumericUpDown
    Friend WithEvents Label53 As Label
    Friend WithEvents NumericUpDown_PsvrHFov As NumericUpDown
    Friend WithEvents Label52 As Label
    Friend WithEvents NumericUpDown_PsvrDistBlueOffset As NumericUpDown
    Friend WithEvents Label51 As Label
    Friend WithEvents NumericUpDown_PsvrDistGreenOffset As NumericUpDown
    Friend WithEvents Label50 As Label
    Friend WithEvents NumericUpDown_PsvrDistRedOffset As NumericUpDown
    Friend WithEvents Label49 As Label
    Friend WithEvents ComboBox_PsvrRenderResolution As ComboBox
    Friend WithEvents LinkLabel_PsvrDistReset As LinkLabel
    Friend WithEvents CheckBox_ShowDistSettings As CheckBox
    Friend WithEvents Label54 As Label
    Friend WithEvents Label62 As Label
    Friend WithEvents Label61 As Label
    Friend WithEvents Label60 As Label
    Friend WithEvents Label59 As Label
    Friend WithEvents Label58 As Label
    Friend WithEvents Label57 As Label
    Friend WithEvents Label56 As Label
    Friend WithEvents Label55 As Label
    Friend WithEvents Label63 As Label
    Friend WithEvents Label64 As Label
    Friend WithEvents CheckBox_OptimizePackets As CheckBox
    Friend WithEvents LinkLabel_PlayCalibShowSettings2 As LinkLabel
    Friend WithEvents ToolTip_Default As ToolTip
    Friend WithEvents Label17 As Label
    Friend WithEvents NumericUpDown_PlayCalibSideOffset As NumericUpDown
    Friend WithEvents UcNumericUpDownBig4 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig7 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig5 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig6 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig1 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig3 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig2 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig14 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig13 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig16 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig15 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig11 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig12 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig10 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig9 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig8 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig17 As UCNumericUpDownBig
End Class
