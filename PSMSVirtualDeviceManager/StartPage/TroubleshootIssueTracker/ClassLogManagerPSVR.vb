Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs
Imports PSMSVirtualDeviceManager.UCPlaystationVR

Public Class ClassLogManagerPSVR
    Implements ILogAction

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate() Implements ILogAction.Generate
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
        Throw New NotImplementedException()
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
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
