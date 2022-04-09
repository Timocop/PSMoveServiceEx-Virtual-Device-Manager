<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCControllerAttachmentsItem
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
        Me.ComboBox_ControllerID = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TimerFPS = New System.Windows.Forms.Timer(Me.components)
        Me.Button_SaveSettings = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox_ParentControllerID = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button_JointNegZ = New System.Windows.Forms.Button()
        Me.Button_JointPosZ = New System.Windows.Forms.Button()
        Me.NumericUpDown_JointOffsetZ = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button_JointNegY = New System.Windows.Forms.Button()
        Me.Button_JointPosY = New System.Windows.Forms.Button()
        Me.NumericUpDown_JointOffsetY = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button_JointNegX = New System.Windows.Forms.Button()
        Me.Button_JointPosX = New System.Windows.Forms.Button()
        Me.NumericUpDown_JointOffsetX = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button_ControllerNegZ = New System.Windows.Forms.Button()
        Me.Button_ControllerPosZ = New System.Windows.Forms.Button()
        Me.NumericUpDown_ControllerOffsetZ = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button_ControllerNegY = New System.Windows.Forms.Button()
        Me.Button_ControllerPosY = New System.Windows.Forms.Button()
        Me.NumericUpDown_ControllerOffsetY = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button_ControllerNegX = New System.Windows.Forms.Button()
        Me.Button_ControllerPosX = New System.Windows.Forms.Button()
        Me.NumericUpDown_ControllerOffsetX = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label_Close = New System.Windows.Forms.Label()
        Me.TextBox_Fps = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumericUpDown_JointOffsetZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_JointOffsetY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_JointOffsetX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.NumericUpDown_ControllerOffsetZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_ControllerOffsetY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_ControllerOffsetX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBox_ControllerID
        '
        Me.ComboBox_ControllerID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_ControllerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_ControllerID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_ControllerID.FormattingEnabled = True
        Me.ComboBox_ControllerID.Location = New System.Drawing.Point(134, 16)
        Me.ComboBox_ControllerID.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.ComboBox_ControllerID.Name = "ComboBox_ControllerID"
        Me.ComboBox_ControllerID.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox_ControllerID.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 19)
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
        Me.Button_SaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_SaveSettings.Location = New System.Drawing.Point(493, 224)
        Me.Button_SaveSettings.Margin = New System.Windows.Forms.Padding(16)
        Me.Button_SaveSettings.Name = "Button_SaveSettings"
        Me.Button_SaveSettings.Size = New System.Drawing.Size(108, 23)
        Me.Button_SaveSettings.TabIndex = 7
        Me.Button_SaveSettings.Text = "Save Settings"
        Me.Button_SaveSettings.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 48)
        Me.Label3.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Parent Controller ID:"
        '
        'ComboBox_ParentControllerID
        '
        Me.ComboBox_ParentControllerID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_ParentControllerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_ParentControllerID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_ParentControllerID.FormattingEnabled = True
        Me.ComboBox_ParentControllerID.Location = New System.Drawing.Point(134, 43)
        Me.ComboBox_ParentControllerID.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.ComboBox_ParentControllerID.Name = "ComboBox_ParentControllerID"
        Me.ComboBox_ParentControllerID.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox_ParentControllerID.TabIndex = 17
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button_JointNegZ)
        Me.GroupBox1.Controls.Add(Me.Button_JointPosZ)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown_JointOffsetZ)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Button_JointNegY)
        Me.GroupBox1.Controls.Add(Me.Button_JointPosY)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown_JointOffsetY)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Button_JointNegX)
        Me.GroupBox1.Controls.Add(Me.Button_JointPosX)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown_JointOffsetX)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 83)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(268, 122)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Joint Offset"
        '
        'Button_JointNegZ
        '
        Me.Button_JointNegZ.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_JointNegZ.Location = New System.Drawing.Point(43, 79)
        Me.Button_JointNegZ.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
        Me.Button_JointNegZ.Name = "Button_JointNegZ"
        Me.Button_JointNegZ.Size = New System.Drawing.Size(23, 23)
        Me.Button_JointNegZ.TabIndex = 29
        Me.Button_JointNegZ.Text = "<"
        Me.Button_JointNegZ.UseVisualStyleBackColor = True
        '
        'Button_JointPosZ
        '
        Me.Button_JointPosZ.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_JointPosZ.Location = New System.Drawing.Point(234, 79)
        Me.Button_JointPosZ.Margin = New System.Windows.Forms.Padding(3, 3, 8, 3)
        Me.Button_JointPosZ.Name = "Button_JointPosZ"
        Me.Button_JointPosZ.Size = New System.Drawing.Size(23, 23)
        Me.Button_JointPosZ.TabIndex = 26
        Me.Button_JointPosZ.Text = ">"
        Me.Button_JointPosZ.UseVisualStyleBackColor = True
        '
        'NumericUpDown_JointOffsetZ
        '
        Me.NumericUpDown_JointOffsetZ.Location = New System.Drawing.Point(72, 80)
        Me.NumericUpDown_JointOffsetZ.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_JointOffsetZ.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_JointOffsetZ.Name = "NumericUpDown_JointOffsetZ"
        Me.NumericUpDown_JointOffsetZ.Size = New System.Drawing.Size(156, 22)
        Me.NumericUpDown_JointOffsetZ.TabIndex = 27
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 84)
        Me.Label5.Margin = New System.Windows.Forms.Padding(16, 8, 3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 13)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "Z"
        '
        'Button_JointNegY
        '
        Me.Button_JointNegY.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_JointNegY.Location = New System.Drawing.Point(43, 51)
        Me.Button_JointNegY.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
        Me.Button_JointNegY.Name = "Button_JointNegY"
        Me.Button_JointNegY.Size = New System.Drawing.Size(23, 23)
        Me.Button_JointNegY.TabIndex = 25
        Me.Button_JointNegY.Text = "<"
        Me.Button_JointNegY.UseVisualStyleBackColor = True
        '
        'Button_JointPosY
        '
        Me.Button_JointPosY.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_JointPosY.Location = New System.Drawing.Point(234, 51)
        Me.Button_JointPosY.Margin = New System.Windows.Forms.Padding(3, 3, 8, 3)
        Me.Button_JointPosY.Name = "Button_JointPosY"
        Me.Button_JointPosY.Size = New System.Drawing.Size(23, 23)
        Me.Button_JointPosY.TabIndex = 22
        Me.Button_JointPosY.Text = ">"
        Me.Button_JointPosY.UseVisualStyleBackColor = True
        '
        'NumericUpDown_JointOffsetY
        '
        Me.NumericUpDown_JointOffsetY.Location = New System.Drawing.Point(72, 52)
        Me.NumericUpDown_JointOffsetY.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_JointOffsetY.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_JointOffsetY.Name = "NumericUpDown_JointOffsetY"
        Me.NumericUpDown_JointOffsetY.Size = New System.Drawing.Size(156, 22)
        Me.NumericUpDown_JointOffsetY.TabIndex = 23
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 56)
        Me.Label4.Margin = New System.Windows.Forms.Padding(16, 8, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(12, 13)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Y"
        '
        'Button_JointNegX
        '
        Me.Button_JointNegX.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_JointNegX.Location = New System.Drawing.Point(43, 23)
        Me.Button_JointNegX.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
        Me.Button_JointNegX.Name = "Button_JointNegX"
        Me.Button_JointNegX.Size = New System.Drawing.Size(23, 23)
        Me.Button_JointNegX.TabIndex = 21
        Me.Button_JointNegX.Text = "<"
        Me.Button_JointNegX.UseVisualStyleBackColor = True
        '
        'Button_JointPosX
        '
        Me.Button_JointPosX.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_JointPosX.Location = New System.Drawing.Point(234, 23)
        Me.Button_JointPosX.Margin = New System.Windows.Forms.Padding(3, 3, 8, 3)
        Me.Button_JointPosX.Name = "Button_JointPosX"
        Me.Button_JointPosX.Size = New System.Drawing.Size(23, 23)
        Me.Button_JointPosX.TabIndex = 20
        Me.Button_JointPosX.Text = ">"
        Me.Button_JointPosX.UseVisualStyleBackColor = True
        '
        'NumericUpDown_JointOffsetX
        '
        Me.NumericUpDown_JointOffsetX.Location = New System.Drawing.Point(72, 24)
        Me.NumericUpDown_JointOffsetX.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_JointOffsetX.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_JointOffsetX.Name = "NumericUpDown_JointOffsetX"
        Me.NumericUpDown_JointOffsetX.Size = New System.Drawing.Size(156, 22)
        Me.NumericUpDown_JointOffsetX.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 28)
        Me.Label2.Margin = New System.Windows.Forms.Padding(16, 8, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(13, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "X"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Button_ControllerNegZ)
        Me.GroupBox2.Controls.Add(Me.Button_ControllerPosZ)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_ControllerOffsetZ)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Button_ControllerNegY)
        Me.GroupBox2.Controls.Add(Me.Button_ControllerPosY)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_ControllerOffsetY)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Button_ControllerNegX)
        Me.GroupBox2.Controls.Add(Me.Button_ControllerPosX)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_ControllerOffsetX)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Location = New System.Drawing.Point(333, 87)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 3, 16, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(268, 118)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Controller Offset"
        '
        'Button_ControllerNegZ
        '
        Me.Button_ControllerNegZ.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_ControllerNegZ.Location = New System.Drawing.Point(43, 79)
        Me.Button_ControllerNegZ.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
        Me.Button_ControllerNegZ.Name = "Button_ControllerNegZ"
        Me.Button_ControllerNegZ.Size = New System.Drawing.Size(23, 23)
        Me.Button_ControllerNegZ.TabIndex = 29
        Me.Button_ControllerNegZ.Text = "<"
        Me.Button_ControllerNegZ.UseVisualStyleBackColor = True
        '
        'Button_ControllerPosZ
        '
        Me.Button_ControllerPosZ.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_ControllerPosZ.Location = New System.Drawing.Point(234, 79)
        Me.Button_ControllerPosZ.Margin = New System.Windows.Forms.Padding(3, 3, 8, 3)
        Me.Button_ControllerPosZ.Name = "Button_ControllerPosZ"
        Me.Button_ControllerPosZ.Size = New System.Drawing.Size(23, 23)
        Me.Button_ControllerPosZ.TabIndex = 26
        Me.Button_ControllerPosZ.Text = ">"
        Me.Button_ControllerPosZ.UseVisualStyleBackColor = True
        '
        'NumericUpDown_ControllerOffsetZ
        '
        Me.NumericUpDown_ControllerOffsetZ.Location = New System.Drawing.Point(72, 80)
        Me.NumericUpDown_ControllerOffsetZ.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_ControllerOffsetZ.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_ControllerOffsetZ.Name = "NumericUpDown_ControllerOffsetZ"
        Me.NumericUpDown_ControllerOffsetZ.Size = New System.Drawing.Size(156, 22)
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
        'Button_ControllerNegY
        '
        Me.Button_ControllerNegY.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_ControllerNegY.Location = New System.Drawing.Point(43, 51)
        Me.Button_ControllerNegY.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
        Me.Button_ControllerNegY.Name = "Button_ControllerNegY"
        Me.Button_ControllerNegY.Size = New System.Drawing.Size(23, 23)
        Me.Button_ControllerNegY.TabIndex = 25
        Me.Button_ControllerNegY.Text = "<"
        Me.Button_ControllerNegY.UseVisualStyleBackColor = True
        '
        'Button_ControllerPosY
        '
        Me.Button_ControllerPosY.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_ControllerPosY.Location = New System.Drawing.Point(234, 51)
        Me.Button_ControllerPosY.Margin = New System.Windows.Forms.Padding(3, 3, 8, 3)
        Me.Button_ControllerPosY.Name = "Button_ControllerPosY"
        Me.Button_ControllerPosY.Size = New System.Drawing.Size(23, 23)
        Me.Button_ControllerPosY.TabIndex = 22
        Me.Button_ControllerPosY.Text = ">"
        Me.Button_ControllerPosY.UseVisualStyleBackColor = True
        '
        'NumericUpDown_ControllerOffsetY
        '
        Me.NumericUpDown_ControllerOffsetY.Location = New System.Drawing.Point(72, 52)
        Me.NumericUpDown_ControllerOffsetY.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_ControllerOffsetY.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_ControllerOffsetY.Name = "NumericUpDown_ControllerOffsetY"
        Me.NumericUpDown_ControllerOffsetY.Size = New System.Drawing.Size(156, 22)
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
        'Button_ControllerNegX
        '
        Me.Button_ControllerNegX.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_ControllerNegX.Location = New System.Drawing.Point(43, 23)
        Me.Button_ControllerNegX.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
        Me.Button_ControllerNegX.Name = "Button_ControllerNegX"
        Me.Button_ControllerNegX.Size = New System.Drawing.Size(23, 23)
        Me.Button_ControllerNegX.TabIndex = 21
        Me.Button_ControllerNegX.Text = "<"
        Me.Button_ControllerNegX.UseVisualStyleBackColor = True
        '
        'Button_ControllerPosX
        '
        Me.Button_ControllerPosX.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_ControllerPosX.Location = New System.Drawing.Point(234, 23)
        Me.Button_ControllerPosX.Margin = New System.Windows.Forms.Padding(3, 3, 8, 3)
        Me.Button_ControllerPosX.Name = "Button_ControllerPosX"
        Me.Button_ControllerPosX.Size = New System.Drawing.Size(23, 23)
        Me.Button_ControllerPosX.TabIndex = 20
        Me.Button_ControllerPosX.Text = ">"
        Me.Button_ControllerPosX.UseVisualStyleBackColor = True
        '
        'NumericUpDown_ControllerOffsetX
        '
        Me.NumericUpDown_ControllerOffsetX.Location = New System.Drawing.Point(72, 24)
        Me.NumericUpDown_ControllerOffsetX.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_ControllerOffsetX.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_ControllerOffsetX.Name = "NumericUpDown_ControllerOffsetX"
        Me.NumericUpDown_ControllerOffsetX.Size = New System.Drawing.Size(156, 22)
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
        'Label_Close
        '
        Me.Label_Close.AutoSize = True
        Me.Label_Close.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label_Close.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label_Close.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Close.ForeColor = System.Drawing.Color.White
        Me.Label_Close.Location = New System.Drawing.Point(579, 0)
        Me.Label_Close.Name = "Label_Close"
        Me.Label_Close.Size = New System.Drawing.Size(38, 13)
        Me.Label_Close.TabIndex = 31
        Me.Label_Close.Text = "    X    "
        Me.Label_Close.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox_Fps
        '
        Me.TextBox_Fps.BackColor = System.Drawing.Color.White
        Me.TextBox_Fps.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Fps.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Fps.Location = New System.Drawing.Point(16, 224)
        Me.TextBox_Fps.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.TextBox_Fps.Name = "TextBox_Fps"
        Me.TextBox_Fps.ReadOnly = True
        Me.TextBox_Fps.Size = New System.Drawing.Size(268, 15)
        Me.TextBox_Fps.TabIndex = 32
        Me.TextBox_Fps.Text = "I/O FPS: 0"
        '
        'UCControllerAttachmentsItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.TextBox_Fps)
        Me.Controls.Add(Me.Label_Close)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBox_ParentControllerID)
        Me.Controls.Add(Me.Button_SaveSettings)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox_ControllerID)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCControllerAttachmentsItem"
        Me.Size = New System.Drawing.Size(617, 263)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumericUpDown_JointOffsetZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_JointOffsetY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_JointOffsetX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.NumericUpDown_ControllerOffsetZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_ControllerOffsetY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_ControllerOffsetX, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Button_JointNegZ As Button
    Friend WithEvents Button_JointPosZ As Button
    Friend WithEvents NumericUpDown_JointOffsetZ As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents Button_JointNegY As Button
    Friend WithEvents Button_JointPosY As Button
    Friend WithEvents NumericUpDown_JointOffsetY As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents Button_JointNegX As Button
    Friend WithEvents Button_JointPosX As Button
    Friend WithEvents NumericUpDown_JointOffsetX As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Button_ControllerNegZ As Button
    Friend WithEvents Button_ControllerPosZ As Button
    Friend WithEvents NumericUpDown_ControllerOffsetZ As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents Button_ControllerNegY As Button
    Friend WithEvents Button_ControllerPosY As Button
    Friend WithEvents NumericUpDown_ControllerOffsetY As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents Button_ControllerNegX As Button
    Friend WithEvents Button_ControllerPosX As Button
    Friend WithEvents NumericUpDown_ControllerOffsetX As NumericUpDown
    Friend WithEvents Label8 As Label
    Friend WithEvents Label_Close As Label
    Friend WithEvents TextBox_Fps As TextBox
End Class
