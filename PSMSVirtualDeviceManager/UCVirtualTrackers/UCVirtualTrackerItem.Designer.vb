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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TrackBar_DeviceExposure = New System.Windows.Forms.TrackBar()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TrackBar_DeviceGain = New System.Windows.Forms.TrackBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button_Close = New System.Windows.Forms.Label()
        Me.CheckBox_ShowCaptureImage = New System.Windows.Forms.CheckBox()
        Me.TrackBar_DeviceGamma = New System.Windows.Forms.TrackBar()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button_RestartDevice = New System.Windows.Forms.Button()
        Me.TrackBar_DeviceConstrast = New System.Windows.Forms.TrackBar()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button_ConfigSave = New System.Windows.Forms.Button()
        Me.CheckBox_Autostart = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage_DeviceProperties = New System.Windows.Forms.TabPage()
        Me.TabPage_TrackerProperties = New System.Windows.Forms.TabPage()
        Me.ComboBox_ImageInterpolation = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBox_FlipHorizontal = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox_Fps = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox_CaptureImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_DeviceExposure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_DeviceGain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_DeviceGamma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_DeviceConstrast, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage_DeviceProperties.SuspendLayout()
        Me.TabPage_TrackerProperties.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox_CaptureImage
        '
        Me.PictureBox_CaptureImage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox_CaptureImage.BackColor = System.Drawing.Color.Black
        Me.PictureBox_CaptureImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox_CaptureImage.Location = New System.Drawing.Point(504, 52)
        Me.PictureBox_CaptureImage.Margin = New System.Windows.Forms.Padding(16)
        Me.PictureBox_CaptureImage.Name = "PictureBox_CaptureImage"
        Me.PictureBox_CaptureImage.Size = New System.Drawing.Size(280, 229)
        Me.PictureBox_CaptureImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox_CaptureImage.TabIndex = 0
        Me.PictureBox_CaptureImage.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(0, 296)
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
        Me.ComboBox_DeviceTrackerId.Location = New System.Drawing.Point(152, 6)
        Me.ComboBox_DeviceTrackerId.Name = "ComboBox_DeviceTrackerId"
        Me.ComboBox_DeviceTrackerId.Size = New System.Drawing.Size(317, 21)
        Me.ComboBox_DeviceTrackerId.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 9)
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
        Me.TrackBar_DeviceExposure.Location = New System.Drawing.Point(123, 9)
        Me.TrackBar_DeviceExposure.Name = "TrackBar_DeviceExposure"
        Me.TrackBar_DeviceExposure.Size = New System.Drawing.Size(346, 16)
        Me.TrackBar_DeviceExposure.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 31)
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
        Me.TrackBar_DeviceGain.Location = New System.Drawing.Point(123, 31)
        Me.TrackBar_DeviceGain.Name = "TrackBar_DeviceGain"
        Me.TrackBar_DeviceGain.Size = New System.Drawing.Size(346, 16)
        Me.TrackBar_DeviceGain.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 9)
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
        'CheckBox_ShowCaptureImage
        '
        Me.CheckBox_ShowCaptureImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_ShowCaptureImage.AutoSize = True
        Me.CheckBox_ShowCaptureImage.Location = New System.Drawing.Point(719, 29)
        Me.CheckBox_ShowCaptureImage.Name = "CheckBox_ShowCaptureImage"
        Me.CheckBox_ShowCaptureImage.Size = New System.Drawing.Size(65, 17)
        Me.CheckBox_ShowCaptureImage.TabIndex = 13
        Me.CheckBox_ShowCaptureImage.Text = "Preview"
        Me.CheckBox_ShowCaptureImage.UseVisualStyleBackColor = True
        '
        'TrackBar_DeviceGamma
        '
        Me.TrackBar_DeviceGamma.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar_DeviceGamma.AutoSize = False
        Me.TrackBar_DeviceGamma.LargeChange = 1
        Me.TrackBar_DeviceGamma.Location = New System.Drawing.Point(123, 53)
        Me.TrackBar_DeviceGamma.Name = "TrackBar_DeviceGamma"
        Me.TrackBar_DeviceGamma.Size = New System.Drawing.Size(346, 16)
        Me.TrackBar_DeviceGamma.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 53)
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
        Me.Button_RestartDevice.Location = New System.Drawing.Point(3, 258)
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
        Me.TrackBar_DeviceConstrast.Location = New System.Drawing.Point(124, 75)
        Me.TrackBar_DeviceConstrast.Name = "TrackBar_DeviceConstrast"
        Me.TrackBar_DeviceConstrast.Size = New System.Drawing.Size(346, 16)
        Me.TrackBar_DeviceConstrast.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 75)
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
        Me.Button_ConfigSave.Location = New System.Drawing.Point(369, 258)
        Me.Button_ConfigSave.Margin = New System.Windows.Forms.Padding(3, 16, 3, 16)
        Me.Button_ConfigSave.Name = "Button_ConfigSave"
        Me.Button_ConfigSave.Size = New System.Drawing.Size(116, 23)
        Me.Button_ConfigSave.TabIndex = 19
        Me.Button_ConfigSave.Text = "Save Settings"
        Me.Button_ConfigSave.UseVisualStyleBackColor = True
        '
        'CheckBox_Autostart
        '
        Me.CheckBox_Autostart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_Autostart.AutoSize = True
        Me.CheckBox_Autostart.Location = New System.Drawing.Point(6, 222)
        Me.CheckBox_Autostart.Margin = New System.Windows.Forms.Padding(6, 6, 3, 3)
        Me.CheckBox_Autostart.Name = "CheckBox_Autostart"
        Me.CheckBox_Autostart.Size = New System.Drawing.Size(338, 17)
        Me.CheckBox_Autostart.TabIndex = 20
        Me.CheckBox_Autostart.Text = "Automatically start this video input device with this program."
        Me.CheckBox_Autostart.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage_DeviceProperties)
        Me.TabControl1.Controls.Add(Me.TabPage_TrackerProperties)
        Me.TabControl1.Location = New System.Drawing.Point(3, 52)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(483, 161)
        Me.TabControl1.TabIndex = 21
        '
        'TabPage_DeviceProperties
        '
        Me.TabPage_DeviceProperties.Controls.Add(Me.Label3)
        Me.TabPage_DeviceProperties.Controls.Add(Me.TrackBar_DeviceExposure)
        Me.TabPage_DeviceProperties.Controls.Add(Me.Label4)
        Me.TabPage_DeviceProperties.Controls.Add(Me.TrackBar_DeviceConstrast)
        Me.TabPage_DeviceProperties.Controls.Add(Me.TrackBar_DeviceGain)
        Me.TabPage_DeviceProperties.Controls.Add(Me.Label8)
        Me.TabPage_DeviceProperties.Controls.Add(Me.Label7)
        Me.TabPage_DeviceProperties.Controls.Add(Me.TrackBar_DeviceGamma)
        Me.TabPage_DeviceProperties.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_DeviceProperties.Name = "TabPage_DeviceProperties"
        Me.TabPage_DeviceProperties.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_DeviceProperties.Size = New System.Drawing.Size(475, 135)
        Me.TabPage_DeviceProperties.TabIndex = 0
        Me.TabPage_DeviceProperties.Text = "Device Properties"
        Me.TabPage_DeviceProperties.UseVisualStyleBackColor = True
        '
        'TabPage_TrackerProperties
        '
        Me.TabPage_TrackerProperties.Controls.Add(Me.ComboBox_ImageInterpolation)
        Me.TabPage_TrackerProperties.Controls.Add(Me.Label5)
        Me.TabPage_TrackerProperties.Controls.Add(Me.CheckBox_FlipHorizontal)
        Me.TabPage_TrackerProperties.Controls.Add(Me.Label2)
        Me.TabPage_TrackerProperties.Controls.Add(Me.Label6)
        Me.TabPage_TrackerProperties.Controls.Add(Me.ComboBox_DeviceTrackerId)
        Me.TabPage_TrackerProperties.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_TrackerProperties.Name = "TabPage_TrackerProperties"
        Me.TabPage_TrackerProperties.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_TrackerProperties.Size = New System.Drawing.Size(475, 135)
        Me.TabPage_TrackerProperties.TabIndex = 1
        Me.TabPage_TrackerProperties.Text = "Tracker Properties"
        Me.TabPage_TrackerProperties.UseVisualStyleBackColor = True
        '
        'ComboBox_ImageInterpolation
        '
        Me.ComboBox_ImageInterpolation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_ImageInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_ImageInterpolation.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_ImageInterpolation.FormattingEnabled = True
        Me.ComboBox_ImageInterpolation.Location = New System.Drawing.Point(152, 50)
        Me.ComboBox_ImageInterpolation.Name = "ComboBox_ImageInterpolation"
        Me.ComboBox_ImageInterpolation.Size = New System.Drawing.Size(317, 21)
        Me.ComboBox_ImageInterpolation.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 53)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Image interpolation:"
        '
        'CheckBox_FlipHorizontal
        '
        Me.CheckBox_FlipHorizontal.AutoSize = True
        Me.CheckBox_FlipHorizontal.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_FlipHorizontal.Location = New System.Drawing.Point(152, 29)
        Me.CheckBox_FlipHorizontal.Name = "CheckBox_FlipHorizontal"
        Me.CheckBox_FlipHorizontal.Size = New System.Drawing.Size(35, 18)
        Me.CheckBox_FlipHorizontal.TabIndex = 12
        Me.CheckBox_FlipHorizontal.Text = " "
        Me.CheckBox_FlipHorizontal.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 31)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Flip image horizontally:"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(246, 258)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(117, 23)
        Me.Button1.TabIndex = 22
        Me.Button1.Text = "Help"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox_Fps
        '
        Me.TextBox_Fps.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Fps.BackColor = System.Drawing.Color.White
        Me.TextBox_Fps.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_Fps.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.TextBox_Fps.Location = New System.Drawing.Point(504, 29)
        Me.TextBox_Fps.Name = "TextBox_Fps"
        Me.TextBox_Fps.ReadOnly = True
        Me.TextBox_Fps.Size = New System.Drawing.Size(209, 15)
        Me.TextBox_Fps.TabIndex = 23
        Me.TextBox_Fps.Text = "FPS: 0 / I/O FPS: 0"
        '
        'UCVirtualTrackerItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.TextBox_Fps)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.CheckBox_Autostart)
        Me.Controls.Add(Me.Button_ConfigSave)
        Me.Controls.Add(Me.Button_RestartDevice)
        Me.Controls.Add(Me.Button_Close)
        Me.Controls.Add(Me.Label_FriendlyName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox_CaptureImage)
        Me.Controls.Add(Me.CheckBox_ShowCaptureImage)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVirtualTrackerItem"
        Me.Size = New System.Drawing.Size(800, 297)
        CType(Me.PictureBox_CaptureImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_DeviceExposure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_DeviceGain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_DeviceGamma, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_DeviceConstrast, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage_DeviceProperties.ResumeLayout(False)
        Me.TabPage_DeviceProperties.PerformLayout()
        Me.TabPage_TrackerProperties.ResumeLayout(False)
        Me.TabPage_TrackerProperties.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox_CaptureImage As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label_FriendlyName As Label
    Friend WithEvents ComboBox_DeviceTrackerId As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TrackBar_DeviceExposure As TrackBar
    Friend WithEvents Label4 As Label
    Friend WithEvents TrackBar_DeviceGain As TrackBar
    Friend WithEvents Label6 As Label
    Friend WithEvents Button_Close As Label
    Friend WithEvents CheckBox_ShowCaptureImage As CheckBox
    Friend WithEvents TrackBar_DeviceGamma As TrackBar
    Friend WithEvents Label7 As Label
    Friend WithEvents Button_RestartDevice As Button
    Friend WithEvents TrackBar_DeviceConstrast As TrackBar
    Friend WithEvents Label8 As Label
    Friend WithEvents Button_ConfigSave As Button
    Friend WithEvents CheckBox_Autostart As CheckBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage_DeviceProperties As TabPage
    Friend WithEvents TabPage_TrackerProperties As TabPage
    Friend WithEvents CheckBox_FlipHorizontal As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox_ImageInterpolation As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox_Fps As TextBox
End Class
