Public Class UCVirtualTrackers
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
            For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_TRACKER_COUNT
                ComboBox_VirtualTrackerCount.Items.Add(CStr(i))
            Next
        Finally
            g_bIgnoreEvents = False
        End Try

        Try
            ' Load all devices that autostart enabled.
            Dim mDeviceList As New List(Of ClassVideoInputDevices.ClassDeviceInfo)
            If (ClassVideoInputDevices.GetDevicesOfVideoInput(mDeviceList)) Then
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
            Dim mConfig As New ClassServiceConfig(GetConfig())
            mConfig.LoadConfig()
            mConfig.SetValue("", "virtual_tracker_count", ComboBox_VirtualTrackerCount.SelectedIndex)
            mConfig.SaveConfig()

            If (Process.GetProcessesByName("PSMoveService").Count > 0) Then
                MessageBox.Show("Restart PSMoveServiceEx to take effect!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetConfig() As String
        Dim sConfigPath As String = ClassServiceConfig.GetConfigPath()
        If (sConfigPath Is Nothing) Then
            Return Nothing
        End If

        Return IO.Path.Combine(sConfigPath, "TrackerManagerConfig.json")
    End Function

    Private Sub LoadSettings()
        Try
            g_bIgnoreEvents = True

            Dim mConfig As New ClassServiceConfig(GetConfig())
            mConfig.LoadConfig()

            SetComboBoxValueClamp(ComboBox_VirtualTrackerCount, mConfig.GetValue(Of Integer)("", "virtual_tracker_count", 0))
        Finally
            g_bIgnoreEvents = False
        End Try
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
            Dim mDeviceList As New List(Of ClassVideoInputDevices.ClassDeviceInfo)
            If (ClassVideoInputDevices.GetDevicesOfVideoInput(mDeviceList)) Then
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

    Public Sub AddNewDevice(mDeviceInfo As ClassVideoInputDevices.ClassDeviceInfo)
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
        Inherits ClassVideoInputDevices.ClassDeviceInfo

        Public Sub New(mDeviceItem As ClassVideoInputDevices.ClassDeviceInfo)
            MyBase.New(mDeviceItem.m_Index, mDeviceItem.m_Name, mDeviceItem.m_Path, mDeviceItem.m_CLSID)
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("ID: {0}, Name: {1}", m_Index, m_Name)
        End Function
    End Class
End Class
