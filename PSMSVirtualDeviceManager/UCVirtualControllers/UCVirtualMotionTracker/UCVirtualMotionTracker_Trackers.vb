Imports System.ComponentModel

Partial Public Class UCVirtualMotionTracker
    Private Sub AutostartLoad()
        Dim mAutostartIndexes As New List(Of Integer)

        Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            Using mIni As New ClassIni(mStream)
                For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                    If (g_mAutostartMenuStrips(i) Is Nothing OrElse g_mAutostartMenuStrips(i).IsDisposed) Then
                        Continue For
                    End If

                    If (mIni.ReadKeyValue("Autostart", CStr(i), "false") = "true") Then
                        mAutostartIndexes.Add(i)
                    End If
                Next
            End Using
        End Using

        For i = 0 To mAutostartIndexes.Count - 1
            Try
                AddVmtTracker(mAutostartIndexes(i))
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next
    End Sub

    Private Sub AddVmtTracker(id As Integer)
        If (ListView_Trackers.Items.Count >= ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT) Then
            Throw New ArgumentException("Maximum number of trackers reached")
        End If

        Dim mItem = New ClassTrackersListViewItem(id, Me)
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

        Dim iIndex As Integer = CInt(mItem.Tag)

        Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            Using mIni As New ClassIni(mStream)
                Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("Autostart", CStr(iIndex), If(mItem.Checked, "true", "false")))

                mIni.WriteKeyValue(mIniContent.ToArray)
            End Using
        End Using
    End Sub

    Private Sub ContextMenuStrip_Autostart_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip_Autostart.Opening
        Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            Using mIni As New ClassIni(mStream)
                For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                    If (g_mAutostartMenuStrips(i) Is Nothing OrElse g_mAutostartMenuStrips(i).IsDisposed) Then
                        Continue For
                    End If

                    g_mAutostartMenuStrips(i).Checked = (mIni.ReadKeyValue("Autostart", CStr(i), "false") = "true")
                Next
            End Using
        End Using
    End Sub

    Private Sub Button_AddVMTController_Click(sender As Object, e As EventArgs) Handles Button_AddVMTController.Click
        Try
            AddVmtTracker(-1)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        Try
            Timer_VMTTrackers.Stop()

            For Each mItem As ListViewItem In ListView_Trackers.Items
                Dim mTrackerItem = DirectCast(mItem, ClassTrackersListViewItem)
                mTrackerItem.UpdateItem()
            Next
        Catch ex As Exception
        Finally
            Timer_VMTTrackers.Start()
        End Try
    End Sub
End Class
