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


    Private Sub LinkLabel_TouchpadShortcutHelp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_TouchpadShortcutHelp.LinkClicked
        Dim mMsg As New FormRtfHelp
        mMsg.RichTextBox_Help.Rtf = My.Resources.HelpTouchpadShortcuts
        mMsg.ShowDialog(Me)
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

    Private Sub NumericUpDown_TouchpadClickDeadzone_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_TouchpadClickDeadzone.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_HtcTouchpadClickDeadzone = NumericUpDown_TouchpadClickDeadzone.Value
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_TouchpadTouchArea_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_TouchpadTouchArea.ValueChanged
        ' See TOUCHPAD_GYRO_MULTI in UCVirutalMotionTrackerItem.vb
        Label_TouchpadTouchAreaDeg.Text = String.Format("cm / {0}°", NumericUpDown_TouchpadTouchArea.Value * 2.5F)

        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_HtcTouchpadTouchAreaCm = NumericUpDown_TouchpadTouchArea.Value
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

            Dim sSelectedName As String = ""
            If (ComboBox_RecenterFromDevice.SelectedItem IsNot Nothing) Then
                sSelectedName = DirectCast(ComboBox_RecenterFromDevice.SelectedItem, ClassRecenterDeviceItem).GetRealName()
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
                If (DirectCast(mItem, ClassRecenterDeviceItem).GetRealName() = sSelectedName) Then
                    ComboBox_RecenterFromDevice.SelectedItem = mItem

                    bLastFound = False
                    Exit For
                End If
            Next

            If (bLastFound) Then
                ' Create new one if its not in the list
                Dim mSelectedItem = New ClassRecenterDeviceItem(sSelectedName, "Any HMD")

                ComboBox_RecenterFromDevice.Items.Add(mSelectedItem)
                ComboBox_RecenterFromDevice.SelectedItem = mSelectedItem
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

            Dim sSelectedName As String = ""
            If (ComboBox_HmdRecenterFromDevice.SelectedItem IsNot Nothing) Then
                sSelectedName = DirectCast(ComboBox_HmdRecenterFromDevice.SelectedItem, ClassRecenterDeviceItem).GetRealName()
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
                If (DirectCast(mItem, ClassRecenterDeviceItem).GetRealName() = sSelectedName) Then
                    ComboBox_HmdRecenterFromDevice.SelectedItem = mItem

                    bLastFound = False
                    Exit For
                End If
            Next

            If (bLastFound) Then
                ' Create new one if its not in the list
                Dim mSelectedItem = New ClassRecenterDeviceItem(sSelectedName, "No Device Selected")

                ComboBox_HmdRecenterFromDevice.Items.Add(mSelectedItem)
                ComboBox_HmdRecenterFromDevice.SelectedItem = mSelectedItem
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

    Private Sub Button_PlayCalibReset_Click(sender As Object, e As EventArgs) Handles Button_PlayCalibReset.Click
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_PlayspaceSettings.Reset()
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_PlayCalibEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_PlayCalibEnabled.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_EnablePlayspaceRecenter = CheckBox_PlayCalibEnabled.Checked
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PlayCalibForwardOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PlayCalibForwardOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_PlayspaceSettings.iForwardOffset = NumericUpDown_PlayCalibForwardOffset.Value
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PlayCalibHeightOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PlayCalibHeightOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_PlayspaceSettings.iHeightOffset = NumericUpDown_PlayCalibHeightOffset.Value
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_PlayCalibForwardMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_PlayCalibForwardMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassControllerSettings.m_PlayspaceSettings.iForwardMethod = CType(ComboBox_PlayCalibForwardMethod.SelectedIndex, ClassControllerSettings.STRUC_PLAYSPACE_SETTINGS.ENUM_FORWARD_METHOD)
        g_ClassControllerSettings.SetUnsavedState(True)
    End Sub

    Private Sub Button_SaveControllerSettings_Click(sender As Object, e As EventArgs) Handles Button_SaveControllerSettings.Click
        Try
            g_ClassControllerSettings.SaveSettings(ENUM_SETTINGS_SAVE_TYPE_FLAGS.ALL)
            g_ClassControllerSettings.SetUnsavedState(False)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
