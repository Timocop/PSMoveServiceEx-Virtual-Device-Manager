Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerSteamVrSettings
    Implements ILogAction

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork

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

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_STEAMVR_SETTINGS
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
