﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCVirtualMotionTrackerItem
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
        Me.ComboBox_VMTTrackerID = New System.Windows.Forms.ComboBox()
        Me.Label_Close = New System.Windows.Forms.Label()
        Me.TextBox_Fps = New System.Windows.Forms.TextBox()
        Me.TextBox_Gyro = New System.Windows.Forms.TextBox()
        Me.TextBox_Pos = New System.Windows.Forms.TextBox()
        Me.TimerPose = New System.Windows.Forms.Timer(Me.components)
        Me.TextBox_Log = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ComboBox_ControllerID
        '
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
        Me.Button_SaveSettings.Location = New System.Drawing.Point(493, 214)
        Me.Button_SaveSettings.Margin = New System.Windows.Forms.Padding(16)
        Me.Button_SaveSettings.Name = "Button_SaveSettings"
        Me.Button_SaveSettings.Size = New System.Drawing.Size(108, 23)
        Me.Button_SaveSettings.TabIndex = 7
        Me.Button_SaveSettings.Text = "Save Settings"
        Me.Button_SaveSettings.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 48)
        Me.Label3.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
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
        Me.ComboBox_VMTTrackerID.Location = New System.Drawing.Point(134, 43)
        Me.ComboBox_VMTTrackerID.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.ComboBox_VMTTrackerID.Name = "ComboBox_VMTTrackerID"
        Me.ComboBox_VMTTrackerID.Size = New System.Drawing.Size(150, 21)
        Me.ComboBox_VMTTrackerID.TabIndex = 17
        '
        'Label_Close
        '
        Me.Label_Close.AutoSize = True
        Me.Label_Close.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label_Close.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label_Close.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Close.ForeColor = System.Drawing.Color.White
        Me.Label_Close.Location = New System.Drawing.Point(591, 0)
        Me.Label_Close.Name = "Label_Close"
        Me.Label_Close.Size = New System.Drawing.Size(26, 13)
        Me.Label_Close.TabIndex = 31
        Me.Label_Close.Text = "  X  "
        Me.Label_Close.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox_Fps
        '
        Me.TextBox_Fps.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Fps.BackColor = System.Drawing.Color.White
        Me.TextBox_Fps.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Fps.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Fps.Location = New System.Drawing.Point(16, 219)
        Me.TextBox_Fps.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.TextBox_Fps.Name = "TextBox_Fps"
        Me.TextBox_Fps.ReadOnly = True
        Me.TextBox_Fps.Size = New System.Drawing.Size(268, 15)
        Me.TextBox_Fps.TabIndex = 32
        Me.TextBox_Fps.Text = "I/O FPS: 0"
        '
        'TextBox_Gyro
        '
        Me.TextBox_Gyro.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_Gyro.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Gyro.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Gyro.HideSelection = False
        Me.TextBox_Gyro.Location = New System.Drawing.Point(137, 77)
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
        Me.TextBox_Pos.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox_Pos.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Pos.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_Pos.HideSelection = False
        Me.TextBox_Pos.Location = New System.Drawing.Point(16, 77)
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
        '
        'TextBox_Log
        '
        Me.TextBox_Log.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Log.BackColor = System.Drawing.Color.White
        Me.TextBox_Log.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.TextBox_Log.Location = New System.Drawing.Point(16, 151)
        Me.TextBox_Log.Margin = New System.Windows.Forms.Padding(16, 3, 16, 3)
        Me.TextBox_Log.Multiline = True
        Me.TextBox_Log.Name = "TextBox_Log"
        Me.TextBox_Log.ReadOnly = True
        Me.TextBox_Log.Size = New System.Drawing.Size(585, 44)
        Me.TextBox_Log.TabIndex = 36
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 135)
        Me.Label2.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Driver Message:"
        '
        'UCVirtualMotionTrackerItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox_Log)
        Me.Controls.Add(Me.TextBox_Gyro)
        Me.Controls.Add(Me.TextBox_Pos)
        Me.Controls.Add(Me.TextBox_Fps)
        Me.Controls.Add(Me.Label_Close)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBox_VMTTrackerID)
        Me.Controls.Add(Me.Button_SaveSettings)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox_ControllerID)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVirtualMotionTrackerItem"
        Me.Size = New System.Drawing.Size(617, 253)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBox_ControllerID As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TimerFPS As Timer
    Friend WithEvents Button_SaveSettings As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox_VMTTrackerID As ComboBox
    Friend WithEvents Label_Close As Label
    Friend WithEvents TextBox_Fps As TextBox
    Friend WithEvents TextBox_Gyro As TextBox
    Friend WithEvents TextBox_Pos As TextBox
    Friend WithEvents TimerPose As Timer
    Friend WithEvents TextBox_Log As TextBox
    Friend WithEvents Label2 As Label
End Class