Public Class UCRestartSteamVRWarning
    Private Shared ReadOnly g_sProcessName As String = "vrserver"

    Private g_mRestartThread As Threading.Thread = Nothing

    Public Sub ShowAndWait()
        Try
            Dim mProcess As Process() = Process.GetProcessesByName(g_sProcessName)
            If (mProcess.Count > 0) Then
                Me.Visible = True

                If (g_mRestartThread Is Nothing OrElse Not g_mRestartThread.IsAlive) Then
                    g_mRestartThread = New Threading.Thread(AddressOf RestartThread)
                    g_mRestartThread.IsBackground = True
                    g_mRestartThread.Start()
                End If
            End If
        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try
    End Sub

    Private Sub RestartThread()
        Try
            Dim mProcess As Process() = Process.GetProcessesByName(g_sProcessName)
            If (mProcess.Count > 0) Then
                For i = 0 To mProcess.Count - 1
                    mProcess(i).WaitForExit()
                Next
            End If
        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try

        ClassUtils.AsyncInvoke(Me, Sub() Me.Visible = False)
    End Sub

    Private Sub CleanUp()
        If (g_mRestartThread IsNot Nothing AndAlso g_mRestartThread.IsAlive) Then
            g_mRestartThread.Abort()
            g_mRestartThread.Join()
            g_mRestartThread = Nothing
        End If
    End Sub
End Class
