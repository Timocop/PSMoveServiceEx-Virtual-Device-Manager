Imports System.Numerics

Public Class UCStartPage
    Private Shared g_mThreadLock As New Object

    Private ReadOnly DRIVER_INSTALLER_TIME As Integer = 60
    Private ReadOnly DRIVER_INSTALLER_MULTI As Integer = 10

    Private g_FormMain As FormMain
    Private g_bIgnoreEvents As Boolean = False
    Private g_bInit As Boolean = False

    Private g_mStatusThread As Threading.Thread = Nothing
    Private g_mServiceDeviceStatusThread As Threading.Thread = Nothing

    Private g_mDriverInstallThread As Threading.Thread = Nothing
    Private g_mDriverInstallFormLoad As FormLoading = Nothing
    Private g_mDriverInstallTimerThread As Threading.Thread = Nothing

    Private g_mUpdateInstallThread As Threading.Thread = Nothing
    Private g_mUpdateInstallFormLoad As FormLoading = Nothing

    Private g_mRuntimeDiagnostics As Threading.Thread = Nothing
    Private g_bRuntimeDiagnosticsExecute As Boolean = False

    Private g_bIsServiceRunning As Boolean = False
    Private g_bIsServiceConnected As Boolean = False
    Private g_mFormRestart As FormLoading = Nothing

    Private ReadOnly g_sServiceProcesses As String() = {
        "PSMoveService.exe",
        "PSMoveServiceAdmin.exe",
        "test_camera.exe",
        "test_camera_parallel.exe",
        "test_console_CAPI.exe",
        "test_ds4_controller.exe",
        "test_kalman_filter.exe",
        "test_navi_controller.exe",
        "test_psmove_controller.exe",
        "unit_test_suite.exe"
    }

    Public Sub New(mFormMain As FormMain)
        g_FormMain = mFormMain

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ToolStripComboBox_ChartSamples.Items.Clear()
        ToolStripComboBox_ChartSamples.Items.Add("100")
        ToolStripComboBox_ChartSamples.Items.Add("250")
        ToolStripComboBox_ChartSamples.Items.Add("500")
        ToolStripComboBox_ChartSamples.Items.Add("1000")
        ToolStripComboBox_ChartSamples.SelectedIndex = 0

        CreateControl()
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

        SetStatusServiceConnected()

        g_mStatusThread = New Threading.Thread(AddressOf CheckConnection_Thread)
        g_mStatusThread.Priority = Threading.ThreadPriority.Lowest
        g_mStatusThread.IsBackground = True
        g_mStatusThread.Start()

        g_mServiceDeviceStatusThread = New Threading.Thread(AddressOf ServiceDeviceStatusThread)
        g_mServiceDeviceStatusThread.Priority = Threading.ThreadPriority.Lowest
        g_mServiceDeviceStatusThread.IsBackground = True
        g_mServiceDeviceStatusThread.Start()

        g_mRuntimeDiagnostics = New Threading.Thread(AddressOf RuntimeDiagnostics_Thread)
        g_mRuntimeDiagnostics.Priority = Threading.ThreadPriority.Lowest
        g_mRuntimeDiagnostics.IsBackground = True
        g_mRuntimeDiagnostics.Start()
    End Sub

    Private Sub ToolTip_Service_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip_Service.Popup
        Try
            If (g_bIgnoreEvents) Then
                Return
            End If

            Dim mConfig As New ClassServiceInfo
            mConfig.LoadConfig()

            Try
                g_bIgnoreEvents = True

                If (mConfig.FileExist) Then
                    ToolTip_Service.ToolTipTitle = "Service path:"
                    ToolTip_Service.SetToolTip(e.AssociatedControl, mConfig.m_FileName)
                Else
                    e.Cancel = True
                End If
            Finally
                g_bIgnoreEvents = False

            End Try
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try
    End Sub

    Private Sub SetStatusServiceConnected()
        Dim bIsServiceRunning As Boolean
        Dim bIsServiceConnected As Boolean

        SyncLock g_mThreadLock
            bIsServiceRunning = g_bIsServiceRunning
            bIsServiceConnected = g_bIsServiceConnected
        End SyncLock

        If (bIsServiceRunning AndAlso Not bIsServiceConnected) Then
            Label_PsmsxStatus.Text = "Connecting to Service..."
            Panel_PsmsxStatus.BackColor = Color.FromArgb(255, 128, 0)

            ' Label Status in MainForm
            g_FormMain.Label_ServiceStatus.Text = "Connecting to Service..."
            g_FormMain.Label_ServiceStatus.Image = My.Resources.Status_YELLOW_16
        Else
            If (bIsServiceConnected) Then
                Label_PsmsxStatus.Text = "Service Connected"
                Panel_PsmsxStatus.BackColor = Color.FromArgb(0, 192, 0)

                ' Label Status in MainForm
                g_FormMain.Label_ServiceStatus.Text = "Service Connected"
                g_FormMain.Label_ServiceStatus.Image = My.Resources.Status_GREEN_16

            Else
                Label_PsmsxStatus.Text = "Service Disconnected"
                Panel_PsmsxStatus.BackColor = Color.FromArgb(192, 0, 0)

                ' Label Status in MainForm
                g_FormMain.Label_ServiceStatus.Text = "Service Disconnected"
                g_FormMain.Label_ServiceStatus.Image = My.Resources.Status_RED_16
            End If
        End If

        If (bIsServiceRunning) Then
            g_FormMain.LinkLabel_RunPSMS.Text = "Stop Service"
            g_FormMain.LinkLabel_RunPSMS.Image = My.Resources.imageres_5337_16x16_32
        Else
            g_FormMain.LinkLabel_RunPSMS.Text = "Run Service"
            g_FormMain.LinkLabel_RunPSMS.Image = My.Resources.imageres_5341_16x16_32
        End If
    End Sub

    Public Sub RunRuntimeDiagnostics()
        SyncLock g_mThreadLock
            g_bRuntimeDiagnosticsExecute = True
        End SyncLock
    End Sub

    Private Sub RuntimeDiagnostics_Thread()
        Dim mLastFileTimestamps As New Dictionary(Of String, Date)
        Dim mConfig As New ClassServiceInfo
        Dim bForceRun As Boolean = False

        Dim sCheckFiles As New List(Of String)
        sCheckFiles.Add(ClassConfigConst.PATH_CONFIG_SETTINGS)
        sCheckFiles.Add(ClassConfigConst.PATH_CONFIG_ATTACHMENT)
        sCheckFiles.Add(ClassConfigConst.PATH_CONFIG_REMOTE)
        sCheckFiles.Add(ClassConfigConst.PATH_CONFIG_VMT)
        sCheckFiles.Add(ClassConfigConst.PATH_CONFIG_DEVICES)

        While True
            Dim bShowIssuesPrompt As Boolean = False
            Dim bOutdatedServiceLogs As Boolean = False
            Dim mFileTimestamps As New Dictionary(Of String, Date)

            While True
                Try
                    ' Just find the process, dont even bother to load from config
                    If (True) Then
                        mConfig.FindByProcess()  ' Check if the log has been updated

                        If (mConfig.FileExist()) Then
                            Dim sServceDirectory As String = IO.Path.GetDirectoryName(mConfig.m_FileName)
                            Dim sFile As String = IO.Path.Combine(sServceDirectory, "PSMoveServiceEx.log")
                            If (IO.File.Exists(sFile)) Then
                                mFileTimestamps(sFile.ToLowerInvariant) = IO.File.GetLastWriteTime(sFile)
                            End If
                        End If
                    End If

                    ' Check VMT config files if they changed.
                    If (True) Then
                        For Each sFile In sCheckFiles
                            If (IO.File.Exists(sFile)) Then
                                mFileTimestamps(sFile.ToLowerInvariant) = IO.File.GetLastWriteTime(sFile)
                            End If
                        Next
                    End If

                    If (Not bForceRun) Then
                        Dim bFileUpdated As Boolean = False

                        For Each sFile As String In mFileTimestamps.Keys
                            Dim sFileInvariant = sFile.ToLowerInvariant

                            If (mLastFileTimestamps.ContainsKey(sFileInvariant)) Then
                                If (mLastFileTimestamps(sFileInvariant) < mFileTimestamps(sFileInvariant)) Then
                                    bFileUpdated = True
                                End If
                            Else
                                bFileUpdated = True
                            End If

                            mLastFileTimestamps(sFileInvariant) = mFileTimestamps(sFileInvariant)
                        Next

                        If (Not bFileUpdated) Then
                            Exit While
                        End If
                    End If

                    bForceRun = False

                    Dim mLogDiagnosticsContent As New ClassLogDiagnostics.ClassLogContent()
                    Dim mLogDiagnosticsJobs = ClassLogDiagnostics.GetAllJobs(g_FormMain, mLogDiagnosticsContent)
                    Dim mLogDiagnosticsIssues As New Dictionary(Of String, List(Of ClassLogDiagnostics.STRUC_LOG_ISSUE))

                    ' Generate diagnostics report
                    For Each mJob In mLogDiagnosticsJobs
                        Try
                            mJob.Generate(True)
                        Catch ex As NotImplementedException
                            ' Ignore
                        Catch ex As Threading.ThreadAbortException
                            Throw
                        Catch ex As Exception
                            ClassAdvancedExceptionLogging.WriteToLog(ex)
                        End Try
                    Next

                    ' Get all issues
                    For Each mJob In mLogDiagnosticsJobs
                        Try
                            Dim sJobTitle As String = mJob.GetActionTitle()

                            mLogDiagnosticsIssues(sJobTitle) = New List(Of ClassLogDiagnostics.STRUC_LOG_ISSUE)
                            mLogDiagnosticsIssues(sJobTitle).AddRange(mJob.GetIssues())
                        Catch ex As NotImplementedException
                            ' Ignore
                        Catch ex As Threading.ThreadAbortException
                            Throw
                        Catch ex As Exception
                            ClassAdvancedExceptionLogging.WriteToLog(ex)
                        End Try
                    Next

                    ' Dont prompt when these issues are available. 
                    Dim bServiceLogFound As Boolean = False
                    Dim bServiceDevicesFound As Boolean = False

                    For Each sJobTitle As String In mLogDiagnosticsIssues.Keys
                        Select Case (sJobTitle)
                            Case ClassLogService.SECTION_PSMOVESERVICEEX
                                bServiceLogFound = True

                                For Each mIssue In mLogDiagnosticsIssues(sJobTitle)
                                    Select Case (mIssue.sMessage)
                                        Case ClassLogService.LOG_ISSUE_EMPTY,
                                             ClassLogService.LOG_ISSUE_SERVICE_LOG_INCOMPLETE

                                            ' We have to make sure the service log is ready and also not empty. 
                                            Exit While

                                        Case ClassLogService.LOG_ISSUE_SERVICE_LOG_OUTDATED
                                            bOutdatedServiceLogs = True
                                    End Select
                                Next


                            Case ClassLogManageServiceDevices.SECTION_VDM_SERVICE_DEVICES
                                bServiceDevicesFound = True

                                For Each mIssue In mLogDiagnosticsIssues(sJobTitle)
                                    Select Case (mIssue.sMessage)
                                        Case ClassLogManageServiceDevices.LOG_ISSUE_EMPTY

                                            ' We have to make sure the service log is ready and also not empty. 
                                            Exit While
                                    End Select
                                Next
                        End Select
                    Next

                    If (bServiceLogFound AndAlso bServiceDevicesFound) Then
                        ' Check for issues
                        For Each sJobTitle As String In mLogDiagnosticsIssues.Keys
                            Select Case (sJobTitle)
                                Case ClassLogDxdiag.SECTION_DXDIAG

                                    ' DirectX diagnostics are not available in silent mode
                                    Continue For
                            End Select

                            For Each mIssue In mLogDiagnosticsIssues(sJobTitle)
                                Select Case (mIssue.sMessage)
                                    Case ClassLogService.LOG_ISSUE_EMPTY,
                                         ClassLogManageServiceDevices.LOG_ISSUE_EMPTY,
                                         ClassLogDxdiag.LOG_ISSUE_EMPTY

                                        ' Not really an issue, skip it
                                        Continue For
                                End Select

                                If (mIssue.iType = ClassLogDiagnostics.ENUM_LOG_ISSUE_TYPE.ERROR) Then
                                    bShowIssuesPrompt = True
                                    Exit For
                                End If
                            Next

                            If (bShowIssuesPrompt) Then
                                Exit For
                            End If
                        Next

                        ClassUtils.AsyncInvoke(Sub()
                                                   Panel_VdmDiagnosticIssue.Visible = bShowIssuesPrompt
                                               End Sub)
                    End If
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLog(ex)
                End Try

                Exit While
            End While

            For i = 1 To 10
                Threading.Thread.Sleep(1000)

                SyncLock g_mThreadLock
                    If (g_bRuntimeDiagnosticsExecute) Then
                        g_bRuntimeDiagnosticsExecute = False

                        bForceRun = True
                        Exit For
                    End If
                End SyncLock

            Next
        End While
    End Sub

    Private Sub CheckConnection_Thread()
        While True
            Try
                ' Check if PSMoveServiceEx is running
                Dim mServiceInfo As New ClassServiceInfo
                Dim bServiceRunning = (mServiceInfo.IsServiceRunning <> ClassServiceInfo.ENUM_SERVICE_PROCESS_TYPE.NONE)

                SyncLock g_mThreadLock
                    If (g_bIsServiceRunning <> bServiceRunning) Then
                        g_bIsServiceRunning = bServiceRunning

                        ClassUtils.AsyncInvoke(Sub() SetStatusServiceConnected())
                    End If
                End SyncLock
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLog(ex)
            End Try

            Try
                ' Check if we are connected ot not
                Dim bIsConnected = g_FormMain.g_mPSMoveServiceCAPI.m_IsServiceConnected

                SyncLock g_mThreadLock
                    If (g_bIsServiceConnected <> bIsConnected) Then
                        g_bIsServiceConnected = bIsConnected

                        ClassUtils.AsyncInvoke(Sub() SetStatusServiceConnected())
                    End If
                End SyncLock
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLog(ex)
            End Try

            Threading.Thread.Sleep(1000)
        End While
    End Sub

    Private Sub ServiceDeviceStatusThread()
        Dim mLastSeqNumDic As New Dictionary(Of String, Integer)
        Dim mFpsCountDic As New Dictionary(Of String, Queue(Of KeyValuePair(Of Date, Integer)))

        Dim iFrameCount As Integer = 0

        While True
            Try
                Const LISTVIEW_SUBITEM_TYPE As Integer = 0
                Const LISTVIEW_SUBITEM_COLOR As Integer = 1
                Const LISTVIEW_SUBITEM_ID As Integer = 2
                Const LISTVIEW_SUBITEM_SERIAL As Integer = 3
                Const LISTVIEW_SUBITEM_POSITION As Integer = 4
                Const LISTVIEW_SUBITEM_ORIENTATION As Integer = 5
                Const LISTVIEW_SUBITEM_BATTERY As Integer = 6
                Const LISTVIEW_SUBITEM_FPS As Integer = 7

                Dim mNow As Date = Now

                ' Show disconnected devices
                ClassUtils.AsyncInvoke(Sub()
                                           If (Not Me.Visible) Then
                                               Return
                                           End If

                                           ListView_ServiceDevices.BeginUpdate()
                                           Try
                                               For Each mListVIewItem As ListViewItem In ListView_ServiceDevices.Items
                                                   If (mListVIewItem.Tag Is Nothing) Then
                                                       Continue For
                                                   End If

                                                   Dim mItemInfo = CType(mListVIewItem.Tag, Dictionary(Of String, Object))
                                                   Dim mLastPoseTime As Date = CDate(mItemInfo("LastTimestamp"))
                                                   Dim mLastItemTime As Date = CDate(mItemInfo("CreationTimestamp"))

                                                   If (mLastItemTime + New TimeSpan(0, 0, 2) > mNow) Then
                                                       mListVIewItem.BackColor = Color.FromArgb(192, 255, 192)
                                                   Else
                                                       If (mLastPoseTime + New TimeSpan(0, 0, 5) > mNow) Then
                                                           mListVIewItem.BackColor = Color.FromArgb(255, 255, 255)
                                                       Else
                                                           mListVIewItem.BackColor = Color.FromArgb(255, 192, 192)
                                                       End If
                                                   End If
                                               Next
                                           Finally
                                               ListView_ServiceDevices.EndUpdate()
                                           End Try
                                       End Sub)

                ' List Controllers
                If (True) Then
                    Dim mDevices = g_FormMain.g_mPSMoveServiceCAPI.GetControllersData()

                    For i = 0 To mDevices.Length - 1
                        Dim mDevice As ClassServiceClient.IControllerData = mDevices(i)

                        If (String.IsNullOrEmpty(mDevice.m_Serial) OrElse mDevice.m_Serial.TrimEnd.Length = 0) Then
                            Continue For
                        End If

                        Dim mPos As Vector3 = mDevice.m_Position
                        Dim mAng As Vector3 = mDevice.GetOrientationEuler()
                        Dim iSeqNum As Integer = mDevice.m_OutputSeqNum
                        Dim iFPS As Integer = 0

                        If (True) Then
                            ' Set queue if missing
                            If (Not mFpsCountDic.ContainsKey(mDevice.m_Serial)) Then
                                mFpsCountDic(mDevice.m_Serial) = New Queue(Of KeyValuePair(Of Date, Integer))
                            End If

                            ' Remove old
                            While (mFpsCountDic(mDevice.m_Serial).Count > 0 AndAlso mFpsCountDic(mDevice.m_Serial).Peek().Key + New TimeSpan(0, 0, 1) < mNow)
                                mFpsCountDic(mDevice.m_Serial).Dequeue()
                            End While

                            ' Store fps sequence
                            If (mLastSeqNumDic.ContainsKey(mDevice.m_Serial) AndAlso mLastSeqNumDic(mDevice.m_Serial) <> 0) Then
                                Dim iSeqFps = Math.Max(iSeqNum - mLastSeqNumDic(mDevice.m_Serial), 0)

                                mFpsCountDic(mDevice.m_Serial).Enqueue(New KeyValuePair(Of Date, Integer)(mNow, iSeqFps))
                            End If

                            mLastSeqNumDic(mDevice.m_Serial) = iSeqNum

                            ' Calc FPS
                            For Each mFps In mFpsCountDic(mDevice.m_Serial)
                                iFPS += mFps.Value
                            Next
                        End If

                        Dim iAxisX As Integer = iFrameCount

                        ClassUtils.AsyncInvoke(Sub()
                                                   Dim bFound As Boolean = False

                                                   Dim sTrackingColor As String = "Unknown"
                                                   Select Case (mDevice.m_TrackingColor)
                                                       Case PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSMTrackingColorType.PSMTrackingColorType_Magenta
                                                           sTrackingColor = "Magenta"
                                                       Case PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSMTrackingColorType.PSMTrackingColorType_Cyan
                                                           sTrackingColor = "Cyan"
                                                       Case PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSMTrackingColorType.PSMTrackingColorType_Yellow
                                                           sTrackingColor = "Yellow"
                                                       Case PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSMTrackingColorType.PSMTrackingColorType_Red
                                                           sTrackingColor = "Red"
                                                       Case PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSMTrackingColorType.PSMTrackingColorType_Green
                                                           sTrackingColor = "Green"
                                                       Case PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSMTrackingColorType.PSMTrackingColorType_Blue
                                                           sTrackingColor = "Blue"
                                                   End Select

                                                   ' Change info about device 
                                                   ListView_ServiceDevices.BeginUpdate()
                                                   Try
                                                       For Each mListVIewItem As ListViewItem In ListView_ServiceDevices.Items
                                                           If (mListVIewItem.SubItems(LISTVIEW_SUBITEM_SERIAL).Text = mDevice.m_Serial) Then
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_COLOR).Text = sTrackingColor
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_ID).Text = CStr(mDevice.m_Id)

                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_POSITION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z)))
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_ORIENTATION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z)))
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_BATTERY).Text = CStr(CInt(mDevice.m_BatteryLevel * 100.0F)) & " %"
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_FPS).Text = CStr(iFPS)

                                                               If (mListVIewItem.Tag IsNot Nothing) Then
                                                                   Dim mItemInfo = CType(mListVIewItem.Tag, Dictionary(Of String, Object))
                                                                   mItemInfo("LastTimestamp") = mDevice.m_LastTimeStamp
                                                                   mListVIewItem.Tag = mItemInfo
                                                               End If

                                                               bFound = True
                                                           End If
                                                       Next
                                                   Finally
                                                       ListView_ServiceDevices.EndUpdate()
                                                   End Try

                                                   ' Added device when not found
                                                   If (Not bFound) Then
                                                       Dim sDeviceType As String = "UNKNOWN"

                                                       Select Case (True)
                                                           Case TypeOf mDevice Is ClassServiceClient.STRUC_PSMOVE_CONTROLLER_DATA
                                                               sDeviceType = "PSMOVE"
                                                       End Select

                                                       Dim mListViewItem = New ListViewItem(New String() {
                                                                sDeviceType,
                                                                sTrackingColor,
                                                                CStr(mDevice.m_Id),
                                                                mDevice.m_Serial,
                                                                String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z))),
                                                                String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z))),
                                                                "0 %",
                                                                CStr(iFPS)
                                                            })
                                                       mListViewItem.BackColor = Color.FromArgb(192, 255, 192)

                                                       Dim mItemInfo As New Dictionary(Of String, Object)
                                                       mItemInfo("LastTimestamp") = mDevice.m_LastTimeStamp
                                                       mItemInfo("CreationTimestamp") = mNow
                                                       mListViewItem.Tag = mItemInfo

                                                       ListView_ServiceDevices.Items.Add(mListViewItem)
                                                   End If

                                                   ' Add to chart
                                                   If (ToolStripComboBox_ChartSamples.SelectedItem IsNot Nothing) Then
                                                       Dim iMaxCount As Integer = Math.Max(CInt(ToolStripComboBox_ChartSamples.SelectedItem), 100)

                                                       AddChartValues(mDevice.m_Serial, iFPS, iAxisX, iMaxCount)
                                                   End If
                                               End Sub)
                    Next
                End If

                ' List HMDs
                If (True) Then
                    Dim mDevices = g_FormMain.g_mPSMoveServiceCAPI.GetHmdsData

                    For i = 0 To mDevices.Length - 1
                        Dim mDevice As ClassServiceClient.IHmdData = mDevices(i)

                        If (String.IsNullOrEmpty(mDevice.m_Serial) OrElse mDevice.m_Serial.TrimEnd.Length = 0) Then
                            Continue For
                        End If

                        Dim mPos As Vector3 = mDevice.m_Position
                        Dim mAng As Vector3 = mDevice.GetOrientationEuler()
                        Dim iSeqNum As Integer = mDevice.m_OutputSeqNum
                        Dim iFPS As Integer = 0

                        If (True) Then
                            ' Set queue if missing
                            If (Not mFpsCountDic.ContainsKey(mDevice.m_Serial)) Then
                                mFpsCountDic(mDevice.m_Serial) = New Queue(Of KeyValuePair(Of Date, Integer))
                            End If

                            ' Remove old
                            While (mFpsCountDic(mDevice.m_Serial).Count > 0 AndAlso mFpsCountDic(mDevice.m_Serial).Peek().Key + New TimeSpan(0, 0, 1) < mNow)
                                mFpsCountDic(mDevice.m_Serial).Dequeue()
                            End While

                            ' Store fps sequence
                            If (mLastSeqNumDic.ContainsKey(mDevice.m_Serial) AndAlso mLastSeqNumDic(mDevice.m_Serial) <> 0) Then
                                Dim iSeqFps = Math.Max(iSeqNum - mLastSeqNumDic(mDevice.m_Serial), 0)

                                mFpsCountDic(mDevice.m_Serial).Enqueue(New KeyValuePair(Of Date, Integer)(mNow, iSeqFps))
                            End If

                            mLastSeqNumDic(mDevice.m_Serial) = iSeqNum

                            ' Calc FPS
                            For Each mFps In mFpsCountDic(mDevice.m_Serial)
                                iFPS += mFps.Value
                            Next
                        End If

                        Dim iAxisX As Integer = iFrameCount

                        ClassUtils.AsyncInvoke(Sub()
                                                   Dim bFound As Boolean = False

                                                   ' Change info about device 
                                                   ListView_ServiceDevices.BeginUpdate()
                                                   Try
                                                       For Each mListVIewItem As ListViewItem In ListView_ServiceDevices.Items
                                                           If (mListVIewItem.SubItems(LISTVIEW_SUBITEM_SERIAL).Text = mDevice.m_Serial) Then
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_COLOR).Text = "N/A"
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_ID).Text = CStr(mDevice.m_Id)

                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_POSITION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z)))
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_ORIENTATION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z)))
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_BATTERY).Text = "N/A"
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_FPS).Text = CStr(iFPS)

                                                               If (mListVIewItem.Tag IsNot Nothing) Then
                                                                   Dim mItemInfo = CType(mListVIewItem.Tag, Dictionary(Of String, Object))
                                                                   mItemInfo("LastTimestamp") = mDevice.m_LastTimeStamp
                                                                   mListVIewItem.Tag = mItemInfo
                                                               End If

                                                               bFound = True
                                                           End If
                                                       Next
                                                   Finally
                                                       ListView_ServiceDevices.EndUpdate()
                                                   End Try

                                                   ' Added device when not found
                                                   If (Not bFound) Then
                                                       Dim sDeviceType As String = "UNKNOWN"

                                                       Select Case (True)
                                                           Case TypeOf mDevice Is ClassServiceClient.STRUC_MORPHEUS_HMD_DATA
                                                               sDeviceType = "MORPHEUS"
                                                           Case TypeOf mDevice Is ClassServiceClient.STRUC_VIRTUAL_HMD_DATA
                                                               sDeviceType = "VIRTUAL"
                                                       End Select

                                                       Dim mListViewItem = New ListViewItem(New String() {
                                                                sDeviceType,
                                                                "N/A",
                                                                CStr(mDevice.m_Id),
                                                                mDevice.m_Serial,
                                                                String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z))),
                                                                String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z))),
                                                                "N/A",
                                                                CStr(iFPS)
                                                            })
                                                       mListViewItem.BackColor = Color.FromArgb(192, 255, 192)

                                                       Dim mItemInfo As New Dictionary(Of String, Object)
                                                       mItemInfo("LastTimestamp") = mDevice.m_LastTimeStamp
                                                       mItemInfo("CreationTimestamp") = mNow
                                                       mListViewItem.Tag = mItemInfo

                                                       ListView_ServiceDevices.Items.Add(mListViewItem)
                                                   End If

                                                   ' Add to chart
                                                   If (ToolStripComboBox_ChartSamples.SelectedItem IsNot Nothing) Then
                                                       Dim iMaxCount As Integer = Math.Max(CInt(ToolStripComboBox_ChartSamples.SelectedItem), 100)

                                                       AddChartValues(mDevice.m_Serial, iFPS, iAxisX, iMaxCount)
                                                   End If
                                               End Sub)
                    Next
                End If


                ' List Trackers
                If (True) Then
                    Dim mDevices = g_FormMain.g_mPSMoveServiceCAPI.GetTrackersData

                    For i = 0 To mDevices.Length - 1
                        Dim mDevice As ClassServiceClient.ITrackerData = mDevices(i)

                        If (String.IsNullOrEmpty(mDevice.m_Path) OrElse mDevice.m_Path.TrimEnd.Length = 0) Then
                            Continue For
                        End If

                        Dim mPos As Vector3 = mDevice.m_Position
                        Dim mAng As Vector3 = mDevice.GetOrientationEuler()
                        Dim iSeqNum As Integer = mDevice.m_OutputSeqNum
                        Dim iFPS As Integer = 0

                        If (True) Then
                            ' Set queue if missing
                            If (Not mFpsCountDic.ContainsKey(mDevice.m_Path)) Then
                                mFpsCountDic(mDevice.m_Path) = New Queue(Of KeyValuePair(Of Date, Integer))
                            End If

                            ' Remove old
                            While (mFpsCountDic(mDevice.m_Path).Count > 0 AndAlso mFpsCountDic(mDevice.m_Path).Peek().Key + New TimeSpan(0, 0, 1) < mNow)
                                mFpsCountDic(mDevice.m_Path).Dequeue()
                            End While

                            ' Store fps sequence
                            If (mLastSeqNumDic.ContainsKey(mDevice.m_Path) AndAlso mLastSeqNumDic(mDevice.m_Path) <> 0) Then
                                Dim iSeqFps = Math.Max(iSeqNum - mLastSeqNumDic(mDevice.m_Path), 0)

                                mFpsCountDic(mDevice.m_Path).Enqueue(New KeyValuePair(Of Date, Integer)(mNow, iSeqFps))
                            End If

                            mLastSeqNumDic(mDevice.m_Path) = iSeqNum

                            ' Calc FPS
                            For Each mFps In mFpsCountDic(mDevice.m_Path)
                                iFPS += mFps.Value
                            Next
                        End If

                        Dim iAxisX As Integer = iFrameCount

                        ClassUtils.AsyncInvoke(Sub()
                                                   Dim bFound As Boolean = False

                                                   ' Change info about device 
                                                   ListView_ServiceDevices.BeginUpdate()
                                                   Try
                                                       For Each mListVIewItem As ListViewItem In ListView_ServiceDevices.Items
                                                           If (mListVIewItem.SubItems(LISTVIEW_SUBITEM_SERIAL).Text = mDevice.m_Path) Then
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_COLOR).Text = "N/A"
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_ID).Text = CStr(mDevice.m_Id)

                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_POSITION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z)))
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_ORIENTATION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z)))
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_BATTERY).Text = "N/A"
                                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_FPS).Text = CStr(iFPS)

                                                               If (mListVIewItem.Tag IsNot Nothing) Then
                                                                   Dim mItemInfo = CType(mListVIewItem.Tag, Dictionary(Of String, Object))
                                                                   mItemInfo("LastTimestamp") = mDevice.m_LastTimeStamp
                                                                   mListVIewItem.Tag = mItemInfo
                                                               End If

                                                               bFound = True
                                                           End If
                                                       Next
                                                   Finally
                                                       ListView_ServiceDevices.EndUpdate()
                                                   End Try

                                                   ' Added device when not found
                                                   If (Not bFound) Then
                                                       Dim mListViewItem = New ListViewItem(New String() {
                                                                "TRACKER",
                                                                "N/A",
                                                                CStr(mDevice.m_Id),
                                                                mDevice.m_Path,
                                                                String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z))),
                                                                String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z))),
                                                                "N/A",
                                                                CStr(iFPS)
                                                            })
                                                       mListViewItem.BackColor = Color.FromArgb(192, 255, 192)

                                                       Dim mItemInfo As New Dictionary(Of String, Object)
                                                       mItemInfo("LastTimestamp") = mDevice.m_LastTimeStamp
                                                       mItemInfo("CreationTimestamp") = mNow
                                                       mListViewItem.Tag = mItemInfo

                                                       ListView_ServiceDevices.Items.Add(mListViewItem)
                                                   End If

                                                   ' Add to chart
                                                   If (ToolStripComboBox_ChartSamples.SelectedItem IsNot Nothing) Then
                                                       Dim iMaxCount As Integer = Math.Max(CInt(ToolStripComboBox_ChartSamples.SelectedItem), 100)

                                                       AddChartValues(mDevice.m_Path, iFPS, iAxisX, iMaxCount)
                                                   End If
                                               End Sub)
                    Next
                End If

            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLog(ex)
            End Try

            iFrameCount += 1
            Threading.Thread.Sleep(500)
        End While
    End Sub

    Private Sub AddChartValues(sSeriesName As String, iValue As Integer, iCount As Integer, iMaxCount As Integer)
        If (Not Chart_ServicePerformance.Enabled) Then
            Return
        End If

        If (Chart_ServicePerformance.ChartAreas.Count < 1) Then
            Return
        End If

        If (Chart_ServicePerformance.Series.IndexOf(sSeriesName) = -1) Then
            Dim mSeries = Chart_ServicePerformance.Series.Add(sSeriesName)

            mSeries.ChartArea = Chart_ServicePerformance.ChartAreas(0).Name
            mSeries.ChartType = DataVisualization.Charting.SeriesChartType.FastLine
            mSeries.BorderWidth = 2
        End If

        Chart_ServicePerformance.Series(sSeriesName).Points.AddXY(iCount, iValue)

        While (Chart_ServicePerformance.Series(sSeriesName).Points.Count > 1 AndAlso
            Chart_ServicePerformance.Series(sSeriesName).Points(1).XValue < (iCount - iMaxCount))
            Chart_ServicePerformance.Series(sSeriesName).Points.RemoveAt(0)
        End While

        ' Adjust bounds
        Dim mAxisX = Chart_ServicePerformance.ChartAreas(0).AxisX()
        If (mAxisX.Maximum < iCount) Then
            mAxisX.Maximum = iCount
        End If
        If (mAxisX.Minimum < (iCount - iMaxCount)) Then
            mAxisX.Minimum = iCount - iMaxCount
        End If

        Dim mAxisY = Chart_ServicePerformance.ChartAreas(0).AxisY()
        Dim iValueMax = Math.Ceiling(iValue * 0.1) * 10
        Dim iValueMin = Math.Floor(iValue * 0.1) * 10

        If (mAxisY.Maximum < iValueMax) Then
            mAxisY.Maximum = iValueMax
        End If
        If (mAxisY.Minimum > iValueMin) Then
            mAxisY.Minimum = iValueMin
        End If
    End Sub

    Private Sub CleanUp()
        If (g_mDriverInstallTimerThread IsNot Nothing AndAlso g_mDriverInstallTimerThread.IsAlive) Then
            g_mDriverInstallTimerThread.Abort()
            g_mDriverInstallTimerThread.Join()
            g_mDriverInstallTimerThread = Nothing
        End If

        If (g_mRuntimeDiagnostics IsNot Nothing AndAlso g_mRuntimeDiagnostics.IsAlive) Then
            g_mRuntimeDiagnostics.Abort()
            g_mRuntimeDiagnostics.Join()
            g_mRuntimeDiagnostics = Nothing
        End If

        If (g_mServiceDeviceStatusThread IsNot Nothing AndAlso g_mServiceDeviceStatusThread.IsAlive) Then
            g_mServiceDeviceStatusThread.Abort()
            g_mServiceDeviceStatusThread.Join()
            g_mServiceDeviceStatusThread = Nothing
        End If

        If (g_mStatusThread IsNot Nothing AndAlso g_mStatusThread.IsAlive) Then
            g_mStatusThread.Abort()
            g_mStatusThread.Join()
            g_mStatusThread = Nothing
        End If

        If (g_mUpdateInstallThread IsNot Nothing AndAlso g_mUpdateInstallThread.IsAlive) Then
            g_mUpdateInstallThread.Abort()
            g_mUpdateInstallThread.Join()
            g_mUpdateInstallThread = Nothing
        End If

        If (g_mDriverInstallThread IsNot Nothing AndAlso g_mDriverInstallThread.IsAlive) Then
            g_mDriverInstallThread.Abort()
            g_mDriverInstallThread.Join()
            g_mDriverInstallThread = Nothing
        End If
    End Sub

    Public Sub LinkLabel_ServiceRun_Click()
        LinkLabel_ServiceRun_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_ServiceRun_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ServiceRun.LinkClicked
        If (g_mFormRestart IsNot Nothing AndAlso Not g_mFormRestart.IsDisposed) Then
            Return
        End If

        Try
            RunService(True)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub LinkLabel_ServiceRunCmd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ServiceRunCmd.LinkClicked
        If (g_mFormRestart IsNot Nothing AndAlso Not g_mFormRestart.IsDisposed) Then
            Return
        End If

        Try
            RunService(False)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub LinkLabel_ConfigToolRunCmd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ConfigToolRunCmd.LinkClicked
        Try
            RunServiceConfigTool(False)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Public Sub LinkLabel_ServiceRestart_Click()
        LinkLabel_ServiceRestart_LinkClicked(Nothing, Nothing)
    End Sub

    Public Sub RunService(bHidden As Boolean)
        Dim mServiceInfo As New ClassServiceInfo
        If (mServiceInfo.IsServiceRunning <> ClassServiceInfo.ENUM_SERVICE_PROCESS_TYPE.NONE) Then
            Return
        End If

        Dim mConfig As New ClassServiceInfo
        mConfig.LoadConfig()

        If (Not mConfig.FileExist()) Then
            If (mConfig.FindByProcess()) Then
                mConfig.SaveConfig()
            Else
                If (mConfig.SearchForService) Then
                    mConfig.SaveConfig()
                Else
                    Return
                End If
            End If
        End If

        Using mProcess As New Process
            mProcess.StartInfo.FileName = mConfig.m_FileName
            mProcess.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(mConfig.m_FileName)
            mProcess.StartInfo.UseShellExecute = False
            mProcess.StartInfo.CreateNoWindow = bHidden

            mProcess.Start()
        End Using
    End Sub

    Public Sub RunServiceConfigTool(bHidden As Boolean)
        Dim mServiceInfo As New ClassServiceInfo
        If (mServiceInfo.IsConfigToolRunning()) Then
            Dim sMsg As New Text.StringBuilder
            sMsg.AppendLine("PSMoveServiceEx Config Tool is already running!")
            sMsg.AppendLine("Do you want to run another instance?")
            If (MessageBox.Show(sMsg.ToString, "Instance already running", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
                Return
            End If
        End If

        Dim mUCVirtualMotionTracker = g_FormMain.g_mUCVirtualMotionTracker
        If (mUCVirtualMotionTracker.g_ClassOscServer.IsRunning AndAlso Not mUCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
            Dim sMsg As New Text.StringBuilder
            sMsg.AppendLine("The Virtual Motion Tracker OSC server is currently running which causes PSMoveServiceEx tracking to be active!")
            sMsg.AppendLine("Having tracking active while working in PSMoveServiceEx Config Tool is not recommended and can lead to problems or bad calibrations.")
            sMsg.AppendLine()
            sMsg.AppendLine("Do you want to pause the Virtual Motion Tracker OSC server and run PSMoveServiceEx Config Tool?")
            sMsg.AppendLine()
            sMsg.AppendLine("(You need to manually start the Virtual Motion Tracker OSC server again if you choose to pause it.)")
            Select Case (MessageBox.Show(sMsg.ToString, "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                Case DialogResult.Yes
                    mUCVirtualMotionTracker.g_UCVmtManagement.LinkLabel_OscPause_Click()
                Case DialogResult.No
                    ' Ignor!
                Case Else
                    Return
            End Select
        End If

        Dim mConfig As New ClassServiceInfo
        mConfig.LoadConfig()

        If (Not mConfig.FileExist()) Then
            If (mConfig.FindByProcess()) Then
                mConfig.SaveConfig()
            Else
                If (mConfig.SearchForService) Then
                    mConfig.SaveConfig()
                Else
                    Return
                End If
            End If
        End If

        Dim sFilePath As String = IO.Path.Combine(IO.Path.GetDirectoryName(mConfig.m_FileName), "PSMoveConfigTool.exe")
        If (Not IO.File.Exists(sFilePath)) Then
            Throw New ArgumentException("PSMoveConfigTool.exe does not exist!")
        End If

        Using mProcess As New Process
            mProcess.StartInfo.FileName = sFilePath
            mProcess.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(sFilePath)
            mProcess.StartInfo.Arguments = "\autoConnect"
            mProcess.StartInfo.UseShellExecute = False
            mProcess.StartInfo.CreateNoWindow = bHidden

            mProcess.Start()
        End Using
    End Sub

    Private Sub LinkLabel_ServiceRestart_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ServiceRestart.LinkClicked
        If (g_mFormRestart IsNot Nothing AndAlso Not g_mFormRestart.IsDisposed) Then
            Return
        End If

        Try
            StopService()

            g_mFormRestart = New FormLoading
            g_mFormRestart.Text = "Restarting PSMoveServiceEx..."
            g_mFormRestart.m_ProgressBar.Style = ProgressBarStyle.Blocks
            g_mFormRestart.m_ProgressBar.Value = 0
            g_mFormRestart.m_ProgressBar.Maximum = 100
            g_mFormRestart.Show(Me)

            Timer_RestartPsms.Enabled = True
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

    End Sub

    Private Sub Timer_RestartPsms_Tick(sender As Object, e As EventArgs) Handles Timer_RestartPsms.Tick
        If (g_mFormRestart Is Nothing OrElse g_mFormRestart.IsDisposed) Then
            Timer_RestartPsms.Enabled = False
            Return
        End If

        If (g_mFormRestart.m_ProgressBar.Value < g_mFormRestart.m_ProgressBar.Maximum) Then
            g_mFormRestart.m_ProgressBar.Increment(25)
            g_mFormRestart.SkipProgressBarAnimation()

            Return
        End If

        Timer_RestartPsms.Enabled = False

        g_mFormRestart.Dispose()
        g_mFormRestart = Nothing

        Try
            RunService(True)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Public Sub LinkLabel_ServiceStop_Click()
        LinkLabel_ServiceStop_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_ServiceStop_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ServiceStop.LinkClicked
        If (g_mFormRestart IsNot Nothing AndAlso Not g_mFormRestart.IsDisposed) Then
            Return
        End If

        Try
            StopService()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Public Sub StopService()
        Dim mServiceInfo As New ClassServiceInfo
        Select Case (mServiceInfo.IsServiceRunning())
            Case ClassServiceInfo.ENUM_SERVICE_PROCESS_TYPE.NORMAL
                For Each mProcess In Process.GetProcessesByName("PSMoveService")
                    mProcess.Kill()
                Next
            Case ClassServiceInfo.ENUM_SERVICE_PROCESS_TYPE.ADMIN
                For Each mProcess In Process.GetProcessesByName("PSMoveServiceAdmin")
                    mProcess.Kill()
                Next
        End Select
    End Sub

    Public Sub LinkLabel_ConfigToolRun_Click()
        LinkLabel_ConfigToolRun_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_ConfigToolRun_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ConfigToolRun.LinkClicked
        Try
            RunServiceConfigTool(True)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub LinkLabel_ConfigToolClose_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ConfigToolClose.LinkClicked
        Try
            Dim mProcesses As Process() = Process.GetProcessesByName("PSMoveConfigTool")
            Dim mServiceInfo As New ClassServiceInfo
            If (Not mServiceInfo.IsConfigToolRunning()) Then
                Return
            End If

            For Each mProcess In mProcesses
                mProcess.Kill()
            Next
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Public Sub LinkLabel_InstallDrivers_Click()
        LinkLabel_InstallDrivers_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_InstallDrivers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_InstallPSEyeDrivers.LinkClicked
        If (g_mDriverInstallThread IsNot Nothing AndAlso g_mDriverInstallThread.IsAlive) Then
            Return
        End If

        g_mDriverInstallThread = New Threading.Thread(
            Sub()
                Try
                    Dim mDriverInstaller As New ClassLibusbDriver

                    Dim mServiceInfo As New ClassServiceInfo
                    If (mServiceInfo.IsServiceRunning() <> ClassServiceInfo.ENUM_SERVICE_PROCESS_TYPE.NONE) Then
                        Throw New ArgumentException("PSMoveServiceEx is running. Please close PSMoveServiceEx!")
                    End If

                    If (mDriverInstaller.VerifyPlaystationEyeDriver64() = ClassLibusbDriver.ENUM_DRIVER_STATE.INSTALLED) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("It seems like you already have all the necessary LibUSB drivers for PlayStation Eye Cameras installed!")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to reinstall all drivers for those devices?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Installation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
                            Return
                        End If
                    End If

                    If (True) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("You are about to install LibUSB drivers for Playstation Eye Cameras.")
                        sMessage.AppendLine("Already existing Playstation Eye drivers will be replaced!")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to continue?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Installation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                            Return
                        End If
                    End If

                    If (True) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("WARNING!")
                        sMessage.AppendLine("The Playstation Eye driver installation might trigger sensitive Anti-Virus programs!")
                        sMessage.AppendLine("It's recommended to whitelist the Virtual Device Manager folder before starting the installation to avoid any issues.")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to continue?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Installation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                            Return
                        End If
                    End If

                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If

                                               g_mDriverInstallFormLoad = New FormLoading
                                               g_mDriverInstallFormLoad.Text = "Installing drivers..."
                                               g_mDriverInstallFormLoad.m_ProgressBar.Style = ProgressBarStyle.Blocks
                                               g_mDriverInstallFormLoad.m_ProgressBar.Maximum = (DRIVER_INSTALLER_TIME * ClassLibusbDriver.DRV_PSEYE_LIBUSB_CONFIGS.Length * DRIVER_INSTALLER_MULTI)
                                               g_mDriverInstallFormLoad.m_ProgressBar.Value = 0
                                               g_mDriverInstallFormLoad.ShowDialog(g_FormMain)
                                           End Sub)

                    SetDriverInstallerThread(True)

                    Dim iExitCode As Integer = ClassUtils.RunWithAdmin(New String() {FormMain.COMMANDLINE_INSTALL_PSEYE_DRIVERS, FormMain.COMMANDLINE_VERBOSE})

                    SetDriverInstallerThread(False)

                    ' Verbose already shows errors messages
                    If (iExitCode = 0) Then
                        MessageBox.Show("Drivers installed successfully!", "Driver Installation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                Finally
                    SetDriverInstallerThread(False)

                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If
                                           End Sub)
                End Try
            End Sub)
        g_mDriverInstallThread.IsBackground = True
        g_mDriverInstallThread.Start()
    End Sub

    Public Sub LinkLabel_InstallPSVRDrivers_Click()
        LinkLabel_InstallPSVRDrivers_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_InstallPSVRDrivers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        If (g_mDriverInstallThread IsNot Nothing AndAlso g_mDriverInstallThread.IsAlive) Then
            Return
        End If

        g_mDriverInstallThread = New Threading.Thread(
            Sub()
                Try
                    Dim mDriverInstaller As New ClassLibusbDriver

                    Dim mServiceInfo As New ClassServiceInfo
                    If (mServiceInfo.IsServiceRunning() <> ClassServiceInfo.ENUM_SERVICE_PROCESS_TYPE.NONE) Then
                        Throw New ArgumentException("PSMoveServiceEx is running. Please close PSMoveServiceEx!")
                    End If

                    If (mDriverInstaller.VerifyPlaystationVrDriver64() = ClassLibusbDriver.ENUM_DRIVER_STATE.INSTALLED) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("It seems like you already have all the necessary LibUSB drivers for PlayStation VR installed!")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to reinstall all drivers for those devices?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Installation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
                            Return
                        End If
                    End If

                    If (True) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("You are about to install LibUSB drivers for PlayStation VR.")
                        sMessage.AppendLine("Already existing PlayStation VR drivers will be replaced!")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to continue?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Installation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                            Return
                        End If
                    End If

                    If (True) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("WARNING!")
                        sMessage.AppendLine("The PlayStation VR driver installation might trigger sensitive Anti-Virus programs!")
                        sMessage.AppendLine("It's recommended to whitelist the Virtual Device Manager folder before starting the installation to avoid any issues.")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to continue?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Installation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                            Return
                        End If
                    End If

                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If

                                               g_mDriverInstallFormLoad = New FormLoading
                                               g_mDriverInstallFormLoad.Text = "Installing drivers..."
                                               g_mDriverInstallFormLoad.m_ProgressBar.Style = ProgressBarStyle.Blocks
                                               g_mDriverInstallFormLoad.m_ProgressBar.Maximum = (DRIVER_INSTALLER_TIME * ClassLibusbDriver.DRV_PSVR_LIBUSB_CONFIGS.Length * DRIVER_INSTALLER_MULTI)
                                               g_mDriverInstallFormLoad.m_ProgressBar.Value = 0
                                               g_mDriverInstallFormLoad.ShowDialog(g_FormMain)
                                           End Sub)

                    SetDriverInstallerThread(True)

                    Dim iExitCode As Integer = ClassUtils.RunWithAdmin(New String() {FormMain.COMMANDLINE_INSTALL_PSVR_DRIVERS, FormMain.COMMANDLINE_VERBOSE})

                    SetDriverInstallerThread(False)

                    ' Verbose already shows errors messages
                    If (iExitCode = 0) Then
                        MessageBox.Show("Drivers installed successfully!", "Driver Installation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                Finally
                    SetDriverInstallerThread(False)

                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If
                                           End Sub)
                End Try
            End Sub)
        g_mDriverInstallThread.IsBackground = True
        g_mDriverInstallThread.Start()
    End Sub


    Public Sub LinkLabel_InstallPS4CamDrivers_Click()
        LinkLabel_InstallPS4CamDrivers_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_InstallPS4CamDrivers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_InstallPS4CamDrivers.LinkClicked
        If (g_mDriverInstallThread IsNot Nothing AndAlso g_mDriverInstallThread.IsAlive) Then
            Return
        End If

        g_mDriverInstallThread = New Threading.Thread(
            Sub()
                Try
                    Dim mDriverInstaller As New ClassLibusbDriver

                    Dim mServiceInfo As New ClassServiceInfo
                    If (mServiceInfo.IsServiceRunning() <> ClassServiceInfo.ENUM_SERVICE_PROCESS_TYPE.NONE) Then
                        Throw New ArgumentException("PSMoveServiceEx is running. Please close PSMoveServiceEx!")
                    End If

                    If (mDriverInstaller.VerifyPlaystation4CamDriver64() = ClassLibusbDriver.ENUM_DRIVER_STATE.INSTALLED) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("It seems like you already have all the necessary WinUSB drivers for PlayStation Stereo Cameras installed!")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to reinstall all drivers for those devices?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Installation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
                            Return
                        End If
                    End If

                    If (True) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("You are about to install WinUSB drivers for Playstation Stereo Cameras.")
                        sMessage.AppendLine("Already existing Playstation Stereo Cameras drivers will be replaced!")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to continue?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Installation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                            Return
                        End If
                    End If

                    If (True) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("WARNING!")
                        sMessage.AppendLine("The Playstation Stereo Camera driver installation might trigger sensitive Anti-Virus programs!")
                        sMessage.AppendLine("It's recommended to whitelist the Virtual Device Manager folder before starting the installation to avoid any issues.")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to continue?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Installation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                            Return
                        End If
                    End If

                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If

                                               g_mDriverInstallFormLoad = New FormLoading
                                               g_mDriverInstallFormLoad.Text = "Installing drivers..."
                                               g_mDriverInstallFormLoad.m_ProgressBar.Style = ProgressBarStyle.Blocks
                                               g_mDriverInstallFormLoad.m_ProgressBar.Maximum = (DRIVER_INSTALLER_TIME * ClassLibusbDriver.DRV_PS4CAM_WINUSB_CONFIGS.Length * DRIVER_INSTALLER_MULTI)
                                               g_mDriverInstallFormLoad.m_ProgressBar.Value = 0
                                               g_mDriverInstallFormLoad.ShowDialog(g_FormMain)
                                           End Sub)

                    SetDriverInstallerThread(True)

                    Dim iExitCode As Integer = ClassUtils.RunWithAdmin(New String() {FormMain.COMMANDLINE_INSTALL_PS4CAM_DRIVERS, FormMain.COMMANDLINE_VERBOSE})

                    SetDriverInstallerThread(False)

                    ' Verbose already shows errors messages
                    If (iExitCode = 0) Then
                        MessageBox.Show("Drivers installed successfully!", "Driver Installation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                Finally
                    SetDriverInstallerThread(False)

                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If
                                           End Sub)
                End Try
            End Sub)
        g_mDriverInstallThread.IsBackground = True
        g_mDriverInstallThread.Start()
    End Sub

    Public Sub LinkLabel_UninstallPS4CamDrivers_Click()
        LinkLabel_UninstallPS4CamDrivers_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_UninstallPS4CamDrivers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_UninstallPS4CamDrivers.LinkClicked
        If (g_mDriverInstallThread IsNot Nothing AndAlso g_mDriverInstallThread.IsAlive) Then
            Return
        End If

        g_mDriverInstallThread = New Threading.Thread(
            Sub()
                Try
                    If (True) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("You are about to uninstall PlayStation Stereo Camera drivers.")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to continue?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Uninstallation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                            Return
                        End If
                    End If

                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If

                                               g_mDriverInstallFormLoad = New FormLoading
                                               g_mDriverInstallFormLoad.Text = "Uninstalling drivers..."
                                               g_mDriverInstallFormLoad.ShowDialog(g_FormMain)
                                           End Sub)

                    Dim iExitCode As Integer = ClassUtils.RunWithAdmin(New String() {FormMain.COMMANDLINE_UNINSTALL_PS4CAM, FormMain.COMMANDLINE_VERBOSE})

                    ' Verbose already shows errors messages
                    If (iExitCode = 0) Then
                        MessageBox.Show("Drivers uninstalled successfully!", "Driver Uninstallation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                Finally
                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If
                                           End Sub)
                End Try
            End Sub)
        g_mDriverInstallThread.IsBackground = True
        g_mDriverInstallThread.Start()
    End Sub

    Public Sub LinkLabel_ConfigPSVRDisplay_Click()
        LinkLabel_ConfigPSVRDisplay_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_ConfigPSVRDisplay_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        If (g_mDriverInstallThread IsNot Nothing AndAlso g_mDriverInstallThread.IsAlive) Then
            Return
        End If

        g_mDriverInstallThread = New Threading.Thread(
            Sub()
                Try
                    Dim mMonitor As New ClassMonitor

                    Dim mServiceInfo As New ClassServiceInfo
                    If (mServiceInfo.IsServiceRunning() <> ClassServiceInfo.ENUM_SERVICE_PROCESS_TYPE.NONE) Then
                        Throw New ArgumentException("PSMoveServiceEx is running. Please close PSMoveServiceEx!")
                    End If

                    Dim bUseDirectMode As Boolean = False
                    Using i As New FormDisplayModeSelection
                        If (i.ShowDialog = DialogResult.OK) Then
                            bUseDirectMode = i.m_ResultDirectMode
                        Else
                            Return
                        End If
                    End Using

                    Dim iMonitorPatch = mMonitor.IsPlaystationVrMonitorPatched()

                    If (iMonitorPatch = ClassMonitor.ENUM_PATCHED_RESGITRY_STATE.WAITING_FOR_RELOAD OrElse
                            (bUseDirectMode AndAlso iMonitorPatch = ClassMonitor.ENUM_PATCHED_RESGITRY_STATE.PATCHED_DIRECT) OrElse
                            (Not bUseDirectMode AndAlso iMonitorPatch = ClassMonitor.ENUM_PATCHED_RESGITRY_STATE.PATCHED_MULTI)) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("It seems like you already have the PlayStation VR display configured!")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to re-configure the PlayStation VR display?")
                        If (MessageBox.Show(sMessage.ToString, "Configuration Installation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
                            Return
                        End If
                    End If

                    If (True) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("You are about to install display configurations for PlayStation VR.")
                        sMessage.AppendLine("Already existing PlayStation VR display configurations will be replaced!")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to continue?")
                        If (MessageBox.Show(sMessage.ToString, "Configuration Installation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                            Return
                        End If
                    End If

                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If

                                               g_mDriverInstallFormLoad = New FormLoading
                                               g_mDriverInstallFormLoad.Text = "Installing configuration..."
                                               g_mDriverInstallFormLoad.ShowDialog(g_FormMain)
                                           End Sub)

                    Dim sMode As String
                    If (bUseDirectMode) Then
                        sMode = FormMain.COMMANDLINE_PATCH_PSVR_MONITOR_DIRECT
                    Else
                        sMode = FormMain.COMMANDLINE_PATCH_PSVR_MONITOR_MULTI
                    End If

                    Dim iExitCode As Integer = ClassUtils.RunWithAdmin(New String() {sMode, FormMain.COMMANDLINE_VERBOSE})

                    ' Verbose already shows errors messages
                    Select Case (iExitCode)
                        Case 0 'Success
                            MessageBox.Show("Configuration installed successfully!", "Configuration Installation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Case 1 ' Finished but unknown status
                            MessageBox.Show("The configuration installation is complete, but the result of the installation cannot be retrieved because it was run with SYSTEM privileges.", "Configuration Installation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Select
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                Finally
                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If
                                           End Sub)
                End Try
            End Sub)
        g_mDriverInstallThread.IsBackground = True
        g_mDriverInstallThread.Start()
    End Sub

    Public Sub LinkLabel_UninstallPSEyeDrivers_Click()
        LinkLabel_UninstallPSEyeDrivers_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_UninstallPSEyeDrivers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_UninstallPSEyeDrivers.LinkClicked
        If (g_mDriverInstallThread IsNot Nothing AndAlso g_mDriverInstallThread.IsAlive) Then
            Return
        End If

        g_mDriverInstallThread = New Threading.Thread(
            Sub()
                Try
                    If (True) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("You are about to uninstall PlayStation Eye drivers.")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to continue?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Uninstallation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                            Return
                        End If
                    End If

                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If

                                               g_mDriverInstallFormLoad = New FormLoading
                                               g_mDriverInstallFormLoad.Text = "Uninstalling drivers..."
                                               g_mDriverInstallFormLoad.ShowDialog(g_FormMain)
                                           End Sub)

                    Dim iExitCode As Integer = ClassUtils.RunWithAdmin(New String() {FormMain.COMMANDLINE_UNINSTALL_PSEYE, FormMain.COMMANDLINE_VERBOSE})

                    ' Verbose already shows errors messages
                    If (iExitCode = 0) Then
                        MessageBox.Show("Drivers uninstalled successfully!", "Driver Uninstallation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                Finally
                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If
                                           End Sub)
                End Try
            End Sub)
        g_mDriverInstallThread.IsBackground = True
        g_mDriverInstallThread.Start()
    End Sub

    Public Sub LinkLabel_UninstallPSVRDrivers_Click()
        LinkLabel_UninstallPSVRDrivers_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_UninstallPSVRDrivers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        If (g_mDriverInstallThread IsNot Nothing AndAlso g_mDriverInstallThread.IsAlive) Then
            Return
        End If

        g_mDriverInstallThread = New Threading.Thread(
            Sub()
                Try
                    If (True) Then
                        Dim sMessage As New Text.StringBuilder
                        sMessage.AppendLine("You are about to uninstall PlayStation VR drivers and display configurations.")
                        sMessage.AppendLine()
                        sMessage.AppendLine("Do you want to continue?")
                        If (MessageBox.Show(sMessage.ToString, "Driver Uninstallation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                            Return
                        End If
                    End If

                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If

                                               g_mDriverInstallFormLoad = New FormLoading
                                               g_mDriverInstallFormLoad.Text = "Uninstalling drivers and display configurations..."
                                               g_mDriverInstallFormLoad.ShowDialog(g_FormMain)
                                           End Sub)

                    Dim iExitCode As Integer = ClassUtils.RunWithAdmin(New String() {FormMain.COMMANDLINE_UNINSTALL_PSVR, FormMain.COMMANDLINE_VERBOSE})

                    ' Verbose already shows errors messages
                    If (iExitCode = 0) Then
                        MessageBox.Show("Drivers uninstalled successfully!", "Driver Uninstallation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                Finally
                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                                   g_mDriverInstallFormLoad.Dispose()
                                                   g_mDriverInstallFormLoad = Nothing
                                               End If
                                           End Sub)
                End Try
            End Sub)
        g_mDriverInstallThread.IsBackground = True
        g_mDriverInstallThread.Start()
    End Sub

    Private Sub SetDriverInstallerThread(bEnabled As Boolean)
        If (bEnabled) Then
            If (g_mDriverInstallTimerThread IsNot Nothing AndAlso g_mDriverInstallTimerThread.IsAlive) Then
                g_mDriverInstallTimerThread.Abort()
                g_mDriverInstallTimerThread.Join()
                g_mDriverInstallTimerThread = Nothing
            End If
            g_mDriverInstallTimerThread = New Threading.Thread(Sub()
                                                                   Try
                                                                       While True
                                                                           If (Process.GetProcessesByName(IO.Path.GetFileNameWithoutExtension(ClassLibusbDriver.DRV_WDI_INSTALLER_NAME)).Count > 0) Then
                                                                               ClassUtils.AsyncInvoke(Sub()
                                                                                                          If (g_mDriverInstallFormLoad Is Nothing OrElse g_mDriverInstallFormLoad.IsDisposed) Then
                                                                                                              Return
                                                                                                          End If

                                                                                                          If (g_mDriverInstallFormLoad.m_ProgressBar.Style = ProgressBarStyle.Blocks) Then
                                                                                                              If (g_mDriverInstallFormLoad.m_ProgressBar.Value < (g_mDriverInstallFormLoad.m_ProgressBar.Maximum * 0.75)) Then
                                                                                                                  g_mDriverInstallFormLoad.m_ProgressBar.Increment(DRIVER_INSTALLER_MULTI)
                                                                                                              ElseIf (g_mDriverInstallFormLoad.m_ProgressBar.Value < (g_mDriverInstallFormLoad.m_ProgressBar.Maximum * 0.95)) Then
                                                                                                                  g_mDriverInstallFormLoad.m_ProgressBar.Increment(CInt(DRIVER_INSTALLER_MULTI / 2))
                                                                                                              End If
                                                                                                          End If
                                                                                                      End Sub)
                                                                           End If

                                                                           Threading.Thread.Sleep(1000)
                                                                       End While
                                                                   Catch ex As Threading.ThreadAbortException
                                                                       Throw
                                                                   Catch ex As Exception
                                                                       ClassAdvancedExceptionLogging.WriteToLog(ex)
                                                                   End Try
                                                               End Sub)
            g_mDriverInstallTimerThread.Priority = Threading.ThreadPriority.Lowest
            g_mDriverInstallTimerThread.IsBackground = True
            g_mDriverInstallTimerThread.Start()
        Else
            If (g_mDriverInstallTimerThread IsNot Nothing AndAlso g_mDriverInstallTimerThread.IsAlive) Then
                g_mDriverInstallTimerThread.Abort()
                g_mDriverInstallTimerThread.Join()
                g_mDriverInstallTimerThread = Nothing
            End If

            ClassUtils.AsyncInvoke(Sub()
                                       If (g_mDriverInstallFormLoad Is Nothing OrElse g_mDriverInstallFormLoad.IsDisposed) Then
                                           Return
                                       End If

                                       If (g_mDriverInstallFormLoad.m_ProgressBar.Style = ProgressBarStyle.Blocks) Then
                                           g_mDriverInstallFormLoad.m_ProgressBar.Value = g_mDriverInstallFormLoad.m_ProgressBar.Maximum
                                       End If
                                   End Sub)
        End If
    End Sub

    Public Sub LinkLabel_ServiceFactory_Click()
        LinkLabel_ServiceFactory_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_ServiceFactory_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ServiceFactory.LinkClicked
        Try
            Dim mServiceInfo As New ClassServiceInfo
            If (mServiceInfo.IsServiceRunning() <> ClassServiceInfo.ENUM_SERVICE_PROCESS_TYPE.NONE) Then
                Throw New ArgumentException("PSMoveServiceEx is running. Please close PSMoveServiceEx!")
            End If

            Dim sMessage As New Text.StringBuilder
            sMessage.AppendLine("You are about to remove all PSMoveServiceEx configurations.")
            sMessage.AppendLine("THIS CAN NOT BE UNDONE!")
            sMessage.AppendLine()
            sMessage.AppendLine("Do you want to continue?")
            If (MessageBox.Show(sMessage.ToString, "Factory Reset", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                Return
            End If

            Dim sConfigFolder As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PSMoveService")

            If (IO.Directory.Exists(sConfigFolder)) Then
                IO.Directory.Delete(sConfigFolder, True)
            End If

            MessageBox.Show("All config have been removed!", "Factory Reset", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Public Sub LinkLabel_ServicePath_Click()
        LinkLabel_ServicePath_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_ServicePath_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ServicePath.LinkClicked
        Try
            Dim mConfig As New ClassServiceInfo
            mConfig.LoadConfig()

            If (mConfig.SearchForService) Then
                mConfig.SaveConfig()
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub LinkLabel_ManageConnectedDevices_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ManageConnectedDevices.LinkClicked
        Try
            Using mForm As New FormConnectedDevices
                mForm.ShowDialog(g_FormMain)
            End Using
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub Button_PsmsxUpdateDownload_Click(sender As Object, e As EventArgs) Handles Button_PsmsxUpdateDownload.Click
        If (g_mUpdateInstallThread IsNot Nothing AndAlso g_mUpdateInstallThread.IsAlive) Then
            Return
        End If

        g_mUpdateInstallThread = New Threading.Thread(
            Sub()
                Try
                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mUpdateInstallFormLoad IsNot Nothing AndAlso Not g_mUpdateInstallFormLoad.IsDisposed) Then
                                                   g_mUpdateInstallFormLoad.Dispose()
                                                   g_mUpdateInstallFormLoad = Nothing
                                               End If

                                               g_mUpdateInstallFormLoad = New FormLoading
                                               g_mUpdateInstallFormLoad.Text = "Downloading and installing new update..."
                                               g_mUpdateInstallFormLoad.ShowDialog(g_FormMain)
                                           End Sub)

                    Dim mConfig As New ClassServiceInfo
                    mConfig.LoadConfig()

                    If (Not mConfig.FileExist()) Then
                        If (Not mConfig.FindByProcess()) Then
                            Throw New ArgumentException("Unable to find PSMoveServiceEx")
                        End If
                    End If

                    Dim sServicePath As String = IO.Path.GetDirectoryName(mConfig.m_FileName)

                    Dim sEndProcessNames As New List(Of String)
                    sEndProcessNames.AddRange(g_sServiceProcesses)
                    sEndProcessNames.Add(IO.Path.GetFileName(Application.ExecutablePath))

                    ClassUpdate.ClassPsms.InstallUpdate(sServicePath, sEndProcessNames.ToArray)
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                Finally
                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mUpdateInstallFormLoad IsNot Nothing AndAlso Not g_mUpdateInstallFormLoad.IsDisposed) Then
                                                   g_mUpdateInstallFormLoad.Dispose()
                                                   g_mUpdateInstallFormLoad = Nothing
                                               End If
                                           End Sub)
                End Try
            End Sub)
        g_mUpdateInstallThread.IsBackground = True
        g_mUpdateInstallThread.Start()
    End Sub

    Private Sub Button_VdmUpdateDownload_Click(sender As Object, e As EventArgs) Handles Button_VdmUpdateDownload.Click
        If (g_mUpdateInstallThread IsNot Nothing AndAlso g_mUpdateInstallThread.IsAlive) Then
            Return
        End If

        g_mUpdateInstallThread = New Threading.Thread(
            Sub()
                Try
                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mUpdateInstallFormLoad IsNot Nothing AndAlso Not g_mUpdateInstallFormLoad.IsDisposed) Then
                                                   g_mUpdateInstallFormLoad.Dispose()
                                                   g_mUpdateInstallFormLoad = Nothing
                                               End If

                                               g_mUpdateInstallFormLoad = New FormLoading
                                               g_mUpdateInstallFormLoad.Text = "Downloading and installing new update..."
                                               g_mUpdateInstallFormLoad.ShowDialog(g_FormMain)
                                           End Sub)

                    Dim sEndProcessNames As New List(Of String)
                    sEndProcessNames.AddRange(g_sServiceProcesses)
                    sEndProcessNames.Add(IO.Path.GetFileName(Application.ExecutablePath))

                    ClassUpdate.ClassVdm.InstallUpdate(Application.StartupPath, sEndProcessNames.ToArray)
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                Finally
                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mUpdateInstallFormLoad IsNot Nothing AndAlso Not g_mUpdateInstallFormLoad.IsDisposed) Then
                                                   g_mUpdateInstallFormLoad.Dispose()
                                                   g_mUpdateInstallFormLoad = Nothing
                                               End If
                                           End Sub)
                End Try
            End Sub)
        g_mUpdateInstallThread.IsBackground = True
        g_mUpdateInstallThread.Start()
    End Sub

    Private Sub Button_VdmUpdateIgnore_Click(sender As Object, e As EventArgs) Handles Button_VdmUpdateIgnore.Click
        Panel_VdmUpdate.Visible = False
    End Sub

    Private Sub Button_PsmsUpdateIgnore_Click(sender As Object, e As EventArgs) Handles Button_PsmsUpdateIgnore.Click
        Panel_PsmsxUpdate.Visible = False
    End Sub

    Private Sub Button_PsmsxInstallDownload_Click(sender As Object, e As EventArgs) Handles Button_PsmsxInstallDownload.Click
        If (g_mUpdateInstallThread IsNot Nothing AndAlso g_mUpdateInstallThread.IsAlive) Then
            Return
        End If

        g_mUpdateInstallThread = New Threading.Thread(
            Sub()
                Try
                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mUpdateInstallFormLoad IsNot Nothing AndAlso Not g_mUpdateInstallFormLoad.IsDisposed) Then
                                                   g_mUpdateInstallFormLoad.Dispose()
                                                   g_mUpdateInstallFormLoad = Nothing
                                               End If

                                               g_mUpdateInstallFormLoad = New FormLoading
                                               g_mUpdateInstallFormLoad.Text = "Downloading and installing PSMoveServiceEx..."
                                               g_mUpdateInstallFormLoad.ShowDialog(g_FormMain)
                                           End Sub)

                    Dim sServicePath As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), "PSMoveServiceEx")
                    If (Not IO.Directory.Exists(sServicePath)) Then
                        IO.Directory.CreateDirectory(sServicePath)
                    End If

                    Dim mConfig As New ClassServiceInfo
                    mConfig.LoadConfig()
                    mConfig.m_FileName = IO.Path.Combine(sServicePath, "PSMoveService.exe")
                    mConfig.SaveConfig()

                    Dim sEndProcessNames As New List(Of String)
                    sEndProcessNames.AddRange(g_sServiceProcesses)
                    sEndProcessNames.Add(IO.Path.GetFileName(Application.ExecutablePath))

                    ClassUpdate.ClassPsms.InstallUpdate(sServicePath, sEndProcessNames.ToArray)
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
                Finally
                    ClassUtils.AsyncInvoke(Sub()
                                               If (g_mUpdateInstallFormLoad IsNot Nothing AndAlso Not g_mUpdateInstallFormLoad.IsDisposed) Then
                                                   g_mUpdateInstallFormLoad.Dispose()
                                                   g_mUpdateInstallFormLoad = Nothing
                                               End If
                                           End Sub)
                End Try
            End Sub)
        g_mUpdateInstallThread.IsBackground = True
        g_mUpdateInstallThread.Start()
    End Sub

    Private Sub Button_PsmsInstallBrowse_Click(sender As Object, e As EventArgs) Handles Button_PsmsInstallBrowse.Click
        Try
            Dim mConfig As New ClassServiceInfo
            mConfig.LoadConfig()

            If (mConfig.SearchForService) Then
                mConfig.SaveConfig()

                Panel_PsmsxInstall.Visible = False
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub Button_PsmsInstallIgnore_Click(sender As Object, e As EventArgs) Handles Button_PsmsInstallIgnore.Click
        Panel_PsmsxInstall.Visible = False
    End Sub

    Private Sub Button_ChartSettings_Click(sender As Object, e As EventArgs) Handles Button_ChartSettings.Click
        ContextMenuStrip_Chart.Show(Button_ChartSettings, New Point(0, Button_ChartSettings.Height))
    End Sub

    Private Sub ToolStripMenuItem_ChartEnabled_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_ChartEnabled.Click
        Chart_ServicePerformance.Enabled = ToolStripMenuItem_ChartEnabled.Checked
    End Sub

    Private Sub ToolStripMenuItem_ChartClear_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_ChartClear.Click
        Chart_ServicePerformance.Series.Clear()
        Chart_ServicePerformance.ChartAreas(0).AxisX.Maximum = 1
        Chart_ServicePerformance.ChartAreas(0).AxisX.Minimum = 0
        Chart_ServicePerformance.ChartAreas(0).AxisY.Maximum = 1
        Chart_ServicePerformance.ChartAreas(0).AxisY.Minimum = 0
    End Sub

    Private Sub ContextMenuStrip_Chart_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip_Chart.Opening
        ToolStripMenuItem_ChartEnabled.Checked = Chart_ServicePerformance.Enabled
    End Sub

    Private Sub LinkLabel_ServiceLog_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ServiceLog.LinkClicked
        Using mLogs As New FormTroubleshootLogs(g_FormMain, False)
            mLogs.ShowDialog(g_FormMain)
        End Using

        RunRuntimeDiagnostics()
    End Sub

    Private Sub Button_VdmDiagnosticsOpen_Click(sender As Object, e As EventArgs) Handles Button_VdmDiagnosticsOpen.Click
        Using mLogs As New FormTroubleshootLogs(g_FormMain, True)
            mLogs.ShowDialog(g_FormMain)
        End Using

        RunRuntimeDiagnostics()
    End Sub

    Private Sub Button_VdmDiagnosticsRestartService_Click(sender As Object, e As EventArgs) Handles Button_VdmDiagnosticsRestartService.Click
        LinkLabel_ServiceRestart_LinkClicked(Nothing, Nothing)
    End Sub
End Class

