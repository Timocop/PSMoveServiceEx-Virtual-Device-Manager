<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTrackerOverrideSetup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormTrackerOverrideSetup))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Button_Add = New System.Windows.Forms.Button()
        Me.RadioButton_VMT = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Custom = New System.Windows.Forms.RadioButton()
        Me.ComboBox_OverrideType = New System.Windows.Forms.ComboBox()
        Me.ComboBox_VMTTracker = New System.Windows.Forms.ComboBox()
        Me.Label_VMTTracker = New System.Windows.Forms.Label()
        Me.Label_CustomTracker = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox_CustomTracker = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Button_Cancel)
        Me.Panel1.Controls.Add(Me.Button_Add)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 280)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(477, 56)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(477, 1)
        Me.Panel2.TabIndex = 2
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_Cancel.Location = New System.Drawing.Point(274, 16)
        Me.Button_Cancel.Margin = New System.Windows.Forms.Padding(3, 16, 3, 16)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(86, 23)
        Me.Button_Cancel.TabIndex = 1
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button_Add
        '
        Me.Button_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Add.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_Add.Location = New System.Drawing.Point(366, 16)
        Me.Button_Add.Margin = New System.Windows.Forms.Padding(3, 16, 16, 16)
        Me.Button_Add.Name = "Button_Add"
        Me.Button_Add.Size = New System.Drawing.Size(86, 23)
        Me.Button_Add.TabIndex = 0
        Me.Button_Add.Text = "Add"
        Me.Button_Add.UseVisualStyleBackColor = True
        '
        'RadioButton_VMT
        '
        Me.RadioButton_VMT.AutoSize = True
        Me.RadioButton_VMT.Checked = True
        Me.RadioButton_VMT.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton_VMT.Location = New System.Drawing.Point(25, 25)
        Me.RadioButton_VMT.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.RadioButton_VMT.Name = "RadioButton_VMT"
        Me.RadioButton_VMT.Size = New System.Drawing.Size(292, 17)
        Me.RadioButton_VMT.TabIndex = 1
        Me.RadioButton_VMT.TabStop = True
        Me.RadioButton_VMT.Text = "Add a VMT tracker to the overrides (recommended)"
        Me.RadioButton_VMT.UseVisualStyleBackColor = True
        '
        'RadioButton_Custom
        '
        Me.RadioButton_Custom.AutoSize = True
        Me.RadioButton_Custom.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton_Custom.Location = New System.Drawing.Point(25, 106)
        Me.RadioButton_Custom.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.RadioButton_Custom.Name = "RadioButton_Custom"
        Me.RadioButton_Custom.Size = New System.Drawing.Size(220, 17)
        Me.RadioButton_Custom.TabIndex = 2
        Me.RadioButton_Custom.Text = "Add a custom tracker to the overrides"
        Me.RadioButton_Custom.UseVisualStyleBackColor = True
        '
        'ComboBox_OverrideType
        '
        Me.ComboBox_OverrideType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_OverrideType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_OverrideType.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_OverrideType.FormattingEnabled = True
        Me.ComboBox_OverrideType.Location = New System.Drawing.Point(175, 217)
        Me.ComboBox_OverrideType.Margin = New System.Windows.Forms.Padding(16)
        Me.ComboBox_OverrideType.Name = "ComboBox_OverrideType"
        Me.ComboBox_OverrideType.Size = New System.Drawing.Size(229, 21)
        Me.ComboBox_OverrideType.TabIndex = 3
        '
        'ComboBox_VMTTracker
        '
        Me.ComboBox_VMTTracker.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_VMTTracker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_VMTTracker.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_VMTTracker.FormattingEnabled = True
        Me.ComboBox_VMTTracker.Location = New System.Drawing.Point(175, 58)
        Me.ComboBox_VMTTracker.Margin = New System.Windows.Forms.Padding(16, 16, 64, 16)
        Me.ComboBox_VMTTracker.Name = "ComboBox_VMTTracker"
        Me.ComboBox_VMTTracker.Size = New System.Drawing.Size(229, 21)
        Me.ComboBox_VMTTracker.TabIndex = 4
        '
        'Label_VMTTracker
        '
        Me.Label_VMTTracker.AutoSize = True
        Me.Label_VMTTracker.Location = New System.Drawing.Point(57, 61)
        Me.Label_VMTTracker.Margin = New System.Windows.Forms.Padding(48, 16, 16, 16)
        Me.Label_VMTTracker.Name = "Label_VMTTracker"
        Me.Label_VMTTracker.Size = New System.Drawing.Size(86, 13)
        Me.Label_VMTTracker.TabIndex = 5
        Me.Label_VMTTracker.Text = "VMT Tracker ID:"
        '
        'Label_CustomTracker
        '
        Me.Label_CustomTracker.AutoSize = True
        Me.Label_CustomTracker.Location = New System.Drawing.Point(57, 142)
        Me.Label_CustomTracker.Margin = New System.Windows.Forms.Padding(48, 16, 16, 16)
        Me.Label_CustomTracker.Name = "Label_CustomTracker"
        Me.Label_CustomTracker.Size = New System.Drawing.Size(88, 13)
        Me.Label_CustomTracker.TabIndex = 6
        Me.Label_CustomTracker.Text = "Custom Tracker:"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel3.Location = New System.Drawing.Point(0, 187)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0, 16, 0, 16)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(477, 1)
        Me.Panel3.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 220)
        Me.Label3.Margin = New System.Windows.Forms.Padding(16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Tracker to Override:"
        '
        'ComboBox_CustomTracker
        '
        Me.ComboBox_CustomTracker.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_CustomTracker.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_CustomTracker.FormattingEnabled = True
        Me.ComboBox_CustomTracker.Location = New System.Drawing.Point(175, 139)
        Me.ComboBox_CustomTracker.Margin = New System.Windows.Forms.Padding(16, 16, 64, 16)
        Me.ComboBox_CustomTracker.Name = "ComboBox_CustomTracker"
        Me.ComboBox_CustomTracker.Size = New System.Drawing.Size(229, 21)
        Me.ComboBox_CustomTracker.TabIndex = 10
        '
        'FormTrackerOverrideSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(477, 336)
        Me.Controls.Add(Me.ComboBox_CustomTracker)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label_CustomTracker)
        Me.Controls.Add(Me.Label_VMTTracker)
        Me.Controls.Add(Me.ComboBox_VMTTracker)
        Me.Controls.Add(Me.ComboBox_OverrideType)
        Me.Controls.Add(Me.RadioButton_Custom)
        Me.Controls.Add(Me.RadioButton_VMT)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormTrackerOverrideSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add New Override"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Button_Cancel As Button
    Friend WithEvents Button_Add As Button
    Friend WithEvents RadioButton_VMT As RadioButton
    Friend WithEvents RadioButton_Custom As RadioButton
    Friend WithEvents ComboBox_OverrideType As ComboBox
    Friend WithEvents ComboBox_VMTTracker As ComboBox
    Friend WithEvents Label_VMTTracker As Label
    Friend WithEvents Label_CustomTracker As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox_CustomTracker As ComboBox
End Class
