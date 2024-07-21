Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerSteamVrDrivers
    Implements ILogAction

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork

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
End Class
