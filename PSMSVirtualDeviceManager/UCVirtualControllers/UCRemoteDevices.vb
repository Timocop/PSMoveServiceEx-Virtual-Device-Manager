﻿Imports System.Net
Imports System.Net.Sockets
Imports System.Numerics
Imports System.Text

Public Class UCRemoteDevices
    Shared _ThreadLock As New Object

    Public g_mClassStrackerSocket As ClassTrackerSocket

    Private g_mRemoveDevices As New Dictionary(Of String, UCRemoteDeviceItem)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        g_mClassStrackerSocket = New ClassTrackerSocket(Me)

        AddHandler g_mClassStrackerSocket.OnTrackerConnected, AddressOf OnTrackerConnected
    End Sub

    Private Sub OnTrackerConnected(mTracker As ClassTrackerSocket.ClassTracker)
        SyncLock _ThreadLock
            Dim sTrackerName As String = mTracker.m_Name

            If (g_mRemoveDevices.ContainsKey(sTrackerName)) Then
                Return
            End If

            Me.Invoke(Sub()
                          Dim mNewDevice As New UCRemoteDeviceItem(sTrackerName, Me)
                          g_mRemoveDevices(sTrackerName) = mNewDevice

                          mNewDevice.Parent = Panel_RemoteDevices
                          mNewDevice.Dock = DockStyle.Top
                          mNewDevice.Show()
                      End Sub)
        End SyncLock
    End Sub

    Private Sub Button_StartSocket_Click(sender As Object, e As EventArgs) Handles Button_StartSocket.Click
        g_mClassStrackerSocket.Init()
    End Sub

    Private Sub CleanUp()
        For Each mItem In g_mRemoveDevices
            g_mRemoveDevices(mItem.Key).Dispose()
        Next
        g_mRemoveDevices.Clear()

        If (g_mClassStrackerSocket IsNot Nothing) Then
            RemoveHandler g_mClassStrackerSocket.OnTrackerConnected, AddressOf OnTrackerConnected

            g_mClassStrackerSocket.Dispose()
            g_mClassStrackerSocket = Nothing
        End If
    End Sub

    Private Sub Timer_SocketCheck_Tick(sender As Object, e As EventArgs) Handles Timer_SocketCheck.Tick
        Try
            Timer_SocketCheck.Stop()

            Button_StartSocket.Enabled = (Not g_mClassStrackerSocket.IsListening)

            Timer_SocketCheck.Start()
        Catch ex As Exception
        End Try
    End Sub

    Class ClassTrackerSocket
        Implements IDisposable

        Public g_mUCRemoteDevices As UCRemoteDevices

        Private g_mSocket As Socket = Nothing
        Private g_mBuffer As Byte() = New Byte(512) {}

        Private g_mLastKeepup As New Stopwatch
        Private g_mTrackers As New Dictionary(Of String, ClassTracker)

        Shared HANDSHAKE_BUFFER As Byte() = New Byte(64) {}
        Shared KEEPUP_BUFFER As Byte() = New Byte(64) {}

        Private g_mSocketListenerThread As Threading.Thread = Nothing
        Private g_mPipeThread As Threading.Thread = Nothing

        Public Event OnTrackerConnected(mTracker As ClassTracker)
        Public Event OnTrackerGyro(mTracker As ClassTracker, iX As Single, iY As Single, iZ As Single)
        Public Event OnTrackerAccel(mTracker As ClassTracker, iX As Single, iY As Single, iZ As Single)
        Public Event OnTrackerRotation(mTracker As ClassTracker, iX As Single, iY As Single, iZ As Single, iW As Single)
        Public Event OnTrackerPacket(mTracker As ClassTracker)
        Public Event OnTrackerBattery(mTracker As ClassTracker, iBatteryPercent As Integer)

        Shared Sub New()
            HANDSHAKE_BUFFER(0) = 3

            Dim iHandshakeMsg As Byte() = Encoding.ASCII.GetBytes("Hey OVR =D 5")
            Array.Copy(iHandshakeMsg, 0, HANDSHAKE_BUFFER, 1, iHandshakeMsg.Length)

            KEEPUP_BUFFER(3) = 1
        End Sub

        Public Sub New(_UCRemoteDevices As UCRemoteDevices)
            g_mUCRemoteDevices = _UCRemoteDevices

            g_mSocket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            g_mBuffer = New Byte(512) {}

            Try
                g_mSocket.Bind(New IPEndPoint(IPAddress.Any, 6970))
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            g_mLastKeepup.Start()
        End Sub

        ReadOnly Property m_Trackers As Dictionary(Of String, ClassTracker)
            Get
                Return g_mTrackers
            End Get
        End Property

        Public Sub Init()
            StartSocketListening()
        End Sub

        Private Sub StartSocketListening()
            If (g_mSocketListenerThread IsNot Nothing AndAlso g_mSocketListenerThread.IsAlive) Then
                Return
            End If

            g_mSocketListenerThread = New Threading.Thread(AddressOf ThreadSocketListener)
            g_mSocketListenerThread.IsBackground = True
            g_mSocketListenerThread.Start()
        End Sub

        Public Function IsListening() As Boolean
            If (g_mSocketListenerThread IsNot Nothing AndAlso g_mSocketListenerThread.IsAlive) Then
                Return True
            End If

            Return False
        End Function

        Private Sub ThreadSocketListener()
            Try
                If (g_mSocket Is Nothing) Then
                    Return
                End If

                While True
                    Try
                        Dim mEndPoint As New IPEndPoint(IPAddress.Any, 6970)
                        Dim __EndPoint As EndPoint = mEndPoint
                        g_mSocket.ReceiveFrom(g_mBuffer, SocketFlags.None, __EndPoint)
                        mEndPoint = CType(__EndPoint, IPEndPoint)

                        Dim mTracker As ClassTracker = Nothing

                        SyncLock _ThreadLock
                            If (g_mTrackers.ContainsKey(mEndPoint.Address.ToString)) Then
                                mTracker = g_mTrackers(mEndPoint.Address.ToString)
                            End If
                        End SyncLock

                        If (mTracker IsNot Nothing) Then
                            mTracker.ReceivedPacket()

                            RaiseEvent OnTrackerPacket(mTracker)
                        End If

                        Using mMemStream As New IO.MemoryStream(g_mBuffer)
                            Using mBinReader As New IO.BinaryReader(mMemStream)
                                Dim iPacketId As Integer = BR_ReadInt32(mBinReader)

                                Select Case (iPacketId)
                                    Case 0 ' Nothing
                                    Case 3 ' Inital Connection
                                        SetupNewTracker(mBinReader, mEndPoint)

                                    Case 1, 16 ' PACKET_ROTATION, PACKET_ROTATION_2
                                        If (mTracker Is Nothing) Then
                                            Exit Select
                                        End If

                                        If (iPacketId <> 1) Then
                                            Exit Select
                                        End If

                                        mBinReader.ReadInt64()

                                        Dim iX As Single = BR_ReadSingle(mBinReader)
                                        Dim iY As Single = BR_ReadSingle(mBinReader)
                                        Dim iZ As Single = BR_ReadSingle(mBinReader)
                                        Dim iW As Single = BR_ReadSingle(mBinReader)
                                        Dim mQuat As New Quaternion(iX, iY, iZ, iW)

                                        RaiseEvent OnTrackerRotation(mTracker, iX, iY, iZ, iW)

                                        SyncLock _ThreadLock
                                            mTracker.m_Orentation = mQuat
                                        End SyncLock

                                    Case 2 ' GYRO
                                        If (mTracker Is Nothing) Then
                                            Exit Select
                                        End If

                                        mBinReader.ReadInt64()

                                        Dim iX As Single = BR_ReadSingle(mBinReader)
                                        Dim iY As Single = BR_ReadSingle(mBinReader)
                                        Dim iZ As Single = BR_ReadSingle(mBinReader)

                                        RaiseEvent OnTrackerGyro(mTracker, iX, iY, iZ)

                                    Case 4 ' ACCEL
                                        If (mTracker Is Nothing) Then
                                            Exit Select
                                        End If

                                        mBinReader.ReadInt64()

                                        Dim iX As Single = BR_ReadSingle(mBinReader)
                                        Dim iY As Single = BR_ReadSingle(mBinReader)
                                        Dim iZ As Single = BR_ReadSingle(mBinReader)

                                        RaiseEvent OnTrackerAccel(mTracker, iX, iY, iZ)

                                    Case 12 ' PACKET_BATTERY_VOLTAGE
                                        If (mTracker Is Nothing) Then
                                            Exit Select
                                        End If

                                        mBinReader.ReadInt64()

                                        Dim iVoltage As Single = BR_ReadSingle(mBinReader)

                                        'Dim iMinVolt As Single = 3.6F
                                        'Dim iMaxVolt As Single = 4.2F

                                        'Dim iVal As Single = (iVoltage - iMinVolt)
                                        'Dim iMaxVal As Single = (iMaxVolt - iMinVolt)
                                        'Dim iPercent As Integer = CInt(Math.Ceiling((iVal / iMaxVal) * 100))

                                        'If (iPercent < 0) Then
                                        '    iPercent = 0
                                        'End If

                                        'If (iPercent > 100) Then
                                        '    iPercent = 100
                                        'End If

                                        RaiseEvent OnTrackerBattery(mTracker, CInt(iVoltage))

                                    Case 14 ' PACKET_RESET_REASON

                                    Case 15 ' PACKET_SENSOR_INFO

                                    Case Else
                                        'Unknown packet
                                End Select
                            End Using
                        End Using


                    Catch ex As Threading.ThreadAbortException
                        Throw
                    Catch ex As SocketException
                    Catch ex As Exception
                        Threading.Thread.Sleep(100)
                    End Try

                    If (g_mLastKeepup.ElapsedMilliseconds > 500) Then
                        g_mLastKeepup.Reset()
                        g_mLastKeepup.Start()

                        SyncLock _ThreadLock
                            For Each mTrackerDic In g_mTrackers
                                Try
                                    Dim mTracker = g_mTrackers(mTrackerDic.Key)

                                    g_mSocket.SendTo(KEEPUP_BUFFER, SocketFlags.None, mTracker.m_EndPoint)

                                Catch ex As Threading.ThreadAbortException
                                    Throw
                                Catch ex As SocketException
                                Catch ex As Exception
                                End Try
                            Next
                        End SyncLock
                    End If
                End While
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
            End Try
        End Sub

        Private Sub SetupNewTracker(mBinReader As IO.BinaryReader, mEndPoint As IPEndPoint)
            Dim mTracker As ClassTracker = Nothing

            SyncLock _ThreadLock
                If (g_mTrackers.ContainsKey(mEndPoint.Address.ToString)) Then
                    mTracker = g_mTrackers(mEndPoint.Address.ToString)
                End If
            End SyncLock

            If (mTracker Is Nothing) Then
                Dim iFirmwareBuild As Integer = -1
                Dim sFirmwareName As String = ""
                Dim sMacAddress As String = ""

                mBinReader.ReadInt64() ' Skip packet number

                If (mBinReader.BaseStream.Length - mBinReader.BaseStream.Position > 0) Then
                    If (mBinReader.BaseStream.Length - mBinReader.BaseStream.Position > 3) Then
                        mBinReader.ReadInt32() ' (SlimeVR) Board Type 
                    End If

                    If (mBinReader.BaseStream.Length - mBinReader.BaseStream.Position > 3) Then
                        mBinReader.ReadInt32() ' (SlimeVR) IMU Type 
                    End If

                    If (mBinReader.BaseStream.Length - mBinReader.BaseStream.Position > 3) Then
                        mBinReader.ReadInt32() ' (SlimeVR) MCU TYPE
                    End If

                    If (mBinReader.BaseStream.Length - mBinReader.BaseStream.Position > 11) Then
                        mBinReader.ReadInt32() ' (SlimeVR) IMU info
                        mBinReader.ReadInt32()
                        mBinReader.ReadInt32()
                    End If

                    If (mBinReader.BaseStream.Length - mBinReader.BaseStream.Position > 3) Then
                        iFirmwareBuild = BR_ReadInt32(mBinReader) ' Firmware Build 
                    End If

                    If (mBinReader.BaseStream.Length - mBinReader.BaseStream.Position > 0) Then
                        Dim sFirmware As New Text.StringBuilder
                        Dim iLen As Integer = (mBinReader.ReadByte And &HFF)

                        While (iLen > 0 AndAlso (mBinReader.BaseStream.Length - mBinReader.BaseStream.Position) > 0)
                            Dim iChar As Char = Chr(mBinReader.ReadByte())
                            If (Asc(iChar) = 0) Then
                                Exit While
                            End If

                            sFirmware.Append(iChar)
                            iLen -= 1
                        End While

                        sFirmwareName = sFirmware.ToString
                    End If

                    Dim iMacAddress As Byte() = New Byte(6) {}
                    If (mBinReader.BaseStream.Length - mBinReader.BaseStream.Position > iMacAddress.Length) Then
                        iMacAddress = mBinReader.ReadBytes(iMacAddress.Length)

                        sMacAddress = String.Format("{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}", iMacAddress(0), iMacAddress(1), iMacAddress(2), iMacAddress(3), iMacAddress(4), iMacAddress(5))

                        If ((iMacAddress(0) Or iMacAddress(1) Or iMacAddress(2) Or iMacAddress(3) Or iMacAddress(4) Or iMacAddress(5)) = 0) Then
                            sMacAddress = ""
                        End If
                    End If

                    Dim sTrackerName As String
                    If (Not String.IsNullOrEmpty(sMacAddress)) Then
                        sTrackerName = String.Format("MAC: {0}", sMacAddress)
                    Else
                        sTrackerName = String.Format("UDP: {0}", mEndPoint.Address.ToString)
                    End If

                    SyncLock _ThreadLock
                        mTracker = New ClassTracker(sTrackerName, mEndPoint)
                        g_mTrackers(mEndPoint.Address.ToString) = mTracker
                    End SyncLock
                End If
            End If

            RaiseEvent OnTrackerConnected(mTracker)

            g_mSocket.SendTo(HANDSHAKE_BUFFER, SocketFlags.None, mEndPoint)
        End Sub

        Private Function BR_ReadInt32(br As IO.BinaryReader) As Integer
            If (Not BitConverter.IsLittleEndian) Then
                Return br.ReadInt32
            End If

            Dim bytes As Byte() = BitConverter.GetBytes(br.ReadInt32())
            Array.Reverse(bytes)
            Return BitConverter.ToInt32(bytes, 0)
        End Function

        Private Function BR_ReadSingle(br As IO.BinaryReader) As Single
            If (Not BitConverter.IsLittleEndian) Then
                Return br.ReadSingle
            End If

            Dim bytes As Byte() = BitConverter.GetBytes(br.ReadSingle())
            Array.Reverse(bytes)
            Return BitConverter.ToSingle(bytes, 0)
        End Function

        Public Class ClassQuaternionTools
            Public Shared Function ToQ(v As Vector3) As Quaternion
                Return ToQ(v.Y, v.X, v.Z)
            End Function

            Public Shared Function ToQ(yaw As Single, pitch As Single, roll As Single) As Quaternion
                yaw *= CSng(Math.PI / 180)
                pitch *= CSng(Math.PI / 180)
                roll *= CSng(Math.PI / 180)
                Dim rollOver2 = roll * 0.5F
                Dim sinRollOver2 As Single = CSng(Math.Sin(rollOver2))
                Dim cosRollOver2 As Single = CSng(Math.Cos(rollOver2))
                Dim pitchOver2 = pitch * 0.5F
                Dim sinPitchOver2 As Single = CSng(Math.Sin(pitchOver2))
                Dim cosPitchOver2 As Single = CSng(Math.Cos(pitchOver2))
                Dim yawOver2 = yaw * 0.5F
                Dim sinYawOver2 As Single = CSng(Math.Sin(yawOver2))
                Dim cosYawOver2 As Single = CSng(Math.Cos(yawOver2))
                Dim result As Quaternion
                result.W = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2
                result.X = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2
                result.Y = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2
                result.Z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2
                Return result
            End Function

            Public Shared Function FromQ2(q1 As Quaternion) As Vector3
                Dim sqw As Single = q1.W * q1.W
                Dim sqx As Single = q1.X * q1.X
                Dim sqy As Single = q1.Y * q1.Y
                Dim sqz As Single = q1.Z * q1.Z
                Dim unit = sqx + sqy + sqz + sqw ' if normalised is one, otherwise is correction factor
                Dim test As Single = q1.X * q1.W - q1.Y * q1.Z
                Dim v As Vector3

                If test > 0.4995F * unit Then ' singularity at north pole
                    v.Y = CSng(2.0F * Math.Atan2(q1.Y, q1.X))
                    v.X = Math.PI / 2
                    v.Z = 0
                    Return NormalizeAngles(v * (180 / Math.PI))
                End If

                If test < -0.4995F * unit Then ' singularity at south pole
                    v.Y = CSng(-2.0F * Math.Atan2(q1.Y, q1.X))
                    v.X = -Math.PI / 2
                    v.Z = 0
                    Return NormalizeAngles(v * (180 / Math.PI))
                End If

                Dim q As Quaternion = New Quaternion(q1.W, q1.Z, q1.X, q1.Y)
                v.Y = CSng(Math.Atan2(2.0F * q.X * q.W + 2.0F * q.Y * q.Z, 1 - 2.0F * (q.Z * q.Z + q.W * q.W)))     ' Yaw
                v.X = CSng(Math.Asin(2.0F * (q.X * q.Z - q.W * q.Y)))                                               ' Pitch
                v.Z = CSng(Math.Atan2(2.0F * q.X * q.Y + 2.0F * q.Z * q.W, 1 - 2.0F * (q.Y * q.Y + q.Z * q.Z)))     ' Roll
                Return NormalizeAngles(v * (180 / Math.PI))
            End Function

            Private Shared Function NormalizeAngles(angles As Vector3) As Vector3
                angles.X = NormalizeAngle(angles.X)
                angles.Y = NormalizeAngle(angles.Y)
                angles.Z = NormalizeAngle(angles.Z)
                Return angles
            End Function

            Private Shared Function NormalizeAngle(angle As Single) As Single
                While (angle > 360)
                    angle -= 360
                End While

                While (angle < 0)
                    angle += 360
                End While

                Return angle
            End Function
        End Class

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    If (g_mSocketListenerThread IsNot Nothing OrElse g_mSocketListenerThread.IsAlive) Then
                        g_mSocketListenerThread.Abort()
                        g_mSocketListenerThread.Join()
                        g_mSocketListenerThread = Nothing
                    End If

                    If (g_mPipeThread IsNot Nothing OrElse g_mPipeThread.IsAlive) Then
                        g_mPipeThread.Abort()
                        g_mPipeThread.Join()
                        g_mPipeThread = Nothing
                    End If

                    If (g_mSocket IsNot Nothing) Then
                        g_mSocket.Close()
                        g_mSocket = Nothing
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

        Public Class ClassTracker
            Private g_sName As String = ""

            Private g_mEndPoint As IPEndPoint
            Private g_mLastPacket As New Stopwatch
            Private g_iLastPacketId As Integer = -1
            Private g_mOrientation As New Quaternion

            'Public ReadOnly mClassIO As ClassIO = Nothing

            Public Sub New(_Name As String, _mEndPoint As IPEndPoint)
                g_sName = _Name
                g_mEndPoint = _mEndPoint

                g_mLastPacket.Start()

                'mClassIO = New ClassIO(Me)
            End Sub

            ReadOnly Property m_Name As String
                Get
                    Return g_sName
                End Get
            End Property

            ReadOnly Property m_LastPacketMillis As Long
                Get
                    Return g_mLastPacket.ElapsedMilliseconds
                End Get
            End Property

            Property m_PacketId As Integer
                Get
                    Return g_iLastPacketId
                End Get
                Set(value As Integer)
                    g_iLastPacketId = value
                End Set
            End Property

            ReadOnly Property m_EndPoint As IPEndPoint
                Get
                    Return g_mEndPoint
                End Get
            End Property

            Property m_Orentation As Quaternion
                Get
                    Return g_mOrientation
                End Get
                Set(value As Quaternion)
                    g_mOrientation = value
                End Set
            End Property

            Public Sub ReceivedPacket()
                g_mLastPacket.Reset()
                g_mLastPacket.Start()
            End Sub

        End Class
    End Class
End Class