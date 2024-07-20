Imports PSMSVirtualDeviceManager

Public Class FormTroubleshootLogs
    Private g_mThreadLock As New Object
    Private g_mThread As Threading.Thread = Nothing
    Private g_mFileContent As New Dictionary(Of String, String)
    Private g_mProgress As FormLoading = Nothing

    Private g_mLogJobs As New List(Of ILogAction)

    Private g_mFormMain As FormMain = Nothing

    Private Interface ILogAction
        Function GetActionTitle() As String

        Sub DoWork(mData As Dictionary(Of String, String))
    End Interface


    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        g_mLogJobs.Clear()
        g_mLogJobs.Add(New ClassLogDxdiag())
        g_mLogJobs.Add(New ClassLogService())
        g_mLogJobs.Add(New ClassLogProcesses())
        g_mLogJobs.Add(New ClassLogManager())
        g_mLogJobs.Add(New ClassLogManagerVmtTrackers(g_mFormMain))
        g_mLogJobs.Add(New ClassLogManageOscDevices(g_mFormMain))
        g_mLogJobs.Add(New ClassLogManageServiceDevices(g_mFormMain))
        g_mLogJobs.Add(New ClassLogManagerAttachments(g_mFormMain))
        g_mLogJobs.Add(New ClassLogManagerRemoteDevices(g_mFormMain))
        g_mLogJobs.Add(New ClassLogManagerPSVR(g_mFormMain))
        g_mLogJobs.Add(New ClassLogManagerVirtualTrackers(g_mFormMain))
        g_mLogJobs.Add(New ClassLogManagerSteamVrDrivers())
        g_mLogJobs.Add(New ClassLogManagerSteamVrManifests())
        g_mLogJobs.Add(New ClassLogManagerSteamVrOverrides())
        g_mLogJobs.Add(New ClassLogManagerSteamVrSettings())
        g_mLogJobs.Add(New ClassLogManagerHardware())
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

            For Each mJob In g_mLogJobs
                Dim sJobAction As String = String.Format("Gathering {0}...", mJob.GetActionTitle())

                ClassUtils.AsyncInvoke(Me, Sub()
                                               If (g_mProgress IsNot Nothing AndAlso Not g_mProgress.IsDisposed) Then
                                                   g_mProgress.Text = sJobAction
                                               End If
                                           End Sub)

                Try
                    mJob.DoWork(m_FileContent)
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    m_FileContent(mJob.GetActionTitle()) = String.Format("EXCEPTION: {0}", ex.Message)
                    ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                End Try
            Next

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

    Class ClassLogDxdiag
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
            sContent.AppendLine(IO.File.ReadAllText(sOutputFile))
            mData(GetActionTitle()) = sContent.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "DirectX Diagnostics"
        End Function
    End Class

    Class ClassLogService
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
            Return "PSMoveServiceEx"
        End Function
    End Class

    Class ClassLogProcesses
        Implements ILogAction

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
            Dim sProcessLog As New Text.StringBuilder

            For Each mProcess In Process.GetProcesses
                Try
                    sProcessLog.AppendFormat("[ProcessID_{0}]", mProcess.Id).AppendLine()
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

            mData(GetActionTitle()) = sProcessLog.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "Running Processes"
        End Function
    End Class

    Class ClassLogManager
        Implements ILogAction

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
            Dim mManagerLogs As New Dictionary(Of String, String)
            mManagerLogs("VDM INI Application Exceptions") = (ClassConfigConst.PATH_LOG_APPLICATION_ERROR)
            mManagerLogs("VDM INI Settings") = (ClassConfigConst.PATH_CONFIG_SETTINGS)
            mManagerLogs("VDM INI Attachments") = (ClassConfigConst.PATH_CONFIG_ATTACHMENT)
            mManagerLogs("VDM INI Remote Devices") = (ClassConfigConst.PATH_CONFIG_REMOTE)
            mManagerLogs("VDM INI Virtual Motion Trackers") = (ClassConfigConst.PATH_CONFIG_VMT)
            mManagerLogs("VDM INI Virutal Trackers") = (ClassConfigConst.PATH_CONFIG_DEVICES)

            For Each mItem In mManagerLogs
                If (Not IO.File.Exists(mItem.Value)) Then
                    Continue For
                End If


                Dim sTmp As String = IO.Path.GetTempFileName
                IO.File.Copy(mItem.Value, sTmp, True)

                mData(mItem.Key) = IO.File.ReadAllText(sTmp)
                IO.File.Delete(sTmp)
            Next
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "VDM Configuration"
        End Function
    End Class

    Class ClassLogManagerVmtTrackers
        Implements ILogAction

        Private g_mFormMain As FormMain

        Public Sub New(_FormMain As FormMain)
            g_mFormMain = _FormMain
        End Sub

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
            If (g_mFormMain.g_mUCVirtualMotionTracker Is Nothing OrElse g_mFormMain.g_mUCVirtualMotionTracker.g_UCVmtTrackers Is Nothing) Then
                Return
            End If

            Dim sTrackersList As New Text.StringBuilder

            ' Not thread-safe
            ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                                   Dim mVmtTrackers = g_mFormMain.g_mUCVirtualMotionTracker.g_UCVmtTrackers.GetVmtTrackers()
                                                   For Each mItem In mVmtTrackers
                                                       If (mItem.g_mClassIO.m_IsHMD) Then
                                                           sTrackersList.AppendFormat("[Hmd_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                                       Else
                                                           sTrackersList.AppendFormat("[Controller_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                                       End If
                                                       sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                                       sTrackersList.AppendFormat("ID={0}", mItem.g_mClassIO.m_Index).AppendLine()
                                                       sTrackersList.AppendFormat("VmtID={0}", mItem.g_mClassIO.m_VmtTracker).AppendLine()
                                                       sTrackersList.AppendFormat("VmtTrackerRole={0}", mItem.g_mClassIO.m_VmtTrackerRole).AppendLine()
                                                       sTrackersList.AppendFormat("FpsOscCounter={0}", mItem.g_mClassIO.m_FpsOscCounter).AppendLine()

                                                       sTrackersList.AppendLine()
                                                   Next
                                               End Sub)

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "VDM VMT Trackers"
        End Function
    End Class

    Class ClassLogManageOscDevices
        Implements ILogAction

        Private g_mFormMain As FormMain

        Public Sub New(_FormMain As FormMain)
            g_mFormMain = _FormMain
        End Sub

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
            If (g_mFormMain.g_mUCVirtualMotionTracker Is Nothing OrElse g_mFormMain.g_mUCVirtualMotionTracker.g_ClassOscDevices Is Nothing) Then
                Return
            End If

            Dim sTrackersList As New Text.StringBuilder

            Dim mVmtTrackers = g_mFormMain.g_mUCVirtualMotionTracker.g_ClassOscDevices.GetDevices
            For Each mItem In mVmtTrackers
                sTrackersList.AppendFormat("[Device_{0}]", mItem.iIndex).AppendLine()
                sTrackersList.AppendFormat("ID={0}", mItem.iIndex).AppendLine()
                sTrackersList.AppendFormat("Type={0}", mItem.iType).AppendLine()
                sTrackersList.AppendFormat("Serial={0}", mItem.sSerial).AppendLine()
                sTrackersList.AppendFormat("Position={0}", mItem.mPos.ToString).AppendLine()
                sTrackersList.AppendFormat("Orientation={0}", mItem.GetOrientationEuler().ToString).AppendLine()

                sTrackersList.AppendLine()
            Next

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "VDM OSC Devices"
        End Function
    End Class

    Class ClassLogManageServiceDevices
        Implements ILogAction

        Private g_mFormMain As FormMain

        Public Sub New(_FormMain As FormMain)
            g_mFormMain = _FormMain
        End Sub

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
            If (g_mFormMain.g_mPSMoveServiceCAPI Is Nothing) Then
                Return
            End If

            If (Not g_mFormMain.g_mPSMoveServiceCAPI.m_IsServiceConnected) Then
                Throw New ArgumentException("Service not connected")
            End If

            Dim sTrackersList As New Text.StringBuilder

            Dim mControllers = g_mFormMain.g_mPSMoveServiceCAPI.GetControllersData
            For Each mItem In mControllers
                sTrackersList.AppendFormat("[Controller_{0}]", mItem.m_Id).AppendLine()
                sTrackersList.AppendFormat("ID={0}", mItem.m_Id).AppendLine()
                sTrackersList.AppendFormat("IsConnected={0}", mItem.m_IsConnected).AppendLine()
                sTrackersList.AppendFormat("IsTracking={0}", mItem.m_IsTracking).AppendLine()
                sTrackersList.AppendFormat("IsValid={0}", mItem.m_IsValid).AppendLine()
                sTrackersList.AppendFormat("Serial={0}", mItem.m_Serial).AppendLine()
                sTrackersList.AppendFormat("TrackingColor={0}", mItem.m_TrackingColor).AppendLine()
                sTrackersList.AppendFormat("BatteryLevel={0}", mItem.m_BatteryLevel).AppendLine()
                sTrackersList.AppendFormat("OutputSeqNum={0}", mItem.m_OutputSeqNum).AppendLine()
                sTrackersList.AppendFormat("Position={0}", mItem.m_Position.ToString).AppendLine()
                sTrackersList.AppendFormat("Orientation={0}", mItem.GetOrientationEuler().ToString).AppendLine()

                sTrackersList.AppendLine()
            Next

            Dim mHmds = g_mFormMain.g_mPSMoveServiceCAPI.GetHmdsData
            For Each mItem In mHmds
                sTrackersList.AppendFormat("[Hmd_{0}]", mItem.m_Id).AppendLine()
                sTrackersList.AppendFormat("ID={0}", mItem.m_Id).AppendLine()
                sTrackersList.AppendFormat("IsConnected={0}", mItem.m_IsConnected).AppendLine()
                sTrackersList.AppendFormat("IsTracking={0}", mItem.m_IsTracking).AppendLine()
                sTrackersList.AppendFormat("IsValid={0}", mItem.m_IsValid).AppendLine()
                sTrackersList.AppendFormat("Serial={0}", mItem.m_Serial).AppendLine()
                sTrackersList.AppendFormat("OutputSeqNum={0}", mItem.m_OutputSeqNum).AppendLine()
                sTrackersList.AppendFormat("Position={0}", mItem.m_Position.ToString).AppendLine()
                sTrackersList.AppendFormat("Orientation={0}", mItem.GetOrientationEuler().ToString).AppendLine()

                sTrackersList.AppendLine()
            Next

            Dim mTrakcers = g_mFormMain.g_mPSMoveServiceCAPI.GetTrackersData
            For Each mItem In mTrakcers
                sTrackersList.AppendFormat("[Tracker_{0}]", mItem.m_Id).AppendLine()
                sTrackersList.AppendFormat("ID={0}", mItem.m_Id).AppendLine()
                sTrackersList.AppendFormat("Path={0}", mItem.m_Path).AppendLine()
                sTrackersList.AppendFormat("OutputSeqNum={0}", mItem.m_OutputSeqNum).AppendLine()
                sTrackersList.AppendFormat("Position={0}", mItem.m_Position.ToString).AppendLine()
                sTrackersList.AppendFormat("Orientation={0}", mItem.GetOrientationEuler().ToString).AppendLine()

                sTrackersList.AppendLine()
            Next

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "VDM Service Devices"
        End Function
    End Class

    Class ClassLogManagerAttachments
        Implements ILogAction

        Private g_mFormMain As FormMain

        Public Sub New(_FormMain As FormMain)
            g_mFormMain = _FormMain
        End Sub

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
            If (g_mFormMain.g_mUCVirtualControllers Is Nothing OrElse g_mFormMain.g_mUCVirtualControllers.g_mUCControllerAttachments Is Nothing) Then
                Return
            End If

            Dim sTrackersList As New Text.StringBuilder

            ' Not thread-safe
            ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                                   Dim mAttachments = g_mFormMain.g_mUCVirtualControllers.g_mUCControllerAttachments.GetAttachments()
                                                   For Each mItem In mAttachments
                                                       sTrackersList.AppendFormat("[Controller_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                                       sTrackersList.AppendFormat("NickName={0}", mItem.m_Nickname).AppendLine()
                                                       sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                                       sTrackersList.AppendFormat("ParentControllerID={0}", mItem.g_mClassIO.m_ParentController).AppendLine()
                                                       sTrackersList.AppendFormat("FpsPipeCounter={0}", mItem.g_mClassIO.m_FpsPipeCounter).AppendLine()
                                                       sTrackersList.AppendFormat("ControllerOffset={0}", mItem.g_mClassIO.m_ControllerOffset.ToString).AppendLine()
                                                       sTrackersList.AppendFormat("ControllerYawCorrection={0}", mItem.g_mClassIO.m_ControllerYawCorrection).AppendLine()
                                                       sTrackersList.AppendFormat("JointOffset={0}", mItem.g_mClassIO.m_JointOffset.ToString).AppendLine()
                                                       sTrackersList.AppendFormat("JointYawCorrection={0}", mItem.g_mClassIO.m_JointYawCorrection).AppendLine()
                                                       sTrackersList.AppendFormat("OnlyJointOffset={0}", mItem.g_mClassIO.m_OnlyJointOffset).AppendLine()

                                                       sTrackersList.AppendLine()
                                                   Next
                                               End Sub)

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "VDM Controller Attachments"
        End Function
    End Class

    Class ClassLogManagerRemoteDevices
        Implements ILogAction

        Private g_mFormMain As FormMain

        Public Sub New(_FormMain As FormMain)
            g_mFormMain = _FormMain
        End Sub

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
            If (g_mFormMain.g_mUCVirtualControllers Is Nothing OrElse g_mFormMain.g_mUCVirtualControllers.g_mUCRemoteDevices Is Nothing) Then
                Return
            End If

            Dim sTrackersList As New Text.StringBuilder

            ' Not thread-safe
            ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                                   Dim mRemoteDevices = g_mFormMain.g_mUCVirtualControllers.g_mUCRemoteDevices.GetRemoteDevices()
                                                   For Each mItem In mRemoteDevices
                                                       sTrackersList.AppendFormat("[Controller_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                                       sTrackersList.AppendFormat("NickName={0}", mItem.m_Nickname).AppendLine()
                                                       sTrackersList.AppendFormat("TrackerName={0}", mItem.m_TrackerName).AppendLine()
                                                       sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                                       sTrackersList.AppendFormat("FpsPipeCounter={0}", mItem.g_mClassIO.m_FpsPipeCounter).AppendLine()
                                                       sTrackersList.AppendFormat("Orientation={0}", ClassQuaternionTools.FromQ(mItem.g_mClassIO.m_Orientation).ToString).AppendLine()
                                                       sTrackersList.AppendFormat("ResetOrientation={0}", ClassQuaternionTools.FromQ(mItem.g_mClassIO.m_ResetOrientation).ToString).AppendLine()
                                                       sTrackersList.AppendFormat("YawOrientationOffset={0}", mItem.g_mClassIO.m_YawOrientationOffset).AppendLine()

                                                       sTrackersList.AppendLine()
                                                   Next
                                               End Sub)

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "VDM Controller Attachments"
        End Function
    End Class

    Class ClassLogManagerPSVR
        Implements ILogAction

        Private g_mFormMain As FormMain

        Public Sub New(_FormMain As FormMain)
            g_mFormMain = _FormMain
        End Sub

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
            If (g_mFormMain.g_mUCPlaystationVR Is Nothing) Then
                Return
            End If

            Dim sTrackersList As New Text.StringBuilder

            ' Not thread-safe
            ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                                   sTrackersList.AppendFormat("[PlayStationVR]").AppendLine()
                                                   sTrackersList.AppendFormat("Status={0}", g_mFormMain.g_mUCPlaystationVR.Label_PSVRStatus.Text).AppendLine()
                                                   sTrackersList.AppendLine()

                                                   sTrackersList.AppendFormat("[PlayStationVR HDMI]").AppendLine()
                                                   sTrackersList.AppendFormat("Status={0}", g_mFormMain.g_mUCPlaystationVR.Label_HDMIStatus.Text).AppendLine()
                                                   sTrackersList.AppendFormat("Text={0}", g_mFormMain.g_mUCPlaystationVR.Label_HDMIStatusText.Text).AppendLine()
                                                   sTrackersList.AppendLine()

                                                   sTrackersList.AppendFormat("[PlayStationVR USB]").AppendLine()
                                                   sTrackersList.AppendFormat("Status={0}", g_mFormMain.g_mUCPlaystationVR.Label_USBStatus.Text).AppendLine()
                                                   sTrackersList.AppendFormat("Text={0}", g_mFormMain.g_mUCPlaystationVR.Label_USBStatusText.Text).AppendLine()
                                                   sTrackersList.AppendLine()

                                                   sTrackersList.AppendFormat("[PlayStationVR Display]").AppendLine()
                                                   sTrackersList.AppendFormat("Status={0}", g_mFormMain.g_mUCPlaystationVR.Label_DisplayStatus.Text).AppendLine()
                                                   sTrackersList.AppendFormat("Text={0}", g_mFormMain.g_mUCPlaystationVR.Label_DisplayStatusText.Text).AppendLine()
                                                   sTrackersList.AppendLine()
                                               End Sub)

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "VDM PlayStation VR"
        End Function
    End Class

    Class ClassLogManagerVirtualTrackers
        Implements ILogAction

        Private g_mFormMain As FormMain

        Public Sub New(_FormMain As FormMain)
            g_mFormMain = _FormMain
        End Sub

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
            If (g_mFormMain.g_mUCVirtualTrackers Is Nothing) Then
                Return
            End If

            Dim sTrackersList As New Text.StringBuilder

            ' Not thread-safe
            ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                                   Dim mTrackers = g_mFormMain.g_mUCVirtualTrackers.GetAllDevices()
                                                   For Each mItem In mTrackers
                                                       sTrackersList.AppendFormat("[{0}]", mItem.m_DevicePath).AppendLine()
                                                       sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                                       sTrackersList.AppendFormat("CameraFramerate={0}", mItem.g_mClassCaptureLogic.m_CameraFramerate).AppendLine()
                                                       sTrackersList.AppendFormat("CameraResolution={0}", mItem.g_mClassCaptureLogic.m_CameraResolution).AppendLine()
                                                       sTrackersList.AppendFormat("DeviceIndex={0}", mItem.g_mClassCaptureLogic.m_DeviceIndex).AppendLine()
                                                       sTrackersList.AppendFormat("FlipImage={0}", mItem.g_mClassCaptureLogic.m_FlipImage).AppendLine()
                                                       sTrackersList.AppendFormat("ImageInterpolation={0}", mItem.g_mClassCaptureLogic.m_ImageInterpolation).AppendLine()
                                                       sTrackersList.AppendFormat("Initialized={0}", mItem.g_mClassCaptureLogic.m_Initialized).AppendLine()
                                                       sTrackersList.AppendFormat("IsPlayStationCamera={0}", mItem.g_mClassCaptureLogic.m_IsPlayStationCamera).AppendLine()
                                                       sTrackersList.AppendFormat("PipeConnected={0}", mItem.g_mClassCaptureLogic.m_PipeConnected).AppendLine()
                                                       sTrackersList.AppendFormat("PipePrimaryIndex={0}", mItem.g_mClassCaptureLogic.m_PipePrimaryIndex).AppendLine()
                                                       sTrackersList.AppendFormat("PipeSecondaryIndex={0}", mItem.g_mClassCaptureLogic.m_PipeSecondaryIndex).AppendLine()
                                                       sTrackersList.AppendFormat("ShowCaptureImage={0}", mItem.g_mClassCaptureLogic.m_ShowCaptureImage).AppendLine()
                                                       sTrackersList.AppendFormat("Supersampling={0}", mItem.g_mClassCaptureLogic.m_Supersampling).AppendLine()
                                                       sTrackersList.AppendFormat("UseMJPG={0}", mItem.g_mClassCaptureLogic.m_UseMJPG).AppendLine()

                                                       sTrackersList.AppendLine()
                                                   Next
                                               End Sub)

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "VDM Virtual Trackers"
        End Function
    End Class

    Class ClassLogManagerSteamVrDrivers
        Implements ILogAction

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork

            Dim sTrackersList As New Text.StringBuilder

            Dim mConfig As New ClassOpenVRConfig
            mConfig.LoadConfig()

            For Each sDriver In mConfig.GetDrivers()
                sTrackersList.AppendFormat("[{0}]", sDriver).AppendLine()
            Next

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "SteamVR Drivers"
        End Function
    End Class

    Class ClassLogManagerSteamVrManifests
        Implements ILogAction

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork

            Dim sTrackersList As New Text.StringBuilder

            Dim mConfig As New ClassSteamVRConfig
            mConfig.LoadConfig()

            mConfig.m_ClassManifests.LoadConfig()
            For Each sManifest In mConfig.m_ClassManifests.GetManifests
                sTrackersList.AppendFormat("[{0}]", sManifest).AppendLine()
            Next

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "SteamVR Manifests"
        End Function
    End Class

    Class ClassLogManagerSteamVrOverrides
        Implements ILogAction

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork

            Dim sTrackersList As New Text.StringBuilder

            Dim mConfig As New ClassSteamVRConfig
            mConfig.LoadConfig()

            For Each sOverrides In mConfig.m_ClassOverrides.GetOverrides
                sTrackersList.AppendFormat("[{0}]", sOverrides.Key).AppendLine()
                sTrackersList.AppendFormat("Override={0}", sOverrides.Value).AppendLine()
            Next

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "SteamVR Overrides"
        End Function
    End Class

    Class ClassLogManagerSteamVrSettings
        Implements ILogAction

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork

            Dim sTrackersList As New Text.StringBuilder

            Dim mConfig As New ClassSteamVRConfig
            mConfig.LoadConfig()

            sTrackersList.AppendFormat("[{0}]", mConfig.m_SteamPath).AppendLine()
            sTrackersList.AppendFormat("ActivateMultipleDrivers={0}", mConfig.m_ClassSettings.m_ActivateMultipleDrivers).AppendLine()
            sTrackersList.AppendFormat("EnableHomeApp={0}", mConfig.m_ClassSettings.m_EnableHomeApp).AppendLine()
            sTrackersList.AppendFormat("EnableMirrorView={0}", mConfig.m_ClassSettings.m_EnableMirrorView).AppendLine()
            sTrackersList.AppendFormat("EnablePerformanceGraph={0}", mConfig.m_ClassSettings.m_EnablePerformanceGraph).AppendLine()
            sTrackersList.AppendFormat("ForcedDriver={0}", mConfig.m_ClassSettings.m_ForcedDriver).AppendLine()
            sTrackersList.AppendFormat("NullHmdEnabled={0}", mConfig.m_ClassSettings.m_NullHmdEnabled).AppendLine()
            sTrackersList.AppendFormat("RequireHmd={0}", mConfig.m_ClassSettings.m_RequireHmd).AppendLine()

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "SteamVR Settings"
        End Function
    End Class

    Class ClassLogManagerHardware
        Implements ILogAction

        Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork

            Dim sTrackersList As New Text.StringBuilder

            Dim mLibusbDriver As New ClassLibusbDriver

            Dim mUsbDevices As New Dictionary(Of String, ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO)
            For Each mDevice In mLibusbDriver.GetAllDevices("USB")
                If (String.IsNullOrEmpty(mDevice.sService)) Then
                    Continue For
                End If

                Dim bSuccess As Boolean = False

                Select Case (mDevice.sService.ToUpperInvariant)
                    Case ClassLibusbDriver.USBVIDEO_SERVICE_NAME.ToUpperInvariant,
                                    ClassLibusbDriver.BTHUSB_SERVICE_NAME.ToUpperInvariant

                        bSuccess = True
                End Select

                If (bSuccess) Then
                    Dim sVID As String = Nothing
                    Dim sPID As String = Nothing
                    Dim sMM As String = Nothing
                    Dim sSerial As String = Nothing
                    If (Not mLibusbDriver.ResolveHardwareID(mDevice.sDeviceID, sVID, sPID, sMM, sSerial)) Then
                        Continue For
                    End If

                    If (String.IsNullOrEmpty(mDevice.sProviderDescription)) Then
                        Continue For
                    End If

                    Dim mKnownConfig As New ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO(mDevice.sProviderDescription, "", sVID, sPID, sMM, mDevice.sService)

                    mUsbDevices(String.Format("{0}/{1}/{2}", sVID, sPID, If(sMM, "XX"))) = mKnownConfig
                End If
            Next

            Dim mTmpList As New List(Of ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO)
            mTmpList.AddRange(ClassLibusbDriver.DRV_PS4CAM_KNOWN_CONFIGS)
            mTmpList.AddRange(ClassLibusbDriver.DRV_PSEYE_KNOWN_CONFIGS)
            mTmpList.AddRange(ClassLibusbDriver.DRV_PSVR_KNOWN_CONFIGS)
            mTmpList.AddRange(ClassLibusbDriver.DRV_PSMOVE_KNOWN_CONFIGS)
            mTmpList.AddRange(ClassLibusbDriver.DRV_CONTROLLER_KNOWN_CONFIGS)
            mTmpList.AddRange(ClassLibusbDriver.DRV_DUALSHOCK_KNOWN_CONFIGS)

            For Each mDevice In mTmpList
                mUsbDevices(String.Format("{0}/{1}/{2}", mDevice.VID, mDevice.PID, If(mDevice.MM, "XX"))) = mDevice
            Next

            For Each mDevice In mUsbDevices
                For Each mProvider In mLibusbDriver.GetDeviceProvider(mDevice.Value.VID, mDevice.Value.PID, mDevice.Value.MM, "USB", "")
                    Dim sVID As String = Nothing
                    Dim sPID As String = Nothing
                    Dim sMM As String = Nothing
                    Dim sSerial As String = Nothing
                    If (mLibusbDriver.ResolveHardwareID(mProvider.sDeviceID, sVID, sPID, sMM, sSerial)) Then
                        If (Not mLibusbDriver.IsUsbDeviceConnected(mDevice.Value.VID, mDevice.Value.PID, "USB", sSerial)) Then
                            Continue For
                        End If
                    End If

                    sTrackersList.AppendFormat("[{0}]", mProvider.sDeviceID).AppendLine()
                    sTrackersList.AppendFormat("Name={0}", mDevice.Value.sName).AppendLine()
                    sTrackersList.AppendFormat("Manufacture={0}", mDevice.Value.sManufacture).AppendLine()
                    sTrackersList.AppendFormat("ExpectedService={0}", mDevice.Value.sService).AppendLine()

                    sTrackersList.AppendFormat("DriverInfPath={0}", mProvider.sDriverInfPath).AppendLine()
                    sTrackersList.AppendFormat("ProviderDescription={0}", mProvider.sProviderDescription).AppendLine()
                    sTrackersList.AppendFormat("ProviderName={0}", mProvider.sProviderName).AppendLine()
                    sTrackersList.AppendFormat("ProviderVersion={0}", mProvider.sProviderVersion).AppendLine()
                    sTrackersList.AppendFormat("Service={0}", mProvider.sService).AppendLine()
                    sTrackersList.AppendFormat("HasDriverInstalled={0}", mProvider.HasDriverInstalled).AppendLine()
                    sTrackersList.AppendFormat("IsEnabled={0}", mProvider.IsEnabled).AppendLine()
                    sTrackersList.AppendFormat("IsRemoved={0}", mProvider.IsRemoved).AppendLine()

                    sTrackersList.AppendFormat("IsCorrectDrvierInstalled={0}", mProvider.sService.ToUpperInvariant = mDevice.Value.sService.ToUpperInvariant).AppendLine()

                    sTrackersList.AppendLine()
                Next
            Next

            mData(GetActionTitle()) = sTrackersList.ToString
        End Sub

        Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
            Return "Connected Hardware"
        End Function
    End Class

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