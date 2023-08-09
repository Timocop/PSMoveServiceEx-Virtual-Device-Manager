Partial Class UCVirtualMotionTracker

    Private Sub Button_PlaySpaceManualCalib_Click(sender As Object, e As EventArgs) Handles Button_PlaySpaceManualCalib.Click
        'TODO
    End Sub

    Private Sub LinkLabel_PlayCalibShowSettings_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_PlayCalibShowSettings.LinkClicked
        TabControl1.SelectedTab = TabPage_Settings
        CheckBox_PlayCalibEnabled.Focus()
    End Sub

    Private Sub ComboBox_PlayCalibControllerID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_PlayCalibControllerID.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_PlayspaceSettings.iRecenterControllerID = CInt(ComboBox_PlayCalibControllerID.SelectedItem)
        'g_ClassControllerSettings.SetUnsavedState(True) 'Make it optional
    End Sub
End Class
