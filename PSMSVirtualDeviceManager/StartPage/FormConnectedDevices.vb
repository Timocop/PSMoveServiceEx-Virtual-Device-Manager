Imports System.Web.Script.Serialization
Imports System.Xml.Serialization

Public Class FormConnectedDevices
    Private g_ClassTreeViewManager As ClassTreeViewManager
    Private g_ClassListViewManager As ClassListViewManager

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
        g_ClassListViewManager = New ClassListViewManager(ListView_BluetoothDevices)
    End Sub

    Class TreeViewSerialization
        <Serializable>
        Class TreeNodeData
            Property sText As String

            Property sDeviceID As String
            Property iConfigFlags As ClassLibusbDriver.DEVICE_CONFIG_FLAGS
            Property sService As String
            Property sProviderName As String
            Property sProviderDescription As String
            Property sProviderVersion As String
            Property sDriverInfPath As String

            Property mNodes As New List(Of TreeNodeData)
        End Class

        Public Function SerializeTreeNodes(mNodes As TreeNodeCollection) As String
            Dim mSerializer As New JavaScriptSerializer()
            Dim mNodeList As New List(Of TreeNodeData)

            For Each mNode As TreeNode In mNodes
                mNodeList.Add(SerializeTreeNode(mNode))
            Next

            Return mSerializer.Serialize(mNodeList)
        End Function

        Private Function SerializeTreeNode(mNode As TreeNode) As TreeNodeData
            Dim mModeData As New TreeNodeData()
            mModeData.sText = mNode.Text

            If (TypeOf mNode Is STRUC_TREEVIEW_NODE_DEVICE_ITEM) Then
                Dim mDeviceNode = DirectCast(mNode, STRUC_TREEVIEW_NODE_DEVICE_ITEM)
                mModeData.sDeviceID = mDeviceNode.m_Device.sDeviceID
                mModeData.iConfigFlags = mDeviceNode.m_Device.iConfigFlags
                mModeData.sService = mDeviceNode.m_Device.sService
                mModeData.sProviderName = mDeviceNode.m_Device.sProviderName
                mModeData.sProviderDescription = mDeviceNode.m_Device.sProviderDescription
                mModeData.sProviderVersion = mDeviceNode.m_Device.sProviderVersion
                mModeData.sDriverInfPath = mDeviceNode.m_Device.sDriverInfPath
            End If

            For Each mChildNode As TreeNode In mNode.Nodes
                mModeData.mNodes.Add(SerializeTreeNode(mChildNode))
            Next

            Return mModeData
        End Function
    End Class

    Class STRUC_TREEVIEW_NODE_DEVICE_ITEM
        Inherits TreeNode

        Private g_bReadOnly As Boolean = False
        Private g_sDeviceName As String = ""
        Private g_mDevice As ClassLibusbDriver.STRUC_DEVICE_PROVIDER = Nothing

        Public Sub New(_Device As ClassLibusbDriver.STRUC_DEVICE_PROVIDER, _ReadOnly As Boolean)
            Me.New("", _Device, _ReadOnly)
        End Sub

        Public Sub New(_DeviceName As String, _Device As ClassLibusbDriver.STRUC_DEVICE_PROVIDER, _ReadOnly As Boolean)
            g_sDeviceName = _DeviceName
            g_mDevice = _Device
            g_bReadOnly = _ReadOnly

            Me.Text = Me.ToString
        End Sub

        Public Overrides Function ToString() As String
            If (String.IsNullOrEmpty(g_mDevice.sProviderDescription) OrElse g_mDevice.sProviderDescription.TrimEnd.Length = 0) Then
                If (String.IsNullOrEmpty(g_sDeviceName) OrElse g_sDeviceName.TrimEnd.Length = 0) Then
                    Return "Unknown"
                Else
                    Return String.Format("{0} ({1})", "Unknown", g_sDeviceName)
                End If
            End If

            If (String.IsNullOrEmpty(g_sDeviceName) OrElse g_sDeviceName.TrimEnd.Length = 0) Then
                Return g_mDevice.sProviderDescription
            Else
                Return String.Format("{0} ({1})", g_mDevice.sProviderDescription, g_sDeviceName)
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

        Public ReadOnly Property m_Device As ClassLibusbDriver.STRUC_DEVICE_PROVIDER
            Get
                Return g_mDevice
            End Get
        End Property
    End Class

    Class STRUC_LISTVIEW_ITEM_DEVICE_ITEM
        Inherits ListViewItem

        Private g_mDevice As ClassLibusbDriver.STRUC_DEVICE_BLUETOOTH = Nothing

        Public Sub New(_Device As ClassLibusbDriver.STRUC_DEVICE_BLUETOOTH)
            If (_Device.sFriendlyName Is Nothing) Then
                Me.Text = "<Unknown Device>"
            Else
                Me.Text = _Device.sFriendlyName
            End If

            g_mDevice = _Device
        End Sub

        Public ReadOnly Property m_Device As ClassLibusbDriver.STRUC_DEVICE_BLUETOOTH
            Get
                Return g_mDevice
            End Get
        End Property
    End Class

    Class ClassTreeViewManager
        Private g_mTreeView As TreeView
        Private g_mTreeViewNodes As New Dictionary(Of String, STRUC_TREEVIEW_NODE_DEVICE_ITEM)

        Public Sub New(_TreeView As TreeView)
            g_mTreeView = _TreeView
        End Sub

        ReadOnly Property m_TreeView As TreeView
            Get
                Return g_mTreeView
            End Get
        End Property

        Public Sub AddDevice(_DriverInfo As ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO, _Device As ClassLibusbDriver.STRUC_DEVICETREE_ITEM)
            Dim mLibusbDriver As New ClassLibusbDriver

            Dim mLastTreeNodeCollection As TreeNodeCollection = g_mTreeView.Nodes

            Dim mTreeNode As TreeNode = Nothing
            Dim mTotalDevices As New List(Of ClassLibusbDriver.STRUC_DEVICE_PROVIDER)
            mTotalDevices.AddRange(_Device.mParentDevices)
            mTotalDevices.Reverse()

            For Each mDevice In mTotalDevices
                mTreeNode = Nothing

                If (g_mTreeViewNodes.ContainsKey(mDevice.sDeviceID)) Then
                    mTreeNode = g_mTreeViewNodes(mDevice.sDeviceID)
                End If

                If (mTreeNode Is Nothing) Then
                    mTreeNode = New STRUC_TREEVIEW_NODE_DEVICE_ITEM(mDevice, True)

                    mTreeNode.ImageKey = "Normal"

                    If (Not String.IsNullOrEmpty(mDevice.sService)) Then
                        Select Case (mDevice.sService.ToUpperInvariant)
                            Case ClassLibusbDriver.USBXHCI_SERVICE_NAME 'USB 3.0
                                mTreeNode.ImageKey = "USB"
                            Case ClassLibusbDriver.USBEHCI_SERVICE_NAME  'USB 2.0 / Not recommended
                                mTreeNode.ImageKey = "Error"
                            Case ClassLibusbDriver.USBOHCI_SERVICE_NAME  'USB 1.1 / Not recommended
                                mTreeNode.ImageKey = "Error"
                            Case ClassLibusbDriver.USBUHCI_SERVICE_NAME  'USB 1.0 / Not recommended
                                mTreeNode.ImageKey = "Error"

                            Case ClassLibusbDriver.BTHUSB_SERVICE_NAME  'Bluetooth
                                mTreeNode.ImageKey = "Bluetooth"
                        End Select
                    End If

                    mTreeNode.SelectedImageKey = mTreeNode.ImageKey

                    g_mTreeViewNodes(mDevice.sDeviceID) = CType(mTreeNode, STRUC_TREEVIEW_NODE_DEVICE_ITEM)
                    mLastTreeNodeCollection.Add(mTreeNode)
                End If

                mLastTreeNodeCollection = mTreeNode.Nodes
            Next

            If (mTreeNode IsNot Nothing) Then
                Dim sName As String = _DriverInfo.sName

                DirectCast(mTreeNode, STRUC_TREEVIEW_NODE_DEVICE_ITEM).m_DeviceName = sName
                DirectCast(mTreeNode, STRUC_TREEVIEW_NODE_DEVICE_ITEM).m_IsReadOnly = False

                Dim bConnected As Boolean = mLibusbDriver.IsUsbDeviceConnected(_DriverInfo.VID, _DriverInfo.PID, _Device.sInterface, _Device.sDeviceSerial)

                If (Not bConnected OrElse Not _Device.mProvider.IsEnabled OrElse _Device.mProvider.IsRemoved) Then
                    mTreeNode.ImageKey = "Removed"
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

    Class ClassListViewManager
        Private g_mListView As ListView
        Private g_mListViewNodes As New Dictionary(Of String, ClassLibusbDriver.STRUC_DEVICE_BLUETOOTH)

        Public Sub New(_ListView As ListView)
            g_mListView = _ListView
        End Sub

        ReadOnly Property m_ListView As ListView
            Get
                Return g_mListView
            End Get
        End Property

        Public Sub AddDevice(_Device As ClassLibusbDriver.STRUC_DEVICE_BLUETOOTH)
            If (g_mListViewNodes.ContainsKey(_Device.sDeviceID)) Then
                Return
            End If

            Dim mItem As New STRUC_LISTVIEW_ITEM_DEVICE_ITEM(_Device)
            mItem.ImageKey = "Bluetooth"

            g_mListView.Items.Add(mItem)
            g_mListViewNodes(_Device.sDeviceID) = _Device
        End Sub

        Public Sub Clear()
            g_mListView.Items.Clear()
            g_mListViewNodes.Clear()
        End Sub
    End Class

    Private Sub FormConnectedDevices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = True
        Me.Refresh()

        UpdateDevices()
    End Sub

    Private Sub Button_Refresh_Click(sender As Object, e As EventArgs) Handles Button_Refresh.Click
        UpdateDevices()
    End Sub

    Private Sub UpdateDevices()
        Try
            Using mFormLoading As New FormLoading
                g_ClassTreeViewManager.m_TreeView.BeginUpdate()
                g_ClassListViewManager.m_ListView.BeginUpdate()
                g_ClassTreeViewManager.Clear()
                g_ClassListViewManager.Clear()

                Dim mLibusbDriver As New ClassLibusbDriver

                Dim mUsbDevices As New Dictionary(Of String, ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO)
                For Each mDevice In mLibusbDriver.GetAllDevices("USB")
                    If (String.IsNullOrEmpty(mDevice.sService)) Then
                        Continue For
                    End If

                    Dim bSuccess As Boolean = False

                    Select Case (mDevice.sService.ToUpperInvariant)
                        Case ClassLibusbDriver.USBVIDEO_SERVICE_NAME.ToUpperInvariant,
                                    ClassLibusbDriver.BTHUSB_SERVICE_NAME.ToUpperInvariant

                            bSuccess = True
                    End Select

                    If (bSuccess) Then
                        Dim sVID As String = Nothing
                        Dim sPID As String = Nothing
                        Dim sMM As String = Nothing
                        Dim sSerial As String = Nothing
                        If (Not mLibusbDriver.ResolveHardwareID(mDevice.sDeviceID, sVID, sPID, sMM, sSerial)) Then
                            Continue For
                        End If

                        If (String.IsNullOrEmpty(mDevice.sProviderDescription)) Then
                            Continue For
                        End If

                        Dim mKnownConfig As New ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO(mDevice.sProviderDescription, "", sVID, sPID, sMM, mDevice.sService)

                        mUsbDevices(String.Format("{0}/{1}/{2}", sVID, sPID, If(sMM, "XX"))) = mKnownConfig
                    End If
                Next

                Dim mTmpList As New List(Of ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO)
                mTmpList.AddRange(ClassLibusbDriver.DRV_PS4CAM_KNOWN_CONFIGS)
                mTmpList.AddRange(ClassLibusbDriver.DRV_PSEYE_KNOWN_CONFIGS)
                mTmpList.AddRange(ClassLibusbDriver.DRV_PSVR_KNOWN_CONFIGS)
                mTmpList.AddRange(ClassLibusbDriver.DRV_PSMOVE_KNOWN_CONFIGS)
                mTmpList.AddRange(ClassLibusbDriver.DRV_CONTROLLER_KNOWN_CONFIGS)
                mTmpList.AddRange(ClassLibusbDriver.DRV_DUALSHOCK_KNOWN_CONFIGS)

                For Each mDevice In mTmpList
                    mUsbDevices(String.Format("{0}/{1}/{2}", mDevice.VID, mDevice.PID, If(mDevice.MM, "XX"))) = mDevice
                Next

                Dim mBtDevices As New Dictionary(Of String, ClassLibusbDriver.STRUC_DEVICE_BLUETOOTH)
                For Each mDevice In mLibusbDriver.GetAllDevicesBluetooth
                    If (String.IsNullOrEmpty(mDevice.sFriendlyName)) Then
                        Continue For
                    End If

                    If (Not mDevice.sDeviceID.StartsWith("Dev_")) Then
                        Continue For
                    End If

                    mBtDevices(mDevice.sDeviceID) = mDevice
                Next

                mFormLoading.m_ProgressBar.Style = ProgressBarStyle.Blocks
                mFormLoading.m_ProgressBar.Minimum = 0
                mFormLoading.m_ProgressBar.Maximum = mUsbDevices.Count + mBtDevices.Count
                mFormLoading.Show()
                mFormLoading.Refresh()

                For Each mItem In mUsbDevices
                    mFormLoading.Text = String.Format("Loading... ({0})", mItem.Value.sName)
                    mFormLoading.Refresh()

                    Dim mTree As ClassLibusbDriver.STRUC_DEVICETREE_ITEM() = mLibusbDriver.GetDeviceTree(mItem.Value, "USB", Not CheckBox_ShowDisconnectedDevices.Checked)
                    If (mTree IsNot Nothing) Then
                        For Each mDevice In mTree
                            g_ClassTreeViewManager.AddDevice(mItem.Value, mDevice)
                        Next
                    End If

                    mFormLoading.m_ProgressBar.Increment(1)
                    mFormLoading.SkipProgressBarAnimation()

                    mFormLoading.Refresh()
                Next

                g_ClassTreeViewManager.m_TreeView.Sort()
                g_ClassTreeViewManager.m_TreeView.ExpandAll()

                For Each mItem In mBtDevices
                    Dim sFriendlyName As String = mItem.Value.sFriendlyName
                    If (String.IsNullOrEmpty(sFriendlyName)) Then
                        sFriendlyName = "<Unknown Device>"
                    End If

                    mFormLoading.Text = String.Format("Loading... ({0})", sFriendlyName)
                    mFormLoading.Refresh()

                    g_ClassListViewManager.AddDevice(mItem.Value)

                    mFormLoading.m_ProgressBar.Increment(1)
                    mFormLoading.SkipProgressBarAnimation()

                    mFormLoading.Refresh()
                Next

                g_ClassListViewManager.m_ListView.Sort()
            End Using
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        Finally
            g_ClassTreeViewManager.m_TreeView.EndUpdate()
            g_ClassListViewManager.m_ListView.EndUpdate()
        End Try
    End Sub

    Private Sub TreeView_ConnectedDevices_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView_ConnectedDevices.AfterSelect
        Try
            If (TreeView_ConnectedDevices.SelectedNode Is Nothing) Then
                ListView_DeviceInfo.Items.Clear()
                Return
            End If

            Dim mDevice = DirectCast(TreeView_ConnectedDevices.SelectedNode, STRUC_TREEVIEW_NODE_DEVICE_ITEM)

            Try
                ListView_DeviceInfo.BeginUpdate()
                ListView_DeviceInfo.Items.Clear()

                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Name", mDevice.m_DeviceName}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Hardware ID", If(mDevice.m_Device.sDeviceID, "")}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Config Flags", mDevice.m_Device.iConfigFlags.ToString}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Driver Inf Path", If(mDevice.m_Device.sDriverInfPath, "")}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Service", If(mDevice.m_Device.sService, "")}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Provider Name", If(mDevice.m_Device.sProviderName, "")}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Provider Version", If(mDevice.m_Device.sProviderVersion, "")}))
                ListView_DeviceInfo.Items.Add(New ListViewItem(New String() {"Provider Description", If(mDevice.m_Device.sProviderDescription, "")}))
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

            Dim mDevice = DirectCast(g_ClassTreeViewManager.m_TreeView.SelectedNode, STRUC_TREEVIEW_NODE_DEVICE_ITEM)
            Dim sDeviceId As String = mDevice.m_Device.sDeviceID
            If (mDevice.m_IsReadOnly) Then
                Throw New ArgumentException("Device can not be changed.")
            End If

            If (String.IsNullOrEmpty(sDeviceId)) Then
                Throw New ArgumentException("Device id is empty.")
            End If

            If (True) Then
                Dim sText As New Text.StringBuilder
                sText.AppendLine("Do you want to enable the following device?")
                sText.AppendLine()
                sText.AppendLine(mDevice.m_DeviceName)
                sText.AppendLine(sDeviceId.ToUpperInvariant)

                If (MessageBox.Show(sText.ToString, "Enable Device", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
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

            Dim mDevice = DirectCast(g_ClassTreeViewManager.m_TreeView.SelectedNode, STRUC_TREEVIEW_NODE_DEVICE_ITEM)
            Dim sDeviceId As String = mDevice.m_Device.sDeviceID
            If (mDevice.m_IsReadOnly) Then
                Throw New ArgumentException("Device can not be changed.")
            End If

            If (String.IsNullOrEmpty(sDeviceId)) Then
                Throw New ArgumentException("Device id is empty.")
            End If

            If (True) Then
                Dim sText As New Text.StringBuilder
                sText.AppendLine("Do you want to disable the following device?")
                sText.AppendLine()
                sText.AppendLine(mDevice.m_DeviceName)
                sText.AppendLine(sDeviceId.ToUpperInvariant)

                If (MessageBox.Show(sText.ToString, "Disable Device", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
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

            Dim mDevice = DirectCast(g_ClassTreeViewManager.m_TreeView.SelectedNode, STRUC_TREEVIEW_NODE_DEVICE_ITEM)
            Dim sDeviceId As String = mDevice.m_Device.sDeviceID
            If (mDevice.m_IsReadOnly) Then
                Throw New ArgumentException("Device can not be changed.")
            End If

            If (String.IsNullOrEmpty(sDeviceId)) Then
                Throw New ArgumentException("Device id is empty.")
            End If

            If (True) Then
                Dim sText As New Text.StringBuilder
                sText.AppendLine("Do you want to uninstall the following device?")
                sText.AppendLine()
                sText.AppendLine(mDevice.m_DeviceName)
                sText.AppendLine(sDeviceId.ToUpperInvariant)

                If (MessageBox.Show(sText.ToString, "Uninstall Device", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No) Then
                    Return
                End If
            End If

            Dim mLibUSB As New ClassLibusbDriver
            If (mLibUSB.RemoveDevice(sDeviceId, True) <> 0) Then
                Throw New ArgumentException("Unable to remove device.")
            End If

            Dim sDeviceDriver As String = mDevice.m_Device.sDriverInfPath
            If (Not String.IsNullOrEmpty(sDeviceDriver) AndAlso sDeviceDriver.ToLowerInvariant.StartsWith("oem") AndAlso MessageBox.Show("Do you want to remove the device driver?", "Uninstall Device", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes) Then
                If (mLibUSB.RemoveDriver(sDeviceDriver) <> 0) Then
                    Throw New ArgumentException("Unable to remove device driver.")
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

            UpdateDevices()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub Button_CopyOutput_Click(sender As Object, e As EventArgs) Handles Button_CopyOutput.Click
        Try
            Dim mTreeViewSerializer As New TreeViewSerialization

            Dim sSerialized = mTreeViewSerializer.SerializeTreeNodes(TreeView_ConnectedDevices.Nodes)
            sSerialized = ClassUtils.FormatJsonOutput(sSerialized)

            Clipboard.SetText(sSerialized)
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub TreeView_ConnectedDevices_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView_ConnectedDevices.NodeMouseClick
        If (e.Node Is Nothing) Then
            Return
        End If

        TreeView_ConnectedDevices.SelectedNode = e.Node
    End Sub

    Private Sub ToolStripMenuItem_BluetoothRefresh_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_BluetoothRefresh.Click
        Try
            Dim mLibUSB As New ClassLibusbDriver
            mLibUSB.ScanDevices()

            UpdateDevices()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub ToolStripMenuItem_BluetoothOpenSystem_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_BluetoothOpenSystem.Click
        Try
            Process.Start("control", "bthprops.cpl")
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub
End Class