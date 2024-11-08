Public Class UCVirtualControllers
    Public g_mFormMain As FormMain

    Public g_mUCRemoteDevices As UCRemoteDevices
    Public g_mUCControllerAttachments As UCControllerAttachments

    Private g_bIgnoreEvents As Boolean = False
    Private g_bInit As Boolean = False

    Public Sub New(_mFormMain As FormMain)
        g_mFormMain = _mFormMain

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        g_mUCRemoteDevices = New UCRemoteDevices(Me)
        g_mUCRemoteDevices.SuspendLayout()
        g_mUCRemoteDevices.Parent = TabPage_RemoteSettings
        g_mUCRemoteDevices.Dock = DockStyle.Fill
        g_mUCRemoteDevices.ResumeLayout()

        g_mUCControllerAttachments = New UCControllerAttachments(Me)
        g_mUCControllerAttachments.SuspendLayout()
        g_mUCControllerAttachments.Parent = TabPage_ControllerAttachments
        g_mUCControllerAttachments.Dock = DockStyle.Fill
        g_mUCControllerAttachments.ResumeLayout()

        Try
            g_bIgnoreEvents = True

            ComboBox_VirtualControllerCount.Items.Clear()
            For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT
                ComboBox_VirtualControllerCount.Items.Add(CStr(i))
            Next
        Finally
            g_bIgnoreEvents = False
        End Try

        CreateControl()
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

        g_mUCRemoteDevices.Init()
        g_mUCControllerAttachments.Init()

        Try
            g_bIgnoreEvents = True

            LoadSettings()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub ComboBox_VirtualControllerCount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_VirtualControllerCount.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Try
            Dim mConfig As New ClassServiceConfig(GetConfig())
            mConfig.LoadConfig()
            mConfig.SetValue("", "virtual_controller_count", CInt(ComboBox_VirtualControllerCount.SelectedItem))
            mConfig.SaveConfig()

            g_mFormMain.PromptRestartPSMoveService()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ComboBox_PSmoveEmu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_PSmoveEmu.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Try
            g_bIgnoreEvents = True

            If (ComboBox_PSmoveEmu.SelectedItem Is Nothing) Then
                Return
            End If

            Try
                Dim mConfig As New ClassServiceConfig(GetCustomConfig(CStr(ComboBox_PSmoveEmu.SelectedItem)))
                mConfig.LoadConfig()
                CheckBox_PSmoveEmu.Checked = mConfig.GetValue(Of Boolean)("", "psmove_emulation", False)
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
            End Try
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub ComboBox_PSmoveEmu_DropDown(sender As Object, e As EventArgs) Handles ComboBox_PSmoveEmu.DropDown
        Try
            Dim sConfigPath As String = ClassServiceConfig.GetConfigPath
            If (sConfigPath Is Nothing) Then
                Return
            End If

            Dim iSelectedIndex As Integer = ComboBox_PSmoveEmu.SelectedIndex

            Try
                g_bIgnoreEvents = True

                ComboBox_PSmoveEmu.Items.Clear()
                For Each sFile As String In IO.Directory.GetFiles(sConfigPath, "VirtualController_*.json")
                    ComboBox_PSmoveEmu.Items.Add(IO.Path.GetFileNameWithoutExtension(sFile))
                Next
            Finally
                g_bIgnoreEvents = False
            End Try

            If (ComboBox_PSmoveEmu.Items.Count > 0 AndAlso iSelectedIndex <> ComboBox_PSmoveEmu.SelectedIndex) Then
                ComboBox_PSmoveEmu.SelectedIndex = Math.Min(iSelectedIndex, ComboBox_PSmoveEmu.Items.Count - 1)
            End If

        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub CheckBox_PSmoveEmu_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_PSmoveEmu.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Try
            g_bIgnoreEvents = True

            If (ComboBox_PSmoveEmu.SelectedItem Is Nothing) Then
                CheckBox_PSmoveEmu.Checked = Not CheckBox_PSmoveEmu.Checked
                Return
            End If

            Try
                Dim mConfig As New ClassServiceConfig(GetCustomConfig(CStr(ComboBox_PSmoveEmu.SelectedItem)))
                mConfig.LoadConfig()
                mConfig.SetValue(Of Boolean)("", "psmove_emulation", CheckBox_PSmoveEmu.Checked)
                mConfig.SaveConfig()

                g_mFormMain.PromptRestartPSMoveService()
            Catch ex As Exception
                ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
            End Try
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Function GetCustomConfig(sName As String) As String
        Dim sConfigPath As String = ClassServiceConfig.GetConfigPath()
        If (sConfigPath Is Nothing) Then
            Return Nothing
        End If

        Return IO.Path.Combine(sConfigPath, String.Format("{0}.json", sName))
    End Function

    Private Function GetConfig() As String
        Dim sConfigPath As String = ClassServiceConfig.GetConfigPath()
        If (sConfigPath Is Nothing) Then
            Return Nothing
        End If

        Return IO.Path.Combine(sConfigPath, "ControllerManagerConfig.json")
    End Function

    Private Sub LoadSettings()
        Try
            g_bIgnoreEvents = True

            Dim mConfig As New ClassServiceConfig(GetConfig())
            mConfig.LoadConfig()

            SetComboBoxValueClamp(ComboBox_VirtualControllerCount, mConfig.GetValue(Of Integer)("", "virtual_controller_count", 0))
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub SetNumericUpDownValueClamp(mControl As NumericUpDown, iValue As Decimal)
        mControl.Value = Math.Max(mControl.Minimum, Math.Min(mControl.Maximum, iValue))
    End Sub

    Private Sub SetComboBoxValueClamp(mControl As ComboBox, iValue As Integer)
        If (mControl.Items.Count = 0) Then
            Return
        End If

        mControl.SelectedIndex = Math.Max(0, Math.Min(mControl.Items.Count - 1, iValue))
    End Sub

    Private Sub CleanUp()
        If (g_mUCControllerAttachments IsNot Nothing AndAlso Not g_mUCControllerAttachments.IsDisposed) Then
            g_mUCControllerAttachments.Dispose()
            g_mUCControllerAttachments = Nothing
        End If

        If (g_mUCRemoteDevices IsNot Nothing AndAlso Not g_mUCRemoteDevices.IsDisposed) Then
            g_mUCRemoteDevices.Dispose()
            g_mUCRemoteDevices = Nothing
        End If
    End Sub
End Class
