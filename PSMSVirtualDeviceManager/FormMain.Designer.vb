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
        Me.MenuStrip_StartPage = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem_File = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_FileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_VirtualDevices = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_VDControllers = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_VDHeadMountDevices = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_VDTrackers = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel_Pages = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button_RestartPSMS = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label_Version = New System.Windows.Forms.Label()
        Me.MenuStrip_StartPage.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip_StartPage
        '
        Me.MenuStrip_StartPage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_File, Me.ToolStripMenuItem_VirtualDevices})
        Me.MenuStrip_StartPage.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip_StartPage.Name = "MenuStrip_StartPage"
        Me.MenuStrip_StartPage.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip_StartPage.Size = New System.Drawing.Size(784, 24)
        Me.MenuStrip_StartPage.TabIndex = 0
        Me.MenuStrip_StartPage.Text = "MenuStrip1"
        '
        'ToolStripMenuItem_File
        '
        Me.ToolStripMenuItem_File.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_FileExit})
        Me.ToolStripMenuItem_File.Name = "ToolStripMenuItem_File"
        Me.ToolStripMenuItem_File.Size = New System.Drawing.Size(37, 20)
        Me.ToolStripMenuItem_File.Text = "File"
        '
        'ToolStripMenuItem_FileExit
        '
        Me.ToolStripMenuItem_FileExit.Name = "ToolStripMenuItem_FileExit"
        Me.ToolStripMenuItem_FileExit.Size = New System.Drawing.Size(93, 22)
        Me.ToolStripMenuItem_FileExit.Text = "Exit"
        '
        'ToolStripMenuItem_VirtualDevices
        '
        Me.ToolStripMenuItem_VirtualDevices.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_VDControllers, Me.ToolStripMenuItem_VDHeadMountDevices, Me.ToolStripMenuItem_VDTrackers})
        Me.ToolStripMenuItem_VirtualDevices.Name = "ToolStripMenuItem_VirtualDevices"
        Me.ToolStripMenuItem_VirtualDevices.Size = New System.Drawing.Size(96, 20)
        Me.ToolStripMenuItem_VirtualDevices.Text = "Virtual Devices"
        '
        'ToolStripMenuItem_VDControllers
        '
        Me.ToolStripMenuItem_VDControllers.Name = "ToolStripMenuItem_VDControllers"
        Me.ToolStripMenuItem_VDControllers.Size = New System.Drawing.Size(184, 22)
        Me.ToolStripMenuItem_VDControllers.Text = "Controllers"
        '
        'ToolStripMenuItem_VDHeadMountDevices
        '
        Me.ToolStripMenuItem_VDHeadMountDevices.Name = "ToolStripMenuItem_VDHeadMountDevices"
        Me.ToolStripMenuItem_VDHeadMountDevices.Size = New System.Drawing.Size(184, 22)
        Me.ToolStripMenuItem_VDHeadMountDevices.Text = "Head Mount Devices"
        '
        'ToolStripMenuItem_VDTrackers
        '
        Me.ToolStripMenuItem_VDTrackers.Name = "ToolStripMenuItem_VDTrackers"
        Me.ToolStripMenuItem_VDTrackers.Size = New System.Drawing.Size(184, 22)
        Me.ToolStripMenuItem_VDTrackers.Text = "Trackers"
        '
        'Panel_Pages
        '
        Me.Panel_Pages.AutoScroll = True
        Me.Panel_Pages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Pages.Location = New System.Drawing.Point(0, 24)
        Me.Panel_Pages.Name = "Panel_Pages"
        Me.Panel_Pages.Size = New System.Drawing.Size(784, 473)
        Me.Panel_Pages.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.Controls.Add(Me.Label_Version)
        Me.Panel2.Controls.Add(Me.Button_RestartPSMS)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 497)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(784, 64)
        Me.Panel2.TabIndex = 2
        '
        'Button_RestartPSMS
        '
        Me.Button_RestartPSMS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_RestartPSMS.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_RestartPSMS.Location = New System.Drawing.Point(622, 16)
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
        Me.Label1.Size = New System.Drawing.Size(784, 1)
        Me.Label1.TabIndex = 0
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
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.Panel_Pages)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.MenuStrip_StartPage)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip_StartPage
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PSMoveServiceEx - Virtual Device Manager"
        Me.MenuStrip_StartPage.ResumeLayout(False)
        Me.MenuStrip_StartPage.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip_StartPage As MenuStrip
    Friend WithEvents ToolStripMenuItem_File As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_FileExit As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_VirtualDevices As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_VDControllers As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_VDHeadMountDevices As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_VDTrackers As ToolStripMenuItem
    Friend WithEvents Panel_Pages As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Button_RestartPSMS As Button
    Friend WithEvents Label_Version As Label
End Class
