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

        If (Not IO.File.Exists(sOutputFile)) Then
            Throw New ArgumentException("DxDiag output log does not exist")
        End If

        Dim sContent As New Text.StringBuilder()
        sContent.AppendLine("[System]")
        sContent.AppendLine(IO.File.ReadAllText(sOutputFile, System.Text.Encoding.Default))
        mData(GetActionTitle()) = sContent.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_DXDIAG
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
            If (Not g_mData.ContainsKey(SECTION_DXDIAG)) Then
                Return Nothing
            End If

            Return g_mData(SECTION_DXDIAG)
        End Function

        Public Function GetIssues() As STRUC_LOG_ISSUE() Implements IIssuesTracker.GetIssues
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)
            mIssues.AddRange(CheckMultipleBluetoothDevices)
            mIssues.AddRange(CheckMultipleUsbControllers)
            Return mIssues.ToArray
        End Function

        Private Function CheckMultipleBluetoothDevices() As STRUC_LOG_ISSUE()
            Dim mIssues As New List(Of STRUC_LOG_ISSUE)

            Dim sContent As String = GetSectionContent()
            If (sContent Is Nothing) Then
                Return mIssues.ToArray
            End If

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Possible Bluetooth Bandwidth Issues - {0}"
            mTemplate.sDescription = "Having multiple bluetooth devices connected, such as '{0}', could cause bandwidth issues if you want to use bluetooth controllers such as PlayStation Move controllers."
            mTemplate.sSolution = "If you encounter issues with connected controllers, reduce the amount of devices connected to your bluetooth adapter."
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.WARNING

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
                        Dim mIssue As New STRUC_LOG_ISSUE
                        mIssue.bValid = True
                        mIssue.sMessage = String.Format(mTemplate.sMessage, sLastDevice)
                        mIssue.sDescription = String.Format(mTemplate.sDescription, sLastDevice)
                        mIssue.sSolution = mTemplate.sSolution
                        mIssue.iType = mTemplate.iType

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

            Dim mTemplate As New STRUC_LOG_ISSUE
            mTemplate.bValid = False
            mTemplate.sMessage = "Not enough USB 3.0 Host Controllers"
            mTemplate.sDescription = "This computer does not have enough USB host controllers which limits the amount of devices you can connect to your computer simultaneously."
            mTemplate.sSolution = "Switch the computer mainboard that has more USB host controllers or get a PCI-Express USB card and install it in your computer."
            mTemplate.iType = ENUM_LOG_ISSUE_TYPE.WARNING

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
                Dim mIssue As New STRUC_LOG_ISSUE
                mIssue.bValid = True
                mIssue.sMessage = mTemplate.sMessage
                mIssue.sDescription = mTemplate.sDescription
                mIssue.sSolution = mTemplate.sSolution
                mIssue.iType = mTemplate.iType

                mIssues.Add(mIssue)
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
End Class
