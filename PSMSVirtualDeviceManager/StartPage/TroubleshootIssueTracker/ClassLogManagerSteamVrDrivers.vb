Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerSteamVrDrivers
    Implements ILogAction

    Public Sub Generate(mData As Dictionary(Of String, String)) Implements ILogAction.Generate

        Dim sTrackersList As New Text.StringBuilder

        Dim mConfig As New ClassOpenVRConfig
        mConfig.LoadConfig()

        For Each sDriver In mConfig.GetDrivers()
            sTrackersList.AppendFormat("[{0}]", sDriver).AppendLine()
        Next

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_STEAMVR_DRIVERS
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
