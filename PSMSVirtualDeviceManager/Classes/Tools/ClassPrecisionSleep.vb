Imports System.Runtime.InteropServices

Public Class ClassPrecisionSleep
    Class ClassWin32
        Public Const CREATE_WAITABLE_TIMER_HIGH_RESOLUTION = &H2
        Public Const TIMER_ALL_ACCESS = &H1F0003
        Public Const INFINTIE = &HFFFFFFFF

        <StructLayout(LayoutKind.Explicit, Size:=8)>
        Public Structure LARGE_INTEGER
            <FieldOffset(0)>
            Public QuadPart As Long
            <FieldOffset(0)>
            Public LowPart As UInteger
            <FieldOffset(4)>
            Public HighPart As Integer
        End Structure

        <DllImport("winmm.dll", EntryPoint:="timeBeginPeriod")>
        Public Shared Sub TimeBeginPeriod(t As Integer)
        End Sub

        <DllImport("winmm.dll", EntryPoint:="timeEndPeriod")>
        Public Shared Sub TimeEndPeriod(t As Integer)
        End Sub

        <DllImport("kernel32.dll")>
        Public Shared Function CreateWaitableTimerEx(lpTimerAttributes As IntPtr, lpTimerName As IntPtr, dwFlags As Integer, dwDesiredAccess As Integer) As IntPtr
        End Function

        <DllImport("kernel32.dll")>
        Public Shared Function SetWaitableTimer(hTimer As IntPtr, ByRef lpDueTime As LARGE_INTEGER, lPeriod As Integer, pfnCompletionRoutine As IntPtr, lpArgToCompletionRoutine As IntPtr, fResume As Boolean) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function WaitForSingleObject(handle As IntPtr, milliseconds As UInteger) As Integer
        End Function

        <DllImport("kernel32", SetLastError:=True)>
        Public Shared Function CloseHandle(handle As IntPtr) As Boolean
        End Function
    End Class

    Public Shared Sub Sleep(ms As Integer)
        Sleep(ms, True)
    End Sub

    Public Shared Sub Sleep(ms As Integer, bUseHighPrecisionTimer As Boolean)
        Static g_bIsSupported As Boolean = True

        If (bUseHighPrecisionTimer AndAlso g_bIsSupported) Then
            Dim hTimer = ClassWin32.CreateWaitableTimerEx(IntPtr.Zero,
                                                          IntPtr.Zero,
                                                          ClassWin32.CREATE_WAITABLE_TIMER_HIGH_RESOLUTION,
                                                          ClassWin32.TIMER_ALL_ACCESS)
            If (hTimer <> IntPtr.Zero) Then
                Try
                    Dim mDuration As New ClassWin32.LARGE_INTEGER
                    mDuration.QuadPart = -((ms * 1000) * 10)

                    If (ClassWin32.SetWaitableTimer(hTimer, mDuration, 0, IntPtr.Zero, IntPtr.Zero, False)) Then
                        ClassWin32.WaitForSingleObject(hTimer, ClassWin32.INFINTIE)
                        Return
                    End If
                Finally
                    ClassWin32.CloseHandle(hTimer)
                End Try
            Else
                ' CREATE_WAITABLE_TIMER_HIGH_RESOLUTION is not supported
                g_bIsSupported = False
            End If
        End If

        Try
            ClassWin32.TimeBeginPeriod(1)
            Threading.Thread.Sleep(ms)
        Finally
            ClassWin32.TimeEndPeriod(1)
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
