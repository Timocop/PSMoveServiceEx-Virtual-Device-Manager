Imports System.Numerics

Public Class UCVmtManagement
    Public g_UCVirtualMotionTracker As UCVirtualMotionTracker
    Private g_bInit As Boolean = False

    Private g_mOscStatusThread As Threading.Thread = Nothing
    Private g_mOscDeviceStatusThread As Threading.Thread = Nothing

    Public Sub New(_UCVirtualMotionTracker As UCVirtualMotionTracker)
        g_UCVirtualMotionTracker = _UCVirtualMotionTracker

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

        SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.DISCONNETED)

        g_mOscStatusThread = New Threading.Thread(AddressOf OscStatusThread)
        g_mOscStatusThread.Priority = Threading.ThreadPriority.Lowest
        g_mOscStatusThread.IsBackground = True
        g_mOscStatusThread.Start()

        g_mOscDeviceStatusThread = New Threading.Thread(AddressOf OscDeviceStatusThread)
        g_mOscDeviceStatusThread.Priority = Threading.ThreadPriority.Lowest
        g_mOscDeviceStatusThread.IsBackground = True
        g_mOscDeviceStatusThread.Start()
    End Sub

    Public Sub LinkLabel_OscRun_Click()
        LinkLabel_OscRun_LinkClicked(Nothing, Nothing)
    End Sub

    Public Sub LinkLabel_OscPause_Click()
        LinkLabel_OscPause_LinkClicked(Nothing, Nothing)
    End Sub

    Public Sub LinkLabel_DriverInstall_Click()
        LinkLabel_DriverInstall_LinkClicked(Nothing, Nothing)
    End Sub

    Public Sub LinkLabel_DriverUninstall_Click()
        LinkLabel_DriverUninstall_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_OscRun_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_OscRun.LinkClicked
        Try
            Dim sRemoteIP As String = g_UCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_OscRemoteIP

            g_UCVirtualMotionTracker.g_ClassOscServer.StartServer(sRemoteIP)
            g_UCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests = False

            g_UCVirtualMotionTracker.g_ClassOscDevices.StartThread()

            g_UCVirtualMotionTracker.g_mFormMain.g_mPSMoveServiceCAPI.RegisterPoseStream("VMT")
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub LinkLabel_OscPause_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_OscPause.LinkClicked
        g_UCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests = True

        g_UCVirtualMotionTracker.g_mFormMain.g_mPSMoveServiceCAPI.UnregisterPoseStream("VMT")
    End Sub

    Private Sub LinkLabel_SteamRun_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_SteamRun.LinkClicked
        Try
            Process.Start("steam://rungameid/250820")
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try
    End Sub

    Private Sub LinkLabel_DriverInstall_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_DriverInstall.LinkClicked
        Try
            If (Process.GetProcessesByName("vrserver").Count > 0) Then
                Throw New ArgumentException("SteamVR is running! Close SteamVR and try again.")
            End If

            Dim sDriverRoot As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), ClassVmtConst.VMT_DRIVER_ROOT_PATH)
            If (Not IO.Directory.Exists(sDriverRoot)) Then
                Throw New ArgumentException("Could not find driver root folder!")
            End If

            Dim sDriverDLL As String = IO.Path.Combine(IO.Path.Combine(sDriverRoot, "bin\win64"), ClassVmtConst.VMT_DRIVER_FILE)
            If (Not IO.File.Exists(sDriverDLL)) Then
                Throw New ArgumentException(String.Format("Could not find driver '{0}'!", ClassVmtConst.VMT_DRIVER_FILE))
            End If

            Dim mConfig As New ClassOpenVRConfig()
            If (Not mConfig.LoadConfig()) Then
                Throw New ArgumentException("Unable to find and load OpenVR config!")
            End If

            ' Find outdated drivers
            If (True) Then
                Dim mDrivers = mConfig.GetDrivers()
                For Each mDriver In mDrivers
                    Dim sDriverPath As String = IO.Path.GetFullPath(mDriver.sDriverPath)
                    If (sDriverPath.ToLowerInvariant = sDriverRoot.ToLowerInvariant) Then
                        Continue For
                    End If

                    If (sDriverPath.ToLowerInvariant.EndsWith(String.Format("\{0}", ClassVmtConst.VMT_DRIVER_NAME.ToLowerInvariant))) Then
                        Dim sMsg As New Text.StringBuilder
                        sMsg.AppendLine("Another version of the SteamVR driver is already installed!")
                        sMsg.AppendLine("Do you want to remove the following outdated driver?")
                        sMsg.AppendLine()
                        sMsg.AppendLine(sDriverPath)
                        If (MessageBox.Show(sMsg.ToString, "Outdated driver found", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes) Then
                            mConfig.RemovePath(mDriver.sDriverPath)
                            mConfig.SaveConfig()
                        End If
                    End If
                Next
            End If

            ' Find same driver
            If (True) Then
                For Each mDriver In mConfig.GetDrivers()
                    Dim sDriverPath As String = IO.Path.GetFullPath(mDriver.sDriverPath)
                    If (sDriverPath.ToLowerInvariant = sDriverRoot.ToLowerInvariant) Then
                        MessageBox.Show("SteamVR driver is already installed!", "Unable to install driver", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                Next
            End If

            mConfig.AddPath(sDriverRoot)
            mConfig.SaveConfig()

            MessageBox.Show("Driver has been successfully registered!", "Driver added to SteamVR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub LinkLabel_DriverUninstall_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_DriverUninstall.LinkClicked
        Try
            If (Process.GetProcessesByName("vrserver").Count > 0) Then
                Throw New ArgumentException("SteamVR is running! Close SteamVR and try again.")
            End If

            Dim sDriverRoot As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), ClassVmtConst.VMT_DRIVER_ROOT_PATH)

            Dim mConfig As New ClassOpenVRConfig()
            If (Not mConfig.LoadConfig()) Then
                Throw New ArgumentException("Unable to find and load OpenVR config!")
            End If

            mConfig.RemovePath(sDriverRoot)
            mConfig.SaveConfig()

            MessageBox.Show("Driver has been successfully unregistered!", "Driver removed from SteamVR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub LinkLabel_SteamSettings_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_SteamSettings.LinkClicked
        Try
            Using mSettings As New FormSteamSettings
                mSettings.ShowDialog(g_UCVirtualMotionTracker.g_mFormMain)
            End Using
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Enum ENUM_OSC_CONNECTION_STATUS
        NOT_STARTED
        DISCONNETED
        CONNECTED
        TIMEOUT
    End Enum

    Private Sub SetOscServerStatus(i As ENUM_OSC_CONNECTION_STATUS)
        Select Case (i)
            Case ENUM_OSC_CONNECTION_STATUS.NOT_STARTED
                Label_OscStatus.Text = "OSC Uninitialized"
                Panel_OscStatus.BackColor = Color.FromArgb(224, 224, 224)

                ' Label Status in MainForm
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavOscStatus.Text = "OSC Uninitialized"
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavOscStatus.Image = My.Resources.Status_WHITE_16

            Case ENUM_OSC_CONNECTION_STATUS.CONNECTED
                Label_OscStatus.Text = "OSC Connected"
                Panel_OscStatus.BackColor = Color.FromArgb(0, 192, 0)

                ' Label Status in MainForm
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavOscStatus.Text = "OSC Connected"
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavOscStatus.Image = My.Resources.Status_GREEN_16

            Case ENUM_OSC_CONNECTION_STATUS.DISCONNETED
                Label_OscStatus.Text = "OSC Disconnected"
                Panel_OscStatus.BackColor = Color.FromArgb(192, 0, 0)

                ' Label Status in MainForm
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavOscStatus.Text = "OSC Disconnected"
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavOscStatus.Image = My.Resources.Status_RED_16

            Case ENUM_OSC_CONNECTION_STATUS.TIMEOUT
                Label_OscStatus.Text = "OSC Timeout"
                Panel_OscStatus.BackColor = Color.FromArgb(192, 0, 0)

                ' Label Status in MainForm
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavOscStatus.Text = "OSC Timeout"
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavOscStatus.Image = My.Resources.Status_RED_16

        End Select

        Select Case (i)
            Case ENUM_OSC_CONNECTION_STATUS.CONNECTED, ENUM_OSC_CONNECTION_STATUS.TIMEOUT
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavStartOsc.Text = "Pause OSC Server"
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavStartOsc.Image = My.Resources.imageres_5315_16x16_32

            Case Else
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavStartOsc.Text = "Start OSC Server"
                g_UCVirtualMotionTracker.g_mFormMain.Button_NavStartOsc.Image = My.Resources.imageres_5341_16x16_32
        End Select
    End Sub

    Private Sub OscStatusThread()
        Dim iFrameCount As Integer = 0

        While True
            Try
                Dim bRunning As Boolean = False

                If (g_UCVirtualMotionTracker.g_ClassOscServer Is Nothing OrElse Not g_UCVirtualMotionTracker.g_ClassOscServer.IsRunning()) Then
                    ClassUtils.AsyncInvoke(Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.NOT_STARTED))
                Else
                    If (g_UCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
                        ClassUtils.AsyncInvoke(Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.DISCONNETED))
                    Else
                        Dim mLastResponse As TimeSpan = (Now - g_UCVirtualMotionTracker.g_ClassOscServer.m_LastResponse)

                        If (mLastResponse.TotalMilliseconds > 5000) Then
                            ClassUtils.AsyncInvoke(Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.TIMEOUT))
                        Else
                            ClassUtils.AsyncInvoke(Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.CONNECTED))
                        End If

                        bRunning = True
                    End If
                End If

                If (bRunning) Then
                    Dim iAxisX As Integer = iFrameCount
                    Dim iLatencyMs As Double = g_UCVirtualMotionTracker.g_ClassOscServer.m_Latency

                    ClassUtils.AsyncInvoke(Sub()
                                               Dim mDevices = g_UCVirtualMotionTracker.g_UCVmtTrackers.GetVmtTrackers()

                                               For i = 0 To mDevices.Length - 1
                                                   Dim mDeviceIO = mDevices(i).g_mClassIO

                                                   Dim iVmtTrackerId As Integer = mDeviceIO.m_VmtTracker
                                                   Dim iFPS As Integer = mDeviceIO.m_FpsOscCounter
                                                   Dim bIsHmd As Boolean = mDeviceIO.m_IsHMD

                                                   ' Add to chart
                                                   If (ToolStripComboBox_ChartSamples.SelectedItem IsNot Nothing) Then
                                                       Dim iMaxCount As Integer = Math.Max(CInt(ToolStripComboBox_ChartSamples.SelectedItem), 100)
                                                       Dim sTargetName As String

                                                       If (bIsHmd) Then
                                                           sTargetName = ((ClassVmtConst.VMT_DEVICE_NAME & "HMD"))
                                                       Else
                                                           sTargetName = ((ClassVmtConst.VMT_DEVICE_NAME & iVmtTrackerId))
                                                       End If

                                                       AddChartValues(sTargetName, iFPS, iAxisX, iMaxCount)
                                                   End If
                                               Next

                                               ' Set the recorded latency 
                                               Chart_VmtPerformance.Titles("Title_Latency").Text = String.Format("Latency: {0} ms", CInt(iLatencyMs))
                                           End Sub)

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

    Private Sub OscDeviceStatusThread()
        While True
            Try
                Const LISTVIEW_SUBITEM_TYPE As Integer = 0
                Const LISTVIEW_SUBITEM_SERIAL As Integer = 1
                Const LISTVIEW_SUBITEM_POSITION As Integer = 2
                Const LISTVIEW_SUBITEM_ORIENTATION As Integer = 3
                Const LISTVIEW_SUBITEM_FPS As Integer = 4

                Dim mNow As Date = Now

                ClassUtils.AsyncInvoke(Sub()
                                           If (Not Me.Visible) Then
                                               Return
                                           End If

                                           ListView_OscDevices.BeginUpdate()
                                           Try
                                               For Each mListVIewItem As ListViewItem In ListView_OscDevices.Items
                                                   If (mListVIewItem.Tag Is Nothing) Then
                                                       Continue For
                                                   End If

                                                   Dim mItemInfo = CType(mListVIewItem.Tag, Dictionary(Of String, Object))
                                                   Dim mLastPoseTime As Date = CDate(mItemInfo("LastPoseTimestamp"))
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
                                               ListView_OscDevices.EndUpdate()
                                           End Try
                                       End Sub)

                Dim mDevices = g_UCVirtualMotionTracker.g_ClassOscDevices.GetDevices

                For i = 0 To mDevices.Length - 1
                    Dim mDevice As UCVirtualMotionTracker.ClassOscDevices.STRUC_DEVICE = mDevices(i)

                    Dim mPos As Vector3 = mDevice.GetPosCm()
                    Dim mAng As Vector3 = mDevice.GetOrientationEuler()

                    ClassUtils.AsyncInvoke(Sub()
                                               Dim bFound As Boolean = False

                                               ' Change info about device
                                               ListView_OscDevices.BeginUpdate()
                                               Try
                                                   For Each mListVIewItem As ListViewItem In ListView_OscDevices.Items
                                                       If (mListVIewItem.SubItems(LISTVIEW_SUBITEM_SERIAL).Text = mDevice.sSerial) Then

                                                           mListVIewItem.SubItems(LISTVIEW_SUBITEM_POSITION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z)))
                                                           mListVIewItem.SubItems(LISTVIEW_SUBITEM_ORIENTATION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z)))

                                                           If (mListVIewItem.Tag IsNot Nothing) Then
                                                               Dim mItemInfo = CType(mListVIewItem.Tag, Dictionary(Of String, Object))
                                                               mItemInfo("LastPoseTimestamp") = mDevice.mLastPoseTimestamp
                                                               mListVIewItem.Tag = mItemInfo
                                                           End If

                                                           bFound = True
                                                       End If
                                                   Next
                                               Finally
                                                   ListView_OscDevices.EndUpdate()
                                               End Try

                                               ' Added device when not found
                                               If (Not bFound) Then
                                                   Dim mListViewItem = New ListViewItem(New String() {
                                                        mDevice.iType.ToString,
                                                        mDevice.sSerial,
                                                        String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z))),
                                                        String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z))),
                                                        "0"
                                                    })
                                                   mListViewItem.BackColor = Color.FromArgb(192, 255, 192)

                                                   Dim mItemInfo As New Dictionary(Of String, Object)
                                                   mItemInfo("LastPoseTimestamp") = mDevice.mLastPoseTimestamp
                                                   mItemInfo("CreationTimestamp") = mNow
                                                   mListViewItem.Tag = mItemInfo

                                                   ListView_OscDevices.Items.Add(mListViewItem)
                                               End If
                                           End Sub)
                Next

            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLog(ex)
            End Try

            Threading.Thread.Sleep(500)
        End While
    End Sub

    Private Sub AddChartValues(sSeriesName As String, iValue As Integer, iCount As Integer, iMaxCount As Integer)
        If (Not Chart_VmtPerformance.Enabled) Then
            Return
        End If

        If (Chart_VmtPerformance.ChartAreas.Count < 1) Then
            Return
        End If

        If (Chart_VmtPerformance.Series.IndexOf(sSeriesName) = -1) Then
            Dim mSeries = Chart_VmtPerformance.Series.Add(sSeriesName)

            mSeries.ChartArea = Chart_VmtPerformance.ChartAreas(0).Name
            mSeries.ChartType = DataVisualization.Charting.SeriesChartType.FastLine
            mSeries.BorderWidth = 2
        End If

        Chart_VmtPerformance.Series(sSeriesName).Points.AddXY(iCount, iValue)

        While (Chart_VmtPerformance.Series(sSeriesName).Points.Count > 1 AndAlso
            Chart_VmtPerformance.Series(sSeriesName).Points(1).XValue < (iCount - iMaxCount))
            Chart_VmtPerformance.Series(sSeriesName).Points.RemoveAt(0)
        End While

        ' Adjust bounds
        Dim mAxisX = Chart_VmtPerformance.ChartAreas(0).AxisX()
        If (mAxisX.Maximum < iCount) Then
            mAxisX.Maximum = iCount
        End If
        If (mAxisX.Minimum < (iCount - iMaxCount)) Then
            mAxisX.Minimum = iCount - iMaxCount
        End If

        Dim mAxisY = Chart_VmtPerformance.ChartAreas(0).AxisY()
        Dim iValueMax = Math.Ceiling(iValue * 0.1) * 10
        Dim iValueMin = Math.Floor(iValue * 0.1) * 10

        If (mAxisY.Maximum < iValueMax) Then
            mAxisY.Maximum = iValueMax
        End If
        If (mAxisY.Minimum > iValueMin) Then
            mAxisY.Minimum = iValueMin
        End If
    End Sub

    Private Sub Button_ChartSettings_Click(sender As Object, e As EventArgs) Handles Button_ChartSettings.Click
        ContextMenuStrip_Chart.Show(Button_ChartSettings, New Point(0, Button_ChartSettings.Height))
    End Sub

    Private Sub ToolStripMenuItem_ChartEnabled_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_ChartEnabled.Click
        Chart_VmtPerformance.Enabled = ToolStripMenuItem_ChartEnabled.Checked
    End Sub

    Private Sub ContextMenuStrip_Chart_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip_Chart.Opening
        ToolStripMenuItem_ChartEnabled.Checked = Chart_VmtPerformance.Enabled
    End Sub

    Private Sub ToolStripMenuItem_ChartClear_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_ChartClear.Click
        Chart_VmtPerformance.Series.Clear()
        Chart_VmtPerformance.ChartAreas(0).AxisX.Maximum = 1
        Chart_VmtPerformance.ChartAreas(0).AxisX.Minimum = 0
        Chart_VmtPerformance.ChartAreas(0).AxisY.Maximum = 1
        Chart_VmtPerformance.ChartAreas(0).AxisY.Minimum = 0
    End Sub

    Private Sub CleanUp()
        If (g_mOscDeviceStatusThread IsNot Nothing AndAlso g_mOscDeviceStatusThread.IsAlive) Then
            g_mOscDeviceStatusThread.Abort()
            g_mOscDeviceStatusThread.Join()
            g_mOscDeviceStatusThread = Nothing
        End If

        If (g_mOscStatusThread IsNot Nothing AndAlso g_mOscStatusThread.IsAlive) Then
            g_mOscStatusThread.Abort()
            g_mOscStatusThread.Join()
            g_mOscStatusThread = Nothing
        End If
    End Sub
End Class
