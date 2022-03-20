<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCVirtualControllers
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox_VirtualControllerCount = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage_RemoteSettings = New System.Windows.Forms.TabPage()
        Me.ComboBox_PSmoveEmu = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBox_PSmoveEmu = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 64)
        Me.Panel1.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(3, 30)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(794, 30)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Create virtual controllers in PSMoveService and control their orientation data an" &
    "d more."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 3)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(150, 21)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Virtual Controllers"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(0, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(800, 1)
        Me.Label1.TabIndex = 0
        '
        'ComboBox_VirtualControllerCount
        '
        Me.ComboBox_VirtualControllerCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_VirtualControllerCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_VirtualControllerCount.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_VirtualControllerCount.FormattingEnabled = True
        Me.ComboBox_VirtualControllerCount.Location = New System.Drawing.Point(199, 80)
        Me.ComboBox_VirtualControllerCount.Name = "ComboBox_VirtualControllerCount"
        Me.ComboBox_VirtualControllerCount.Size = New System.Drawing.Size(488, 21)
        Me.ComboBox_VirtualControllerCount.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(32, 83)
        Me.Label5.Margin = New System.Windows.Forms.Padding(32, 16, 6, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(130, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Virtual controller count:"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage_RemoteSettings)
        Me.TabControl1.Location = New System.Drawing.Point(32, 163)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(32)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(736, 397)
        Me.TabControl1.TabIndex = 11
        '
        'TabPage_RemoteSettings
        '
        Me.TabPage_RemoteSettings.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_RemoteSettings.Name = "TabPage_RemoteSettings"
        Me.TabPage_RemoteSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_RemoteSettings.Size = New System.Drawing.Size(728, 371)
        Me.TabPage_RemoteSettings.TabIndex = 0
        Me.TabPage_RemoteSettings.Text = "Remote Devices"
        Me.TabPage_RemoteSettings.UseVisualStyleBackColor = True
        '
        'ComboBox_PSmoveEmu
        '
        Me.ComboBox_PSmoveEmu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_PSmoveEmu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_PSmoveEmu.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_PSmoveEmu.FormattingEnabled = True
        Me.ComboBox_PSmoveEmu.Location = New System.Drawing.Point(199, 115)
        Me.ComboBox_PSmoveEmu.Margin = New System.Windows.Forms.Padding(3, 6, 16, 3)
        Me.ComboBox_PSmoveEmu.Name = "ComboBox_PSmoveEmu"
        Me.ComboBox_PSmoveEmu.Size = New System.Drawing.Size(401, 21)
        Me.ComboBox_PSmoveEmu.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 118)
        Me.Label3.Margin = New System.Windows.Forms.Padding(32, 16, 6, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "PSmove Emulation:"
        '
        'CheckBox_PSmoveEmu
        '
        Me.CheckBox_PSmoveEmu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_PSmoveEmu.AutoSize = True
        Me.CheckBox_PSmoveEmu.Location = New System.Drawing.Point(619, 117)
        Me.CheckBox_PSmoveEmu.Name = "CheckBox_PSmoveEmu"
        Me.CheckBox_PSmoveEmu.Size = New System.Drawing.Size(68, 17)
        Me.CheckBox_PSmoveEmu.TabIndex = 14
        Me.CheckBox_PSmoveEmu.Text = "Enabled"
        Me.CheckBox_PSmoveEmu.UseVisualStyleBackColor = True
        '
        'UCVirtualControllers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.CheckBox_PSmoveEmu)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBox_PSmoveEmu)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ComboBox_VirtualControllerCount)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVirtualControllers"
        Me.Size = New System.Drawing.Size(800, 592)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox_VirtualControllerCount As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage_RemoteSettings As TabPage
    Friend WithEvents ComboBox_PSmoveEmu As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents CheckBox_PSmoveEmu As CheckBox
End Class
