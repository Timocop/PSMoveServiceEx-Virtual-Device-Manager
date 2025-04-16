<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormTroubleshootLogs
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button_LogLoad = New System.Windows.Forms.Button()
        Me.Button_LogSave = New System.Windows.Forms.Button()
        Me.Button_LogCopy = New System.Windows.Forms.Button()
        Me.Button_LogRefresh = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabControl_Diagnostic = New System.Windows.Forms.TabControl()
        Me.TabPage_Issues = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ListView_Issues = New System.Windows.Forms.ListView()
        Me.ColumnHeader_Message = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ImageList_Issues = New System.Windows.Forms.ImageList(Me.components)
        Me.TextBox_IssueInfo = New System.Windows.Forms.TextBox()
        Me.TabPage_Devices = New System.Windows.Forms.TabPage()
        Me.TabPage_Logs = New System.Windows.Forms.TabPage()
        Me.TextBox_Logs = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox_Logs = New System.Windows.Forms.ComboBox()
        Me.TreeView_DeviceProperties = New System.Windows.Forms.TreeView()
        Me.Panel1.SuspendLayout()
        Me.TabControl_Diagnostic.SuspendLayout()
        Me.TabPage_Issues.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabPage_Devices.SuspendLayout()
        Me.TabPage_Logs.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.Button_LogLoad)
        Me.Panel1.Controls.Add(Me.Button_LogSave)
        Me.Panel1.Controls.Add(Me.Button_LogCopy)
        Me.Panel1.Controls.Add(Me.Button_LogRefresh)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 513)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 48)
        Me.Panel1.TabIndex = 0
        '
        'Button_LogLoad
        '
        Me.Button_LogLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_LogLoad.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_LogLoad.Location = New System.Drawing.Point(594, 13)
        Me.Button_LogLoad.Name = "Button_LogLoad"
        Me.Button_LogLoad.Size = New System.Drawing.Size(86, 23)
        Me.Button_LogLoad.TabIndex = 4
        Me.Button_LogLoad.Text = "Load"
        Me.Button_LogLoad.UseVisualStyleBackColor = True
        '
        'Button_LogSave
        '
        Me.Button_LogSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_LogSave.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_LogSave.Location = New System.Drawing.Point(104, 13)
        Me.Button_LogSave.Name = "Button_LogSave"
        Me.Button_LogSave.Size = New System.Drawing.Size(86, 23)
        Me.Button_LogSave.TabIndex = 3
        Me.Button_LogSave.Text = "Save Logs"
        Me.Button_LogSave.UseVisualStyleBackColor = True
        '
        'Button_LogCopy
        '
        Me.Button_LogCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_LogCopy.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_LogCopy.Location = New System.Drawing.Point(12, 13)
        Me.Button_LogCopy.Name = "Button_LogCopy"
        Me.Button_LogCopy.Size = New System.Drawing.Size(86, 23)
        Me.Button_LogCopy.TabIndex = 2
        Me.Button_LogCopy.Text = "Copy Logs"
        Me.Button_LogCopy.UseVisualStyleBackColor = True
        '
        'Button_LogRefresh
        '
        Me.Button_LogRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_LogRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_LogRefresh.Location = New System.Drawing.Point(686, 13)
        Me.Button_LogRefresh.Name = "Button_LogRefresh"
        Me.Button_LogRefresh.Size = New System.Drawing.Size(86, 23)
        Me.Button_LogRefresh.TabIndex = 1
        Me.Button_LogRefresh.Text = "Refresh"
        Me.Button_LogRefresh.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(784, 1)
        Me.Panel2.TabIndex = 0
        '
        'TabControl_Diagnostic
        '
        Me.TabControl_Diagnostic.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl_Diagnostic.Controls.Add(Me.TabPage_Issues)
        Me.TabControl_Diagnostic.Controls.Add(Me.TabPage_Devices)
        Me.TabControl_Diagnostic.Controls.Add(Me.TabPage_Logs)
        Me.TabControl_Diagnostic.Location = New System.Drawing.Point(12, 12)
        Me.TabControl_Diagnostic.Name = "TabControl_Diagnostic"
        Me.TabControl_Diagnostic.SelectedIndex = 0
        Me.TabControl_Diagnostic.Size = New System.Drawing.Size(760, 495)
        Me.TabControl_Diagnostic.TabIndex = 2
        '
        'TabPage_Issues
        '
        Me.TabPage_Issues.BackColor = System.Drawing.Color.White
        Me.TabPage_Issues.Controls.Add(Me.SplitContainer1)
        Me.TabPage_Issues.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Issues.Name = "TabPage_Issues"
        Me.TabPage_Issues.Size = New System.Drawing.Size(752, 469)
        Me.TabPage_Issues.TabIndex = 1
        Me.TabPage_Issues.Text = "Diagnostics"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListView_Issues)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBox_IssueInfo)
        Me.SplitContainer1.Size = New System.Drawing.Size(752, 469)
        Me.SplitContainer1.SplitterDistance = 250
        Me.SplitContainer1.TabIndex = 1
        '
        'ListView_Issues
        '
        Me.ListView_Issues.BackColor = System.Drawing.Color.White
        Me.ListView_Issues.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView_Issues.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader_Message})
        Me.ListView_Issues.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_Issues.FullRowSelect = True
        Me.ListView_Issues.HideSelection = False
        Me.ListView_Issues.Location = New System.Drawing.Point(0, 0)
        Me.ListView_Issues.MultiSelect = False
        Me.ListView_Issues.Name = "ListView_Issues"
        Me.ListView_Issues.Size = New System.Drawing.Size(752, 250)
        Me.ListView_Issues.SmallImageList = Me.ImageList_Issues
        Me.ListView_Issues.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView_Issues.TabIndex = 0
        Me.ListView_Issues.UseCompatibleStateImageBehavior = False
        Me.ListView_Issues.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader_Message
        '
        Me.ColumnHeader_Message.Text = "Message"
        Me.ColumnHeader_Message.Width = 721
        '
        'ImageList_Issues
        '
        Me.ImageList_Issues.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ImageList_Issues.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList_Issues.TransparentColor = System.Drawing.Color.Transparent
        '
        'TextBox_IssueInfo
        '
        Me.TextBox_IssueInfo.BackColor = System.Drawing.Color.White
        Me.TextBox_IssueInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_IssueInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_IssueInfo.Location = New System.Drawing.Point(0, 0)
        Me.TextBox_IssueInfo.Multiline = True
        Me.TextBox_IssueInfo.Name = "TextBox_IssueInfo"
        Me.TextBox_IssueInfo.ReadOnly = True
        Me.TextBox_IssueInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_IssueInfo.Size = New System.Drawing.Size(752, 215)
        Me.TextBox_IssueInfo.TabIndex = 1
        Me.TextBox_IssueInfo.Text = "Nothing selected."
        '
        'TabPage_Devices
        '
        Me.TabPage_Devices.BackColor = System.Drawing.Color.White
        Me.TabPage_Devices.Controls.Add(Me.TreeView_DeviceProperties)
        Me.TabPage_Devices.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Devices.Name = "TabPage_Devices"
        Me.TabPage_Devices.Size = New System.Drawing.Size(752, 469)
        Me.TabPage_Devices.TabIndex = 2
        Me.TabPage_Devices.Text = "Devices"
        '
        'TabPage_Logs
        '
        Me.TabPage_Logs.BackColor = System.Drawing.Color.White
        Me.TabPage_Logs.Controls.Add(Me.ComboBox_Logs)
        Me.TabPage_Logs.Controls.Add(Me.Label1)
        Me.TabPage_Logs.Controls.Add(Me.TextBox_Logs)
        Me.TabPage_Logs.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_Logs.Name = "TabPage_Logs"
        Me.TabPage_Logs.Size = New System.Drawing.Size(752, 469)
        Me.TabPage_Logs.TabIndex = 0
        Me.TabPage_Logs.Text = "Logs"
        '
        'TextBox_Logs
        '
        Me.TextBox_Logs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Logs.BackColor = System.Drawing.Color.White
        Me.TextBox_Logs.Location = New System.Drawing.Point(3, 56)
        Me.TextBox_Logs.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.TextBox_Logs.Multiline = True
        Me.TextBox_Logs.Name = "TextBox_Logs"
        Me.TextBox_Logs.ReadOnly = True
        Me.TextBox_Logs.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_Logs.Size = New System.Drawing.Size(746, 410)
        Me.TextBox_Logs.TabIndex = 0
        Me.TextBox_Logs.WordWrap = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 19)
        Me.Label1.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Logs:"
        '
        'ComboBox_Logs
        '
        Me.ComboBox_Logs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Logs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Logs.FormattingEnabled = True
        Me.ComboBox_Logs.Location = New System.Drawing.Point(69, 16)
        Me.ComboBox_Logs.Margin = New System.Windows.Forms.Padding(16, 16, 16, 3)
        Me.ComboBox_Logs.Name = "ComboBox_Logs"
        Me.ComboBox_Logs.Size = New System.Drawing.Size(667, 21)
        Me.ComboBox_Logs.TabIndex = 2
        '
        'TreeView_DeviceProperties
        '
        Me.TreeView_DeviceProperties.BackColor = System.Drawing.Color.White
        Me.TreeView_DeviceProperties.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView_DeviceProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView_DeviceProperties.Location = New System.Drawing.Point(0, 0)
        Me.TreeView_DeviceProperties.Name = "TreeView_DeviceProperties"
        Me.TreeView_DeviceProperties.Size = New System.Drawing.Size(752, 469)
        Me.TreeView_DeviceProperties.TabIndex = 0
        '
        'FormTroubleshootLogs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.TabControl_Diagnostic)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormTroubleshootLogs"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Logs and Diagnostics"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl_Diagnostic.ResumeLayout(False)
        Me.TabPage_Issues.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabPage_Devices.ResumeLayout(False)
        Me.TabPage_Logs.ResumeLayout(False)
        Me.TabPage_Logs.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button_LogCopy As Button
    Friend WithEvents Button_LogRefresh As Button
    Friend WithEvents Button_LogSave As Button
    Friend WithEvents Button_LogLoad As Button
    Friend WithEvents TabControl_Diagnostic As TabControl
    Friend WithEvents TabPage_Logs As TabPage
    Friend WithEvents TabPage_Issues As TabPage
    Friend WithEvents ListView_Issues As ListView
    Friend WithEvents ImageList_Issues As ImageList
    Friend WithEvents ColumnHeader_Message As ColumnHeader
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TextBox_IssueInfo As TextBox
    Friend WithEvents TabPage_Devices As TabPage
    Friend WithEvents ComboBox_Logs As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox_Logs As TextBox
    Friend WithEvents TreeView_DeviceProperties As TreeView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
End Class
