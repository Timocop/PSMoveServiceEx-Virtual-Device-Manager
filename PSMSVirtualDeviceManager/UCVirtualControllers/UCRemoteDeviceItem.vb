Imports System.Numerics
Imports System.Text
Imports PSMSVirtualDeviceManager.UCRemoteDevices.ClassTrackerSocket

Public Class UCRemoteDeviceItem
    Shared _ThreadLock As New Object

    Public g_mUCRemoteDevices As UCRemoteDevices
    Public g_mClassIO As ClassIO
    Public g_mClassConfig As ClassConfig

    Private g_sTrackerName As String = ""

    Private g_mRotationWait As New Stopwatch
    Private g_mBatteryWait As New Stopwatch

    Private g_bIgnoreEvents As Boolean = False
    Private g_iFpsCounter As Integer = 0

    Public Sub New(sTrackerName As String, _UCRemoteDevices As UCRemoteDevices)
        g_mUCRemoteDevices = _UCRemoteDevices
        g_sTrackerName = sTrackerName

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Label_TrackerName.Text = sTrackerName

        Try
            g_bIgnoreEvents = True

            ComboBox_ControllerID.Items.Clear()
            For i = -1 To 6
                ComboBox_ControllerID.Items.Add(CStr(i))
            Next
            ComboBox_ControllerID.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        g_mClassIO = New ClassIO()
        g_mClassConfig = New ClassConfig(Me)

        AddHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerRotation, AddressOf OnTrackerRotation
        AddHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerBattery, AddressOf OnTrackerBattery

        g_mRotationWait.Start()
        g_mBatteryWait.Start()

        g_mClassIO.Enable()
    End Sub

    Private Sub UCRemoteDeviceItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            g_mClassConfig.LoadConfig()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboBox_ControllerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_ControllerID.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassIO.m_Index = CInt(ComboBox_ControllerID.SelectedItem)
        g_mClassIO.Enable()
    End Sub

    Private Sub Button_Recenter_Click(sender As Object, e As EventArgs) Handles Button_Recenter.Click
        g_mClassIO.RecenterOrientation()
    End Sub

    Private Sub Button_SaveSettings_Click(sender As Object, e As EventArgs) Handles Button_SaveSettings.Click
        Try
            g_mClassConfig.SaveConfig()

            MessageBox.Show("Device settings saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TimerFPS_Tick(sender As Object, e As EventArgs) Handles TimerFPS.Tick
        TimerFPS.Stop()

        SyncLock _ThreadLock
            Label_Fps.Text = String.Format("FPS: {0}", g_iFpsCounter)
            g_iFpsCounter = 0
        End SyncLock

        TimerFPS.Start()
    End Sub

    Private Sub OnTrackerRotation(mTracker As ClassTracker, iX As Single, iY As Single, iZ As Single, iW As Single)
        If (mTracker.m_Name <> m_TrackerName) Then
            Return
        End If

        SyncLock _ThreadLock
            g_iFpsCounter += 1
        End SyncLock

        g_mClassIO.m_Orientation = New Quaternion(iX, iY, iZ, iW)

        If (g_mRotationWait.ElapsedMilliseconds > 100) Then
            g_mRotationWait.Restart()

            Dim mAngle As Vector3 = ClassQuaternionTools.FromQ2(New Quaternion(iX, iY, iZ, iW))

            Me.BeginInvoke(Sub()
                               Label_Axis.Text = String.Format("X: {1}{0}Y: {2}{0}Z: {3}", Environment.NewLine, Math.Round(mAngle.X), Math.Round(mAngle.Y), Math.Round(mAngle.Z))
                           End Sub)
        End If


    End Sub

    Private Sub OnTrackerBattery(mTracker As ClassTracker, iBatteryPercent As Integer)
        If (mTracker.m_Name <> m_TrackerName) Then
            Return
        End If

        If (g_mBatteryWait.ElapsedMilliseconds > 1000) Then
            g_mBatteryWait.Restart()
            Me.BeginInvoke(Sub()
                               Label_Battery.Text = String.Format("Battery: {0}%", iBatteryPercent)
                           End Sub)
        End If
    End Sub

    ReadOnly Property m_TrackerName As String
        Get
            Return g_sTrackerName
        End Get
    End Property

    Private Sub CleanUp()
        If (g_mUCRemoteDevices.g_mClassStrackerSocket IsNot Nothing) Then
            RemoveHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerRotation, AddressOf OnTrackerRotation
            RemoveHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerBattery, AddressOf OnTrackerBattery
        End If

        If (g_mClassIO IsNot Nothing) Then
            g_mClassIO.Dispose()
            g_mClassIO = Nothing
        End If
    End Sub

    Public Class ClassIO
        Implements IDisposable

        Public _ThreadLock As New Object

        Private g_iIndex As Integer = -1
        Private g_PipeThread As Threading.Thread = Nothing

        Private g_mOrientation As Quaternion = Quaternion.Identity
        Private g_mResetOrentation As Quaternion = Quaternion.Identity

        Public Sub New()
        End Sub

        Property m_Index As Integer
            Get
                Return g_iIndex
            End Get
            Set(value As Integer)
                If (g_PipeThread IsNot Nothing AndAlso g_PipeThread.IsAlive) Then
                    Disable()
                    g_iIndex = value
                    Enable()
                Else
                    g_iIndex = value
                End If
            End Set
        End Property

        Property m_Orientation As Quaternion
            Get
                SyncLock _ThreadLock
                    Return g_mOrientation
                End SyncLock
            End Get
            Set(value As Quaternion)
                SyncLock _ThreadLock
                    g_mOrientation = value
                End SyncLock
            End Set
        End Property

        Property m_ResetOrientation As Quaternion
            Get
                SyncLock _ThreadLock
                    Return g_mResetOrentation
                End SyncLock
            End Get
            Set(value As Quaternion)
                SyncLock _ThreadLock
                    g_mResetOrentation = value
                End SyncLock
            End Set
        End Property

        Public Sub RecenterOrientation()
            m_ResetOrientation = m_Orientation
        End Sub

        Public Sub Enable()
            If (g_iIndex < 0) Then
                Return
            End If

            If (g_PipeThread IsNot Nothing AndAlso g_PipeThread.IsAlive) Then
                Return
            End If

            g_PipeThread = New Threading.Thread(AddressOf ThreadPipe)
            g_PipeThread.IsBackground = True
            g_PipeThread.Start()
        End Sub

        Public Sub Disable()
            If (g_PipeThread Is Nothing OrElse Not g_PipeThread.IsAlive) Then
                Return
            End If

            g_PipeThread.Abort()
            g_PipeThread.Join()
            g_PipeThread = Nothing
        End Sub

        Private Sub ThreadPipe()
            While True
                Try
                    If (g_iIndex < 0) Then
                        Return
                    End If

                    Using mPipe As New IO.Pipes.NamedPipeClientStream(".", "PSMoveSerivceEx\VirtPSmoveStream_" & g_iIndex, IO.Pipes.PipeDirection.Out)
                        ' The thread when aborting will hang if we dont put a timeout.
                        mPipe.Connect(5000)

                        While True
                            Dim iBytes = New Byte(128) {}

                            Using mMem As New IO.MemoryStream(iBytes)
                                Using Bw As New IO.BinaryWriter(mMem)
                                    SyncLock _ThreadLock
                                        ' Send Orientation
                                        Bw.Write(Encoding.ASCII.GetBytes(m_Orientation.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(m_Orientation.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes((-m_Orientation.Y).ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(m_Orientation.W.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))

                                        ' Send Reset Orientation
                                        Bw.Write(Encoding.ASCII.GetBytes(m_ResetOrientation.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(m_ResetOrientation.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes((-m_ResetOrientation.Y).ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(m_ResetOrientation.W.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                    End SyncLock
                                End Using

                                iBytes = mMem.ToArray
                            End Using

                            ' Write to pipe and wait for response.
                            ' $TODO Latency is quite ok but somewhat noticeable
                            mPipe.Write(iBytes, 0, iBytes.Length)
                            mPipe.WaitForPipeDrain()
                        End While
                    End Using
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    Threading.Thread.Sleep(1000)
                End Try
            End While
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).

                    If (g_PipeThread IsNot Nothing AndAlso g_PipeThread.IsAlive) Then
                        g_PipeThread.Abort()
                        g_PipeThread.Join()
                        g_PipeThread = Nothing
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

    Class ClassConfig
        Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "remote_devices.ini")

        Private g_mUCRemoteDeviceItem As UCRemoteDeviceItem

        Public Sub New(_UCRemoteDeviceItem As UCRemoteDeviceItem)
            g_mUCRemoteDeviceItem = _UCRemoteDeviceItem
        End Sub

        Public Sub SaveConfig()
            Dim sDevicePath As String = g_mUCRemoteDeviceItem.m_TrackerName

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    SyncLock _ThreadLock
                        Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Recenter.X", g_mUCRemoteDeviceItem.g_mClassIO.m_ResetOrientation.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Recenter.Y", g_mUCRemoteDeviceItem.g_mClassIO.m_ResetOrientation.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Recenter.Z", g_mUCRemoteDeviceItem.g_mClassIO.m_ResetOrientation.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Recenter.W", g_mUCRemoteDeviceItem.g_mClassIO.m_ResetOrientation.W.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "ControllerID", CStr(CInt(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedIndex))))

                        mIni.WriteKeyValue(mIniContent.ToArray)
                    End SyncLock
                End Using
            End Using
        End Sub

        Public Sub LoadConfig()
            Dim sDevicePath As String = g_mUCRemoteDeviceItem.m_TrackerName

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    Dim iX As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Recenter.X", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iY As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Recenter.Y", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iZ As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Recenter.Z", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iW As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Recenter.W", "1.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

                    g_mUCRemoteDeviceItem.g_mClassIO.m_ResetOrientation = New Quaternion(iX, iY, iZ, iW)

                    SetComboBoxClamp(g_mUCRemoteDeviceItem.ComboBox_ControllerID, CInt(mIni.ReadKeyValue(sDevicePath, "ControllerID", "0")))
                End Using
            End Using
        End Sub

        Private Sub SetComboBoxClamp(mControl As ComboBox, iIndex As Integer)
            If (mControl.Items.Count = 0) Then
                Return
            End If

            mControl.SelectedIndex = Math.Max(0, Math.Min(mControl.Items.Count - 1, iIndex))
        End Sub
    End Class
End Class
