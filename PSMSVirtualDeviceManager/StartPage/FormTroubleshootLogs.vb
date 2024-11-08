Imports PSMSVirtualDeviceManager

Public Class FormTroubleshootLogs
    ReadOnly SECTION_BUFFER As String = New String("#"c, 32)

    Public Const SECTION_DETAILED_LOG_SUMMARY = "Detailed Log Summary"
    Public Const SECTION_DETAILED_LOG_EXCEPTIONS = "Log Generation Exceptions"

    Public Const SECTION_DXDIAG = "DirectX Diagnostics"
    Public Const SECTION_PSMOVESERVICEEX = "PSMoveServiceEx"
    Public Const SECTION_PROCESSES = "Running Processes"
    Public Const SECTION_VDM_CONFIG = "VDM Configuration"
    Public Const SECTION_VDM_VMT_TRACKERS = "VDM VMT Trackers"
    Public Const SECTION_VDM_OSC_DEVICES = "VDM OSC Devices"
    Public Const SECTION_VDM_SERVICE_DEVICES = "VDM Service Devices"
    Public Const SECTION_VDM_CONTROLLER_ATTACHMENTS = "VDM Controller Attachments"
    Public Const SECTION_VDM_REMOTE_DEVICES = "VDM Remote Devices"
    Public Const SECTION_VDM_PSVR = "VDM PlayStation VR"
    Public Const SECTION_VDM_VIRTUAL_TRACKERS = "VDM Virtual Trackers"
    Public Const SECTION_STEAMVR_DRIVERS = "SteamVR Drivers"
    Public Const SECTION_STEAMVR_MANIFESTS = "SteamVR Manifests"
    Public Const SECTION_STEAMVR_OVERRIDES = "SteamVR Overrides"
    Public Const SECTION_STEAMVR_SETTINGS = "SteamVR Settings"
    Public Const SECTION_CONNECTED_HARDWARE = "Connected Hardware"

    Private g_mThread As Threading.Thread = Nothing
    Private g_mLogContent As New ClassLogContent()
    Private g_mProgress As FormLoading = Nothing

    Private g_mLogJobs As New List(Of ILogAction)

    Private g_mFormMain As FormMain = Nothing

    Class ClassLogContent
        Private g_mThreadLock As New Object
        Private g_mLogContent As New Dictionary(Of String, String)

        ReadOnly Property m_Content As Dictionary(Of String, String)
            Get
                SyncLock g_mThreadLock
                    Return g_mLogContent
                End SyncLock
            End Get
        End Property

        ReadOnly Property m_Lock As Object
            Get
                Return g_mThreadLock
            End Get
        End Property
    End Class

    Enum ENUM_LOG_ISSUE_TYPE
        INFO
        WARNING
        [ERROR]
    End Enum

    Structure STRUC_LOG_ISSUE
        Dim bValid As Boolean

        Dim sMessage As String
        Dim sDescription As String
        Dim sSolution As String
        Dim iType As ENUM_LOG_ISSUE_TYPE

        Public Sub New(_Issue As STRUC_LOG_ISSUE)
            Me.New(_Issue.sMessage, _Issue.sDescription, _Issue.sSolution, _Issue.iType)
        End Sub

        Public Sub New(_Message As String, _Description As String, _Solution As String, _Type As ENUM_LOG_ISSUE_TYPE)
            bValid = True
            sMessage = _Message
            sDescription = _Description
            sSolution = _Solution
            iType = _Type
        End Sub
    End Structure

    Public Interface ILogAction
        Function GetActionTitle() As String
        Sub Generate()
        Function GetIssues() As STRUC_LOG_ISSUE()
        Function GetSectionContent() As String
    End Interface

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ImageList_Issues.Images.Clear()
        ImageList_Issues.Images.Add("Info", My.Resources.user32_104_16x16_32)
        ImageList_Issues.Images.Add("Warn", My.Resources.user32_101_16x16_32)
        ImageList_Issues.Images.Add("Error", My.Resources.imageres_5337_16x16_32)
        ImageList_Issues.Images.Add("Good", My.Resources.netshell_1610_32x32_32)

        g_mLogJobs.Clear()
        g_mLogJobs.Add(New ClassLogDxdiag(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogService(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogProcesses(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManager(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManagerVmtTrackers(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManageOscDevices(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManageServiceDevices(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManagerAttachments(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManagerRemoteDevices(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManagerPSVR(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManagerVirtualTrackers(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManagerSteamVrDrivers(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManagerSteamVrManifests(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManagerSteamVrOverrides(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManagerSteamVrSettings(g_mFormMain, m_LogContent))
        g_mLogJobs.Add(New ClassLogManagerHardware(g_mFormMain, m_LogContent))
    End Sub

    Private Sub Button_LogRefresh_Click(sender As Object, e As EventArgs) Handles Button_LogRefresh.Click
        If (g_mFormMain.g_mPSMoveServiceCAPI IsNot Nothing AndAlso Not g_mFormMain.g_mPSMoveServiceCAPI.m_IsServiceConnected) Then
            Dim sMessage As New Text.StringBuilder
            sMessage.AppendLine("PSMoveServiceEx is not running!")
            sMessage.AppendLine("To generate all diagnostic information, PSMoveServiceEx must be running.")
            sMessage.AppendLine("If PSMoveServiceEx is not running, some diagnostic information may be missing or displayed incorrectly.")
            sMessage.AppendLine("It's recommended to run PSMoveServiceEx before generating diagnostics information!")
            sMessage.AppendLine("")
            sMessage.AppendLine("Click OK to ignore this warning and generate diagnostic information without PSMovServiceEx running.")
            sMessage.AppendLine("Otherwise click CANCEL to abort.")

            If (MessageBox.Show(sMessage.ToString, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                Return
            End If
        End If

        StartLogAnalysis(True, True, True, True)
    End Sub

    Private Sub Button_LogCopy_Click(sender As Object, e As EventArgs) Handles Button_LogCopy.Click
        Try
            Dim sContent As String = GetCombinedLogs()
            If (String.IsNullOrEmpty(sContent)) Then
                Throw New ArgumentException("Logs are empty")
            End If

            Clipboard.SetText(sContent)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub Button_LogSave_Click(sender As Object, e As EventArgs) Handles Button_LogSave.Click
        Try
            Dim sContent As String = GetCombinedLogs()
            If (String.IsNullOrEmpty(sContent)) Then
                Throw New ArgumentException("Logs are empty")
            End If

            Using mForm As New SaveFileDialog
                mForm.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*"
                mForm.FileName = "combined_logs.txt"

                If (mForm.ShowDialog(Me) = DialogResult.OK) Then
                    IO.File.WriteAllText(mForm.FileName, sContent)
                End If
            End Using
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub Button_LogLoad_Click(sender As Object, e As EventArgs) Handles Button_LogLoad.Click
        Try
            Using mForm As New OpenFileDialog
                mForm.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*"

                If (mForm.ShowDialog(Me) = DialogResult.OK) Then
                    Dim iMaxLength As Integer = (1 * 1024 * 1024)
                    If (New IO.FileInfo(mForm.FileName).Length > iMaxLength) Then
                        If (MessageBox.Show("The file to you are trying to load is quite big. Do you want to load it anways?", "Log too big", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No) Then
                            Return
                        End If
                    End If

                    m_LogContent.m_Content.Clear()

                    ' Detect encoding
                    Using mFile As New IO.StreamReader(mForm.FileName, True)
                        For Each mItem In ParseCombinedLogs(mFile.ReadToEnd())
                            m_LogContent.m_Content(mItem.Key) = mItem.Value
                        Next
                    End Using


                    StartLogAnalysis(False, True, True, True)
                End If
            End Using
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Function GetCombinedLogs() As String
        Dim sContent As New Text.StringBuilder

        If (ListView_Issues.Items.Count > 0) Then
            sContent.AppendFormat("{0} {1} {2}", SECTION_BUFFER, SECTION_DETAILED_LOG_SUMMARY, SECTION_BUFFER).AppendLine()
            sContent.AppendLine()

            For Each mItem As ListViewItem In ListView_Issues.Items
                Dim sMessage As String = mItem.SubItems(0).Text
                Dim sDescription As String = mItem.SubItems(1).Text
                Dim sSolution As String = mItem.SubItems(2).Text

                If (String.IsNullOrEmpty(sMessage) OrElse sMessage.Trim.Length = 0) Then
                    Continue For
                End If

                sContent.AppendFormat("Name: {0}", sMessage.Replace(Environment.NewLine, " ")).AppendLine()
                sContent.AppendFormat(" - Description: {0}", sDescription.Replace(Environment.NewLine, " ")).AppendLine()
                sContent.AppendFormat(" - Solution: {0}", sSolution.Replace(Environment.NewLine, " ")).AppendLine()
                sContent.AppendLine()
            Next


            sContent.AppendLine()
        End If

        SyncLock m_LogContent.m_Lock
            For Each mItem In m_LogContent.m_Content
                sContent.AppendFormat("{0} {1} {2}", SECTION_BUFFER, mItem.Key, SECTION_BUFFER).AppendLine()
                sContent.AppendLine()

                sContent.AppendLine(mItem.Value)

                sContent.AppendLine()
            Next
        End SyncLock

        Return sContent.ToString
    End Function

    Private Function ParseCombinedLogs(sContent As String) As Dictionary(Of String, String)
        Dim mData As New Dictionary(Of String, String)

        Dim sSection As String = Nothing
        Dim sSectionContent As New Text.StringBuilder
        Dim sContentLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)

        For i = 0 To sContentLines.Length - 1
            Dim sLine As String = sContentLines(i).Trim

            If (sLine.StartsWith(SECTION_BUFFER) OrElse sLine.EndsWith(SECTION_BUFFER)) Then
                If (Not String.IsNullOrEmpty(sSection)) Then
                    If (sSection <> SECTION_DETAILED_LOG_SUMMARY) Then
                        mData(sSection) = sSectionContent.ToString.Trim
                    End If
                End If

                sSectionContent = New Text.StringBuilder
                sSection = Nothing

                If (sLine.StartsWith(SECTION_BUFFER) AndAlso sLine.EndsWith(SECTION_BUFFER)) Then
                    sSection = sLine
                    sSection = sSection.Remove(sSection.Length - SECTION_BUFFER.Length, SECTION_BUFFER.Length)
                    sSection = sSection.Remove(0, SECTION_BUFFER.Length)
                    sSection = sSection.Trim
                End If

                Continue For
            End If

            If (String.IsNullOrEmpty(sSection)) Then
                Continue For
            End If

            sSectionContent.AppendLine(sLine)
        Next

        If (Not String.IsNullOrEmpty(sSection)) Then
            mData(sSection) = sSectionContent.ToString.Trim
        End If

        Return mData
    End Function

    ReadOnly Property m_LogContent As ClassLogContent
        Get
            SyncLock g_mLogContent.m_Lock
                Return g_mLogContent
            End SyncLock
        End Get
    End Property

    Public Sub StartLogAnalysis(bGenerateLogs As Boolean, bDiagnostics As Boolean, bRefreshDisplayLogs As Boolean, bRefreshDevices As Boolean)
        If (g_mThread IsNot Nothing AndAlso g_mThread.IsAlive) Then
            Return
        End If

        g_mThread = New Threading.Thread(Sub() ThreadLogAnalysis(bGenerateLogs, bDiagnostics, bRefreshDisplayLogs, bRefreshDevices))
        g_mThread.IsBackground = True
        g_mThread.Start()
    End Sub

    Private Sub ThreadLogAnalysis(bGenerateLogs As Boolean, bDiagnostics As Boolean, bRefreshDisplayLogs As Boolean, bRefreshDevices As Boolean)
        Try
            ClassUtils.AsyncInvoke(Sub()
                                       If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                           g_mProgress.Dispose()
                                           g_mProgress = Nothing
                                       End If

                                       g_mProgress = New FormLoading
                                       g_mProgress.Text = "Preparing..."
                                       g_mProgress.ShowDialog(Me)
                                   End Sub)

            If (bGenerateLogs) Then
                ThreadDoGenerateLogs()
            End If

            If (bDiagnostics) Then
                ThreadDoDiagnostics()
            End If

            If (bRefreshDisplayLogs) Then
                ClassUtils.AsyncInvoke(Sub() RefreshDisplayLogs())
            End If

            If (bRefreshDevices) Then
                ClassUtils.AsyncInvoke(Sub() RefreshDevices())
            End If

        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        Finally
            ClassUtils.AsyncInvoke(Sub()
                                       If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                           g_mProgress.Dispose()
                                           g_mProgress = Nothing
                                       End If
                                   End Sub)
        End Try
    End Sub

    Private Sub ThreadDoGenerateLogs()
        m_LogContent.m_Content.Clear()

        Dim sLogExceptions As New List(Of String)

        For Each mJob In g_mLogJobs
            Dim sJobAction As String = String.Format("Gathering {0}...", mJob.GetActionTitle())

            ClassUtils.AsyncInvoke(Sub()
                                       If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                           g_mProgress.Text = sJobAction
                                       End If
                                   End Sub)

            Try
                mJob.Generate()
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                sLogExceptions.Add(String.Format("{0}: {1}", mJob.GetActionTitle(), ex.Message))

                ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
            End Try
        Next

        If (sLogExceptions.Count > 0) Then
            m_LogContent.m_Content(SECTION_DETAILED_LOG_EXCEPTIONS) = String.Join(Environment.NewLine, sLogExceptions.ToArray)
        End If
    End Sub

    Private Sub ThreadDoDiagnostics()
        Dim mIssues As New List(Of KeyValuePair(Of String, STRUC_LOG_ISSUE))

        For Each mJob In g_mLogJobs
            Dim sJobAction As String = String.Format("Checking {0}...", mJob.GetActionTitle())

            ClassUtils.AsyncInvoke(Sub()
                                       If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                           g_mProgress.Text = sJobAction
                                       End If
                                   End Sub)

            Try
                Dim sContent As String = mJob.GetSectionContent()
                If (Not String.IsNullOrEmpty(sContent) AndAlso sContent.Trim.StartsWith("EXCEPTION: ")) Then
                    Dim sException As String = sContent.Trim.Remove(0, "EXCEPTION: ".Length)

                    Dim mException As New STRUC_LOG_ISSUE(
                        "Log generation exception",
                        sException,
                        "",
                        ENUM_LOG_ISSUE_TYPE.ERROR
                    )
                    mIssues.Add(New KeyValuePair(Of String, STRUC_LOG_ISSUE)(mJob.GetActionTitle(), mException))
                Else
                    For Each mIssue In mJob.GetIssues()
                        mIssues.Add(New KeyValuePair(Of String, STRUC_LOG_ISSUE)(mJob.GetActionTitle(), mIssue))
                    Next
                End If
            Catch ex As NotImplementedException
                ' Ignore
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
            End Try
        Next

        ClassUtils.AsyncInvoke(Sub()
                                   ListView_Issues.Items.Clear()

                                   ListView_Issues.BeginUpdate()
                                   Try
                                       For Each mItem In mIssues
                                           Dim mNewItem As New ListViewItem(New String() {
                                                   mItem.Value.sMessage,
                                                   mItem.Value.sDescription,
                                                   mItem.Value.sSolution,
                                                   mItem.Key
                                               })

                                           mNewItem.ImageIndex = 0

                                           Select Case (mItem.Value.iType)
                                               Case ENUM_LOG_ISSUE_TYPE.INFO
                                                   mNewItem.ImageKey = "Info"
                                               Case ENUM_LOG_ISSUE_TYPE.WARNING
                                                   mNewItem.ImageKey = "Warn"
                                               Case ENUM_LOG_ISSUE_TYPE.ERROR
                                                   mNewItem.ImageKey = "Error"
                                           End Select

                                           ListView_Issues.Items.Add(mNewItem)
                                       Next

                                       If (ListView_Issues.Items.Count < 1) Then
                                           Dim mNewItem As New ListViewItem(New String() {
                                                    "No issues have been found",
                                                    "",
                                                    "",
                                                    ""
                                                })

                                           mNewItem.ImageKey = "Good"
                                           ListView_Issues.Items.Add(mNewItem)
                                       End If
                                   Finally
                                       ListView_Issues.EndUpdate()
                                   End Try
                               End Sub)
    End Sub

    Private Sub RefreshDisplayLogs()
        Try
            TabControl_Logs.SuspendLayout()

            For i = TabControl_Logs.TabCount - 1 To 0 Step -1
                TabControl_Logs.TabPages(i).Dispose()
            Next

            SyncLock m_LogContent.m_Lock
                For Each mItem In m_LogContent.m_Content
                    Dim mTab As New TabPage(mItem.Key)
                    mTab.BackColor = Color.White

                    Dim mTextBox As New TextBox
                    mTextBox.SuspendLayout()
                    mTextBox.Parent = mTab

                    mTextBox.Multiline = True
                    mTextBox.WordWrap = False
                    mTextBox.ReadOnly = True
                    mTextBox.BackColor = Color.White

                    mTextBox.Text = mItem.Value
                    mTextBox.Dock = DockStyle.Fill
                    mTextBox.BorderStyle = BorderStyle.None
                    mTextBox.ScrollBars = ScrollBars.Both
                    mTextBox.ResumeLayout()

                    TabControl_Logs.TabPages.Add(mTab)
                Next
            End SyncLock
        Finally
            TabControl_Logs.ResumeLayout()
        End Try
    End Sub

    Private Sub RefreshDevices()
        Try
            ListView_Devices.BeginUpdate()
            ListView_Devices.Items.Clear()

            Dim mDevices = New ClassLogManageServiceDevices(g_mFormMain, m_LogContent)
            Dim mService = New ClassLogService(g_mFormMain, m_LogContent)

            Dim mDeviceList = mDevices.GetDevices()

            For i = mDeviceList.Length - 1 To 0 Step -1
                Dim mDeviceConfig = mService.FindConfigFromSerial(mDeviceList(i).sSerial)

                Dim mItem As New ListViewItem(New String() {
                                              mDeviceList(i).iType.ToString,
                                              CStr(mDeviceList(i).iId),
                                              mDeviceList(i).sSerial
                })

                If (mDeviceConfig Is Nothing) Then
                    mItem.Tag = ""
                Else
                    mItem.Tag = mDeviceConfig.SaveToString()
                End If

                ListView_Devices.Items.Add(mItem)
            Next

            If (ListView_Devices.Items.Count = 0) Then
                ListView_Devices.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
            Else
                ListView_Devices.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            End If
        Finally
            ListView_Devices.EndUpdate()
        End Try
    End Sub

    Private Sub ListView_Issues_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_Issues.SelectedIndexChanged
        If (ListView_Issues.SelectedItems.Count < 1) Then
            TextBox_IssueInfo.Text = "Nothing selected."
            Return
        End If

        Dim mItem As ListViewItem = ListView_Issues.SelectedItems(0)

        Dim sInfo As New Text.StringBuilder
        sInfo.AppendLine("Message:")
        sInfo.AppendLine(mItem.SubItems(0).Text)
        sInfo.AppendLine()

        sInfo.AppendLine("Description:")
        sInfo.AppendLine(mItem.SubItems(1).Text)
        sInfo.AppendLine()

        sInfo.AppendLine("Solution:")
        sInfo.AppendLine(mItem.SubItems(2).Text)
        sInfo.AppendLine()

        sInfo.AppendLine("Module:")
        sInfo.AppendLine(mItem.SubItems(3).Text)
        sInfo.AppendLine()

        TextBox_IssueInfo.Text = sInfo.ToString
    End Sub

    Private Sub ListView_Devices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_Devices.SelectedIndexChanged
        If (ListView_Devices.SelectedItems.Count < 1) Then
            Return
        End If

        If (ListView_Devices.SelectedItems(0).Tag Is Nothing) Then
            Return
        End If

        ClassUtils.UpdateTextBoxNoScroll(TextBox_DeviceConfig, CStr(ListView_Devices.SelectedItems(0).Tag))
    End Sub

    Private Sub FormTroubleshootLogs_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CleanUp()
    End Sub

    Private Sub CleanUp()
        If (g_mThread IsNot Nothing AndAlso g_mThread.IsAlive) Then
            g_mThread.Abort()
            g_mThread.Join()
            g_mThread = Nothing
        End If

        If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
            g_mProgress.Dispose()
            g_mProgress = Nothing
        End If
    End Sub
End Class