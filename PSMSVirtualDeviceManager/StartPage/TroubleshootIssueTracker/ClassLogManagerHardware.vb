Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerHardware
    Implements ILogAction

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork

        Dim sTrackersList As New Text.StringBuilder

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

        For Each mDevice In mUsbDevices
            For Each mProvider In mLibusbDriver.GetDeviceProvider(mDevice.Value.VID, mDevice.Value.PID, mDevice.Value.MM, "USB", "")
                Dim sVID As String = Nothing
                Dim sPID As String = Nothing
                Dim sMM As String = Nothing
                Dim sSerial As String = Nothing
                If (mLibusbDriver.ResolveHardwareID(mProvider.sDeviceID, sVID, sPID, sMM, sSerial)) Then
                    If (Not mLibusbDriver.IsUsbDeviceConnected(mDevice.Value.VID, mDevice.Value.PID, "USB", sSerial)) Then
                        Continue For
                    End If
                End If

                sTrackersList.AppendFormat("[{0}]", mProvider.sDeviceID).AppendLine()
                sTrackersList.AppendFormat("Name={0}", mDevice.Value.sName).AppendLine()
                sTrackersList.AppendFormat("Manufacture={0}", mDevice.Value.sManufacture).AppendLine()
                sTrackersList.AppendFormat("ExpectedService={0}", mDevice.Value.sService).AppendLine()

                sTrackersList.AppendFormat("DriverInfPath={0}", mProvider.sDriverInfPath).AppendLine()
                sTrackersList.AppendFormat("ProviderDescription={0}", mProvider.sProviderDescription).AppendLine()
                sTrackersList.AppendFormat("ProviderName={0}", mProvider.sProviderName).AppendLine()
                sTrackersList.AppendFormat("ProviderVersion={0}", mProvider.sProviderVersion).AppendLine()
                sTrackersList.AppendFormat("Service={0}", mProvider.sService).AppendLine()
                sTrackersList.AppendFormat("HasDriverInstalled={0}", mProvider.HasDriverInstalled).AppendLine()
                sTrackersList.AppendFormat("IsEnabled={0}", mProvider.IsEnabled).AppendLine()
                sTrackersList.AppendFormat("IsRemoved={0}", mProvider.IsRemoved).AppendLine()

                sTrackersList.AppendFormat("IsCorrectDrvierInstalled={0}", mProvider.sService.ToUpperInvariant = mDevice.Value.sService.ToUpperInvariant).AppendLine()

                sTrackersList.AppendLine()
            Next
        Next

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_CONNECTED_HARDWARE
    End Function

    Public Function GetIssues(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Throw New NotImplementedException()
    End Function
End Class
