﻿Imports System.ComponentModel
Imports Rug.Osc

Public Class UCVirtualMotionTracker
    Public g_mUCVirtualControllers As UCVirtualControllers

    Private g_mAutostartMenuStrips As New Dictionary(Of Integer, ToolStripMenuItem)
    Private g_mVMTControllers As New List(Of UCVirtualMotionTrackerItem)
    Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "vmt_devices.ini")

    Public g_ClassOscServer As ClassOscServer

    Public Sub New(_mUCVirtualControllers As UCVirtualControllers)
        g_mUCVirtualControllers = _mUCVirtualControllers

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        g_ClassOscServer = New ClassOscServer

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

    Private Sub Button_StartOscServer_Click(sender As Object, e As EventArgs) Handles Button_StartOscServer.Click
        Try
            g_ClassOscServer.StartServer()

            Button_StartOscServer.Enabled = False

            g_mUCVirtualControllers.g_mFormMain.g_mPSMoveServiceCAPI.StartPoseStream()
            g_mUCVirtualControllers.g_mFormMain.g_mPSMoveServiceCAPI.StartProcessing()
        Catch ex As Exception
            With New Text.StringBuilder
                .AppendLine("Unable to create OSC Server!")
                .AppendLine()
                .AppendLine(ex.Message)

                MessageBox.Show(.ToString)
            End With
        End Try
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
                AddVmtTracker(mAutostartIndexes(i))
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next
    End Sub

    Private Sub AddVmtTracker(id As Integer)
        ' Remove disposed controls
        For i = g_mVMTControllers.Count - 1 To 0 Step -1
            If (g_mVMTControllers(i) Is Nothing OrElse g_mVMTControllers(i).IsDisposed) Then
                g_mVMTControllers.RemoveAt(i)
            End If
        Next

        If (g_mVMTControllers.Count >= ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT) Then
            Throw New ArgumentException("Maximum of trackers reached")
        End If

        Dim mItem As New UCVirtualMotionTrackerItem(id, Me)
        g_mVMTControllers.Add(mItem)

        mItem.Parent = Panel_VMTTrackers
        mItem.Dock = DockStyle.Top
    End Sub

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

    Private Sub LinkLabel_ReadMore_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ReadMore.LinkClicked

    End Sub

    Private Sub CleanUp()
        For Each mItem In g_mVMTControllers
            If (mItem IsNot Nothing AndAlso Not mItem.IsDisposed) Then
                mItem.Dispose()
            End If
        Next
        g_mVMTControllers.Clear()

        For Each mItem In g_mAutostartMenuStrips
            If (mItem.Value IsNot Nothing AndAlso Not mItem.Value.IsDisposed) Then
                mItem.Value.Dispose()
            End If
        Next
        g_mAutostartMenuStrips.Clear()

        If (g_ClassOscServer IsNot Nothing) Then
            g_ClassOscServer.Dispose()
            g_ClassOscServer = Nothing
        End If
    End Sub

    Class ClassOscServer
        Implements IDisposable

        Private g_VmtOsc As ClassOSC = Nothing

        Public Event OnOscProcessBundle(mBundle As OscBundle)
        Public Event OnOscProcessMessage(mMessage As OscMessage)

        Public Sub New()
        End Sub

        Public Sub StartServer()
            If (g_VmtOsc IsNot Nothing) Then
                Return
            End If

            g_VmtOsc = New ClassOSC("127.0.0.1", 39571, 39570, AddressOf __OnOscProcessBundle, AddressOf __OnOscProcessMessage)
        End Sub

        Public Sub Send(mPacket As OscPacket)
            g_VmtOsc.Send(mPacket)
        End Sub

        Public Function IsRunning() As Boolean
            Return (g_VmtOsc IsNot Nothing)
        End Function

        Private Sub __OnOscProcessBundle(mBundle As OscBundle)
            RaiseEvent OnOscProcessBundle(mBundle)
        End Sub

        Private Sub __OnOscProcessMessage(mMessage As OscMessage)
            RaiseEvent OnOscProcessMessage(mMessage)
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    If (g_VmtOsc IsNot Nothing) Then
                        g_VmtOsc.Dispose()
                        g_VmtOsc = Nothing
                    End If

                    ' TODO: dispose managed state (managed objects).
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