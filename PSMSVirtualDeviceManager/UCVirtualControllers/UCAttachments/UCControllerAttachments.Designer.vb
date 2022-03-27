<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCControllerAttachments
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
        Me.Button_AddAttachment = New System.Windows.Forms.Button()
        Me.Panel_Attachments = New System.Windows.Forms.Panel()
        Me.ContextMenuStrip_Autostart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Button_Autostart = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LinkLabel_ReadMore = New System.Windows.Forms.LinkLabel()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button_AddAttachment
        '
        Me.Button_AddAttachment.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_AddAttachment.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_AddAttachment.Location = New System.Drawing.Point(664, 78)
        Me.Button_AddAttachment.Margin = New System.Windows.Forms.Padding(3, 3, 16, 3)
        Me.Button_AddAttachment.Name = "Button_AddAttachment"
        Me.Button_AddAttachment.Size = New System.Drawing.Size(120, 23)
        Me.Button_AddAttachment.TabIndex = 8
        Me.Button_AddAttachment.Text = "Add attachment"
        Me.Button_AddAttachment.UseVisualStyleBackColor = True
        '
        'Panel_Attachments
        '
        Me.Panel_Attachments.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Attachments.AutoScroll = True
        Me.Panel_Attachments.Location = New System.Drawing.Point(16, 120)
        Me.Panel_Attachments.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_Attachments.Name = "Panel_Attachments"
        Me.Panel_Attachments.Size = New System.Drawing.Size(768, 464)
        Me.Panel_Attachments.TabIndex = 7
        '
        'ContextMenuStrip_Autostart
        '
        Me.ContextMenuStrip_Autostart.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip_Autostart.Size = New System.Drawing.Size(61, 4)
        '
        'Button_Autostart
        '
        Me.Button_Autostart.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_Autostart.Location = New System.Drawing.Point(16, 78)
        Me.Button_Autostart.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Button_Autostart.Name = "Button_Autostart"
        Me.Button_Autostart.Size = New System.Drawing.Size(162, 23)
        Me.Button_Autostart.TabIndex = 12
        Me.Button_Autostart.Text = "Autostart attachments..."
        Me.Button_Autostart.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.PictureBox1.Location = New System.Drawing.Point(16, 16)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Location = New System.Drawing.Point(38, 16)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 16, 16, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(746, 33)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "You can attach controllers to each other, creating joint-based trackers which do " &
    "not require LEDs to track but still can provide enough tracking information."
        '
        'LinkLabel_ReadMore
        '
        Me.LinkLabel_ReadMore.AutoSize = True
        Me.LinkLabel_ReadMore.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_ReadMore.Location = New System.Drawing.Point(38, 49)
        Me.LinkLabel_ReadMore.Name = "LinkLabel_ReadMore"
        Me.LinkLabel_ReadMore.Size = New System.Drawing.Size(62, 13)
        Me.LinkLabel_ReadMore.TabIndex = 15
        Me.LinkLabel_ReadMore.TabStop = True
        Me.LinkLabel_ReadMore.Text = "Read more"
        '
        'UCControllerAttachments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.LinkLabel_ReadMore)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button_Autostart)
        Me.Controls.Add(Me.Button_AddAttachment)
        Me.Controls.Add(Me.Panel_Attachments)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCControllerAttachments"
        Me.Size = New System.Drawing.Size(800, 600)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_AddAttachment As Button
    Friend WithEvents Panel_Attachments As Panel
    Friend WithEvents ContextMenuStrip_Autostart As ContextMenuStrip
    Friend WithEvents Button_Autostart As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LinkLabel_ReadMore As LinkLabel
End Class
