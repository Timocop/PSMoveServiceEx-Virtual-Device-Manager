<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.Panel_Pages = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label_Version = New System.Windows.Forms.Label()
        Me.Button_RestartPSMS = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LinkLabel_Controllers = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Pages
        '
        Me.Panel_Pages.AutoScroll = True
        Me.Panel_Pages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Pages.Location = New System.Drawing.Point(197, 0)
        Me.Panel_Pages.Name = "Panel_Pages"
        Me.Panel_Pages.Size = New System.Drawing.Size(858, 697)
        Me.Panel_Pages.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.Controls.Add(Me.Label_Version)
        Me.Panel2.Controls.Add(Me.Button_RestartPSMS)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 697)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1055, 64)
        Me.Panel2.TabIndex = 2
        '
        'Label_Version
        '
        Me.Label_Version.AutoSize = True
        Me.Label_Version.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_Version.Location = New System.Drawing.Point(12, 26)
        Me.Label_Version.Name = "Label_Version"
        Me.Label_Version.Size = New System.Drawing.Size(66, 13)
        Me.Label_Version.TabIndex = 2
        Me.Label_Version.Text = "Version: 1.0"
        '
        'Button_RestartPSMS
        '
        Me.Button_RestartPSMS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_RestartPSMS.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_RestartPSMS.Location = New System.Drawing.Point(893, 16)
        Me.Button_RestartPSMS.Margin = New System.Windows.Forms.Padding(3, 16, 3, 16)
        Me.Button_RestartPSMS.Name = "Button_RestartPSMS"
        Me.Button_RestartPSMS.Size = New System.Drawing.Size(150, 32)
        Me.Button_RestartPSMS.TabIndex = 1
        Me.Button_RestartPSMS.Text = "Restart PSMoveService"
        Me.Button_RestartPSMS.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1055, 1)
        Me.Label1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.LinkLabel2)
        Me.Panel1.Controls.Add(Me.LinkLabel1)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.LinkLabel_Controllers)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(197, 697)
        Me.Panel1.TabIndex = 2
        '
        'LinkLabel2
        '
        Me.LinkLabel2.ActiveLinkColor = System.Drawing.Color.Black
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel2.Location = New System.Drawing.Point(17, 144)
        Me.LinkLabel2.Margin = New System.Windows.Forms.Padding(8, 8, 3, 0)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(130, 13)
        Me.LinkLabel2.TabIndex = 2
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Manage Virtual Trackers"
        Me.LinkLabel2.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'LinkLabel1
        '
        Me.LinkLabel1.ActiveLinkColor = System.Drawing.Color.Black
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel1.Location = New System.Drawing.Point(17, 107)
        Me.LinkLabel1.Margin = New System.Windows.Forms.Padding(8, 8, 3, 3)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(113, 26)
        Me.LinkLabel1.TabIndex = 1
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Manage Virtual" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Head Mount Devices"
        Me.LinkLabel1.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(196, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1, 697)
        Me.Panel3.TabIndex = 0
        '
        'LinkLabel_Controllers
        '
        Me.LinkLabel_Controllers.ActiveLinkColor = System.Drawing.Color.Black
        Me.LinkLabel_Controllers.AutoSize = True
        Me.LinkLabel_Controllers.ForeColor = System.Drawing.Color.Navy
        Me.LinkLabel_Controllers.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel_Controllers.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel_Controllers.Location = New System.Drawing.Point(17, 86)
        Me.LinkLabel_Controllers.Margin = New System.Windows.Forms.Padding(8, 32, 3, 0)
        Me.LinkLabel_Controllers.Name = "LinkLabel_Controllers"
        Me.LinkLabel_Controllers.Size = New System.Drawing.Size(146, 13)
        Me.LinkLabel_Controllers.TabIndex = 0
        Me.LinkLabel_Controllers.TabStop = True
        Me.LinkLabel_Controllers.Text = "Manage Virtual Controllers"
        Me.LinkLabel_Controllers.VisitedLinkColor = System.Drawing.Color.Navy
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(17, 41)
        Me.Label2.Margin = New System.Windows.Forms.Padding(8, 32, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(144, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Virtual Devices Navigation"
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1055, 761)
        Me.Controls.Add(Me.Panel_Pages)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PSMoveServiceEx - Virtual Device Manager"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Pages As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Button_RestartPSMS As Button
    Friend WithEvents Label_Version As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents LinkLabel_Controllers As LinkLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents LinkLabel2 As LinkLabel
End Class
