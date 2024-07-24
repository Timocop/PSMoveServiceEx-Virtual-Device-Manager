Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerAttachments
    Implements ILogAction

    Private g_mFormMain As FormMain

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain
    End Sub

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
        If (g_mFormMain.g_mUCVirtualControllers Is Nothing OrElse g_mFormMain.g_mUCVirtualControllers.g_mUCControllerAttachments Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        ' Not thread-safe
        ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                               Dim mAttachments = g_mFormMain.g_mUCVirtualControllers.g_mUCControllerAttachments.GetAttachments()
                                               For Each mItem In mAttachments
                                                   sTrackersList.AppendFormat("[Controller_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                                   sTrackersList.AppendFormat("NickName={0}", mItem.m_Nickname).AppendLine()
                                                   sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                                   sTrackersList.AppendFormat("ParentControllerID={0}", mItem.g_mClassIO.m_ParentController).AppendLine()
                                                   sTrackersList.AppendFormat("FpsPipeCounter={0}", mItem.g_mClassIO.m_FpsPipeCounter).AppendLine()
                                                   sTrackersList.AppendFormat("ControllerOffset={0}", mItem.g_mClassIO.m_ControllerOffset.ToString).AppendLine()
                                                   sTrackersList.AppendFormat("ControllerYawCorrection={0}", mItem.g_mClassIO.m_ControllerYawCorrection).AppendLine()
                                                   sTrackersList.AppendFormat("JointOffset={0}", mItem.g_mClassIO.m_JointOffset.ToString).AppendLine()
                                                   sTrackersList.AppendFormat("JointYawCorrection={0}", mItem.g_mClassIO.m_JointYawCorrection).AppendLine()
                                                   sTrackersList.AppendFormat("OnlyJointOffset={0}", mItem.g_mClassIO.m_OnlyJointOffset).AppendLine()

                                                   sTrackersList.AppendLine()
                                               Next
                                           End Sub)

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_CONTROLLER_ATTACHMENTS
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
