Public Class UCRestartProcessWarning
    Private g_sProcessName As String = ""

    Private g_mRestartThread As Threading.Thread = Nothing

    Private Shared ReadOnly g_mThreadLock As New Object

    Property m_Message As String
        Get
            Return Label_Message.Text
        End Get
        Set(value As String)
            Label_Message.Text = value
        End Set
    End Property

    Property m_ProcessName As String
        Get
            SyncLock g_mThreadLock
                Return g_sProcessName
            End SyncLock
        End Get
        Set(value As String)
            SyncLock g_mThreadLock
                g_sProcessName = value
            End SyncLock
        End Set
    End Property

    Property m_ProcessNames As String()
        Get
            SyncLock g_mThreadLock
                Return g_sProcessName.Split(";"c)
            End SyncLock
        End Get
        Set(value As String())
            SyncLock g_mThreadLock
                g_sProcessName = String.Join(";"c, value)
            End SyncLock
        End Set
    End Property

    Public Sub ShowAndWait()
        Try
            Dim sProcessName As String = m_ProcessName
            If (String.IsNullOrEmpty(sProcessName) OrElse sProcessName.TrimEnd.Length = 0) Then
                Throw New ArgumentException("Process name empty")
            End If

            Dim mProcesses As New List(Of Process)
            For Each sName As String In m_ProcessNames
                If (String.IsNullOrEmpty(sName) OrElse sName.TrimEnd.Length = 0) Then
                    Continue For
                End If

                mProcesses.AddRange(Process.GetProcessesByName(sName))
            Next

            If (mProcesses.Count > 0) Then
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
            Dim sProcessName As String = m_ProcessName
            If (String.IsNullOrEmpty(sProcessName) OrElse sProcessName.TrimEnd.Length = 0) Then
                Throw New ArgumentException("Process name empty")
            End If

            Dim mProcesses As New List(Of Process)
            For Each sName As String In m_ProcessNames
                If (String.IsNullOrEmpty(sName) OrElse sName.TrimEnd.Length = 0) Then
                    Continue For
                End If

                mProcesses.AddRange(Process.GetProcessesByName(sName))
            Next

            If (mProcesses.Count > 0) Then
                For i = 0 To mProcesses.Count - 1
                    mProcesses(i).WaitForExit()
                Next
            End If
        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try

        ClassUtils.AsyncInvoke(Sub() Me.Visible = False)
    End Sub

    Private Sub CleanUp()
        If (g_mRestartThread IsNot Nothing AndAlso g_mRestartThread.IsAlive) Then
            g_mRestartThread.Abort()
            g_mRestartThread.Join()
            g_mRestartThread = Nothing
        End If
    End Sub
End Class
