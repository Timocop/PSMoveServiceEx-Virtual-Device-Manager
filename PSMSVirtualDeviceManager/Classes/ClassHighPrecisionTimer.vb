Public Class ClassHighPrecisionTimer
    Private Shared ReadOnly m_Clock As Stopwatch = Stopwatch.StartNew()
    Private Shared ReadOnly m_Frequency As Double = Stopwatch.Frequency

    Public Shared Function GetTicks() As Long
        If (Not Stopwatch.IsHighResolution) Then
            Debug.WriteLine("IsHighResolution = false")
        End If

        Return Stopwatch.GetTimestamp()
    End Function

    Public Shared Function TicksToSeconds(iTicks As Long) As Double
        Return iTicks / m_Frequency
    End Function

    Public Shared Function TicksToMilliseconds(iTicks As Long) As Double
        Return iTicks * 1000.0 / m_Frequency
    End Function

    Public Shared Function ElapsedSeconds(iTicks As Long, iLastTicks As Long) As Double
        Return (iTicks - iLastTicks) / m_Frequency
    End Function

    Public Shared Function ElapsedMilliseconds(iTicks As Long, iLastTicks As Long) As Double
        Return (iTicks - iLastTicks) * 1000.0 / m_Frequency
    End Function

    Public Shared Function ElapsedTimeSpan(iTicks As Long, iLastTicks As Long) As TimeSpan
        Return TimeSpan.FromSeconds(ElapsedSeconds(iTicks, iLastTicks))
    End Function
End Class
