<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDisplayDistortMonitor
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PictureBox_EyeR = New System.Windows.Forms.PictureBox()
        Me.PictureBox_EyeL = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.PictureBox_EyeR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox_EyeL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PictureBox_EyeR, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PictureBox_EyeL, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(800, 450)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'PictureBox_EyeR
        '
        Me.PictureBox_EyeR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox_EyeR.Location = New System.Drawing.Point(400, 0)
        Me.PictureBox_EyeR.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox_EyeR.Name = "PictureBox_EyeR"
        Me.PictureBox_EyeR.Size = New System.Drawing.Size(400, 450)
        Me.PictureBox_EyeR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox_EyeR.TabIndex = 1
        Me.PictureBox_EyeR.TabStop = False
        '
        'PictureBox_EyeL
        '
        Me.PictureBox_EyeL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox_EyeL.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox_EyeL.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox_EyeL.Name = "PictureBox_EyeL"
        Me.PictureBox_EyeL.Size = New System.Drawing.Size(400, 450)
        Me.PictureBox_EyeL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox_EyeL.TabIndex = 0
        Me.PictureBox_EyeL.TabStop = False
        '
        'FormDisplayDistortMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FormDisplayDistortMonitor"
        Me.Text = "FormDisplayDistortMonitor"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.PictureBox_EyeR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox_EyeL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents PictureBox_EyeR As PictureBox
    Friend WithEvents PictureBox_EyeL As PictureBox
End Class
