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
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel_VMTTrackers = New System.Windows.Forms.Panel()
        Me.Button_VMTControllers = New System.Windows.Forms.Button()
        Me.Button_AddVMTController = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBox_JoystickShortcutClick = New System.Windows.Forms.CheckBox()
        Me.CheckBox_JoystickShortcuts = New System.Windows.Forms.CheckBox()
        Me.LinkLabel_JoystickShortcutsInfo = New System.Windows.Forms.LinkLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ComboBox_GrabButtonMethod = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox_TouchpadClickMethod = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CheckBox_EnableHeptics = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBox_DisableBasestations = New System.Windows.Forms.CheckBox()
        Me.Button_SaveControllerSettings = New System.Windows.Forms.Button()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Panel_SteamVRRestart = New System.Windows.Forms.Panel()
        Me.LinkLabel_SteamVRRestartOff = New System.Windows.Forms.LinkLabel()
        Me.PictureBox3 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button_Refresh = New System.Windows.Forms.Button()
        Me.Button_Remove = New System.Windows.Forms.Button()
        Me.Button_Add = New System.Windows.Forms.Button()
        Me.PictureBox2 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ListView_Overrides = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip_Autostart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ContextMenuStrip_SteamVRDriver = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_DriverRegister = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_DriverUnregister = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button_InstallVmtDriver = New System.Windows.Forms.Button()
        Me.Button_PauseOscServer = New System.Windows.Forms.Button()
        Me.Button_StartOscServer = New System.Windows.Forms.Button()
        Me.PictureBox1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.Panel_SteamVRRestart.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip_SteamVRDriver.SuspendLayout()
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
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(16, 107)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(16, 3, 16, 16)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(768, 477)
        Me.TabControl1.TabIndex = 22
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel_VMTTrackers)
        Me.TabPage1.Controls.Add(Me.Button_VMTControllers)
        Me.TabPage1.Controls.Add(Me.Button_AddVMTController)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(760, 451)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Trackers"
        Me.TabPage1.UseVisualStyleBackColor = True
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
        Me.Panel_VMTTrackers.Size = New System.Drawing.Size(722, 374)
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
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TabControl2)
        Me.TabPage2.Controls.Add(Me.Button_SaveControllerSettings)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(760, 451)
        Me.TabPage2.TabIndex = 3
        Me.TabPage2.Text = "Settings"
        Me.TabPage2.UseVisualStyleBackColor = True
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
        Me.TabControl2.Size = New System.Drawing.Size(748, 387)
        Me.TabControl2.TabIndex = 47
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.GroupBox1)
        Me.TabPage4.Controls.Add(Me.GroupBox2)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(740, 361)
        Me.TabPage4.TabIndex = 0
        Me.TabPage4.Text = "PSMove Controller"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.CheckBox_JoystickShortcutClick)
        Me.GroupBox1.Controls.Add(Me.CheckBox_JoystickShortcuts)
        Me.GroupBox1.Controls.Add(Me.LinkLabel_JoystickShortcutsInfo)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(728, 76)
        Me.GroupBox1.TabIndex = 44
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "General"
        '
        'CheckBox_JoystickShortcutClick
        '
        Me.CheckBox_JoystickShortcutClick.AutoSize = True
        Me.CheckBox_JoystickShortcutClick.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_JoystickShortcutClick.Location = New System.Drawing.Point(51, 44)
        Me.CheckBox_JoystickShortcutClick.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.CheckBox_JoystickShortcutClick.Name = "CheckBox_JoystickShortcutClick"
        Me.CheckBox_JoystickShortcutClick.Size = New System.Drawing.Size(332, 18)
        Me.CheckBox_JoystickShortcutClick.TabIndex = 44
        Me.CheckBox_JoystickShortcutClick.Text = "Click touchpad when using shortcuts (HTC Vive Emulation)"
        Me.CheckBox_JoystickShortcutClick.UseVisualStyleBackColor = True
        '
        'CheckBox_JoystickShortcuts
        '
        Me.CheckBox_JoystickShortcuts.AutoSize = True
        Me.CheckBox_JoystickShortcuts.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_JoystickShortcuts.Location = New System.Drawing.Point(19, 21)
        Me.CheckBox_JoystickShortcuts.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_JoystickShortcuts.Name = "CheckBox_JoystickShortcuts"
        Me.CheckBox_JoystickShortcuts.Size = New System.Drawing.Size(214, 18)
        Me.CheckBox_JoystickShortcuts.TabIndex = 42
        Me.CheckBox_JoystickShortcuts.Text = "Enable joystick emulation shortcuts"
        Me.CheckBox_JoystickShortcuts.UseVisualStyleBackColor = True
        '
        'LinkLabel_JoystickShortcutsInfo
        '
        Me.LinkLabel_JoystickShortcutsInfo.AutoSize = True
        Me.LinkLabel_JoystickShortcutsInfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel_JoystickShortcutsInfo.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_JoystickShortcutsInfo.Location = New System.Drawing.Point(233, 22)
        Me.LinkLabel_JoystickShortcutsInfo.Name = "LinkLabel_JoystickShortcutsInfo"
        Me.LinkLabel_JoystickShortcutsInfo.Size = New System.Drawing.Size(26, 13)
        Me.LinkLabel_JoystickShortcutsInfo.TabIndex = 43
        Me.LinkLabel_JoystickShortcutsInfo.TabStop = True
        Me.LinkLabel_JoystickShortcutsInfo.Text = "( ? )"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ComboBox_GrabButtonMethod)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.ComboBox_TouchpadClickMethod)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 88)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(728, 81)
        Me.GroupBox2.TabIndex = 46
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "HTC Vive Emulation Settings"
        '
        'ComboBox_GrabButtonMethod
        '
        Me.ComboBox_GrabButtonMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_GrabButtonMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_GrabButtonMethod.FormattingEnabled = True
        Me.ComboBox_GrabButtonMethod.Location = New System.Drawing.Point(154, 45)
        Me.ComboBox_GrabButtonMethod.Name = "ComboBox_GrabButtonMethod"
        Me.ComboBox_GrabButtonMethod.Size = New System.Drawing.Size(449, 21)
        Me.ComboBox_GrabButtonMethod.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 48)
        Me.Label5.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(117, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Grab button method:"
        '
        'ComboBox_TouchpadClickMethod
        '
        Me.ComboBox_TouchpadClickMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_TouchpadClickMethod.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_TouchpadClickMethod.FormattingEnabled = True
        Me.ComboBox_TouchpadClickMethod.Location = New System.Drawing.Point(154, 18)
        Me.ComboBox_TouchpadClickMethod.Name = "ComboBox_TouchpadClickMethod"
        Me.ComboBox_TouchpadClickMethod.Size = New System.Drawing.Size(449, 21)
        Me.ComboBox_TouchpadClickMethod.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 21)
        Me.Label4.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(129, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Touchpad click method:"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Label7)
        Me.TabPage5.Controls.Add(Me.CheckBox_EnableHeptics)
        Me.TabPage5.Controls.Add(Me.Label6)
        Me.TabPage5.Controls.Add(Me.CheckBox_DisableBasestations)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(740, 361)
        Me.TabPage5.TabIndex = 1
        Me.TabPage5.Text = "Other"
        Me.TabPage5.UseVisualStyleBackColor = True
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
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label6.Location = New System.Drawing.Point(16, 37)
        Me.Label6.Margin = New System.Windows.Forms.Padding(16, 0, 3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(3)
        Me.Label6.Size = New System.Drawing.Size(592, 45)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = resources.GetString("Label6.Text")
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
        'Button_SaveControllerSettings
        '
        Me.Button_SaveControllerSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SaveControllerSettings.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16761_16x16_32
        Me.Button_SaveControllerSettings.Location = New System.Drawing.Point(624, 412)
        Me.Button_SaveControllerSettings.Margin = New System.Windows.Forms.Padding(16)
        Me.Button_SaveControllerSettings.Name = "Button_SaveControllerSettings"
        Me.Button_SaveControllerSettings.Size = New System.Drawing.Size(120, 23)
        Me.Button_SaveControllerSettings.TabIndex = 45
        Me.Button_SaveControllerSettings.Text = "Save settings"
        Me.Button_SaveControllerSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_SaveControllerSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_SaveControllerSettings.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Panel_SteamVRRestart)
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Controls.Add(Me.Button_Refresh)
        Me.TabPage3.Controls.Add(Me.Button_Remove)
        Me.TabPage3.Controls.Add(Me.Button_Add)
        Me.TabPage3.Controls.Add(Me.PictureBox2)
        Me.TabPage3.Controls.Add(Me.ListView_Overrides)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(760, 451)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "SteamVR Tracker Overrides"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Panel_SteamVRRestart
        '
        Me.Panel_SteamVRRestart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_SteamVRRestart.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel_SteamVRRestart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_SteamVRRestart.Controls.Add(Me.LinkLabel_SteamVRRestartOff)
        Me.Panel_SteamVRRestart.Controls.Add(Me.PictureBox3)
        Me.Panel_SteamVRRestart.Controls.Add(Me.Label3)
        Me.Panel_SteamVRRestart.Location = New System.Drawing.Point(64, 301)
        Me.Panel_SteamVRRestart.Name = "Panel_SteamVRRestart"
        Me.Panel_SteamVRRestart.Size = New System.Drawing.Size(632, 42)
        Me.Panel_SteamVRRestart.TabIndex = 28
        '
        'LinkLabel_SteamVRRestartOff
        '
        Me.LinkLabel_SteamVRRestartOff.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel_SteamVRRestartOff.AutoSize = True
        Me.LinkLabel_SteamVRRestartOff.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_SteamVRRestartOff.Location = New System.Drawing.Point(569, 13)
        Me.LinkLabel_SteamVRRestartOff.Margin = New System.Windows.Forms.Padding(16)
        Me.LinkLabel_SteamVRRestartOff.Name = "LinkLabel_SteamVRRestartOff"
        Me.LinkLabel_SteamVRRestartOff.Size = New System.Drawing.Size(45, 13)
        Me.LinkLabel_SteamVRRestartOff.TabIndex = 28
        Me.LinkLabel_SteamVRRestartOff.TabStop = True
        Me.LinkLabel_SteamVRRestartOff.Text = "Dismiss"
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.PictureBox3.Location = New System.Drawing.Point(16, 13)
        Me.PictureBox3.m_HighQuality = False
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox3.TabIndex = 26
        Me.PictureBox3.TabStop = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Location = New System.Drawing.Point(38, -1)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 16, 16, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(512, 41)
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
        Me.Button_Refresh.Location = New System.Drawing.Point(64, 272)
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
        Me.Button_Remove.Location = New System.Drawing.Point(472, 272)
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
        Me.Button_Add.Location = New System.Drawing.Point(587, 272)
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
        Me.ListView_Overrides.Location = New System.Drawing.Point(64, 81)
        Me.ListView_Overrides.Margin = New System.Windows.Forms.Padding(64, 32, 64, 3)
        Me.ListView_Overrides.Name = "ListView_Overrides"
        Me.ListView_Overrides.Size = New System.Drawing.Size(632, 185)
        Me.ListView_Overrides.TabIndex = 0
        Me.ListView_Overrides.UseCompatibleStateImageBehavior = False
        Me.ListView_Overrides.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Tracker"
        Me.ColumnHeader1.Width = 139
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Override"
        Me.ColumnHeader2.Width = 158
        '
        'ContextMenuStrip_Autostart
        '
        Me.ContextMenuStrip_Autostart.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip_Autostart.Size = New System.Drawing.Size(61, 4)
        '
        'ContextMenuStrip_SteamVRDriver
        '
        Me.ContextMenuStrip_SteamVRDriver.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_DriverRegister, Me.ToolStripMenuItem_DriverUnregister})
        Me.ContextMenuStrip_SteamVRDriver.Name = "ContextMenuStrip_SteamVRDriver"
        Me.ContextMenuStrip_SteamVRDriver.Size = New System.Drawing.Size(162, 48)
        '
        'ToolStripMenuItem_DriverRegister
        '
        Me.ToolStripMenuItem_DriverRegister.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5303_16x16_32
        Me.ToolStripMenuItem_DriverRegister.Name = "ToolStripMenuItem_DriverRegister"
        Me.ToolStripMenuItem_DriverRegister.Size = New System.Drawing.Size(161, 22)
        Me.ToolStripMenuItem_DriverRegister.Text = "Register driver"
        '
        'ToolStripMenuItem_DriverUnregister
        '
        Me.ToolStripMenuItem_DriverUnregister.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.ToolStripMenuItem_DriverUnregister.Name = "ToolStripMenuItem_DriverUnregister"
        Me.ToolStripMenuItem_DriverUnregister.Size = New System.Drawing.Size(161, 22)
        Me.ToolStripMenuItem_DriverUnregister.Text = "Unregister driver"
        '
        'Button_InstallVmtDriver
        '
        Me.Button_InstallVmtDriver.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.Button_InstallVmtDriver.Location = New System.Drawing.Point(16, 78)
        Me.Button_InstallVmtDriver.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.Button_InstallVmtDriver.Name = "Button_InstallVmtDriver"
        Me.Button_InstallVmtDriver.Size = New System.Drawing.Size(139, 23)
        Me.Button_InstallVmtDriver.TabIndex = 26
        Me.Button_InstallVmtDriver.Text = "SteamVR driver..."
        Me.Button_InstallVmtDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_InstallVmtDriver.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_InstallVmtDriver.UseVisualStyleBackColor = True
        '
        'Button_PauseOscServer
        '
        Me.Button_PauseOscServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_PauseOscServer.Enabled = False
        Me.Button_PauseOscServer.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5315_16x16_32
        Me.Button_PauseOscServer.Location = New System.Drawing.Point(500, 78)
        Me.Button_PauseOscServer.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.Button_PauseOscServer.Name = "Button_PauseOscServer"
        Me.Button_PauseOscServer.Size = New System.Drawing.Size(139, 23)
        Me.Button_PauseOscServer.TabIndex = 25
        Me.Button_PauseOscServer.Text = "Pause OSC Server"
        Me.Button_PauseOscServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_PauseOscServer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_PauseOscServer.UseVisualStyleBackColor = True
        '
        'Button_StartOscServer
        '
        Me.Button_StartOscServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_StartOscServer.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.Button_StartOscServer.Location = New System.Drawing.Point(645, 78)
        Me.Button_StartOscServer.Margin = New System.Windows.Forms.Padding(3, 16, 16, 3)
        Me.Button_StartOscServer.Name = "Button_StartOscServer"
        Me.Button_StartOscServer.Size = New System.Drawing.Size(139, 23)
        Me.Button_StartOscServer.TabIndex = 23
        Me.Button_StartOscServer.Text = "Start OSC Server"
        Me.Button_StartOscServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_StartOscServer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_StartOscServer.UseVisualStyleBackColor = True
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
        Me.Controls.Add(Me.Button_InstallVmtDriver)
        Me.Controls.Add(Me.Button_PauseOscServer)
        Me.Controls.Add(Me.Button_StartOscServer)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.LinkLabel_ReadMore)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVirtualMotionTracker"
        Me.Size = New System.Drawing.Size(800, 600)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.Panel_SteamVRRestart.ResumeLayout(False)
        Me.Panel_SteamVRRestart.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip_SteamVRDriver.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LinkLabel_ReadMore As LinkLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As ClassPictureBoxQuality
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Panel_VMTTrackers As Panel
    Friend WithEvents Button_VMTControllers As Button
    Friend WithEvents Button_AddVMTController As Button
    Friend WithEvents ContextMenuStrip_Autostart As ContextMenuStrip
    Friend WithEvents Button_StartOscServer As Button
    Friend WithEvents ListView_Overrides As ListView
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
    Friend WithEvents Button_PauseOscServer As Button
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents CheckBox_JoystickShortcuts As CheckBox
    Friend WithEvents LinkLabel_JoystickShortcutsInfo As LinkLabel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CheckBox_JoystickShortcutClick As CheckBox
    Friend WithEvents Button_SaveControllerSettings As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ComboBox_TouchpadClickMethod As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ComboBox_GrabButtonMethod As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Button_InstallVmtDriver As Button
    Friend WithEvents ContextMenuStrip_SteamVRDriver As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_DriverRegister As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_DriverUnregister As ToolStripMenuItem
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents CheckBox_DisableBasestations As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CheckBox_EnableHeptics As CheckBox
    Friend WithEvents Label7 As Label
End Class
