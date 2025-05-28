Public Class FormButtonInput
    Private g_mKeyboardHook As ClassKeyboardHook
    Private g_mControllerHook(ClassControllerHook.ENUM_PLAYER_INDEX.__MAX - 1) As ClassControllerHook

    Private g_mThreadButtonDetection As Threading.Thread = Nothing
    Private g_mThreadLock As New Object

    Enum ENUM_DEVICE_TYPE
        KEYBOARD
        CONTROLLER
    End Enum
    Private g_iDeviceType As ENUM_DEVICE_TYPE = ENUM_DEVICE_TYPE.KEYBOARD

    Private g_iKeyboardKeys As New HashSet(Of Keys)
    Private g_iControllerKeys As ClassControllerHook.ClassWin32.XInputButtons = 0
    Private g_iControllerIndex As ClassControllerHook.ENUM_PLAYER_INDEX = CType(-1, ClassControllerHook.ENUM_PLAYER_INDEX)

    Public Sub New(_DeviceType As ENUM_DEVICE_TYPE)
        g_iDeviceType = _DeviceType

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Select Case (_DeviceType)
            Case ENUM_DEVICE_TYPE.KEYBOARD
                g_mKeyboardHook = New ClassKeyboardHook()

            Case ENUM_DEVICE_TYPE.CONTROLLER
                For i = 0 To ClassControllerHook.ENUM_PLAYER_INDEX.__MAX - 1
                    g_mControllerHook(i) = New ClassControllerHook(CType(i, ClassControllerHook.ENUM_PLAYER_INDEX))
                Next

        End Select
    End Sub

    Private Sub FormButtonInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        g_mThreadButtonDetection = New Threading.Thread(AddressOf ThreadButtonDetection)
        g_mThreadButtonDetection.IsBackground = True
        g_mThreadButtonDetection.Start()
    End Sub

    Private Sub ThreadButtonDetection()
        Dim bHasPressed As Boolean = False

        Try
            While True
                Try
                    Select Case (m_DeviceType)
                        Case ENUM_DEVICE_TYPE.KEYBOARD
                            g_mKeyboardHook.Update()

                            If (g_mKeyboardHook.AnyButtonDown()) Then
                                bHasPressed = True

                                SyncLock g_mThreadLock
                                    For Each iKey In g_mKeyboardHook.GetButtonDown()
                                        g_iKeyboardKeys.Add(iKey)
                                    Next
                                End SyncLock
                            Else
                                If (bHasPressed) Then
                                    ClassUtils.AsyncInvoke(Sub()
                                                               Me.DialogResult = DialogResult.OK
                                                               Me.Close()
                                                           End Sub)
                                    Return
                                End If
                            End If

                        Case ENUM_DEVICE_TYPE.CONTROLLER
                            For i = 0 To ClassControllerHook.ENUM_PLAYER_INDEX.__MAX - 1
                                If (Not g_mControllerHook(i).IsConnected) Then
                                    Continue For
                                End If

                                g_mControllerHook(i).Update()

                                If (g_mControllerHook(i).AnyButtonDown()) Then
                                    bHasPressed = True

                                    SyncLock g_mThreadLock
                                        g_iControllerIndex = CType(i, ClassControllerHook.ENUM_PLAYER_INDEX)
                                        g_iControllerKeys = (g_iControllerKeys Or g_mControllerHook(i).GetButtonDown)
                                    End SyncLock
                                Else
                                    If (bHasPressed) Then
                                        ClassUtils.AsyncInvoke(Sub()
                                                                   Me.DialogResult = DialogResult.OK
                                                                   Me.Close()
                                                               End Sub)
                                        Return
                                    End If
                                End If
                            Next

                    End Select
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLog(ex)
                End Try

                Threading.Thread.Sleep(1)
            End While
        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try
    End Sub

    ReadOnly Property m_DeviceType As ENUM_DEVICE_TYPE
        Get
            SyncLock g_mThreadLock
                Return g_iDeviceType
            End SyncLock
        End Get
    End Property

    ReadOnly Property m_KeyboardKeys As Keys()
        Get
            SyncLock g_mThreadLock
                Return g_iKeyboardKeys.ToArray
            End SyncLock
        End Get
    End Property

    ReadOnly Property m_ControllerIndex As ClassControllerHook.ENUM_PLAYER_INDEX
        Get
            SyncLock g_mThreadLock
                Return g_iControllerIndex
            End SyncLock
        End Get
    End Property

    ReadOnly Property m_ControllerKeys As ClassControllerHook.ClassWin32.XInputButtons
        Get
            SyncLock g_mThreadLock
                Return g_iControllerKeys
            End SyncLock
        End Get
    End Property

    Private Sub CleanUp()
        If (g_mThreadButtonDetection IsNot Nothing AndAlso g_mThreadButtonDetection.IsAlive) Then
            g_mThreadButtonDetection.Abort()
            g_mThreadButtonDetection.Join()
            g_mThreadButtonDetection = Nothing
        End If

        If (g_mKeyboardHook IsNot Nothing) Then
            g_mKeyboardHook.Dispose()
            g_mKeyboardHook = Nothing
        End If

        For i = 0 To ClassControllerHook.ENUM_PLAYER_INDEX.__MAX - 1
            If (g_mControllerHook(i) IsNot Nothing) Then
                g_mControllerHook(i).Dispose()
                g_mControllerHook(i) = Nothing
            End If
        Next
    End Sub
End Class