Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerSteamVrDrivers
    Implements ILogAction

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate() Implements ILogAction.Generate
        Dim sTrackersList As New Text.StringBuilder

        Dim mConfig As New ClassOpenVRConfig
        mConfig.LoadConfig()

        For Each sDriver In mConfig.GetDrivers()
            sTrackersList.AppendFormat("[{0}]", sDriver).AppendLine()
        Next

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_STEAMVR_DRIVERS
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckVmtDriver())
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
    End Function

    Public Function CheckVmtDriver() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Virtual motion tracker SteamVR driver not installed",
            "You set up virtual motion trackers but the SteamVR driver has not been registered yet.",
            "Register the SteamVR driver to use the virtual motion trackers in SteamVR. If you dont use SteamVR ignore this warning.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mBadPathTemplate As New STRUC_LOG_ISSUE(
            "Virtual motion tracker SteamVR driver is not properly installed",
            "The SteamVR driver may not work properly because the driver is registered under an incorrect path '{0}' but should be in '{1}'.",
            "Re-register the SteamVR driver to fix the registered driver path.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim mLogVmt As New ClassLogManagerVmtTrackers(g_mFormMain, g_ClassLogContent)
        Dim bDriverExist As Boolean = False
        Dim bDriverPathCorrect As Boolean = False
        Dim sDriverPath As String = ""

        ' $TODO Maybe get assembly name instead? User can change the file name here.
        Dim sApplicationName As String = IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath)

        Dim mLogProcesses As New ClassLogProcesses(g_mFormMain, g_ClassLogContent)
        Dim mApplicationProcess As New ClassLogProcesses.STRUC_PROCESS_ITEM
        If (Not mLogProcesses.GetProcessByName(sApplicationName, mApplicationProcess)) Then
            Return {}
        End If

        If (String.IsNullOrEmpty(mApplicationProcess.sPath)) Then
            Return {}
        End If

        Dim sApplicationDirectory As String = IO.Path.GetDirectoryName(mApplicationProcess.sPath)
        Dim sCorrectDriverPath As String = IO.Path.Combine(sApplicationDirectory, ClassVmtConst.VMT_DRIVER_ROOT_PATH)

        If (mLogVmt.GetDevices().Length > 0) Then
            For Each sDriver In GetDrivers()
                If (sDriver.ToLowerInvariant.EndsWith(ClassVmtConst.VMT_DRIVER_NAME.ToLowerInvariant)) Then
                    bDriverExist = True
                    sDriverPath = sDriver

                    If (sDriver.ToLowerInvariant.StartsWith(sCorrectDriverPath.ToLowerInvariant)) Then
                        bDriverPathCorrect = True
                    End If

                    Exit For
                End If
            Next

            If (Not bDriverExist) Then
                Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                mIssues.Add(mIssue)
            Else
                If (Not bDriverPathCorrect) Then
                    Dim mIssue As New STRUC_LOG_ISSUE(mBadPathTemplate)
                    mIssue.sDescription = String.Format(mIssue.sDescription, sDriverPath, sCorrectDriverPath)
                    mIssues.Add(mIssue)
                End If
            End If
        End If

        Return mIssues.ToArray
    End Function

    Public Function GetDrivers() As String()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim sDriverList As New List(Of String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("[") AndAlso sLine.EndsWith("]"c)) Then
                Dim sDriverPath As String = sLine.Substring(1, sLine.Length - 2)

                sDriverList.Add(sDriverPath)
            End If
        Next

        Return sDriverList.ToArray
    End Function
End Class
