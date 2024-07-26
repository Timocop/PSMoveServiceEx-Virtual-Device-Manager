Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerSteamVrOverrides
    Implements ILogAction

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate() Implements ILogAction.Generate
        Dim sTrackersList As New Text.StringBuilder

        Dim mConfig As New ClassSteamVRConfig
        mConfig.LoadConfig()

        For Each sOverrides In mConfig.m_ClassOverrides.GetOverrides
            sTrackersList.AppendFormat("[{0}]", sOverrides.Key).AppendLine()
            sTrackersList.AppendFormat("Override={0}", sOverrides.Value).AppendLine()
        Next

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_STEAMVR_OVERRIDES
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
