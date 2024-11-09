Imports PSMSVirtualDeviceManager.ClassLogDiagnostics

Public Class ClassLogDxdiag
    Implements ILogAction

    Public Shared ReadOnly SECTION_DXDIAG As String = "DirectX Diagnostics"

    Public Shared ReadOnly LOG_ISSUE_EMPTY As String = "Log is unavailable"
    Public Shared ReadOnly LOG_ISSUE_BLUETOOTH_BANDWIDTH_DEVICES As String = "Possible Bluetooth bandwidth issues"
    Public Shared ReadOnly LOG_ISSUE_NOT_ENOUGH_USB_HOST_CONTROLLERS As String = "Not enough USB 3.0 Host Controllers"
    Public Shared ReadOnly LOG_ISSUE_USB_HOST_CONTROLLER As String = "USB 3.0 Host Controller"

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
        ' Prompting a UAC promot isnt silent
        If (bSilent) Then
            Return
        End If

        Dim sDxDiagExecutable As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "dxdiag.exe")
        Dim sOutputFile As String = IO.Path.Combine(IO.Path.GetTempPath(), IO.Path.GetRandomFileName)

        If (Not IO.File.Exists(sDxDiagExecutable)) Then
            Throw New ArgumentException("DirectX diagnostics executable could not be found")
        End If

        Using mProcess As New Process
            mProcess.StartInfo.FileName = sDxDiagExecutable
            mProcess.StartInfo.Arguments = String.Format("/whql:off /t ""{0}""", sOutputFile)
            mProcess.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(sDxDiagExecutable)
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True
            mProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()
            mProcess.WaitForExit()
        End Using

        If (Not IO.File.Exists(sOutputFile)) Then
            Throw New ArgumentException("DirectX diagnostics output log could not be found")
        End If

        ' DxDiag output is UTF-7?
        Dim sContent As String = IO.File.ReadAllText(sOutputFile, System.Text.Encoding.UTF7)
        If (String.IsNullOrEmpty(sContent) OrElse sContent.Trim.Length = 0) Then
            Throw New ArgumentException("DirectX diagnostics output log is empty")
        End If

        g_ClassLogContent.m_Content(GetActionTitle()) = "[System]" & Environment.NewLine & sContent
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_DXDIAG
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckEmpty())
        mIssues.AddRange(CheckMultipleBluetoothDevices())
        mIssues.AddRange(CheckMultipleUsbControllers())
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle)) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle)
    End Function

    Public Function CheckEmpty() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_EMPTY,
            "Some diagnostic details are unavailable due to missing log information.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        If (sContent Is Nothing OrElse sContent.Trim.Length = 0) Then
            mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))
        End If

        Return mIssues.ToArray
    End Function

    Private Function CheckMultipleBluetoothDevices() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BLUETOOTH_BANDWIDTH_DEVICES,
            "Having multiple bluetooth devices connected, such as '{0}', could cause bandwidth issues if you want to use bluetooth devices such as PlayStation Move controllers.",
            "If you encounter issues with connected controllers, reduce the amount of devices connected to your bluetooth adapter.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim sLastDevice As String = ""
        Dim sLastHardwareID As String = ""

        Dim sLines As String() = FindSection("Sound Devices", sContent)
        If (sLines Is Nothing OrElse sLines.Length < 1) Then
            Return mIssues.ToArray
        End If

        For i = sLines.Length - 1 To 0 Step -1
            If (sLines(i).TrimStart.StartsWith("Description:")) Then
                sLastDevice = sLines(i).TrimStart.Remove(0, "Description:".Length).Trim

                If (sLastHardwareID.ToUpperInvariant.Contains("BTHENUM\".ToUpperInvariant)) Then
                    Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                    mIssue.sDescription = String.Format(mTemplate.sDescription, sLastDevice)

                    mIssues.Add(mIssue)
                End If

                sLastDevice = ""
                sLastHardwareID = ""
            End If

            If (sLines(i).TrimStart.StartsWith("Hardware ID:")) Then
                sLastHardwareID &= sLines(i).TrimStart.Remove(0, "Hardware ID:".Length) & Environment.NewLine
            End If
        Next

        Return mIssues.ToArray
    End Function

    Private Function CheckMultipleUsbControllers() As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_NOT_ENOUGH_USB_HOST_CONTROLLERS,
            "This computer does not have enough USB host controllers which limits the amount of devices you can connect to your computer simultaneously.",
            "Switch the computer mainboard that has more USB host controllers or get a PCI-Express USB card and install it in your computer.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim mHostTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_USB_HOST_CONTROLLER,
            "Found USB 3.0 Host Controller '{0}'.",
            "",
            ENUM_LOG_ISSUE_TYPE.INFO
        )

        Dim iUsbHostCount As Integer = 0

        Dim sLastDevice As String = ""
        Dim sLastDriver As String = ""

        Dim mHostControllers As New List(Of String)

        Dim sLines As String() = FindSection("System Devices", sContent)
        If (sLines Is Nothing OrElse sLines.Length < 1) Then
            Return mIssues.ToArray
        End If

        For i = sLines.Length - 1 To 0 Step -1
            If (sLines(i).TrimStart.StartsWith("Name:")) Then
                sLastDevice = sLines(i).TrimStart.Remove(0, "Name:".Length).Trim

                If (sLastDriver.ToUpperInvariant.Contains("USBXHCI.SYS".ToUpperInvariant)) Then
                    iUsbHostCount += 1

                    mHostControllers.Add(sLastDevice)
                End If

                sLastDevice = ""
                sLastDriver = ""
            End If

            If (sLines(i).TrimStart.StartsWith("Driver:")) Then
                sLastDriver &= sLines(i).TrimStart.Remove(0, "Driver:".Length) & Environment.NewLine
            End If
        Next

        For Each sHostController In mHostControllers
            Dim mIssue As New STRUC_LOG_ISSUE(mHostTemplate)
            mIssue.sDescription = String.Format(mHostTemplate.sDescription, sHostController)

            mIssues.Add(mIssue)
        Next

        If (iUsbHostCount < 2) Then
            mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))
        End If

        Return mIssues.ToArray
    End Function

    Public Function FindSection(sSection As String, ByRef sContent As String) As String()
        If (sContent Is Nothing) Then
            Return Nothing
        End If

        Dim sSectionContent As New List(Of String)

        Dim iLineState As Integer = 0
        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = 0 To sLines.Length - 1
            Dim sLine As String = sLines(i)

            Select Case (iLineState)
                Case 0
                    If (sLine.StartsWith("-"c)) Then
                        iLineState = 1
                    Else
                        iLineState = 0
                    End If
                Case 1
                    If (sLine.StartsWith(sSection)) Then
                        iLineState = 2
                    Else
                        iLineState = 0
                    End If
                Case 2
                    ' Section started
                    If (sLine.StartsWith("-"c)) Then
                        iLineState = 3
                    Else
                        iLineState = 0
                    End If

                Case 3
                    ' Section ended
                    If (sLine.StartsWith("-"c)) Then
                        Exit For
                    Else
                        sSectionContent.Add(sLine)
                    End If
            End Select
        Next

        Return sSectionContent.ToArray
    End Function
End Class
