Imports PSMSVirtualDeviceManager.ClassLogDiagnostics
Imports PSMSVirtualDeviceManager.UCPlaystationVR

Public Class ClassLogManagerPSVR
    Implements ILogAction

    Public Shared ReadOnly SECTION_VDM_PSVR As String = "VDM PlayStation VR"

    Public Shared ReadOnly LOG_ISSUE_PSVR_HDMI_ERROR As String = "PlayStation VR encountered an HDMI issue"
    Public Shared ReadOnly LOG_ISSUE_PSVR_USB_ERROR As String = "PlayStation VR encountered an USB issue"
    Public Shared ReadOnly LOG_ISSUE_PSVR_DISPLAY_ERROR As String = "PlayStation VR encountered an display issue"

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
        If (g_mFormMain.g_mUCPlaystationVR Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        sTrackersList.AppendFormat("[PlayStationVR]").AppendLine()
        sTrackersList.AppendFormat("HdmiStatus={0}", CInt(g_mFormMain.g_mUCPlaystationVR.m_DeviceHdmiStatus)).AppendLine()
        sTrackersList.AppendFormat("HdmiStatusName={0}", g_mFormMain.g_mUCPlaystationVR.m_DeviceHdmiStatus.ToString).AppendLine()
        sTrackersList.AppendFormat("UsbStatus={0}", CInt(g_mFormMain.g_mUCPlaystationVR.m_DeviceUsbStatus)).AppendLine()
        sTrackersList.AppendFormat("UsbStatusName={0}", g_mFormMain.g_mUCPlaystationVR.m_DeviceUsbStatus.ToString).AppendLine()
        sTrackersList.AppendFormat("DisplayStatus={0}", CInt(g_mFormMain.g_mUCPlaystationVR.m_DeviceDisplayStatus)).AppendLine()
        sTrackersList.AppendFormat("DisplayStatusName={0}", g_mFormMain.g_mUCPlaystationVR.m_DeviceDisplayStatus.ToString).AppendLine()
        sTrackersList.AppendLine()

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_PSVR
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckPsvrHasError())
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
    End Function

    Public Function CheckPsvrHasError() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mHdmiTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_PSVR_HDMI_ERROR,
            "PlayStation VR encountered the following error: {0}",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mUsbTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_PSVR_USB_ERROR,
            "PlayStation VR encountered the following error: {0}",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mDisplayTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_PSVR_DISPLAY_ERROR,
            "PlayStation VR encountered the following error: {0}",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim iHdmiStatus As ENUM_DEVICE_HDMI_STATUS
        Dim iUsbStatus As ENUM_DEVICE_USB_STATUS
        Dim iDisplayStatus As ENUM_DEVICE_DISPLAY_STATUS
        If (Not GetDeviceStatus(iHdmiStatus, iUsbStatus, iDisplayStatus)) Then
            Return {}
        End If

        If (iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED AndAlso
                iUsbStatus = ENUM_DEVICE_USB_STATUS.NOT_CONNECTED AndAlso
                iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.NOT_CONNECTED) Then
            Return {}
        End If

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim mHdmiStatusSolution As New Dictionary(Of ENUM_DEVICE_HDMI_STATUS, String)
        Dim mUsbStatusSolution As New Dictionary(Of ENUM_DEVICE_USB_STATUS, String)
        Dim mDisplayStatusSolution As New Dictionary(Of ENUM_DEVICE_DISPLAY_STATUS, String)

        mUsbStatusSolution(ENUM_DEVICE_USB_STATUS.DRIVER_ISSUE) = "Install the PlayStation VR drivers found in 'PlayStation VR Management'."
        mUsbStatusSolution(ENUM_DEVICE_USB_STATUS.CONNECTED_NO_DATA) = "Run PSMoveServiceEx to receive data from your PlayStation VR head-mounted display."
        mDisplayStatusSolution(ENUM_DEVICE_DISPLAY_STATUS.WAITING_FOR_RELOAD) = "PlayStation VR display configuration has been changed. Restart the PlayStation VR or re-plug the HDMI cable."
        mDisplayStatusSolution(ENUM_DEVICE_DISPLAY_STATUS.BAD_FREQUENCY) = "Increase the PlayStation VR display frequency found in 'PlayStation VR Management'."
        mDisplayStatusSolution(ENUM_DEVICE_DISPLAY_STATUS.DISABLED) = "Enable the PlayStation VR display in the Windows display settings and set it to 'Extended'."
        mDisplayStatusSolution(ENUM_DEVICE_DISPLAY_STATUS.MIRRROED) = "Set the PlayStation VR display in the Windows display settings to 'Extended'."
        mDisplayStatusSolution(ENUM_DEVICE_DISPLAY_STATUS.NOT_CONFIGURED) = "Configure the PlayStation VR display for Virtual-Mode or Direct-Mode found in 'PlayStation VR Management'."
        mDisplayStatusSolution(ENUM_DEVICE_DISPLAY_STATUS.UNSUPPORTED) = "Contact support."

        Select Case (iHdmiStatus)
            Case ENUM_DEVICE_HDMI_STATUS.CONNECTED, ENUM_DEVICE_HDMI_STATUS.DIRECT_MODE
                ' Good
            Case Else
                Dim mIssue As New STRUC_LOG_ISSUE(mHdmiTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, iHdmiStatus.ToString)

                If (mHdmiStatusSolution.ContainsKey(iHdmiStatus)) Then
                    mIssue.sSolution = mHdmiStatusSolution(iHdmiStatus)
                End If

                mIssues.Add(mIssue)
        End Select

        Select Case (iUsbStatus)
            Case ENUM_DEVICE_USB_STATUS.CONNECTED
                ' Good 
            Case Else
                Dim mIssue As New STRUC_LOG_ISSUE(mUsbTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, iUsbStatus.ToString)

                If (mUsbStatusSolution.ContainsKey(iUsbStatus)) Then
                    mIssue.sSolution = mUsbStatusSolution(iUsbStatus)
                End If

                mIssues.Add(mIssue)
        End Select

        Select Case (iDisplayStatus)
            Case ENUM_DEVICE_DISPLAY_STATUS.CONFIGURED_MULTI, ENUM_DEVICE_DISPLAY_STATUS.DIRECT_MODE
                ' Good 
            Case Else
                Dim mIssue As New STRUC_LOG_ISSUE(mDisplayTemplate)
                mIssue.sDescription = String.Format(mIssue.sDescription, iDisplayStatus.ToString)

                If (mDisplayStatusSolution.ContainsKey(iDisplayStatus)) Then
                    mIssue.sSolution = mDisplayStatusSolution(iDisplayStatus)
                End If

                mIssues.Add(mIssue)
        End Select

        Return mIssues.ToArray
    End Function

    Public Function GetDeviceStatus(ByRef _HdmiStatus As ENUM_DEVICE_HDMI_STATUS,
                                    ByRef _UsbStatus As ENUM_DEVICE_USB_STATUS,
                                    ByRef _DisplayStatus As ENUM_DEVICE_DISPLAY_STATUS) As Boolean
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return False
        End If

        Dim mDeviceProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i)

            If (sLine.StartsWith("["c) AndAlso sLine.EndsWith("]"c)) Then
                If (mDeviceProp.ContainsKey("HdmiStatus") AndAlso
                    mDeviceProp.ContainsKey("UsbStatus") AndAlso
                    mDeviceProp.ContainsKey("DisplayStatus")) Then

                    _HdmiStatus = CType(CInt(mDeviceProp("HdmiStatus")), ENUM_DEVICE_HDMI_STATUS)
                    _UsbStatus = CType(CInt(mDeviceProp("UsbStatus")), ENUM_DEVICE_USB_STATUS)
                    _DisplayStatus = CType(CInt(mDeviceProp("DisplayStatus")), ENUM_DEVICE_DISPLAY_STATUS)

                    Return True
                End If

                mDeviceProp.Clear()
            End If

            If (sLine.Contains("="c)) Then
                Dim sKey As String = sLine.Substring(0, sLine.IndexOf("="c))
                Dim sValue As String = sLine.Remove(0, sLine.IndexOf("="c) + 1)

                mDeviceProp(sKey) = sValue
            End If
        Next

        Return False
    End Function
End Class
