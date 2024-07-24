Imports System.Text.RegularExpressions
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogService
    Implements ILogAction

    Public Sub Generate(mData As Dictionary(Of String, String)) Implements ILogAction.Generate
        Dim mConfig As New ClassServiceInfo
        mConfig.LoadConfig()

        If (Not mConfig.FileExist()) Then
            If (Not mConfig.FindByProcess()) Then
                Throw New ArgumentException("PSMoveServiceEx not found")
            End If
        End If

        If (Not mConfig.FileExist) Then
            Return
        End If

        Dim sServceDirectory As String = IO.Path.GetDirectoryName(mConfig.m_FileName)
        Dim sLogFile As String = IO.Path.Combine(sServceDirectory, "PSMoveServiceEx.log")

        If (Not IO.File.Exists(sLogFile)) Then
            Return
        End If

        Dim sTmp As String = IO.Path.GetTempFileName
        IO.File.Copy(sLogFile, sTmp, True)

        mData(GetActionTitle()) = IO.File.ReadAllText(sTmp, System.Text.Encoding.Default)
        IO.File.Delete(sTmp)
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_PSMOVESERVICEEX
    End Function

    Public Function GetIssues(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckServiceVersion(mData))
        mIssues.AddRange(CheckServiceFps(mData))
        mIssues.AddRange(CheckServiceLegacy(mData))
        mIssues.AddRange(CheckServiceConfigReset(mData))
        mIssues.AddRange(CheckServiceRadioFail(mData))
        mIssues.AddRange(CheckServiceRadioAssignFail(mData))
        mIssues.AddRange(CheckServiceDeviceOpenFail(mData))
        mIssues.AddRange(CheckServiceDeviceLimit(mData))
        mIssues.AddRange(CheckServiceMorpheusFail(mData))
        mIssues.AddRange(CheckServicePairingNotFound(mData))
        mIssues.AddRange(CheckServicePairingFail(mData))
        mIssues.AddRange(CheckServiceDeviceTimeout(mData))
        mIssues.AddRange(CheckServiceEmpty(mData))
        mIssues.AddRange(CheckServiceIncomplete(mData))
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent(mData As Dictionary(Of String, String)) As String Implements ILogAction.GetSectionContent
        If (Not mData.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return mData(GetActionTitle())
    End Function

    Public Function CheckServiceVersion(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Outdated PSMoveServiceEx Version",
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
                Dim mMatch As Match = Regex.Match(sLine, "Starting PSMoveServiceEx v(?<Version>0.27.0.0)", RegexOptions.IgnoreCase)

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

    Public Function CheckServiceFps(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "PSMoveServiceEx framerate too low",
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

    Public Function CheckServiceLegacy(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Legacy PSMoveService configuration",
            "Legacy PSMoveService configuration has been found. Using legacy configuration can cause abnormal tracking side effects and bad performance.",
            "By default PSMoveServieEx should factory reset all configurations automatically when a legacy configuration has been found. But a full factory reset and uninstalling legacy PSMoveService is recommended.",
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

    Public Function CheckServiceConfigReset(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Some configurations have been factory reset",
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

    Public Function CheckServiceRadioFail(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Failed to retrieve bluetooth adapter information",
            "PSMoveServiceEx is unable to retrieve any bluetooth adapter information.",
            "",
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

    Public Function CheckServiceRadioAssignFail(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Failed to set host address",
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

    Public Function CheckServiceDeviceOpenFail(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Failed to open device",
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

    Public Function CheckServiceDeviceLimit(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Device limit reached",
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

    Public Function CheckServiceMorpheusFail(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Failed to open PlayStation VR Head-mounted Display",
            "PSMoveServiceEx could not open the PlayStation VR Head-mounted Display (Morpheus) device.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mDiabledTemplate As New STRUC_LOG_ISSUE(
            "Failed to open PlayStation VR",
            "PSMoveServiceEx could not open the PlayStation VR Head-mounted Display device (Morpheus) because it has been disabled.",
            "",
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
                    mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))
                Else
                    mIssues.Add(New STRUC_LOG_ISSUE(mDiabledTemplate))
                End If

                Exit For
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckServicePairingNotFound(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mDeviceList As New List(Of String)

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Failed to find bluetooth device",
            "PSMoveServiceEx could not find the target bluetooth device ({0}) for pairing.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("No Bluetooth device found matching the given address")) Then
                Dim mMatch As Match = Regex.Match(sLine, "No Bluetooth device found matching the given address\: (?<Address>(..\:..\:..\:..\:..\:..))", RegexOptions.IgnoreCase)

                If (mMatch.Success AndAlso mMatch.Groups("Address").Success) Then
                    Dim sDeviceAddress As String = mMatch.Groups("Address").Value.Trim.ToUpperInvariant

                    If (Not mDeviceList.Contains(sDeviceAddress)) Then
                        Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                        mNewIssue.sDescription = String.Format(mTemplate.sDescription, sDeviceAddress)

                        mIssues.Add(mNewIssue)

                        mDeviceList.Add(sDeviceAddress)
                    End If
                End If
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckServicePairingFail(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Multiple bluetooth pairing issues",
            "PSMoveServiceEx encountered multiple bluetooth pairing issues. See logs for more details.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim sTotalFailures As String() = {
                "No Bluetooth device found matching the given address",
                "Failed to get registry value. Error Code",
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
                "Failed to enable HID service. Error code:"
            }

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (Not sLine.Contains("Bluetooth")) Then
                Continue For
            End If

            Dim bFailed As Boolean = False
            For Each sFailure As String In sTotalFailures
                If (sLine.Contains(sFailure)) Then
                    bFailed = True
                    Exit For
                End If
            Next


            If (bFailed) Then
                mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))

                Exit For
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckServiceDeviceTimeout(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTimedoutDevices As New List(Of Integer)

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Device timed out",
            "PSMoveServiceEx had to close device {0} that timed out. This happens when PSMoveServiceEx does not receive any data from the device for example due to connection issues.",
            "Check your connection to the device. If the device is connected via bluetooth, make sure you didnt connected too mandy devices and are in range of the bluetooth adapter.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            If (Not sLine.StartsWith("[")) Then
                Continue For
            End If

            If (sLine.Contains("Device id") AndAlso sLine.Contains("closing due to no data")) Then
                Dim mMatch As Match = Regex.Match(sLine, "Device id (?<ID>\d+) closing due to no data", RegexOptions.IgnoreCase)

                If (mMatch.Success AndAlso mMatch.Groups("ID").Success) Then
                    Dim iDeviceId As Integer = CInt(mMatch.Groups("ID").Value)

                    If (Not mTimedoutDevices.Contains(iDeviceId)) Then
                        Dim mNewIssue As New STRUC_LOG_ISSUE(mTemplate)
                        mNewIssue.sDescription = String.Format(mTemplate.sDescription, iDeviceId)

                        mIssues.Add(mNewIssue)

                        mTimedoutDevices.Add(iDeviceId)
                    End If
                End If
            End If
        Next

        Return mIssues.ToArray
    End Function

    Public Function CheckServiceEmpty(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "PSMoveServiceEx Log Unavailable",
            "Some diagnostic details are unavailable due to missing log information.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        If (sContent Is Nothing OrElse sContent.Trim.Length = 0) Then
            mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))
        End If

        Return mIssues.ToArray
    End Function

    Public Function CheckServiceIncomplete(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "PSMoveServiceEx log incomplete",
            "The PSMoveServiceEx log is incompelte and has missing logging details. Some diagnostic details are unavailable due to missing log information.",
            "Let PSMoveServiceEx finish initalizing.",
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

End Class
