<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCVmtSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCVmtSettings))
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
        Me.LinkLabel_HmdRecenterFromOverride = New System.Windows.Forms.LinkLabel()
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
        Me.CheckBox_HybridGripToggle = New System.Windows.Forms.CheckBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.CheckBox_OculusGripToggle = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboBox_OculusButtonLayout = New System.Windows.Forms.ComboBox()
        Me.Label_TouchpadTouchAreaDeg = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.CheckBox_HtcTouchpadShortcuts = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBox_HtcGrabButtonMethod = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox_HtcTouchpadClickMethod = New System.Windows.Forms.ComboBox()
        Me.CheckBox_HtcTouchpadShortcutClick = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown_HtcTouchpadClickDeadzone = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig6 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.LinkLabel_TouchpadShortcutHelp = New System.Windows.Forms.LinkLabel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.NumericUpDown_JoystickArea = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig5 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ComboBox_JoystickMethod = New System.Windows.Forms.ComboBox()
        Me.TabPage_SettingsPlayspace = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CheckBox_PlayCalibAutoscale = New System.Windows.Forms.CheckBox()
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
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.CheckBox_EnableManualVelocity = New System.Windows.Forms.CheckBox()
        Me.CheckBox_EnableVelocityTrackers = New System.Windows.Forms.CheckBox()
        Me.CheckBox_EnableVelocityControllers = New System.Windows.Forms.CheckBox()
        Me.CheckBox_EnableVelocityHmd = New System.Windows.Forms.CheckBox()
        Me.LinkLabel_OscIpChange = New System.Windows.Forms.LinkLabel()
        Me.TextBox_OscRemoteIP = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBox_RenderFix = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown_OscMaxThreadFps = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig18 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.CheckBox_OptimizePackets = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown_OscThreadSleep = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig4 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.CheckBox_EnableHeptics = New System.Windows.Forms.CheckBox()
        Me.CheckBox_DisableBasestations = New System.Windows.Forms.CheckBox()
        Me.Button_SaveControllerSettings = New System.Windows.Forms.Button()
        Me.ToolTip_Info = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip_Default = New System.Windows.Forms.ToolTip(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
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
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.NumericUpDown_HtcTouchpadClickDeadzone, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_HtcTouchpadClickDeadzone.SuspendLayout()
        CType(Me.NumericUpDown_JoystickArea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_JoystickArea.SuspendLayout()
        Me.TabPage_SettingsPlayspace.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.NumericUpDown_PlayCalibSideOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PlayCalibSideOffset.SuspendLayout()
        CType(Me.NumericUpDown_PlayCalibHeightOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PlayCalibHeightOffset.SuspendLayout()
        CType(Me.NumericUpDown_PlayCalibForwardOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PlayCalibForwardOffset.SuspendLayout()
        Me.TabPage_SettingsOther.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.NumericUpDown_OscMaxThreadFps, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_OscMaxThreadFps.SuspendLayout()
        CType(Me.NumericUpDown_OscThreadSleep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_OscThreadSleep.SuspendLayout()
        Me.SuspendLayout()
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
        Me.TabControl_SettingsDevices.Location = New System.Drawing.Point(3, 3)
        Me.TabControl_SettingsDevices.Name = "TabControl_SettingsDevices"
        Me.TabControl_SettingsDevices.SelectedIndex = 0
        Me.TabControl_SettingsDevices.Size = New System.Drawing.Size(794, 1119)
        Me.TabControl_SettingsDevices.TabIndex = 48
        '
        'TabPage_SettingsPSVR
        '
        Me.TabPage_SettingsPSVR.BackColor = System.Drawing.Color.White
        Me.TabPage_SettingsPSVR.Controls.Add(Me.GroupBox_Distortion)
        Me.TabPage_SettingsPSVR.Controls.Add(Me.GroupBox4)
        Me.TabPage_SettingsPSVR.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_SettingsPSVR.Name = "TabPage_SettingsPSVR"
        Me.TabPage_SettingsPSVR.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_SettingsPSVR.Size = New System.Drawing.Size(786, 1093)
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
        Me.GroupBox_Distortion.Size = New System.Drawing.Size(780, 286)
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
        Me.GroupBox4.Size = New System.Drawing.Size(780, 106)
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
        Me.ToolTip_Default.SetToolTip(Me.ComboBox_PsvrRenderResolution, "Default: 130%; Recommended: 250%")
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
        Me.TabPage_SettingsPSmove.Size = New System.Drawing.Size(786, 1093)
        Me.TabPage_SettingsPSmove.TabIndex = 0
        Me.TabPage_SettingsPSmove.Text = "PSMove Controller"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LinkLabel_HmdRecenterFromOverride)
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
        Me.GroupBox1.Location = New System.Drawing.Point(3, 347)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(780, 526)
        Me.GroupBox1.TabIndex = 47
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Recenter Settings"
        '
        'LinkLabel_HmdRecenterFromOverride
        '
        Me.LinkLabel_HmdRecenterFromOverride.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_HmdRecenterFromOverride.AutoSize = True
        Me.LinkLabel_HmdRecenterFromOverride.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_HmdRecenterFromOverride.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_HmdRecenterFromOverride.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_HmdRecenterFromOverride.Location = New System.Drawing.Point(198, 312)
        Me.LinkLabel_HmdRecenterFromOverride.Name = "LinkLabel_HmdRecenterFromOverride"
        Me.LinkLabel_HmdRecenterFromOverride.Size = New System.Drawing.Size(307, 13)
        Me.LinkLabel_HmdRecenterFromOverride.TabIndex = 72
        Me.LinkLabel_HmdRecenterFromOverride.TabStop = True
        Me.LinkLabel_HmdRecenterFromOverride.Text = "Set from Head-mounted Display SteamVR tracker overrides"
        Me.LinkLabel_HmdRecenterFromOverride.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_PlayCalibShowSettings2
        '
        Me.LinkLabel_PlayCalibShowSettings2.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_PlayCalibShowSettings2.AutoSize = True
        Me.LinkLabel_PlayCalibShowSettings2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_PlayCalibShowSettings2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_PlayCalibShowSettings2.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_PlayCalibShowSettings2.Location = New System.Drawing.Point(48, 457)
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
        Me.Label_ScrollFocus.Location = New System.Drawing.Point(682, 510)
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
        Me.Label25.Location = New System.Drawing.Point(48, 406)
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
        Me.CheckBox_PlayCalibEnabled.Location = New System.Drawing.Point(16, 383)
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
        Me.NumericUpDown_RecenterButtonTime.Location = New System.Drawing.Point(201, 484)
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
        Me.Label13.Location = New System.Drawing.Point(16, 486)
        Me.Label13.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(172, 13)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "Recenter button press time (ms):"
        '
        'Button_ResetRecenter
        '
        Me.Button_ResetRecenter.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.Button_ResetRecenter.Location = New System.Drawing.Point(16, 341)
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
        Me.Label20.Location = New System.Drawing.Point(48, 223)
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
        Me.ComboBox_HmdRecenterFromDevice.Location = New System.Drawing.Point(201, 288)
        Me.ComboBox_HmdRecenterFromDevice.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_HmdRecenterFromDevice.Name = "ComboBox_HmdRecenterFromDevice"
        Me.ComboBox_HmdRecenterFromDevice.Size = New System.Drawing.Size(531, 21)
        Me.ComboBox_HmdRecenterFromDevice.TabIndex = 57
        Me.ToolTip_Info.SetToolTip(Me.ComboBox_HmdRecenterFromDevice, "The list might not be populated with trackers if the OSC Server is not running.")
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(48, 291)
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
        Me.ComboBox_HmdRecenterMethod.Location = New System.Drawing.Point(201, 261)
        Me.ComboBox_HmdRecenterMethod.Margin = New System.Windows.Forms.Padding(3, 16, 48, 3)
        Me.ComboBox_HmdRecenterMethod.Name = "ComboBox_HmdRecenterMethod"
        Me.ComboBox_HmdRecenterMethod.Size = New System.Drawing.Size(531, 21)
        Me.ComboBox_HmdRecenterMethod.TabIndex = 55
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(48, 264)
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
        Me.Label16.Location = New System.Drawing.Point(48, 185)
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
        Me.CheckBox_HmdRecenterEnabled.Location = New System.Drawing.Point(16, 162)
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
        Me.ComboBox_RecenterFromDevice.Location = New System.Drawing.Point(198, 122)
        Me.ComboBox_RecenterFromDevice.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_RecenterFromDevice.Name = "ComboBox_RecenterFromDevice"
        Me.ComboBox_RecenterFromDevice.Size = New System.Drawing.Size(534, 21)
        Me.ComboBox_RecenterFromDevice.TabIndex = 50
        Me.ToolTip_Info.SetToolTip(Me.ComboBox_RecenterFromDevice, "The list might not be populated with trackers if the OSC Server is not running.")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(45, 125)
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
        Me.ComboBox_RecenterMethod.Location = New System.Drawing.Point(198, 95)
        Me.ComboBox_RecenterMethod.Margin = New System.Windows.Forms.Padding(3, 16, 48, 3)
        Me.ComboBox_RecenterMethod.Name = "ComboBox_RecenterMethod"
        Me.ComboBox_RecenterMethod.Size = New System.Drawing.Size(534, 21)
        Me.ComboBox_RecenterMethod.TabIndex = 48
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(45, 98)
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
        Me.Label11.Location = New System.Drawing.Point(45, 44)
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
        Me.CheckBox_ControllerRecenterEnabled.Location = New System.Drawing.Point(16, 21)
        Me.CheckBox_ControllerRecenterEnabled.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_ControllerRecenterEnabled.Name = "CheckBox_ControllerRecenterEnabled"
        Me.CheckBox_ControllerRecenterEnabled.Size = New System.Drawing.Size(236, 18)
        Me.CheckBox_ControllerRecenterEnabled.TabIndex = 0
        Me.CheckBox_ControllerRecenterEnabled.Text = "Enable orientation recentering shortcut"
        Me.CheckBox_ControllerRecenterEnabled.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CheckBox_HybridGripToggle)
        Me.GroupBox2.Controls.Add(Me.GroupBox6)
        Me.GroupBox2.Controls.Add(Me.Label_TouchpadTouchAreaDeg)
        Me.GroupBox2.Controls.Add(Me.GroupBox5)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_JoystickArea)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.ComboBox_JoystickMethod)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(780, 344)
        Me.GroupBox2.TabIndex = 46
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Controller Emulation Settings"
        '
        'CheckBox_HybridGripToggle
        '
        Me.CheckBox_HybridGripToggle.AutoSize = True
        Me.CheckBox_HybridGripToggle.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_HybridGripToggle.Location = New System.Drawing.Point(16, 315)
        Me.CheckBox_HybridGripToggle.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_HybridGripToggle.Name = "CheckBox_HybridGripToggle"
        Me.CheckBox_HybridGripToggle.Size = New System.Drawing.Size(127, 18)
        Me.CheckBox_HybridGripToggle.TabIndex = 56
        Me.CheckBox_HybridGripToggle.Text = "Hybrid grip toggle"
        Me.CheckBox_HybridGripToggle.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.CheckBox_OculusGripToggle)
        Me.GroupBox6.Controls.Add(Me.Label8)
        Me.GroupBox6.Controls.Add(Me.ComboBox_OculusButtonLayout)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox6.Location = New System.Drawing.Point(3, 175)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(774, 79)
        Me.GroupBox6.TabIndex = 55
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Oculus Touch Controller"
        '
        'CheckBox_OculusGripToggle
        '
        Me.CheckBox_OculusGripToggle.AutoSize = True
        Me.CheckBox_OculusGripToggle.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_OculusGripToggle.Location = New System.Drawing.Point(16, 48)
        Me.CheckBox_OculusGripToggle.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_OculusGripToggle.Name = "CheckBox_OculusGripToggle"
        Me.CheckBox_OculusGripToggle.Size = New System.Drawing.Size(91, 18)
        Me.CheckBox_OculusGripToggle.TabIndex = 54
        Me.CheckBox_OculusGripToggle.Text = "Grip toggle"
        Me.CheckBox_OculusGripToggle.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 24)
        Me.Label8.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 13)
        Me.Label8.TabIndex = 54
        Me.Label8.Text = "Button layout:"
        '
        'ComboBox_OculusButtonLayout
        '
        Me.ComboBox_OculusButtonLayout.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_OculusButtonLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_OculusButtonLayout.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_OculusButtonLayout.FormattingEnabled = True
        Me.ComboBox_OculusButtonLayout.Location = New System.Drawing.Point(195, 21)
        Me.ComboBox_OculusButtonLayout.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_OculusButtonLayout.Name = "ComboBox_OculusButtonLayout"
        Me.ComboBox_OculusButtonLayout.Size = New System.Drawing.Size(534, 21)
        Me.ComboBox_OculusButtonLayout.TabIndex = 55
        '
        'Label_TouchpadTouchAreaDeg
        '
        Me.Label_TouchpadTouchAreaDeg.AutoSize = True
        Me.Label_TouchpadTouchAreaDeg.Location = New System.Drawing.Point(321, 289)
        Me.Label_TouchpadTouchAreaDeg.Name = "Label_TouchpadTouchAreaDeg"
        Me.Label_TouchpadTouchAreaDeg.Size = New System.Drawing.Size(41, 13)
        Me.Label_TouchpadTouchAreaDeg.TabIndex = 53
        Me.Label_TouchpadTouchAreaDeg.Text = "cm / 0°"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.CheckBox_HtcTouchpadShortcuts)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.ComboBox_HtcGrabButtonMethod)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Controls.Add(Me.ComboBox_HtcTouchpadClickMethod)
        Me.GroupBox5.Controls.Add(Me.CheckBox_HtcTouchpadShortcutClick)
        Me.GroupBox5.Controls.Add(Me.NumericUpDown_HtcTouchpadClickDeadzone)
        Me.GroupBox5.Controls.Add(Me.Label22)
        Me.GroupBox5.Controls.Add(Me.LinkLabel_TouchpadShortcutHelp)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox5.Location = New System.Drawing.Point(3, 18)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(774, 157)
        Me.GroupBox5.TabIndex = 54
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "HTC Vive Controller"
        '
        'CheckBox_HtcTouchpadShortcuts
        '
        Me.CheckBox_HtcTouchpadShortcuts.AutoSize = True
        Me.CheckBox_HtcTouchpadShortcuts.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_HtcTouchpadShortcuts.Location = New System.Drawing.Point(20, 21)
        Me.CheckBox_HtcTouchpadShortcuts.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_HtcTouchpadShortcuts.Name = "CheckBox_HtcTouchpadShortcuts"
        Me.CheckBox_HtcTouchpadShortcuts.Size = New System.Drawing.Size(226, 18)
        Me.CheckBox_HtcTouchpadShortcuts.TabIndex = 42
        Me.CheckBox_HtcTouchpadShortcuts.Text = "Enable touchpad emulation shortcuts"
        Me.CheckBox_HtcTouchpadShortcuts.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 72)
        Me.Label4.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(129, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Touchpad click method:"
        '
        'ComboBox_HtcGrabButtonMethod
        '
        Me.ComboBox_HtcGrabButtonMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_HtcGrabButtonMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_HtcGrabButtonMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_HtcGrabButtonMethod.FormattingEnabled = True
        Me.ComboBox_HtcGrabButtonMethod.Location = New System.Drawing.Point(202, 124)
        Me.ComboBox_HtcGrabButtonMethod.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_HtcGrabButtonMethod.Name = "ComboBox_HtcGrabButtonMethod"
        Me.ComboBox_HtcGrabButtonMethod.Size = New System.Drawing.Size(527, 21)
        Me.ComboBox_HtcGrabButtonMethod.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 127)
        Me.Label5.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(114, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Grip button method:"
        '
        'ComboBox_HtcTouchpadClickMethod
        '
        Me.ComboBox_HtcTouchpadClickMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_HtcTouchpadClickMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_HtcTouchpadClickMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_HtcTouchpadClickMethod.FormattingEnabled = True
        Me.ComboBox_HtcTouchpadClickMethod.Location = New System.Drawing.Point(202, 69)
        Me.ComboBox_HtcTouchpadClickMethod.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_HtcTouchpadClickMethod.Name = "ComboBox_HtcTouchpadClickMethod"
        Me.ComboBox_HtcTouchpadClickMethod.Size = New System.Drawing.Size(527, 21)
        Me.ComboBox_HtcTouchpadClickMethod.TabIndex = 1
        '
        'CheckBox_HtcTouchpadShortcutClick
        '
        Me.CheckBox_HtcTouchpadShortcutClick.AutoSize = True
        Me.CheckBox_HtcTouchpadShortcutClick.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_HtcTouchpadShortcutClick.Location = New System.Drawing.Point(52, 45)
        Me.CheckBox_HtcTouchpadShortcutClick.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.CheckBox_HtcTouchpadShortcutClick.Name = "CheckBox_HtcTouchpadShortcutClick"
        Me.CheckBox_HtcTouchpadShortcutClick.Size = New System.Drawing.Size(224, 18)
        Me.CheckBox_HtcTouchpadShortcutClick.TabIndex = 44
        Me.CheckBox_HtcTouchpadShortcutClick.Text = "Click touchpad when using shortcuts"
        Me.CheckBox_HtcTouchpadShortcutClick.UseVisualStyleBackColor = True
        '
        'NumericUpDown_HtcTouchpadClickDeadzone
        '
        Me.NumericUpDown_HtcTouchpadClickDeadzone.Controls.Add(Me.UcNumericUpDownBig6)
        Me.NumericUpDown_HtcTouchpadClickDeadzone.DecimalPlaces = 2
        Me.NumericUpDown_HtcTouchpadClickDeadzone.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.NumericUpDown_HtcTouchpadClickDeadzone.Location = New System.Drawing.Point(195, 96)
        Me.NumericUpDown_HtcTouchpadClickDeadzone.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_HtcTouchpadClickDeadzone.Name = "NumericUpDown_HtcTouchpadClickDeadzone"
        Me.NumericUpDown_HtcTouchpadClickDeadzone.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_HtcTouchpadClickDeadzone.TabIndex = 50
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_HtcTouchpadClickDeadzone, "Default: 0.25")
        Me.NumericUpDown_HtcTouchpadClickDeadzone.Value = New Decimal(New Integer() {25, 0, 0, 131072})
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
        Me.UcNumericUpDownBig6.m_NumericUpDown = Me.NumericUpDown_HtcTouchpadClickDeadzone
        Me.UcNumericUpDownBig6.m_ResetValue = New Decimal(New Integer() {25, 0, 0, 131072})
        Me.UcNumericUpDownBig6.m_ResetVisible = True
        Me.UcNumericUpDownBig6.Name = "UcNumericUpDownBig6"
        Me.UcNumericUpDownBig6.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig6.TabIndex = 54
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(20, 98)
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
        Me.LinkLabel_TouchpadShortcutHelp.Location = New System.Drawing.Point(252, 20)
        Me.LinkLabel_TouchpadShortcutHelp.Name = "LinkLabel_TouchpadShortcutHelp"
        Me.LinkLabel_TouchpadShortcutHelp.Padding = New System.Windows.Forms.Padding(18, 3, 0, 3)
        Me.LinkLabel_TouchpadShortcutHelp.Size = New System.Drawing.Size(91, 19)
        Me.LinkLabel_TouchpadShortcutHelp.TabIndex = 48
        Me.LinkLabel_TouchpadShortcutHelp.TabStop = True
        Me.LinkLabel_TouchpadShortcutHelp.Text = "What is this?"
        Me.LinkLabel_TouchpadShortcutHelp.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(13, 289)
        Me.Label23.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(176, 13)
        Me.Label23.TabIndex = 51
        Me.Label23.Text = "Touchpad/Joystick area (cm/deg):"
        '
        'NumericUpDown_JoystickArea
        '
        Me.NumericUpDown_JoystickArea.Controls.Add(Me.UcNumericUpDownBig5)
        Me.NumericUpDown_JoystickArea.DecimalPlaces = 2
        Me.NumericUpDown_JoystickArea.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.NumericUpDown_JoystickArea.Location = New System.Drawing.Point(195, 287)
        Me.NumericUpDown_JoystickArea.Name = "NumericUpDown_JoystickArea"
        Me.NumericUpDown_JoystickArea.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_JoystickArea.TabIndex = 52
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_JoystickArea, "Default: 7.50")
        Me.NumericUpDown_JoystickArea.Value = New Decimal(New Integer() {750, 0, 0, 131072})
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
        Me.UcNumericUpDownBig5.m_NumericUpDown = Me.NumericUpDown_JoystickArea
        Me.UcNumericUpDownBig5.m_ResetValue = New Decimal(New Integer() {750, 0, 0, 131072})
        Me.UcNumericUpDownBig5.m_ResetVisible = True
        Me.UcNumericUpDownBig5.Name = "UcNumericUpDownBig5"
        Me.UcNumericUpDownBig5.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig5.TabIndex = 54
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 263)
        Me.Label10.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(147, 13)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Touchpad/Joystick method:"
        '
        'ComboBox_JoystickMethod
        '
        Me.ComboBox_JoystickMethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_JoystickMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_JoystickMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_JoystickMethod.FormattingEnabled = True
        Me.ComboBox_JoystickMethod.Location = New System.Drawing.Point(195, 260)
        Me.ComboBox_JoystickMethod.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_JoystickMethod.Name = "ComboBox_JoystickMethod"
        Me.ComboBox_JoystickMethod.Size = New System.Drawing.Size(534, 21)
        Me.ComboBox_JoystickMethod.TabIndex = 47
        '
        'TabPage_SettingsPlayspace
        '
        Me.TabPage_SettingsPlayspace.BackColor = System.Drawing.Color.White
        Me.TabPage_SettingsPlayspace.Controls.Add(Me.GroupBox3)
        Me.TabPage_SettingsPlayspace.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_SettingsPlayspace.Name = "TabPage_SettingsPlayspace"
        Me.TabPage_SettingsPlayspace.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_SettingsPlayspace.Size = New System.Drawing.Size(786, 1093)
        Me.TabPage_SettingsPlayspace.TabIndex = 2
        Me.TabPage_SettingsPlayspace.Text = "Playspace"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CheckBox_PlayCalibAutoscale)
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
        Me.GroupBox3.Size = New System.Drawing.Size(780, 221)
        Me.GroupBox3.TabIndex = 82
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Playspace Calibration Settings"
        '
        'CheckBox_PlayCalibAutoscale
        '
        Me.CheckBox_PlayCalibAutoscale.AutoSize = True
        Me.CheckBox_PlayCalibAutoscale.Enabled = False
        Me.CheckBox_PlayCalibAutoscale.Location = New System.Drawing.Point(19, 148)
        Me.CheckBox_PlayCalibAutoscale.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.CheckBox_PlayCalibAutoscale.Name = "CheckBox_PlayCalibAutoscale"
        Me.CheckBox_PlayCalibAutoscale.Size = New System.Drawing.Size(151, 17)
        Me.CheckBox_PlayCalibAutoscale.TabIndex = 84
        Me.CheckBox_PlayCalibAutoscale.Text = "Adjust playspace scaling"
        Me.CheckBox_PlayCalibAutoscale.UseVisualStyleBackColor = True
        Me.CheckBox_PlayCalibAutoscale.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(16, 62)
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
        Me.Label26.Location = New System.Drawing.Point(16, 34)
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
        Me.ComboBox_PlayCalibForwardMethod.Size = New System.Drawing.Size(595, 21)
        Me.ComboBox_PlayCalibForwardMethod.TabIndex = 81
        '
        'Button_PlayCalibReset
        '
        Me.Button_PlayCalibReset.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.Button_PlayCalibReset.Location = New System.Drawing.Point(19, 181)
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
        Me.Label40.Location = New System.Drawing.Point(16, 119)
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
        Me.Label29.Location = New System.Drawing.Point(16, 90)
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
        Me.TabPage_SettingsOther.Controls.Add(Me.GroupBox7)
        Me.TabPage_SettingsOther.Controls.Add(Me.LinkLabel_OscIpChange)
        Me.TabPage_SettingsOther.Controls.Add(Me.TextBox_OscRemoteIP)
        Me.TabPage_SettingsOther.Controls.Add(Me.Label3)
        Me.TabPage_SettingsOther.Controls.Add(Me.CheckBox_RenderFix)
        Me.TabPage_SettingsOther.Controls.Add(Me.NumericUpDown_OscMaxThreadFps)
        Me.TabPage_SettingsOther.Controls.Add(Me.Label65)
        Me.TabPage_SettingsOther.Controls.Add(Me.CheckBox_OptimizePackets)
        Me.TabPage_SettingsOther.Controls.Add(Me.NumericUpDown_OscThreadSleep)
        Me.TabPage_SettingsOther.Controls.Add(Me.Label21)
        Me.TabPage_SettingsOther.Controls.Add(Me.CheckBox_EnableHeptics)
        Me.TabPage_SettingsOther.Controls.Add(Me.CheckBox_DisableBasestations)
        Me.TabPage_SettingsOther.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_SettingsOther.Name = "TabPage_SettingsOther"
        Me.TabPage_SettingsOther.Size = New System.Drawing.Size(786, 1093)
        Me.TabPage_SettingsOther.TabIndex = 1
        Me.TabPage_SettingsOther.Text = "Other"
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox7.Controls.Add(Me.CheckBox_EnableManualVelocity)
        Me.GroupBox7.Controls.Add(Me.CheckBox_EnableVelocityTrackers)
        Me.GroupBox7.Controls.Add(Me.CheckBox_EnableVelocityControllers)
        Me.GroupBox7.Controls.Add(Me.CheckBox_EnableVelocityHmd)
        Me.GroupBox7.Location = New System.Drawing.Point(3, 191)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(780, 125)
        Me.GroupBox7.TabIndex = 61
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Linear and angular velocity"
        '
        'CheckBox_EnableManualVelocity
        '
        Me.CheckBox_EnableManualVelocity.AutoSize = True
        Me.CheckBox_EnableManualVelocity.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_EnableManualVelocity.Location = New System.Drawing.Point(13, 93)
        Me.CheckBox_EnableManualVelocity.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_EnableManualVelocity.Name = "CheckBox_EnableManualVelocity"
        Me.CheckBox_EnableManualVelocity.Size = New System.Drawing.Size(213, 18)
        Me.CheckBox_EnableManualVelocity.TabIndex = 62
        Me.CheckBox_EnableManualVelocity.Text = "Calculate velocity per frame instead"
        Me.ToolTip_Info.SetToolTip(Me.CheckBox_EnableManualVelocity, resources.GetString("CheckBox_EnableManualVelocity.ToolTip"))
        Me.CheckBox_EnableManualVelocity.UseVisualStyleBackColor = True
        '
        'CheckBox_EnableVelocityTrackers
        '
        Me.CheckBox_EnableVelocityTrackers.AutoSize = True
        Me.CheckBox_EnableVelocityTrackers.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_EnableVelocityTrackers.Location = New System.Drawing.Point(13, 69)
        Me.CheckBox_EnableVelocityTrackers.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_EnableVelocityTrackers.Name = "CheckBox_EnableVelocityTrackers"
        Me.CheckBox_EnableVelocityTrackers.Size = New System.Drawing.Size(129, 18)
        Me.CheckBox_EnableVelocityTrackers.TabIndex = 61
        Me.CheckBox_EnableVelocityTrackers.Text = "Enable for Trackers"
        Me.ToolTip_Info.SetToolTip(Me.CheckBox_EnableVelocityTrackers, "Some games require linear and angular velocity data from SteamVR to function prop" &
        "erly." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "For example, disabling velocity can prevent throwing objects in games." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.CheckBox_EnableVelocityTrackers.UseVisualStyleBackColor = True
        '
        'CheckBox_EnableVelocityControllers
        '
        Me.CheckBox_EnableVelocityControllers.AutoSize = True
        Me.CheckBox_EnableVelocityControllers.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_EnableVelocityControllers.Location = New System.Drawing.Point(13, 45)
        Me.CheckBox_EnableVelocityControllers.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_EnableVelocityControllers.Name = "CheckBox_EnableVelocityControllers"
        Me.CheckBox_EnableVelocityControllers.Size = New System.Drawing.Size(145, 18)
        Me.CheckBox_EnableVelocityControllers.TabIndex = 60
        Me.CheckBox_EnableVelocityControllers.Text = "Enable for Controllers"
        Me.ToolTip_Info.SetToolTip(Me.CheckBox_EnableVelocityControllers, "Some games require linear and angular velocity data from SteamVR to function prop" &
        "erly." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "For example, disabling velocity can prevent throwing objects in games." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.CheckBox_EnableVelocityControllers.UseVisualStyleBackColor = True
        '
        'CheckBox_EnableVelocityHmd
        '
        Me.CheckBox_EnableVelocityHmd.AutoSize = True
        Me.CheckBox_EnableVelocityHmd.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_EnableVelocityHmd.Location = New System.Drawing.Point(13, 21)
        Me.CheckBox_EnableVelocityHmd.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_EnableVelocityHmd.Name = "CheckBox_EnableVelocityHmd"
        Me.CheckBox_EnableVelocityHmd.Size = New System.Drawing.Size(211, 18)
        Me.CheckBox_EnableVelocityHmd.TabIndex = 59
        Me.CheckBox_EnableVelocityHmd.Text = "Enable for Head-mounted Displays"
        Me.ToolTip_Info.SetToolTip(Me.CheckBox_EnableVelocityHmd, "Some games require linear and angular velocity data from SteamVR to function prop" &
        "erly." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "For example, disabling velocity can prevent throwing objects in games." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.CheckBox_EnableVelocityHmd.UseVisualStyleBackColor = True
        '
        'LinkLabel_OscIpChange
        '
        Me.LinkLabel_OscIpChange.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_OscIpChange.AutoSize = True
        Me.LinkLabel_OscIpChange.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_OscIpChange.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_OscIpChange.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_OscIpChange.Location = New System.Drawing.Point(353, 166)
        Me.LinkLabel_OscIpChange.Margin = New System.Windows.Forms.Padding(3)
        Me.LinkLabel_OscIpChange.Name = "LinkLabel_OscIpChange"
        Me.LinkLabel_OscIpChange.Size = New System.Drawing.Size(90, 13)
        Me.LinkLabel_OscIpChange.TabIndex = 58
        Me.LinkLabel_OscIpChange.TabStop = True
        Me.LinkLabel_OscIpChange.Text = "Change address"
        Me.LinkLabel_OscIpChange.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'TextBox_OscRemoteIP
        '
        Me.TextBox_OscRemoteIP.BackColor = System.Drawing.Color.White
        Me.TextBox_OscRemoteIP.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.TextBox_OscRemoteIP.Location = New System.Drawing.Point(229, 163)
        Me.TextBox_OscRemoteIP.Name = "TextBox_OscRemoteIP"
        Me.TextBox_OscRemoteIP.ReadOnly = True
        Me.TextBox_OscRemoteIP.Size = New System.Drawing.Size(118, 22)
        Me.TextBox_OscRemoteIP.TabIndex = 57
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 166)
        Me.Label3.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 13)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "OSC Remote IP:"
        '
        'CheckBox_RenderFix
        '
        Me.CheckBox_RenderFix.AutoSize = True
        Me.CheckBox_RenderFix.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_RenderFix.Location = New System.Drawing.Point(16, 139)
        Me.CheckBox_RenderFix.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_RenderFix.Name = "CheckBox_RenderFix"
        Me.CheckBox_RenderFix.Size = New System.Drawing.Size(238, 18)
        Me.CheckBox_RenderFix.TabIndex = 54
        Me.CheckBox_RenderFix.Text = "Enable Virtual-Mode SteamVR render fix"
        Me.ToolTip_Info.SetToolTip(Me.CheckBox_RenderFix, "SteamVR has a bug where rendering stops if the Virtual-Mode render window loses f" &
        "ocus. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enabling this setting can resolve the issue." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.CheckBox_RenderFix.UseVisualStyleBackColor = True
        '
        'NumericUpDown_OscMaxThreadFps
        '
        Me.NumericUpDown_OscMaxThreadFps.Controls.Add(Me.UcNumericUpDownBig18)
        Me.NumericUpDown_OscMaxThreadFps.Location = New System.Drawing.Point(229, 87)
        Me.NumericUpDown_OscMaxThreadFps.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.NumericUpDown_OscMaxThreadFps.Minimum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.NumericUpDown_OscMaxThreadFps.Name = "NumericUpDown_OscMaxThreadFps"
        Me.NumericUpDown_OscMaxThreadFps.Size = New System.Drawing.Size(118, 22)
        Me.NumericUpDown_OscMaxThreadFps.TabIndex = 53
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_OscMaxThreadFps, "Default: 200")
        Me.NumericUpDown_OscMaxThreadFps.Value = New Decimal(New Integer() {200, 0, 0, 0})
        '
        'UcNumericUpDownBig18
        '
        Me.UcNumericUpDownBig18.AutoSize = True
        Me.UcNumericUpDownBig18.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig18.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig18.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig18.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig18.Location = New System.Drawing.Point(52, 0)
        Me.UcNumericUpDownBig18.m_bDockOnControl = True
        Me.UcNumericUpDownBig18.m_NumericUpDown = Me.NumericUpDown_OscMaxThreadFps
        Me.UcNumericUpDownBig18.m_ResetValue = New Decimal(New Integer() {200, 0, 0, 0})
        Me.UcNumericUpDownBig18.m_ResetVisible = True
        Me.UcNumericUpDownBig18.Name = "UcNumericUpDownBig18"
        Me.UcNumericUpDownBig18.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig18.TabIndex = 52
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(16, 89)
        Me.Label65.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(207, 13)
        Me.Label65.TabIndex = 52
        Me.Label65.Text = "Maximum processing thread framerate:"
        '
        'CheckBox_OptimizePackets
        '
        Me.CheckBox_OptimizePackets.AutoSize = True
        Me.CheckBox_OptimizePackets.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_OptimizePackets.Location = New System.Drawing.Point(16, 115)
        Me.CheckBox_OptimizePackets.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_OptimizePackets.Name = "CheckBox_OptimizePackets"
        Me.CheckBox_OptimizePackets.Size = New System.Drawing.Size(208, 18)
        Me.CheckBox_OptimizePackets.TabIndex = 50
        Me.CheckBox_OptimizePackets.Text = "Optimize OSC packet transmission"
        Me.ToolTip_Info.SetToolTip(Me.CheckBox_OptimizePackets, "Only send OSC packets if they are different from the previous send packet." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This " &
        "reduces the number of transmission packets." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Disable this setting if you notice " &
        "packet loss or unusual behavior." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.CheckBox_OptimizePackets.UseVisualStyleBackColor = True
        '
        'NumericUpDown_OscThreadSleep
        '
        Me.NumericUpDown_OscThreadSleep.Controls.Add(Me.UcNumericUpDownBig4)
        Me.NumericUpDown_OscThreadSleep.Location = New System.Drawing.Point(229, 56)
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
        Me.Label21.Location = New System.Drawing.Point(16, 58)
        Me.Label21.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(155, 13)
        Me.Label21.TabIndex = 48
        Me.Label21.Text = "Processing thread sleep (ms):"
        '
        'CheckBox_EnableHeptics
        '
        Me.CheckBox_EnableHeptics.AutoSize = True
        Me.CheckBox_EnableHeptics.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_EnableHeptics.Location = New System.Drawing.Point(16, 32)
        Me.CheckBox_EnableHeptics.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_EnableHeptics.Name = "CheckBox_EnableHeptics"
        Me.CheckBox_EnableHeptics.Size = New System.Drawing.Size(338, 18)
        Me.CheckBox_EnableHeptics.TabIndex = 46
        Me.CheckBox_EnableHeptics.Text = "Enable haptic feedback (PSMove && DualShock 4 controllers)"
        Me.ToolTip_Info.SetToolTip(Me.CheckBox_EnableHeptics, "Enabling haptic feedback can significantly reduce battery life. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "On PSMove contr" &
        "ollers, enabling the rumble motor may interfere with the IMU magnetometer and" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "l" &
        "ead to orientation drift.")
        Me.CheckBox_EnableHeptics.UseVisualStyleBackColor = True
        '
        'CheckBox_DisableBasestations
        '
        Me.CheckBox_DisableBasestations.AutoSize = True
        Me.CheckBox_DisableBasestations.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_DisableBasestations.Location = New System.Drawing.Point(16, 8)
        Me.CheckBox_DisableBasestations.Margin = New System.Windows.Forms.Padding(16, 8, 3, 3)
        Me.CheckBox_DisableBasestations.Name = "CheckBox_DisableBasestations"
        Me.CheckBox_DisableBasestations.Size = New System.Drawing.Size(241, 18)
        Me.CheckBox_DisableBasestations.TabIndex = 43
        Me.CheckBox_DisableBasestations.Text = "Disable emulated Base Station spawning"
        Me.ToolTip_Info.SetToolTip(Me.CheckBox_DisableBasestations, resources.GetString("CheckBox_DisableBasestations.ToolTip"))
        Me.CheckBox_DisableBasestations.UseVisualStyleBackColor = True
        '
        'Button_SaveControllerSettings
        '
        Me.Button_SaveControllerSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SaveControllerSettings.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16761_16x16_32
        Me.Button_SaveControllerSettings.Location = New System.Drawing.Point(664, 1141)
        Me.Button_SaveControllerSettings.Margin = New System.Windows.Forms.Padding(16)
        Me.Button_SaveControllerSettings.Name = "Button_SaveControllerSettings"
        Me.Button_SaveControllerSettings.Size = New System.Drawing.Size(120, 23)
        Me.Button_SaveControllerSettings.TabIndex = 49
        Me.Button_SaveControllerSettings.Text = "Save settings"
        Me.Button_SaveControllerSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_SaveControllerSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_SaveControllerSettings.UseVisualStyleBackColor = True
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
        'UCVmtSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Button_SaveControllerSettings)
        Me.Controls.Add(Me.TabControl_SettingsDevices)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVmtSettings"
        Me.Size = New System.Drawing.Size(800, 1180)
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
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.NumericUpDown_HtcTouchpadClickDeadzone, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_HtcTouchpadClickDeadzone.ResumeLayout(False)
        Me.NumericUpDown_HtcTouchpadClickDeadzone.PerformLayout()
        CType(Me.NumericUpDown_JoystickArea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_JoystickArea.ResumeLayout(False)
        Me.NumericUpDown_JoystickArea.PerformLayout()
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
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        CType(Me.NumericUpDown_OscMaxThreadFps, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_OscMaxThreadFps.ResumeLayout(False)
        Me.NumericUpDown_OscMaxThreadFps.PerformLayout()
        CType(Me.NumericUpDown_OscThreadSleep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_OscThreadSleep.ResumeLayout(False)
        Me.NumericUpDown_OscThreadSleep.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl_SettingsDevices As TabControl
    Friend WithEvents TabPage_SettingsPSVR As TabPage
    Friend WithEvents GroupBox_Distortion As GroupBox
    Friend WithEvents Label62 As Label
    Friend WithEvents Label61 As Label
    Friend WithEvents Label60 As Label
    Friend WithEvents Label59 As Label
    Friend WithEvents Label58 As Label
    Friend WithEvents Label57 As Label
    Friend WithEvents Label56 As Label
    Friend WithEvents Label55 As Label
    Friend WithEvents LinkLabel_PsvrDistReset As LinkLabel
    Friend WithEvents NumericUpDown_PsvrVFov As NumericUpDown
    Friend WithEvents UcNumericUpDownBig14 As UCNumericUpDownBig
    Friend WithEvents Label53 As Label
    Friend WithEvents NumericUpDown_PsvrHFov As NumericUpDown
    Friend WithEvents UcNumericUpDownBig13 As UCNumericUpDownBig
    Friend WithEvents Label52 As Label
    Friend WithEvents NumericUpDown_PsvrDistBlueOffset As NumericUpDown
    Friend WithEvents UcNumericUpDownBig16 As UCNumericUpDownBig
    Friend WithEvents Label51 As Label
    Friend WithEvents NumericUpDown_PsvrDistGreenOffset As NumericUpDown
    Friend WithEvents UcNumericUpDownBig15 As UCNumericUpDownBig
    Friend WithEvents Label50 As Label
    Friend WithEvents NumericUpDown_PsvrDistRedOffset As NumericUpDown
    Friend WithEvents UcNumericUpDownBig11 As UCNumericUpDownBig
    Friend WithEvents Label49 As Label
    Friend WithEvents NumericUpDown_PsvrDistScale As NumericUpDown
    Friend WithEvents UcNumericUpDownBig12 As UCNumericUpDownBig
    Friend WithEvents Label48 As Label
    Friend WithEvents NumericUpDown_PsvrDistK1 As NumericUpDown
    Friend WithEvents UcNumericUpDownBig10 As UCNumericUpDownBig
    Friend WithEvents Label47 As Label
    Friend WithEvents NumericUpDown_PsvrDistK0 As NumericUpDown
    Friend WithEvents UcNumericUpDownBig9 As UCNumericUpDownBig
    Friend WithEvents Label46 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Label63 As Label
    Friend WithEvents Label54 As Label
    Friend WithEvents CheckBox_ShowDistSettings As CheckBox
    Friend WithEvents NumericUpDown_PsvrIPD As NumericUpDown
    Friend WithEvents UcNumericUpDownBig8 As UCNumericUpDownBig
    Friend WithEvents Label45 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox_PsvrRenderResolution As ComboBox
    Friend WithEvents TabPage_SettingsPSmove As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LinkLabel_HmdRecenterFromOverride As LinkLabel
    Friend WithEvents LinkLabel_PlayCalibShowSettings2 As LinkLabel
    Friend WithEvents Label_ScrollFocus As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents CheckBox_PlayCalibEnabled As CheckBox
    Friend WithEvents NumericUpDown_RecenterButtonTime As NumericUpDown
    Friend WithEvents UcNumericUpDownBig7 As UCNumericUpDownBig
    Friend WithEvents Label13 As Label
    Friend WithEvents Button_ResetRecenter As Button
    Friend WithEvents Label20 As Label
    Friend WithEvents ComboBox_HmdRecenterFromDevice As ComboBox
    Friend WithEvents Label18 As Label
    Friend WithEvents ComboBox_HmdRecenterMethod As ComboBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents CheckBox_HmdRecenterEnabled As CheckBox
    Friend WithEvents ComboBox_RecenterFromDevice As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents ComboBox_RecenterMethod As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents CheckBox_ControllerRecenterEnabled As CheckBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label_TouchpadTouchAreaDeg As Label
    Friend WithEvents NumericUpDown_JoystickArea As NumericUpDown
    Friend WithEvents UcNumericUpDownBig5 As UCNumericUpDownBig
    Friend WithEvents Label23 As Label
    Friend WithEvents NumericUpDown_HtcTouchpadClickDeadzone As NumericUpDown
    Friend WithEvents UcNumericUpDownBig6 As UCNumericUpDownBig
    Friend WithEvents Label22 As Label
    Friend WithEvents LinkLabel_TouchpadShortcutHelp As LinkLabel
    Friend WithEvents ComboBox_JoystickMethod As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents CheckBox_HtcTouchpadShortcutClick As CheckBox
    Friend WithEvents CheckBox_HtcTouchpadShortcuts As CheckBox
    Friend WithEvents ComboBox_HtcGrabButtonMethod As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ComboBox_HtcTouchpadClickMethod As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TabPage_SettingsPlayspace As TabPage
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents CheckBox_PlayCalibAutoscale As CheckBox
    Friend WithEvents Label17 As Label
    Friend WithEvents NumericUpDown_PlayCalibSideOffset As NumericUpDown
    Friend WithEvents UcNumericUpDownBig3 As UCNumericUpDownBig
    Friend WithEvents Label26 As Label
    Friend WithEvents ComboBox_PlayCalibForwardMethod As ComboBox
    Friend WithEvents Button_PlayCalibReset As Button
    Friend WithEvents Label40 As Label
    Friend WithEvents NumericUpDown_PlayCalibHeightOffset As NumericUpDown
    Friend WithEvents UcNumericUpDownBig2 As UCNumericUpDownBig
    Friend WithEvents Label29 As Label
    Friend WithEvents NumericUpDown_PlayCalibForwardOffset As NumericUpDown
    Friend WithEvents UcNumericUpDownBig1 As UCNumericUpDownBig
    Friend WithEvents TabPage_SettingsOther As TabPage
    Friend WithEvents NumericUpDown_OscMaxThreadFps As NumericUpDown
    Friend WithEvents UcNumericUpDownBig18 As UCNumericUpDownBig
    Friend WithEvents Label65 As Label
    Friend WithEvents CheckBox_OptimizePackets As CheckBox
    Friend WithEvents NumericUpDown_OscThreadSleep As NumericUpDown
    Friend WithEvents UcNumericUpDownBig4 As UCNumericUpDownBig
    Friend WithEvents Label21 As Label
    Friend WithEvents CheckBox_EnableHeptics As CheckBox
    Friend WithEvents CheckBox_DisableBasestations As CheckBox
    Friend WithEvents Button_SaveControllerSettings As Button
    Friend WithEvents ToolTip_Info As ToolTip
    Friend WithEvents ToolTip_Default As ToolTip
    Friend WithEvents CheckBox_RenderFix As CheckBox
    Friend WithEvents TextBox_OscRemoteIP As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents LinkLabel_OscIpChange As LinkLabel
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents ComboBox_OculusButtonLayout As ComboBox
    Friend WithEvents CheckBox_OculusGripToggle As CheckBox
    Friend WithEvents CheckBox_EnableVelocityHmd As CheckBox
    Friend WithEvents CheckBox_HybridGripToggle As CheckBox
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents CheckBox_EnableVelocityTrackers As CheckBox
    Friend WithEvents CheckBox_EnableVelocityControllers As CheckBox
    Friend WithEvents CheckBox_EnableManualVelocity As CheckBox
End Class
