﻿Imports System.Web.Script.Serialization

Public Class FormTroubleshootLogs
    ReadOnly SECTION_BUFFER As String = New String("#"c, 32)

    ReadOnly NOTHING_SELECTED As String = "Nothing selected. Refresh to generate or load a log, then click an entry above to show more details about the issue."

    Public Const SECTION_DETAILED_LOG_SUMMARY = "Detailed Log Summary"
    Public Const SECTION_DETAILED_LOG_EXCEPTIONS = "Log Generation Exceptions"

    Private g_mThread As Threading.Thread = Nothing
    Private g_mLogContent As New ClassLogDiagnostics.ClassLogContent()
    Private g_mProgress As FormLoading = Nothing
    Private g_mLogJobs As New List(Of ClassLogDiagnostics.ILogAction)
    Private g_sLoadedFile As String = ""

    Private g_mFormMain As FormMain
    Private g_bInitRefresh As Boolean = False
    Private g_bSilentRefresh As Boolean = False

    Structure STRUC_LOG_COMBOBOX_ITEM
        Dim sText As String
        Dim sLog As String

        Public Sub New(_Text As String, _Log As String)
            sText = _Text
            sLog = _Log
        End Sub

        Public Overrides Function ToString() As String
            Return sText
        End Function
    End Structure

    Public Sub New(_FormMain As FormMain, bRefresh As Boolean, bSilentRefresh As Boolean)
        g_mFormMain = _FormMain
        g_bInitRefresh = bRefresh

        If (bRefresh) Then
            g_bSilentRefresh = bSilentRefresh
        End If

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ImageList_Issues.Images.Clear()
        ImageList_Issues.Images.Add("Info", My.Resources.user32_104_16x16_32)
        ImageList_Issues.Images.Add("Warn", My.Resources.user32_101_16x16_32)
        ImageList_Issues.Images.Add("Error", My.Resources.imageres_5337_16x16_32)
        ImageList_Issues.Images.Add("Good", My.Resources.netshell_1610_32x32_32)

        g_mLogJobs.Clear()
        g_mLogJobs.AddRange(ClassLogDiagnostics.GetAllJobs(g_mFormMain, m_LogContent))

        TextBox_IssueInfo.Text = NOTHING_SELECTED
    End Sub

    Property m_LoadedFile As String
        Get
            Return g_sLoadedFile
        End Get
        Set(value As String)
            g_sLoadedFile = value

            UpdateTitle()
        End Set
    End Property

    Private Sub UpdateTitle()
        If (String.IsNullOrEmpty(m_LoadedFile)) Then
            Me.Text = String.Format("Logs and Diagnostics")
        Else
            Me.Text = String.Format("Logs and Diagnostics ({0})", IO.Path.GetFileName(m_LoadedFile))
        End If
    End Sub

    Private Sub Button_LogRefresh_Click(sender As Object, e As EventArgs) Handles Button_LogRefresh.Click
        RereshLogs(False)
    End Sub

    Private Sub FormTroubleshootLogs_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (g_bInitRefresh) Then
            Me.Visible = True
            Me.Refresh()

            RereshLogs(g_bSilentRefresh)
        End If
    End Sub

    Private Sub RereshLogs(bSilent As Boolean)
        If (Not bSilent) Then
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
        End If

        m_LoadedFile = ""

        StartLogAnalysis(True, True, True, True, bSilent)
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
                        If (MessageBox.Show("The file to you are trying to load is quite big. Do you want to load it anyways?", "Log too big", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No) Then
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

                    m_LoadedFile = mForm.FileName

                    StartLogAnalysis(False, True, True, True, False)
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
                Dim sModule As String = mItem.SubItems(3).Text

                If (String.IsNullOrEmpty(sMessage) OrElse sMessage.Trim.Length = 0) Then
                    Continue For
                End If

                sContent.AppendFormat("Name: {0}", sMessage.Replace(Environment.NewLine, " ")).AppendLine()
                sContent.AppendFormat(" - Description: {0}", sDescription.Replace(Environment.NewLine, " ")).AppendLine()
                sContent.AppendFormat(" - Solution: {0}", sSolution.Replace(Environment.NewLine, " ")).AppendLine()
                sContent.AppendFormat(" - Module: {0}", sModule.Replace(Environment.NewLine, " ")).AppendLine()
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

    ReadOnly Property m_LogContent As ClassLogDiagnostics.ClassLogContent
        Get
            SyncLock g_mLogContent.m_Lock
                Return g_mLogContent
            End SyncLock
        End Get
    End Property

    Public Sub StartLogAnalysis(bGenerateLogs As Boolean, bDiagnostics As Boolean, bRefreshDisplayLogs As Boolean, bRefreshDevices As Boolean, bSilent As Boolean)
        If (g_mThread IsNot Nothing AndAlso g_mThread.IsAlive) Then
            Return
        End If

        g_mThread = New Threading.Thread(Sub() ThreadLogAnalysis(bGenerateLogs, bDiagnostics, bRefreshDisplayLogs, bRefreshDevices, bSilent))
        g_mThread.IsBackground = True
        g_mThread.Start()
    End Sub

    Private Sub ThreadLogAnalysis(bGenerateLogs As Boolean, bDiagnostics As Boolean, bRefreshDisplayLogs As Boolean, bRefreshDevices As Boolean, bSilent As Boolean)
        Try
            ClassUtils.AsyncInvoke(Sub()
                                       If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                           g_mProgress.Dispose()
                                           g_mProgress = Nothing
                                       End If

                                       g_mProgress = New FormLoading
                                       g_mProgress.Text = "Preparing..."

                                       g_mProgress.m_ProgressBar.Style = ProgressBarStyle.Blocks
                                       g_mProgress.m_ProgressBar.Maximum = 100
                                       g_mProgress.m_ProgressBar.Value = 0

                                       g_mProgress.ShowDialog(Me)
                                   End Sub)

            Dim iTotalJobs As Integer = 0
            If (bGenerateLogs) Then
                iTotalJobs += g_mLogJobs.Count
            End If

            If (bDiagnostics) Then
                iTotalJobs += g_mLogJobs.Count
            End If

            If (bGenerateLogs) Then
                ThreadDoGenerateLogs(iTotalJobs, bSilent)
            End If

            If (bDiagnostics) Then
                ThreadDoDiagnostics(iTotalJobs)
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

    Private Sub ThreadDoGenerateLogs(iTotalJobs As Integer, bSilent As Boolean)
        m_LogContent.m_Content.Clear()

        Dim sLogExceptions As New List(Of String)

        For Each mJob In g_mLogJobs
            Dim sJobAction As String = String.Format("Gathering {0}...", mJob.GetActionTitle())

            ClassUtils.AsyncInvoke(Sub()
                                       If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                           g_mProgress.Text = sJobAction

                                           If (iTotalJobs > 0) Then
                                               g_mProgress.m_ProgressBar.Maximum = iTotalJobs
                                               g_mProgress.m_ProgressBar.Increment(1)
                                               g_mProgress.SkipProgressBarAnimation()
                                           End If
                                       End If
                                   End Sub)

            Try
                mJob.Generate(bSilent)
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

    Private Sub ThreadDoDiagnostics(iTotalJobs As Integer)
        Dim mIssues As New List(Of KeyValuePair(Of String, ClassLogDiagnostics.STRUC_LOG_ISSUE))

        For Each mJob In g_mLogJobs
            Dim sJobAction As String = String.Format("Checking {0}...", mJob.GetActionTitle())

            ClassUtils.AsyncInvoke(Sub()
                                       If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                           g_mProgress.Text = sJobAction

                                           If (iTotalJobs > 0) Then
                                               g_mProgress.m_ProgressBar.Maximum = iTotalJobs
                                               g_mProgress.m_ProgressBar.Increment(1)
                                               g_mProgress.SkipProgressBarAnimation()
                                           End If
                                       End If
                                   End Sub)

            Try
                Dim sContent As String = mJob.GetSectionContent()
                If (Not String.IsNullOrEmpty(sContent) AndAlso sContent.Trim.StartsWith("EXCEPTION: ")) Then
                    Dim sException As String = sContent.Trim.Remove(0, "EXCEPTION: ".Length)

                    Dim mException As New ClassLogDiagnostics.STRUC_LOG_ISSUE(
                        "Log generation exception",
                        sException,
                        "",
                        ClassLogDiagnostics.ENUM_LOG_ISSUE_TYPE.ERROR
                    )
                    mIssues.Add(New KeyValuePair(Of String, ClassLogDiagnostics.STRUC_LOG_ISSUE)(mJob.GetActionTitle(), mException))
                Else
                    For Each mIssue In mJob.GetIssues()
                        mIssues.Add(New KeyValuePair(Of String, ClassLogDiagnostics.STRUC_LOG_ISSUE)(mJob.GetActionTitle(), mIssue))
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
                                               Case ClassLogDiagnostics.ENUM_LOG_ISSUE_TYPE.INFO
                                                   mNewItem.ImageKey = "Info"
                                               Case ClassLogDiagnostics.ENUM_LOG_ISSUE_TYPE.WARNING
                                                   mNewItem.ImageKey = "Warn"
                                               Case ClassLogDiagnostics.ENUM_LOG_ISSUE_TYPE.ERROR
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
            ComboBox_Logs.BeginUpdate()
            ComboBox_Logs.Items.Clear()

            SyncLock m_LogContent.m_Lock
                For Each mItem In m_LogContent.m_Content
                    ComboBox_Logs.Items.Add(New STRUC_LOG_COMBOBOX_ITEM(mItem.Key, mItem.Value))
                Next
            End SyncLock

            If (ComboBox_Logs.Items.Count > 0) Then
                ComboBox_Logs.SelectedIndex = 0
            End If
        Finally
            ComboBox_Logs.EndUpdate()
        End Try
    End Sub

    Private Sub RefreshDevices()
        Dim mDevicesProperties As New Dictionary(Of String, String)

        Dim mDevices = New ClassLogManageServiceDevices(g_mFormMain, m_LogContent)
        Dim mService = New ClassLogService(g_mFormMain, m_LogContent)

        Dim mDeviceList = mDevices.GetDevices()

        For i = mDeviceList.Length - 1 To 0 Step -1
            Dim mDeviceConfig = mService.FindConfigFromSerial(mDeviceList(i).sSerial)

            Dim sName As String = String.Format("{0} {1} ({2})", mDeviceList(i).iType.ToString, mDeviceList(i).iId, mDeviceList(i).sSerial)
            Dim sConfig As String = ""

            If (mDeviceConfig IsNot Nothing) Then
                sConfig = mDeviceConfig.SaveToString()
            End If

            mDevicesProperties(sName) = sConfig
        Next

        Try
            TreeView_DeviceProperties.BeginUpdate()
            TreeView_DeviceProperties.Nodes.Clear()

            For Each mItem In mDevicesProperties
                Try
                    PopulateTreeViewWithJson(TreeView_DeviceProperties, mItem.Key, mItem.Value)
                Catch ex As Exception
                    'Just in case the Json is malformed, show error
                    Dim mNode As New TreeNode(mItem.Key)
                    TreeView_DeviceProperties.Nodes.Add(mNode)
                    mNode.Nodes.Add(New TreeNode(String.Format("ERROR: {0}", ex.Message)))
                End Try
            Next
        Finally
            TreeView_DeviceProperties.EndUpdate()
        End Try
    End Sub

    Private Sub PopulateTreeViewWithJson(mTreeView As TreeView, sRootName As String, sJson As String)
        Dim mJson As New JavaScriptSerializer()
        Dim mJsonData As Object = mJson.DeserializeObject(sJson)

        If (mJsonData Is Nothing) Then
            Return
        End If

        Dim mNode As New TreeNode(sRootName)
        mTreeView.Nodes.Add(mNode)

        PopulateTreeViewWithJsonRecursive(mJsonData, mNode)
    End Sub

    Private Sub PopulateTreeViewWithJsonRecursive(mJsonData As Object, mParentNode As TreeNode)
        Select Case (True)
            Case (TypeOf mJsonData Is IDictionary(Of String, Object))
                For Each mItem As KeyValuePair(Of String, Object) In CType(mJsonData, IDictionary(Of String, Object))
                    Dim mNode As New TreeNode(mItem.Key)
                    mParentNode.Nodes.Add(mNode)
                    PopulateTreeViewWithJsonRecursive(mItem.Value, mNode)
                Next

            Case (TypeOf mJsonData Is IList)
                Dim i As Integer = 0
                For Each mItem In CType(mJsonData, IList)
                    Dim mNode As New TreeNode(CStr(i))

                    mParentNode.Nodes.Add(mNode)

                    PopulateTreeViewWithJsonRecursive(mItem, mNode)
                    i += 1
                Next

            Case Else
                Dim sValue As String = If(mJsonData IsNot Nothing, mJsonData.ToString(), "null")
                'mParentNode.Nodes.Add(New TreeNode(sValue))

                mParentNode.Text = String.Format("{0} = {1}", mParentNode.Text, sValue)

        End Select
    End Sub

    Private Sub ListView_Issues_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_Issues.SelectedIndexChanged
        If (ListView_Issues.SelectedItems.Count < 1) Then
            TextBox_IssueInfo.Text = NOTHING_SELECTED
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

    Private Sub ComboBox_Logs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Logs.SelectedIndexChanged
        If (ComboBox_Logs.SelectedItem Is Nothing) Then
            TextBox_Logs.Text = ""
            Return
        End If

        If (TypeOf ComboBox_Logs.SelectedItem IsNot STRUC_LOG_COMBOBOX_ITEM) Then
            TextBox_Logs.Text = ""
            Return
        End If

        Dim mSleectedItem = DirectCast(ComboBox_Logs.SelectedItem, STRUC_LOG_COMBOBOX_ITEM)
        TextBox_Logs.Text = mSleectedItem.sLog
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