<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCVmtTrackers
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel_AvailableTrackers = New System.Windows.Forms.Panel()
        Me.ListView_Trackers = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.Panel_VMTTrackers = New System.Windows.Forms.Panel()
        Me.Button_VMTControllers = New System.Windows.Forms.Button()
        Me.Button_AddVMTController = New System.Windows.Forms.Button()
        Me.ContextMenuStrip_Autostart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ContextMenuStrip_AddTracker = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_AddTracker = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_AddHmd = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip_Trackers = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_TrackerRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer_VMTTrackers = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_AvailableTrackers.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.ContextMenuStrip_AddTracker.SuspendLayout()
        Me.ContextMenuStrip_Trackers.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_AvailableTrackers
        '
        Me.Panel_AvailableTrackers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_AvailableTrackers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_AvailableTrackers.Controls.Add(Me.ListView_Trackers)
        Me.Panel_AvailableTrackers.Controls.Add(Me.Panel19)
        Me.Panel_AvailableTrackers.Location = New System.Drawing.Point(16, 58)
        Me.Panel_AvailableTrackers.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_AvailableTrackers.Name = "Panel_AvailableTrackers"
        Me.Panel_AvailableTrackers.Size = New System.Drawing.Size(768, 192)
        Me.Panel_AvailableTrackers.TabIndex = 26
        '
        'ListView_Trackers
        '
        Me.ListView_Trackers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView_Trackers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.ListView_Trackers.ContextMenuStrip = Me.ContextMenuStrip_Trackers
        Me.ListView_Trackers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_Trackers.FullRowSelect = True
        Me.ListView_Trackers.HideSelection = False
        Me.ListView_Trackers.Location = New System.Drawing.Point(0, 42)
        Me.ListView_Trackers.Margin = New System.Windows.Forms.Padding(16)
        Me.ListView_Trackers.MultiSelect = False
        Me.ListView_Trackers.Name = "ListView_Trackers"
        Me.ListView_Trackers.Size = New System.Drawing.Size(766, 148)
        Me.ListView_Trackers.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView_Trackers.TabIndex = 21
        Me.ListView_Trackers.UseCompatibleStateImageBehavior = False
        Me.ListView_Trackers.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Type"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "ID"
        Me.ColumnHeader3.Width = 75
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "VMT ID"
        Me.ColumnHeader4.Width = 75
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Tracker Role"
        Me.ColumnHeader5.Width = 400
        '
        'Panel19
        '
        Me.Panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel19.Controls.Add(Me.Label66)
        Me.Panel19.Controls.Add(Me.Panel20)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel19.Location = New System.Drawing.Point(0, 0)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(766, 42)
        Me.Panel19.TabIndex = 0
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.Transparent
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label66.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.Color.Navy
        Me.Label66.Location = New System.Drawing.Point(0, 0)
        Me.Label66.Name = "Label66"
        Me.Label66.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label66.Size = New System.Drawing.Size(766, 41)
        Me.Label66.TabIndex = 1
        Me.Label66.Text = "Available Remote Devices"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel20
        '
        Me.Panel20.BackColor = System.Drawing.Color.Gray
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel20.Location = New System.Drawing.Point(0, 41)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(766, 1)
        Me.Panel20.TabIndex = 0
        '
        'Panel_VMTTrackers
        '
        Me.Panel_VMTTrackers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_VMTTrackers.AutoScroll = True
        Me.Panel_VMTTrackers.Location = New System.Drawing.Point(16, 281)
        Me.Panel_VMTTrackers.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_VMTTrackers.Name = "Panel_VMTTrackers"
        Me.Panel_VMTTrackers.Size = New System.Drawing.Size(768, 303)
        Me.Panel_VMTTrackers.TabIndex = 25
        '
        'Button_VMTControllers
        '
        Me.Button_VMTControllers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5353_16x16_32
        Me.Button_VMTControllers.Location = New System.Drawing.Point(16, 16)
        Me.Button_VMTControllers.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Button_VMTControllers.Name = "Button_VMTControllers"
        Me.Button_VMTControllers.Size = New System.Drawing.Size(162, 23)
        Me.Button_VMTControllers.TabIndex = 24
        Me.Button_VMTControllers.Text = "Autostart trackers..."
        Me.Button_VMTControllers.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_VMTControllers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_VMTControllers.UseVisualStyleBackColor = True
        '
        'Button_AddVMTController
        '
        Me.Button_AddVMTController.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_AddVMTController.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.DevicePairing_6101_16x16_32
        Me.Button_AddVMTController.Location = New System.Drawing.Point(664, 16)
        Me.Button_AddVMTController.Margin = New System.Windows.Forms.Padding(3, 3, 16, 3)
        Me.Button_AddVMTController.Name = "Button_AddVMTController"
        Me.Button_AddVMTController.Size = New System.Drawing.Size(120, 23)
        Me.Button_AddVMTController.TabIndex = 23
        Me.Button_AddVMTController.Text = "Add tracker..."
        Me.Button_AddVMTController.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_AddVMTController.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_AddVMTController.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip_Autostart
        '
        Me.ContextMenuStrip_Autostart.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip_Autostart.Size = New System.Drawing.Size(61, 4)
        '
        'ContextMenuStrip_AddTracker
        '
        Me.ContextMenuStrip_AddTracker.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_AddTracker, Me.ToolStripMenuItem_AddHmd})
        Me.ContextMenuStrip_AddTracker.Name = "ContextMenuStrip_AddTracker"
        Me.ContextMenuStrip_AddTracker.Size = New System.Drawing.Size(214, 48)
        '
        'ToolStripMenuItem_AddTracker
        '
        Me.ToolStripMenuItem_AddTracker.Name = "ToolStripMenuItem_AddTracker"
        Me.ToolStripMenuItem_AddTracker.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem_AddTracker.Text = "By Controller"
        '
        'ToolStripMenuItem_AddHmd
        '
        Me.ToolStripMenuItem_AddHmd.Name = "ToolStripMenuItem_AddHmd"
        Me.ToolStripMenuItem_AddHmd.Size = New System.Drawing.Size(213, 22)
        Me.ToolStripMenuItem_AddHmd.Text = "By Head-Mounted Display"
        '
        'ContextMenuStrip_Trackers
        '
        Me.ContextMenuStrip_Trackers.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_TrackerRemove})
        Me.ContextMenuStrip_Trackers.Name = "ContextMenuStrip_Trackers"
        Me.ContextMenuStrip_Trackers.Size = New System.Drawing.Size(118, 26)
        '
        'ToolStripMenuItem_TrackerRemove
        '
        Me.ToolStripMenuItem_TrackerRemove.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.ToolStripMenuItem_TrackerRemove.Name = "ToolStripMenuItem_TrackerRemove"
        Me.ToolStripMenuItem_TrackerRemove.Size = New System.Drawing.Size(117, 22)
        Me.ToolStripMenuItem_TrackerRemove.Text = "Remove"
        '
        'Timer_VMTTrackers
        '
        Me.Timer_VMTTrackers.Enabled = True
        Me.Timer_VMTTrackers.Interval = 500
        '
        'UCVmtTrackers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel_AvailableTrackers)
        Me.Controls.Add(Me.Panel_VMTTrackers)
        Me.Controls.Add(Me.Button_VMTControllers)
        Me.Controls.Add(Me.Button_AddVMTController)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVmtTrackers"
        Me.Size = New System.Drawing.Size(800, 600)
        Me.Panel_AvailableTrackers.ResumeLayout(False)
        Me.Panel19.ResumeLayout(False)
        Me.ContextMenuStrip_AddTracker.ResumeLayout(False)
        Me.ContextMenuStrip_Trackers.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel_AvailableTrackers As Panel
    Friend WithEvents ListView_Trackers As ClassListViewEx
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents Panel19 As Panel
    Friend WithEvents Label66 As Label
    Friend WithEvents Panel20 As Panel
    Friend WithEvents Panel_VMTTrackers As Panel
    Friend WithEvents Button_VMTControllers As Button
    Friend WithEvents Button_AddVMTController As Button
    Friend WithEvents ContextMenuStrip_Autostart As ContextMenuStrip
    Friend WithEvents ContextMenuStrip_AddTracker As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_AddTracker As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_AddHmd As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip_Trackers As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_TrackerRemove As ToolStripMenuItem
    Friend WithEvents Timer_VMTTrackers As Timer
End Class
