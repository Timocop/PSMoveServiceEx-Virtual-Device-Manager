Partial Public Class UCVirtualMotionTracker
    Private Sub Button_Add_Click(sender As Object, e As EventArgs) Handles Button_Add.Click
        Try
            Dim mConfig As New ClassSteamVRConfig
            mConfig.LoadConfig()
            If (Not mConfig.LoadConfig()) Then
                Throw New ArgumentException("Unable to load SteamVR configs")
            End If

            Dim sKnownTrackers As String() = mConfig.m_ClassTrackerRoles.GetKnownTrackers

            Using i As New FormTrackerOverrideSetup(sKnownTrackers)
                If (i.ShowDialog = DialogResult.OK) Then
                    Dim mResult = i.m_DialogResult

                    Dim sTracker As String = ""
                    Dim sOverride As String = ""

                    If (mResult.bCustomTracker) Then
                        sTracker = mResult.sCustomTrackerName
                    Else
                        sTracker = (ClassVmtConst.VMT_DEVICE_SERIAL & mResult.iVMTTracker)
                    End If

                    Select Case (mResult.iOverrideType)
                        Case FormTrackerOverrideSetup.ENUM_OVERRIDE_TYPE.HEAD
                            sOverride = "/user/head"
                        Case FormTrackerOverrideSetup.ENUM_OVERRIDE_TYPE.LEFT_HAND
                            sOverride = "/user/hand/left"
                        Case FormTrackerOverrideSetup.ENUM_OVERRIDE_TYPE.RIGHT_HAND
                            sOverride = "/user/hand/right"
                        Case Else
                            Throw New ArgumentException("Invalid")
                    End Select

                    If (mConfig.m_ClassOverrides.GetOverride(sTracker) IsNot Nothing) Then
                        If (MessageBox.Show(String.Format("A tracker with the name '{0}' already exists! Do you want to override the tracker override with the current one?", sTracker), "Override?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No) Then
                            Return
                        End If
                    End If

                    mConfig.m_ClassOverrides.SetOverride(sTracker, sOverride)
                    mConfig.SaveConfig()

                    RefreshOverrides()

                    ' Check if SteamVR is running
                    If (Process.GetProcessesByName("vrserver").Length > 0) Then
                        g_mFormMain.g_mUCVirtualMotionTracker.Panel_SteamVRRestart.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub Button_Remove_Click(sender As Object, e As EventArgs) Handles Button_Remove.Click
        Try
            If (ListView_Overrides.SelectedItems.Count < 1) Then
                Return
            End If

            Dim sMessage As New Text.StringBuilder
            sMessage.AppendLine("Are you sure you want to remove following trackers from the overrides?")
            sMessage.AppendLine()
            For Each mSelectedItem As ListViewItem In ListView_Overrides.SelectedItems
                sMessage.AppendLine(mSelectedItem.SubItems(0).Text)
            Next
            If (MessageBox.Show(sMessage.ToString, "Remove overrides", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No) Then
                Return
            End If

            Dim mConfig As New ClassSteamVRConfig
            If (Not mConfig.LoadConfig()) Then
                Throw New ArgumentException("Unable to load SteamVR configs")
            End If

            For Each mSelectedItem As ListViewItem In ListView_Overrides.SelectedItems
                mConfig.m_ClassOverrides.RemoveOverride(mSelectedItem.SubItems(0).Text)
            Next
            mConfig.SaveConfig()

            RefreshOverrides()

            ' Check if SteamVR is running
            If (Process.GetProcessesByName("vrserver").Length > 0) Then
                g_mFormMain.g_mUCVirtualMotionTracker.Panel_SteamVRRestart.Visible = True
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub Button_Refresh_Click(sender As Object, e As EventArgs) Handles Button_Refresh.Click
        Try
            RefreshOverrides()

        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub RefreshOverrides()
        ListView_Overrides.Items.Clear()

        Dim mSteamCOnfig As New ClassSteamVRConfig
        If (mSteamCOnfig.LoadConfig()) Then
            For Each mOverride In mSteamCOnfig.m_ClassOverrides.GetOverrides()
                ListView_Overrides.Items.Add(New ListViewItem(New String() {mOverride.Key, mOverride.Value}))
            Next
        End If
    End Sub

    Private Sub LinkLabel_SteamVRRestartOff_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_SteamVRRestartOff.LinkClicked
        g_mFormMain.g_mUCVirtualMotionTracker.Panel_SteamVRRestart.Visible = False
    End Sub
End Class
