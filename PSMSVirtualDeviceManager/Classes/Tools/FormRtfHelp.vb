Public Class FormRtfHelp
    Private Sub RichTextBox_Help_MouseWheel(sender As Object, e As MouseEventArgs) Handles RichTextBox_Help.MouseWheel
        If ((ModifierKeys And Keys.Control) <> 0) Then
            DirectCast(e, HandledMouseEventArgs).Handled = True
        End If
    End Sub
End Class