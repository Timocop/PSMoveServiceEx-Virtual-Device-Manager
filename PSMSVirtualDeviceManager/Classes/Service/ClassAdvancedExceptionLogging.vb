Public Class ClassAdvancedExceptionLogging
    Public Shared ReadOnly g_sLogName As String = IO.Path.Combine(Application.StartupPath, "application_error.ini")
    Private Shared g_mLogThread As Threading.Thread = Nothing
    Private Shared g_mLogPool As New ClassIni()
    Private Shared g_mThreadLock As New Object()
    Private Shared g_mLoggingTime As New TimeSpan(0, 1, 0)
    Private Shared g_bHasLoadedFromFile As Boolean = False
    Private Shared g_mExceptionQueue As New Queue(Of ClassIni.STRUC_INI_CONTENT())
    Private Shared g_bEnableLogging As Boolean = False

    Public Shared Sub LoadPoolFromFile()
        SyncLock g_mThreadLock
            If (Not m_EnableLogging) Then
                Return
            End If

            If (IO.File.Exists(g_sLogName)) Then
                g_mLogPool.ParseFromFile(g_sLogName)
            End If

            g_mExceptionQueue.Clear()
            g_bHasLoadedFromFile = True
        End SyncLock
    End Sub

    Public Shared Sub WritePoolToFile()
        SyncLock g_mThreadLock
            If (Not m_EnableLogging) Then
                Return
            End If

            If (Not g_bHasLoadedFromFile) Then
                Return
            End If

            g_mLogPool.ExportToFile(g_sLogName)
        End SyncLock
    End Sub

    Public Shared Sub WriteToLog(ex As Exception)
        Try
            If (Not m_EnableLogging) Then
                Return
            End If

            Dim sMessage As String = ex.Message
            Dim sStackTrace As String = ex.StackTrace
            Dim sFullException As String = ex.ToString

            If (String.IsNullOrEmpty(sMessage) OrElse sMessage.TrimEnd.Length = 0) Then
                Return
            End If

            If (String.IsNullOrEmpty(sStackTrace) OrElse sStackTrace.TrimEnd.Length = 0) Then
                Return
            End If

            Dim sChecksum As Integer = ClassUtils.CreateChecksum(sFullException, 0)
            Dim sMessageSingle As String = sMessage.Replace(vbCrLf, "\n").Replace(vbLf, "\n")
            Dim sStackTraceSingle As String = sStackTrace.Replace(vbCrLf, "\n").Replace(vbLf, "\n")

            Dim mList As New List(Of ClassIni.STRUC_INI_CONTENT)
            mList.Add(New ClassIni.STRUC_INI_CONTENT(CStr(sChecksum), "Message", sMessageSingle))
            mList.Add(New ClassIni.STRUC_INI_CONTENT(CStr(sChecksum), "StackTrace", sStackTraceSingle))
            mList.Add(New ClassIni.STRUC_INI_CONTENT(CStr(sChecksum), "Date", Date.Now.ToString(Globalization.CultureInfo.InvariantCulture)))

            SyncLock g_mThreadLock
                g_mExceptionQueue.Enqueue(mList.ToArray)
            End SyncLock
        Catch what As Exception
            ' Huh what?!
        End Try
    End Sub

    Public Shared Sub WriteToLogMessageBox(ex As Exception)
        WriteToLog(ex)
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Shared Property m_EnableLogging As Boolean
        Get
            SyncLock g_mThreadLock
                Return g_bEnableLogging
            End SyncLock
        End Get
        Set(value As Boolean)
            SyncLock g_mThreadLock
                g_bEnableLogging = value

                If (Not value AndAlso m_AutoFlushToFile) Then
                    m_AutoFlushToFile = False
                End If
            End SyncLock
        End Set
    End Property

    Shared Property m_AutoFlushToFile As Boolean
        Get
            SyncLock g_mThreadLock
                If (g_mLogThread IsNot Nothing AndAlso g_mLogThread.IsAlive) Then
                    Return True
                End If

                Return False
            End SyncLock
        End Get
        Set(value As Boolean)
            SyncLock g_mThreadLock
                If (value) Then
                    If (g_mLogThread Is Nothing OrElse Not g_mLogThread.IsAlive) Then
                        g_mLogThread = New Threading.Thread(AddressOf AutoFlushThread)
                        g_mLogThread.IsBackground = True
                        g_mLogThread.Start()
                    End If
                Else
                    If (g_mLogThread IsNot Nothing AndAlso g_mLogThread.IsAlive) Then
                        g_mLogThread.Abort()
                        g_mLogThread.Join()
                        g_mLogThread = Nothing

                        FlushToLog()
                        WritePoolToFile()
                    End If
                End If
            End SyncLock
        End Set
    End Property

    Private Shared Sub AutoFlushThread()
        While True
            Try
                SyncLock g_mThreadLock
                    If (Not g_bHasLoadedFromFile) Then
                        LoadPoolFromFile()
                    Else
                        FlushToLog()
                        WritePoolToFile()
                    End If
                End SyncLock
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                ' Ignore
            End Try

            Dim iSleepTime As Integer = 0

            SyncLock g_mThreadLock
                iSleepTime = CInt(g_mLoggingTime.TotalMilliseconds)
            End SyncLock

            Threading.Thread.Sleep(iSleepTime)
        End While
    End Sub

    Public Shared Sub FlushToLog()
        SyncLock g_mThreadLock
            If (g_bHasLoadedFromFile) Then
                Dim mLogList As New List(Of ClassIni.STRUC_INI_CONTENT)

                While (g_mExceptionQueue.Count > 0)
                    mLogList.AddRange(g_mExceptionQueue.Dequeue())
                End While

                g_mLogPool.WriteKeyValue(mLogList.ToArray)
            End If
        End SyncLock
    End Sub

    Public Shared Function GetDebugStackTrace(sText As String) As String
#If DEBUG Then
        Dim mStackTrace As New StackTrace(True)
        If (mStackTrace.FrameCount < 1) Then
            Return ""
        End If

        Dim sFile As String = mStackTrace.GetFrame(1).GetFileName
        Dim iLine As Integer = mStackTrace.GetFrame(1).GetFileLineNumber

        Return String.Format("{0}({1}): {2}", sFile, iLine, sText)
#Else
        Throw New ArgumentException("Only available in debug mode")
#End If
    End Function
End Class
