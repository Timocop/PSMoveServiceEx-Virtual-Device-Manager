Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogDxdiag
    Implements ILogAction

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
        Dim sRootFolder As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "dxdiag.exe")
        Dim sOutputFile As String = IO.Path.Combine(IO.Path.GetTempPath(), IO.Path.GetRandomFileName)

        If (Not IO.File.Exists(sRootFolder)) Then
            Return
        End If

        Try
            Using mProcess As New Process
                mProcess.StartInfo.FileName = sRootFolder
                mProcess.StartInfo.Arguments = String.Format("/whql:off /t ""{0}""", sOutputFile)
                mProcess.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(sRootFolder)
                mProcess.StartInfo.CreateNoWindow = True
                mProcess.StartInfo.UseShellExecute = True
                mProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

                If (Environment.OSVersion.Version.Major > 5) Then
                    mProcess.StartInfo.Verb = "runas"
                End If

                mProcess.Start()
                mProcess.WaitForExit()
            End Using
        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            ' Ignore errors
        End Try

        If (Not IO.File.Exists(sOutputFile)) Then
            Return
        End If

        Dim sContent As String = IO.File.ReadAllText(sOutputFile, System.Text.Encoding.Default)
        If (String.IsNullOrEmpty(sContent) OrElse sContent.Trim.Length = 0) Then
            Return
        End If

        mData(GetActionTitle()) = "[System]" & Environment.NewLine & sContent
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_DXDIAG
    End Function

    Public Function GetIssues(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckMultipleBluetoothDevices(mData))
        mIssues.AddRange(CheckMultipleUsbControllers(mData))
        mIssues.AddRange(CheckEmpty(mData))
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent(mData As Dictionary(Of String, String)) As String Implements ILogAction.GetSectionContent
        If (Not mData.ContainsKey(GetActionTitle)) Then
            Return Nothing
        End If

        Return mData(GetActionTitle)
    End Function

    Private Function CheckMultipleBluetoothDevices(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Possible Bluetooth Bandwidth Issues",
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

    Private Function CheckMultipleUsbControllers(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return mIssues.ToArray
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Not enough USB 3.0 Host Controllers",
            "This computer does not have enough USB host controllers which limits the amount of devices you can connect to your computer simultaneously.",
            "Switch the computer mainboard that has more USB host controllers or get a PCI-Express USB card and install it in your computer.",
            ENUM_LOG_ISSUE_TYPE.WARNING
        )

        Dim iUsbHostCount As Integer = 0

        Dim sLastDevice As String = ""
        Dim sLastDriver As String = ""

        Dim sLines As String() = FindSection("System Devices", sContent)
        If (sLines Is Nothing OrElse sLines.Length < 1) Then
            Return mIssues.ToArray
        End If

        For i = sLines.Length - 1 To 0 Step -1
            If (sLines(i).TrimStart.StartsWith("Name:")) Then
                sLastDevice = sLines(i).TrimStart.Remove(0, "Name:".Length)

                If (sLastDriver.ToUpperInvariant.Contains("USBXHCI.SYS".ToUpperInvariant)) Then
                    iUsbHostCount += 1
                End If

                sLastDevice = ""
                sLastDriver = ""
            End If

            If (sLines(i).TrimStart.StartsWith("Driver:")) Then
                sLastDriver &= sLines(i).TrimStart.Remove(0, "Driver:".Length) & Environment.NewLine
            End If
        Next

        If (iUsbHostCount < 2) Then
            mIssues.Add(New STRUC_LOG_ISSUE(mTemplate))
        End If

        Return mIssues.ToArray
    End Function

    Public Function CheckEmpty(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE()
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim sContent As String = GetSectionContent(mData)

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "DxDiag Log Unavailable",
            "Some diagnostic details are unavailable due to missing log information.",
            "",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        If (sContent Is Nothing OrElse sContent.Trim.Length = 0) Then
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
