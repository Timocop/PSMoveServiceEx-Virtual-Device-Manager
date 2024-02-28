Public Class UCVirtualTrackers
    Private g_bIgnoreEvents As Boolean = False

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 

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

    Class ClassVideoInputDevicesListViewItem
        Inherits ListViewItem
        Implements IDisposable

        Private g_UCVirtualTrackerItem As UCVirtualTrackerItem
        Private g_UCVirtualTrackers As UCVirtualTrackers
        Private g_mClassDeviceInfo As ClassVideoInputDevices.ClassDeviceInfo

        Public Sub New(_UCVirtualMotionTracker As UCVirtualTrackers, _DeviceInfo As ClassVideoInputDevices.ClassDeviceInfo)
            MyBase.New(New String() {"", "", "", ""})

            g_UCVirtualTrackers = _UCVirtualMotionTracker
            g_mClassDeviceInfo = New ClassVideoInputDevices.ClassDeviceInfo(
                _DeviceInfo.m_Index,
                _DeviceInfo.m_Name,
                _DeviceInfo.m_Path,
                _DeviceInfo.m_CLSID)
            g_UCVirtualTrackerItem = New UCVirtualTrackerItem(_UCVirtualMotionTracker, _DeviceInfo)

            UpdateItem()
        End Sub

        Public Sub UpdateItem()
            'Is there any error?
            If (g_UCVirtualTrackerItem Is Nothing OrElse g_UCVirtualTrackerItem.IsDisposed) Then
                Me.BackColor = Color.FromArgb(255, 192, 192)
            Else
                Me.BackColor = Color.FromArgb(255, 255, 255)
            End If

            If (g_UCVirtualTrackerItem Is Nothing OrElse g_UCVirtualTrackerItem.IsDisposed) Then
                Return
            End If

            Me.SubItems(0).Text = CStr(g_mClassDeviceInfo.m_Index)
            Me.SubItems(1).Text = CStr(g_mClassDeviceInfo.m_Name)
            Me.SubItems(2).Text = CStr(g_mClassDeviceInfo.m_Path.ToUpperInvariant)
        End Sub

        ReadOnly Property m_UCVirtualMotionTrackerItem As UCVirtualTrackerItem
            Get
                Return g_UCVirtualTrackerItem
            End Get
        End Property

        ReadOnly Property m_ClassDeviceInfo As ClassVideoInputDevices.ClassDeviceInfo
            Get
                Return g_mClassDeviceInfo
            End Get
        End Property

        Property m_Visible As Boolean
            Get
                If (g_UCVirtualTrackerItem Is Nothing OrElse g_UCVirtualTrackerItem.IsDisposed) Then
                    Return False
                End If

                Return (g_UCVirtualTrackerItem.Parent Is g_UCVirtualTrackers.Panel_Devices)
            End Get
            Set(value As Boolean)
                If (value) Then
                    If (g_UCVirtualTrackerItem Is Nothing OrElse g_UCVirtualTrackerItem.IsDisposed) Then
                        Return
                    End If

                    g_UCVirtualTrackerItem.Parent = g_UCVirtualTrackers.Panel_Devices
                    g_UCVirtualTrackerItem.Dock = DockStyle.Top
                Else
                    g_UCVirtualTrackerItem.Parent = Nothing
                End If
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    If (g_UCVirtualTrackerItem IsNot Nothing AndAlso Not g_UCVirtualTrackerItem.IsDisposed) Then
                        g_UCVirtualTrackerItem.Dispose()
                        g_UCVirtualTrackerItem = Nothing
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

    Private Sub ToolStripMenuItem_VideoReconnect_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_VideoReconnect.Click
        Try
            If (ListView_VideoDevices.SelectedItems.Count < 1) Then
                Return
            End If

            Dim mAttachmentItem = DirectCast(ListView_VideoDevices.SelectedItems(0), ClassVideoInputDevicesListViewItem)
            If (mAttachmentItem Is Nothing) Then
                Return
            End If

            mAttachmentItem.m_UCVirtualMotionTrackerItem.Dispose()

            ' Copy and get the new index. Should fail if -1
            Dim mNewDeviceInfo As New ClassVideoInputDevices.ClassDeviceInfo(
                mAttachmentItem.m_ClassDeviceInfo.GetIndexByPath(),
                mAttachmentItem.m_ClassDeviceInfo.m_Name,
                mAttachmentItem.m_ClassDeviceInfo.m_Path,
                mAttachmentItem.m_ClassDeviceInfo.m_CLSID)

            If (mNewDeviceInfo.m_Index < 0) Then
                Throw New ArgumentException("Could not find video input device.")
            End If

            AddNewDevice(mNewDeviceInfo)
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

        ' Remove disposed devices
        For i = ListView_VideoDevices.Items.Count - 1 To 0 Step -1
            Dim mTrackerItem = DirectCast(ListView_VideoDevices.Items(i), ClassVideoInputDevicesListViewItem)
            If (mTrackerItem.m_ClassDeviceInfo.m_Path <> mDeviceInfo.m_Path) Then
                Continue For
            End If

            If (mTrackerItem.m_UCVirtualMotionTrackerItem Is Nothing OrElse mTrackerItem.m_UCVirtualMotionTrackerItem.IsDisposed) Then
                ListView_VideoDevices.Items.RemoveAt(i)
            End If
        Next

        Dim mItem = New ClassVideoInputDevicesListViewItem(Me, mDeviceInfo)
        ListView_VideoDevices.Items.Add(mItem)
        mItem.Selected = True
    End Sub

    Public Function GetAllDevices() As UCVirtualTrackerItem()
        Dim mTrackerList As New List(Of UCVirtualTrackerItem)

        For Each mItem As ListViewItem In ListView_VideoDevices.Items
            Dim mTrackerItem = DirectCast(mItem, ClassVideoInputDevicesListViewItem)
            If (mTrackerItem.m_UCVirtualMotionTrackerItem Is Nothing OrElse mTrackerItem.m_UCVirtualMotionTrackerItem.IsDisposed) Then
                Continue For
            End If

            mTrackerList.Add(mTrackerItem.m_UCVirtualMotionTrackerItem)
        Next

        Return mTrackerList.ToArray
    End Function

    Private Sub Timer_VideoInputDevices_Tick(sender As Object, e As EventArgs) Handles Timer_VideoInputDevices.Tick
        Try
            Timer_VideoInputDevices.Stop()

            For Each mItem As ListViewItem In ListView_VideoDevices.Items
                Dim mTrackerItem = DirectCast(mItem, ClassVideoInputDevicesListViewItem)
                mTrackerItem.UpdateItem()
            Next
        Catch ex As Exception
        Finally
            Timer_VideoInputDevices.Start()
        End Try
    End Sub

    Private Sub ListView_VideoDevices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_VideoDevices.SelectedIndexChanged
        For Each mItem As ListViewItem In ListView_VideoDevices.Items
            Dim mTrackerItem = DirectCast(mItem, ClassVideoInputDevicesListViewItem)
            If (mTrackerItem.Selected) Then
                If (Not mTrackerItem.m_Visible) Then
                    mTrackerItem.m_Visible = True
                End If
            Else
                If (mTrackerItem.m_Visible) Then
                    mTrackerItem.m_Visible = False
                End If
            End If
        Next
    End Sub

    Private Sub ToolStripMenuItem_VideoRemove_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_VideoRemove.Click
        If (ListView_VideoDevices.SelectedItems.Count < 1) Then
            Return
        End If

        Dim mAttachmentItem = DirectCast(ListView_VideoDevices.SelectedItems(0), ClassVideoInputDevicesListViewItem)
        ListView_VideoDevices.Items.Remove(mAttachmentItem)

        mAttachmentItem.Dispose()
    End Sub

    Class ClassComboBoxDeviceInfoItem
        Inherits ClassVideoInputDevices.ClassDeviceInfo

        Public Sub New(mDeviceItem As ClassVideoInputDevices.ClassDeviceInfo)
            MyBase.New(mDeviceItem.m_Index, mDeviceItem.m_Name, mDeviceItem.m_Path, mDeviceItem.m_CLSID)
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("ID: {0}, Name: {1}", m_Index, m_Name)
        End Function
    End Class

    Private Sub CleanUp()
        For Each mItem As ListViewItem In ListView_VideoDevices.Items
            Dim mTrackerItem = DirectCast(mItem, ClassVideoInputDevicesListViewItem)

            mTrackerItem.Dispose()
        Next
        ListView_VideoDevices.Items.Clear()
    End Sub
End Class
