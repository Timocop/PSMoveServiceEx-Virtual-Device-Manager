﻿Public Class UCVirtualTrackers
    Public g_mFormMain As FormMain

    Private g_bIgnoreEvents As Boolean = False
    Private g_bInit As Boolean = False

    Public Sub New(_mFormMain As FormMain)
        g_mFormMain = _mFormMain

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

        CreateControl()
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

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
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

        Try
            g_bIgnoreEvents = True

            LoadSettings()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Class ClassVideoInputDevicesListViewItem
        Inherits ListViewItem
        Implements IDisposable

        Private g_UCVirtualTrackerItem As UCVirtualTrackerItem
        Private g_UCVirtualTrackers As UCVirtualTrackers
        Private g_mClassDeviceInfo As ClassVideoInputDevices.ClassDeviceInfo

        Public Sub New(_UCVirtualMotionTracker As UCVirtualTrackers, _DeviceInfo As ClassVideoInputDevices.ClassDeviceInfo)
            MyBase.New(New String() {"", "", "", ""})

            Dim sName As String = _DeviceInfo.m_Name
            For Each mDevice In ClassLibusbDriver.DRV_PS4CAM_KNOWN_CONFIGS
                Dim sHardwareId As String = String.Format("\USB#VID_{0}&PID_{1}", mDevice.VID, mDevice.PID)

                If (_DeviceInfo.m_Path.ToLowerInvariant.Contains(sHardwareId.ToLowerInvariant)) Then
                    sName = mDevice.sName
                    Exit For
                End If
            Next

            g_UCVirtualTrackers = _UCVirtualMotionTracker
            g_mClassDeviceInfo = New ClassVideoInputDevices.ClassDeviceInfo(
                _DeviceInfo.m_Index,
                sName,
                _DeviceInfo.m_Path,
                _DeviceInfo.m_CLSID)
            g_UCVirtualTrackerItem = New UCVirtualTrackerItem(g_UCVirtualTrackers, g_mClassDeviceInfo)
            g_UCVirtualTrackerItem.Init()

            UpdateItem(New ClassVideoInputDevices.ClassDeviceInfo(
                _DeviceInfo.m_Index,
                sName,
                _DeviceInfo.m_Path,
                _DeviceInfo.m_CLSID))
        End Sub

        Public Sub UpdateItem()
            UpdateItem(Nothing)
        End Sub

        Public Sub UpdateItem(mDeviceInfo As ClassVideoInputDevices.ClassDeviceInfo)
            Const LISTVIEW_SUBITEM_ID As Integer = 0
            Const LISTVIEW_SUBITEM_TRACKERID As Integer = 1
            Const LISTVIEW_SUBITEM_NAME As Integer = 2
            Const LISTVIEW_SUBITEM_PATH As Integer = 3

            'Is there any error?
            If (g_UCVirtualTrackerItem Is Nothing OrElse g_UCVirtualTrackerItem.IsDisposed OrElse g_UCVirtualTrackerItem.m_HasStatusError) Then
                Me.BackColor = Color.FromArgb(255, 192, 192)
            Else
                Me.BackColor = Color.FromArgb(255, 255, 255)
            End If

            If (g_UCVirtualTrackerItem Is Nothing OrElse g_UCVirtualTrackerItem.IsDisposed) Then
                Return
            End If

            Dim iIndex As Integer = -1
            Dim sName As String = ""
            Dim sPath As String = ""
            Dim sCLSID As String = ""

            If (mDeviceInfo Is Nothing) Then
                iIndex = g_mClassDeviceInfo.m_Index
                sName = g_mClassDeviceInfo.m_Name
                sPath = g_mClassDeviceInfo.m_Path.ToUpperInvariant
                sCLSID = g_mClassDeviceInfo.m_CLSID
            Else
                iIndex = mDeviceInfo.m_Index
                sName = mDeviceInfo.m_Name
                sPath = mDeviceInfo.m_Path.ToUpperInvariant
                sCLSID = mDeviceInfo.m_CLSID
            End If

            Me.SubItems(LISTVIEW_SUBITEM_ID).Text = CStr(iIndex)

            If (g_UCVirtualTrackerItem.g_mClassCaptureLogic IsNot Nothing AndAlso g_UCVirtualTrackerItem.g_mClassCaptureLogic.m_Initialized) Then
                If (g_UCVirtualTrackerItem.g_mClassCaptureLogic.m_IsPlayStationCamera AndAlso g_UCVirtualTrackerItem.g_mClassCaptureLogic.m_PipePrimaryIndex > -1) Then
                    Me.SubItems(LISTVIEW_SUBITEM_TRACKERID).Text = String.Format("{0} & {1}",
                                                                             g_UCVirtualTrackerItem.g_mClassCaptureLogic.m_PipePrimaryIndex,
                                                                             g_UCVirtualTrackerItem.g_mClassCaptureLogic.m_PipeSecondaryIndex)
                Else
                    Me.SubItems(LISTVIEW_SUBITEM_TRACKERID).Text = CStr(g_UCVirtualTrackerItem.g_mClassCaptureLogic.m_PipePrimaryIndex)
                End If
            Else
                Me.SubItems(LISTVIEW_SUBITEM_TRACKERID).Text = "-1"
            End If

            Me.SubItems(LISTVIEW_SUBITEM_NAME).Text = sName
            Me.SubItems(LISTVIEW_SUBITEM_PATH).Text = sPath
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
                    g_UCVirtualTrackerItem.Visible = True
                Else
                    g_UCVirtualTrackerItem.Parent = Nothing
                    g_UCVirtualTrackerItem.Visible = False
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

            g_mFormMain.PromptRestartPSMoveService()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
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

            ClassMathUtils.SetComboBoxSelectedIndexClamp(ComboBox_VirtualTrackerCount, mConfig.GetValue(Of Integer)("", "virtual_tracker_count", 0))
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub Button_DeviceAdd_Click(sender As Object, e As EventArgs) Handles Button_DeviceAdd.Click
        Try
            Using mForm As New FormVideoInputDeviceSelection()
                If (mForm.ShowDialog(Me) <> DialogResult.OK) Then
                    Return
                End If

                If (Not mForm.m_DialogResult.bIsValid) Then
                    Return
                End If

                AddNewDevice(mForm.m_DialogResult.mVideoInputDeviceItem)
            End Using
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
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

            If (mAttachmentItem.m_UCVirtualMotionTrackerItem.g_mClassCaptureLogic IsNot Nothing AndAlso
                    Not mAttachmentItem.m_UCVirtualMotionTrackerItem.g_mClassCaptureLogic.m_Initialized) Then
                Throw New ArgumentException("Please wait until video input device is initialized.")
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
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Public Sub AddNewDevice(mDeviceInfo As ClassVideoInputDevices.ClassDeviceInfo)
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
        Timer_VideoInputDevices.Stop()

        Try
            If (Me.Visible) Then
                For Each mItem As ListViewItem In ListView_VideoDevices.Items
                    Dim mTrackerItem = DirectCast(mItem, ClassVideoInputDevicesListViewItem)
                    mTrackerItem.UpdateItem()
                Next
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try

        Timer_VideoInputDevices.Start()
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
        Try
            If (ListView_VideoDevices.SelectedItems.Count < 1) Then
                Return
            End If

            Dim mAttachmentItem = DirectCast(ListView_VideoDevices.SelectedItems(0), ClassVideoInputDevicesListViewItem)

            If (mAttachmentItem.m_UCVirtualMotionTrackerItem.g_mClassCaptureLogic IsNot Nothing AndAlso
                    Not mAttachmentItem.m_UCVirtualMotionTrackerItem.g_mClassCaptureLogic.m_Initialized) Then
                Throw New ArgumentException("Please wait until video input device is initialized.")
            End If

            ListView_VideoDevices.Items.Remove(mAttachmentItem)
            mAttachmentItem.Dispose()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub CleanUp()
        For Each mItem As ListViewItem In ListView_VideoDevices.Items
            Dim mTrackerItem = DirectCast(mItem, ClassVideoInputDevicesListViewItem)

            mTrackerItem.Dispose()
        Next
        ListView_VideoDevices.Items.Clear()
    End Sub
End Class
