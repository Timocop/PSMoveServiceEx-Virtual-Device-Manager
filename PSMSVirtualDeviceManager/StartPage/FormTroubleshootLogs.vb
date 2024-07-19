Public Class FormTroubleshootLogs
    Private g_mThreadLock As New Object
    Private g_mThread As Threading.Thread = Nothing
    Private g_mFileContent As New Dictionary(Of String, String)
    Private g_mProgress As FormLoading = Nothing

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Button_LogRefresh_Click(sender As Object, e As EventArgs) Handles Button_LogRefresh.Click
        If (g_mThread IsNot Nothing AndAlso g_mThread.IsAlive) Then
            Return
        End If

        g_mThread = New Threading.Thread(AddressOf ThreadRefresh)
        g_mThread.IsBackground = True
        g_mThread.Start()
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

                If (mForm.ShowDialog = DialogResult.OK) Then
                    IO.File.WriteAllText(mForm.FileName, sContent)
                End If
            End Using
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Function GetCombinedLogs() As String
        Dim sContent As New Text.StringBuilder

        For Each mItem In m_FileContent
            sContent.AppendFormat("{0} {1} {2}", New String("#"c, 32), mItem.Key, New String("#"c, 32)).AppendLine()
            sContent.AppendLine()

            sContent.AppendLine(mItem.Value)

            sContent.AppendLine()
        Next

        Return sContent.ToString
    End Function

    ReadOnly Property m_FileContent As Dictionary(Of String, String)
        Get
            SyncLock g_mThreadLock
                Return g_mFileContent
            End SyncLock
        End Get
    End Property

    Private Sub ThreadRefresh()
        Try
            ClassUtils.AsyncInvoke(Me, Sub()
                                           If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                               g_mProgress.Dispose()
                                               g_mProgress = Nothing
                                           End If

                                           g_mProgress = New FormLoading
                                           g_mProgress.Text = "Gathering logs..."
                                           g_mProgress.ShowDialog(Me)
                                       End Sub)

            m_FileContent.Clear()

            ClassUtils.AsyncInvoke(Me, Sub()
                                           If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                               g_mProgress.Text = "Gathering DxDiag logs..."
                                           End If
                                       End Sub)
            RefreshDxdiagLog()

            ClassUtils.AsyncInvoke(Me, Sub()
                                           If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                               g_mProgress.Text = "Gathering PSMoveServiceEx logs..."
                                           End If
                                       End Sub)
            RefreshServiceLogs()

            ClassUtils.AsyncInvoke(Me, Sub()
                                           If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                               g_mProgress.Text = "Gathering running processes..."
                                           End If
                                       End Sub)
            RefreshProcesses()

            ClassUtils.AsyncInvoke(Me, Sub()
                                           If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                               g_mProgress.Text = "Gathering Virtual Device Manager logs..."
                                           End If
                                       End Sub)
            RefreshManager()

            ClassUtils.AsyncInvoke(Me, Sub()
                                           For i = TabControl_Logs.TabCount - 1 To 0 Step -1
                                               TabControl_Logs.TabPages(i).Dispose()
                                           Next

                                           For Each mItem In m_FileContent
                                               Dim mTab As New TabPage(mItem.Key)
                                               mTab.BackColor = Color.White

                                               Dim mTextBox As New TextBox
                                               mTextBox.SuspendLayout()
                                               mTextBox.Parent = mTab
                                               mTextBox.Text = mItem.Value
                                               mTextBox.Multiline = True
                                               mTextBox.WordWrap = False
                                               mTextBox.Dock = DockStyle.Fill
                                               mTextBox.BorderStyle = BorderStyle.None
                                               mTextBox.ScrollBars = ScrollBars.Both
                                               mTextBox.ResumeLayout()

                                               TabControl_Logs.TabPages.Add(mTab)
                                           Next

                                           If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                               g_mProgress.Dispose()
                                               g_mProgress = Nothing
                                           End If
                                       End Sub)
        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        Finally
            ClassUtils.AsyncInvoke(Me, Sub()
                                           If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                               g_mProgress.Dispose()
                                               g_mProgress = Nothing
                                           End If
                                       End Sub)
        End Try
    End Sub

    Private Sub RefreshDxdiagLog()
        Dim sRootFolder As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "dxdiag.exe")
        Dim sOutputFile As String = IO.Path.Combine(IO.Path.GetTempPath(), IO.Path.GetRandomFileName)

        If (Not IO.File.Exists(sRootFolder)) Then
            Return
        End If

        Using mProcess As New Process
            mProcess.StartInfo.FileName = sRootFolder
            mProcess.StartInfo.Arguments = String.Format("/t ""{0}""", sOutputFile)
            mProcess.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(sRootFolder)
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True
            mProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            mProcess.Start()
            mProcess.WaitForExit()
        End Using

        If (Not IO.File.Exists(sOutputFile)) Then
            Throw New ArgumentException("DxDiag output log does not exist")
        End If

        m_FileContent("DxDiag") = IO.File.ReadAllText(sOutputFile)
    End Sub

    Private Sub RefreshServiceLogs()
        Dim mConfig As New ClassServiceInfo
        mConfig.LoadConfig()

        If (Not mConfig.FileExist()) Then
            If (mConfig.FindByProcess()) Then
                mConfig.SaveConfig()
            Else
                If (mConfig.SearchForService) Then
                    mConfig.SaveConfig()
                Else
                    ' Logs does not exist
                    Return
                End If
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

        m_FileContent("PSMoveServiceEx") = IO.File.ReadAllText(sLogFile)
    End Sub

    Private Sub RefreshProcesses()
        Dim sProcessLog As New Text.StringBuilder

        For Each mProcess In Process.GetProcesses
            Try
                sProcessLog.AppendFormat("[{0}]", mProcess.Id).AppendLine()
                sProcessLog.AppendFormat("ProcessName={0}", mProcess.ProcessName).AppendLine()
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
            End Try

            Try
                sProcessLog.AppendFormat("FileName={0}", mProcess.MainModule.FileName).AppendLine()
                sProcessLog.AppendFormat("FileDescription={0}", mProcess.MainModule.FileVersionInfo.FileDescription).AppendLine()
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
            End Try

            sProcessLog.AppendLine()
        Next

        m_FileContent("Running Processes") = sProcessLog.ToString
    End Sub

    Private Sub RefreshManager()
        Dim mManagerLogs As New Dictionary(Of String, String)
        mManagerLogs("VDM Application Exceptions") = (ClassConfigConst.PATH_LOG_APPLICATION_ERROR)
        mManagerLogs("VDM Settings") = (ClassConfigConst.PATH_CONFIG_SETTINGS)
        mManagerLogs("VDM Attachments") = (ClassConfigConst.PATH_CONFIG_ATTACHMENT)
        mManagerLogs("VDM Remote Devices") = (ClassConfigConst.PATH_CONFIG_REMOTE)
        mManagerLogs("VDM Virtual Motion Trackers") = (ClassConfigConst.PATH_CONFIG_VMT)
        mManagerLogs("VDM Virutal Trackers") = (ClassConfigConst.PATH_CONFIG_DEVICES)

        For Each mItem In mManagerLogs
            If (Not IO.File.Exists(mItem.Value)) Then
                Continue For
            End If

            m_FileContent(mItem.Key) = IO.File.ReadAllText(mItem.Value)
        Next
    End Sub

    Private Sub CleanUp()
        If (g_mThread IsNot Nothing AndAlso g_mThread.IsAlive) Then
            g_mThread.Abort()
            g_mThread.Join()
            g_mThread = Nothing
        End If
    End Sub

    Private Sub FormTroubleshootLogs_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CleanUp()
    End Sub
End Class