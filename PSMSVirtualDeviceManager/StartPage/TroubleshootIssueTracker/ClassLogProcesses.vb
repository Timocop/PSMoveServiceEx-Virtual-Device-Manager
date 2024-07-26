Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogProcesses
    Implements ILogAction

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate() Implements ILogAction.Generate
        Dim sProcessLog As New Text.StringBuilder

        For Each mProcess In Process.GetProcesses
            Try
                sProcessLog.AppendFormat("[ProcessID_{0}]", mProcess.Id).AppendLine()
                sProcessLog.AppendFormat("ProcessName={0}", mProcess.ProcessName).AppendLine()
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
            End Try

            Try
                sProcessLog.AppendFormat("FileName={0}", mProcess.MainModule.FileName).AppendLine()
                sProcessLog.AppendFormat("FileDescription={0}", mProcess.MainModule.FileVersionInfo.FileDescription).AppendLine()
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
            End Try

            sProcessLog.AppendLine()
        Next

        g_ClassLogContent.m_Content(GetActionTitle()) = sProcessLog.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_PROCESSES
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
