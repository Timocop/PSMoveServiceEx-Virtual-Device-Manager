Public Class UCVirtualTrackers
    Private g_mTrackerSettings As New Dictionary(Of String, ClassServiceConfig.ClassSettingsKey)

    Private g_bIgnoreEvents As Boolean = False

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        Me.AutoSize = True
        Me.AutoSizeMode = AutoSizeMode.GrowOnly

        Panel_Devices.AutoSize = True
        Panel_Devices.AutoSizeMode = AutoSizeMode.GrowOnly

        Try
            g_bIgnoreEvents = True

            ComboBox_VirtualTrackerCount.Items.Clear()
            For i = 0 To ClassPSMoveSerivceConst.PSMOVESERVICE_MAX_TRACKER_COUNT
                ComboBox_VirtualTrackerCount.Items.Add(CStr(i))
            Next
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            ' Load all devices that autostart enabled.
            Dim mDeviceList As New List(Of ClassDevices.ClassDeviceInfo)
            If (ClassDevices.GetDevicesOfVideoInput(mDeviceList)) Then
                For i = 0 To mDeviceList.Count - 1
                    Dim bAutostart As Boolean = UCVirtualTrackerItem.ClassCaptureLogic.ClassConfig.CanDeviceAutostart(mDeviceList(i).m_Path)
                    If (Not bAutostart) Then
                        Continue For
                    End If

                    AddNewDevice(mDeviceList(i))
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub ComboBox_VirtualTrackerCount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_VirtualTrackerCount.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Try
            SettingsChanged(sender)
            SaveSettings()

            MessageBox.Show("Restart PSMoveService to take effect!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadSettings()
        Try
            g_bIgnoreEvents = True

            Dim sPsmsPath As String = Environment.ExpandEnvironmentVariables("%AppData%\PSMoveService")
            If (Not IO.Directory.Exists(sPsmsPath)) Then
                Throw New ArgumentException("PSMoveService configs not found. Please setup PSMoveService first.")
            End If

            Dim sTrackerSettings As String = IO.Path.Combine(sPsmsPath, "TrackerManagerConfig.json")

            g_mTrackerSettings.Clear()
            g_mTrackerSettings(ComboBox_VirtualTrackerCount.Name) = New ClassServiceConfig.ClassSettingsKey(sTrackerSettings, "virtual_tracker_count", ClassServiceConfig.ClassSettingsKey.ENUM_TYPE.NUM)

            For Each sKey As String In g_mTrackerSettings.Keys
                Try
                    g_mTrackerSettings(sKey).Load()
                Catch ex As Exception
                    Throw New ArgumentException(String.Format("Unable to load key '{0}'", g_mTrackerSettings(sKey).m_SettingsKey))
                End Try
            Next

            SetComboBoxValueClamp(ComboBox_VirtualTrackerCount, CInt(g_mTrackerSettings(ComboBox_VirtualTrackerCount.Name).m_ValueF))
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub SaveSettings()
        For Each sKey As String In g_mTrackerSettings.Keys
            Try
                Dim mSettingsKey = g_mTrackerSettings(sKey)
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
            If (g_mTrackerSettings.ContainsKey(mNumericUpDown.Name)) Then
                g_mTrackerSettings(mNumericUpDown.Name).m_ValueF = mNumericUpDown.Value
            End If

            Return
        End If

        Dim mCheckBox = TryCast(sender, CheckBox)
        If (mCheckBox IsNot Nothing) Then
            If (g_mTrackerSettings.ContainsKey(mCheckBox.Name)) Then
                g_mTrackerSettings(mCheckBox.Name).m_ValueB = mCheckBox.Checked
            End If

            Return
        End If

        Dim mComboBox = TryCast(sender, ComboBox)
        If (mComboBox IsNot Nothing) Then
            If (g_mTrackerSettings.ContainsKey(mComboBox.Name)) Then
                g_mTrackerSettings(mComboBox.Name).m_ValueF = mComboBox.SelectedIndex
            End If

            Return
        End If
    End Sub

    Private Sub SetNumericUpDownValueClamp(mControl As NumericUpDown, iValue As Decimal)
        mControl.Value = Math.Max(mControl.Minimum, Math.Min(mControl.Maximum, iValue))
    End Sub

    Private Sub SetComboBoxValueClamp(mControl As ComboBox, iValue As Integer)
        If (mControl.Items.Count = 0) Then
            Return
        End If

        mControl.SelectedIndex = Math.Max(0, Math.Min(mControl.Items.Count - 1, iValue))
    End Sub

    Private Sub ComboBox_Devices_DropDown(sender As Object, e As EventArgs) Handles ComboBox_Devices.DropDown
        Try
            Dim iPrevIndex As Integer = ComboBox_Devices.SelectedIndex

            ComboBox_Devices.Items.Clear()

            ' Fill the list with video input devices.
            Dim mDeviceList As New List(Of ClassDevices.ClassDeviceInfo)
            If (ClassDevices.GetDevicesOfVideoInput(mDeviceList)) Then
                For i = 0 To mDeviceList.Count - 1
                    ComboBox_Devices.Items.Add(New ClassComboBoxDeviceInfoItem(mDeviceList(i)))
                Next
            End If

            ' Nothing found? We better not set the index otherwise we error.
            If (ComboBox_Devices.Items.Count > 0) Then
                ComboBox_Devices.SelectedIndex = Math.Max(0, Math.Min(ComboBox_Devices.Items.Count - 1, iPrevIndex))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button_DeviceAdd_Click(sender As Object, e As EventArgs) Handles Button_DeviceAdd.Click
        Try
            Dim mSelectedItem = TryCast(ComboBox_Devices.SelectedItem, ClassComboBoxDeviceInfoItem)
            If (mSelectedItem Is Nothing) Then
                Return
            End If

            AddNewDevice(mSelectedItem)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Unable to add to list", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AddNewDevice(mDeviceInfo As ClassDevices.ClassDeviceInfo)
        Dim mUCVirtualTrackerItem As UCVirtualTrackerItem

        For Each mUCVirtualTrackerItem In GetAllDevices()
            If (mUCVirtualTrackerItem.m_DevicePath = mDeviceInfo.m_Path) Then
                Throw New ArgumentException("Device already in the list")
            End If
        Next

        mUCVirtualTrackerItem = New UCVirtualTrackerItem(Me, mDeviceInfo)
        mUCVirtualTrackerItem.Parent = Panel_Devices
        mUCVirtualTrackerItem.Dock = DockStyle.Top
    End Sub

    Public Function GetAllDevices() As UCVirtualTrackerItem()
        Dim mUCVirtualTrackers As New List(Of UCVirtualTrackerItem)

        For Each mControl As Control In Panel_Devices.Controls
            Dim mUCVirtualTrackerItem = TryCast(mControl, UCVirtualTrackerItem)
            If (mUCVirtualTrackerItem Is Nothing) Then
                Continue For
            End If

            mUCVirtualTrackers.Add(mUCVirtualTrackerItem)
        Next

        Return mUCVirtualTrackers.ToArray
    End Function

    Class ClassComboBoxDeviceInfoItem
        Inherits ClassDevices.ClassDeviceInfo

        Public Sub New(mDeviceItem As ClassDevices.ClassDeviceInfo)
            MyBase.New(mDeviceItem.m_Index, mDeviceItem.m_Name, mDeviceItem.m_Path, mDeviceItem.m_CLSID)
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("ID: {0}, Name: {1}", m_Index, m_Name)
        End Function
    End Class
End Class
