﻿Imports System.Numerics

Partial Public Class UCVirtualMotionTracker
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
            g_ClassOscServer.StartServer()
            g_ClassOscServer.m_SuspendRequests = False

            g_ClassOscDevices.StartThread()

            g_mFormMain.g_mPSMoveServiceCAPI.RegisterPoseStream("VMT")
        Catch ex As Exception
            With New Text.StringBuilder
                .AppendLine("Unable to create OSC Server!")
                .AppendLine()
                .AppendLine(ex.Message)

                MessageBox.Show(.ToString)
            End With
        End Try
    End Sub

    Private Sub LinkLabel_OscPause_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_OscPause.LinkClicked
        g_ClassOscServer.m_SuspendRequests = True

        g_mFormMain.g_mPSMoveServiceCAPI.UnregisterPoseStream("VMT")
    End Sub

    Private Sub LinkLabel_SteamRun_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_SteamRun.LinkClicked
        Try
            Process.Start("steam://rungameid/250820")
        Catch ex As Exception
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
                Dim mDrivers As String() = mConfig.GetDrivers()
                If (mDrivers IsNot Nothing) Then
                    For Each sDriver As String In mDrivers
                        Dim sDriverPath As String = IO.Path.GetFullPath(sDriver)
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
                                mConfig.RemovePath(sDriver)
                                mConfig.SaveConfig()
                            End If
                        End If
                    Next
                End If
            End If

            ' Find same driver
            If (True) Then
                Dim mDrivers As String() = mConfig.GetDrivers()
                If (mDrivers IsNot Nothing) Then
                    For Each sDriver As String In mDrivers
                        Dim sDriverPath As String = IO.Path.GetFullPath(sDriver)
                        If (sDriverPath.ToLowerInvariant = sDriverRoot.ToLowerInvariant) Then
                            MessageBox.Show("SteamVR driver is already installed!", "Unable to install driver", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return
                        End If
                    Next
                End If
            End If

            mConfig.AddPath(sDriverRoot)
            mConfig.SaveConfig()

            MessageBox.Show("Driver has been successfully registered!", "Driver added to SteamVR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_SteamSettings_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_SteamSettings.LinkClicked
        Try
            Using mSettings As New FormSteamSettings
                mSettings.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            Case ENUM_OSC_CONNECTION_STATUS.CONNECTED
                Label_OscStatus.Text = "OSC Connected"
                Panel_OscStatus.BackColor = Color.FromArgb(0, 192, 0)

            Case ENUM_OSC_CONNECTION_STATUS.DISCONNETED
                Label_OscStatus.Text = "OSC Disconnected"
                Panel_OscStatus.BackColor = Color.FromArgb(192, 0, 0)

            Case ENUM_OSC_CONNECTION_STATUS.TIMEOUT
                Label_OscStatus.Text = "OSC Timeout"
                Panel_OscStatus.BackColor = Color.FromArgb(192, 0, 0)

        End Select
    End Sub

    Private Sub OscStatusThread()
        While True
            Try
                If (g_ClassOscServer Is Nothing OrElse Not g_ClassOscServer.IsRunning()) Then
                    ClassUtils.AsyncInvoke(Me, Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.NOT_STARTED))
                Else
                    If (g_ClassOscServer.m_SuspendRequests) Then
                        ClassUtils.AsyncInvoke(Me, Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.DISCONNETED))
                    Else
                        Dim mLastResponse As TimeSpan = (Now - g_ClassOscServer.m_LastResponse)

                        If (mLastResponse.TotalMilliseconds > 5000) Then
                            ClassUtils.AsyncInvoke(Me, Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.TIMEOUT))
                        Else
                            ClassUtils.AsyncInvoke(Me, Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.CONNECTED))
                        End If
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception

            End Try

            Threading.Thread.Sleep(1000)
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

                Dim mDevices = g_ClassOscDevices.GetDevices

                For i = 0 To mDevices.Length - 1
                    Dim mDevice As ClassOscDevices.STRUC_DEVICE = mDevices(i)

                    Dim mPos As Vector3 = mDevice.GetPosCm()
                    Dim mAng As Vector3 = mDevice.GetOrientationEuler()

                    ClassUtils.AsyncInvoke(Me, Sub()
                                                   If (Not Me.Visible) Then
                                                       Return
                                                   End If

                                                   Dim bFound As Boolean = False

                                                   ' Change info about device
                                                   ListView_OscDevices.BeginUpdate()
                                                   For Each mListVIewItem As ListViewItem In ListView_OscDevices.Items
                                                       If (mListVIewItem.SubItems(LISTVIEW_SUBITEM_SERIAL).Text = mDevice.sSerial) Then

                                                           mListVIewItem.SubItems(LISTVIEW_SUBITEM_POSITION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z)))
                                                           mListVIewItem.SubItems(LISTVIEW_SUBITEM_ORIENTATION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z)))
                                                           mListVIewItem.Tag = New Object() {mDevice.mLastPoseTimestamp}

                                                           bFound = True
                                                       End If
                                                   Next
                                                   ListView_OscDevices.EndUpdate()

                                                   ' Added device when not found
                                                   If (Not bFound) Then
                                                       Dim mListViewItem = New ListViewItem(New String() {
                                                mDevice.iType.ToString,
                                                mDevice.sSerial,
                                                String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z))),
                                                String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z))),
                                                "0"
                                            })
                                                       mListViewItem.Tag = New Object() {mDevice.mLastPoseTimestamp}

                                                       ListView_OscDevices.Items.Add(mListViewItem)
                                                   End If
                                               End Sub)
                Next

                ClassUtils.AsyncInvoke(Me, Sub()
                                               If (Not Me.Visible) Then
                                                   Return
                                               End If

                                               ListView_OscDevices.BeginUpdate()
                                               For Each mListVIewItem As ListViewItem In ListView_OscDevices.Items
                                                   Dim mLastPoseTime As Date = CDate(DirectCast(mListVIewItem.Tag, Object())(0))

                                                   If (mLastPoseTime + New TimeSpan(0, 0, 5) > Now) Then
                                                       mListVIewItem.BackColor = Color.FromArgb(255, 255, 255)
                                                   Else
                                                       mListVIewItem.BackColor = Color.FromArgb(255, 192, 192)
                                                   End If
                                               Next
                                               ListView_OscDevices.EndUpdate()
                                           End Sub)

            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception

            End Try

            Threading.Thread.Sleep(500)
        End While
    End Sub

End Class