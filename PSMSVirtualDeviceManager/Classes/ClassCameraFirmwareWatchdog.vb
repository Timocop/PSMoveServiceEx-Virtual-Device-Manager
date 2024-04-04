Public Class ClassCameraFirmwareWatchdog
    Implements IDisposable

    Private g_bInit As Boolean = False

    Public ReadOnly FIRM_PS4CAM_VIDEO As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation 4 Gen 1 Stereo Camera", "05A9", "0580", "05A9", "058A"),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation 4 Gen 2 Stereo Camera", "05A9", "0580", "05A9", "058B")
    }

    Private g_ClassUsbNotify As ClassDevicesNotify = Nothing

    Private g_mHardwareChangeStatusThread As Threading.Thread = Nothing
    Private g_bHardwareChangeStatusUpdatenNow As Boolean = False

    Private Shared _ThreadLock As New Object

    Structure STRUC_DEVICE_DRIVER_INFO
        Dim sName As String
        Dim VID As String
        Dim PID As String
        Dim VID_V As String
        Dim PID_V As String

        Public Sub New(_Name As String, _VID As String, _PID As String, _VID_V As String, _PID_V As String)
            sName = _Name
            VID = _VID
            PID = _PID
            VID_V = _VID_V
            PID_V = _PID_V
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
    End Structure

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
        g_mHardwareChangeStatusThread.IsBackground = True
        g_mHardwareChangeStatusThread.Start()
    End Sub

    Private Sub OnDeviceChanged(bConnected As Boolean, sDeviceName As String)
        If (Not bConnected) Then
            Return
        End If

        Dim bFound As Boolean = False

        For Each mDevice In FIRM_PS4CAM_VIDEO
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
        SyncLock _ThreadLock
            g_bHardwareChangeStatusUpdatenNow = True
        End SyncLock
    End Sub

    Private Sub HardwareChangeStatusThread()
        Try
            While True
                SyncLock _ThreadLock
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
        SyncLock _ThreadLock
            For Each mDevice In FIRM_PS4CAM_VIDEO
                Dim iTries As Integer = 10

                Dim mLibUsbDriver As New ClassLibusbDriver
                While (mLibUsbDriver.VerifyPlaystation4CamDriver64() AndAlso
                        mLibUsbDriver.IsUsbDeviceConnected(New ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO(Nothing, Nothing, mDevice.VID, mDevice.PID, Nothing, Nothing)))
                    mDevice.RunFirmwareUploader()

                    iTries -= 1

                    If (iTries < 1) Then
                        Throw New ArgumentException("Unable to upload firmware")
                    End If
                End While
            Next
        End SyncLock
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
