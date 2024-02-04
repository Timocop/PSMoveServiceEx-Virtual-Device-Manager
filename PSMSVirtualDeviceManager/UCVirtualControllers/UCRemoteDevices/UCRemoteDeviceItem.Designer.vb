<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCRemoteDeviceItem
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
        Me.LinkLabel_EditName = New System.Windows.Forms.LinkLabel()
        Me.TextBox_TrackerName = New System.Windows.Forms.TextBox()
        Me.TextBox_Axis = New System.Windows.Forms.TextBox()
        Me.TextBox_Battery = New System.Windows.Forms.TextBox()
        Me.TextBox_Fps = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericUpDown_YawOffset = New System.Windows.Forms.NumericUpDown()
        Me.TextBox_Gyro = New System.Windows.Forms.TextBox()
        Me.Panel_Status = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button_SaveSettings = New System.Windows.Forms.Button()
        Me.Button_Recenter = New System.Windows.Forms.Button()
        Me.UcNumericUpDownBig1 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        CType(Me.NumericUpDown_YawOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_YawOffset.SuspendLayout()
        Me.Panel_Status.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBox_ControllerID
        '
        Me.ComboBox_ControllerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_ControllerID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_ControllerID.FormattingEnabled = True
        Me.ComboBox_ControllerID.Location = New System.Drawing.Point(108, 123)
        Me.ComboBox_ControllerID.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.ComboBox_ControllerID.Name = "ComboBox_ControllerID"
        Me.ComboBox_ControllerID.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox_ControllerID.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 126)
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
        'LinkLabel_EditName
        '
        Me.LinkLabel_EditName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel_EditName.AutoSize = True
        Me.LinkLabel_EditName.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_EditName.Location = New System.Drawing.Point(542, 16)
        Me.LinkLabel_EditName.Margin = New System.Windows.Forms.Padding(3, 16, 16, 0)
        Me.LinkLabel_EditName.Name = "LinkLabel_EditName"
        Me.LinkLabel_EditName.Size = New System.Drawing.Size(59, 13)
        Me.LinkLabel_EditName.TabIndex = 8
        Me.LinkLabel_EditName.TabStop = True
        Me.LinkLabel_EditName.Text = "Edit Name"
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
        Me.TextBox_TrackerName.TabIndex = 9
        Me.TextBox_TrackerName.Text = "Tracker Name:  Unknown"
        Me.TextBox_TrackerName.WordWrap = False
        '
        'TextBox_Axis
        '
        Me.TextBox_Axis.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_Axis.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Axis.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Axis.HideSelection = False
        Me.TextBox_Axis.Location = New System.Drawing.Point(16, 34)
        Me.TextBox_Axis.Margin = New System.Windows.Forms.Padding(16, 3, 3, 0)
        Me.TextBox_Axis.Multiline = True
        Me.TextBox_Axis.Name = "TextBox_Axis"
        Me.TextBox_Axis.ReadOnly = True
        Me.TextBox_Axis.Size = New System.Drawing.Size(115, 42)
        Me.TextBox_Axis.TabIndex = 10
        Me.TextBox_Axis.Text = "X: 0" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Y: 0" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Z: 0"
        Me.TextBox_Axis.WordWrap = False
        '
        'TextBox_Battery
        '
        Me.TextBox_Battery.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_Battery.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Battery.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Battery.HideSelection = False
        Me.TextBox_Battery.Location = New System.Drawing.Point(16, 79)
        Me.TextBox_Battery.Margin = New System.Windows.Forms.Padding(16, 3, 3, 0)
        Me.TextBox_Battery.Name = "TextBox_Battery"
        Me.TextBox_Battery.ReadOnly = True
        Me.TextBox_Battery.Size = New System.Drawing.Size(446, 15)
        Me.TextBox_Battery.TabIndex = 11
        Me.TextBox_Battery.Text = "Battery: Unknown"
        Me.TextBox_Battery.WordWrap = False
        '
        'TextBox_Fps
        '
        Me.TextBox_Fps.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_Fps.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Fps.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Fps.HideSelection = False
        Me.TextBox_Fps.Location = New System.Drawing.Point(16, 97)
        Me.TextBox_Fps.Margin = New System.Windows.Forms.Padding(16, 3, 3, 0)
        Me.TextBox_Fps.Name = "TextBox_Fps"
        Me.TextBox_Fps.ReadOnly = True
        Me.TextBox_Fps.Size = New System.Drawing.Size(446, 15)
        Me.TextBox_Fps.TabIndex = 12
        Me.TextBox_Fps.Text = "FPS: Unknown"
        Me.TextBox_Fps.WordWrap = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 155)
        Me.Label2.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Yaw Correction:"
        '
        'NumericUpDown_YawOffset
        '
        Me.NumericUpDown_YawOffset.Location = New System.Drawing.Point(108, 151)
        Me.NumericUpDown_YawOffset.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.NumericUpDown_YawOffset.Minimum = New Decimal(New Integer() {99999999, 0, 0, -2147483648})
        Me.NumericUpDown_YawOffset.Name = "NumericUpDown_YawOffset"
        Me.NumericUpDown_YawOffset.Size = New System.Drawing.Size(150, 22)
        Me.NumericUpDown_YawOffset.TabIndex = 13
        '
        'TextBox_Gyro
        '
        Me.TextBox_Gyro.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_Gyro.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Gyro.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Gyro.HideSelection = False
        Me.TextBox_Gyro.Location = New System.Drawing.Point(150, 34)
        Me.TextBox_Gyro.Margin = New System.Windows.Forms.Padding(16, 3, 3, 0)
        Me.TextBox_Gyro.Multiline = True
        Me.TextBox_Gyro.Name = "TextBox_Gyro"
        Me.TextBox_Gyro.ReadOnly = True
        Me.TextBox_Gyro.Size = New System.Drawing.Size(115, 42)
        Me.TextBox_Gyro.TabIndex = 17
        Me.TextBox_Gyro.Text = "X: 0" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Y: 0" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Z: 0"
        Me.TextBox_Gyro.WordWrap = False
        '
        'Panel_Status
        '
        Me.Panel_Status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Status.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Panel_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Status.Controls.Add(Me.PictureBox1)
        Me.Panel_Status.Controls.Add(Me.Label4)
        Me.Panel_Status.Controls.Add(Me.Label3)
        Me.Panel_Status.Location = New System.Drawing.Point(16, 195)
        Me.Panel_Status.Margin = New System.Windows.Forms.Padding(3, 6, 3, 16)
        Me.Panel_Status.Name = "Panel_Status"
        Me.Panel_Status.Size = New System.Drawing.Size(585, 42)
        Me.Panel_Status.TabIndex = 18
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(549, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "This device is not sending any IMU data. It either encountered an error or is cur" &
    "rently in calibration mode."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(25, 3)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(140, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Device is not responding!"
        '
        'Button_SaveSettings
        '
        Me.Button_SaveSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SaveSettings.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16761_16x16_32
        Me.Button_SaveSettings.Location = New System.Drawing.Point(481, 121)
        Me.Button_SaveSettings.Margin = New System.Windows.Forms.Padding(16, 16, 16, 3)
        Me.Button_SaveSettings.Name = "Button_SaveSettings"
        Me.Button_SaveSettings.Size = New System.Drawing.Size(120, 23)
        Me.Button_SaveSettings.TabIndex = 7
        Me.Button_SaveSettings.Text = "Save Settings"
        Me.Button_SaveSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_SaveSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_SaveSettings.UseVisualStyleBackColor = True
        '
        'Button_Recenter
        '
        Me.Button_Recenter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Recenter.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5345_16x16_32
        Me.Button_Recenter.Location = New System.Drawing.Point(481, 150)
        Me.Button_Recenter.Margin = New System.Windows.Forms.Padding(3, 3, 16, 16)
        Me.Button_Recenter.Name = "Button_Recenter"
        Me.Button_Recenter.Size = New System.Drawing.Size(120, 23)
        Me.Button_Recenter.TabIndex = 1
        Me.Button_Recenter.Text = "Recenter"
        Me.Button_Recenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Recenter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_Recenter.UseVisualStyleBackColor = True
        '
        'UcNumericUpDownBig1
        '
        Me.UcNumericUpDownBig1.AutoSize = True
        Me.UcNumericUpDownBig1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig1.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig1.Location = New System.Drawing.Point(84, 0)
        Me.UcNumericUpDownBig1.m_bDockOnControl = True
        Me.UcNumericUpDownBig1.m_NumericUpDown = Me.NumericUpDown_YawOffset
        Me.UcNumericUpDownBig1.m_ResetValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.UcNumericUpDownBig1.m_ResetVisible = True
        Me.UcNumericUpDownBig1.Name = "UcNumericUpDownBig1"
        Me.UcNumericUpDownBig1.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig1.TabIndex = 19
        '
        'UCRemoteDeviceItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Panel_Status)
        Me.Controls.Add(Me.TextBox_Gyro)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.NumericUpDown_YawOffset)
        Me.Controls.Add(Me.TextBox_Fps)
        Me.Controls.Add(Me.TextBox_Battery)
        Me.Controls.Add(Me.TextBox_Axis)
        Me.Controls.Add(Me.TextBox_TrackerName)
        Me.Controls.Add(Me.LinkLabel_EditName)
        Me.Controls.Add(Me.Button_SaveSettings)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox_ControllerID)
        Me.Controls.Add(Me.Button_Recenter)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCRemoteDeviceItem"
        Me.Size = New System.Drawing.Size(617, 251)
        CType(Me.NumericUpDown_YawOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_YawOffset.ResumeLayout(False)
        Me.NumericUpDown_YawOffset.PerformLayout()
        Me.Panel_Status.ResumeLayout(False)
        Me.Panel_Status.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button_Recenter As Button
    Friend WithEvents ComboBox_ControllerID As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TimerFPS As Timer
    Friend WithEvents Button_SaveSettings As Button
    Friend WithEvents LinkLabel_EditName As LinkLabel
    Friend WithEvents TextBox_TrackerName As TextBox
    Friend WithEvents TextBox_Axis As TextBox
    Friend WithEvents TextBox_Battery As TextBox
    Friend WithEvents TextBox_Fps As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDown_YawOffset As NumericUpDown
    Friend WithEvents TextBox_Gyro As TextBox
    Friend WithEvents Panel_Status As Panel
    Friend WithEvents PictureBox1 As ClassPictureBoxQuality
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents UcNumericUpDownBig1 As UCNumericUpDownBig
End Class
