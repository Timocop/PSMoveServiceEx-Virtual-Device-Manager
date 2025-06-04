Imports System.Numerics
Imports PSMSVirtualDeviceManager.ClassLogDiagnostics

Public Class ClassLogManageServiceDevices
    Implements ILogAction

    Public Shared ReadOnly SECTION_VDM_SERVICE_DEVICES As String = "VDM Service Devices"

    Public Shared ReadOnly LOG_ISSUE_EMPTY As String = "Log is unavailable"
    Public Shared ReadOnly LOG_ISSUE_LIMITED_TRACKING As String = "Very limited tracking quality"
    Public Shared ReadOnly LOG_ISSUE_NO_OPTICAL_TRACKING As String = "No optical tracking"
    Public Shared ReadOnly LOG_ISSUE_VIRTUAL_HMD_DEPRICATED As String = "Virtual Head-mounted Displays deprecated"
    Public Shared ReadOnly LOG_ISSUE_NO_CONTROLLERS As String = "No controllers found"
    Public Shared ReadOnly LOG_ISSUE_NO_HMDS As String = "No head-mounted displays found"
    Public Shared ReadOnly LOG_ISSUE_MULTIPLE_HMDS As String = "Multiple head-mounted displays found"
    Public Shared ReadOnly LOG_ISSUE_BAD_COLOR_CALIBRATION As String = "Bad color calibration for device"
    Public Shared ReadOnly LOG_ISSUE_COLOR_COLLISION As String = "Possible color collsion between devices"
    Public Shared ReadOnly LOG_ISSUE_BAD_TRACKING_COLOR As String = "Device uses wrong tracking color"
    Public Shared ReadOnly LOG_ISSUE_BAD_MAGNETOMETER As String = "Missing or uncalibrated magnetometer"
    Public Shared ReadOnly LOG_ISSUE_BAD_GYROSCOPE As String = "Uncalibrated gyroscope"
    Public Shared ReadOnly LOG_ISSUE_BAD_ACCELEROMETER As String = "Uncalibrated accelerometer"
    Public Shared ReadOnly LOG_ISSUE_PS4CAM_COLOR_SENSTIVITY As String = "Unoptimal color detection sensitivity"
    Public Shared ReadOnly LOG_ISSUE_TRACKER_BAD_RESOLUTION_PSVR As String = "Tracker resolution too low for Head-mounted Display"
    Public Shared ReadOnly LOG_ISSUE_TRACKER_BAD_FPS As String = "Tracker bad framrate"
    Public Shared ReadOnly LOG_ISSUE_TRACKER_BAD_TIMEOUT As String = "Tracker possible timeout"
    Public Shared ReadOnly LOG_ISSUE_TRACKER_FACING_EXCLUDED As String = "Facing trackers triangulation excluded"
    Public Shared ReadOnly LOG_ISSUE_TRACKER_SYNC_MODE As String = "Tracker distance too small for current synchronization mode"


    Enum ENUM_DEVICE_TYPE
        INVALID = 0
        CONTROLLER
        HMD
        TRACKER
    End Enum

    Structure STRUC_DEVICE_ITEM
        Dim iType As ENUM_DEVICE_TYPE
        Dim iId As Integer
        Dim sSerial As String

        Dim bPositionValid As Boolean
        Dim mPosition As Vector3

        Dim bOrientationValid As Boolean
        Dim mOrientation As Vector3

        Dim iAverageFps As Integer
    End Structure

    Structure STRUC_DEVICE_COLOR_CALIBRATION
        Dim iDeviceId As Integer
        Dim iDeviceType As ENUM_DEVICE_TYPE
        Dim iTrackerId As Integer
        Dim sColor As String

        Dim iHueCenter As Single
        Dim iHueRange As Single
        Dim iSaturationCenter As Single
        Dim iSaturationRange As Single
        Dim iValueCenter As Single
        Dim iValueRange As Single

        ReadOnly bIsValid As Boolean

        Public Sub New(_DeviceId As Integer,
                       _DeviceType As ENUM_DEVICE_TYPE,
                       _TrackerId As Integer,
                       _Color As String,
                       _HueCenter As Single,
                       _HueRange As Single,
                       _SaturationCenter As Single,
                       _SaturationRange As Single,
                       _ValueCenter As Single,
                       _ValueRange As Single)
            iDeviceId = _DeviceId
            iDeviceType = _DeviceType
            iTrackerId = _TrackerId
            sColor = _Color

            iHueCenter = _HueCenter
            iHueRange = _HueRange
            iSaturationCenter = _SaturationCenter
            iSaturationRange = _SaturationRange
            iValueCenter = _ValueCenter
            iValueRange = _ValueRange

            bIsValid = True
        End Sub
    End Structure

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
        If (g_mFormMain.g_mPSMoveServiceCAPI Is Nothing) Then
            Return
        End If

        If (Not g_mFormMain.g_mPSMoveServiceCAPI.m_IsServiceConnected) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder
        sTrackersList.Append("[Service]").AppendLine()
        sTrackersList.AppendFormat("ControllerCount={0}", g_mFormMain.g_mPSMoveServiceCAPI.GetControllersData.Length).AppendLine()
        sTrackersList.AppendFormat("HmdCount={0}", g_mFormMain.g_mPSMoveServiceCAPI.GetHmdsData.Length).AppendLine()
        sTrackersList.AppendFormat("TrackerCount={0}", g_mFormMain.g_mPSMoveServiceCAPI.GetTrackersData.Length).AppendLine()
        sTrackersList.AppendLine()

        Dim mControllers = g_mFormMain.g_mPSMoveServiceCAPI.GetControllersData
        For Each mItem In mControllers
            Dim mPos As Vector3 = mItem.m_Position
            Dim mAng As Vector3 = mItem.GetOrientationEuler()

            sTrackersList.AppendFormat("[Controller_{0}]", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("ID={0}", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("IsConnected={0}", mItem.m_IsConnected).AppendLine()
            sTrackersList.AppendFormat("IsTracking={0}", mItem.m_IsTracking).AppendLine()
            sTrackersList.AppendFormat("IsValid={0}", mItem.m_IsValid).AppendLine()
            sTrackersList.AppendFormat("Serial={0}", mItem.m_Serial).AppendLine()
            sTrackersList.AppendFormat("TrackingColor={0}", CInt(mItem.m_TrackingColor)).AppendLine()
            sTrackersList.AppendFormat("TrackingColorName={0}", mItem.m_TrackingColor.ToString).AppendLine()
            sTrackersList.AppendFormat("BatteryLevel={0}", mItem.m_BatteryLevel).AppendLine()
            sTrackersList.AppendFormat("AverageFps={0}", mItem.m_AverageFps).AppendLine()
            sTrackersList.AppendFormat("OutputSeqNum={0}", mItem.m_OutputSeqNum).AppendLine()
            sTrackersList.AppendFormat("Position={0}", String.Format("{0}, {1}, {2}",
                                                                        mPos.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                        mPos.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                        mPos.Z.ToString(Globalization.CultureInfo.InvariantCulture)
                                                                    )).AppendLine()
            sTrackersList.AppendFormat("Orientation={0}", String.Format("{0}, {1}, {2}",
                                                                            mAng.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mAng.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mAng.Z.ToString(Globalization.CultureInfo.InvariantCulture)
                                                                        )).AppendLine()

            sTrackersList.AppendLine()
        Next

        Dim mHmds = g_mFormMain.g_mPSMoveServiceCAPI.GetHmdsData
        For Each mItem In mHmds
            Dim mPos As Vector3 = mItem.m_Position
            Dim mAng As Vector3 = mItem.GetOrientationEuler()

            sTrackersList.AppendFormat("[Hmd_{0}]", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("ID={0}", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("IsConnected={0}", mItem.m_IsConnected).AppendLine()
            sTrackersList.AppendFormat("IsTracking={0}", mItem.m_IsTracking).AppendLine()
            sTrackersList.AppendFormat("IsValid={0}", mItem.m_IsValid).AppendLine()
            sTrackersList.AppendFormat("Serial={0}", mItem.m_Serial).AppendLine()
            sTrackersList.AppendFormat("AverageFps={0}", mItem.m_AverageFps).AppendLine()
            sTrackersList.AppendFormat("OutputSeqNum={0}", mItem.m_OutputSeqNum).AppendLine()
            sTrackersList.AppendFormat("Position={0}", String.Format("{0}, {1}, {2}",
                                                                        mPos.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                        mPos.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                        mPos.Z.ToString(Globalization.CultureInfo.InvariantCulture)
                                                                    )).AppendLine()
            sTrackersList.AppendFormat("Orientation={0}", String.Format("{0}, {1}, {2}",
                                                                            mAng.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mAng.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mAng.Z.ToString(Globalization.CultureInfo.InvariantCulture)
                                                                        )).AppendLine()

            sTrackersList.AppendLine()
        Next

        Dim mTrakcers = g_mFormMain.g_mPSMoveServiceCAPI.GetTrackersData
        For Each mItem In mTrakcers
            Dim mPos As Vector3 = mItem.m_Position
            Dim mAng As Vector3 = mItem.GetOrientationEuler()

            sTrackersList.AppendFormat("[Tracker_{0}]", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("ID={0}", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("Path={0}", mItem.m_Path).AppendLine()
            sTrackersList.AppendFormat("AverageFps={0}", mItem.m_AverageFps).AppendLine()
            sTrackersList.AppendFormat("OutputSeqNum={0}", mItem.m_OutputSeqNum).AppendLine()
            sTrackersList.AppendFormat("Position={0}", String.Format("{0}, {1}, {2}",
                                                                        mPos.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                        mPos.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                        mPos.Z.ToString(Globalization.CultureInfo.InvariantCulture)
                                                                    )).AppendLine()
            sTrackersList.AppendFormat("Orientation={0}", String.Format("{0}, {1}, {2}",
                                                                            mAng.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mAng.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mAng.Z.ToString(Globalization.CultureInfo.InvariantCulture)
                                                                        )).AppendLine()

            sTrackersList.AppendLine()
        Next

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_SERVICE_DEVICES
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckEmpty())
        mIssues.AddRange(CheckTrackerCount())
        mIssues.AddRange(CheckVirtualHmdObsolete())
        mIssues.AddRange(CheckDeviceCount())
        mIssues.AddRange(CheckColorCalibration())
        mIssues.AddRange(CheckMagnetometer())
        mIssues.AddRange(CheckGyroAndAccel())
        mIssues.AddRange(CheckPs4CameraColorSensitivity())
        mIssues.AddRange(CheckTrackerResolutionWithPSVR())
        mIssues.AddRange(CheckFps())
        mIssues.AddRange(CheckFacingTrackers())
        mIssues.AddRange(CheckTrackerDistanceSyncMode())
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
    End Function

    Public Function CheckEmpty() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_EMPTY,
            "Some diagnostic details are unavailable due to missing log data.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        If (sContent Is Nothing OrElse sContent.Trim.Length = 0) Then
            mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))
        End If

        Return mIssues.ToArray
    End Function

    Public Function CheckTrackerCount() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mOneTrackerTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_LIMITED_TRACKING,
            "You are using only one tracker for optical tracking. Triangulation is not possible with a single tracker, and tracking quality is significantly reduced.",
            "Add more trackers, such as an additional PlayStation Eye, Webcam or PlayStation 4 Stereo Camera.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )
        Dim mNoTrackerTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_NO_OPTICAL_TRACKING,
            "No trackers are detected, so optical device tracking is unavailable.",
            "Add trackers such as PlayStation Eyes, Webcams or PlayStation 4 Stereo Cameras to enable optical tracking.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim iTrackerCount As Integer = 0
        For Each mDevice In GetDevices()
            If (mDevice.iType <> ENUM_DEVICE_TYPE.TRACKER) Then
                Continue For
            End If

            iTrackerCount += 1
        Next

        Select Case (iTrackerCount)
            Case 0
                mIssues.Add(New STRUC_LOG_ISSUE(mNoTrackerTemplate))
            Case 1
                mIssues.Add(New STRUC_LOG_ISSUE(mOneTrackerTemplate))
        End Select

        Return mIssues.ToArray
    End Function

    Public Function CheckVirtualHmdObsolete() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_VIRTUAL_HMD_DEPRICATED,
            "You are using virtual head-mounted displays. Those types of virtual devices are deprecated due to limited functionality and will only be used for backwards compatibility.",
            "Do not use virtual head-mounted displays and use virtual controllers to track your head-mounted display instead. Unless the third-party application does not support controllers for Head-mounted Display tracking.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        For Each mDevice In GetDevices()
            If (mDevice.iType <> ENUM_DEVICE_TYPE.HMD) Then
                Continue For
            End If

            If (Not mDevice.sSerial.StartsWith("VirtualHMD")) Then
                Continue For
            End If

            mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))
            Exit For
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckDeviceCount() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mControllerTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_NO_CONTROLLERS,
            "",
            "",
            ENUM_LOG_ISSUE_TYPE.INFO
        )
        Dim mHmdTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_NO_HMDS,
            "",
            "",
            ENUM_LOG_ISSUE_TYPE.INFO
        )
        Dim mManyTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_MULTIPLE_HMDS,
            "There are currently {0} head-mounted displays available but some applications may only support a single device.",
            "Its recommended to reduce the number of head-mounted displays to a single device.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim iControllers As Integer = 0
        Dim iHmds As Integer = 0

        For Each mDevice In GetDevices()
            Select Case (mDevice.iType)
                Case ENUM_DEVICE_TYPE.CONTROLLER
                    iControllers += 1
                Case ENUM_DEVICE_TYPE.HMD
                    iHmds += 1
            End Select
        Next

        If (iControllers = 0) Then
            Dim mNewIssue As New STRUC_LOG_ISSUE(mControllerTemplate)
            mIssues.Add(mNewIssue)
        End If

        If (iHmds = 0) Then
            Dim mNewIssue As New STRUC_LOG_ISSUE(mHmdTemplate)
            mIssues.Add(mNewIssue)
        ElseIf (iHmds > 1) Then
            Dim mNewIssue As New STRUC_LOG_ISSUE(mManyTemplate)

            mNewIssue.sDescription = String.Format(mNewIssue.sDescription, iHmds)
            mNewIssue.sSolution = mNewIssue.sSolution

            mIssues.Add(mNewIssue)
        End If

        Return mIssues.ToArray
    End Function

    Public Function CheckColorCalibration() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mBadTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_COLOR_CALIBRATION,
            "Color calibration for {0} id {1} is not properly set on tracker id {2} which may cause tracking issues.",
            "Properly calibrate tracking colors for this device using PSMoveServiceEx Config Tool.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mColorCollisionTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_COLOR_COLLISION,
            "{0} id {1} has possible color collisions with {2} id {3} on tracker id {4} which may cause tracking issues.",
            "Properly calibrate color for this device or enable 'Prevent color collisions' setting in color calibration before sampling colors using PSMoveServiceEx Config Tool.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mWrongColorTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_TRACKING_COLOR,
            "The tracking color of {0} id {1} on tracker id {2} not match the selected tracking color ({3}) that is set for this device.",
            "Properly calibrate tracking colors for this device using PSMoveServiceEx Config Tool.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mServiceDevicesLog As New ClassLogManageServiceDevices(g_mFormMain, g_ClassLogContent)
        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)

        Dim mColorConfigs = GetDevicesColorCalibration()

        For Each mColorConfig As STRUC_DEVICE_COLOR_CALIBRATION In mColorConfigs
            If (Not mColorConfig.bIsValid) Then
                Continue For
            End If

            Dim sDeviceType As String = "<Unknown>"

            Select Case (mColorConfig.iDeviceType)
                Case ENUM_DEVICE_TYPE.CONTROLLER
                    sDeviceType = "Controller"

                Case ENUM_DEVICE_TYPE.HMD
                    sDeviceType = "Head-mounted Display"
            End Select

            ' Assuming the user didnt even change anything
            If (CInt(mColorConfig.iHueRange) = 10 AndAlso
                    CInt(mColorConfig.iSaturationCenter) = 255 AndAlso CInt(mColorConfig.iValueCenter) = 255 AndAlso
                    CInt(mColorConfig.iSaturationRange) = 32 AndAlso CInt(mColorConfig.iValueRange) = 32) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mBadTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, mColorConfig.iDeviceId, mColorConfig.iTrackerId)
                mIssues.Add(mNewIssue)

            ElseIf (mColorConfig.iSaturationCenter < 80.0F OrElse mColorConfig.iValueCenter < 80.0F) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mBadTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, mColorConfig.iDeviceId, mColorConfig.iTrackerId)
                mIssues.Add(mNewIssue)
            End If

            Dim bGoodColor As Boolean = True
            Dim iGoodColorRange As Integer = 25
            Select Case (mColorConfig.sColor.ToLower)
                Case "magenta"
                    bGoodColor = IsHueInRange(CInt(mColorConfig.iHueCenter), 150, iGoodColorRange)
                Case "cyan"
                    bGoodColor = IsHueInRange(CInt(mColorConfig.iHueCenter), 90, iGoodColorRange)
                Case "yellow"
                    bGoodColor = IsHueInRange(CInt(mColorConfig.iHueCenter), 30, iGoodColorRange)
                Case "red"
                    bGoodColor = IsHueInRange(CInt(mColorConfig.iHueCenter), 0, iGoodColorRange)
                Case "green"
                    bGoodColor = IsHueInRange(CInt(mColorConfig.iHueCenter), 60, iGoodColorRange)
                Case "blue"
                    bGoodColor = IsHueInRange(CInt(mColorConfig.iHueCenter), 120, iGoodColorRange)
                Case Else
                    ' Ignore custom colors
            End Select

            If (Not bGoodColor) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mWrongColorTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, mColorConfig.iDeviceId, mColorConfig.iTrackerId, mColorConfig.sColor)
                mIssues.Add(mNewIssue)
            End If

            ' Check for color collisions between devices
            For Each mOtherColorConfigs As STRUC_DEVICE_COLOR_CALIBRATION In mColorConfigs
                Dim sOtherDeviceType As String = "<Unknown>"

                Select Case (mOtherColorConfigs.iDeviceType)
                    Case ENUM_DEVICE_TYPE.CONTROLLER
                        sOtherDeviceType = "Controller"

                    Case ENUM_DEVICE_TYPE.HMD
                        sOtherDeviceType = "Head-mounted Display"
                End Select

                ' Must be the same tracker
                If (mColorConfig.iTrackerId <> mOtherColorConfigs.iTrackerId) Then
                    Continue For
                End If

                ' Dont check same device
                If (mColorConfig.iDeviceId = mOtherColorConfigs.iDeviceId AndAlso mColorConfig.iDeviceType = mOtherColorConfigs.iDeviceType) Then
                    Continue For
                End If

                If (Not IsHueInRange(CInt(mColorConfig.iHueCenter), CInt(mOtherColorConfigs.iHueCenter), CInt(mOtherColorConfigs.iHueRange))) Then
                    Continue For
                End If

                Dim mNewIssue As New STRUC_LOG_ISSUE(mColorCollisionTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, mColorConfig.iDeviceId, sOtherDeviceType, mOtherColorConfigs.iDeviceId, mColorConfig.iTrackerId)
                mIssues.Add(mNewIssue)
            Next
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckMagnetometer() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mBadTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_MAGNETOMETER,
            "The controller id {0} does not have a magnetometer or is not yet calibrated, which may cause orientation yaw drift over time.",
            "Calibrate the controllers magnetometer using PSMoveServiceEx Config Tool if available. Ignore this warning if the magnetometer is not available.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mServiceDevicesLog As New ClassLogManageServiceDevices(g_mFormMain, g_ClassLogContent)
        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)

        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                Continue For
            End If

            If (mDevice.iType <> ENUM_DEVICE_TYPE.CONTROLLER) Then
                Continue For
            End If

            ' Ignore virtual controllers
            If (mDevice.sSerial.StartsWith("VirtualController")) Then
                Continue For
            End If

            Dim mDeviceConfig = mServiceLog.FindConfigFromSerial(mDevice.sSerial)
            If (mDeviceConfig Is Nothing) Then
                Continue For
            End If

            Dim sExtX = Single.Parse(mDeviceConfig.GetValue(Of String)("Calibration\Magnetometer\Extents", "X", "-1"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
            Dim sExtY = Single.Parse(mDeviceConfig.GetValue(Of String)("Calibration\Magnetometer\Extents", "Y", "-1"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
            Dim sExtZ = Single.Parse(mDeviceConfig.GetValue(Of String)("Calibration\Magnetometer\Extents", "Z", "-1"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
            Dim sIdentX = Single.Parse(mDeviceConfig.GetValue(Of String)("Calibration\Magnetometer\Identity", "X", "-1"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
            Dim sIdentY = Single.Parse(mDeviceConfig.GetValue(Of String)("Calibration\Magnetometer\Identity", "Y", "-1"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
            Dim sIdentZ = Single.Parse(mDeviceConfig.GetValue(Of String)("Calibration\Magnetometer\Identity", "Z", "-1"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

            If (sExtX = 0 AndAlso sExtY = 0 AndAlso sExtZ = 0) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mBadTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, mDevice.iId)
                mIssues.Add(mNewIssue)

            ElseIf (sIdentX = 0 AndAlso sIdentY = 0 AndAlso sIdentZ = 0) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mBadTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, mDevice.iId)
                mIssues.Add(mNewIssue)
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckGyroAndAccel() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mBadGyroTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_GYROSCOPE,
            "The {0} id {1} gyroscope has not been calibrated yet, which may cause orientation drift.",
            "Calibrate the {0} gyroscope using PSMoveServiceEx Config Tool.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mBadAccelTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_ACCELEROMETER,
            "The {0} id {1} accelerometer has not been calibrated yet, which may cause orientation drift.",
            "Calibrate the {0} accelerometer using PSMoveServiceEx Config Tool.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mServiceDevicesLog As New ClassLogManageServiceDevices(g_mFormMain, g_ClassLogContent)
        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)

        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                Continue For
            End If

            Dim sDeviceType As String = "<Unknown>"

            Select Case (mDevice.iType)
                Case ENUM_DEVICE_TYPE.CONTROLLER
                    sDeviceType = "Controller"

                Case ENUM_DEVICE_TYPE.HMD
                    sDeviceType = "Head-mounted Display"
            End Select

            Dim mDeviceConfig = mServiceLog.FindConfigFromSerial(mDevice.sSerial)
            If (mDeviceConfig Is Nothing) Then
                Continue For
            End If

            Dim iGyroVariance As Single = Single.Parse(mDeviceConfig.GetValue(Of String)("Calibration\Gyro", "Variance", "-1"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
            Dim iAccelVariance As Single = Single.Parse(mDeviceConfig.GetValue(Of String)("Calibration\Accel", "Variance", "-1"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

            ' These are the default values set by PSMoveServiceEx
            Dim iPSMoveDefaultVariance As New KeyValuePair(Of Single, Single)(0.00035F, 0.0000072F)
            Dim iDualShockDefaultVariance As New KeyValuePair(Of Single, Single)(0.00000475F, 0.0000145F)
            Dim iMorpheusDefaultVariance As New KeyValuePair(Of Single, Single)(0.00000133875039F, 0.0F)

            Dim bBadGyro As Boolean = False
            Dim bBadAccel As Boolean = False

            Select Case (mDevice.iType)
                Case ENUM_DEVICE_TYPE.CONTROLLER
                    Select Case (iGyroVariance)
                        Case iPSMoveDefaultVariance.Key, iDualShockDefaultVariance.Key
                            bBadGyro = True
                    End Select

                    Select Case (iAccelVariance)
                        Case iPSMoveDefaultVariance.Value, iDualShockDefaultVariance.Value
                            bBadAccel = True
                    End Select

                Case ENUM_DEVICE_TYPE.HMD
                    Select Case (iGyroVariance)
                        Case iMorpheusDefaultVariance.Key
                            bBadGyro = True
                    End Select

                    Select Case (iAccelVariance)
                        Case iMorpheusDefaultVariance.Value
                            bBadAccel = True
                    End Select
            End Select

            If (bBadGyro) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mBadGyroTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, mDevice.iId)
                mNewIssue.sSolution = String.Format(mNewIssue.sSolution, sDeviceType)
                mIssues.Add(mNewIssue)
            End If

            If (bBadAccel) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mBadAccelTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, mDevice.iId)
                mNewIssue.sSolution = String.Format(mNewIssue.sSolution, sDeviceType)
                mIssues.Add(mNewIssue)
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckPs4CameraColorSensitivity() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_PS4CAM_COLOR_SENSTIVITY,
            "{0} id {1} color detection sensitivity on tracker id {2} is too low. " &
                "The PlayStation 4 stereo camera requires higher color detection sensitivity compared to other cameras due the edges of the image being darker than the center.",
            "Redo color calibration with higher color detection sensitivity for {0} id {1} on tracker id {2} using PSMoveServiceEx Config Tool.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mVirtualTrackersLog As New ClassLogManagerVirtualTrackers(g_mFormMain, g_ClassLogContent)

        Dim mColorConfigs = GetDevicesColorCalibration()

        For Each mDevice In mVirtualTrackersLog.GetDevices()
            If (Not mDevice.bIsPlayStationCamera) Then
                Continue For
            End If

            Dim mIndexes = New Integer() {mDevice.iPipePrimaryIndex, mDevice.iPipeSecondaryIndex}
            For Each iIndex In mIndexes
                If (iIndex < 0) Then
                    Continue For
                End If

                For Each mColorConfig In mColorConfigs
                    If (mColorConfig.iDeviceId < 0) Then
                        Continue For
                    End If

                    If (mColorConfig.iTrackerId <> iIndex) Then
                        Continue For
                    End If

                    Dim sDeviceType As String = "<Unknown>"

                    Select Case (mColorConfig.iDeviceType)
                        Case ClassLogManageServiceDevices.ENUM_DEVICE_TYPE.CONTROLLER
                            sDeviceType = "Controller"
                        Case ClassLogManageServiceDevices.ENUM_DEVICE_TYPE.HMD
                            sDeviceType = "Head-mounted Display"
                    End Select

                    If (CInt(mColorConfig.iSaturationRange) < 64 OrElse CInt(mColorConfig.iValueRange) < 64) Then
                        Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                        mIssue.sDescription = String.Format(mIssue.sDescription, sDeviceType, mColorConfig.iDeviceId, iIndex)
                        mIssue.sSolution = String.Format(mIssue.sSolution, sDeviceType, mColorConfig.iDeviceId, iIndex)
                        mIssues.Add(mIssue)
                    End If
                Next
            Next
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckTrackerResolutionWithPSVR() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_TRACKER_BAD_RESOLUTION_PSVR,
            "The PlayStation VR head-mounted display (Morpheus) id {0} built-in tracking lights may not be tracked properly by tracker id {1} due to its low resolution.",
            "To ensure proper tracking, increase the resolution of tracker id {1}. Alternatively use a different/bigger tracking bulb for the PlayStation VR head-mounted display (Morpheus) id {0} to allow tracking on lower tracker resolutions.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)

        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                Continue For
            End If

            If (mDevice.iType <> ENUM_DEVICE_TYPE.HMD) Then
                Continue For
            End If

            If (Not mDevice.sSerial.StartsWith("MorpheusHMD")) Then
                Continue For
            End If

            Dim mDeviceConfig = mServiceLog.FindConfigFromSerial(mDevice.sSerial)
            If (mDeviceConfig Is Nothing) Then
                Continue For
            End If

            Dim sUsingCustomTracking As String = mDeviceConfig.GetValue(Of String)("", "use_custom_optical_tracking", "")
            If (String.IsNullOrEmpty(sUsingCustomTracking) OrElse sUsingCustomTracking.Trim.Length = 0) Then
                Continue For
            End If

            Dim sOverrideTrackingLights As String = mDeviceConfig.GetValue(Of String)("", "override_custom_tracking_leds", "")
            If (String.IsNullOrEmpty(sOverrideTrackingLights) OrElse sOverrideTrackingLights.Trim.Length = 0) Then
                Continue For
            End If

            If (sUsingCustomTracking = "true") Then
                Dim iOverrideTrackingLights As Integer = CInt(sOverrideTrackingLights)
                If (iOverrideTrackingLights > 0) Then
                    ' Build-in LEDs
                Else
                    Continue For
                End If
            Else
                ' Build-in LEDs
            End If

            For Each mOtherDevice In GetDevices()
                If (mOtherDevice.iId < 0) Then
                    Continue For
                End If

                If (mOtherDevice.iType <> ENUM_DEVICE_TYPE.TRACKER) Then
                    Continue For
                End If

                Dim mOtherDeviceConfig = mServiceLog.FindConfigFromSerial(mOtherDevice.sSerial)
                If (mOtherDeviceConfig Is Nothing) Then
                    Continue For
                End If

                Dim sResolutionWidth As String = mOtherDeviceConfig.GetValue("", "frame_width", "")
                If (String.IsNullOrEmpty(sResolutionWidth)) Then
                    Continue For
                End If

                Dim iResolutionWidth As Integer = CInt(sResolutionWidth)
                If (iResolutionWidth >= 1920) Then
                    Continue For
                End If

                Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iId, mOtherDevice.iId)
                mIssue.sSolution = String.Format(mIssue.sSolution, mDevice.iId, mOtherDevice.iId)
                mIssues.Add(mIssue)
            Next
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckFps() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_TRACKER_BAD_FPS,
            "The tracker id {0} framerate ({1} fps) is lower than the configured framerate ({2} fps). An unstable framerate may cause poor tracking performance.",
            "Check for connection issues and ensure all trackers are running at the same framerate. If bandwidth issues persist, try reducing the framerate.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mTimeoutTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_TRACKER_BAD_TIMEOUT,
            "Tracker id {0} may have timed out and is no longer receiving data.",
            "Check for connection issues and ensure all trackers are running at the same framerate. If bandwidth issues persist, try reducing the framerate.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)

        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                Continue For
            End If

            If (mDevice.iType <> ENUM_DEVICE_TYPE.TRACKER) Then
                Continue For
            End If

            If (mDevice.sSerial.StartsWith("VirtualTracker")) Then
                Continue For
            End If

            ' Not available
            If (mDevice.iAverageFps < 0) Then
                Continue For
            End If

            Dim mDeviceConfig = mServiceLog.FindConfigFromSerial(mDevice.sSerial)
            If (mDeviceConfig Is Nothing) Then
                Continue For
            End If


            Dim sConfigFramerate As String = mDeviceConfig.GetValue(Of String)("", "frame_rate", "")
            If (String.IsNullOrEmpty(sConfigFramerate) OrElse sConfigFramerate.Trim.Length = 0) Then
                Continue For
            End If

            Dim iConfigFramerate As Integer = CInt(sConfigFramerate)

            If (mDevice.iAverageFps < iConfigFramerate - 3) Then
                Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iId, mDevice.iAverageFps, iConfigFramerate)
                mIssues.Add(mIssue)

            ElseIf (mDevice.iAverageFps > iConfigFramerate + 7) Then
                ' PSEyes run at maximum thread fps when connection has lost. So jsut check for increased fps.
                Dim mIssue As New STRUC_LOG_ISSUE(mTimeoutTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iId)
                mIssues.Add(mIssue)
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckFacingTrackers() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mProcessedTrackerPairs As New HashSet(Of Integer)

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_TRACKER_FACING_EXCLUDED,
            "Excluded triangulation between tracker id {0} and tracker id {1} that are facing each other with an angle {2} in degrees.",
            "",
            ENUM_LOG_ISSUE_TYPE.INFO
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)

        Dim mTrackerConfig = mServiceLog.FindConfigFromSerial("TrackerManagerConfig")

        Dim iAngleLimit As Single = Single.Parse(mTrackerConfig.GetValue("", "tracker_deviation_exclude_angle", "35.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                Continue For
            End If

            If (mDevice.iType <> ENUM_DEVICE_TYPE.TRACKER) Then
                Continue For
            End If

            Dim mDeviceConfig = mServiceLog.FindConfigFromSerial(mDevice.sSerial)
            If (mDeviceConfig Is Nothing) Then
                Continue For
            End If

            Dim mTrackerOrientation As New Quaternion(
                Single.Parse(mDeviceConfig.GetValue("pose\orientation", "x", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                Single.Parse(mDeviceConfig.GetValue("pose\orientation", "y", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                Single.Parse(mDeviceConfig.GetValue("pose\orientation", "z", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                Single.Parse(mDeviceConfig.GetValue("pose\orientation", "w", "1.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
            )

            For Each mDeviceOther In GetDevices()
                If (mDeviceOther.iId < 0) Then
                    Continue For
                End If

                If (mDeviceOther.iType <> ENUM_DEVICE_TYPE.TRACKER) Then
                    Continue For
                End If

                If (mDeviceOther.iId = mDevice.iId) Then
                    Continue For
                End If

                Dim mDeviceOtherConfig = mServiceLog.FindConfigFromSerial(mDeviceOther.sSerial)
                If (mDeviceOtherConfig Is Nothing) Then
                    Continue For
                End If

                ' Dont process the same pair multiple times
                Dim iTrackerPairBits As Integer = (1 << mDevice.iId) Or (1 << mDeviceOther.iId)
                If (mProcessedTrackerPairs.Contains(iTrackerPairBits)) Then
                    Continue For
                End If

                mProcessedTrackerPairs.Add(iTrackerPairBits)

                Dim mOtherTrackerOrientation As New Quaternion(
                    Single.Parse(mDeviceOtherConfig.GetValue("pose\orientation", "x", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                    Single.Parse(mDeviceOtherConfig.GetValue("pose\orientation", "y", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                    Single.Parse(mDeviceOtherConfig.GetValue("pose\orientation", "z", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                    Single.Parse(mDeviceOtherConfig.GetValue("pose\orientation", "w", "1.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                )

                Dim fAngleDiff As Single = ClassMathUtils.CalculateAngleDegreesDifference(mTrackerOrientation, mOtherTrackerOrientation)
                If (fAngleDiff > 180.0F - iAngleLimit) Then
                    Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                    mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iId, mDeviceOther.iId, CInt(Math.Abs(fAngleDiff - 180.0F)))
                    mIssues.Add(mIssue)
                End If
            Next
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckTrackerDistanceSyncMode() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Const MAX_TRACKER_DISTANCE_CM As Integer = 100
        Const TRACKER_SYNC_FASTEST_AVAILABLE As Integer = 1

        Dim mProcessedTrackerPairs As New HashSet(Of Integer)

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_TRACKER_SYNC_MODE,
            "The distance between tracker id {0} and tracker id {1} is too short ({2} cm apart) for the current tracker synchronization mode and my result tracking issues.",
            "Set the tracker synchronization mode to 'Wait All' in PSMoveServiceEx Config Tool 'Advanced Settings' or increase the distance between both trackers.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)

        Dim mTrackerConfig = mServiceLog.FindConfigFromSerial("TrackerManagerConfig")

        Dim iTrackerSyncMode As Integer = Integer.Parse(mTrackerConfig.GetValue("", "tracker_sync_mode", "0"))

        ' Check if 'Fastest Available' sync mode
        If (iTrackerSyncMode <> TRACKER_SYNC_FASTEST_AVAILABLE) Then
            Return {}
        End If

        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                Continue For
            End If

            If (mDevice.iType <> ENUM_DEVICE_TYPE.TRACKER) Then
                Continue For
            End If

            Dim mDeviceConfig = mServiceLog.FindConfigFromSerial(mDevice.sSerial)
            If (mDeviceConfig Is Nothing) Then
                Continue For
            End If

            Dim mTrackerPosition As New Vector3(
                Single.Parse(mDeviceConfig.GetValue("pose\position", "x", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                Single.Parse(mDeviceConfig.GetValue("pose\position", "y", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                Single.Parse(mDeviceConfig.GetValue("pose\position", "z", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
            )

            For Each mDeviceOther In GetDevices()
                If (mDeviceOther.iId < 0) Then
                    Continue For
                End If

                If (mDeviceOther.iType <> ENUM_DEVICE_TYPE.TRACKER) Then
                    Continue For
                End If

                If (mDeviceOther.iId = mDevice.iId) Then
                    Continue For
                End If

                Dim mDeviceOtherConfig = mServiceLog.FindConfigFromSerial(mDeviceOther.sSerial)
                If (mDeviceOtherConfig Is Nothing) Then
                    Continue For
                End If

                ' Dont process the same pair multiple times
                Dim iTrackerPairBits As Integer = (1 << mDevice.iId) Or (1 << mDeviceOther.iId)
                If (mProcessedTrackerPairs.Contains(iTrackerPairBits)) Then
                    Continue For
                End If

                mProcessedTrackerPairs.Add(iTrackerPairBits)

                Dim mOtherTrackerPosition As New Vector3(
                    Single.Parse(mDeviceOtherConfig.GetValue("pose\position", "x", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                    Single.Parse(mDeviceOtherConfig.GetValue("pose\position", "y", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                    Single.Parse(mDeviceOtherConfig.GetValue("pose\position", "z", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                )

                Dim fTrackerDistance As Single = Vector3.Distance(mTrackerPosition, mOtherTrackerPosition)
                If (fTrackerDistance < MAX_TRACKER_DISTANCE_CM) Then
                    Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                    mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iId, mDeviceOther.iId, CInt(fTrackerDistance))
                    mIssues.Add(mIssue)
                End If
            Next
        Next

        Return mIssues.ToArray
    End Function

    Private Function IsHueInRange(iTarget As Integer, iCenter As Integer, iRange As Integer) As Boolean
        Dim iLower As Integer = (iCenter - iRange + 180) Mod 180
        Dim iUpper As Integer = (iCenter + iRange) Mod 180

        If (iLower < iUpper) Then
            Return (iTarget >= iLower AndAlso iTarget <= iUpper)
        Else
            Return (iTarget >= iLower OrElse iTarget <= iUpper)
        End If
    End Function

    Public Function GetDevices() As STRUC_DEVICE_ITEM()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mDeviceList As New List(Of STRUC_DEVICE_ITEM)
        Dim mDeviceProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("["c) AndAlso sLine.EndsWith("]"c)) Then
                Dim sDeviceKey As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewDevice As New STRUC_DEVICE_ITEM

                ' Optional
                If (mDeviceProp.ContainsKey("Position") AndAlso mDeviceProp("Position").Split(","c).Count = 3) Then
                    Dim mPos = mDeviceProp("Position").Split(","c)
                    mNewDevice.mPosition = New Vector3(
                        Single.Parse(mPos(0), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                        Single.Parse(mPos(1), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                        Single.Parse(mPos(2), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    )
                    mNewDevice.bPositionValid = True
                End If

                If (mDeviceProp.ContainsKey("Orientation") AndAlso mDeviceProp("Orientation").Split(","c).Count = 3) Then
                    Dim mAng = mDeviceProp("Orientation").Split(","c)
                    mNewDevice.mOrientation = New Vector3(
                        Single.Parse(mAng(0), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                        Single.Parse(mAng(1), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture),
                        Single.Parse(mAng(2), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                    )
                    mNewDevice.bOrientationValid = True
                End If

                If (mDeviceProp.ContainsKey("AverageFps")) Then
                    mNewDevice.iAverageFps = CInt(mDeviceProp("AverageFps"))
                Else
                    mNewDevice.iAverageFps = -1
                End If

                ' Required
                While True
                    Select Case (True)
                        Case sDeviceKey.StartsWith("Controller_")
                            mNewDevice.iType = ENUM_DEVICE_TYPE.CONTROLLER

                            If (mDeviceProp.ContainsKey("Serial")) Then
                                mNewDevice.sSerial = mDeviceProp("Serial")
                            Else
                                Exit While
                            End If

                        Case sDeviceKey.StartsWith("Hmd_")
                            mNewDevice.iType = ENUM_DEVICE_TYPE.HMD

                            If (mDeviceProp.ContainsKey("Serial")) Then
                                mNewDevice.sSerial = mDeviceProp("Serial")
                            Else
                                Exit While
                            End If

                        Case sDeviceKey.StartsWith("Tracker_")
                            mNewDevice.iType = ENUM_DEVICE_TYPE.TRACKER

                            If (mDeviceProp.ContainsKey("Path")) Then
                                mNewDevice.sSerial = mDeviceProp("Path")
                            Else
                                Exit While
                            End If

                        Case Else
                            Exit While
                    End Select

                    If (mDeviceProp.ContainsKey("ID")) Then
                        mNewDevice.iId = CInt(mDeviceProp("ID"))
                    Else
                        Exit While
                    End If

                    mDeviceList.Add(mNewDevice)
                    Exit While
                End While

                mDeviceProp.Clear()
            End If

            If (sLine.Contains("="c)) Then
                Dim sKey As String = sLine.Substring(0, sLine.IndexOf("="c))
                Dim sValue As String = sLine.Remove(0, sLine.IndexOf("="c) + 1)

                mDeviceProp(sKey) = sValue
            End If
        Next

        Return mDeviceList.ToArray
    End Function

    Public Function GetDevicesColorCalibration() As STRUC_DEVICE_COLOR_CALIBRATION()
        Dim mServiceDevicesLog As New ClassLogManageServiceDevices(g_mFormMain, g_ClassLogContent)
        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)

        Dim mColorSettings As New List(Of STRUC_DEVICE_COLOR_CALIBRATION)

        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                Continue For
            End If

            Dim sFullSerial As String = ""
            Dim sDeviceType As String = ""
            Dim bIsMorpheusHmd As Boolean = False

            Select Case (mDevice.iType)
                Case ENUM_DEVICE_TYPE.CONTROLLER
                    sFullSerial = String.Format("controller_{0}", mDevice.sSerial)
                    sDeviceType = "Controller"

                Case ENUM_DEVICE_TYPE.HMD
                    If (mDevice.sSerial.StartsWith("MorpheusHMD")) Then
                        sFullSerial = String.Format("hmd_{0}", "morpheus")
                        bIsMorpheusHmd = True
                    Else
                        sFullSerial = String.Format("hmd_{0}", mDevice.sSerial)
                    End If

                    sDeviceType = "Head-mounted Display"
                Case Else
                    Continue For
            End Select

            Dim mDeviceConfig = mServiceLog.FindConfigFromSerial(mDevice.sSerial)
            If (mDeviceConfig Is Nothing) Then
                Continue For
            End If

            Dim sSelectedColor As String = ""
            If (bIsMorpheusHmd) Then
                Dim sUsingCustomTracking As String = mDeviceConfig.GetValue(Of String)("", "use_custom_optical_tracking", "")
                If (String.IsNullOrEmpty(sUsingCustomTracking) OrElse sUsingCustomTracking.Trim.Length = 0) Then
                    Continue For
                End If

                If (sUsingCustomTracking <> "true") Then
                    sSelectedColor = "blue"
                Else
                    sSelectedColor = "custom9"
                End If
            Else
                sSelectedColor = mDeviceConfig.GetValue(Of String)("", "tracking_color", "")
                If (String.IsNullOrEmpty(sSelectedColor) OrElse sSelectedColor.Trim.Length = 0) Then
                    Continue For
                End If

                ' We dont care about custom
                If (sSelectedColor.StartsWith("custom")) Then
                    Continue For
                End If
            End If

            For Each mServiceDevice In mServiceDevicesLog.GetDevices()
                If (mServiceDevice.iType <> ClassLogManageServiceDevices.ENUM_DEVICE_TYPE.TRACKER) Then
                    Continue For
                End If

                Dim mTrackerConfig = mServiceLog.FindConfigFromSerial(mServiceDevice.sSerial)
                If (mTrackerConfig Is Nothing) Then
                    Continue For
                End If

                Dim sColorPath As String = String.Format("{0}\color_preset\{1}", sFullSerial, sSelectedColor)

                Dim mSettings As New Dictionary(Of String, Object)

                mSettings("device_id") = mDevice.iId
                mSettings("device_type") = mDevice.iType
                mSettings("tracker_id") = mServiceDevice.iId
                mSettings("color") = sSelectedColor

                Dim iHeuCenter = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "hue_center", "0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                Dim iHueRange = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "hue_range", "10"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                Dim iSaturationCenter = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "saturation_center", "255"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                Dim iSaturationRange = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "saturation_range", "32"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                Dim iValueCenter = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "value_center", "255"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                Dim iValueRange = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "value_range", "32"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

                Dim mColorConfig As New STRUC_DEVICE_COLOR_CALIBRATION(
                    mDevice.iId,
                    mDevice.iType,
                    mServiceDevice.iId,
                    sSelectedColor,
                    iHeuCenter, iHueRange,
                    iSaturationCenter, iSaturationRange,
                    iValueCenter, iValueRange)

                mColorSettings.Add(mColorConfig)
            Next
        Next

        Return mColorSettings.ToArray
    End Function
End Class
