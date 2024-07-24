Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerVmtTrackers
    Implements ILogAction

    Private g_mFormMain As FormMain

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain
    End Sub

    Public Sub Generate(mData As Dictionary(Of String, String)) Implements ILogAction.Generate
        If (g_mFormMain.g_mUCVirtualMotionTracker Is Nothing OrElse g_mFormMain.g_mUCVirtualMotionTracker.g_UCVmtTrackers Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        ' Not thread-safe
        ClassUtils.SyncInvoke(g_mFormMain, Sub()
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
                                                   sTrackersList.AppendFormat("VmtTrackerRole={0}", mItem.g_mClassIO.m_VmtTrackerRole).AppendLine()
                                                   sTrackersList.AppendFormat("FpsOscCounter={0}", mItem.g_mClassIO.m_FpsOscCounter).AppendLine()

                                                   sTrackersList.AppendLine()
                                               Next
                                           End Sub)

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_VMT_TRACKERS
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
