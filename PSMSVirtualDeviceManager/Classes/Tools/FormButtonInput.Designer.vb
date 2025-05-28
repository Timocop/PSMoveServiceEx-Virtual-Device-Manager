<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormButtonInput
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
        Me.Label_BindingInfo = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label_BindingInfo
        '
        Me.Label_BindingInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_BindingInfo.Location = New System.Drawing.Point(0, 0)
        Me.Label_BindingInfo.Name = "Label_BindingInfo"
        Me.Label_BindingInfo.Size = New System.Drawing.Size(330, 77)
        Me.Label_BindingInfo.TabIndex = 0
        Me.Label_BindingInfo.Text = "Please press a button combination to progress..."
        Me.Label_BindingInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FormButtonInput
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(330, 77)
        Me.Controls.Add(Me.Label_BindingInfo)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormButtonInput"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Recording input..."
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label_BindingInfo As Label
End Class
