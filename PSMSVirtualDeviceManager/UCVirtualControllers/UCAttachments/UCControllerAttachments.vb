Imports System.ComponentModel

Public Class UCControllerAttachments
    Private g_mAutostartMenuStrips As New Dictionary(Of Integer, ToolStripMenuItem)
    Private g_mAttachments As New List(Of UCControllerAttachmentsItem)
    Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "attach_devices.ini")

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        For i = 0 To ClassPSMoveSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
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
                For i = 0 To ClassPSMoveSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
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
        ' Remove disposed controls
        For i = g_mAttachments.Count - 1 To 0 Step -1
            If (g_mAttachments(i) Is Nothing OrElse g_mAttachments(i).IsDisposed) Then
                g_mAttachments.RemoveAt(i)
            End If
        Next

        If (g_mAttachments.Count >= ClassPSMoveSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT) Then
            Throw New ArgumentException("Maximum of attachments reached")
        End If

        Dim mItem As New UCControllerAttachmentsItem(id, Me)
        g_mAttachments.Add(mItem)

        mItem.Parent = Panel_Attachments
        mItem.Dock = DockStyle.Top
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
                For i = 0 To ClassPSMoveSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
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
        Dim mMsg As New FormAttachmentsHelp
        mMsg.ShowDialog(Me)
    End Sub

    Private Sub CleanUp()
        For Each mItem In g_mAttachments
            If (mItem IsNot Nothing AndAlso Not mItem.IsDisposed) Then
                mItem.Dispose()
            End If
        Next
        g_mAttachments.Clear()

        For Each mItem In g_mAutostartMenuStrips
            If (mItem.Value IsNot Nothing AndAlso Not mItem.Value.IsDisposed) Then
                mItem.Value.Dispose()
            End If
        Next
        g_mAutostartMenuStrips.Clear()
    End Sub
End Class
