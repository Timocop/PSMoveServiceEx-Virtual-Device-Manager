Imports System.Numerics
Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerRemoteDevices
    Implements ILogAction

    Public Shared ReadOnly LOG_ISSUE_REMOTE_DEVICE_ERROR As String = "Remote device encountered an error"
    Public Shared ReadOnly LOG_ISSUE_BAD_REMOTE_DEVICE_IDS As String = "Invalid remote device ids"
    Public Shared ReadOnly LOG_ISSUE_REMOTE_DEVICE_NO_DEVICE As String = "Remote devices does not point to an existing device"
    Public Shared ReadOnly LOG_ISSUE_NO_PSMOVE_EMULATION As String = "Virtual controller PlayStation Move emulation not enabled"
    Public Shared ReadOnly LOG_ISSUE_BAD_ORIENTATION_FILTER As String = "Controller orientation filter not set properly"

    Structure STRUC_DEVICE_ITEM
        Dim iId As Integer

        Dim sNickName As String
        Dim sTrackerName As String
        Dim bHasStatusError As Boolean
        Dim sHasStatusErrorMessage As String
        Dim iFpsPipeCounter As Integer
    End Structure

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
        If (g_mFormMain.g_mUCVirtualControllers Is Nothing OrElse g_mFormMain.g_mUCVirtualControllers.g_mUCRemoteDevices Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        ' Not thread-safe
        ClassUtils.SyncInvoke(Sub()
                                  Dim mRemoteDevices = g_mFormMain.g_mUCVirtualControllers.g_mUCRemoteDevices.GetRemoteDevices()
                                  For Each mItem In mRemoteDevices
                                      Dim mAng As Vector3 = ClassMathUtils.FromQ(mItem.g_mClassIO.m_Orientation)
                                      Dim mReset As Vector3 = ClassMathUtils.FromQ(mItem.g_mClassIO.m_ResetOrientation)

                                      sTrackersList.AppendFormat("[Controller_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                      sTrackersList.AppendFormat("ID={0}", mItem.g_mClassIO.m_Index).AppendLine()
                                      sTrackersList.AppendFormat("NickName={0}", mItem.m_Nickname).AppendLine()
                                      sTrackersList.AppendFormat("TrackerName={0}", mItem.m_TrackerName).AppendLine()
                                      sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                      sTrackersList.AppendFormat("HasStatusErrorMessage={0}", mItem.m_HasStatusErrorMessage.Value.Replace(vbNewLine, "").Replace(vbLf, "")).AppendLine()
                                      sTrackersList.AppendFormat("FpsPipeCounter={0}", mItem.g_mClassIO.m_FpsPipeCounter).AppendLine()
                                      sTrackersList.AppendFormat("Orientation={0}", String.Format("{0}, {1}, {2}",
                                                                                                                mAng.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                                                                mAng.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                                                                mAng.Z.ToString(Globalization.CultureInfo.InvariantCulture))).AppendLine()
                                      sTrackersList.AppendFormat("ResetOrientation={0}", String.Format("{0}, {1}, {2}",
                                                                                                                mReset.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                                                                mReset.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                                                                mReset.Z.ToString(Globalization.CultureInfo.InvariantCulture))).AppendLine()
                                      sTrackersList.AppendFormat("YawOrientationOffset={0}", mItem.g_mClassIO.m_YawOrientationOffset).AppendLine()

                                      sTrackersList.AppendLine()
                                  Next
                              End Sub)

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_REMOTE_DEVICES
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckHasError())
        mIssues.AddRange(CheckInvalidIds())
        mIssues.AddRange(CheckControllerExternalSource())
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
            LOG_ISSUE_REMOTE_DEVICE_ERROR,
            "Remote device with controller id {0} and name '{1}' encountered the following error: {2}",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        ' Check if IDs are -1 or invalid 
        For Each mDevice In GetDevices()
            If (mDevice.bHasStatusError) Then
                Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iId, mDevice.sTrackerName, mDevice.sHasStatusErrorMessage)
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
            LOG_ISSUE_BAD_REMOTE_DEVICE_IDS,
            "Some remote device controller ids are not set properly. Therefore those remote devices are disabled.",
            "Properly asign the remote device controller id to an existing PSMoveServiceEx device.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mBadIdTemplate As New STRUC_LOG_ISSUE(
           LOG_ISSUE_REMOTE_DEVICE_NO_DEVICE,
            "Remote device controller id {0} does not point to a existing PSMoveServiceEx device.",
            "Properly asign the remote device controller id to an existing PSMoveServiceEx device.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mServiceDevicesLog As New ClassLogManageServiceDevices(g_mFormMain, g_ClassLogContent)

        ' Check if IDs are -1 or invalid 
        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                mIssues.Add(New STRUC_LOG_ISSUE(mInvalidIdTemplate))
                Exit For
            End If
        Next

        ' Check if ID even point to existing devices
        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                Continue For
            End If

            Dim bExist As Boolean = False

            For Each mServiceDevice In mServiceDevicesLog.GetDevices()
                If (mServiceDevice.iType <> ClassLogManageServiceDevices.ENUM_DEVICE_TYPE.CONTROLLER) Then
                    Continue For
                End If

                If (mDevice.iId <> mServiceDevice.iId) Then
                    Continue For
                End If

                bExist = True
                Exit For
            Next

            If (Not bExist) Then
                Dim mIssue As New STRUC_LOG_ISSUE(mBadIdTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iId)
                mIssues.Add(mIssue)
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckControllerExternalSource() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mVirtualTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_NO_PSMOVE_EMULATION,
            "To orientate the controller id {0} using external sources such as remote devices, PlayStation Move emulation must be enabled. Otherwise, orientation data will not be transmitted via the protocol.",
            "Enable PlayStation Move emulation for this controller.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mFilterTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_ORIENTATION_FILTER,
            "To orientate the controller id {0} using external sources such as remote devices, filter 'OrientationExternal' must be used. Otherwise, orientation data will not be transmitted via the protocol.",
            "Switch to the 'OrientationExternal' orientation filter.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        Dim mServiceDevicesLog As New ClassLogManageServiceDevices(g_mFormMain, g_ClassLogContent)
        Dim mServiceLog As New ClassLogService(g_mFormMain, g_ClassLogContent)

        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                Continue For
            End If

            For Each mServiceDevice In mServiceDevicesLog.GetDevices()
                If (mServiceDevice.iType <> ClassLogManageServiceDevices.ENUM_DEVICE_TYPE.CONTROLLER) Then
                    Continue For
                End If

                If (mDevice.iId <> mServiceDevice.iId) Then
                    Continue For
                End If

                Dim mServiceConfig = mServiceLog.FindConfigFromSerial(mServiceDevice.sSerial)
                If (mServiceConfig Is Nothing) Then
                    Continue For
                End If

                If (mServiceDevice.sSerial.StartsWith("VirtualController")) Then
                    Dim sPSmoveEmulation As String = mServiceConfig.GetValue("", "psmove_emulation", "")
                    If (String.IsNullOrEmpty(sPSmoveEmulation)) Then
                        Continue For
                    End If

                    If (sPSmoveEmulation <> "true") Then
                        Dim mIssue As New STRUC_LOG_ISSUE(mVirtualTemplate)
                        mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iId)
                        mIssues.Add(mIssue)
                    End If
                Else
                    Dim sOrientationExternal As String = mServiceConfig.GetValue("OrientationFilter", "FilterType", "")
                    If (String.IsNullOrEmpty(sOrientationExternal)) Then
                        Continue For
                    End If

                    If (sOrientationExternal <> "OrientationExternal") Then
                        Dim mIssue As New STRUC_LOG_ISSUE(mFilterTemplate)
                        mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iId)
                        mIssues.Add(mIssue)
                    End If
                End If

                Exit For
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
        Dim mDevoceProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("[") AndAlso sLine.EndsWith("]"c)) Then
                Dim sDeviceKey As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewDevice As New STRUC_DEVICE_ITEM

                ' Required
                While True
                    If (mDevoceProp.ContainsKey("ID")) Then
                        mNewDevice.iId = CInt(mDevoceProp("ID"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("NickName")) Then
                        mNewDevice.sNickName = mDevoceProp("NickName")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("TrackerName")) Then
                        mNewDevice.sTrackerName = mDevoceProp("TrackerName")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("HasStatusError")) Then
                        mNewDevice.bHasStatusError = (mDevoceProp("HasStatusError").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("FpsPipeCounter")) Then
                        mNewDevice.iFpsPipeCounter = CInt(mDevoceProp("FpsPipeCounter"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("HasStatusErrorMessage")) Then
                        mNewDevice.sHasStatusErrorMessage = mDevoceProp("HasStatusErrorMessage")
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
