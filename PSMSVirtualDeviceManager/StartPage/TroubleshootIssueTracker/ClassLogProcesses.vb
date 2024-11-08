Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogProcesses
    Implements ILogAction

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Structure STRUC_PROCESS_ITEM
        Dim iId As Integer
        Dim sName As String
        Dim sPath As String
        Dim sDescription As String
        Dim sFileVersion As String
    End Structure

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
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
                sProcessLog.AppendFormat("FileVersion={0}", mProcess.MainModule.FileVersionInfo.FileVersion).AppendLine()
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

    Public Function GetProcesses() As STRUC_PROCESS_ITEM()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mProcessList As New List(Of STRUC_PROCESS_ITEM)
        Dim mProcessProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("["c) AndAlso sLine.EndsWith("]"c)) Then
                Dim sProcessKey As String = sLine.Substring(1, sLine.Length - 2)
                Dim sProcessId As String = sProcessKey.Remove(0, "ProcessID_".Length)

                Dim mNewDevice As New STRUC_PROCESS_ITEM
                mNewDevice.iId = CInt(sProcessId)

                ' Optional
                If (mProcessProp.ContainsKey("ProcessName")) Then
                    mNewDevice.sName = mProcessProp("ProcessName")
                End If

                If (mProcessProp.ContainsKey("FileName")) Then
                    mNewDevice.sPath = mProcessProp("FileName")
                End If

                If (mProcessProp.ContainsKey("FileDescription")) Then
                    mNewDevice.sDescription = mProcessProp("FileDescription")
                End If

                If (mProcessProp.ContainsKey("FileVersion")) Then
                    mNewDevice.sFileVersion = mProcessProp("FileVersion")
                End If

                mProcessList.Add(mNewDevice)
                mProcessProp.Clear()
            End If

            If (sLine.Contains("="c)) Then
                Dim sKey As String = sLine.Substring(0, sLine.IndexOf("="c))
                Dim sValue As String = sLine.Remove(0, sLine.IndexOf("="c) + 1)

                mProcessProp(sKey) = sValue
            End If
        Next

        Return mProcessList.ToArray
    End Function

    Public Function GetProcessByName(sProcessName As String, ByRef r_Process As STRUC_PROCESS_ITEM) As Boolean
        For Each mProcess In GetProcesses()
            If (String.IsNullOrEmpty(mProcess.sName)) Then
                Continue For
            End If

            If (mProcess.sName.ToLowerInvariant = sProcessName.ToLower) Then
                r_Process = mProcess
                Return True
            End If
        Next

        Return False
    End Function
End Class
