Partial Public Class UCVirtualMotionTracker

    Public Class ClassRecenterDeviceItem
        Private g_sName As String = ""
        Private g_sDisplayName As String = ""

        Public Sub New(_Name As String)
            Me.New(_Name, _Name)
        End Sub

        Public Sub New(_Name As String, _DisplayNameIfEEmpty As String)
            g_sName = _Name
            g_sDisplayName = _Name

            If (String.IsNullOrEmpty(g_sName) OrElse g_sName.TrimEnd.Length = 0) Then
                g_sDisplayName = _DisplayNameIfEEmpty
            End If
        End Sub

        Public Function GetRealName() As String
            Return g_sName
        End Function

        Public Overrides Function ToString() As String
            Return g_sDisplayName
        End Function
    End Class

    Private Sub LinkLabel_JoystickShortcutsInfo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_JoystickShortcutsInfo.LinkClicked
        Dim sHelp As New Text.StringBuilder
        sHelp.AppendLine("Touchpad axis can be bound to buttons on the controller so you dont have to move your controller for touchpad emulation.")
        sHelp.AppendLine("For example if you want to bind SQUARE with touchpad forward and CROSS with touchpad backwards to move forward and backwards with 2 buttons instead of the MOVE button and moving the controller.")
        sHelp.AppendLine("This makes it easier to navigate in games.")
        sHelp.AppendLine()
        sHelp.AppendLine("HOW TO BIND:")
        sHelp.AppendLine("On your PSMove controller, hold both the MOVE button and the button you want to bind the touchpad axis to and move the controller in any direction.")
        sHelp.AppendLine("Release both buttons to accept.")
        sHelp.AppendLine("The saved touchpad axis will be applied when pressing the button now.")
        sHelp.AppendLine()
        sHelp.AppendLine("HOW TO UNBIND:")
        sHelp.AppendLine("Quickly press both the MOVE button and the button you want to unbind.")

        MessageBox.Show(sHelp.ToString, "Touchpad Shortcut Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub CheckBox_JoystickShortcuts_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_TouchpadShortcuts.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_JoystickShortcutBinding = CheckBox_TouchpadShortcuts.Checked
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_JoystickShortcutClick_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_TouchpadShortcutClick.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_JoystickShortcutTouchpadClick = CheckBox_TouchpadShortcutClick.Checked
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

    Private Sub CheckBox_TouchpadClampBounds_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_TouchpadClampBounds.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_HtcClampTouchpadToBounds = CheckBox_TouchpadClampBounds.Checked
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_TouchpadMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_TouchpadMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_HtcTouchpadMethod = CType(ComboBox_TouchpadMethod.SelectedIndex, ClassControllerSettings.ENUM_HTC_TOUCHPAD_METHOD)
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub


    Private Sub CheckBox_ControllerRecenterEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ControllerRecenterEnabled.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_EnableControllerRecenter = CheckBox_ControllerRecenterEnabled.Checked
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_RecenterMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_RecenterMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_ControllerRecenterMethod = CType(ComboBox_RecenterMethod.SelectedIndex, ClassControllerSettings.ENUM_DEVICE_RECENTER_METHOD)
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_RecenterFromDevice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_RecenterFromDevice.SelectedIndexChanged
        Try
            If (g_bIgnoreEvents) Then
                Return
            End If

            g_ClassControllerSettings.m_ControllerRecenterFromDeviceName = DirectCast(ComboBox_RecenterFromDevice.SelectedItem, ClassRecenterDeviceItem).GetRealName()
            g_ClassControllerSettings.SetUnsavedState(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboBox_RecenterFromDevice_DropDown(sender As Object, e As EventArgs) Handles ComboBox_RecenterFromDevice.DropDown
        Try
            g_bIgnoreEvents = True

            Dim mSelectedItem As String = ""
            If (ComboBox_RecenterFromDevice.SelectedItem IsNot Nothing) Then
                mSelectedItem = DirectCast(ComboBox_RecenterFromDevice.SelectedItem, ClassRecenterDeviceItem).GetRealName()
            End If

            Dim mDevices As New List(Of ClassRecenterDeviceItem)
            For Each mItem In g_ClassOscDevices.GetDevices
                Dim sDisplayName As String = String.Format("{0}, {1}", mItem.iType.ToString, mItem.sSerial)

                mDevices.Add(New ClassRecenterDeviceItem(mItem.sSerial, sDisplayName))
            Next

            ComboBox_RecenterFromDevice.BeginUpdate()
            ComboBox_RecenterFromDevice.Items.Clear()
            ComboBox_RecenterFromDevice.Items.Add(New ClassRecenterDeviceItem("", "Any HMD"))
            ComboBox_RecenterFromDevice.Items.AddRange(mDevices.ToArray)
            ComboBox_RecenterFromDevice.EndUpdate()

            Dim bLastFound As Boolean = True
            For Each mItem In ComboBox_RecenterFromDevice.Items
                If (DirectCast(mItem, ClassRecenterDeviceItem).GetRealName() = mSelectedItem) Then
                    ComboBox_RecenterFromDevice.SelectedItem = mItem

                    bLastFound = False
                    Exit For
                End If
            Next

            If (bLastFound) Then
                ' Assume we have at least one in the list
                ComboBox_RecenterFromDevice.SelectedIndex = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub CheckBox_HmdRecenterEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_HmdRecenterEnabled.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_EnableHmdRecenter = CheckBox_HmdRecenterEnabled.Checked
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_HmdRecenterMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_HmdRecenterMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_HmdRecenterMethod = CType(ComboBox_HmdRecenterMethod.SelectedIndex, ClassControllerSettings.ENUM_DEVICE_RECENTER_METHOD)
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_HmdRecenterFromDevice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_HmdRecenterFromDevice.SelectedIndexChanged
        Try
            If (g_bIgnoreEvents) Then
                Return
            End If

            g_ClassControllerSettings.m_HmdRecenterFromDeviceName = DirectCast(ComboBox_HmdRecenterFromDevice.SelectedItem, ClassRecenterDeviceItem).GetRealName()
            g_ClassControllerSettings.SetUnsavedState(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboBox_HmdRecenterFromDevice_DropDown(sender As Object, e As EventArgs) Handles ComboBox_HmdRecenterFromDevice.DropDown
        Try
            g_bIgnoreEvents = True

            Dim mSelectedItem As String = ""
            If (ComboBox_HmdRecenterFromDevice.SelectedItem IsNot Nothing) Then
                mSelectedItem = DirectCast(ComboBox_HmdRecenterFromDevice.SelectedItem, ClassRecenterDeviceItem).GetRealName()
            End If

            Dim mDevices As New List(Of ClassRecenterDeviceItem)
            For Each mItem In g_ClassOscDevices.GetDevices
                Dim sDisplayName As String = String.Format("{0}, {1}", mItem.iType.ToString, mItem.sSerial)

                mDevices.Add(New ClassRecenterDeviceItem(mItem.sSerial, sDisplayName))
            Next

            ComboBox_HmdRecenterFromDevice.BeginUpdate()
            ComboBox_HmdRecenterFromDevice.Items.Clear()
            ComboBox_HmdRecenterFromDevice.Items.Add(New ClassRecenterDeviceItem("", "No Device Selected"))
            ComboBox_HmdRecenterFromDevice.Items.AddRange(mDevices.ToArray)
            ComboBox_HmdRecenterFromDevice.EndUpdate()

            Dim bLastFound As Boolean = True
            For Each mItem In ComboBox_HmdRecenterFromDevice.Items
                If (DirectCast(mItem, ClassRecenterDeviceItem).GetRealName() = mSelectedItem) Then
                    ComboBox_HmdRecenterFromDevice.SelectedItem = mItem

                    bLastFound = False
                    Exit For
                End If
            Next

            If (bLastFound) Then
                ' Assume we have at least one in the list
                ComboBox_HmdRecenterFromDevice.SelectedIndex = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub Button_ResetRecenter_Click(sender As Object, e As EventArgs) Handles Button_ResetRecenter.Click
        Try
            For Each mTracker In GetVmtTrackers()
                mTracker.g_mClassIO.ResetRecenter()
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NumericUpDown_RecenterButtonTime_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_RecenterButtonTime.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_RecenterButtonTimeMs = CLng(NumericUpDown_RecenterButtonTime.Value)
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_OscThreadSleep_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_OscThreadSleep.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_OscThreadSleepMs = CLng(NumericUpDown_OscThreadSleep.Value)
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub Button_OscThreadSleepReset_Click(sender As Object, e As EventArgs) Handles Button_OscThreadSleepReset.Click
        NumericUpDown_OscThreadSleep.Value = 1
    End Sub

    Private Sub Button_RecenterButtonTimeReset_Click(sender As Object, e As EventArgs) Handles Button_RecenterButtonTimeReset.Click
        NumericUpDown_RecenterButtonTime.Value = 500
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
