Imports System.ComponentModel

Public Class UCVmtTrackers
    Public g_UCVirtualMotionTracker As UCVirtualMotionTracker

    Private g_mAutostartControllerMenuStrips As New Dictionary(Of Integer, ToolStripMenuItem)
    Private g_mAutostartHmdMenuStrips As New Dictionary(Of Integer, ToolStripMenuItem)

    Public Sub New(_UCVirtualMotionTracker As UCVirtualMotionTracker)
        g_UCVirtualMotionTracker = _UCVirtualMotionTracker

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        If (True) Then
            For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                Dim mItem As New ToolStripMenuItem("Controller ID: " & CStr(i))

                g_mAutostartControllerMenuStrips(i) = mItem

                mItem.Tag = New Object() {
                    False,
                    i
                }

                ContextMenuStrip_Autostart.Items.Add(mItem)
            Next

            For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_HMD_COUNT - 1
                Dim mItem As New ToolStripMenuItem("HMD ID: " & CStr(i))

                g_mAutostartHmdMenuStrips(i) = mItem

                mItem.Tag = New Object() {
                    True,
                    i
                }

                ContextMenuStrip_Autostart.Items.Add(mItem)
            Next
        End If

    End Sub

    Public Sub AutostartLoad()
        Dim mAutostartControllerIndexes As New List(Of Integer)
        Dim mAutostartHmdIndexes As New List(Of Integer)

        Using mStream As New IO.FileStream(UCVirtualMotionTracker.g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
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

    Public Sub AddVmtTracker(id As Integer, bIsHmd As Boolean)
        If (ListView_Trackers.Items.Count >= ClassVmtConst.VMT_TRACKER_MAX) Then
            Throw New ArgumentException("Maximum number of trackers reached")
        End If

        Dim mItem = New ClassTrackersListViewItem(id, bIsHmd, g_UCVirtualMotionTracker)
        ListView_Trackers.Items.Add(mItem)
        mItem.Selected = True
    End Sub

    Public Function GetVmtTrackers() As UCVirtualMotionTrackerItem()
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

        Using mStream As New IO.FileStream(UCVirtualMotionTracker.g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
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
        Using mStream As New IO.FileStream(UCVirtualMotionTracker.g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
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

    Private Sub CleanUp()
        For Each mItem As ListViewItem In ListView_Trackers.Items
            Dim mTrackerItem = DirectCast(mItem, ClassTrackersListViewItem)

            mTrackerItem.Dispose()
        Next
        ListView_Trackers.Items.Clear()

        For Each mItem In g_mAutostartControllerMenuStrips
            If (mItem.Value IsNot Nothing AndAlso Not mItem.Value.IsDisposed) Then
                mItem.Value.Dispose()
            End If
        Next
        g_mAutostartControllerMenuStrips.Clear()

        For Each mItem In g_mAutostartHmdMenuStrips
            If (mItem.Value IsNot Nothing AndAlso Not mItem.Value.IsDisposed) Then
                mItem.Value.Dispose()
            End If
        Next
        g_mAutostartHmdMenuStrips.Clear()
    End Sub


    Class ClassTrackersListViewItem
        Inherits ListViewItem
        Implements IDisposable

        Private g_UCVirtualMotionTrackerItem As UCVirtualMotionTrackerItem
        Private g_UCVirtualMotionTracker As UCVirtualMotionTracker

        Public Sub New(iControllerID As Integer, bIsHmd As Boolean, _UCVirtualMotionTracker As UCVirtualMotionTracker)
            MyBase.New(New String() {"", "", "", ""})

            g_UCVirtualMotionTracker = _UCVirtualMotionTracker
            g_UCVirtualMotionTrackerItem = New UCVirtualMotionTrackerItem(iControllerID, bIsHmd, _UCVirtualMotionTracker)

            UpdateItem()
        End Sub

        Public Sub UpdateItem()
            Const LISTVIEW_SUBITEM_TYPE As Integer = 0
            Const LISTVIEW_SUBITEM_INDEX As Integer = 1
            Const LISTVIEW_SUBITEM_VMTID As Integer = 2
            Const LISTVIEW_SUBITEM_ROLE As Integer = 3

            If (g_UCVirtualMotionTrackerItem Is Nothing OrElse g_UCVirtualMotionTrackerItem.IsDisposed) Then
                Return
            End If

            If (g_UCVirtualMotionTrackerItem.g_mClassIO Is Nothing) Then
                Return
            End If

            If (g_UCVirtualMotionTrackerItem.g_mClassIO.m_IsHMD) Then
                Me.SubItems(LISTVIEW_SUBITEM_TYPE).Text = "HMD"
            Else
                Me.SubItems(LISTVIEW_SUBITEM_TYPE).Text = "Controller"
            End If

            Me.SubItems(LISTVIEW_SUBITEM_INDEX).Text = CStr(g_UCVirtualMotionTrackerItem.g_mClassIO.m_Index)
            Me.SubItems(LISTVIEW_SUBITEM_VMTID).Text = CStr(g_UCVirtualMotionTrackerItem.g_mClassIO.m_VmtTracker)

            If (g_UCVirtualMotionTrackerItem.g_mClassIO.m_IsHMD) Then
                Me.SubItems(LISTVIEW_SUBITEM_ROLE).Text = "Head-Mounted Display"
            Else
                If (g_UCVirtualMotionTrackerItem.ComboBox_VMTTrackerRole.SelectedItem IsNot Nothing AndAlso g_UCVirtualMotionTrackerItem.ComboBox_SteamTrackerRole.SelectedItem IsNot Nothing) Then
                    Me.SubItems(LISTVIEW_SUBITEM_ROLE).Text = String.Format("{0} ({1})", CStr(g_UCVirtualMotionTrackerItem.ComboBox_VMTTrackerRole.SelectedItem), CStr(g_UCVirtualMotionTrackerItem.ComboBox_SteamTrackerRole.SelectedItem))
                Else
                    Me.SubItems(LISTVIEW_SUBITEM_ROLE).Text = ""
                End If
            End If

            'Is there any error?
            If (g_UCVirtualMotionTrackerItem.m_HasStatusError) Then
                Me.BackColor = Color.FromArgb(255, 192, 192)
            Else
                Me.BackColor = Color.FromArgb(255, 255, 255)
            End If
        End Sub

        ReadOnly Property m_UCVirtualMotionTrackerItem As UCVirtualMotionTrackerItem
            Get
                Return g_UCVirtualMotionTrackerItem
            End Get
        End Property

        Property m_Visible As Boolean
            Get
                If (g_UCVirtualMotionTrackerItem Is Nothing OrElse g_UCVirtualMotionTrackerItem.IsDisposed) Then
                    Return False
                End If

                Return (g_UCVirtualMotionTrackerItem.Parent Is g_UCVirtualMotionTracker.g_UCVmtTrackers.Panel_VMTTrackers)
            End Get
            Set(value As Boolean)
                If (value) Then
                    If (g_UCVirtualMotionTrackerItem Is Nothing OrElse g_UCVirtualMotionTrackerItem.IsDisposed) Then
                        Return
                    End If

                    g_UCVirtualMotionTrackerItem.Parent = g_UCVirtualMotionTracker.g_UCVmtTrackers.Panel_VMTTrackers
                    g_UCVirtualMotionTrackerItem.Dock = DockStyle.Top
                    g_UCVirtualMotionTrackerItem.Visible = True
                Else
                    g_UCVirtualMotionTrackerItem.Parent = Nothing
                    g_UCVirtualMotionTrackerItem.Visible = False
                End If
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    If (g_UCVirtualMotionTrackerItem IsNot Nothing AndAlso Not g_UCVirtualMotionTrackerItem.IsDisposed) Then
                        g_UCVirtualMotionTrackerItem.Dispose()
                        g_UCVirtualMotionTrackerItem = Nothing
                    End If
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

End Class
