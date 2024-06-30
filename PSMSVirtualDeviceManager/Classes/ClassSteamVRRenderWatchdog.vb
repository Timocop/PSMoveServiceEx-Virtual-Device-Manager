Imports System.Runtime.InteropServices

Public Class ClassSteamVRRenderWatchdog
    Implements IDisposable

    Class Win32
        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function FindWindowEx(hwndParent As IntPtr, hwndChildAfter As IntPtr, lpszClass As String, lpszWindow As String) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function SendMessage(hWnd As IntPtr, Msg As UInteger, wParam As IntPtr, lParam As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function PostMessage(hWnd As IntPtr, Msg As UInteger, wParam As IntPtr, lParam As IntPtr) As Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function IsWindowVisible(hWnd As IntPtr) As Boolean
        End Function

        Public Const WM_LBUTTONDOWN As UInteger = &H201
        Public Const WM_LBUTTONUP As UInteger = &H202
    End Class

    Private g_mUCVirtualMotionTracker As UCVirtualMotionTracker

    Private g_bInit As Boolean = False
    Private g_mActivateRenderWindowThread As Threading.Thread = Nothing

    Public Sub New(_UCVirtualMotionTracker As UCVirtualMotionTracker)
        g_mUCVirtualMotionTracker = _UCVirtualMotionTracker
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

        g_mActivateRenderWindowThread = New Threading.Thread(AddressOf ActivateRenderWindowThread)
        g_mActivateRenderWindowThread.IsBackground = True
        g_mActivateRenderWindowThread.Start()
    End Sub

    Private Sub ActivateRenderWindowThread()
        Try
            While True
                Try
                    If (g_mUCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_RenderWindowFix) Then
                        Dim mVRCompositor = Process.GetProcessesByName("vrcompositor")

                        If (mVRCompositor IsNot Nothing AndAlso mVRCompositor.Count > 0) Then
                            Dim mHeadsetWnd As IntPtr = Win32.FindWindow("Headset Window", "Headset Window")

                            If (mHeadsetWnd <> IntPtr.Zero AndAlso Win32.IsWindowVisible(mHeadsetWnd)) Then
                                ' The proxy needs to be gone, thats how it causes the render glitch.
                                ' Activate the proxy again by simulating a click onto the "Headset Window".
                                Dim mProxyWnd As IntPtr = Win32.FindWindow("D3DProxyWindow", "D3DProxyWindow")

                                If ((mProxyWnd = IntPtr.Zero) OrElse (mProxyWnd <> IntPtr.Zero AndAlso Not Win32.IsWindowVisible(mProxyWnd))) Then
                                    ' Bring the window to the foreground
                                    'SetForegroundWindow(hWnd)

                                    Win32.PostMessage(mHeadsetWnd, Win32.WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero)
                                    Win32.PostMessage(mHeadsetWnd, Win32.WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero)
                                End If
                            End If
                        End If
                    End If

                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ' Whatever happens 
                End Try

                Threading.Thread.Sleep(1000)
            End While
        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
        End Try
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If (g_mActivateRenderWindowThread IsNot Nothing AndAlso g_mActivateRenderWindowThread.IsAlive) Then
                    g_mActivateRenderWindowThread.Abort()
                    g_mActivateRenderWindowThread.Join()
                    g_mActivateRenderWindowThread = Nothing
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
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
