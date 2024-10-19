Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerSteamVrDrivers
    Implements ILogAction

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    ' The following drivers can cause conflicts with the VDM SteamVR driver. Mostly because they access the same hardware, which isnt possible.
    Private ReadOnly g_mBadDrivers As String() = {
        "psmove",       'Legacy PSMoveService VRBridge. Its dead and unsupported. VDM SteamVR driver is the successor.
        "trinuspsvr",   'TrinusVR. Conflicts with PSVR hardware access.
        "driver4vr",    'Driver4VR. PSVR support dropped. Conflicts with PS4 Camera and PSVR hardware access.
        "iVRy"          'iVRy. PSVR support outdated and PSMS module broken? Conflicts with PSVR and PS4 Camera hardware access.
    }

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
        mIssues.AddRange(CheckBadDrivers())
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
            "Virtual motion trackers are set up but the SteamVR driver has not been registered yet.",
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

    Public Function CheckBadDrivers() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Third-party SteamVR driver detected",
            "A third-party SteamVR driver '{0}' has been detected. Make sure its configured to work properly with the virtual motion tracker SteamVR driver to avoid issues and crashes.",
            "",
            ENUM_LOG_ISSUE_TYPE.INFO
        )

        Dim mBadTemplate As New STRUC_LOG_ISSUE(
            "Conflicting third-party SteamVR driver detected",
            "The third-party SteamVR driver '{0}' is not compatible with the virtual motion tracker SteamVR driver or may causes issues or crashes when activated.",
            "Uninstall or deactivate the conflicting third-party SteamVR driver '{0}'.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim mLogVmt As New ClassLogManagerVmtTrackers(g_mFormMain, g_ClassLogContent)
        Dim bDriverExist As Boolean = False

        If (mLogVmt.GetDevices().Length > 0) Then
            For Each sDriver In GetDrivers()
                If (sDriver.ToLowerInvariant.EndsWith(ClassVmtConst.VMT_DRIVER_NAME.ToLowerInvariant)) Then
                    bDriverExist = True
                    Exit For
                End If
            Next

            If (bDriverExist) Then
                For Each sDriver In GetDrivers()
                    Dim bBadFound As Boolean = False

                    If (sDriver.ToLowerInvariant.EndsWith(ClassVmtConst.VMT_DRIVER_NAME.ToLowerInvariant)) Then
                        Continue For
                    End If

                    For Each sBadDriver In g_mBadDrivers
                        If (sDriver.ToLowerInvariant.EndsWith(sBadDriver.ToLowerInvariant)) Then
                            Dim mIssue As New STRUC_LOG_ISSUE(mBadTemplate)
                            mIssue.sDescription = String.Format(mIssue.sDescription, sDriver)
                            mIssue.sSolution = String.Format(mIssue.sSolution, sDriver)
                            mIssues.Add(mIssue)

                            bBadFound = True
                        End If
                    Next

                    If (Not bBadFound) Then
                        Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                        mIssue.sDescription = String.Format(mIssue.sDescription, sDriver)
                        mIssues.Add(mIssue)
                    End If
                Next
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

