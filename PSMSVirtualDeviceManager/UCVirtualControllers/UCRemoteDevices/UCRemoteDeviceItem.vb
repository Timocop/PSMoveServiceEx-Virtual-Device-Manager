Imports System.Numerics
Imports System.Text
Imports PSMSVirtualDeviceManager.UCRemoteDevices.ClassTrackerSocket

Public Class UCRemoteDeviceItem
    Const MAX_DEVICE_TIMEOUT As Integer = 5000

    Private Shared g_mThreadLock As New Object
    Private g_bInit As Boolean = False

    Public g_mUCRemoteDevices As UCRemoteDevices

    Public g_mClassIO As ClassIO
    Public g_mClassConfig As ClassConfig

    Private g_sTrackerName As String = ""
    Private g_sNickname As String = ""

    Private g_mRotationWait As New Stopwatch
    Private g_mGyroWait As New Stopwatch
    Private g_mBatteryWait As New Stopwatch

    Private g_bIgnoreEvents As Boolean = False
    Private g_bIgnoreUnsaved As Boolean = False
    Private g_mFpsPacketCounter As New Queue(Of Date)
    Private g_mFpsOrientationCounter As New Queue(Of Date)

    Private g_iStatusHideHeight As Integer = 0
    Private g_iStatusShowHeight As Integer = g_iStatusHideHeight
    Private g_iStatusDeviceResponseMS As Long = 0
    Private g_iStatusPipeFps As Integer = 0
    Private g_bHasStatusError As Boolean = False
    Private g_sHasStatusErrormessage As New KeyValuePair(Of String, String)("", "")
    Private g_mLastDeviceResponse As New Stopwatch

    Public Sub New(sTrackerName As String, _UCRemoteDevices As UCRemoteDevices)
        g_mUCRemoteDevices = _UCRemoteDevices
        g_sTrackerName = sTrackerName

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.  
        TextBox_TrackerName.Text = sTrackerName

        Try
            g_bIgnoreEvents = True

            ComboBox_ControllerID.Items.Clear()
            For i = -1 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                ComboBox_ControllerID.Items.Add(CStr(i))
            Next
            ComboBox_ControllerID.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        g_mClassIO = New ClassIO()
        g_mClassConfig = New ClassConfig(Me)

        g_mRotationWait.Start()
        g_mGyroWait.Start()
        g_mBatteryWait.Start()

        g_mClassIO.Enable()

        g_mLastDeviceResponse.Start()

        SetUnsavedState(False)

        CreateControl()

        ' Hide timeout error
        Panel_Status.Visible = False
        g_iStatusHideHeight = (Me.Height - Panel_Status.Height - Panel_Status.Margin.Top)
        g_iStatusShowHeight = Me.Height
        Me.Height = g_iStatusHideHeight
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

        AddHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerRotation, AddressOf OnTrackerRotation
        AddHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerGyro, AddressOf OnTrackerGyro
        AddHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerBattery, AddressOf OnTrackerBattery
        AddHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerPacket, AddressOf OnTrackerPacket

        Try
            Try
                g_bIgnoreUnsaved = True
                g_mClassConfig.LoadConfig()
            Finally
                g_bIgnoreUnsaved = False
            End Try

            SetUnsavedState(False)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub SetUnsavedState(bIsUnsaved As Boolean)
        If (g_bIgnoreUnsaved) Then
            Return
        End If

        If (bIsUnsaved) Then
            Button_SaveSettings.Text = String.Format("Save Settings*")
            Button_SaveSettings.Font = New Font(Button_SaveSettings.Font, FontStyle.Bold)
        Else
            Button_SaveSettings.Text = String.Format("Save Settings")
            Button_SaveSettings.Font = New Font(Button_SaveSettings.Font, FontStyle.Regular)
        End If
    End Sub

    Private Sub ComboBox_ControllerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_ControllerID.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassIO.m_Index = CInt(ComboBox_ControllerID.SelectedItem)
        g_mClassIO.Enable()

        SetUnsavedState(True)
    End Sub

    Private Sub Button_Recenter_Click(sender As Object, e As EventArgs) Handles Button_Recenter.Click
        g_mClassIO.RecenterOrientation()
        SetUnsavedState(True)
    End Sub

    Private Sub Button_SaveSettings_Click(sender As Object, e As EventArgs) Handles Button_SaveSettings.Click
        Try
            g_mClassConfig.SaveConfig()
            SetUnsavedState(False)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub TimerFPS_Tick(sender As Object, e As EventArgs) Handles TimerFPS.Tick
        TimerFPS.Stop()

        Try
            Dim iFpsPacketCounter As Integer
            Dim iFpsOrientationCounter As Integer
            Dim iFpssPipeCounter As Integer

            SyncLock g_mThreadLock
                iFpsPacketCounter = m_FpsPacketCounter
                iFpsOrientationCounter = m_FpsOrientationCounter
                iFpssPipeCounter = g_mClassIO.m_FpsPipeCounter
            End SyncLock

            If (iFpsOrientationCounter > 0) Then
                g_mLastDeviceResponse.Restart()
            End If

            g_iStatusDeviceResponseMS = g_mLastDeviceResponse.ElapsedMilliseconds
            g_iStatusPipeFps = iFpssPipeCounter

            If (Me.Visible) Then
                TextBox_Fps.Text = String.Format("Packets Total: {0}/s | Orientation Packets: {1}/s | Pipe IO: {2}/s", iFpsPacketCounter, iFpsOrientationCounter, iFpssPipeCounter)
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try

        TimerFPS.Start()
    End Sub


    Private Sub Timer_Status_Tick(sender As Object, e As EventArgs) Handles Timer_Status.Tick
        Timer_Status.Stop()

        Try
            Dim sTitle As String = ""
            Dim sMessage As String = ""
            Dim iStatusType As Integer = -1 ' -1 Hide, 0 Info, 1 Warn, 2 Error

            While True
                ' Check if data
                If (g_iStatusDeviceResponseMS > MAX_DEVICE_TIMEOUT) Then
                    sTitle = "Remote device is not responding"

                    Dim sText As New Text.StringBuilder
                    sText.AppendLine("This device is not sending any sensor data. It either lost connection, encountered an error or is currently in calibration mode.")

                    sMessage = sText.ToString
                    iStatusType = 2

                    Exit While
                End If

                ' Check if index valid
                If (g_mClassIO IsNot Nothing AndAlso g_mClassIO.m_Index < 0) Then
                    sTitle = "Remote device is disabled"

                    Dim sText As New Text.StringBuilder
                    sText.AppendLine("The controller id has not been set.")

                    sMessage = sText.ToString
                    iStatusType = 2

                    Exit While
                End If

                ' Check if connected
                If (g_iStatusPipeFps < 1) Then
                    sTitle = "Remote device is not connected to PSMoveServiceEx"

                    Dim sText As New Text.StringBuilder
                    sText.AppendLine("The remote device is currently not connected. Please select a controller id that has external orientation enabled using the 'OrientationExternal' filter or PSMoveServiceEx is not running.")

                    sMessage = sText.ToString
                    iStatusType = 2

                    Exit While
                End If

                Exit While
            End While

            g_bHasStatusError = (iStatusType > -1)
            g_sHasStatusErrormessage = New KeyValuePair(Of String, String)(sTitle, sMessage)

            If (Me.Visible) Then
                If (Label_StatusTitle.Text <> g_sHasStatusErrormessage.Key OrElse Label_StatusMessage.Text <> g_sHasStatusErrormessage.Value) Then
                    Label_StatusTitle.Text = g_sHasStatusErrormessage.Key
                    Label_StatusMessage.Text = g_sHasStatusErrormessage.Value
                End If

                If (g_bHasStatusError) Then
                    If (Not Panel_Status.Visible) Then
                        Panel_Status.Visible = True

                        If (Me.Height <> g_iStatusShowHeight) Then
                            Me.Height = g_iStatusShowHeight
                        End If
                    End If
                Else
                    If (Panel_Status.Visible) Then
                        Panel_Status.Visible = False

                        If (Me.Height <> g_iStatusHideHeight) Then
                            Me.Height = g_iStatusHideHeight
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try

        Timer_Status.Start()
    End Sub

    ReadOnly Property m_HasStatusError As Boolean
        Get
            Return g_bHasStatusError
        End Get
    End Property

    ReadOnly Property m_HasStatusErrorMessage As KeyValuePair(Of String, String)
        Get
            Return g_sHasStatusErrormessage
        End Get
    End Property

    Private Sub OnTrackerRotation(mTracker As ClassTracker, iX As Single, iY As Single, iZ As Single, iW As Single)
        If (mTracker.m_Name <> m_TrackerName) Then
            Return
        End If

        g_mClassIO.m_Orientation = New Quaternion(iX, iY, iZ, iW)

        AddFpsOrientationCounter()

        Dim bNewStatus As Boolean = False
        SyncLock g_mThreadLock
            If (g_mRotationWait.ElapsedMilliseconds > 100) Then
                g_mRotationWait.Restart()

                bNewStatus = True
            End If
        End SyncLock

        If (bNewStatus) Then
            Dim mAngle As Vector3 = ClassMathUtils.FromQ(New Quaternion(iX, iY, iZ, iW))
            Dim mResetAngle As Vector3 = ClassMathUtils.FromQ(g_mClassIO.m_ResetOrientation)
            Dim iOffsetAngle As Integer = g_mClassIO.m_YawOrientationOffset

            mAngle = ClassMathUtils.NormalizeAngles(New Vector3(mAngle.X - mResetAngle.X, mAngle.Y - mResetAngle.Y, mAngle.Z - mResetAngle.Z - iOffsetAngle))

            ClassUtils.AsyncInvoke(Sub()
                                       If (Me.Visible) Then
                                           TextBox_Axis.Text = String.Format("X: {1}{0}Y: {2}{0}Z: {3}", Environment.NewLine, Math.Round(mAngle.X), Math.Round(mAngle.Y), Math.Round(mAngle.Z))
                                       End If
                                   End Sub)
        End If
    End Sub

    Private Sub OnTrackerGyro(mTracker As ClassTracker, iX As Single, iY As Single, iZ As Single)
        If (mTracker.m_Name <> m_TrackerName) Then
            Return
        End If

        'g_mClassIO.m_Orientation = g_mClassIO.m_Orientation * ClassQuaternionTools.ToQ(iY, iX, iZ)

        Dim bNewStatus As Boolean = False
        SyncLock g_mThreadLock
            If (g_mGyroWait.ElapsedMilliseconds > 100) Then
                g_mGyroWait.Restart()

                bNewStatus = True
            End If
        End SyncLock

        If (bNewStatus) Then
            ClassUtils.AsyncInvoke(Sub()
                                       If (Me.Visible) Then
                                           TextBox_Gyro.Text = String.Format("X: {1}{0}Y: {2}{0}Z: {3}", Environment.NewLine, iX.ToString(Globalization.CultureInfo.InvariantCulture), iY.ToString(Globalization.CultureInfo.InvariantCulture), iZ.ToString(Globalization.CultureInfo.InvariantCulture))
                                       End If
                                   End Sub)
        End If


    End Sub

    Private Sub OnTrackerPacket(mTracker As ClassTracker)
        If (mTracker.m_Name <> m_TrackerName) Then
            Return
        End If

        AddFpsPacketCounter()
    End Sub

    Private Sub OnTrackerBattery(mTracker As ClassTracker, iBatteryPercent As Integer)
        If (mTracker.m_Name <> m_TrackerName) Then
            Return
        End If

        Dim bNewStatus As Boolean = False
        SyncLock g_mThreadLock
            If (g_mBatteryWait.ElapsedMilliseconds > 100) Then
                g_mBatteryWait.Restart()

                bNewStatus = True
            End If
        End SyncLock

        If (bNewStatus) Then
            ClassUtils.AsyncInvoke(Sub()
                                       If (Me.Visible) Then
                                           TextBox_Battery.Text = String.Format("Battery: {0}%", iBatteryPercent)
                                       End If
                                   End Sub)
        End If
    End Sub

    Private Sub SetTrackerNameText()
        TextBox_TrackerName.Text = m_TrackerName

        If (Not String.IsNullOrEmpty(m_Nickname)) Then
            TextBox_TrackerName.Text &= String.Format(" ({0})", m_Nickname)
        End If
    End Sub

    ReadOnly Property m_TrackerName As String
        Get
            Return g_sTrackerName
        End Get
    End Property

    Property m_Nickname As String
        Get
            Return g_sNickname
        End Get
        Set(value As String)
            g_sNickname = value
            SetTrackerNameText()
        End Set
    End Property

    Private Sub LinkLabel_EditName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_EditName.LinkClicked
        Dim sName As String = InputBox("Enter a new device name:", "New device name", m_Nickname)

        If (sName Is Nothing) Then
            Return
        End If

        If (sName.Length > 128) Then
            MessageBox.Show("Name is too long!", "Unable to set name", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If

        m_Nickname = sName
        SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_YawOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_YawOffset.ValueChanged
        g_mClassIO.m_YawOrientationOffset = CInt(NumericUpDown_YawOffset.Value)
        SetUnsavedState(True)
    End Sub

    Private ReadOnly Property m_FpsPacketCounter As Integer
        Get
            SyncLock g_mThreadLock
                Dim mNow As Date = Now

                While (g_mFpsPacketCounter.Count > 0 AndAlso g_mFpsPacketCounter.Peek() + New TimeSpan(0, 0, 1) < mNow)
                    g_mFpsPacketCounter.Dequeue()
                End While

                Return g_mFpsPacketCounter.Count
            End SyncLock
        End Get
    End Property

    Private Sub AddFpsPacketCounter()
        SyncLock g_mThreadLock
            Dim mNow As Date = Now

            While (g_mFpsPacketCounter.Count > 0 AndAlso g_mFpsPacketCounter.Peek() + New TimeSpan(0, 0, 1) < mNow)
                g_mFpsPacketCounter.Dequeue()
            End While

            g_mFpsPacketCounter.Enqueue(Now)
        End SyncLock
    End Sub

    Private ReadOnly Property m_FpsOrientationCounter As Integer
        Get
            SyncLock g_mThreadLock
                Dim mNow As Date = Now

                While (g_mFpsOrientationCounter.Count > 0 AndAlso g_mFpsOrientationCounter.Peek() + New TimeSpan(0, 0, 1) < mNow)
                    g_mFpsOrientationCounter.Dequeue()
                End While

                Return g_mFpsOrientationCounter.Count
            End SyncLock
        End Get
    End Property

    Private Sub AddFpsOrientationCounter()
        SyncLock g_mThreadLock
            Dim mNow As Date = Now

            While (g_mFpsOrientationCounter.Count > 0 AndAlso g_mFpsOrientationCounter.Peek() + New TimeSpan(0, 0, 1) < mNow)
                g_mFpsOrientationCounter.Dequeue()
            End While

            g_mFpsOrientationCounter.Enqueue(mNow)
        End SyncLock
    End Sub


    Private Sub CleanUp()
        If (g_mUCRemoteDevices.g_mClassStrackerSocket IsNot Nothing) Then
            RemoveHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerRotation, AddressOf OnTrackerRotation
            RemoveHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerGyro, AddressOf OnTrackerGyro
            RemoveHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerBattery, AddressOf OnTrackerBattery
            RemoveHandler g_mUCRemoteDevices.g_mClassStrackerSocket.OnTrackerPacket, AddressOf OnTrackerPacket
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
        Private g_mPipeThread As Threading.Thread = Nothing
        Private g_mPipeEvent As New Threading.AutoResetEvent(False)

        Private g_mOrientation As Quaternion = Quaternion.Identity
        Private g_mResetOrentation As Quaternion = Quaternion.Identity

        Private g_iYawOrientationOffset As Integer = 0
        Private g_mFpsPipeCounter As New Queue(Of Date)

        Public Sub New()
        End Sub

        Property m_Index As Integer
            Get
                Return g_iIndex
            End Get
            Set(value As Integer)
                If (g_mPipeThread IsNot Nothing AndAlso g_mPipeThread.IsAlive) Then
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

                g_mPipeEvent.Set()
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

        Property m_YawOrientationOffset As Integer
            Get
                SyncLock _ThreadLock
                    Return g_iYawOrientationOffset
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock _ThreadLock
                    g_iYawOrientationOffset = value
                End SyncLock
            End Set

        End Property

        Public Sub RecenterOrientation()
            m_ResetOrientation = m_Orientation
            g_mPipeEvent.Set()
        End Sub

        Public Sub Enable()
            If (g_iIndex < 0) Then
                Return
            End If

            If (g_mPipeThread IsNot Nothing AndAlso g_mPipeThread.IsAlive) Then
                Return
            End If

            g_mPipeThread = New Threading.Thread(AddressOf ThreadPipe)
            g_mPipeThread.IsBackground = True
            g_mPipeThread.Start()
        End Sub

        ReadOnly Property m_FpsPipeCounter As Integer
            Get
                SyncLock _ThreadLock
                    Dim mNow As Date = Now

                    While (g_mFpsPipeCounter.Count > 0 AndAlso g_mFpsPipeCounter.Peek() + New TimeSpan(0, 0, 1) < mNow)
                        g_mFpsPipeCounter.Dequeue()
                    End While

                    Return g_mFpsPipeCounter.Count
                End SyncLock
            End Get
        End Property

        Public Sub AddFpsPipeCounter()
            SyncLock _ThreadLock
                Dim mNow As Date = Now

                While (g_mFpsPipeCounter.Count > 0 AndAlso g_mFpsPipeCounter.Peek() + New TimeSpan(0, 0, 1) < mNow)
                    g_mFpsPipeCounter.Dequeue()
                End While

                g_mFpsPipeCounter.Enqueue(mNow)
            End SyncLock
        End Sub

        Public Sub Disable()
            If (g_mPipeThread Is Nothing OrElse Not g_mPipeThread.IsAlive) Then
                Return
            End If

            g_mPipeThread.Abort()
            g_mPipeThread.Join()
            g_mPipeThread = Nothing
        End Sub

        Private Sub ThreadPipe()
            While True
                Dim bExceptionSleep As Boolean = False

                Try
                    If (g_iIndex < 0) Then
                        Return
                    End If

                    Using mPipe As New IO.Pipes.NamedPipeClientStream(".", "PSMoveSerivceEx\VirtPSmoveStream_" & g_iIndex, IO.Pipes.PipeDirection.Out)
                        ' The thread when aborting will hang if we dont put a timeout.
                        mPipe.Connect(5000)

                        While True
                            g_mPipeEvent.WaitOne()

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
                                        Dim mNewResetOrientation = Quaternion.CreateFromAxisAngle(New Vector3(0, 0, 1), CSng(g_iYawOrientationOffset * (Math.PI / 180)))
                                        mNewResetOrientation = mNewResetOrientation * m_ResetOrientation

                                        Bw.Write(Encoding.ASCII.GetBytes(mNewResetOrientation.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(mNewResetOrientation.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes((-mNewResetOrientation.Y).ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(mNewResetOrientation.W.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))

                                        AddFpsPipeCounter()
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
                    bExceptionSleep = True
                    ClassAdvancedExceptionLogging.WriteToLog(ex)
                End Try

                ' Thread.Abort will not trigger inside a Try/Catch
                If (bExceptionSleep) Then
                    bExceptionSleep = False
                    Threading.Thread.Sleep(1000)
                End If
            End While
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).

                    If (g_mPipeThread IsNot Nothing AndAlso g_mPipeThread.IsAlive) Then
                        g_mPipeThread.Abort()
                        g_mPipeThread.Join()
                        g_mPipeThread = Nothing
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

        Private g_mUCRemoteDeviceItem As UCRemoteDeviceItem

        Public Sub New(_UCRemoteDeviceItem As UCRemoteDeviceItem)
            g_mUCRemoteDeviceItem = _UCRemoteDeviceItem
        End Sub

        Public Sub SaveConfig()
            SyncLock g_mThreadLock
                Dim sDevicePath As String = g_mUCRemoteDeviceItem.m_TrackerName

                Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_REMOTE, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)
                        Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Recenter.X", g_mUCRemoteDeviceItem.g_mClassIO.m_ResetOrientation.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Recenter.Y", g_mUCRemoteDeviceItem.g_mClassIO.m_ResetOrientation.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Recenter.Z", g_mUCRemoteDeviceItem.g_mClassIO.m_ResetOrientation.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Recenter.W", g_mUCRemoteDeviceItem.g_mClassIO.m_ResetOrientation.W.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "YawOffset", CStr(g_mUCRemoteDeviceItem.NumericUpDown_YawOffset.Value)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "ControllerID", CStr(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedIndex)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Nickname", g_mUCRemoteDeviceItem.g_sNickname))

                        mIni.WriteKeyValue(mIniContent.ToArray)
                    End Using
                End Using
            End SyncLock
        End Sub

        Public Sub LoadConfig()
            SyncLock g_mThreadLock
                Dim sDevicePath As String = g_mUCRemoteDeviceItem.m_TrackerName

                Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_REMOTE, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)

                        Dim iX As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Recenter.X", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        Dim iY As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Recenter.Y", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        Dim iZ As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Recenter.Z", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                        Dim iW As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Recenter.W", "1.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

                        g_mUCRemoteDeviceItem.g_mClassIO.m_ResetOrientation = New Quaternion(iX, iY, iZ, iW)

                        SetNumericUpDownClamp(g_mUCRemoteDeviceItem.NumericUpDown_YawOffset, CInt(mIni.ReadKeyValue(sDevicePath, "YawOffset", "0")))
                        SetComboBoxClamp(g_mUCRemoteDeviceItem.ComboBox_ControllerID, CInt(mIni.ReadKeyValue(sDevicePath, "ControllerID", "0")))
                        g_mUCRemoteDeviceItem.m_Nickname = CStr(mIni.ReadKeyValue(sDevicePath, "Nickname", ""))
                    End Using
                End Using
            End SyncLock
        End Sub

        Private Sub SetNumericUpDownClamp(mControl As NumericUpDown, iValue As Integer)
            mControl.Value = Math.Max(mControl.Minimum, Math.Min(mControl.Maximum, iValue))
        End Sub

        Private Sub SetComboBoxClamp(mControl As ComboBox, iIndex As Integer)
            If (mControl.Items.Count = 0) Then
                Return
            End If

            mControl.SelectedIndex = Math.Max(0, Math.Min(mControl.Items.Count - 1, iIndex))
        End Sub
    End Class
End Class
