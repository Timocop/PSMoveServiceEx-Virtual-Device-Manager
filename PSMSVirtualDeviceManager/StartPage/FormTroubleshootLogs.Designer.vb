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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button_LogCopy = New System.Windows.Forms.Button()
        Me.Button_LogRefresh = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabControl_Logs = New System.Windows.Forms.TabControl()
        Me.Button_LogSave = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.Button_LogSave)
        Me.Panel1.Controls.Add(Me.Button_LogCopy)
        Me.Panel1.Controls.Add(Me.Button_LogRefresh)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 510)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(683, 48)
        Me.Panel1.TabIndex = 0
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
        Me.Button_LogRefresh.Location = New System.Drawing.Point(585, 13)
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
        Me.Panel2.Size = New System.Drawing.Size(683, 1)
        Me.Panel2.TabIndex = 0
        '
        'TabControl_Logs
        '
        Me.TabControl_Logs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl_Logs.Location = New System.Drawing.Point(12, 12)
        Me.TabControl_Logs.Multiline = True
        Me.TabControl_Logs.Name = "TabControl_Logs"
        Me.TabControl_Logs.SelectedIndex = 0
        Me.TabControl_Logs.Size = New System.Drawing.Size(659, 492)
        Me.TabControl_Logs.TabIndex = 1
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
        'FormTroubleshootLogs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(683, 558)
        Me.Controls.Add(Me.TabControl_Logs)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormTroubleshootLogs"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Logs and Diagnostics"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button_LogCopy As Button
    Friend WithEvents Button_LogRefresh As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TabControl_Logs As TabControl
    Friend WithEvents Button_LogSave As Button
End Class
