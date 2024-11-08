Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerSteamVrManifests
    Implements ILogAction

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
        Dim sTrackersList As New Text.StringBuilder

        Dim mConfig As New ClassSteamVRConfig
        mConfig.LoadConfig()

        mConfig.m_ClassManifests.LoadConfig()
        For Each sManifest In mConfig.m_ClassManifests.GetManifests
            sTrackersList.AppendFormat("[{0}]", sManifest).AppendLine()
        Next

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_STEAMVR_MANIFESTS
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
