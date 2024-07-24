Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerRemoteDevices
    Implements ILogAction

    Private g_mFormMain As FormMain

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain
    End Sub

    Public Sub Generate(mData As Dictionary(Of String, String)) Implements ILogAction.Generate
        If (g_mFormMain.g_mUCVirtualControllers Is Nothing OrElse g_mFormMain.g_mUCVirtualControllers.g_mUCRemoteDevices Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        ' Not thread-safe
        ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                               Dim mRemoteDevices = g_mFormMain.g_mUCVirtualControllers.g_mUCRemoteDevices.GetRemoteDevices()
                                               For Each mItem In mRemoteDevices
                                                   sTrackersList.AppendFormat("[Controller_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                                   sTrackersList.AppendFormat("NickName={0}", mItem.m_Nickname).AppendLine()
                                                   sTrackersList.AppendFormat("TrackerName={0}", mItem.m_TrackerName).AppendLine()
                                                   sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                                   sTrackersList.AppendFormat("FpsPipeCounter={0}", mItem.g_mClassIO.m_FpsPipeCounter).AppendLine()
                                                   sTrackersList.AppendFormat("Orientation={0}", ClassQuaternionTools.FromQ(mItem.g_mClassIO.m_Orientation).ToString).AppendLine()
                                                   sTrackersList.AppendFormat("ResetOrientation={0}", ClassQuaternionTools.FromQ(mItem.g_mClassIO.m_ResetOrientation).ToString).AppendLine()
                                                   sTrackersList.AppendFormat("YawOrientationOffset={0}", mItem.g_mClassIO.m_YawOrientationOffset).AppendLine()

                                                   sTrackersList.AppendLine()
                                               Next
                                           End Sub)

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_REMOTE_DEVICES
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
