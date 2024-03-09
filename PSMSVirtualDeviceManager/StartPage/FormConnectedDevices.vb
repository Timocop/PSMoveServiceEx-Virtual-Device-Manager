Public Class FormConnectedDevices
    Private g_ClassTreeViewManager As ClassTreeViewManager

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ImageList_Devices.Images.Clear()
        ImageList_Devices.Images.Add("Normal", My.Resources.Status_WHITE_16)
        ImageList_Devices.Images.Add("Removed", My.Resources.Status_RED_16)
        ImageList_Devices.Images.Add("Enabled", My.Resources.Status_GREEN_16)
        ImageList_Devices.Images.Add("AddDevice", My.Resources.DevicePairing_6101_16x16_32)
        ImageList_Devices.Images.Add("Error", My.Resources.user32_101_16x16_32)
        ImageList_Devices.Images.Add("USB", My.Resources.imageres_34_16x16_32)
        ImageList_Devices.Images.Add("Bluetooth", My.Resources.bthci_201_16x16_32)

        g_ClassTreeViewManager = New ClassTreeViewManager(TreeView_ConnectedDevices)

    End Sub

    Class STRUC_LISTVIEW_NODE_DEVICE_ITEM
        Inherits TreeNode

        Private g_bReadOnly As Boolean = False
        Private g_sDeviceName As String = ""
        Private g_mDevice As ClassLibusbDriver.STRUC_DEVICETREE_ITEM = Nothing

        Public Sub New(_Device As ClassLibusbDriver.STRUC_DEVICETREE_ITEM, _ReadOnly As Boolean)
            Me.New("", _Device, _ReadOnly)
        End Sub

        Public Sub New(_DeviceName As String, _Device As ClassLibusbDriver.STRUC_DEVICETREE_ITEM, _ReadOnly As Boolean)
            g_sDeviceName = _DeviceName
            g_mDevice = New ClassLibusbDriver.STRUC_DEVICETREE_ITEM(_Device)
            g_bReadOnly = _ReadOnly

            Me.Text = Me.ToString
        End Sub

        Public Overrides Function ToString() As String
            If (String.IsNullOrEmpty(g_mDevice.mProvider.sProviderDescription) OrElse g_mDevice.mProvider.sProviderDescription.TrimEnd.Length = 0) Then
                If (String.IsNullOrEmpty(g_sDeviceName) OrElse g_sDeviceName.TrimEnd.Length = 0) Then
                    Return "Unknown"
                Else
                    Return String.Format("{0} ({1})", "Unknown", g_sDeviceName)
                End If
            End If

            If (String.IsNullOrEmpty(g_sDeviceName) OrElse g_sDeviceName.TrimEnd.Length = 0) Then
                Return g_mDevice.mProvider.sProviderDescription
            Else
                Return String.Format("{0} ({1})", g_mDevice.mProvider.sProviderDescription, g_sDeviceName)
            End If
        End Function

        Public Property m_DeviceName As String
            Get
                Return g_sDeviceName
            End Get
            Set(value As String)
                g_sDeviceName = value

                Me.Text = Me.ToString
            End Set
        End Property

        Public Property m_IsReadOnly As Boolean
            Get
                Return g_bReadOnly
            End Get
            Set(value As Boolean)
                g_bReadOnly = value
            End Set
        End Property

        Public ReadOnly Property m_Device As ClassLibusbDriver.STRUC_DEVICETREE_ITEM
            Get
                Return g_mDevice
            End Get
        End Property
    End Class

    Class ClassTreeViewManager
        Private g_mTreeView As TreeView
        Private g_mTreeViewNodes As New Dictionary(Of String, STRUC_LISTVIEW_NODE_DEVICE_ITEM)

        Public Sub New(_TreeView As TreeView)
            g_mTreeView = _TreeView
        End Sub

        ReadOnly Property m_TreeView As TreeView
            Get
                Return g_mTreeView
            End Get
        End Property

        Public Sub AddDevice(_DriverInfo As ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO, _Device As ClassLibusbDriver.STRUC_DEVICETREE_ITEM, bIncludeDisconnected As Boolean)
            Dim mLibusbDriver As New ClassLibusbDriver

            Dim bConnected As Boolean = mLibusbDriver.IsUsbDeviceConnected(_DriverInfo.VID, _DriverInfo.PID, _Device.sInterface, _Device.sDeviceSerial)

            If (Not bIncludeDisconnected) Then
                If (Not bConnected) Then
                    Return
                End If
            End If

            Dim mLastTreeNodeCollection As TreeNodeCollection = g_mTreeView.Nodes

            Dim mTotalDevices As ClassLibusbDriver.STRUC_DEVICETREE_ITEM() = Nothing

            If (True) Then
                Dim mLastDevice = _Device
                Dim mLastTotalDevice As New List(Of ClassLibusbDriver.STRUC_DEVICETREE_ITEM)

                While True
                    mLastTotalDevice.Add(New ClassLibusbDriver.STRUC_DEVICETREE_ITEM(mLastDevice))

                    If (mLastDevice.mParentDevice Is Nothing) Then
                        Exit While
                    End If

                    mLastDevice = mLastDevice.mParentDevice
                End While

                mLastTotalDevice.Reverse()
                mTotalDevices = mLastTotalDevice.ToArray
            End If

            Dim mTreeNode As TreeNode = Nothing

            For Each mDevice In mTotalDevices
                mTreeNode = Nothing

                If (g_mTreeViewNodes.ContainsKey(mDevice.sDeviceID)) Then
                    mTreeNode = g_mTreeViewNodes(mDevice.sDeviceID)
                End If

                If (mTreeNode Is Nothing) Then
                    mTreeNode = New STRUC_LISTVIEW_NODE_DEVICE_ITEM(mDevice, True)

                    mTreeNode.ImageKey = "Normal"

                    If (Not String.IsNullOrEmpty(mDevice.mProvider.sService)) Then
                        Select Case (mDevice.mProvider.sService.ToUpperInvariant)
                            Case ClassLibusbDriver.USBXHCI_SERVICE_NAME 'USB 3.0
                                mTreeNode.ImageKey = "USB"
                            Case ClassLibusbDriver.USBEHCI_SERVICE_NAME  'USB 2.0
                                mTreeNode.ImageKey = "Error"
                            Case ClassLibusbDriver.USBOHCI_SERVICE_NAME  'USB 1.1
                                mTreeNode.ImageKey = "Error"
                            Case ClassLibusbDriver.USBUHCI_SERVICE_NAME  'USB 1.0
                                mTreeNode.ImageKey = "Error"

                            Case ClassLibusbDriver.BTHUSB_SERVICE_NAME  'Bluetooth
                                mTreeNode.ImageKey = "Bluetooth"
                        End Select
                    End If

                    mTreeNode.SelectedImageKey = mTreeNode.ImageKey

                    g_mTreeViewNodes(mDevice.sDeviceID) = CType(mTreeNode, STRUC_LISTVIEW_NODE_DEVICE_ITEM)
                    mLastTreeNodeCollection.Add(mTreeNode)
                End If

                mLastTreeNodeCollection = mTreeNode.Nodes
            Next

            If (mTreeNode IsNot Nothing) Then
                Dim sName As String = _DriverInfo.sName

                DirectCast(mTreeNode, STRUC_LISTVIEW_NODE_DEVICE_ITEM).m_DeviceName = sName
                DirectCast(mTreeNode, STRUC_LISTVIEW_NODE_DEVICE_ITEM).m_IsReadOnly = False

                If (Not bConnected OrElse Not _Device.mProvider.IsEnabled OrElse _Device.mProvider.IsRemoved) Then
                    mTreeNode.ImageKey = "Removed"
                    mTreeNode.SelectedImageKey = mTreeNode.ImageKey
                Else
                    If (Not String.IsNullOrEmpty(_Device.mProvider.sService) AndAlso Not String.IsNullOrEmpty(_DriverInfo.sService)) Then
                        Select Case (_Device.mProvider.sService.ToUpperInvariant)
                            Case ClassLibusbDriver.BTHUSB_SERVICE_NAME  'Bluetooth
                                mTreeNode.ImageKey = "Bluetooth"

                            Case _DriverInfo.sService.ToUpperInvariant
                                mTreeNode.ImageKey = "Enabled"

                            Case Else
                                mTreeNode.ImageKey = "Error"
                        End Select
                    Else
                        mTreeNode.ImageKey = "Error"
                    End If
                End If

                mTreeNode.SelectedImageKey = mTreeNode.ImageKey
            End If
        End Sub

        Public Sub Clear()
            g_mTreeView.Nodes.Clear()
            g_mTreeViewNodes.Clear()
        End Sub
    End Class

    Private Sub FormConnectedDevices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateDevices()

        ClassUtils.SetControlDoubleBufferingChilds(Me, True)
    End Sub

    Private Sub Button_Refresh_Click(sender As Object, e As EventArgs) Handles Button_Refresh.Click
        UpdateDevices()
    End Sub

    Private Sub UpdateDevices()
        Try
            Using mFormLoading As New FormLoading
                g_ClassTreeViewManager.m_TreeView.BeginUpdate()
                g_ClassTreeViewManager.Clear()

                Dim mLibusbDriver As New ClassLibusbDriver

                Dim mTotalDevices As New List(Of ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO)
                For Each mDevice In mLibusbDriver.GetAllDevices("USB")
                    If (String.IsNullOrEmpty(mDevice.sService)) Then
                        Continue For
                    End If

                    Select Case (mDevice.sService.ToLowerInvariant)
                        Case ClassLibusbDriver.USBVIDEO_SERVICE_NAME.ToLowerInvariant,
                             ClassLibusbDriver.BTHUSB_SERVICE_NAME.ToLowerInvariant

                            Dim sVID As String = Nothing
                            Dim sPID As String = Nothing
                            Dim sMM As String = Nothing
                            If (Not mLibusbDriver.ResolveHardwareID(mDevice.sDeviceID, sVID, sPID, sMM)) Then
                                Continue For
                            End If

                            If (String.IsNullOrEmpty(mDevice.sProviderDescription)) Then
                                Continue For
                            End If

                            Dim mKnownConfig As New ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO(mDevice.sProviderDescription, "", sVID, sPID, sMM, mDevice.sService)

                            mTotalDevices.Add(mKnownConfig)
                    End Select
                Next
                mTotalDevices.AddRange(ClassLibusbDriver.DRV_PS4CAM_KNOWN_CONFIGS)
                mTotalDevices.AddRange(ClassLibusbDriver.DRV_PSEYE_KNOWN_CONFIGS)
                mTotalDevices.AddRange(ClassLibusbDriver.DRV_PSVR_KNOWN_CONFIGS)
                mTotalDevices.AddRange(ClassLibusbDriver.DRV_PSMOVE_KNOWN_CONFIGS)
                mTotalDevices.AddRange(ClassLibusbDriver.DRV_CONTROLLER_KNOWN_CONFIGS)
                mTotalDevices.AddRange(ClassLibusbDriver.DRV_DUALSHOCK_KNOWN_CONFIGS)


                mFormLoading.ProgressBar1.Style = ProgressBarStyle.Blocks
                mFormLoading.ProgressBar1.Minimum = 0
                mFormLoading.ProgressBar1.Maximum = mTotalDevices.Count
                mFormLoading.Show()
                mFormLoading.Refresh()

                For Each mItem In mTotalDevices
                    mFormLoading.Text = String.Format("Loading... ({0})", mItem.sName)
                    mFormLoading.Refresh()

                    Dim mTree As ClassLibusbDriver.STRUC_DEVICETREE_ITEM() = mLibusbDriver.GetDeviceTree(mItem, "USB")
                    If (mTree IsNot Nothing) Then
                        For Each mDevice In mTree
                            g_ClassTreeViewManager.AddDevice(mItem, mDevice, CheckBox_ShowDisconnectedDevices.Checked)
                        Next
                    End If

                    mFormLoading.ProgressBar1.Increment(1)

                    ' Skip smooth animation
                    Dim iOldVal = mFormLoading.ProgressBar1.Value
                    mFormLoading.ProgressBar1.Increment(1)
                    mFormLoading.ProgressBar1.Value = iOldVal

                    mFormLoading.Refresh()
                Next

                g_ClassTreeViewManager.m_TreeView.Sort()
                g_ClassTreeViewManager.m_TreeView.ExpandAll()
            End Using
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        Finally
            g_ClassTreeViewManager.m_TreeView.EndUpdate()
        End Try
    End Sub

    Private Sub TreeView_ConnectedDevices_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView_ConnectedDevices.AfterSelect
        Try
            If (TreeView_ConnectedDevices.SelectedNode Is Nothing) Then
                ListView_DeviceInfo.Items.Clear()
                Return
            End If

            Dim mDevice = DirectCast(TreeView_ConnectedDevices.SelectedNode, STRUC_LISTVIEW_NODE_DEVICE_ITEM)

            Try
                ListView_DeviceInfo.BeginUpdate()
                ListView_DeviceInfo.Items.Clear()

                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Name", mDevice.m_DeviceName}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"VID", mDevice.m_Device.sDeviceVID}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"PID", mDevice.m_Device.sDevicePID}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"MI", mDevice.m_Device.sDeviceMM}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Serial", mDevice.m_Device.sDeviceSerial}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Hardware ID", If(mDevice.m_Device.mProvider.sDeviceID, "")}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Config Flags", mDevice.m_Device.mProvider.iConfigFlags.ToString}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Driver Inf Path", If(mDevice.m_Device.mProvider.sDriverInfPath, "")}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Service", If(mDevice.m_Device.mProvider.sService, "")}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Provider Name", If(mDevice.m_Device.mProvider.sProviderName, "")}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Provider Version", If(mDevice.m_Device.mProvider.sProviderVersion, "")}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Provider Description", If(mDevice.m_Device.mProvider.sProviderDescription, "")}))
            Finally
                ListView_DeviceInfo.EndUpdate()
            End Try
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ToolStripMenuItem_DeviceEnable_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_DeviceEnable.Click
        Try
            If (g_ClassTreeViewManager.m_TreeView.SelectedNode Is Nothing) Then
                Return
            End If

            Dim mDevice = DirectCast(TreeView_ConnectedDevices.SelectedNode, STRUC_LISTVIEW_NODE_DEVICE_ITEM)
            Dim sDeviceId As String = mDevice.m_Device.mProvider.sDeviceID
            If (mDevice.m_IsReadOnly) Then
                Throw New ArgumentException("Device can not be changed.")
            End If

            If (String.IsNullOrEmpty(sDeviceId)) Then
                Throw New ArgumentException("Device id is empty.")
            End If

            If (True) Then
                Dim sText As New Text.StringBuilder
                sText.AppendLine("Do you want to enable device:")
                sText.AppendLine(mDevice.m_DeviceName)

                If (MessageBox.Show(sText.ToString, "Uninstall Device", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
                    Return
                End If
            End If

            Dim mLibUSB As New ClassLibusbDriver
            If (mLibUSB.DeviceSetState(sDeviceId, True) <> 0) Then
                Throw New ArgumentException("Unable to set device state.")
            End If

        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ToolStripMenuItem_DeviceDisable_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_DeviceDisable.Click
        Try
            If (g_ClassTreeViewManager.m_TreeView.SelectedNode Is Nothing) Then
                Return
            End If

            Dim mDevice = DirectCast(TreeView_ConnectedDevices.SelectedNode, STRUC_LISTVIEW_NODE_DEVICE_ITEM)
            Dim sDeviceId As String = mDevice.m_Device.mProvider.sDeviceID
            If (mDevice.m_IsReadOnly) Then
                Throw New ArgumentException("Device can not be changed.")
            End If

            If (String.IsNullOrEmpty(sDeviceId)) Then
                Throw New ArgumentException("Device id is empty.")
            End If

            If (True) Then
                Dim sText As New Text.StringBuilder
                sText.AppendLine("Do you want to disable device:")
                sText.AppendLine(mDevice.m_DeviceName)

                If (MessageBox.Show(sText.ToString, "Uninstall Device", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
                    Return
                End If
            End If

            Dim mLibUSB As New ClassLibusbDriver
            If (mLibUSB.DeviceSetState(sDeviceId, False) <> 0) Then
                Throw New ArgumentException("Unable to set device state.")
            End If

        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ToolStripMenuItem_DeviceUninstall_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_DeviceUninstall.Click
        Try
            If (g_ClassTreeViewManager.m_TreeView.SelectedNode Is Nothing) Then
                Return
            End If

            Dim mDevice = DirectCast(TreeView_ConnectedDevices.SelectedNode, STRUC_LISTVIEW_NODE_DEVICE_ITEM)
            Dim sDeviceId As String = mDevice.m_Device.mProvider.sDeviceID
            If (mDevice.m_IsReadOnly) Then
                Throw New ArgumentException("Device can not be changed.")
            End If

            If (String.IsNullOrEmpty(sDeviceId)) Then
                Throw New ArgumentException("Device id is empty.")
            End If

            If (True) Then
                Dim sText As New Text.StringBuilder
                sText.AppendLine("Do you want to uninstall device:")
                sText.AppendLine(mDevice.m_DeviceName)

                If (MessageBox.Show(sText.ToString, "Uninstall Device", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
                    Return
                End If
            End If

            Dim mLibUSB As New ClassLibusbDriver
            If (mLibUSB.RemoveDevice(sDeviceId, True) <> 0) Then
                Throw New ArgumentException("Unable to set device state.")
            End If

            Dim sDeviceDriver As String = mDevice.m_Device.mProvider.sDriverInfPath
            If (Not String.IsNullOrEmpty(sDeviceDriver) AndAlso MessageBox.Show("Do you want to remove the device driver?", "Uninstall Device", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes) Then
                If (mLibUSB.RemoveDriver(sDeviceDriver) <> 0) Then
                    Throw New ArgumentException("Unable to set device state.")
                End If
            End If

        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ToolStripMenuItem_DeviceRefresh_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_DeviceRefresh.Click
        Try
            Dim mLibUSB As New ClassLibusbDriver
            mLibUSB.ScanDevices()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub
End Class