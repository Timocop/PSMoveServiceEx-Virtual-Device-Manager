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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCControllerAttachments))
        Me.Panel_Attachments = New System.Windows.Forms.Panel()
        Me.ContextMenuStrip_Attachments = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_AttachmentRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip_Autostart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Button_Autostart = New System.Windows.Forms.Button()
        Me.Button_AddAttachment = New System.Windows.Forms.Button()
        Me.Timer_Attachment = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_AvailableAttachments = New System.Windows.Forms.Panel()
        Me.ListView_Attachments = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UcInformation1 = New PSMSVirtualDeviceManager.UCInformation()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip_Attachments.SuspendLayout()
        Me.Panel_AvailableAttachments.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Attachments
        '
        Me.Panel_Attachments.AutoSize = True
        Me.Panel_Attachments.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel_Attachments.Location = New System.Drawing.Point(0, 315)
        Me.Panel_Attachments.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_Attachments.MinimumSize = New System.Drawing.Size(0, 32)
        Me.Panel_Attachments.Name = "Panel_Attachments"
        Me.Panel_Attachments.Padding = New System.Windows.Forms.Padding(16, 0, 16, 0)
        Me.Panel_Attachments.Size = New System.Drawing.Size(800, 32)
        Me.Panel_Attachments.TabIndex = 7
        '
        'ContextMenuStrip_Attachments
        '
        Me.ContextMenuStrip_Attachments.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_AttachmentRemove})
        Me.ContextMenuStrip_Attachments.Name = "ContextMenuStrip_Attachments"
        Me.ContextMenuStrip_Attachments.Size = New System.Drawing.Size(118, 26)
        '
        'ToolStripMenuItem_AttachmentRemove
        '
        Me.ToolStripMenuItem_AttachmentRemove.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.ToolStripMenuItem_AttachmentRemove.Name = "ToolStripMenuItem_AttachmentRemove"
        Me.ToolStripMenuItem_AttachmentRemove.Size = New System.Drawing.Size(117, 22)
        Me.ToolStripMenuItem_AttachmentRemove.Text = "Remove"
        '
        'ContextMenuStrip_Autostart
        '
        Me.ContextMenuStrip_Autostart.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip_Autostart.Size = New System.Drawing.Size(61, 4)
        '
        'Button_Autostart
        '
        Me.Button_Autostart.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5353_16x16_32
        Me.Button_Autostart.Location = New System.Drawing.Point(16, 92)
        Me.Button_Autostart.Margin = New System.Windows.Forms.Padding(16, 16, 3, 16)
        Me.Button_Autostart.Name = "Button_Autostart"
        Me.Button_Autostart.Size = New System.Drawing.Size(180, 23)
        Me.Button_Autostart.TabIndex = 12
        Me.Button_Autostart.Text = "Autostart attachments..."
        Me.Button_Autostart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Autostart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_Autostart.UseVisualStyleBackColor = True
        '
        'Button_AddAttachment
        '
        Me.Button_AddAttachment.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_AddAttachment.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.DevicePairing_6101_16x16_32
        Me.Button_AddAttachment.Location = New System.Drawing.Point(634, 92)
        Me.Button_AddAttachment.Margin = New System.Windows.Forms.Padding(3, 3, 16, 16)
        Me.Button_AddAttachment.Name = "Button_AddAttachment"
        Me.Button_AddAttachment.Size = New System.Drawing.Size(150, 23)
        Me.Button_AddAttachment.TabIndex = 8
        Me.Button_AddAttachment.Text = "Add attachment"
        Me.Button_AddAttachment.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_AddAttachment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_AddAttachment.UseVisualStyleBackColor = True
        '
        'Timer_Attachment
        '
        Me.Timer_Attachment.Enabled = True
        Me.Timer_Attachment.Interval = 500
        '
        'Panel_AvailableAttachments
        '
        Me.Panel_AvailableAttachments.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_AvailableAttachments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_AvailableAttachments.Controls.Add(Me.ListView_Attachments)
        Me.Panel_AvailableAttachments.Controls.Add(Me.Panel8)
        Me.Panel_AvailableAttachments.Location = New System.Drawing.Point(16, 134)
        Me.Panel_AvailableAttachments.Margin = New System.Windows.Forms.Padding(16, 3, 16, 16)
        Me.Panel_AvailableAttachments.Name = "Panel_AvailableAttachments"
        Me.Panel_AvailableAttachments.Size = New System.Drawing.Size(768, 165)
        Me.Panel_AvailableAttachments.TabIndex = 22
        '
        'ListView_Attachments
        '
        Me.ListView_Attachments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView_Attachments.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView_Attachments.ContextMenuStrip = Me.ContextMenuStrip_Attachments
        Me.ListView_Attachments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_Attachments.FullRowSelect = True
        Me.ListView_Attachments.HideSelection = False
        Me.ListView_Attachments.Location = New System.Drawing.Point(0, 42)
        Me.ListView_Attachments.Margin = New System.Windows.Forms.Padding(16)
        Me.ListView_Attachments.MultiSelect = False
        Me.ListView_Attachments.Name = "ListView_Attachments"
        Me.ListView_Attachments.Size = New System.Drawing.Size(766, 121)
        Me.ListView_Attachments.TabIndex = 0
        Me.ListView_Attachments.UseCompatibleStateImageBehavior = False
        Me.ListView_Attachments.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Controller ID"
        Me.ColumnHeader1.Width = 125
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Parent Controller ID"
        Me.ColumnHeader2.Width = 125
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
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Navy
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label12.Size = New System.Drawing.Size(766, 41)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Available Controller Attachments"
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
        Me.Panel1.Controls.Add(Me.Button_AddAttachment)
        Me.Panel1.Controls.Add(Me.Button_Autostart)
        Me.Panel1.Controls.Add(Me.Panel_AvailableAttachments)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 315)
        Me.Panel1.TabIndex = 23
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
        Me.UcInformation1.m_Text = resources.GetString("UcInformation1.m_Text")
        Me.UcInformation1.Margin = New System.Windows.Forms.Padding(16, 16, 16, 3)
        Me.UcInformation1.Name = "UcInformation1"
        Me.UcInformation1.Size = New System.Drawing.Size(768, 57)
        Me.UcInformation1.TabIndex = 24
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Name"
        Me.ColumnHeader3.Width = 250
        '
        'UCControllerAttachments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel_Attachments)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCControllerAttachments"
        Me.Size = New System.Drawing.Size(800, 732)
        Me.ContextMenuStrip_Attachments.ResumeLayout(False)
        Me.Panel_AvailableAttachments.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_AddAttachment As Button
    Friend WithEvents Panel_Attachments As Panel
    Friend WithEvents ContextMenuStrip_Autostart As ContextMenuStrip
    Friend WithEvents Button_Autostart As Button
    Friend WithEvents ListView_Attachments As ClassListViewEx
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ContextMenuStrip_Attachments As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_AttachmentRemove As ToolStripMenuItem
    Friend WithEvents Timer_Attachment As Timer
    Friend WithEvents Panel_AvailableAttachments As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents UcInformation1 As UCInformation
    Friend WithEvents ColumnHeader3 As ColumnHeader
End Class
