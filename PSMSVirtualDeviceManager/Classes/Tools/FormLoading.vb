Public Class FormLoading
    Public ReadOnly Property m_ProgressBar As ProgressBar
        Get
            Return ProgressBar1
        End Get
    End Property

    Public Sub SkipProgressBarAnimation()
        If (Me.ProgressBar1.Style = ProgressBarStyle.Marquee) Then
            Return
        End If

        Dim iOldVal = ProgressBar1.Value
        ProgressBar1.Increment(1)
        ProgressBar1.Value = iOldVal
    End Sub
End Class