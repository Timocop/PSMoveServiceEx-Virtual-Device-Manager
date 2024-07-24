Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogProcesses
    Implements ILogAction

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
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

        mData(GetActionTitle()) = sProcessLog.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_PROCESSES
    End Function

    Public Function GetIssues(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Throw New NotImplementedException()
    End Function

    Public Function GetSectionContent(mData As Dictionary(Of String, String)) As String Implements ILogAction.GetSectionContent
        Throw New NotImplementedException()
    End Function
End Class
