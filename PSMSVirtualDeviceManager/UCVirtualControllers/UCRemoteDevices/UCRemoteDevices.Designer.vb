<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCRemoteDevices
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label_Port = New System.Windows.Forms.Label()
        Me.Panel_RemoteDevices = New System.Windows.Forms.Panel()
        Me.Button_StartSocket = New System.Windows.Forms.Button()
        Me.Timer_SocketCheck = New System.Windows.Forms.Timer(Me.components)
        Me.CheckBox_AllowNewDevices = New System.Windows.Forms.CheckBox()
        Me.LinkLabel_EditPort = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ReadMore = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label_Port
        '
        Me.Label_Port.AutoSize = True
        Me.Label_Port.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Port.Location = New System.Drawing.Point(16, 78)
        Me.Label_Port.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label_Port.Name = "Label_Port"
        Me.Label_Port.Size = New System.Drawing.Size(147, 13)
        Me.Label_Port.TabIndex = 0
        Me.Label_Port.Text = "Listening Socket Port: 0"
        '
        'Panel_RemoteDevices
        '
        Me.Panel_RemoteDevices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_RemoteDevices.AutoScroll = True
        Me.Panel_RemoteDevices.Location = New System.Drawing.Point(16, 115)
        Me.Panel_RemoteDevices.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_RemoteDevices.Name = "Panel_RemoteDevices"
        Me.Panel_RemoteDevices.Size = New System.Drawing.Size(768, 469)
        Me.Panel_RemoteDevices.TabIndex = 1
        '
        'Button_StartSocket
        '
        Me.Button_StartSocket.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_StartSocket.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_StartSocket.Location = New System.Drawing.Point(664, 73)
        Me.Button_StartSocket.Margin = New System.Windows.Forms.Padding(3, 3, 16, 3)
        Me.Button_StartSocket.Name = "Button_StartSocket"
        Me.Button_StartSocket.Size = New System.Drawing.Size(120, 23)
        Me.Button_StartSocket.TabIndex = 2
        Me.Button_StartSocket.Text = "Start Socket"
        Me.Button_StartSocket.UseVisualStyleBackColor = True
        '
        'Timer_SocketCheck
        '
        Me.Timer_SocketCheck.Enabled = True
        Me.Timer_SocketCheck.Interval = 1000
        '
        'CheckBox_AllowNewDevices
        '
        Me.CheckBox_AllowNewDevices.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_AllowNewDevices.AutoSize = True
        Me.CheckBox_AllowNewDevices.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_AllowNewDevices.Location = New System.Drawing.Point(532, 77)
        Me.CheckBox_AllowNewDevices.Name = "CheckBox_AllowNewDevices"
        Me.CheckBox_AllowNewDevices.Size = New System.Drawing.Size(126, 18)
        Me.CheckBox_AllowNewDevices.TabIndex = 3
        Me.CheckBox_AllowNewDevices.Text = "Allow new devcies"
        Me.CheckBox_AllowNewDevices.UseVisualStyleBackColor = True
        '
        'LinkLabel_EditPort
        '
        Me.LinkLabel_EditPort.AutoSize = True
        Me.LinkLabel_EditPort.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_EditPort.Location = New System.Drawing.Point(169, 78)
        Me.LinkLabel_EditPort.Name = "LinkLabel_EditPort"
        Me.LinkLabel_EditPort.Size = New System.Drawing.Size(52, 13)
        Me.LinkLabel_EditPort.TabIndex = 4
        Me.LinkLabel_EditPort.TabStop = True
        Me.LinkLabel_EditPort.Text = "Edit port"
        '
        'LinkLabel_ReadMore
        '
        Me.LinkLabel_ReadMore.AutoSize = True
        Me.LinkLabel_ReadMore.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_ReadMore.Location = New System.Drawing.Point(38, 49)
        Me.LinkLabel_ReadMore.Name = "LinkLabel_ReadMore"
        Me.LinkLabel_ReadMore.Size = New System.Drawing.Size(62, 13)
        Me.LinkLabel_ReadMore.TabIndex = 18
        Me.LinkLabel_ReadMore.TabStop = True
        Me.LinkLabel_ReadMore.Text = "Read more"
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Location = New System.Drawing.Point(38, 16)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 16, 16, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(746, 33)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Using custom remote devices with IMUs, you can override the default orientation d" &
    "ata of PSmove controllers and Virtual controllers within PSMoveServiceEx." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.PictureBox1.Location = New System.Drawing.Point(16, 16)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'UCRemoteDevices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.LinkLabel_ReadMore)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LinkLabel_EditPort)
        Me.Controls.Add(Me.CheckBox_AllowNewDevices)
        Me.Controls.Add(Me.Button_StartSocket)
        Me.Controls.Add(Me.Panel_RemoteDevices)
        Me.Controls.Add(Me.Label_Port)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCRemoteDevices"
        Me.Size = New System.Drawing.Size(800, 600)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label_Port As Label
    Friend WithEvents Panel_RemoteDevices As Panel
    Friend WithEvents Button_StartSocket As Button
    Friend WithEvents Timer_SocketCheck As Timer
    Friend WithEvents CheckBox_AllowNewDevices As CheckBox
    Friend WithEvents LinkLabel_EditPort As LinkLabel
    Friend WithEvents LinkLabel_ReadMore As LinkLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class
