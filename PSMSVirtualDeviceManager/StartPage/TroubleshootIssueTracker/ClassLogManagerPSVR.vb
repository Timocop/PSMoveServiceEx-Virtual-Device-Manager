Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerPSVR
    Implements ILogAction

    Private g_mFormMain As FormMain

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain
    End Sub

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
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

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_PSVR
    End Function

    Public Function GetIssues(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Throw New NotImplementedException()
    End Function

    Public Function GetSectionContent(mData As Dictionary(Of String, String)) As String Implements ILogAction.GetSectionContent
        If (Not mData.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return mData(GetActionTitle())
    End Function
End Class
