Imports PSMSVirtualDeviceManager.ClassLogDiagnostics

Public Class ClassLogManagerSteamVrDrivers
    Implements ILogAction

    Public Shared ReadOnly SECTION_STEAMVR_DRIVERS As String = "SteamVR Drivers"

    Public Shared ReadOnly LOG_ISSUE_NO_STEAMVR_DRIVER As String = "Virtual motion tracker SteamVR driver not installed"
    Public Shared ReadOnly LOG_ISSUE_BAD_STEAMVR_DRIVER As String = "Virtual motion tracker SteamVR driver is not properly installed"
    Public Shared ReadOnly LOG_ISSUE_DISABLED_STEAMVR_DRIVER As String = "Virtual motion tracker SteamVR drivers is disabled or blocked"
    Public Shared ReadOnly LOG_ISSUE_THIRD_PARTY_DRIVER As String = "Third-party SteamVR driver detected"
    Public Shared ReadOnly LOG_ISSUE_CONFLICT_THIRD_PARTY_DRIVER As String = "Conflicting third-party SteamVR driver detected"

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    ' The following drivers can cause conflicts with the VDM SteamVR driver. Mostly because they access the same hardware, which isnt possible.
    Private ReadOnly g_mBadDrivers As String() = {
        "psmove",       'Legacy PSMoveService VRBridge. Its dead and unsupported. VDM SteamVR driver is the successor.
        "trinuspsvr"   'TrinusVR. Conflicts with PSVR hardware access.
    }
    Private ReadOnly g_mWarnDrivers As String() = {
        "driver4vr",    'Driver4VR. PSVR support dropped. Conflicts with PS4 Camera and PSVR hardware access.
        "ivry"          'iVRy. PSVR support outdated and PSMS module broken? Conflicts with PSVR and PS4 Camera hardware access.
    }

    Structure STRUC_DRIVER_ITEM
        Dim sDriverPath As String
        Dim sDriverName As String

        Dim bEnabled As Boolean
        Dim bBlocked As Boolean
    End Structure

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
        Dim sTrackersList As New Text.StringBuilder

        Dim mConfig As New ClassSteamVRConfig
        If (mConfig.LoadConfig()) Then
            For Each mDriver In mConfig.m_ClassDrivers.GetDrivers()
                sTrackersList.AppendFormat("[{0}]", mDriver.sDriverPath).AppendLine()

                If (Not String.IsNullOrEmpty(mDriver.sDriverName)) Then
                    sTrackersList.AppendFormat("DriverName={0}", mDriver.sDriverName).AppendLine()
                    sTrackersList.AppendFormat("Enabled={0}", mConfig.m_ClassDrivers.m_DriverEnabled(mDriver.sDriverName)).AppendLine()
                    sTrackersList.AppendFormat("Blocked={0}", mConfig.m_ClassDrivers.m_DriverBlocked(mDriver.sDriverName)).AppendLine()
                End If
            Next
        End If


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
            LOG_ISSUE_NO_STEAMVR_DRIVER,
            "Virtual motion trackers are set up but the SteamVR driver has not been registered yet.",
            "Register the SteamVR driver in 'Virtual Motion Tracker' to use the virtual motion trackers in SteamVR. If you dont use SteamVR ignore this warning.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mBadPathTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_STEAMVR_DRIVER,
            "The SteamVR driver may not work properly because the driver is registered under an incorrect path '{0}' but should be in '{1}'.",
            "Re-register the SteamVR driver in 'Virtual Motion Tracker' to fix the registered driver path.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mDisabledTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_DISABLED_STEAMVR_DRIVER,
            "The SteamVR driver has been disabled or is blocked by SteamVR due to a recent crash.",
            "Go to the SteamVR settings and re-enable the '{0}' driver.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim mLogVmt As New ClassLogManagerVmtTrackers(g_mFormMain, g_ClassLogContent)
        Dim bDriverExist As Boolean = False
        Dim bDriverDisabled As Boolean = False
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
            For Each mDriver In GetDrivers()
                If (String.IsNullOrEmpty(mDriver.sDriverName)) Then
                    Continue For
                End If

                If (mDriver.sDriverName.ToLowerInvariant = ClassVmtConst.VMT_DRIVER_NAME.ToLowerInvariant) Then
                    bDriverExist = True
                    sDriverPath = mDriver.sDriverPath

                    If (Not mDriver.bEnabled OrElse mDriver.bBlocked) Then
                        bDriverDisabled = True
                    End If

                    If (mDriver.sDriverPath.ToLowerInvariant.StartsWith(sCorrectDriverPath.ToLowerInvariant)) Then
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

                If (bDriverDisabled) Then
                    Dim mIssue As New STRUC_LOG_ISSUE(mDisabledTemplate)
                    mIssue.sSolution = String.Format(mIssue.sSolution, ClassVmtConst.VMT_DRIVER_NAME)
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
            LOG_ISSUE_THIRD_PARTY_DRIVER,
            "A third-party SteamVR driver '{0}' in '{1}' has been detected. Make sure its configured to work properly with the virtual motion tracker SteamVR driver to avoid issues and crashes.",
            "",
            ENUM_LOG_ISSUE_TYPE.INFO
        )

        'ENUM_LOG_ISSUE_TYPE changed dynamicly
        Dim mBadTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_CONFLICT_THIRD_PARTY_DRIVER,
            "The third-party SteamVR driver '{0}' in '{1}' may not compatible with the virtual motion tracker SteamVR driver or may causes issues or crashes when activated.",
            "Uninstall or deactivate the conflicting third-party SteamVR driver '{0}'.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim mLogVmt As New ClassLogManagerVmtTrackers(g_mFormMain, g_ClassLogContent)
        Dim bDriverExist As Boolean = False

        If (mLogVmt.GetDevices().Length > 0) Then
            For Each mDriver In GetDrivers()
                If (String.IsNullOrEmpty(mDriver.sDriverName)) Then
                    Continue For
                End If

                If (mDriver.sDriverName.ToLowerInvariant = ClassVmtConst.VMT_DRIVER_NAME.ToLowerInvariant) Then
                    bDriverExist = True
                    Exit For
                End If
            Next

            If (bDriverExist) Then
                For Each mDriver In GetDrivers()
                    Dim bBadFound As Boolean = False

                    If (String.IsNullOrEmpty(mDriver.sDriverName)) Then
                        Continue For
                    End If

                    If (Not mDriver.bEnabled) Then
                        Continue For
                    End If

                    If (mDriver.sDriverName.ToLowerInvariant = ClassVmtConst.VMT_DRIVER_NAME.ToLowerInvariant) Then
                        Continue For
                    End If

                    For Each sBadDriver In g_mBadDrivers
                        If (mDriver.sDriverName.ToLowerInvariant = sBadDriver.ToLowerInvariant) Then
                            Dim mIssue As New STRUC_LOG_ISSUE(mBadTemplate)
                            mIssue.sDescription = String.Format(mIssue.sDescription, mDriver.sDriverName, mDriver.sDriverPath)
                            mIssue.sSolution = String.Format(mIssue.sSolution, mDriver.sDriverName)
                            mIssue.iType = ENUM_LOG_ISSUE_TYPE.ERROR
                            mIssues.Add(mIssue)

                            bBadFound = True
                        End If
                    Next

                    For Each sBadDriver In g_mWarnDrivers
                        If (mDriver.sDriverName.ToLowerInvariant = sBadDriver.ToLowerInvariant) Then
                            Dim mIssue As New STRUC_LOG_ISSUE(mBadTemplate)
                            mIssue.sDescription = String.Format(mIssue.sDescription, mDriver.sDriverName, mDriver.sDriverPath)
                            mIssue.sSolution = String.Format(mIssue.sSolution, mDriver.sDriverName)
                            mIssue.iType = ENUM_LOG_ISSUE_TYPE.WARNING
                            mIssues.Add(mIssue)

                            bBadFound = True
                        End If
                    Next

                    If (Not bBadFound) Then
                        Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                        mIssue.sDescription = String.Format(mIssue.sDescription, mDriver.sDriverName, mDriver.sDriverPath)
                        mIssues.Add(mIssue)
                    End If
                Next
            End If
        End If

        Return mIssues.ToArray
    End Function

    Public Function GetDrivers() As STRUC_DRIVER_ITEM()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim sDriverList As New List(Of STRUC_DRIVER_ITEM)
        Dim mDriverProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("[") AndAlso sLine.EndsWith("]"c)) Then
                Dim sDevicePath As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewDevice As New STRUC_DRIVER_ITEM

                ' Required
                While True
                    mNewDevice.sDriverPath = sDevicePath

                    If (mDriverProp.ContainsKey("DriverName")) Then
                        mNewDevice.sDriverName = CStr(mDriverProp("DriverName"))
                    Else
                        Exit While
                    End If

                    If (mDriverProp.ContainsKey("Enabled")) Then
                        mNewDevice.bEnabled = (mDriverProp("Enabled").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDriverProp.ContainsKey("Blocked")) Then
                        mNewDevice.bBlocked = (mDriverProp("Blocked").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    sDriverList.Add(mNewDevice)
                    Exit While
                End While

                mDriverProp.Clear()
            End If

            If (sLine.Contains("="c)) Then
                Dim sKey As String = sLine.Substring(0, sLine.IndexOf("="c))
                Dim sValue As String = sLine.Remove(0, sLine.IndexOf("="c) + 1)

                mDriverProp(sKey) = sValue
            End If
        Next

        Return sDriverList.ToArray
    End Function

End Class

