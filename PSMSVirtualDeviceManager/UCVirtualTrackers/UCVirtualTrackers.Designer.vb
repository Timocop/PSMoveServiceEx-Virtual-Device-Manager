<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCVirtualTrackers
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
        Me.Panel_AvailableDevices = New System.Windows.Forms.Panel()
        Me.ListView_VideoDevices = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader_Id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_TrackerId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Name = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_HardwareId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip_VideoInputDevice = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_VideoReconnect = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_VideoRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Timer_VideoInputDevices = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel_AvailableDevices.SuspendLayout()
        Me.ContextMenuStrip_VideoInputDevice.SuspendLayout()
        Me.Panel8.SuspendLayout()
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
        Me.Label4.Text = "Create virtual trackers for optical tracking using any DirectShow-compatible vide" &
    "o input device." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
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
        Me.Panel_Devices.Location = New System.Drawing.Point(32, 334)
        Me.Panel_Devices.Margin = New System.Windows.Forms.Padding(32, 16, 32, 32)
        Me.Panel_Devices.Name = "Panel_Devices"
        Me.Panel_Devices.Size = New System.Drawing.Size(736, 569)
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
        'Panel_AvailableDevices
        '
        Me.Panel_AvailableDevices.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_AvailableDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_AvailableDevices.Controls.Add(Me.ListView_VideoDevices)
        Me.Panel_AvailableDevices.Controls.Add(Me.Panel8)
        Me.Panel_AvailableDevices.Location = New System.Drawing.Point(36, 167)
        Me.Panel_AvailableDevices.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_AvailableDevices.Name = "Panel_AvailableDevices"
        Me.Panel_AvailableDevices.Size = New System.Drawing.Size(728, 135)
        Me.Panel_AvailableDevices.TabIndex = 7
        '
        'ListView_VideoDevices
        '
        Me.ListView_VideoDevices.BackColor = System.Drawing.Color.White
        Me.ListView_VideoDevices.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView_VideoDevices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader_Id, Me.ColumnHeader_TrackerId, Me.ColumnHeader_Name, Me.ColumnHeader_HardwareId})
        Me.ListView_VideoDevices.ContextMenuStrip = Me.ContextMenuStrip_VideoInputDevice
        Me.ListView_VideoDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_VideoDevices.FullRowSelect = True
        Me.ListView_VideoDevices.HideSelection = False
        Me.ListView_VideoDevices.Location = New System.Drawing.Point(0, 42)
        Me.ListView_VideoDevices.MultiSelect = False
        Me.ListView_VideoDevices.Name = "ListView_VideoDevices"
        Me.ListView_VideoDevices.Size = New System.Drawing.Size(726, 91)
        Me.ListView_VideoDevices.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView_VideoDevices.TabIndex = 1
        Me.ListView_VideoDevices.UseCompatibleStateImageBehavior = False
        Me.ListView_VideoDevices.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader_Id
        '
        Me.ColumnHeader_Id.Text = "ID"
        Me.ColumnHeader_Id.Width = 50
        '
        'ColumnHeader_TrackerId
        '
        Me.ColumnHeader_TrackerId.Text = "Tracker ID"
        Me.ColumnHeader_TrackerId.Width = 100
        '
        'ColumnHeader_Name
        '
        Me.ColumnHeader_Name.Text = "Name"
        Me.ColumnHeader_Name.Width = 250
        '
        'ColumnHeader_HardwareId
        '
        Me.ColumnHeader_HardwareId.Text = "Hardware ID"
        Me.ColumnHeader_HardwareId.Width = 300
        '
        'ContextMenuStrip_VideoInputDevice
        '
        Me.ContextMenuStrip_VideoInputDevice.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_VideoReconnect, Me.ToolStripMenuItem_VideoRemove})
        Me.ContextMenuStrip_VideoInputDevice.Name = "ContextMenuStrip_Trackers"
        Me.ContextMenuStrip_VideoInputDevice.Size = New System.Drawing.Size(131, 48)
        '
        'ToolStripMenuItem_VideoReconnect
        '
        Me.ToolStripMenuItem_VideoReconnect.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.DevicePairing_6101_16x16_32
        Me.ToolStripMenuItem_VideoReconnect.Name = "ToolStripMenuItem_VideoReconnect"
        Me.ToolStripMenuItem_VideoReconnect.Size = New System.Drawing.Size(130, 22)
        Me.ToolStripMenuItem_VideoReconnect.Text = "Reconnect"
        '
        'ToolStripMenuItem_VideoRemove
        '
        Me.ToolStripMenuItem_VideoRemove.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.ToolStripMenuItem_VideoRemove.Name = "ToolStripMenuItem_VideoRemove"
        Me.ToolStripMenuItem_VideoRemove.Size = New System.Drawing.Size(130, 22)
        Me.ToolStripMenuItem_VideoRemove.Text = "Remove"
        '
        'Panel8
        '
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.Label12)
        Me.Panel8.Controls.Add(Me.Panel10)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(726, 42)
        Me.Panel8.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Navy
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label12.Size = New System.Drawing.Size(726, 41)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Available Video Input Devices"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Gray
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel10.Location = New System.Drawing.Point(0, 41)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(726, 1)
        Me.Panel10.TabIndex = 0
        '
        'Timer_VideoInputDevices
        '
        Me.Timer_VideoInputDevices.Enabled = True
        Me.Timer_VideoInputDevices.Interval = 500
        '
        'UCVirtualTrackers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel_AvailableDevices)
        Me.Controls.Add(Me.ComboBox_VirtualTrackerCount)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel_Devices)
        Me.Controls.Add(Me.Button_DeviceAdd)
        Me.Controls.Add(Me.ComboBox_Devices)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVirtualTrackers"
        Me.Size = New System.Drawing.Size(800, 935)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel_AvailableDevices.ResumeLayout(False)
        Me.ContextMenuStrip_VideoInputDevice.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
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
    Friend WithEvents Panel_AvailableDevices As Panel
    Friend WithEvents ListView_VideoDevices As ClassListViewEx
    Friend WithEvents ColumnHeader_Id As ColumnHeader
    Friend WithEvents ColumnHeader_Name As ColumnHeader
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents ColumnHeader_HardwareId As ColumnHeader
    Friend WithEvents Timer_VideoInputDevices As Timer
    Friend WithEvents ContextMenuStrip_VideoInputDevice As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_VideoRemove As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_VideoReconnect As ToolStripMenuItem
    Friend WithEvents ColumnHeader_TrackerId As ColumnHeader
End Class
