Imports System.Numerics
Imports System.Text
Imports Rug.Osc

Public Class UCVirtualMotionTrackerItem
    Const MAX_VMT_TRACKER As Integer = 20
    Const MAX_DRIVER_TIMEOUT As Integer = 5000
    Const MAX_CONTROLLER_TIMEOUT As Integer = 5000
    Const VMT_DRIVER_VERSION_EXPECT As String = "VMT_013"

    Shared _ThreadLock As New Object

    Public g_mUCVirtualMotionTracker As UCVirtualMotionTracker

    Public g_mClassIO As ClassIO
    Public g_mClassConfig As ClassConfig

    Private g_sTrackerName As String = ""
    Private g_sNickname As String = ""

    Private g_bIgnoreEvents As Boolean = False
    Private g_bIgnoreUnsaved As Boolean = False


    Private g_iStatusHideHeight As Integer = 0
    Private g_iStatusShowHeight As Integer = g_iStatusHideHeight

    Public g_mDriverLastResponse As New Stopwatch
    Public g_mControllerLastResponse As New Stopwatch
    Public g_sDriverLastResponseCode As Integer = 0
    Public g_sDriverLastResponseMessage As String = ""
    Public g_sDriverVersion As String = ""
    Public g_sDriverPath As String = ""

    Public Sub New(iControllerID As Integer, _UCVirtualMotionTracker As UCVirtualMotionTracker)
        g_mUCVirtualMotionTracker = _UCVirtualMotionTracker

        If (iControllerID < 0 OrElse iControllerID > ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1) Then
            iControllerID = -1
        End If

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.   
        Try
            g_bIgnoreEvents = True

            ComboBox_ControllerID.Items.Clear()
            For i = -1 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
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
            For i = -1 To MAX_VMT_TRACKER
                ComboBox_VMTTrackerID.Items.Add(CStr(i))
            Next
            ComboBox_VMTTrackerID.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        g_mClassIO = New ClassIO(Me)
        g_mClassConfig = New ClassConfig(Me)

        g_mClassIO.m_Index = CInt(ComboBox_ControllerID.SelectedItem)
        g_mClassIO.Enable()

        g_mDriverLastResponse.Start()
        g_mControllerLastResponse.Start()

        SetUnsavedState(False)

        AddHandler g_mUCVirtualMotionTracker.g_ClassOscServer.OnOscProcessMessage, AddressOf OnOscProcessMessage

        CreateControl()

        ' Hide timeout error
        Panel_Status.Visible = False
        g_iStatusHideHeight = (Me.Height - Panel_Status.Height - Panel_Status.Margin.Top)
        g_iStatusShowHeight = Me.Height
        Me.Height = g_iStatusHideHeight
    End Sub

    Private Sub OnOscProcessMessage(mMessage As OscMessage)
        Try
            Select Case (mMessage.Address)
                Case "/VMT/Out/Log"
                    If (mMessage.Count < 2) Then
                        Return
                    End If

                    Dim iNum As Integer = CInt(mMessage(0))
                    Dim sMessage As String = CStr(mMessage(1))

                    ' #TODO Add log

                Case "/VMT/Out/Alive"
                    If (mMessage.Count < 1) Then
                        Return
                    End If

                    Dim sDriverVersion As String = CStr(mMessage(0))
                    Dim sDriverPath As String = ""
                    If (mMessage.Count > 1) Then
                        sDriverPath = CStr(mMessage(1))
                    End If

                    Me.BeginInvoke(Sub()
                                       g_mDriverLastResponse.Restart()

                                       g_sDriverVersion = sDriverVersion
                                       g_sDriverPath = sDriverPath
                                   End Sub)
                Case "/VMT/Out/Haptic"
                    ' No Le Hep
                Case "/VMT/Out/Unavailable"
                    If (mMessage.Count < 2) Then
                        Return
                    End If

                    Dim iCode As Integer = CInt(mMessage(0))
                    Dim sReason As String = CStr(mMessage(1))

                    Select Case (iCode)
                        Case 1 'Room matrix not setup
                            If (g_mUCVirtualMotionTracker.g_ClassOscServer.IsRunning) Then
                                g_mUCVirtualMotionTracker.g_ClassOscServer.Send(New OscMessage(
                                    "/VMT/SetRoomMatrix/Temporary", New Object() {
                                        1.0F, 0F, 0F, 0F,
                                        0F, 1.0F, 0F, 0F,
                                        0F, 0F, 1.0F, 0F
                                    }))
                            End If
                    End Select

                    If (iCode = 0) Then
                        Me.BeginInvoke(Sub()
                                           g_mDriverLastResponse.Restart()

                                           g_sDriverLastResponseCode = iCode
                                           g_sDriverLastResponseMessage = sReason
                                       End Sub)
                    Else
                        Me.BeginInvoke(Sub()
                                           g_mDriverLastResponse.Restart()

                                           g_sDriverLastResponseCode = iCode
                                           g_sDriverLastResponseMessage = sReason
                                       End Sub)
                    End If

            End Select
        Catch ex As Exception
            ' Something sussy is going on...
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

        g_mClassIO.m_VmtTracker = CInt(ComboBox_VMTTrackerID.SelectedItem)
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
        Try
            TimerFPS.Stop()

            SyncLock _ThreadLock
                TextBox_Fps.Text = String.Format("OSC IO: {0}/s", g_mClassIO.m_FpsOscCounter)

                If (g_mClassIO.m_FpsOscCounter > 0) Then
                    g_mControllerLastResponse.Restart()
                End If

                g_mClassIO.m_FpsOscCounter = 0
            End SyncLock
        Finally
            TimerFPS.Start()
        End Try
    End Sub

    Private Sub TimerPose_Tick(sender As Object, e As EventArgs) Handles TimerPose.Tick
        Try
            TimerPose.Stop()

            SyncLock _ThreadLock
                TextBox_Pos.Text = String.Format("Pos X: {0}{3}Pos Y: {1}{3}Pos Z: {2}", Math.Floor(g_mClassIO.m_Data.mPosition.X), Math.Floor(g_mClassIO.m_Data.mPosition.Y), Math.Floor(g_mClassIO.m_Data.mPosition.Z), Environment.NewLine)

                Dim iAng = ClassQuaternionTools.FromQ2(g_mClassIO.m_Data.mOrientation)
                TextBox_Gyro.Text = String.Format("Ang X: {0}{3}Ang Y: {1}{3}Ang Z: {2}", Math.Floor(iAng.X), Math.Floor(iAng.Y), Math.Floor(iAng.Z), Environment.NewLine)
            End SyncLock
        Finally
            TimerPose.Start()
        End Try
    End Sub


    Private Sub Timer_Status_Tick(sender As Object, e As EventArgs) Handles Timer_Status.Tick
        Try
            Timer_Status.Stop()

            Dim sTitle As String = ""
            Dim sMessage As String = ""
            Dim iStatusType As Integer = -1 ' -1 Hide, 0 Info, 1 Warn, 2 Error

            While True
                ' Show driver timeouts
                If (g_mDriverLastResponse.ElapsedMilliseconds > MAX_DRIVER_TIMEOUT) Then
                    sTitle = "Driver not is responding!"

                    Dim sText As New Text.StringBuilder
                    sText.AppendLine("The driver is not responding! Make sure the OSC server is running and the driver installed correctly.")

                    If (Not String.IsNullOrEmpty(g_sDriverVersion)) Then
                        sText.AppendFormat("VMT driver version: {0}", g_sDriverVersion).AppendLine()
                    End If
                    If (Not String.IsNullOrEmpty(g_sDriverPath)) Then
                        sText.AppendFormat("VMT driver path: {0}", g_sDriverPath).AppendLine()
                    End If

                    sMessage = sText.ToString
                    iStatusType = 2

                    Exit While
                End If


                If (g_sDriverLastResponseCode <> 0) Then
                    sTitle = String.Format("Driver responded with an error (Code: {0})!", g_sDriverLastResponseCode)

                    Dim sText As New Text.StringBuilder
                    sText.AppendLine(g_sDriverLastResponseMessage)

                    If (Not String.IsNullOrEmpty(g_sDriverVersion)) Then
                        sText.AppendFormat("VMT driver version: {0}", g_sDriverVersion).AppendLine()
                    End If
                    If (Not String.IsNullOrEmpty(g_sDriverPath)) Then
                        sText.AppendFormat("VMT driver path: {0}", g_sDriverPath).AppendLine()
                    End If

                    sMessage = sText.ToString
                    iStatusType = 2

                    Exit While
                End If

                ' Show user wrong driver version
                If (Not String.IsNullOrEmpty(g_sDriverVersion) AndAlso g_sDriverVersion <> VMT_DRIVER_VERSION_EXPECT) Then
                    sTitle = "Driver running but might be incompatible"


                    Dim sText As New Text.StringBuilder
                    sText.AppendFormat("The driver reported version '{0}' but required is {1}! This may cause problems.", g_sDriverVersion, VMT_DRIVER_VERSION_EXPECT).AppendLine()

                    If (Not String.IsNullOrEmpty(g_sDriverVersion)) Then
                        sText.AppendFormat("VMT driver version: {0}", g_sDriverVersion).AppendLine()
                    End If
                    If (Not String.IsNullOrEmpty(g_sDriverPath)) Then
                        sText.AppendFormat("VMT driver path: {0}", g_sDriverPath).AppendLine()
                    End If

                    sMessage = sText.ToString
                    iStatusType = 1

                    Exit While
                End If

                ' Show tracker not working
                If (g_mControllerLastResponse.ElapsedMilliseconds > MAX_CONTROLLER_TIMEOUT) Then
                    sTitle = "Controller is not responding!"

                    Dim sText As New Text.StringBuilder
                    sText.AppendLine("There are no new incoming pose data. Make sure PSMoveServiceEx is running.")

                    sMessage = sText.ToString
                    iStatusType = 2

                    Exit While
                End If

                Exit While
            End While

            If (Label_StatusTitle.Text <> sTitle OrElse Label_StatusMessage.Text <> sMessage) Then
                Label_StatusTitle.Text = sTitle
                Label_StatusMessage.Text = sMessage
            End If

            If (iStatusType < 0) Then
                If (Panel_Status.Visible) Then
                    Panel_Status.Visible = False

                    Me.Height = g_iStatusHideHeight
                End If
            Else
                If (Not Panel_Status.Visible) Then
                    Panel_Status.Visible = True

                    Me.Height = g_iStatusShowHeight
                End If
            End If
        Finally
            Timer_Status.Start()
        End Try
    End Sub

    Private Sub CleanUp()
        If (g_mUCVirtualMotionTracker IsNot Nothing AndAlso g_mUCVirtualMotionTracker.g_ClassOscServer IsNot Nothing) Then
            RemoveHandler g_mUCVirtualMotionTracker.g_ClassOscServer.OnOscProcessMessage, AddressOf OnOscProcessMessage
        End If

        If (g_mClassIO IsNot Nothing) Then
            g_mClassIO.Dispose()
            g_mClassIO = Nothing
        End If
    End Sub

    Public Class ClassIO
        Implements IDisposable

        Public _ThreadLock As New Object
        Public g_UCVirtualMotionTrackerItem As UCVirtualMotionTrackerItem

        Private g_iIndex As Integer = -1
        Private g_iVmtTracker As Integer = -1
        Private g_mOscThread As Threading.Thread = Nothing

        Private g_mJointOffset As New Vector3(0, 0, 0)
        Private g_mControllerOffset As New Vector3(0, 0, 0)
        Private g_iJointYawCorrection As Integer = 0
        Private g_iControllerYawCorrection As Integer = 0
        Private g_bOnlyJointOffset As Boolean = False

        Private g_iFpsOscCounter As Integer = 0
        Private g_mData As ClassServiceClient.STRUC_CONTROLLER_DATA

        Private g_mOscDataPack As New STRUC_OSC_DATA_PACK()

        Enum ENUM_CONTROLLER_TYPE
            PSMOVE = &H0
            PSNAVI = &H1
            PSDUALSHOCK = &H2
            VIRTUALCONTROLLER = &H3
        End Enum

        Public Interface IControllerData
        End Interface

        Class STRUC_OSC_DATA_PACK
            Public mPosition As New Vector3(0, 0, 0)
            Public mOrientation As New Quaternion(0, 0, 0, 1)
        End Class

        Public Sub New(_UCVirtualMotionTrackerItem As UCVirtualMotionTrackerItem)
            g_UCVirtualMotionTrackerItem = _UCVirtualMotionTrackerItem
        End Sub

        Property m_Index As Integer
            Get
                Return g_iIndex
            End Get
            Set(value As Integer)
                If (g_mOscThread IsNot Nothing AndAlso g_mOscThread.IsAlive) Then
                    Disable()
                    g_iIndex = value
                    Enable()
                Else
                    g_iIndex = value
                End If
            End Set
        End Property

        Property m_VmtTracker As Integer
            Get
                SyncLock _ThreadLock
                    Return g_iVmtTracker
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock _ThreadLock
                    g_iVmtTracker = value
                End SyncLock
            End Set
        End Property

        Public Sub Enable()
            If (g_iIndex < 0) Then
                Return
            End If

            If (g_mOscThread IsNot Nothing AndAlso g_mOscThread.IsAlive) Then
                Return
            End If

            g_mOscThread = New Threading.Thread(AddressOf ThreadOsc)
            g_mOscThread.IsBackground = True
            g_mOscThread.Start()
        End Sub

        Property m_FpsOscCounter As Integer
            Get
                SyncLock _ThreadLock
                    Return g_iFpsOscCounter
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock _ThreadLock
                    g_iFpsOscCounter = value
                End SyncLock
            End Set
        End Property

        Property m_Data As ClassServiceClient.STRUC_CONTROLLER_DATA
            Get
                SyncLock _ThreadLock
                    If (g_mData Is Nothing) Then
                        Return New ClassServiceClient.STRUC_CONTROLLER_DATA
                    End If

                    Return g_mData
                End SyncLock
            End Get
            Set(value As ClassServiceClient.STRUC_CONTROLLER_DATA)
                SyncLock _ThreadLock
                    g_mData = value
                End SyncLock
            End Set
        End Property

        Public Sub Disable()
            If (g_mOscThread Is Nothing OrElse Not g_mOscThread.IsAlive) Then
                Return
            End If

            g_mOscThread.Abort()
            g_mOscThread.Join()
            g_mOscThread = Nothing
        End Sub

        Private Sub ThreadOsc()
            Dim iLastOutputSeqNum As Integer = 0

            While True
                Try
                    If (g_iIndex < 0) Then
                        Return
                    End If

                    If (Not g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.IsRunning) Then
                        Throw New ArgumentException("OSC server is not running")
                    End If

                    m_Data = ClassServiceClient.m_ControllerData(g_iIndex)

                    If (m_Data IsNot Nothing) Then
                        ' We got any new data?
                        If (iLastOutputSeqNum <> m_Data.iOutputSeqNum) Then
                            iLastOutputSeqNum = m_Data.iOutputSeqNum

                            Const ENABLE_TRACKER As Integer = 1
                            'Const ENABLE_TRACKINGREFECNCE As Integer = 4

                            If (m_VmtTracker < 3) Then
                                Throw New ArgumentException("Unsupported VMT tracker id")
                            End If

                            SyncLock _ThreadLock
                                g_mOscDataPack.mOrientation = m_Data.mOrientation
                                g_mOscDataPack.mPosition = m_Data.mPosition * CSng(PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSM_CENTIMETERS_TO_METERS)
                            End SyncLock

                            'Use Right-Handed space for SteamVR 
                            g_UCVirtualMotionTrackerItem.g_mUCVirtualMotionTracker.g_ClassOscServer.Send(
                                New OscMessage(
                                    "/VMT/Room/Driver",
                                    m_VmtTracker, ENABLE_TRACKER, 0.0F,
                                    g_mOscDataPack.mPosition.X,
                                    g_mOscDataPack.mPosition.Y,
                                    g_mOscDataPack.mPosition.Z,
                                    g_mOscDataPack.mOrientation.X,
                                    g_mOscDataPack.mOrientation.Y,
                                    g_mOscDataPack.mOrientation.Z,
                                    g_mOscDataPack.mOrientation.W
                                ))

                            m_FpsOscCounter += 1
                        End If
                    End If

                    ClassPrecisionSleep.Sleep(1)
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

                    If (g_mOscThread IsNot Nothing AndAlso g_mOscThread.IsAlive) Then
                        g_mOscThread.Abort()
                        g_mOscThread.Join()
                        g_mOscThread = Nothing
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
