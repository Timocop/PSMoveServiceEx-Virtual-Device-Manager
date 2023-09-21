<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCPlaystationVR
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label_DisplayStatusText = New System.Windows.Forms.Label()
        Me.Label_DisplayStatus = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label_HDMIStatus = New System.Windows.Forms.Label()
        Me.Label_HDMIStatusText = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label_USBStatusText = New System.Windows.Forms.Label()
        Me.Label_USBStatus = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label_PSVRStatus = New System.Windows.Forms.Label()
        Me.Panel_PSVRStatus = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.ClassPictureBox_DisplayStatus = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBox_HDMIStatus = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.ClassPictureBox_USBStatus = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.PictureBox1 = New PSMSVirtualDeviceManager.ClassPictureBoxQuality()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel12.SuspendLayout()
        CType(Me.ClassPictureBox_DisplayStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBox_HDMIStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClassPictureBox_USBStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 64)
        Me.Panel1.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(66, 30)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(731, 30)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Manage or confugure your PlayStation VR hardware."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(66, 3)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(229, 21)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "PlayStation VR Management"
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
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel2.Controls.Add(Me.Panel12)
        Me.Panel2.Location = New System.Drawing.Point(16, 83)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(16)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(768, 268)
        Me.Panel2.TabIndex = 10
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel5, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel3, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel4, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 42)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.42857!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.57143!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(766, 224)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label_DisplayStatusText)
        Me.Panel5.Controls.Add(Me.ClassPictureBox_DisplayStatus)
        Me.Panel5.Controls.Add(Me.Label_DisplayStatus)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 103)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(383, 121)
        Me.Panel5.TabIndex = 2
        '
        'Label_DisplayStatusText
        '
        Me.Label_DisplayStatusText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_DisplayStatusText.Location = New System.Drawing.Point(109, 43)
        Me.Label_DisplayStatusText.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_DisplayStatusText.Name = "Label_DisplayStatusText"
        Me.Label_DisplayStatusText.Size = New System.Drawing.Size(271, 49)
        Me.Label_DisplayStatusText.TabIndex = 6
        Me.Label_DisplayStatusText.Text = "The PlayStation VR display has not been configured correctly."
        '
        'Label_DisplayStatus
        '
        Me.Label_DisplayStatus.AutoSize = True
        Me.Label_DisplayStatus.BackColor = System.Drawing.Color.Transparent
        Me.Label_DisplayStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_DisplayStatus.ForeColor = System.Drawing.Color.Navy
        Me.Label_DisplayStatus.Location = New System.Drawing.Point(108, 19)
        Me.Label_DisplayStatus.Margin = New System.Windows.Forms.Padding(3, 16, 3, 0)
        Me.Label_DisplayStatus.Name = "Label_DisplayStatus"
        Me.Label_DisplayStatus.Size = New System.Drawing.Size(167, 21)
        Me.Label_DisplayStatus.TabIndex = 5
        Me.Label_DisplayStatus.Text = "Display not configured"
        Me.Label_DisplayStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.ClassPictureBox_HDMIStatus)
        Me.Panel3.Controls.Add(Me.Label_HDMIStatus)
        Me.Panel3.Controls.Add(Me.Label_HDMIStatusText)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(383, 103)
        Me.Panel3.TabIndex = 0
        '
        'Label_HDMIStatus
        '
        Me.Label_HDMIStatus.AutoSize = True
        Me.Label_HDMIStatus.BackColor = System.Drawing.Color.Transparent
        Me.Label_HDMIStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_HDMIStatus.ForeColor = System.Drawing.Color.Navy
        Me.Label_HDMIStatus.Location = New System.Drawing.Point(107, 19)
        Me.Label_HDMIStatus.Margin = New System.Windows.Forms.Padding(3, 16, 3, 0)
        Me.Label_HDMIStatus.Name = "Label_HDMIStatus"
        Me.Label_HDMIStatus.Size = New System.Drawing.Size(147, 21)
        Me.Label_HDMIStatus.TabIndex = 2
        Me.Label_HDMIStatus.Text = "HDMI Disconnected"
        Me.Label_HDMIStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label_HDMIStatusText
        '
        Me.Label_HDMIStatusText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_HDMIStatusText.Location = New System.Drawing.Point(108, 43)
        Me.Label_HDMIStatusText.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_HDMIStatusText.Name = "Label_HDMIStatusText"
        Me.Label_HDMIStatusText.Size = New System.Drawing.Size(272, 53)
        Me.Label_HDMIStatusText.TabIndex = 3
        Me.Label_HDMIStatusText.Text = "Please attach the PlayStation VR HDMI cable to your computer."
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label_USBStatusText)
        Me.Panel4.Controls.Add(Me.ClassPictureBox_USBStatus)
        Me.Panel4.Controls.Add(Me.Label_USBStatus)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(383, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(383, 103)
        Me.Panel4.TabIndex = 1
        '
        'Label_USBStatusText
        '
        Me.Label_USBStatusText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_USBStatusText.Location = New System.Drawing.Point(109, 43)
        Me.Label_USBStatusText.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_USBStatusText.Name = "Label_USBStatusText"
        Me.Label_USBStatusText.Size = New System.Drawing.Size(271, 49)
        Me.Label_USBStatusText.TabIndex = 6
        Me.Label_USBStatusText.Text = "Please attach the PlayStation VR USB cable to your computer."
        '
        'Label_USBStatus
        '
        Me.Label_USBStatus.AutoSize = True
        Me.Label_USBStatus.BackColor = System.Drawing.Color.Transparent
        Me.Label_USBStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_USBStatus.ForeColor = System.Drawing.Color.Navy
        Me.Label_USBStatus.Location = New System.Drawing.Point(108, 19)
        Me.Label_USBStatus.Margin = New System.Windows.Forms.Padding(3, 16, 3, 0)
        Me.Label_USBStatus.Name = "Label_USBStatus"
        Me.Label_USBStatus.Size = New System.Drawing.Size(136, 21)
        Me.Label_USBStatus.TabIndex = 5
        Me.Label_USBStatus.Text = "USB Disconnected"
        Me.Label_USBStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel12
        '
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Controls.Add(Me.Label_PSVRStatus)
        Me.Panel12.Controls.Add(Me.Panel_PSVRStatus)
        Me.Panel12.Controls.Add(Me.Panel6)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(766, 42)
        Me.Panel12.TabIndex = 0
        '
        'Label_PSVRStatus
        '
        Me.Label_PSVRStatus.BackColor = System.Drawing.Color.Transparent
        Me.Label_PSVRStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PSVRStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_PSVRStatus.ForeColor = System.Drawing.Color.Navy
        Me.Label_PSVRStatus.Location = New System.Drawing.Point(19, 0)
        Me.Label_PSVRStatus.Name = "Label_PSVRStatus"
        Me.Label_PSVRStatus.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.Label_PSVRStatus.Size = New System.Drawing.Size(747, 41)
        Me.Label_PSVRStatus.TabIndex = 1
        Me.Label_PSVRStatus.Text = "PlayStation VR Disconnected"
        Me.Label_PSVRStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel_PSVRStatus
        '
        Me.Panel_PSVRStatus.BackColor = System.Drawing.Color.LightGray
        Me.Panel_PSVRStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel_PSVRStatus.Location = New System.Drawing.Point(0, 0)
        Me.Panel_PSVRStatus.Name = "Panel_PSVRStatus"
        Me.Panel_PSVRStatus.Size = New System.Drawing.Size(19, 41)
        Me.Panel_PSVRStatus.TabIndex = 2
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
        'ClassPictureBox_DisplayStatus
        '
        Me.ClassPictureBox_DisplayStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Connection_DISPLAY_FAIL
        Me.ClassPictureBox_DisplayStatus.Location = New System.Drawing.Point(16, 6)
        Me.ClassPictureBox_DisplayStatus.m_HighQuality = True
        Me.ClassPictureBox_DisplayStatus.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.ClassPictureBox_DisplayStatus.Name = "ClassPictureBox_DisplayStatus"
        Me.ClassPictureBox_DisplayStatus.Size = New System.Drawing.Size(86, 86)
        Me.ClassPictureBox_DisplayStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBox_DisplayStatus.TabIndex = 4
        Me.ClassPictureBox_DisplayStatus.TabStop = False
        '
        'ClassPictureBox_HDMIStatus
        '
        Me.ClassPictureBox_HDMIStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Connection_HDMI_FAIL
        Me.ClassPictureBox_HDMIStatus.Location = New System.Drawing.Point(16, 6)
        Me.ClassPictureBox_HDMIStatus.m_HighQuality = True
        Me.ClassPictureBox_HDMIStatus.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.ClassPictureBox_HDMIStatus.Name = "ClassPictureBox_HDMIStatus"
        Me.ClassPictureBox_HDMIStatus.Size = New System.Drawing.Size(86, 86)
        Me.ClassPictureBox_HDMIStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBox_HDMIStatus.TabIndex = 1
        Me.ClassPictureBox_HDMIStatus.TabStop = False
        '
        'ClassPictureBox_USBStatus
        '
        Me.ClassPictureBox_USBStatus.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources.Connection_USB_FAIL
        Me.ClassPictureBox_USBStatus.Location = New System.Drawing.Point(16, 6)
        Me.ClassPictureBox_USBStatus.m_HighQuality = True
        Me.ClassPictureBox_USBStatus.Margin = New System.Windows.Forms.Padding(16, 3, 3, 3)
        Me.ClassPictureBox_USBStatus.Name = "ClassPictureBox_USBStatus"
        Me.ClassPictureBox_USBStatus.Size = New System.Drawing.Size(86, 86)
        Me.ClassPictureBox_USBStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ClassPictureBox_USBStatus.TabIndex = 4
        Me.ClassPictureBox_USBStatus.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PSMSVirtualDeviceManager.My.Resources.Resources._450
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.m_HighQuality = True
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(57, 57)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'UCPlaystationVR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCPlaystationVR"
        Me.Size = New System.Drawing.Size(800, 658)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        CType(Me.ClassPictureBox_DisplayStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBox_HDMIStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClassPictureBox_USBStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As ClassPictureBoxQuality
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label_USBStatusText As Label
    Friend WithEvents Label_USBStatus As Label
    Friend WithEvents ClassPictureBox_USBStatus As ClassPictureBoxQuality
    Friend WithEvents Label_HDMIStatusText As Label
    Friend WithEvents Label_HDMIStatus As Label
    Friend WithEvents ClassPictureBox_HDMIStatus As ClassPictureBoxQuality
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Label_PSVRStatus As Label
    Friend WithEvents Panel_PSVRStatus As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label_DisplayStatusText As Label
    Friend WithEvents ClassPictureBox_DisplayStatus As ClassPictureBoxQuality
    Friend WithEvents Label_DisplayStatus As Label
End Class
