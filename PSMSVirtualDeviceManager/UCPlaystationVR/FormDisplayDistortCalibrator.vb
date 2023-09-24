Public Class FormDisplayDistortCalibrator
    Private g_FormDisplayDistortMonitor As FormDisplayDistortMonitor
    Private g_bIgnoreEvents As Boolean = True

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        g_FormDisplayDistortMonitor = New FormDisplayDistortMonitor
        g_FormDisplayDistortMonitor.g_iDistortFov = NumericUpDown_Fov.Value
        g_FormDisplayDistortMonitor.g_iDistortK0 = NumericUpDown_DistortK0.Value
        g_FormDisplayDistortMonitor.g_iDistortK1 = NumericUpDown_DistortK1.Value
        g_FormDisplayDistortMonitor.g_iDistortScale = NumericUpDown_DistortScale.Value
        g_FormDisplayDistortMonitor.g_iDistortRScale = NumericUpDown_RedScale.Value
        g_FormDisplayDistortMonitor.g_iDistortGScale = NumericUpDown_GreenScale.Value
        g_FormDisplayDistortMonitor.g_iDistortBScale = NumericUpDown_BlueScale.Value
        g_FormDisplayDistortMonitor.g_iPatternSize = CInt(NumericUpDown_PatternSize.Value)
        g_bIgnoreEvents = False
    End Sub

    Private Sub NumericUpDown_Fov_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_Fov.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_FormDisplayDistortMonitor.g_iDistortFov = NumericUpDown_Fov.Value
    End Sub

    Private Sub NumericUpDown_DistortK0_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_DistortK0.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_FormDisplayDistortMonitor.g_iDistortK0 = NumericUpDown_DistortK0.Value
    End Sub

    Private Sub NumericUpDown_DistortK1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_DistortK1.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_FormDisplayDistortMonitor.g_iDistortK1 = NumericUpDown_DistortK1.Value
    End Sub

    Private Sub NumericUpDown_DistortScale_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_DistortScale.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_FormDisplayDistortMonitor.g_iDistortScale = NumericUpDown_DistortScale.Value
    End Sub

    Private Sub NumericUpDown_RedScale_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_RedScale.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_FormDisplayDistortMonitor.g_iDistortRScale = NumericUpDown_RedScale.Value
    End Sub

    Private Sub NumericUpDown_GreenScale_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_GreenScale.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_FormDisplayDistortMonitor.g_iDistortGScale = NumericUpDown_GreenScale.Value
    End Sub

    Private Sub NumericUpDown_BlueScale_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_BlueScale.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_FormDisplayDistortMonitor.g_iDistortBScale = NumericUpDown_BlueScale.Value
    End Sub

    Private Sub NumericUpDown_PatternSize_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PatternSize.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_FormDisplayDistortMonitor.g_iPatternSize = CInt(NumericUpDown_PatternSize.Value)
    End Sub



    Private Sub FormDisplayDistortCalibrator_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CleanUp()
    End Sub

    Private Sub CleanUp()
        If (g_FormDisplayDistortMonitor IsNot Nothing AndAlso Not g_FormDisplayDistortMonitor.IsDisposed) Then
            g_FormDisplayDistortMonitor.Dispose()
            g_FormDisplayDistortMonitor = Nothing
        End If
    End Sub

End Class