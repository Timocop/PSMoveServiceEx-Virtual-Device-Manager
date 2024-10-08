﻿Imports System.Numerics
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
