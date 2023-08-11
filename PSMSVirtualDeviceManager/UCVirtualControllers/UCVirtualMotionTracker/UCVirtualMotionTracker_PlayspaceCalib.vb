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
        TabControl_SettingsDevices.SelectedTab = TabPage_SettingsPSmove

        ' Weird focus 
        Label_ScrollFocus.Focus()
        CheckBox_PlayCalibEnabled.Focus()
    End Sub

    Private Sub ComboBox_PlayCalibControllerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_PlayCalibControllerID.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_PlayspaceSettings.iRecenterControllerID = CInt(ComboBox_PlayCalibControllerID.SelectedItem)
        'g_ClassControllerSettings.SetUnsavedState(True) 'Make it optional
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
                    Panel_PlayCalibStatus.BackColor = Color.FromArgb(0, 192, 0)

            End Select
        End If

        Select Case (iStatus)
            Case ENUM_PLAYSPACE_CALIBRATION_STATUS.IDLE
                ProgressBar_PlayCalibStep1.Value = 0
                ProgressBar_PlayCalibStep2.Value = 0
                ProgressBar_PlayCalibStep3.Value = 0
                ProgressBar_PlayCalibStep4.Value = 0
                ProgressBar_PlayCalibStep5.Value = 0

                ClassPictureBoxQuality_CalibStep1.Image = mImageArrow
                ClassPictureBoxQuality_CalibStep2.Image = mImageArrow
                ClassPictureBoxQuality_CalibStep3.Image = mImageArrow
                ClassPictureBoxQuality_CalibStep4.Image = mImageArrow
                ClassPictureBoxQuality_CalibStep5.Image = mImageArrow

            Case ENUM_PLAYSPACE_CALIBRATION_STATUS.PREPARE
                ProgressBar_PlayCalibStep1.Value = iPercentage
                ProgressBar_PlayCalibStep2.Value = 0
                ProgressBar_PlayCalibStep3.Value = 0
                ProgressBar_PlayCalibStep4.Value = 0
                ProgressBar_PlayCalibStep5.Value = 0

                ClassPictureBoxQuality_CalibStep1.Image = If(bFailed, mImageError, mImageArrow)
                ClassPictureBoxQuality_CalibStep2.Image = Nothing
                ClassPictureBoxQuality_CalibStep3.Image = Nothing
                ClassPictureBoxQuality_CalibStep4.Image = Nothing
                ClassPictureBoxQuality_CalibStep5.Image = Nothing

            Case ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_START
                ProgressBar_PlayCalibStep1.Value = 100
                ProgressBar_PlayCalibStep2.Value = iPercentage
                ProgressBar_PlayCalibStep3.Value = 0
                ProgressBar_PlayCalibStep4.Value = 0
                ProgressBar_PlayCalibStep5.Value = 0

                ClassPictureBoxQuality_CalibStep1.Image = mImageOk
                ClassPictureBoxQuality_CalibStep2.Image = If(bFailed, mImageError, mImageArrow)
                ClassPictureBoxQuality_CalibStep3.Image = Nothing
                ClassPictureBoxQuality_CalibStep4.Image = Nothing
                ClassPictureBoxQuality_CalibStep5.Image = Nothing

            Case ENUM_PLAYSPACE_CALIBRATION_STATUS.MOVE_FORWARD
                ProgressBar_PlayCalibStep1.Value = 100
                ProgressBar_PlayCalibStep2.Value = 100
                ProgressBar_PlayCalibStep3.Value = iPercentage
                ProgressBar_PlayCalibStep4.Value = 0
                ProgressBar_PlayCalibStep5.Value = 0

                ClassPictureBoxQuality_CalibStep1.Image = mImageOk
                ClassPictureBoxQuality_CalibStep2.Image = mImageOk
                ClassPictureBoxQuality_CalibStep3.Image = If(bFailed, mImageError, mImageArrow)
                ClassPictureBoxQuality_CalibStep4.Image = Nothing
                ClassPictureBoxQuality_CalibStep5.Image = Nothing

            Case ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_END
                ProgressBar_PlayCalibStep1.Value = 100
                ProgressBar_PlayCalibStep2.Value = 100
                ProgressBar_PlayCalibStep3.Value = 100
                ProgressBar_PlayCalibStep4.Value = iPercentage
                ProgressBar_PlayCalibStep5.Value = 0

                ClassPictureBoxQuality_CalibStep1.Image = mImageOk
                ClassPictureBoxQuality_CalibStep2.Image = mImageOk
                ClassPictureBoxQuality_CalibStep3.Image = mImageOk
                ClassPictureBoxQuality_CalibStep4.Image = If(bFailed, mImageError, mImageArrow)
                ClassPictureBoxQuality_CalibStep5.Image = Nothing
            Case ENUM_PLAYSPACE_CALIBRATION_STATUS.COMPLETED
                ProgressBar_PlayCalibStep1.Value = 100
                ProgressBar_PlayCalibStep2.Value = 100
                ProgressBar_PlayCalibStep3.Value = 100
                ProgressBar_PlayCalibStep4.Value = 100
                ProgressBar_PlayCalibStep5.Value = 100

                ClassPictureBoxQuality_CalibStep1.Image = mImageOk
                ClassPictureBoxQuality_CalibStep2.Image = mImageOk
                ClassPictureBoxQuality_CalibStep3.Image = mImageOk
                ClassPictureBoxQuality_CalibStep4.Image = mImageOk
                ClassPictureBoxQuality_CalibStep5.Image = mImageOk

        End Select
    End Sub

    Private Sub PlayspaceCalibrationThread()
        Dim iStep As ENUM_PLAYSPACE_CALIBRATION_STATUS = ENUM_PLAYSPACE_CALIBRATION_STATUS.PREPARE
        Dim iPercentage As Integer = 0

        Try
            Dim iControllerID As Integer = CInt(Me.Invoke(Function() ComboBox_PlayCalibControllerID.SelectedItem))

            ' Check if controller even exists
            Dim mControllerData = g_mUCVirtualControllers.g_mFormMain.g_mPSMoveServiceCAPI.m_ControllerData(iControllerID)
            If (mControllerData Is Nothing) Then
                Throw New ArgumentException("Controller is not available!")
            End If

            Dim mTargetTracker = DirectCast(Me.Invoke(
                Function()
                    Dim bFound As Boolean = False

                    For Each mTracker In GetVmtTrackers()
                        If (mTracker.g_mClassIO.m_Index <> iControllerID) Then
                            Continue For
                        End If

                        Return mTracker
                    Next

                    Return Nothing
                End Function), UCVirtualMotionTrackerItem)

            If (mTargetTracker Is Nothing) Then
                Throw New ArgumentException("Controller is not available!")
            End If

            Try
                While True
                    Select Case (iStep)
                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.PREPARE
                            Me.BeginInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += 20

                            If (iPercentage >= 100) Then
                                iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_START
                                iPercentage = 0

                                mTargetTracker.g_mClassIO.StartManualPlayspaceCalibration()
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_START
                            Me.BeginInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += 50

                            If (iPercentage >= 100) Then
                                iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.MOVE_FORWARD
                                iPercentage = 0
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.MOVE_FORWARD
                            Me.BeginInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += 20

                            If (iPercentage >= 100) Then
                                iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_END
                                iPercentage = 0

                                mTargetTracker.g_mClassIO.m_ManualPlaysapceCalibrationAllowSave = True
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.SAMPLE_END
                            Me.BeginInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, iPercentage))

                            iPercentage += 50

                            If (iPercentage >= 100) Then
                                If (mTargetTracker.g_mClassIO.m_ManualPlayspaceCalibrationStatus = UCVirtualMotionTrackerItem.ClassIO.ENUM_PLAYSPACE_CALIBRATION_STATUS.DONE) Then
                                    iStep = ENUM_PLAYSPACE_CALIBRATION_STATUS.COMPLETED
                                    iPercentage = 0
                                Else
                                    Throw New ArgumentException("Controller and head mount display have not been moved in time! Aborted.")
                                End If
                            End If

                        Case ENUM_PLAYSPACE_CALIBRATION_STATUS.COMPLETED
                            Me.BeginInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, 100))
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
            Me.BeginInvoke(Sub() SetPlayspaceCalibrationStatus(iStep, -1))
            MessageBox.Show(ex.Message, "Playspace Calibration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
