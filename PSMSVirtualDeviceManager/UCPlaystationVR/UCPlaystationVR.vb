Public Class UCPlaystationVR
    Private g_FormMain As FormMain
    Private Shared _ThreadLock As New Object

    Private g_ClassUsbNotify As ClassDevicesNotify
    Private g_ClassDisplayNotify As ClassDevicesNotify

    Private g_mHardwareChangeStatusThread As Threading.Thread = Nothing
    Private g_bHardwareChangeStatusUpdatenNow As Boolean = False

    Enum ENUM_DEVICE_HDMI_STATUS
        NOT_CONNECTED
        CONNECTED
        GENERAL_ISSUE
    End Enum

    Enum ENUM_DEVICE_USB_STATUS
        NOT_CONNECTED
        CONNECTED
        DRIVER_ISSUE
        GENERAL_ISSUE
    End Enum

    Public Sub New(mFormMain As FormMain)
        g_FormMain = mFormMain

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetPlaystationVRStatus(ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED, ENUM_DEVICE_USB_STATUS.NOT_CONNECTED)

        g_ClassUsbNotify = New ClassDevicesNotify()
        g_ClassUsbNotify.RegisterDeviceChange(ClassDevicesNotify.ENUM_DEVICE_TYPE.GUID_DEVINTERFACE_USB_DEVICE)

        g_ClassDisplayNotify = New ClassDevicesNotify()
        g_ClassDisplayNotify.RegisterDeviceChange(ClassDevicesNotify.ENUM_DEVICE_TYPE.GUID_DEVINTERFACE_MONITOR)

        AddHandler g_ClassUsbNotify.OnDeviceConnectionChanged, AddressOf OnDeviceChanged
        AddHandler g_ClassDisplayNotify.OnDeviceConnectionChanged, AddressOf OnDeviceChanged

        CreateControl()

        g_mHardwareChangeStatusThread = New Threading.Thread(AddressOf HardwareChangeStatusThread)
        g_mHardwareChangeStatusThread.IsBackground = True
        g_mHardwareChangeStatusThread.Start()
    End Sub

    Public Sub SetPlaystationVRStatus(iHdmiStatus As ENUM_DEVICE_HDMI_STATUS, iUsbStatus As ENUM_DEVICE_USB_STATUS)
        Select Case (iHdmiStatus)
            Case ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED
                Label_HDMIStatus.Text = "HDMI Disconnected"
                Label_HDMIStatusText.Text = "HDMI cable is not connected. Please attach the PlayStation VR HDMI cable to your computer."
                ClassPictureBox_HDMIStatus.Image = My.Resources.Connection_HDMI_FAIL

            Case ENUM_DEVICE_HDMI_STATUS.CONNECTED
                Label_HDMIStatus.Text = "HDMI Connected"
                Label_HDMIStatusText.Text = "HDMI cable is connected."
                ClassPictureBox_HDMIStatus.Image = My.Resources.Connection_HDMI_OK

            Case Else
                Label_HDMIStatus.Text = "HDMI Error"
                Label_HDMIStatusText.Text = "HDMI connection has an unknown error."
                ClassPictureBox_HDMIStatus.Image = My.Resources.Connection_HDMI_FAIL

        End Select

        Select Case (iUsbStatus)
            Case ENUM_DEVICE_USB_STATUS.NOT_CONNECTED
                Label_USBStatus.Text = "USB Disconnected"
                Label_USBStatusText.Text = "USB cable is not connected. Please attach the PlayStation VR USB cable to your computer."
                ClassPictureBox_USBStatus.Image = My.Resources.Connection_USB_FAIL

            Case ENUM_DEVICE_USB_STATUS.CONNECTED
                Label_USBStatus.Text = "USB Connected"
                Label_USBStatusText.Text = "USB cable is connected."
                ClassPictureBox_USBStatus.Image = My.Resources.Connection_USB_OK

            Case ENUM_DEVICE_USB_STATUS.DRIVER_ISSUE
                Label_USBStatus.Text = "USB Driver Issue"
                Label_USBStatusText.Text = "USB drivers are not properly installed. Please install the Playstation VR USB drivers correctly."
                ClassPictureBox_USBStatus.Image = My.Resources.Connection_USB_WARN

            Case Else
                Label_USBStatus.Text = "HDMI Error"
                Label_USBStatusText.Text = "HDMI connection has an unknown error."
                ClassPictureBox_USBStatus.Image = My.Resources.Connection_USB_FAIL

        End Select

        Select Case (True)
            Case (iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.CONNECTED AndAlso iUsbStatus = ENUM_DEVICE_USB_STATUS.CONNECTED)
                ' All good
                Label_PSVRStatus.Text = "PlayStation VR Connected"
                Panel_PSVRStatus.BackColor = Color.FromArgb(0, 192, 0)

            Case (iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED AndAlso iUsbStatus = ENUM_DEVICE_USB_STATUS.NOT_CONNECTED)
                ' All connected bug issues.
                Label_PSVRStatus.Text = "PlayStation VR Disconnected"
                Panel_PSVRStatus.BackColor = Color.FromArgb(224, 224, 224)

            Case (iUsbStatus = ENUM_DEVICE_USB_STATUS.DRIVER_ISSUE)
                ' All USB driver issue.
                Label_PSVRStatus.Text = "PlayStation VR Issues Detected"
                Panel_PSVRStatus.BackColor = Color.FromArgb(255, 128, 0)

            Case (iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.GENERAL_ISSUE OrElse iUsbStatus = ENUM_DEVICE_USB_STATUS.GENERAL_ISSUE)
                ' USB or HDMI general issues.
                Label_PSVRStatus.Text = "PlayStation VR Error"
                Panel_PSVRStatus.BackColor = Color.FromArgb(192, 0, 0)

            Case (iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED OrElse iUsbStatus = ENUM_DEVICE_USB_STATUS.NOT_CONNECTED)
                ' Either HDMI or USB not connected.
                Label_PSVRStatus.Text = "PlayStation VR Not Properly Connected"
                Panel_PSVRStatus.BackColor = Color.FromArgb(255, 128, 0)

            Case Else
                ' Dunno.
                Label_PSVRStatus.Text = "PlayStation VR Unknown Status"
                Panel_PSVRStatus.BackColor = Color.FromArgb(224, 224, 224)

        End Select
    End Sub

    Private Sub OnDeviceChanged(bConnected As Boolean, sDeviceName As String)
        UpdateHardwareChangeStatusNow()
    End Sub

    Public Sub UpdateHardwareChangeStatusNow()
        SyncLock _ThreadLock
            g_bHardwareChangeStatusUpdatenNow = True
        End SyncLock
    End Sub

    Private Sub HardwareChangeStatusThread()
        While True
            Try
                Dim iHdmiStatus As ENUM_DEVICE_HDMI_STATUS = ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED
                Dim iUsbStatus As ENUM_DEVICE_USB_STATUS = ENUM_DEVICE_USB_STATUS.NOT_CONNECTED

                ' Check HDMI connection. Well we cant really check for HDMI but we can just find the monitor instead.
                Try
                    Dim mClassMonitor As New ClassMonitor
                    Dim mPsvrMonitor As ClassMonitor.DEVMODE = Nothing
                    If (mClassMonitor.FindPlaystationVrMonitor(mPsvrMonitor, Nothing)) Then
                        iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.CONNECTED
                    Else
                        iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED
                    End If
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.GENERAL_ISSUE
                End Try


                ' Check USB connection and its drivers.
                Try
                    Dim mClassLibusbDriver As New ClassLibusbDriver

                    If (mClassLibusbDriver.IsPlaystationVrUsbDeviceConnected()) Then
                        If (mClassLibusbDriver.VerifyPlaystationVrDriver64()) Then
                            iUsbStatus = ENUM_DEVICE_USB_STATUS.CONNECTED
                        Else
                            iUsbStatus = ENUM_DEVICE_USB_STATUS.DRIVER_ISSUE
                        End If
                    Else
                        iUsbStatus = ENUM_DEVICE_USB_STATUS.NOT_CONNECTED
                    End If
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    iUsbStatus = ENUM_DEVICE_USB_STATUS.GENERAL_ISSUE
                End Try

                ClassUtils.AsyncInvoke(Me, Sub() SetPlaystationVRStatus(iHdmiStatus, iUsbStatus))

            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception

            End Try

            For i = 0 To 10
                SyncLock _ThreadLock
                    If (g_bHardwareChangeStatusUpdatenNow) Then
                        g_bHardwareChangeStatusUpdatenNow = False
                        Exit For
                    End If
                End SyncLock

                Threading.Thread.Sleep(1000)
            Next
        End While
    End Sub

    Private Sub CleanUp()
        If (g_mHardwareChangeStatusThread IsNot Nothing AndAlso g_mHardwareChangeStatusThread.IsAlive) Then
            g_mHardwareChangeStatusThread.Abort()
            g_mHardwareChangeStatusThread.Join()
            g_mHardwareChangeStatusThread = Nothing
        End If

        If (g_ClassUsbNotify IsNot Nothing) Then
            RemoveHandler g_ClassUsbNotify.OnDeviceConnectionChanged, AddressOf OnDeviceChanged

            g_ClassUsbNotify.Dispose()
            g_ClassUsbNotify = Nothing
        End If

        If (g_ClassDisplayNotify IsNot Nothing) Then
            RemoveHandler g_ClassDisplayNotify.OnDeviceConnectionChanged, AddressOf OnDeviceChanged

            g_ClassDisplayNotify.Dispose()
            g_ClassDisplayNotify = Nothing
        End If

    End Sub
End Class
