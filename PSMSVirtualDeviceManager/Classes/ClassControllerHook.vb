Imports System.Numerics
Imports System.Runtime.InteropServices

Public Class ClassControllerHook
    Implements IDisposable

    Class ClassWin32
        <DllImport("xinput1_4.dll")>
        Public Shared Function XInputGetState(
            ByVal dwUserIndex As Integer,
            ByRef pState As XInputState) As Integer
        End Function

        <Flags>
        Public Enum XInputButtons As UShort
            DPAD_UP = &H1
            DPAD_DOWN = &H2
            DPAD_LEFT = &H4
            DPAD_RIGHT = &H8
            START = &H10
            BACK = &H20
            LEFT_THUMB = &H40
            RIGHT_THUMB = &H80
            LEFT_SHOULDER = &H100
            RIGHT_SHOULDER = &H200
            A = &H1000
            B = &H2000
            X = &H4000
            Y = &H8000
        End Enum

        <StructLayout(LayoutKind.Sequential)>
        Public Structure XInputState
            Public PacketNumber As Integer
            Public Gamepad As XInputGamepad
        End Structure

        <StructLayout(LayoutKind.Sequential)>
        Public Structure XInputGamepad
            Public Buttons As XInputButtons
            Public LeftTrigger As Byte
            Public RightTrigger As Byte
            Public ThumbLX As Short
            Public ThumbLY As Short
            Public ThumbRX As Short
            Public ThumbRY As Short
        End Structure

    End Class

    Enum ENUM_PLAYER_INDEX
        PLAYER_0
        PLAYER_1
        PLAYER_2
        PLAYER_3

        __MAX
    End Enum

    Private g_iPlayerIndex As ENUM_PLAYER_INDEX
    Private g_mCurrentState As New ClassWin32.XInputState
    Private g_mPreviousState As New ClassWin32.XInputState

    Public Sub New(playerIndex As ENUM_PLAYER_INDEX)
        g_iPlayerIndex = playerIndex
    End Sub

    Public Sub Update()
        g_mPreviousState = g_mCurrentState
        ClassWin32.XInputGetState(g_iPlayerIndex, g_mCurrentState)
    End Sub

    Public Function AnyButtonDown() As Boolean
        Return (g_mCurrentState.Gamepad.Buttons <> 0)
    End Function

    Public Function GetButtonDown() As ClassWin32.XInputButtons
        Return g_mCurrentState.Gamepad.Buttons
    End Function

    Public Function IsButtonDown(iButton As ClassWin32.XInputButtons) As Boolean
        Return (g_mCurrentState.Gamepad.Buttons And iButton) = iButton
    End Function

    Public Function IsButtonPressed(iButton As ClassWin32.XInputButtons) As Boolean
        Return (g_mCurrentState.Gamepad.Buttons And iButton) = iButton AndAlso
               (g_mPreviousState.Gamepad.Buttons And iButton) <> iButton
    End Function

    Public Function IsButtonReleased(iButton As ClassWin32.XInputButtons) As Boolean
        Return (g_mCurrentState.Gamepad.Buttons And iButton) <> iButton AndAlso
               (g_mPreviousState.Gamepad.Buttons And iButton) = iButton
    End Function

    Public Function GetLeftTrigger() As Single
        Return g_mCurrentState.Gamepad.LeftTrigger / 255.0F
    End Function

    Public Function GetRightTrigger() As Single
        Return g_mCurrentState.Gamepad.RightTrigger / 255.0F
    End Function

    Public Function GetLeftThumbstick() As Vector2
        Dim x As Single = g_mCurrentState.Gamepad.ThumbLX / 32768.0F
        Dim y As Single = g_mCurrentState.Gamepad.ThumbLY / 32768.0F
        Return New Vector2(If(Math.Abs(x) < 0.1F, 0.0F, x), If(Math.Abs(y) < 0.1F, 0.0F, y))
    End Function

    Public Function GetRightThumbstick() As Vector2
        Dim x As Single = g_mCurrentState.Gamepad.ThumbRX / 32768.0F
        Dim y As Single = g_mCurrentState.Gamepad.ThumbRY / 32768.0F
        Return New Vector2(If(Math.Abs(x) < 0.1F, 0.0F, x), If(Math.Abs(y) < 0.1F, 0.0F, y))
    End Function

    Public Function IsConnected() As Boolean
        Try
            Dim state As ClassWin32.XInputState
            Return ClassWin32.XInputGetState(g_iPlayerIndex, state) = 0
        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
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
