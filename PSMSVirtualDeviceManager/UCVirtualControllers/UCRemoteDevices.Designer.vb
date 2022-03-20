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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel_RemoteDevices = New System.Windows.Forms.Panel()
        Me.Button_StartSocket = New System.Windows.Forms.Button()
        Me.Timer_SocketCheck = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 16)
        Me.Label1.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(147, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Listening Socket Port: 6970"
        '
        'Panel_RemoteDevices
        '
        Me.Panel_RemoteDevices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_RemoteDevices.AutoScroll = True
        Me.Panel_RemoteDevices.Location = New System.Drawing.Point(16, 45)
        Me.Panel_RemoteDevices.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_RemoteDevices.Name = "Panel_RemoteDevices"
        Me.Panel_RemoteDevices.Size = New System.Drawing.Size(830, 483)
        Me.Panel_RemoteDevices.TabIndex = 1
        '
        'Button_StartSocket
        '
        Me.Button_StartSocket.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_StartSocket.Location = New System.Drawing.Point(726, 11)
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
        'UCRemoteDevices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Button_StartSocket)
        Me.Controls.Add(Me.Panel_RemoteDevices)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCRemoteDevices"
        Me.Size = New System.Drawing.Size(862, 544)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Panel_RemoteDevices As Panel
    Friend WithEvents Button_StartSocket As Button
    Friend WithEvents Timer_SocketCheck As Timer
End Class
