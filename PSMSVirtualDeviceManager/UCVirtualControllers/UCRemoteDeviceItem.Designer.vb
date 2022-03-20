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
        Me.Label_Battery = New System.Windows.Forms.Label()
        Me.Label_TrackerName = New System.Windows.Forms.Label()
        Me.Label_Axis = New System.Windows.Forms.Label()
        Me.Label_Fps = New System.Windows.Forms.Label()
        Me.ComboBox_ControllerID = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TimerFPS = New System.Windows.Forms.Timer(Me.components)
        Me.Button_SaveSettings = New System.Windows.Forms.Button()
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
        'Label_Battery
        '
        Me.Label_Battery.AutoSize = True
        Me.Label_Battery.Location = New System.Drawing.Point(16, 74)
        Me.Label_Battery.Margin = New System.Windows.Forms.Padding(16, 3, 3, 0)
        Me.Label_Battery.Name = "Label_Battery"
        Me.Label_Battery.Size = New System.Drawing.Size(99, 13)
        Me.Label_Battery.TabIndex = 3
        Me.Label_Battery.Text = "Battery: Unknown"
        '
        'Label_TrackerName
        '
        Me.Label_TrackerName.AutoSize = True
        Me.Label_TrackerName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_TrackerName.Location = New System.Drawing.Point(16, 16)
        Me.Label_TrackerName.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label_TrackerName.Name = "Label_TrackerName"
        Me.Label_TrackerName.Size = New System.Drawing.Size(137, 13)
        Me.Label_TrackerName.TabIndex = 0
        Me.Label_TrackerName.Text = "Tracker Name:  Unknown"
        '
        'Label_Axis
        '
        Me.Label_Axis.AutoSize = True
        Me.Label_Axis.Location = New System.Drawing.Point(16, 32)
        Me.Label_Axis.Margin = New System.Windows.Forms.Padding(16, 3, 3, 0)
        Me.Label_Axis.Name = "Label_Axis"
        Me.Label_Axis.Size = New System.Drawing.Size(25, 39)
        Me.Label_Axis.TabIndex = 2
        Me.Label_Axis.Text = "X: 0" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Y: 0" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Z: 0"
        '
        'Label_Fps
        '
        Me.Label_Fps.AutoSize = True
        Me.Label_Fps.Location = New System.Drawing.Point(16, 90)
        Me.Label_Fps.Margin = New System.Windows.Forms.Padding(16, 3, 3, 0)
        Me.Label_Fps.Name = "Label_Fps"
        Me.Label_Fps.Size = New System.Drawing.Size(82, 13)
        Me.Label_Fps.TabIndex = 4
        Me.Label_Fps.Text = "FPS: Unknown"
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
        'UCRemoteDeviceItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Button_SaveSettings)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox_ControllerID)
        Me.Controls.Add(Me.Label_Fps)
        Me.Controls.Add(Me.Label_Battery)
        Me.Controls.Add(Me.Label_Axis)
        Me.Controls.Add(Me.Button_Recenter)
        Me.Controls.Add(Me.Label_TrackerName)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCRemoteDeviceItem"
        Me.Size = New System.Drawing.Size(617, 144)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button_Recenter As Button
    Friend WithEvents Label_Battery As Label
    Friend WithEvents Label_TrackerName As Label
    Friend WithEvents Label_Axis As Label
    Friend WithEvents Label_Fps As Label
    Friend WithEvents ComboBox_ControllerID As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TimerFPS As Timer
    Friend WithEvents Button_SaveSettings As Button
End Class
