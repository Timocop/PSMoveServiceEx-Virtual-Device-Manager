<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDisplayDistortCalibrator
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NumericUpDown_DistortK0 = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumericUpDown_DistortK1 = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PatternSize = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.NumericUpDown_RedScale = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumericUpDown_GreenScale = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.NumericUpDown_BlueScale = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.NumericUpDown_Fov = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown_DistortScale = New System.Windows.Forms.NumericUpDown()
        CType(Me.NumericUpDown_DistortK0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_DistortK1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_PatternSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_RedScale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_GreenScale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_BlueScale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_Fov, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_DistortScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 25)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Distortion Settings:"
        '
        'NumericUpDown_DistortK0
        '
        Me.NumericUpDown_DistortK0.DecimalPlaces = 3
        Me.NumericUpDown_DistortK0.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.NumericUpDown_DistortK0.Location = New System.Drawing.Point(107, 72)
        Me.NumericUpDown_DistortK0.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_DistortK0.Name = "NumericUpDown_DistortK0"
        Me.NumericUpDown_DistortK0.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_DistortK0.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 74)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "K0:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 102)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "K1:"
        '
        'NumericUpDown_DistortK1
        '
        Me.NumericUpDown_DistortK1.DecimalPlaces = 3
        Me.NumericUpDown_DistortK1.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.NumericUpDown_DistortK1.Location = New System.Drawing.Point(107, 100)
        Me.NumericUpDown_DistortK1.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_DistortK1.Name = "NumericUpDown_DistortK1"
        Me.NumericUpDown_DistortK1.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_DistortK1.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 130)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Scale:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 246)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 16, 3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Pattern Settings:"
        '
        'NumericUpDown_PatternSize
        '
        Me.NumericUpDown_PatternSize.Location = New System.Drawing.Point(107, 265)
        Me.NumericUpDown_PatternSize.Name = "NumericUpDown_PatternSize"
        Me.NumericUpDown_PatternSize.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_PatternSize.TabIndex = 9
        Me.NumericUpDown_PatternSize.Value = New Decimal(New Integer() {64, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 267)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Size:"
        '
        'NumericUpDown_RedScale
        '
        Me.NumericUpDown_RedScale.DecimalPlaces = 3
        Me.NumericUpDown_RedScale.Increment = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.NumericUpDown_RedScale.Location = New System.Drawing.Point(107, 156)
        Me.NumericUpDown_RedScale.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_RedScale.Name = "NumericUpDown_RedScale"
        Me.NumericUpDown_RedScale.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_RedScale.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 158)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Red Offset:"
        '
        'NumericUpDown_GreenScale
        '
        Me.NumericUpDown_GreenScale.DecimalPlaces = 3
        Me.NumericUpDown_GreenScale.Increment = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.NumericUpDown_GreenScale.Location = New System.Drawing.Point(107, 184)
        Me.NumericUpDown_GreenScale.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_GreenScale.Name = "NumericUpDown_GreenScale"
        Me.NumericUpDown_GreenScale.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_GreenScale.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 186)
        Me.Label8.Margin = New System.Windows.Forms.Padding(3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Green Offset:"
        '
        'NumericUpDown_BlueScale
        '
        Me.NumericUpDown_BlueScale.DecimalPlaces = 3
        Me.NumericUpDown_BlueScale.Increment = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.NumericUpDown_BlueScale.Location = New System.Drawing.Point(107, 212)
        Me.NumericUpDown_BlueScale.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_BlueScale.Name = "NumericUpDown_BlueScale"
        Me.NumericUpDown_BlueScale.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_BlueScale.TabIndex = 15
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 214)
        Me.Label9.Margin = New System.Windows.Forms.Padding(3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 13)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Blue Offset:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 46)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 13)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "FOV:"
        '
        'NumericUpDown_Fov
        '
        Me.NumericUpDown_Fov.DecimalPlaces = 3
        Me.NumericUpDown_Fov.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.NumericUpDown_Fov.Location = New System.Drawing.Point(107, 44)
        Me.NumericUpDown_Fov.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumericUpDown_Fov.Name = "NumericUpDown_Fov"
        Me.NumericUpDown_Fov.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_Fov.TabIndex = 16
        '
        'NumericUpDown_DistortScale
        '
        Me.NumericUpDown_DistortScale.DecimalPlaces = 3
        Me.NumericUpDown_DistortScale.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.NumericUpDown_DistortScale.Location = New System.Drawing.Point(107, 128)
        Me.NumericUpDown_DistortScale.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.NumericUpDown_DistortScale.Name = "NumericUpDown_DistortScale"
        Me.NumericUpDown_DistortScale.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown_DistortScale.TabIndex = 6
        Me.NumericUpDown_DistortScale.Value = New Decimal(New Integer() {10, 0, 0, 65536})
        '
        'FormDisplayDistortCalibrator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(239, 349)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.NumericUpDown_Fov)
        Me.Controls.Add(Me.NumericUpDown_BlueScale)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.NumericUpDown_GreenScale)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.NumericUpDown_RedScale)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.NumericUpDown_PatternSize)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.NumericUpDown_DistortScale)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.NumericUpDown_DistortK1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.NumericUpDown_DistortK0)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormDisplayDistortCalibrator"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Debug Display Distortion"
        CType(Me.NumericUpDown_DistortK0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_DistortK1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_PatternSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_RedScale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_GreenScale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_BlueScale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_Fov, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_DistortScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents NumericUpDown_DistortK0 As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents NumericUpDown_DistortK1 As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents NumericUpDown_PatternSize As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents NumericUpDown_RedScale As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents NumericUpDown_GreenScale As NumericUpDown
    Friend WithEvents Label8 As Label
    Friend WithEvents NumericUpDown_BlueScale As NumericUpDown
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents NumericUpDown_Fov As NumericUpDown
    Friend WithEvents NumericUpDown_DistortScale As NumericUpDown
End Class
