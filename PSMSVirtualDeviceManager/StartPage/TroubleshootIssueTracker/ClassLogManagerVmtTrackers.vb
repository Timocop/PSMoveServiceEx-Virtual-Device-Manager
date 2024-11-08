Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs
Imports PSMSVirtualDeviceManager.UCVirtualMotionTrackerItem.ClassIO

Public Class ClassLogManagerVmtTrackers
    Implements ILogAction

    Enum ENUM_DEVICE_TYPE
        INVALID = 0
        CONTROLLER
        HMD
    End Enum

    Structure STRUC_DEVICE_ITEM
        Dim iId As Integer
        Dim iVmtId As Integer
        Dim iType As ENUM_DEVICE_TYPE

        Dim iVmtTrackerRole As ENUM_TRACKER_ROLE
        Dim bHasStatusError As Boolean
        Dim iFpsOscCounter As Integer
    End Structure

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
        If (g_mFormMain.g_mUCVirtualMotionTracker Is Nothing OrElse g_mFormMain.g_mUCVirtualMotionTracker.g_UCVmtTrackers Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        ' Not thread-safe
        ClassUtils.SyncInvoke(Sub()
                                  Dim mVmtTrackers = g_mFormMain.g_mUCVirtualMotionTracker.g_UCVmtTrackers.GetVmtTrackers()
                                  For Each mItem In mVmtTrackers
                                      If (mItem.g_mClassIO.m_IsHMD) Then
                                          sTrackersList.AppendFormat("[Hmd_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                      Else
                                          sTrackersList.AppendFormat("[Controller_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                      End If
                                      sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                      sTrackersList.AppendFormat("ID={0}", mItem.g_mClassIO.m_Index).AppendLine()
                                      sTrackersList.AppendFormat("VmtID={0}", mItem.g_mClassIO.m_VmtTracker).AppendLine()
                                      sTrackersList.AppendFormat("VmtTrackerRole={0}", CInt(mItem.g_mClassIO.m_VmtTrackerRole)).AppendLine()
                                      sTrackersList.AppendFormat("VmtTrackerRoleName={0}", mItem.g_mClassIO.m_VmtTrackerRole.ToString).AppendLine()
                                      sTrackersList.AppendFormat("FpsOscCounter={0}", mItem.g_mClassIO.m_FpsOscCounter).AppendLine()

                                      sTrackersList.AppendLine()
                                  Next
                              End Sub)

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_VMT_TRACKERS
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckHasError())
        mIssues.AddRange(CheckInvalidIds())
        mIssues.AddRange(CheckCheckGenericRoles())
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
            "Virtual motion tracker encountered an error",
            "Virtual motion tracker id {0} with controller id {1} is encountering an error.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        ' Check if IDs are -1 or invalid 
        For Each mDevice In GetDevices()
            If (mDevice.bHasStatusError) Then
                Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iVmtId, mDevice.iId)
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
            "Invalid virtual motion tracker ids",
            "Some virtual motion tracker controller, head-mounted display or VMT ids have not set properly. Therefore those trackers are disabled.",
            "Properly asign the controller or head-mounted display id to an existing PSMoveServiceEx device and the VMT id to free slot that is not in use.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mBadIdTemplate As New STRUC_LOG_ISSUE(
            "Virtual motion tracker {0} id does not point to a existing device",
            "The virtual motion tracker {0} id {1} does not point to a existing PSMoveServiceEx device.",
            "Properly asign the {0} id to an existing PSMoveServiceEx device and the VMT id to free slot that is not in use.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        ' Check if IDs are -1 or invalid 
        For Each mDevice In GetDevices()
            Select Case (mDevice.iType)
                Case ENUM_DEVICE_TYPE.CONTROLLER
                    If (mDevice.iId < 0 OrElse mDevice.iVmtId < 0) Then
                        mIssues.Add(New STRUC_LOG_ISSUE(mInvalidIdTemplate))
                        Exit For
                    End If

                Case ENUM_DEVICE_TYPE.HMD
                    If (mDevice.iId < 0) Then
                        mIssues.Add(New STRUC_LOG_ISSUE(mInvalidIdTemplate))
                        Exit For
                    End If

            End Select

        Next

        ' Check if ID even point to existing devices
        For Each mDevice In GetDevices()
            If (mDevice.iId < 0) Then
                Continue For
            End If

            Dim bExist As Boolean = False
            Dim mServiceLog As New ClassLogManageServiceDevices(g_mFormMain, g_ClassLogContent)

            Select Case (mDevice.iType)
                Case ENUM_DEVICE_TYPE.CONTROLLER
                    For Each mServiceDevice In mServiceLog.GetDevices()
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
                        mIssue.sMessage = String.Format(mIssue.sMessage, "Controller")
                        mIssue.sDescription = String.Format(mIssue.sDescription, "Controller", mDevice.iId)
                        mIssue.sSolution = String.Format(mIssue.sSolution, "Controller")
                        mIssues.Add(mIssue)
                    End If

                Case ENUM_DEVICE_TYPE.HMD
                    For Each mServiceDevice In mServiceLog.GetDevices()
                        If (mServiceDevice.iType <> ClassLogManageServiceDevices.ENUM_DEVICE_TYPE.HMD) Then
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
                        mIssue.sMessage = String.Format(mIssue.sMessage, "Head-mounted Display")
                        mIssue.sDescription = String.Format(mIssue.sDescription, "Head-mounted Display", mDevice.iId)
                        mIssue.sSolution = String.Format(mIssue.sSolution, "Head-mounted Display")
                        mIssues.Add(mIssue)
                    End If
            End Select
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckCheckGenericRoles() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Virtual motion tracker uses generic tracker roles",
            "Virtual motion tracker controller id {0} uses generic tracker roles. Generic tracker roles require manual bindings to be set up and is not recommended for SteamVR.",
            "If you are planning to use virtual motion trackers with SteamVR, use HTC Vive or Oculus emulated device roles instead.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        For Each mDevice In GetDevices()
            If (mDevice.iType <> ENUM_DEVICE_TYPE.CONTROLLER) Then
                Continue For
            End If

            Select Case (mDevice.iVmtTrackerRole)
                Case ENUM_TRACKER_ROLE.HTC_VIVE_LEFT_CONTROLLER,
                      ENUM_TRACKER_ROLE.HTC_VIVE_RIGHT_CONTROLLER,
                      ENUM_TRACKER_ROLE.OCULUS_TOUCH_LEFT_CONTROLLER,
                      ENUM_TRACKER_ROLE.OCULUS_TOUCH_RIGHT_CONTROLLER,
                      ENUM_TRACKER_ROLE.HTC_VIVE_TRACKER
                    Continue For
            End Select

            Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
            mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iId)
            mIssues.Add(New STRUC_LOG_ISSUE(mIssue))
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

                ' Required
                While True
                    Select Case (True)
                        Case sDevicePath.StartsWith("Controller_")
                            mNewDevice.iType = ENUM_DEVICE_TYPE.CONTROLLER

                        Case sDevicePath.StartsWith("Hmd_")
                            mNewDevice.iType = ENUM_DEVICE_TYPE.HMD

                        Case Else
                            Exit While
                    End Select

                    If (mDevoceProp.ContainsKey("ID")) Then
                        mNewDevice.iId = CInt(mDevoceProp("ID"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("VmtID")) Then
                        mNewDevice.iVmtId = CInt(mDevoceProp("VmtID"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("VmtTrackerRole")) Then
                        mNewDevice.iVmtTrackerRole = CType(CInt(mDevoceProp("VmtTrackerRole")), ENUM_TRACKER_ROLE)
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("FpsOscCounter")) Then
                        mNewDevice.iFpsOscCounter = CInt(mDevoceProp("FpsOscCounter"))
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
