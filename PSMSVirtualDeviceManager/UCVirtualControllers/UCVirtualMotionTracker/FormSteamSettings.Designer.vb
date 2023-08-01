<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSteamSettings
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
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CheckBox_EnableNullDriver = New System.Windows.Forms.CheckBox()
        Me.CheckBox_ForceNullDriver = New System.Windows.Forms.CheckBox()
        Me.CheckBox_RequireHmd = New System.Windows.Forms.CheckBox()
        Me.CheckBox_EnableMultipleDrivers = New System.Windows.Forms.CheckBox()
        Me.CheckBox_EnableHome = New System.Windows.Forms.CheckBox()
        Me.CheckBox_EnableMirror = New System.Windows.Forms.CheckBox()
        Me.CheckBox_EnablePerfGraph = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.LinkLabel_ResetForcedDrvier = New System.Windows.Forms.LinkLabel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(364, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Change advanced SteamVR settings to enhance your user experience."
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 233)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(464, 48)
        Me.Panel1.TabIndex = 23
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button2.Location = New System.Drawing.Point(254, 13)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Apply"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.Location = New System.Drawing.Point(356, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Close"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(464, 1)
        Me.Panel2.TabIndex = 0
        '
        'CheckBox_EnableNullDriver
        '
        Me.CheckBox_EnableNullDriver.AutoSize = True
        Me.CheckBox_EnableNullDriver.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_EnableNullDriver.Location = New System.Drawing.Point(25, 38)
        Me.CheckBox_EnableNullDriver.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.CheckBox_EnableNullDriver.Name = "CheckBox_EnableNullDriver"
        Me.CheckBox_EnableNullDriver.Size = New System.Drawing.Size(124, 18)
        Me.CheckBox_EnableNullDriver.TabIndex = 24
        Me.CheckBox_EnableNullDriver.Text = "Enable Null Driver"
        Me.ToolTip1.SetToolTip(Me.CheckBox_EnableNullDriver, "Enables a dummy head mount display when enabled. Allowed to see VR view on your d" &
        "esktop and fully simulates a head mount display.")
        Me.CheckBox_EnableNullDriver.UseVisualStyleBackColor = True
        '
        'CheckBox_ForceNullDriver
        '
        Me.CheckBox_ForceNullDriver.AutoSize = True
        Me.CheckBox_ForceNullDriver.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_ForceNullDriver.Location = New System.Drawing.Point(57, 62)
        Me.CheckBox_ForceNullDriver.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.CheckBox_ForceNullDriver.Name = "CheckBox_ForceNullDriver"
        Me.CheckBox_ForceNullDriver.Size = New System.Drawing.Size(117, 18)
        Me.CheckBox_ForceNullDriver.TabIndex = 25
        Me.CheckBox_ForceNullDriver.Text = "Force Null Driver"
        Me.ToolTip1.SetToolTip(Me.CheckBox_ForceNullDriver, "Always enforces the Null Driver and ignores any other driver.")
        Me.CheckBox_ForceNullDriver.UseVisualStyleBackColor = True
        '
        'CheckBox_RequireHmd
        '
        Me.CheckBox_RequireHmd.AutoSize = True
        Me.CheckBox_RequireHmd.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_RequireHmd.Location = New System.Drawing.Point(25, 85)
        Me.CheckBox_RequireHmd.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_RequireHmd.Name = "CheckBox_RequireHmd"
        Me.CheckBox_RequireHmd.Size = New System.Drawing.Size(180, 18)
        Me.CheckBox_RequireHmd.TabIndex = 26
        Me.CheckBox_RequireHmd.Text = "Require Head Mount Display"
        Me.ToolTip1.SetToolTip(Me.CheckBox_RequireHmd, "Make SteamVR require a head mount display. Disable this if you want to run SteamV" &
        "R without any head mount display.")
        Me.CheckBox_RequireHmd.UseVisualStyleBackColor = True
        '
        'CheckBox_EnableMultipleDrivers
        '
        Me.CheckBox_EnableMultipleDrivers.AutoSize = True
        Me.CheckBox_EnableMultipleDrivers.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_EnableMultipleDrivers.Location = New System.Drawing.Point(25, 109)
        Me.CheckBox_EnableMultipleDrivers.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_EnableMultipleDrivers.Name = "CheckBox_EnableMultipleDrivers"
        Me.CheckBox_EnableMultipleDrivers.Size = New System.Drawing.Size(156, 18)
        Me.CheckBox_EnableMultipleDrivers.TabIndex = 27
        Me.CheckBox_EnableMultipleDrivers.Text = "Activate Multiple Drivers"
        Me.ToolTip1.SetToolTip(Me.CheckBox_EnableMultipleDrivers, "Allows to run multiple drivers. This should be enabled if you use third party tra" &
        "ckers.")
        Me.CheckBox_EnableMultipleDrivers.UseVisualStyleBackColor = True
        '
        'CheckBox_EnableHome
        '
        Me.CheckBox_EnableHome.AutoSize = True
        Me.CheckBox_EnableHome.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_EnableHome.Location = New System.Drawing.Point(25, 133)
        Me.CheckBox_EnableHome.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_EnableHome.Name = "CheckBox_EnableHome"
        Me.CheckBox_EnableHome.Size = New System.Drawing.Size(148, 18)
        Me.CheckBox_EnableHome.TabIndex = 28
        Me.CheckBox_EnableHome.Text = "Enable SteamVR Home"
        Me.ToolTip1.SetToolTip(Me.CheckBox_EnableHome, "Show SteamVR Home when no game is running when enabled. It is recommended to disa" &
        "ble SteamVR Home because it makes it easier to see connected trackers.")
        Me.CheckBox_EnableHome.UseVisualStyleBackColor = True
        '
        'CheckBox_EnableMirror
        '
        Me.CheckBox_EnableMirror.AutoSize = True
        Me.CheckBox_EnableMirror.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_EnableMirror.Location = New System.Drawing.Point(25, 157)
        Me.CheckBox_EnableMirror.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_EnableMirror.Name = "CheckBox_EnableMirror"
        Me.CheckBox_EnableMirror.Size = New System.Drawing.Size(150, 18)
        Me.CheckBox_EnableMirror.TabIndex = 29
        Me.CheckBox_EnableMirror.Text = "Enable SteamVR Mirror"
        Me.ToolTip1.SetToolTip(Me.CheckBox_EnableMirror, "Enables the SteamVR mirror to view the head mount display's perspective.")
        Me.CheckBox_EnableMirror.UseVisualStyleBackColor = True
        '
        'CheckBox_EnablePerfGraph
        '
        Me.CheckBox_EnablePerfGraph.AutoSize = True
        Me.CheckBox_EnablePerfGraph.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBox_EnablePerfGraph.Location = New System.Drawing.Point(25, 181)
        Me.CheckBox_EnablePerfGraph.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.CheckBox_EnablePerfGraph.Name = "CheckBox_EnablePerfGraph"
        Me.CheckBox_EnablePerfGraph.Size = New System.Drawing.Size(169, 18)
        Me.CheckBox_EnablePerfGraph.TabIndex = 30
        Me.CheckBox_EnablePerfGraph.Text = "Enable Performance Graph"
        Me.CheckBox_EnablePerfGraph.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.AutomaticDelay = 100
        Me.ToolTip1.AutoPopDelay = 30000
        Me.ToolTip1.InitialDelay = 100
        Me.ToolTip1.ReshowDelay = 20
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = "Info"
        '
        'LinkLabel_ResetForcedDrvier
        '
        Me.LinkLabel_ResetForcedDrvier.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ResetForcedDrvier.AutoSize = True
        Me.LinkLabel_ResetForcedDrvier.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ResetForcedDrvier.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ResetForcedDrvier.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ResetForcedDrvier.Location = New System.Drawing.Point(176, 64)
        Me.LinkLabel_ResetForcedDrvier.Margin = New System.Windows.Forms.Padding(0)
        Me.LinkLabel_ResetForcedDrvier.Name = "LinkLabel_ResetForcedDrvier"
        Me.LinkLabel_ResetForcedDrvier.Size = New System.Drawing.Size(276, 13)
        Me.LinkLabel_ResetForcedDrvier.TabIndex = 31
        Me.LinkLabel_ResetForcedDrvier.TabStop = True
        Me.LinkLabel_ResetForcedDrvier.Text = "Another driver is already used, click here to override."
        Me.LinkLabel_ResetForcedDrvier.Visible = False
        Me.LinkLabel_ResetForcedDrvier.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'FormSteamSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(464, 281)
        Me.Controls.Add(Me.LinkLabel_ResetForcedDrvier)
        Me.Controls.Add(Me.CheckBox_EnablePerfGraph)
        Me.Controls.Add(Me.CheckBox_EnableMirror)
        Me.Controls.Add(Me.CheckBox_EnableHome)
        Me.Controls.Add(Me.CheckBox_EnableMultipleDrivers)
        Me.Controls.Add(Me.CheckBox_RequireHmd)
        Me.Controls.Add(Me.CheckBox_ForceNullDriver)
        Me.Controls.Add(Me.CheckBox_EnableNullDriver)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(480, 320)
        Me.Name = "FormSteamSettings"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SteamVR Settings"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents CheckBox_EnableNullDriver As CheckBox
    Friend WithEvents CheckBox_ForceNullDriver As CheckBox
    Friend WithEvents CheckBox_RequireHmd As CheckBox
    Friend WithEvents CheckBox_EnableMultipleDrivers As CheckBox
    Friend WithEvents CheckBox_EnableHome As CheckBox
    Friend WithEvents CheckBox_EnableMirror As CheckBox
    Friend WithEvents CheckBox_EnablePerfGraph As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents LinkLabel_ResetForcedDrvier As LinkLabel
End Class
