Imports System.ComponentModel

Public Class UCControllerAttachments
    Public g_mUCVirtualControllers As UCVirtualControllers
    Private g_bInit As Boolean = False
    Private Shared g_mThreadLock As New Object

    Private g_mAutostartMenuStrips As New Dictionary(Of Integer, ToolStripMenuItem)

    Class ClassAttachmentListViewItem
        Inherits ListViewItem
        Implements IDisposable

        Public g_UCControllerAttachmentsItem As UCControllerAttachmentsItem
        Public g_UCControllerAttachments As UCControllerAttachments

        Public Sub New(_Id As Integer, _UCControllerAttachments As UCControllerAttachments)
            MyBase.New(New String() {"", ""})

            g_UCControllerAttachments = _UCControllerAttachments
            g_UCControllerAttachmentsItem = New UCControllerAttachmentsItem(_Id, _UCControllerAttachments)
            g_UCControllerAttachmentsItem.Init()

            UpdateItem()
        End Sub

        Public Sub UpdateItem()
            Const LISTVIEW_SUBITEM_INDEX As Integer = 0
            Const LISTVIEW_SUBITEM_PARENTID As Integer = 1

            If (g_UCControllerAttachmentsItem Is Nothing OrElse g_UCControllerAttachmentsItem.IsDisposed) Then
                Return
            End If

            If (g_UCControllerAttachmentsItem.g_mClassIO Is Nothing) Then
                Return
            End If

            Me.SubItems(LISTVIEW_SUBITEM_INDEX).Text = CStr(g_UCControllerAttachmentsItem.g_mClassIO.m_Index)
            Me.SubItems(LISTVIEW_SUBITEM_PARENTID).Text = CStr(g_UCControllerAttachmentsItem.g_mClassIO.m_ParentController)

            'Is there any error?
            If (g_UCControllerAttachmentsItem.m_HasStatusError) Then
                Me.BackColor = Color.FromArgb(255, 192, 192)
            Else
                Me.BackColor = Color.FromArgb(255, 255, 255)
            End If
        End Sub

        Property m_Visible As Boolean
            Get
                If (g_UCControllerAttachmentsItem Is Nothing OrElse g_UCControllerAttachmentsItem.IsDisposed) Then
                    Return False
                End If

                Return (g_UCControllerAttachmentsItem.Parent Is g_UCControllerAttachments.Panel_Attachments)
            End Get
            Set(value As Boolean)
                If (g_UCControllerAttachmentsItem Is Nothing OrElse g_UCControllerAttachmentsItem.IsDisposed) Then
                    Return
                End If

                If (value) Then
                    g_UCControllerAttachmentsItem.Parent = g_UCControllerAttachments.Panel_Attachments
                    g_UCControllerAttachmentsItem.Dock = DockStyle.Top
                    g_UCControllerAttachmentsItem.Visible = True
                Else
                    g_UCControllerAttachmentsItem.Parent = Nothing
                    g_UCControllerAttachmentsItem.Visible = False
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
                    If (g_UCControllerAttachmentsItem IsNot Nothing AndAlso Not g_UCControllerAttachmentsItem.IsDisposed) Then
                        g_UCControllerAttachmentsItem.Dispose()
                        g_UCControllerAttachmentsItem = Nothing
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

    Public Sub New(_mUCVirtualControllers As UCVirtualControllers)
        g_mUCVirtualControllers = _mUCVirtualControllers

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        UcInformation1.m_ReadMoreAction = AddressOf ShowHelpControllerAttachmentsHelp

        For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
            Dim mItem As New ToolStripMenuItem("Controller ID: " & CStr(i))

            g_mAutostartMenuStrips(i) = mItem

            mItem.Tag = i

            ContextMenuStrip_Autostart.Items.Add(mItem)
        Next

        CreateControl()
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

        AutostartLoad()
    End Sub

    Private Sub AutostartLoad()
        Dim mAutostartIndexes As New List(Of Integer)

        SyncLock g_mThreadLock
            Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_ATTACHMENT, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
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
        End SyncLock

        For i = 0 To mAutostartIndexes.Count - 1
            Try
                AddAttachment(mAutostartIndexes(i))
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
            End Try
        Next
    End Sub

    Private Sub AddAttachment(id As Integer)
        If (ListView_Attachments.Items.Count >= ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT) Then
            Throw New ArgumentException("Maximum of attachments reached")
        End If

        Dim mItem = New ClassAttachmentListViewItem(id, Me)
        ListView_Attachments.Items.Add(mItem)
        mItem.Selected = True
    End Sub

    Public Function GetAttachments() As UCControllerAttachmentsItem()
        Dim mAttachments As New List(Of UCControllerAttachmentsItem)

        For Each mItem As ListViewItem In ListView_Attachments.Items
            Dim mAttachmentItem = DirectCast(mItem, ClassAttachmentListViewItem)
            If (mAttachmentItem.g_UCControllerAttachmentsItem Is Nothing OrElse mAttachmentItem.g_UCControllerAttachmentsItem.IsDisposed) Then
                Continue For
            End If

            mAttachments.Add(mAttachmentItem.g_UCControllerAttachmentsItem)
        Next

        Return mAttachments.ToArray
    End Function

    Private Sub Button_Autostart_Click(sender As Object, e As EventArgs) Handles Button_Autostart.Click
        ContextMenuStrip_Autostart.Show(Button_Autostart, New Point(0, Button_Autostart.Size.Height))
    End Sub

    Private Sub ContextMenuStrip_Autostart_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ContextMenuStrip_Autostart.ItemClicked
        Try
            SyncLock g_mThreadLock
                Dim mItem As ToolStripMenuItem = TryCast(e.ClickedItem, ToolStripMenuItem)
                If (mItem Is Nothing) Then
                    Return
                End If

                mItem.Checked = Not mItem.Checked

                Dim iIndex As Integer = CInt(mItem.Tag)

                Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_ATTACHMENT, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)
                        Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("Autostart", CStr(iIndex), If(mItem.Checked, "true", "false")))

                        mIni.WriteKeyValue(mIniContent.ToArray)
                    End Using
                End Using
            End SyncLock
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ContextMenuStrip_Autostart_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip_Autostart.Opening
        Try
            SyncLock g_mThreadLock
                Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_ATTACHMENT, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)
                        For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                            If (g_mAutostartMenuStrips(i) Is Nothing OrElse g_mAutostartMenuStrips(i).IsDisposed) Then
                                Continue For
                            End If

                            g_mAutostartMenuStrips(i).Checked = (mIni.ReadKeyValue("Autostart", CStr(i), "false") = "true")
                        Next
                    End Using
                End Using
            End SyncLock
        Catch ex As Exception
        ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub Button_AddAttachment_Click(sender As Object, e As EventArgs) Handles Button_AddAttachment.Click
        Try
            AddAttachment(-1)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ShowHelpControllerAttachmentsHelp()
        Dim mMsg As New FormRtfHelp
        mMsg.RichTextBox_Help.Rtf = My.Resources.HelpControllerAttachments
        mMsg.ShowDialog(g_mUCVirtualControllers.g_mFormMain)
    End Sub

    Private Sub CleanUp()
        For Each mItem As ListViewItem In ListView_Attachments.Items
            Dim mAttachmentItem = DirectCast(mItem, ClassAttachmentListViewItem)

            mAttachmentItem.Dispose()
        Next
        ListView_Attachments.Items.Clear()

        For Each mItem In g_mAutostartMenuStrips
            If (mItem.Value IsNot Nothing AndAlso Not mItem.Value.IsDisposed) Then
                mItem.Value.Dispose()
            End If
        Next
        g_mAutostartMenuStrips.Clear()
    End Sub

    Private Sub ToolStripMenuItem_AttachmentRemove_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_AttachmentRemove.Click
        If (ListView_Attachments.SelectedItems.Count < 1) Then
            Return
        End If

        Dim mAttachmentItem = DirectCast(ListView_Attachments.SelectedItems(0), ClassAttachmentListViewItem)
        ListView_Attachments.Items.Remove(mAttachmentItem)

        mAttachmentItem.Dispose()
    End Sub

    Private Sub ListView_Attachments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_Attachments.SelectedIndexChanged
        For Each mItem As ListViewItem In ListView_Attachments.Items
            Dim mAttachmentItem = DirectCast(mItem, ClassAttachmentListViewItem)
            If (mAttachmentItem.Selected) Then
                If (Not mAttachmentItem.m_Visible) Then
                    mAttachmentItem.m_Visible = True
                End If
            Else
                If (mAttachmentItem.m_Visible) Then
                    mAttachmentItem.m_Visible = False
                End If
            End If
        Next
    End Sub

    Private Sub Timer_Attachment_Tick(sender As Object, e As EventArgs) Handles Timer_Attachment.Tick
        Timer_Attachment.Stop()

        Try
            If (Me.Visible) Then
                For Each mItem As ListViewItem In ListView_Attachments.Items
                    Dim mAttachmentItem = DirectCast(mItem, ClassAttachmentListViewItem)
                    mAttachmentItem.UpdateItem()
                Next
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try

        Timer_Attachment.Start()
    End Sub
End Class
