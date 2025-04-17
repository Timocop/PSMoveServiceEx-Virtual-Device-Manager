<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCRemoteDevices
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
        Me.components = New System.ComponentModel.Container()
        Me.Label_Port = New System.Windows.Forms.Label()
        Me.Panel_RemoteDevices = New System.Windows.Forms.Panel()
        Me.Button_StartSocket = New System.Windows.Forms.Button()
        Me.Timer_SocketCheck = New System.Windows.Forms.Timer(Me.components)
        Me.CheckBox_AllowNewDevices = New System.Windows.Forms.CheckBox()
        Me.LinkLabel_EditPort = New System.Windows.Forms.LinkLabel()
        Me.Label_ConnectedDevices = New System.Windows.Forms.Label()
        Me.ListView_RemoteDevices = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Timer_RemoteDevices = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_AvailableRemoteDevices = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UcInformation1 = New PSMSVirtualDeviceManager.UCInformation()
        Me.Panel_AvailableRemoteDevices.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label_Port
        '
        Me.Label_Port.AutoSize = True
        Me.Label_Port.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Port.Location = New System.Drawing.Point(16, 76)
        Me.Label_Port.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label_Port.Name = "Label_Port"
        Me.Label_Port.Size = New System.Drawing.Size(104, 13)
        Me.Label_Port.TabIndex = 0
        Me.Label_Port.Text = "Listening Socket: 0"
        '
        'Panel_RemoteDevices
        '
        Me.Panel_RemoteDevices.AutoSize = True
        Me.Panel_RemoteDevices.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel_RemoteDevices.Location = New System.Drawing.Point(0, 297)
        Me.Panel_RemoteDevices.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_RemoteDevices.MinimumSize = New System.Drawing.Size(0, 32)
        Me.Panel_RemoteDevices.Name = "Panel_RemoteDevices"
        Me.Panel_RemoteDevices.Padding = New System.Windows.Forms.Padding(16, 0, 16, 0)
        Me.Panel_RemoteDevices.Size = New System.Drawing.Size(800, 32)
        Me.Panel_RemoteDevices.TabIndex = 1
        '
        'Button_StartSocket
        '
        Me.Button_StartSocket.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_StartSocket.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.Button_StartSocket.Location = New System.Drawing.Point(664, 82)
        Me.Button_StartSocket.Margin = New System.Windows.Forms.Padding(3, 3, 16, 16)
        Me.Button_StartSocket.Name = "Button_StartSocket"
        Me.Button_StartSocket.Size = New System.Drawing.Size(120, 23)
        Me.Button_StartSocket.TabIndex = 2
        Me.Button_StartSocket.Text = "Start Socket"
        Me.Button_StartSocket.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_StartSocket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
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
        Me.CheckBox_AllowNewDevices.Location = New System.Drawing.Point(532, 85)
        Me.CheckBox_AllowNewDevices.Name = "CheckBox_AllowNewDevices"
        Me.CheckBox_AllowNewDevices.Size = New System.Drawing.Size(126, 18)
        Me.CheckBox_AllowNewDevices.TabIndex = 3
        Me.CheckBox_AllowNewDevices.Text = "Allow new devices"
        Me.CheckBox_AllowNewDevices.UseVisualStyleBackColor = True
        '
        'LinkLabel_EditPort
        '
        Me.LinkLabel_EditPort.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_EditPort.AutoSize = True
        Me.LinkLabel_EditPort.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_EditPort.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_EditPort.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_EditPort.Location = New System.Drawing.Point(219, 76)
        Me.LinkLabel_EditPort.Name = "LinkLabel_EditPort"
        Me.LinkLabel_EditPort.Size = New System.Drawing.Size(52, 13)
        Me.LinkLabel_EditPort.TabIndex = 4
        Me.LinkLabel_EditPort.TabStop = True
        Me.LinkLabel_EditPort.Text = "Edit port"
        Me.LinkLabel_EditPort.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label_ConnectedDevices
        '
        Me.Label_ConnectedDevices.AutoSize = True
        Me.Label_ConnectedDevices.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_ConnectedDevices.Location = New System.Drawing.Point(16, 92)
        Me.Label_ConnectedDevices.Margin = New System.Windows.Forms.Padding(16, 3, 3, 16)
        Me.Label_ConnectedDevices.Name = "Label_ConnectedDevices"
        Me.Label_ConnectedDevices.Size = New System.Drawing.Size(116, 13)
        Me.Label_ConnectedDevices.TabIndex = 19
        Me.Label_ConnectedDevices.Text = "Connected devices: 0"
        '
        'ListView_RemoteDevices
        '
        Me.ListView_RemoteDevices.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView_RemoteDevices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.ListView_RemoteDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_RemoteDevices.FullRowSelect = True
        Me.ListView_RemoteDevices.HideSelection = False
        Me.ListView_RemoteDevices.Location = New System.Drawing.Point(0, 42)
        Me.ListView_RemoteDevices.Margin = New System.Windows.Forms.Padding(16)
        Me.ListView_RemoteDevices.MultiSelect = False
        Me.ListView_RemoteDevices.Name = "ListView_RemoteDevices"
        Me.ListView_RemoteDevices.Size = New System.Drawing.Size(766, 114)
        Me.ListView_RemoteDevices.TabIndex = 20
        Me.ListView_RemoteDevices.UseCompatibleStateImageBehavior = False
        Me.ListView_RemoteDevices.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Device Name"
        Me.ColumnHeader1.Width = 400
        '
        'Timer_RemoteDevices
        '
        Me.Timer_RemoteDevices.Enabled = True
        Me.Timer_RemoteDevices.Interval = 500
        '
        'Panel_AvailableRemoteDevices
        '
        Me.Panel_AvailableRemoteDevices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_AvailableRemoteDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_AvailableRemoteDevices.Controls.Add(Me.ListView_RemoteDevices)
        Me.Panel_AvailableRemoteDevices.Controls.Add(Me.Panel8)
        Me.Panel_AvailableRemoteDevices.Location = New System.Drawing.Point(16, 123)
        Me.Panel_AvailableRemoteDevices.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_AvailableRemoteDevices.Name = "Panel_AvailableRemoteDevices"
        Me.Panel_AvailableRemoteDevices.Size = New System.Drawing.Size(768, 158)
        Me.Panel_AvailableRemoteDevices.TabIndex = 21
        '
        'Panel8
        '
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.Label12)
        Me.Panel8.Controls.Add(Me.Panel10)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(766, 42)
        Me.Panel8.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Navy
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label12.Size = New System.Drawing.Size(766, 41)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Available Remote Devices"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Gray
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel10.Location = New System.Drawing.Point(0, 41)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(766, 1)
        Me.Panel10.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UcInformation1)
        Me.Panel1.Controls.Add(Me.Panel_AvailableRemoteDevices)
        Me.Panel1.Controls.Add(Me.Label_Port)
        Me.Panel1.Controls.Add(Me.Label_ConnectedDevices)
        Me.Panel1.Controls.Add(Me.Button_StartSocket)
        Me.Panel1.Controls.Add(Me.CheckBox_AllowNewDevices)
        Me.Panel1.Controls.Add(Me.LinkLabel_EditPort)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 297)
        Me.Panel1.TabIndex = 22
        '
        'UcInformation1
        '
        Me.UcInformation1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcInformation1.BackColor = System.Drawing.Color.White
        Me.UcInformation1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcInformation1.Location = New System.Drawing.Point(16, 16)
        Me.UcInformation1.m_InfoType = PSMSVirtualDeviceManager.UCInformation.ENUM_INFO_TYPE.INFORMATION
        Me.UcInformation1.m_ReadMoreAction = Nothing
        Me.UcInformation1.m_ReadMoreText = "Read more"
        Me.UcInformation1.m_Text = "With custom remote devices equipped with IMUs, you can modify the orientation dat" &
    "a and inputs of controllers." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.UcInformation1.Margin = New System.Windows.Forms.Padding(16, 16, 16, 3)
        Me.UcInformation1.Name = "UcInformation1"
        Me.UcInformation1.Size = New System.Drawing.Size(768, 41)
        Me.UcInformation1.TabIndex = 23
        '
        'UCRemoteDevices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel_RemoteDevices)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCRemoteDevices"
        Me.Size = New System.Drawing.Size(800, 600)
        Me.Panel_AvailableRemoteDevices.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label_Port As Label
    Friend WithEvents Panel_RemoteDevices As Panel
    Friend WithEvents Button_StartSocket As Button
    Friend WithEvents Timer_SocketCheck As Timer
    Friend WithEvents CheckBox_AllowNewDevices As CheckBox
    Friend WithEvents LinkLabel_EditPort As LinkLabel
    Friend WithEvents Label_ConnectedDevices As Label
    Friend WithEvents ListView_RemoteDevices As ClassListViewEx
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents Timer_RemoteDevices As Timer
    Friend WithEvents Panel_AvailableRemoteDevices As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents UcInformation1 As UCInformation
End Class
