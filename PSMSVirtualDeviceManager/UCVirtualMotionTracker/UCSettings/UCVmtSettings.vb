
Imports System.Numerics
Imports System.Text.RegularExpressions

Public Class UCVmtSettings
    Public g_UCVirtualMotionTracker As UCVirtualMotionTracker

    Private g_bIgnoreEvents As Boolean = True
    Private g_bInit As Boolean = False

    Private g_bBulbOffsetPreviewTop As Boolean = False

    Structure STRUC_VIEWPOINT_OFFSET_ITEM
        Dim sName As String
        Dim iX As Single
        Dim iY As Single
        Dim iZ As Single

        Dim bValid As Boolean

        Public Sub New(_Name As String, _Offset As Vector3)
            sName = _Name
            iX = _Offset.X
            iY = _Offset.Y
            iZ = _Offset.Z
            bValid = True
        End Sub

        Public Overrides Function ToString() As String
            Return sName
        End Function
    End Structure

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

            ComboBox_OculusButtonLayout.Items.Clear()
            ComboBox_OculusButtonLayout.Items.Add("X/A, Y/B, Grip, Stick (Left: [O], [▲], [X], [■] / Right: [X], [■], [O], [▲])")
            ComboBox_OculusButtonLayout.Items.Add("X/A, Y/B, Grip, Stick (Left: [X], [■], [O], [▲] / Right: [O], [▲], [X], [■])")
            ComboBox_OculusButtonLayout.Items.Add("X/A, Y/B, Grip, Stick (Both: [O], [▲], [X], [■])")
            ComboBox_OculusButtonLayout.Items.Add("X/A, Y/B, Grip, Stick (Both: [X], [■], [O], [▲])")

            ComboBox_OculusButtonLayout.SelectedIndex = 0

            If (ComboBox_OculusButtonLayout.Items.Count <> UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_OCULUS_BUTTON_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_HtcTouchpadClickMethod.Items.Clear()
            ComboBox_HtcTouchpadClickMethod.Items.Add("Button Drag (Left: [▲] / Right: [■])")
            ComboBox_HtcTouchpadClickMethod.Items.Add("Button Drag (Both: [■])")
            ComboBox_HtcTouchpadClickMethod.Items.Add("Button Drag (Both: [▲])")
            ComboBox_HtcTouchpadClickMethod.Items.Add("While holding MOVE [~] button")

            ComboBox_HtcTouchpadClickMethod.SelectedIndex = 0

            If (ComboBox_HtcTouchpadClickMethod.Items.Count <> UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_HtcGrabButtonMethod.Items.Clear()
            ComboBox_HtcGrabButtonMethod.Items.Add("Button Toggle (Left: [O] / Right: [X])")
            ComboBox_HtcGrabButtonMethod.Items.Add("Button Toggle (Both: [X])")
            ComboBox_HtcGrabButtonMethod.Items.Add("Button Toggle (Both: [O])")
            ComboBox_HtcGrabButtonMethod.Items.Add("Button Holding (Left: [O] / Right: [X])")
            ComboBox_HtcGrabButtonMethod.Items.Add("Button Holding (Both: [X])")
            ComboBox_HtcGrabButtonMethod.Items.Add("Button Holding (Both: [O])")

            ComboBox_HtcGrabButtonMethod.SelectedIndex = 0

            If (ComboBox_HtcGrabButtonMethod.Items.Count <> UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            g_bIgnoreEvents = True

            ComboBox_JoystickMethod.Items.Clear()
            ComboBox_JoystickMethod.Items.Add("Use Controller Position")
            ComboBox_JoystickMethod.Items.Add("Use Controller Orientation")

            ComboBox_JoystickMethod.SelectedIndex = 0

            If (ComboBox_JoystickMethod.Items.Count <> UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_CONTROLLER_JOYSTICK_METHOD.__MAX) Then
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

            ComboBox_HmdViewOffsetPreset.Items.Clear()
            ComboBox_HmdViewOffsetPreset.Items.Add("Select viewpoint preset...")
            ComboBox_HmdViewOffsetPreset.Items.Add(New STRUC_VIEWPOINT_OFFSET_ITEM("PhoneVR HMD Left", New Vector3(-90, -90, 0)))
            ComboBox_HmdViewOffsetPreset.Items.Add(New STRUC_VIEWPOINT_OFFSET_ITEM("PhoneVR HMD Right", New Vector3(90, -90, 0)))
            ComboBox_HmdViewOffsetPreset.Items.Add(New STRUC_VIEWPOINT_OFFSET_ITEM("Default", New Vector3(0, 0, 0)))

            ComboBox_HmdViewOffsetPreset.SelectedIndex = 0
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

        CreateControl()
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

        Try
            LoadSettings()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

        SetBulbOffsetPreview(False)
    End Sub

    Public Sub LoadSettings()
        Dim mClassSettings = g_UCVirtualMotionTracker.g_ClassSettings
        Dim mUCVmtPlayspaceCalib = g_UCVirtualMotionTracker.g_UCVmtPlayspaceCalib

        mClassSettings.LoadSettings()

        Try
            g_bIgnoreEvents = True

            ' Controller Settings
            ' Htc
            CheckBox_HtcTouchpadShortcuts.Checked = mClassSettings.m_ControllerSettings.m_HtcTouchpadShortcutBinding
            CheckBox_HtcTouchpadShortcutClick.Checked = mClassSettings.m_ControllerSettings.m_HtcTouchpadShortcutTouchpadClick
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_HtcTouchpadClickDeadzone, mClassSettings.m_ControllerSettings.m_HtcTouchpadClickDeadzone)
            ClassMathUtils.SetComboBoxSelectedIndexClamp(ComboBox_HtcTouchpadClickMethod, mClassSettings.m_ControllerSettings.m_HtcTouchpadEmulationClickMethod)
            ClassMathUtils.SetComboBoxSelectedIndexClamp(ComboBox_HtcGrabButtonMethod, mClassSettings.m_ControllerSettings.m_HtcGripButtonMethod)

            'Oculus 
            ClassMathUtils.SetComboBoxSelectedIndexClamp(ComboBox_OculusButtonLayout, mClassSettings.m_ControllerSettings.m_OculusButtonMethod)
            CheckBox_OculusGripToggle.Checked = mClassSettings.m_ControllerSettings.m_OculusGripToggle
            CheckBox_HybridGripToggle.Checked = mClassSettings.m_ControllerSettings.m_HybridGripToggle

            'Misc 
            ClassMathUtils.SetComboBoxSelectedIndexClamp(ComboBox_JoystickMethod, mClassSettings.m_ControllerSettings.m_ControllerJoystickMethod)
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_JoystickArea, mClassSettings.m_ControllerSettings.m_ControllerJoystickAreaCm)

            CheckBox_ControllerRecenterEnabled.Checked = mClassSettings.m_ControllerSettings.m_EnableControllerRecenter
            ClassMathUtils.SetComboBoxSelectedIndexClamp(ComboBox_RecenterMethod, mClassSettings.m_ControllerSettings.m_ControllerRecenterMethod)

            ComboBox_RecenterFromDevice.Items.Clear()
            ComboBox_RecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(mClassSettings.m_ControllerSettings.m_ControllerRecenterFromDeviceName))
            ComboBox_RecenterFromDevice.SelectedIndex = 0

            CheckBox_HmdRecenterEnabled.Checked = mClassSettings.m_ControllerSettings.m_EnableHmdRecenter
            ClassMathUtils.SetComboBoxSelectedIndexClamp(ComboBox_HmdRecenterMethod, mClassSettings.m_ControllerSettings.m_HmdRecenterMethod)

            ComboBox_HmdRecenterFromDevice.Items.Clear()
            ComboBox_HmdRecenterFromDevice.Items.Add(New ClassRecenterDeviceItem(mClassSettings.m_ControllerSettings.m_HmdRecenterFromDeviceName))
            ComboBox_HmdRecenterFromDevice.SelectedIndex = 0

            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_RecenterButtonTime, mClassSettings.m_ControllerSettings.m_RecenterButtonTimeMs)
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_OscThreadSleep, mClassSettings.m_ControllerSettings.m_OscThreadSleepMs)

            CheckBox_PlayCalibEnabled.Checked = mClassSettings.m_ControllerSettings.m_EnablePlayspaceRecenter

            ' Hmd Settings
            CheckBox_ShowDistSettings.Checked = mClassSettings.m_HmdSettings.m_UseCustomDistortion
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PsvrDistK0, mClassSettings.m_HmdSettings.m_DistortionK0(True))
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PsvrDistK1, mClassSettings.m_HmdSettings.m_DistortionK1(True))
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PsvrDistScale, mClassSettings.m_HmdSettings.m_DistortionScale(True))
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PsvrDistRedOffset, mClassSettings.m_HmdSettings.m_DistortionRedOffset(True))
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PsvrDistGreenOffset, mClassSettings.m_HmdSettings.m_DistortionGreenOffset(True))
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PsvrDistBlueOffset, mClassSettings.m_HmdSettings.m_DistortionBlueOffset(True))
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PsvrHFov, mClassSettings.m_HmdSettings.m_HFov(True))
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PsvrVFov, mClassSettings.m_HmdSettings.m_VFov(True))
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PsvrIPD, mClassSettings.m_HmdSettings.m_IPD)
            ComboBox_PsvrRenderResolution.SelectedItem = New STRUC_RENDER_RES_ITEM(mClassSettings.m_HmdSettings.m_RenderScale)
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_BulbOffsetX, mClassSettings.m_HmdSettings.m_ViewPositionOffset.X)
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_BulbOffsetY, mClassSettings.m_HmdSettings.m_ViewPositionOffset.Y)
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_BulbOffsetZ, mClassSettings.m_HmdSettings.m_ViewPositionOffset.Z)
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_HmdViewOffsetX, mClassSettings.m_HmdSettings.m_ViewRotationOffset.X)
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_HmdViewOffsetY, mClassSettings.m_HmdSettings.m_ViewRotationOffset.Y)
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_HmdViewOffsetZ, mClassSettings.m_HmdSettings.m_ViewRotationOffset.Z)
            ' $TODO Replace all with ClampValue()

            'Misc Settings
            CheckBox_DisableBasestations.Checked = mClassSettings.m_MiscSettings.m_DisableBaseStationSpawning
            CheckBox_EnableHeptics.Checked = mClassSettings.m_MiscSettings.m_EnableHepticFeedback
            CheckBox_OptimizePackets.Checked = mClassSettings.m_MiscSettings.m_OptimizeTransportPackets
            CheckBox_RenderFix.Checked = mClassSettings.m_MiscSettings.m_RenderWindowFix
            CheckBox_EnableVelocityHmd.Checked = mClassSettings.m_MiscSettings.m_EnableVelocityHmd
            CheckBox_EnableVelocityControllers.Checked = mClassSettings.m_MiscSettings.m_EnableVelocityController
            CheckBox_EnableVelocityTrackers.Checked = mClassSettings.m_MiscSettings.m_EnableVelocityTracker
            CheckBox_EnableManualVelocity.Checked = mClassSettings.m_MiscSettings.m_EnableManualVelocity
            TextBox_OscRemoteIP.Text = mClassSettings.m_MiscSettings.m_OscRemoteIP

            ' Playspace Settings 
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PlayCalibForwardOffset, mClassSettings.m_PlayspaceSettings.m_ForwardOffset)
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PlayCalibSideOffset, mClassSettings.m_PlayspaceSettings.m_SideOffset)
            ClassMathUtils.SetNumericUpDownValueClamp(NumericUpDown_PlayCalibHeightOffset, mClassSettings.m_PlayspaceSettings.m_HeightOffset)
            ClassMathUtils.SetComboBoxSelectedIndexClamp(ComboBox_PlayCalibForwardMethod, mClassSettings.m_PlayspaceSettings.m_ForwardMethod)
            ClassMathUtils.SetComboBoxSelectedIndexClamp(mUCVmtPlayspaceCalib.ComboBox_PlayCalibControllerID, mClassSettings.m_PlayspaceSettings.m_CalibrationControllerId)
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
        mMsg.ShowDialog(g_UCVirtualMotionTracker.g_mFormMain)
    End Sub

    Private Sub CheckBox_JoystickShortcuts_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_HtcTouchpadShortcuts.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HtcTouchpadShortcutBinding = CheckBox_HtcTouchpadShortcuts.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_JoystickShortcutClick_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_HtcTouchpadShortcutClick.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HtcTouchpadShortcutTouchpadClick = CheckBox_HtcTouchpadShortcutClick.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_HtcTouchpadClickMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_HtcTouchpadClickMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HtcTouchpadEmulationClickMethod = CType(ComboBox_HtcTouchpadClickMethod.SelectedIndex, UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_TOUCHPAD_CLICK_METHOD)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_HtcGrabButtonMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_HtcGrabButtonMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HtcGripButtonMethod = CType(ComboBox_HtcGrabButtonMethod.SelectedIndex, UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_HTC_GRIP_BUTTON_METHOD)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_OculusButtonLayout_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_OculusButtonLayout.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_OculusButtonMethod = CType(ComboBox_OculusButtonLayout.SelectedIndex, UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_OCULUS_BUTTON_METHOD)
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_OculusGripToggle_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_OculusGripToggle.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_OculusGripToggle = CheckBox_OculusGripToggle.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_HybridGripToggle_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_HybridGripToggle.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HybridGripToggle = CheckBox_HybridGripToggle.Checked
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

    Private Sub CheckBox_EnableVelocity_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_EnableVelocityHmd.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_EnableVelocityHmd = CheckBox_EnableVelocityHmd.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_EnableVelocityControllers_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_EnableVelocityControllers.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_EnableVelocityController = CheckBox_EnableVelocityControllers.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_EnableVelocityTrackers_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_EnableVelocityTrackers.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_EnableVelocityTracker = CheckBox_EnableVelocityTrackers.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_EnableManualVelocity_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_EnableManualVelocity.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_MiscSettings.m_EnableManualVelocity = CheckBox_EnableManualVelocity.Checked
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_JoystickMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_JoystickMethod.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_ControllerJoystickMethod = CType(ComboBox_JoystickMethod.SelectedIndex, UCVirtualMotionTracker.ClassSettings.STRUC_CONTROLLER_SETTINGS.ENUM_CONTROLLER_JOYSTICK_METHOD)
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

    Private Sub NumericUpDown_HtcTouchpadClickDeadzone_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_HtcTouchpadClickDeadzone.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_HtcTouchpadClickDeadzone = NumericUpDown_HtcTouchpadClickDeadzone.Value
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_JoystickArea_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_JoystickArea.ValueChanged
        ' See TOUCHPAD_GYRO_MULTI in UCVirutalMotionTrackerItem.vb
        Label_TouchpadTouchAreaDeg.Text = String.Format("cm / {0}°", NumericUpDown_JoystickArea.Value * 2.5F)

        If (g_bIgnoreEvents) Then
            Return
        End If

        g_UCVirtualMotionTracker.g_ClassSettings.m_ControllerSettings.m_ControllerJoystickAreaCm = NumericUpDown_JoystickArea.Value
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

    Private Sub NumericUpDown_BulbOffsetX_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_BulbOffsetX.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Dim iBulbOffset As New Vector3(
            NumericUpDown_BulbOffsetX.Value,
            NumericUpDown_BulbOffsetY.Value,
            NumericUpDown_BulbOffsetZ.Value)

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_ViewPositionOffset = iBulbOffset
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
        SetBulbOffsetPreview(True)
    End Sub

    Private Sub NumericUpDown_BulbOffsetY_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_BulbOffsetY.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Dim iBulbOffset As New Vector3(
            NumericUpDown_BulbOffsetX.Value,
            NumericUpDown_BulbOffsetY.Value,
            NumericUpDown_BulbOffsetZ.Value)

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_ViewPositionOffset = iBulbOffset
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
        SetBulbOffsetPreview(False)
    End Sub

    Private Sub NumericUpDown_BulbOffsetZ_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_BulbOffsetZ.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Dim iBulbOffset As New Vector3(
            NumericUpDown_BulbOffsetX.Value,
            NumericUpDown_BulbOffsetY.Value,
            NumericUpDown_BulbOffsetZ.Value)

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_ViewPositionOffset = iBulbOffset
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
        SetBulbOffsetPreview(g_bBulbOffsetPreviewTop)
    End Sub

    Private Sub NumericUpDown_HmdViewOffsetX_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_HmdViewOffsetX.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Dim mOffset As New Vector3(
            NumericUpDown_HmdViewOffsetX.Value,
            NumericUpDown_HmdViewOffsetY.Value,
            NumericUpDown_HmdViewOffsetZ.Value)

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_ViewRotationOffset = mOffset
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_HmdViewOffsetY_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_HmdViewOffsetY.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Dim mOffset As New Vector3(
            NumericUpDown_HmdViewOffsetX.Value,
            NumericUpDown_HmdViewOffsetY.Value,
            NumericUpDown_HmdViewOffsetZ.Value)

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_ViewRotationOffset = mOffset
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_HmdViewOffsetZ_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_HmdViewOffsetZ.ValueChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Dim mOffset As New Vector3(
            NumericUpDown_HmdViewOffsetX.Value,
            NumericUpDown_HmdViewOffsetY.Value,
            NumericUpDown_HmdViewOffsetZ.Value)

        g_UCVirtualMotionTracker.g_ClassSettings.m_HmdSettings.m_ViewRotationOffset = mOffset
        g_UCVirtualMotionTracker.g_ClassSettings.SetUnsavedState(True)
    End Sub

    Private Sub NumericUpDown_BulbOffsetX_Click(sender As Object, e As EventArgs) Handles NumericUpDown_BulbOffsetX.Click
        SetBulbOffsetPreview(True)
    End Sub

    Private Sub NumericUpDown_BulbOffsetY_Click(sender As Object, e As EventArgs) Handles NumericUpDown_BulbOffsetY.Click
        SetBulbOffsetPreview(False)
    End Sub

    Private Sub NumericUpDown_BulbOffsetZ_Click(sender As Object, e As EventArgs) Handles NumericUpDown_BulbOffsetZ.Click
        SetBulbOffsetPreview(g_bBulbOffsetPreviewTop)
    End Sub

    Private Sub SetBulbOffsetPreview(bTopView As Boolean)
        g_bBulbOffsetPreviewTop = bTopView

        If (bTopView) Then
            PictureBoxQuality_HmdViewOffset.Image = My.Resources.PSVR_View_Top
        Else
            PictureBoxQuality_HmdViewOffset.Image = My.Resources.PSVR_View_Side
        End If

        Dim iScale As Single = 0.032F
        Dim iBulbOffset As New Vector3(
            NumericUpDown_BulbOffsetX.Value,
            NumericUpDown_BulbOffsetY.Value,
            NumericUpDown_BulbOffsetZ.Value)

        If (bTopView) Then
            Dim iTopCenter As New Vector2(
                PictureBoxQuality_HmdViewOffset.Width * 0.5F,
                PictureBoxQuality_HmdViewOffset.Height * 0.075F)

            Dim mOffsetPoint = New Point(CInt(iTopCenter.X), CInt(iTopCenter.Y))
            mOffsetPoint.X -= CInt(Panel_BulbOffset.Width / 2.0F)
            mOffsetPoint.Y -= CInt(Panel_BulbOffset.Height / 2.0F)

            mOffsetPoint.X += CInt((iBulbOffset.X * iScale) * PictureBoxQuality_HmdViewOffset.Width)
            mOffsetPoint.Y += CInt((iBulbOffset.Z * iScale) * PictureBoxQuality_HmdViewOffset.Height)

            Panel_BulbOffset.Location = mOffsetPoint
        Else
            Dim iSideCenter As New Vector2(
                PictureBoxQuality_HmdViewOffset.Width * 0.925F,
                PictureBoxQuality_HmdViewOffset.Height * 0.5F)

            Dim mOffsetPoint = New Point(CInt(iSideCenter.X), CInt(iSideCenter.Y))
            mOffsetPoint.X -= CInt(Panel_BulbOffset.Width / 2.0F)
            mOffsetPoint.Y -= CInt(Panel_BulbOffset.Height / 2.0F)

            mOffsetPoint.X -= CInt((iBulbOffset.Z * iScale) * PictureBoxQuality_HmdViewOffset.Width)
            mOffsetPoint.Y -= CInt((iBulbOffset.Y * iScale) * PictureBoxQuality_HmdViewOffset.Height)

            Panel_BulbOffset.Location = mOffsetPoint
        End If
    End Sub

    Private Sub LinkLabel_EnableVelocityPerFrame_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_EnableVelocityPerFrame.LinkClicked
        TabControl_SettingsDevices.SelectedTab = TabPage_SettingsOther

        CheckBox_EnableManualVelocity.Focus()
    End Sub

    Private Sub ComboBox_HmdViewOffsetPreset_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_HmdViewOffsetPreset.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        If (TypeOf ComboBox_HmdViewOffsetPreset.SelectedItem IsNot STRUC_VIEWPOINT_OFFSET_ITEM) Then
            Return
        End If

        Dim mViewpointOffset = DirectCast(ComboBox_HmdViewOffsetPreset.SelectedItem, STRUC_VIEWPOINT_OFFSET_ITEM)

        NumericUpDown_HmdViewOffsetX.Value = CDec(mViewpointOffset.iX)
        NumericUpDown_HmdViewOffsetY.Value = CDec(mViewpointOffset.iY)
        NumericUpDown_HmdViewOffsetZ.Value = CDec(mViewpointOffset.iZ)
    End Sub

    Private Sub CleanUp()

    End Sub
End Class
