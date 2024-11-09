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
            "Some diagnostic details are unavailable due to missing log information.",
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
            "You are using only one tracker for optical tracking. Triangulation is not available for singular trackers and tracking quality is greatly redcued.",
            "Add more trackers, such as an additional PlayStation Eye, Webcam or PlayStation 4 Stereo Camera.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )
        Dim mNoTrackerTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_NO_OPTICAL_TRACKING,
            "You have no trackers and are unable to track your devices optically.",
            "Add trackers such as PlayStation Eyes, Webcams or PlayStation 4 Stereo Cameras to enable optical tracking.",
            ENUM_LOG_ISSUE_TYPE.ERROR
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
            "You are using virtual head-mounted displays. Those types of virtual devices are deprecated due to limited functionality and will only be used for backwards protocol compatibility.",
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
            "There are currently {0} head-mounted displays available but some applications may only support {1} at the time.",
            "Its recommended to reduce the number of head-mounted displays to {0}.",
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

            mNewIssue.sDescription = String.Format(mNewIssue.sDescription, iHmds, "1")
            mNewIssue.sSolution = String.Format(mNewIssue.sSolution, "1")

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
            "Properly calibrate color for this device.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mColorCollisionTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_COLOR_COLLISION,
            "{0} id {1} has possible color collisions with {2} id {3} on tracker id {4} which may cause tracking issues.",
            "Properly calibrate color for this device or enable 'Prevent color collisions' setting in color calibration before sampling colors.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mWrongColorTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_TRACKING_COLOR,
            "The tracking color of {0} id {1} on tracker id {2} not match the selected tracking color ({3}) that is set for this device.",
            "Properly calibrate color for this device.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mServiceDevicesLog As New ClassLogManageServiceDevices(g_mFormMain, g_ClassLogContent)
        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)

        Dim mColorSettings As New List(Of Dictionary(Of String, Object))

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

                mSettings("hue_center") = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "hue_center", "0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                mSettings("hue_range") = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "hue_range", "10"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                mSettings("saturation_center") = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "saturation_center", "255"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                mSettings("saturation_range") = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "saturation_range", "32"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                mSettings("value_center") = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "value_center", "255"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                mSettings("value_range") = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "value_range", "32"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

                mColorSettings.Add(mSettings)

            Next
        Next

        For Each mSettings As Dictionary(Of String, Object) In mColorSettings
            If (mSettings Is Nothing) Then
                Continue For
            End If

            Dim iDeviceId As Integer = CInt(mSettings("device_id"))
            Dim iDeviceType As ENUM_DEVICE_TYPE = CType(mSettings("device_type"), ENUM_DEVICE_TYPE)
            Dim iTrackerId As Integer = CInt(mSettings("tracker_id"))
            Dim sSelectedColor As String = CStr(mSettings("color"))

            Dim iHueCenter As Single = CSng(mSettings("hue_center"))
            Dim iHueRange As Single = CSng(mSettings("hue_range"))
            Dim iSaturationCenter As Single = CSng(mSettings("saturation_center"))
            Dim iSaturationRange As Single = CSng(mSettings("saturation_range"))
            Dim iValueCenter As Single = CSng(mSettings("value_center"))
            Dim iValueRange As Single = CSng(mSettings("value_range"))

            Dim sDeviceType As String = "<Unknown>"

            Select Case (iDeviceType)
                Case ENUM_DEVICE_TYPE.CONTROLLER
                    sDeviceType = "Controller"

                Case ENUM_DEVICE_TYPE.HMD
                    sDeviceType = "Head-mounted Display"
            End Select

            ' Assuming the user didnt even change anything
            If (CInt(iHueRange) = 10 AndAlso
                    CInt(iSaturationCenter) = 255 AndAlso CInt(iValueCenter) = 255 AndAlso
                    CInt(iSaturationRange) = 32 AndAlso CInt(iValueRange) = 32) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mBadTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, iDeviceId, iTrackerId)
                mIssues.Add(mNewIssue)

            ElseIf (iSaturationCenter < 80.0F OrElse iValueCenter < 80.0F) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mBadTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, iDeviceId, iTrackerId)
                mIssues.Add(mNewIssue)
            End If

            Dim bGoodColor As Boolean = True
            Dim iGoodColorRange As Integer = 25
            Select Case (sSelectedColor.ToLower)
                Case "magenta"
                    bGoodColor = IsHueInRange(CInt(iHueCenter), 150, iGoodColorRange)
                Case "cyan"
                    bGoodColor = IsHueInRange(CInt(iHueCenter), 90, iGoodColorRange)
                Case "yellow"
                    bGoodColor = IsHueInRange(CInt(iHueCenter), 30, iGoodColorRange)
                Case "red"
                    bGoodColor = IsHueInRange(CInt(iHueCenter), 0, iGoodColorRange)
                Case "green"
                    bGoodColor = IsHueInRange(CInt(iHueCenter), 60, iGoodColorRange)
                Case "blue"
                    bGoodColor = IsHueInRange(CInt(iHueCenter), 120, iGoodColorRange)
                Case Else
                    ' Ignore custom colors
            End Select

            If (Not bGoodColor) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mWrongColorTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, iDeviceId, iTrackerId, sSelectedColor)
                mIssues.Add(mNewIssue)
            End If

            ' Check for color collisions between devices
            For Each mOtherSettings As Dictionary(Of String, Object) In mColorSettings
                Dim iOtherDeviceId As Integer = CInt(mOtherSettings("device_id"))
                Dim iOtherDeviceType As ENUM_DEVICE_TYPE = CType(mOtherSettings("device_type"), ENUM_DEVICE_TYPE)
                Dim iOtherTrackerId As Integer = CInt(mOtherSettings("tracker_id"))

                Dim iOtherHueCenter As Single = CSng(mOtherSettings("hue_center"))
                Dim iOtherHueRange As Single = CSng(mOtherSettings("hue_range"))
                Dim iOtherSaturationCenter As Single = CSng(mOtherSettings("saturation_center"))
                Dim iOtherSaturationRange As Single = CSng(mOtherSettings("saturation_range"))
                Dim iOtherValueCenter As Single = CSng(mOtherSettings("value_center"))
                Dim iOtherValueRange As Single = CSng(mOtherSettings("value_range"))

                Dim sOtherDeviceType As String = "<Unknown>"

                Select Case (iOtherDeviceType)
                    Case ENUM_DEVICE_TYPE.CONTROLLER
                        sOtherDeviceType = "Controller"

                    Case ENUM_DEVICE_TYPE.HMD
                        sOtherDeviceType = "Head-mounted Display"
                End Select

                ' Must be the same tracker
                If (iTrackerId <> iOtherTrackerId) Then
                    Continue For
                End If

                ' Dont check same device
                If (iDeviceId = iOtherDeviceId AndAlso iDeviceType = iOtherDeviceType) Then
                    Continue For
                End If

                If (Not IsHueInRange(CInt(iHueCenter), CInt(iOtherHueCenter), CInt(iOtherHueRange))) Then
                    Continue For
                End If

                Dim mNewIssue As New STRUC_LOG_ISSUE(mColorCollisionTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, iDeviceId, sOtherDeviceType, iOtherDeviceId, iTrackerId)
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
            "Calibrate the controllers magnetometer if available. Ignore this warning if the magnetometer is not available.",
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
            "Calibrate the {0} gyroscope.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mBadAccelTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_ACCELEROMETER,
            "The {0} id {1} accelerometer has not been calibrated yet, which may cause orientation drift.",
            "Calibrate the {0} accelerometer.",
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
End Class
