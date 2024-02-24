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

    Class ClassFrameTimed
        Dim g_mTimer As New Stopwatch
        Dim g_iFramerate As Integer = -1

        Public Sub New()
            Me.New(-1)
        End Sub

        Public Sub New(_iFramerate As Integer)
            g_iFramerate = _iFramerate

            g_mTimer.Start()
        End Sub

        Public Property m_Framerate As Integer
            Get
                Return g_iFramerate
            End Get
            Set(value As Integer)
                g_iFramerate = value
            End Set
        End Property

        Public Function CanExecute() As Boolean
            If (g_iFramerate < 1) Then
                Return True
            End If

            If (g_mTimer.ElapsedMilliseconds > ((1000.0F / g_iFramerate) - 1.0F)) Then
                g_mTimer.Restart()
                Return True
            End If

            Return False
        End Function
    End Class
End Class
