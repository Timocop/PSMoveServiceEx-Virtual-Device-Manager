Imports System.Numerics
Imports System.Runtime.InteropServices

Public Class ClassUtils
    Class ClassWin32
        Friend Shared ReadOnly GWL_EXSTYLE As Integer = -20
        Friend Shared ReadOnly WS_EX_COMPOSITED As Integer = &H2000000

        <DllImport("user32")>
        Friend Shared Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
        End Function

        <DllImport("user32")>
        Friend Shared Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
        End Function
    End Class


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

    Public Shared Sub SyncInvoke(mControl As Control, mFunction As [Delegate])
        mControl.Invoke(mFunction)
    End Sub

    Public Shared Function SyncInvokeEx(Of T)(mControl As Control, mFunction As [Delegate]) As T
        Return CType(mControl.Invoke(mFunction), T)
    End Function

    Public Shared Sub AsyncInvoke(mControl As Control, mFunction As [Delegate])
        mControl.BeginInvoke(Sub()
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
End Class
