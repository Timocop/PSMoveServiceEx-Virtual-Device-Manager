Imports System.Runtime.InteropServices

Public Class ClassKeyboardHook
    Implements IDisposable

    Public Class ClassWin32
        Public Const WH_KEYBOARD As Integer = 2
        Public Const WH_MOUSE As Integer = 7
        Public Const WH_KEYBOARD_LL As Integer = 13
        Public Const WH_MOUSE_LL As Integer = 14
        Public Const HC_ACTION As Integer = 0
        Public Const WM_KEYDOWN = &H100
        Public Const WM_KEYUP = &H101
        Public Const WM_SYSKEYDOWN = &H104
        Public Const WM_SYSKEYUP = &H105

        Public Delegate Function HookProc(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr

        <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function SetWindowsHookEx(
            ByVal idHook As Integer,
            ByVal lpfn As HookProc,
            ByVal hMod As IntPtr,
            ByVal dwThreadId As UInteger) As IntPtr
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function UnhookWindowsHookEx(ByVal hhk As IntPtr) As Boolean
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function CallNextHookEx(
            ByVal hhk As IntPtr,
            ByVal nCode As Integer,
            ByVal wParam As IntPtr,
            ByVal lParam As IntPtr) As IntPtr
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function GetModuleHandle(ByVal lpModuleName As String) As IntPtr
        End Function

        <StructLayout(LayoutKind.Sequential)>
        Public Structure KBDLLHOOKSTRUCT
            Public vkCode As Integer
            Public scanCode As Integer
            Public flags As Integer
            Public time As Integer
            Public dwExtraInfo As IntPtr
        End Structure
    End Class

    Private g_mHookHandle As IntPtr = IntPtr.Zero
    Private g_mHookProc As ClassWin32.HookProc = Nothing

    Public Event KeyPressedDown(iKey As Keys)
    Public Event KeyPressedUp(iKey As Keys)

    Public Sub InstallHook()
        If (g_mHookHandle <> IntPtr.Zero) Then
            Return
        End If

        g_mHookProc = New ClassWin32.HookProc(AddressOf LowLevelKeyboardProc)
        g_mHookHandle = ClassWin32.SetWindowsHookEx(
            ClassWin32.WH_KEYBOARD_LL,
            g_mHookProc,
            ClassWin32.GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName),
            0)

        If (g_mHookHandle = IntPtr.Zero) Then
            Dim iErrorCode As Integer = Marshal.GetLastWin32Error()
            Throw New ArgumentException(String.Format("Failed to install hook. Error: {0}", iErrorCode))
        End If
    End Sub

    Public Sub UninstallHook()
        If (g_mHookHandle <> IntPtr.Zero) Then
            ClassWin32.UnhookWindowsHookEx(g_mHookHandle)
            g_mHookHandle = IntPtr.Zero
        End If
    End Sub

    Private Function LowLevelKeyboardProc(ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        If (nCode >= 0) Then
            Dim mInfo As ClassWin32.KBDLLHOOKSTRUCT = CType(Marshal.PtrToStructure(lParam, GetType(ClassWin32.KBDLLHOOKSTRUCT)), ClassWin32.KBDLLHOOKSTRUCT)

            Select Case (CInt(wParam))
                Case ClassWin32.WM_KEYDOWN, ClassWin32.WM_SYSKEYDOWN
                    RaiseEvent KeyPressedDown(CType(mInfo.vkCode, Keys))

                Case ClassWin32.WM_KEYUP, ClassWin32.WM_SYSKEYUP
                    RaiseEvent KeyPressedUp(CType(mInfo.vkCode, Keys))
            End Select

        End If

        Return ClassWin32.CallNextHookEx(g_mHookHandle, nCode, wParam, lParam)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                UninstallHook()
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
