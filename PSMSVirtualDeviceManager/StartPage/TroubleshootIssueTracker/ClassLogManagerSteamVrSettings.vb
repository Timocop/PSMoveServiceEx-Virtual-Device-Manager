Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerSteamVrSettings
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

        sTrackersList.AppendFormat("[{0}]", mConfig.m_SteamPath).AppendLine()
        sTrackersList.AppendFormat("ActivateMultipleDrivers={0}", mConfig.m_ClassSettings.m_ActivateMultipleDrivers).AppendLine()
        sTrackersList.AppendFormat("EnableHomeApp={0}", mConfig.m_ClassSettings.m_EnableHomeApp).AppendLine()
        sTrackersList.AppendFormat("EnableMirrorView={0}", mConfig.m_ClassSettings.m_EnableMirrorView).AppendLine()
        sTrackersList.AppendFormat("EnablePerformanceGraph={0}", mConfig.m_ClassSettings.m_EnablePerformanceGraph).AppendLine()
        sTrackersList.AppendFormat("ForcedDriver={0}", mConfig.m_ClassSettings.m_ForcedDriver).AppendLine()
        sTrackersList.AppendFormat("NullHmdEnabled={0}", mConfig.m_ClassSettings.m_NullHmdEnabled).AppendLine()
        sTrackersList.AppendFormat("RequireHmd={0}", mConfig.m_ClassSettings.m_RequireHmd).AppendLine()

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_STEAMVR_SETTINGS
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
