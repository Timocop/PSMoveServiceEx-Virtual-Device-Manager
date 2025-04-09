Imports PSMSVirtualDeviceManager.ClassLogDiagnostics
Imports PSMSVirtualDeviceManager.UCVirtualTrackerItem.ClassCaptureLogic

Public Class ClassLogManagerVirtualTrackers
    Implements ILogAction

    Public Shared ReadOnly SECTION_VDM_VIRTUAL_TRACKERS As String = "VDM Virtual Trackers"

    Public Shared ReadOnly LOG_ISSUE_VIDEO_INPUT_DEVICE_ERROR As String = "Video input device encountered an error"
    Public Shared ReadOnly LOG_ISSUE_BAD_TRACKER_IDS As String = "Invalid virtual tracker ids"
    Public Shared ReadOnly LOG_ISSUE_VIRTUAL_TRACKER_NO_DEVICE As String = "Virtual tracker does not point to a existing device"
    Public Shared ReadOnly LOG_ISSUE_VIRTUAL_TRACKER_COUNT_LOW As String = "Virtual tracker slot count too low"
    Public Shared ReadOnly LOG_ISSUE_BAD_VIRTUAL_TRACKER_COUNT As String = "More virtual tracker slots than video input devices"
    Public Shared ReadOnly LOG_ISSUE_VIRTUAL_TRACKER_RESSOURCE_HEAVY As String = "Virtual tracker too ressource intensive"
    Public Shared ReadOnly LOG_ISSUE_VIRTUAL_TRACKER_RESOLUTION_MISMATCH As String = "Virtual tracker and video input device resolution mismatch"
    Public Shared ReadOnly LOG_ISSUE_VIRTUAL_TRACKER_BAD_CODEC As String = "Virtual tracker bad codec set"
    Public Shared ReadOnly LOG_ISSUE_VIRTUAL_TRACKER_OPTIMAL_CODEC As String = "Virtual tracker set optimal codec"
    Public Shared ReadOnly LOG_ISSUE_VIRTUAL_TRACKER_BAD_FPS As String = "Virtual tracker bad framrate"
    Public Shared ReadOnly LOG_ISSUE_VIRTUAL_TRACKER_AUTO_SETTINGS As String = "Video input device properties set manually"

    Structure STRUC_DEVICE_ITEM
        Dim sPath As String
        Dim iDeviceIndex As Integer
        Dim iPipePrimaryIndex As Integer
        Dim iPipeSecondaryIndex As Integer

        Dim iCameraFramerate As Integer
        Dim iCameraResolution As ENUM_RESOLUTION
        Dim bFlipImage As Boolean
        Dim iImageInterpolation As ENUM_INTERPOLATION
        Dim bInitialized As Boolean
        Dim bIsPlayStationCamera As Boolean
        Dim bPipeConnected As Boolean
        Dim bSupersampling As Boolean
        Dim bUseMJPG As Boolean
        Dim bHasStatusError As Boolean
        Dim sHasStatusErrorMessage As String
        Dim iFpsCaptureCounter As Integer
        Dim iFpsPipeCounter As Integer
        Dim bAutoDetectSettings As Boolean
    End Structure

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
        If (g_mFormMain.g_mUCVirtualTrackers Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        ' Not thread-safe
        ClassUtils.SyncInvoke(Sub()
                                  Dim mTrackers = g_mFormMain.g_mUCVirtualTrackers.GetAllDevices()
                                  For Each mItem In mTrackers
                                      sTrackersList.AppendFormat("[{0}]", mItem.m_DevicePath).AppendLine()
                                      sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                      sTrackersList.AppendFormat("HasStatusErrorMessage={0}", mItem.m_HasStatusErrorMessage.Value.Replace(vbNewLine, "").Replace(vbLf, "")).AppendLine()
                                      sTrackersList.AppendFormat("CameraFramerate={0}", mItem.g_mClassCaptureLogic.m_CameraFramerate).AppendLine()
                                      sTrackersList.AppendFormat("CameraResolution={0}", CInt(mItem.g_mClassCaptureLogic.m_CameraResolution)).AppendLine()
                                      sTrackersList.AppendFormat("CameraResolutionName={0}", mItem.g_mClassCaptureLogic.m_CameraResolution.ToString).AppendLine()
                                      sTrackersList.AppendFormat("DeviceIndex={0}", mItem.g_mClassCaptureLogic.m_DeviceIndex).AppendLine()
                                      sTrackersList.AppendFormat("FlipImage={0}", mItem.g_mClassCaptureLogic.m_FlipImage).AppendLine()
                                      sTrackersList.AppendFormat("ImageInterpolation={0}", CInt(mItem.g_mClassCaptureLogic.m_ImageInterpolation)).AppendLine()
                                      sTrackersList.AppendFormat("ImageInterpolationName={0}", mItem.g_mClassCaptureLogic.m_ImageInterpolation.ToString).AppendLine()
                                      sTrackersList.AppendFormat("Initialized={0}", mItem.g_mClassCaptureLogic.m_Initialized).AppendLine()
                                      sTrackersList.AppendFormat("IsPlayStationCamera={0}", mItem.g_mClassCaptureLogic.m_IsPlayStationCamera).AppendLine()
                                      sTrackersList.AppendFormat("PipeConnected={0}", mItem.g_mClassCaptureLogic.m_PipeConnected).AppendLine()
                                      sTrackersList.AppendFormat("PipePrimaryIndex={0}", mItem.g_mClassCaptureLogic.m_PipePrimaryIndex).AppendLine()
                                      sTrackersList.AppendFormat("PipeSecondaryIndex={0}", mItem.g_mClassCaptureLogic.m_PipeSecondaryIndex).AppendLine()
                                      sTrackersList.AppendFormat("ShowCaptureImage={0}", mItem.g_mClassCaptureLogic.m_ShowCaptureImage).AppendLine()
                                      sTrackersList.AppendFormat("Supersampling={0}", mItem.g_mClassCaptureLogic.m_Supersampling).AppendLine()
                                      sTrackersList.AppendFormat("UseMJPG={0}", mItem.g_mClassCaptureLogic.m_UseMJPG).AppendLine()
                                      sTrackersList.AppendFormat("FpsCaptureCounter={0}", mItem.g_mClassCaptureLogic.m_FpsCaptureCounter).AppendLine()
                                      sTrackersList.AppendFormat("FpsPipeCounter={0}", mItem.g_mClassCaptureLogic.m_FpsPipeCounter).AppendLine()
                                      sTrackersList.AppendFormat("AutoDetectSettings={0}", mItem.g_mClassCaptureLogic.m_AutoDetectSettings).AppendLine()

                                      sTrackersList.AppendLine()
                                  Next
                              End Sub)

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_VIRTUAL_TRACKERS
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckHasError())
        mIssues.AddRange(CheckInvalidIds())
        mIssues.AddRange(CheckBadTrackerCount())
        mIssues.AddRange(CheckTooDemanding())
        mIssues.AddRange(CheckIncompatibleResolution())
        mIssues.AddRange(CheckCodec())
        mIssues.AddRange(CheckFps())
        mIssues.AddRange(CheckAutoDetectSettings())
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
    End Function

    Public Function CheckHasError() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_VIDEO_INPUT_DEVICE_ERROR,
            "Video input device id {0} ({1}) encountered the following error: {2}",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        ' Check if IDs are -1 or invalid 
        For Each mDevice In GetDevices()
            If (mDevice.bHasStatusError) Then
                Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iDeviceIndex, mDevice.sPath, mDevice.sHasStatusErrorMessage)
                mIssues.Add(mIssue)
                Exit For
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckInvalidIds() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mInvalidIdTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_TRACKER_IDS,
            "Some virtual tracker ids have not set properly. Therefore those trackers are disabled.",
            "Properly asign the tracker id to an existing PSMoveServiceEx device.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mBadIdTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_VIRTUAL_TRACKER_NO_DEVICE,
            "The virtual tracker id {0} for video input device id {1} ({2}) does not point to a existing PSMoveServiceEx device.",
            "Properly asign the tracker id to an existing PSMoveServiceEx device.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        ' Check if IDs are -1 or invalid 
        For Each mDevice In GetDevices()
            If (mDevice.iPipePrimaryIndex < 0) Then
                mIssues.Add(New STRUC_LOG_ISSUE(mInvalidIdTemplate))
                Exit For
            End If
        Next

        ' Check if ID even point to existing devices
        For Each mDevice In GetDevices()
            If (mDevice.iPipePrimaryIndex < 0) Then
                Continue For
            End If

            Dim bExist As Boolean = False
            Dim mServiceLog As New ClassLogManageServiceDevices(g_mFormMain, g_ClassLogContent)

            For Each mServiceDevice In mServiceLog.GetDevices()
                If (mServiceDevice.iType <> ClassLogManageServiceDevices.ENUM_DEVICE_TYPE.TRACKER) Then
                    Continue For
                End If

                If (Not mServiceDevice.sSerial.StartsWith("VirtualTracker")) Then
                    Continue For
                End If

                If (mDevice.iPipePrimaryIndex <> mServiceDevice.iId) Then
                    Continue For
                End If

                bExist = True
                Exit For
            Next

            If (Not bExist) Then
                Dim mIssue As New STRUC_LOG_ISSUE(mBadIdTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iPipePrimaryIndex, mDevice.iDeviceIndex, mDevice.sPath)
                mIssues.Add(mIssue)
            End If

        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckBadTrackerCount() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_VIRTUAL_TRACKER_COUNT_LOW,
            "Virtual tracker slot count for PSMoveServiceEx is {0} but there are currently {1} available video input devices. Some video input devices will be unavailable.",
            "Set the virtual tracker count to the available video input device count.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mNoMatchTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_VIRTUAL_TRACKER_COUNT,
            "Virtual tracker slot count for PSMoveServiceEx is {0} but there are currently {1} available video input devices. " &
                "You currently dont have enough video input devices to fill all those available slots. Having unused virtual trackers will reduce performance.",
            "Set the virtual tracker count to the available video input device count.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)
        Dim mServiceConfig = mServiceLog.FindConfigFromSerial("TrackerManagerConfig")

        If (mServiceConfig IsNot Nothing) Then
            Dim sServiceCount As String = mServiceConfig.GetValue("", "virtual_tracker_count", "")

            If (Not String.IsNullOrEmpty(sServiceCount)) Then
                Dim iServiceCount As Integer = CInt(sServiceCount)
                Dim iCount As Integer = 0

                For Each mDevice In GetDevices()
                    If (mDevice.bIsPlayStationCamera) Then
                        iCount += 2
                    Else
                        iCount += 1
                    End If
                Next

                If (iServiceCount < iCount) Then
                    Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                    mIssue.sDescription = String.Format(mIssue.sDescription, iServiceCount, iCount)
                    mIssues.Add(mIssue)
                End If

                If (iServiceCount > iCount) Then
                    Dim mIssue As New STRUC_LOG_ISSUE(mNoMatchTemplate)
                    mIssue.sDescription = String.Format(mIssue.sDescription, iServiceCount, iCount)
                    mIssues.Add(mIssue)
                End If
            End If
        End If

        Return mIssues.ToArray
    End Function

    Public Function CheckTooDemanding() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_VIRTUAL_TRACKER_RESSOURCE_HEAVY,
            "Virtual tracker resolution and framerate is too high for virtual input device id {0} ({1}). Using too high settings demands more system resosurces.",
            "Its recommended to use either high resolution and lower framrate or lower resolution and high framerate. Not both resolution and framerate on high.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        For Each mDevice In GetDevices()
            If (mDevice.iCameraFramerate < 60) Then
                Continue For
            End If

            If (mDevice.iCameraResolution = ENUM_RESOLUTION.SD AndAlso Not mDevice.bSupersampling) Then
                Continue For
            End If

            Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
            mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iDeviceIndex, mDevice.sPath)
            mIssues.Add(mIssue)
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckIncompatibleResolution() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_VIRTUAL_TRACKER_RESOLUTION_MISMATCH,
            "The video input device {0} ({1}) resolution and PSMoveServiceEx resolution for virtual tracker id {2} do not match. This will result in a 'Tracker timed out.' error.",
            "Match the resolution of video input device id {0} ({1}) with the PSMoveServiceEx virtual tracker {2} in color calibration inside the config tool.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)
        Dim mServiceDevicesLog As New ClassLogManageServiceDevices(g_mFormMain, g_ClassLogContent)

        Dim mServiceDevices = mServiceDevicesLog.GetDevices()

        For Each mDevice In GetDevices()
            Dim mIndexes As Integer()

            If (mDevice.bIsPlayStationCamera) Then
                mIndexes = New Integer() {mDevice.iPipePrimaryIndex, mDevice.iPipeSecondaryIndex}
            Else
                mIndexes = New Integer() {mDevice.iPipePrimaryIndex}
            End If

            For Each iIndex In mIndexes
                If (iIndex < 0) Then
                    Continue For
                End If

                For Each mServiceDevice In mServiceDevices
                    If (mServiceDevice.iId <> iIndex) Then
                        Continue For
                    End If

                    Dim mServiceConfig = mServiceLog.FindConfigFromSerial(mServiceDevice.sSerial)
                    If (mServiceConfig Is Nothing) Then
                        Exit For
                    End If

                    Dim sResolutionWidth As String = mServiceConfig.GetValue("", "frame_width", "")
                    If (String.IsNullOrEmpty(sResolutionWidth)) Then
                        Exit For
                    End If

                    Dim iResolutionWidth As Integer = CInt(sResolutionWidth)
                    If (mDevice.iCameraResolution = ENUM_RESOLUTION.SD AndAlso iResolutionWidth = 640) Then
                        Exit For
                    End If
                    If (mDevice.iCameraResolution = ENUM_RESOLUTION.HD AndAlso iResolutionWidth = 1920) Then
                        Exit For
                    End If

                    Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                    mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iDeviceIndex, mDevice.sPath, mDevice.iPipePrimaryIndex)
                    mIssue.sSolution = String.Format(mIssue.sSolution, mDevice.iDeviceIndex, mDevice.sPath, mDevice.iPipePrimaryIndex)
                    mIssues.Add(mIssue)
                    Exit For
                Next
            Next
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckCodec() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_VIRTUAL_TRACKER_BAD_CODEC,
            "The video input device {0} ({1}) resolution is too high for the currently set YUY2 codec and may cause stuttering and freezing image stream.",
            "Enable the MJPG codec for video input device id {0} ({1}).",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mOptimizedTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_VIRTUAL_TRACKER_OPTIMAL_CODEC,
            "The video input device {0} ({1}) is currently using codec MJPG. This codec uses compression and therefore adds latency and requires a bit more processing power.",
            "Disable the MJPG codec for video input device id {0} ({1}). Ignore this if YUY2 is not working.",
            ENUM_LOG_ISSUE_TYPE.INFO
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        For Each mDevice In GetDevices()
            If (mDevice.bIsPlayStationCamera) Then
                Continue For
            End If

            ' YUY2 does not like high resolutions
            If (mDevice.iCameraResolution = ENUM_RESOLUTION.HD OrElse mDevice.bSupersampling) Then
                If (Not mDevice.bUseMJPG) Then
                    Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                    mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iDeviceIndex, mDevice.sPath)
                    mIssue.sSolution = String.Format(mIssue.sSolution, mDevice.iDeviceIndex, mDevice.sPath)
                    mIssues.Add(mIssue)
                End If
            Else
                If (mDevice.bUseMJPG) Then
                    Dim mIssue As New STRUC_LOG_ISSUE(mOptimizedTemplate)
                    mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iDeviceIndex, mDevice.sPath)
                    mIssue.sSolution = String.Format(mIssue.sSolution, mDevice.iDeviceIndex, mDevice.sPath)
                    mIssues.Add(mIssue)
                End If
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckFps() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_VIRTUAL_TRACKER_BAD_FPS,
            "The video input device {0} ({1}) framerate ({2} fps) is lower than the configured framerate ({3} fps). An unstable framerate may cause poor tracking performance.",
            "Check for connection issues or lower your settings for video input device id {0} ({1}) to reach the target framerate.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        For Each mDevice In GetDevices()
            If (mDevice.iFpsCaptureCounter < (mDevice.iCameraFramerate - 3)) Then
                Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iDeviceIndex, mDevice.sPath, mDevice.iFpsCaptureCounter, mDevice.iCameraFramerate)
                mIssue.sSolution = String.Format(mIssue.sSolution, mDevice.iDeviceIndex, mDevice.sPath)
                mIssues.Add(mIssue)
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckAutoDetectSettings() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_VIRTUAL_TRACKER_AUTO_SETTINGS,
            "The video input device {0} ({1}) properties are set manually and do not automatically synchronize with PSMoveServiceEx virtual tracker settings. " &
                "This may cause problems when using color calibration in PSMoveServiceEx Config Tool.",
            "Enable 'Automatically detect settings' for video input device id {0} ({1}).",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        For Each mDevice In GetDevices()
            If (Not mDevice.bAutoDetectSettings) Then
                Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iDeviceIndex, mDevice.sPath)
                mIssue.sSolution = String.Format(mIssue.sSolution, mDevice.iDeviceIndex, mDevice.sPath)
                mIssues.Add(mIssue)
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function GetDevices() As STRUC_DEVICE_ITEM()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mDeviceList As New List(Of STRUC_DEVICE_ITEM)
        Dim mDevoceProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("[") AndAlso sLine.EndsWith("]"c)) Then
                Dim sDevicePath As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewDevice As New STRUC_DEVICE_ITEM

                mNewDevice.sPath = sDevicePath

                ' Required
                While True
                    If (mDevoceProp.ContainsKey("DeviceIndex")) Then
                        mNewDevice.iDeviceIndex = CInt(mDevoceProp("DeviceIndex"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("PipePrimaryIndex")) Then
                        mNewDevice.iPipePrimaryIndex = CInt(mDevoceProp("PipePrimaryIndex"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("PipeSecondaryIndex")) Then
                        mNewDevice.iPipeSecondaryIndex = CInt(mDevoceProp("PipeSecondaryIndex"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("CameraFramerate")) Then
                        mNewDevice.iCameraFramerate = CInt(mDevoceProp("CameraFramerate"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("CameraResolution")) Then
                        mNewDevice.iCameraResolution = CType(CInt(mDevoceProp("CameraResolution")), ENUM_RESOLUTION)
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("FlipImage")) Then
                        mNewDevice.bFlipImage = (mDevoceProp("FlipImage").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("ImageInterpolation")) Then
                        mNewDevice.iImageInterpolation = CType(CInt(mDevoceProp("ImageInterpolation")), ENUM_INTERPOLATION)
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("Initialized")) Then
                        mNewDevice.bInitialized = (mDevoceProp("Initialized").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("IsPlayStationCamera")) Then
                        mNewDevice.bIsPlayStationCamera = (mDevoceProp("IsPlayStationCamera").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("PipeConnected")) Then
                        mNewDevice.bPipeConnected = (mDevoceProp("PipeConnected").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("Supersampling")) Then
                        mNewDevice.bSupersampling = (mDevoceProp("Supersampling").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("UseMJPG")) Then
                        mNewDevice.bUseMJPG = (mDevoceProp("UseMJPG").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("HasStatusError")) Then
                        mNewDevice.bHasStatusError = (mDevoceProp("HasStatusError").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("HasStatusErrorMessage")) Then
                        mNewDevice.sHasStatusErrorMessage = mDevoceProp("HasStatusErrorMessage")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("FpsCaptureCounter")) Then
                        mNewDevice.iFpsCaptureCounter = CInt(mDevoceProp("FpsCaptureCounter"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("FpsPipeCounter")) Then
                        mNewDevice.iFpsPipeCounter = CInt(mDevoceProp("FpsPipeCounter"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("AutoDetectSettings")) Then
                        mNewDevice.bAutoDetectSettings = (mDevoceProp("AutoDetectSettings").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    mDeviceList.Add(mNewDevice)
                    Exit While
                End While

                mDevoceProp.Clear()
            End If

            If (sLine.Contains("="c)) Then
                Dim sKey As String = sLine.Substring(0, sLine.IndexOf("="c))
                Dim sValue As String = sLine.Remove(0, sLine.IndexOf("="c) + 1)

                mDevoceProp(sKey) = sValue
            End If
        Next

        Return mDeviceList.ToArray
    End Function
End Class
