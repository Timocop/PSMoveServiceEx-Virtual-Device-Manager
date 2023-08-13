Imports System.Numerics

Public Class UCStartPage
    Private g_FormMain As FormMain
    Private g_bIgnoreEvents As Boolean = False

    Private g_mStatusThread As Threading.Thread = Nothing
    Private g_mServiceDeviceStatusThread As Threading.Thread = Nothing

    Private g_mDriverInstallThread As Threading.Thread = Nothing
    Private g_mDriverInstallFormLoad As FormLoading = Nothing

    Public Sub New(mFormMain As FormMain)
        g_FormMain = mFormMain

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        SetStatusServiceConnected(False)

        g_mStatusThread = New Threading.Thread(AddressOf CheckConnection_Thread)
        g_mStatusThread.IsBackground = True
        g_mStatusThread.Start()

        g_mServiceDeviceStatusThread = New Threading.Thread(AddressOf ServiceDeviceStatusThread)
        g_mServiceDeviceStatusThread.IsBackground = True
        g_mServiceDeviceStatusThread.Start()
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

        End Try
    End Sub

    Private Sub SetStatusServiceConnected(bConnected As Boolean)
        If (bConnected) Then
            Label_PsmsxStatus.Text = "Service Connected"
            Panel_PsmsxStatus.BackColor = Color.FromArgb(0, 192, 0)
        Else
            Label_PsmsxStatus.Text = "Service Disconnected"
            Panel_PsmsxStatus.BackColor = Color.FromArgb(192, 0, 0)
        End If

    End Sub

    Private Sub CheckConnection_Thread()
        While True
            Try
                Threading.Thread.Sleep(1000)

                Dim bIsConnected = g_FormMain.g_mPSMoveServiceCAPI.m_IsServiceConnected

                Me.BeginInvoke(Sub() SetStatusServiceConnected(bIsConnected))
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
            End Try
        End While
    End Sub

    Private Sub ServiceDeviceStatusThread()
        While True
            Try
                Const LISTVIEW_SUBITEM_TYPE As Integer = 0
                Const LISTVIEW_SUBITEM_COLOR As Integer = 1
                Const LISTVIEW_SUBITEM_ID As Integer = 2
                Const LISTVIEW_SUBITEM_SERIAL As Integer = 3
                Const LISTVIEW_SUBITEM_POSITION As Integer = 4
                Const LISTVIEW_SUBITEM_ORIENTATION As Integer = 5
                Const LISTVIEW_SUBITEM_BATTERY As Integer = 6

                ' List Controllers
                If (True) Then
                    Dim mDevices = g_FormMain.g_mPSMoveServiceCAPI.GetControllersData

                    For i = 0 To mDevices.Length - 1
                        Dim mDevice As ClassServiceClient.IControllerData = mDevices(i)

                        If (String.IsNullOrEmpty(mDevice.m_Serial) OrElse mDevice.m_Serial.TrimEnd.Length = 0) Then
                            Continue For
                        End If

                        Dim mPos As Vector3 = mDevice.m_Position
                        Dim mAng As Vector3 = mDevice.GetOrientationEuler()

                        Me.BeginInvoke(Sub()
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
                                           For Each mListVIewItem As ListViewItem In ListView_ServiceDevices.Items
                                               If (mListVIewItem.SubItems(LISTVIEW_SUBITEM_SERIAL).Text = mDevice.m_Serial) Then
                                                   mListVIewItem.SubItems(LISTVIEW_SUBITEM_COLOR).Text = sTrackingColor
                                                   mListVIewItem.SubItems(LISTVIEW_SUBITEM_ID).Text = CStr(mDevice.m_Id)

                                                   mListVIewItem.SubItems(LISTVIEW_SUBITEM_POSITION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z)))
                                                   mListVIewItem.SubItems(LISTVIEW_SUBITEM_ORIENTATION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z)))
                                                   mListVIewItem.SubItems(LISTVIEW_SUBITEM_BATTERY).Text = CStr(CInt(mDevice.m_BatteryLevel * 100.0F)) & " %"
                                                   mListVIewItem.Tag = New Object() {mDevice.m_LastTimeStamp}

                                                   bFound = True
                                               End If
                                           Next
                                           ListView_ServiceDevices.EndUpdate()

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
                                                    "0",
                                                    "0 %"
                                                })
                                               mListViewItem.BackColor = Color.FromArgb(192, 255, 192)
                                               mListViewItem.Tag = New Object() {mDevice.m_LastTimeStamp}

                                               ListView_ServiceDevices.Items.Add(mListViewItem)
                                           End If
                                       End Sub)
                    Next
                End If

                ' List HMDs
                If (True) Then
                    ' TODO: List HMDs
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

                        Me.BeginInvoke(Sub()
                                           If (Not Me.Visible) Then
                                               Return
                                           End If

                                           Dim bFound As Boolean = False

                                           ' Change info about device 
                                           ListView_ServiceDevices.BeginUpdate()
                                           For Each mListVIewItem As ListViewItem In ListView_ServiceDevices.Items
                                               If (mListVIewItem.SubItems(LISTVIEW_SUBITEM_SERIAL).Text = mDevice.m_Path) Then
                                                   mListVIewItem.SubItems(LISTVIEW_SUBITEM_COLOR).Text = ""
                                                   mListVIewItem.SubItems(LISTVIEW_SUBITEM_ID).Text = CStr(mDevice.m_Id)

                                                   mListVIewItem.SubItems(LISTVIEW_SUBITEM_POSITION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z)))
                                                   mListVIewItem.SubItems(LISTVIEW_SUBITEM_ORIENTATION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z)))
                                                   mListVIewItem.SubItems(LISTVIEW_SUBITEM_BATTERY).Text = ""
                                                   mListVIewItem.Tag = New Object() {Now}

                                                   bFound = True
                                               End If
                                           Next
                                           ListView_ServiceDevices.EndUpdate()

                                           ' Added device when not found
                                           If (Not bFound) Then
                                               Dim mListViewItem = New ListViewItem(New String() {
                                                    "TRACKER",
                                                    "",
                                                    CStr(mDevice.m_Id),
                                                    mDevice.m_Path,
                                                    String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z))),
                                                    String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z))),
                                                    "0",
                                                    ""
                                                })
                                               mListViewItem.Tag = New Object() {Now}

                                               ListView_ServiceDevices.Items.Add(mListViewItem)
                                           End If
                                       End Sub)
                    Next
                End If

                ' Show disconnected devices
                Me.BeginInvoke(Sub()
                                   If (Not Me.Visible) Then
                                       Return
                                   End If

                                   ListView_ServiceDevices.BeginUpdate()
                                   For Each mListVIewItem As ListViewItem In ListView_ServiceDevices.Items
                                       Dim mLastPoseTime As Date = CDate(DirectCast(mListVIewItem.Tag, Object())(0))

                                       If (mLastPoseTime + New TimeSpan(0, 0, 5) > Now) Then
                                           mListVIewItem.BackColor = Color.FromArgb(255, 255, 255)
                                       Else
                                           mListVIewItem.BackColor = Color.FromArgb(255, 192, 192)
                                       End If
                                   Next
                                   ListView_ServiceDevices.EndUpdate()
                               End Sub)

            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception

            End Try

            Threading.Thread.Sleep(1000)
        End While
    End Sub

    Private Sub CleanUp()
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
        Try
            If (Process.GetProcessesByName("PSMoveService").Count > 0) Then
                Throw New ArgumentException("PSMoveServiceEx is already running!")
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

                mProcess.Start()

            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LinkLabel_ServiceRestart_Click()
        LinkLabel_ServiceRestart_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_ServiceRestart_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ServiceRestart.LinkClicked
        Try
            Dim sPSMSPath As String = Nothing

            Dim mProcesses As Process() = Process.GetProcessesByName("PSMoveService")
            If (mProcesses Is Nothing OrElse mProcesses.Length < 1) Then
                Throw New ArgumentException("PSMoveServiceEx not running!")
            End If

            For Each mProcess In mProcesses
                If (sPSMSPath Is Nothing) Then
                    sPSMSPath = mProcess.MainModule.FileName
                End If

                If (mProcess.CloseMainWindow()) Then
                    mProcess.WaitForExit(10000)
                Else
                    mProcess.Kill()
                End If
            Next

            If (sPSMSPath Is Nothing OrElse Not IO.File.Exists(sPSMSPath)) Then
                Throw New ArgumentException("PSMoveServiceEx executable not found. Please start manualy.")
            End If

            Threading.Thread.Sleep(1000)

            Using mProcess As New Process
                mProcess.StartInfo.FileName = sPSMSPath
                mProcess.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(sPSMSPath)
                mProcess.StartInfo.UseShellExecute = False

                mProcess.Start()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LinkLabel_ServiceStop_Click()
        LinkLabel_ServiceStop_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_ServiceStop_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ServiceStop.LinkClicked
        Try
            Dim mProcesses As Process() = Process.GetProcessesByName("PSMoveService")
            If (mProcesses Is Nothing OrElse mProcesses.Length < 1) Then
                Throw New ArgumentException("PSMoveServiceEx is not running!")
            End If

            For Each mProcess In mProcesses
                If (mProcess.CloseMainWindow()) Then
                    mProcess.WaitForExit(10000)
                Else
                    mProcess.Kill()
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LinkLabel_ConfigToolRun_Click()
        LinkLabel_ConfigToolRun_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_ConfigToolRun_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ConfigToolRun.LinkClicked
        Try
            If (Process.GetProcessesByName("PSMoveConfigTool").Count > 0) Then
                Dim sMsg As New Text.StringBuilder
                sMsg.AppendLine("PSMoveServiceEx Config Tool is already running!")
                sMsg.AppendLine("Do you want to run another instance?")
                If (MessageBox.Show(sMsg.ToString, "Instance already running", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
                    Return
                End If
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

                mProcess.Start()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_ConfigToolClose_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ConfigToolClose.LinkClicked
        Try
            Dim mProcesses As Process() = Process.GetProcessesByName("PSMoveConfigTool")
            If (mProcesses Is Nothing OrElse mProcesses.Length < 1) Then
                Throw New ArgumentException("PSMoveConfigTool is not running!")
            End If

            For Each mProcess In mProcesses
                If (mProcess.CloseMainWindow()) Then
                    mProcess.WaitForExit(10000)
                Else
                    mProcess.Kill()
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LinkLabel_InstallDrivers_Click()
        LinkLabel_InstallDrivers_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_InstallDrivers_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_InstallDrivers.LinkClicked
        If (g_mDriverInstallThread IsNot Nothing AndAlso g_mDriverInstallThread.IsAlive) Then
            Return
        End If

        g_mDriverInstallThread = New Threading.Thread(
            Sub()
                Try
                    If (Process.GetProcessesByName("PSMoveService").Count > 0) Then
                        Throw New ArgumentException("PSMoveServiceEx is running. Please close PSMoveServiceEx!")
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

                    Me.BeginInvoke(Sub()
                                       If (g_mDriverInstallFormLoad IsNot Nothing AndAlso Not g_mDriverInstallFormLoad.IsDisposed) Then
                                           g_mDriverInstallFormLoad.Dispose()
                                           g_mDriverInstallFormLoad = Nothing
                                       End If

                                       g_mDriverInstallFormLoad = New FormLoading
                                       g_mDriverInstallFormLoad.Text = "Installing drivers..."
                                       g_mDriverInstallFormLoad.ShowDialog(Me)
                                   End Sub)

                    Dim mDriverInstaller As New ClassLibusbDriver
                    mDriverInstaller.InstallDriver64()

                    MessageBox.Show("Drivers installed successfully!", "Driver Installation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    Me.BeginInvoke(Sub()
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

    Public Sub LinkLabel_ServiceFactory_Click()
        LinkLabel_ServiceFactory_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_ServiceFactory_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ServiceFactory.LinkClicked
        Try
            If (Process.GetProcessesByName("PSMoveService").Count > 0) Then
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
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_Github_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Github.LinkClicked
        Try
            Process.Start("https://github.com/Timocop/PSMoveServiceEx-Virtual-Device-Manager")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LinkLabel_Updates_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Updates.LinkClicked
        Try
            Process.Start("https://github.com/Timocop/PSMoveServiceEx-Virtual-Device-Manager/releases")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button_PsmsxUpdateDownload_Click(sender As Object, e As EventArgs) Handles Button_PsmsxUpdateDownload.Click
        Try
            Process.Start("https://github.com/Timocop/PSMoveServiceEx/releases")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button_VdmUpdateDownload_Click(sender As Object, e As EventArgs) Handles Button_VdmUpdateDownload.Click
        Try
            Process.Start("https://github.com/Timocop/PSMoveServiceEx-Virtual-Device-Manager/releases")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button_VdmUpdateIgnore_Click(sender As Object, e As EventArgs) Handles Button_VdmUpdateIgnore.Click
        Panel_VdmUpdate.Visible = False
    End Sub

    Private Sub Button_PsmsUpdateIgnore_Click(sender As Object, e As EventArgs) Handles Button_PsmsUpdateIgnore.Click
        Panel_PsmsxUpdate.Visible = False
    End Sub
End Class
