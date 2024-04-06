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

        g_ClassSettings.m_ControllerSettings.m_JoystickShortcutBinding = CheckBox_TouchpadShortcuts.Checked
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_JoystickShortcutClick_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_TouchpadShortcutClick.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_JoystickShortcutTouchpadClick = CheckBox_TouchpadShortcutClick.Checked
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_TouchpadClickMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_TouchpadClickMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_HtcTouchpadEmulationClickMethod = CType(ComboBox_TouchpadClickMethod.SelectedIndex, ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD)
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_GrabButtonMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_GrabButtonMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_HtcGripButtonMethod = CType(ComboBox_GrabButtonMethod.SelectedIndex, ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD)
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_DisableBasestations_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_DisableBasestations.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_MiscSettings.m_DisableBaseStationSpawning = CheckBox_DisableBasestations.Checked
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_EnableHeptics_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_EnableHeptics.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_MiscSettings.m_EnableHepticFeedback = CheckBox_EnableHeptics.Checked
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_OptimizePackets_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_OptimizePackets.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_MiscSettings.m_OptimizeTransportPackets = CheckBox_OptimizePackets.Checked
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_TouchpadClampBounds_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_TouchpadClampBounds.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_HtcClampTouchpadToBounds = CheckBox_TouchpadClampBounds.Checked
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_TouchpadMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_TouchpadMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_HtcTouchpadMethod = CType(ComboBox_TouchpadMethod.SelectedIndex, ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_METHOD)
        g_ClassSettings.SetUnsavedState(True)
    End Sub


    Private Sub CheckBox_ControllerRecenterEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ControllerRecenterEnabled.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_EnableControllerRecenter = CheckBox_ControllerRecenterEnabled.Checked
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_RecenterMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_RecenterMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_ControllerRecenterMethod = CType(ComboBox_RecenterMethod.SelectedIndex, ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD)
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_TouchpadClickDeadzone_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_TouchpadClickDeadzone.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_HtcTouchpadClickDeadzone = NumericUpDown_TouchpadClickDeadzone.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_TouchpadTouchArea_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_TouchpadTouchArea.ValueChanged
        ' See TOUCHPAD_GYRO_MULTI in UCVirutalMotionTrackerItem.vb
        Label_TouchpadTouchAreaDeg.Text = String.Format("cm / {0}°", NumericUpDown_TouchpadTouchArea.Value * 2.5F)

        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_HtcTouchpadTouchAreaCm = NumericUpDown_TouchpadTouchArea.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_RecenterFromDevice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_RecenterFromDevice.SelectedIndexChanged
        Try
            If (g_bIgnoreEvents) Then
                Return
            End If

            g_ClassSettings.m_ControllerSettings.m_ControllerRecenterFromDeviceName = DirectCast(ComboBox_RecenterFromDevice.SelectedItem, ClassRecenterDeviceItem).GetRealName()
            g_ClassSettings.SetUnsavedState(True)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
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
            Try
                ComboBox_RecenterFromDevice.Items.Clear()
                ComboBox_RecenterFromDevice.Items.Add(New ClassRecenterDeviceItem("", "Any Head-Mounted Display"))
                ComboBox_RecenterFromDevice.Items.AddRange(mDevices.ToArray)
            Finally
                ComboBox_RecenterFromDevice.EndUpdate()
            End Try

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
                Dim mSelectedItem = New ClassRecenterDeviceItem(sSelectedName, "Any Head-Mounted Display")

                ComboBox_RecenterFromDevice.Items.Add(mSelectedItem)
                ComboBox_RecenterFromDevice.SelectedItem = mSelectedItem
            End If

        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub CheckBox_HmdRecenterEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_HmdRecenterEnabled.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_EnableHmdRecenter = CheckBox_HmdRecenterEnabled.Checked
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_HmdRecenterMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_HmdRecenterMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_HmdRecenterMethod = CType(ComboBox_HmdRecenterMethod.SelectedIndex, ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD)
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_HmdRecenterFromDevice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_HmdRecenterFromDevice.SelectedIndexChanged
        Try
            If (g_bIgnoreEvents) Then
                Return
            End If

            g_ClassSettings.m_ControllerSettings.m_HmdRecenterFromDeviceName = DirectCast(ComboBox_HmdRecenterFromDevice.SelectedItem, ClassRecenterDeviceItem).GetRealName()
            g_ClassSettings.SetUnsavedState(True)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
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
            Try
                ComboBox_HmdRecenterFromDevice.Items.Clear()
                ComboBox_HmdRecenterFromDevice.Items.Add(New ClassRecenterDeviceItem("", "Any Head-Mounted Display"))
                ComboBox_HmdRecenterFromDevice.Items.AddRange(mDevices.ToArray)
            Finally
                ComboBox_HmdRecenterFromDevice.EndUpdate()
            End Try

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
                Dim mSelectedItem = New ClassRecenterDeviceItem(sSelectedName, "Any Head-Mounted Display")

                ComboBox_HmdRecenterFromDevice.Items.Add(mSelectedItem)
                ComboBox_HmdRecenterFromDevice.SelectedItem = mSelectedItem
            End If

        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
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
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub NumericUpDown_RecenterButtonTime_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_RecenterButtonTime.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_RecenterButtonTimeMs = CLng(NumericUpDown_RecenterButtonTime.Value)
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_OscThreadSleep_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_OscThreadSleep.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_OscThreadSleepMs = CInt(NumericUpDown_OscThreadSleep.Value)
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_OscMaxThreadFps_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_OscMaxThreadFps.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_OscMaxThreadFps = CInt(NumericUpDown_OscMaxThreadFps.Value)
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub Button_PlayCalibReset_Click(sender As Object, e As EventArgs) Handles Button_PlayCalibReset.Click
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_PlayspaceSettings.Reset()
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_PlayCalibEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_PlayCalibEnabled.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_ControllerSettings.m_EnablePlayspaceRecenter = CheckBox_PlayCalibEnabled.Checked
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PlayCalibForwardOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PlayCalibForwardOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_PlayspaceSettings.m_ForwardOffset = NumericUpDown_PlayCalibForwardOffset.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PlayCalibSideOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PlayCalibSideOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_PlayspaceSettings.m_SideOffset = NumericUpDown_PlayCalibSideOffset.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PlayCalibHeightOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PlayCalibHeightOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_PlayspaceSettings.m_HeightOffset = NumericUpDown_PlayCalibHeightOffset.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_PlayCalibForwardMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_PlayCalibForwardMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_PlayspaceSettings.m_ForwardMethod = CType(ComboBox_PlayCalibForwardMethod.SelectedIndex, ClassSettings.STRUC_PLAYSPACE_SETTINGS.ENUM_FORWARD_METHOD)
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_PlayCalibAutoscale_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_PlayCalibAutoscale.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_PlayspaceSettings.m_AutoScale = CheckBox_PlayCalibAutoscale.Checked
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_PsvrRenderResolution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_PsvrRenderResolution.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_HmdSettings.m_RenderScale = CType(ComboBox_PsvrRenderResolution.SelectedItem, STRUC_RENDER_RES_ITEM).g_iScale
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PsvrIPD_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrIPD.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_HmdSettings.m_IPD = NumericUpDown_PsvrIPD.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PsvrDistK0_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistK0.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_HmdSettings.m_DistortionK0(True) = NumericUpDown_PsvrDistK0.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PsvrDistK1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistK1.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_HmdSettings.m_DistortionK1(True) = NumericUpDown_PsvrDistK1.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PsvrDistScale_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistScale.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_HmdSettings.m_DistortionScale(True) = NumericUpDown_PsvrDistScale.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PsvrDistRedOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistRedOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_HmdSettings.m_DistortionRedOffset(True) = NumericUpDown_PsvrDistRedOffset.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PsvrDistGreenOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistGreenOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_HmdSettings.m_DistortionGreenOffset(True) = NumericUpDown_PsvrDistGreenOffset.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PsvrDistBlueOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistBlueOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_HmdSettings.m_DistortionBlueOffset(True) = NumericUpDown_PsvrDistBlueOffset.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PsvrHFov_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrHFov.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_HmdSettings.m_HFov(True) = NumericUpDown_PsvrHFov.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PsvrVFov_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrVFov.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_HmdSettings.m_VFov(True) = NumericUpDown_PsvrVFov.Value
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_ShowDistSettings_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ShowDistSettings.CheckedChanged
        GroupBox_Distortion.Visible = CheckBox_ShowDistSettings.Checked

        If (g_bIgnoreEvents) Then
            Return
        End If

        g_ClassSettings.m_HmdSettings.m_UseCustomDistortion = CheckBox_ShowDistSettings.Checked
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub LinkLabel_PsvrDistReset_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_PsvrDistReset.LinkClicked
        If (g_bIgnoreEvents) Then
            Return
        End If

        NumericUpDown_PsvrDistK0.Value = CDec(ClassSettings.DISPLAY_DISTORTION_K0)
        NumericUpDown_PsvrDistK1.Value = CDec(ClassSettings.DISPLAY_DISTORTION_K1)
        NumericUpDown_PsvrDistScale.Value = CDec(ClassSettings.DISPLAY_DISTORTION_SCALE)
        NumericUpDown_PsvrDistRedOffset.Value = CDec(ClassSettings.DISPLAY_DISTORTION_RED_OFFSET)
        NumericUpDown_PsvrDistGreenOffset.Value = CDec(ClassSettings.DISPLAY_DISTORTION_GREEN_OFFSET)
        NumericUpDown_PsvrDistBlueOffset.Value = CDec(ClassSettings.DISPLAY_DISTORTION_BLUE_OFFSET)
        NumericUpDown_PsvrHFov.Value = CDec(ClassSettings.DISPLAY_HFOV)
        NumericUpDown_PsvrVFov.Value = CDec(ClassSettings.DISPLAY_VFOV)
        g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub LinkLabel_PlayCalibShowSettings2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_PlayCalibShowSettings2.LinkClicked
        TabControl_Vmt.SelectedTab = TabPage_Settings
        TabControl_SettingsDevices.SelectedTab = TabPage_SettingsPlayspace

        ' Weird focus  
        CheckBox_PlayCalibEnabled.Focus()
    End Sub

    Private Sub Button_SaveControllerSettings_Click(sender As Object, e As EventArgs) Handles Button_SaveControllerSettings.Click
        Try
            g_ClassSettings.SaveSettings(ENUM_SETTINGS_SAVE_TYPE_FLAGS.ALL)
            g_ClassSettings.SetUnsavedState(False)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub LinkLabel_HmdRecenterFromOverride_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_HmdRecenterFromOverride.LinkClicked
        Try
            Dim mConfig As New ClassSteamVRConfig
            mConfig.LoadConfig()

            For Each mOverride In mConfig.m_ClassOverrides.GetOverrides()
                If (mConfig.m_ClassOverrides.GetOverrideTypeFromName(mOverride.Value) <> ClassSteamVRConfig.ClassOverrides.ENUM_OVERRIDE_TYPE.HEAD) Then
                    Continue For
                End If

                Dim sPaths As String() = mOverride.Key.Split("/"c)
                If (sPaths.Count < 1) Then
                    Continue For
                End If

                Dim sTrackerName As String = sPaths(sPaths.Count - 1)
                If (String.IsNullOrEmpty(sTrackerName) OrElse Not sTrackerName.ToLowerInvariant.StartsWith(ClassVmtConst.VMT_DEVICE_NAME.ToLowerInvariant)) Then
                    Continue For
                End If

                ComboBox_HmdRecenterFromDevice.Items.Clear()
                ComboBox_HmdRecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(sTrackerName, "Any Head-Mounted Display"))
                ComboBox_HmdRecenterFromDevice.SelectedIndex = 0

                Return
            Next

            MessageBox.Show("Cound not find any Head-mounted Display SteamVR tracker overrides.", "Could not find override", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub
End Class
