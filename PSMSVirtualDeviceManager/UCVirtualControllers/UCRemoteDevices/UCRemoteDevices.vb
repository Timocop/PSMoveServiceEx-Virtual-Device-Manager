Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Numerics
Imports System.Text

Public Class UCRemoteDevices
    Public g_mUCVirtualControllers As UCVirtualControllers

    Const DEFAULT_SOCKET_PORT As Integer = 6969

    Shared _ThreadLock As New Object

    Public g_mClassStrackerSocket As ClassTrackerSocket

    Private g_iSocketPort As Integer = 0
    Private g_sLocalIP As String = ""

    Private g_mLocalAddressThread As Threading.Thread = Nothing

    Class ClassRemoteDevicesListViewItem
        Inherits ListViewItem
        Implements IDisposable

        Private g_UCRemoteDeviceItem As UCRemoteDeviceItem
        Private g_UCRemoteDevices As UCRemoteDevices

        Public Sub New(sTrackerName As String, _UCRemoteDevices As UCRemoteDevices)
            MyBase.New(New String() {""})

            g_UCRemoteDevices = _UCRemoteDevices
            g_UCRemoteDeviceItem = New UCRemoteDeviceItem(sTrackerName, _UCRemoteDevices)

            UpdateItem()
        End Sub

        Public Sub UpdateItem()
            If (g_UCRemoteDeviceItem Is Nothing OrElse g_UCRemoteDeviceItem.IsDisposed) Then
                Return
            End If

            If (g_UCRemoteDeviceItem.g_mClassIO Is Nothing) Then
                Return
            End If

            Dim sTrackerName As String = g_UCRemoteDeviceItem.m_TrackerName
            If (Not String.IsNullOrEmpty(g_UCRemoteDeviceItem.m_Nickname)) Then
                sTrackerName &= String.Format(" ({0})", g_UCRemoteDeviceItem.m_Nickname)
            End If

            Me.SubItems(0).Text = sTrackerName

            'Is there any error?
            If (g_UCRemoteDeviceItem.Panel_Status.Visible) Then
                Me.BackColor = Color.FromArgb(255, 192, 192)
            Else
                Me.BackColor = Color.FromArgb(255, 255, 255)
            End If
        End Sub

        ReadOnly Property m_TrackerName As String
            Get
                If (g_UCRemoteDeviceItem Is Nothing OrElse g_UCRemoteDeviceItem.IsDisposed) Then
                    Return Nothing
                End If

                Return g_UCRemoteDeviceItem.m_TrackerName
            End Get
        End Property

        Property m_Visible As Boolean
            Get
                If (g_UCRemoteDeviceItem Is Nothing OrElse g_UCRemoteDeviceItem.IsDisposed) Then
                    Return False
                End If

                Return (g_UCRemoteDeviceItem.Parent Is g_UCRemoteDevices.Panel_RemoteDevices)
            End Get
            Set(value As Boolean)
                If (value) Then
                    If (g_UCRemoteDeviceItem Is Nothing OrElse g_UCRemoteDeviceItem.IsDisposed) Then
                        Return
                    End If

                    g_UCRemoteDeviceItem.Parent = g_UCRemoteDevices.Panel_RemoteDevices
                    g_UCRemoteDeviceItem.Dock = DockStyle.Top
                Else
                    g_UCRemoteDeviceItem.Parent = Nothing
                End If
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    If (g_UCRemoteDeviceItem IsNot Nothing AndAlso Not g_UCRemoteDeviceItem.IsDisposed) Then
                        g_UCRemoteDeviceItem.Dispose()
                        g_UCRemoteDeviceItem = Nothing
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

    Public Sub New(_mUCVirtualControllers As UCVirtualControllers)
        g_mUCVirtualControllers = _mUCVirtualControllers

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        g_mClassStrackerSocket = New ClassTrackerSocket(Me)

        AddHandler g_mClassStrackerSocket.OnTrackerConnected, AddressOf OnTrackerConnected

        m_SocketPort = DEFAULT_SOCKET_PORT
        m_SocketAddress = "127.0.0.1"

        CreateControl()

        g_mLocalAddressThread = New Threading.Thread(AddressOf LocalAddressThread)
        g_mLocalAddressThread.IsBackground = True
        g_mLocalAddressThread.Start()
    End Sub

    Private Sub LocalAddressThread()
        Try
            Dim sAddress As String = "127.0.0.1"

            For Each mAdapter As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
                If (mAdapter.OperationalStatus <> OperationalStatus.Up) Then
                    Continue For
                End If

                Dim bSuccess As Boolean = False

                For Each mAddress In mAdapter.GetIPProperties().UnicastAddresses
                    If (mAddress.Address.AddressFamily = AddressFamily.InterNetwork AndAlso mAddress.IsDnsEligible) Then
                        sAddress = mAddress.Address.ToString()
                        bSuccess = True
                        Exit For
                    End If
                Next

                If (bSuccess) Then
                    Exit For
                End If
            Next

            Me.BeginInvoke(Sub() m_SocketAddress = sAddress)
        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
        End Try
    End Sub

    Property m_SocketPort As Integer
        Get
            Return g_iSocketPort
        End Get
        Set(value As Integer)
            g_iSocketPort = value

            Label_Port.Text = String.Format("Listening Socket: {0}:{1}", g_sLocalIP, g_iSocketPort)
        End Set
    End Property

    Private Property m_SocketAddress As String
        Get
            Return g_sLocalIP
        End Get
        Set(value As String)
            g_sLocalIP = value

            Label_Port.Text = String.Format("Listening Socket: {0}:{1}", g_sLocalIP, g_iSocketPort)
        End Set
    End Property

    Private Sub OnTrackerConnected(mTracker As ClassTrackerSocket.ClassTracker)
        SyncLock _ThreadLock
            Dim sTrackerName As String = mTracker.m_Name

            Me.BeginInvoke(Sub()
                               For Each mItem As ListViewItem In ListView_RemoteDevices.Items
                                   Dim mRemoteDevicesItem = DirectCast(mItem, ClassRemoteDevicesListViewItem)
                                   If (mRemoteDevicesItem.m_TrackerName = sTrackerName) Then
                                       Return
                                   End If
                               Next

                               ListView_RemoteDevices.Items.Add(New ClassRemoteDevicesListViewItem(sTrackerName, Me))
                               Label_ConnectedDevices.Text = String.Format("Connected devices: {0}", ListView_RemoteDevices.Items.Count)
                           End Sub)
        End SyncLock
    End Sub

    Private Sub Button_StartSocket_Click(sender As Object, e As EventArgs) Handles Button_StartSocket.Click
        Try
            g_mClassStrackerSocket.Init()

            LinkLabel_EditPort.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CheckBox_AllowNewDevices_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_AllowNewDevices.CheckedChanged
        g_mClassStrackerSocket.m_AllowNewDevices = CheckBox_AllowNewDevices.Checked
    End Sub

    Private Sub CleanUp()
        If (g_mLocalAddressThread IsNot Nothing AndAlso g_mLocalAddressThread.IsAlive) Then
            g_mLocalAddressThread.Abort()
            g_mLocalAddressThread.Join()
            g_mLocalAddressThread = Nothing
        End If

        For Each mItem As ListViewItem In ListView_RemoteDevices.Items
            Dim mRemoteDevicesItem = DirectCast(mItem, ClassRemoteDevicesListViewItem)

            mRemoteDevicesItem.Dispose()
        Next
        ListView_RemoteDevices.Items.Clear()

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
        Catch ex As Exception
        Finally
            Timer_SocketCheck.Start()
        End Try
    End Sub

    Private Sub LinkLabel_EditPort_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_EditPort.LinkClicked
        Try
            Dim sValue As String = InputBox("Enter a new port number:", "Edit Port Number", CStr(m_SocketPort))
            If (String.IsNullOrEmpty(sValue)) Then
                Return
            End If

            Dim iPort As Integer = -1
            If (Not Integer.TryParse(sValue, iPort) OrElse iPort < 0 OrElse iPort > UShort.MaxValue) Then
                Throw New ArgumentException("Invalid port number")
            End If

            m_SocketPort = iPort
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Class ClassTrackerSocket
        Implements IDisposable

        Public g_mUCRemoteDevices As UCRemoteDevices

        Private g_mSocket As Socket = Nothing
        Private g_mBuffer As Byte() = New Byte(512) {}

        Private g_mLastKeepup As New Stopwatch
        Private g_mTrackers As New Dictionary(Of String, ClassTracker)
        Private g_bAllowNewDevices As Boolean = False

        Shared HANDSHAKE_BUFFER As Byte() = New Byte(64) {}
        Shared KEEPUP_BUFFER As Byte() = New Byte(64) {}

        Enum ENUM_PACKET_RECEIVE_TYPE
            HEARTBEAT = 0
            ROTATION
            GYRO
            HANDSHAKE
            ACCEL
            MAG
            RAW_CALIBRATION_DATA
            CALIBRATION_FINISHED
            CONFIG
            RAW_MAGENTOMETER
            PING_PONG
            SERIAL
            BATTERY_LEVEL
            TAP
            RESET_REASON
            SENSOR_INFO
            ROTATION_2
            ROTATION_DATA
            MAGENTOMETER_ACCURACY

            UNCALIBRATED_GYRO = 30
            UNCALIBRATED_ACCEL
            UNCALIBRATED_MAG
            CALIBRATED_GYRO
            CALIBRATED_ACCEL
            CALIBRATED_MAG

            BUTTON_PUSHED = 60
            SEND_MAG_STATUS
            CHANGE_MAG_STATUS
        End Enum

        Enum ENUM_PACKET_SEND_TYPE
            RECIEVE_HEARTBEAT = 1
            RECIEVE_VIBRATE
            RECIEVE_HANDSHAKE
            RECIEVE_COMMAND
        End Enum

        Private g_mSocketListenerThread As Threading.Thread = Nothing
        Private g_mPipeThread As Threading.Thread = Nothing

        Public Event OnTrackerConnected(mTracker As ClassTracker)
        Public Event OnTrackerGyro(mTracker As ClassTracker, iX As Single, iY As Single, iZ As Single)
        Public Event OnTrackerAccel(mTracker As ClassTracker, iX As Single, iY As Single, iZ As Single)
        Public Event OnTrackerRotation(mTracker As ClassTracker, iX As Single, iY As Single, iZ As Single, iW As Single)
        Public Event OnTrackerPacket(mTracker As ClassTracker)
        Public Event OnTrackerBattery(mTracker As ClassTracker, iBatteryPercent As Integer)

        Shared Sub New()
            HANDSHAKE_BUFFER(0) = CByte(ENUM_PACKET_SEND_TYPE.RECIEVE_HANDSHAKE)

            Dim iHandshakeMsg As Byte() = Encoding.ASCII.GetBytes("Hey OVR =D 5")
            Array.Copy(iHandshakeMsg, 0, HANDSHAKE_BUFFER, 1, iHandshakeMsg.Length)

            KEEPUP_BUFFER(3) = CByte(ENUM_PACKET_SEND_TYPE.RECIEVE_HEARTBEAT)
        End Sub

        Public Sub New(_UCRemoteDevices As UCRemoteDevices)
            g_mUCRemoteDevices = _UCRemoteDevices

            g_mSocket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            g_mBuffer = New Byte(512) {}

            g_mSocket.ReceiveTimeout = 5000
            g_mSocket.SendTimeout = 5000

            g_mLastKeepup.Start()
        End Sub

        Property m_AllowNewDevices As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bAllowNewDevices
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bAllowNewDevices = value
                End SyncLock
            End Set
        End Property

        ReadOnly Property m_Trackers As Dictionary(Of String, ClassTracker)
            Get
                Return g_mTrackers
            End Get
        End Property

        Public Sub Init()
            g_mSocket.Bind(New IPEndPoint(IPAddress.Any, g_mUCRemoteDevices.m_SocketPort))

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

        'https://github.com/abb128/SlimeVR-Server/blob/main/src/main/java/io/eiren/vr/trackers/TrackersUDPServer.java
        Private Sub ThreadSocketListener()
            Try
                If (g_mSocket Is Nothing) Then
                    Return
                End If

                While True
                    Try
                        Dim mEndPoint As New IPEndPoint(IPAddress.Any, 0)
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
                        Else
                            If (Not m_AllowNewDevices) Then
                                Continue While
                            End If
                        End If

                        Using mMemStream As New IO.MemoryStream(g_mBuffer)
                            Using mBinReader As New IO.BinaryReader(mMemStream)
                                Dim iPacketId As Integer = BR_ReadInt32(mBinReader)

                                'Debug.WriteLine("Packet: " & iPacketId)

                                Select Case (iPacketId)
                                    Case ENUM_PACKET_RECEIVE_TYPE.HEARTBEAT
                                        'Nothing

                                    Case ENUM_PACKET_RECEIVE_TYPE.HANDSHAKE
                                        SetupNewTracker(mBinReader, mEndPoint)

                                    Case ENUM_PACKET_RECEIVE_TYPE.ROTATION, ENUM_PACKET_RECEIVE_TYPE.ROTATION_2
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

                                    Case ENUM_PACKET_RECEIVE_TYPE.GYRO
                                        If (mTracker Is Nothing) Then
                                            Exit Select
                                        End If

                                        mBinReader.ReadInt64()

                                        Dim iX As Single = BR_ReadSingle(mBinReader)
                                        Dim iY As Single = BR_ReadSingle(mBinReader)
                                        Dim iZ As Single = BR_ReadSingle(mBinReader)

                                        RaiseEvent OnTrackerGyro(mTracker, iX, iY, iZ)

                                    Case ENUM_PACKET_RECEIVE_TYPE.ACCEL
                                        If (mTracker Is Nothing) Then
                                            Exit Select
                                        End If

                                        mBinReader.ReadInt64()

                                        Dim iX As Single = BR_ReadSingle(mBinReader)
                                        Dim iY As Single = BR_ReadSingle(mBinReader)
                                        Dim iZ As Single = BR_ReadSingle(mBinReader)

                                        RaiseEvent OnTrackerAccel(mTracker, iX, iY, iZ)

                                    Case ENUM_PACKET_RECEIVE_TYPE.BATTERY_LEVEL
                                        If (mTracker Is Nothing) Then
                                            Exit Select
                                        End If

                                        mBinReader.ReadInt64()

                                        Dim iVoltage As Single = BR_ReadSingle(mBinReader)
                                        ' Dim iPercentage As Single = BR_ReadSingle(mBinReader) 'SlimeVR only

                                        Select Case (mTracker.m_ProtocolType)
                                            Case ClassTracker.ENUM_PROTOCOL_TYPE.SLIMEVR
                                                Dim iMinVolt As Single = 3.2F
                                                Dim iMaxVolt As Single = 4.2F

                                                Dim iVal As Single = (iVoltage - iMinVolt)
                                                Dim iMaxVal As Single = (iMaxVolt - iMinVolt)
                                                Dim iPercent As Integer = CInt(Math.Ceiling((iVal / iMaxVal) * 100))

                                                If (iPercent < 0) Then
                                                    iPercent = 0
                                                End If

                                                If (iPercent > 100) Then
                                                    iPercent = 100
                                                End If

                                                RaiseEvent OnTrackerBattery(mTracker, iPercent)

                                            Case ClassTracker.ENUM_PROTOCOL_TYPE.OWOTRACK
                                                ' owoTrack uses voltage as percentage
                                                Dim iPercent As Integer = CInt(iVoltage * 100)

                                                If (iPercent < 0) Then
                                                    iPercent = 0
                                                End If

                                                If (iPercent > 100) Then
                                                    iPercent = 100
                                                End If

                                                RaiseEvent OnTrackerBattery(mTracker, iPercent)
                                        End Select

                                    Case ENUM_PACKET_RECEIVE_TYPE.ROTATION_DATA
                                        If (mTracker Is Nothing) Then
                                            Exit Select
                                        End If

                                        If (mTracker.m_ProtocolType <> ClassTracker.ENUM_PROTOCOL_TYPE.SLIMEVR) Then
                                            Exit Select
                                        End If

                                        mBinReader.ReadInt64()

                                        Dim iSensorId As Integer = (mBinReader.ReadByte And &HFF)

                                        ' $TODO Add multiple sensors.
                                        If (iSensorId <> 0) Then
                                            Exit Select
                                        End If

                                        Const DATA_TYPE_NORMAL = 1
                                        Const DATA_TYPE_CORRECTION = 2

                                        Dim iDataType As Integer = (mBinReader.ReadByte And &HFF)
                                        If (iDataType <> DATA_TYPE_NORMAL) Then
                                            Exit Select
                                        End If

                                        Dim iX As Single = BR_ReadSingle(mBinReader)
                                        Dim iY As Single = BR_ReadSingle(mBinReader)
                                        Dim iZ As Single = BR_ReadSingle(mBinReader)
                                        Dim iW As Single = BR_ReadSingle(mBinReader)
                                        Dim mQuat As New Quaternion(iX, iY, iZ, iW)

                                        RaiseEvent OnTrackerRotation(mTracker, iX, iY, iZ, iW)

                                        SyncLock _ThreadLock
                                            mTracker.m_Orentation = mQuat
                                        End SyncLock

                                        'Dim iCalibrationInfo As Integer = (mBinReader.ReadByte And &HFF)

                                    Case Else
                                        'Unknown packet
                                End Select
                            End Using
                        End Using
                    Catch ex As Threading.ThreadAbortException
                        Throw
                    Catch ex As SocketException
                    Catch ex As Exception
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
                Dim iProtocolType As ClassTracker.ENUM_PROTOCOL_TYPE = ClassTracker.ENUM_PROTOCOL_TYPE.SLIMEVR

                mBinReader.ReadInt64() ' Skip packet number

                mBinReader.ReadInt32() ' (SlimeVR) Board Type  

                mBinReader.ReadInt32() ' (SlimeVR) IMU Type  

                mBinReader.ReadInt32() ' (SlimeVR) MCU TYPE 

                mBinReader.ReadInt32() ' (SlimeVR) IMU info
                mBinReader.ReadInt32()
                mBinReader.ReadInt32()

                iFirmwareBuild = BR_ReadInt32(mBinReader) ' Firmware Build  

                ' No firmware build number?
                If (iFirmwareBuild < 1) Then
                    Return
                End If

                Dim sFirmware As New Text.StringBuilder
                Dim iLen As Integer = (mBinReader.ReadByte And &HFF)

                While (iLen > 0 AndAlso (mBinReader.BaseStream.Length - mBinReader.BaseStream.Position) >= 1)
                    Dim iChar As Char = Chr(mBinReader.ReadByte())
                    If (Asc(iChar) = 0) Then
                        Exit While
                    End If

                    sFirmware.Append(iChar)
                    iLen -= 1
                End While

                sFirmwareName = sFirmware.ToString

                ' No firmware name?
                If (String.IsNullOrEmpty(sFirmwareName)) Then
                    Return
                End If

                If (sFirmwareName.StartsWith("owoTrack")) Then
                    iProtocolType = ClassTracker.ENUM_PROTOCOL_TYPE.OWOTRACK
                End If

                Dim iMacAddress As Byte() = New Byte(6) {}
                If ((mBinReader.BaseStream.Length - mBinReader.BaseStream.Position) >= iMacAddress.Length) Then
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
                    mTracker = New ClassTracker(sTrackerName, iProtocolType, mEndPoint)
                    g_mTrackers(mEndPoint.Address.ToString) = mTracker
                End SyncLock

                RaiseEvent OnTrackerConnected(mTracker)
            End If

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

            Enum ENUM_PROTOCOL_TYPE
                INVALID = -1
                OWOTRACK
                SLIMEVR
            End Enum

            Private g_mEndPoint As IPEndPoint
            Private g_mLastPacket As New Stopwatch
            Private g_iLastPacketId As Integer = -1
            Private g_mOrientation As New Quaternion
            Private g_iProtocolType As ENUM_PROTOCOL_TYPE = ENUM_PROTOCOL_TYPE.INVALID

            Public Sub New(_Name As String, _ProtocolType As ENUM_PROTOCOL_TYPE, _mEndPoint As IPEndPoint)
                g_sName = _Name
                g_mEndPoint = _mEndPoint
                g_iProtocolType = _ProtocolType

                g_mLastPacket.Start()
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

            Property m_ProtocolType As ENUM_PROTOCOL_TYPE
                Get
                    Return g_iProtocolType
                End Get
                Set(value As ENUM_PROTOCOL_TYPE)
                    g_iProtocolType = value
                End Set
            End Property

            Public Sub ReceivedPacket()
                g_mLastPacket.Reset()
                g_mLastPacket.Start()
            End Sub

        End Class
    End Class

    Private Sub LinkLabel_ReadMore_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ReadMore.LinkClicked
        Dim mMsg As New FormRtfHelp
        mMsg.RichTextBox_Help.Rtf = My.Resources.HelpRemoteDevices
        mMsg.ShowDialog(Me)
    End Sub

    Private Sub ListView_RemoteDevices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_RemoteDevices.SelectedIndexChanged
        For Each mItem As ListViewItem In ListView_RemoteDevices.Items
            Dim mAttachmentItem = DirectCast(mItem, ClassRemoteDevicesListViewItem)
            If (mAttachmentItem.Selected) Then
                If (Not mAttachmentItem.m_Visible) Then
                    mAttachmentItem.m_Visible = True
                End If
            Else
                If (mAttachmentItem.m_Visible) Then
                    mAttachmentItem.m_Visible = False
                End If
            End If
        Next
    End Sub

    Private Sub Timer_RemoteDevices_Tick(sender As Object, e As EventArgs) Handles Timer_RemoteDevices.Tick
        Try
            Timer_RemoteDevices.Stop()

            For Each mItem As ListViewItem In ListView_RemoteDevices.Items
                Dim mAttachmentItem = DirectCast(mItem, ClassRemoteDevicesListViewItem)
                mAttachmentItem.UpdateItem()
            Next
        Catch ex As Exception
        Finally
            Timer_RemoteDevices.Start()
        End Try
    End Sub
End Class
