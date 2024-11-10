Imports System.Runtime.InteropServices
Imports System.Text
Imports Microsoft.Win32

Public Class ClassLibusbDriver
    Protected Class ClassWin32
        Public Const DIGCF_PRESENT As Integer = &H2
        Public Const DIGCF_ALLCLASSES As Integer = &H4
        Public Const SPDRP_HARDWAREID As Integer = &H1

        <StructLayout(LayoutKind.Sequential)>
        Public Class SP_DEVINFO_DATA
            Public cbSize As Integer
            Public ClassGuid As Guid
            Public DevInst As Integer
            Public Reserved As IntPtr
        End Class

        <DllImport("setupapi.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function SetupDiGetClassDevs(ByRef ClassGuid As Guid, Enumerator As IntPtr, hwndParent As IntPtr, Flags As Integer) As IntPtr
        End Function

        <DllImport("setupapi.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function SetupDiEnumDeviceInfo(DeviceInfoSet As IntPtr, MemberIndex As Integer, <MarshalAs(UnmanagedType.LPStruct)> DeviceInfoData As SP_DEVINFO_DATA) As Boolean
        End Function

        <DllImport("setupapi.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function SetupDiDestroyDeviceInfoList(DeviceInfoSet As IntPtr) As Boolean
        End Function

        <DllImport("setupapi.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function SetupDiGetDeviceRegistryProperty(DeviceInfoSet As IntPtr, <MarshalAs(UnmanagedType.LPStruct)> DeviceInfoData As SP_DEVINFO_DATA, Property_ As Integer, ByRef PropertyRegDataType As Integer, PropertyBuffer As StringBuilder, PropertyBufferSize As Integer, ByRef RequiredSize As Integer) As Boolean
        End Function

        <DllImport("setupapi.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function SetupDiGetDeviceInstanceId(DeviceInfoSet As IntPtr, <MarshalAs(UnmanagedType.LPStruct)> DeviceInfoData As SP_DEVINFO_DATA, DeviceInstanceId As StringBuilder, DeviceInstanceIdSize As Integer, ByRef RequiredSize As Integer) As Boolean
        End Function

        <DllImport("cfgmgr32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function CM_Get_Parent(ByRef pdnDevInst As Integer, dnDevInst As Integer, ulFlags As Integer) As Integer
        End Function

        <DllImport("cfgmgr32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function CM_Get_Device_ID(dnDevInst As Integer, Buffer As StringBuilder, BufferLen As Integer, ulFlags As Integer) As Integer
        End Function
    End Class

    Public Shared ReadOnly LIBUSB_SERVICE_NAME As String = "libusb0".ToUpperInvariant
    Public Shared ReadOnly WINUSB_SERVICE_NAME As String = "WinUSB".ToUpperInvariant
    Public Shared ReadOnly HID_SERVICE_NAME As String = "HidUsb".ToUpperInvariant
    Public Shared ReadOnly USBCTRL_SERVICE_NAME As String = "usbccgp".ToUpperInvariant
    Public Shared ReadOnly USBVIDEO_SERVICE_NAME As String = "usbvideo".ToUpperInvariant
    Public Shared ReadOnly USBAUDIO_SERVICE_NAME As String = "usbaudio".ToUpperInvariant
    Public Shared ReadOnly BTHUSB_SERVICE_NAME As String = "BTHUSB".ToUpperInvariant
    Public Shared ReadOnly USBHUB3_SERVICE_NAME As String = "USBHUB3".ToUpperInvariant
    Public Shared ReadOnly USBXHCI_SERVICE_NAME As String = "USBXHCI".ToUpperInvariant
    Public Shared ReadOnly USBEHCI_SERVICE_NAME As String = "USBEHCI".ToUpperInvariant
    Public Shared ReadOnly USBOHCI_SERVICE_NAME As String = "USBOHCI".ToUpperInvariant
    Public Shared ReadOnly USBUHCI_SERVICE_NAME As String = "USBUHCI".ToUpperInvariant

    Public Shared ReadOnly DRV_WDI_ROOT_NAME As String = "libusb_driver"
    Public Shared ReadOnly DRV_WDI_INSTALLER_NAME As String = "wdi-simple.exe"
    Public Shared ReadOnly DRV_PS4CAM_ROOT_NAME As String = "ps4cam_driver"

    Public Shared ReadOnly DRV_PS4CAM_INSTALLER_NAME As String = "InstallDriver.exe"
    Public Shared ReadOnly DRV_PS4CAM_FIRMWARE_NAME As String = "FirmwareLoader.exe"
    Public Shared ReadOnly DRV_PS4CAM_FIRMWARE_BIN_NAME As String = "firmware.bin"

    Public Shared ReadOnly DRV_PS4CAM_KNOWN_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation 4 Stereo Camera", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "05A9", "0580", Nothing, WINUSB_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation 4 (Gen 1) Stereo Camera (Composite Device)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "05A9", "058A", Nothing, USBCTRL_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation 4 (Gen 1) Stereo Camera Video (Interface 0)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "05A9", "058A", "00", USBVIDEO_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation 4 (Gen 2) Stereo Camera (Composite Device)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "05A9", "058B", Nothing, USBCTRL_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation 4 (Gen 2) Stereo Camera Video (Interface 0)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "05A9", "058B", "00", USBVIDEO_SERVICE_NAME)
    }
    Public Shared ReadOnly DRV_PS4CAM_WINUSB_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation 4 Stereo Camera", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "05A9", "0580", Nothing, WINUSB_SERVICE_NAME, ENUM_WDI_DRIVERTYPE.WINUSBK)
    }


    Public Shared ReadOnly DRV_PSEYE_KNOWN_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation Eye Camera (Composite Device)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", Nothing, USBCTRL_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation Eye Camera Video (Interface 0)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", "00", LIBUSB_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation Eye Camera Audio (Interface 1)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", "01", USBAUDIO_SERVICE_NAME)
    }
    Public Shared ReadOnly DRV_PSEYE_LIBUSB_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation Eye Camera Video", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", "00", LIBUSB_SERVICE_NAME, ENUM_WDI_DRIVERTYPE.LIBUSB)
    }


    Public Shared ReadOnly DRV_PSVR_KNOWN_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR (Composite Device)", "Sony Corp.", "054C", "09AF", Nothing, USBCTRL_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR 3D Audio (Interface 0)", "Sony Corp.", "054C", "09AF", "00", LIBUSB_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Audio Device (Interface 1)", "Sony Corp.", "054C", "09AF", "01", USBAUDIO_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Sensor (Interface 4)", "Sony Corp.", "054C", "09AF", "04", HID_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Control (Interface 5)", "Sony Corp.", "054C", "09AF", "05", LIBUSB_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR H.264 (Interface 6)", "Sony Corp.", "054C", "09AF", "06", LIBUSB_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR BulkIn (Interface 7)", "Sony Corp.", "054C", "09AF", "07", LIBUSB_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Input Device (Interface 8)", "Sony Corp.", "054C", "09AF", "08", HID_SERVICE_NAME)
    }
    Public Shared ReadOnly DRV_PSVR_LIBUSB_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR 3D Audio", "Sony Corp.", "054C", "09AF", "00", LIBUSB_SERVICE_NAME, ENUM_WDI_DRIVERTYPE.LIBUSB), ' Just add a driver so it does not show as hardware issue.
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Control", "Sony Corp.", "054C", "09AF", "05", LIBUSB_SERVICE_NAME, ENUM_WDI_DRIVERTYPE.LIBUSB),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR H.264", "Sony Corp.", "054C", "09AF", "06", LIBUSB_SERVICE_NAME, ENUM_WDI_DRIVERTYPE.LIBUSB), ' Just add a driver so it does not show as hardware issue.
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR BulkIn", "Sony Corp.", "054C", "09AF", "07", LIBUSB_SERVICE_NAME, ENUM_WDI_DRIVERTYPE.LIBUSB) ' Just add a driver so it does not show as hardware issue.
    }
    Public Shared ReadOnly DRV_PSVR_HID_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Sensor", "Sony Corp.", "054C", "09AF", "04", HID_SERVICE_NAME)
    }


    Public Shared ReadOnly DRV_PSMOVE_KNOWN_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation 3 Motion Controller", "Sony Corp.", "054C", "03D5", Nothing, HID_SERVICE_NAME),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation 4 Motion Controller", "Sony Corp.", "054C", "0C5E", Nothing, HID_SERVICE_NAME)
    }


    Public Shared ReadOnly DRV_CONTROLLER_KNOWN_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation Controller (Composite Device)", "Sony Corp.", "1D6B", "0104", Nothing, USBCTRL_SERVICE_NAME) ' PSNavi / DualShock3 (via RaspberryPi Multifunction Composite Device)
    }


    Public Shared ReadOnly DRV_DUALSHOCK_KNOWN_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation DualShock 4 Controller", "Sony Corp.", "054C", "05C4", Nothing, HID_SERVICE_NAME)
    }

    Enum ENUM_WDI_DRIVERTYPE
        WINUSB = 0
        LIBUSB = 1
        LIBUSBK = 2
        USBSER = 3
        WINUSBK = 4
        CUSTOM = 5
    End Enum

    Enum ENUM_WDI_ERROR
        WDI_SUCCESS = 0
        WDI_ERROR_IO = -1
        WDI_ERROR_INVALID_PARAM = -2
        WDI_ERROR_ACCESS = -3
        WDI_ERROR_NO_DEVICE = -4
        WDI_ERROR_NOT_FOUND = -5
        WDI_ERROR_BUSY = -6
        WDI_ERROR_TIMEOUT = -7
        WDI_ERROR_OVERFLOW = -8
        WDI_ERROR_PENDING_INSTALLATION = -9
        WDI_ERROR_INTERRUPTED = -10
        WDI_ERROR_RESOURCE = -11
        WDI_ERROR_NOT_SUPPORTED = -12
        WDI_ERROR_EXISTS = -13
        WDI_ERROR_USER_CANCEL = -14
        WDI_ERROR_NEEDS_ADMIN = -15
        WDI_ERROR_WOW64 = -16
        WDI_ERROR_INF_SYNTAX = -17
        WDI_ERROR_CAT_MISSING = -18
        WDI_ERROR_UNSIGNED = -19
        WDI_ERROR_OTHER = -99
    End Enum

    <Flags>
    Public Enum DEVICE_CONFIG_FLAGS As Integer
        ' Device is disabled
        CONFIGFLAG_DISABLED = &H1

        ' Device has been removed or uninstalled
        CONFIGFLAG_REMOVED = &H2

        ' Device was manually installed
        CONFIGFLAG_MANUAL_INSTALL = &H4

        ' Ignore boot configuration for the device
        CONFIGFLAG_IGNORE_BOOT_LC = &H8

        ' Device is used for network booting
        CONFIGFLAG_NET_BOOT = &H10

        ' Device should be reinstalled
        CONFIGFLAG_REINSTALL = &H20

        ' Device installation has failed
        CONFIGFLAG_FAILEDINSTALL = &H40

        ' Device can't be stopped because it has child devices
        CONFIGFLAG_CANTSTOPACHILD = &H80

        ' It is safe to remove the device's ROM
        CONFIGFLAG_OKREMOVEROM = &H100

        ' No "remove" action should be taken when the device is removed
        CONFIGFLAG_NOREMOVEEXIT = &H200

        ' Device is disabled in the current hardware profile
        CONFIGFLAG_HWPROFILE_DISABLED = &H800000

        ' Device is enabled in the current hardware profile
        CONFIGFLAG_HWPROFILE_ENABLED = &H400000

        ' The system has attempted to restart the device
        CONFIGFLAG_RESTART_REQUIRED = &H200000
    End Enum

    Class STRUC_DEVICETREE_ITEM
        Public sDeviceID As String = Nothing
        Public sDeviceVID As String = Nothing
        Public sDevicePID As String = Nothing
        Public sDeviceMM As String = Nothing
        Public sDeviceSerial As String = Nothing
        Public sInterface As String = Nothing
        Public mProvider As STRUC_DEVICE_PROVIDER
        Public mParentDevices As STRUC_DEVICE_PROVIDER()

        Public Sub New(_DeviceID As String, _DeviceVID As String, _DevicePID As String, _DeviceMM As String, _DeviceSerial As String, _Interface As String)
            sDeviceID = _DeviceID
            sDeviceVID = _DeviceVID
            sDevicePID = _DevicePID
            sDeviceMM = _DeviceMM
            sDeviceSerial = _DeviceSerial
            sInterface = _Interface

            mProvider = New STRUC_DEVICE_PROVIDER()
        End Sub

        Public Sub New(_Item As STRUC_DEVICETREE_ITEM)
            sDeviceID = _Item.sDeviceID
            sDeviceVID = _Item.sDeviceVID
            sDevicePID = _Item.sDevicePID
            sDeviceSerial = _Item.sDeviceSerial

            mProvider = _Item.mProvider
            mParentDevices = _Item.mParentDevices
        End Sub
    End Class

    Structure STRUC_DEVICE_PROVIDER
        Dim sDeviceID As String
        Dim iConfigFlags As DEVICE_CONFIG_FLAGS
        Dim sService As String
        Dim sProviderName As String
        Dim sProviderDescription As String
        Dim sProviderVersion As String
        Dim sDriverInfPath As String

        Public Function IsEnabled() As Boolean
            Return ((iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_DISABLED) = 0)
        End Function

        Public Function IsRemoved() As Boolean
            Return ((iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_REMOVED) <> 0)
        End Function

        Public Function HasDriverInstalled() As Boolean
            Return (Not String.IsNullOrEmpty(sService))
        End Function
    End Structure

    Structure STRUC_DEVICE_DRIVER_INFO
        Dim sName As String
        Dim sManufacture As String
        Dim VID As String
        Dim PID As String
        Dim MM As String
        Dim iDriver As ENUM_WDI_DRIVERTYPE
        Dim sService As String

        Public Sub New(_Name As String, _Manufacture As String, _VID As String, _PID As String, _MM As String, _Service As String)
            sName = _Name
            sManufacture = _Manufacture
            VID = _VID
            PID = _PID
            MM = _MM
            sService = _Service
            iDriver = ENUM_WDI_DRIVERTYPE.LIBUSB
        End Sub

        Public Sub New(_Name As String, _Manufacture As String, _VID As String, _PID As String, _MM As String, _Service As String, _Driver As ENUM_WDI_DRIVERTYPE)
            sName = _Name
            sManufacture = _Manufacture
            VID = _VID
            PID = _PID
            MM = _MM
            sService = _Service
            iDriver = _Driver
        End Sub

        Public Function CreateCommandLine() As String
            Dim sCmd As New List(Of String)
            sCmd.Add(String.Format("-n ""{0}""", sName))
            sCmd.Add(String.Format("-f ""{0}.inf""", sName))
            sCmd.Add(String.Format("-m ""{0}""", sManufacture))
            sCmd.Add(String.Format("-v ""{0}""", Convert.ToInt32(VID, 16)))
            sCmd.Add(String.Format("-p ""{0}""", Convert.ToInt32(PID, 16)))

            If (MM IsNot Nothing) Then
                sCmd.Add(String.Format("-i ""{0}""", Convert.ToInt32(MM, 16)))
            End If

            sCmd.Add(String.Format("-t ""{0}""", CInt(iDriver)))

            Dim sRootFolder As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), DRV_WDI_ROOT_NAME)
            sCmd.Add(String.Format("-e ""{0}.inf""", IO.Path.Combine(sRootFolder, sName)))

            Return String.Join(" ", sCmd.ToArray)
        End Function

    End Structure

    Public Sub New()
    End Sub

    Public Function InstallPlaystation4CamDriver64() As ENUM_WDI_ERROR
        For Each mConfig As STRUC_DEVICE_DRIVER_INFO In DRV_PS4CAM_WINUSB_CONFIGS
            'Remove device first so we speed up the driver installation using WDI
            For Each mUsbInfo In GetDeviceProviderUSB(mConfig)
                RemoveDevice(mUsbInfo.sDeviceID, False)
            Next

            Dim iExitCode As ENUM_WDI_ERROR = InternalInstallDriver64(mConfig)
            If (iExitCode <> ENUM_WDI_ERROR.WDI_SUCCESS) Then
                Return iExitCode
            End If
        Next

        Return ENUM_WDI_ERROR.WDI_SUCCESS
    End Function

    Public Function InstallPlaystationEyeDriver64() As ENUM_WDI_ERROR
        For Each mConfig As STRUC_DEVICE_DRIVER_INFO In DRV_PSEYE_LIBUSB_CONFIGS
            'Remove device first so we speed up the driver installation using WDI
            For Each mUsbInfo In GetDeviceProviderUSB(mConfig)
                RemoveDevice(mUsbInfo.sDeviceID, False)
            Next

            Dim iExitCode As ENUM_WDI_ERROR = InternalInstallDriver64(mConfig)
            If (iExitCode <> ENUM_WDI_ERROR.WDI_SUCCESS) Then
                Return iExitCode
            End If
        Next

        Return ENUM_WDI_ERROR.WDI_SUCCESS
    End Function

    Public Function InstallPlaystationVrDrvier64() As ENUM_WDI_ERROR
        For Each mConfig As STRUC_DEVICE_DRIVER_INFO In DRV_PSVR_LIBUSB_CONFIGS
            'Remove device first so we speed up the driver installation using WDI
            For Each mUsbInfo In GetDeviceProviderUSB(mConfig)
                RemoveDevice(mUsbInfo.sDeviceID, False)
            Next

            Dim iExitCode As ENUM_WDI_ERROR = InternalInstallDriver64(mConfig)
            If (iExitCode <> ENUM_WDI_ERROR.WDI_SUCCESS) Then
                Return iExitCode
            End If
        Next

        Return ENUM_WDI_ERROR.WDI_SUCCESS
    End Function

    Public Sub UninstallPlaystation4CamDriver64()
        Dim bScanNewDevices As Boolean = False

        Dim sDevicesToRemove As New List(Of String)

        For Each mInfo In DRV_PS4CAM_WINUSB_CONFIGS
            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                ' Dont allow anything else than non-system drivers past here!
                If (Not String.IsNullOrEmpty(mUsbInfo.sDriverInfPath) AndAlso mUsbInfo.sDriverInfPath.ToLowerInvariant.StartsWith("oem")) Then
                    RemoveDriver(mUsbInfo.sDriverInfPath)
                End If

                sDevicesToRemove.Add(mUsbInfo.sDeviceID)
            Next
        Next

        For Each mInfo In DRV_PS4CAM_KNOWN_CONFIGS
            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                ' Dont allow anything else than non-system drivers past here!
                If (Not String.IsNullOrEmpty(mUsbInfo.sDriverInfPath) AndAlso mUsbInfo.sDriverInfPath.ToLowerInvariant.StartsWith("oem")) Then
                    RemoveDriver(mUsbInfo.sDriverInfPath)
                End If

                sDevicesToRemove.Add(mUsbInfo.sDeviceID)
            Next
        Next

        ' Remove devices after everything is done.
        For Each mDeviceID As String In sDevicesToRemove
            RemoveDevice(mDeviceID, True)
            bScanNewDevices = True
        Next

        If (bScanNewDevices) Then
            ScanDevices()
        End If
    End Sub

    Public Sub UninstallPlaystationEyeDriver64()
        Dim bScanNewDevices As Boolean = False

        Dim sDevicesToRemove As New List(Of String)

        For Each mInfo In DRV_PSEYE_KNOWN_CONFIGS
            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                ' Dont allow anything else than non-system drivers past here!
                If (Not String.IsNullOrEmpty(mUsbInfo.sDriverInfPath) AndAlso mUsbInfo.sDriverInfPath.ToLowerInvariant.StartsWith("oem")) Then
                    RemoveDriver(mUsbInfo.sDriverInfPath)
                End If

                sDevicesToRemove.Add(mUsbInfo.sDeviceID)
            Next
        Next

        ' Remove devices after everything is done.
        For Each mDeviceID As String In sDevicesToRemove
            RemoveDevice(mDeviceID, True)
            bScanNewDevices = True
        Next

        If (bScanNewDevices) Then
            ScanDevices()
        End If
    End Sub

    Public Sub UninstallPlaystationVrDriver64()
        Dim bScanNewDevices As Boolean = False
        Dim sDevicesToRemove As New List(Of String)

        For Each mInfo In DRV_PSVR_KNOWN_CONFIGS
            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                ' Dont allow anything else than non-system drivers past here!
                If (Not String.IsNullOrEmpty(mUsbInfo.sDriverInfPath) AndAlso mUsbInfo.sDriverInfPath.ToLowerInvariant.StartsWith("oem")) Then
                    RemoveDriver(mUsbInfo.sDriverInfPath)
                End If

                sDevicesToRemove.Add(mUsbInfo.sDeviceID)
            Next
        Next

        ' Remove conflicting drivers (e.g libusb on hid).
        For Each mInfo In DRV_PSVR_HID_CONFIGS
            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                ' Dont allow anything else than non-system drivers past here!
                If (Not String.IsNullOrEmpty(mUsbInfo.sDriverInfPath) AndAlso mUsbInfo.sDriverInfPath.ToLowerInvariant.StartsWith("oem")) Then
                    RemoveDriver(mUsbInfo.sDriverInfPath)
                End If

                sDevicesToRemove.Add(mUsbInfo.sDeviceID)
            Next
        Next

        ' Remove devices after everything is done.
        For Each mDeviceID As String In sDevicesToRemove
            RemoveDevice(mDeviceID, True)
            bScanNewDevices = True
        Next

        If (bScanNewDevices) Then
            ScanDevices()
        End If
    End Sub

    Public Function VerifyPlaystation4CamDriver64() As Boolean
        Dim bDriversInstalled As Boolean = True

        For Each mInfo In DRV_PS4CAM_WINUSB_CONFIGS
            Dim bDeviceRegistered As Boolean = False

            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                bDeviceRegistered = True

                If ((mUsbInfo.iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_FAILEDINSTALL) <> 0) Then
                    If ((mUsbInfo.iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_REINSTALL) <> 0) Then
                        Continue For
                    End If
                Else
                    If (mUsbInfo.iConfigFlags <> 0) Then
                        Continue For
                    End If
                End If

                If (Not mUsbInfo.HasDriverInstalled()) Then
                    bDriversInstalled = False
                Else
                    If (String.IsNullOrEmpty(mUsbInfo.sService) AndAlso String.IsNullOrEmpty(mInfo.sService)) Then
                        Continue For
                    End If

                    If (Not String.IsNullOrEmpty(mUsbInfo.sService) AndAlso Not String.IsNullOrEmpty(mInfo.sService)) Then
                        If (mUsbInfo.sService.ToUpperInvariant = mInfo.sService.ToUpperInvariant) Then
                            Continue For
                        End If
                    End If

                    bDriversInstalled = False
                End If
            Next

            If (Not bDeviceRegistered) Then
                bDriversInstalled = False
            End If
        Next

        Return bDriversInstalled
    End Function

    Public Function VerifyPlaystation4CamVideoDriver64() As Boolean
        Dim bDriversInstalled As Boolean = True

        For Each mInfo In DRV_PS4CAM_KNOWN_CONFIGS
            Dim bDeviceRegistered As Boolean = False

            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                bDeviceRegistered = True

                If ((mUsbInfo.iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_FAILEDINSTALL) <> 0) Then
                    If ((mUsbInfo.iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_REINSTALL) <> 0) Then
                        Continue For
                    End If
                Else
                    If (mUsbInfo.iConfigFlags <> 0) Then
                        Continue For
                    End If
                End If

                If (Not mUsbInfo.HasDriverInstalled()) Then
                    bDriversInstalled = False
                Else
                    If (String.IsNullOrEmpty(mUsbInfo.sService) AndAlso String.IsNullOrEmpty(mInfo.sService)) Then
                        Continue For
                    End If

                    If (Not String.IsNullOrEmpty(mUsbInfo.sService) AndAlso Not String.IsNullOrEmpty(mInfo.sService)) Then
                        If (mUsbInfo.sService.ToUpperInvariant = mInfo.sService.ToUpperInvariant) Then
                            Continue For
                        End If
                    End If

                    bDriversInstalled = False
                End If
            Next

            If (Not bDeviceRegistered) Then
                bDriversInstalled = False
            End If
        Next

        Return bDriversInstalled
    End Function

    Public Function VerifyPlaystationEyeDriver64() As Boolean
        Dim bDriversInstalled As Boolean = True

        For Each mInfo In DRV_PSEYE_LIBUSB_CONFIGS
            Dim bDeviceRegistered As Boolean = False

            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                bDeviceRegistered = True

                If ((mUsbInfo.iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_FAILEDINSTALL) <> 0) Then
                    If ((mUsbInfo.iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_REINSTALL) <> 0) Then
                        Continue For
                    End If
                Else
                    If (mUsbInfo.iConfigFlags <> 0) Then
                        Continue For
                    End If
                End If

                If (Not mUsbInfo.HasDriverInstalled()) Then
                    bDriversInstalled = False
                Else
                    If (String.IsNullOrEmpty(mUsbInfo.sService) AndAlso String.IsNullOrEmpty(mInfo.sService)) Then
                        Continue For
                    End If

                    If (Not String.IsNullOrEmpty(mUsbInfo.sService) AndAlso Not String.IsNullOrEmpty(mInfo.sService)) Then
                        If (mUsbInfo.sService.ToUpperInvariant = mInfo.sService.ToUpperInvariant) Then
                            Continue For
                        End If
                    End If

                    bDriversInstalled = False
                End If
            Next

            If (Not bDeviceRegistered) Then
                bDriversInstalled = False
            End If
        Next

        Return bDriversInstalled
    End Function

    Public Function VerifyPlaystationVrDriver64() As Boolean
        Dim bDriversInstalled As Boolean = True
        Dim bHidInstalled As Boolean = True

        For Each mInfo In DRV_PSVR_LIBUSB_CONFIGS
            Dim bDeviceRegistered As Boolean = False

            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                bDeviceRegistered = True

                If ((mUsbInfo.iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_FAILEDINSTALL) <> 0) Then
                    If ((mUsbInfo.iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_REINSTALL) <> 0) Then
                        Continue For
                    End If
                Else
                    If (mUsbInfo.iConfigFlags <> 0) Then
                        Continue For
                    End If
                End If

                If (Not mUsbInfo.HasDriverInstalled()) Then
                    bDriversInstalled = False
                Else
                    If (String.IsNullOrEmpty(mUsbInfo.sService) AndAlso String.IsNullOrEmpty(mInfo.sService)) Then
                        Continue For
                    End If

                    If (Not String.IsNullOrEmpty(mUsbInfo.sService) AndAlso Not String.IsNullOrEmpty(mInfo.sService)) Then
                        If (mUsbInfo.sService.ToUpperInvariant = mInfo.sService.ToUpperInvariant) Then
                            Continue For
                        End If
                    End If

                    bDriversInstalled = False
                End If
            Next

            If (Not bDeviceRegistered) Then
                bDriversInstalled = False
            End If
        Next

        For Each mInfo In DRV_PSVR_HID_CONFIGS
            Dim bDeviceRegistered As Boolean = False

            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                bDeviceRegistered = True

                If ((mUsbInfo.iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_FAILEDINSTALL) <> 0) Then
                    If ((mUsbInfo.iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_REINSTALL) <> 0) Then
                        Continue For
                    End If
                Else
                    If (mUsbInfo.iConfigFlags <> 0) Then
                        Continue For
                    End If
                End If

                If (Not mUsbInfo.HasDriverInstalled()) Then
                    bHidInstalled = False
                Else
                    If (String.IsNullOrEmpty(mUsbInfo.sService) AndAlso String.IsNullOrEmpty(mInfo.sService)) Then
                        Continue For
                    End If

                    If (Not String.IsNullOrEmpty(mUsbInfo.sService) AndAlso Not String.IsNullOrEmpty(mInfo.sService)) Then
                        If (mUsbInfo.sService.ToUpperInvariant = mInfo.sService.ToUpperInvariant) Then
                            Continue For
                        End If
                    End If

                    bHidInstalled = False
                End If
            Next

            If (Not bDeviceRegistered) Then
                bDriversInstalled = False
            End If
        Next
        Return (bDriversInstalled AndAlso bHidInstalled)
    End Function

    Private Function InternalInstallDriver64(mInfo As STRUC_DEVICE_DRIVER_INFO) As ENUM_WDI_ERROR
        Dim sRootFolder As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), DRV_WDI_ROOT_NAME)
        Dim sInstallerPath As String = IO.Path.Combine(sRootFolder, DRV_WDI_INSTALLER_NAME)

        Using mProcess As New Process
            mProcess.StartInfo.FileName = sInstallerPath
            mProcess.StartInfo.Arguments = mInfo.CreateCommandLine()
            mProcess.StartInfo.WorkingDirectory = sRootFolder
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True
            mProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()

            mProcess.WaitForExit()

            Return CType(mProcess.ExitCode, ENUM_WDI_ERROR)
        End Using
    End Function

    Public Function ScanDevices() As Integer
        Using mProcess As New Process
            mProcess.StartInfo.FileName = "pnputil.exe"
            mProcess.StartInfo.Arguments = "/scan-devices"
            mProcess.StartInfo.WorkingDirectory = Environment.SystemDirectory
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True
            mProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()

            mProcess.WaitForExit()

            Return mProcess.ExitCode
        End Using
    End Function

    Public Function DeviceSetState(sInstanceID As String, bEnable As Boolean) As Integer
        Using mProcess As New Process
            mProcess.StartInfo.FileName = "pnputil.exe"

            If (bEnable) Then
                mProcess.StartInfo.Arguments = String.Format("/enable-device ""{0}""", sInstanceID)
            Else
                mProcess.StartInfo.Arguments = String.Format("/disable-device ""{0}""", sInstanceID)
            End If

            mProcess.StartInfo.WorkingDirectory = Environment.SystemDirectory
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True
            mProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()

            mProcess.WaitForExit()

            Return mProcess.ExitCode
        End Using
    End Function

    Public Function RestartDevice(sInstanceID As String) As Integer
        Using mProcess As New Process
            mProcess.StartInfo.FileName = "pnputil.exe"
            mProcess.StartInfo.Arguments = String.Format("/restart-device ""{0}""", sInstanceID)
            mProcess.StartInfo.WorkingDirectory = Environment.SystemDirectory
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True
            mProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()

            mProcess.WaitForExit()

            Return mProcess.ExitCode
        End Using
    End Function

    Public Function RemoveDevice(sInstanceID As String, bRemoveSubInstances As Boolean) As Integer
        Using mProcess As New Process
            mProcess.StartInfo.FileName = "pnputil.exe"

            If (bRemoveSubInstances) Then
                mProcess.StartInfo.Arguments = String.Format("/remove-device ""{0}"" /subtree", sInstanceID)
            Else
                mProcess.StartInfo.Arguments = String.Format("/remove-device ""{0}""", sInstanceID)
            End If

            mProcess.StartInfo.WorkingDirectory = Environment.SystemDirectory
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True
            mProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()

            mProcess.WaitForExit()

            Return mProcess.ExitCode
        End Using
    End Function

    Public Function RemoveDriver(sInfFile As String) As Integer
        'We dont want anything else removed
        If (Not sInfFile.ToLowerInvariant.StartsWith("oem")) Then
            Throw New ArgumentException(String.Format("Unable to remove driver '{0}'. Driver is system driver.", sInfFile))
        End If

        Using mProcess As New Process
            mProcess.StartInfo.FileName = "pnputil.exe"
            mProcess.StartInfo.Arguments = String.Format("/delete-driver {0} /force", sInfFile)
            mProcess.StartInfo.WorkingDirectory = Environment.SystemDirectory
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True
            mProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()

            mProcess.WaitForExit()

            Return mProcess.ExitCode
        End Using
    End Function

    Public Function GetDeviceProviderUSB(mInfo As STRUC_DEVICE_DRIVER_INFO) As STRUC_DEVICE_PROVIDER()
        Return InternalGetDeviceProvider(mInfo.VID, mInfo.PID, mInfo.MM, "USB", Nothing)
    End Function

    Public Function GetDeviceProviderUSB(sVID As String, sPID As String, sMI As String) As STRUC_DEVICE_PROVIDER()
        Return InternalGetDeviceProvider(sVID, sPID, sMI, "USB", Nothing)
    End Function

    Public Function GetDeviceProviderHID(mInfo As STRUC_DEVICE_DRIVER_INFO) As STRUC_DEVICE_PROVIDER()
        Return InternalGetDeviceProvider(mInfo.VID, mInfo.PID, mInfo.MM, "HID", Nothing)
    End Function

    Public Function GetDeviceProviderHID(sVID As String, sPID As String, sMI As String) As STRUC_DEVICE_PROVIDER()
        Return InternalGetDeviceProvider(sVID, sPID, sMI, "HID", Nothing)
    End Function

    Public Function GetDeviceProviderDISPLAY(sSerial As String) As STRUC_DEVICE_PROVIDER()
        Return InternalGetDeviceProvider(sSerial, Nothing, Nothing, "DISPLAY", Nothing)
    End Function

    Public Function GetDeviceProvider(sVID As String, sPID As String, sMI As String, sInterface As String, sSerial As String) As STRUC_DEVICE_PROVIDER()
        Return InternalGetDeviceProvider(sVID, sPID, sMI, sInterface, sSerial)
    End Function

    Private Function InternalGetDeviceProvider(sVID As String, sPID As String, sMI As String, sInterface As String, sSerial As String) As STRUC_DEVICE_PROVIDER()
        Dim mProviderList As New List(Of STRUC_DEVICE_PROVIDER)

        Dim sUseDevice As String = Nothing
        Select Case (sInterface)
            Case "DISPLAY"
                sUseDevice = String.Format("{0}\{1}", sInterface, sVID)

            Case Else
                If (String.IsNullOrEmpty(sMI)) Then
                    sUseDevice = String.Format("{0}\VID_{1}&PID_{2}", sInterface, sVID, sPID)
                Else
                    If (sMI.Length = 1) Then
                        sMI = "0" & sMI
                    End If

                    sUseDevice = String.Format("{0}\VID_{1}&PID_{2}&MI_{3}", sInterface, sVID, sPID, sMI)
                End If
        End Select



        Dim mUsbDeviceKey As RegistryKey = Registry.LocalMachine.OpenSubKey(String.Format("SYSTEM\CurrentControlSet\Enum\{0}", sUseDevice), False)
        If (mUsbDeviceKey Is Nothing) Then
            Return mProviderList.ToArray
        End If

        For Each mDeviceSerialName As String In mUsbDeviceKey.GetSubKeyNames()
            Dim mDeviceSerialKey As RegistryKey = mUsbDeviceKey.OpenSubKey(mDeviceSerialName)
            If (mDeviceSerialKey Is Nothing) Then
                Continue For
            End If

            If (Not String.IsNullOrEmpty(sSerial)) Then
                If (Not mDeviceSerialName.ToUpperInvariant.EndsWith(sSerial.ToUpperInvariant)) Then
                    Continue For
                End If
            End If

            Dim sDeviceID As String = String.Format("{0}\{1}", sUseDevice, mDeviceSerialName)

            Dim sDriverLocation As String = TryCast(mDeviceSerialKey.GetValue("Driver"), String)
            Dim sService As String = TryCast(mDeviceSerialKey.GetValue("Service"), String)
            Dim sConfigFlags As DEVICE_CONFIG_FLAGS = CType(mDeviceSerialKey.GetValue("ConfigFlags", 0), DEVICE_CONFIG_FLAGS)

            If (sDriverLocation IsNot Nothing) Then
                Dim mDriverKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Class\" & sDriverLocation, False)
                If (mDriverKey Is Nothing) Then
                    Continue For
                End If

                Dim sProviderName As String = TryCast(mDriverKey.GetValue("ProviderName"), String)
                Dim sProviderVersion As String = TryCast(mDriverKey.GetValue("DriverVersion"), String)
                Dim sProviderDescription As String = TryCast(mDriverKey.GetValue("DriverDesc"), String)
                Dim sDriverInfPath As String = TryCast(mDriverKey.GetValue("InfPath"), String)

                Dim mInfo As New STRUC_DEVICE_PROVIDER
                mInfo.sDeviceID = sDeviceID
                mInfo.iConfigFlags = sConfigFlags
                mInfo.sService = sService
                mInfo.sProviderName = sProviderName
                mInfo.sProviderVersion = sProviderVersion
                mInfo.sProviderDescription = sProviderDescription
                mInfo.sDriverInfPath = sDriverInfPath

                mProviderList.Add(mInfo)
            Else
                ' No driver installed
                Dim mInfo As New STRUC_DEVICE_PROVIDER
                mInfo.sDeviceID = sDeviceID
                mInfo.iConfigFlags = sConfigFlags
                mInfo.sService = sService
                mInfo.sProviderName = Nothing
                mInfo.sProviderVersion = Nothing
                mInfo.sProviderDescription = Nothing
                mInfo.sDriverInfPath = Nothing

                mProviderList.Add(mInfo)
            End If
        Next

        Return mProviderList.ToArray
    End Function

    Public Function IsPlaystationVrUsbDeviceConnected() As Boolean
        For Each mItem In DRV_PSVR_HID_CONFIGS
            Return IsUsbDeviceConnected(mItem)
        Next

        Return False
    End Function

    Public Function IsPlaystationEyeUsbDeviceConnected() As Boolean
        For Each mItem In DRV_PSEYE_LIBUSB_CONFIGS
            Return IsUsbDeviceConnected(mItem)
        Next

        Return False
    End Function

    Public Function IsUsbDeviceConnected(mInfo As STRUC_DEVICE_DRIVER_INFO) As Boolean
        Return IsUsbDeviceConnected(mInfo.VID, mInfo.PID, "USB", Nothing)
    End Function

    Public Function GetAllDevices(sInterface As String) As STRUC_DEVICE_PROVIDER()
        Dim mProviderList As New List(Of STRUC_DEVICE_PROVIDER)

        Dim mDeviceEnumKey As RegistryKey = Registry.LocalMachine.OpenSubKey(String.Format("SYSTEM\CurrentControlSet\Enum\{0}", sInterface), False)
        If (mDeviceEnumKey Is Nothing) Then
            Return mProviderList.ToArray
        End If

        For Each sDeviceName As String In mDeviceEnumKey.GetSubKeyNames()
            Dim mDeviceNameKey As RegistryKey = mDeviceEnumKey.OpenSubKey(sDeviceName, False)
            If (mDeviceNameKey Is Nothing) Then
                Continue For
            End If

            For Each sDeviceSerialName As String In mDeviceNameKey.GetSubKeyNames()
                Dim mDeviceSerialKey As RegistryKey = mDeviceNameKey.OpenSubKey(sDeviceSerialName, False)
                If (mDeviceSerialKey Is Nothing) Then
                    Continue For
                End If

                Dim sDeviceID As String = String.Format("{0}\{1}", sDeviceName, sDeviceSerialName)

                Dim sDriverLocation As String = TryCast(mDeviceSerialKey.GetValue("Driver"), String)
                Dim sService As String = TryCast(mDeviceSerialKey.GetValue("Service"), String)
                Dim sConfigFlags As DEVICE_CONFIG_FLAGS = CType(mDeviceSerialKey.GetValue("ConfigFlags", 0), DEVICE_CONFIG_FLAGS)

                If (sDriverLocation IsNot Nothing) Then
                    Dim mDriverKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Class\" & sDriverLocation, False)
                    If (mDriverKey Is Nothing) Then
                        Continue For
                    End If

                    Dim sProviderName As String = TryCast(mDriverKey.GetValue("ProviderName"), String)
                    Dim sProviderVersion As String = TryCast(mDriverKey.GetValue("DriverVersion"), String)
                    Dim sProviderDescription As String = TryCast(mDriverKey.GetValue("DriverDesc"), String)
                    Dim sDriverInfPath As String = TryCast(mDriverKey.GetValue("InfPath"), String)

                    Dim mInfo As New STRUC_DEVICE_PROVIDER
                    mInfo.sDeviceID = sDeviceID
                    mInfo.iConfigFlags = sConfigFlags
                    mInfo.sService = sService
                    mInfo.sProviderName = sProviderName
                    mInfo.sProviderVersion = sProviderVersion
                    mInfo.sProviderDescription = sProviderDescription
                    mInfo.sDriverInfPath = sDriverInfPath

                    mProviderList.Add(mInfo)
                Else
                    ' No driver installed
                    Dim mInfo As New STRUC_DEVICE_PROVIDER
                    mInfo.sDeviceID = sDeviceID
                    mInfo.iConfigFlags = sConfigFlags
                    mInfo.sService = sService
                    mInfo.sProviderName = Nothing
                    mInfo.sProviderVersion = Nothing
                    mInfo.sProviderDescription = Nothing
                    mInfo.sDriverInfPath = Nothing

                    mProviderList.Add(mInfo)
                End If
            Next
        Next

        Return mProviderList.ToArray
    End Function

    Public Function IsUsbDeviceConnected(sVID As String, sPID As String, sInterface As String, sSerial As String) As Boolean
        Dim mDevInfo As IntPtr = ClassWin32.SetupDiGetClassDevs(Guid.Empty, IntPtr.Zero, IntPtr.Zero, ClassWin32.DIGCF_PRESENT Or ClassWin32.DIGCF_ALLCLASSES)
        If (mDevInfo = IntPtr.Zero) Then
            Throw New ArgumentException("SetupDiGetClassDevs failed")
        End If

        Try
            Dim iDevIndex As Integer = 0
            Dim mDevInfoData As New ClassWin32.SP_DEVINFO_DATA
            mDevInfoData.cbSize = Marshal.SizeOf(mDevInfoData)

            While (ClassWin32.SetupDiEnumDeviceInfo(mDevInfo, iDevIndex, mDevInfoData))
                iDevIndex += 1

                Dim requiredSize As Integer = 0
                Dim sInstanceId As New StringBuilder(256)
                If (ClassWin32.SetupDiGetDeviceInstanceId(mDevInfo, mDevInfoData, sInstanceId, sInstanceId.Capacity, requiredSize)) Then
                    If (sInstanceId.ToString().ToUpperInvariant.StartsWith(String.Format("{0}\VID_{1}&PID_{2}", sInterface, sVID, sPID).ToUpperInvariant)) Then
                        If (String.IsNullOrEmpty(sSerial)) Then
                            Return True
                        Else
                            If (sInstanceId.ToString().ToUpperInvariant.EndsWith(sSerial.ToUpperInvariant)) Then
                                Return True
                            End If
                        End If
                    End If
                End If

            End While
        Finally
            If (mDevInfo <> IntPtr.Zero) Then
                ClassWin32.SetupDiDestroyDeviceInfoList(mDevInfo)
            End If
        End Try

        Return False ' Device is not connected
    End Function

    Public Function GetDeviceTree(sIstanceId As String, sInterface As String, bConnectedOnly As Boolean) As STRUC_DEVICETREE_ITEM()
        Return GetDeviceTree(sIstanceId, Nothing, Nothing, Nothing, sInterface, bConnectedOnly)
    End Function

    Public Function GetDeviceTree(mInfo As STRUC_DEVICE_DRIVER_INFO, sInterface As String, bConnectedOnly As Boolean) As STRUC_DEVICETREE_ITEM()
        Return GetDeviceTree(Nothing, mInfo.VID, mInfo.PID, mInfo.MM, sInterface, bConnectedOnly)
    End Function

    Public Function GetDeviceTree(sVID As String, sPID As String, sMM As String, sInterface As String, bConnectedOnly As Boolean) As STRUC_DEVICETREE_ITEM()
        Return GetDeviceTree(Nothing, sVID, sPID, sMM, sInterface, bConnectedOnly)
    End Function

    Public Function GetDeviceTree(sInstanceId As String, sVID As String, sPID As String, sMM As String, sInterface As String, bConnectedOnly As Boolean) As STRUC_DEVICETREE_ITEM()
        Dim sTargetHardwareId As String

        If (String.IsNullOrEmpty(sInstanceId) OrElse sInstanceId.TrimEnd.Length = 0) Then
            If (String.IsNullOrEmpty(sMM) OrElse sMM.TrimEnd.Length = 0) Then
                sTargetHardwareId = String.Format("{0}\VID_{1}&PID_{2}", sInterface, sVID, sPID)
            Else
                sTargetHardwareId = String.Format("{0}\VID_{1}&PID_{2}&MI_{3}", sInterface, sVID, sPID, sMM)
            End If
        Else
            sTargetHardwareId = sInstanceId
        End If

        Dim mDevices As New List(Of STRUC_DEVICETREE_ITEM)

        Dim mUsbDeviceKey As RegistryKey = Registry.LocalMachine.OpenSubKey(String.Format("SYSTEM\CurrentControlSet\Enum\{0}", sTargetHardwareId), False)
        If (mUsbDeviceKey Is Nothing) Then
            Return mDevices.ToArray
        End If

        For Each sDeviceSerial As String In mUsbDeviceKey.GetSubKeyNames()
            If (String.IsNullOrEmpty(sDeviceSerial) OrElse sDeviceSerial.TrimEnd.Length = 0) Then
                Continue For
            End If

            Dim sDeviceID As String = String.Format("{0}\{1}", sTargetHardwareId, sDeviceSerial)

            mDevices.Add(New STRUC_DEVICETREE_ITEM(sDeviceID, sVID, sPID, sMM, sDeviceSerial, sInterface))
        Next

        For Each mDevice In mDevices
            sTargetHardwareId = mDevice.sDeviceID

            Dim mParentDevices As New List(Of STRUC_DEVICE_PROVIDER)

            Dim iFlags As Integer = ClassWin32.DIGCF_ALLCLASSES
            If (bConnectedOnly) Then
                iFlags = iFlags Or ClassWin32.DIGCF_PRESENT
            End If

            Dim mDeviceInfoSet As IntPtr = ClassWin32.SetupDiGetClassDevs(Guid.Empty, IntPtr.Zero, IntPtr.Zero, iFlags)
            If (mDeviceInfoSet = IntPtr.Zero) Then
                Continue For
            End If

            Try
                Dim iIndex As Integer = 0

                Dim mDeviceInfoData As New ClassWin32.SP_DEVINFO_DATA
                mDeviceInfoData.cbSize = Marshal.SizeOf(mDeviceInfoData)
                While (ClassWin32.SetupDiEnumDeviceInfo(mDeviceInfoSet, iIndex, mDeviceInfoData))
                    iIndex += 1

                    'Dim sHardwareId As New StringBuilder(256)
                    Dim sDeviceId As New StringBuilder(256)
                    Dim requiredSize As Integer = 0
                    Dim regType As Integer = 0

                    'If (Not ClassWin32.SetupDiGetDeviceRegistryProperty(mDeviceInfoSet, mDeviceInfoData, ClassWin32.SPDRP_HARDWAREID, regType, sHardwareId, sHardwareId.Capacity, requiredSize)) Then
                    '    Continue While
                    'End If

                    If (Not ClassWin32.SetupDiGetDeviceInstanceId(mDeviceInfoSet, mDeviceInfoData, sDeviceId, sDeviceId.Capacity, requiredSize)) Then
                        Continue While
                    End If

                    If (Not sDeviceId.ToString().ToUpperInvariant.StartsWith(sTargetHardwareId.ToUpperInvariant)) Then
                        Continue While
                    End If

                    ' Find information about the device
                    Dim mDeviceEnum As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Enum", False)
                    If (mDeviceEnum Is Nothing) Then
                        Exit While
                    End If

                    Dim mDeviceKey As RegistryKey = mDeviceEnum.OpenSubKey(sDeviceId.ToString, False)
                    If (mDeviceKey IsNot Nothing) Then
                        Dim sDriverLocation As String = TryCast(mDeviceKey.GetValue("Driver"), String)
                        Dim sService As String = TryCast(mDeviceKey.GetValue("Service"), String)
                        Dim sConfigFlags As DEVICE_CONFIG_FLAGS = CType(mDeviceKey.GetValue("ConfigFlags", 0), DEVICE_CONFIG_FLAGS)

                        If (sDriverLocation IsNot Nothing) Then
                            Dim mDriverKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Class\" & sDriverLocation, False)
                            If (mDriverKey Is Nothing) Then
                                Exit While
                            End If

                            Dim sProviderName As String = TryCast(mDriverKey.GetValue("ProviderName"), String)
                            Dim sProviderVersion As String = TryCast(mDriverKey.GetValue("DriverVersion"), String)
                            Dim sProviderDescription As String = TryCast(mDriverKey.GetValue("DriverDesc"), String)
                            Dim sDriverInfPath As String = TryCast(mDriverKey.GetValue("InfPath"), String)

                            Dim mInfo As New STRUC_DEVICE_PROVIDER
                            mInfo.sDeviceID = sDeviceId.ToString
                            mInfo.iConfigFlags = sConfigFlags
                            mInfo.sService = sService
                            mInfo.sProviderName = sProviderName
                            mInfo.sProviderVersion = sProviderVersion
                            mInfo.sProviderDescription = sProviderDescription
                            mInfo.sDriverInfPath = sDriverInfPath
                            mParentDevices.Add(mInfo)

                            ' Set main provider from first match
                            mDevice.mProvider = mParentDevices(0)
                        Else
                            ' No driver installed
                            Dim mInfo As New STRUC_DEVICE_PROVIDER
                            mInfo.sDeviceID = sDeviceId.ToString
                            mInfo.iConfigFlags = sConfigFlags
                            mInfo.sService = sService
                            mInfo.sProviderName = Nothing
                            mInfo.sProviderVersion = Nothing
                            mInfo.sProviderDescription = Nothing
                            mInfo.sDriverInfPath = Nothing
                            mParentDevices.Add(mInfo)

                            ' Set main provider from first match
                            mDevice.mProvider = mParentDevices(0)
                        End If
                    End If

                    Dim iParentDevInst As Integer = 0
                    If (ClassWin32.CM_Get_Parent(iParentDevInst, mDeviceInfoData.DevInst, 0) <> 0) Then
                        Exit While
                    End If

                    Dim sParentHardwareID As New StringBuilder(1024)
                    If (ClassWin32.CM_Get_Device_ID(iParentDevInst, sParentHardwareID, sParentHardwareID.Capacity, 0) <> 0) Then
                        Exit While
                    End If

                    ' Redo the search with the parent hardware id
                    iIndex = 0
                    sTargetHardwareId = sParentHardwareID.ToString
                End While
            Finally
                ClassWin32.SetupDiDestroyDeviceInfoList(mDeviceInfoSet)
            End Try

            mDevice.mParentDevices = mParentDevices.ToArray
        Next

        Return mDevices.ToArray
    End Function

    ''' <summary>
    ''' Resolves hardware ids such as 'VID_1BCF&PID_28C4&MI_00' or 'USB\VID_1BCF&PID_28C4&MI_00'
    ''' </summary>
    ''' <param name="sHardwareId"></param>
    ''' <param name="sVID"></param>
    ''' <param name="sPID"></param>
    ''' <param name="sMM"></param>
    ''' <returns></returns>
    Public Function ResolveHardwareID(sHardwareId As String, ByRef sVID As String, ByRef sPID As String, ByRef sMM As String, ByRef sSerial As String) As Boolean
        sVID = Nothing
        sPID = Nothing
        sMM = Nothing
        sSerial = Nothing

        Dim sPathSplit As String() = sHardwareId.Split(New Char() {"\"c, "/"c})

        For i = 0 To sPathSplit.Length - 1
            If (sPathSplit(i).ToUpperInvariant.IndexOf("VID_") > -1 AndAlso sPathSplit(i).ToUpperInvariant.IndexOf("PID_") > -1) Then
                Dim sIdSplit As String() = sPathSplit(i).Split("&"c)

                If (i + 1 < sPathSplit.Length) Then
                    sSerial = sPathSplit(i + 1)
                End If

                Select Case (sIdSplit.Length)
                    Case 2
                        sVID = sIdSplit(0).Split("_"c)(1)
                        sPID = sIdSplit(1).Split("_"c)(1)

                        Return True
                    Case 3
                        sVID = sIdSplit(0).Split("_"c)(1)
                        sPID = sIdSplit(1).Split("_"c)(1)
                        sMM = sIdSplit(2).Split("_"c)(1)

                        Return True
                End Select
            End If
        Next

        Return False
    End Function

End Class
