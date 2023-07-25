Partial Public Class UCVirtualMotionTracker
    Private Sub LinkLabel_JoystickShortcutsInfo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_JoystickShortcutsInfo.LinkClicked
        Dim sHelp As New Text.StringBuilder
        sHelp.AppendLine("Joystick values can be bound to buttons on the controller so you dont have to move your controller for joystick emulation.")
        sHelp.AppendLine("For example if you want to bind SQUARE with joystick forward and CROSS with joystick backwards to move forward and backwards with 2 buttons instead of the MOVE button and moving the controller.")
        sHelp.AppendLine("This makes it easier to navigate in games.")
        sHelp.AppendLine()
        sHelp.AppendLine("HOW TO BIND:")
        sHelp.AppendLine("On your PSMove controller, hold both the MOVE button and the button you want to bind the joystick value to and move the controller in any direction.")
        sHelp.AppendLine("Release both buttons to accept.")
        sHelp.AppendLine("The saved joystick value will be applied when pressing the button now.")
        sHelp.AppendLine()
        sHelp.AppendLine("HOW TO UNBIND:")
        sHelp.AppendLine("Quickly press both the MOVE button and the button you want to unbind. Done.")

        MessageBox.Show(sHelp.ToString, "Joystick Shortcut Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub CheckBox_JoystickShortcuts_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_JoystickShortcuts.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_JoystickShortcutBinding = CheckBox_JoystickShortcuts.Checked
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_JoystickShortcutClick_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_JoystickShortcutClick.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_JoystickShortcutTouchpadClick = CheckBox_JoystickShortcutClick.Checked
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_TouchpadClickMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_TouchpadClickMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_HtcTouchpadEmulationClickMethod = CType(ComboBox_TouchpadClickMethod.SelectedIndex, ClassControllerSettings.ENUM_HTC_TOUCHPAD_CLICK_METHOD)
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_GrabButtonMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_GrabButtonMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_HtcGripButtonMethod = CType(ComboBox_GrabButtonMethod.SelectedIndex, ClassControllerSettings.ENUM_HTC_GRIP_BUTTON_METHOD)
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_DisableBasestations_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_DisableBasestations.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_DisableBaseStationSpawning = CheckBox_DisableBasestations.Checked
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_EnableHeptics_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_EnableHeptics.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_EnableHepticFeedback = CheckBox_EnableHeptics.Checked
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub Button_SaveControllerSettings_Click(sender As Object, e As EventArgs) Handles Button_SaveControllerSettings.Click
        Try
            g_ClassControllerSettings.SaveSettings()
            g_ClassControllerSettings.SetUnsavedState(False)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
