Public Class UCVirtualControllers
    Private g_mFormMain As FormMain

    Private g_mControllerSettings As New Dictionary(Of String, ClassServiceConfig.ClassSettingsKey)

    Private g_bIgnoreEvents As Boolean = False

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

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

End Class
