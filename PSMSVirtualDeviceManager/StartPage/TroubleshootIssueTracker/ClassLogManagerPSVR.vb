Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

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

        ' Not thread-safe
        ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                               sTrackersList.AppendFormat("[PlayStationVR]").AppendLine()
                                               sTrackersList.AppendFormat("Status={0}", g_mFormMain.g_mUCPlaystationVR.Label_PSVRStatus.Text).AppendLine()
                                               sTrackersList.AppendLine()

                                               sTrackersList.AppendFormat("[PlayStationVR HDMI]").AppendLine()
                                               sTrackersList.AppendFormat("Status={0}", g_mFormMain.g_mUCPlaystationVR.Label_HDMIStatus.Text).AppendLine()
                                               sTrackersList.AppendFormat("Text={0}", g_mFormMain.g_mUCPlaystationVR.Label_HDMIStatusText.Text).AppendLine()
                                               sTrackersList.AppendLine()

                                               sTrackersList.AppendFormat("[PlayStationVR USB]").AppendLine()
                                               sTrackersList.AppendFormat("Status={0}", g_mFormMain.g_mUCPlaystationVR.Label_USBStatus.Text).AppendLine()
                                               sTrackersList.AppendFormat("Text={0}", g_mFormMain.g_mUCPlaystationVR.Label_USBStatusText.Text).AppendLine()
                                               sTrackersList.AppendLine()

                                               sTrackersList.AppendFormat("[PlayStationVR Display]").AppendLine()
                                               sTrackersList.AppendFormat("Status={0}", g_mFormMain.g_mUCPlaystationVR.Label_DisplayStatus.Text).AppendLine()
                                               sTrackersList.AppendFormat("Text={0}", g_mFormMain.g_mUCPlaystationVR.Label_DisplayStatusText.Text).AppendLine()
                                               sTrackersList.AppendLine()
                                           End Sub)

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
End Class
