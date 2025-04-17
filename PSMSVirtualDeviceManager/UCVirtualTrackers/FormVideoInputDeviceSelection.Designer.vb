<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormVideoInputDeviceSelection
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Button_Add = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox_Devices = New System.Windows.Forms.ComboBox()
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
        Me.Panel1.Location = New System.Drawing.Point(0, 72)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(542, 56)
        Me.Panel1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(542, 1)
        Me.Panel2.TabIndex = 2
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1608_16x16_32
        Me.Button_Cancel.Location = New System.Drawing.Point(320, 12)
        Me.Button_Cancel.Margin = New System.Windows.Forms.Padding(3, 16, 3, 16)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(100, 28)
        Me.Button_Cancel.TabIndex = 1
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button_Add
        '
        Me.Button_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Add.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.wmploc_474_16x16_32
        Me.Button_Add.Location = New System.Drawing.Point(426, 12)
        Me.Button_Add.Margin = New System.Windows.Forms.Padding(3, 16, 16, 16)
        Me.Button_Add.Name = "Button_Add"
        Me.Button_Add.Size = New System.Drawing.Size(100, 28)
        Me.Button_Add.TabIndex = 0
        Me.Button_Add.Text = "Add"
        Me.Button_Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_Add.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 25)
        Me.Label3.Margin = New System.Windows.Forms.Padding(16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(158, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Available video input devices:"
        '
        'ComboBox_Devices
        '
        Me.ComboBox_Devices.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Devices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Devices.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_Devices.FormattingEnabled = True
        Me.ComboBox_Devices.Location = New System.Drawing.Point(215, 22)
        Me.ComboBox_Devices.Margin = New System.Windows.Forms.Padding(16, 16, 48, 16)
        Me.ComboBox_Devices.Name = "ComboBox_Devices"
        Me.ComboBox_Devices.Size = New System.Drawing.Size(270, 21)
        Me.ComboBox_Devices.TabIndex = 10
        '
        'FormVideoInputDeviceSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(542, 128)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBox_Devices)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormVideoInputDeviceSelection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add New Video Input Device"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Button_Cancel As Button
    Friend WithEvents Button_Add As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox_Devices As ComboBox
End Class
