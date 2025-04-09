Imports System.Text.RegularExpressions
Imports PSMSVirtualDeviceManager.ClassLogDiagnostics

Public Class ClassLogService
    Implements ILogAction

    Public Shared ReadOnly SECTION_PSMOVESERVICEEX As String = "PSMoveServiceEx"

    Public Shared ReadOnly LOG_ISSUE_EMPTY As String = "Log is unavailable"
    Public Shared ReadOnly LOG_ISSUE_BAD_SERVICE_VERSION As String = "Outdated PSMoveServiceEx version"
    Public Shared ReadOnly LOG_ISSUE_BAD_SERVICE_FPS As String = "PSMoveServiceEx framerate too low"
    Public Shared ReadOnly LOG_ISSUE_BAD_LEGACY_SERVICE As String = "Legacy PSMoveService configuration detected"
    Public Shared ReadOnly LOG_ISSUE_CONFIG_RESET As String = "Some configurations have been factory reset"
    Public Shared ReadOnly LOG_ISSUE_BLUETOOTH_FAIL As String = "Failed to retrieve bluetooth adapter information"
    Public Shared ReadOnly LOG_ISSUE_BLUETOOTH_ADDRESS_FAIL As String = "Failed to set host address"
    Public Shared ReadOnly LOG_ISSUE_DEVICE_FAIL As String = "Failed to open device"
    Public Shared ReadOnly LOG_ISSUE_DEVICE_SLOT_MAX As String = "Device slot limit reached"
    Public Shared ReadOnly LOG_ISSUE_PSVR_FAIL As String = "Failed to open PlayStation VR Head-mounted Display"
    Public Shared ReadOnly LOG_ISSUE_NO_BLUETOOTH As String = "Failed to find bluetooth device"
    Public Shared ReadOnly LOG_ISSUE_BLUETOOTH_PAIRING As String = "Bluetooth device encountered pairing issues"
    Public Shared ReadOnly LOG_ISSUE_BLUETOOTH_NOT_FOUND As String = "Bluetooth device could not be found"
    Public Shared ReadOnly LOG_ISSUE_DEVICE_TIMEOUT As String = "Device timed out"
    Public Shared ReadOnly LOG_ISSUE_SERVICE_LOG_INCOMPLETE As String = "PSMoveServiceEx log incomplete"
    Public Shared ReadOnly LOG_ISSUE_DEVICE_BAD_TRACKING As String = "Bad device tracking deviations"

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
        Dim mConfig As New ClassServiceInfo
        mConfig.LoadConfig()

        If (Not mConfig.FileExist()) Then
            If (Not mConfig.FindByProcess()) Then
                Throw New ArgumentException("PSMoveServiceEx not found")
            End If
        End If

        If (Not mConfig.FileExist) Then
            Throw New ArgumentException("Could not find PSMoveServiceEx executable")
        End If

        Dim sServceDirectory As String = IO.Path.GetDirectoryName(mConfig.m_FileName)
        Dim sLogFile As String = IO.Path.Combine(sServceDirectory, "PSMoveServiceEx.log")

        If (Not IO.File.Exists(sLogFile)) Then
            Throw New ArgumentException("Could not find PSMoveServiceEx logs")
        End If

        Dim sLogContent As String = ClassUtils.FileReadAllTextSafe(sLogFile)

        ' Post process logs.
        PostProcessLogs(sLogContent)

        g_ClassLogContent.m_Content(GetActionTitle()) = sLogContent
    End Sub

    Private Sub PostProcessLogs(ByRef sLogContent As String)
        Dim mConfigs As New Dictionary(Of String, KeyValuePair(Of String, String))
        Dim sConfigPath As String = ClassServiceConfig.GetConfigPath()
        If (String.IsNullOrEmpty(sConfigPath)) Then
            Return
        End If

        ' Get all changed config files
        Dim sLines As String() = sLogContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            ' We only want changed files.
            If (sLine.StartsWith("["c) AndAlso sLine.EndsWith(".json - Configuration saved!")) Then
                Dim sSplitPath As String() = sLine.Split("\"c)
                If (sSplitPath.Length > 0) Then
                    Dim sFileName As String = sSplitPath(sSplitPath.Length - 1)
                    sFileName = sFileName.Substring(0, sFileName.LastIndexOf(".json")) & ".json"
                    sFileName = IO.Path.GetFileName(sFileName)

                    Dim sConfigFile As String = IO.Path.Combine(sConfigPath, sFileName)
                    If (IO.File.Exists(sConfigFile)) Then
                        Dim sConfigContent As String = ClassUtils.FileReadAllTextSafe(sConfigFile)

                        mConfigs(sFileName.ToLowerInvariant) = New KeyValuePair(Of String, String)(
                            sConfigFile,
                            ClassUtils.FormatJsonOutput(sConfigContent)
                        )
                    End If
                End If
            End If
        Next

        ' Append all changed config files
        Dim sLogContentBuilder As New Text.StringBuilder(sLogContent)
        Dim mNow As Date = Now
        Dim iNowMilliseconds As Integer = mNow.Millisecond
        Dim sFormattedTime As String = String.Format("[{0:yyyy-MM-dd HH:mm:ss}.{1:D3}]: ", mNow, iNowMilliseconds)
        For Each mItem In mConfigs
            Dim sFilePath As String = IO.Path.GetFullPath(mItem.Value.Key)

            sLogContentBuilder.AppendFormat("{0} {1} - Configuration changed!", sFormattedTime, sFilePath).AppendLine()
            sLogContentBuilder.AppendFormat("{0} {1} - {2}", sFormattedTime, sFilePath, mItem.Value.Value).AppendLine()
        Next

        sLogContent = sLogContentBuilder.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_PSMOVESERVICEEX
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckEmpty())
        mIssues.AddRange(CheckVersion())
        mIssues.AddRange(CheckFps())
        mIssues.AddRange(CheckLegacy())
        mIssues.AddRange(CheckConfigReset())
        mIssues.AddRange(CheckBluetoothAdapterFail())
        mIssues.AddRange(CheckBluetoothAdapterAssignFail())
        mIssues.AddRange(CheckDeviceOpenFail())
        mIssues.AddRange(CheckDeviceLimit())
        mIssues.AddRange(CheckMorpheusFail())
        mIssues.AddRange(CheckBluetoothPairingNotFound())
        mIssues.AddRange(CheckBluetoothPairingFail())
        mIssues.AddRange(CheckDeviceTimeout())
        mIssues.AddRange(CheckIncomplete())
        mIssues.AddRange(CheckBadDeviations())
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
    End Function

    Public Function CheckEmpty() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_EMPTY,
            "Some diagnostic details are unavailable due to missing log data.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        If (sContent Is Nothing OrElse sContent.Trim.Length = 0) Then
            mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))
        End If

        Return mIssues.ToArray
    End Function

    Public Function CheckVersion() As STRUC_LOG_ISSUE()
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

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("Starting PSMoveServiceEx")) Then
                Dim mMatch As Match = Regex.Match(sLine, "Starting PSMoveServiceEx v(?<Version>\d+\.\d+\.\d+\.\d+)", RegexOptions.IgnoreCase)

                If (mMatch.Success AndAlso mMatch.Groups("Version").Success) Then
                    Try
                        Dim sCurrentVersion As String = mMatch.Groups("Version").Value
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
                End If

                Exit For
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckFps() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_SERVICE_FPS,
            "PSMoveServiceEx is running at a very low framerate {0} (minimum {1}) which can cause bad tracking quality.",
            "Upgrade your computers CPU.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("PSMoveServiceEx - Main thread running")) Then
                Dim mAvgMatch As Match = Regex.Match(sLine, "Main thread running at (?<Average>(\d+)) FPS average", RegexOptions.IgnoreCase)
                Dim mMiNMatch As Match = Regex.Match(sLine, "Lowest FPS was (?<Minimum>(\d+))", RegexOptions.IgnoreCase)

                If (mAvgMatch.Success AndAlso mMiNMatch.Success AndAlso mAvgMatch.Groups("Average").Success AndAlso mMiNMatch.Groups("Minimum").Success) Then
                    Dim iFpsAvg As Integer = CInt(mAvgMatch.Groups("Average").Value)
                    Dim iFpsMinimum As Integer = CInt(mMiNMatch.Groups("Minimum").Value)

                    If (iFpsAvg < 100 OrElse iFpsMinimum < 100) Then
                        Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                        mNewIssue.sDescription = String.Format(mTemplate.sDescription, iFpsAvg, iFpsMinimum)

                        mIssues.Add(mNewIssue)
                    End If
                End If

                Exit For
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckLegacy() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_LEGACY_SERVICE,
            "Legacy PSMoveService configuration has been found. Abnormal tracking side effects and bad performance can be caused by using outdated legacy configuration.",
            "By default PSMoveServieEx should factory reset all configurations automatically when legacy configurations have been found. But a full factory reset and uninstalling legacy PSMoveService is recommended.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("Legacy PSMoveService config detected")) Then
                mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))

                Exit For
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckConfigReset() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_CONFIG_RESET,
            "Due to version mismatch some configurations have been factory reset and some devices have to be configured again.",
            "",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("Config version") AndAlso sLine.Contains("does not match expected version")) Then
                mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))

                Exit For
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckBluetoothAdapterFail() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BLUETOOTH_FAIL,
            "PSMoveServiceEx is unable to retrieve any bluetooth adapter information.",
            "Ensure your computer has an active Bluetooth radio connection.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("Failed to retrieve radio info")) Then
                mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))

                Exit For
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckBluetoothAdapterAssignFail() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BLUETOOTH_ADDRESS_FAIL,
            "PSMoveServiceEx failed to asign the host address to the controller id {0}.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("Failed to set host address")) Then
                Dim mMatch As Match = Regex.Match(sLine, "on controller id (?<ID>(\d+))", RegexOptions.IgnoreCase)

                If (mMatch.Success AndAlso mMatch.Groups("ID").Success) Then
                    Dim iControllerID As Integer = CInt(mMatch.Groups("ID").Value)

                    Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                    mNewIssue.sDescription = String.Format(mTemplate.sDescription, iControllerID)

                    mIssues.Add(mNewIssue)
                End If
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckDeviceOpenFail() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_DEVICE_FAIL,
            "PSMoveServiceEx failed to open device {0} ({1}).",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("Device device_id") AndAlso sLine.Contains("failed to open!")) Then
                Dim mMatch As Match = Regex.Match(sLine, "Device device_id (?<DeviceID>(\d+)) \(?<Path>(.*?)\) failed to open!", RegexOptions.IgnoreCase)

                If (mMatch.Success AndAlso mMatch.Groups("DeviceID").Success AndAlso mMatch.Groups("Path").Success) Then
                    Dim iDeviceID As Integer = CInt(mMatch.Groups("DeviceID").Value)
                    Dim sPath As String = CStr(mMatch.Groups("Path").Value)

                    Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                    mNewIssue.sDescription = String.Format(mTemplate.sDescription, iDeviceID, sPath)

                    mIssues.Add(mNewIssue)
                End If
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckDeviceLimit() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_DEVICE_SLOT_MAX,
            "PSMoveServiceEx could not open any more devices due to the device limit being hit.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("Can't connect any more new devices. Too many open device")) Then
                mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))

                Exit For
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckMorpheusFail() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_PSVR_FAIL,
            "Due to an error, PSMoveServiceEx could not open the PlayStation VR Head-mounted Display (Morpheus).",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mDiabledTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_PSVR_FAIL,
            "PSMoveServiceEx could not open the PlayStation VR Head-mounted Display (Morpheus) because it has been disabled.",
            "Enable the PlayStation VR Head-mounted Display (Morpheus) in 'PSMoveServiceEx Config Tool > Advanced Settings > Head-Mounted Display Settings'.",
            ENUM_LOG_ISSUE_TYPE.INFO
        )

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If


            If (sLine.Contains("Failed to open MorpheusHMD")) Then
                If (sLine.Contains("MorpheusHMD is disabled")) Then
                    mIssues.Add(New STRUC_LOG_ISSUE(mDiabledTemplate))
                Else
                    mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))
                End If

                Exit For
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckBluetoothPairingNotFound() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mDeviceList As New List(Of String)

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_NO_BLUETOOTH,
            "PSMoveServiceEx failed to locate the target Bluetooth device ({0}) for pairing.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("- No Bluetooth device found matching the given address")) Then
                Dim mMatch As Match = Regex.Match(sLine, "No Bluetooth device found matching the given address\: (?<Address>(..\:..\:..\:..\:..\:..))", RegexOptions.IgnoreCase)

                If (mMatch.Success AndAlso mMatch.Groups("Address").Success) Then
                    Dim sDeviceAddress As String = mMatch.Groups("Address").Value.Trim.ToUpperInvariant

                    If (Not mDeviceList.Contains(sDeviceAddress)) Then
                        mDeviceList.Add(sDeviceAddress)
                    End If
                End If
            End If

            ' Remove found devices.
            If (sLine.Contains("- Bluetooth device found matching the given address")) Then
                Dim mMatch As Match = Regex.Match(sLine, "Bluetooth device found matching the given address\: (?<Address>(..\:..\:..\:..\:..\:..))", RegexOptions.IgnoreCase)

                If (mMatch.Success AndAlso mMatch.Groups("Address").Success) Then
                    Dim sDeviceAddress As String = mMatch.Groups("Address").Value.Trim.ToUpperInvariant

                    mDeviceList.Remove(sDeviceAddress)
                End If
            End If
        Next

        For Each sDevice In mDeviceList
            Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
            mNewIssue.sDescription = String.Format(mTemplate.sDescription, sDevice)

            mIssues.Add(mNewIssue)
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckBluetoothPairingFail() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BLUETOOTH_PAIRING,
            "PSMoveServiceEx experienced multiple Bluetooth pairing failures with device '{0}' with the following reason: {1}",
            "See logs for more details.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mTemplateNotFound As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BLUETOOTH_NOT_FOUND,
            "PSMoveServiceEx failed to detect Bluetooth device '{0}'.",
            "Ensure Bluetooth is enabled in Windows settings and that your Bluetooth device is in pairing mode.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim sTotalFailures As String() = {
                "Failed to set 'VirtuallyCabled'",
                "Failed to build registry subkey",
                "Failed to create registry key",
                "Failed to enumerate installed services",
                "Failed to count installed services",
                "Failed to enumerate attached bluetooth devices",
                "Failed to close bluetooth device enumeration handle",
                "Failed to read device info",
                "Bluetooth device matching the given address is not an expected controller type",
                "BluetoothRegisterForAuthentication failed given address",
                "Failed to authenticate",
                "Invalid parameter",
                "User canceled the authentication",
                "Failed to reset event",
                "Failed to set event",
                "BluetoothSendAuthenticationResponseEx failed",
                "Not authenticated",
                "Device not ready",
                "Failure during authentication",
                "Bluetooth device denied passkey response",
                "Failed to enable incoming connections on radio",
                "Failed to enable radio",
                "Failed to enable HID service"
            }

        Const PAIR_IDLE = 0
        Const PAIR_PROGRESS = 1
        Const PAIR_DONE = 2

        Dim bPairCanceled As Boolean = False
        Dim bFoundDevice As Boolean = False
        Dim bDeviceFailure As Boolean = False
        Dim sDeviceSerial As String = ""
        Dim mFailureReason As New List(Of String)
        Dim mIssuesPerDevice As New Dictionary(Of String, List(Of STRUC_LOG_ISSUE))

        Dim iPairState As Integer = PAIR_IDLE

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            ' We hit the end of the logs
            If (i = sLines.Length - 1) Then
                If (iPairState <> PAIR_IDLE) Then
                    iPairState = PAIR_DONE
                    bDeviceFailure = True
                End If
            End If

            Select Case (iPairState)
                Case PAIR_IDLE
                    If (Not sLine.StartsWith("[")) Then
                        Continue For
                    End If

                    If (sLine.Contains("Async bluetooth request([Pair]") AndAlso sLine.EndsWith("started.")) Then
                        iPairState = PAIR_PROGRESS

                        bPairCanceled = False
                        bFoundDevice = False
                        bDeviceFailure = False
                        sDeviceSerial = ""
                        mFailureReason.Clear()
                    End If

                Case PAIR_PROGRESS
                    If (Not sLine.StartsWith("[")) Then
                        Continue For
                    End If

                    For Each sFailure As String In sTotalFailures
                        If (sLine.Contains(sFailure)) Then
                            bDeviceFailure = True

                            Dim sReason As String = sLine.Remove(0, sLine.IndexOf(sFailure)).Trim
                            If (Not mFailureReason.Contains(sReason)) Then
                                mFailureReason.Add(sReason)
                            End If

                            Exit For
                        End If
                    Next

                    If (sLine.Contains("- Bluetooth device found matching the given address")) Then
                        bFoundDevice = True
                        sDeviceSerial = ""

                        Dim sMatch As String = "matching the given address:"
                        If (sLine.IndexOf(sMatch) > -1) Then
                            sDeviceSerial = sLine.Remove(0, sLine.IndexOf(sMatch) + sMatch.Length).Trim
                        End If
                    End If

                    If (sLine.Contains("- No Bluetooth device found matching the given address")) Then
                        bFoundDevice = False
                        sDeviceSerial = ""

                        Dim sMatch As String = "matching the given address:"
                        If (sLine.IndexOf(sMatch) > -1) Then
                            sDeviceSerial = sLine.Remove(0, sLine.IndexOf(sMatch) + sMatch.Length).Trim
                        End If
                    End If

                    If (sLine.Contains("Async bluetooth request([Pair]") AndAlso sLine.EndsWith("Canceled.")) Then
                        iPairState = PAIR_DONE
                        bPairCanceled = True
                    End If

                    If (sLine.Contains("Async bluetooth request([Pair]") AndAlso sLine.EndsWith("failed!")) Then
                        iPairState = PAIR_DONE
                        bDeviceFailure = True
                    End If

                    If (sLine.Contains("Async bluetooth request([Pair]") AndAlso sLine.EndsWith("completed.")) Then
                        iPairState = PAIR_DONE
                        bDeviceFailure = False
                        bPairCanceled = False
                    End If

                Case PAIR_DONE
                    iPairState = PAIR_IDLE

                    If (Not String.IsNullOrEmpty(sDeviceSerial) AndAlso sDeviceSerial.Trim.Length > 0) Then
                        If (Not mIssuesPerDevice.ContainsKey(sDeviceSerial)) Then
                            mIssuesPerDevice(sDeviceSerial) = New List(Of STRUC_LOG_ISSUE)
                        End If

                        If (bDeviceFailure) Then
                            If (mFailureReason.Count > 0) Then
                                For Each sReason In mFailureReason
                                    Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                                    mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceSerial, sReason)
                                    mIssuesPerDevice(sDeviceSerial).Add(mNewIssue)
                                Next
                            Else
                                Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceSerial, "Unknown")
                                mIssuesPerDevice(sDeviceSerial).Add(mNewIssue)
                            End If
                        Else
                            ' Good pair, clear issues.
                            mIssuesPerDevice(sDeviceSerial).Clear()
                        End If

                        If (Not bFoundDevice) Then
                            Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplateNotFound)
                            mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceSerial)
                            mIssuesPerDevice(sDeviceSerial).Add(mNewIssue)
                        End If
                    End If

            End Select
        Next

        For Each sSerial In mIssuesPerDevice.Keys
            mIssues.AddRange(mIssuesPerDevice(sSerial).ToArray)
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckDeviceTimeout() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_DEVICE_TIMEOUT,
            "PSMoveServiceEx closed {0} id {1} due to timeout (no data received), typically caused by connection issues.",
            "Check your connection to the device. If the device is connected via Bluetooth, ensure you are within range and that you haven't connected too many devices simultaneously.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Const CONST_SEARCH_CLOSED_DEVICE = 0
        Const CONST_SEARCH_CLOSED_DEVICE_TYPE = 1

        Dim iState As Integer = 0
        Dim iStateDeviceId As Integer = -1

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            Select Case (iState)
                Case CONST_SEARCH_CLOSED_DEVICE
                    iStateDeviceId = -1

                    If (sLine.Contains("Device id") AndAlso sLine.Contains("closing due to no data")) Then
                        Dim mMatch As Match = Regex.Match(sLine, "Device id (?<ID>\d+) closing due to no data", RegexOptions.IgnoreCase)

                        If (mMatch.Success AndAlso mMatch.Groups("ID").Success) Then
                            Dim iDeviceId As Integer = CInt(mMatch.Groups("ID").Value)

                            iState = CONST_SEARCH_CLOSED_DEVICE_TYPE
                            iStateDeviceId = iDeviceId
                        End If
                    End If

                Case CONST_SEARCH_CLOSED_DEVICE_TYPE
                    If (sLine.Contains("Closing")) Then
                        Dim mMatch As Match = Regex.Match(sLine, "close \- Closing \b(?<Type>[a-zA-Z0-9_]+)\b", RegexOptions.IgnoreCase)
                        If (mMatch.Success AndAlso mMatch.Groups("Type").Success) Then
                            Dim sDeviceType As String = mMatch.Groups("Type").Value

                            iState = CONST_SEARCH_CLOSED_DEVICE

                            Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                            mNewIssue.sDescription = String.Format(mTemplate.sDescription, sDeviceType, iStateDeviceId)
                            mIssues.Add(mNewIssue)
                        End If
                    End If
            End Select
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckIncomplete() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_SERVICE_LOG_INCOMPLETE,
            "The PSMoveServiceEx log is incomplete, resulting in missing diagnostic details due to insufficient log data.",
            "Allow PSMoveServiceEx to complete initialization.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim iState As Integer = 0
        Dim bSuccess As Boolean = False

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (Not sLine.Contains("PSMoveServiceEx")) Then
                Continue For
            End If

            Select Case (iState)
                Case 0
                    If (sLine.Contains("Starting PSMoveServiceEx")) Then
                        iState = 1
                    End If

                Case 1
                    If (sLine.Contains("Startup successful!")) Then
                        iState = 2
                    End If

                Case 2
                    If (sLine.Contains("Successfully Initialized!")) Then
                        iState = 3
                        bSuccess = True
                    End If
            End Select
        Next

        If (Not bSuccess) Then
            mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))
        End If

        Return mIssues.ToArray
    End Function

    Public Function CheckBadDeviations() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_DEVICE_BAD_TRACKING,
            "{0} id {1} lost tracking on Tracker id {2} due to excessive deviations ({3} instances). Common causes: color noise/collisions, improper calibration, or tracker movement after pose calibration.",
            "Redo the pose calibration and check for color noise or collisions in the color calibration using the PSMoveServiceEx Config Tool.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mWarnTemplate As New STRUC_LOG_ISSUE(
            mTemplate.sMessage,
            mTemplate.sDescription,
            mTemplate.sSolution,
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mBadDeviation As New Dictionary(Of String, Dictionary(Of String, Object))

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("deviated too much from other trackers and stopped tracking")) Then
                Dim mMatch As Match = Regex.Match(sLine, "\b(?<DeviceType>Controller|Hmd)\b id (?<DeviceId>\d+) and tracker id (?<TrackerId>\d+) deviated too much from other trackers and stopped tracking", RegexOptions.IgnoreCase)
                If (mMatch.Success) Then
                    Dim sDeviceType As String = mMatch.Groups("DeviceType").Value
                    Dim iDeviceId As Integer = CInt(mMatch.Groups("DeviceId").Value)
                    Dim iTrackerId As Integer = CInt(mMatch.Groups("TrackerId").Value)

                    Dim sKey As String = String.Format("{0}\{1}\{2}", sDeviceType, iDeviceId, iTrackerId)
                    If (Not mBadDeviation.ContainsKey(sKey)) Then
                        Dim mDevice As New Dictionary(Of String, Object)

                        ' Give HMDs a proper long name
                        If (sDeviceType = "Hmd") Then
                            sDeviceType = "Head-mounted Display"
                        End If

                        mDevice("device_type") = sDeviceType
                        mDevice("device_id") = iDeviceId
                        mDevice("tracker_id") = iTrackerId
                        mDevice("bad_count") = 0
                        mDevice("total_bad_count") = 0

                        mBadDeviation(sKey) = mDevice
                    End If

                    mBadDeviation(sKey)("bad_count") = CInt(mBadDeviation(sKey)("bad_count")) + 1
                    mBadDeviation(sKey)("total_bad_count") = CInt(mBadDeviation(sKey)("total_bad_count")) + 1
                End If
            End If

            If (sLine.Contains("tracking restored!")) Then
                Dim mMatch As Match = Regex.Match(sLine, "\b(?<DeviceType>Controller|Hmd)\b id (?<DeviceId>\d+) and tracker id (?<TrackerId>\d+) tracking restored", RegexOptions.IgnoreCase)
                If (mMatch.Success) Then
                    Dim sDeviceType As String = mMatch.Groups("DeviceType").Value
                    Dim iDeviceId As Integer = CInt(mMatch.Groups("DeviceId").Value)
                    Dim iTrackerId As Integer = CInt(mMatch.Groups("TrackerId").Value)

                    Dim sKey As String = String.Format("{0}\{1}\{2}", sDeviceType, iDeviceId, iTrackerId)
                    If (Not mBadDeviation.ContainsKey(sKey)) Then
                        Dim mDevice As New Dictionary(Of String, Object)

                        ' Give HMDs a proper long name
                        If (sDeviceType = "Hmd") Then
                            sDeviceType = "Head-mounted Display"
                        End If

                        mDevice("device_type") = sDeviceType
                        mDevice("device_id") = iDeviceId
                        mDevice("tracker_id") = iTrackerId
                        mDevice("bad_count") = 0
                        mDevice("total_bad_count") = 0

                        mBadDeviation(sKey) = mDevice
                    End If

                    mBadDeviation(sKey)("bad_count") = CInt(mBadDeviation(sKey)("bad_count")) - 1
                End If
            End If
        Next

        For Each sKey As String In mBadDeviation.Keys
            Dim mDevice = mBadDeviation(sKey)

            Dim sDeviceType As String = CStr(mDevice("device_type"))
            Dim iDeviceId As Integer = CInt(mDevice("device_id"))
            Dim iTrackerId As Integer = CInt(mDevice("tracker_id"))
            Dim iBadCount As Integer = CInt(mDevice("bad_count"))
            Dim iTotalBadCount As Integer = CInt(mDevice("total_bad_count"))

            If (iTotalBadCount > 10) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, iDeviceId, iTrackerId, iTotalBadCount)
                mIssues.Add(mNewIssue)

            ElseIf (iTotalBadCount > 0) Then
                Dim mNewIssue As New STRUC_LOG_ISSUE(mWarnTemplate)
                mNewIssue.sDescription = String.Format(mNewIssue.sDescription, sDeviceType, iDeviceId, iTrackerId, iTotalBadCount)
                mIssues.Add(mNewIssue)
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function FindConfigFromSerial(sSerial As String) As ClassServiceConfig
        Dim mConfigs = GetConfigs()

        ' $TODO Config files have different naming schemes. Maybe device index to json instead? This is fucking aids.
        ' This might not even work correctly with PSNavis...

        ' PSEyes
        If (sSerial.ToUpperInvariant.Contains(String.Format("VID_{0}&PID_{1}", ClassLibusbDriver.DRV_PSEYE_KNOWN_CONFIGS(0).VID, ClassLibusbDriver.DRV_PSEYE_KNOWN_CONFIGS(0).PID).ToUpperInvariant)) Then
            Dim sPathSplit As String() = sSerial.Split("\"c)
            If (sPathSplit.Length > 0) Then
                Dim sPathEnd As String = String.Format("ps3eye_{0}", sPathSplit(sPathSplit.Length - 1))

                For Each mConfig In mConfigs
                    If (mConfig.Key.EndsWith(sPathEnd)) Then
                        Return mConfig.Value
                    End If
                Next
            End If
        End If

        ' Virtual Trackers
        If (sSerial.StartsWith("VirtualTracker")) Then
            Dim sPathEnd As String = sSerial.Replace("VirtualTracker_", "virtual_")

            For Each mConfig In mConfigs
                If (mConfig.Key.EndsWith(sPathEnd)) Then
                    Return mConfig.Value
                End If
            Next
        End If

        ' Morpheus
        If (sSerial.StartsWith("MorpheusHMD") OrElse
                sSerial.ToUpperInvariant.Contains(String.Format("VID_{0}&PID_{1}", ClassLibusbDriver.DRV_PSVR_KNOWN_CONFIGS(0).VID, ClassLibusbDriver.DRV_PSVR_KNOWN_CONFIGS(0).PID).ToUpperInvariant)) Then
            If (mConfigs.ContainsKey("MorpheusHMDConfig")) Then
                Return mConfigs("MorpheusHMDConfig")
            End If
        End If

        ' Bluetooth address (dualshock, psmove, psnavi)
        If (sSerial.Split(":"c).Count = 6 OrElse sSerial.Split("_"c).Count = 6) Then
            Dim sPathEnd As String = sSerial.Replace(":"c, "_"c).ToLowerInvariant

            For Each mConfig In mConfigs
                If (mConfig.Key.EndsWith(sPathEnd)) Then
                    Return mConfig.Value
                End If
            Next
        End If

        ' Try anything else, if it works
        For Each mConfig In mConfigs
            If (mConfig.Key.EndsWith(sSerial)) Then
                Return mConfig.Value
            End If
        Next

        Return Nothing
    End Function

    Public Function GetConfigs() As Dictionary(Of String, ClassServiceConfig)
        Dim mConfigs As New Dictionary(Of String, ClassServiceConfig)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mConfigs
        End If

        Dim mConfigLines As New List(Of String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("["c) AndAlso sLine.EndsWith(".json - {")) Then
                Try
                    mConfigLines.Add(sLine.Remove(0, sLine.LastIndexOf("{"c)))

                    mConfigLines.Reverse()
                    Dim sConfigContent As String = String.Join(Environment.NewLine, mConfigLines.ToArray)

                    Dim sSplitPath As String() = sLine.Split("\"c)
                    If (sSplitPath.Length > 0) Then
                        Dim sFileName As String = sSplitPath(sSplitPath.Length - 1)
                        sFileName = sFileName.Substring(0, sFileName.LastIndexOf(".json")) & ".json"

                        Dim sFileNameNoExt As String = IO.Path.GetFileNameWithoutExtension(sFileName)

                        If (Not mConfigs.ContainsKey(sFileNameNoExt)) Then
                            ' Attempt to load parsed config
                            Dim mParsedConfig As New ClassServiceConfig()
                            mParsedConfig.LoadFromString(sConfigContent)

                            mConfigs(sFileNameNoExt) = mParsedConfig
                        End If
                    End If
                Catch ex As Exception
                    'Ignore any issues.
                End Try

                mConfigLines.Clear()
            End If

            mConfigLines.Add(sLine)

            If (sLine.StartsWith("["c)) Then
                mConfigLines.Clear()
            End If
        Next

        Return mConfigs
    End Function
End Class
