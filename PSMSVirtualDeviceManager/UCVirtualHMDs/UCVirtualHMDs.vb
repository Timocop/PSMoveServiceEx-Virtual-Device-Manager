Public Class UCVirtualHMDs
    Private g_mHMDSettings As New Dictionary(Of String, ClassServiceConfig.ClassSettingsKey)

    Private g_bIgnoreEvents As Boolean = False

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Try
            g_bIgnoreEvents = True

            ComboBox_VirtualHMDCount.Items.Clear()
            For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_HMD_COUNT
                ComboBox_VirtualHMDCount.Items.Add(CStr(i))
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

    Private Sub ComboBox_VirtualHMDCount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_VirtualHMDCount.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Try
            SettingsChanged(sender)
            SaveSettings()

            If (Process.GetProcessesByName("PSMoveService").Count > 0) Then
                MessageBox.Show("Restart PSMoveServiceEx to take effect!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadSettings()
        Try
            g_bIgnoreEvents = True

            Dim sPsmsPath As String = Environment.ExpandEnvironmentVariables("%AppData%\PSMoveService")
            If (Not IO.Directory.Exists(sPsmsPath)) Then
                Return
            End If

            Dim sTrackerSettings As String = IO.Path.Combine(sPsmsPath, "HMDManagerConfig.json")

            g_mHMDSettings.Clear()
            g_mHMDSettings(ComboBox_VirtualHMDCount.Name) = New ClassServiceConfig.ClassSettingsKey(sTrackerSettings, "virtual_hmd_count", ClassServiceConfig.ClassSettingsKey.ENUM_TYPE.NUM)

            For Each sKey As String In g_mHMDSettings.Keys
                Try
                    g_mHMDSettings(sKey).Load()
                Catch ex As Exception
                    Throw New ArgumentException(String.Format("Unable to load key '{0}'", sKey))
                End Try
            Next

            SetComboBoxValueClamp(ComboBox_VirtualHMDCount, CInt(g_mHMDSettings(ComboBox_VirtualHMDCount.Name).m_ValueF))
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub SaveSettings()
        For Each sKey As String In g_mHMDSettings.Keys
            Try
                Dim mSettingsKey = g_mHMDSettings(sKey)
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
            If (g_mHMDSettings.ContainsKey(mNumericUpDown.Name)) Then
                g_mHMDSettings(mNumericUpDown.Name).m_ValueF = mNumericUpDown.Value
            End If

            Return
        End If

        Dim mCheckBox = TryCast(sender, CheckBox)
        If (mCheckBox IsNot Nothing) Then
            If (g_mHMDSettings.ContainsKey(mCheckBox.Name)) Then
                g_mHMDSettings(mCheckBox.Name).m_ValueB = mCheckBox.Checked
            End If

            Return
        End If

        Dim mComboBox = TryCast(sender, ComboBox)
        If (mComboBox IsNot Nothing) Then
            If (g_mHMDSettings.ContainsKey(mComboBox.Name)) Then
                g_mHMDSettings(mComboBox.Name).m_ValueF = mComboBox.SelectedIndex
            End If

            Return
        End If
    End Sub

    Private Sub SetComboBoxValueClamp(mControl As ComboBox, iValue As Integer)
        If (mControl.Items.Count = 0) Then
            Return
        End If

        mControl.SelectedIndex = Math.Max(0, Math.Min(mControl.Items.Count - 1, iValue))
    End Sub
End Class
