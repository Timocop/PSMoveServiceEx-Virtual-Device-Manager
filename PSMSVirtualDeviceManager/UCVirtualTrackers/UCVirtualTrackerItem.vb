Imports System.Numerics
Imports System.Runtime.InteropServices

Public Class UCVirtualTrackerItem
    Const MAX_PSMOVESERIVCE_TRACKERS = 8
    Const PROBE_MULTIPLY = 64
    Const PS4_TRACKER_POSE_ANGLE_MAX_DIVIATION = 0.5F

    Const PS4CAM_EYE_STARTPADDING As Integer = 48
    Const PS4CAM_EYE_HEIGHT_PADDING As Integer = 8
    Const PS4CAM_EYE_WDITH As Integer = 1280

    Public g_mClassCaptureLogic As ClassCaptureLogic

    Public g_mUCVirtualTrackers As UCVirtualTrackers

    Private g_mStatusThread As Threading.Thread = Nothing
    Private g_iStatisLastTrackerId As Integer = -1
    Private g_iStatusHideHeight As Integer = 0
    Private g_iStatusShowHeight As Integer = g_iStatusHideHeight
    Private g_bHasStatusError As Boolean = False
    Private g_mStatusConfigDate As New Dictionary(Of String, Date)
    Private g_bStatusDistortionError As Boolean = False
    Private g_bStatusDistortionAngle As Single = 0.0F
    Private g_bStatusDistortionWarning As Boolean = False
    Private g_bStatusBadPoseError As Boolean = False
    Private g_mStatusBadPoseQuatentions As New Dictionary(Of String, Quaternion)

    Private g_mMessageLabel As Label

    Private g_iPreviousTrackerIdSelectedIndex As Integer = -1

    Public g_bIgnoreEvents As Boolean = False
    Public g_bIgnoreUnsaved As Boolean = False

    Private g_iCaptureFps As Integer = 0
    Private g_iPipeFps As Integer = 0

    Structure STRUC_CAMERA_TRACKER_ID_ITEM
        Dim iTrackerId_1 As Integer
        Dim iTrackerId_2 As Integer

        Public Sub New(_TrackerId_1 As Integer, _TrackerId_2 As Integer)
            iTrackerId_1 = _TrackerId_1
            iTrackerId_2 = _TrackerId_2
        End Sub

        Public Overrides Function ToString() As String
            If (iTrackerId_2 > -1) Then
                Return String.Format("{0} & {1}", CStr(iTrackerId_1), CStr(iTrackerId_2))
            Else
                Return CStr(iTrackerId_1)
            End If
        End Function
    End Structure

    Structure STRUC_CAMERA_DISTORTION_ITEM
        Dim mDistort As ClassSerivceConst.ClassCameraDistortion.STRUC_CAMERA_DISTORTION_ITEM

        Public Sub New(_Distort As ClassSerivceConst.ClassCameraDistortion.STRUC_CAMERA_DISTORTION_ITEM)
            mDistort = _Distort
        End Sub

        Public Overrides Function ToString() As String
            If (String.IsNullOrEmpty(mDistort.sName)) Then
                Return "Unknown"
            End If

            Return mDistort.sName
        End Function
    End Structure

    Public Sub New(_UCVirtualTrackers As UCVirtualTrackers, mDeviceInfo As ClassVideoInputDevices.ClassDeviceInfo)
        g_mUCVirtualTrackers = _UCVirtualTrackers

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.  
        TrackBar_DeviceExposure.Minimum = -1
        TrackBar_DeviceExposure.Maximum = 1
        TrackBar_DeviceExposure.Tag = False
        TrackBar_DeviceGain.Minimum = -1
        TrackBar_DeviceGain.Maximum = 1
        TrackBar_DeviceGain.Tag = False
        TrackBar_DeviceGamma.Minimum = -1
        TrackBar_DeviceGamma.Maximum = 1
        TrackBar_DeviceGamma.Tag = False
        TrackBar_DeviceConstrast.Minimum = -1
        TrackBar_DeviceConstrast.Maximum = 1
        TrackBar_DeviceConstrast.Tag = False

        g_mClassCaptureLogic = New ClassCaptureLogic(Me, mDeviceInfo.m_Index, mDeviceInfo.m_Path)
        Label_FriendlyName.Text = mDeviceInfo.m_Name

        For Each mDevice In ClassLibusbDriver.DRV_PS4CAM_KNOWN_CONFIGS
            Dim sHardwareId As String = String.Format("\USB#VID_{0}&PID_{1}", mDevice.VID, mDevice.PID)

            If (mDeviceInfo.m_Path.ToLowerInvariant.Contains(sHardwareId.ToLowerInvariant)) Then
                g_mClassCaptureLogic.m_IsPlayStationCamera = True
                Exit For
            End If
        Next

        ' Keep the UI disabled until we are finished
        Me.Enabled = False

        Try
            g_bIgnoreEvents = True

            ' Add all possible trackers. Where as -1 means disabled.
            ComboBox_CameraLensDistortion.Items.Clear()
            ComboBox_CameraLensDistortion.Items.Add("Select distortion preset...")
            For Each mDistort In ClassSerivceConst.ClassCameraDistortion.PSMOVESERVICE_KNOWN_DISTORTION
                ComboBox_CameraLensDistortion.Items.Add(New STRUC_CAMERA_DISTORTION_ITEM(mDistort))
            Next

            ComboBox_CameraLensDistortion.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ' Add all possible trackers. Where as -1 means disabled.
            ComboBox_DeviceTrackerId.Items.Clear()

            If (g_mClassCaptureLogic.m_IsPlayStationCamera) Then
                For i = -1 To ClassSerivceConst.PSMOVESERVICE_MAX_TRACKER_COUNT - 2
                    If (i < 0) Then
                        ComboBox_DeviceTrackerId.Items.Add(New STRUC_CAMERA_TRACKER_ID_ITEM(i, -1))
                    Else
                        ComboBox_DeviceTrackerId.Items.Add(New STRUC_CAMERA_TRACKER_ID_ITEM(i, i + 1))
                    End If
                Next
            Else
                For i = -1 To ClassSerivceConst.PSMOVESERVICE_MAX_TRACKER_COUNT - 1
                    ComboBox_DeviceTrackerId.Items.Add(New STRUC_CAMERA_TRACKER_ID_ITEM(i, -1))
                Next
            End If

            ComboBox_DeviceTrackerId.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_ImageInterpolation.Items.Clear()
            ComboBox_ImageInterpolation.Items.Add("Nearest Neighbot (fastest/worest)")
            ComboBox_ImageInterpolation.Items.Add("Bilinear (default)")
            ComboBox_ImageInterpolation.Items.Add("Bicubic")
            ComboBox_ImageInterpolation.Items.Add("Lanczos (slowest/best)")
            If ([Enum].GetNames(GetType(ClassCaptureLogic.ENUM_INTERPOLATION)).Count <> ComboBox_ImageInterpolation.Items.Count) Then
                Throw New ArgumentException("Not equal")
            End If
            ComboBox_ImageInterpolation.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_CameraResolution.Items.Clear()
            ComboBox_CameraResolution.Items.Add("480p (default)")
            ComboBox_CameraResolution.Items.Add("1080p")
            If ([Enum].GetNames(GetType(ClassCaptureLogic.ENUM_RESOLUTION)).Count <> ComboBox_CameraResolution.Items.Count) Then
                Throw New ArgumentException("Not equal")
            End If
            ComboBox_CameraResolution.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_CameraFramerate.Items.Clear()
            ComboBox_CameraFramerate.Items.Add("30")
            ComboBox_CameraFramerate.Items.Add("60")
            ComboBox_CameraFramerate.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        If (g_mClassCaptureLogic.m_IsPlayStationCamera) Then
            CheckBox_FlipHorizontal.Enabled = False
            ComboBox_ImageInterpolation.Enabled = False
            CheckBox_UseMjpg.Enabled = False
            CheckBox_DeviceSupersampling.Enabled = False
        End If

        ' Add a "Please wait" UI message while initalizing the video input device
        g_mMessageLabel = New Label()
        g_mMessageLabel.Parent = Me
        g_mMessageLabel.TextAlign = ContentAlignment.MiddleCenter
        g_mMessageLabel.AutoSize = False
        g_mMessageLabel.Dock = DockStyle.Fill
        g_mMessageLabel.Text = "Please wait..."
        g_mMessageLabel.Font = New Font(g_mMessageLabel.Font.FontFamily, 24, FontStyle.Bold)
        g_mMessageLabel.BringToFront()
        g_mMessageLabel.Show()

        SetFpsText(0, 0)
        SetUnsavedState(False)
        SetRequireRestart(False)

        CreateControl()

        Try
            g_bIgnoreUnsaved = True

            'Load settings
            g_mClassCaptureLogic.g_mClassConfig.LoadConfig()

            'Apply loaded settings
            TrackBar_DeviceExposure_ValueChanged(Nothing, Nothing)
            TrackBar_DeviceGain_ValueChanged(Nothing, Nothing)
            TrackBar_DeviceGamma_ValueChanged(Nothing, Nothing)
            TrackBar_DeviceConstrast_ValueChanged(Nothing, Nothing)
            ComboBox_DeviceTrackerId_SelectedIndexChanged(Nothing, Nothing)
            CheckBox_FlipHorizontal_CheckedChanged(Nothing, Nothing)
            ComboBox_ImageInterpolation_SelectedIndexChanged(Nothing, Nothing)
            CheckBox_UseMjpg_CheckedChanged(Nothing, Nothing)
            CheckBox_DeviceSupersampling_CheckedChanged(Nothing, Nothing)
            ComboBox_CameraResolution_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        Finally
            g_bIgnoreUnsaved = False
        End Try

        g_mClassCaptureLogic.StartInitThread(False)

        g_mStatusThread = New Threading.Thread(AddressOf StatusThread)
        g_mStatusThread.IsBackground = True
        g_mStatusThread.Start()

        ' Hide timeout error
        Panel_Status.Visible = False
        g_iStatusHideHeight = (Me.Height - Panel_Status.Height - Panel_Status.Margin.Top)
        g_iStatusShowHeight = Me.Height
        Me.Height = g_iStatusHideHeight
    End Sub


    Private Sub Timer_Status_Tick(sender As Object, e As EventArgs) Handles Timer_Status.Tick
        Timer_Status.Stop()

        Try
            Dim sTitle As String = ""
            Dim sMessage As String = ""
            Dim iStatusType As Integer = -1 ' -1 Hide, 0 Info, 1 Warn, 2 Error

            While True
                Dim iPipeIndexes As Integer() = New Integer() {
                    g_mClassCaptureLogic.m_PipePrimaryIndex,
                    g_mClassCaptureLogic.m_PipeSecondaryIndex
                }

                ' Check if disabled
                If (True) Then
                    If (iPipeIndexes(0) < 0) Then
                        sTitle = "Virtual Tracker is disabled"

                        Dim sText As New Text.StringBuilder
                        sText.AppendLine("The tracker id is not set or video input device is disabled.")
                        sMessage = sText.ToString

                        iStatusType = 2
                        Exit While
                    End If
                End If

                Dim bTrackerIdChanged As Boolean = False
                If (g_iStatisLastTrackerId <> iPipeIndexes(0)) Then
                    g_iStatisLastTrackerId = iPipeIndexes(0)
                    bTrackerIdChanged = True
                End If

                ' Check configs
                If (True) Then
                    Dim sConfigPath As String = ClassServiceConfig.GetConfigPath()
                    If (Not String.IsNullOrEmpty(sConfigPath)) Then
                        Dim sConfigFiles As New List(Of String)

                        If (g_mClassCaptureLogic.m_IsPlayStationCamera) Then
                            sConfigFiles.Add(IO.Path.Combine(sConfigPath, String.Format("PS3EyeTrackerConfig_virtual_{0}.json", iPipeIndexes(0))))
                            sConfigFiles.Add(IO.Path.Combine(sConfigPath, String.Format("PS3EyeTrackerConfig_virtual_{0}.json", iPipeIndexes(1))))
                        Else
                            sConfigFiles.Add(IO.Path.Combine(sConfigPath, String.Format("PS3EyeTrackerConfig_virtual_{0}.json", iPipeIndexes(0))))
                        End If

                        Dim bConfigUpdated As Boolean = False

                        For Each sConfigFile As String In sConfigFiles
                            If (IO.File.Exists(sConfigFile)) Then
                                Dim mLastWriteTime = IO.File.GetLastWriteTime(sConfigFile)

                                ' Dont instantly access the file
                                If (mLastWriteTime + New TimeSpan(0, 0, 1) > Date.Now) Then
                                    Continue For
                                End If

                                ' Only check if the file changed since last time 
                                If (Not bTrackerIdChanged) Then
                                    If (g_mStatusConfigDate.ContainsKey(sConfigFile.ToLowerInvariant)) Then
                                        If (mLastWriteTime <= g_mStatusConfigDate(sConfigFile.ToLowerInvariant)) Then
                                            Continue For
                                        End If
                                    End If
                                End If

                                g_mStatusConfigDate(sConfigFile.ToLowerInvariant) = mLastWriteTime
                                bConfigUpdated = True

                                ' Get camera calibrated pose
                                If (True) Then
                                    Dim mConfigPose As New ClassSerivceConst.ClassCameraPose.STRUC_CAMERA_POSE_ITEM
                                    mConfigPose.LoadFromConfig(sConfigFile)
                                    g_mStatusBadPoseQuatentions(sConfigFile.ToLowerInvariant) = mConfigPose.mOrientation
                                End If

                                ' Check camera distortion values
                                If (True) Then
                                    Dim mConfigCameraDistortion As New ClassSerivceConst.ClassCameraDistortion.STRUC_CAMERA_DISTORTION_ITEM
                                    mConfigCameraDistortion.LoadFromConfig(sConfigFile)

                                    ' The PS4 Cam requires precomputed distortion.
                                    Dim mConstCameraDistortion As ClassSerivceConst.ClassCameraDistortion.STRUC_CAMERA_DISTORTION_ITEM = Nothing
                                    If (g_mClassCaptureLogic.m_IsPlayStationCamera) Then
                                        If (ClassSerivceConst.ClassCameraDistortion.GetKnownDistortionByType(ClassSerivceConst.ClassCameraDistortion.ENUM_CAMERA_DISTORTION_TYPE.PS4CAM, mConstCameraDistortion)) Then
                                            Dim bCorrectDistort As Boolean = (
                                                CSng(mConfigCameraDistortion.iFocalLengthX) = CSng(mConstCameraDistortion.iFocalLengthX) AndAlso
                                                CSng(mConfigCameraDistortion.iFocalLengthY) = CSng(mConstCameraDistortion.iFocalLengthY) AndAlso
                                                CSng(mConfigCameraDistortion.iPrincipalX) = CSng(mConstCameraDistortion.iPrincipalX) AndAlso
                                                CSng(mConfigCameraDistortion.iPrincipalY) = CSng(mConstCameraDistortion.iPrincipalY) AndAlso
                                                CSng(mConfigCameraDistortion.iDistortionK1) = CSng(mConstCameraDistortion.iDistortionK1) AndAlso
                                                CSng(mConfigCameraDistortion.iDistortionK2) = CSng(mConstCameraDistortion.iDistortionK2) AndAlso
                                                CSng(mConfigCameraDistortion.iDistortionK3) = CSng(mConstCameraDistortion.iDistortionK3) AndAlso
                                                CSng(mConfigCameraDistortion.iDistortionP1) = CSng(mConstCameraDistortion.iDistortionP1) AndAlso
                                                CSng(mConfigCameraDistortion.iDistortionP2) = CSng(mConstCameraDistortion.iDistortionP2))

                                            If (Not bCorrectDistort) Then
                                                g_bStatusDistortionError = True
                                            Else
                                                g_bStatusDistortionError = False
                                            End If
                                        Else
                                            g_bStatusDistortionError = False
                                        End If
                                    Else
                                        ' Check for general cameras if they have been calibrated or not
                                        If (ClassSerivceConst.ClassCameraDistortion.GetKnownDistortionByType(ClassSerivceConst.ClassCameraDistortion.ENUM_CAMERA_DISTORTION_TYPE.PSEYE, mConstCameraDistortion)) Then
                                            Dim bCorrectDistort As Boolean = (
                                                CSng(mConfigCameraDistortion.iFocalLengthX) = CSng(mConstCameraDistortion.iFocalLengthX) AndAlso
                                                CSng(mConfigCameraDistortion.iFocalLengthY) = CSng(mConstCameraDistortion.iFocalLengthY) AndAlso
                                                CSng(mConfigCameraDistortion.iPrincipalX) = CSng(mConstCameraDistortion.iPrincipalX) AndAlso
                                                CSng(mConfigCameraDistortion.iPrincipalY) = CSng(mConstCameraDistortion.iPrincipalY) AndAlso
                                                CSng(mConfigCameraDistortion.iDistortionK1) = CSng(mConstCameraDistortion.iDistortionK1) AndAlso
                                                CSng(mConfigCameraDistortion.iDistortionK2) = CSng(mConstCameraDistortion.iDistortionK2) AndAlso
                                                CSng(mConfigCameraDistortion.iDistortionK3) = CSng(mConstCameraDistortion.iDistortionK3) AndAlso
                                                CSng(mConfigCameraDistortion.iDistortionP1) = CSng(mConstCameraDistortion.iDistortionP1) AndAlso
                                                CSng(mConfigCameraDistortion.iDistortionP2) = CSng(mConstCameraDistortion.iDistortionP2))

                                            If (bCorrectDistort) Then
                                                g_bStatusDistortionWarning = True
                                            Else
                                                g_bStatusDistortionWarning = False
                                            End If
                                        Else
                                            g_bStatusDistortionWarning = False
                                        End If
                                    End If
                                End If
                            End If
                        Next

                        ' Check for bad PS4 cam pose calibration
                        If (True) Then
                            If (bConfigUpdated) Then
                                If (g_mClassCaptureLogic.m_IsPlayStationCamera AndAlso sConfigFiles.Count = 2) Then
                                    If (g_mStatusBadPoseQuatentions.ContainsKey(sConfigFiles(0).ToLowerInvariant) AndAlso g_mStatusBadPoseQuatentions.ContainsKey(sConfigFiles(1).ToLowerInvariant)) Then
                                        Dim mPose1 = g_mStatusBadPoseQuatentions(sConfigFiles(0).ToLowerInvariant)
                                        Dim mPose2 = g_mStatusBadPoseQuatentions(sConfigFiles(1).ToLowerInvariant)

                                        Dim iAngleDiff = ClassQuaternionTools.CalculateAngleDegreesDifference(mPose1, mPose2)

                                        If (iAngleDiff > PS4_TRACKER_POSE_ANGLE_MAX_DIVIATION OrElse
                                            (mPose1 = Quaternion.Identity OrElse mPose2 = Quaternion.Identity)) Then
                                            g_bStatusDistortionAngle = iAngleDiff
                                            g_bStatusBadPoseError = True
                                        Else
                                            g_bStatusBadPoseError = False
                                        End If
                                    Else
                                        g_bStatusBadPoseError = False
                                    End If
                                End If
                            End If
                        End If
                    End If

                    If (g_bStatusDistortionError) Then
                        sTitle = "Incorrect distortion values"

                        Dim sText As New Text.StringBuilder
                        sText.AppendLine("The current distortion values for this virtual tracker are incorrect. Distortion preset 'PlayStation 4 Stereo Camera' is required!")
                        sMessage = sText.ToString

                        iStatusType = 2
                        Exit While
                    End If
                End If

                ' Check if connected to PSMS-EX
                If (True) Then
                    If (Not g_mClassCaptureLogic.m_PipeConnected) Then
                        sTitle = "Virtual tracker not connected to PSMoveServiceEx"

                        Dim sText As New Text.StringBuilder
                        sText.AppendLine("Unable to connect to the virtual tracker in PSMoveServiceEx that corresponds to the tracker id.")
                        sMessage = sText.ToString

                        iStatusType = 2
                        Exit While
                    End If
                End If

                If (True) Then
                    If (g_bStatusDistortionWarning) Then
                        sTitle = "Uncalibrated distortion values"

                        Dim sText As New Text.StringBuilder
                        sText.AppendLine("The current distortion values for this virtual tracker are uncalibrated and will cause tracking issues. Please calibrate tracker distortion!")
                        sMessage = sText.ToString

                        iStatusType = 2
                        Exit While
                    End If
                End If

                If (True) Then
                    If (g_bStatusBadPoseError) Then
                        sTitle = "Bad pose calibration"

                        Dim sText As New Text.StringBuilder
                        sText.AppendFormat(
                            "The calibrated pose angle difference between the virtual trackers are too big (Angle: {0}). Please re-calibrate pose!",
                            g_bStatusDistortionAngle.ToString("F2", Globalization.CultureInfo.InvariantCulture)).AppendLine()
                        sMessage = sText.ToString

                        iStatusType = 2
                        Exit While
                    End If
                End If

                Exit While
            End While

            g_bHasStatusError = (iStatusType > -1)

            If (Me.Visible) Then
                If (Label_StatusTitle.Text <> sTitle OrElse Label_StatusMessage.Text <> sMessage) Then
                    Label_StatusTitle.Text = sTitle
                    Label_StatusMessage.Text = sMessage
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


    Private Sub StatusThread()
        While True


            Threading.Thread.Sleep(1000)
        End While
    End Sub


    Private Sub SetUnsavedState(bIsUnsaved As Boolean)
        If (g_bIgnoreUnsaved) Then
            Return
        End If

        If (bIsUnsaved) Then
            Button_ConfigSave.Text = String.Format("Save Settings*")
            Button_ConfigSave.Font = New Font(Button_ConfigSave.Font, FontStyle.Bold)
        Else
            Button_ConfigSave.Text = String.Format("Save Settings")
            Button_ConfigSave.Font = New Font(Button_ConfigSave.Font, FontStyle.Regular)
        End If
    End Sub

    Private Sub SetRequireRestart(bRequired As Boolean)
        If (g_bIgnoreUnsaved) Then
            Return
        End If

        If (bRequired) Then
            Button_RestartDevice.Text = String.Format("Restart device*")
            Button_RestartDevice.Font = New Font(Button_RestartDevice.Font, FontStyle.Bold)
        Else
            Button_RestartDevice.Text = String.Format("Restart device")
            Button_RestartDevice.Font = New Font(Button_RestartDevice.Font, FontStyle.Regular)
        End If
    End Sub

    Private Sub SetPreviewFullscreen(bFullscreen As Boolean)
        If (IsPreviewFullscreen()) Then
            If (Not bFullscreen) Then
                PictureBox_CaptureImage.Parent = Panel_Preview
                PictureBox_CaptureImage.BringToFront()
            End If
        Else
            If (bFullscreen) Then
                PictureBox_CaptureImage.Parent = Me
                PictureBox_CaptureImage.BringToFront()
            End If
        End If
    End Sub

    Private Function IsPreviewFullscreen() As Boolean
        Return (PictureBox_CaptureImage.Parent IsNot Panel_Preview)
    End Function

    ReadOnly Property m_DevicePath As String
        Get
            If (g_mClassCaptureLogic Is Nothing) Then
                Return Nothing
            End If

            Return g_mClassCaptureLogic.m_DevicePath
        End Get
    End Property

    Private Sub TrackBar_DeviceExposure_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_DeviceExposure.ValueChanged
        If (g_bIgnoreEvents OrElse Not CBool(TrackBar_DeviceExposure.Tag)) Then
            Return
        End If

        If (g_mClassCaptureLogic.m_Capture Is Nothing) Then
            Return
        End If

        g_mClassCaptureLogic.m_Capture.Exposure = TrackBar_DeviceExposure.Value
        SetUnsavedState(True)
    End Sub

    Private Sub TrackBar_DeviceGain_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_DeviceGain.ValueChanged
        If (g_bIgnoreEvents OrElse Not CBool(TrackBar_DeviceGain.Tag)) Then
            Return
        End If

        If (g_mClassCaptureLogic.m_Capture Is Nothing) Then
            Return
        End If

        g_mClassCaptureLogic.m_Capture.Gain = TrackBar_DeviceGain.Value
        SetUnsavedState(True)
    End Sub

    Private Sub TrackBar_DeviceGamma_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_DeviceGamma.ValueChanged
        If (g_bIgnoreEvents OrElse Not CBool(TrackBar_DeviceGamma.Tag)) Then
            Return
        End If

        If (g_mClassCaptureLogic.m_Capture Is Nothing) Then
            Return
        End If

        g_mClassCaptureLogic.m_Capture.Gamma = TrackBar_DeviceGamma.Value
        SetUnsavedState(True)
    End Sub

    Private Sub TrackBar_DeviceConstrast_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_DeviceConstrast.ValueChanged
        If (g_bIgnoreEvents OrElse Not CBool(TrackBar_DeviceConstrast.Tag)) Then
            Return
        End If

        If (g_mClassCaptureLogic.m_Capture Is Nothing) Then
            Return
        End If

        g_mClassCaptureLogic.m_Capture.Contrast = TrackBar_DeviceConstrast.Value
        SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_ShowCaptureImage_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ShowCaptureImage.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        ' Disable others capture previews if they are enabled. We only need one esp. for performance.
        If (CheckBox_ShowCaptureImage.Checked) Then
            For Each mDeviceItem In g_mUCVirtualTrackers.GetAllDevices()
                If (mDeviceItem Is Me) Then
                    Continue For
                End If

                mDeviceItem.CheckBox_ShowCaptureImage.Checked = False
            Next
        End If

        g_mClassCaptureLogic.m_ShowCaptureImage = CheckBox_ShowCaptureImage.Checked
    End Sub

    Private Sub ComboBox_DeviceTrackerId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_DeviceTrackerId.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        If (TypeOf ComboBox_DeviceTrackerId.SelectedItem IsNot STRUC_CAMERA_TRACKER_ID_ITEM) Then
            Return
        End If

        Dim iSelectedTrackerId_1 As Integer = DirectCast(ComboBox_DeviceTrackerId.SelectedItem, STRUC_CAMERA_TRACKER_ID_ITEM).iTrackerId_1
        Dim iSelectedTrackerId_2 As Integer = DirectCast(ComboBox_DeviceTrackerId.SelectedItem, STRUC_CAMERA_TRACKER_ID_ITEM).iTrackerId_2

        ' Check if the pipe index already has been used so we dont write to the same pipe.
        If (iSelectedTrackerId_1 > -1 OrElse iSelectedTrackerId_2 > -1) Then
            For Each mDeviceItem In g_mUCVirtualTrackers.GetAllDevices()
                If (mDeviceItem Is Me) Then
                    Continue For
                End If

                If (mDeviceItem.g_mClassCaptureLogic Is Nothing) Then
                    Continue For
                End If

                If (mDeviceItem.g_mClassCaptureLogic.m_PipePrimaryIndex < 0) Then
                    Continue For
                End If

                If (mDeviceItem.g_mClassCaptureLogic.m_IsPlayStationCamera) Then
                    If (mDeviceItem.g_mClassCaptureLogic.m_PipePrimaryIndex = iSelectedTrackerId_1 OrElse mDeviceItem.g_mClassCaptureLogic.m_PipeSecondaryIndex = iSelectedTrackerId_2 OrElse
                        mDeviceItem.g_mClassCaptureLogic.m_PipeSecondaryIndex = iSelectedTrackerId_1 OrElse mDeviceItem.g_mClassCaptureLogic.m_PipePrimaryIndex = iSelectedTrackerId_2) Then
                        MessageBox.Show("This tracker id is already being in use!", "Unable to set tracker id", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        ComboBox_DeviceTrackerId.SelectedIndex = 0
                        Return
                    End If
                Else
                    If (mDeviceItem.g_mClassCaptureLogic.m_PipePrimaryIndex = iSelectedTrackerId_1 OrElse mDeviceItem.g_mClassCaptureLogic.m_PipePrimaryIndex = iSelectedTrackerId_2) Then
                        MessageBox.Show("This tracker id is already being in use!", "Unable to set tracker id", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        ComboBox_DeviceTrackerId.SelectedIndex = 0
                        Return
                    End If
                End If
            Next
        End If

        If (iSelectedTrackerId_1 > -1 AndAlso g_iPreviousTrackerIdSelectedIndex > 0 AndAlso ComboBox_DeviceTrackerId.SelectedIndex <> g_iPreviousTrackerIdSelectedIndex) Then
            Dim sMessage As New Text.StringBuilder
            sMessage.AppendLine("You are about to change the tracker id associated with this video input device.")
            sMessage.AppendLine("PSMoveServiceEx saves its virtual tracker settings using their tracker id and you will lose all settings configured for this device if you change the tracker id!")
            sMessage.AppendLine("You will have to re-configure this virtual tracker in PSMoveServiceEx again. (e.g. distortion calibration, color calibration etc.)")
            sMessage.AppendLine()
            sMessage.AppendLine("Click OK to continue or CANCEL to abort.")

            If (MessageBox.Show(sMessage.ToString, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                ComboBox_DeviceTrackerId.SelectedIndex = g_iPreviousTrackerIdSelectedIndex
                Return
            End If
        End If

        g_mClassCaptureLogic.m_PipePrimaryIndex = iSelectedTrackerId_1
        g_iPreviousTrackerIdSelectedIndex = ComboBox_DeviceTrackerId.SelectedIndex
        SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_FlipHorizontal_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_FlipHorizontal.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassCaptureLogic.m_FlipImage = CheckBox_FlipHorizontal.Checked
        SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_ImageInterpolation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_ImageInterpolation.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassCaptureLogic.m_ImageInterpolation = CType(ComboBox_ImageInterpolation.SelectedIndex, ClassCaptureLogic.ENUM_INTERPOLATION)
        SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_CameraResolution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_CameraResolution.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassCaptureLogic.m_CameraResolution = CType(ComboBox_CameraResolution.SelectedIndex, ClassCaptureLogic.ENUM_RESOLUTION)
        SetUnsavedState(True)

        ' Config is currently loading, dont show messagebox
        If (g_bIgnoreUnsaved) Then
            Return
        End If

        MessageBox.Show("This video input device needs to be restarted in order for changes to take effect!", "Device restart required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        SetRequireRestart(True)
    End Sub

    Private Sub ComboBox_CameraFramerate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_CameraFramerate.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassCaptureLogic.m_CameraFramerate = CInt(ComboBox_CameraFramerate.SelectedItem)
        SetUnsavedState(True)

        ' Config is currently loading, dont show messagebox
        If (g_bIgnoreUnsaved) Then
            Return
        End If

        MessageBox.Show("This video input device needs to be restarted in order for changes to take effect!", "Device restart required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        SetRequireRestart(True)
    End Sub

    Private Sub CheckBox_UseMjpg_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_UseMjpg.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassCaptureLogic.m_UseMJPG = CheckBox_UseMjpg.Checked
        SetUnsavedState(True)

        ' Config is currently loading, dont show messagebox
        If (g_bIgnoreUnsaved) Then
            Return
        End If

        MessageBox.Show("This video input device needs to be restarted in order for changes to take effect!", "Device restart required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        SetRequireRestart(True)
    End Sub

    Private Sub CheckBox_DeviceSupersampling_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_DeviceSupersampling.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassCaptureLogic.m_Supersampling = CheckBox_DeviceSupersampling.Checked
        SetUnsavedState(True)

        ' Config is currently loading, dont show messagebox
        If (g_bIgnoreUnsaved) Then
            Return
        End If

        MessageBox.Show("This video input device needs to be restarted in order for changes to take effect!", "Device restart required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        SetRequireRestart(True)
    End Sub

    Private Sub ComboBox_CameraLesnDistortion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_CameraLensDistortion.SelectedIndexChanged
        Try
            If (g_bIgnoreEvents) Then
                Return
            End If

            If (TypeOf ComboBox_CameraLensDistortion.SelectedItem IsNot STRUC_CAMERA_DISTORTION_ITEM) Then
                ComboBox_CameraLensDistortion.SelectedIndex = 0
                Return
            End If

            Dim mDistort As STRUC_CAMERA_DISTORTION_ITEM = DirectCast(ComboBox_CameraLensDistortion.SelectedItem, STRUC_CAMERA_DISTORTION_ITEM)

            Dim sMessage As New Text.StringBuilder
            sMessage.AppendLine("You are about to re-configure this trackers distortion calibration using preset:")
            sMessage.AppendLine(mDistort.ToString)
            sMessage.AppendLine()
            sMessage.AppendLine("Do you want to continue?")
            If (MessageBox.Show(sMessage.ToString, "Override Tracker Distortion?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No) Then
                ComboBox_CameraLensDistortion.SelectedIndex = 0
                Return
            End If

            Dim mIds As New List(Of Integer)
            If (g_mClassCaptureLogic.m_IsPlayStationCamera) Then
                mIds.Add(g_mClassCaptureLogic.m_PipePrimaryIndex)
                mIds.Add(g_mClassCaptureLogic.m_PipeSecondaryIndex)
            Else
                mIds.Add(g_mClassCaptureLogic.m_PipePrimaryIndex)
            End If

            For Each iTrackerID As Integer In mIds
                If (iTrackerID < 0) Then
                    Throw New ArgumentException("Tracker id is invalid")
                End If

                Dim sConfigPath As String = ClassServiceConfig.GetConfigPath()
                If (String.IsNullOrEmpty(sConfigPath)) Then
                    Throw New ArgumentException("Config path does not exist")
                End If

                Dim sConfigFile As String = IO.Path.Combine(sConfigPath, String.Format("PS3EyeTrackerConfig_virtual_{0}.json", iTrackerID))

                mDistort.mDistort.SaveToConfig(sConfigFile)
            Next

            ComboBox_CameraLensDistortion.SelectedIndex = 0
            MessageBox.Show("Tracker distortion preset has been applied! PSMoveServiceEx needs to be restarted in order for changes to take effect!", "PSMoveServiceEx restart required", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ComboBox_CameraLensDistortion.SelectedIndex = 0
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub Button_RestartDevice_Click(sender As Object, e As EventArgs) Handles Button_RestartDevice.Click
        Try
            Me.Enabled = False

            ' Restart the init thread to get changed information about the device.
            g_mClassCaptureLogic.StartInitThread(True)
            SetRequireRestart(False)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)

            Me.Dispose()
        End Try
    End Sub

    Private Sub Button_ConfigSave_Click(sender As Object, e As EventArgs) Handles Button_ConfigSave.Click
        Try
            g_mClassCaptureLogic.g_mClassConfig.SaveConfig()
            SetUnsavedState(False)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub CheckBox_Autostart_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Autostart.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        SetUnsavedState(True)
    End Sub

    Public Sub SetFpsText(iCaptureFps As Integer, iPipeFps As Integer)
        If (iCaptureFps > -1) Then
            g_iCaptureFps = iCaptureFps
        End If

        If (iPipeFps > -1) Then
            g_iPipeFps = iPipeFps
        End If

        If (g_iCaptureFps < 20 OrElse g_iPipeFps < 20) Then
            TextBox_Fps.ForeColor = Color.Red
        ElseIf (g_iCaptureFps < 27 OrElse g_iPipeFps < 27) Then
            TextBox_Fps.ForeColor = Color.DarkOrange
        Else
            TextBox_Fps.ForeColor = Color.Black
        End If

        TextBox_Fps.Text = String.Format("FPS: {0}, I/O FPS: {1}", g_iCaptureFps, g_iPipeFps)
    End Sub

    Private Sub CleanUp()
        If (g_mStatusThread IsNot Nothing AndAlso g_mStatusThread.IsAlive) Then
            g_mStatusThread.Abort()
            g_mStatusThread.Join()
            g_mStatusThread = Nothing
        End If

        If (g_mClassCaptureLogic IsNot Nothing) Then
            g_mClassCaptureLogic.Dispose()
            g_mClassCaptureLogic = Nothing
        End If
    End Sub

    Class ClassCaptureLogic
        Implements IDisposable

        Private g_bInitalized As Boolean = False

        Private g_mInitThread As Threading.Thread = Nothing
        Private g_mCaptureThread As Threading.Thread = Nothing
        Private g_mPipeThread() As Threading.Thread = {Nothing, Nothing}
        Private g_mDeviceWatchdogThread As Threading.Thread

        Private g_mCapture As OpenCvSharp.VideoCapture = Nothing
        Private g_mCaptureFrame As OpenCvSharp.Mat() = {Nothing, Nothing}
        Private g_mPipEvent As Threading.AutoResetEvent() = {Nothing, Nothing}

        Private g_mThreadLock As New Object
        Private g_mThreadInitLock As New Object

        Private g_mUCVirtualTrackerItem As UCVirtualTrackerItem

        Private g_bIsPlayStationCamera As Boolean = False
        Private g_bShowCaptureImage As Boolean = False
        Private g_iPipeIndex As Integer = -1
        Private g_bPipeConnected As Boolean() = {False, False}

        Private g_iDeviceIndex As Integer = -1
        Private g_sDevicePath As String = ""
        Private g_bFlipImage As Boolean = False
        Private g_bUseMJPG As Boolean = False
        Private g_bSupersampling As Boolean = False
        Private g_iCameraFramerate As Integer = 30

        Enum ENUM_INTERPOLATION
            NEARST_NEIGHBOR
            BILINEAR
            BICUBIC
            LANCZOS
        End Enum
        Private g_iImageInterpolation As ENUM_INTERPOLATION = ENUM_INTERPOLATION.NEARST_NEIGHBOR

        Enum ENUM_RESOLUTION
            SD
            HD
        End Enum
        Private g_iCameraResolution As ENUM_RESOLUTION = ENUM_RESOLUTION.SD

        Public g_mClassConfig As ClassConfig

        Public Sub New(_UCVirtualTrackerItem As UCVirtualTrackerItem, _DeviceIndex As Integer, _DevicePath As String)
            g_mUCVirtualTrackerItem = _UCVirtualTrackerItem
            g_iDeviceIndex = _DeviceIndex
            g_sDevicePath = _DevicePath

            For i = 0 To g_mPipEvent.Length - 1
                g_mPipEvent(i) = New Threading.AutoResetEvent(False)
            Next

            g_mClassConfig = New ClassConfig(Me)
        End Sub

        ''' <summary>
        ''' Starts or restarts the init thread.
        ''' The init thread prepares this class for future use. This is required to run before anything else.
        ''' </summary>
        ''' <param name="bRestart"></param>
        Public Sub StartInitThread(bRestart As Boolean)
            If (g_mInitThread IsNot Nothing AndAlso g_mInitThread.IsAlive) Then
                If (bRestart) Then
                    g_mInitThread.Abort()
                    g_mInitThread.Join()
                    g_mInitThread = Nothing
                Else
                    Return
                End If
            End If

            If (g_mCaptureThread IsNot Nothing AndAlso g_mCaptureThread.IsAlive) Then
                g_mCaptureThread.Abort()
                g_mCaptureThread.Join()
                g_mCaptureThread = Nothing
            End If

            For i = 0 To g_mPipeThread.Length - 1
                If (g_mPipeThread(i) IsNot Nothing AndAlso g_mPipeThread(i).IsAlive) Then
                    g_mPipeThread(i).Abort()
                    g_mPipeThread(i).Join()
                    g_mPipeThread(i) = Nothing
                End If
            Next

            If (g_mDeviceWatchdogThread IsNot Nothing AndAlso g_mDeviceWatchdogThread.IsAlive) Then
                g_mDeviceWatchdogThread.Abort()
                g_mDeviceWatchdogThread.Join()
                g_mDeviceWatchdogThread = Nothing
            End If

            g_mUCVirtualTrackerItem.Enabled = False

            g_mInitThread = New Threading.Thread(AddressOf InitThread)
            g_mInitThread.IsBackground = True
            g_mInitThread.Start()
        End Sub

        Public ReadOnly Property m_Initialized As Boolean
            Get
                SyncLock g_mThreadInitLock
                    Return g_bInitalized
                End SyncLock
            End Get
        End Property

        ''' <summary>
        ''' Start or restart the capture thread.
        ''' The capture thread will read and cache the devices frames.
        ''' </summary>
        ''' <param name="bRestart"></param>
        Private Sub StartCaptureThread(bRestart As Boolean)
            If (Not m_Initialized) Then
                Return
            End If

            If (g_mCaptureThread IsNot Nothing AndAlso g_mCaptureThread.IsAlive) Then
                If (bRestart) Then
                    g_mCaptureThread.Abort()
                    g_mCaptureThread.Join()
                    g_mCaptureThread = Nothing
                Else
                    Return
                End If
            End If

            g_mCaptureThread = New Threading.Thread(AddressOf CaptureThread)
            g_mCaptureThread.IsBackground = True
            g_mCaptureThread.Start()
        End Sub

        ''' <summary>
        ''' Start or restart the named pipe thread.
        ''' The pipe thread reads the cached frame and sends it to a named pipe to PSMoveSerivce.
        ''' </summary>
        ''' <param name="bRestart"></param>
        Private Sub StartPipeThread(bRestart As Boolean)
            If (Not m_Initialized) Then
                Return
            End If

            For i = 0 To g_mPipeThread.Length - 1
                If (g_mPipeThread(i) IsNot Nothing AndAlso g_mPipeThread(i).IsAlive) Then
                    If (bRestart) Then
                        g_mPipeThread(i).Abort()
                        g_mPipeThread(i).Join()
                        g_mPipeThread(i) = Nothing
                    Else
                        Continue For
                    End If
                End If

                Select Case (i)
                    Case 0
                        g_mPipeThread(i) = New Threading.Thread(AddressOf PipeThreadPrimary)
                        g_mPipeThread(i).IsBackground = True
                        g_mPipeThread(i).Start()
                    Case 1
                        If (Not m_IsPlayStationCamera) Then
                            Continue For
                        End If

                        g_mPipeThread(i) = New Threading.Thread(AddressOf PipeThreadSecondary)
                        g_mPipeThread(i).IsBackground = True
                        g_mPipeThread(i).Start()
                End Select
            Next
        End Sub

        ''' <summary>
        ''' Starts or restarts the watchdog thread.
        ''' This thread will re-run the init thread if video input devices change.
        ''' 
        ''' $TODO Use WMI Plug-and-Play watcher instead.
        ''' </summary>
        ''' <param name="bRestart"></param>
        Private Sub StartDeviceWatchodogThread(bRestart As Boolean)
            If (Not m_Initialized) Then
                Return
            End If

            If (g_mDeviceWatchdogThread IsNot Nothing AndAlso g_mDeviceWatchdogThread.IsAlive) Then
                If (bRestart) Then
                    g_mDeviceWatchdogThread.Abort()
                    g_mDeviceWatchdogThread.Join()
                    g_mDeviceWatchdogThread = Nothing
                Else
                    Return
                End If
            End If

            g_mDeviceWatchdogThread = New Threading.Thread(AddressOf DeviceWatchdogThread)
            g_mDeviceWatchdogThread.IsBackground = True
            g_mDeviceWatchdogThread.Start()
        End Sub

        Public Property m_IsPlayStationCamera As Boolean
            Get
                SyncLock g_mThreadLock
                    Return g_bIsPlayStationCamera
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock g_mThreadLock
                    g_bIsPlayStationCamera = value
                End SyncLock
            End Set
        End Property

        ReadOnly Property m_CaptureResolution(iPipeID As Integer) As ENUM_RESOLUTION
            Get
                Select Case (m_CaptureFrame(iPipeID).Rows)
                    Case 480
                        Return ENUM_RESOLUTION.SD

                    Case 1080
                        Return ENUM_RESOLUTION.HD

                End Select

                Return CType(-1, ENUM_RESOLUTION)
            End Get
        End Property

        Public Property m_Capture As OpenCvSharp.VideoCapture
            Get
                SyncLock g_mThreadLock
                    Return g_mCapture
                End SyncLock
            End Get
            Set(value As OpenCvSharp.VideoCapture)
                SyncLock g_mThreadLock
                    g_mCapture = value
                End SyncLock
            End Set
        End Property

        Public Property m_CaptureFrame(iPipeID As Integer) As OpenCvSharp.Mat
            Get
                SyncLock g_mThreadLock
                    Return g_mCaptureFrame(iPipeID)
                End SyncLock
            End Get
            Set(value As OpenCvSharp.Mat)
                SyncLock g_mThreadLock
                    g_mCaptureFrame(iPipeID) = value
                End SyncLock
            End Set
        End Property

        Public ReadOnly Property m_CaptureFrameLength As Integer
            Get
                Return g_mCaptureFrame.Length
            End Get
        End Property

        Public Property m_ShowCaptureImage As Boolean
            Get
                SyncLock g_mThreadLock
                    Return g_bShowCaptureImage
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock g_mThreadLock
                    g_bShowCaptureImage = value
                End SyncLock
            End Set
        End Property

        Public Property m_PipePrimaryIndex As Integer
            Get
                SyncLock g_mThreadLock
                    Return g_iPipeIndex
                End SyncLock
            End Get
            Set(value As Integer)
                Dim bRefreshPipe As Boolean = False

                SyncLock g_mThreadLock
                    If (value > ClassSerivceConst.PSMOVESERVICE_MAX_TRACKER_COUNT - 1) Then
                        Return
                    End If

                    If (g_iPipeIndex <> value) Then
                        g_iPipeIndex = value

                        bRefreshPipe = True
                    End If
                End SyncLock

                If (bRefreshPipe) Then
                    StartPipeThread(True)
                End If
            End Set
        End Property

        Public ReadOnly Property m_PipeConnected As Boolean
            Get
                SyncLock g_mThreadLock
                    If (m_IsPlayStationCamera) Then
                        Return (g_bPipeConnected(0) AndAlso g_bPipeConnected(1))
                    Else
                        Return g_bPipeConnected(0)
                    End If
                End SyncLock
            End Get
        End Property

        Public ReadOnly Property m_PipeSecondaryIndex As Integer
            Get
                Return m_PipePrimaryIndex + 1
            End Get
        End Property

        Public Property m_DeviceIndex As Integer
            Get
                SyncLock g_mThreadLock
                    Return g_iDeviceIndex
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock g_mThreadLock
                    g_iDeviceIndex = value
                End SyncLock
            End Set
        End Property

        Public Property m_DevicePath As String
            Get
                SyncLock g_mThreadLock
                    Return g_sDevicePath
                End SyncLock
            End Get
            Set(value As String)
                SyncLock g_mThreadLock
                    g_sDevicePath = value
                End SyncLock
            End Set
        End Property

        Public Property m_FlipImage As Boolean
            Get
                SyncLock g_mThreadLock
                    Return g_bFlipImage
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock g_mThreadLock
                    g_bFlipImage = value
                End SyncLock
            End Set
        End Property

        Public Property m_UseMJPG As Boolean
            Get
                SyncLock g_mThreadLock
                    Return g_bUseMJPG
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock g_mThreadLock
                    g_bUseMJPG = value
                End SyncLock
            End Set
        End Property

        Public Property m_Supersampling As Boolean
            Get
                SyncLock g_mThreadLock
                    Return g_bSupersampling
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock g_mThreadLock
                    g_bSupersampling = value
                End SyncLock
            End Set
        End Property

        Public Property m_ImageInterpolation As ENUM_INTERPOLATION
            Get
                SyncLock g_mThreadLock
                    Return g_iImageInterpolation
                End SyncLock
            End Get
            Set(value As ENUM_INTERPOLATION)
                If (value < ENUM_INTERPOLATION.NEARST_NEIGHBOR) Then
                    value = ENUM_INTERPOLATION.NEARST_NEIGHBOR
                End If

                If (value > [Enum].GetNames(GetType(ENUM_INTERPOLATION)).Count - 1) Then
                    value = ENUM_INTERPOLATION.LANCZOS
                End If

                SyncLock g_mThreadLock
                    g_iImageInterpolation = value
                End SyncLock
            End Set
        End Property

        Public Property m_CameraResolution As ENUM_RESOLUTION
            Get
                SyncLock g_mThreadLock
                    Return g_iCameraResolution
                End SyncLock
            End Get
            Set(value As ENUM_RESOLUTION)
                If (value < ENUM_RESOLUTION.SD) Then
                    value = ENUM_RESOLUTION.SD
                End If

                If (value > [Enum].GetNames(GetType(ENUM_RESOLUTION)).Count - 1) Then
                    value = ENUM_RESOLUTION.HD
                End If

                SyncLock g_mThreadLock
                    g_iCameraResolution = value
                End SyncLock
            End Set
        End Property

        Public Function GetCameraResolutionSize() As Size
            Select Case (m_CameraResolution)
                Case ENUM_RESOLUTION.SD
                    Return New Size(640, 480)

                Case ENUM_RESOLUTION.HD
                    Return New Size(1920, 1080)
            End Select

            Return New Size(640, 480)
        End Function

        Public Property m_CameraFramerate As Integer
            Get
                SyncLock g_mThreadLock
                    Return g_iCameraFramerate
                End SyncLock
            End Get
            Set(value As Integer)
                If (value < 30) Then
                    value = 30
                End If

                If (value > 60) Then
                    value = 60
                End If

                SyncLock g_mThreadLock
                    g_iCameraFramerate = value
                End SyncLock
            End Set
        End Property

        Public Function GetImageInterpolation() As OpenCvSharp.InterpolationFlags
            Select Case (m_ImageInterpolation)
                Case ENUM_INTERPOLATION.NEARST_NEIGHBOR
                    Return OpenCvSharp.InterpolationFlags.Nearest
                Case ENUM_INTERPOLATION.BILINEAR
                    Return OpenCvSharp.InterpolationFlags.Linear
                Case ENUM_INTERPOLATION.BICUBIC
                    Return OpenCvSharp.InterpolationFlags.Cubic
                Case ENUM_INTERPOLATION.LANCZOS
                    Return OpenCvSharp.InterpolationFlags.Lanczos4
            End Select

            Return OpenCvSharp.InterpolationFlags.Nearest
        End Function

        Public Function IsDeviceValid() As Boolean
            Return (GetDeviceIndexByPath() = m_DeviceIndex)
        End Function

        Public Function GetDeviceIndexByPath() As Integer
            Dim mDeviceList As New List(Of ClassVideoInputDevices.ClassDeviceInfo)
            If (ClassVideoInputDevices.GetDevicesOfVideoInput(mDeviceList)) Then
                For i = 0 To mDeviceList.Count - 1
                    If (mDeviceList(i).m_Path = m_DevicePath) Then
                        Return mDeviceList(i).m_Index
                    End If
                Next
            End If

            Return -1
        End Function

        Private Sub InitThread()
            Dim mCapture As OpenCvSharp.VideoCapture = Nothing

            Try
                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.g_mMessageLabel.Text = "Initializing device...")
                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.g_mMessageLabel.Visible = True)
                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.g_mMessageLabel.BringToFront())

                Dim bIsPlayStationCamera As Boolean = m_IsPlayStationCamera
                Dim iCameraResolution As ENUM_RESOLUTION = m_CameraResolution
                Dim bSupersampling As Boolean = m_Supersampling
                Dim iCameraFramerate As Integer = m_CameraFramerate
                Dim bUseMJPG As Boolean = m_UseMJPG

                'Remove old capture before we create a new one
                SyncLock g_mThreadLock
                    If (m_Capture IsNot Nothing AndAlso Not m_Capture.IsDisposed) Then
                        m_Capture.Dispose()
                        m_Capture = Nothing
                    End If
                End SyncLock

                Dim iRealIndex As Integer = GetDeviceIndexByPath()
                If (iRealIndex < 0) Then
                    Throw New ArgumentException("Unable to open video input device. Device can not be found.")
                End If

                ' Replace the old index with the new one if it changed by any way.
                m_DeviceIndex = iRealIndex
                mCapture = New OpenCvSharp.VideoCapture(iRealIndex, OpenCvSharp.VideoCaptureAPIs.DSHOW)
                If (Not mCapture.IsOpened) Then
                    Throw New ArgumentException("Unable to open video input device.")
                End If

                ' Try to read the first frame before we change anything. Just in case properties wont apply instandly.
                CapturePoll(mCapture)

                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.g_mMessageLabel.Text = "Setting default device properties...")

                ' Disable any unneeded stuff. Tho, some things wont work still...
                mCapture.AutoExposure = -1
                mCapture.AutoFocus = False
                mCapture.WhiteBalanceBlueU = -1
                mCapture.WhiteBalanceRedV = -1

                ' $TODO: For some reason, changing FourCC requires FrameH/FrameW to be set first everytime
                Dim iFrameH As Integer = 480
                Dim iFrameW As Integer = 640
                Dim iFrameR As Integer = 30

                Select Case (iCameraResolution)
                    Case ENUM_RESOLUTION.HD
                        iFrameH = 1080
                        iFrameW = 1920

                End Select

                Dim iUnscaledFrameH = iFrameH
                Dim iUnscaledFrameW = iFrameW

                SyncLock g_mThreadLock
                    For i = 0 To g_mCaptureFrame.Length - 1
                        If (g_mCaptureFrame(i) IsNot Nothing AndAlso Not g_mCaptureFrame(i).IsDisposed) Then
                            g_mCaptureFrame(i).Dispose()
                            g_mCaptureFrame(i) = Nothing
                        End If
                        g_mCaptureFrame(i) = New OpenCvSharp.Mat(iFrameH, iFrameW, OpenCvSharp.MatType.CV_8UC3)
                    Next
                End SyncLock


                If (bIsPlayStationCamera) Then
                    iFrameH = 800
                    iFrameW = 1280 * 2
                Else
                    If (bSupersampling) Then
                        iFrameH *= 2
                        iFrameW *= 2
                    End If
                End If

                iFrameR = iCameraFramerate

                If (bUseMJPG AndAlso Not bIsPlayStationCamera) Then
                    Dim sLastCodec As String = mCapture.FourCC

                    mCapture.FrameHeight = iFrameH
                    mCapture.FrameWidth = iFrameW
                    mCapture.Fps = iFrameR
                    mCapture.FourCC = "mjpg"
                    CapturePoll(mCapture)

                    If (mCapture.FourCC <> "mjpg") Then
                        mCapture.FrameHeight = iFrameH
                        mCapture.FrameWidth = iFrameW
                        mCapture.Fps = iFrameR
                        mCapture.FourCC = "MJPG"
                        CapturePoll(mCapture)
                    End If

                    ' Failed fall back to last codec
                    If (mCapture.FourCC <> "MJPG") Then
                        mCapture.FrameHeight = iFrameH
                        mCapture.FrameWidth = iFrameW
                        mCapture.Fps = iFrameR
                        mCapture.FourCC = sLastCodec
                        CapturePoll(mCapture)
                    End If
                Else
                    Dim sLastCodec As String = mCapture.FourCC

                    ' $TODO: For some reason, changing FourCC requires FrameH/FrameW to be set
                    mCapture.FrameHeight = iFrameH
                    mCapture.FrameWidth = iFrameW
                    mCapture.Fps = iFrameR
                    mCapture.FourCC = "yuy2"
                    CapturePoll(mCapture)

                    If (mCapture.FourCC <> "yuy2") Then
                        mCapture.FrameHeight = iFrameH
                        mCapture.FrameWidth = iFrameW
                        mCapture.Fps = iFrameR
                        mCapture.FourCC = "YUY2"
                        CapturePoll(mCapture)
                    End If

                    ' Failed fall back to last codec
                    If (mCapture.FourCC <> "YUY2") Then
                        mCapture.FrameHeight = iFrameH
                        mCapture.FrameWidth = iFrameW
                        mCapture.Fps = iFrameR
                        mCapture.FourCC = sLastCodec
                        CapturePoll(mCapture)
                    End If
                End If

                Dim sCurrentCodec As String = mCapture.FourCC
                If (sCurrentCodec.Trim(vbNullChar(0)).Length = 0) Then
                    sCurrentCodec = "UNKNOWN"
                End If

                If (sCurrentCodec = "MJPG") Then
                    sCurrentCodec &= " (compressed)"
                End If

                Dim sCurrentFramerate As String = CStr(CInt(mCapture.Fps))
                Dim sCurrentResolution As String = String.Format("{0}x{1}", mCapture.FrameWidth, mCapture.FrameHeight)
                Dim sScalingWarning As String = ""
                Dim sUnscaledResolution As String = String.Format("{0}x{1}", iUnscaledFrameW, iUnscaledFrameH)
                If (Not bIsPlayStationCamera AndAlso sCurrentResolution <> sUnscaledResolution) Then
                    sScalingWarning &= String.Format(" (scaled to {0})", sUnscaledResolution)
                End If

                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.Label_DeviceCodec.Text = String.Format("Codec: {0}", sCurrentCodec))
                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.Label_DeviceResolution.Text = String.Format("Resolution: {0}@{1}Hz{2}", sCurrentResolution, sCurrentFramerate, sScalingWarning))
                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.g_mMessageLabel.Text = "Probing device properties...")

                ' $TODO: Minimize copy paste code. This looks like ass.
                ' Probing exposure
                Dim iExposureDefault As Double = mCapture.Exposure
                Dim iExposureMin As Double = 0
                Dim iExposureMax As Double = 0

                If (True) Then
                    For j = 0 To 1
                        mCapture.Exposure = 0

                        For i = 1 To 255
                            If (j = 0) Then
                                mCapture.Exposure = (i * PROBE_MULTIPLY)
                                CapturePoll(mCapture)

                                If (mCapture.Exposure = iExposureMax) Then
                                    iExposureMax = mCapture.Exposure
                                    Exit For
                                End If

                                iExposureMax = mCapture.Exposure
                            Else
                                mCapture.Exposure = -(i * PROBE_MULTIPLY)
                                CapturePoll(mCapture)

                                If (mCapture.Exposure = iExposureMin) Then
                                    iExposureMin = mCapture.Exposure
                                    Exit For
                                End If

                                iExposureMin = mCapture.Exposure
                            End If
                        Next
                    Next

                    mCapture.Exposure = iExposureDefault
                End If

                ' Probing gain
                Dim iGainDefault As Double = mCapture.Gain
                Dim iGainMin As Double = 0
                Dim iGainMax As Double = 0

                If (True) Then
                    For j = 0 To 1
                        mCapture.Gain = 0

                        For i = 1 To 255
                            If (j = 0) Then
                                mCapture.Gain = (i * PROBE_MULTIPLY)
                                CapturePoll(mCapture)

                                If (mCapture.Gain = iGainMax) Then
                                    iGainMax = mCapture.Gain
                                    Exit For
                                End If

                                iGainMax = mCapture.Gain
                            Else
                                mCapture.Gain = -(i * PROBE_MULTIPLY)
                                CapturePoll(mCapture)

                                If (mCapture.Gain = iGainMin) Then
                                    iGainMin = mCapture.Gain
                                    Exit For
                                End If

                                iGainMin = mCapture.Gain
                            End If
                        Next
                    Next

                    mCapture.Gain = iGainDefault
                End If

                ' Probing gamma
                Dim iGammaDefault As Double = mCapture.Gamma
                Dim iGammaMin As Double = 0
                Dim iGammaMax As Double = 0

                If (True) Then
                    For j = 0 To 1
                        mCapture.Gamma = 0

                        For i = 1 To 255
                            If (j = 0) Then
                                mCapture.Gamma = (i * PROBE_MULTIPLY)
                                CapturePoll(mCapture)

                                If (mCapture.Gamma = iGammaMax) Then
                                    iGammaMax = mCapture.Gamma
                                    Exit For
                                End If

                                iGammaMax = mCapture.Gamma
                            Else
                                mCapture.Gamma = -(i * PROBE_MULTIPLY)
                                CapturePoll(mCapture)

                                If (mCapture.Gamma = iGammaMin) Then
                                    iGammaMin = mCapture.Gamma
                                    Exit For
                                End If

                                iGammaMin = mCapture.Gamma
                            End If
                        Next
                    Next

                    mCapture.Gamma = iGammaDefault
                End If

                ' Probing constrast
                Dim iContrastDefault As Double = mCapture.Contrast
                Dim iContrastMin As Double = 0
                Dim iContrastMax As Double = 0

                If (True) Then
                    For j = 0 To 1
                        mCapture.Contrast = 0

                        For i = 1 To 255
                            If (j = 0) Then
                                mCapture.Contrast = (i * PROBE_MULTIPLY)
                                CapturePoll(mCapture)

                                If (mCapture.Contrast = iContrastMax) Then
                                    iContrastMax = mCapture.Contrast
                                    Exit For
                                End If

                                iContrastMax = mCapture.Contrast
                            Else
                                mCapture.Contrast = -(i * PROBE_MULTIPLY)
                                CapturePoll(mCapture)

                                If (mCapture.Contrast = iContrastMin) Then
                                    iContrastMin = mCapture.Contrast
                                    Exit For
                                End If

                                iContrastMin = mCapture.Contrast
                            End If
                        Next
                    Next

                    mCapture.Contrast = iContrastDefault
                End If

                m_Capture = mCapture
                mCapture = Nothing

                SyncLock g_mThreadInitLock
                    g_bInitalized = True
                End SyncLock

                ClassUtils.SyncInvoke(g_mUCVirtualTrackerItem, Sub()
                                                                   Try
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = True
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True

                                                                       Dim bDisabled As Boolean = (iExposureMin = iExposureMax)
                                                                       If (bDisabled) Then
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Enabled = False
                                                                           Return
                                                                       End If

                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Minimum = CInt(iExposureMin)
                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Maximum = CInt(iExposureMax)

                                                                       ' Has a value been set?
                                                                       If (Not CBool(g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Tag)) Then
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Value = CInt(Math.Max(iExposureMin, Math.Min(iExposureMax, iExposureDefault)))
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Tag = True
                                                                       End If

                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Enabled = True
                                                                   Finally
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = False
                                                                   End Try

                                                                   Try
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True
                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceExposure_ValueChanged(Nothing, Nothing)
                                                                   Finally
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                                   End Try
                                                               End Sub)

                ClassUtils.SyncInvoke(g_mUCVirtualTrackerItem, Sub()
                                                                   Try
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = True
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True

                                                                       Dim bDisabled As Boolean = (iGainMin = iGainMax)
                                                                       If (bDisabled) Then
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Enabled = False
                                                                           Return
                                                                       End If

                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Minimum = CInt(iGainMin)
                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Maximum = CInt(iGainMax)

                                                                       If (Not CBool(g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Tag)) Then
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Value = CInt(Math.Max(iGainMin, Math.Min(iGainMax, iGainDefault)))
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Tag = True
                                                                       End If

                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Enabled = True
                                                                   Finally
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = False
                                                                   End Try

                                                                   Try
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True
                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGain_ValueChanged(Nothing, Nothing)
                                                                   Finally
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                                   End Try
                                                               End Sub)

                ClassUtils.SyncInvoke(g_mUCVirtualTrackerItem, Sub()
                                                                   Try
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = True
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True

                                                                       Dim bDisabled As Boolean = (iGammaMin = iGammaMax)
                                                                       If (bDisabled) Then
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Enabled = False
                                                                           Return
                                                                       End If

                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Minimum = CInt(iGammaMin)
                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Maximum = CInt(iGammaMax)

                                                                       If (Not CBool(g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Tag)) Then
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Value = CInt(Math.Max(iGammaMin, Math.Min(iGammaMax, iGammaDefault)))
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Tag = True
                                                                       End If

                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Enabled = True
                                                                   Finally
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = False
                                                                   End Try

                                                                   Try
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True
                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGamma_ValueChanged(Nothing, Nothing)
                                                                   Finally
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                                   End Try
                                                               End Sub)

                ClassUtils.SyncInvoke(g_mUCVirtualTrackerItem, Sub()
                                                                   Try
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = True
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True

                                                                       Dim bDisabled As Boolean = (iContrastMin = iContrastMax)
                                                                       If (bDisabled) Then
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Enabled = False
                                                                           Return
                                                                       End If

                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Minimum = CInt(iContrastMin)
                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Maximum = CInt(iContrastMax)

                                                                       If (Not CBool(g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Tag)) Then
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Value = CInt(Math.Max(iContrastMin, Math.Min(iContrastMax, iContrastDefault)))
                                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Tag = True
                                                                       End If

                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Enabled = True
                                                                   Finally
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = False
                                                                   End Try

                                                                   Try
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True
                                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast_ValueChanged(Nothing, Nothing)
                                                                   Finally
                                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                                   End Try
                                                               End Sub)

                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.g_mMessageLabel.Visible = False)
                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.Enabled = True)

                ' Start all needed threads
                StartCaptureThread(False)
                StartPipeThread(False)
                StartDeviceWatchodogThread(False)

            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)

                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.Dispose())
            Finally
                Try
                    If (mCapture IsNot Nothing AndAlso Not mCapture.IsDisposed) Then
                        mCapture.Dispose()
                        mCapture = Nothing
                    End If
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLog(ex)
                End Try
            End Try
        End Sub

        Private Sub CapturePoll(mCapture As OpenCvSharp.VideoCapture)
            Using mMat As New OpenCvSharp.Mat
                mCapture.Read(mMat)
            End Using
        End Sub

        Private Sub CaptureThread()
            Dim mFrameImage As New Stopwatch
            Dim mFramePrint As New Stopwatch
            mFrameImage.Start()
            mFramePrint.Start()

            Dim iFpsCount As Integer = 0

            Try
                Using mFrame As New OpenCvSharp.Mat()
                    While True
                        Dim bExceptionSleep As Boolean = False

                        Try
                            If (m_Capture Is Nothing) Then
                                Throw New ArgumentException("Video capture is NULL.")
                            End If

                            If (Not m_Capture.IsOpened) Then
                                Throw New ArgumentException("Video capture not open.")
                            End If

                            If (Not m_Capture.Read(mFrame)) Then
                                Throw New ArgumentException("Unable to read video capture.")
                            End If

                            If (mFrame.Empty) Then
                                Throw New ArgumentException("Frame empty.")
                            End If

                            ' Calculate FPS
                            If (True) Then
                                iFpsCount += 1
                                If (mFramePrint.ElapsedMilliseconds > 1000) Then
                                    Dim iNewFpsCount As Integer = iFpsCount

                                    ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.SetFpsText(iNewFpsCount, -1))

                                    mFramePrint.Restart()
                                    iFpsCount = 0
                                End If
                            End If

                            ' Copy to global frame
                            If (m_IsPlayStationCamera) Then
                                For i = 0 To m_CaptureFrameLength - 1
                                    Using mNewFrame = mFrame(PS4CAM_EYE_HEIGHT_PADDING, mFrame.Rows - PS4CAM_EYE_HEIGHT_PADDING, 0 + PS4CAM_EYE_STARTPADDING + (PS4CAM_EYE_WDITH * i), PS4CAM_EYE_STARTPADDING + PS4CAM_EYE_WDITH + (PS4CAM_EYE_WDITH * i))
                                        Using mNewFrame2 = mNewFrame.Resize(New OpenCvSharp.Size(m_CaptureFrame(i).Cols, m_CaptureFrame(i).Rows), 0, 0, OpenCvSharp.InterpolationFlags.Linear)
                                            Using mNewFrame3 = mNewFrame2.Flip(OpenCvSharp.FlipMode.Y)
                                                mNewFrame3.CopyTo(m_CaptureFrame(i))
                                            End Using
                                        End Using
                                    End Using
                                Next
                            Else
                                ' Sometimes setting resolutions on devices wont work. (e.g. Kinect One)
                                ' We NEED to resize the image to 640x480 because our buffer is only that size!
                                If (mFrame.Cols <> m_CaptureFrame(0).Cols OrElse mFrame.Rows <> m_CaptureFrame(0).Rows) Then
                                    Dim mInterpolation As OpenCvSharp.InterpolationFlags = GetImageInterpolation()

                                    Using mNewFrame = mFrame.Resize(New OpenCvSharp.Size(m_CaptureFrame(0).Cols, m_CaptureFrame(0).Rows), 0, 0, mInterpolation)
                                        mNewFrame.CopyTo(mFrame)
                                    End Using
                                End If


                                ' PSEyes have their Y flipped.
                                ' But some video input devices do Not. So flip them here instead.
                                If (m_FlipImage) Then
                                    Using mNewMat = mFrame.Flip(OpenCvSharp.FlipMode.Y)
                                        mNewMat.CopyTo(mFrame)
                                    End Using
                                End If

                                mFrame.CopyTo(m_CaptureFrame(0))
                            End If

                            For i = 0 To g_mPipEvent.Length - 1
                                g_mPipEvent(i).Set()
                            Next

                            ' Preview captured image
                            ' $TODO: Quite performance heavy, find a better way.
                            If (mFrameImage.ElapsedMilliseconds > (1000 / 30)) Then
                                mFrameImage.Restart()

                                If (g_bShowCaptureImage) Then
                                    Dim mBitmap As Bitmap = Nothing

                                    Using mStream As New IO.MemoryStream
                                        Dim iByte As Byte() = mFrame.ImEncode()

                                        mStream.Write(iByte, 0, iByte.Length)

                                        mBitmap = New Bitmap(mStream)
                                    End Using

                                    ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem,
                                                        Sub()
                                                            If (g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image IsNot Nothing) Then
                                                                g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image.Dispose()
                                                                g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image = Nothing
                                                            End If

                                                            g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image = mBitmap
                                                        End Sub)
                                Else
                                    ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem,
                                                        Sub()
                                                            If (g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image IsNot Nothing) Then
                                                                g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image.Dispose()
                                                                g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image = Nothing
                                                            End If

                                                            g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image = Nothing
                                                        End Sub)
                                End If
                            End If
                        Catch ex As Threading.ThreadAbortException
                            Throw
                        Catch ex As Exception
                            bExceptionSleep = True
                            ClassAdvancedExceptionLogging.WriteToLog(ex)

                            If (mFramePrint.ElapsedMilliseconds > 1000) Then
                                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.SetFpsText(0, -1))

                                mFramePrint.Restart()
                                iFpsCount = 0
                            End If
                        End Try

                        ' Thread.Abort will not trigger inside a Try/Catch
                        If (bExceptionSleep) Then
                            bExceptionSleep = False
                            Threading.Thread.Sleep(1000)
                        End If
                    End While
                End Using
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLog(ex)
            End Try
        End Sub

        Private Sub PipeThreadPrimary()
            DoPipeLogic(0)
        End Sub

        Private Sub PipeThreadSecondary()
            DoPipeLogic(1)
        End Sub

        Private Sub DoPipeLogic(iPipeID As Integer)
            Try
                Dim iBytes = New Byte() {}

                Dim mFrameImage As New Stopwatch
                Dim mFramePrint As New Stopwatch
                mFrameImage.Start()
                mFramePrint.Start()

                Dim iFpsCount As Integer = 0

                While True
                    Dim bExceptionSleep As Boolean = False

                    Try
                        Dim iPipeIndex As Integer = m_PipePrimaryIndex

                        If (iPipeIndex < 0) Then
                            Throw New ArgumentException("Invalid pipe index.")
                        End If

                        Dim sPipeName As String = String.Format("PSMoveSerivceEx\VirtPSeyeStream{0}_{1}", CInt(m_CaptureResolution(iPipeID)), iPipeIndex + iPipeID)

                        Using mPipe As New IO.Pipes.NamedPipeClientStream(".", sPipeName, IO.Pipes.PipeDirection.Out)
                            ' The thread when aborting will hang if we dont put a timeout.
                            mPipe.Connect(5000)

                            While True
                                ' Calculate FPS
                                If (True) Then
                                    iFpsCount += 1

                                    If (mFramePrint.ElapsedMilliseconds > 1000) Then
                                        ' $TODO Add for each pipe
                                        If (iPipeID = 0) Then
                                            Dim iNewFpsCount As Integer = iFpsCount

                                            ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.SetFpsText(-1, iNewFpsCount))
                                        End If

                                        mFramePrint.Restart()
                                        iFpsCount = 0
                                    End If
                                End If

                                g_mPipEvent(iPipeID).WaitOne(New TimeSpan(0, 0, 5))

                                Dim VIRT_BUFFER_SIZE As Integer = (m_CaptureFrame(iPipeID).Height * m_CaptureFrame(iPipeID).Width * 3)
                                If (iBytes.Length - 1 <> VIRT_BUFFER_SIZE) Then
                                    iBytes = New Byte(VIRT_BUFFER_SIZE) {}
                                End If

                                Marshal.Copy(m_CaptureFrame(iPipeID).Data, iBytes, 0, iBytes.Length)

                                ' Write to pipe and wait for response.
                                ' $TODO Latency is quite ok but somewhat noticeable
                                mPipe.Write(iBytes, 0, iBytes.Length)
                                mPipe.WaitForPipeDrain()

                                SyncLock g_mThreadLock
                                    g_bPipeConnected(iPipeID) = True
                                End SyncLock
                            End While
                        End Using
                    Catch ex As Threading.ThreadAbortException
                        Throw
                    Catch ex As Exception
                        bExceptionSleep = True
                        ClassAdvancedExceptionLogging.WriteToLog(ex)

                        SyncLock g_mThreadLock
                            g_bPipeConnected(iPipeID) = False
                        End SyncLock

                        If (mFramePrint.ElapsedMilliseconds > 1000) Then
                            ' $TODO Add for each pipe
                            If (iPipeID = 0) Then
                                ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() g_mUCVirtualTrackerItem.SetFpsText(-1, 0))
                            End If

                            mFramePrint.Restart()
                            iFpsCount = 0
                        End If
                    End Try

                    ' Thread.Abort will not trigger inside a Try/Catch
                    If (bExceptionSleep) Then
                        bExceptionSleep = False
                        Threading.Thread.Sleep(1000)
                    End If
                End While
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLog(ex)
            End Try
        End Sub

        Private Sub DeviceWatchdogThread()
            While True
                Dim bExceptionSleep As Boolean = False

                Try
                    Threading.Thread.Sleep(5000)

                    If (Not IsDeviceValid()) Then
                        ' The device has changed somehow! Re-initiate!
                        ClassUtils.AsyncInvoke(g_mUCVirtualTrackerItem, Sub() StartInitThread(True))

                        Return
                    End If
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

        Class ClassConfig
            Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "devices.ini")

            Private g_mClassCaptureLogic As ClassCaptureLogic
            Private g_bConfigLoaded As Boolean = False

            Public Sub New(_ClassCaptureLogic As ClassCaptureLogic)
                g_mClassCaptureLogic = _ClassCaptureLogic
            End Sub

            Public Shared Function CanDeviceAutostart(sDevicePath As String) As Boolean
                Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.Read)
                    Using mIni As New ClassIni(mStream)
                        Return (mIni.ReadKeyValue(sDevicePath, "Autostart", "False") = "True")
                    End Using
                End Using

                Return False
            End Function

            Public Sub SaveConfig()
                If (Not g_bConfigLoaded) Then
                    Return
                End If

                Dim sDevicePath As String = g_mClassCaptureLogic.m_DevicePath

                Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)
                        Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "FriendlyName", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.Label_FriendlyName.Text)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "DeviceExposure", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Value)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "DeviceGain", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Value)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "DeviceGamma", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Value)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "DeviceContrast", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Value)))

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "TrackerId", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.ComboBox_DeviceTrackerId.SelectedIndex)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "FlipImageHorizontal", If(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.CheckBox_FlipHorizontal.Checked, "True", "False")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "ImageInterpolation", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.ComboBox_ImageInterpolation.SelectedIndex)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "UseMJPG", If(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.CheckBox_UseMjpg.Checked, "True", "False")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Supersampling", If(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.CheckBox_DeviceSupersampling.Checked, "True", "False")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Resolution", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.ComboBox_CameraResolution.SelectedIndex)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Framerate", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.ComboBox_CameraFramerate.SelectedIndex)))

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Autostart", If(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.CheckBox_Autostart.Checked, "True", "False")))

                        mIni.WriteKeyValue(mIniContent.ToArray)
                    End Using
                End Using
            End Sub

            Public Sub LoadConfig()
                Dim mUCVirtualTrackerItem = g_mClassCaptureLogic.g_mUCVirtualTrackerItem

                Dim sDevicePath As String = g_mClassCaptureLogic.m_DevicePath

                Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)
                        SetTrackBarClamp(mUCVirtualTrackerItem.TrackBar_DeviceExposure, mIni.ReadKeyValue(sDevicePath, "DeviceExposure", Nothing), True, True)
                        SetTrackBarClamp(mUCVirtualTrackerItem.TrackBar_DeviceGain, mIni.ReadKeyValue(sDevicePath, "DeviceGain", Nothing), True, True)
                        SetTrackBarClamp(mUCVirtualTrackerItem.TrackBar_DeviceGamma, mIni.ReadKeyValue(sDevicePath, "DeviceGamma", Nothing), True, True)
                        SetTrackBarClamp(mUCVirtualTrackerItem.TrackBar_DeviceConstrast, mIni.ReadKeyValue(sDevicePath, "DeviceContrast", Nothing), True, True)

                        SetComboBoxClamp(mUCVirtualTrackerItem.ComboBox_DeviceTrackerId, CInt(mIni.ReadKeyValue(sDevicePath, "TrackerId", "0")))
                        mUCVirtualTrackerItem.CheckBox_FlipHorizontal.Checked = (mIni.ReadKeyValue(sDevicePath, "FlipImageHorizontal", "True") = "True")
                        SetComboBoxClamp(mUCVirtualTrackerItem.ComboBox_ImageInterpolation, CInt(mIni.ReadKeyValue(sDevicePath, "ImageInterpolation", CStr(ENUM_INTERPOLATION.BILINEAR))))
                        mUCVirtualTrackerItem.CheckBox_UseMjpg.Checked = (mIni.ReadKeyValue(sDevicePath, "UseMJPG", "False") = "True")
                        mUCVirtualTrackerItem.CheckBox_DeviceSupersampling.Checked = (mIni.ReadKeyValue(sDevicePath, "Supersampling", "False") = "True")
                        SetComboBoxClamp(mUCVirtualTrackerItem.ComboBox_CameraResolution, CInt(mIni.ReadKeyValue(sDevicePath, "Resolution", CStr(ENUM_RESOLUTION.SD))))
                        SetComboBoxClamp(mUCVirtualTrackerItem.ComboBox_CameraFramerate, CInt(mIni.ReadKeyValue(sDevicePath, "Framerate", "0")))

                        mUCVirtualTrackerItem.CheckBox_Autostart.Checked = (mIni.ReadKeyValue(sDevicePath, "Autostart", "False") = "True")
                    End Using
                End Using

                g_bConfigLoaded = True
            End Sub

            Private Sub SetTrackBarClamp(mControl As TrackBar, iValue As String, bTagFalseIfNothing As Boolean, bAdjustRange As Boolean)
                If (bTagFalseIfNothing AndAlso iValue Is Nothing) Then
                    mControl.Tag = False
                    Return
                End If

                If (bAdjustRange) Then
                    If (CInt(iValue) < mControl.Minimum) Then
                        mControl.Minimum = CInt(iValue)
                    End If

                    If (CInt(iValue) > mControl.Maximum) Then
                        mControl.Maximum = CInt(iValue)
                    End If
                End If

                mControl.Value = Math.Max(mControl.Minimum, Math.Min(mControl.Maximum, CInt(iValue)))
                mControl.Tag = True
            End Sub

            Private Sub SetComboBoxClamp(mControl As ComboBox, iIndex As Integer)
                If (mControl.Items.Count = 0) Then
                    Return
                End If

                mControl.SelectedIndex = Math.Max(0, Math.Min(mControl.Items.Count - 1, iIndex))
            End Sub
        End Class

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects). 
                    If (g_mInitThread IsNot Nothing AndAlso g_mInitThread.IsAlive) Then
                        g_mInitThread.Abort()
                        g_mInitThread.Join()
                        g_mInitThread = Nothing
                    End If

                    If (g_mCaptureThread IsNot Nothing AndAlso g_mCaptureThread.IsAlive) Then
                        g_mCaptureThread.Abort()
                        g_mCaptureThread.Join()
                        g_mCaptureThread = Nothing
                    End If

                    For i = 0 To g_mPipeThread.Length - 1
                        If (g_mPipeThread(i) IsNot Nothing AndAlso g_mPipeThread(i).IsAlive) Then
                            g_mPipeThread(i).Abort()
                            g_mPipeThread(i).Join()
                            g_mPipeThread(i) = Nothing
                        End If
                    Next

                    If (g_mDeviceWatchdogThread IsNot Nothing AndAlso g_mDeviceWatchdogThread.IsAlive) Then
                        g_mDeviceWatchdogThread.Abort()
                        g_mDeviceWatchdogThread.Join()
                        g_mDeviceWatchdogThread = Nothing
                    End If

                    If (g_mCapture IsNot Nothing AndAlso Not g_mCapture.IsDisposed) Then
                        g_mCapture.Dispose()
                        g_mCapture = Nothing
                    End If

                    For i = 0 To g_mCaptureFrame.Length - 1
                        If (g_mCaptureFrame(i) IsNot Nothing AndAlso Not g_mCaptureFrame(i).IsDisposed) Then
                            g_mCaptureFrame(i).Dispose()
                            g_mCaptureFrame(i) = Nothing
                        End If
                    Next
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim mHelp As New FormRtfHelp
        mHelp.RichTextBox_Help.Rtf = My.Resources.HelpVirtualTracker
        mHelp.ShowDialog(Me)
    End Sub

    Private Sub LinkLabel_MiscSettings_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_MiscSettings.LinkClicked
        If (g_mClassCaptureLogic.m_Capture Is Nothing) Then
            Return
        End If

        If (Not g_mClassCaptureLogic.m_Capture.IsOpened) Then
            MessageBox.Show("Video input device not opened!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        g_mClassCaptureLogic.m_Capture.Settings = 0
        g_mClassCaptureLogic.m_Capture.Settings = 1
    End Sub

    Private Sub PictureBox_CaptureImage_Click(sender As Object, e As EventArgs) Handles PictureBox_CaptureImage.Click
        SetPreviewFullscreen(Not IsPreviewFullscreen())
    End Sub

    Private Sub UCVirtualTrackerItem_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If (Me.Disposing OrElse Me.IsDisposed) Then
            Return
        End If

        If (Me.Visible) Then
            Return
        End If

        CheckBox_ShowCaptureImage.Checked = False
        SetPreviewFullscreen(False)
    End Sub
End Class
