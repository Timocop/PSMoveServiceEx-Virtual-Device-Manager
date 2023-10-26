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

    Enum ENUM_DEVICE_DISPLAY_STATUS
        NOT_CONNECTED
        CONFIGURED_MULTI
        CONFIGURED_DIRECT
        WAITING_FOR_RELOAD
        NOT_CONFIGURED
        MIRRROED
        DISABLED
        BAD_FREQUENCY
        GENERAL_ISSUE
    End Enum

    Public Sub New(mFormMain As FormMain)
        g_FormMain = mFormMain

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetPlaystationVRStatus(ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED, ENUM_DEVICE_USB_STATUS.NOT_CONNECTED, ENUM_DEVICE_DISPLAY_STATUS.NOT_CONNECTED)

        Try
            g_ClassUsbNotify = New ClassDevicesNotify()
            g_ClassUsbNotify.RegisterDeviceChange(ClassDevicesNotify.ENUM_DEVICE_TYPE.GUID_DEVINTERFACE_USB_DEVICE)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            g_ClassDisplayNotify = New ClassDevicesNotify()
            g_ClassDisplayNotify.RegisterDeviceChange(ClassDevicesNotify.ENUM_DEVICE_TYPE.GUID_DEVINTERFACE_MONITOR)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        AddHandler g_ClassUsbNotify.OnDeviceConnectionChanged, AddressOf OnDeviceChanged
        AddHandler g_ClassDisplayNotify.OnDeviceConnectionChanged, AddressOf OnDeviceChanged

        CreateControl()

        g_mHardwareChangeStatusThread = New Threading.Thread(AddressOf HardwareChangeStatusThread)
        g_mHardwareChangeStatusThread.IsBackground = True
        g_mHardwareChangeStatusThread.Start()
    End Sub

    Public Sub SetPlaystationVRStatus(iHdmiStatus As ENUM_DEVICE_HDMI_STATUS, iUsbStatus As ENUM_DEVICE_USB_STATUS, iDisplayStatus As ENUM_DEVICE_DISPLAY_STATUS)
        Select Case (iHdmiStatus)
            Case ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED
                Label_HDMIStatus.Text = "HDMI Disconnected"
                Label_HDMIStatusText.Text = "HDMI cable is not connected. Please attach the HDMI cable to your computer."
                ClassPictureBox_HDMIStatus.Image = My.Resources.Connection_HDMI_FAIL

            Case ENUM_DEVICE_HDMI_STATUS.CONNECTED
                Label_HDMIStatus.Text = "HDMI Connected"
                Label_HDMIStatusText.Text = "HDMI cable is connected."
                ClassPictureBox_HDMIStatus.Image = My.Resources.Connection_HDMI_OK

            Case Else
                Label_HDMIStatus.Text = "HDMI Error"
                Label_HDMIStatusText.Text = "HDMI connection encountered an unknown error."
                ClassPictureBox_HDMIStatus.Image = My.Resources.Connection_HDMI_FAIL

        End Select

        Select Case (iUsbStatus)
            Case ENUM_DEVICE_USB_STATUS.NOT_CONNECTED
                Label_USBStatus.Text = "USB Disconnected"
                Label_USBStatusText.Text = "USB cable is not connected. Please attach the USB cable to your computer."
                ClassPictureBox_USBStatus.Image = My.Resources.Connection_USB_FAIL

            Case ENUM_DEVICE_USB_STATUS.CONNECTED
                Label_USBStatus.Text = "USB Connected"
                Label_USBStatusText.Text = "USB cable is connected."
                ClassPictureBox_USBStatus.Image = My.Resources.Connection_USB_OK

            Case ENUM_DEVICE_USB_STATUS.DRIVER_ISSUE
                Label_USBStatus.Text = "USB Driver Issue"
                Label_USBStatusText.Text = "USB drivers are not properly installed. Please install the USB drivers correctly."
                ClassPictureBox_USBStatus.Image = My.Resources.Connection_USB_WARN

            Case Else
                Label_USBStatus.Text = "HDMI Error"
                Label_USBStatusText.Text = "HDMI connection encountered an unknown error."
                ClassPictureBox_USBStatus.Image = My.Resources.Connection_USB_FAIL

        End Select

        Select Case (iDisplayStatus)
            Case ENUM_DEVICE_DISPLAY_STATUS.NOT_CONNECTED
                Label_DisplayStatus.Text = "Display Disconnected"
                Label_DisplayStatusText.Text = "Display is not connected. Please attach the HDMI cable to your computer."
                ClassPictureBox_DisplayStatus.Image = My.Resources.Connection_DISPLAY_FAIL

            Case ENUM_DEVICE_DISPLAY_STATUS.CONFIGURED_MULTI
                Label_DisplayStatus.Text = "Display Working"
                Label_DisplayStatusText.Text = "Display is currently working in virtual-mode (compatibility mode)."
                ClassPictureBox_DisplayStatus.Image = My.Resources.Connection_DISPLAY_OK

            Case ENUM_DEVICE_DISPLAY_STATUS.CONFIGURED_DIRECT
                Label_DisplayStatus.Text = "Display Working"
                Label_DisplayStatusText.Text = "Display is currently working in direct-mode."
                ClassPictureBox_DisplayStatus.Image = My.Resources.Connection_DISPLAY_OK

            Case ENUM_DEVICE_DISPLAY_STATUS.NOT_CONFIGURED
                Label_DisplayStatus.Text = "Display not configured"
                Label_DisplayStatusText.Text = "Display has not been configured correctly. Please setup the display correctly."
                ClassPictureBox_DisplayStatus.Image = My.Resources.Connection_DISPLAY_WARN

            Case ENUM_DEVICE_DISPLAY_STATUS.WAITING_FOR_RELOAD
                Label_DisplayStatus.Text = "Display waiting for update"
                Label_DisplayStatusText.Text = "Display has been set up correctly but is waiting to apply the new configuration. Please replug the HDMI cable or reboot your computer."
                ClassPictureBox_DisplayStatus.Image = My.Resources.Connection_DISPLAY_WARN

            Case ENUM_DEVICE_DISPLAY_STATUS.BAD_FREQUENCY
                Label_DisplayStatus.Text = "Display frequency too low"
                Label_DisplayStatusText.Text = "Display is running at a low frequency which is not recommended to use while in VR-Mode. Switch to a higher display frequency."
                ClassPictureBox_DisplayStatus.Image = My.Resources.Connection_DISPLAY_WARN

            Case ENUM_DEVICE_DISPLAY_STATUS.MIRRROED
                Label_DisplayStatus.Text = "Display not in extended mode"
                Label_DisplayStatusText.Text = "Display is currently not in extended-mode. Please use extended-mode for the display in the Windows display settings."
                ClassPictureBox_DisplayStatus.Image = My.Resources.Connection_DISPLAY_FAIL

            Case ENUM_DEVICE_DISPLAY_STATUS.DISABLED
                Label_DisplayStatus.Text = "Display Disabled"
                Label_DisplayStatusText.Text = "Display has been disabled. Please enable the display in the Windows display settings."
                ClassPictureBox_DisplayStatus.Image = My.Resources.Connection_DISPLAY_FAIL

            Case Else
                Label_DisplayStatus.Text = "Display Error"
                Label_DisplayStatusText.Text = "Display encountered an unknown error."
                ClassPictureBox_DisplayStatus.Image = My.Resources.Connection_DISPLAY_FAIL

        End Select

        Select Case (True)
            Case (iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.CONNECTED AndAlso
                    iUsbStatus = ENUM_DEVICE_USB_STATUS.CONNECTED AndAlso
                    (iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.CONFIGURED_MULTI OrElse iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.CONFIGURED_DIRECT))
                ' All good
                Label_PSVRStatus.Text = "PlayStation VR Connected"
                Panel_PSVRStatus.BackColor = Color.FromArgb(0, 192, 0)

                ' Label Status in MainForm
                g_FormMain.Label_PsvrStatus.Text = "PlayStation VR Connected"
                g_FormMain.Label_PsvrStatus.Image = My.Resources.Status_GREEN_16

            Case (iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED AndAlso
                    iUsbStatus = ENUM_DEVICE_USB_STATUS.NOT_CONNECTED AndAlso
                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.NOT_CONNECTED)
                ' Nothing connected issues.
                Label_PSVRStatus.Text = "PlayStation VR Disconnected"
                Panel_PSVRStatus.BackColor = Color.FromArgb(224, 224, 224)

                ' Label Status in MainForm
                g_FormMain.Label_PsvrStatus.Text = "PlayStation VR Disconnected"
                g_FormMain.Label_PsvrStatus.Image = My.Resources.Status_WHITE_16

            Case (iUsbStatus = ENUM_DEVICE_USB_STATUS.DRIVER_ISSUE OrElse
                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.NOT_CONFIGURED OrElse
                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.WAITING_FOR_RELOAD OrElse
                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.MIRRROED OrElse
                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.DISABLED OrElse
                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.BAD_FREQUENCY)
                ' USB driver or Display configuration iggues.
                Label_PSVRStatus.Text = "PlayStation VR Issues Detected"
                Panel_PSVRStatus.BackColor = Color.FromArgb(255, 128, 0)

                ' Label Status in MainForm
                g_FormMain.Label_PsvrStatus.Text = "PlayStation VR Warning"
                g_FormMain.Label_PsvrStatus.Image = My.Resources.Status_YELLOW_16

            Case (iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.GENERAL_ISSUE OrElse
                    iUsbStatus = ENUM_DEVICE_USB_STATUS.GENERAL_ISSUE OrElse
                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.GENERAL_ISSUE)
                ' USB, HDMI or Display general issues.
                Label_PSVRStatus.Text = "PlayStation VR Error"
                Panel_PSVRStatus.BackColor = Color.FromArgb(192, 0, 0)

                ' Label Status in MainForm
                g_FormMain.Label_PsvrStatus.Text = "PlayStation VR Error"
                g_FormMain.Label_PsvrStatus.Image = My.Resources.Status_RED_16

            Case (iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED OrElse
                    iUsbStatus = ENUM_DEVICE_USB_STATUS.NOT_CONNECTED OrElse
                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.NOT_CONNECTED)
                ' If something isnt connected issues.
                Label_PSVRStatus.Text = "PlayStation VR Not Properly Connected"
                Panel_PSVRStatus.BackColor = Color.FromArgb(255, 128, 0)

                ' Label Status in MainForm
                g_FormMain.Label_PsvrStatus.Text = "PlayStation VR Warning"
                g_FormMain.Label_PsvrStatus.Image = My.Resources.Status_YELLOW_16

            Case Else
                ' Dunno.
                Label_PSVRStatus.Text = "PlayStation VR Unknown Status"
                Panel_PSVRStatus.BackColor = Color.FromArgb(224, 224, 224)

                ' Label Status in MainForm
                g_FormMain.Label_PsvrStatus.Text = "PlayStation VR Unknown Status"
                g_FormMain.Label_PsvrStatus.Image = My.Resources.Status_WHITE_16

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
                Dim iDisplayStatus As ENUM_DEVICE_DISPLAY_STATUS = ENUM_DEVICE_DISPLAY_STATUS.NOT_CONFIGURED

                ' Check HDMI connection. Well we cant really check for HDMI but we can just find the monitor instead.
                Try
                    Dim mClassMonitor As New ClassMonitor
                    Dim mPsvrMonitor As ClassMonitor.DEVMODE = Nothing
                    Select Case (mClassMonitor.FindPlaystationVrMonitor(mPsvrMonitor, Nothing))
                        Case ClassMonitor.ENUM_PSVR_MONITOR_STATUS.SUCCESS,
                             ClassMonitor.ENUM_PSVR_MONITOR_STATUS.ERROR_NOT_ACTIVE,
                             ClassMonitor.ENUM_PSVR_MONITOR_STATUS.ERROR_MIRRROED
                            iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.CONNECTED

                        Case ClassMonitor.ENUM_PSVR_MONITOR_STATUS.ERROR_NOT_FOUND
                            iHdmiStatus = ENUM_DEVICE_HDMI_STATUS.NOT_CONNECTED

                    End Select
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

                ' Check Display configuration.
                Try
                    Dim mClassMonitor As New ClassMonitor
                    Dim mPsvrMonitor As ClassMonitor.DEVMODE = Nothing
                    Select Case (mClassMonitor.FindPlaystationVrMonitor(mPsvrMonitor, Nothing))
                        Case ClassMonitor.ENUM_PSVR_MONITOR_STATUS.ERROR_MIRRROED
                            iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.MIRRROED

                        Case ClassMonitor.ENUM_PSVR_MONITOR_STATUS.ERROR_NOT_ACTIVE
                            iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.DISABLED

                        Case ClassMonitor.ENUM_PSVR_MONITOR_STATUS.ERROR_NOT_FOUND
                            iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.NOT_CONNECTED

                        Case ClassMonitor.ENUM_PSVR_MONITOR_STATUS.SUCCESS
                            Select Case (mClassMonitor.IsPlaystationVrMonitorPatched())
                                Case ClassMonitor.ENUM_PATCHED_RESGITRY_STATE.NOT_PATCHED
                                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.NOT_CONFIGURED

                                Case ClassMonitor.ENUM_PATCHED_RESGITRY_STATE.PATCHED_MULTI
                                    If (mPsvrMonitor.dmDeviceName Is Nothing) Then
                                        ' Monitor disabled?
                                        iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.GENERAL_ISSUE
                                    Else
                                        If (mPsvrMonitor.dmDisplayFrequency < 90) Then
                                            iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.BAD_FREQUENCY
                                        Else
                                            iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.CONFIGURED_MULTI
                                        End If
                                    End If

                                Case ClassMonitor.ENUM_PATCHED_RESGITRY_STATE.PATCHED_DIRECT
                                    If (mPsvrMonitor.dmDeviceName Is Nothing) Then
                                        ' Monitor disabled?
                                        iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.GENERAL_ISSUE
                                    Else
                                        iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.CONFIGURED_DIRECT
                                    End If

                                Case ClassMonitor.ENUM_PATCHED_RESGITRY_STATE.WAITING_FOR_RELOAD
                                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.WAITING_FOR_RELOAD

                                Case Else
                                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.GENERAL_ISSUE

                            End Select

                    End Select
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    iDisplayStatus = ENUM_DEVICE_DISPLAY_STATUS.GENERAL_ISSUE
                End Try

                ClassUtils.AsyncInvoke(Me, Sub() SetPlaystationVRStatus(iHdmiStatus, iUsbStatus, iDisplayStatus))

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

    Private Sub LinkLabel_InstallPSVR_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_InstallPSVRDrivers.LinkClicked
        g_FormMain.g_mUCStartPage.LinkLabel_InstallPSVRDrivers_Click()
    End Sub

    Private Sub LinkLabel_ConfigPSVRDisplay_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ConfigPSVRDisplay.LinkClicked
        g_FormMain.g_mUCStartPage.LinkLabel_ConfigPSVRDisplay_Click()
    End Sub

    Private Sub LinkLabel_UninstallPSVR_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_UninstallPSVR.LinkClicked
        g_FormMain.g_mUCStartPage.LinkLabel_UninstallPSVRDrivers_Click()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Using mForm As New FormDisplayFrequency
            mForm.ShowDialog(Me)
        End Using

        UpdateHardwareChangeStatusNow()
    End Sub

    Private Sub LinkLabel_DisplayDistortDebug_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_DisplayDistortDebug.LinkClicked
        Using mForm As New FormDisplayDistortCalibrator
            mForm.ShowDialog(Me)
        End Using
    End Sub
End Class
