Imports System.Numerics
Imports System.Text

Public Class UCControllerAttachmentsItem
    Shared _ThreadLock As New Object

    Public g_mUCControllerAttachments As UCControllerAttachments

    Public g_mClassIO As ClassIO
    Public g_mClassConfig As ClassConfig

    Private g_sNickname As String = ""

    Private g_bIgnoreEvents As Boolean = False
    Private g_bIgnoreUnsaved As Boolean = False

    Private g_iStatusHideHeight As Integer = 0
    Private g_iStatusShowHeight As Integer = g_iStatusHideHeight
    Private g_iStatusPipeFps As Integer = 0
    Private g_bHasStatusError As Boolean = False
    Private g_sHasStatusErrormessage As New KeyValuePair(Of String, String)("", "")

    Public Sub New(iControllerID As Integer, _UCControllerAttachments As UCControllerAttachments)
        g_mUCControllerAttachments = _UCControllerAttachments

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

            ComboBox_ParentControllerID.Items.Clear()
            For i = -1 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                ComboBox_ParentControllerID.Items.Add(CStr(i))
            Next
            ComboBox_ParentControllerID.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        g_mClassIO = New ClassIO()
        g_mClassConfig = New ClassConfig(Me)

        g_mClassIO.m_Index = CInt(ComboBox_ControllerID.SelectedItem)
        g_mClassIO.Enable()

        SetTrackerNameText()

        SetUnsavedState(False)

        CreateControl()

        ' Hide timeout error
        Panel_Status.Visible = False
        g_iStatusHideHeight = (Me.Height - Panel_Status.Height)
        g_iStatusShowHeight = Me.Height
        Me.Height = g_iStatusHideHeight
    End Sub

    Property m_Nickname As String
        Get
            Return g_sNickname
        End Get
        Set(value As String)
            g_sNickname = value
            SetTrackerNameText()
        End Set
    End Property

    Private Sub SetTrackerNameText()
        Dim iControllerID As Integer = CInt(ComboBox_ControllerID.SelectedItem)

        TextBox_TrackerName.Text = "Attachment Name: "

        If (iControllerID > -1 AndAlso Not String.IsNullOrEmpty(m_Nickname)) Then
            TextBox_TrackerName.Text &= m_Nickname
        Else
            TextBox_TrackerName.Text &= "Invalid"
        End If
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
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ComboBox_ControllerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_ControllerID.SelectedIndexChanged
        SetTrackerNameText()

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

    Private Sub ComboBox_ParentControllerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_ParentControllerID.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassIO.m_ParentController = CInt(ComboBox_ParentControllerID.SelectedItem)
        SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_JointOffsetX_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_JointOffsetX.ValueChanged
        g_mClassIO.m_JointOffset = New Vector3(NumericUpDown_JointOffsetX.Value, NumericUpDown_JointOffsetY.Value, NumericUpDown_JointOffsetZ.Value)
        SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_JointOffsetY_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_JointOffsetY.ValueChanged
        g_mClassIO.m_JointOffset = New Vector3(NumericUpDown_JointOffsetX.Value, NumericUpDown_JointOffsetY.Value, NumericUpDown_JointOffsetZ.Value)
        SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_JointOffsetZ_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_JointOffsetZ.ValueChanged
        g_mClassIO.m_JointOffset = New Vector3(NumericUpDown_JointOffsetX.Value, NumericUpDown_JointOffsetY.Value, NumericUpDown_JointOffsetZ.Value)
        SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_ControllerOffsetX_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_ControllerOffsetX.ValueChanged
        g_mClassIO.m_ControllerOffset = New Vector3(NumericUpDown_ControllerOffsetX.Value, NumericUpDown_ControllerOffsetY.Value, NumericUpDown_ControllerOffsetZ.Value)
        SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_ControllerOffsetY_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_ControllerOffsetY.ValueChanged
        g_mClassIO.m_ControllerOffset = New Vector3(NumericUpDown_ControllerOffsetX.Value, NumericUpDown_ControllerOffsetY.Value, NumericUpDown_ControllerOffsetZ.Value)
        SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_ControllerOffsetZ_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_ControllerOffsetZ.ValueChanged
        g_mClassIO.m_ControllerOffset = New Vector3(NumericUpDown_ControllerOffsetX.Value, NumericUpDown_ControllerOffsetY.Value, NumericUpDown_ControllerOffsetZ.Value)
        SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_JointYawCorrection_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_JointYawCorrection.ValueChanged
        g_mClassIO.m_JointYawCorrection = CInt(NumericUpDown_JointYawCorrection.Value)
        SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_ControllerYawCorrection_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_ControllerYawCorrection.ValueChanged
        g_mClassIO.m_ControllerYawCorrection = CInt(NumericUpDown_ControllerYawCorrection.Value)
        SetUnsavedState(True)
    End Sub

    Private Sub Button_JointNegX_Click(sender As Object, e As EventArgs)
        NumericUpDown_JointOffsetX.Value -= 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_JointNegY_Click(sender As Object, e As EventArgs)
        NumericUpDown_JointOffsetY.Value -= 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_JointNegZ_Click(sender As Object, e As EventArgs)
        NumericUpDown_JointOffsetZ.Value -= 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_JointPosX_Click(sender As Object, e As EventArgs)
        NumericUpDown_JointOffsetX.Value += 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_JointPosY_Click(sender As Object, e As EventArgs)
        NumericUpDown_JointOffsetY.Value += 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_JointPosZ_Click(sender As Object, e As EventArgs)
        NumericUpDown_JointOffsetZ.Value += 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_ControllerNegX_Click(sender As Object, e As EventArgs)
        NumericUpDown_ControllerOffsetX.Value -= 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_ControllerNegY_Click(sender As Object, e As EventArgs)
        NumericUpDown_ControllerOffsetY.Value -= 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_ControllerNegZ_Click(sender As Object, e As EventArgs)
        NumericUpDown_ControllerOffsetZ.Value -= 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_ControllerPosX_Click(sender As Object, e As EventArgs)
        NumericUpDown_ControllerOffsetX.Value += 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_ControllerPosY_Click(sender As Object, e As EventArgs)
        NumericUpDown_ControllerOffsetY.Value += 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_ControllerPosZ_Click(sender As Object, e As EventArgs)
        NumericUpDown_ControllerOffsetZ.Value += 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_JointNegYaw_Click(sender As Object, e As EventArgs)
        NumericUpDown_JointYawCorrection.Value -= 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_JointPosYaw_Click(sender As Object, e As EventArgs)
        NumericUpDown_JointYawCorrection.Value += 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_ControllerNegYaw_Click(sender As Object, e As EventArgs)
        NumericUpDown_ControllerYawCorrection.Value -= 5
        SetUnsavedState(True)
    End Sub

    Private Sub Button_ControllerPosYaw_Click(sender As Object, e As EventArgs)
        NumericUpDown_ControllerYawCorrection.Value += 5
        SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_JointOnly_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_JointOnly.CheckedChanged
        g_mClassIO.m_OnlyJointOffset = CheckBox_JointOnly.Checked
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

    Private Sub LinkLabel_EditName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_EditName.LinkClicked
        Dim sName As String = InputBox("Enter a new attachment name:", "New attachment name", m_Nickname)

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

    Private Sub TimerFPS_Tick(sender As Object, e As EventArgs) Handles TimerFPS.Tick
        TimerFPS.Stop()

        Try
            Dim iFpsPipeCounter As Integer = g_mClassIO.m_FpsPipeCounter

            g_iStatusPipeFps = iFpsPipeCounter

            If (Me.Visible) Then
                TextBox_Fps.Text = String.Format("Pipe IO: {0}/s", iFpsPipeCounter)
            End If

            g_mClassIO.m_FpsPipeCounter = 0
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
                ' Check if index valid
                If (g_mClassIO IsNot Nothing AndAlso g_mClassIO.m_Index < 0) Then
                    sTitle = "Controller attachment is disabled"

                    Dim sText As New Text.StringBuilder
                    sText.AppendLine("The controller id has not been set.")

                    sMessage = sText.ToString
                    iStatusType = 2

                    Exit While
                End If

                ' Check if connected
                If (g_iStatusPipeFps < 1) Then
                    sTitle = "Controller attachment is not connected to PSMoveServiceEx"

                    Dim sText As New Text.StringBuilder
                    sText.AppendLine("The controller attachment is currently not connected. Please select a controller id that has attachments enabled using the 'PositionExternalAttachment' filter or PSMoveServiceEx is not running.")

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

        Private g_mJointOffset As Vector3 = Vector3.Zero
        Private g_mControllerOffset As Vector3 = Vector3.Zero
        Private g_iJointYawCorrection As Integer = 0
        Private g_iControllerYawCorrection As Integer = 0
        Private g_bOnlyJointOffset As Boolean = False

        Private g_iFpsPipeCounter As Integer = 0

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

        Property m_JointOffset As Vector3
            Get
                SyncLock _ThreadLock
                    Return g_mJointOffset
                End SyncLock
            End Get
            Set(value As Vector3)
                SyncLock _ThreadLock
                    g_mJointOffset = value
                End SyncLock
            End Set
        End Property

        Property m_ControllerOffset As Vector3
            Get
                SyncLock _ThreadLock
                    Return g_mControllerOffset
                End SyncLock
            End Get
            Set(value As Vector3)
                SyncLock _ThreadLock
                    g_mControllerOffset = value
                End SyncLock
            End Set
        End Property

        Property m_JointYawCorrection As Integer
            Get
                SyncLock _ThreadLock
                    Return g_iJointYawCorrection
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock _ThreadLock
                    g_iJointYawCorrection = value
                End SyncLock
            End Set
        End Property

        Property m_ControllerYawCorrection As Integer
            Get
                SyncLock _ThreadLock
                    Return g_iControllerYawCorrection
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock _ThreadLock
                    g_iControllerYawCorrection = value
                End SyncLock
            End Set
        End Property

        Property m_OnlyJointOffset As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bOnlyJointOffset
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bOnlyJointOffset = value
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

                    Using mPipe As New IO.Pipes.NamedPipeClientStream(".", "PSMoveSerivceEx\AttachPSmoveStream_" & g_iIndex, IO.Pipes.PipeDirection.Out)
                        ' The thread when aborting will hang if we dont put a timeout.
                        mPipe.Connect(5000)

                        While True
                            Dim iBytes = New Byte(512) {}

                            Using mMem As New IO.MemoryStream(iBytes)
                                Using Bw As New IO.BinaryWriter(mMem)
                                    SyncLock _ThreadLock
                                        ' Set target parent controller
                                        Bw.Write(Encoding.ASCII.GetBytes(CStr(m_ParentController)))
                                        Bw.Write(CByte(0))

                                        ' Send joint offset
                                        Bw.Write(Encoding.ASCII.GetBytes(m_JointOffset.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(m_JointOffset.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(m_JointOffset.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))

                                        ' Send controller offset 
                                        Dim mControllerOffset As Vector3
                                        If (m_OnlyJointOffset) Then
                                            mControllerOffset = Vector3.Zero
                                        Else
                                            mControllerOffset = m_ControllerOffset
                                        End If

                                        Bw.Write(Encoding.ASCII.GetBytes(mControllerOffset.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(mControllerOffset.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(mControllerOffset.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))

                                        ' Send joint yaw correction orientation 
                                        Dim mNewJointYawCorrection = Quaternion.CreateFromAxisAngle(New Vector3(0, 0, 1), CSng(g_iJointYawCorrection * (Math.PI / 180)))

                                        Bw.Write(Encoding.ASCII.GetBytes(mNewJointYawCorrection.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(mNewJointYawCorrection.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes((-mNewJointYawCorrection.Y).ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(mNewJointYawCorrection.W.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))

                                        ' Send controller yaw correction orientation 
                                        Dim mNewControllerYawCorrection = Quaternion.CreateFromAxisAngle(New Vector3(0, 0, 1), CSng(g_iControllerYawCorrection * (Math.PI / 180)))

                                        Bw.Write(Encoding.ASCII.GetBytes(mNewControllerYawCorrection.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(mNewControllerYawCorrection.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes((-mNewControllerYawCorrection.Y).ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))
                                        Bw.Write(Encoding.ASCII.GetBytes(mNewControllerYawCorrection.W.ToString(Globalization.CultureInfo.InvariantCulture)))
                                        Bw.Write(CByte(0))

                                        g_iFpsPipeCounter += 1
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
        Private g_mUCRemoteDeviceItem As UCControllerAttachmentsItem

        Public Sub New(_UCRemoteDeviceItem As UCControllerAttachmentsItem)
            g_mUCRemoteDeviceItem = _UCRemoteDeviceItem
        End Sub

        Public Sub SaveConfig()
            If (CInt(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedItem) < 0) Then
                Return
            End If

            Dim sDevicePath As String = CType(g_mUCRemoteDeviceItem.ComboBox_ControllerID.SelectedItem, String)

            Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_ATTACHMENT, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    SyncLock _ThreadLock
                        Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Joint.X", g_mUCRemoteDeviceItem.g_mClassIO.m_JointOffset.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Joint.Y", g_mUCRemoteDeviceItem.g_mClassIO.m_JointOffset.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Joint.Z", g_mUCRemoteDeviceItem.g_mClassIO.m_JointOffset.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "JointYawCorrection", CStr(g_mUCRemoteDeviceItem.g_mClassIO.m_JointYawCorrection)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Controller.X", g_mUCRemoteDeviceItem.g_mClassIO.m_ControllerOffset.X.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Controller.Y", g_mUCRemoteDeviceItem.g_mClassIO.m_ControllerOffset.Y.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Controller.Z", g_mUCRemoteDeviceItem.g_mClassIO.m_ControllerOffset.Z.ToString(Globalization.CultureInfo.InvariantCulture)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "ControllerYawCorrection", CStr(g_mUCRemoteDeviceItem.g_mClassIO.m_ControllerYawCorrection)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "ParentControllerID", CStr(g_mUCRemoteDeviceItem.ComboBox_ParentControllerID.SelectedIndex)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "OnlyJointOffset", If(g_mUCRemoteDeviceItem.g_mClassIO.m_OnlyJointOffset, "True", "False")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Nickname", g_mUCRemoteDeviceItem.g_sNickname))

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

            Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_ATTACHMENT, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    Dim iJointX As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Joint.X", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iJointY As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Joint.Y", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iJointZ As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Joint.Z", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iJointYawCorrection As Integer = Integer.Parse(mIni.ReadKeyValue(sDevicePath, "JointYawCorrection", "0"))
                    Dim iControllerX As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Controller.X", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iControllerY As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Controller.Y", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iControllerZ As Single = Single.Parse(mIni.ReadKeyValue(sDevicePath, "Controller.Z", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    Dim iControllerYawCorrection As Integer = Integer.Parse(mIni.ReadKeyValue(sDevicePath, "ControllerYawCorrection", "0"))
                    Dim bOnlyJointOffset As Boolean = (mIni.ReadKeyValue(sDevicePath, "OnlyJointOffset", "False") = "True")

                    SetNumericUpDownClamp(g_mUCRemoteDeviceItem.NumericUpDown_JointOffsetX, iJointX)
                    SetNumericUpDownClamp(g_mUCRemoteDeviceItem.NumericUpDown_JointOffsetY, iJointY)
                    SetNumericUpDownClamp(g_mUCRemoteDeviceItem.NumericUpDown_JointOffsetZ, iJointZ)
                    SetNumericUpDownClamp(g_mUCRemoteDeviceItem.NumericUpDown_JointYawCorrection, iJointYawCorrection)

                    SetNumericUpDownClamp(g_mUCRemoteDeviceItem.NumericUpDown_ControllerOffsetX, iControllerX)
                    SetNumericUpDownClamp(g_mUCRemoteDeviceItem.NumericUpDown_ControllerOffsetY, iControllerY)
                    SetNumericUpDownClamp(g_mUCRemoteDeviceItem.NumericUpDown_ControllerOffsetZ, iControllerZ)
                    SetNumericUpDownClamp(g_mUCRemoteDeviceItem.NumericUpDown_ControllerYawCorrection, iControllerYawCorrection)

                    SetComboBoxClamp(g_mUCRemoteDeviceItem.ComboBox_ParentControllerID, CInt(mIni.ReadKeyValue(sDevicePath, "ParentControllerID", "-1")))

                    g_mUCRemoteDeviceItem.CheckBox_JointOnly.Checked = bOnlyJointOffset

                    g_mUCRemoteDeviceItem.m_Nickname = CStr(mIni.ReadKeyValue(sDevicePath, "Nickname", ""))
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
End Class
