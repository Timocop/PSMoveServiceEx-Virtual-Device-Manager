Imports System.Runtime.InteropServices

Public Class ClassTreeViewBuffered
    Inherits TreeView

    Class ClassNatives
        Public Const TVM_SETEXTENDEDSTYLE As Integer = &H1100 + 44
        Public Const TVS_EX_DOUBLEBUFFER As Integer = &H4

        <DllImport("user32.dll")>
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        End Function
    End Class

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        ClassNatives.SendMessage(Me.Handle, ClassNatives.TVM_SETEXTENDEDSTYLE, New IntPtr(ClassNatives.TVS_EX_DOUBLEBUFFER), New IntPtr(ClassNatives.TVS_EX_DOUBLEBUFFER))
        MyBase.OnHandleCreated(e)
    End Sub
End Class
