<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCVirtualTrackers
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox_Devices = New System.Windows.Forms.ComboBox()
        Me.Panel_Devices = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox_VirtualTrackerCount = New System.Windows.Forms.ComboBox()
        Me.Button_DeviceAdd = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 64)
        Me.Panel1.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources._466
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.m_HighQuality = True
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(57, 57)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(66, 30)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(731, 30)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Create virtual trackers for PSMoveService using any DirectShow video input device" &
    " other than playstation eyes."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(66, 3)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 21)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Virtual Trackers"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(0, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(800, 1)
        Me.Label1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 118)
        Me.Label3.Margin = New System.Windows.Forms.Padding(32, 16, 6, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(158, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Available video input devices:"
        '
        'ComboBox_Devices
        '
        Me.ComboBox_Devices.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Devices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Devices.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_Devices.FormattingEnabled = True
        Me.ComboBox_Devices.Location = New System.Drawing.Point(199, 115)
        Me.ComboBox_Devices.Name = "ComboBox_Devices"
        Me.ComboBox_Devices.Size = New System.Drawing.Size(488, 21)
        Me.ComboBox_Devices.TabIndex = 2
        '
        'Panel_Devices
        '
        Me.Panel_Devices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Devices.AutoScroll = True
        Me.Panel_Devices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Devices.Location = New System.Drawing.Point(32, 169)
        Me.Panel_Devices.Margin = New System.Windows.Forms.Padding(32)
        Me.Panel_Devices.Name = "Panel_Devices"
        Me.Panel_Devices.Size = New System.Drawing.Size(736, 384)
        Me.Panel_Devices.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(32, 83)
        Me.Label5.Margin = New System.Windows.Forms.Padding(32, 16, 6, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(115, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Virtual tracker count:"
        '
        'ComboBox_VirtualTrackerCount
        '
        Me.ComboBox_VirtualTrackerCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_VirtualTrackerCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_VirtualTrackerCount.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_VirtualTrackerCount.FormattingEnabled = True
        Me.ComboBox_VirtualTrackerCount.Location = New System.Drawing.Point(199, 80)
        Me.ComboBox_VirtualTrackerCount.Name = "ComboBox_VirtualTrackerCount"
        Me.ComboBox_VirtualTrackerCount.Size = New System.Drawing.Size(488, 21)
        Me.ComboBox_VirtualTrackerCount.TabIndex = 6
        '
        'Button_DeviceAdd
        '
        Me.Button_DeviceAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_DeviceAdd.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.DevicePairing_6101_16x16_32
        Me.Button_DeviceAdd.Location = New System.Drawing.Point(693, 113)
        Me.Button_DeviceAdd.Margin = New System.Windows.Forms.Padding(3, 3, 32, 3)
        Me.Button_DeviceAdd.Name = "Button_DeviceAdd"
        Me.Button_DeviceAdd.Size = New System.Drawing.Size(75, 23)
        Me.Button_DeviceAdd.TabIndex = 3
        Me.Button_DeviceAdd.Text = "Add"
        Me.Button_DeviceAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_DeviceAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_DeviceAdd.UseVisualStyleBackColor = True
        '
        'UCVirtualTrackers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.ComboBox_VirtualTrackerCount)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel_Devices)
        Me.Controls.Add(Me.Button_DeviceAdd)
        Me.Controls.Add(Me.ComboBox_Devices)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVirtualTrackers"
        Me.Size = New System.Drawing.Size(800, 585)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox_Devices As ComboBox
    Friend WithEvents Button_DeviceAdd As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel_Devices As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents ComboBox_VirtualTrackerCount As ComboBox
    Friend WithEvents PictureBox1 As ClassPictureBoxQuality
End Class
