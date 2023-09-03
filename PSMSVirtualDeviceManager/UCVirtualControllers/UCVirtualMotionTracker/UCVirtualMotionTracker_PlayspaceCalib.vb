Partial Class UCVirtualMotionTracker

    Enum ENUM_PLAYSPACE_CALIBRATION_STATUS
        IDLE
        PREPARE
        SAMPLE_START
        MOVE_FORWARD
        SAMPLE_END
        COMPLETED
    End Enum

    Private Sub Button_PlaySpaceManualCalib_Click(sender As Object, e As EventArgs) Handles Button_PlaySpaceManualCalib.Click
        StartPlayspaceCalibration()

        Panel_PlayCalibSteps.Focus()
    End Sub

    Private Sub LinkLabel_PlayCalibShowSettings_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_PlayCalibShowSettings.LinkClicked
        TabControl_Vmt.SelectedTab = TabPage_Settings
        TabControl_SettingsDevices.SelectedTab = TabPage_SettingsPlayspace

        ' Weird focus  
        CheckBox_PlayCalibEnabled.Focus()
    End Sub

    Public Sub StartPlayspaceCalibration()
        If (g_mPlayspaceCalibrationThread IsNot Nothing AndAlso g_mPlayspaceCalibrationThread.IsAlive) Then
            Return
        End If

        g_mPlayspaceCalibrationThread = New Threading.Thread(AddressOf PlayspaceCalibrationThread)
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
        Else
            Select Case (iStatus)
                Case ENUM_PLAYSPACE_CALIBRATION_STATUS.IDLE
                    Label_PlayCalibTitle.Text = "Playspace Calibration Not Started"
                    Panel_PlayCalibStatus.BackColor = Color.FromArgb(224, 224, 224)

                Case ENUM_PLAYSPACE_CALIBRATION_STATUS.PREPARE,
                        ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_START,
                        ENUM_PLAYSPACE_CALIBRATION_STATUS.MOVE_FORWARD,
                        ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_END
                    Label_PlayCalibTitle.Text = "Playspace Calibration Running"
                    Panel_PlayCalibStatus.BackColor = Color.FromArgb(0, 192, 0)

                Case ENUM_PLAYSPACE_CALIBRATION_STATUS.COMPLETED
                    Label_PlayCalibTitle.Text = "Playspace Calibration Completed"
                    'Panel_PlayCalibStatus.BackColor = Color.FromArgb(0, 192, 0)
                    Panel_PlayCalibStatus.BackColor = Color.FromArgb(224, 224, 224)

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
            ClassUtils.SyncInvoke(Me, Sub() Button_PlaySpaceManualCalib.Enabled = False)

            Dim iControllerID As Integer = ClassUtils.SyncInvokeEx(Of Integer)(Me, Function() ComboBox_PlayCalibControllerID.SelectedItem)
            Dim iPrepTimeSec As Integer = ClassUtils.SyncInvokeEx(Of Integer)(Me, Function() NumericUpDown_PlayCalibPrepTime.Value)
            If (iPrepTimeSec < 1) Then
                iPrepTimeSec = 1
            End If

            Dim mTargetTracker = ClassUtils.SyncInvokeEx(Of UCVirtualMotionTrackerItem)(Me,
                                    Function()
                                        Dim bFound As Boolean = False

                                        For Each mTracker In GetVmtTrackers()
                                            If (mTracker.g_mClassIO.m_Index <> iControllerID) Then
                                                Continue For
                                            End If

                                            Return mTracker
                                        Next

                                        Return Nothing
                                    End Function)

            If (mTargetTracker Is Nothing) Then
                Throw New ArgumentException("Controller is not available!")
            End If

            Try
                While True
                    ' Check if controller even exists
                    Dim mControllerData = g_mUCVirtualControllers.g_mFormMain.g_mPSMoveServiceCAPI.m_ControllerData(iControllerID)
                    If (mControllerData Is Nothing) Then
                        Throw New ArgumentException("Controller is not available!")
                    End If

                    Select Case (iStep)
                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.PREPARE
                            ClassUtils.AsyncInvoke(Me, Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += CInt(100.0F / iPrepTimeSec)

                            If (iPercentage >= 100) Then
                                If (Not mControllerData.m_IsTracking) Then
                                    Throw New ArgumentException("Controller is not being tracked!")
                                End If

                                iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_START
                                iPercentage = 0

                                mTargetTracker.g_mClassIO.StartManualPlayspaceCalibration()
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_START
                            ClassUtils.AsyncInvoke(Me, Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += 50

                            If (iPercentage >= 100) Then
                                If (Not mControllerData.m_IsTracking) Then
                                    Throw New ArgumentException("Controller is not being tracked!")
                                End If

                                iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.MOVE_FORWARD
                                iPercentage = 0
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.MOVE_FORWARD
                            ClassUtils.AsyncInvoke(Me, Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += 20

                            If (iPercentage >= 100) Then
                                If (Not mControllerData.m_IsTracking) Then
                                    Throw New ArgumentException("Controller is not being tracked!")
                                End If

                                iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_END
                                iPercentage = 0

                                mTargetTracker.g_mClassIO.m_ManualPlayspaceCalibrationAllowSave = True
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_END
                            ClassUtils.AsyncInvoke(Me, Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += 50

                            If (iPercentage >= 100) Then
                                If (Not mControllerData.m_IsTracking) Then
                                    Throw New ArgumentException("Controller is not being tracked!")
                                End If

                                If (mTargetTracker.g_mClassIO.m_ManualPlayspaceCalibrationStatus <> UCVirtualMotionTrackerItem.ClassIO.ENUM_PLAYSPACE_CALIBRATION_STATUS.DONE) Then
                                    Throw New ArgumentException("Controller or head-mounted display have not been moved in time! Aborted.")
                                End If

                                iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.COMPLETED
                                iPercentage = 0
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.COMPLETED
                            ClassUtils.AsyncInvoke(Me, Sub() SetPlayspaceCalibrationStatus(iStep, 100))
                            Exit While
                    End Select

                    Threading.Thread.Sleep(1000)
                End While
            Finally
                mTargetTracker.g_mClassIO.StopManualPlayspaceCalibration()
            End Try

        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            ClassUtils.AsyncInvoke(Me, Sub() SetPlayspaceCalibrationStatus(iStep, -1))
            MessageBox.Show(ex.Message, "Playspace Calibration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ClassUtils.AsyncInvoke(Me, Sub() Button_PlaySpaceManualCalib.Enabled = True)
        End Try
    End Sub

End Class
