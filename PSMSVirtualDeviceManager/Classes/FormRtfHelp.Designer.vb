<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormRtfHelp
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRtfHelp))
        Me.RichTextBox_Help = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'RichTextBox_Help
        '
        Me.RichTextBox_Help.BackColor = System.Drawing.Color.White
        Me.RichTextBox_Help.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox_Help.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.RichTextBox_Help.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox_Help.Location = New System.Drawing.Point(16, 16)
        Me.RichTextBox_Help.Name = "RichTextBox_Help"
        Me.RichTextBox_Help.ReadOnly = True
        Me.RichTextBox_Help.Size = New System.Drawing.Size(768, 545)
        Me.RichTextBox_Help.TabIndex = 0
        Me.RichTextBox_Help.Text = ""
        '
        'FormRtfHelp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.RichTextBox_Help)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "FormRtfHelp"
        Me.Padding = New System.Windows.Forms.Padding(16, 16, 0, 0)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Help"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RichTextBox_Help As RichTextBox
End Class
