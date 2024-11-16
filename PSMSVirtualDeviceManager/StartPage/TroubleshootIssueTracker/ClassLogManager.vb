Imports PSMSVirtualDeviceManager.ClassLogDiagnostics

Public Class ClassLogManager
    Implements ILogAction

    Public Shared ReadOnly SECTION_VDM_CONFIG As String = "VDM Configuration"

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
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

            Using mSafeCopy As New ClassUtils.ClassSafeFileCopy(mItem.Value)
                g_ClassLogContent.m_Content(mItem.Key) = IO.File.ReadAllText(mSafeCopy.m_TemporaryFile)
            End Using
        Next
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_CONFIG
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
