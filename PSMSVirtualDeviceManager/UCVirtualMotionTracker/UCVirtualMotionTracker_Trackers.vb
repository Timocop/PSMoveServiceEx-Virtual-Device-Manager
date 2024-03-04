Imports System.ComponentModel

Partial Public Class UCVirtualMotionTracker
    Private Sub AutostartLoad()
        Dim mAutostartControllerIndexes As New List(Of Integer)
        Dim mAutostartHmdIndexes As New List(Of Integer)

        Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            Using mIni As New ClassIni(mStream)
                For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                    If (g_mAutostartControllerMenuStrips(i) Is Nothing OrElse g_mAutostartControllerMenuStrips(i).IsDisposed) Then
                        Continue For
                    End If

                    If (mIni.ReadKeyValue("Autostart", CStr(i), "false") = "true") Then
                        mAutostartControllerIndexes.Add(i)
                    End If
                Next

                For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_HMD_COUNT - 1
                    If (g_mAutostartHmdMenuStrips(i) Is Nothing OrElse g_mAutostartHmdMenuStrips(i).IsDisposed) Then
                        Continue For
                    End If

                    If (mIni.ReadKeyValue("AutostartHmd", CStr(i), "false") = "true") Then
                        mAutostartHmdIndexes.Add(i)
                    End If
                Next
            End Using
        End Using

        For i = 0 To mAutostartControllerIndexes.Count - 1
            Try
                AddVmtTracker(mAutostartControllerIndexes(i), False)
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
            End Try
        Next

        For i = 0 To mAutostartHmdIndexes.Count - 1
            Try
                AddVmtTracker(mAutostartHmdIndexes(i), True)
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
            End Try
        Next
    End Sub

    Private Sub AddVmtTracker(id As Integer, bIsHmd As Boolean)
        If (ListView_Trackers.Items.Count >= ClassVmtConst.VMT_TRACKER_MAX) Then
            Throw New ArgumentException("Maximum number of trackers reached")
        End If

        Dim mItem = New ClassTrackersListViewItem(id, bIsHmd, Me)
        ListView_Trackers.Items.Add(mItem)
        mItem.Selected = True
    End Sub

    Private Function GetVmtTrackers() As UCVirtualMotionTrackerItem()
        Dim mTrackerList As New List(Of UCVirtualMotionTrackerItem)

        For Each mItem As ListViewItem In ListView_Trackers.Items
            Dim mTrackerItem = DirectCast(mItem, ClassTrackersListViewItem)
            If (mTrackerItem.m_UCVirtualMotionTrackerItem Is Nothing OrElse mTrackerItem.m_UCVirtualMotionTrackerItem.IsDisposed) Then
                Continue For
            End If

            mTrackerList.Add(mTrackerItem.m_UCVirtualMotionTrackerItem)
        Next

        Return mTrackerList.ToArray
    End Function

    Private Sub Button_VMTControllers_Click(sender As Object, e As EventArgs) Handles Button_VMTControllers.Click
        ContextMenuStrip_Autostart.Show(Button_VMTControllers, New Point(0, Button_VMTControllers.Size.Height))
    End Sub

    Private Sub ContextMenuStrip_Autostart_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ContextMenuStrip_Autostart.ItemClicked
        Dim mItem As ToolStripMenuItem = TryCast(e.ClickedItem, ToolStripMenuItem)
        If (mItem Is Nothing) Then
            Return
        End If

        mItem.Checked = Not mItem.Checked

        Dim mTagData As Object() = CType(mItem.Tag, Object())
        Dim bIsHMD As Boolean = CBool(mTagData(0))
        Dim iIndex As Integer = CInt(mTagData(1))

        Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            Using mIni As New ClassIni(mStream)
                If (bIsHMD) Then
                    Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                    mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("AutostartHmd", CStr(iIndex), If(mItem.Checked, "true", "false")))

                    mIni.WriteKeyValue(mIniContent.ToArray)
                Else
                    Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                    mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("Autostart", CStr(iIndex), If(mItem.Checked, "true", "false")))

                    mIni.WriteKeyValue(mIniContent.ToArray)
                End If
            End Using
        End Using
    End Sub

    Private Sub ContextMenuStrip_Autostart_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip_Autostart.Opening
        Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            Using mIni As New ClassIni(mStream)
                For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                    If (g_mAutostartControllerMenuStrips(i) Is Nothing OrElse g_mAutostartControllerMenuStrips(i).IsDisposed) Then
                        Continue For
                    End If

                    g_mAutostartControllerMenuStrips(i).Checked = (mIni.ReadKeyValue("Autostart", CStr(i), "false") = "true")
                Next

                For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_HMD_COUNT - 1
                    If (g_mAutostartHmdMenuStrips(i) Is Nothing OrElse g_mAutostartHmdMenuStrips(i).IsDisposed) Then
                        Continue For
                    End If

                    g_mAutostartHmdMenuStrips(i).Checked = (mIni.ReadKeyValue("AutostartHmd", CStr(i), "false") = "true")
                Next
            End Using
        End Using
    End Sub

    Private Sub Button_AddVMTController_Click(sender As Object, e As EventArgs) Handles Button_AddVMTController.Click
        ContextMenuStrip_AddTracker.Show(Button_AddVMTController, 0, Button_AddVMTController.Height)
    End Sub

    Private Sub ToolStripMenuItem_AddTracker_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_AddTracker.Click
        Try
            AddVmtTracker(-1, False)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ToolStripMenuItem_AddHmd_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_AddHmd.Click
        Try
            AddVmtTracker(-1, True)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ListView_Trackers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_Trackers.SelectedIndexChanged
        For Each mItem As ListViewItem In ListView_Trackers.Items
            Dim mTrackerItem = DirectCast(mItem, ClassTrackersListViewItem)
            If (mTrackerItem.Selected) Then
                If (Not mTrackerItem.m_Visible) Then
                    mTrackerItem.m_Visible = True
                End If
            Else
                If (mTrackerItem.m_Visible) Then
                    mTrackerItem.m_Visible = False
                End If
            End If
        Next
    End Sub

    Private Sub ToolStripMenuItem_TrackerRemove_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_TrackerRemove.Click
        If (ListView_Trackers.SelectedItems.Count < 1) Then
            Return
        End If

        Dim mAttachmentItem = DirectCast(ListView_Trackers.SelectedItems(0), ClassTrackersListViewItem)
        ListView_Trackers.Items.Remove(mAttachmentItem)

        mAttachmentItem.Dispose()
    End Sub

    Private Sub Timer_VMTTrackers_Tick(sender As Object, e As EventArgs) Handles Timer_VMTTrackers.Tick
        Timer_VMTTrackers.Stop()

        Try
            If (Me.Visible) Then
                For Each mItem As ListViewItem In ListView_Trackers.Items
                    Dim mTrackerItem = DirectCast(mItem, ClassTrackersListViewItem)
                    mTrackerItem.UpdateItem()
                Next
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try

        Timer_VMTTrackers.Start()
    End Sub
End Class
