<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCVirtualTrackerItem
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
        Me.PictureBox_CaptureImage = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label_FriendlyName = New System.Windows.Forms.Label()
        Me.ComboBox_DeviceTrackerId = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TrackBar_DeviceExposure = New System.Windows.Forms.TrackBar()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TrackBar_DeviceGain = New System.Windows.Forms.TrackBar()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button_Close = New System.Windows.Forms.Label()
        Me.Label_FPS = New System.Windows.Forms.Label()
        Me.CheckBox_ShowCaptureImage = New System.Windows.Forms.CheckBox()
        Me.TrackBar_DeviceGamma = New System.Windows.Forms.TrackBar()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button_RestartDevice = New System.Windows.Forms.Button()
        Me.TrackBar_DeviceConstrast = New System.Windows.Forms.TrackBar()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button_ConfigSave = New System.Windows.Forms.Button()
        Me.CheckBox_Autostart = New System.Windows.Forms.CheckBox()
        CType(Me.PictureBox_CaptureImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_DeviceExposure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_DeviceGain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_DeviceGamma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_DeviceConstrast, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox_CaptureImage
        '
        Me.PictureBox_CaptureImage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox_CaptureImage.BackColor = System.Drawing.Color.Black
        Me.PictureBox_CaptureImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox_CaptureImage.Location = New System.Drawing.Point(504, 29)
        Me.PictureBox_CaptureImage.Margin = New System.Windows.Forms.Padding(16)
        Me.PictureBox_CaptureImage.Name = "PictureBox_CaptureImage"
        Me.PictureBox_CaptureImage.Size = New System.Drawing.Size(280, 224)
        Me.PictureBox_CaptureImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox_CaptureImage.TabIndex = 0
        Me.PictureBox_CaptureImage.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(0, 268)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(800, 1)
        Me.Label1.TabIndex = 1
        '
        'Label_FriendlyName
        '
        Me.Label_FriendlyName.AutoSize = True
        Me.Label_FriendlyName.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_FriendlyName.Location = New System.Drawing.Point(3, 3)
        Me.Label_FriendlyName.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_FriendlyName.Name = "Label_FriendlyName"
        Me.Label_FriendlyName.Size = New System.Drawing.Size(206, 21)
        Me.Label_FriendlyName.TabIndex = 2
        Me.Label_FriendlyName.Text = "#0 - Device FriendlyName"
        '
        'ComboBox_DeviceTrackerId
        '
        Me.ComboBox_DeviceTrackerId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_DeviceTrackerId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_DeviceTrackerId.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_DeviceTrackerId.FormattingEnabled = True
        Me.ComboBox_DeviceTrackerId.Location = New System.Drawing.Point(120, 169)
        Me.ComboBox_DeviceTrackerId.Name = "ComboBox_DeviceTrackerId"
        Me.ComboBox_DeviceTrackerId.Size = New System.Drawing.Size(365, 21)
        Me.ComboBox_DeviceTrackerId.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 30)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Device Properties:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 52)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Exposure:"
        '
        'TrackBar_DeviceExposure
        '
        Me.TrackBar_DeviceExposure.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar_DeviceExposure.AutoSize = False
        Me.TrackBar_DeviceExposure.LargeChange = 1
        Me.TrackBar_DeviceExposure.Location = New System.Drawing.Point(120, 52)
        Me.TrackBar_DeviceExposure.Name = "TrackBar_DeviceExposure"
        Me.TrackBar_DeviceExposure.Size = New System.Drawing.Size(365, 16)
        Me.TrackBar_DeviceExposure.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 74)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Gain:"
        '
        'TrackBar_DeviceGain
        '
        Me.TrackBar_DeviceGain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar_DeviceGain.AutoSize = False
        Me.TrackBar_DeviceGain.LargeChange = 1
        Me.TrackBar_DeviceGain.Location = New System.Drawing.Point(120, 74)
        Me.TrackBar_DeviceGain.Name = "TrackBar_DeviceGain"
        Me.TrackBar_DeviceGain.Size = New System.Drawing.Size(365, 16)
        Me.TrackBar_DeviceGain.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 150)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Tracker Properties:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 172)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Tracker Id:"
        '
        'Button_Close
        '
        Me.Button_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Close.AutoSize = True
        Me.Button_Close.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_Close.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button_Close.ForeColor = System.Drawing.Color.White
        Me.Button_Close.Location = New System.Drawing.Point(774, 0)
        Me.Button_Close.Name = "Button_Close"
        Me.Button_Close.Size = New System.Drawing.Size(26, 13)
        Me.Button_Close.TabIndex = 11
        Me.Button_Close.Text = "  X  "
        '
        'Label_FPS
        '
        Me.Label_FPS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_FPS.AutoSize = True
        Me.Label_FPS.Location = New System.Drawing.Point(501, 11)
        Me.Label_FPS.Name = "Label_FPS"
        Me.Label_FPS.Size = New System.Drawing.Size(37, 13)
        Me.Label_FPS.TabIndex = 12
        Me.Label_FPS.Text = "FPS: 0"
        '
        'CheckBox_ShowCaptureImage
        '
        Me.CheckBox_ShowCaptureImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_ShowCaptureImage.AutoSize = True
        Me.CheckBox_ShowCaptureImage.Location = New System.Drawing.Point(653, 11)
        Me.CheckBox_ShowCaptureImage.Name = "CheckBox_ShowCaptureImage"
        Me.CheckBox_ShowCaptureImage.Size = New System.Drawing.Size(131, 17)
        Me.CheckBox_ShowCaptureImage.TabIndex = 13
        Me.CheckBox_ShowCaptureImage.Text = "Show capture image"
        Me.CheckBox_ShowCaptureImage.UseVisualStyleBackColor = True
        '
        'TrackBar_DeviceGamma
        '
        Me.TrackBar_DeviceGamma.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar_DeviceGamma.AutoSize = False
        Me.TrackBar_DeviceGamma.LargeChange = 1
        Me.TrackBar_DeviceGamma.Location = New System.Drawing.Point(120, 96)
        Me.TrackBar_DeviceGamma.Name = "TrackBar_DeviceGamma"
        Me.TrackBar_DeviceGamma.Size = New System.Drawing.Size(365, 16)
        Me.TrackBar_DeviceGamma.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 96)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Gamma:"
        '
        'Button_RestartDevice
        '
        Me.Button_RestartDevice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_RestartDevice.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_RestartDevice.Location = New System.Drawing.Point(3, 230)
        Me.Button_RestartDevice.Margin = New System.Windows.Forms.Padding(3, 16, 3, 16)
        Me.Button_RestartDevice.Name = "Button_RestartDevice"
        Me.Button_RestartDevice.Size = New System.Drawing.Size(116, 23)
        Me.Button_RestartDevice.TabIndex = 16
        Me.Button_RestartDevice.Text = "Restart device"
        Me.Button_RestartDevice.UseVisualStyleBackColor = True
        '
        'TrackBar_DeviceConstrast
        '
        Me.TrackBar_DeviceConstrast.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar_DeviceConstrast.AutoSize = False
        Me.TrackBar_DeviceConstrast.LargeChange = 1
        Me.TrackBar_DeviceConstrast.Location = New System.Drawing.Point(121, 118)
        Me.TrackBar_DeviceConstrast.Name = "TrackBar_DeviceConstrast"
        Me.TrackBar_DeviceConstrast.Size = New System.Drawing.Size(365, 16)
        Me.TrackBar_DeviceConstrast.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(4, 118)
        Me.Label8.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Constrast:"
        '
        'Button_ConfigSave
        '
        Me.Button_ConfigSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ConfigSave.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_ConfigSave.Location = New System.Drawing.Point(369, 230)
        Me.Button_ConfigSave.Margin = New System.Windows.Forms.Padding(3, 16, 3, 16)
        Me.Button_ConfigSave.Name = "Button_ConfigSave"
        Me.Button_ConfigSave.Size = New System.Drawing.Size(116, 23)
        Me.Button_ConfigSave.TabIndex = 19
        Me.Button_ConfigSave.Text = "Save Settings"
        Me.Button_ConfigSave.UseVisualStyleBackColor = True
        '
        'CheckBox_Autostart
        '
        Me.CheckBox_Autostart.AutoSize = True
        Me.CheckBox_Autostart.Location = New System.Drawing.Point(6, 199)
        Me.CheckBox_Autostart.Margin = New System.Windows.Forms.Padding(6, 6, 3, 3)
        Me.CheckBox_Autostart.Name = "CheckBox_Autostart"
        Me.CheckBox_Autostart.Size = New System.Drawing.Size(338, 17)
        Me.CheckBox_Autostart.TabIndex = 20
        Me.CheckBox_Autostart.Text = "Automatically start this video input device with this program."
        Me.CheckBox_Autostart.UseVisualStyleBackColor = True
        '
        'UCVirtualTrackerItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.CheckBox_Autostart)
        Me.Controls.Add(Me.Button_ConfigSave)
        Me.Controls.Add(Me.TrackBar_DeviceConstrast)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Button_RestartDevice)
        Me.Controls.Add(Me.TrackBar_DeviceGamma)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label_FPS)
        Me.Controls.Add(Me.Button_Close)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TrackBar_DeviceGain)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TrackBar_DeviceExposure)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBox_DeviceTrackerId)
        Me.Controls.Add(Me.Label_FriendlyName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox_CaptureImage)
        Me.Controls.Add(Me.CheckBox_ShowCaptureImage)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVirtualTrackerItem"
        Me.Size = New System.Drawing.Size(800, 269)
        CType(Me.PictureBox_CaptureImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_DeviceExposure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_DeviceGain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_DeviceGamma, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_DeviceConstrast, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox_CaptureImage As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label_FriendlyName As Label
    Friend WithEvents ComboBox_DeviceTrackerId As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TrackBar_DeviceExposure As TrackBar
    Friend WithEvents Label4 As Label
    Friend WithEvents TrackBar_DeviceGain As TrackBar
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Button_Close As Label
    Friend WithEvents Label_FPS As Label
    Friend WithEvents CheckBox_ShowCaptureImage As CheckBox
    Friend WithEvents TrackBar_DeviceGamma As TrackBar
    Friend WithEvents Label7 As Label
    Friend WithEvents Button_RestartDevice As Button
    Friend WithEvents TrackBar_DeviceConstrast As TrackBar
    Friend WithEvents Label8 As Label
    Friend WithEvents Button_ConfigSave As Button
    Friend WithEvents CheckBox_Autostart As CheckBox
End Class
