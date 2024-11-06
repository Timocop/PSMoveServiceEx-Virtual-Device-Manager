<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCStartPage
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCStartPage))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LinkLabel_ServiceLog = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ServiceFactory = New System.Windows.Forms.LinkLabel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LinkLabel_ServiceRunCmd = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ServicePath = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LinkLabel_ServiceRun = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ServiceRestart = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ServiceStop = New System.Windows.Forms.LinkLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LinkLabel_ConfigToolRunCmd = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_ConfigToolClose = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LinkLabel_ConfigToolRun = New System.Windows.Forms.LinkLabel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.LinkLabel_ManageConnectedDevices = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_UninstallPS4CamDrivers = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_InstallPS4CamDrivers = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_UninstallPSEyeDrivers = New System.Windows.Forms.LinkLabel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LinkLabel_InstallPSEyeDrivers = New System.Windows.Forms.LinkLabel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label_PsmsxStatus = New System.Windows.Forms.Label()
        Me.Panel_PsmsxStatus = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.ToolTip_Service = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Button_PsmsUpdateIgnore = New System.Windows.Forms.Button()
        Me.Button_PsmsxUpdateDownload = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel_PsmsxUpdate = New System.Windows.Forms.Panel()
        Me.Panel_VdmUpdate = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Button_VdmUpdateIgnore = New System.Windows.Forms.Button()
        Me.Button_VdmUpdateDownload = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.Timer_RestartPsms = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_PsmsxInstall = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Button_PsmsInstallIgnore = New System.Windows.Forms.Button()
        Me.Button_PsmsInstallBrowse = New System.Windows.Forms.Button()
        Me.Button_PsmsxInstallDownload = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel24 = New System.Windows.Forms.Panel()
        Me.Chart_ServicePerformance = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Panel25 = New System.Windows.Forms.Panel()
        Me.Button_ChartSettings = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel26 = New System.Windows.Forms.Panel()
        Me.ContextMenuStrip_Chart = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_ChartEnabled = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_ChartClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripComboBox_ChartSamples = New System.Windows.Forms.ToolStripComboBox()
        Me.ListView_ServiceDevices = New PSMSVirtualDeviceManager.ClassListViewEx()
        Me.ColumnHeader_Type = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Color = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Serial = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Pos = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Orientation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Battery = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_FPS = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ClassPictureBoxQuality4 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality2 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality3 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality6 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality5 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBoxQuality7 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.PictureBox1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel_PsmsxUpdate.SuspendLayout()
        Me.Panel_VdmUpdate.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel18.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.Panel_PsmsxInstall.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel24.SuspendLayout()
        CType(Me.Chart_ServicePerformance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel25.SuspendLayout()
        Me.ContextMenuStrip_Chart.SuspendLayout()
        CType(Me.ClassPictureBoxQuality4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBoxQuality7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Controls.Add(Me.Panel12)
        Me.Panel1.Location = New System.Drawing.Point(16, 19)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(861, 316)
        Me.Panel1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel7, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel4, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel5, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 42)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.52941!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.47059!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(859, 272)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label7)
        Me.Panel7.Controls.Add(Me.ClassPictureBoxQuality4)
        Me.Panel7.Controls.Add(Me.LinkLabel_ServiceLog)
        Me.Panel7.Controls.Add(Me.LinkLabel_ServiceFactory)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(429, 131)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(430, 141)
        Me.Panel7.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(102, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(199, 21)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Help and Throubleshooting"
        '
        'LinkLabel_ServiceLog
        '
        Me.LinkLabel_ServiceLog.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServiceLog.AutoSize = True
        Me.LinkLabel_ServiceLog.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceLog.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServiceLog.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceLog.Location = New System.Drawing.Point(103, 40)
        Me.LinkLabel_ServiceLog.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ServiceLog.Name = "LinkLabel_ServiceLog"
        Me.LinkLabel_ServiceLog.Size = New System.Drawing.Size(117, 13)
        Me.LinkLabel_ServiceLog.TabIndex = 31
        Me.LinkLabel_ServiceLog.TabStop = True
        Me.LinkLabel_ServiceLog.Text = "Logs and Diagnostics"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ServiceLog, "Tooltip")
        Me.LinkLabel_ServiceLog.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_ServiceFactory
        '
        Me.LinkLabel_ServiceFactory.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServiceFactory.AutoSize = True
        Me.LinkLabel_ServiceFactory.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceFactory.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServiceFactory.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceFactory.Location = New System.Drawing.Point(103, 62)
        Me.LinkLabel_ServiceFactory.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
        Me.LinkLabel_ServiceFactory.Name = "LinkLabel_ServiceFactory"
        Me.LinkLabel_ServiceFactory.Size = New System.Drawing.Size(153, 13)
        Me.LinkLabel_ServiceFactory.TabIndex = 27
        Me.LinkLabel_ServiceFactory.TabStop = True
        Me.LinkLabel_ServiceFactory.Text = "Factory Reset PSMoveService"
        Me.LinkLabel_ServiceFactory.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.LinkLabel_ServiceRunCmd)
        Me.Panel4.Controls.Add(Me.LinkLabel_ServicePath)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.ClassPictureBoxQuality1)
        Me.Panel4.Controls.Add(Me.LinkLabel_ServiceRun)
        Me.Panel4.Controls.Add(Me.LinkLabel_ServiceRestart)
        Me.Panel4.Controls.Add(Me.LinkLabel_ServiceStop)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(429, 131)
        Me.Panel4.TabIndex = 1
        '
        'LinkLabel_ServiceRunCmd
        '
        Me.LinkLabel_ServiceRunCmd.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServiceRunCmd.AutoSize = True
        Me.LinkLabel_ServiceRunCmd.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRunCmd.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServiceRunCmd.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRunCmd.Location = New System.Drawing.Point(103, 59)
        Me.LinkLabel_ServiceRunCmd.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ServiceRunCmd.Name = "LinkLabel_ServiceRunCmd"
        Me.LinkLabel_ServiceRunCmd.Size = New System.Drawing.Size(80, 13)
        Me.LinkLabel_ServiceRunCmd.TabIndex = 20
        Me.LinkLabel_ServiceRunCmd.TabStop = True
        Me.LinkLabel_ServiceRunCmd.Text = "Debug Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ServiceRunCmd, "Tooltip")
        Me.LinkLabel_ServiceRunCmd.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_ServicePath
        '
        Me.LinkLabel_ServicePath.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServicePath.AutoSize = True
        Me.LinkLabel_ServicePath.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServicePath.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServicePath.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServicePath.Location = New System.Drawing.Point(103, 113)
        Me.LinkLabel_ServicePath.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
        Me.LinkLabel_ServicePath.Name = "LinkLabel_ServicePath"
        Me.LinkLabel_ServicePath.Size = New System.Drawing.Size(96, 13)
        Me.LinkLabel_ServicePath.TabIndex = 19
        Me.LinkLabel_ServicePath.TabStop = True
        Me.LinkLabel_ServicePath.Text = "Set Service Path..."
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ServicePath, "Tooltip")
        Me.LinkLabel_ServicePath.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(102, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 21)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Service Control"
        '
        'LinkLabel_ServiceRun
        '
        Me.LinkLabel_ServiceRun.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServiceRun.AutoSize = True
        Me.LinkLabel_ServiceRun.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRun.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServiceRun.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRun.Location = New System.Drawing.Point(103, 43)
        Me.LinkLabel_ServiceRun.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ServiceRun.Name = "LinkLabel_ServiceRun"
        Me.LinkLabel_ServiceRun.Size = New System.Drawing.Size(66, 13)
        Me.LinkLabel_ServiceRun.TabIndex = 17
        Me.LinkLabel_ServiceRun.TabStop = True
        Me.LinkLabel_ServiceRun.Text = "Run Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ServiceRun, "Tooltip")
        Me.LinkLabel_ServiceRun.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_ServiceRestart
        '
        Me.LinkLabel_ServiceRestart.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServiceRestart.AutoSize = True
        Me.LinkLabel_ServiceRestart.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRestart.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServiceRestart.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceRestart.Location = New System.Drawing.Point(103, 75)
        Me.LinkLabel_ServiceRestart.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ServiceRestart.Name = "LinkLabel_ServiceRestart"
        Me.LinkLabel_ServiceRestart.Size = New System.Drawing.Size(81, 13)
        Me.LinkLabel_ServiceRestart.TabIndex = 11
        Me.LinkLabel_ServiceRestart.TabStop = True
        Me.LinkLabel_ServiceRestart.Text = "Restart Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ServiceRestart, "Tooltip")
        Me.LinkLabel_ServiceRestart.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_ServiceStop
        '
        Me.LinkLabel_ServiceStop.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ServiceStop.AutoSize = True
        Me.LinkLabel_ServiceStop.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceStop.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ServiceStop.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ServiceStop.Location = New System.Drawing.Point(103, 91)
        Me.LinkLabel_ServiceStop.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ServiceStop.Name = "LinkLabel_ServiceStop"
        Me.LinkLabel_ServiceStop.Size = New System.Drawing.Size(69, 13)
        Me.LinkLabel_ServiceStop.TabIndex = 12
        Me.LinkLabel_ServiceStop.TabStop = True
        Me.LinkLabel_ServiceStop.Text = "Stop Service"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ServiceStop, "Tooltip")
        Me.LinkLabel_ServiceStop.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.LinkLabel_ConfigToolRunCmd)
        Me.Panel2.Controls.Add(Me.LinkLabel_ConfigToolClose)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.ClassPictureBoxQuality2)
        Me.Panel2.Controls.Add(Me.LinkLabel_ConfigToolRun)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(429, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(430, 131)
        Me.Panel2.TabIndex = 0
        '
        'LinkLabel_ConfigToolRunCmd
        '
        Me.LinkLabel_ConfigToolRunCmd.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ConfigToolRunCmd.AutoSize = True
        Me.LinkLabel_ConfigToolRunCmd.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolRunCmd.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ConfigToolRunCmd.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolRunCmd.Location = New System.Drawing.Point(103, 59)
        Me.LinkLabel_ConfigToolRunCmd.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ConfigToolRunCmd.Name = "LinkLabel_ConfigToolRunCmd"
        Me.LinkLabel_ConfigToolRunCmd.Size = New System.Drawing.Size(105, 13)
        Me.LinkLabel_ConfigToolRunCmd.TabIndex = 25
        Me.LinkLabel_ConfigToolRunCmd.TabStop = True
        Me.LinkLabel_ConfigToolRunCmd.Text = "Debug Config Tool"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ConfigToolRunCmd, "Tooltip")
        Me.LinkLabel_ConfigToolRunCmd.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_ConfigToolClose
        '
        Me.LinkLabel_ConfigToolClose.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ConfigToolClose.AutoSize = True
        Me.LinkLabel_ConfigToolClose.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolClose.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ConfigToolClose.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolClose.Location = New System.Drawing.Point(103, 75)
        Me.LinkLabel_ConfigToolClose.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ConfigToolClose.Name = "LinkLabel_ConfigToolClose"
        Me.LinkLabel_ConfigToolClose.Size = New System.Drawing.Size(98, 13)
        Me.LinkLabel_ConfigToolClose.TabIndex = 24
        Me.LinkLabel_ConfigToolClose.TabStop = True
        Me.LinkLabel_ConfigToolClose.Text = "Close Config Tool"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ConfigToolClose, "Tooltip")
        Me.LinkLabel_ConfigToolClose.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(102, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 21)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Configuration"
        '
        'LinkLabel_ConfigToolRun
        '
        Me.LinkLabel_ConfigToolRun.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ConfigToolRun.AutoSize = True
        Me.LinkLabel_ConfigToolRun.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolRun.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ConfigToolRun.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ConfigToolRun.Location = New System.Drawing.Point(103, 43)
        Me.LinkLabel_ConfigToolRun.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_ConfigToolRun.Name = "LinkLabel_ConfigToolRun"
        Me.LinkLabel_ConfigToolRun.Size = New System.Drawing.Size(91, 13)
        Me.LinkLabel_ConfigToolRun.TabIndex = 22
        Me.LinkLabel_ConfigToolRun.TabStop = True
        Me.LinkLabel_ConfigToolRun.Text = "Run Config Tool"
        Me.ToolTip_Service.SetToolTip(Me.LinkLabel_ConfigToolRun, "Tooltip")
        Me.LinkLabel_ConfigToolRun.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.LinkLabel_ManageConnectedDevices)
        Me.Panel5.Controls.Add(Me.LinkLabel_UninstallPS4CamDrivers)
        Me.Panel5.Controls.Add(Me.LinkLabel_InstallPS4CamDrivers)
        Me.Panel5.Controls.Add(Me.LinkLabel_UninstallPSEyeDrivers)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.ClassPictureBoxQuality3)
        Me.Panel5.Controls.Add(Me.LinkLabel_InstallPSEyeDrivers)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 131)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(429, 141)
        Me.Panel5.TabIndex = 6
        '
        'LinkLabel_ManageConnectedDevices
        '
        Me.LinkLabel_ManageConnectedDevices.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_ManageConnectedDevices.AutoSize = True
        Me.LinkLabel_ManageConnectedDevices.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ManageConnectedDevices.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_ManageConnectedDevices.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_ManageConnectedDevices.Location = New System.Drawing.Point(103, 116)
        Me.LinkLabel_ManageConnectedDevices.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
        Me.LinkLabel_ManageConnectedDevices.Name = "LinkLabel_ManageConnectedDevices"
        Me.LinkLabel_ManageConnectedDevices.Size = New System.Drawing.Size(149, 13)
        Me.LinkLabel_ManageConnectedDevices.TabIndex = 34
        Me.LinkLabel_ManageConnectedDevices.TabStop = True
        Me.LinkLabel_ManageConnectedDevices.Text = "Manage Connected Devices"
        Me.LinkLabel_ManageConnectedDevices.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_UninstallPS4CamDrivers
        '
        Me.LinkLabel_UninstallPS4CamDrivers.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_UninstallPS4CamDrivers.AutoSize = True
        Me.LinkLabel_UninstallPS4CamDrivers.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_UninstallPS4CamDrivers.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_UninstallPS4CamDrivers.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_UninstallPS4CamDrivers.Location = New System.Drawing.Point(103, 94)
        Me.LinkLabel_UninstallPS4CamDrivers.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_UninstallPS4CamDrivers.Name = "LinkLabel_UninstallPS4CamDrivers"
        Me.LinkLabel_UninstallPS4CamDrivers.Size = New System.Drawing.Size(228, 13)
        Me.LinkLabel_UninstallPS4CamDrivers.TabIndex = 33
        Me.LinkLabel_UninstallPS4CamDrivers.TabStop = True
        Me.LinkLabel_UninstallPS4CamDrivers.Text = "Uninstall PlayStation Stereo Camera Drivers"
        Me.LinkLabel_UninstallPS4CamDrivers.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_InstallPS4CamDrivers
        '
        Me.LinkLabel_InstallPS4CamDrivers.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_InstallPS4CamDrivers.AutoSize = True
        Me.LinkLabel_InstallPS4CamDrivers.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_InstallPS4CamDrivers.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_InstallPS4CamDrivers.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_InstallPS4CamDrivers.Location = New System.Drawing.Point(103, 78)
        Me.LinkLabel_InstallPS4CamDrivers.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
        Me.LinkLabel_InstallPS4CamDrivers.Name = "LinkLabel_InstallPS4CamDrivers"
        Me.LinkLabel_InstallPS4CamDrivers.Size = New System.Drawing.Size(213, 13)
        Me.LinkLabel_InstallPS4CamDrivers.TabIndex = 32
        Me.LinkLabel_InstallPS4CamDrivers.TabStop = True
        Me.LinkLabel_InstallPS4CamDrivers.Text = "Install PlayStation Stereo Camera Drivers"
        Me.LinkLabel_InstallPS4CamDrivers.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'LinkLabel_UninstallPSEyeDrivers
        '
        Me.LinkLabel_UninstallPSEyeDrivers.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_UninstallPSEyeDrivers.AutoSize = True
        Me.LinkLabel_UninstallPSEyeDrivers.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_UninstallPSEyeDrivers.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_UninstallPSEyeDrivers.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_UninstallPSEyeDrivers.Location = New System.Drawing.Point(103, 56)
        Me.LinkLabel_UninstallPSEyeDrivers.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_UninstallPSEyeDrivers.Name = "LinkLabel_UninstallPSEyeDrivers"
        Me.LinkLabel_UninstallPSEyeDrivers.Size = New System.Drawing.Size(171, 13)
        Me.LinkLabel_UninstallPSEyeDrivers.TabIndex = 30
        Me.LinkLabel_UninstallPSEyeDrivers.TabStop = True
        Me.LinkLabel_UninstallPSEyeDrivers.Text = "Uninstall PlayStation Eye Drivers"
        Me.LinkLabel_UninstallPSEyeDrivers.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(102, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 21)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Management"
        '
        'LinkLabel_InstallPSEyeDrivers
        '
        Me.LinkLabel_InstallPSEyeDrivers.ActiveLinkColor = System.Drawing.Color.CornflowerBlue
        Me.LinkLabel_InstallPSEyeDrivers.AutoSize = True
        Me.LinkLabel_InstallPSEyeDrivers.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_InstallPSEyeDrivers.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel_InstallPSEyeDrivers.LinkColor = System.Drawing.Color.RoyalBlue
        Me.LinkLabel_InstallPSEyeDrivers.Location = New System.Drawing.Point(103, 40)
        Me.LinkLabel_InstallPSEyeDrivers.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LinkLabel_InstallPSEyeDrivers.Name = "LinkLabel_InstallPSEyeDrivers"
        Me.LinkLabel_InstallPSEyeDrivers.Size = New System.Drawing.Size(156, 13)
        Me.LinkLabel_InstallPSEyeDrivers.TabIndex = 25
        Me.LinkLabel_InstallPSEyeDrivers.TabStop = True
        Me.LinkLabel_InstallPSEyeDrivers.Text = "Install PlayStation Eye Drivers"
        Me.LinkLabel_InstallPSEyeDrivers.VisitedLinkColor = System.Drawing.Color.RoyalBlue
        '
        'Panel12
        '
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Controls.Add(Me.Label_PsmsxStatus)
        Me.Panel12.Controls.Add(Me.Panel_PsmsxStatus)
        Me.Panel12.Controls.Add(Me.Panel3)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(859, 42)
        Me.Panel12.TabIndex = 0
        '
        'Label_PsmsxStatus
        '
        Me.Label_PsmsxStatus.BackColor = System.Drawing.Color.White
        Me.Label_PsmsxStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PsmsxStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_PsmsxStatus.ForeColor = System.Drawing.Color.Navy
        Me.Label_PsmsxStatus.Location = New System.Drawing.Point(19, 0)
        Me.Label_PsmsxStatus.Name = "Label_PsmsxStatus"
        Me.Label_PsmsxStatus.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label_PsmsxStatus.Size = New System.Drawing.Size(840, 41)
        Me.Label_PsmsxStatus.TabIndex = 1
        Me.Label_PsmsxStatus.Text = "PSMoveServiceEx Disconnected."
        Me.Label_PsmsxStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel_PsmsxStatus
        '
        Me.Panel_PsmsxStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel_PsmsxStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel_PsmsxStatus.Location = New System.Drawing.Point(0, 0)
        Me.Panel_PsmsxStatus.Name = "Panel_PsmsxStatus"
        Me.Panel_PsmsxStatus.Size = New System.Drawing.Size(19, 41)
        Me.Panel_PsmsxStatus.TabIndex = 2
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Gray
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 41)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(859, 1)
        Me.Panel3.TabIndex = 0
        '
        'ToolTip_Service
        '
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.PictureBox1)
        Me.Panel6.Controls.Add(Me.Label4)
        Me.Panel6.Controls.Add(Me.Label3)
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(893, 64)
        Me.Panel6.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(66, 30)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(824, 30)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Manage service and configurations here."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(66, 3)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(172, 21)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Service Management"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(0, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(893, 1)
        Me.Label5.TabIndex = 0
        '
        'Panel8
        '
        Me.Panel8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.ListView_ServiceDevices)
        Me.Panel8.Controls.Add(Me.Panel14)
        Me.Panel8.Location = New System.Drawing.Point(16, 16)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(861, 228)
        Me.Panel8.TabIndex = 2
        '
        'Panel14
        '
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.Label12)
        Me.Panel14.Controls.Add(Me.Panel16)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(0, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(859, 42)
        Me.Panel14.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Navy
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label12.Size = New System.Drawing.Size(859, 41)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Available Service Devices"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.Gray
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel16.Location = New System.Drawing.Point(0, 41)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(859, 1)
        Me.Panel16.TabIndex = 0
        '
        'Panel9
        '
        Me.Panel9.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.Button_PsmsUpdateIgnore)
        Me.Panel9.Controls.Add(Me.Button_PsmsxUpdateDownload)
        Me.Panel9.Controls.Add(Me.Label10)
        Me.Panel9.Controls.Add(Me.ClassPictureBoxQuality5)
        Me.Panel9.Controls.Add(Me.Panel10)
        Me.Panel9.Location = New System.Drawing.Point(16, 19)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(861, 140)
        Me.Panel9.TabIndex = 3
        '
        'Button_PsmsUpdateIgnore
        '
        Me.Button_PsmsUpdateIgnore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_PsmsUpdateIgnore.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_PsmsUpdateIgnore.Location = New System.Drawing.Point(215, 99)
        Me.Button_PsmsUpdateIgnore.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.Button_PsmsUpdateIgnore.Name = "Button_PsmsUpdateIgnore"
        Me.Button_PsmsUpdateIgnore.Size = New System.Drawing.Size(95, 23)
        Me.Button_PsmsUpdateIgnore.TabIndex = 4
        Me.Button_PsmsUpdateIgnore.Text = "Ignore"
        Me.Button_PsmsUpdateIgnore.UseVisualStyleBackColor = True
        '
        'Button_PsmsxUpdateDownload
        '
        Me.Button_PsmsxUpdateDownload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_PsmsxUpdateDownload.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_PsmsxUpdateDownload.Location = New System.Drawing.Point(70, 99)
        Me.Button_PsmsxUpdateDownload.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.Button_PsmsxUpdateDownload.Name = "Button_PsmsxUpdateDownload"
        Me.Button_PsmsxUpdateDownload.Size = New System.Drawing.Size(139, 23)
        Me.Button_PsmsxUpdateDownload.TabIndex = 3
        Me.Button_PsmsxUpdateDownload.Text = "Download and Install"
        Me.Button_PsmsxUpdateDownload.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(67, 49)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(511, 39)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = resources.GetString("Label10.Text")
        '
        'Panel10
        '
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label8)
        Me.Panel10.Controls.Add(Me.Panel11)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(859, 42)
        Me.Panel10.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Lavender
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Navy
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label8.Size = New System.Drawing.Size(859, 41)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "PSMoveServiceEx Software Update Available"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.Gray
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel11.Location = New System.Drawing.Point(0, 41)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(859, 1)
        Me.Panel11.TabIndex = 0
        '
        'Panel_PsmsxUpdate
        '
        Me.Panel_PsmsxUpdate.Controls.Add(Me.Panel9)
        Me.Panel_PsmsxUpdate.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel_PsmsxUpdate.Location = New System.Drawing.Point(0, 239)
        Me.Panel_PsmsxUpdate.Name = "Panel_PsmsxUpdate"
        Me.Panel_PsmsxUpdate.Size = New System.Drawing.Size(893, 175)
        Me.Panel_PsmsxUpdate.TabIndex = 4
        Me.Panel_PsmsxUpdate.Visible = False
        '
        'Panel_VdmUpdate
        '
        Me.Panel_VdmUpdate.Controls.Add(Me.Panel17)
        Me.Panel_VdmUpdate.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel_VdmUpdate.Location = New System.Drawing.Point(0, 414)
        Me.Panel_VdmUpdate.Name = "Panel_VdmUpdate"
        Me.Panel_VdmUpdate.Size = New System.Drawing.Size(893, 175)
        Me.Panel_VdmUpdate.TabIndex = 5
        Me.Panel_VdmUpdate.Visible = False
        '
        'Panel17
        '
        Me.Panel17.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel17.Controls.Add(Me.Button_VdmUpdateIgnore)
        Me.Panel17.Controls.Add(Me.Button_VdmUpdateDownload)
        Me.Panel17.Controls.Add(Me.Label11)
        Me.Panel17.Controls.Add(Me.ClassPictureBoxQuality6)
        Me.Panel17.Controls.Add(Me.Panel18)
        Me.Panel17.Location = New System.Drawing.Point(16, 19)
        Me.Panel17.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(861, 140)
        Me.Panel17.TabIndex = 3
        '
        'Button_VdmUpdateIgnore
        '
        Me.Button_VdmUpdateIgnore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_VdmUpdateIgnore.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_VdmUpdateIgnore.Location = New System.Drawing.Point(215, 99)
        Me.Button_VdmUpdateIgnore.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.Button_VdmUpdateIgnore.Name = "Button_VdmUpdateIgnore"
        Me.Button_VdmUpdateIgnore.Size = New System.Drawing.Size(95, 23)
        Me.Button_VdmUpdateIgnore.TabIndex = 7
        Me.Button_VdmUpdateIgnore.Text = "Ignore"
        Me.Button_VdmUpdateIgnore.UseVisualStyleBackColor = True
        '
        'Button_VdmUpdateDownload
        '
        Me.Button_VdmUpdateDownload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_VdmUpdateDownload.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_VdmUpdateDownload.Location = New System.Drawing.Point(70, 99)
        Me.Button_VdmUpdateDownload.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.Button_VdmUpdateDownload.Name = "Button_VdmUpdateDownload"
        Me.Button_VdmUpdateDownload.Size = New System.Drawing.Size(139, 23)
        Me.Button_VdmUpdateDownload.TabIndex = 6
        Me.Button_VdmUpdateDownload.Text = "Download and Install"
        Me.Button_VdmUpdateDownload.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(67, 49)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(511, 39)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = resources.GetString("Label11.Text")
        '
        'Panel18
        '
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel18.Controls.Add(Me.Label9)
        Me.Panel18.Controls.Add(Me.Panel19)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel18.Location = New System.Drawing.Point(0, 0)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(859, 42)
        Me.Panel18.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Lavender
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Navy
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label9.Size = New System.Drawing.Size(859, 41)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Virtual Device Manager Software Update Available"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel19
        '
        Me.Panel19.BackColor = System.Drawing.Color.Gray
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel19.Location = New System.Drawing.Point(0, 41)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(859, 1)
        Me.Panel19.TabIndex = 0
        '
        'Panel20
        '
        Me.Panel20.Controls.Add(Me.Panel1)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel20.Location = New System.Drawing.Point(0, 589)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(893, 354)
        Me.Panel20.TabIndex = 6
        '
        'Panel21
        '
        Me.Panel21.Controls.Add(Me.Panel8)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel21.Location = New System.Drawing.Point(0, 943)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(893, 260)
        Me.Panel21.TabIndex = 7
        '
        'Timer_RestartPsms
        '
        Me.Timer_RestartPsms.Interval = 1000
        '
        'Panel_PsmsxInstall
        '
        Me.Panel_PsmsxInstall.Controls.Add(Me.Panel15)
        Me.Panel_PsmsxInstall.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel_PsmsxInstall.Location = New System.Drawing.Point(0, 64)
        Me.Panel_PsmsxInstall.Name = "Panel_PsmsxInstall"
        Me.Panel_PsmsxInstall.Size = New System.Drawing.Size(893, 175)
        Me.Panel_PsmsxInstall.TabIndex = 8
        Me.Panel_PsmsxInstall.Visible = False
        '
        'Panel15
        '
        Me.Panel15.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel15.Controls.Add(Me.Button_PsmsInstallIgnore)
        Me.Panel15.Controls.Add(Me.Button_PsmsInstallBrowse)
        Me.Panel15.Controls.Add(Me.Button_PsmsxInstallDownload)
        Me.Panel15.Controls.Add(Me.Label13)
        Me.Panel15.Controls.Add(Me.ClassPictureBoxQuality7)
        Me.Panel15.Controls.Add(Me.Panel22)
        Me.Panel15.Location = New System.Drawing.Point(16, 19)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(861, 140)
        Me.Panel15.TabIndex = 3
        '
        'Button_PsmsInstallIgnore
        '
        Me.Button_PsmsInstallIgnore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_PsmsInstallIgnore.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_PsmsInstallIgnore.Location = New System.Drawing.Point(316, 99)
        Me.Button_PsmsInstallIgnore.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.Button_PsmsInstallIgnore.Name = "Button_PsmsInstallIgnore"
        Me.Button_PsmsInstallIgnore.Size = New System.Drawing.Size(95, 23)
        Me.Button_PsmsInstallIgnore.TabIndex = 5
        Me.Button_PsmsInstallIgnore.Text = "Ignore"
        Me.Button_PsmsInstallIgnore.UseVisualStyleBackColor = True
        '
        'Button_PsmsInstallBrowse
        '
        Me.Button_PsmsInstallBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_PsmsInstallBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_PsmsInstallBrowse.Location = New System.Drawing.Point(215, 99)
        Me.Button_PsmsInstallBrowse.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.Button_PsmsInstallBrowse.Name = "Button_PsmsInstallBrowse"
        Me.Button_PsmsInstallBrowse.Size = New System.Drawing.Size(95, 23)
        Me.Button_PsmsInstallBrowse.TabIndex = 4
        Me.Button_PsmsInstallBrowse.Text = "Browse"
        Me.Button_PsmsInstallBrowse.UseVisualStyleBackColor = True
        '
        'Button_PsmsxInstallDownload
        '
        Me.Button_PsmsxInstallDownload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_PsmsxInstallDownload.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button_PsmsxInstallDownload.Location = New System.Drawing.Point(70, 99)
        Me.Button_PsmsxInstallDownload.Margin = New System.Windows.Forms.Padding(3, 3, 3, 16)
        Me.Button_PsmsxInstallDownload.Name = "Button_PsmsxInstallDownload"
        Me.Button_PsmsxInstallDownload.Size = New System.Drawing.Size(139, 23)
        Me.Button_PsmsxInstallDownload.TabIndex = 3
        Me.Button_PsmsxInstallDownload.Text = "Download and Install"
        Me.Button_PsmsxInstallDownload.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(67, 49)
        Me.Label13.Margin = New System.Windows.Forms.Padding(3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(449, 39)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = resources.GetString("Label13.Text")
        '
        'Panel22
        '
        Me.Panel22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel22.Controls.Add(Me.Label14)
        Me.Panel22.Controls.Add(Me.Panel23)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel22.Location = New System.Drawing.Point(0, 0)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(859, 42)
        Me.Panel22.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Lavender
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Navy
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label14.Size = New System.Drawing.Size(859, 41)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "Download and Install PSMoveServiceEx"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel23
        '
        Me.Panel23.BackColor = System.Drawing.Color.Gray
        Me.Panel23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel23.Location = New System.Drawing.Point(0, 41)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(859, 1)
        Me.Panel23.TabIndex = 0
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.Panel24)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 1203)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(893, 396)
        Me.Panel13.TabIndex = 9
        '
        'Panel24
        '
        Me.Panel24.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel24.Controls.Add(Me.Chart_ServicePerformance)
        Me.Panel24.Controls.Add(Me.Panel25)
        Me.Panel24.Location = New System.Drawing.Point(16, 16)
        Me.Panel24.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(861, 364)
        Me.Panel24.TabIndex = 2
        '
        'Chart_ServicePerformance
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart_ServicePerformance.ChartAreas.Add(ChartArea1)
        Me.Chart_ServicePerformance.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.Chart_ServicePerformance.Legends.Add(Legend1)
        Me.Chart_ServicePerformance.Location = New System.Drawing.Point(0, 42)
        Me.Chart_ServicePerformance.Name = "Chart_ServicePerformance"
        Me.Chart_ServicePerformance.Size = New System.Drawing.Size(859, 320)
        Me.Chart_ServicePerformance.TabIndex = 1
        Me.Chart_ServicePerformance.Text = "Chart1"
        Title1.Alignment = System.Drawing.ContentAlignment.MiddleRight
        Title1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title1.Name = "Title1"
        Title1.Text = "Service device frames per second"
        Me.Chart_ServicePerformance.Titles.Add(Title1)
        '
        'Panel25
        '
        Me.Panel25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel25.Controls.Add(Me.Button_ChartSettings)
        Me.Panel25.Controls.Add(Me.Label15)
        Me.Panel25.Controls.Add(Me.Panel26)
        Me.Panel25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel25.Location = New System.Drawing.Point(0, 0)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(859, 42)
        Me.Panel25.TabIndex = 0
        '
        'Button_ChartSettings
        '
        Me.Button_ChartSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ChartSettings.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5321_16x16_32
        Me.Button_ChartSettings.Location = New System.Drawing.Point(716, 6)
        Me.Button_ChartSettings.Margin = New System.Windows.Forms.Padding(16, 6, 16, 6)
        Me.Button_ChartSettings.Name = "Button_ChartSettings"
        Me.Button_ChartSettings.Size = New System.Drawing.Size(127, 30)
        Me.Button_ChartSettings.TabIndex = 2
        Me.Button_ChartSettings.Text = "Graph Settings..."
        Me.Button_ChartSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_ChartSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button_ChartSettings.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.White
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Navy
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label15.Size = New System.Drawing.Size(859, 41)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Service Performance Graph"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel26
        '
        Me.Panel26.BackColor = System.Drawing.Color.Gray
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel26.Location = New System.Drawing.Point(0, 41)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Size = New System.Drawing.Size(859, 1)
        Me.Panel26.TabIndex = 0
        '
        'ContextMenuStrip_Chart
        '
        Me.ContextMenuStrip_Chart.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_ChartEnabled, Me.ToolStripMenuItem_ChartClear, Me.ToolStripSeparator1, Me.ToolStripMenuItem1, Me.ToolStripComboBox_ChartSamples})
        Me.ContextMenuStrip_Chart.Name = "ContextMenuStrip_Chart"
        Me.ContextMenuStrip_Chart.Size = New System.Drawing.Size(182, 103)
        '
        'ToolStripMenuItem_ChartEnabled
        '
        Me.ToolStripMenuItem_ChartEnabled.CheckOnClick = True
        Me.ToolStripMenuItem_ChartEnabled.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5341_16x16_32
        Me.ToolStripMenuItem_ChartEnabled.Name = "ToolStripMenuItem_ChartEnabled"
        Me.ToolStripMenuItem_ChartEnabled.Size = New System.Drawing.Size(181, 22)
        Me.ToolStripMenuItem_ChartEnabled.Text = "Enabled"
        '
        'ToolStripMenuItem_ChartClear
        '
        Me.ToolStripMenuItem_ChartClear.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5305_16x16_32
        Me.ToolStripMenuItem_ChartClear.Name = "ToolStripMenuItem_ChartClear"
        Me.ToolStripMenuItem_ChartClear.Size = New System.Drawing.Size(181, 22)
        Me.ToolStripMenuItem_ChartClear.Text = "Clear"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(178, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Enabled = False
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(181, 22)
        Me.ToolStripMenuItem1.Text = "Maximum Samples:"
        '
        'ToolStripComboBox_ChartSamples
        '
        Me.ToolStripComboBox_ChartSamples.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ToolStripComboBox_ChartSamples.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ToolStripComboBox_ChartSamples.Name = "ToolStripComboBox_ChartSamples"
        Me.ToolStripComboBox_ChartSamples.Size = New System.Drawing.Size(121, 23)
        '
        'ListView_ServiceDevices
        '
        Me.ListView_ServiceDevices.BackColor = System.Drawing.Color.White
        Me.ListView_ServiceDevices.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView_ServiceDevices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader_Type, Me.ColumnHeader_Color, Me.ColumnHeader_ID, Me.ColumnHeader_Serial, Me.ColumnHeader_Pos, Me.ColumnHeader_Orientation, Me.ColumnHeader_Battery, Me.ColumnHeader_FPS})
        Me.ListView_ServiceDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_ServiceDevices.FullRowSelect = True
        Me.ListView_ServiceDevices.HideSelection = False
        Me.ListView_ServiceDevices.Location = New System.Drawing.Point(0, 42)
        Me.ListView_ServiceDevices.Name = "ListView_ServiceDevices"
        Me.ListView_ServiceDevices.Size = New System.Drawing.Size(859, 184)
        Me.ListView_ServiceDevices.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView_ServiceDevices.TabIndex = 1
        Me.ListView_ServiceDevices.UseCompatibleStateImageBehavior = False
        Me.ListView_ServiceDevices.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader_Type
        '
        Me.ColumnHeader_Type.Text = "Type"
        Me.ColumnHeader_Type.Width = 100
        '
        'ColumnHeader_Color
        '
        Me.ColumnHeader_Color.Text = "Color"
        Me.ColumnHeader_Color.Width = 75
        '
        'ColumnHeader_ID
        '
        Me.ColumnHeader_ID.Text = "ID"
        Me.ColumnHeader_ID.Width = 30
        '
        'ColumnHeader_Serial
        '
        Me.ColumnHeader_Serial.Text = "Serial"
        Me.ColumnHeader_Serial.Width = 200
        '
        'ColumnHeader_Pos
        '
        Me.ColumnHeader_Pos.Text = "Position"
        Me.ColumnHeader_Pos.Width = 125
        '
        'ColumnHeader_Orientation
        '
        Me.ColumnHeader_Orientation.Text = "Orientation"
        Me.ColumnHeader_Orientation.Width = 125
        '
        'ColumnHeader_Battery
        '
        Me.ColumnHeader_Battery.Text = "Battery"
        Me.ColumnHeader_Battery.Width = 50
        '
        'ColumnHeader_FPS
        '
        Me.ColumnHeader_FPS.Text = "I/O FPS"
        Me.ColumnHeader_FPS.Width = 50
        '
        'ClassPictureBoxQuality4
        '
        Me.ClassPictureBoxQuality4.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources._369
        Me.ClassPictureBoxQuality4.Location = New System.Drawing.Point(32, 16)
        Me.ClassPictureBoxQuality4.m_HighQuality = True
        Me.ClassPictureBoxQuality4.Margin = New System.Windows.Forms.Padding(32, 16, 3, 3)
        Me.ClassPictureBoxQuality4.Name = "ClassPictureBoxQuality4"
        Me.ClassPictureBoxQuality4.Size = New System.Drawing.Size(64, 64)
        Me.ClassPictureBoxQuality4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality4.TabIndex = 24
        Me.ClassPictureBoxQuality4.TabStop = False
        '
        'ClassPictureBoxQuality1
        '
        Me.ClassPictureBoxQuality1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.devmgr_201_256x256_32
        Me.ClassPictureBoxQuality1.Location = New System.Drawing.Point(32, 19)
        Me.ClassPictureBoxQuality1.m_HighQuality = True
        Me.ClassPictureBoxQuality1.Margin = New System.Windows.Forms.Padding(32, 16, 3, 3)
        Me.ClassPictureBoxQuality1.Name = "ClassPictureBoxQuality1"
        Me.ClassPictureBoxQuality1.Size = New System.Drawing.Size(64, 64)
        Me.ClassPictureBoxQuality1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality1.TabIndex = 10
        Me.ClassPictureBoxQuality1.TabStop = False
        '
        'ClassPictureBoxQuality2
        '
        Me.ClassPictureBoxQuality2.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.imageres_5364_64x64_32
        Me.ClassPictureBoxQuality2.Location = New System.Drawing.Point(32, 19)
        Me.ClassPictureBoxQuality2.m_HighQuality = True
        Me.ClassPictureBoxQuality2.Margin = New System.Windows.Forms.Padding(32, 16, 3, 3)
        Me.ClassPictureBoxQuality2.Name = "ClassPictureBoxQuality2"
        Me.ClassPictureBoxQuality2.Size = New System.Drawing.Size(64, 64)
        Me.ClassPictureBoxQuality2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality2.TabIndex = 19
        Me.ClassPictureBoxQuality2.TabStop = False
        '
        'ClassPictureBoxQuality3
        '
        Me.ClassPictureBoxQuality3.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources._366
        Me.ClassPictureBoxQuality3.Location = New System.Drawing.Point(32, 16)
        Me.ClassPictureBoxQuality3.m_HighQuality = True
        Me.ClassPictureBoxQuality3.Margin = New System.Windows.Forms.Padding(32, 16, 3, 3)
        Me.ClassPictureBoxQuality3.Name = "ClassPictureBoxQuality3"
        Me.ClassPictureBoxQuality3.Size = New System.Drawing.Size(64, 64)
        Me.ClassPictureBoxQuality3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality3.TabIndex = 24
        Me.ClassPictureBoxQuality3.TabStop = False
        '
        'ClassPictureBoxQuality6
        '
        Me.ClassPictureBoxQuality6.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.connect_10101_256x256_32
        Me.ClassPictureBoxQuality6.Location = New System.Drawing.Point(8, 49)
        Me.ClassPictureBoxQuality6.m_HighQuality = True
        Me.ClassPictureBoxQuality6.Margin = New System.Windows.Forms.Padding(8)
        Me.ClassPictureBoxQuality6.Name = "ClassPictureBoxQuality6"
        Me.ClassPictureBoxQuality6.Size = New System.Drawing.Size(48, 48)
        Me.ClassPictureBoxQuality6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality6.TabIndex = 4
        Me.ClassPictureBoxQuality6.TabStop = False
        '
        'ClassPictureBoxQuality5
        '
        Me.ClassPictureBoxQuality5.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.connect_10101_256x256_32
        Me.ClassPictureBoxQuality5.Location = New System.Drawing.Point(8, 49)
        Me.ClassPictureBoxQuality5.m_HighQuality = True
        Me.ClassPictureBoxQuality5.Margin = New System.Windows.Forms.Padding(8)
        Me.ClassPictureBoxQuality5.Name = "ClassPictureBoxQuality5"
        Me.ClassPictureBoxQuality5.Size = New System.Drawing.Size(48, 48)
        Me.ClassPictureBoxQuality5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality5.TabIndex = 1
        Me.ClassPictureBoxQuality5.TabStop = False
        '
        'ClassPictureBoxQuality7
        '
        Me.ClassPictureBoxQuality7.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.connect_10101_256x256_32
        Me.ClassPictureBoxQuality7.Location = New System.Drawing.Point(8, 49)
        Me.ClassPictureBoxQuality7.m_HighQuality = True
        Me.ClassPictureBoxQuality7.Margin = New System.Windows.Forms.Padding(8)
        Me.ClassPictureBoxQuality7.Name = "ClassPictureBoxQuality7"
        Me.ClassPictureBoxQuality7.Size = New System.Drawing.Size(48, 48)
        Me.ClassPictureBoxQuality7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBoxQuality7.TabIndex = 1
        Me.ClassPictureBoxQuality7.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.mmcshext_128_256x256_32
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.m_HighQuality = True
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(57, 57)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'UCStartPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel13)
        Me.Controls.Add(Me.Panel21)
        Me.Controls.Add(Me.Panel20)
        Me.Controls.Add(Me.Panel_VdmUpdate)
        Me.Controls.Add(Me.Panel_PsmsxUpdate)
        Me.Controls.Add(Me.Panel_PsmsxInstall)
        Me.Controls.Add(Me.Panel6)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCStartPage"
        Me.Size = New System.Drawing.Size(893, 1895)
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel_PsmsxUpdate.ResumeLayout(False)
        Me.Panel_VdmUpdate.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        Me.Panel18.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel21.ResumeLayout(False)
        Me.Panel_PsmsxInstall.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.Panel22.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.Panel24.ResumeLayout(False)
        CType(Me.Chart_ServicePerformance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel25.ResumeLayout(False)
        Me.ContextMenuStrip_Chart.ResumeLayout(False)
        CType(Me.ClassPictureBoxQuality4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBoxQuality7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label_PsmsxStatus As Label
    Friend WithEvents ToolTip_Service As ToolTip
    Friend WithEvents ClassPictureBoxQuality1 As ClassPictureBoxQuality
    Friend WithEvents LinkLabel_ServiceRestart As LinkLabel
    Friend WithEvents LinkLabel_ServiceStop As LinkLabel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents LinkLabel_ServiceRun As LinkLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents ClassPictureBoxQuality2 As ClassPictureBoxQuality
    Friend WithEvents LinkLabel_ConfigToolRun As LinkLabel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents PictureBox1 As ClassPictureBoxQuality
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents ClassPictureBoxQuality3 As ClassPictureBoxQuality
    Friend WithEvents LinkLabel_InstallPSEyeDrivers As LinkLabel
    Friend WithEvents LinkLabel_ServiceFactory As LinkLabel
    Friend WithEvents LinkLabel_ServicePath As LinkLabel
    Friend WithEvents Panel_PsmsxStatus As Panel
    Friend WithEvents LinkLabel_ConfigToolClose As LinkLabel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents ClassPictureBoxQuality4 As ClassPictureBoxQuality
    Friend WithEvents Panel8 As Panel
    Friend WithEvents ListView_ServiceDevices As ClassListViewEx
    Friend WithEvents ColumnHeader_Type As ColumnHeader
    Friend WithEvents ColumnHeader_Serial As ColumnHeader
    Friend WithEvents ColumnHeader_Pos As ColumnHeader
    Friend WithEvents ColumnHeader_Orientation As ColumnHeader
    Friend WithEvents ColumnHeader_Battery As ColumnHeader
    Friend WithEvents Panel14 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel16 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Panel_PsmsxUpdate As Panel
    Friend WithEvents Panel_VdmUpdate As Panel
    Friend WithEvents Panel17 As Panel
    Friend WithEvents Panel18 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel19 As Panel
    Friend WithEvents Panel20 As Panel
    Friend WithEvents Panel21 As Panel
    Friend WithEvents Button_PsmsxUpdateDownload As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents ClassPictureBoxQuality5 As ClassPictureBoxQuality
    Friend WithEvents Button_VdmUpdateDownload As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents ClassPictureBoxQuality6 As ClassPictureBoxQuality
    Friend WithEvents Button_PsmsUpdateIgnore As Button
    Friend WithEvents Button_VdmUpdateIgnore As Button
    Friend WithEvents ColumnHeader_ID As ColumnHeader
    Friend WithEvents ColumnHeader_Color As ColumnHeader
    Friend WithEvents LinkLabel_ServiceRunCmd As LinkLabel
    Friend WithEvents LinkLabel_ConfigToolRunCmd As LinkLabel
    Friend WithEvents Timer_RestartPsms As Timer
    Friend WithEvents LinkLabel_UninstallPSEyeDrivers As LinkLabel
    Friend WithEvents LinkLabel_ServiceLog As LinkLabel
    Friend WithEvents Panel_PsmsxInstall As Panel
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Button_PsmsInstallIgnore As Button
    Friend WithEvents Button_PsmsInstallBrowse As Button
    Friend WithEvents Button_PsmsxInstallDownload As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents ClassPictureBoxQuality7 As ClassPictureBoxQuality
    Friend WithEvents Panel22 As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents Panel23 As Panel
    Friend WithEvents LinkLabel_UninstallPS4CamDrivers As LinkLabel
    Friend WithEvents LinkLabel_InstallPS4CamDrivers As LinkLabel
    Friend WithEvents LinkLabel_ManageConnectedDevices As LinkLabel
    Friend WithEvents ColumnHeader_FPS As ColumnHeader
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Panel24 As Panel
    Friend WithEvents Chart_ServicePerformance As DataVisualization.Charting.Chart
    Friend WithEvents Panel25 As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents Panel26 As Panel
    Friend WithEvents Button_ChartSettings As Button
    Friend WithEvents ContextMenuStrip_Chart As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_ChartEnabled As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ChartClear As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripComboBox_ChartSamples As ToolStripComboBox
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
End Class
