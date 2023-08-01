Public Class FormSteamSettings
    Private g_bSettingsLoaded As Boolean = False
    Private g_sForcedDriver As String = ""

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            LoadSettings()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If (g_sForcedDriver IsNot Nothing) Then
            If (g_sForcedDriver.Length > 0 AndAlso g_sForcedDriver <> "null") Then
                'Its another driver
                CheckBox_ForceNullDriver.Enabled = False
                LinkLabel_ResetForcedDrvier.Visible = True
            End If
        End If

    End Sub

    Private Sub FormSteamSettings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (Me.DialogResult <> DialogResult.OK) Then
            Return
        End If

        Try
            SaveSettings()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_ResetForcedDrvier_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ResetForcedDrvier.LinkClicked
        CheckBox_ForceNullDriver.Enabled = True
        LinkLabel_ResetForcedDrvier.Visible = False
    End Sub

    Private Sub LoadSettings()
        Dim mSteamConfig As New ClassSteamVRConfig
        mSteamConfig.LoadConfig()

        g_sForcedDriver = mSteamConfig.m_ClassSettings.m_ForcedDriver

        CheckBox_EnableNullDriver.Checked = mSteamConfig.m_ClassSettings.m_NullHmdEnabled
        CheckBox_ForceNullDriver.Checked = (mSteamConfig.m_ClassSettings.m_ForcedDriver = "null")
        CheckBox_RequireHmd.Checked = mSteamConfig.m_ClassSettings.m_RequireHmd
        CheckBox_EnableMultipleDrivers.Checked = mSteamConfig.m_ClassSettings.m_ActivateMultipleDrivers
        CheckBox_EnableHome.Checked = mSteamConfig.m_ClassSettings.m_EnableHomeApp
        CheckBox_EnableMirror.Checked = mSteamConfig.m_ClassSettings.m_EnableMirrorView
        CheckBox_EnablePerfGraph.Checked = mSteamConfig.m_ClassSettings.m_EnablePerformanceGraph

        g_bSettingsLoaded = True
    End Sub

    Private Sub SaveSettings()
        If (Not g_bSettingsLoaded) Then
            Return
        End If

        Dim mSteamConfig As New ClassSteamVRConfig
        mSteamConfig.LoadConfig()

        mSteamConfig.m_ClassSettings.m_NullHmdEnabled = CheckBox_EnableNullDriver.Checked

        If (CheckBox_ForceNullDriver.Enabled) Then
            mSteamConfig.m_ClassSettings.m_ForcedDriver = If(CheckBox_ForceNullDriver.Checked, "null", "")
        End If

        mSteamConfig.m_ClassSettings.m_RequireHmd = CheckBox_RequireHmd.Checked
        mSteamConfig.m_ClassSettings.m_ActivateMultipleDrivers = CheckBox_EnableMultipleDrivers.Checked
        mSteamConfig.m_ClassSettings.m_EnableHomeApp = CheckBox_EnableHome.Checked
        mSteamConfig.m_ClassSettings.m_EnableMirrorView = CheckBox_EnableMirror.Checked
        mSteamConfig.m_ClassSettings.m_EnablePerformanceGraph = CheckBox_EnablePerfGraph.Checked

        mSteamConfig.SaveConfig()
    End Sub
End Class