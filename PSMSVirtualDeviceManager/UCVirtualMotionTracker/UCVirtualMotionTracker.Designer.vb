<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCVirtualMotionTracker
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
        Me.LinkLabel_ReadMore = New System.Windows.Forms.LinkLabel()
        Me.TabControl_Vmt = New System.Windows.Forms.TabControl()
        Me.TabPage_Management = New System.Windows.Forms.TabPage()
        Me.TabPage_Trackers = New System.Windows.Forms.TabPage()
        Me.TabPage_Settings = New System.Windows.Forms.TabPage()
        Me.TabPage_PlayspaceCalib = New System.Windows.Forms.TabPage()
        Me.TabPage_Overrides = New System.Windows.Forms.TabPage()
        Me.ToolTip_Info = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer_VMTTrackers = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ClassPictureBoxQuality4 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.ToolTip_Default = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControl_Vmt.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.ClassPictureBoxQuality4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LinkLabel_ReadMore
        '
        Me.LinkLabel_ReadMore.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ReadMore.AutoSize = True
        Me.LinkLabel_ReadMore.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ReadMore.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ReadMore.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ReadMore.Location = New System.Drawing.Point(67, 60)
        Me.LinkLabel_ReadMore.Margin = New System.Windows.Forms.Padding(3, 0, 3, 8)
        Me.LinkLabel_ReadMore.Name = "LinkLabel_ReadMore"
        Me.LinkLabel_ReadMore.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.LinkLabel_ReadMore.Size = New System.Drawing.Size(62, 16)
        Me.LinkLabel_ReadMore.TabIndex = 21
        Me.LinkLabel_ReadMore.TabStop = True
        Me.LinkLabel_ReadMore.Text = "Read more"
        Me.LinkLabel_ReadMore.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'TabControl_Vmt
        '
        Me.TabControl_Vmt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl_Vmt.Controls.Add(Me.TabPage_Management)
        Me.TabControl_Vmt.Controls.Add(Me.TabPage_Trackers)
        Me.TabControl_Vmt.Controls.Add(Me.TabPage_Settings)
        Me.TabControl_Vmt.Controls.Add(Me.TabPage_PlayspaceCalib)
        Me.TabControl_Vmt.Controls.Add(Me.TabPage_Overrides)
        Me.TabControl_Vmt.Location = New System.Drawing.Point(16, 96)
        Me.TabControl_Vmt.Margin = New System.Windows.Forms.Padding(16, 8, 16, 16)
        Me.TabControl_Vmt.Name = "TabControl_Vmt"
        Me.TabControl_Vmt.SelectedIndex = 0
        Me.TabControl_Vmt.Size = New System.Drawing.Size(768, 488)
        Me.TabControl_Vmt.TabIndex = 22
        '
        'TabPage_Management
        '
        Me.TabPage_Management.BackColor = System.Drawing.Color.White
        Me.TabPage_Management.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Management.Name = "TabPage_Management"
        Me.TabPage_Management.Size = New System.Drawing.Size(760, 462)
        Me.TabPage_Management.TabIndex = 4
        Me.TabPage_Management.Text = "Management"
        '
        'TabPage_Trackers
        '
        Me.TabPage_Trackers.BackColor = System.Drawing.Color.White
        Me.TabPage_Trackers.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Trackers.Name = "TabPage_Trackers"
        Me.TabPage_Trackers.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Trackers.Size = New System.Drawing.Size(760, 454)
        Me.TabPage_Trackers.TabIndex = 0
        Me.TabPage_Trackers.Text = "Trackers"
        '
        'TabPage_Settings
        '
        Me.TabPage_Settings.BackColor = System.Drawing.Color.White
        Me.TabPage_Settings.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Settings.Name = "TabPage_Settings"
        Me.TabPage_Settings.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Settings.Size = New System.Drawing.Size(760, 454)
        Me.TabPage_Settings.TabIndex = 3
        Me.TabPage_Settings.Text = "Settings"
        '
        'TabPage_PlayspaceCalib
        '
        Me.TabPage_PlayspaceCalib.BackColor = System.Drawing.Color.White
        Me.TabPage_PlayspaceCalib.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_PlayspaceCalib.Name = "TabPage_PlayspaceCalib"
        Me.TabPage_PlayspaceCalib.Size = New System.Drawing.Size(760, 454)
        Me.TabPage_PlayspaceCalib.TabIndex = 5
        Me.TabPage_PlayspaceCalib.Text = "Playspace Calibration"
        '
        'TabPage_Overrides
        '
        Me.TabPage_Overrides.BackColor = System.Drawing.Color.White
        Me.TabPage_Overrides.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Overrides.Name = "TabPage_Overrides"
        Me.TabPage_Overrides.Size = New System.Drawing.Size(760, 454)
        Me.TabPage_Overrides.TabIndex = 2
        Me.TabPage_Overrides.Text = "SteamVR Tracker Overrides"
        '
        'ToolTip_Info
        '
        Me.ToolTip_Info.AutomaticDelay = 100
        Me.ToolTip_Info.AutoPopDelay = 30000
        Me.ToolTip_Info.InitialDelay = 100
        Me.ToolTip_Info.ReshowDelay = 20
        Me.ToolTip_Info.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip_Info.ToolTipTitle = "Information"
        '
        'Timer_VMTTrackers
        '
        Me.Timer_VMTTrackers.Enabled = True
        Me.Timer_VMTTrackers.Interval = 500
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LinkLabel_ReadMore)
        Me.Panel1.Controls.Add(Me.ClassPictureBoxQuality4)
        Me.Panel1.Controls.Add(Me.Label42)
        Me.Panel1.Controls.Add(Me.Label43)
        Me.Panel1.Controls.Add(Me.Label44)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 85)
        Me.Panel1.TabIndex = 23
        '
        'ClassPictureBoxQuality4
        '
        Me.ClassPictureBoxQuality4.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources._374
        Me.ClassPictureBoxQuality4.Location = New System.Drawing.Point(3, 3)
        Me.ClassPictureBoxQuality4.m_HighQuality = True
        Me.ClassPictureBoxQuality4.Name = "ClassPictureBoxQuality4"
        Me.ClassPictureBoxQuality4.Size = New System.Drawing.Size(57, 57)
        Me.ClassPictureBoxQuality4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality4.TabIndex = 16
        Me.ClassPictureBoxQuality4.TabStop = False
        '
        'Label42
        '
        Me.Label42.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label42.Location = New System.Drawing.Point(66, 30)
        Me.Label42.Margin = New System.Windows.Forms.Padding(3)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(731, 51)
        Me.Label42.TabIndex = 2
        Me.Label42.Text = "Virtual Motion Trackers (VMT) lets you to create SteamVR and OSC trackers." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "These" &
    " trackers can be used for body trackers, hand trackers, or to replace the 6-DoF " &
    "position of head-mounted displays" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(66, 3)
        Me.Label43.Margin = New System.Windows.Forms.Padding(3)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(188, 21)
        Me.Label43.TabIndex = 1
        Me.Label43.Text = "Virtual Motion Trackers"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label44.Location = New System.Drawing.Point(0, 84)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(800, 1)
        Me.Label44.TabIndex = 0
        '
        'ToolTip_Default
        '
        Me.ToolTip_Default.AutomaticDelay = 100
        Me.ToolTip_Default.AutoPopDelay = 30000
        Me.ToolTip_Default.InitialDelay = 100
        Me.ToolTip_Default.ReshowDelay = 20
        '
        'UCVirtualMotionTracker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.TabControl_Vmt)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVirtualMotionTracker"
        Me.Size = New System.Drawing.Size(800, 600)
        Me.TabControl_Vmt.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ClassPictureBoxQuality4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LinkLabel_ReadMore As LinkLabel
    Friend WithEvents TabControl_Vmt As TabControl
    Friend WithEvents TabPage_Trackers As TabPage
    Friend WithEvents TabPage_Overrides As TabPage
    Friend WithEvents TabPage_Settings As TabPage
    Friend WithEvents TabPage_Management As TabPage
    Friend WithEvents TabPage_PlayspaceCalib As TabPage
    Friend WithEvents ToolTip_Info As ToolTip
    Friend WithEvents Timer_VMTTrackers As Timer
    Friend WithEvents ClassPictureBoxQuality4 As ClassPictureBoxQuality
    Friend WithEvents Label42 As Label
    Friend WithEvents Label43 As Label
    Friend WithEvents Label44 As Label
    Friend WithEvents ToolTip_Default As ToolTip
    Friend WithEvents Panel1 As Panel
End Class
