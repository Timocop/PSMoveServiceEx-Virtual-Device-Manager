<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCVirtualMotionTrackerItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCVirtualMotionTrackerItem))
        Me.ComboBox_DeviceID = New System.Windows.Forms.ComboBox()
        Me.Label_DeviceID = New System.Windows.Forms.Label()
        Me.TimerFPS = New System.Windows.Forms.Timer(Me.components)
        Me.Button_SaveSettings = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox_VMTTrackerID = New System.Windows.Forms.ComboBox()
        Me.TextBox_Fps = New System.Windows.Forms.TextBox()
        Me.TextBox_Gyro = New System.Windows.Forms.TextBox()
        Me.TextBox_Pos = New System.Windows.Forms.TextBox()
        Me.TimerPose = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_Status = New System.Windows.Forms.Panel()
        Me.PictureBox_StatusImage = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Label_StatusMessage = New System.Windows.Forms.Label()
        Me.Label_StatusTitle = New System.Windows.Forms.Label()
        Me.Timer_Status = New System.Windows.Forms.Timer(Me.components)
        Me.ImageList_Status = New System.Windows.Forms.ImageList(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox_VMTTrackerRole = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ComboBox_HmdViewPointOffset = New System.Windows.Forms.ComboBox()
        Me.ComboBox_SteamTrackerRole = New System.Windows.Forms.ComboBox()
        Me.Label_TrackerName = New System.Windows.Forms.Label()
        Me.Button_TrackerRecenter = New System.Windows.Forms.Button()
        Me.ContextMenuStrip_TrackerRecenter = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_TrackerRecenterNow = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_TrackerRecenterDelayed = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem_TrackerRecenterClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer_RecenterTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.LinkLabel_HmdViewPointOffset = New System.Windows.Forms.LinkLabel()
        Me.Panel_Status.SuspendLayout()
        CType(Me.PictureBox_StatusImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip_TrackerRecenter.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBox_DeviceID
        '
        Me.ComboBox_DeviceID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_DeviceID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_DeviceID.FormattingEnabled = True
        Me.ComboBox_DeviceID.Location = New System.Drawing.Point(114, 6)
        Me.ComboBox_DeviceID.Name = "ComboBox_DeviceID"
        Me.ComboBox_DeviceID.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox_DeviceID.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.ComboBox_DeviceID, "The PSMoveServiceEx device id that will be used for this tracker.")
        '
        'Label_DeviceID
        '
        Me.Label_DeviceID.AutoSize = True
        Me.Label_DeviceID.Location = New System.Drawing.Point(6, 9)
        Me.Label_DeviceID.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.Label_DeviceID.Name = "Label_DeviceID"
        Me.Label_DeviceID.Size = New System.Drawing.Size(57, 13)
        Me.Label_DeviceID.TabIndex = 6
        Me.Label_DeviceID.Text = "Device ID:"
        '
        'TimerFPS
        '
        Me.TimerFPS.Enabled = True
        Me.TimerFPS.Interval = 1000
        '
        'Button_SaveSettings
        '
        Me.Button_SaveSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SaveSettings.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16761_16x16_32
        Me.Button_SaveSettings.Location = New System.Drawing.Point(481, 220)
        Me.Button_SaveSettings.Margin = New System.Windows.Forms.Padding(3, 16, 16, 16)
        Me.Button_SaveSettings.Name = "Button_SaveSettings"
        Me.Button_SaveSettings.Size = New System.Drawing.Size(120, 23)
        Me.Button_SaveSettings.TabIndex = 7
        Me.Button_SaveSettings.Text = "Save Settings"
        Me.Button_SaveSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_SaveSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_SaveSettings.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "VMT Tracker ID:"
        '
        'ComboBox_VMTTrackerID
        '
        Me.ComboBox_VMTTrackerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_VMTTrackerID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_VMTTrackerID.FormattingEnabled = True
        Me.ComboBox_VMTTrackerID.Location = New System.Drawing.Point(114, 33)
        Me.ComboBox_VMTTrackerID.Name = "ComboBox_VMTTrackerID"
        Me.ComboBox_VMTTrackerID.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox_VMTTrackerID.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.ComboBox_VMTTrackerID, resources.GetString("ComboBox_VMTTrackerID.ToolTip"))
        '
        'TextBox_Fps
        '
        Me.TextBox_Fps.BackColor = System.Drawing.Color.White
        Me.TextBox_Fps.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Fps.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Fps.Location = New System.Drawing.Point(16, 225)
        Me.TextBox_Fps.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.TextBox_Fps.Name = "TextBox_Fps"
        Me.TextBox_Fps.ReadOnly = True
        Me.TextBox_Fps.Size = New System.Drawing.Size(268, 15)
        Me.TextBox_Fps.TabIndex = 32
        Me.TextBox_Fps.Text = "I/O FPS: 0"
        '
        'TextBox_Gyro
        '
        Me.TextBox_Gyro.BackColor = System.Drawing.Color.White
        Me.TextBox_Gyro.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Gyro.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Gyro.HideSelection = False
        Me.TextBox_Gyro.Location = New System.Drawing.Point(134, 180)
        Me.TextBox_Gyro.Margin = New System.Windows.Forms.Padding(3, 16, 3, 0)
        Me.TextBox_Gyro.Multiline = True
        Me.TextBox_Gyro.Name = "TextBox_Gyro"
        Me.TextBox_Gyro.ReadOnly = True
        Me.TextBox_Gyro.Size = New System.Drawing.Size(115, 42)
        Me.TextBox_Gyro.TabIndex = 35
        Me.TextBox_Gyro.Text = "Ang X: 0" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ang Y: 0" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ang Z: 0"
        Me.TextBox_Gyro.WordWrap = False
        '
        'TextBox_Pos
        '
        Me.TextBox_Pos.BackColor = System.Drawing.Color.White
        Me.TextBox_Pos.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Pos.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Pos.HideSelection = False
        Me.TextBox_Pos.Location = New System.Drawing.Point(13, 180)
        Me.TextBox_Pos.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.TextBox_Pos.Multiline = True
        Me.TextBox_Pos.Name = "TextBox_Pos"
        Me.TextBox_Pos.ReadOnly = True
        Me.TextBox_Pos.Size = New System.Drawing.Size(115, 42)
        Me.TextBox_Pos.TabIndex = 34
        Me.TextBox_Pos.Text = "Pos X: 0" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Pos Y: 0" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Pos Z: 0"
        Me.TextBox_Pos.WordWrap = False
        '
        'TimerPose
        '
        Me.TimerPose.Enabled = True
        Me.TimerPose.Interval = 500
        '
        'Panel_Status
        '
        Me.Panel_Status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Status.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Panel_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Status.Controls.Add(Me.PictureBox_StatusImage)
        Me.Panel_Status.Controls.Add(Me.Label_StatusMessage)
        Me.Panel_Status.Controls.Add(Me.Label_StatusTitle)
        Me.Panel_Status.Location = New System.Drawing.Point(16, 252)
        Me.Panel_Status.Margin = New System.Windows.Forms.Padding(3, 6, 3, 16)
        Me.Panel_Status.Name = "Panel_Status"
        Me.Panel_Status.Size = New System.Drawing.Size(585, 75)
        Me.Panel_Status.TabIndex = 36
        '
        'PictureBox_StatusImage
        '
        Me.PictureBox_StatusImage.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1608_16x16_32
        Me.PictureBox_StatusImage.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox_StatusImage.m_HighQuality = False
        Me.PictureBox_StatusImage.Name = "PictureBox_StatusImage"
        Me.PictureBox_StatusImage.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox_StatusImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox_StatusImage.TabIndex = 2
        Me.PictureBox_StatusImage.TabStop = False
        '
        'Label_StatusMessage
        '
        Me.Label_StatusMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_StatusMessage.Location = New System.Drawing.Point(25, 19)
        Me.Label_StatusMessage.Name = "Label_StatusMessage"
        Me.Label_StatusMessage.Size = New System.Drawing.Size(555, 54)
        Me.Label_StatusMessage.TabIndex = 1
        Me.Label_StatusMessage.Text = "Message"
        '
        'Label_StatusTitle
        '
        Me.Label_StatusTitle.AutoSize = True
        Me.Label_StatusTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_StatusTitle.Location = New System.Drawing.Point(25, 3)
        Me.Label_StatusTitle.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_StatusTitle.Name = "Label_StatusTitle"
        Me.Label_StatusTitle.Size = New System.Drawing.Size(29, 13)
        Me.Label_StatusTitle.TabIndex = 0
        Me.Label_StatusTitle.Text = "Title"
        '
        'Timer_Status
        '
        Me.Timer_Status.Enabled = True
        Me.Timer_Status.Interval = 2500
        '
        'ImageList_Status
        '
        Me.ImageList_Status.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ImageList_Status.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList_Status.TransparentColor = System.Drawing.Color.Transparent
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "VMT Tracker role:"
        '
        'ComboBox_VMTTrackerRole
        '
        Me.ComboBox_VMTTrackerRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_VMTTrackerRole.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_VMTTrackerRole.FormattingEnabled = True
        Me.ComboBox_VMTTrackerRole.Location = New System.Drawing.Point(114, 60)
        Me.ComboBox_VMTTrackerRole.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.ComboBox_VMTTrackerRole.Name = "ComboBox_VMTTrackerRole"
        Me.ComboBox_VMTTrackerRole.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox_VMTTrackerRole.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.ComboBox_VMTTrackerRole, resources.GetString("ComboBox_VMTTrackerRole.ToolTip"))
        '
        'ToolTip1
        '
        Me.ToolTip1.AutomaticDelay = 100
        Me.ToolTip1.AutoPopDelay = 30000
        Me.ToolTip1.InitialDelay = 100
        Me.ToolTip1.ReshowDelay = 20
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = "Information"
        '
        'ComboBox_HmdViewPointOffset
        '
        Me.ComboBox_HmdViewPointOffset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_HmdViewPointOffset.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_HmdViewPointOffset.FormattingEnabled = True
        Me.ComboBox_HmdViewPointOffset.Location = New System.Drawing.Point(157, 6)
        Me.ComboBox_HmdViewPointOffset.Name = "ComboBox_HmdViewPointOffset"
        Me.ComboBox_HmdViewPointOffset.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox_HmdViewPointOffset.TabIndex = 45
        Me.ToolTip1.SetToolTip(Me.ComboBox_HmdViewPointOffset, "When enabled, this device uses the head-mounted display view point offsets from t" &
        "he head-mounted display settings." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This is only useful if you are using this tra" &
        "cker as a head-mounted display tracker.")
        '
        'ComboBox_SteamTrackerRole
        '
        Me.ComboBox_SteamTrackerRole.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_SteamTrackerRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_SteamTrackerRole.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_SteamTrackerRole.FormattingEnabled = True
        Me.ComboBox_SteamTrackerRole.Location = New System.Drawing.Point(421, 60)
        Me.ComboBox_SteamTrackerRole.Name = "ComboBox_SteamTrackerRole"
        Me.ComboBox_SteamTrackerRole.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox_SteamTrackerRole.TabIndex = 42
        Me.ToolTip1.SetToolTip(Me.ComboBox_SteamTrackerRole, "Prefered SteamVR tracker roles." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Most of the time changing this setting is not ne" &
        "eded. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "But it can solve some controller issues or controllers not being detecte" &
        "d properly.")
        '
        'Label_TrackerName
        '
        Me.Label_TrackerName.AutoSize = True
        Me.Label_TrackerName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_TrackerName.Location = New System.Drawing.Point(16, 16)
        Me.Label_TrackerName.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Label_TrackerName.Name = "Label_TrackerName"
        Me.Label_TrackerName.Size = New System.Drawing.Size(118, 13)
        Me.Label_TrackerName.TabIndex = 42
        Me.Label_TrackerName.Text = "Tracker Name: Invalid"
        '
        'Button_TrackerRecenter
        '
        Me.Button_TrackerRecenter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_TrackerRecenter.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5345_16x16_32
        Me.Button_TrackerRecenter.Location = New System.Drawing.Point(355, 220)
        Me.Button_TrackerRecenter.Margin = New System.Windows.Forms.Padding(3, 16, 3, 16)
        Me.Button_TrackerRecenter.Name = "Button_TrackerRecenter"
        Me.Button_TrackerRecenter.Size = New System.Drawing.Size(120, 23)
        Me.Button_TrackerRecenter.TabIndex = 43
        Me.Button_TrackerRecenter.Text = "Recenter..."
        Me.Button_TrackerRecenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_TrackerRecenter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_TrackerRecenter.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip_TrackerRecenter
        '
        Me.ContextMenuStrip_TrackerRecenter.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_TrackerRecenterNow, Me.ToolStripMenuItem_TrackerRecenterDelayed, Me.ToolStripSeparator1, Me.ToolStripMenuItem_TrackerRecenterClear})
        Me.ContextMenuStrip_TrackerRecenter.Name = "ContextMenuStrip_TrackerRecenter"
        Me.ContextMenuStrip_TrackerRecenter.Size = New System.Drawing.Size(180, 76)
        '
        'ToolStripMenuItem_TrackerRecenterNow
        '
        Me.ToolStripMenuItem_TrackerRecenterNow.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5345_16x16_32
        Me.ToolStripMenuItem_TrackerRecenterNow.Name = "ToolStripMenuItem_TrackerRecenterNow"
        Me.ToolStripMenuItem_TrackerRecenterNow.Size = New System.Drawing.Size(179, 22)
        Me.ToolStripMenuItem_TrackerRecenterNow.Text = "Now"
        '
        'ToolStripMenuItem_TrackerRecenterDelayed
        '
        Me.ToolStripMenuItem_TrackerRecenterDelayed.Name = "ToolStripMenuItem_TrackerRecenterDelayed"
        Me.ToolStripMenuItem_TrackerRecenterDelayed.Size = New System.Drawing.Size(179, 22)
        Me.ToolStripMenuItem_TrackerRecenterDelayed.Text = "Delayed (5 seconds)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(176, 6)
        '
        'ToolStripMenuItem_TrackerRecenterClear
        '
        Me.ToolStripMenuItem_TrackerRecenterClear.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.ToolStripMenuItem_TrackerRecenterClear.Name = "ToolStripMenuItem_TrackerRecenterClear"
        Me.ToolStripMenuItem_TrackerRecenterClear.Size = New System.Drawing.Size(179, 22)
        Me.ToolStripMenuItem_TrackerRecenterClear.Text = "Reset"
        '
        'Timer_RecenterTimer
        '
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Use HMD viewpoint offset:"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(16, 48)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(16)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(585, 117)
        Me.TabControl1.TabIndex = 46
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.ComboBox_SteamTrackerRole)
        Me.TabPage1.Controls.Add(Me.Label_DeviceID)
        Me.TabPage1.Controls.Add(Me.ComboBox_DeviceID)
        Me.TabPage1.Controls.Add(Me.ComboBox_VMTTrackerID)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.ComboBox_VMTTrackerRole)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(577, 91)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Tracker Properties"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(298, 63)
        Me.Label4.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(117, 13)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "SteamVR Tracker role:"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.White
        Me.TabPage2.Controls.Add(Me.LinkLabel_HmdViewPointOffset)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.ComboBox_HmdViewPointOffset)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(577, 91)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Advanced Settings"
        '
        'LinkLabel_HmdViewPointOffset
        '
        Me.LinkLabel_HmdViewPointOffset.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_HmdViewPointOffset.AutoSize = True
        Me.LinkLabel_HmdViewPointOffset.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_HmdViewPointOffset.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_HmdViewPointOffset.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_HmdViewPointOffset.Location = New System.Drawing.Point(313, 9)
        Me.LinkLabel_HmdViewPointOffset.Name = "LinkLabel_HmdViewPointOffset"
        Me.LinkLabel_HmdViewPointOffset.Size = New System.Drawing.Size(75, 13)
        Me.LinkLabel_HmdViewPointOffset.TabIndex = 73
        Me.LinkLabel_HmdViewPointOffset.TabStop = True
        Me.LinkLabel_HmdViewPointOffset.Text = "Go to setting"
        Me.LinkLabel_HmdViewPointOffset.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'UCVirtualMotionTrackerItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button_TrackerRecenter)
        Me.Controls.Add(Me.Label_TrackerName)
        Me.Controls.Add(Me.Panel_Status)
        Me.Controls.Add(Me.TextBox_Gyro)
        Me.Controls.Add(Me.TextBox_Pos)
        Me.Controls.Add(Me.TextBox_Fps)
        Me.Controls.Add(Me.Button_SaveSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVirtualMotionTrackerItem"
        Me.Size = New System.Drawing.Size(617, 343)
        Me.Panel_Status.ResumeLayout(False)
        Me.Panel_Status.PerformLayout()
        CType(Me.PictureBox_StatusImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip_TrackerRecenter.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox_DeviceID As ComboBox
    Friend WithEvents Label_DeviceID As Label
    Friend WithEvents TimerFPS As Timer
    Friend WithEvents Button_SaveSettings As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox_VMTTrackerID As ComboBox
    Friend WithEvents TextBox_Fps As TextBox
    Friend WithEvents TextBox_Gyro As TextBox
    Friend WithEvents TextBox_Pos As TextBox
    Friend WithEvents TimerPose As Timer
    Friend WithEvents Panel_Status As Panel
    Friend WithEvents PictureBox_StatusImage As ClassPictureBoxQuality
    Friend WithEvents Label_StatusMessage As Label
    Friend WithEvents Label_StatusTitle As Label
    Friend WithEvents Timer_Status As Timer
    Friend WithEvents ImageList_Status As ImageList
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox_VMTTrackerRole As ComboBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label_TrackerName As Label
    Friend WithEvents Button_TrackerRecenter As Button
    Friend WithEvents ContextMenuStrip_TrackerRecenter As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_TrackerRecenterNow As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_TrackerRecenterDelayed As ToolStripMenuItem
    Friend WithEvents Timer_RecenterTimer As Timer
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem_TrackerRecenterClear As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox_HmdViewPointOffset As ComboBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label4 As Label
    Friend WithEvents ComboBox_SteamTrackerRole As ComboBox
    Friend WithEvents LinkLabel_HmdViewPointOffset As LinkLabel
End Class
