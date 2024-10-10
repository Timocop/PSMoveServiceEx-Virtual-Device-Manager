Imports System.Numerics
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManageServiceDevices
    Implements ILogAction

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

    Public Sub Generate() Implements ILogAction.Generate
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
            String.Format("{0} log unavailable", GetActionTitle()),
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
            "Very limited tracking quality",
            "You are using only one tracker for optical tracking. Triangulation is not available for singular trackers and tracking quality is greatly redcued.",
            "Add more trackers, such as an additional PlayStation Eye, Webcam or PlayStation 4 Stereo Camera.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )
        Dim mNoTrackerTemplate As New STRUC_LOG_ISSUE(
            "No optical tracking",
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
            "Virtual Head-mounted Displays deprecated",
            "You are using virtual head-mounted displays. Those types of virtual devices are deprecated due to limited functionality and will only be used for backwards protocol compatibility.",
            "Do not use virtual head-mounted displays and use virtual controllers instead to track your head-mounted display. Unless the third-party application does not support controllers for Head-mounted Display tracking.",
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
        Dim mTemplate As New STRUC_LOG_ISSUE(
            "No {0} found",
            "",
            "",
            ENUM_LOG_ISSUE_TYPE.INFO
        )
        Dim mManyTemplate As New STRUC_LOG_ISSUE(
            "Multiple {0} found",
            "There are currently {0} {1} available but some applications may only support {2} at the time.",
            "Its recommended to reduce the number of {0} to {1}.",
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
            Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)

            mNewIssue.sMessage = String.Format(mNewIssue.sMessage, "Controllers")

            mIssues.Add(mNewIssue)
        End If

        If (iHmds = 0) Then
            Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)

            mNewIssue.sMessage = String.Format(mNewIssue.sMessage, "Head-mounted Displays")

            mIssues.Add(mNewIssue)
        ElseIf (iHmds > 1) Then
            Dim mNewIssue As New STRUC_LOG_ISSUE(mManyTemplate)

            mNewIssue.sMessage = String.Format(mNewIssue.sMessage, "Head-mounted Displays")
            mNewIssue.sDescription = String.Format(mNewIssue.sDescription, iHmds, "Head-mounted Displays", "1")
            mNewIssue.sSolution = String.Format(mNewIssue.sSolution, "Head-mounted Displays", "1")

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
            "Bad color calibration for device",
            "Color calibration for {0} id {1} is not properly set on trakcer id {2} which may cause tracking issues.",
            "Properly calibrate color for this device.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mServiceDevicesLog As New ClassLogManageServiceDevices(g_mFormMain, g_ClassLogContent)
        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)

        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                Continue For
            End If

            Dim sFullSerial As String = ""
            Dim sDeviceType As String = ""

            Select Case (mDevice.iType)
                Case ENUM_DEVICE_TYPE.CONTROLLER
                    sFullSerial = String.Format("controller_{0}", mDevice.sSerial)
                    sDeviceType = "Controller"
                Case ENUM_DEVICE_TYPE.HMD
                    sFullSerial = String.Format("hmd_{0}", mDevice.sSerial)
                    sDeviceType = "Head-mounted Display"
                Case Else
                    Continue For
            End Select

            Dim mDeviceConfig = mServiceLog.FindConfigFromSerial(mDevice.sSerial)
            If (mDeviceConfig Is Nothing) Then
                Continue For
            End If

            Dim sSelectedColor As String = mDeviceConfig.GetValue(Of String)("", "tracking_color", "")
            If (String.IsNullOrEmpty(sSelectedColor) OrElse sSelectedColor.Trim.Length = 0) Then
                Continue For
            End If

            ' We dont care about custom
            If (sSelectedColor.StartsWith("custom")) Then
                Continue For
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

                Dim iHueCenter = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "hue_center", "0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                Dim iHueRange = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "hue_range", "10"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                Dim iSaturationCenter = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "saturation_center", "255"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                Dim iSaturationRange = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "saturation_range", "32"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                Dim iValueCenter = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "value_center", "255"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                Dim iValueRange = Single.Parse(mTrackerConfig.GetValue(Of String)(sColorPath, "value_range", "32"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

                ' Assuming the user didnt even change anything
                If (iHueRange = 10 AndAlso
                        iSaturationCenter = 255 AndAlso iValueCenter = 255 AndAlso
                        iSaturationRange = 32 AndAlso iValueRange = 32) Then
                    Dim mNewIssue As New STRUC_LOG_ISSUE(mBadTemplate)
                    mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, mDevice.iId, mServiceDevice.iId)
                    mIssues.Add(mNewIssue)

                ElseIf (iSaturationCenter < 80.0F OrElse iValueCenter < 80.0F) Then
                    Dim mNewIssue As New STRUC_LOG_ISSUE(mBadTemplate)
                    mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, mDevice.iId, mServiceDevice.iId)
                    mIssues.Add(mNewIssue)
                End If
            Next
        Next

        Return mIssues.ToArray
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
                    Dim iPos(3) As Single

                    If (Single.TryParse(mPos(0), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iPos(0)) AndAlso
                        Single.TryParse(mPos(1), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iPos(1)) AndAlso
                        Single.TryParse(mPos(2), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iPos(2))) Then

                        mNewDevice.mPosition = New Vector3(iPos(0), iPos(1), iPos(2))
                        mNewDevice.bPositionValid = True
                    End If
                End If

                If (mDeviceProp.ContainsKey("Orientation") AndAlso mDeviceProp("Orientation").Split(","c).Count = 3) Then
                    Dim mAng = mDeviceProp("Orientation").Split(","c)
                    Dim iAng(3) As Single

                    If (Single.TryParse(mAng(0), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iAng(0)) AndAlso
                        Single.TryParse(mAng(1), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iAng(1)) AndAlso
                        Single.TryParse(mAng(2), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iAng(2))) Then

                        mNewDevice.mOrientation = New Vector3(iAng(0), iAng(1), iAng(2))
                        mNewDevice.bOrientationValid = True
                    End If
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
