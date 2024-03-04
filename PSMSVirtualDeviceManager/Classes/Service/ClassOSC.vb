Imports System.Net
Imports System.Threading
Imports Rug.Osc

Public Class ClassOSC
    Implements IDisposable

    Public g_mOnBundle As Action(Of OscBundle) = Nothing
    Public g_mOnMessage As Action(Of OscMessage) = Nothing
    Public g_iPacketCounter As UInteger = 0

    Private g_mOscReceiver As OscReceiver = Nothing
    Private g_mOscSender As OscSender = Nothing
    Private g_mProcessThread As Thread = Nothing

    Public g_iPort As Integer

    Public Sub New(sAddress As String, iReceiverPort As Integer, iSenderPort As Integer, mOnBundle As Action(Of OscBundle), mOnMessage As Action(Of OscMessage))
        g_iPort = iReceiverPort

        g_mOnBundle = mOnBundle
        g_mOnMessage = mOnMessage

        Dim mAddress As IPAddress = IPAddress.Parse(sAddress)
        g_mOscSender = New OscSender(mAddress, 0, iSenderPort)
        g_mOscSender.Connect()

        g_mOscReceiver = New OscReceiver(iReceiverPort)
        g_mOscReceiver.Connect()

        g_mProcessThread = New Thread(AddressOf OnReceiveThread)
        g_mProcessThread.IsBackground = True
        g_mProcessThread.Start()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If (g_mProcessThread IsNot Nothing AndAlso g_mProcessThread.IsAlive) Then
            g_mProcessThread.Abort()
            g_mProcessThread.Join()
            g_mProcessThread = Nothing
        End If

        If (g_mOscReceiver IsNot Nothing) Then
            g_mOscReceiver.Close()
            g_mOscReceiver.Dispose()
            g_mOscReceiver = Nothing
        End If

        If (g_mOscSender IsNot Nothing) Then
            g_mOscSender.Close()
            g_mOscSender.Dispose()
            g_mOscSender = Nothing
        End If
    End Sub

    Public Sub Send(packet As OscPacket)
        g_mOscSender.Send(packet)
    End Sub

    Private Sub OnReceiveThread()
        Try
            While (g_mOscReceiver.State <> OscSocketState.Closed)
                If (g_mOscReceiver.State = OscSocketState.Connected) Then
                    Dim packet As OscPacket = g_mOscReceiver.Receive()

                    ProcessPacket(packet)
                Else
                    Threading.Thread.Sleep(100)
                End If
            End While
        Catch ex As ThreadAbortException
            Throw
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try
    End Sub

    Private Sub ProcessPacket(mPacket As OscPacket)
        g_iPacketCounter += CUInt(1)

        Dim mOscBundle As OscBundle = TryCast(mPacket, OscBundle)
        If (mOscBundle IsNot Nothing) Then
            Try
                If (g_mOnBundle IsNot Nothing) Then
                    g_mOnBundle(mOscBundle)
                End If

                Return
            Catch ex As ThreadAbortException
                Throw
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLog(ex)
            End Try

            Return
        End If

        Dim mOscMessage As OscMessage = TryCast(mPacket, OscMessage)
        If (mOscMessage IsNot Nothing) Then
            Try
                If (g_mOnMessage IsNot Nothing) Then
                    g_mOnMessage(mOscMessage)
                End If
            Catch ex As ThreadAbortException
                Throw
            Catch ex3 As Exception
            End Try

            Return
        End If
    End Sub
End Class
