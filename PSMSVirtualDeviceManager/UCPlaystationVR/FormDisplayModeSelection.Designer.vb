<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDisplayModeSelection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDisplayModeSelection))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button_Apply = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RadioButton_ModeDirect = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RadioButton_ModeVirtual = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.Button_Apply)
        Me.Panel1.Controls.Add(Me.Button_Cancel)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 361)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(553, 48)
        Me.Panel1.TabIndex = 0
        '
        'Button_Apply
        '
        Me.Button_Apply.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Apply.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_Apply.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_Apply.Location = New System.Drawing.Point(363, 13)
        Me.Button_Apply.Name = "Button_Apply"
        Me.Button_Apply.Size = New System.Drawing.Size(86, 23)
        Me.Button_Apply.TabIndex = 2
        Me.Button_Apply.Text = "Apply"
        Me.Button_Apply.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_Cancel.Location = New System.Drawing.Point(455, 13)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(86, 23)
        Me.Button_Cancel.TabIndex = 1
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(553, 1)
        Me.Panel2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.RadioButton_ModeDirect)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 207)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(529, 105)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(3, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(3)
        Me.Label2.Size = New System.Drawing.Size(523, 84)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'RadioButton_ModeDirect
        '
        Me.RadioButton_ModeDirect.AutoSize = True
        Me.RadioButton_ModeDirect.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton_ModeDirect.Location = New System.Drawing.Point(6, 0)
        Me.RadioButton_ModeDirect.Name = "RadioButton_ModeDirect"
        Me.RadioButton_ModeDirect.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton_ModeDirect.TabIndex = 0
        Me.RadioButton_ModeDirect.Text = "Direct-Mode"
        Me.RadioButton_ModeDirect.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.RadioButton_ModeVirtual)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 82)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(529, 119)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(3, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(3)
        Me.Label3.Size = New System.Drawing.Size(523, 98)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = resources.GetString("Label3.Text")
        '
        'RadioButton_ModeVirtual
        '
        Me.RadioButton_ModeVirtual.AutoSize = True
        Me.RadioButton_ModeVirtual.Checked = True
        Me.RadioButton_ModeVirtual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton_ModeVirtual.Location = New System.Drawing.Point(6, 0)
        Me.RadioButton_ModeVirtual.Name = "RadioButton_ModeVirtual"
        Me.RadioButton_ModeVirtual.Size = New System.Drawing.Size(183, 17)
        Me.RadioButton_ModeVirtual.TabIndex = 1
        Me.RadioButton_ModeVirtual.TabStop = True
        Me.RadioButton_ModeVirtual.Text = "Virtual-Mode (Recommended)"
        Me.RadioButton_ModeVirtual.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(82, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(278, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Select a display mode for the PlayStation VR display."
        '
        'ClassPictureBoxQuality1
        '
        Me.ClassPictureBoxQuality1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources._441
        Me.ClassPictureBoxQuality1.Location = New System.Drawing.Point(12, 12)
        Me.ClassPictureBoxQuality1.m_HighQuality = True
        Me.ClassPictureBoxQuality1.Name = "ClassPictureBoxQuality1"
        Me.ClassPictureBoxQuality1.Size = New System.Drawing.Size(64, 64)
        Me.ClassPictureBoxQuality1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality1.TabIndex = 1
        Me.ClassPictureBoxQuality1.TabStop = False
        '
        'FormDisplayModeSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(553, 409)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ClassPictureBoxQuality1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormDisplayModeSelection"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Display Mode Selection"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ClassPictureBoxQuality1 As ClassPictureBoxQuality
    Friend WithEvents Button_Apply As Button
    Friend WithEvents Button_Cancel As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioButton_ModeDirect As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RadioButton_ModeVirtual As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
