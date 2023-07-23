Imports System.ComponentModel
Imports System.Numerics
Imports Rug.Osc

Public Class UCVirtualMotionTracker
    Public g_mUCVirtualControllers As UCVirtualControllers

    Private g_mAutostartMenuStrips As New Dictionary(Of Integer, ToolStripMenuItem)
    Private g_mVMTControllers As New List(Of UCVirtualMotionTrackerItem)
    Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "vmt_devices.ini")

    Public g_ClassOscServer As ClassOscServer
    Public g_ClassControllerSettings As ClassControllerSettings
    Public g_ClassOscDevices As ClassOscDevices

    Private g_bIgnoreEvents As Boolean = False
    Private g_mOscStatusThread As Threading.Thread = Nothing
    Private g_mOscDeviceStatusThread As Threading.Thread = Nothing

    Public Sub New(_mUCVirtualControllers As UCVirtualControllers)
        g_mUCVirtualControllers = _mUCVirtualControllers

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        g_ClassOscServer = New ClassOscServer
        g_ClassControllerSettings = New ClassControllerSettings(Me)
        g_ClassOscDevices = New ClassOscDevices(Me)

        Try
            g_bIgnoreEvents = True

            ComboBox_TouchpadClickMethod.Items.Clear()
            ComboBox_TouchpadClickMethod.Items.Add("Button Drag (Left Controller: TRIANGLE [▲] / Right Controller: SQUARE [■])")
            ComboBox_TouchpadClickMethod.Items.Add("Button Drag (Both Controllers: SQUARE [■])")
            ComboBox_TouchpadClickMethod.Items.Add("Button Drag (Both Controllers: TRIANGLE [▲])")
            ComboBox_TouchpadClickMethod.Items.Add("While holding MOVE [~] button")

            ComboBox_TouchpadClickMethod.SelectedIndex = 0

            If (ComboBox_TouchpadClickMethod.Items.Count <> ClassControllerSettings.ENUM_HTC_TOUCHPAD_CLICK_METHOD.__MAX) Then
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

            If (ComboBox_GrabButtonMethod.Items.Count <> ClassControllerSettings.ENUM_HTC_GRIP_BUTTON_METHOD.__MAX) Then
                Throw New ArgumentException("Invalid size")
            End If
        Finally
            g_bIgnoreEvents = False
        End Try

        For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
            Dim mItem As New ToolStripMenuItem("Controller ID: " & CStr(i))

            g_mAutostartMenuStrips(i) = mItem

            mItem.Tag = i

            ContextMenuStrip_Autostart.Items.Add(mItem)
        Next

        CreateControl()

        Panel_SteamVRRestart.Visible = False

        g_mOscStatusThread = New Threading.Thread(AddressOf OscStatusThread)
        g_mOscStatusThread.IsBackground = True
        g_mOscStatusThread.Start()

        g_mOscDeviceStatusThread = New Threading.Thread(AddressOf OscDeviceStatusThread)
        g_mOscDeviceStatusThread.IsBackground = True
        g_mOscDeviceStatusThread.Start()
    End Sub

    Private Sub UCControllerAttachments_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            g_ClassControllerSettings.LoadSettings()

            Try
                g_bIgnoreEvents = True

                CheckBox_JoystickShortcuts.Checked = g_ClassControllerSettings.m_JoystickShortcutBinding
                CheckBox_JoystickShortcutClick.Checked = g_ClassControllerSettings.m_JoystickShortcutTouchpadClick
                CheckBox_DisableBasestations.Checked = g_ClassControllerSettings.m_DisableBaseStationSpawning
                CheckBox_EnableHeptics.Checked = g_ClassControllerSettings.m_EnableHepticFeedback
                ComboBox_TouchpadClickMethod.SelectedIndex = g_ClassControllerSettings.m_HtcTouchpadEmulationClickMethod
                ComboBox_GrabButtonMethod.SelectedIndex = g_ClassControllerSettings.m_HtcGripButtonMethod

                g_ClassControllerSettings.SetUnsavedState(False)
            Finally
                g_bIgnoreEvents = False
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            AutostartLoad()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            RefreshOverrides()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub AutostartLoad()
        Dim mAutostartIndexes As New List(Of Integer)

        Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            Using mIni As New ClassIni(mStream)
                For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                    If (g_mAutostartMenuStrips(i) Is Nothing OrElse g_mAutostartMenuStrips(i).IsDisposed) Then
                        Continue For
                    End If

                    If (mIni.ReadKeyValue("Autostart", CStr(i), "false") = "true") Then
                        mAutostartIndexes.Add(i)
                    End If
                Next
            End Using
        End Using

        For i = 0 To mAutostartIndexes.Count - 1
            Try
                AddVmtTracker(mAutostartIndexes(i))
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next
    End Sub

    Private Sub AddVmtTracker(id As Integer)
        ' Remove disposed controls
        For i = g_mVMTControllers.Count - 1 To 0 Step -1
            If (g_mVMTControllers(i) Is Nothing OrElse g_mVMTControllers(i).IsDisposed) Then
                g_mVMTControllers.RemoveAt(i)
            End If
        Next

        If (g_mVMTControllers.Count >= ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT) Then
            Throw New ArgumentException("Maximum of trackers reached")
        End If

        Dim mItem As New UCVirtualMotionTrackerItem(id, Me)
        g_mVMTControllers.Add(mItem)

        mItem.Parent = Panel_VMTTrackers
        mItem.Dock = DockStyle.Top
    End Sub

    Private Sub Button_VMTControllers_Click(sender As Object, e As EventArgs) Handles Button_VMTControllers.Click
        ContextMenuStrip_Autostart.Show(Button_VMTControllers, New Point(0, Button_VMTControllers.Size.Height))
    End Sub

    Private Sub ContextMenuStrip_Autostart_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ContextMenuStrip_Autostart.ItemClicked
        Dim mItem As ToolStripMenuItem = TryCast(e.ClickedItem, ToolStripMenuItem)
        If (mItem Is Nothing) Then
            Return
        End If

        mItem.Checked = Not mItem.Checked

        Dim iIndex As Integer = CInt(mItem.Tag)

        Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            Using mIni As New ClassIni(mStream)
                Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("Autostart", CStr(iIndex), If(mItem.Checked, "true", "false")))

                mIni.WriteKeyValue(mIniContent.ToArray)
            End Using
        End Using
    End Sub

    Private Sub ContextMenuStrip_Autostart_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip_Autostart.Opening
        Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            Using mIni As New ClassIni(mStream)
                For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_CONTROLLER_COUNT - 1
                    If (g_mAutostartMenuStrips(i) Is Nothing OrElse g_mAutostartMenuStrips(i).IsDisposed) Then
                        Continue For
                    End If

                    g_mAutostartMenuStrips(i).Checked = (mIni.ReadKeyValue("Autostart", CStr(i), "false") = "true")
                Next
            End Using
        End Using
    End Sub

    Private Sub Button_AddVMTController_Click(sender As Object, e As EventArgs) Handles Button_AddVMTController.Click
        Try
            AddVmtTracker(-1)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabel_ReadMore_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ReadMore.LinkClicked
        ' TODO: Add help
    End Sub

    Private Sub Button_Add_Click(sender As Object, e As EventArgs) Handles Button_Add.Click
        Try
            Dim mConfig As New ClassSteamVRConfig
            mConfig.LoadConfig()
            If (Not mConfig.LoadConfig()) Then
                Throw New ArgumentException("Unable to load SteamVR configs")
            End If

            Dim sKnownTrackers As String() = mConfig.m_ClassTrackerRoles.GetKnownTrackers

            Using i As New FormTrackerOverrideSetup(sKnownTrackers)
                If (i.ShowDialog = DialogResult.OK) Then
                    Dim mResult = i.m_DialogResult

                    Dim sTracker As String = ""
                    Dim sOverride As String = ""

                    If (mResult.bCustomTracker) Then
                        sTracker = mResult.sCustomTrackerName
                    Else
                        sTracker = (ClassVmtConst.VMT_DEVICE_SERIAL & mResult.iVMTTracker)
                    End If

                    Select Case (mResult.iOverrideType)
                        Case FormTrackerOverrideSetup.ENUM_OVERRIDE_TYPE.HEAD
                            sOverride = "/user/head"
                        Case FormTrackerOverrideSetup.ENUM_OVERRIDE_TYPE.LEFT_HAND
                            sOverride = "/user/hand/left"
                        Case FormTrackerOverrideSetup.ENUM_OVERRIDE_TYPE.RIGHT_HAND
                            sOverride = "/user/hand/right"
                        Case Else
                            Throw New ArgumentException("Invalid")
                    End Select

                    If (mConfig.m_ClassOverrides.GetOverride(sTracker) IsNot Nothing) Then
                        If (MessageBox.Show(String.Format("A tracker with the name '{0}' already exists! Do you want to override the tracker override with the current one?", sTracker), "Override?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No) Then
                            Return
                        End If
                    End If

                    mConfig.m_ClassOverrides.SetOverride(sTracker, sOverride)
                    mConfig.SaveConfig()

                    RefreshOverrides()

                    ' Check if SteamVR is running
                    If (Process.GetProcessesByName("vrserver").Length > 0) Then
                        g_mUCVirtualControllers.g_mUCVirtualMotionTracker.Panel_SteamVRRestart.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button_Remove_Click(sender As Object, e As EventArgs) Handles Button_Remove.Click
        Try
            If (ListView_Overrides.SelectedItems.Count < 1) Then
                Return
            End If

            Dim sMessage As New Text.StringBuilder
            sMessage.AppendLine("Are you sure you want to remove following trackers from the overrides?")
            sMessage.AppendLine()
            For Each mSelectedItem As ListViewItem In ListView_Overrides.SelectedItems
                sMessage.AppendLine(mSelectedItem.SubItems(0).Text)
            Next
            If (MessageBox.Show(sMessage.ToString, "Remove overrides", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No) Then
                Return
            End If

            Dim mConfig As New ClassSteamVRConfig
            If (Not mConfig.LoadConfig()) Then
                Throw New ArgumentException("Unable to load SteamVR configs")
            End If

            For Each mSelectedItem As ListViewItem In ListView_Overrides.SelectedItems
                mConfig.m_ClassOverrides.RemoveOverride(mSelectedItem.SubItems(0).Text)
            Next
            mConfig.SaveConfig()

            RefreshOverrides()

            ' Check if SteamVR is running
            If (Process.GetProcessesByName("vrserver").Length > 0) Then
                g_mUCVirtualControllers.g_mUCVirtualMotionTracker.Panel_SteamVRRestart.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button_Refresh_Click(sender As Object, e As EventArgs) Handles Button_Refresh.Click
        Try
            RefreshOverrides()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RefreshOverrides()
        ListView_Overrides.Items.Clear()

        Dim mSteamCOnfig As New ClassSteamVRConfig
        If (mSteamCOnfig.LoadConfig()) Then
            For Each mOverride In mSteamCOnfig.m_ClassOverrides.GetOverrides()
                ListView_Overrides.Items.Add(New ListViewItem(New String() {mOverride.Key, mOverride.Value}))
            Next
        End If
    End Sub

    Private Sub LinkLabel_SteamVRRestartOff_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_SteamVRRestartOff.LinkClicked
        g_mUCVirtualControllers.g_mUCVirtualMotionTracker.Panel_SteamVRRestart.Visible = False
    End Sub

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

    Public Sub LinkLabel_OscRun_Click()
        LinkLabel_OscRun_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_OscRun_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_OscRun.LinkClicked
        Try
            g_ClassOscServer.StartServer()
            g_ClassOscServer.m_SuspendRequests = False

            g_ClassOscDevices.StartThread()

            g_mUCVirtualControllers.g_mFormMain.g_mPSMoveServiceCAPI.RegisterPoseStream("VMT")
        Catch ex As Exception
            With New Text.StringBuilder
                .AppendLine("Unable to create OSC Server!")
                .AppendLine()
                .AppendLine(ex.Message)

                MessageBox.Show(.ToString)
            End With
        End Try
    End Sub

    Public Sub LinkLabel_OscPause_Click()
        LinkLabel_OscPause_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_OscPause_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_OscPause.LinkClicked
        g_ClassOscServer.m_SuspendRequests = True

        g_mUCVirtualControllers.g_mFormMain.g_mPSMoveServiceCAPI.UnregisterPoseStream("VMT")
    End Sub

    Public Sub LinkLabel_DriverInstall_Click()
        LinkLabel_DriverInstall_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_SteamRun_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_SteamRun.LinkClicked
        Try
            Process.Start("steam://rungameid/250820")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LinkLabel_DriverInstall_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_DriverInstall.LinkClicked
        Try
            If (Process.GetProcessesByName("vrserver").Count > 0) Then
                Throw New ArgumentException("SteamVR is running! Close SteamVR and try again.")
            End If

            Dim sDriverRoot As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), ClassVmtConst.VMT_DRIVER_ROOT_PATH)
            If (Not IO.Directory.Exists(sDriverRoot)) Then
                Throw New ArgumentException("Could not find driver root folder!")
            End If

            Dim sDriverDLL As String = IO.Path.Combine(IO.Path.Combine(sDriverRoot, "bin\win64"), ClassVmtConst.VMT_DRIVER_FILE)
            If (Not IO.File.Exists(sDriverDLL)) Then
                Throw New ArgumentException(String.Format("Could not find driver '{0}'!", ClassVmtConst.VMT_DRIVER_FILE))
            End If

            Dim mConfig As New ClassOpenVRConfig()
            If (Not mConfig.LoadConfig()) Then
                Throw New ArgumentException("Unable to find and load OpenVR config!")
            End If

            ' Find outdated drivers
            If (True) Then
                Dim mDrivers As String() = mConfig.GetDrivers()
                If (mDrivers IsNot Nothing) Then
                    For Each sDriver As String In mDrivers
                        Dim sDriverPath As String = IO.Path.GetFullPath(sDriver)
                        If (sDriverPath.ToLowerInvariant = sDriverRoot.ToLowerInvariant) Then
                            Continue For
                        End If

                        If (sDriverPath.ToLowerInvariant.EndsWith(String.Format("\{0}", ClassVmtConst.VMT_DRIVER_NAME.ToLowerInvariant))) Then
                            Dim sMsg As New Text.StringBuilder
                            sMsg.AppendLine("Another version of the SteamVR driver is already installed!")
                            sMsg.AppendLine("Do you want to remove the following outdated driver?")
                            sMsg.AppendLine()
                            sMsg.AppendLine(sDriverPath)
                            If (MessageBox.Show(sMsg.ToString, "Outdated driver found", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes) Then
                                mConfig.RemovePath(sDriver)
                                mConfig.SaveConfig()
                            End If
                        End If
                    Next
                End If
            End If

            ' Find same driver
            If (True) Then
                Dim mDrivers As String() = mConfig.GetDrivers()
                If (mDrivers IsNot Nothing) Then
                    For Each sDriver As String In mDrivers
                        Dim sDriverPath As String = IO.Path.GetFullPath(sDriver)
                        If (sDriverPath.ToLowerInvariant = sDriverRoot.ToLowerInvariant) Then
                            MessageBox.Show("SteamVR driver is already installed!", "Unable to install driver", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return
                        End If
                    Next
                End If
            End If

            mConfig.AddPath(sDriverRoot)
            mConfig.SaveConfig()

            MessageBox.Show("Driver has ben successfully registered!", "Driver added to SteamVR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LinkLabel_DriverUninstall_Click()
        LinkLabel_DriverUninstall_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub LinkLabel_DriverUninstall_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_DriverUninstall.LinkClicked
        Try
            If (Process.GetProcessesByName("vrserver").Count > 0) Then
                Throw New ArgumentException("SteamVR is running! Close SteamVR and try again.")
            End If

            Dim sDriverRoot As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), ClassVmtConst.VMT_DRIVER_ROOT_PATH)

            Dim mConfig As New ClassOpenVRConfig()
            If (Not mConfig.LoadConfig()) Then
                Throw New ArgumentException("Unable to find and load OpenVR config!")
            End If

            mConfig.RemovePath(sDriverRoot)
            mConfig.SaveConfig()

            MessageBox.Show("Driver has ben successfully unregistered!", "Driver removed from SteamVR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CleanUp()
        If (g_mOscDeviceStatusThread IsNot Nothing AndAlso g_mOscDeviceStatusThread.IsAlive) Then
            g_mOscDeviceStatusThread.Abort()
            g_mOscDeviceStatusThread.Join()
            g_mOscDeviceStatusThread = Nothing
        End If

        If (g_mOscStatusThread IsNot Nothing AndAlso g_mOscStatusThread.IsAlive) Then
            g_mOscStatusThread.Abort()
            g_mOscStatusThread.Join()
            g_mOscStatusThread = Nothing
        End If

        For Each mItem In g_mVMTControllers
            If (mItem IsNot Nothing AndAlso Not mItem.IsDisposed) Then
                mItem.Dispose()
            End If
        Next
        g_mVMTControllers.Clear()

        For Each mItem In g_mAutostartMenuStrips
            If (mItem.Value IsNot Nothing AndAlso Not mItem.Value.IsDisposed) Then
                mItem.Value.Dispose()
            End If
        Next
        g_mAutostartMenuStrips.Clear()

        If (g_ClassOscDevices IsNot Nothing) Then
            g_ClassOscDevices.Dispose()
            g_ClassOscDevices = Nothing
        End If

        If (g_ClassOscServer IsNot Nothing) Then
            g_ClassOscServer.Dispose()
            g_ClassOscServer = Nothing
        End If
    End Sub

    Enum ENUM_OSC_CONNECTION_STATUS
        NOT_STARTED
        DISCONNETED
        CONNECTED
        TIMEOUT
    End Enum

    Private Sub SetOscServerStatus(i As ENUM_OSC_CONNECTION_STATUS)
        Select Case (i)
            Case ENUM_OSC_CONNECTION_STATUS.NOT_STARTED
                Label_OscStatus.Text = "OSC Uninitialized"
                Panel_OscStatus.BackColor = Color.FromArgb(224, 224, 224)

            Case ENUM_OSC_CONNECTION_STATUS.CONNECTED
                Label_OscStatus.Text = "OSC Connected"
                Panel_OscStatus.BackColor = Color.FromArgb(0, 192, 0)

            Case ENUM_OSC_CONNECTION_STATUS.DISCONNETED
                Label_OscStatus.Text = "OSC Disconnected"
                Panel_OscStatus.BackColor = Color.FromArgb(192, 0, 0)

            Case ENUM_OSC_CONNECTION_STATUS.TIMEOUT
                Label_OscStatus.Text = "OSC Timeout"
                Panel_OscStatus.BackColor = Color.FromArgb(192, 0, 0)

        End Select
    End Sub

    Private Sub OscStatusThread()
        While True
            Try
                If (g_ClassOscServer Is Nothing OrElse Not g_ClassOscServer.IsRunning()) Then
                    Me.BeginInvoke(Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.NOT_STARTED))
                Else
                    If (g_ClassOscServer.m_SuspendRequests) Then
                        Me.BeginInvoke(Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.DISCONNETED))
                    Else
                        Dim mLastResponse As TimeSpan = (Now - g_ClassOscServer.m_LastResponse)

                        If (mLastResponse.TotalMilliseconds > 5000) Then
                            Me.BeginInvoke(Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.TIMEOUT))
                        Else
                            Me.BeginInvoke(Sub() SetOscServerStatus(ENUM_OSC_CONNECTION_STATUS.CONNECTED))
                        End If
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception

            End Try

            Threading.Thread.Sleep(1000)
        End While
    End Sub

    Private Sub OscDeviceStatusThread()
        While True
            Try
                Dim mDevices = g_ClassOscDevices.GetDevices
                For i = 0 To mDevices.Length - 1
                    Dim mDevice As ClassOscDevices.STRUC_DEVICE = mDevices(i)

                    Dim mPos As Vector3 = mDevice.GetPosCm()
                    Dim mAng As Vector3 = mDevice.GetOrientationEuler()

                    Me.BeginInvoke(Sub()
                                       Dim bFound As Boolean = False

                                       For Each mListVIewItem As ListViewItem In ListView_OscDevices.Items
                                           Const LISTVIEW_SUBITEM_TYPE As Integer = 0
                                           Const LISTVIEW_SUBITEM_SERIAL As Integer = 1
                                           Const LISTVIEW_SUBITEM_POSITION As Integer = 2
                                           Const LISTVIEW_SUBITEM_ORIENTATION As Integer = 3

                                           If (mListVIewItem.SubItems(LISTVIEW_SUBITEM_SERIAL).Text = mDevice.sSerial) Then
                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_POSITION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z)))
                                               mListVIewItem.SubItems(LISTVIEW_SUBITEM_ORIENTATION).Text = String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z)))

                                               bFound = True
                                           End If
                                       Next

                                       If (Not bFound) Then
                                           ListView_OscDevices.Items.Add(New ListViewItem(New String() {
                                                mDevice.iType.ToString,
                                                mDevice.sSerial,
                                                String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mPos.X)), CInt(Math.Floor(mPos.Y)), CInt(Math.Floor(mPos.Z))),
                                                String.Format("X: {0}, Y: {1}, Z: {2}", CInt(Math.Floor(mAng.X)), CInt(Math.Floor(mAng.Y)), CInt(Math.Floor(mAng.Z)))
                                            }))
                                       End If
                                   End Sub)
                Next

            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception

            End Try

            Threading.Thread.Sleep(1000)
        End While
    End Sub

    Class ClassOscServer
        Implements IDisposable

        Shared _ThreadLock As New Object
        Private g_VmtOsc As ClassOSC = Nothing

        Public Event OnOscProcessBundle(mBundle As OscBundle)
        Public Event OnOscProcessMessage(mMessage As OscMessage)

        Public Event OnSuspendChanged()

        Private g_bSuspendRequest As Boolean = False
        Private g_mLastResponse As Date


        Public Sub New()
        End Sub

        Public Sub StartServer()
            SyncLock _ThreadLock
                If (g_VmtOsc IsNot Nothing) Then
                    Return
                End If

                g_VmtOsc = New ClassOSC(ClassVmtConst.VMT_IP, ClassVmtConst.VMT_PORT_RECEIVE, ClassVmtConst.VMT_PORT_SEND, AddressOf __OnOscProcessBundle, AddressOf __OnOscProcessMessage)
            End SyncLock

            RaiseEvent OnSuspendChanged()
        End Sub

        Property m_SuspendRequests As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bSuspendRequest
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bSuspendRequest = value
                End SyncLock

                RaiseEvent OnSuspendChanged()
            End Set
        End Property

        ReadOnly Property m_LastResponse As Date
            Get
                SyncLock _ThreadLock
                    Return g_mLastResponse
                End SyncLock
            End Get
        End Property

        Public Sub Send(mPacket As OscPacket)
            If (m_SuspendRequests) Then
                Return
            End If

            g_VmtOsc.Send(mPacket)
        End Sub

        Public Function IsRunning() As Boolean
            SyncLock _ThreadLock
                Return (g_VmtOsc IsNot Nothing)
            End SyncLock
        End Function

        Private Sub __OnOscProcessBundle(mBundle As OscBundle)
            If (m_SuspendRequests) Then
                Return
            End If

            SyncLock _ThreadLock
                g_mLastResponse = Now
            End SyncLock

            RaiseEvent OnOscProcessBundle(mBundle)
        End Sub

        Private Sub __OnOscProcessMessage(mMessage As OscMessage)
            If (m_SuspendRequests) Then
                Return
            End If

            SyncLock _ThreadLock
                g_mLastResponse = Now
            End SyncLock

            RaiseEvent OnOscProcessMessage(mMessage)
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    If (g_VmtOsc IsNot Nothing) Then
                        g_VmtOsc.Dispose()
                        g_VmtOsc = Nothing
                    End If

                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    Class ClassControllerSettings
        Private g_UCVirtualMotionTracker As UCVirtualMotionTracker
        Private Shared _ThreadLock As New Object

        Private g_bSettingsLoaded As Boolean = False

        Enum ENUM_HTC_TOUCHPAD_CLICK_METHOD
            BUTTON_MIRRORED
            BUTTON_STRICT_SQUARE
            BUTTON_STRICT_TRIANGLE
            MOVE_ALWAYS

            __MAX
        End Enum

        Enum ENUM_HTC_GRIP_BUTTON_METHOD
            BUTTON_TOGGLE_MIRRORED
            BUTTON_TOGGLE_STRICT_CROSS
            BUTTON_TOGGLE_STRICT_CIRCLE
            BUTTON_HOLDING_MIRRORED
            BUTTON_HOLDING_STRICT_CROSS
            BUTTON_HOLDING_STRICT_CIRCLE

            __MAX
        End Enum

        Private g_bJoystickShortcutBinding As Boolean = False
        Private g_bJoystickShortcutTouchpadClick As Boolean = False
        Private g_iHtcTouchpadEmulationClickMethod As ENUM_HTC_TOUCHPAD_CLICK_METHOD = ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED
        Private g_iHtcGripButtonMethod As ENUM_HTC_GRIP_BUTTON_METHOD = ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_MIRRORED
        Private g_bDisableBaseStationSpawning As Boolean = False
        Private g_bEnableHepticFeedback As Boolean = True

        Public Sub New(_UCVirtualMotionTracker As UCVirtualMotionTracker)
            g_UCVirtualMotionTracker = _UCVirtualMotionTracker
        End Sub

        Property m_JoystickShortcutBinding As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bJoystickShortcutBinding
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bJoystickShortcutBinding = value
                End SyncLock
            End Set
        End Property

        Property m_JoystickShortcutTouchpadClick As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bJoystickShortcutTouchpadClick
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bJoystickShortcutTouchpadClick = value
                End SyncLock
            End Set
        End Property

        Property m_HtcTouchpadEmulationClickMethod As ENUM_HTC_TOUCHPAD_CLICK_METHOD
            Get
                SyncLock _ThreadLock
                    Return g_iHtcTouchpadEmulationClickMethod
                End SyncLock
            End Get
            Set(value As ENUM_HTC_TOUCHPAD_CLICK_METHOD)
                If (value < 0) Then
                    value = 0
                End If

                If (value > ENUM_HTC_TOUCHPAD_CLICK_METHOD.__MAX - 1) Then
                    value = CType(ENUM_HTC_TOUCHPAD_CLICK_METHOD.__MAX - 1, ENUM_HTC_TOUCHPAD_CLICK_METHOD)
                End If

                SyncLock _ThreadLock
                    g_iHtcTouchpadEmulationClickMethod = value
                End SyncLock
            End Set
        End Property

        Property m_HtcGripButtonMethod As ENUM_HTC_GRIP_BUTTON_METHOD
            Get
                SyncLock _ThreadLock
                    Return g_iHtcGripButtonMethod
                End SyncLock
            End Get
            Set(value As ENUM_HTC_GRIP_BUTTON_METHOD)
                If (value < 0) Then
                    value = 0
                End If

                If (value > ENUM_HTC_GRIP_BUTTON_METHOD.__MAX - 1) Then
                    value = CType(ENUM_HTC_GRIP_BUTTON_METHOD.__MAX - 1, ENUM_HTC_GRIP_BUTTON_METHOD)
                End If

                SyncLock _ThreadLock
                    g_iHtcGripButtonMethod = value
                End SyncLock
            End Set
        End Property

        Property m_DisableBaseStationSpawning As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bDisableBaseStationSpawning
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bDisableBaseStationSpawning = value
                End SyncLock
            End Set
        End Property

        Property m_EnableHepticFeedback As Boolean
            Get
                SyncLock _ThreadLock
                    Return g_bEnableHepticFeedback
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock _ThreadLock
                    g_bEnableHepticFeedback = value
                End SyncLock
            End Set
        End Property

        Public Sub LoadSettings()
            g_bSettingsLoaded = True

            Dim tmp As Integer

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    m_JoystickShortcutBinding = (mIni.ReadKeyValue("ControllerSettings", "JoystickShortcutBindings", "false") = "true")
                    m_JoystickShortcutTouchpadClick = (mIni.ReadKeyValue("ControllerSettings", "JoystickShortcutTouchpadClick", "false") = "true")
                    m_DisableBaseStationSpawning = (mIni.ReadKeyValue("ControllerSettings", "DisableBaseStationSpawning", "false") = "true")
                    m_EnableHepticFeedback = (mIni.ReadKeyValue("ControllerSettings", "EnableHepticFeedback", "true") = "true")

                    If (Integer.TryParse(mIni.ReadKeyValue("ControllerSettings", "HtcTouchpadEmulationClickMethod", CStr(CInt(ENUM_HTC_TOUCHPAD_CLICK_METHOD.BUTTON_MIRRORED))), tmp)) Then
                        m_HtcTouchpadEmulationClickMethod = CType(tmp, ENUM_HTC_TOUCHPAD_CLICK_METHOD)
                    End If

                    If (Integer.TryParse(mIni.ReadKeyValue("ControllerSettings", "HtcGripButtonMethod", CStr(CInt(ENUM_HTC_GRIP_BUTTON_METHOD.BUTTON_TOGGLE_MIRRORED))), tmp)) Then
                        m_HtcGripButtonMethod = CType(tmp, ENUM_HTC_GRIP_BUTTON_METHOD)
                    End If
                End Using
            End Using
        End Sub

        Public Sub SaveSettings()
            If (Not g_bSettingsLoaded) Then
                Return
            End If

            Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                    mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "JoystickShortcutBindings", If(m_JoystickShortcutBinding, "true", "false")))
                    mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "JoystickShortcutTouchpadClick", If(m_JoystickShortcutTouchpadClick, "true", "false")))
                    mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "DisableBaseStationSpawning", If(m_DisableBaseStationSpawning, "true", "false")))
                    mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "EnableHepticFeedback", If(m_EnableHepticFeedback, "true", "false")))
                    mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcTouchpadEmulationClickMethod", CStr(CInt(m_HtcTouchpadEmulationClickMethod))))
                    mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("ControllerSettings", "HtcGripButtonMethod", CStr(CInt(m_HtcGripButtonMethod))))

                    mIni.WriteKeyValue(mIniContent.ToArray)
                End Using
            End Using
        End Sub

        Public Sub SetUnsavedState(bIsUnsaved As Boolean)
            If (bIsUnsaved) Then
                g_UCVirtualMotionTracker.Button_SaveControllerSettings.Text = String.Format("Save Settings*")
                g_UCVirtualMotionTracker.Button_SaveControllerSettings.Font = New Font(g_UCVirtualMotionTracker.Button_SaveControllerSettings.Font, FontStyle.Bold)
            Else
                g_UCVirtualMotionTracker.Button_SaveControllerSettings.Text = String.Format("Save Settings")
                g_UCVirtualMotionTracker.Button_SaveControllerSettings.Font = New Font(g_UCVirtualMotionTracker.Button_SaveControllerSettings.Font, FontStyle.Regular)
            End If
        End Sub
    End Class

    Class ClassOscDevices
        Implements IDisposable

        Shared _ThreadLock As New Object
        Private g_UCVirtualMotionTracker As UCVirtualMotionTracker
        Private g_mDevicesThread As Threading.Thread = Nothing

        Event OnDeviceArrived(mDevice As STRUC_DEVICE)
        Event OnDeviceRemoved(mDevice As STRUC_DEVICE)
        Event OnDevicePose(mDevice As STRUC_DEVICE)

        Structure STRUC_DEVICE
            Enum ENUM_DEVICE_TYPE
                INVALID
                HMD
                CONTROLLER
                TRACKER
                REFERENCE
                REDIRECT
            End Enum

            Public Sub New(_Index As Integer, _Type As ENUM_DEVICE_TYPE, _Serial As String)
                iIndex = _Index
                iType = _Type
                sSerial = _Serial
                mPos = New Vector3()
                mOrientation = Quaternion.Identity
            End Sub

            Public Sub New(_Index As Integer, _Type As ENUM_DEVICE_TYPE, _Serial As String, _Pos As Vector3, _Orientation As Quaternion)
                iIndex = _Index
                iType = _Type
                sSerial = _Serial
                mPos = _Pos
                mOrientation = _Orientation
            End Sub

            Dim iIndex As Integer
            Dim iType As ENUM_DEVICE_TYPE
            Dim sSerial As String
            Dim mPos As Vector3
            Dim mOrientation As Quaternion

            Public Function GetPosCm() As Vector3
                Return New Vector3(
                    mPos.X * CSng(PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSM_METERS_TO_CENTIMETERS),
                    mPos.Y * CSng(PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSM_METERS_TO_CENTIMETERS),
                    mPos.Z * CSng(PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants.PSM_METERS_TO_CENTIMETERS))
            End Function

            Public Function GetOrientationEuler() As Vector3
                Return ClassQuaternionTools.FromQ2(mOrientation)
            End Function

            Dim mLastPoseTimestamp As Date
        End Structure

        Private g_mDevicesDic As New Dictionary(Of String, STRUC_DEVICE)

        Public Sub New(mUCVirtualMotionTracker As UCVirtualMotionTracker)
            g_UCVirtualMotionTracker = mUCVirtualMotionTracker

            AddHandler g_UCVirtualMotionTracker.g_ClassOscServer.OnOscProcessMessage, AddressOf OnOscMessage
        End Sub

        Public Sub StartThread()
            If (g_mDevicesThread IsNot Nothing AndAlso g_mDevicesThread.IsAlive) Then
                Return
            End If

            g_mDevicesThread = New Threading.Thread(AddressOf DevicesThread)
            g_mDevicesThread.IsBackground = True
            g_mDevicesThread.Start()
        End Sub

        Private Sub DevicesThread()
            Dim mLastDeviceList As Date = Now

            While True
                Try
                    If (Not g_UCVirtualMotionTracker.g_ClassOscServer.IsRunning OrElse g_UCVirtualMotionTracker.g_ClassOscServer.m_SuspendRequests) Then
                        Threading.Thread.Sleep(1000)
                        Continue While
                    End If

                    ' Request Device List
                    Dim mLastDeviceListTimespan = Now - mLastDeviceList

                    If (mLastDeviceListTimespan.TotalMilliseconds > 1000) Then
                        mLastDeviceList = Now

                        g_UCVirtualMotionTracker.g_ClassOscServer.Send(New OscMessage("/VMT/GetDevicesList"))
                    End If

                    ' Request Device Pose 
                    SyncLock _ThreadLock
                        For Each mDevice In g_mDevicesDic
                            g_UCVirtualMotionTracker.g_ClassOscServer.Send(New OscMessage("/VMT/GetDevicePose", New Object() {mDevice.Value.sSerial}))
                        Next
                    End SyncLock
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception

                End Try

                ClassPrecisionSleep.Sleep(1)
            End While
        End Sub

        Private Sub OnOscMessage(mMessage As OscMessage)
            Try
                Select Case (mMessage.Address)
                    Case "/VMT/Out/DevicesList"
                        If (mMessage.Count < 1) Then
                            Return
                        End If

                        Dim sDevices As String = CStr(mMessage(0))

                        Dim mDeviceList As New List(Of STRUC_DEVICE)

                        For Each sDevice As String In sDevices.Split(New String() {vbCrLf, vbLf}, 0)
                            Dim sInfos As String() = sDevice.Split(":"c)
                            If (sInfos.Length < 3) Then
                                Continue For
                            End If

                            Dim iIndex As Integer = -1
                            Dim iType As Integer = -1
                            Dim sSerial As String = ""

                            For i = 0 To sInfos.Length - 1
                                Select Case (i)
                                    Case 0
                                        iIndex = CInt(sInfos(i))
                                    Case 1
                                        iType = CInt(sInfos(i))
                                    Case Else
                                        sSerial &= sInfos(i)
                                End Select
                            Next

                            mDeviceList.Add(New STRUC_DEVICE(iIndex, CType(iType, STRUC_DEVICE.ENUM_DEVICE_TYPE), sSerial))
                        Next


                        SyncLock _ThreadLock
                            ' Add missing devices
                            For Each mDevice As STRUC_DEVICE In mDeviceList
                                If (Not g_mDevicesDic.ContainsKey(mDevice.sSerial)) Then
                                    g_mDevicesDic(mDevice.sSerial) = mDevice

                                    RaiseEvent OnDeviceArrived(mDevice)
                                End If
                            Next

                            ' Remove devices that do not exist anymore
                            For Each mDevice In g_mDevicesDic
                                If (mDeviceList.Exists(Function(x As STRUC_DEVICE) x.sSerial = mDevice.Value.sSerial)) Then
                                    Continue For
                                End If

                                RaiseEvent OnDeviceRemoved(mDevice.Value)

                                g_mDevicesDic.Remove(mDevice.Value.sSerial)
                            Next
                        End SyncLock

                    Case "/VMT/Out/DevicePose"
                        If (mMessage.Count < 8) Then
                            Return
                        End If

                        Dim sSerial As String = CStr(mMessage(0))
                        Dim iPosX As Single = CSng(mMessage(1))
                        Dim iPosY As Single = CSng(mMessage(2))
                        Dim iPosZ As Single = CSng(mMessage(3))
                        Dim iQuatX As Single = CSng(mMessage(4))
                        Dim iQuatY As Single = CSng(mMessage(5))
                        Dim iQuatZ As Single = CSng(mMessage(6))
                        Dim iQuatW As Single = CSng(mMessage(7))

                        SyncLock _ThreadLock
                            Dim mDevice As STRUC_DEVICE = Nothing

                            If (g_mDevicesDic.TryGetValue(sSerial, mDevice)) Then
                                mDevice.mPos = New Vector3(iPosX, iPosY, iPosZ)
                                mDevice.mOrientation = New Quaternion(iQuatX, iQuatY, iQuatZ, iQuatW)

                                mDevice.mLastPoseTimestamp = Now

                                g_mDevicesDic(sSerial) = mDevice

                                RaiseEvent OnDevicePose(mDevice)
                            End If
                        End SyncLock
                End Select
            Catch ex As Exception
                ' Something sussy is going on...
            End Try
        End Sub

        Public Function GetDevices() As STRUC_DEVICE()
            SyncLock _ThreadLock
                Dim mDeviceList As New List(Of STRUC_DEVICE)

                For Each mDevice In g_mDevicesDic
                    mDeviceList.Add(mDevice.Value)
                Next

                Return mDeviceList.ToArray
            End SyncLock
        End Function

        Public Function GetDeviceBySerial(sSerial As String, ByRef mDevice As STRUC_DEVICE) As Boolean
            SyncLock _ThreadLock
                If (g_mDevicesDic.TryGetValue(sSerial, mDevice)) Then
                    Return True
                End If

                Return False
            End SyncLock
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    If (g_UCVirtualMotionTracker IsNot Nothing AndAlso g_UCVirtualMotionTracker.g_ClassOscServer IsNot Nothing) Then
                        RemoveHandler g_UCVirtualMotionTracker.g_ClassOscServer.OnOscProcessMessage, AddressOf OnOscMessage
                    End If

                    If (g_mDevicesThread IsNot Nothing AndAlso g_mDevicesThread.IsAlive) Then
                        g_mDevicesThread.Abort()
                        g_mDevicesThread.Join()
                        g_mDevicesThread = Nothing
                    End If
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

End Class
