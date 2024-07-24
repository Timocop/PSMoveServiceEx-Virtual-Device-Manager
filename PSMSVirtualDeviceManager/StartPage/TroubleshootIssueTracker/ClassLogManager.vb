Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManager
    Implements ILogAction

    Public Sub Generate(mData As Dictionary(Of String, String)) Implements ILogAction.Generate
        Dim mManagerLogs As New Dictionary(Of String, String)
        mManagerLogs("VDM INI Application Exceptions") = (ClassConfigConst.PATH_LOG_APPLICATION_ERROR)
        mManagerLogs("VDM INI Settings") = (ClassConfigConst.PATH_CONFIG_SETTINGS)
        mManagerLogs("VDM INI Attachments") = (ClassConfigConst.PATH_CONFIG_ATTACHMENT)
        mManagerLogs("VDM INI Remote Devices") = (ClassConfigConst.PATH_CONFIG_REMOTE)
        mManagerLogs("VDM INI Virtual Motion Trackers") = (ClassConfigConst.PATH_CONFIG_VMT)
        mManagerLogs("VDM INI Virutal Trackers") = (ClassConfigConst.PATH_CONFIG_DEVICES)

        For Each mItem In mManagerLogs
            If (Not IO.File.Exists(mItem.Value)) Then
                Continue For
            End If


            Dim sTmp As String = IO.Path.GetTempFileName
            IO.File.Copy(mItem.Value, sTmp, True)

            mData(mItem.Key) = IO.File.ReadAllText(sTmp, System.Text.Encoding.Default)
            IO.File.Delete(sTmp)
        Next
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_CONFIG
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
