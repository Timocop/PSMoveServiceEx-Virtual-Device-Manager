Public Class UCVirtualControllers
    Public g_mUCRemoteDevices As UCRemoteDevices
    Public g_mUCControllerAttachments As UCControllerAttachments
    Public g_mUCVirtualMotionTracker As UCVirtualMotionTracker

    Private g_mControllerSettings As New Dictionary(Of String, ClassServiceConfig.ClassSettingsKey)

    Private g_bIgnoreEvents As Boolean = False

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        g_mUCRemoteDevices = New UCRemoteDevices()
        g_mUCRemoteDevices.Parent = TabPage_RemoteSettings
        g_mUCRemoteDevices.Dock = DockStyle.Fill

        g_mUCControllerAttachments = New UCControllerAttachments()
        g_mUCControllerAttachments.Parent = TabPage_ControllerAttachments
        g_mUCControllerAttachments.Dock = DockStyle.Fill

        g_mUCVirtualMotionTracker = New UCVirtualMotionTracker()
        g_mUCVirtualMotionTracker.Parent = TabPage_VMT
        g_mUCVirtualMotionTracker.Dock = DockStyle.Fill

        Try
            g_bIgnoreEvents = True

            ComboBox_VirtualControllerCount.Items.Clear()
            For i = 0 To ClassPSMoveSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT
                ComboBox_VirtualControllerCount.Items.Add(CStr(i))
            Next
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            LoadSettings()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            g_bIgnoreEvents = False
        End Try

        CreateControl()
    End Sub

    Private Sub ComboBox_VirtualControllerCount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_VirtualControllerCount.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Try
            SettingsChanged(sender)
            SaveSettings()

            MessageBox.Show("Restart PSMoveService to take effect!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            Dim sPsmsPath As String = Environment.ExpandEnvironmentVariables("%AppData%\PSMoveService")
            If (Not IO.Directory.Exists(sPsmsPath)) Then
                Throw New ArgumentException("PSMoveService configs not found. Please setup PSMoveService first.")
            End If

            Dim sTrackerSettings As String = IO.Path.Combine(sPsmsPath, String.Format("{0}.json", CStr(ComboBox_PSmoveEmu.SelectedItem)))
            If (Not IO.File.Exists(sTrackerSettings)) Then
                Return
            End If

            Dim mKey = New ClassServiceConfig.ClassSettingsKey(sTrackerSettings, "psmove_emulation", ClassServiceConfig.ClassSettingsKey.ENUM_TYPE.BOOL)

            Try
                mKey.Load()

                CheckBox_PSmoveEmu.Checked = mKey.m_ValueB
            Catch ex As Exception
                MessageBox.Show(String.Format("Unable to load key '{0}'", mKey.m_SettingsKey), "Error", MessageBoxButtons.OK)
            End Try
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub ComboBox_PSmoveEmu_DropDown(sender As Object, e As EventArgs) Handles ComboBox_PSmoveEmu.DropDown
        Try
            Dim sPsmsPath As String = Environment.ExpandEnvironmentVariables("%AppData%\PSMoveService")
            If (Not IO.Directory.Exists(sPsmsPath)) Then
                Throw New ArgumentException("PSMoveService configs not found. Please setup PSMoveService first.")
            End If

            Dim iSelectedIndex As Integer = ComboBox_PSmoveEmu.SelectedIndex

            Try
                g_bIgnoreEvents = True

                ComboBox_PSmoveEmu.Items.Clear()
                For Each sFile As String In IO.Directory.GetFiles(sPsmsPath, "VirtualController_*.json")
                    ComboBox_PSmoveEmu.Items.Add(IO.Path.GetFileNameWithoutExtension(sFile))
                Next
            Finally
                g_bIgnoreEvents = False
            End Try

            If (ComboBox_PSmoveEmu.Items.Count > 0 AndAlso iSelectedIndex <> ComboBox_PSmoveEmu.SelectedIndex) Then
                ComboBox_PSmoveEmu.SelectedIndex = Math.Min(iSelectedIndex, ComboBox_PSmoveEmu.Items.Count - 1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            Dim sPsmsPath As String = Environment.ExpandEnvironmentVariables("%AppData%\PSMoveService")
            If (Not IO.Directory.Exists(sPsmsPath)) Then
                Throw New ArgumentException("PSMoveService configs not found. Please setup PSMoveService first.")
            End If

            Dim sTrackerSettings As String = IO.Path.Combine(sPsmsPath, String.Format("{0}.json", CStr(ComboBox_PSmoveEmu.SelectedItem)))
            If (Not IO.File.Exists(sTrackerSettings)) Then
                Return
            End If

            Dim mKey = New ClassServiceConfig.ClassSettingsKey(sTrackerSettings, "psmove_emulation", ClassServiceConfig.ClassSettingsKey.ENUM_TYPE.BOOL)

            Try
                mKey.m_ValueB = CheckBox_PSmoveEmu.Checked
                mKey.Save()

                MessageBox.Show("Restart PSMoveService to take effect!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(String.Format("Unable to load key '{0}'", mKey.m_SettingsKey), "Error", MessageBoxButtons.OK)
            End Try
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub LoadSettings()
        Try
            g_bIgnoreEvents = True

            Dim sPsmsPath As String = Environment.ExpandEnvironmentVariables("%AppData%\PSMoveService")
            If (Not IO.Directory.Exists(sPsmsPath)) Then
                Throw New ArgumentException("PSMoveService configs not found. Please setup PSMoveService first.")
            End If

            Dim sTrackerSettings As String = IO.Path.Combine(sPsmsPath, "ControllerManagerConfig.json")

            g_mControllerSettings.Clear()
            g_mControllerSettings(ComboBox_VirtualControllerCount.Name) = New ClassServiceConfig.ClassSettingsKey(sTrackerSettings, "virtual_controller_count", ClassServiceConfig.ClassSettingsKey.ENUM_TYPE.NUM)

            For Each sKey As String In g_mControllerSettings.Keys
                Try
                    g_mControllerSettings(sKey).Load()
                Catch ex As Exception
                    Throw New ArgumentException(String.Format("Unable to load key '{0}'", sKey))
                End Try
            Next

            SetComboBoxValueClamp(ComboBox_VirtualControllerCount, CInt(g_mControllerSettings(ComboBox_VirtualControllerCount.Name).m_ValueF))
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub SaveSettings()
        For Each sKey As String In g_mControllerSettings.Keys
            Try
                Dim mSettingsKey = g_mControllerSettings(sKey)
                If (Not mSettingsKey.m_Loaded) Then
                    Continue For
                End If

                mSettingsKey.Save()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Unable to save some settings", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next
    End Sub

    Private Sub SettingsChanged(sender As Object)
        If (g_bIgnoreEvents) Then
            Return
        End If

        Dim mNumericUpDown = TryCast(sender, NumericUpDown)
        If (mNumericUpDown IsNot Nothing) Then
            If (g_mControllerSettings.ContainsKey(mNumericUpDown.Name)) Then
                g_mControllerSettings(mNumericUpDown.Name).m_ValueF = mNumericUpDown.Value
            End If

            Return
        End If

        Dim mCheckBox = TryCast(sender, CheckBox)
        If (mCheckBox IsNot Nothing) Then
            If (g_mControllerSettings.ContainsKey(mCheckBox.Name)) Then
                g_mControllerSettings(mCheckBox.Name).m_ValueB = mCheckBox.Checked
            End If

            Return
        End If

        Dim mComboBox = TryCast(sender, ComboBox)
        If (mComboBox IsNot Nothing) Then
            If (g_mControllerSettings.ContainsKey(mComboBox.Name)) Then
                g_mControllerSettings(mComboBox.Name).m_ValueF = mComboBox.SelectedIndex
            End If

            Return
        End If
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
        If (g_mUCRemoteDevices IsNot Nothing AndAlso Not g_mUCRemoteDevices.IsDisposed) Then
            g_mUCRemoteDevices.Dispose()
            g_mUCRemoteDevices = Nothing
        End If

        If (g_mUCControllerAttachments IsNot Nothing AndAlso Not g_mUCControllerAttachments.IsDisposed) Then
            g_mUCControllerAttachments.Dispose()
            g_mUCControllerAttachments = Nothing
        End If

        If (g_mUCVirtualMotionTracker IsNot Nothing AndAlso Not g_mUCVirtualMotionTracker.IsDisposed) Then
            g_mUCVirtualMotionTracker.Dispose()
            g_mUCVirtualMotionTracker = Nothing
        End If
    End Sub
End Class
