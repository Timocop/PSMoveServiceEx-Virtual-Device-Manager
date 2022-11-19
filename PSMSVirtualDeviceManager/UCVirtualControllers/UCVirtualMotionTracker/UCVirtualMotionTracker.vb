Imports System.ComponentModel
Imports System.Web.Script.Serialization
Imports Rug.Osc

Public Class UCVirtualMotionTracker
    Public g_mUCVirtualControllers As UCVirtualControllers

    Private g_mAutostartMenuStrips As New Dictionary(Of Integer, ToolStripMenuItem)
    Private g_mVMTControllers As New List(Of UCVirtualMotionTrackerItem)
    Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "vmt_devices.ini")

    Public g_ClassOscServer As ClassOscServer
    Public g_ClassTrackerOverrides As ClassTrackerOverrides

    Public Sub New(_mUCVirtualControllers As UCVirtualControllers)
        g_mUCVirtualControllers = _mUCVirtualControllers

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        g_ClassOscServer = New ClassOscServer
        g_ClassTrackerOverrides = New ClassTrackerOverrides(Me)

        For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
            Dim mItem As New ToolStripMenuItem("Controller ID: " & CStr(i))

            g_mAutostartMenuStrips(i) = mItem

            mItem.Tag = i

            ContextMenuStrip_Autostart.Items.Add(mItem)
        Next

        CreateControl()

        Panel_SteamVRRestart.Visible = False
    End Sub

    Private Sub UCControllerAttachments_Load(sender As Object, e As EventArgs) Handles Me.Load
        AutostartLoad()

        Try
            RefreshOverrides()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    Class ClassTrackerOverrides
        Const STEAM_INSTALL_PATH_REGISTRY As String = "SOFTWARE\WOW6432Node\Valve\Steam"

        Private g_mUCVirtualMotionTracker As UCVirtualMotionTracker

        Private g_bConfigLoaded As Boolean = False
        Private g_mConfig As New Dictionary(Of String, Object)

        Enum ENUM_OVERRIDE_TYPE
            HEAD
            LEFT_HAND
            RIGHT_HAND
        End Enum

        Sub New(_mUCVirtualMotionTracker As UCVirtualMotionTracker)
            g_mUCVirtualMotionTracker = _mUCVirtualMotionTracker
        End Sub

        ReadOnly Property m_SteamPath As String
            Get
                Return CStr(My.Computer.Registry.LocalMachine.OpenSubKey(STEAM_INSTALL_PATH_REGISTRY, False).GetValue("InstallPath", Nothing))
            End Get
        End Property

        ReadOnly Property m_OverrideTypeName(iType As ENUM_OVERRIDE_TYPE) As String
            Get
                Select Case (iType)
                    Case ENUM_OVERRIDE_TYPE.HEAD
                        Return "/user/head"
                    Case ENUM_OVERRIDE_TYPE.LEFT_HAND
                        Return "/user/hand/left"
                    Case ENUM_OVERRIDE_TYPE.RIGHT_HAND
                        Return "/user/hand/right"
                End Select

                Return Nothing
            End Get
        End Property

        Public Sub LoadConfig()
            Dim sSteamPath As String = m_SteamPath
            If (sSteamPath Is Nothing) Then
                Return
            End If

            Dim sConfigPath As String = IO.Path.Combine(sSteamPath, "config\steamvr.vrsettings")
            If (Not IO.File.Exists(sConfigPath)) Then
                Return
            End If

            Dim sContent As String = IO.File.ReadAllText(sConfigPath)

            Dim mTmp As Object = Nothing
            g_mConfig = (New JavaScriptSerializer).Deserialize(Of Dictionary(Of String, Object))(sContent)

            g_bConfigLoaded = True
        End Sub

        Public Sub SetOverride(sTrackerName As String, sTrackerToOverride As String)
            If (Not g_mConfig.ContainsKey("TrackingOverrides")) Then
                g_mConfig("TrackingOverrides") = New Dictionary(Of String, Object)
            End If

            Dim mScansDic = TryCast(g_mConfig("TrackingOverrides"), Dictionary(Of String, Object))

            mScansDic(sTrackerName) = sTrackerToOverride
        End Sub

        Public Sub RemoveOverride(sTrackerName As String)
            If (Not g_mConfig.ContainsKey("TrackingOverrides")) Then
                g_mConfig("TrackingOverrides") = New Dictionary(Of String, Object)
            End If

            Dim mScansDic = TryCast(g_mConfig("TrackingOverrides"), Dictionary(Of String, Object))
            If (Not mScansDic.ContainsKey(sTrackerName)) Then
                Return
            End If

            mScansDic.Remove(sTrackerName)
        End Sub

        Public Function GetOverride(sTrackerName As String) As String
            Dim mOverrides = GetOverrides()

            For Each mItem In mOverrides
                If (mItem.Key = sTrackerName) Then
                    Return mItem.Value
                End If
            Next

            Return Nothing
        End Function

        Public Function GetOverrides() As KeyValuePair(Of String, String)()
            Dim mOverides As New List(Of KeyValuePair(Of String, String))

            If (Not g_mConfig.ContainsKey("TrackingOverrides")) Then
                Return mOverides.ToArray
            End If

            Dim mOverrideDic = TryCast(g_mConfig("TrackingOverrides"), Dictionary(Of String, Object))
            If (mOverrideDic Is Nothing) Then
                Return mOverides.ToArray
            End If

            For Each mItem In mOverrideDic
                mOverides.Add(New KeyValuePair(Of String, String)(mItem.Key, CStr(mItem.Value)))
            Next

            Return mOverides.ToArray
        End Function

        Public Sub SaveConfig()
            If (Not g_bConfigLoaded) Then
                Return
            End If

            Dim sSteamPath As String = m_SteamPath
            If (sSteamPath Is Nothing) Then
                Return
            End If

            Dim sConfigPath As String = IO.Path.Combine(sSteamPath, "config\steamvr.vrsettings")
            If (Not IO.File.Exists(sConfigPath)) Then
                Return
            End If

            Dim sContent = FormatOutput((New JavaScriptSerializer).Serialize(g_mConfig))

            IO.File.WriteAllText(sConfigPath, sContent)
        End Sub

        Private Function FormatOutput(ByVal sContent As String) As String
            Dim mText = New Text.StringBuilder()
            Dim bEscape As Boolean = False
            Dim bQuotes As Boolean = False
            Dim iInt As Integer = 0

            For Each iChar As Char In sContent
                If (bEscape) Then
                    bEscape = False
                    mText.Append(iChar)
                Else
                    If (iChar = "\"c) Then
                        bEscape = True
                        mText.Append(iChar)
                    ElseIf (iChar = """"c) Then
                        bQuotes = Not bQuotes
                        mText.Append(iChar)
                    ElseIf (Not bQuotes) Then
                        If (iChar = ","c) Then
                            mText.Append(iChar)
                            mText.Append(vbCrLf)
                            mText.Append(CChar(vbTab), iInt)
                        ElseIf iChar = "["c OrElse iChar = "{"c Then
                            iInt += 1

                            mText.Append(iChar)
                            mText.Append(vbCrLf)
                            mText.Append(CChar(vbTab), iInt)
                        ElseIf (iChar = "]"c) OrElse (iChar = "}"c) Then
                            iInt -= 1

                            mText.Append(vbCrLf)
                            mText.Append(CChar(vbTab), iInt)
                            mText.Append(iChar)
                        ElseIf (iChar = ":"c) Then
                            mText.Append(iChar)
                            mText.Append(vbTab)
                        ElseIf (Not Char.IsWhiteSpace(iChar)) Then
                            mText.Append(iChar)
                        End If
                    Else
                        mText.Append(iChar)
                    End If
                End If
            Next

            Return mText.ToString()
        End Function
    End Class

    Private Sub Button_Add_Click(sender As Object, e As EventArgs) Handles Button_Add.Click
        Try
            Using i As New FormTrackerOverrideSetup
                If (i.ShowDialog = DialogResult.OK) Then
                    Dim mResult = i.m_DialogResult

                    Dim sTracker As String = ""
                    Dim sOverride As String = ""

                    If (mResult.bCustomTracker) Then
                        sTracker = mResult.sCustomTrackerName
                    Else
                        sTracker = String.Format("/devices/vmt/VMT_{0}", mResult.iVMTTracker)
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

                    If (g_ClassTrackerOverrides.GetOverride(sTracker) IsNot Nothing) Then
                        If (MessageBox.Show(String.Format("A tracker with the name '{0}' already exists! Do you want to override the tracker override with the current one?", sTracker), "Override?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No) Then
                            Return
                        End If
                    End If

                        g_ClassTrackerOverrides.SetOverride(sTracker, sOverride)
                        g_ClassTrackerOverrides.SaveConfig()

                    RefreshOverrides()

                    g_mUCVirtualControllers.g_mUCVirtualMotionTracker.Panel_SteamVRRestart.Visible = True
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button_Remove_Click(sender As Object, e As EventArgs) Handles Button_Remove.Click
        Try
            If (ListView_Overrides.SelectedItems.Count < 1) Then
                Return
            End If

            Dim sMessage As New Text.StringBuilder
            sMessage.AppendLine("Are you sure you want to delere following trackers from the overrides?")
            sMessage.AppendLine()
            For Each mSelectedItem As ListViewItem In ListView_Overrides.SelectedItems
                sMessage.AppendLine(mSelectedItem.SubItems(0).Text)
            Next
            If (MessageBox.Show(sMessage.ToString, "Remove overrides", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No) Then
                Return
            End If

            For Each mSelectedItem As ListViewItem In ListView_Overrides.SelectedItems
                g_ClassTrackerOverrides.RemoveOverride(mSelectedItem.SubItems(0).Text)
            Next
            g_ClassTrackerOverrides.SaveConfig()

            RefreshOverrides()

            g_mUCVirtualControllers.g_mUCVirtualMotionTracker.Panel_SteamVRRestart.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button_Refresh_Click(sender As Object, e As EventArgs) Handles Button_Refresh.Click
        Try
            RefreshOverrides()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RefreshOverrides()
        ListView_Overrides.Items.Clear()

        g_ClassTrackerOverrides.LoadConfig()

        For Each mOverride In g_ClassTrackerOverrides.GetOverrides()
            ListView_Overrides.Items.Add(New ListViewItem(New String() {mOverride.Key, mOverride.Value}))
        Next
    End Sub

    Private Sub LinkLabel_SteamVRRestartOff_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_SteamVRRestartOff.LinkClicked
        g_mUCVirtualControllers.g_mUCVirtualMotionTracker.Panel_SteamVRRestart.Visible = False
    End Sub
End Class
