<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.Panel_Pages = New System.Windows.Forms.Panel()
        Me.ToolTip_Service = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button_NavRunSteamVR = New System.Windows.Forms.Button()
        Me.Button_NavGitHub = New System.Windows.Forms.Button()
        Me.Button_NavUpdate = New System.Windows.Forms.Button()
        Me.Button_NavVersion = New System.Windows.Forms.Button()
        Me.Button_NavStartPlayCalib = New System.Windows.Forms.Button()
        Me.Button_NavPlayCalibStatus = New System.Windows.Forms.Button()
        Me.Button_NavStartOsc = New System.Windows.Forms.Button()
        Me.Button_NavOscStatus = New System.Windows.Forms.Button()
        Me.Button_NavVirtualMotionTrackers = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Button_NavVirtualTrackers = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Button_NavHeadMountedDisplay = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Button_NavStartRemoteSocket = New System.Windows.Forms.Button()
        Me.Button_NavRemoteDeviceStatus = New System.Windows.Forms.Button()
        Me.Button_NavVirtualControllers = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Button_NavPsvrStatus = New System.Windows.Forms.Button()
        Me.Button_NavPsvrManagement = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button_NavRunServiceConfigTool = New System.Windows.Forms.Button()
        Me.Button_NavRestartService = New System.Windows.Forms.Button()
        Me.Button_NavRunService = New System.Windows.Forms.Button()
        Me.Button_NavServiceStatus = New System.Windows.Forms.Button()
        Me.Button_NavServiceManagement = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Pages
        '
        Me.Panel_Pages.AutoScroll = True
        Me.Panel_Pages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Pages.Location = New System.Drawing.Point(229, 0)
        Me.Panel_Pages.Name = "Panel_Pages"
        Me.Panel_Pages.Size = New System.Drawing.Size(830, 761)
        Me.Panel_Pages.TabIndex = 1
        '
        'ToolTip_Service
        '
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.AutoScrollMinSize = New System.Drawing.Size(0, 550)
        Me.Panel1.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Button_NavRunSteamVR)
        Me.Panel1.Controls.Add(Me.Button_NavGitHub)
        Me.Panel1.Controls.Add(Me.Button_NavUpdate)
        Me.Panel1.Controls.Add(Me.Button_NavVersion)
        Me.Panel1.Controls.Add(Me.Button_NavStartPlayCalib)
        Me.Panel1.Controls.Add(Me.Button_NavPlayCalibStatus)
        Me.Panel1.Controls.Add(Me.Button_NavStartOsc)
        Me.Panel1.Controls.Add(Me.Button_NavOscStatus)
        Me.Panel1.Controls.Add(Me.Button_NavVirtualMotionTrackers)
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Controls.Add(Me.Button_NavVirtualTrackers)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.Button_NavHeadMountedDisplay)
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Controls.Add(Me.Button_NavStartRemoteSocket)
        Me.Panel1.Controls.Add(Me.Button_NavRemoteDeviceStatus)
        Me.Panel1.Controls.Add(Me.Button_NavVirtualControllers)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Button_NavPsvrStatus)
        Me.Panel1.Controls.Add(Me.Button_NavPsvrManagement)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Button_NavRunServiceConfigTool)
        Me.Panel1.Controls.Add(Me.Button_NavRestartService)
        Me.Panel1.Controls.Add(Me.Button_NavRunService)
        Me.Panel1.Controls.Add(Me.Button_NavServiceStatus)
        Me.Panel1.Controls.Add(Me.Button_NavServiceManagement)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(229, 761)
        Me.Panel1.TabIndex = 2
        '
        'Button_NavRunSteamVR
        '
        Me.Button_NavRunSteamVR.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavRunSteamVR.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button_NavRunSteamVR.FlatAppearance.BorderSize = 0
        Me.Button_NavRunSteamVR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavRunSteamVR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavRunSteamVR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavRunSteamVR.ForeColor = System.Drawing.Color.Black
        Me.Button_NavRunSteamVR.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5323_16x16_32
        Me.Button_NavRunSteamVR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavRunSteamVR.Location = New System.Drawing.Point(0, 665)
        Me.Button_NavRunSteamVR.Name = "Button_NavRunSteamVR"
        Me.Button_NavRunSteamVR.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.Button_NavRunSteamVR.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavRunSteamVR.TabIndex = 62
        Me.Button_NavRunSteamVR.Text = "Launch SteamVR"
        Me.Button_NavRunSteamVR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavRunSteamVR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavRunSteamVR.UseVisualStyleBackColor = True
        '
        'Button_NavGitHub
        '
        Me.Button_NavGitHub.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavGitHub.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button_NavGitHub.FlatAppearance.BorderSize = 0
        Me.Button_NavGitHub.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavGitHub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavGitHub.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavGitHub.ForeColor = System.Drawing.Color.Black
        Me.Button_NavGitHub.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netcenter_7_16x16_32
        Me.Button_NavGitHub.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavGitHub.Location = New System.Drawing.Point(0, 689)
        Me.Button_NavGitHub.Name = "Button_NavGitHub"
        Me.Button_NavGitHub.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.Button_NavGitHub.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavGitHub.TabIndex = 61
        Me.Button_NavGitHub.Text = "GitHub"
        Me.Button_NavGitHub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavGitHub.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavGitHub.UseVisualStyleBackColor = True
        '
        'Button_NavUpdate
        '
        Me.Button_NavUpdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavUpdate.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button_NavUpdate.FlatAppearance.BorderSize = 0
        Me.Button_NavUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavUpdate.ForeColor = System.Drawing.Color.Black
        Me.Button_NavUpdate.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_16x16_32
        Me.Button_NavUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavUpdate.Location = New System.Drawing.Point(0, 713)
        Me.Button_NavUpdate.Name = "Button_NavUpdate"
        Me.Button_NavUpdate.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.Button_NavUpdate.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavUpdate.TabIndex = 60
        Me.Button_NavUpdate.Text = "Check for Updates"
        Me.Button_NavUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavUpdate.UseVisualStyleBackColor = True
        '
        'Button_NavVersion
        '
        Me.Button_NavVersion.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button_NavVersion.FlatAppearance.BorderSize = 0
        Me.Button_NavVersion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavVersion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavVersion.ForeColor = System.Drawing.Color.Gray
        Me.Button_NavVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavVersion.Location = New System.Drawing.Point(0, 737)
        Me.Button_NavVersion.Name = "Button_NavVersion"
        Me.Button_NavVersion.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.Button_NavVersion.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavVersion.TabIndex = 59
        Me.Button_NavVersion.Text = "Version: 1.0"
        Me.Button_NavVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavVersion.UseVisualStyleBackColor = True
        '
        'Button_NavStartPlayCalib
        '
        Me.Button_NavStartPlayCalib.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavStartPlayCalib.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavStartPlayCalib.FlatAppearance.BorderSize = 0
        Me.Button_NavStartPlayCalib.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavStartPlayCalib.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavStartPlayCalib.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavStartPlayCalib.ForeColor = System.Drawing.Color.Black
        Me.Button_NavStartPlayCalib.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.Button_NavStartPlayCalib.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavStartPlayCalib.Location = New System.Drawing.Point(0, 469)
        Me.Button_NavStartPlayCalib.Name = "Button_NavStartPlayCalib"
        Me.Button_NavStartPlayCalib.Padding = New System.Windows.Forms.Padding(48, 0, 0, 0)
        Me.Button_NavStartPlayCalib.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavStartPlayCalib.TabIndex = 57
        Me.Button_NavStartPlayCalib.Text = "Start Calibration"
        Me.Button_NavStartPlayCalib.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavStartPlayCalib.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavStartPlayCalib.UseVisualStyleBackColor = True
        '
        'Button_NavPlayCalibStatus
        '
        Me.Button_NavPlayCalibStatus.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavPlayCalibStatus.FlatAppearance.BorderSize = 0
        Me.Button_NavPlayCalibStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavPlayCalibStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavPlayCalibStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavPlayCalibStatus.ForeColor = System.Drawing.Color.Gray
        Me.Button_NavPlayCalibStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Status_WHITE_16
        Me.Button_NavPlayCalibStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavPlayCalibStatus.Location = New System.Drawing.Point(0, 445)
        Me.Button_NavPlayCalibStatus.Name = "Button_NavPlayCalibStatus"
        Me.Button_NavPlayCalibStatus.Padding = New System.Windows.Forms.Padding(32, 0, 0, 0)
        Me.Button_NavPlayCalibStatus.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavPlayCalibStatus.TabIndex = 56
        Me.Button_NavPlayCalibStatus.Text = "Playspace Calibration Status"
        Me.Button_NavPlayCalibStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavPlayCalibStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavPlayCalibStatus.UseVisualStyleBackColor = True
        '
        'Button_NavStartOsc
        '
        Me.Button_NavStartOsc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavStartOsc.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavStartOsc.FlatAppearance.BorderSize = 0
        Me.Button_NavStartOsc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavStartOsc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavStartOsc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavStartOsc.ForeColor = System.Drawing.Color.Black
        Me.Button_NavStartOsc.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.Button_NavStartOsc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavStartOsc.Location = New System.Drawing.Point(0, 421)
        Me.Button_NavStartOsc.Name = "Button_NavStartOsc"
        Me.Button_NavStartOsc.Padding = New System.Windows.Forms.Padding(48, 0, 0, 0)
        Me.Button_NavStartOsc.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavStartOsc.TabIndex = 55
        Me.Button_NavStartOsc.Text = "Start Socket"
        Me.Button_NavStartOsc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavStartOsc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavStartOsc.UseVisualStyleBackColor = True
        '
        'Button_NavOscStatus
        '
        Me.Button_NavOscStatus.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavOscStatus.FlatAppearance.BorderSize = 0
        Me.Button_NavOscStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavOscStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavOscStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavOscStatus.ForeColor = System.Drawing.Color.Gray
        Me.Button_NavOscStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Status_WHITE_16
        Me.Button_NavOscStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavOscStatus.Location = New System.Drawing.Point(0, 397)
        Me.Button_NavOscStatus.Name = "Button_NavOscStatus"
        Me.Button_NavOscStatus.Padding = New System.Windows.Forms.Padding(32, 0, 0, 0)
        Me.Button_NavOscStatus.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavOscStatus.TabIndex = 54
        Me.Button_NavOscStatus.Text = "OSC Status"
        Me.Button_NavOscStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavOscStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavOscStatus.UseVisualStyleBackColor = True
        '
        'Button_NavVirtualMotionTrackers
        '
        Me.Button_NavVirtualMotionTrackers.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavVirtualMotionTrackers.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavVirtualMotionTrackers.FlatAppearance.BorderSize = 0
        Me.Button_NavVirtualMotionTrackers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavVirtualMotionTrackers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavVirtualMotionTrackers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavVirtualMotionTrackers.ForeColor = System.Drawing.Color.Black
        Me.Button_NavVirtualMotionTrackers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.Button_NavVirtualMotionTrackers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavVirtualMotionTrackers.Location = New System.Drawing.Point(0, 373)
        Me.Button_NavVirtualMotionTrackers.Name = "Button_NavVirtualMotionTrackers"
        Me.Button_NavVirtualMotionTrackers.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.Button_NavVirtualMotionTrackers.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavVirtualMotionTrackers.TabIndex = 53
        Me.Button_NavVirtualMotionTrackers.Text = "Virtual Motion Trackers"
        Me.Button_NavVirtualMotionTrackers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavVirtualMotionTrackers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavVirtualMotionTrackers.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 365)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(228, 8)
        Me.Panel6.TabIndex = 52
        '
        'Button_NavVirtualTrackers
        '
        Me.Button_NavVirtualTrackers.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavVirtualTrackers.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavVirtualTrackers.FlatAppearance.BorderSize = 0
        Me.Button_NavVirtualTrackers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavVirtualTrackers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavVirtualTrackers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavVirtualTrackers.ForeColor = System.Drawing.Color.Black
        Me.Button_NavVirtualTrackers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.Button_NavVirtualTrackers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavVirtualTrackers.Location = New System.Drawing.Point(0, 341)
        Me.Button_NavVirtualTrackers.Name = "Button_NavVirtualTrackers"
        Me.Button_NavVirtualTrackers.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.Button_NavVirtualTrackers.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavVirtualTrackers.TabIndex = 50
        Me.Button_NavVirtualTrackers.Text = "Virtual Trackers"
        Me.Button_NavVirtualTrackers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavVirtualTrackers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavVirtualTrackers.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 333)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(228, 8)
        Me.Panel5.TabIndex = 51
        '
        'Button_NavHeadMountedDisplay
        '
        Me.Button_NavHeadMountedDisplay.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavHeadMountedDisplay.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavHeadMountedDisplay.FlatAppearance.BorderSize = 0
        Me.Button_NavHeadMountedDisplay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavHeadMountedDisplay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavHeadMountedDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavHeadMountedDisplay.ForeColor = System.Drawing.Color.Black
        Me.Button_NavHeadMountedDisplay.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.Button_NavHeadMountedDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavHeadMountedDisplay.Location = New System.Drawing.Point(0, 309)
        Me.Button_NavHeadMountedDisplay.Name = "Button_NavHeadMountedDisplay"
        Me.Button_NavHeadMountedDisplay.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.Button_NavHeadMountedDisplay.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavHeadMountedDisplay.TabIndex = 49
        Me.Button_NavHeadMountedDisplay.Text = "Virtual Head-Mounted Displays"
        Me.Button_NavHeadMountedDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavHeadMountedDisplay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavHeadMountedDisplay.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 301)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(228, 8)
        Me.Panel7.TabIndex = 58
        '
        'Button_NavStartRemoteSocket
        '
        Me.Button_NavStartRemoteSocket.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavStartRemoteSocket.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavStartRemoteSocket.FlatAppearance.BorderSize = 0
        Me.Button_NavStartRemoteSocket.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavStartRemoteSocket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavStartRemoteSocket.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavStartRemoteSocket.ForeColor = System.Drawing.Color.Black
        Me.Button_NavStartRemoteSocket.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.Button_NavStartRemoteSocket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavStartRemoteSocket.Location = New System.Drawing.Point(0, 277)
        Me.Button_NavStartRemoteSocket.Name = "Button_NavStartRemoteSocket"
        Me.Button_NavStartRemoteSocket.Padding = New System.Windows.Forms.Padding(48, 0, 0, 0)
        Me.Button_NavStartRemoteSocket.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavStartRemoteSocket.TabIndex = 48
        Me.Button_NavStartRemoteSocket.Text = "Start Socket"
        Me.Button_NavStartRemoteSocket.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavStartRemoteSocket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavStartRemoteSocket.UseVisualStyleBackColor = True
        '
        'Button_NavRemoteDeviceStatus
        '
        Me.Button_NavRemoteDeviceStatus.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavRemoteDeviceStatus.FlatAppearance.BorderSize = 0
        Me.Button_NavRemoteDeviceStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavRemoteDeviceStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavRemoteDeviceStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavRemoteDeviceStatus.ForeColor = System.Drawing.Color.Gray
        Me.Button_NavRemoteDeviceStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Status_WHITE_16
        Me.Button_NavRemoteDeviceStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavRemoteDeviceStatus.Location = New System.Drawing.Point(0, 253)
        Me.Button_NavRemoteDeviceStatus.Name = "Button_NavRemoteDeviceStatus"
        Me.Button_NavRemoteDeviceStatus.Padding = New System.Windows.Forms.Padding(32, 0, 0, 0)
        Me.Button_NavRemoteDeviceStatus.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavRemoteDeviceStatus.TabIndex = 47
        Me.Button_NavRemoteDeviceStatus.Text = "Remote Device Status"
        Me.Button_NavRemoteDeviceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavRemoteDeviceStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavRemoteDeviceStatus.UseVisualStyleBackColor = True
        '
        'Button_NavVirtualControllers
        '
        Me.Button_NavVirtualControllers.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavVirtualControllers.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavVirtualControllers.FlatAppearance.BorderSize = 0
        Me.Button_NavVirtualControllers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavVirtualControllers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavVirtualControllers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavVirtualControllers.ForeColor = System.Drawing.Color.Black
        Me.Button_NavVirtualControllers.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.Button_NavVirtualControllers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavVirtualControllers.Location = New System.Drawing.Point(0, 229)
        Me.Button_NavVirtualControllers.Name = "Button_NavVirtualControllers"
        Me.Button_NavVirtualControllers.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.Button_NavVirtualControllers.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavVirtualControllers.TabIndex = 46
        Me.Button_NavVirtualControllers.Text = "Virtual Controllers"
        Me.Button_NavVirtualControllers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavVirtualControllers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavVirtualControllers.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 221)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(228, 8)
        Me.Panel4.TabIndex = 45
        '
        'Button_NavPsvrStatus
        '
        Me.Button_NavPsvrStatus.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavPsvrStatus.FlatAppearance.BorderSize = 0
        Me.Button_NavPsvrStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavPsvrStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavPsvrStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavPsvrStatus.ForeColor = System.Drawing.Color.Gray
        Me.Button_NavPsvrStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Status_WHITE_16
        Me.Button_NavPsvrStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavPsvrStatus.Location = New System.Drawing.Point(0, 197)
        Me.Button_NavPsvrStatus.Name = "Button_NavPsvrStatus"
        Me.Button_NavPsvrStatus.Padding = New System.Windows.Forms.Padding(32, 0, 0, 0)
        Me.Button_NavPsvrStatus.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavPsvrStatus.TabIndex = 43
        Me.Button_NavPsvrStatus.Text = "PSVR Management Status"
        Me.Button_NavPsvrStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavPsvrStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavPsvrStatus.UseVisualStyleBackColor = True
        '
        'Button_NavPsvrManagement
        '
        Me.Button_NavPsvrManagement.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavPsvrManagement.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavPsvrManagement.FlatAppearance.BorderSize = 0
        Me.Button_NavPsvrManagement.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavPsvrManagement.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavPsvrManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavPsvrManagement.ForeColor = System.Drawing.Color.Black
        Me.Button_NavPsvrManagement.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.Button_NavPsvrManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavPsvrManagement.Location = New System.Drawing.Point(0, 173)
        Me.Button_NavPsvrManagement.Name = "Button_NavPsvrManagement"
        Me.Button_NavPsvrManagement.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.Button_NavPsvrManagement.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavPsvrManagement.TabIndex = 42
        Me.Button_NavPsvrManagement.Text = "PlayStation VR Management"
        Me.Button_NavPsvrManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavPsvrManagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavPsvrManagement.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 165)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(228, 8)
        Me.Panel2.TabIndex = 44
        '
        'Button_NavRunServiceConfigTool
        '
        Me.Button_NavRunServiceConfigTool.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavRunServiceConfigTool.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavRunServiceConfigTool.FlatAppearance.BorderSize = 0
        Me.Button_NavRunServiceConfigTool.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavRunServiceConfigTool.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavRunServiceConfigTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavRunServiceConfigTool.ForeColor = System.Drawing.Color.Black
        Me.Button_NavRunServiceConfigTool.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.mmcshext_128_16x16_32
        Me.Button_NavRunServiceConfigTool.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavRunServiceConfigTool.Location = New System.Drawing.Point(0, 141)
        Me.Button_NavRunServiceConfigTool.Name = "Button_NavRunServiceConfigTool"
        Me.Button_NavRunServiceConfigTool.Padding = New System.Windows.Forms.Padding(48, 0, 0, 0)
        Me.Button_NavRunServiceConfigTool.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavRunServiceConfigTool.TabIndex = 41
        Me.Button_NavRunServiceConfigTool.Text = "Run Service Config Tool"
        Me.Button_NavRunServiceConfigTool.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavRunServiceConfigTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavRunServiceConfigTool.UseVisualStyleBackColor = True
        '
        'Button_NavRestartService
        '
        Me.Button_NavRestartService.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavRestartService.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavRestartService.FlatAppearance.BorderSize = 0
        Me.Button_NavRestartService.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavRestartService.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavRestartService.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavRestartService.ForeColor = System.Drawing.Color.Black
        Me.Button_NavRestartService.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.shell32_16739_16x16_32
        Me.Button_NavRestartService.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavRestartService.Location = New System.Drawing.Point(0, 117)
        Me.Button_NavRestartService.Name = "Button_NavRestartService"
        Me.Button_NavRestartService.Padding = New System.Windows.Forms.Padding(48, 0, 0, 0)
        Me.Button_NavRestartService.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavRestartService.TabIndex = 40
        Me.Button_NavRestartService.Text = "Restart Service"
        Me.Button_NavRestartService.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavRestartService.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavRestartService.UseVisualStyleBackColor = True
        '
        'Button_NavRunService
        '
        Me.Button_NavRunService.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavRunService.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavRunService.FlatAppearance.BorderSize = 0
        Me.Button_NavRunService.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavRunService.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavRunService.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavRunService.ForeColor = System.Drawing.Color.Black
        Me.Button_NavRunService.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.Button_NavRunService.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavRunService.Location = New System.Drawing.Point(0, 93)
        Me.Button_NavRunService.Name = "Button_NavRunService"
        Me.Button_NavRunService.Padding = New System.Windows.Forms.Padding(48, 0, 0, 0)
        Me.Button_NavRunService.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavRunService.TabIndex = 39
        Me.Button_NavRunService.Text = "Run Service"
        Me.Button_NavRunService.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavRunService.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavRunService.UseVisualStyleBackColor = True
        '
        'Button_NavServiceStatus
        '
        Me.Button_NavServiceStatus.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavServiceStatus.FlatAppearance.BorderSize = 0
        Me.Button_NavServiceStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavServiceStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.GhostWhite
        Me.Button_NavServiceStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavServiceStatus.ForeColor = System.Drawing.Color.Gray
        Me.Button_NavServiceStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Status_WHITE_16
        Me.Button_NavServiceStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavServiceStatus.Location = New System.Drawing.Point(0, 69)
        Me.Button_NavServiceStatus.Name = "Button_NavServiceStatus"
        Me.Button_NavServiceStatus.Padding = New System.Windows.Forms.Padding(32, 0, 0, 0)
        Me.Button_NavServiceStatus.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavServiceStatus.TabIndex = 38
        Me.Button_NavServiceStatus.Text = "Service Management Status"
        Me.Button_NavServiceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavServiceStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavServiceStatus.UseVisualStyleBackColor = True
        '
        'Button_NavServiceManagement
        '
        Me.Button_NavServiceManagement.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button_NavServiceManagement.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button_NavServiceManagement.FlatAppearance.BorderSize = 0
        Me.Button_NavServiceManagement.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lavender
        Me.Button_NavServiceManagement.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lavender
        Me.Button_NavServiceManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NavServiceManagement.ForeColor = System.Drawing.Color.Black
        Me.Button_NavServiceManagement.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.odbcint_1439_16x16_32
        Me.Button_NavServiceManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavServiceManagement.Location = New System.Drawing.Point(0, 45)
        Me.Button_NavServiceManagement.Name = "Button_NavServiceManagement"
        Me.Button_NavServiceManagement.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.Button_NavServiceManagement.Size = New System.Drawing.Size(228, 24)
        Me.Button_NavServiceManagement.TabIndex = 37
        Me.Button_NavServiceManagement.Text = "Service Management"
        Me.Button_NavServiceManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_NavServiceManagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_NavServiceManagement.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.GhostWhite
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(16, 16, 3, 16)
        Me.Label2.Size = New System.Drawing.Size(84, 45)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Navigation"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(228, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1, 761)
        Me.Panel3.TabIndex = 0
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1059, 761)
        Me.Controls.Add(Me.Panel_Pages)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1000, 600)
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PSMoveServiceEx - Virtual Device Manager"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Pages As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ToolTip_Service As ToolTip
    Friend WithEvents Button_NavServiceManagement As Button
    Friend WithEvents Button_NavServiceStatus As Button
    Friend WithEvents Button_NavRunService As Button
    Friend WithEvents Button_NavRestartService As Button
    Friend WithEvents Button_NavRunServiceConfigTool As Button
    Friend WithEvents Button_NavPsvrManagement As Button
    Friend WithEvents Button_NavPsvrStatus As Button
    Friend WithEvents Button_NavVirtualControllers As Button
    Friend WithEvents Button_NavRemoteDeviceStatus As Button
    Friend WithEvents Button_NavStartRemoteSocket As Button
    Friend WithEvents Button_NavHeadMountedDisplay As Button
    Friend WithEvents Button_NavVirtualTrackers As Button
    Friend WithEvents Button_NavVirtualMotionTrackers As Button
    Friend WithEvents Button_NavStartOsc As Button
    Friend WithEvents Button_NavOscStatus As Button
    Friend WithEvents Button_NavStartPlayCalib As Button
    Friend WithEvents Button_NavPlayCalibStatus As Button
    Friend WithEvents Button_NavUpdate As Button
    Friend WithEvents Button_NavVersion As Button
    Friend WithEvents Button_NavRunSteamVR As Button
    Friend WithEvents Button_NavGitHub As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
End Class
