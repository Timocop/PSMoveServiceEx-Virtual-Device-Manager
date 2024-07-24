Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerSteamVrManifests
    Implements ILogAction

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork

        Dim sTrackersList As New Text.StringBuilder

        Dim mConfig As New ClassSteamVRConfig
        mConfig.LoadConfig()

        mConfig.m_ClassManifests.LoadConfig()
        For Each sManifest In mConfig.m_ClassManifests.GetManifests
            sTrackersList.AppendFormat("[{0}]", sManifest).AppendLine()
        Next

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_STEAMVR_MANIFESTS
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
