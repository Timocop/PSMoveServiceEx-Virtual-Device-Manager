Imports System.Text.RegularExpressions
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogService
    Implements ILogAction

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
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

        mData(GetActionTitle()) = IO.File.ReadAllText(sTmp)
        IO.File.Delete(sTmp)
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_PSMOVESERVICEEX
    End Function

    Public Function GetIssues(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mTracker As New ClassIssuesTracker(mData)
        Return mTracker.GetIssues
    End Function

    Class ClassIssuesTracker
        Implements IIssuesTracker

        Private g_mData As Dictionary(Of String, String)

        Public Sub New(_Data As Dictionary(Of String, String))
            g_mData = _Data
        End Sub

        Public Function GetSectionContent() As String Implements IIssuesTracker.GetSectionContent
            If (Not g_mData.ContainsKey(SECTION_PSMOVESERVICEEX)) Then
                Return Nothing
            End If

            Return g_mData(SECTION_PSMOVESERVICEEX)
        End Function

        Public Function GetIssues() As STRUC_LOG_ISSUE() Implements IIssuesTracker.GetIssues
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)
            mIssues.AddRange(CheckServiceVersion)
            mIssues.AddRange(CheckServiceFps)
            mIssues.AddRange(CheckServiceLegacy)
            mIssues.AddRange(CheckServiceConfigReset)
            mIssues.AddRange(CheckServiceRadioFail)
            mIssues.AddRange(CheckServiceRadioAssignFail)
            mIssues.AddRange(CheckServiceDeviceOpenFail)
            mIssues.AddRange(CheckServiceDeviceLimit)
            mIssues.AddRange(CheckServiceMorpheusFail)
            mIssues.AddRange(CheckServicePairingNotFound)
            mIssues.AddRange(CheckServicePairingFail)
            mIssues.AddRange(CheckServiceDeviceTimeout)
            Return mIssues.ToArray
        End Function

        Public Function CheckServiceVersion() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Outdated PSMoveServiceEx Version - v{0}"
            mTemplate.sDescription = "This PSMoveServiceEx version is outdated (Current: v{0} / Newest: v{1}) and could still have issues that already have been fixed or missing new features."
            mTemplate.sSolution = "Udpate PSMoveServiceEx to v{0}."
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.WARNING

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
                                Dim mNewIssue As New STRUC_LOG_ISSUE
                                mNewIssue.bValid = True
                                mNewIssue.sMessage = String.Format(mTemplate.sMessage, New Version(sCurrentVersion).ToString)
                                mNewIssue.sDescription = String.Format(mTemplate.sDescription, New Version(sCurrentVersion).ToString, New Version(sNextVersion).ToString)
                                mNewIssue.sSolution = String.Format(mTemplate.sSolution, New Version(sNextVersion).ToString)
                                mNewIssue.iType = mTemplate.iType

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

        Public Function CheckServiceFps() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "PSMoveServiceEx framerate too low"
            mTemplate.sDescription = "PSMoveServiceEx is running at a very low framerate {0} (minimum {1}) which can cause bad tracking quality."
            mTemplate.sSolution = "Upgrade your computers CPU."
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.ERROR

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
                            Dim mNewIssue As New STRUC_LOG_ISSUE
                            mNewIssue.bValid = True
                            mNewIssue.sMessage = mTemplate.sMessage
                            mNewIssue.sDescription = String.Format(mTemplate.sDescription, iFpsAvg, iFpsMinimum)
                            mNewIssue.sSolution = mTemplate.sSolution
                            mNewIssue.iType = mTemplate.iType

                            mIssues.Add(mNewIssue)
                        End If
                    End If

                    Exit For
                End If
            Next

            Return mIssues.ToArray
        End Function

        Public Function CheckServiceLegacy() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Legacy PSMoveService configuration"
            mTemplate.sDescription = "Legacy PSMoveService configuration has been found. Using legacy configuration can cause abnormal tracking side effects and bad performance."
            mTemplate.sSolution = "By default PSMoveServieEx should factory reset all configurations automatically when a legacy configuration has been found. But a full factory reset and uninstalling legacy PSMoveService is recommended."
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.WARNING

            Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
            For i = 0 To sLines.Length - 1
                Dim sLine As String = sLines(i)

                If (Not sLine.StartsWith("[")) Then
                    Continue For
                End If

                If (sLine.Contains("Legacy PSMoveService config detected")) Then
                    Dim mNewIssue As New STRUC_LOG_ISSUE
                    mNewIssue.bValid = True
                    mNewIssue.sMessage = mTemplate.sMessage
                    mNewIssue.sDescription = mTemplate.sDescription
                    mNewIssue.sSolution = mTemplate.sSolution
                    mNewIssue.iType = mTemplate.iType

                    mIssues.Add(mNewIssue)

                    Exit For
                End If
            Next

            Return mIssues.ToArray
        End Function

        Public Function CheckServiceConfigReset() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Some configration have been factory reset"
            mTemplate.sDescription = "Some configurations have been factory reset due to version mismatch and some devices have to be configured again."
            mTemplate.sSolution = ""
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.WARNING

            Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
            For i = 0 To sLines.Length - 1
                Dim sLine As String = sLines(i)

                If (Not sLine.StartsWith("[")) Then
                    Continue For
                End If

                If (sLine.Contains("Config version") AndAlso sLine.Contains("does not match expected version")) Then
                    Dim mNewIssue As New STRUC_LOG_ISSUE
                    mNewIssue.bValid = True
                    mNewIssue.sMessage = mTemplate.sMessage
                    mNewIssue.sDescription = mTemplate.sDescription
                    mNewIssue.sSolution = mTemplate.sSolution
                    mNewIssue.iType = mTemplate.iType

                    mIssues.Add(mNewIssue)

                    Exit For
                End If
            Next

            Return mIssues.ToArray
        End Function

        Public Function CheckServiceRadioFail() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Failed to retrieve bluetooth adapter information"
            mTemplate.sDescription = "PSMoveServiceEx is unable to retrieve any bluetooth adapter information."
            mTemplate.sSolution = ""
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.ERROR

            Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
            For i = 0 To sLines.Length - 1
                Dim sLine As String = sLines(i)

                If (Not sLine.StartsWith("[")) Then
                    Continue For
                End If

                If (sLine.Contains("Failed to retrieve radio info")) Then
                    Dim mNewIssue As New STRUC_LOG_ISSUE
                    mNewIssue.bValid = True
                    mNewIssue.sMessage = mTemplate.sMessage
                    mNewIssue.sDescription = mTemplate.sDescription
                    mNewIssue.sSolution = mTemplate.sSolution
                    mNewIssue.iType = mTemplate.iType

                    mIssues.Add(mNewIssue)

                    Exit For
                End If
            Next

            Return mIssues.ToArray
        End Function

        Public Function CheckServiceRadioAssignFail() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Failed to set host address"
            mTemplate.sDescription = "PSMoveServiceEx failed to asign the host address to the controller id {0}."
            mTemplate.sSolution = ""
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.ERROR

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

                        Dim mNewIssue As New STRUC_LOG_ISSUE
                        mNewIssue.bValid = True
                        mNewIssue.sMessage = mTemplate.sMessage
                        mNewIssue.sDescription = String.Format(mTemplate.sDescription, iControllerID)
                        mNewIssue.sSolution = mTemplate.sSolution
                        mNewIssue.iType = mTemplate.iType

                        mIssues.Add(mNewIssue)
                    End If
                End If
            Next

            Return mIssues.ToArray
        End Function

        Public Function CheckServiceDeviceOpenFail() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Failed to open device"
            mTemplate.sDescription = "PSMoveServiceEx failed to open device id {0} ({1})."
            mTemplate.sSolution = ""
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.ERROR

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

                        Dim mNewIssue As New STRUC_LOG_ISSUE
                        mNewIssue.bValid = True
                        mNewIssue.sMessage = mTemplate.sMessage
                        mNewIssue.sDescription = String.Format(mTemplate.sDescription, iDeviceID, sPath)
                        mNewIssue.sSolution = mTemplate.sSolution
                        mNewIssue.iType = mTemplate.iType

                        mIssues.Add(mNewIssue)
                    End If
                End If
            Next

            Return mIssues.ToArray
        End Function

        Public Function CheckServiceDeviceLimit() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Device limit reached"
            mTemplate.sDescription = "PSMoveServiceEx could not open any more devices due to the device limit being hit."
            mTemplate.sSolution = ""
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.ERROR

            Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
            For i = 0 To sLines.Length - 1
                Dim sLine As String = sLines(i)

                If (Not sLine.StartsWith("[")) Then
                    Continue For
                End If

                If (sLine.Contains("Can't connect any more new devices. Too many open device")) Then
                    Dim mNewIssue As New STRUC_LOG_ISSUE
                    mNewIssue.bValid = True
                    mNewIssue.sMessage = mTemplate.sMessage
                    mNewIssue.sDescription = mTemplate.sDescription
                    mNewIssue.sSolution = mTemplate.sSolution
                    mNewIssue.iType = mTemplate.iType

                    mIssues.Add(mNewIssue)

                    Exit For
                End If
            Next

            Return mIssues.ToArray
        End Function

        Public Function CheckServiceMorpheusFail() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Failed to open PlayStation VR Head-mounted Display"
            mTemplate.sDescription = "PSMoveServiceEx could not open the PlayStation VR Head-mounted Display (Morpheus) device."
            mTemplate.sSolution = ""
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.ERROR

            Dim mDisabledTemplate As New STRUC_LOG_ISSUE
            mDisabledTemplate.bValid = False
            mDisabledTemplate.sMessage = "Failed to open PlayStation VR"
            mDisabledTemplate.sDescription = "PSMoveServiceEx could not open the PlayStation VR Head-mounted Display device (Morpheus) because it has been disabled."
            mDisabledTemplate.sSolution = ""
            mDisabledTemplate.iType = ENUM_LOG_ISSUE_TYPE.INFO

            Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
            For i = 0 To sLines.Length - 1
                Dim sLine As String = sLines(i)

                If (Not sLine.StartsWith("[")) Then
                    Continue For
                End If


                If (sLine.Contains("Failed to open MorpheusHMD")) Then
                    If (sLine.Contains("MorpheusHMD is disabled")) Then
                        Dim mNewIssue As New STRUC_LOG_ISSUE
                        mNewIssue.bValid = True
                        mNewIssue.sMessage = mDisabledTemplate.sMessage
                        mNewIssue.sDescription = mDisabledTemplate.sDescription
                        mNewIssue.sSolution = mDisabledTemplate.sSolution
                        mNewIssue.iType = mDisabledTemplate.iType

                        mIssues.Add(mNewIssue)
                    Else
                        Dim mNewIssue As New STRUC_LOG_ISSUE
                        mNewIssue.bValid = True
                        mNewIssue.sMessage = mTemplate.sMessage
                        mNewIssue.sDescription = mTemplate.sDescription
                        mNewIssue.sSolution = mTemplate.sSolution
                        mNewIssue.iType = mTemplate.iType

                        mIssues.Add(mNewIssue)
                    End If

                    Exit For
                End If
            Next

            Return mIssues.ToArray
        End Function

        Public Function CheckServicePairingNotFound() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mDeviceList As New List(Of String)

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Failed to find bluetooth device"
            mTemplate.sDescription = "PSMoveServiceEx could not find the target bluetooth device for pairing ({0})."
            mTemplate.sSolution = ""
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.ERROR

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
                            Dim mNewIssue As New STRUC_LOG_ISSUE
                            mNewIssue.bValid = True
                            mNewIssue.sMessage = mTemplate.sMessage
                            mNewIssue.sDescription = String.Format(mTemplate.sDescription, sDeviceAddress)
                            mNewIssue.sSolution = mTemplate.sSolution
                            mNewIssue.iType = mTemplate.iType

                            mIssues.Add(mNewIssue)

                            mDeviceList.Add(sDeviceAddress)
                        End If
                    End If
                End If
            Next

            Return mIssues.ToArray
        End Function

        Public Function CheckServicePairingFail() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Multiple bluetooth pairing issues"
            mTemplate.sDescription = "PSMoveServiceEx encountered multiple bluetooth pairing issues. See logs for more details."
            mTemplate.sSolution = ""
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.ERROR

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
                    Dim mNewIssue As New STRUC_LOG_ISSUE
                    mNewIssue.bValid = True
                    mNewIssue.sMessage = mTemplate.sMessage
                    mNewIssue.sDescription = mTemplate.sDescription
                    mNewIssue.sSolution = mTemplate.sSolution
                    mNewIssue.iType = mTemplate.iType

                    mIssues.Add(mNewIssue)

                    Exit For
                End If
            Next

            Return mIssues.ToArray
        End Function

        Public Function CheckServiceDeviceTimeout() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTimedoutDevices As New List(Of Integer)

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Device '{0}' timed out"
            mTemplate.sDescription = "PSMoveServiceEx had to close a device (ID: {0}) that timed out. This happens when PSMoveServiceEx does not receive any data from the device for example due to connection issues."
            mTemplate.sSolution = "Check your connection to the device. If the device is connected via bluetooth, make sure you didnt connected too mandy devices and are in range of the bluetooth adapter."
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.ERROR

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
                            Dim mNewIssue As New STRUC_LOG_ISSUE
                            mNewIssue.bValid = True
                            mNewIssue.sMessage = String.Format(mTemplate.sMessage, iDeviceId)
                            mNewIssue.sDescription = String.Format(mTemplate.sDescription, iDeviceId)
                            mNewIssue.sSolution = mTemplate.sSolution
                            mNewIssue.iType = mTemplate.iType

                            mIssues.Add(mNewIssue)

                            mTimedoutDevices.Add(iDeviceId)
                        End If
                    End If
                End If
            Next

            Return mIssues.ToArray
        End Function
    End Class
End Class
