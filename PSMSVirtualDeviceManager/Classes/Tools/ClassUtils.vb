Imports System.Numerics
Imports System.Runtime.InteropServices

Public Class ClassUtils
    Class ClassWin32
        Public Const GWL_EXSTYLE As Integer = -20
        Public Const WS_EX_COMPOSITED As Integer = &H2000000
        Public Const EM_GETFIRSTVISIBLELINE As Integer = &HCE
        Public Const EM_LINESCROLL As Integer = &HB6
        Public Const WM_SETREDRAW As Integer = &HB

        <DllImport("user32")>
        Public Shared Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
        End Function

        <DllImport("user32")>
        Public Shared Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
        End Function

        <DllImport("user32.dll")>
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
        End Function

        Public Shared Function GetTopVisibleLine(tb As TextBox) As Integer
            Return SendMessage(tb.Handle, EM_GETFIRSTVISIBLELINE, 0, 0)
        End Function

        Public Shared Sub SetTopVisibleLine(tb As TextBox, lineIndex As Integer)
            Dim currentTopLine As Integer = GetTopVisibleLine(tb)
            SendMessage(tb.Handle, EM_LINESCROLL, 0, lineIndex - currentTopLine)
        End Sub
    End Class

    Public Shared Sub SuspendDrawing(tb As TextBox)
        ClassWin32.SendMessage(tb.Handle, ClassWin32.WM_SETREDRAW, 0, 0)
    End Sub

    Public Shared Sub ResumeDrawing(tb As TextBox)
        ClassWin32.SendMessage(tb.Handle, ClassWin32.WM_SETREDRAW, 1, 0)
        tb.Invalidate() ' 
    End Sub

    Public Shared Sub UpdateTextBoxNoScroll(mTextBox As TextBox, sText As String)
        SuspendDrawing(mTextBox)
        Try
            Dim i As Integer = ClassWin32.GetTopVisibleLine(mTextBox)
            mTextBox.Text = sText
            ClassWin32.SetTopVisibleLine(mTextBox, i)
        Finally
            ResumeDrawing(mTextBox)
        End Try
    End Sub

    Public Shared Function RunWithAdmin(sCmds As String()) As Integer
        Using mProcess As New Process
            mProcess.StartInfo.FileName = Application.ExecutablePath
            mProcess.StartInfo.Arguments = String.Join(" ", sCmds)
            mProcess.StartInfo.WorkingDirectory = Application.StartupPath
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()
            mProcess.WaitForExit()

            Return mProcess.ExitCode
        End Using
    End Function

    Public Shared Sub RunAsSystem(sCmds As String())
        Dim sTaskIdentifier As String = Guid.NewGuid().ToString
        Dim sTaskExecutable As String = "schtasks.exe"
        Dim sProcessName As String = IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath)

        Dim iProcessIds As New List(Of Integer)


        Dim sTaskCreateCmd As String = String.Format(
            "/create /tn ""{0}"" /tr """"{1}"" {0} {2}"" /sc once /st 00:00 /rl highest /ru SYSTEM",
            sTaskIdentifier, Application.ExecutablePath, String.Join(" ", sCmds)
        )
        Dim sTaskRunCmd As String = String.Format("/run /tn ""{0}""", sTaskIdentifier)
        Dim sTaskDeleteCmd As String = String.Format("/delete /tn ""{0}"" /f", sTaskIdentifier)

        Dim sCommandList As New List(Of String)
        sCommandList.Add(sTaskCreateCmd)
        sCommandList.Add(sTaskRunCmd)
        sCommandList.Add(sTaskDeleteCmd)

        ' Lets grab all already running application process ids so we can find the new application later
        ' This is terribly lazy
        For Each mProcess In Process.GetProcessesByName(sProcessName)
            iProcessIds.Add(mProcess.Id)
        Next

        For Each sTaskComamnd As String In sCommandList
            Using mProcess As New Process
                mProcess.StartInfo.FileName = sTaskExecutable
                mProcess.StartInfo.Arguments = sTaskComamnd
                mProcess.StartInfo.CreateNoWindow = True
                mProcess.StartInfo.UseShellExecute = False

                mProcess.Start()
                mProcess.WaitForExit()

                If (mProcess.ExitCode <> 0) Then
                    Throw New ArgumentException("Task failed with error code: " & mProcess.ExitCode)
                End If
            End Using
        Next

        ' Lets wait all new spawned processes
        For Each mProcess In Process.GetProcessesByName(sProcessName)
            If (iProcessIds.IndexOf(mProcess.Id) > -1) Then
                Continue For
            End If

            mProcess.WaitForExit()
        Next
    End Sub

    Public Shared Property m_InvokeControl As Control = Nothing
    Public Shared Sub SyncInvoke(mFunction As [Delegate])
        m_InvokeControl.Invoke(mFunction)
    End Sub

    Public Shared Function SyncInvokeEx(Of T)(mFunction As [Delegate]) As T
        Return CType(m_InvokeControl.Invoke(mFunction), T)
    End Function

    Public Shared Sub AsyncInvoke(mFunction As [Delegate])
        m_InvokeControl.BeginInvoke(
            Sub()
                Try
                    mFunction.DynamicInvoke()
                Catch ex As Exception
                End Try
            End Sub)
    End Sub

    Public Shared Function FormatJsonOutput(ByVal sContent As String) As String
        Dim mText = New Text.StringBuilder()
        Dim bEscape As Boolean = False
        Dim bQuotes As Boolean = False
        Dim iInt As Integer = 0

        For Each iChar As Char In sContent
            If (bEscape) Then
                bEscape = False
                mText.Append(iChar)
            Else
                If (iChar = "\"c) Then
                    bEscape = True
                    mText.Append(iChar)
                ElseIf (iChar = """"c) Then
                    bQuotes = Not bQuotes
                    mText.Append(iChar)
                ElseIf (Not bQuotes) Then
                    If (iChar = ","c) Then
                        mText.Append(iChar)
                        mText.Append(vbCrLf)
                        mText.Append(CChar(vbTab), iInt)
                    ElseIf iChar = "["c OrElse iChar = "{"c Then
                        iInt += 1

                        mText.Append(iChar)
                        mText.Append(vbCrLf)
                        mText.Append(CChar(vbTab), iInt)
                    ElseIf (iChar = "]"c) OrElse (iChar = "}"c) Then
                        iInt -= 1

                        mText.Append(vbCrLf)
                        mText.Append(CChar(vbTab), iInt)
                        mText.Append(iChar)
                    ElseIf (iChar = ":"c) Then
                        mText.Append(iChar)
                        mText.Append(vbTab)
                    ElseIf (Not Char.IsWhiteSpace(iChar)) Then
                        mText.Append(iChar)
                    End If
                Else
                    mText.Append(iChar)
                End If
            End If
        Next

        Return mText.ToString()
    End Function

    Public Shared Function CreateChecksum(sText As String, iSalt As Integer) As Integer
        Dim iSum As Integer = iSalt

        For i = 0 To sText.Length - 1
            iSum = iSum * 101 + AscW(sText(i))
        Next

        Return iSum
    End Function

    Public Shared Sub SetControlDoubleBuffering(cControl As Control, value As Boolean)
        Dim controlProperty As Reflection.PropertyInfo = GetType(Control).GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
        controlProperty.SetValue(cControl, value, Nothing)
    End Sub

    Public Shared Sub SetControlDoubleBufferingChilds(cControl As Control, value As Boolean)
        SetControlDoubleBuffering(cControl, value)

        For Each c As Control In cControl.Controls
            SetControlDoubleBuffering(c, value)

            SetControlDoubleBufferingChilds(c, value)
        Next
    End Sub

    Public Shared Sub SetControlComposited(cControl As Control, value As Boolean)
        If (Environment.OSVersion.Version.Major > 5) Then
            Dim style As Integer = ClassWin32.GetWindowLong(cControl.Handle, ClassWin32.GWL_EXSTYLE)

            If (value) Then
                style = style Or ClassWin32.WS_EX_COMPOSITED
            Else
                style = style And Not ClassWin32.WS_EX_COMPOSITED
            End If

            ClassWin32.SetWindowLong(cControl.Handle, ClassWin32.GWL_EXSTYLE, style)
        End If
    End Sub

    Public Shared Sub SetControlCompositedChilds(cControl As Control, value As Boolean)
        If (Environment.OSVersion.Version.Major > 5) Then
            SetControlComposited(cControl, value)

            For Each c As Control In cControl.Controls
                SetControlCompositedChilds(c, value)
            Next
        End If
    End Sub

    ' Class for non-blocking file reading.
    Class ClassSafeFileRead
        Class ClassWin32
            ' Constants for CreateFile
            Public Const GENERIC_READ As UInteger = &H80000000UI
            Public Const OPEN_EXISTING As UInteger = 3
            Public Const FILE_SHARE_READ As UInteger = &H1
            Public Const FILE_SHARE_WRITE As UInteger = &H2

            ' Error codes
            Public Shared ReadOnly INVALID_HANDLE_VALUE As IntPtr = New IntPtr(-1)

            <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
            Public Shared Function CreateFile(
                    lpFileName As String,
                    dwDesiredAccess As UInteger,
                    dwShareMode As UInteger,
                    lpSecurityAttributes As IntPtr,
                    dwCreationDisposition As UInteger,
                    dwFlagsAndAttributes As UInteger,
                    hTemplateFile As IntPtr
                ) As IntPtr
            End Function

            <DllImport("kernel32.dll", SetLastError:=True)>
            Public Shared Function ReadFile(
                    hFile As IntPtr,
                    lpBuffer As Byte(),
                    nNumberOfBytesToRead As UInteger,
                    ByRef lpNumberOfBytesRead As UInteger,
                    lpOverlapped As IntPtr
                ) As Boolean
            End Function

            <DllImport("kernel32.dll", SetLastError:=True)>
            Public Shared Function CloseHandle(hObject As IntPtr) As Boolean
            End Function
        End Class

        Public Shared Function ReadFile(sFile As String) As String
            Return ReadFile(sFile, Text.Encoding.UTF8)
        End Function

        Public Shared Function ReadFile(sFile As String, mEncoding As Text.Encoding) As String
            Dim mFile As IntPtr = ClassWin32.CreateFile(
                sFile,
                ClassWin32.GENERIC_READ,
                ClassWin32.FILE_SHARE_READ Or ClassWin32.FILE_SHARE_WRITE,
                IntPtr.Zero,
                ClassWin32.OPEN_EXISTING,
                0,
                IntPtr.Zero
            )

            If (mFile = ClassWin32.INVALID_HANDLE_VALUE) Then
                Throw New ArgumentException(String.Format("Unable to open file '{0}'", sFile))
            End If

            Try
                Dim iBufferSize As Integer = 4096
                Dim iBuffer(iBufferSize - 1) As Byte
                Dim iBytesRead As UInteger

                Using mMemStream As New IO.MemoryStream
                    mMemStream.Seek(0, IO.SeekOrigin.Begin)

                    While True
                        Dim bSuccess As Boolean = ClassWin32.ReadFile(mFile, iBuffer, CType(iBuffer.Length, UInteger), iBytesRead, IntPtr.Zero)
                        If (Not bSuccess) Then
                            Throw New ArgumentException(String.Format("Unable to read file '{0}'", sFile))
                        End If

                        If (iBytesRead = 0) Then
                            Exit While
                        End If

                        mMemStream.Write(iBuffer, 0, CType(iBytesRead, Integer))
                    End While

                    ' Detect encoding
                    Using mMemReader As New IO.StreamReader(mMemStream, mEncoding)
                        mMemStream.Seek(0, IO.SeekOrigin.Begin)

                        Return mMemReader.ReadToEnd()
                    End Using
                End Using
            Finally
                ClassWin32.CloseHandle(mFile)
            End Try
        End Function
    End Class
End Class
