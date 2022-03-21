<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCRemoteDeviceItem
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
        Me.Button_Recenter = New System.Windows.Forms.Button()
        Me.ComboBox_ControllerID = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TimerFPS = New System.Windows.Forms.Timer(Me.components)
        Me.Button_SaveSettings = New System.Windows.Forms.Button()
        Me.LinkLabel_EditName = New System.Windows.Forms.LinkLabel()
        Me.TextBox_TrackerName = New System.Windows.Forms.TextBox()
        Me.TextBox_Axis = New System.Windows.Forms.TextBox()
        Me.TextBox_Battery = New System.Windows.Forms.TextBox()
        Me.TextBox_Fps = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Button_Recenter
        '
        Me.Button_Recenter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Recenter.Location = New System.Drawing.Point(493, 105)
        Me.Button_Recenter.Margin = New System.Windows.Forms.Padding(3, 3, 16, 16)
        Me.Button_Recenter.Name = "Button_Recenter"
        Me.Button_Recenter.Size = New System.Drawing.Size(108, 23)
        Me.Button_Recenter.TabIndex = 1
        Me.Button_Recenter.Text = "Recenter"
        Me.Button_Recenter.UseVisualStyleBackColor = True
        '
        'ComboBox_ControllerID
        '
        Me.ComboBox_ControllerID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_ControllerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_ControllerID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_ControllerID.FormattingEnabled = True
        Me.ComboBox_ControllerID.Location = New System.Drawing.Point(382, 107)
        Me.ComboBox_ControllerID.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.ComboBox_ControllerID.Name = "ComboBox_ControllerID"
        Me.ComboBox_ControllerID.Size = New System.Drawing.Size(92, 21)
        Me.ComboBox_ControllerID.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(300, 110)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
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
        Me.Button_SaveSettings.Location = New System.Drawing.Point(493, 76)
        Me.Button_SaveSettings.Margin = New System.Windows.Forms.Padding(16, 16, 16, 3)
        Me.Button_SaveSettings.Name = "Button_SaveSettings"
        Me.Button_SaveSettings.Size = New System.Drawing.Size(108, 23)
        Me.Button_SaveSettings.TabIndex = 7
        Me.Button_SaveSettings.Text = "Save Settings"
        Me.Button_SaveSettings.UseVisualStyleBackColor = True
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
        Me.TextBox_Axis.Size = New System.Drawing.Size(293, 42)
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
        Me.TextBox_Battery.Size = New System.Drawing.Size(293, 15)
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
        Me.TextBox_Fps.Size = New System.Drawing.Size(293, 15)
        Me.TextBox_Fps.TabIndex = 12
        Me.TextBox_Fps.Text = "FPS: Unknown"
        Me.TextBox_Fps.WordWrap = False
        '
        'UCRemoteDeviceItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
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
        Me.Size = New System.Drawing.Size(617, 144)
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
End Class
