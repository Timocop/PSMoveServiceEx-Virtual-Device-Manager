Imports System.Runtime.InteropServices

Public Class ClassPrecisionSleep
    <DllImport("winmm.dll", EntryPoint:="timeBeginPeriod")>
    Private Shared Sub TimeBeginPeriod(t As Integer)

    End Sub
    <DllImport("winmm.dll", EntryPoint:="timeEndPeriod")>
    Private Shared Sub TimeEndPeriod(t As Integer)
    End Sub

    Public Shared Sub Sleep(ms As Integer)
        Try
            TimeBeginPeriod(1)
            Threading.Thread.Sleep(ms)
        Finally
            TimeEndPeriod(1)
        End Try
    End Sub
End Class
