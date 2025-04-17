<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCInformation
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
        Me.Panel_Color = New System.Windows.Forms.Panel()
        Me.Label_Text = New System.Windows.Forms.Label()
        Me.LinkLabel_MoreInfo = New System.Windows.Forms.LinkLabel()
        Me.ClassPictureBox_Icon = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        CType(Me.ClassPictureBox_Icon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel_Color
        '
        Me.Panel_Color.BackColor = System.Drawing.Color.RoyalBlue
        Me.Panel_Color.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel_Color.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Color.Name = "Panel_Color"
        Me.Panel_Color.Size = New System.Drawing.Size(3, 55)
        Me.Panel_Color.TabIndex = 0
        '
        'Label_Text
        '
        Me.Label_Text.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_Text.ForeColor = System.Drawing.Color.Black
        Me.Label_Text.Location = New System.Drawing.Point(31, 3)
        Me.Label_Text.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Text.Name = "Label_Text"
        Me.Label_Text.Size = New System.Drawing.Size(557, 49)
        Me.Label_Text.TabIndex = 2
        Me.Label_Text.Text = "Information Text"
        '
        'LinkLabel_MoreInfo
        '
        Me.LinkLabel_MoreInfo.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_MoreInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel_MoreInfo.AutoSize = True
        Me.LinkLabel_MoreInfo.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_MoreInfo.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_MoreInfo.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_MoreInfo.Location = New System.Drawing.Point(31, 39)
        Me.LinkLabel_MoreInfo.Margin = New System.Windows.Forms.Padding(3)
        Me.LinkLabel_MoreInfo.Name = "LinkLabel_MoreInfo"
        Me.LinkLabel_MoreInfo.Size = New System.Drawing.Size(62, 13)
        Me.LinkLabel_MoreInfo.TabIndex = 22
        Me.LinkLabel_MoreInfo.TabStop = True
        Me.LinkLabel_MoreInfo.Text = "Read more"
        Me.LinkLabel_MoreInfo.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'ClassPictureBox_Icon
        '
        Me.ClassPictureBox_Icon.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.ClassPictureBox_Icon.Location = New System.Drawing.Point(9, 3)
        Me.ClassPictureBox_Icon.m_HighQuality = True
        Me.ClassPictureBox_Icon.Name = "ClassPictureBox_Icon"
        Me.ClassPictureBox_Icon.Size = New System.Drawing.Size(16, 16)
        Me.ClassPictureBox_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBox_Icon.TabIndex = 1
        Me.ClassPictureBox_Icon.TabStop = False
        '
        'UCInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.LinkLabel_MoreInfo)
        Me.Controls.Add(Me.Label_Text)
        Me.Controls.Add(Me.ClassPictureBox_Icon)
        Me.Controls.Add(Me.Panel_Color)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCInformation"
        Me.Size = New System.Drawing.Size(591, 55)
        CType(Me.ClassPictureBox_Icon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel_Color As Panel
    Friend WithEvents ClassPictureBox_Icon As ClassPictureBoxQuality
    Friend WithEvents Label_Text As Label
    Friend WithEvents LinkLabel_MoreInfo As LinkLabel
End Class
