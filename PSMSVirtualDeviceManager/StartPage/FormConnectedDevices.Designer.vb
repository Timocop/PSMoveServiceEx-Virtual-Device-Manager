<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormConnectedDevices
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormConnectedDevices))
        Me.TreeView_ConnectedDevices = New System.Windows.Forms.TreeView()
        Me.ContextMenuStrip_Devices = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_DeviceRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem_DeviceEnable = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_DeviceDisable = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem_DeviceUninstall = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageList_Devices = New System.Windows.Forms.ImageList(Me.components)
        Me.Button_Refresh = New System.Windows.Forms.Button()
        Me.ListView_DeviceInfo = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.CheckBox_ShowDisconnectedDevices = New System.Windows.Forms.CheckBox()
        Me.Button_CopyOutput = New System.Windows.Forms.Button()
        Me.ContextMenuStrip_Devices.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TreeView_ConnectedDevices
        '
        Me.TreeView_ConnectedDevices.BackColor = System.Drawing.Color.White
        Me.TreeView_ConnectedDevices.ContextMenuStrip = Me.ContextMenuStrip_Devices
        Me.TreeView_ConnectedDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView_ConnectedDevices.FullRowSelect = True
        Me.TreeView_ConnectedDevices.HideSelection = False
        Me.TreeView_ConnectedDevices.ImageIndex = 0
        Me.TreeView_ConnectedDevices.ImageList = Me.ImageList_Devices
        Me.TreeView_ConnectedDevices.Location = New System.Drawing.Point(0, 0)
        Me.TreeView_ConnectedDevices.Name = "TreeView_ConnectedDevices"
        Me.TreeView_ConnectedDevices.SelectedImageIndex = 0
        Me.TreeView_ConnectedDevices.Size = New System.Drawing.Size(474, 525)
        Me.TreeView_ConnectedDevices.TabIndex = 0
        '
        'ContextMenuStrip_Devices
        '
        Me.ContextMenuStrip_Devices.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_DeviceRefresh, Me.ToolStripSeparator2, Me.ToolStripMenuItem_DeviceEnable, Me.ToolStripMenuItem_DeviceDisable, Me.ToolStripSeparator1, Me.ToolStripMenuItem_DeviceUninstall})
        Me.ContextMenuStrip_Devices.Name = "ContextMenuStrip_Devices"
        Me.ContextMenuStrip_Devices.Size = New System.Drawing.Size(179, 104)
        '
        'ToolStripMenuItem_DeviceRefresh
        '
        Me.ToolStripMenuItem_DeviceRefresh.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16739_16x16_32
        Me.ToolStripMenuItem_DeviceRefresh.Name = "ToolStripMenuItem_DeviceRefresh"
        Me.ToolStripMenuItem_DeviceRefresh.Size = New System.Drawing.Size(178, 22)
        Me.ToolStripMenuItem_DeviceRefresh.Text = "Refresh Plug && Play"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(175, 6)
        '
        'ToolStripMenuItem_DeviceEnable
        '
        Me.ToolStripMenuItem_DeviceEnable.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.ToolStripMenuItem_DeviceEnable.Name = "ToolStripMenuItem_DeviceEnable"
        Me.ToolStripMenuItem_DeviceEnable.Size = New System.Drawing.Size(178, 22)
        Me.ToolStripMenuItem_DeviceEnable.Text = "Enable Device"
        '
        'ToolStripMenuItem_DeviceDisable
        '
        Me.ToolStripMenuItem_DeviceDisable.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Close_16x16_32
        Me.ToolStripMenuItem_DeviceDisable.Name = "ToolStripMenuItem_DeviceDisable"
        Me.ToolStripMenuItem_DeviceDisable.Size = New System.Drawing.Size(178, 22)
        Me.ToolStripMenuItem_DeviceDisable.Text = "Disable Device"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(175, 6)
        '
        'ToolStripMenuItem_DeviceUninstall
        '
        Me.ToolStripMenuItem_DeviceUninstall.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.ToolStripMenuItem_DeviceUninstall.Name = "ToolStripMenuItem_DeviceUninstall"
        Me.ToolStripMenuItem_DeviceUninstall.Size = New System.Drawing.Size(178, 22)
        Me.ToolStripMenuItem_DeviceUninstall.Text = "Uninstall Device"
        '
        'ImageList_Devices
        '
        Me.ImageList_Devices.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList_Devices.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList_Devices.TransparentColor = System.Drawing.Color.Transparent
        '
        'Button_Refresh
        '
        Me.Button_Refresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_Refresh.Location = New System.Drawing.Point(691, 543)
        Me.Button_Refresh.Name = "Button_Refresh"
        Me.Button_Refresh.Size = New System.Drawing.Size(86, 23)
        Me.Button_Refresh.TabIndex = 1
        Me.Button_Refresh.Text = "Refresh"
        Me.Button_Refresh.UseVisualStyleBackColor = True
        '
        'ListView_DeviceInfo
        '
        Me.ListView_DeviceInfo.BackColor = System.Drawing.Color.White
        Me.ListView_DeviceInfo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView_DeviceInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_DeviceInfo.HideSelection = False
        Me.ListView_DeviceInfo.Location = New System.Drawing.Point(0, 0)
        Me.ListView_DeviceInfo.Name = "ListView_DeviceInfo"
        Me.ListView_DeviceInfo.Size = New System.Drawing.Size(287, 525)
        Me.ListView_DeviceInfo.TabIndex = 2
        Me.ListView_DeviceInfo.UseCompatibleStateImageBehavior = False
        Me.ListView_DeviceInfo.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Name"
        Me.ColumnHeader1.Width = 99
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Value"
        Me.ColumnHeader2.Width = 178
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 12)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView_ConnectedDevices)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ListView_DeviceInfo)
        Me.SplitContainer1.Size = New System.Drawing.Size(765, 525)
        Me.SplitContainer1.SplitterDistance = 474
        Me.SplitContainer1.TabIndex = 4
        '
        'CheckBox_ShowDisconnectedDevices
        '
        Me.CheckBox_ShowDisconnectedDevices.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_ShowDisconnectedDevices.AutoSize = True
        Me.CheckBox_ShowDisconnectedDevices.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_ShowDisconnectedDevices.Location = New System.Drawing.Point(12, 548)
        Me.CheckBox_ShowDisconnectedDevices.Name = "CheckBox_ShowDisconnectedDevices"
        Me.CheckBox_ShowDisconnectedDevices.Size = New System.Drawing.Size(173, 18)
        Me.CheckBox_ShowDisconnectedDevices.TabIndex = 5
        Me.CheckBox_ShowDisconnectedDevices.Text = "Show disconnected devices"
        Me.CheckBox_ShowDisconnectedDevices.UseVisualStyleBackColor = True
        '
        'Button_CopyOutput
        '
        Me.Button_CopyOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_CopyOutput.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_CopyOutput.Location = New System.Drawing.Point(582, 543)
        Me.Button_CopyOutput.Name = "Button_CopyOutput"
        Me.Button_CopyOutput.Size = New System.Drawing.Size(103, 23)
        Me.Button_CopyOutput.TabIndex = 7
        Me.Button_CopyOutput.Text = "Copy Output"
        Me.Button_CopyOutput.UseVisualStyleBackColor = True
        '
        'FormConnectedDevices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(789, 578)
        Me.Controls.Add(Me.Button_CopyOutput)
        Me.Controls.Add(Me.CheckBox_ShowDisconnectedDevices)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Button_Refresh)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormConnectedDevices"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Device Manager"
        Me.ContextMenuStrip_Devices.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TreeView_ConnectedDevices As TreeView
    Friend WithEvents Button_Refresh As Button
    Friend WithEvents ImageList_Devices As ImageList
    Friend WithEvents ListView_DeviceInfo As ListView
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents CheckBox_ShowDisconnectedDevices As CheckBox
    Friend WithEvents ContextMenuStrip_Devices As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_DeviceEnable As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_DeviceDisable As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem_DeviceUninstall As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_DeviceRefresh As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents Button_CopyOutput As Button
End Class
