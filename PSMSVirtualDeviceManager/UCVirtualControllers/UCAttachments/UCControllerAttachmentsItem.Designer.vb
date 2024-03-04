<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCControllerAttachmentsItem
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
        Me.ComboBox_ControllerID = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TimerFPS = New System.Windows.Forms.Timer(Me.components)
        Me.Button_SaveSettings = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox_ParentControllerID = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown_JointYawCorrection = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.NumericUpDown_JointOffsetZ = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumericUpDown_JointOffsetY = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NumericUpDown_JointOffsetX = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown_ControllerYawCorrection = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.NumericUpDown_ControllerOffsetZ = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.NumericUpDown_ControllerOffsetY = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumericUpDown_ControllerOffsetX = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox_Fps = New System.Windows.Forms.TextBox()
        Me.CheckBox_JointOnly = New System.Windows.Forms.CheckBox()
        Me.TextBox_TrackerName = New System.Windows.Forms.TextBox()
        Me.LinkLabel_EditName = New System.Windows.Forms.LinkLabel()
        Me.Timer_Status = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_Status = New System.Windows.Forms.Panel()
        Me.Label_StatusMessage = New System.Windows.Forms.Label()
        Me.Label_StatusTitle = New System.Windows.Forms.Label()
        Me.PictureBox1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.UcNumericUpDownBig4 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.UcNumericUpDownBig3 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.UcNumericUpDownBig2 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.UcNumericUpDownBig1 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.UcNumericUpDownBig8 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.UcNumericUpDownBig7 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.UcNumericUpDownBig6 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.UcNumericUpDownBig5 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumericUpDown_JointYawCorrection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_JointYawCorrection.SuspendLayout()
        CType(Me.NumericUpDown_JointOffsetZ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_JointOffsetZ.SuspendLayout()
        CType(Me.NumericUpDown_JointOffsetY, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_JointOffsetY.SuspendLayout()
        CType(Me.NumericUpDown_JointOffsetX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_JointOffsetX.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.NumericUpDown_ControllerYawCorrection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_ControllerYawCorrection.SuspendLayout()
        CType(Me.NumericUpDown_ControllerOffsetZ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_ControllerOffsetZ.SuspendLayout()
        CType(Me.NumericUpDown_ControllerOffsetY, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_ControllerOffsetY.SuspendLayout()
        CType(Me.NumericUpDown_ControllerOffsetX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_ControllerOffsetX.SuspendLayout()
        Me.Panel_Status.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBox_ControllerID
        '
        Me.ComboBox_ControllerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_ControllerID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_ControllerID.FormattingEnabled = True
        Me.ComboBox_ControllerID.Location = New System.Drawing.Point(134, 44)
        Me.ComboBox_ControllerID.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.ComboBox_ControllerID.Name = "ComboBox_ControllerID"
        Me.ComboBox_ControllerID.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox_ControllerID.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 47)
        Me.Label1.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Controller ID:"
        '
        'TimerFPS
        '
        Me.TimerFPS.Enabled = True
        Me.TimerFPS.Interval = 1000
        '
        'Button_SaveSettings
        '
        Me.Button_SaveSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SaveSettings.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16761_16x16_32
        Me.Button_SaveSettings.Location = New System.Drawing.Point(481, 325)
        Me.Button_SaveSettings.Margin = New System.Windows.Forms.Padding(16)
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
        Me.Label3.Location = New System.Drawing.Point(16, 76)
        Me.Label3.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Parent Controller ID:"
        '
        'ComboBox_ParentControllerID
        '
        Me.ComboBox_ParentControllerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_ParentControllerID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_ParentControllerID.FormattingEnabled = True
        Me.ComboBox_ParentControllerID.Location = New System.Drawing.Point(134, 71)
        Me.ComboBox_ParentControllerID.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.ComboBox_ParentControllerID.Name = "ComboBox_ParentControllerID"
        Me.ComboBox_ParentControllerID.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox_ParentControllerID.TabIndex = 17
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.NumericUpDown_JointYawCorrection)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown_JointOffsetZ)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown_JointOffsetY)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown_JointOffsetX)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 111)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(268, 147)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Joint Offset"
        '
        'NumericUpDown_JointYawCorrection
        '
        Me.NumericUpDown_JointYawCorrection.Controls.Add(Me.UcNumericUpDownBig8)
        Me.NumericUpDown_JointYawCorrection.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown_JointYawCorrection.Location = New System.Drawing.Point(116, 108)
        Me.NumericUpDown_JointYawCorrection.Margin = New System.Windows.Forms.Padding(8, 3, 8, 3)
        Me.NumericUpDown_JointYawCorrection.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_JointYawCorrection.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_JointYawCorrection.Name = "NumericUpDown_JointYawCorrection"
        Me.NumericUpDown_JointYawCorrection.Size = New System.Drawing.Size(141, 22)
        Me.NumericUpDown_JointYawCorrection.TabIndex = 31
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(19, 112)
        Me.Label9.Margin = New System.Windows.Forms.Padding(16, 8, 3, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(86, 13)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "Yaw Correction:"
        '
        'NumericUpDown_JointOffsetZ
        '
        Me.NumericUpDown_JointOffsetZ.Controls.Add(Me.UcNumericUpDownBig7)
        Me.NumericUpDown_JointOffsetZ.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown_JointOffsetZ.Location = New System.Drawing.Point(116, 80)
        Me.NumericUpDown_JointOffsetZ.Margin = New System.Windows.Forms.Padding(8, 3, 8, 3)
        Me.NumericUpDown_JointOffsetZ.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_JointOffsetZ.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_JointOffsetZ.Name = "NumericUpDown_JointOffsetZ"
        Me.NumericUpDown_JointOffsetZ.Size = New System.Drawing.Size(141, 22)
        Me.NumericUpDown_JointOffsetZ.TabIndex = 27
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 84)
        Me.Label5.Margin = New System.Windows.Forms.Padding(16, 8, 3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 13)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "Z:"
        '
        'NumericUpDown_JointOffsetY
        '
        Me.NumericUpDown_JointOffsetY.Controls.Add(Me.UcNumericUpDownBig6)
        Me.NumericUpDown_JointOffsetY.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown_JointOffsetY.Location = New System.Drawing.Point(116, 52)
        Me.NumericUpDown_JointOffsetY.Margin = New System.Windows.Forms.Padding(8, 3, 8, 3)
        Me.NumericUpDown_JointOffsetY.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_JointOffsetY.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_JointOffsetY.Name = "NumericUpDown_JointOffsetY"
        Me.NumericUpDown_JointOffsetY.Size = New System.Drawing.Size(141, 22)
        Me.NumericUpDown_JointOffsetY.TabIndex = 23
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 56)
        Me.Label4.Margin = New System.Windows.Forms.Padding(16, 8, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(15, 13)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Y:"
        '
        'NumericUpDown_JointOffsetX
        '
        Me.NumericUpDown_JointOffsetX.Controls.Add(Me.UcNumericUpDownBig5)
        Me.NumericUpDown_JointOffsetX.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown_JointOffsetX.Location = New System.Drawing.Point(116, 24)
        Me.NumericUpDown_JointOffsetX.Margin = New System.Windows.Forms.Padding(8, 3, 8, 3)
        Me.NumericUpDown_JointOffsetX.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_JointOffsetX.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_JointOffsetX.Name = "NumericUpDown_JointOffsetX"
        Me.NumericUpDown_JointOffsetX.Size = New System.Drawing.Size(141, 22)
        Me.NumericUpDown_JointOffsetX.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 28)
        Me.Label2.Margin = New System.Windows.Forms.Padding(16, 8, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(16, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "X:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_ControllerYawCorrection)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_ControllerOffsetZ)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_ControllerOffsetY)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_ControllerOffsetX)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Location = New System.Drawing.Point(333, 111)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 3, 16, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(268, 147)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Controller Offset"
        '
        'NumericUpDown_ControllerYawCorrection
        '
        Me.NumericUpDown_ControllerYawCorrection.Controls.Add(Me.UcNumericUpDownBig4)
        Me.NumericUpDown_ControllerYawCorrection.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown_ControllerYawCorrection.Location = New System.Drawing.Point(116, 108)
        Me.NumericUpDown_ControllerYawCorrection.Margin = New System.Windows.Forms.Padding(8, 3, 8, 3)
        Me.NumericUpDown_ControllerYawCorrection.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_ControllerYawCorrection.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_ControllerYawCorrection.Name = "NumericUpDown_ControllerYawCorrection"
        Me.NumericUpDown_ControllerYawCorrection.Size = New System.Drawing.Size(141, 22)
        Me.NumericUpDown_ControllerYawCorrection.TabIndex = 35
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(19, 112)
        Me.Label10.Margin = New System.Windows.Forms.Padding(16, 8, 3, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(86, 13)
        Me.Label10.TabIndex = 36
        Me.Label10.Text = "Yaw Correction:"
        '
        'NumericUpDown_ControllerOffsetZ
        '
        Me.NumericUpDown_ControllerOffsetZ.Controls.Add(Me.UcNumericUpDownBig3)
        Me.NumericUpDown_ControllerOffsetZ.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown_ControllerOffsetZ.Location = New System.Drawing.Point(116, 80)
        Me.NumericUpDown_ControllerOffsetZ.Margin = New System.Windows.Forms.Padding(8, 3, 8, 3)
        Me.NumericUpDown_ControllerOffsetZ.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_ControllerOffsetZ.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_ControllerOffsetZ.Name = "NumericUpDown_ControllerOffsetZ"
        Me.NumericUpDown_ControllerOffsetZ.Size = New System.Drawing.Size(141, 22)
        Me.NumericUpDown_ControllerOffsetZ.TabIndex = 27
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 84)
        Me.Label6.Margin = New System.Windows.Forms.Padding(16, 8, 3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(13, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Z"
        '
        'NumericUpDown_ControllerOffsetY
        '
        Me.NumericUpDown_ControllerOffsetY.Controls.Add(Me.UcNumericUpDownBig2)
        Me.NumericUpDown_ControllerOffsetY.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown_ControllerOffsetY.Location = New System.Drawing.Point(116, 52)
        Me.NumericUpDown_ControllerOffsetY.Margin = New System.Windows.Forms.Padding(8, 3, 8, 3)
        Me.NumericUpDown_ControllerOffsetY.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_ControllerOffsetY.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_ControllerOffsetY.Name = "NumericUpDown_ControllerOffsetY"
        Me.NumericUpDown_ControllerOffsetY.Size = New System.Drawing.Size(141, 22)
        Me.NumericUpDown_ControllerOffsetY.TabIndex = 23
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 56)
        Me.Label7.Margin = New System.Windows.Forms.Padding(16, 8, 3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(12, 13)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Y"
        '
        'NumericUpDown_ControllerOffsetX
        '
        Me.NumericUpDown_ControllerOffsetX.Controls.Add(Me.UcNumericUpDownBig1)
        Me.NumericUpDown_ControllerOffsetX.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown_ControllerOffsetX.Location = New System.Drawing.Point(116, 24)
        Me.NumericUpDown_ControllerOffsetX.Margin = New System.Windows.Forms.Padding(8, 3, 8, 3)
        Me.NumericUpDown_ControllerOffsetX.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_ControllerOffsetX.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_ControllerOffsetX.Name = "NumericUpDown_ControllerOffsetX"
        Me.NumericUpDown_ControllerOffsetX.Size = New System.Drawing.Size(141, 22)
        Me.NumericUpDown_ControllerOffsetX.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(19, 28)
        Me.Label8.Margin = New System.Windows.Forms.Padding(16, 8, 3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(13, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "X"
        '
        'TextBox_Fps
        '
        Me.TextBox_Fps.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Fps.BackColor = System.Drawing.Color.White
        Me.TextBox_Fps.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Fps.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Fps.Location = New System.Drawing.Point(16, 330)
        Me.TextBox_Fps.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.TextBox_Fps.Name = "TextBox_Fps"
        Me.TextBox_Fps.ReadOnly = True
        Me.TextBox_Fps.Size = New System.Drawing.Size(268, 15)
        Me.TextBox_Fps.TabIndex = 32
        Me.TextBox_Fps.Text = "I/O FPS: 0"
        '
        'CheckBox_JointOnly
        '
        Me.CheckBox_JointOnly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_JointOnly.AutoSize = True
        Me.CheckBox_JointOnly.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_JointOnly.Location = New System.Drawing.Point(359, 328)
        Me.CheckBox_JointOnly.Name = "CheckBox_JointOnly"
        Me.CheckBox_JointOnly.Size = New System.Drawing.Size(115, 18)
        Me.CheckBox_JointOnly.TabIndex = 33
        Me.CheckBox_JointOnly.Text = "Joint offset only"
        Me.CheckBox_JointOnly.UseVisualStyleBackColor = True
        '
        'TextBox_TrackerName
        '
        Me.TextBox_TrackerName.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_TrackerName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_TrackerName.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_TrackerName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_TrackerName.HideSelection = False
        Me.TextBox_TrackerName.Location = New System.Drawing.Point(16, 16)
        Me.TextBox_TrackerName.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.TextBox_TrackerName.Name = "TextBox_TrackerName"
        Me.TextBox_TrackerName.ReadOnly = True
        Me.TextBox_TrackerName.Size = New System.Drawing.Size(293, 15)
        Me.TextBox_TrackerName.TabIndex = 35
        Me.TextBox_TrackerName.Text = "Tracker Name:  Unknown"
        Me.TextBox_TrackerName.WordWrap = False
        '
        'LinkLabel_EditName
        '
        Me.LinkLabel_EditName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel_EditName.AutoSize = True
        Me.LinkLabel_EditName.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_EditName.Location = New System.Drawing.Point(542, 16)
        Me.LinkLabel_EditName.Margin = New System.Windows.Forms.Padding(3, 16, 16, 0)
        Me.LinkLabel_EditName.Name = "LinkLabel_EditName"
        Me.LinkLabel_EditName.Size = New System.Drawing.Size(59, 13)
        Me.LinkLabel_EditName.TabIndex = 34
        Me.LinkLabel_EditName.TabStop = True
        Me.LinkLabel_EditName.Text = "Edit Name"
        '
        'Timer_Status
        '
        Me.Timer_Status.Enabled = True
        Me.Timer_Status.Interval = 2500
        '
        'Panel_Status
        '
        Me.Panel_Status.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Status.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Panel_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Status.Controls.Add(Me.PictureBox1)
        Me.Panel_Status.Controls.Add(Me.Label_StatusMessage)
        Me.Panel_Status.Controls.Add(Me.Label_StatusTitle)
        Me.Panel_Status.Location = New System.Drawing.Point(16, 267)
        Me.Panel_Status.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Panel_Status.Name = "Panel_Status"
        Me.Panel_Status.Size = New System.Drawing.Size(585, 54)
        Me.Panel_Status.TabIndex = 36
        '
        'Label_StatusMessage
        '
        Me.Label_StatusMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_StatusMessage.Location = New System.Drawing.Point(25, 19)
        Me.Label_StatusMessage.Name = "Label_StatusMessage"
        Me.Label_StatusMessage.Size = New System.Drawing.Size(555, 32)
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
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1608_16x16_32
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.m_HighQuality = False
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'UcNumericUpDownBig4
        '
        Me.UcNumericUpDownBig4.AutoSize = True
        Me.UcNumericUpDownBig4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig4.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig4.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig4.Location = New System.Drawing.Point(75, 0)
        Me.UcNumericUpDownBig4.m_bDockOnControl = True
        Me.UcNumericUpDownBig4.m_NumericUpDown = Me.NumericUpDown_ControllerYawCorrection
        Me.UcNumericUpDownBig4.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig4.m_ResetVisible = True
        Me.UcNumericUpDownBig4.Name = "UcNumericUpDownBig4"
        Me.UcNumericUpDownBig4.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig4.TabIndex = 36
        '
        'UcNumericUpDownBig3
        '
        Me.UcNumericUpDownBig3.AutoSize = True
        Me.UcNumericUpDownBig3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig3.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig3.Location = New System.Drawing.Point(75, 0)
        Me.UcNumericUpDownBig3.m_bDockOnControl = True
        Me.UcNumericUpDownBig3.m_NumericUpDown = Me.NumericUpDown_ControllerOffsetZ
        Me.UcNumericUpDownBig3.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig3.m_ResetVisible = True
        Me.UcNumericUpDownBig3.Name = "UcNumericUpDownBig3"
        Me.UcNumericUpDownBig3.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig3.TabIndex = 36
        '
        'UcNumericUpDownBig2
        '
        Me.UcNumericUpDownBig2.AutoSize = True
        Me.UcNumericUpDownBig2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig2.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig2.Location = New System.Drawing.Point(75, 0)
        Me.UcNumericUpDownBig2.m_bDockOnControl = True
        Me.UcNumericUpDownBig2.m_NumericUpDown = Me.NumericUpDown_ControllerOffsetY
        Me.UcNumericUpDownBig2.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig2.m_ResetVisible = True
        Me.UcNumericUpDownBig2.Name = "UcNumericUpDownBig2"
        Me.UcNumericUpDownBig2.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig2.TabIndex = 36
        '
        'UcNumericUpDownBig1
        '
        Me.UcNumericUpDownBig1.AutoSize = True
        Me.UcNumericUpDownBig1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig1.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig1.Location = New System.Drawing.Point(75, 0)
        Me.UcNumericUpDownBig1.m_bDockOnControl = True
        Me.UcNumericUpDownBig1.m_NumericUpDown = Me.NumericUpDown_ControllerOffsetX
        Me.UcNumericUpDownBig1.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig1.m_ResetVisible = True
        Me.UcNumericUpDownBig1.Name = "UcNumericUpDownBig1"
        Me.UcNumericUpDownBig1.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig1.TabIndex = 36
        '
        'UcNumericUpDownBig8
        '
        Me.UcNumericUpDownBig8.AutoSize = True
        Me.UcNumericUpDownBig8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig8.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig8.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig8.Location = New System.Drawing.Point(75, 0)
        Me.UcNumericUpDownBig8.m_bDockOnControl = True
        Me.UcNumericUpDownBig8.m_NumericUpDown = Me.NumericUpDown_JointYawCorrection
        Me.UcNumericUpDownBig8.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig8.m_ResetVisible = True
        Me.UcNumericUpDownBig8.Name = "UcNumericUpDownBig8"
        Me.UcNumericUpDownBig8.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig8.TabIndex = 36
        '
        'UcNumericUpDownBig7
        '
        Me.UcNumericUpDownBig7.AutoSize = True
        Me.UcNumericUpDownBig7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig7.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig7.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig7.Location = New System.Drawing.Point(75, 0)
        Me.UcNumericUpDownBig7.m_bDockOnControl = True
        Me.UcNumericUpDownBig7.m_NumericUpDown = Me.NumericUpDown_JointOffsetZ
        Me.UcNumericUpDownBig7.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig7.m_ResetVisible = True
        Me.UcNumericUpDownBig7.Name = "UcNumericUpDownBig7"
        Me.UcNumericUpDownBig7.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig7.TabIndex = 36
        '
        'UcNumericUpDownBig6
        '
        Me.UcNumericUpDownBig6.AutoSize = True
        Me.UcNumericUpDownBig6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig6.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig6.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig6.Location = New System.Drawing.Point(75, 0)
        Me.UcNumericUpDownBig6.m_bDockOnControl = True
        Me.UcNumericUpDownBig6.m_NumericUpDown = Me.NumericUpDown_JointOffsetY
        Me.UcNumericUpDownBig6.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig6.m_ResetVisible = True
        Me.UcNumericUpDownBig6.Name = "UcNumericUpDownBig6"
        Me.UcNumericUpDownBig6.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig6.TabIndex = 36
        '
        'UcNumericUpDownBig5
        '
        Me.UcNumericUpDownBig5.AutoSize = True
        Me.UcNumericUpDownBig5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig5.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig5.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig5.Location = New System.Drawing.Point(75, 0)
        Me.UcNumericUpDownBig5.m_bDockOnControl = True
        Me.UcNumericUpDownBig5.m_NumericUpDown = Me.NumericUpDown_JointOffsetX
        Me.UcNumericUpDownBig5.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig5.m_ResetVisible = True
        Me.UcNumericUpDownBig5.Name = "UcNumericUpDownBig5"
        Me.UcNumericUpDownBig5.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig5.TabIndex = 36
        '
        'UCControllerAttachmentsItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Panel_Status)
        Me.Controls.Add(Me.TextBox_TrackerName)
        Me.Controls.Add(Me.LinkLabel_EditName)
        Me.Controls.Add(Me.CheckBox_JointOnly)
        Me.Controls.Add(Me.TextBox_Fps)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBox_ParentControllerID)
        Me.Controls.Add(Me.Button_SaveSettings)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox_ControllerID)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCControllerAttachmentsItem"
        Me.Size = New System.Drawing.Size(617, 364)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumericUpDown_JointYawCorrection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_JointYawCorrection.ResumeLayout(False)
        Me.NumericUpDown_JointYawCorrection.PerformLayout()
        CType(Me.NumericUpDown_JointOffsetZ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_JointOffsetZ.ResumeLayout(False)
        Me.NumericUpDown_JointOffsetZ.PerformLayout()
        CType(Me.NumericUpDown_JointOffsetY, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_JointOffsetY.ResumeLayout(False)
        Me.NumericUpDown_JointOffsetY.PerformLayout()
        CType(Me.NumericUpDown_JointOffsetX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_JointOffsetX.ResumeLayout(False)
        Me.NumericUpDown_JointOffsetX.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.NumericUpDown_ControllerYawCorrection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_ControllerYawCorrection.ResumeLayout(False)
        Me.NumericUpDown_ControllerYawCorrection.PerformLayout()
        CType(Me.NumericUpDown_ControllerOffsetZ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_ControllerOffsetZ.ResumeLayout(False)
        Me.NumericUpDown_ControllerOffsetZ.PerformLayout()
        CType(Me.NumericUpDown_ControllerOffsetY, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_ControllerOffsetY.ResumeLayout(False)
        Me.NumericUpDown_ControllerOffsetY.PerformLayout()
        CType(Me.NumericUpDown_ControllerOffsetX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_ControllerOffsetX.ResumeLayout(False)
        Me.NumericUpDown_ControllerOffsetX.PerformLayout()
        Me.Panel_Status.ResumeLayout(False)
        Me.Panel_Status.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox_ControllerID As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TimerFPS As Timer
    Friend WithEvents Button_SaveSettings As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox_ParentControllerID As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents NumericUpDown_JointOffsetZ As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents NumericUpDown_JointOffsetY As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents NumericUpDown_JointOffsetX As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents NumericUpDown_ControllerOffsetZ As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents NumericUpDown_ControllerOffsetY As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents NumericUpDown_ControllerOffsetX As NumericUpDown
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBox_Fps As TextBox
    Friend WithEvents NumericUpDown_JointYawCorrection As NumericUpDown
    Friend WithEvents Label9 As Label
    Friend WithEvents NumericUpDown_ControllerYawCorrection As NumericUpDown
    Friend WithEvents Label10 As Label
    Friend WithEvents CheckBox_JointOnly As CheckBox
    Friend WithEvents TextBox_TrackerName As TextBox
    Friend WithEvents LinkLabel_EditName As LinkLabel
    Friend WithEvents UcNumericUpDownBig8 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig7 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig6 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig5 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig4 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig3 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig2 As UCNumericUpDownBig
    Friend WithEvents UcNumericUpDownBig1 As UCNumericUpDownBig
    Friend WithEvents Timer_Status As Timer
    Friend WithEvents Panel_Status As Panel
    Friend WithEvents PictureBox1 As ClassPictureBoxQuality
    Friend WithEvents Label_StatusMessage As Label
    Friend WithEvents Label_StatusTitle As Label
End Class
