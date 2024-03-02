Imports System.ComponentModel

Public Class UCControllerAttachments
    Public g_mUCVirtualControllers As UCVirtualControllers

    Private g_mAutostartMenuStrips As New Dictionary(Of Integer, ToolStripMenuItem)
    Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "attach_devices.ini")

    Class ClassAttachmentListViewItem
        Inherits ListViewItem
        Implements IDisposable

        Private g_UCControllerAttachmentsItem As UCControllerAttachmentsItem
        Private g_UCControllerAttachments As UCControllerAttachments

        Public Sub New(_Id As Integer, _UCControllerAttachments As UCControllerAttachments)
            MyBase.New(New String() {"", ""})

            g_UCControllerAttachments = _UCControllerAttachments
            g_UCControllerAttachmentsItem = New UCControllerAttachmentsItem(_Id, _UCControllerAttachments)

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
        For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
            Dim mItem As New ToolStripMenuItem("Controller ID: " & CStr(i))

            g_mAutostartMenuStrips(i) = mItem

            mItem.Tag = i

            ContextMenuStrip_Autostart.Items.Add(mItem)
        Next

        CreateControl()
    End Sub

    Private Sub UCControllerAttachments_Load(sender As Object, e As EventArgs) Handles Me.Load
        AutostartLoad()
    End Sub

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
                AddAttachment(mAutostartIndexes(i))
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub Button_Autostart_Click(sender As Object, e As EventArgs) Handles Button_Autostart.Click
        ContextMenuStrip_Autostart.Show(Button_Autostart, New Point(0, Button_Autostart.Size.Height))
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

    Private Sub Button_AddAttachment_Click(sender As Object, e As EventArgs) Handles Button_AddAttachment.Click
        Try
            AddAttachment(-1)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_ReadMore_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ReadMore.LinkClicked
        Dim mMsg As New FormRtfHelp
        mMsg.RichTextBox_Help.Rtf = My.Resources.HelpControllerAttachments
        mMsg.ShowDialog(Me)
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
        Try
            Timer_Attachment.Stop()

            For Each mItem As ListViewItem In ListView_Attachments.Items
                Dim mAttachmentItem = DirectCast(mItem, ClassAttachmentListViewItem)
                mAttachmentItem.UpdateItem()
            Next
        Catch ex As Exception
        Finally
            Timer_Attachment.Start()
        End Try
    End Sub
End Class
