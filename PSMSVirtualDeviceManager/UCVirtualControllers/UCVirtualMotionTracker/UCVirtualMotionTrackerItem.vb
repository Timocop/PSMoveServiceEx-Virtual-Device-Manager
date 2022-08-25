Imports System.Numerics
Imports System.Text

Public Class UCVirtualMotionTrackerItem
    Shared _ThreadLock As New Object

    Public g_mUCVirtualMotionTracker As UCVirtualMotionTracker

    Public g_mClassIO As ClassIO
    Public g_mClassConfig As ClassConfig

    Private g_sTrackerName As String = ""
    Private g_sNickname As String = ""

    Private g_bIgnoreEvents As Boolean = False
    Private g_bIgnoreUnsaved As Boolean = False

    Public Sub New(iControllerID As Integer, _UCVirtualMotionTracker As UCVirtualMotionTracker)
        g_mUCVirtualMotionTracker = _UCVirtualMotionTracker

        If (iControllerID < 0 OrElse iControllerID > ClassPSMoveSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1) Then
            iControllerID = -1
        End If

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.  
        Try
            g_bIgnoreEvents = True

            ComboBox_ControllerID.Items.Clear()
            For i = -1 To ClassPSMoveSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                ComboBox_ControllerID.Items.Add(CStr(i))
            Next
            ComboBox_ControllerID.SelectedIndex = 0

            If (iControllerID > -1) Then
                ComboBox_ControllerID.SelectedIndex = iControllerID + 1
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_VMTTrackerID.Items.Clear()
            For i = -1 To ClassPSMoveSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                ComboBox_VMTTrackerID.Items.Add(CStr(i))
            Next
            ComboBox_VMTTrackerID.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        g_mClassIO = New ClassIO()
        g_mClassConfig = New ClassConfig(Me)

        g_mClassIO.m_Index = CInt(ComboBox_ControllerID.SelectedItem)
        g_mClassIO.Enable()

        SetUnsavedState(False)

        CreateControl()
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

    Private Sub UCRemoteDeviceItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Try
                g_bIgnoreUnsaved = True
                g_mClassConfig.LoadConfig()
            Finally
                g_bIgnoreUnsaved = False
            End Try

            SetUnsavedState(False)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboBox_ControllerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_ControllerID.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Try
            g_bIgnoreUnsaved = True
            g_mClassConfig.LoadConfig()
        Finally
            g_bIgnoreUnsaved = False
        End Try

        g_mClassIO.m_Index = CInt(ComboBox_ControllerID.SelectedItem)
        g_mClassIO.Enable()

        SetUnsavedState(False)
    End Sub

    Private Sub ComboBox_ParentControllerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_VMTTrackerID.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassIO.m_ParentController = CInt(ComboBox_VMTTrackerID.SelectedItem)
        SetUnsavedState(True)
    End Sub

    Private Sub Button_SaveSettings_Click(sender As Object, e As EventArgs) Handles Button_SaveSettings.Click
        Try
            g_mClassConfig.SaveConfig()
            SetUnsavedState(False)

            MessageBox.Show("Device settings saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TimerFPS_Tick(sender As Object, e As EventArgs) Handles TimerFPS.Tick
        TimerFPS.Stop()

        SyncLock _ThreadLock
            TextBox_Fps.Text = String.Format("Pipe IO: {0}/s", g_mClassIO.m_FpsPipeCounter)

            g_mClassIO.m_FpsPipeCounter = 0
        End SyncLock

        TimerFPS.Start()
    End Sub

    Private Sub TimerPose_Tick(sender As Object, e As EventArgs) Handles TimerPose.Tick
        TimerPose.Stop()

        SyncLock _ThreadLock
            If (TypeOf g_mClassIO.m_Data Is ClassIO.STRUC_PSMOVE_DATA) Then
                Dim mData = DirectCast(g_mClassIO.m_Data, ClassIO.STRUC_PSMOVE_DATA)

                TextBox_Pos.Text = String.Format("Pos X: {0}{3}Pos Y: {1}{3}Pos Z: {2}", Math.Floor(mData.mPosition.X), Math.Floor(mData.mPosition.Y), Math.Floor(mData.mPosition.Z), Environment.NewLine)

                Dim iAng = ClassQuaternionTools.FromQ2(New Quaternion(mData.mOrientation.X, mData.mOrientation.Y, mData.mOrientation.Z, mData.mOrientation.W))
                TextBox_Gyro.Text = String.Format("Ang X: {0}{3}Ang Y: {1}{3}Ang Z: {2}", Math.Floor(iAng.X), Math.Floor(iAng.Y), Math.Floor(iAng.Z), Environment.NewLine)
            End If
        End SyncLock

        TimerPose.Start()
    End Sub


    Private Sub CleanUp()
        If (g_mClassIO IsNot Nothing) Then
            g_mClassIO.Dispose()
            g_mClassIO = Nothing
        End If
    End Sub

    Public Class ClassIO
        Implements IDisposable

        Public _ThreadLock As New Object

        Private g_iIndex As Integer = -1
        Private g_iParentIndex As Integer = -1
        Private g_mPipeThread As Threading.Thread = Nothing

        Private g_mJointOffset As New Vector3(0, 0, 0)
        Private g_mControllerOffset As New Vector3(0, 0, 0)
        Private g_iJointYawCorrection As Integer = 0
        Private g_iControllerYawCorrection As Integer = 0
        Private g_bOnlyJointOffset As Boolean = False

        Private g_iFpsPipeCounter As Integer = 0
        Private g_mData As IControllerData = Nothing

        Enum ENUM_CONTROLLER_TYPE
            PSMOVE = &H0
            PSNAVI = &H1
            PSDUALSHOCK = &H2
            VIRTUALCONTROLLER = &H3
        End Enum

        Public Interface IControllerData
        End Interface

        Class STRUC_PSMOVE_DATA
            Implements IControllerData

            Public Shared ReadOnly MAX_ITEMS As Integer = (11 + 4 + 3)

            Dim iDeviceID As Integer
            Public iSequenceNumber As Integer
            Public bIsOpen As Boolean
            Public iDeviceType As ENUM_CONTROLLER_TYPE

            Public bIsValid As Boolean
            Public bIsCurrentlyTracking As Boolean
            Public bIsTrackingEnabled As Boolean
            Public bIsOrientationStateValid As Boolean
            Public bIsPositionStateValid As Boolean

            Public mOrientation As New Vector4
            Public mPosition As New Vector3

            Public iTriggerValue As Integer
            Public iBatteryValue As Integer

            Sub New(_DeviceID As Integer,
                    _SequenceNumber As Integer,
                    _IsOpen As Boolean,
                    _DeviceType As ENUM_CONTROLLER_TYPE,
                    _IsValid As Boolean,
                    _IsCurrentlyTracking As Boolean,
                    _IsTrackingEnabled As Boolean,
                    _IsOrientationStateValid As Boolean,
                    _IsPositionStateValid As Boolean,
                    _Orientation As Vector4,
                    _Position As Vector3,
                    _TriggerValue As Integer,
                    _BatteryValue As Integer)
                iDeviceID = _DeviceID
                iSequenceNumber = _SequenceNumber
                bIsOpen = _IsOpen
                iDeviceType = _DeviceType
                bIsValid = _IsValid
                bIsCurrentlyTracking = _IsCurrentlyTracking
                bIsTrackingEnabled = _IsTrackingEnabled
                bIsOrientationStateValid = _IsOrientationStateValid
                bIsPositionStateValid = _IsPositionStateValid
                mOrientation = _Orientation
                mPosition = _Position
                iTriggerValue = _TriggerValue
                iBatteryValue = _BatteryValue
            End Sub
        End Class

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

        Property m_ParentController As Integer
            Get
                SyncLock _ThreadLock
                    Return g_iParentIndex
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock _ThreadLock
                    g_iParentIndex = value
                End SyncLock
            End Set
        End Property

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

        Property m_FpsPipeCounter As Integer
            Get
                SyncLock _ThreadLock
                    Return g_iFpsPipeCounter
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock _ThreadLock
                    g_iFpsPipeCounter = value
                End SyncLock
            End Set
        End Property

        Property m_Data As IControllerData
            Get
                SyncLock _ThreadLock
                    Return g_mData
                End SyncLock
            End Get
            Set(value As IControllerData)
                SyncLock _ThreadLock
                    g_mData = value
                End SyncLock
            End Set
        End Property

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
                Try
                    If (g_iIndex < 0) Then
                        Return
                    End If

                    Using mPipe As New IO.Pipes.NamedPipeClientStream(".", "PSMoveSerivceEx\ControllerDataStream_" & g_iIndex, IO.Pipes.PipeDirection.In)
                        ' The thread when aborting will hang if we dont put a timeout.
                        mPipe.Connect(1000)

                        While True
                            Dim iBytes = New Byte(2048 * 4) {}
                            mPipe.ReadAsync(iBytes, 0, iBytes.Length).Wait(1000)

                            SyncLock _ThreadLock
                                Dim sData As String = Encoding.ASCII.GetString(iBytes)

                                m_Data = ParseData(sData)

                                g_iFpsPipeCounter += 1
                            End SyncLock

                            'Threading.Thread.Sleep(1)
                        End While
                    End Using
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    Threading.Thread.Sleep(1000)
                End Try
            End While
        End Sub

        Private Function ParseData(sData As String) As STRUC_PSMOVE_DATA
            Dim sDatas As String() = sData.Split(New String() {vbLf}, StringSplitOptions.None)

            If (sDatas.Length < STRUC_PSMOVE_DATA.MAX_ITEMS + 1) Then
                Return Nothing
            End If

            Dim iPipeVersion As Integer = Integer.Parse(sDatas(0))
            If (iPipeVersion <> 1) Then
                Return Nothing
            End If

            Dim iDeviceID As Integer = CInt(sDatas(1))
            Dim iSequenceNumber As Integer = CInt(sDatas(2))
            Dim bIsOpen As Boolean = CBool(sDatas(3))
            Dim iDeviceType As ENUM_CONTROLLER_TYPE = CType(CInt(sDatas(4)), ENUM_CONTROLLER_TYPE)

            Select Case (iDeviceType)
                Case ENUM_CONTROLLER_TYPE.PSMOVE
                    Dim bIsValid As Boolean = CBool(sDatas(5))
                    Dim bIsCurrentlyTracking As Boolean = CBool(sDatas(6))
                    Dim bIsTrackingEnabled As Boolean = CBool(sDatas(7))
                    Dim bIsOrientationStateValid As Boolean = CBool(sDatas(8))
                    Dim getIsPositionStateValid As Boolean = CBool(sDatas(9))

                    Dim iOrientationW As Double = Double.Parse(sDatas(10), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iOrientationX As Double = Double.Parse(sDatas(11), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iOrientationY As Double = Double.Parse(sDatas(12), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iOrientationZ As Double = Double.Parse(sDatas(13), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

                    Dim iPositionX As Double = Double.Parse(sDatas(14), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iPositionY As Double = Double.Parse(sDatas(15), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iPositionZ As Double = Double.Parse(sDatas(16), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

                    Dim iTriggerValue As Integer = CInt(sDatas(17))
                    Dim iBatteryValue As Integer = CInt(sDatas(18))

                    Return New STRUC_PSMOVE_DATA(
                        iDeviceID,
                        iSequenceNumber,
                        bIsOpen,
                        iDeviceType,
                        bIsValid,
                        bIsCurrentlyTracking,
                        bIsTrackingEnabled,
                        bIsOrientationStateValid,
                        getIsPositionStateValid,
                        New Vector4(CSng(iOrientationX), CSng(iOrientationY), CSng(iOrientationZ), CSng(iOrientationW)),
                        New Vector3(CSng(iPositionX), CSng(iPositionY), CSng(iPositionZ)),
                        iTriggerValue,
                        iBatteryValue
                    )
            End Select

            Return Nothing
        End Function

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
        Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "vmt_devices.ini")

        Private g_mUCRemoteDeviceItem As UCVirtualMotionTrackerItem

        Public Sub New(_UCRemoteDeviceItem As UCVirtualMotionTrackerItem)
            g_mUCRemoteDeviceItem = _UCRemoteDeviceItem
        End Sub

        Public Sub SaveConfig()
            If (CInt(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedItem) < 0) Then
                Return
            End If

            Dim sDevicePath As String = CType(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedItem, String)

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    SyncLock _ThreadLock
                        Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "VMTTrackerID", CStr(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerID.SelectedIndex)))

                        mIni.WriteKeyValue(mIniContent.ToArray)
                    End SyncLock
                End Using
            End Using
        End Sub

        Public Sub LoadConfig()
            If (CInt(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedItem) < 0) Then
                Return
            End If

            Dim sDevicePath As String = CType(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedItem, String)

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    SetComboBoxClamp(g_mUCRemoteDeviceItem.ComboBox_VMTTrackerID, CInt(mIni.ReadKeyValue(sDevicePath, "VMTTrackerID", "-1")))
                End Using
            End Using
        End Sub

        Private Sub SetNumericUpDownClamp(mControl As NumericUpDown, iValue As Single)
            mControl.Value = CDec(Math.Max(mControl.Minimum, Math.Min(mControl.Maximum, iValue)))
        End Sub

        Private Sub SetComboBoxClamp(mControl As ComboBox, iIndex As Integer)
            If (mControl.Items.Count = 0) Then
                Return
            End If

            mControl.SelectedIndex = Math.Max(0, Math.Min(mControl.Items.Count - 1, iIndex))
        End Sub
    End Class

    Private Sub Label_Close_Click(sender As Object, e As EventArgs) Handles Label_Close.Click
        Me.Dispose()
    End Sub
End Class
