
Imports System.Text.RegularExpressions

Public Class UCVmtSettings
    Public g_UCVirtualMotionTracker As UCVirtualMotionTracker

    Private g_bIgnoreEvents As Boolean = True

    Structure STRUC_RENDER_RES_ITEM
        Public g_iScale As Single

        Public Sub New(_Scale As Single)
            g_iScale = _Scale
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("{0}% - {1}x{2}", CInt(g_iScale * 100), CStr(1920 * g_iScale), CStr(1080 * g_iScale))
        End Function
    End Structure

    Public Sub New(_UCVirtualMotionTracker As UCVirtualMotionTracker)
        g_UCVirtualMotionTracker = _UCVirtualMotionTracker

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Label_ScrollFocus.Text = ""
        GroupBox_Distortion.Visible = False


        Try
            g_bIgnoreEvents = True

            ComboBox_TouchpadClickMethod.Items.Clear()
            ComboBox_TouchpadClickMethod.Items.Add("Button Drag (Left Controller: TRIANGLE [▲] / Right Controller: SQUARE [■])")
            ComboBox_TouchpadClickMethod.Items.Add("Button Drag (Both Controllers: SQUARE [■])")
            ComboBox_TouchpadClickMethod.Items.Add("Button Drag (Both Controllers: TRIANGLE [▲])")
            ComboBox_TouchpadClickMethod.Items.Add("While holding MOVE [~] button")

            ComboBox_TouchpadClickMethod.SelectedIndex = 0

            If (ComboBox_TouchpadClickMethod.Items.Count <> UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_GrabButtonMethod.Items.Clear()
            ComboBox_GrabButtonMethod.Items.Add("Button Toggle (Left Controller: CIRCLE [O] / Right Controller: CROSS [X])")
            ComboBox_GrabButtonMethod.Items.Add("Button Toggle (Both Controllers: CROSS [X])")
            ComboBox_GrabButtonMethod.Items.Add("Button Toggle (Both Controllers: CIRCLE [O])")
            ComboBox_GrabButtonMethod.Items.Add("Button Holding (Left Controller: CIRCLE [O] / Right Controller: CROSS [X])")
            ComboBox_GrabButtonMethod.Items.Add("Button Holding (Both Controllers: CROSS [X])")
            ComboBox_GrabButtonMethod.Items.Add("Button Holding (Both Controllers: CIRCLE [O])")

            ComboBox_GrabButtonMethod.SelectedIndex = 0

            If (ComboBox_GrabButtonMethod.Items.Count <> UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_TouchpadMethod.Items.Clear()
            ComboBox_TouchpadMethod.Items.Add("Use Controller Position")
            ComboBox_TouchpadMethod.Items.Add("Use Controller Orientation")

            ComboBox_TouchpadMethod.SelectedIndex = 0

            If (ComboBox_TouchpadMethod.Items.Count <> UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_RecenterMethod.Items.Clear()
            ComboBox_RecenterMethod.Items.Add("Use Current Controller")
            ComboBox_RecenterMethod.Items.Add("Use PSMoveServiceEx Playspace Orientation")

            ComboBox_RecenterMethod.SelectedIndex = 0

            If (ComboBox_RecenterMethod.Items.Count <> UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_RecenterFromDevice.Items.Clear()
            ComboBox_RecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(""))

            ComboBox_RecenterFromDevice.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_HmdRecenterMethod.Items.Clear()
            ComboBox_HmdRecenterMethod.Items.Add("Use Current Controller")
            ComboBox_HmdRecenterMethod.Items.Add("Use PSMoveServiceEx Playspace Orientation")

            ComboBox_HmdRecenterMethod.SelectedIndex = 0

            If (ComboBox_HmdRecenterMethod.Items.Count <> UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_HmdRecenterFromDevice.Items.Clear()
            ComboBox_HmdRecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(""))

            ComboBox_HmdRecenterFromDevice.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_PlayCalibForwardMethod.Items.Clear()
            ComboBox_PlayCalibForwardMethod.Items.Add("Use Head-Mounted Display Forward")
            ComboBox_PlayCalibForwardMethod.Items.Add("Use Calibrated Playspace Forward")

            ComboBox_PlayCalibForwardMethod.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_PsvrRenderResolution.Items.Clear()
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(0.25F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(0.5F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(0.75F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(1.0F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(1.25F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(1.3F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(1.5F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(1.75F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(2.0F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(2.5F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(3.0F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(3.5F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(4.0F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(4.5F))
            ComboBox_PsvrRenderResolution.Items.Add(New STRUC_RENDER_RES_ITEM(5.0F))

            ComboBox_PsvrRenderResolution.SelectedIndex = 0

            ComboBox_PsvrRenderResolution.SelectedItem = New STRUC_RENDER_RES_ITEM(1.3F)
        Finally
            g_bIgnoreEvents = False
        End Try

    End Sub

    Public Sub LoadSettings()
        Dim mClassSettings = g_UCVirtualMotionTracker.g_ClassSettings
        Dim mUCVmtPlayspaceCalib = g_UCVirtualMotionTracker.g_UCVmtPlayspaceCalib

        mClassSettings.LoadSettings()

        Try
            g_bIgnoreEvents = True

            ' Controller Settings
            CheckBox_TouchpadShortcuts.Checked = mClassSettings.m_ControllerSettings.m_JoystickShortcutBinding
            CheckBox_TouchpadShortcutClick.Checked = mClassSettings.m_ControllerSettings.m_JoystickShortcutTouchpadClick
            ComboBox_TouchpadClickMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_TouchpadClickMethod.Items.Count - 1, mClassSettings.m_ControllerSettings.m_HtcTouchpadEmulationClickMethod))
            ComboBox_GrabButtonMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_GrabButtonMethod.Items.Count - 1, mClassSettings.m_ControllerSettings.m_HtcGripButtonMethod))
            CheckBox_TouchpadClampBounds.Checked = mClassSettings.m_ControllerSettings.m_HtcClampTouchpadToBounds
            ComboBox_TouchpadMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_TouchpadMethod.Items.Count - 1, mClassSettings.m_ControllerSettings.m_HtcTouchpadMethod))

            CheckBox_ControllerRecenterEnabled.Checked = mClassSettings.m_ControllerSettings.m_EnableControllerRecenter
            ComboBox_RecenterMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_RecenterMethod.Items.Count - 1, mClassSettings.m_ControllerSettings.m_ControllerRecenterMethod))

            ComboBox_RecenterFromDevice.Items.Clear()
            ComboBox_RecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(mClassSettings.m_ControllerSettings.m_ControllerRecenterFromDeviceName))
            ComboBox_RecenterFromDevice.SelectedIndex = 0

            CheckBox_HmdRecenterEnabled.Checked = mClassSettings.m_ControllerSettings.m_EnableHmdRecenter
            ComboBox_HmdRecenterMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_HmdRecenterMethod.Items.Count - 1, mClassSettings.m_ControllerSettings.m_HmdRecenterMethod))

            ComboBox_HmdRecenterFromDevice.Items.Clear()
            ComboBox_HmdRecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(mClassSettings.m_ControllerSettings.m_HmdRecenterFromDeviceName))
            ComboBox_HmdRecenterFromDevice.SelectedIndex = 0

            NumericUpDown_RecenterButtonTime.Value = Math.Max(NumericUpDown_RecenterButtonTime.Minimum, Math.Min(NumericUpDown_RecenterButtonTime.Maximum, mClassSettings.m_ControllerSettings.m_RecenterButtonTimeMs))
            NumericUpDown_OscThreadSleep.Value = Math.Max(NumericUpDown_OscThreadSleep.Minimum, Math.Min(NumericUpDown_OscThreadSleep.Maximum, mClassSettings.m_ControllerSettings.m_OscThreadSleepMs))

            NumericUpDown_TouchpadClickDeadzone.Value = CDec(Math.Max(NumericUpDown_TouchpadClickDeadzone.Minimum, Math.Min(NumericUpDown_TouchpadClickDeadzone.Maximum, mClassSettings.m_ControllerSettings.m_HtcTouchpadClickDeadzone)))
            NumericUpDown_TouchpadTouchArea.Value = CDec(Math.Max(NumericUpDown_TouchpadTouchArea.Minimum, Math.Min(NumericUpDown_TouchpadTouchArea.Maximum, mClassSettings.m_ControllerSettings.m_HtcTouchpadTouchAreaCm)))

            CheckBox_PlayCalibEnabled.Checked = mClassSettings.m_ControllerSettings.m_EnablePlayspaceRecenter

            ' Hmd Settings
            CheckBox_ShowDistSettings.Checked = mClassSettings.m_HmdSettings.m_UseCustomDistortion
            NumericUpDown_PsvrDistK0.Value = CDec(Math.Max(NumericUpDown_PsvrDistK0.Minimum, Math.Min(NumericUpDown_PsvrDistK0.Maximum, mClassSettings.m_HmdSettings.m_DistortionK0(True))))
            NumericUpDown_PsvrDistK1.Value = CDec(Math.Max(NumericUpDown_PsvrDistK1.Minimum, Math.Min(NumericUpDown_PsvrDistK1.Maximum, mClassSettings.m_HmdSettings.m_DistortionK1(True))))
            NumericUpDown_PsvrDistScale.Value = CDec(Math.Max(NumericUpDown_PsvrDistScale.Minimum, Math.Min(NumericUpDown_PsvrDistScale.Maximum, mClassSettings.m_HmdSettings.m_DistortionScale(True))))
            NumericUpDown_PsvrDistRedOffset.Value = CDec(Math.Max(NumericUpDown_PsvrDistRedOffset.Minimum, Math.Min(NumericUpDown_PsvrDistRedOffset.Maximum, mClassSettings.m_HmdSettings.m_DistortionRedOffset(True))))
            NumericUpDown_PsvrDistGreenOffset.Value = CDec(Math.Max(NumericUpDown_PsvrDistGreenOffset.Minimum, Math.Min(NumericUpDown_PsvrDistGreenOffset.Maximum, mClassSettings.m_HmdSettings.m_DistortionGreenOffset(True))))
            NumericUpDown_PsvrDistBlueOffset.Value = CDec(Math.Max(NumericUpDown_PsvrDistBlueOffset.Minimum, Math.Min(NumericUpDown_PsvrDistBlueOffset.Maximum, mClassSettings.m_HmdSettings.m_DistortionBlueOffset(True))))
            NumericUpDown_PsvrHFov.Value = CDec(Math.Max(NumericUpDown_PsvrHFov.Minimum, Math.Min(NumericUpDown_PsvrHFov.Maximum, mClassSettings.m_HmdSettings.m_HFov(True))))
            NumericUpDown_PsvrVFov.Value = CDec(Math.Max(NumericUpDown_PsvrVFov.Minimum, Math.Min(NumericUpDown_PsvrVFov.Maximum, mClassSettings.m_HmdSettings.m_VFov(True))))
            NumericUpDown_PsvrIPD.Value = CDec(Math.Max(NumericUpDown_PsvrIPD.Minimum, Math.Min(NumericUpDown_PsvrIPD.Maximum, mClassSettings.m_HmdSettings.m_IPD)))
            ComboBox_PsvrRenderResolution.SelectedItem = New STRUC_RENDER_RES_ITEM(mClassSettings.m_HmdSettings.m_RenderScale)

            'Misc Settings
            CheckBox_DisableBasestations.Checked = mClassSettings.m_MiscSettings.m_DisableBaseStationSpawning
            CheckBox_EnableHeptics.Checked = mClassSettings.m_MiscSettings.m_EnableHepticFeedback
            CheckBox_OptimizePackets.Checked = mClassSettings.m_MiscSettings.m_OptimizeTransportPackets
            CheckBox_RenderFix.Checked = mClassSettings.m_MiscSettings.m_RenderWindowFix
            TextBox_OscRemoteIP.Text = mClassSettings.m_MiscSettings.m_OscRemoteIP

            ' Playspace Settings
            NumericUpDown_PlayCalibForwardOffset.Value = CDec(Math.Max(NumericUpDown_PlayCalibForwardOffset.Minimum, Math.Min(NumericUpDown_PlayCalibForwardOffset.Maximum, mClassSettings.m_PlayspaceSettings.m_ForwardOffset)))
            NumericUpDown_PlayCalibSideOffset.Value = CDec(Math.Max(NumericUpDown_PlayCalibSideOffset.Minimum, Math.Min(NumericUpDown_PlayCalibSideOffset.Maximum, mClassSettings.m_PlayspaceSettings.m_SideOffset)))
            NumericUpDown_PlayCalibHeightOffset.Value = CDec(Math.Max(NumericUpDown_PlayCalibHeightOffset.Minimum, Math.Min(NumericUpDown_PlayCalibHeightOffset.Maximum, mClassSettings.m_PlayspaceSettings.m_HeightOffset)))
            ComboBox_PlayCalibForwardMethod.SelectedIndex = Math.Max(0, Math.Min(ComboBox_PlayCalibForwardMethod.Items.Count - 1, mClassSettings.m_PlayspaceSettings.m_ForwardMethod))
            mUCVmtPlayspaceCalib.ComboBox_PlayCalibControllerID.SelectedIndex = Math.Max(0, Math.Min(mUCVmtPlayspaceCalib.ComboBox_PlayCalibControllerID.Items.Count - 1, mClassSettings.m_PlayspaceSettings.m_CalibrationControllerId))
            CheckBox_PlayCalibAutoscale.Checked = mClassSettings.m_PlayspaceSettings.m_AutoScale

            mClassSettings.SetUnsavedState(False)
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Public Class ClassRecenterDeviceItem
        Private g_sName As String = ""
        Private g_sDisplayName As String = ""

        Public Sub New(_Name As String)
            g_sName = _Name
            g_sDisplayName = _Name

            If (String.IsNullOrEmpty(g_sName) OrElse g_sName.TrimEnd.Length = 0) Then
                g_sDisplayName = "Any Head-Mounted Display"
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

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_JoystickShortcutBinding = CheckBox_TouchpadShortcuts.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_JoystickShortcutClick_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_TouchpadShortcutClick.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_JoystickShortcutTouchpadClick = CheckBox_TouchpadShortcutClick.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_TouchpadClickMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_TouchpadClickMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HtcTouchpadEmulationClickMethod = CType(ComboBox_TouchpadClickMethod.SelectedIndex, UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_GrabButtonMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_GrabButtonMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HtcGripButtonMethod = CType(ComboBox_GrabButtonMethod.SelectedIndex, UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_DisableBasestations_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_DisableBasestations.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_DisableBaseStationSpawning = CheckBox_DisableBasestations.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_EnableHeptics_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_EnableHeptics.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_EnableHepticFeedback = CheckBox_EnableHeptics.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_OptimizePackets_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_OptimizePackets.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_OptimizeTransportPackets = CheckBox_OptimizePackets.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_RenderFix_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_RenderFix.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_RenderWindowFix = CheckBox_RenderFix.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_TouchpadClampBounds_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_TouchpadClampBounds.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HtcClampTouchpadToBounds = CheckBox_TouchpadClampBounds.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_TouchpadMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_TouchpadMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HtcTouchpadMethod = CType(ComboBox_TouchpadMethod.SelectedIndex, UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_METHOD)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub


    Private Sub CheckBox_ControllerRecenterEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ControllerRecenterEnabled.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_EnableControllerRecenter = CheckBox_ControllerRecenterEnabled.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_RecenterMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_RecenterMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_ControllerRecenterMethod = CType(ComboBox_RecenterMethod.SelectedIndex, UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_TouchpadClickDeadzone_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_TouchpadClickDeadzone.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HtcTouchpadClickDeadzone = NumericUpDown_TouchpadClickDeadzone.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_TouchpadTouchArea_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_TouchpadTouchArea.ValueChanged
        ' See TOUCHPAD_GYRO_MULTI in UCVirutalMotionTrackerItem.vb
        Label_TouchpadTouchAreaDeg.Text = String.Format("cm / {0}°", NumericUpDown_TouchpadTouchArea.Value * 2.5F)

        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HtcTouchpadTouchAreaCm = NumericUpDown_TouchpadTouchArea.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_RecenterFromDevice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_RecenterFromDevice.SelectedIndexChanged
        Try
            If (g_bIgnoreEvents) Then
                Return
            End If

            g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_ControllerRecenterFromDeviceName = DirectCast(ComboBox_RecenterFromDevice.SelectedItem, ClassRecenterDeviceItem).GetRealName()
            g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
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
            For Each mItem In g_UCVirtualMotionTracker.g_ClassOscDevices.GetDevices
                mDevices.Add(New ClassRecenterDeviceItem(mItem.sSerial))
            Next

            ComboBox_RecenterFromDevice.BeginUpdate()
            Try
                ComboBox_RecenterFromDevice.Items.Clear()
                ComboBox_RecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(""))
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
                Dim mSelectedItem = New ClassRecenterDeviceItem(sSelectedName)

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

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_EnableHmdRecenter = CheckBox_HmdRecenterEnabled.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_HmdRecenterMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_HmdRecenterMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HmdRecenterMethod = CType(ComboBox_HmdRecenterMethod.SelectedIndex, UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_DEVICE_RECENTER_METHOD)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_HmdRecenterFromDevice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_HmdRecenterFromDevice.SelectedIndexChanged
        Try
            If (g_bIgnoreEvents) Then
                Return
            End If

            g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HmdRecenterFromDeviceName = DirectCast(ComboBox_HmdRecenterFromDevice.SelectedItem, ClassRecenterDeviceItem).GetRealName()
            g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
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
            For Each mItem In g_UCVirtualMotionTracker.g_ClassOscDevices.GetDevices
                mDevices.Add(New ClassRecenterDeviceItem(mItem.sSerial))
            Next

            ComboBox_HmdRecenterFromDevice.BeginUpdate()
            Try
                ComboBox_HmdRecenterFromDevice.Items.Clear()
                ComboBox_HmdRecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(""))
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
                Dim mSelectedItem = New ClassRecenterDeviceItem(sSelectedName)

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
            For Each mTracker In g_UCVirtualMotionTracker.g_UCVmtTrackers.GetVmtTrackers()
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

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_RecenterButtonTimeMs = CLng(NumericUpDown_RecenterButtonTime.Value)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_OscThreadSleep_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_OscThreadSleep.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_OscThreadSleepMs = CInt(NumericUpDown_OscThreadSleep.Value)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_OscMaxThreadFps_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_OscMaxThreadFps.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_OscMaxThreadFps = CInt(NumericUpDown_OscMaxThreadFps.Value)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub Button_PlayCalibReset_Click(sender As Object, e As EventArgs) Handles Button_PlayCalibReset.Click
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_PlayspaceSettings.Reset()
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_PlayCalibEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_PlayCalibEnabled.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_EnablePlayspaceRecenter = CheckBox_PlayCalibEnabled.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PlayCalibForwardOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PlayCalibForwardOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_PlayspaceSettings.m_ForwardOffset = NumericUpDown_PlayCalibForwardOffset.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PlayCalibSideOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PlayCalibSideOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_PlayspaceSettings.m_SideOffset = NumericUpDown_PlayCalibSideOffset.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PlayCalibHeightOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PlayCalibHeightOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_PlayspaceSettings.m_HeightOffset = NumericUpDown_PlayCalibHeightOffset.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_PlayCalibForwardMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_PlayCalibForwardMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_PlayspaceSettings.m_ForwardMethod = CType(ComboBox_PlayCalibForwardMethod.SelectedIndex, UCVirtualMotionTracker.ClassSettings.STRUC_PLAYSPACE_SETTINGS.ENUM_FORWARD_METHOD)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_PlayCalibAutoscale_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_PlayCalibAutoscale.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_PlayspaceSettings.m_AutoScale = CheckBox_PlayCalibAutoscale.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_PsvrRenderResolution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_PsvrRenderResolution.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_RenderScale = CType(ComboBox_PsvrRenderResolution.SelectedItem, STRUC_RENDER_RES_ITEM).g_iScale
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub NumericUpDown_PsvrIPD_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrIPD.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_IPD = NumericUpDown_PsvrIPD.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_PsvrDistK0_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistK0.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_DistortionK0(True) = NumericUpDown_PsvrDistK0.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub NumericUpDown_PsvrDistK1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistK1.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_DistortionK1(True) = NumericUpDown_PsvrDistK1.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub NumericUpDown_PsvrDistScale_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistScale.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_DistortionScale(True) = NumericUpDown_PsvrDistScale.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub NumericUpDown_PsvrDistRedOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistRedOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_DistortionRedOffset(True) = NumericUpDown_PsvrDistRedOffset.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub NumericUpDown_PsvrDistGreenOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistGreenOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_DistortionGreenOffset(True) = NumericUpDown_PsvrDistGreenOffset.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub NumericUpDown_PsvrDistBlueOffset_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrDistBlueOffset.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_DistortionBlueOffset(True) = NumericUpDown_PsvrDistBlueOffset.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub NumericUpDown_PsvrHFov_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrHFov.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_HFov(True) = NumericUpDown_PsvrHFov.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub NumericUpDown_PsvrVFov_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_PsvrVFov.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_VFov(True) = NumericUpDown_PsvrVFov.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub CheckBox_ShowDistSettings_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ShowDistSettings.CheckedChanged
        GroupBox_Distortion.Visible = CheckBox_ShowDistSettings.Checked

        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_UseCustomDistortion = CheckBox_ShowDistSettings.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)

        g_UCVirtualMotionTracker.g_mFormMain.PromptRestartSteamVR()
    End Sub

    Private Sub LinkLabel_PsvrDistReset_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_PsvrDistReset.LinkClicked
        If (g_bIgnoreEvents) Then
            Return
        End If

        NumericUpDown_PsvrDistK0.Value = CDec(UCVirtualMotionTracker.ClassSettings.DISPLAY_DISTORTION_K0)
        NumericUpDown_PsvrDistK1.Value = CDec(UCVirtualMotionTracker.ClassSettings.DISPLAY_DISTORTION_K1)
        NumericUpDown_PsvrDistScale.Value = CDec(UCVirtualMotionTracker.ClassSettings.DISPLAY_DISTORTION_SCALE)
        NumericUpDown_PsvrDistRedOffset.Value = CDec(UCVirtualMotionTracker.ClassSettings.DISPLAY_DISTORTION_RED_OFFSET)
        NumericUpDown_PsvrDistGreenOffset.Value = CDec(UCVirtualMotionTracker.ClassSettings.DISPLAY_DISTORTION_GREEN_OFFSET)
        NumericUpDown_PsvrDistBlueOffset.Value = CDec(UCVirtualMotionTracker.ClassSettings.DISPLAY_DISTORTION_BLUE_OFFSET)
        NumericUpDown_PsvrHFov.Value = CDec(UCVirtualMotionTracker.ClassSettings.DISPLAY_HFOV)
        NumericUpDown_PsvrVFov.Value = CDec(UCVirtualMotionTracker.ClassSettings.DISPLAY_VFOV)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub LinkLabel_PlayCalibShowSettings2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_PlayCalibShowSettings2.LinkClicked
        g_UCVirtualMotionTracker.TabControl_Vmt.SelectedTab = g_UCVirtualMotionTracker.TabPage_Settings
        TabControl_SettingsDevices.SelectedTab = TabPage_SettingsPlayspace

        ' Weird focus  
        CheckBox_PlayCalibEnabled.Focus()
    End Sub

    Private Sub Button_SaveControllerSettings_Click(sender As Object, e As EventArgs) Handles Button_SaveControllerSettings.Click
        Try
            g_UCVirtualMotionTracker.g_ClassSettings.SaveSettings(UCVirtualMotionTracker.ENUM_SETTINGS_SAVE_TYPE_FLAGS.ALL)
            g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(False)
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
                ComboBox_HmdRecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(sTrackerName))
                ComboBox_HmdRecenterFromDevice.SelectedIndex = 0

                Return
            Next

            MessageBox.Show("Cound not find any Head-mounted Display SteamVR tracker overrides.", "Could not find override", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub LinkLabel_OscIpChange_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_OscIpChange.LinkClicked
        Try
            If (g_bIgnoreEvents) Then
                Return
            End If

            If (g_UCVirtualMotionTracker.g_ClassOscServer IsNot Nothing AndAlso g_UCVirtualMotionTracker.g_ClassOscServer.IsRunning) Then
                Throw New ArgumentException("You can not change the OSC remote IP address while the OSC server is already initialized and running!")
            End If

            Dim sRemoteIP As String = g_UCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_OscRemoteIP

            sRemoteIP = InputBox("Enter a new remote IP for a OSC connection. If you already have run the OSC server, you need to restart the application in order to use the new remote IP instead.", "OSC Remote IP", sRemoteIP)
            If (String.IsNullOrEmpty(sRemoteIP)) Then
                sRemoteIP = ""
            Else
                sRemoteIP = sRemoteIP.Trim
            End If

            If (Not String.IsNullOrEmpty(sRemoteIP) AndAlso Not Regex.IsMatch(sRemoteIP, "^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$", RegexOptions.IgnoreCase)) Then
                Throw New ArgumentException("The entered IP is not valid")
            End If

            g_UCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_OscRemoteIP = sRemoteIP
            g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)

            TextBox_OscRemoteIP.Text = sRemoteIP

        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

    End Sub

    Private Sub CleanUp()

    End Sub
End Class
