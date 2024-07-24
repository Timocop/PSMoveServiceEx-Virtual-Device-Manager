Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerSteamVrOverrides
    Implements ILogAction

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork

        Dim sTrackersList As New Text.StringBuilder

        Dim mConfig As New ClassSteamVRConfig
        mConfig.LoadConfig()

        For Each sOverrides In mConfig.m_ClassOverrides.GetOverrides
            sTrackersList.AppendFormat("[{0}]", sOverrides.Key).AppendLine()
            sTrackersList.AppendFormat("Override={0}", sOverrides.Value).AppendLine()
        Next

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_STEAMVR_OVERRIDES
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
