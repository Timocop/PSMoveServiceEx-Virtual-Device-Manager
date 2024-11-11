Public Class ClassCameraFirmwareWatchdog
    Implements IDisposable

    Private g_bInit As Boolean = False

    Private g_ClassUsbNotify As ClassDevicesNotify = Nothing

    Private g_mHardwareChangeStatusThread As Threading.Thread = Nothing
    Private g_bHardwareChangeStatusUpdatenNow As Boolean = False

    Private g_mThreadLock As New Object

    Public Sub New()
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

        g_ClassUsbNotify = New ClassDevicesNotify()
        g_ClassUsbNotify.RegisterDeviceChange(ClassDevicesNotify.ENUM_DEVICE_TYPE.GUID_DEVINTERFACE_USB_DEVICE)

        AddHandler g_ClassUsbNotify.OnDeviceConnectionChanged, AddressOf OnDeviceChanged

        g_mHardwareChangeStatusThread = New Threading.Thread(AddressOf HardwareChangeStatusThread)
        g_mHardwareChangeStatusThread.Priority = Threading.ThreadPriority.Lowest
        g_mHardwareChangeStatusThread.IsBackground = True
        g_mHardwareChangeStatusThread.Start()
    End Sub

    Private Sub OnDeviceChanged(bConnected As Boolean, sDeviceName As String)
        If (Not bConnected) Then
            Return
        End If

        Dim bFound As Boolean = False

        For Each mDevice In ClassLibusbDriver.DRV_PS4CAM_WINUSB_CONFIGS
            Dim sHardwareId As String = String.Format("\USB#VID_{0}&PID_{1}", mDevice.VID, mDevice.PID)

            If (sDeviceName.ToLowerInvariant.Contains(sHardwareId.ToLowerInvariant)) Then
                bFound = True
                Exit For
            End If
        Next

        If (Not bFound) Then
            Return
        End If

        UpdateHardwareChangeStatusNow()
    End Sub

    Public Sub UpdateHardwareChangeStatusNow()
        SyncLock g_mThreadLock
            g_bHardwareChangeStatusUpdatenNow = True
        End SyncLock
    End Sub

    Private Sub HardwareChangeStatusThread()
        Try
            While True
                SyncLock g_mThreadLock
                    If (g_bHardwareChangeStatusUpdatenNow) Then
                        g_bHardwareChangeStatusUpdatenNow = False

                        Try
                            UploadFirmware()
                        Catch ex As Threading.ThreadAbortException
                            Throw
                        Catch ex As Exception
                            ' Whatever happens to the uploader
                        End Try
                    End If
                End SyncLock

                Threading.Thread.Sleep(1000)
            End While
        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
        End Try
    End Sub

    Public Sub UploadFirmware()
        SyncLock g_mThreadLock
            For Each mDevice In ClassLibusbDriver.DRV_PS4CAM_WINUSB_CONFIGS
                Dim iTries As Integer = 10

                Dim mLibUsbDriver As New ClassLibusbDriver
                While (True)
                    ' Dont run firmware uploader if we dont ahve any drivers installed
                    If (mLibUsbDriver.VerifyPlaystation4CamDriver64() <> ClassLibusbDriver.ENUM_DRIVER_STATE.INSTALLED) Then
                        Exit While
                    End If

                    ' We dont have to run the firmware uploader if 'USB Boot' device is not connected
                    If (Not mLibUsbDriver.IsUsbDeviceConnected(New ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO(Nothing, Nothing, mDevice.VID, mDevice.PID, Nothing, Nothing))) Then
                        Exit While
                    End If

                    RunFirmwareUploader()

                    If (iTries > 0) Then
                        iTries -= 1

                        Threading.Thread.Sleep(100)
                        Continue While
                    End If

                    Throw New ArgumentException("Unable to upload firmware")
                    Exit While
                End While
            Next
        End SyncLock
    End Sub

    Public Sub RunFirmwareUploader()
        Dim sRootFolder As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), ClassLibusbDriver.DRV_PS4CAM_ROOT_NAME)
        Dim sInstallerPath As String = IO.Path.Combine(sRootFolder, ClassLibusbDriver.DRV_PS4CAM_FIRMWARE_NAME)
        Dim sFirmwarePath As String = IO.Path.Combine(sRootFolder, ClassLibusbDriver.DRV_PS4CAM_FIRMWARE_BIN_NAME)

        If (Not IO.File.Exists(sFirmwarePath)) Then
            Throw New ArgumentException("Firmware file does not exist")
        End If

        Using mProcess As New Process
            mProcess.StartInfo.FileName = sInstallerPath
            mProcess.StartInfo.Arguments = ""
            mProcess.StartInfo.WorkingDirectory = sRootFolder
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True
            mProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            mProcess.Start()

            mProcess.WaitForExit(60000)
        End Using
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
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
