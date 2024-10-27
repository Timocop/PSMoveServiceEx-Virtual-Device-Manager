<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCVmtPlayspaceCalib
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCVmtPlayspaceCalib))
        Me.NumericUpDown_PlayCalibPrepTime = New System.Windows.Forms.NumericUpDown()
        Me.UcNumericUpDownBig1 = New PSMSVirtualDeviceManager.UCNumericUpDownBig()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Panel_PlayCalibSteps = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality_CalibStep5 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ProgressBar_PlayCalibStep5 = New System.Windows.Forms.ProgressBar()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality_CalibStep4 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ProgressBar_PlayCalibStep4 = New System.Windows.Forms.ProgressBar()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality_CalibStep3 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ProgressBar_PlayCalibStep3 = New System.Windows.Forms.ProgressBar()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality_CalibStep2 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ProgressBar_PlayCalibStep2 = New System.Windows.Forms.ProgressBar()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality_CalibStep1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ProgressBar_PlayCalibStep1 = New System.Windows.Forms.ProgressBar()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label_PlayCalibTitle = New System.Windows.Forms.Label()
        Me.Panel_PlayCalibStatus = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.ComboBox_PlayCalibControllerID = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.LinkLabel_PlayCalibShowSettings = New System.Windows.Forms.LinkLabel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Button_PlaySpaceManualCalib = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.ClassPictureBoxQuality3 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ToolTip_Info = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip_Default = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.NumericUpDown_PlayCalibPrepTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NumericUpDown_PlayCalibPrepTime.SuspendLayout()
        Me.Panel_PlayCalibSteps.SuspendLayout()
        Me.Panel15.SuspendLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel14.SuspendLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel13.SuspendLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel11.SuspendLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        CType(Me.ClassPictureBoxQuality3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NumericUpDown_PlayCalibPrepTime
        '
        Me.NumericUpDown_PlayCalibPrepTime.Controls.Add(Me.UcNumericUpDownBig1)
        Me.NumericUpDown_PlayCalibPrepTime.Location = New System.Drawing.Point(168, 134)
        Me.NumericUpDown_PlayCalibPrepTime.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.NumericUpDown_PlayCalibPrepTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_PlayCalibPrepTime.Name = "NumericUpDown_PlayCalibPrepTime"
        Me.NumericUpDown_PlayCalibPrepTime.Size = New System.Drawing.Size(132, 22)
        Me.NumericUpDown_PlayCalibPrepTime.TabIndex = 81
        Me.ToolTip_Default.SetToolTip(Me.NumericUpDown_PlayCalibPrepTime, "Default: 5")
        Me.NumericUpDown_PlayCalibPrepTime.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'UcNumericUpDownBig1
        '
        Me.UcNumericUpDownBig1.AutoSize = True
        Me.UcNumericUpDownBig1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.UcNumericUpDownBig1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.UcNumericUpDownBig1.Dock = System.Windows.Forms.DockStyle.Right
        Me.UcNumericUpDownBig1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcNumericUpDownBig1.Location = New System.Drawing.Point(66, 0)
        Me.UcNumericUpDownBig1.m_bDockOnControl = True
        Me.UcNumericUpDownBig1.m_NumericUpDown = Me.NumericUpDown_PlayCalibPrepTime
        Me.UcNumericUpDownBig1.m_ResetValue = New Decimal(New Integer() {5, 0, 0, 0})
        Me.UcNumericUpDownBig1.m_ResetVisible = True
        Me.UcNumericUpDownBig1.Name = "UcNumericUpDownBig1"
        Me.UcNumericUpDownBig1.Size = New System.Drawing.Size(66, 22)
        Me.UcNumericUpDownBig1.TabIndex = 82
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(16, 136)
        Me.Label41.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(146, 13)
        Me.Label41.TabIndex = 80
        Me.Label41.Text = "Preparation time (seconds):"
        '
        'Panel_PlayCalibSteps
        '
        Me.Panel_PlayCalibSteps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_PlayCalibSteps.AutoSize = True
        Me.Panel_PlayCalibSteps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel16)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel15)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel14)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel17)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel13)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel18)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel11)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel9)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel7)
        Me.Panel_PlayCalibSteps.Controls.Add(Me.Panel5)
        Me.Panel_PlayCalibSteps.Location = New System.Drawing.Point(16, 213)
        Me.Panel_PlayCalibSteps.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel_PlayCalibSteps.Name = "Panel_PlayCalibSteps"
        Me.Panel_PlayCalibSteps.Size = New System.Drawing.Size(768, 367)
        Me.Panel_PlayCalibSteps.TabIndex = 79
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.LightGray
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(0, 301)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(766, 1)
        Me.Panel16.TabIndex = 7
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.Label38)
        Me.Panel15.Controls.Add(Me.Label39)
        Me.Panel15.Controls.Add(Me.ClassPictureBoxQuality_CalibStep5)
        Me.Panel15.Controls.Add(Me.ProgressBar_PlayCalibStep5)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel15.Location = New System.Drawing.Point(0, 301)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(766, 64)
        Me.Panel15.TabIndex = 6
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(59, 22)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(240, 13)
        Me.Label38.TabIndex = 5
        Me.Label38.Text = "Playspace has been successfuly synchronized!"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(59, 6)
        Me.Label39.Margin = New System.Windows.Forms.Padding(3)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(162, 13)
        Me.Label39.TabIndex = 1
        Me.Label39.Text = "Step 5: Calibration Completed"
        '
        'ClassPictureBoxQuality_CalibStep5
        '
        Me.ClassPictureBoxQuality_CalibStep5.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClassPictureBoxQuality_CalibStep5.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_32x32_32
        Me.ClassPictureBoxQuality_CalibStep5.Location = New System.Drawing.Point(0, 0)
        Me.ClassPictureBoxQuality_CalibStep5.m_HighQuality = True
        Me.ClassPictureBoxQuality_CalibStep5.Name = "ClassPictureBoxQuality_CalibStep5"
        Me.ClassPictureBoxQuality_CalibStep5.Size = New System.Drawing.Size(53, 54)
        Me.ClassPictureBoxQuality_CalibStep5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ClassPictureBoxQuality_CalibStep5.TabIndex = 0
        Me.ClassPictureBoxQuality_CalibStep5.TabStop = False
        '
        'ProgressBar_PlayCalibStep5
        '
        Me.ProgressBar_PlayCalibStep5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar_PlayCalibStep5.Location = New System.Drawing.Point(0, 54)
        Me.ProgressBar_PlayCalibStep5.Name = "ProgressBar_PlayCalibStep5"
        Me.ProgressBar_PlayCalibStep5.Size = New System.Drawing.Size(766, 10)
        Me.ProgressBar_PlayCalibStep5.TabIndex = 4
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.Label36)
        Me.Panel14.Controls.Add(Me.Label37)
        Me.Panel14.Controls.Add(Me.ClassPictureBoxQuality_CalibStep4)
        Me.Panel14.Controls.Add(Me.ProgressBar_PlayCalibStep4)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(0, 237)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(766, 64)
        Me.Panel14.TabIndex = 5
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(59, 22)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(205, 13)
        Me.Label36.TabIndex = 5
        Me.Label36.Text = "Stand still to sample the ending point."
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(59, 6)
        Me.Label37.Margin = New System.Windows.Forms.Padding(3)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(148, 13)
        Me.Label37.TabIndex = 1
        Me.Label37.Text = "Step 4: Sampling End Point"
        '
        'ClassPictureBoxQuality_CalibStep4
        '
        Me.ClassPictureBoxQuality_CalibStep4.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClassPictureBoxQuality_CalibStep4.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_32x32_32
        Me.ClassPictureBoxQuality_CalibStep4.Location = New System.Drawing.Point(0, 0)
        Me.ClassPictureBoxQuality_CalibStep4.m_HighQuality = True
        Me.ClassPictureBoxQuality_CalibStep4.Name = "ClassPictureBoxQuality_CalibStep4"
        Me.ClassPictureBoxQuality_CalibStep4.Size = New System.Drawing.Size(53, 54)
        Me.ClassPictureBoxQuality_CalibStep4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ClassPictureBoxQuality_CalibStep4.TabIndex = 0
        Me.ClassPictureBoxQuality_CalibStep4.TabStop = False
        '
        'ProgressBar_PlayCalibStep4
        '
        Me.ProgressBar_PlayCalibStep4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar_PlayCalibStep4.Location = New System.Drawing.Point(0, 54)
        Me.ProgressBar_PlayCalibStep4.Name = "ProgressBar_PlayCalibStep4"
        Me.ProgressBar_PlayCalibStep4.Size = New System.Drawing.Size(766, 10)
        Me.ProgressBar_PlayCalibStep4.TabIndex = 4
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.LightGray
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17.Location = New System.Drawing.Point(0, 236)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(766, 1)
        Me.Panel17.TabIndex = 6
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.Label34)
        Me.Panel13.Controls.Add(Me.Label32)
        Me.Panel13.Controls.Add(Me.ClassPictureBoxQuality_CalibStep3)
        Me.Panel13.Controls.Add(Me.ProgressBar_PlayCalibStep3)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 172)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(766, 64)
        Me.Panel13.TabIndex = 4
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(59, 22)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(240, 13)
        Me.Label34.TabIndex = 5
        Me.Label34.Text = "Take some steps forward and then stand still."
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(59, 6)
        Me.Label32.Margin = New System.Windows.Forms.Padding(3)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(121, 13)
        Me.Label32.TabIndex = 1
        Me.Label32.Text = "Step 3: Move Forward"
        '
        'ClassPictureBoxQuality_CalibStep3
        '
        Me.ClassPictureBoxQuality_CalibStep3.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClassPictureBoxQuality_CalibStep3.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_32x32_32
        Me.ClassPictureBoxQuality_CalibStep3.Location = New System.Drawing.Point(0, 0)
        Me.ClassPictureBoxQuality_CalibStep3.m_HighQuality = True
        Me.ClassPictureBoxQuality_CalibStep3.Name = "ClassPictureBoxQuality_CalibStep3"
        Me.ClassPictureBoxQuality_CalibStep3.Size = New System.Drawing.Size(53, 54)
        Me.ClassPictureBoxQuality_CalibStep3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ClassPictureBoxQuality_CalibStep3.TabIndex = 0
        Me.ClassPictureBoxQuality_CalibStep3.TabStop = False
        '
        'ProgressBar_PlayCalibStep3
        '
        Me.ProgressBar_PlayCalibStep3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar_PlayCalibStep3.Location = New System.Drawing.Point(0, 54)
        Me.ProgressBar_PlayCalibStep3.Name = "ProgressBar_PlayCalibStep3"
        Me.ProgressBar_PlayCalibStep3.Size = New System.Drawing.Size(766, 10)
        Me.ProgressBar_PlayCalibStep3.TabIndex = 4
        '
        'Panel18
        '
        Me.Panel18.BackColor = System.Drawing.Color.LightGray
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel18.Location = New System.Drawing.Point(0, 171)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(766, 1)
        Me.Panel18.TabIndex = 8
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Label35)
        Me.Panel11.Controls.Add(Me.Label33)
        Me.Panel11.Controls.Add(Me.ClassPictureBoxQuality_CalibStep2)
        Me.Panel11.Controls.Add(Me.ProgressBar_PlayCalibStep2)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 107)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(766, 64)
        Me.Panel11.TabIndex = 3
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(59, 22)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(208, 13)
        Me.Label35.TabIndex = 6
        Me.Label35.Text = "Stand still to sample the starting point."
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(59, 6)
        Me.Label33.Margin = New System.Windows.Forms.Padding(3)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(152, 13)
        Me.Label33.TabIndex = 1
        Me.Label33.Text = "Step 2: Sampling Start Point"
        '
        'ClassPictureBoxQuality_CalibStep2
        '
        Me.ClassPictureBoxQuality_CalibStep2.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClassPictureBoxQuality_CalibStep2.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_32x32_32
        Me.ClassPictureBoxQuality_CalibStep2.Location = New System.Drawing.Point(0, 0)
        Me.ClassPictureBoxQuality_CalibStep2.m_HighQuality = True
        Me.ClassPictureBoxQuality_CalibStep2.Name = "ClassPictureBoxQuality_CalibStep2"
        Me.ClassPictureBoxQuality_CalibStep2.Size = New System.Drawing.Size(53, 54)
        Me.ClassPictureBoxQuality_CalibStep2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ClassPictureBoxQuality_CalibStep2.TabIndex = 0
        Me.ClassPictureBoxQuality_CalibStep2.TabStop = False
        '
        'ProgressBar_PlayCalibStep2
        '
        Me.ProgressBar_PlayCalibStep2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar_PlayCalibStep2.Location = New System.Drawing.Point(0, 54)
        Me.ProgressBar_PlayCalibStep2.Name = "ProgressBar_PlayCalibStep2"
        Me.ProgressBar_PlayCalibStep2.Size = New System.Drawing.Size(766, 10)
        Me.ProgressBar_PlayCalibStep2.TabIndex = 4
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.LightGray
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 106)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(766, 1)
        Me.Panel9.TabIndex = 2
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label31)
        Me.Panel7.Controls.Add(Me.Label30)
        Me.Panel7.Controls.Add(Me.ClassPictureBoxQuality_CalibStep1)
        Me.Panel7.Controls.Add(Me.ProgressBar_PlayCalibStep1)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 42)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(766, 64)
        Me.Panel7.TabIndex = 1
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(59, 22)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(553, 26)
        Me.Label31.TabIndex = 2
        Me.Label31.Text = "Hold the controller bulb in front of your head-mounted display. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Keep the contro" &
    "ller bulb consistently in front of your head-mounted display while performing th" &
    "ese steps!"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(59, 6)
        Me.Label30.Margin = New System.Windows.Forms.Padding(3)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(106, 13)
        Me.Label30.TabIndex = 1
        Me.Label30.Text = "Step 1: Preparation"
        '
        'ClassPictureBoxQuality_CalibStep1
        '
        Me.ClassPictureBoxQuality_CalibStep1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ClassPictureBoxQuality_CalibStep1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.netshell_1607_32x32_32
        Me.ClassPictureBoxQuality_CalibStep1.Location = New System.Drawing.Point(0, 0)
        Me.ClassPictureBoxQuality_CalibStep1.m_HighQuality = True
        Me.ClassPictureBoxQuality_CalibStep1.Name = "ClassPictureBoxQuality_CalibStep1"
        Me.ClassPictureBoxQuality_CalibStep1.Size = New System.Drawing.Size(53, 54)
        Me.ClassPictureBoxQuality_CalibStep1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ClassPictureBoxQuality_CalibStep1.TabIndex = 0
        Me.ClassPictureBoxQuality_CalibStep1.TabStop = False
        '
        'ProgressBar_PlayCalibStep1
        '
        Me.ProgressBar_PlayCalibStep1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar_PlayCalibStep1.Location = New System.Drawing.Point(0, 54)
        Me.ProgressBar_PlayCalibStep1.Name = "ProgressBar_PlayCalibStep1"
        Me.ProgressBar_PlayCalibStep1.Size = New System.Drawing.Size(766, 10)
        Me.ProgressBar_PlayCalibStep1.TabIndex = 3
        '
        'Panel5
        '
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Label_PlayCalibTitle)
        Me.Panel5.Controls.Add(Me.Panel_PlayCalibStatus)
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(766, 42)
        Me.Panel5.TabIndex = 0
        '
        'Label_PlayCalibTitle
        '
        Me.Label_PlayCalibTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PlayCalibTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_PlayCalibTitle.ForeColor = System.Drawing.Color.Navy
        Me.Label_PlayCalibTitle.Location = New System.Drawing.Point(19, 0)
        Me.Label_PlayCalibTitle.Name = "Label_PlayCalibTitle"
        Me.Label_PlayCalibTitle.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label_PlayCalibTitle.Size = New System.Drawing.Size(747, 41)
        Me.Label_PlayCalibTitle.TabIndex = 1
        Me.Label_PlayCalibTitle.Text = "Playspace Calibration Steps"
        Me.Label_PlayCalibTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel_PlayCalibStatus
        '
        Me.Panel_PlayCalibStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel_PlayCalibStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel_PlayCalibStatus.Location = New System.Drawing.Point(0, 0)
        Me.Panel_PlayCalibStatus.Name = "Panel_PlayCalibStatus"
        Me.Panel_PlayCalibStatus.Size = New System.Drawing.Size(19, 41)
        Me.Panel_PlayCalibStatus.TabIndex = 2
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Gray
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 41)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(766, 1)
        Me.Panel6.TabIndex = 0
        '
        'ComboBox_PlayCalibControllerID
        '
        Me.ComboBox_PlayCalibControllerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_PlayCalibControllerID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBox_PlayCalibControllerID.FormattingEnabled = True
        Me.ComboBox_PlayCalibControllerID.Location = New System.Drawing.Point(168, 104)
        Me.ComboBox_PlayCalibControllerID.Margin = New System.Windows.Forms.Padding(3, 3, 48, 3)
        Me.ComboBox_PlayCalibControllerID.Name = "ComboBox_PlayCalibControllerID"
        Me.ComboBox_PlayCalibControllerID.Size = New System.Drawing.Size(132, 21)
        Me.ComboBox_PlayCalibControllerID.TabIndex = 78
        Me.ToolTip_Info.SetToolTip(Me.ComboBox_PlayCalibControllerID, "The PSMoveServiceEx controller id you want to use for playspace calibration.")
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(16, 107)
        Me.Label28.Margin = New System.Windows.Forms.Padding(16, 16, 3, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(76, 13)
        Me.Label28.TabIndex = 77
        Me.Label28.Text = "Controller ID:"
        '
        'LinkLabel_PlayCalibShowSettings
        '
        Me.LinkLabel_PlayCalibShowSettings.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_PlayCalibShowSettings.AutoSize = True
        Me.LinkLabel_PlayCalibShowSettings.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_PlayCalibShowSettings.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_PlayCalibShowSettings.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_PlayCalibShowSettings.Location = New System.Drawing.Point(236, 70)
        Me.LinkLabel_PlayCalibShowSettings.Name = "LinkLabel_PlayCalibShowSettings"
        Me.LinkLabel_PlayCalibShowSettings.Size = New System.Drawing.Size(80, 13)
        Me.LinkLabel_PlayCalibShowSettings.TabIndex = 72
        Me.LinkLabel_PlayCalibShowSettings.TabStop = True
        Me.LinkLabel_PlayCalibShowSettings.Text = "Show settings"
        Me.LinkLabel_PlayCalibShowSettings.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.Label27.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label27.Location = New System.Drawing.Point(13, 162)
        Me.Label27.Margin = New System.Windows.Forms.Padding(48, 3, 3, 3)
        Me.Label27.Name = "Label27"
        Me.Label27.Padding = New System.Windows.Forms.Padding(3)
        Me.Label27.Size = New System.Drawing.Size(646, 32)
        Me.Label27.TabIndex = 76
        Me.Label27.Text = "        PSMove controllers do not need manual calibration; use the buttons SELECT" &
    "+START to recenter the playspace instead. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "        See settings for more detail" &
    "s."
        '
        'Button_PlaySpaceManualCalib
        '
        Me.Button_PlaySpaceManualCalib.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.Button_PlaySpaceManualCalib.Location = New System.Drawing.Point(16, 65)
        Me.Button_PlaySpaceManualCalib.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.Button_PlaySpaceManualCalib.Name = "Button_PlaySpaceManualCalib"
        Me.Button_PlaySpaceManualCalib.Size = New System.Drawing.Size(214, 23)
        Me.Button_PlaySpaceManualCalib.TabIndex = 75
        Me.Button_PlaySpaceManualCalib.Text = "Start Playspace Calibration"
        Me.Button_PlaySpaceManualCalib.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_PlaySpaceManualCalib.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_PlaySpaceManualCalib.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label24.Location = New System.Drawing.Point(38, 16)
        Me.Label24.Margin = New System.Windows.Forms.Padding(3, 16, 16, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(746, 33)
        Me.Label24.TabIndex = 74
        Me.Label24.Text = resources.GetString("Label24.Text")
        '
        'ClassPictureBoxQuality3
        '
        Me.ClassPictureBoxQuality3.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.user32_104_16x16_32
        Me.ClassPictureBoxQuality3.Location = New System.Drawing.Point(16, 16)
        Me.ClassPictureBoxQuality3.m_HighQuality = False
        Me.ClassPictureBoxQuality3.Margin = New System.Windows.Forms.Padding(16, 16, 3, 3)
        Me.ClassPictureBoxQuality3.Name = "ClassPictureBoxQuality3"
        Me.ClassPictureBoxQuality3.Size = New System.Drawing.Size(16, 16)
        Me.ClassPictureBoxQuality3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality3.TabIndex = 73
        Me.ClassPictureBoxQuality3.TabStop = False
        '
        'ToolTip_Info
        '
        Me.ToolTip_Info.AutomaticDelay = 100
        Me.ToolTip_Info.AutoPopDelay = 30000
        Me.ToolTip_Info.InitialDelay = 100
        Me.ToolTip_Info.ReshowDelay = 20
        Me.ToolTip_Info.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip_Info.ToolTipTitle = "Information"
        '
        'ToolTip_Default
        '
        Me.ToolTip_Default.AutomaticDelay = 100
        Me.ToolTip_Default.AutoPopDelay = 30000
        Me.ToolTip_Default.InitialDelay = 100
        Me.ToolTip_Default.ReshowDelay = 20
        '
        'UCVmtPlayspaceCalib
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.NumericUpDown_PlayCalibPrepTime)
        Me.Controls.Add(Me.Label41)
        Me.Controls.Add(Me.Panel_PlayCalibSteps)
        Me.Controls.Add(Me.ComboBox_PlayCalibControllerID)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.LinkLabel_PlayCalibShowSettings)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Button_PlaySpaceManualCalib)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.ClassPictureBoxQuality3)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCVmtPlayspaceCalib"
        Me.Size = New System.Drawing.Size(800, 600)
        CType(Me.NumericUpDown_PlayCalibPrepTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NumericUpDown_PlayCalibPrepTime.ResumeLayout(False)
        Me.NumericUpDown_PlayCalibPrepTime.PerformLayout()
        Me.Panel_PlayCalibSteps.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.ClassPictureBoxQuality_CalibStep1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        CType(Me.ClassPictureBoxQuality3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents NumericUpDown_PlayCalibPrepTime As NumericUpDown
    Friend WithEvents Label41 As Label
    Friend WithEvents Panel_PlayCalibSteps As Panel
    Friend WithEvents Panel16 As Panel
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Label38 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents ClassPictureBoxQuality_CalibStep5 As ClassPictureBoxQuality
    Friend WithEvents ProgressBar_PlayCalibStep5 As ProgressBar
    Friend WithEvents Panel14 As Panel
    Friend WithEvents Label36 As Label
    Friend WithEvents Label37 As Label
    Friend WithEvents ClassPictureBoxQuality_CalibStep4 As ClassPictureBoxQuality
    Friend WithEvents ProgressBar_PlayCalibStep4 As ProgressBar
    Friend WithEvents Panel17 As Panel
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Label34 As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents ClassPictureBoxQuality_CalibStep3 As ClassPictureBoxQuality
    Friend WithEvents ProgressBar_PlayCalibStep3 As ProgressBar
    Friend WithEvents Panel18 As Panel
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Label35 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents ClassPictureBoxQuality_CalibStep2 As ClassPictureBoxQuality
    Friend WithEvents ProgressBar_PlayCalibStep2 As ProgressBar
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label31 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents ClassPictureBoxQuality_CalibStep1 As ClassPictureBoxQuality
    Friend WithEvents ProgressBar_PlayCalibStep1 As ProgressBar
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label_PlayCalibTitle As Label
    Friend WithEvents Panel_PlayCalibStatus As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents ComboBox_PlayCalibControllerID As ComboBox
    Friend WithEvents Label28 As Label
    Friend WithEvents LinkLabel_PlayCalibShowSettings As LinkLabel
    Friend WithEvents Label27 As Label
    Friend WithEvents Button_PlaySpaceManualCalib As Button
    Friend WithEvents Label24 As Label
    Friend WithEvents ClassPictureBoxQuality3 As ClassPictureBoxQuality
    Friend WithEvents UcNumericUpDownBig1 As UCNumericUpDownBig
    Friend WithEvents ToolTip_Info As ToolTip
    Friend WithEvents ToolTip_Default As ToolTip
End Class
