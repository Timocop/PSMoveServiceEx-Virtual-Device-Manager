Public Class ClassUtils
    Public Shared Function FormatOutput(ByVal sContent As String) As String
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
End Class
