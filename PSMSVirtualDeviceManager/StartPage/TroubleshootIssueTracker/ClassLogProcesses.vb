Imports System.Text.RegularExpressions
Imports PSMSVirtualDeviceManager.ClassLogDiagnostics

Public Class ClassLogProcesses
    Implements ILogAction

    Public Shared ReadOnly SECTION_PROCESSES As String = "Running Processes"
    Public Shared ReadOnly LOG_ISSUE_BAD_SERVICE_PATH As String = "PSMoveServiceEx not installed using Virtual Device Manager"
    Public Shared ReadOnly LOG_ISSUE_BAD_SERVICE_VERSION As String = "Outdated PSMoveServiceEx version"
    Public Shared ReadOnly LOG_ISSUE_BAD_MANAGER_VERSION As String = "Outdated PSMoveServiceEx - Virtual Device Manager version"

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
            Dim bIgnore As Boolean = False
            Try
                Dim sProcessPath As String = mProcess.MainModule.FileName
                If (sProcessPath.ToLowerInvariant.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.System).ToLowerInvariant) OrElse
                    sProcessPath.ToLowerInvariant.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86).ToLowerInvariant)) Then
                    bIgnore = True
                End If
            Catch ex As Exception
                ' Protected process or access denied
                bIgnore = True
            End Try

            If (bIgnore) Then
                Continue For
            End If

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
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckServicePath())
        mIssues.AddRange(CheckServiceVersion())
        mIssues.AddRange(CheckManagerVersion())
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
    End Function

    Public Function CheckServicePath() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_SERVICE_PATH,
            "PSMoveServiceEx which is located in '{0}' has not been installed using Virtual Device Manager. Installing PSMoveServiceEx in a different location than the Virtual Device Manager directory may cause log-reading errors and are harder to manage.",
            "Uninstall any existing PSMoveServiceEx installations (such as '{0}') and reinstall it via Virtual Device Manager using: Service Management > Reinstall PSMoveServiceEx.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim sManagerPath As String = ""
        Dim sServicePath As String = ""

        For Each mProcess In GetProcesses()
            Select Case (mProcess.sName.ToLowerInvariant)
                Case "PSMSVirtualDeviceManager".ToLowerInvariant

                    sManagerPath = mProcess.sPath
                Case "PSMoveService".ToLowerInvariant,
                     "PSMoveServiceAdmin".ToLowerInvariant

                    sServicePath = mProcess.sPath
            End Select
        Next



        If (Not String.IsNullOrEmpty(sManagerPath) AndAlso sManagerPath.TrimEnd.Length > 0 AndAlso
            Not String.IsNullOrEmpty(sServicePath) AndAlso sServicePath.TrimEnd.Length > 0) Then

            Dim sManagerDirectory As String = IO.Path.GetDirectoryName(sManagerPath)
            Dim sExpectedPath As String = IO.Path.Combine(sManagerDirectory, "PSMoveServiceEx")

            If (Not sServicePath.ToLowerInvariant.StartsWith(sExpectedPath.ToLowerInvariant)) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                mNewIssue.sDescription = String.Format(mTemplate.sDescription, sServicePath)
                mNewIssue.sSolution = String.Format(mTemplate.sSolution, sServicePath)

                mIssues.Add(mNewIssue)
            End If
        End If

        Return mIssues.ToArray
    End Function

    Public Function CheckServiceVersion() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_SERVICE_VERSION,
            "This PSMoveServiceEx version is outdated (Current: v{0} / Newest: v{1}) and could still have issues that already have been fixed or missing new features.",
            "Udpate PSMoveServiceEx.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        For Each mProcess In GetProcesses()
            Select Case (mProcess.sName.ToLowerInvariant)
                Case "PSMoveService".ToLowerInvariant,
                     "PSMoveServiceAdmin".ToLowerInvariant
                    Dim sCurrentVersion As String = mProcess.sFileVersion

                    Try
                        Dim sNextVersion As String = ClassUpdate.ClassPsms.GetNextVersion(Nothing)

                        sNextVersion = Regex.Match(sNextVersion, "[0-9\.]+").Value
                        sCurrentVersion = Regex.Match(sCurrentVersion, "[0-9\.]+").Value

                        If (New Version(sNextVersion) > New Version(sCurrentVersion)) Then
                            Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                            mNewIssue.sDescription = String.Format(mTemplate.sDescription, New Version(sCurrentVersion).ToString, New Version(sNextVersion).ToString)

                            mIssues.Add(mNewIssue)
                        End If
                    Catch ex As Threading.ThreadAbortException
                        Throw
                    Catch ex As Exception
                        ' Ignore any connection issues
                    End Try
            End Select
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckManagerVersion() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_MANAGER_VERSION,
            "This PSMoveServiceEx - Virtual Device Manager version is outdated (Current: v{0} / Newest: v{1}) and could still have issues that already have been fixed or missing new features.",
            "Udpate PSMoveServiceEx - Virtual Device Manager.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        For Each mProcess In GetProcesses()
            Select Case (mProcess.sName.ToLowerInvariant)
                Case "PSMSVirtualDeviceManager".ToLowerInvariant
                    Dim sCurrentVersion As String = mProcess.sFileVersion

                    Try
                        Dim sNextVersion As String = ClassUpdate.ClassVdm.GetNextVersion(Nothing)

                        sNextVersion = Regex.Match(sNextVersion, "[0-9\.]+").Value
                        sCurrentVersion = Regex.Match(sCurrentVersion, "[0-9\.]+").Value

                        If (New Version(sNextVersion) > New Version(sCurrentVersion)) Then
                            Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                            mNewIssue.sDescription = String.Format(mTemplate.sDescription, New Version(sCurrentVersion).ToString, New Version(sNextVersion).ToString)

                            mIssues.Add(mNewIssue)
                        End If
                    Catch ex As Threading.ThreadAbortException
                        Throw
                    Catch ex As Exception
                        ' Ignore any connection issues
                    End Try
            End Select
        Next

        Return mIssues.ToArray
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
