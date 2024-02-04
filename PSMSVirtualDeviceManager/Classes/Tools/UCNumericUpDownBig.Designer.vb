<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCNumericUpDownBig
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.Button_NumDown = New System.Windows.Forms.Button()
        Me.Button_NumUp = New System.Windows.Forms.Button()
        Me.Timer_Up = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_Down = New System.Windows.Forms.Timer(Me.components)
        Me.Button_Reset = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button_NumDown
        '
        Me.Button_NumDown.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_NumDown.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_NumDown.Location = New System.Drawing.Point(0, 0)
        Me.Button_NumDown.Margin = New System.Windows.Forms.Padding(0)
        Me.Button_NumDown.Name = "Button_NumDown"
        Me.Button_NumDown.Size = New System.Drawing.Size(22, 22)
        Me.Button_NumDown.TabIndex = 1
        Me.Button_NumDown.Text = "<"
        Me.Button_NumDown.UseVisualStyleBackColor = True
        '
        'Button_NumUp
        '
        Me.Button_NumUp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_NumUp.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_NumUp.Location = New System.Drawing.Point(22, 0)
        Me.Button_NumUp.Margin = New System.Windows.Forms.Padding(0)
        Me.Button_NumUp.Name = "Button_NumUp"
        Me.Button_NumUp.Size = New System.Drawing.Size(22, 22)
        Me.Button_NumUp.TabIndex = 2
        Me.Button_NumUp.Text = ">"
        Me.Button_NumUp.UseVisualStyleBackColor = True
        '
        'Timer_Up
        '
        '
        'Timer_Down
        '
        '
        'Button_Reset
        '
        Me.Button_Reset.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_Reset.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_Reset.Location = New System.Drawing.Point(44, 0)
        Me.Button_Reset.Margin = New System.Windows.Forms.Padding(0)
        Me.Button_Reset.Name = "Button_Reset"
        Me.Button_Reset.Size = New System.Drawing.Size(22, 22)
        Me.Button_Reset.TabIndex = 3
        Me.Button_Reset.Text = "X"
        Me.Button_Reset.UseVisualStyleBackColor = True
        Me.Button_Reset.Visible = False
        '
        'UCNumericUpDownBig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Controls.Add(Me.Button_NumUp)
        Me.Controls.Add(Me.Button_NumDown)
        Me.Controls.Add(Me.Button_Reset)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCNumericUpDownBig"
        Me.Size = New System.Drawing.Size(66, 22)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button_NumDown As Button
    Friend WithEvents Button_NumUp As Button
    Friend WithEvents Timer_Up As Timer
    Friend WithEvents Timer_Down As Timer
    Friend WithEvents Button_Reset As Button
End Class
