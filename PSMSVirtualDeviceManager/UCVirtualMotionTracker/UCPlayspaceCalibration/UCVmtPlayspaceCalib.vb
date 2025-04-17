Public Class UCVmtPlayspaceCalib
    Public g_UCVirtualMotionTracker As UCVirtualMotionTracker

    Private g_bInit As Boolean = False
    Private g_bIgnoreEvents As Boolean = True
    Private g_mPlayspaceCalibrationThread As Threading.Thread = Nothing

    Public Sub New(_UCVirtualMotionTracker As UCVirtualMotionTracker)
        g_UCVirtualMotionTracker = _UCVirtualMotionTracker

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        UcInformation1.m_ReadMoreAction = AddressOf ShowSettings

        Try
            g_bIgnoreEvents = True

            ComboBox_PlayCalibControllerID.Items.Clear()
            For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                ComboBox_PlayCalibControllerID.Items.Add(i)
            Next

            ComboBox_PlayCalibControllerID.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        CreateControl()
    End Sub

    Private Sub ShowSettings()
        g_UCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_UCVirtualMotionTracker.TabPage_Settings
        g_UCVirtualMotionTracker.g_UCVmtSettings.TabControl_SettingsDevices.SelectedTab = g_UCVirtualMotionTracker.g_UCVmtSettings.TabPage_SettingsPSmove

        ' Weird focus  
        g_UCVirtualMotionTracker.g_UCVmtSettings.Label_ScrollFocus.Focus()
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

        SetPlayspaceCalibrationStatus(ENUM_PLAYSPACE_CALIBRATION_STATUS.IDLE, 0)
    End Sub

    Enum ENUM_PLAYSPACE_CALIBRATION_STATUS
        IDLE
        PREPARE
        SAMPLE_START
        MOVE_FORWARD
        SAMPLE_END
        COMPLETED
    End Enum

    Public Sub Button_PlaySpaceManualCalib_Click()
        Button_PlaySpaceManualCalib_Click(Nothing, Nothing)
    End Sub

    Private Sub Button_PlaySpaceManualCalib_Click(sender As Object, e As EventArgs) Handles Button_PlaySpaceManualCalib.Click
        StartPlayspaceCalibration()

        Panel_PlayCalibSteps.Focus()
    End Sub

    Private Sub ComboBox_PlayCalibControllerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_PlayCalibControllerID.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_PlayspaceSettings.m_CalibrationControllerId = ComboBox_PlayCalibControllerID.SelectedIndex
        g_UCVirtualMotionTracker.g_ClassSettings.SaveSettings(UCVirtualMotionTracker.ENUM_SETTINGS_SAVE_TYPE_FLAGS.PLAYSPACE_CALIB_CONTROLLER)
    End Sub

    Private Sub LinkLabel_PlayCalibShowSettings_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_PlayCalibShowSettings.LinkClicked
        g_UCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_UCVirtualMotionTracker.TabPage_Settings
        g_UCVirtualMotionTracker.g_UCVmtSettings.TabControl_SettingsDevices.SelectedTab = g_UCVirtualMotionTracker.g_UCVmtSettings.TabPage_SettingsPlayspace

        ' Weird focus  
        g_UCVirtualMotionTracker.g_UCVmtSettings.CheckBox_PlayCalibEnabled.Focus()
    End Sub

    Public Sub StartPlayspaceCalibration()
        If (g_mPlayspaceCalibrationThread IsNot Nothing AndAlso g_mPlayspaceCalibrationThread.IsAlive) Then
            Return
        End If

        g_mPlayspaceCalibrationThread = New Threading.Thread(AddressOf PlayspaceCalibrationThread)
        g_mPlayspaceCalibrationThread.Priority = Threading.ThreadPriority.Lowest
        g_mPlayspaceCalibrationThread.IsBackground = True
        g_mPlayspaceCalibrationThread.Start()
    End Sub

    Public Sub SetPlayspaceCalibrationStatus(iStatus As ENUM_PLAYSPACE_CALIBRATION_STATUS, iPercentage As Integer)
        Dim mImageOk = My.Resources.netshell_1610_32x32_32
        Dim mImageArrow = My.Resources.netshell_1607_32x32_32
        Dim mImageError = My.Resources.netshell_1608_32x32_32

        Dim bFailed As Boolean = (iPercentage < 0)
        If (iPercentage < 0) Then
            iPercentage = 0
        End If
        If (iPercentage > 100) Then
            iPercentage = 100
        End If

        If (bFailed) Then
            Label_PlayCalibTitle.Text = "Playspace Calibration Failed"
            Panel_PlayCalibStatus.BackColor = Color.FromArgb(192, 0, 0)

            g_UCVirtualMotionTracker.g_mFormMain.Button_NavPlayCalibStatus.Text = "Playspace Calibration Failed"
            g_UCVirtualMotionTracker.g_mFormMain.Button_NavPlayCalibStatus.Image = My.Resources.Status_RED_16
        Else
            Select Case (iStatus)
                Case ENUM_PLAYSPACE_CALIBRATION_STATUS.IDLE
                    Label_PlayCalibTitle.Text = "Playspace Calibration Not Started"
                    Panel_PlayCalibStatus.BackColor = Color.FromArgb(224, 224, 224)

                    g_UCVirtualMotionTracker.g_mFormMain.Button_NavPlayCalibStatus.Text = "Playspace Calibration Idle"
                    g_UCVirtualMotionTracker.g_mFormMain.Button_NavPlayCalibStatus.Image = My.Resources.Status_WHITE_16

                Case ENUM_PLAYSPACE_CALIBRATION_STATUS.PREPARE,
                        ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_START,
                        ENUM_PLAYSPACE_CALIBRATION_STATUS.MOVE_FORWARD,
                        ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_END
                    Label_PlayCalibTitle.Text = "Playspace Calibration Running"
                    Panel_PlayCalibStatus.BackColor = Color.FromArgb(0, 192, 0)

                    g_UCVirtualMotionTracker.g_mFormMain.Button_NavPlayCalibStatus.Text = "Playspace Calibration Running"
                    g_UCVirtualMotionTracker.g_mFormMain.Button_NavPlayCalibStatus.Image = My.Resources.Status_GREEN_16

                Case ENUM_PLAYSPACE_CALIBRATION_STATUS.COMPLETED
                    Label_PlayCalibTitle.Text = "Playspace Calibration Completed"
                    'Panel_PlayCalibStatus.BackColor = Color.FromArgb(0, 192, 0)
                    Panel_PlayCalibStatus.BackColor = Color.FromArgb(224, 224, 224)

                    g_UCVirtualMotionTracker.g_mFormMain.Button_NavPlayCalibStatus.Text = "Playspace Calibration Idle"
                    g_UCVirtualMotionTracker.g_mFormMain.Button_NavPlayCalibStatus.Image = My.Resources.Status_WHITE_16
            End Select
        End If

        Dim mProgressBars As ProgressBar() = {
            ProgressBar_PlayCalibStep1,
            ProgressBar_PlayCalibStep2,
            ProgressBar_PlayCalibStep3,
            ProgressBar_PlayCalibStep4,
            ProgressBar_PlayCalibStep5
        }

        Dim mPictureBoxes As PictureBox() = {
            ClassPictureBoxQuality_CalibStep1,
            ClassPictureBoxQuality_CalibStep2,
            ClassPictureBoxQuality_CalibStep3,
            ClassPictureBoxQuality_CalibStep4,
            ClassPictureBoxQuality_CalibStep5
        }

        Select Case (iStatus)
            Case ENUM_PLAYSPACE_CALIBRATION_STATUS.IDLE
                For i = 0 To mProgressBars.Length - 1
                    InternalSetProgressBar(mProgressBars(i), 0)
                Next
                For i = 0 To mPictureBoxes.Length - 1
                    mPictureBoxes(i).Image = mImageArrow
                Next

            Case ENUM_PLAYSPACE_CALIBRATION_STATUS.PREPARE,
                 ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_START,
                 ENUM_PLAYSPACE_CALIBRATION_STATUS.MOVE_FORWARD,
                 ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_END
                For i = 0 To mProgressBars.Length - 1
                    If (i < (iStatus - 1)) Then
                        InternalSetProgressBar(mProgressBars(i), 100)
                        Continue For
                    End If

                    If (i = (iStatus - 1)) Then
                        InternalSetProgressBar(mProgressBars(i), iPercentage)
                        Continue For
                    End If

                    InternalSetProgressBar(mProgressBars(i), 0)
                Next

                For i = 0 To mPictureBoxes.Length - 1
                    If (i < (iStatus - 1)) Then
                        mPictureBoxes(i).Image = mImageOk
                        Continue For
                    End If

                    If (i = (iStatus - 1)) Then
                        mPictureBoxes(i).Image = If(bFailed, mImageError, mImageArrow)
                        Continue For
                    End If

                    mPictureBoxes(i).Image = Nothing
                Next

            Case ENUM_PLAYSPACE_CALIBRATION_STATUS.COMPLETED
                For i = 0 To mProgressBars.Length - 1
                    InternalSetProgressBar(mProgressBars(i), 100)
                Next
                For i = 0 To mPictureBoxes.Length - 1
                    mPictureBoxes(i).Image = mImageOk
                Next
        End Select

    End Sub

    ' Disable progressbar bar animations
    Private Sub InternalSetProgressBar(mControl As ProgressBar, iValue As Integer)
        Dim iOrgMax = mControl.Maximum

        mControl.Maximum = mControl.Maximum + 1

        mControl.Value = iValue + 1
        mControl.Value = iValue

        mControl.Maximum = iOrgMax
    End Sub

    Private Sub PlayspaceCalibrationThread()
        Dim iStep As ENUM_PLAYSPACE_CALIBRATION_STATUS = ENUM_PLAYSPACE_CALIBRATION_STATUS.PREPARE
        Dim iPercentage As Integer = 0

        Try
            ClassUtils.SyncInvoke(Sub() Button_PlaySpaceManualCalib.Enabled = False)

            Dim iControllerID As Integer = ClassUtils.SyncInvokeEx(Of Integer)(Function() ComboBox_PlayCalibControllerID.SelectedItem)
            Dim iPrepTimeSec As Integer = ClassUtils.SyncInvokeEx(Of Integer)(Function() NumericUpDown_PlayCalibPrepTime.Value)
            If (iPrepTimeSec < 1) Then
                iPrepTimeSec = 1
            End If

            Dim mTargetTracker = ClassUtils.SyncInvokeEx(Of UCVirtualMotionTrackerItem)(
                                    Function()
                                        Dim bFound As Boolean = False

                                        For Each mTracker In g_UCVirtualMotionTracker.g_UCVmtTrackers.GetVmtTrackers()
                                            If (mTracker.g_mClassIO.m_Index <> iControllerID) Then
                                                Continue For
                                            End If

                                            Return mTracker
                                        Next

                                        Return Nothing
                                    End Function)

            If (mTargetTracker Is Nothing) Then
                Throw New ArgumentException("Controller is not available.")
            End If

            Try
                While True
                    ' Check if controller even exists
                    Dim mControllerData = g_UCVirtualMotionTracker.g_mFormMain.g_mPSMoveServiceCAPI.m_ControllerData(iControllerID)
                    If (mControllerData Is Nothing) Then
                        Throw New ArgumentException("Controller is not available.")
                    End If

                    Select Case (iStep)
                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.PREPARE
                            ClassUtils.AsyncInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += CInt(100.0F / iPrepTimeSec)

                            If (iPercentage >= 100) Then
                                If (Not mControllerData.m_IsTracking) Then
                                    Throw New ArgumentException("Controller is not being tracked.")
                                End If

                                iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_START
                                iPercentage = 0

                                mTargetTracker.g_mClassIO.m_PlayspaceCalibration.StartCalibration()
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_START
                            ClassUtils.AsyncInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += 50

                            If (iPercentage >= 100) Then
                                If (Not mControllerData.m_IsTracking) Then
                                    Throw New ArgumentException("Controller is not being tracked.")
                                End If

                                Select Case (mTargetTracker.g_mClassIO.m_PlayspaceCalibration.GetStatus)
                                    Case UCVirtualMotionTrackerItem.ClassIO.STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.FAILED
                                        ' Failure. Either no HMD found or PSMS-EX HMD used.
                                        Throw New ArgumentException("Invalid Head-Mounted Display for playspace calibration.")
                                End Select

                                iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.MOVE_FORWARD
                                iPercentage = 0
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.MOVE_FORWARD
                            ClassUtils.AsyncInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += 20

                            If (iPercentage >= 100) Then
                                If (Not mControllerData.m_IsTracking) Then
                                    Throw New ArgumentException("Controller is not being tracked.")
                                End If

                                iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_END
                                iPercentage = 0
                                mTargetTracker.g_mClassIO.m_PlayspaceCalibration.m_FinishCalibration = True
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_END
                            ClassUtils.AsyncInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += 50

                            If (iPercentage >= 100) Then
                                If (Not mControllerData.m_IsTracking) Then
                                    Throw New ArgumentException("Controller is not being tracked.")
                                End If

                                Select Case (mTargetTracker.g_mClassIO.m_PlayspaceCalibration.GetStatus)
                                    Case UCVirtualMotionTrackerItem.ClassIO.STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.DONE
                                        ' All good

                                    Case UCVirtualMotionTrackerItem.ClassIO.STURC_PLAYSPACE_CALIBRATION_STATUS.ENUM_PLAYSPACE_CALIBRATION_STATUS.FAILED
                                        ' Failure. Either no HMD found or PSMS-EX HMD used.
                                        Throw New ArgumentException("Invalid Head-Mounted Display for playspace calibration.")

                                    Case Else
                                        ' Has not been moved at all
                                        Throw New ArgumentException("Controller or Head-Mounted Display have not been moved in time.")
                                End Select

                                iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.COMPLETED
                                iPercentage = 0
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.COMPLETED
                            ClassUtils.AsyncInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, 100))
                            Exit While
                    End Select

                    Threading.Thread.Sleep(1000)
                End While
            Finally
                mTargetTracker.g_mClassIO.m_PlayspaceCalibration.StopCalibration()
            End Try

        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            ClassUtils.AsyncInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, -1))
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        Finally
            ClassUtils.AsyncInvoke(Sub() Button_PlaySpaceManualCalib.Enabled = True)
        End Try
    End Sub

    Private Sub CleanUp()
        If (g_mPlayspaceCalibrationThread IsNot Nothing AndAlso g_mPlayspaceCalibrationThread.IsAlive) Then
            g_mPlayspaceCalibrationThread.Abort()
            g_mPlayspaceCalibrationThread.Join()
            g_mPlayspaceCalibrationThread = Nothing
        End If
    End Sub
End Class
