Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs
Imports PSMSVirtualDeviceManager.UCVirtualTrackerItem.ClassCaptureLogic

Public Class ClassLogManagerVirtualTrackers
    Implements ILogAction

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
    End Structure

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate() Implements ILogAction.Generate
        If (g_mFormMain.g_mUCVirtualTrackers Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        ' Not thread-safe
        ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                               Dim mTrackers = g_mFormMain.g_mUCVirtualTrackers.GetAllDevices()
                                               For Each mItem In mTrackers
                                                   sTrackersList.AppendFormat("[{0}]", mItem.m_DevicePath).AppendLine()
                                                   sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
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
        mIssues.AddRange(CheckInvalidIds())
        mIssues.AddRange(CheckBadTrackerCount())
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
    End Function

    Public Function CheckInvalidIds() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mInvalidIdTemplate As New STRUC_LOG_ISSUE(
            "Invalid virtual tracker ids",
            "Some virtual tracker ids have not set properly. Therefore those trackers are disabled.",
            "Properly asign the tracker id to an existing PSMoveServiceEx device.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mBadIdTemplate As New STRUC_LOG_ISSUE(
            "Virtual tracker does not point to a existing device",
            "The virtual tracker id {0} ({1}) does not point to a existing PSMoveServiceEx device.",
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

                If (Not mServiceDevice.sSerial.StartsWith("VirtualTracker_")) Then
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
                mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iPipePrimaryIndex, mDevice.sPath)
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
            "Virtual tracker slot count too low",
            "Virtual tracker slot count for PSMoveServiceEx is {0} but there are currently {1} available video input devices. Some virtual input devices will be unavailable.",
            "Increase the virtual tracker count.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)
        Dim mServiceConfig = mServiceLog.FindConfigFromSerial("TrackerManagerConfig")
        If (mServiceConfig IsNot Nothing) Then
            Dim sServiceCount As String = mServiceConfig.GetValue(Of String)("", "virtual_tracker_count", Nothing)
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
            End If
        End If

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
