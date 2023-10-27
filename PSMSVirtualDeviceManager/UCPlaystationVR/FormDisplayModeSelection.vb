Public Class FormDisplayModeSelection
    Private g_bIgnoreEvents As Boolean = False

    Private Sub RadioButton_ModeDirect_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton_ModeDirect.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_bIgnoreEvents = True
        RadioButton_ModeVirtual.Checked = Not RadioButton_ModeDirect.Checked
        g_bIgnoreEvents = False
    End Sub

    Private Sub RadioButton_ModeVirtual_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton_ModeVirtual.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_bIgnoreEvents = True
        RadioButton_ModeDirect.Checked = Not RadioButton_ModeVirtual.Checked
        g_bIgnoreEvents = False
    End Sub

    Public ReadOnly Property m_ResultDirectMode As Boolean
        Get
            Return RadioButton_ModeDirect.Checked
        End Get
    End Property
End Class