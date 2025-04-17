<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCVmtOverrides
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCVmtOverrides))
        Me.Button_Refresh = New System.Windows.Forms.Button()
        Me.Button_Remove = New System.Windows.Forms.Button()
        Me.Button_Add = New System.Windows.Forms.Button()
        Me.ToolTip_Info = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip_Default = New System.Windows.Forms.ToolTip(Me.components)
        Me.UcInformation1 = New PSMSVirtualDeviceManager.UCInformation()
        Me.ListView_Overrides = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'Button_Refresh
        '
        Me.Button_Refresh.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16739_16x16_32
        Me.Button_Refresh.Location = New System.Drawing.Point(16, 272)
        Me.Button_Refresh.Name = "Button_Refresh"
        Me.Button_Refresh.Size = New System.Drawing.Size(109, 23)
        Me.Button_Refresh.TabIndex = 32
        Me.Button_Refresh.Text = "Refresh"
        Me.Button_Refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_Refresh.UseVisualStyleBackColor = True
        '
        'Button_Remove
        '
        Me.Button_Remove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Remove.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.Button_Remove.Location = New System.Drawing.Point(560, 272)
        Me.Button_Remove.Name = "Button_Remove"
        Me.Button_Remove.Size = New System.Drawing.Size(109, 23)
        Me.Button_Remove.TabIndex = 31
        Me.Button_Remove.Text = "Remove"
        Me.Button_Remove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Remove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_Remove.UseVisualStyleBackColor = True
        '
        'Button_Add
        '
        Me.Button_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Add.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.wmploc_474_16x16_32
        Me.Button_Add.Location = New System.Drawing.Point(675, 272)
        Me.Button_Add.Name = "Button_Add"
        Me.Button_Add.Size = New System.Drawing.Size(109, 23)
        Me.Button_Add.TabIndex = 30
        Me.Button_Add.Text = "Add"
        Me.Button_Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_Add.UseVisualStyleBackColor = True
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
        'ToolTip_Default
        '
        Me.ToolTip_Default.AutomaticDelay = 100
        Me.ToolTip_Default.AutoPopDelay = 30000
        Me.ToolTip_Default.InitialDelay = 100
        Me.ToolTip_Default.ReshowDelay = 20
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
        Me.UcInformation1.m_ReadMoreText = ""
        Me.UcInformation1.m_Text = resources.GetString("UcInformation1.m_Text")
        Me.UcInformation1.Margin = New System.Windows.Forms.Padding(16)
        Me.UcInformation1.Name = "UcInformation1"
        Me.UcInformation1.Size = New System.Drawing.Size(768, 47)
        Me.UcInformation1.TabIndex = 33
        '
        'ListView_Overrides
        '
        Me.ListView_Overrides.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_Overrides.BackColor = System.Drawing.Color.White
        Me.ListView_Overrides.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView_Overrides.FullRowSelect = True
        Me.ListView_Overrides.HideSelection = False
        Me.ListView_Overrides.Location = New System.Drawing.Point(16, 82)
        Me.ListView_Overrides.Margin = New System.Windows.Forms.Padding(16, 3, 16, 3)
        Me.ListView_Overrides.Name = "ListView_Overrides"
        Me.ListView_Overrides.Size = New System.Drawing.Size(768, 184)
        Me.ListView_Overrides.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView_Overrides.TabIndex = 29
        Me.ListView_Overrides.UseCompatibleStateImageBehavior = False
        Me.ListView_Overrides.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Tracker"
        Me.ColumnHeader1.Width = 300
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Override"
        Me.ColumnHeader2.Width = 300
        '
        'UCVmtOverrides
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.UcInformation1)
        Me.Controls.Add(Me.Button_Refresh)
        Me.Controls.Add(Me.Button_Remove)
        Me.Controls.Add(Me.Button_Add)
        Me.Controls.Add(Me.ListView_Overrides)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVmtOverrides"
        Me.Size = New System.Drawing.Size(800, 306)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button_Refresh As Button
    Friend WithEvents Button_Remove As Button
    Friend WithEvents Button_Add As Button
    Friend WithEvents ListView_Overrides As ClassListViewEx
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ToolTip_Info As ToolTip
    Friend WithEvents ToolTip_Default As ToolTip
    Friend WithEvents UcInformation1 As UCInformation
End Class
