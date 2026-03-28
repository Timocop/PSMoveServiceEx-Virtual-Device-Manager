Public Class ClassAdvancedExceptionLogging
    Private Shared g_mThreadLock As New Object

    Private Shared g_mMaxLoggingKeep As New TimeSpan(30, 0, 0, 0)
    Private Shared g_bHasLoadedFromFile As Boolean = False
    Private Shared g_bEnableLogging As Boolean = False
    Private Shared g_mExceptionDictionary As New Dictionary(Of Integer, STRUC_EXCEPTION_INFO)

    Private Structure STRUC_EXCEPTION_INFO
        Dim sMessage As String
        Dim sStackTrace As String
        Dim mDate As Date
        Dim iCount As Integer
        Dim sProductVersion As String

        Sub New(_Message As String,
                _StackTrace As String,
                _Date As Date,
                _Count As Integer,
                _ProductVersion As String)
            sMessage = _Message
            sStackTrace = _StackTrace
            mDate = _Date
            iCount = _Count
            sProductVersion = _ProductVersion
        End Sub
    End Structure

    Public Shared Sub LoadPoolFromFile()
        SyncLock g_mThreadLock
            If (Not m_EnableLogging) Then
                Return
            End If

            Try
                If (IO.File.Exists(ClassConfigConst.PATH_LOG_APPLICATION_ERROR)) Then
                    Using mIni As New ClassIni(ClassConfigConst.PATH_LOG_APPLICATION_ERROR, IO.FileMode.OpenOrCreate)
                        For Each sSection In mIni.GetSectionNames
                            Dim iChecksum As Integer = CInt(sSection)

                            g_mExceptionDictionary(iChecksum) = New STRUC_EXCEPTION_INFO(
                                mIni.ReadKeyValue(sSection, "Message"),
                                mIni.ReadKeyValue(sSection, "StackTrace"),
                                Date.Parse(mIni.ReadKeyValue(sSection, "Date"), Globalization.CultureInfo.InvariantCulture),
                                CInt(mIni.ReadKeyValue(sSection, "Count")),
                                mIni.ReadKeyValue(sSection, "Version")
                            )
                        Next
                    End Using
                End If
            Catch ex As Exception
            End Try

            m_HasLoadedFromFile = True
        End SyncLock
    End Sub

    Public Shared Sub WritePoolToFile()
        SyncLock g_mThreadLock
            If (Not m_EnableLogging) Then
                Return
            End If

            If (Not m_HasLoadedFromFile) Then
                Return
            End If

            Try
                Using mIni As New ClassIni(ClassConfigConst.PATH_LOG_APPLICATION_ERROR, IO.FileMode.OpenOrCreate)
                    For Each mItem In g_mExceptionDictionary
                        If (mItem.Value.mDate + g_mMaxLoggingKeep < Now) Then
                            mIni.RemoveSection(CStr(mItem.Key))
                            Continue For
                        End If

                        mIni.WriteKeyValue(CStr(mItem.Key), "Message", mItem.Value.sMessage)
                        mIni.WriteKeyValue(CStr(mItem.Key), "StackTrace", mItem.Value.sStackTrace)
                        mIni.WriteKeyValue(CStr(mItem.Key), "Date", mItem.Value.mDate.ToString(Globalization.CultureInfo.InvariantCulture))
                        mIni.WriteKeyValue(CStr(mItem.Key), "Count", CStr(mItem.Value.iCount))
                        mIni.WriteKeyValue(CStr(mItem.Key), "Version", mItem.Value.sProductVersion)
                    Next
                End Using
            Catch ex As Exception
            End Try
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

            Dim iChecksum As Integer = CreateChecksum(sFullException, 0)
            Dim sMessageSingle As String = sMessage.Replace(vbCrLf, "\n").Replace(vbLf, "\n")
            Dim sStackTraceSingle As String = sStackTrace.Replace(vbCrLf, "\n").Replace(vbLf, "\n")
            Dim iExceptionCount As Integer = 0

            SyncLock g_mThreadLock
                If (g_mExceptionDictionary.ContainsKey(iChecksum)) Then
                    iExceptionCount = g_mExceptionDictionary(iChecksum).iCount
                End If

                iExceptionCount += 1

                g_mExceptionDictionary(iChecksum) = New STRUC_EXCEPTION_INFO(sMessageSingle, sStackTraceSingle, Now, iExceptionCount, Application.ProductVersion)
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
            End SyncLock
        End Set
    End Property

    Private Shared Property m_HasLoadedFromFile As Boolean
        Get
            SyncLock g_mThreadLock
                Return g_bHasLoadedFromFile
            End SyncLock
        End Get
        Set(value As Boolean)
            SyncLock g_mThreadLock
                g_bHasLoadedFromFile = value
            End SyncLock
        End Set
    End Property

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

    Private Shared Function CreateChecksum(sText As String, iSalt As Integer) As Integer
        Dim iSum As Integer = iSalt

        For i = 0 To sText.Length - 1
            iSum = iSum * 101 + AscW(sText(i))
        Next

        Return iSum
    End Function
End Class
