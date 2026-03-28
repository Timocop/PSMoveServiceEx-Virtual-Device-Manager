Public Class ClassIni
    Implements IDisposable

    Private g_mStream As IO.Stream
    Private g_mStreamWriter As IO.StreamWriter
    Private g_mStreamReader As IO.StreamReader

    Private g_mIniContent As New Dictionary(Of String, Dictionary(Of String, String))
    Private g_bHasLoaded As Boolean = False
    Private g_bHasChanged As Boolean = False

    Structure STRUC_INI_ITEM
        Dim sSection As String
        Dim sKey As String
        Dim sValue As String

        Sub New(_Section As String, _Key As String, _Value As String)
            sSection = _Section
            sKey = _Key
            sValue = _Value
        End Sub
    End Structure

    Public Sub New()
        Me.New(New IO.MemoryStream())

        Load()
    End Sub

    Public Sub New(sContent As String)
        Me.New(New IO.MemoryStream())

        g_mStreamWriter.Write(sContent)

        Load()
    End Sub

    Public Sub New(sFile As String, iMode As IO.FileMode)
        Me.New(New IO.FileStream(sFile, iMode, IO.FileAccess.ReadWrite))

        Load()
    End Sub

    Public Sub New(mStream As IO.Stream)
        g_mStream = mStream

        If (mStream.CanWrite) Then
            g_mStreamWriter = New IO.StreamWriter(mStream)
            g_mStreamWriter.AutoFlush = True
        End If

        If (mStream.CanRead) Then
            g_mStreamReader = New IO.StreamReader(mStream)
        End If

        Load()
    End Sub

    Public ReadOnly Property m_ReadOnly As Boolean
        Get
            Return (Not g_mStream.CanWrite)
        End Get
    End Property

    Public Function ReadKeyValue(sSection As String, sKey As String) As String
        If (Not g_mIniContent.ContainsKey(sSection)) Then
            Return Nothing
        End If

        If (Not g_mIniContent(sSection).ContainsKey(sKey)) Then
            Return Nothing
        End If

        Return g_mIniContent(sSection)(sKey)
    End Function

    Public Function ReadKeyValue(sSection As String, sKey As String, sReturnIfNothing As String) As String
        If (Not g_mIniContent.ContainsKey(sSection)) Then
            Return sReturnIfNothing
        End If

        If (Not g_mIniContent(sSection).ContainsKey(sKey)) Then
            Return sReturnIfNothing
        End If

        Return g_mIniContent(sSection)(sKey)
    End Function

    Public Function ReadKeyValueThrow(sSection As String, sKey As String) As String
        If (Not g_mIniContent.ContainsKey(sSection)) Then
            Throw New ArgumentException("Can not find INI section")
        End If

        If (Not g_mIniContent(sSection).ContainsKey(sKey)) Then
            Throw New ArgumentException("Can not find INI key")
        End If

        Return g_mIniContent(sSection)(sKey)
    End Function

    Public Sub RemoveSection(sSection As String)
        If (Not g_mIniContent.ContainsKey(sSection)) Then
            Return
        End If

        g_mIniContent.Remove(sSection)

        g_bHasChanged = True
    End Sub

    Public Sub RemoveKeyValue(sSection As String, sKey As String)
        If (Not g_mIniContent.ContainsKey(sSection)) Then
            Return
        End If

        If (Not g_mIniContent(sSection).ContainsKey(sKey)) Then
            Return
        End If

        g_mIniContent(sSection).Remove(sKey)

        g_bHasChanged = True
    End Sub

    Public Sub WriteKeyValue(sSection As String, sKey As String, Optional sValue As String = Nothing)
        If (sValue Is Nothing) Then
            RemoveKeyValue(sSection, sKey)
        Else
            If (Not g_mIniContent.ContainsKey(sSection)) Then
                g_mIniContent(sSection) = New Dictionary(Of String, String)
            End If

            g_mIniContent(sSection)(sKey) = sValue
        End If

        g_bHasChanged = True
    End Sub

    Public Function GetAllItems() As STRUC_INI_ITEM()
        Dim mTotalItems As New List(Of STRUC_INI_ITEM)

        For Each mItem In g_mIniContent
            For Each mKeys In mItem.Value
                mTotalItems.Add(New STRUC_INI_ITEM(mItem.Key, mKeys.Key, mKeys.Value))
            Next
        Next

        Return mTotalItems.ToArray
    End Function

    Public Function GetSectionNames() As String()
        Dim sKeys As New List(Of String)
        For Each sKey In g_mIniContent.Keys
            sKeys.Add(sKey)
        Next

        Return sKeys.ToArray
    End Function

    Public Function GetSectionKeys(sSection As String) As KeyValuePair(Of String, String)()
        If (Not g_mIniContent.ContainsKey(sSection)) Then
            Return {}
        End If

        Dim mKeys As New List(Of KeyValuePair(Of String, String))

        For Each mItem In g_mIniContent(sSection)
            mKeys.Add(New KeyValuePair(Of String, String)(mItem.Key, mItem.Value))
        Next

        Return mKeys.ToArray
    End Function

    Public Sub ExportToFile(sFile As String)
        If (Not Save(False)) Then
            Return
        End If

        g_mStream.Seek(0, IO.SeekOrigin.Begin)

        Using mFile As New IO.FileStream(sFile, IO.FileMode.OpenOrCreate, IO.FileAccess.Write, IO.FileShare.Read, 4096, IO.FileOptions.SequentialScan)
            mFile.SetLength(0)

            CopyStream(g_mStream, mFile, 1024 * 8)
        End Using
    End Sub

    Public Function ExportToString() As String
        If (Not Save(False)) Then
            Return ""
        End If

        g_mStream.Seek(0, IO.SeekOrigin.Begin)
        Return g_mStreamReader.ReadToEnd
    End Function

    Public Sub ParseFromFile(sFile As String)
        g_mStream.Seek(0, IO.SeekOrigin.Begin)
        g_mStream.SetLength(0)

        Using mFile As New IO.FileStream(sFile, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite, 4096, IO.FileOptions.SequentialScan)
            CopyStream(mFile, g_mStream, 1024 * 8)
        End Using

        Load()
    End Sub

    Public Sub ParseFromString(sText As String)
        g_mStreamWriter.BaseStream.Seek(0, IO.SeekOrigin.Begin)
        g_mStreamWriter.BaseStream.SetLength(0)
        g_mStreamWriter.Write(sText)

        Load()
    End Sub

    Private Sub Load()
        Dim sCurrentSection As String = ""

        g_mIniContent.Clear()

        g_mStreamReader.BaseStream.Seek(0, IO.SeekOrigin.Begin)

        While True
            Dim sLine As String = g_mStreamReader.ReadLine
            If (sLine Is Nothing) Then
                Exit While
            End If

            'Ignore comments
            If (sLine.TrimStart.StartsWith(";")) Then
                Continue While
            End If

            Dim sSection As String = GetSectionFromLine(sLine)
            If (sSection IsNot Nothing) Then
                sCurrentSection = sSection
                Continue While
            End If

            Dim mContent = GetKeyAndValueFromLine(sLine)
            If (mContent.Key Is Nothing OrElse mContent.Value Is Nothing) Then
                Continue While
            End If

            If (Not g_mIniContent.ContainsKey(sCurrentSection)) Then
                g_mIniContent(sCurrentSection) = New Dictionary(Of String, String)
            End If

            g_mIniContent(sCurrentSection)(mContent.Key) = mContent.Value
        End While

        g_bHasLoaded = True
        g_bHasChanged = False
    End Sub

    Private Function Save(Optional bOnlyChanged As Boolean = True) As Boolean
        If (Not g_bHasLoaded) Then
            Return False
        End If

        If (bOnlyChanged) Then
            If (Not g_bHasChanged) Then
                Return False
            End If
        End If

        Dim sContentWriter As New Text.StringBuilder

        For Each mItem In g_mIniContent
            Dim sSectionWriter As New Text.StringBuilder
            Dim bKeysInSection As Boolean = False

            sSectionWriter.AppendFormat("[{0}]", mItem.Key).AppendLine()

            For Each mKeys In mItem.Value
                If (String.IsNullOrEmpty(mKeys.Key)) Then
                    Continue For
                End If

                If (String.IsNullOrEmpty(mKeys.Value)) Then
                    Continue For
                End If

                sSectionWriter.AppendFormat("{0}={1}", mKeys.Key, mKeys.Value).AppendLine()
                bKeysInSection = True
            Next

            If (bKeysInSection) Then
                sContentWriter.Append(sSectionWriter.ToString)
            End If
        Next

        g_mStreamWriter.BaseStream.Seek(0, IO.SeekOrigin.Begin)
        g_mStreamWriter.BaseStream.SetLength(0)
        g_mStreamWriter.Write(sContentWriter.ToString)

        g_bHasChanged = False
        Return True
    End Function

    Private Sub CopyStream(mInput As IO.Stream, mOutput As IO.Stream, iBufferSize As Integer)
        Dim iBuffer As Byte() = New Byte(iBufferSize - 1) {}
        Dim iBytesRead As Integer = 0

        Do
            iBytesRead = mInput.Read(iBuffer, 0, iBuffer.Length)
            mOutput.Write(iBuffer, 0, iBytesRead)
        Loop While iBytesRead > 0
    End Sub

    Private Function GetSectionFromLine(sLine As String) As String
        If (sLine.TrimStart.StartsWith("["c) AndAlso sLine.TrimEnd.EndsWith("]"c)) Then
            Return sLine.Trim.Trim("["c, "]"c)
        Else
            Return Nothing
        End If
    End Function

    Private Function GetKeyAndValueFromLine(sLine As String) As KeyValuePair(Of String, String)
        sLine = sLine.Trim

        Dim iAssignIndex As Integer = sLine.IndexOf("="c)
        If (iAssignIndex < 0) Then
            Return New KeyValuePair(Of String, String)(Nothing, Nothing)
        End If

        Dim sKey As String = sLine.Substring(0, iAssignIndex)
        Dim sValue As String = sLine.Remove(0, iAssignIndex + 1)

        Return New KeyValuePair(Of String, String)(sKey, sValue)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If (g_mStreamWriter IsNot Nothing) Then
                    Save()
                End If

                ' TODO: dispose managed state (managed objects).
                If (g_mStreamWriter IsNot Nothing) Then
                    g_mStreamWriter.Dispose()
                    g_mStreamWriter = Nothing
                End If

                If (g_mStreamReader IsNot Nothing) Then
                    g_mStreamReader.Dispose()
                    g_mStreamReader = Nothing
                End If

                If (g_mStream IsNot Nothing) Then
                    g_mStream.Dispose()
                    g_mStream = Nothing
                End If
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null. 
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        'GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class