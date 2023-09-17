Imports Microsoft.Win32

Public Class ClassLibusbDriver
    Public Const LIBUSB_SERVICE_NAME As String = "libusb0"
    Public Const HID_SERVICE_NAME As String = "HidUsb"

    Public Const DRV_ROOT_NAME As String = "libusb_driver"
    Public Const DRV_INSTALLER_NAME As String = "wdi-simple.exe"
    'Public ReadOnly DRV_PSEYE_INSTALLER_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
    '    New STRUC_DEVICE_DRIVER_INFO("USB Playstation Eye Camera", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", Nothing), ' Unknown device (Composite device). 
    '    New STRUC_DEVICE_DRIVER_INFO("USB Playstation Eye Camera (Interface 0)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", "0") ' Device detected but no driver found. Audio works by default.
    '}
    Public ReadOnly DRV_PSEYE_LIBUSB_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB Playstation Eye Camera", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", Nothing) ' Unknown device (Composite device).  
    }

    'Public ReadOnly DRV_PSVR_INSTALLER_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
    '    New STRUC_DEVICE_DRIVER_INFO("PS VR Control (Interface 5)", "Sony Corp.", "054C", "09AF", "05"),
    '    New STRUC_DEVICE_DRIVER_INFO("PS VR Sensor (Interface 4)", "Sony Corp.", "054C", "09AF", "04"), ' We dont install libUSB on this, HID is required for it to work.
    '    New STRUC_DEVICE_DRIVER_INFO("PS VR H.264", "Sony Corp.", "054C", "09AF", "06"), ' Just add a driver so it does not show as hardware issue.
    '    New STRUC_DEVICE_DRIVER_INFO("PS VR BulkIn", "Sony Corp.", "054C", "09AF", "07"), ' Just add a driver so it does not show as hardware issue.
    '    New STRUC_DEVICE_DRIVER_INFO("PS VR 3D Audio", "Sony Corp.", "054C", "09AF", "00") ' Just add a driver so it does not show as hardware issue.
    '}
    Public ReadOnly DRV_PSVR_LIBUSB_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("PS VR Control (Interface 5)", "Sony Corp.", "054C", "09AF", "05")
    }
    Public ReadOnly DRV_PSVR_HID_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
      New STRUC_DEVICE_DRIVER_INFO("PS VR Sensor (Interface 4)", "Sony Corp.", "054C", "09AF", "04")
    }

    Enum ENUM_WDI_DRIVERTYPE
        WINUSB = 0
        LIBUSB = 1
        LIBUSBK = 2
        USBSER = 3
        CUSTOM = 4
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

    Structure STRUC_DEVICE_PROVIDER
        Dim sDeviceID As String
        Dim iConfigFlags As DEVICE_CONFIG_FLAGS
        Dim sService As String
        Dim sProviderName As String
        Dim sProviderVersion As String
        Dim sDriverInfPath As String

        Public Function IsEnabled() As Boolean
            Return ((iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_DISABLED) = 0)
        End Function

        Public Function IsRemoved() As Boolean
            Return ((iConfigFlags And DEVICE_CONFIG_FLAGS.CONFIGFLAG_REMOVED) <> 0)
        End Function

        Public Function HasDriverInstalled() As Boolean
            Return (sService Is Nothing)
        End Function
    End Structure

    Structure STRUC_DEVICE_DRIVER_INFO
        Dim sName As String
        Dim sManufacture As String
        Dim VID As String
        Dim PID As String
        Dim MM As String
        Dim iDriver As ENUM_WDI_DRIVERTYPE

        Public Sub New(_Name As String, _Manufacture As String, _VID As String, _PID As String, _MM As String)
            sName = _Name
            sManufacture = _Manufacture
            VID = _VID
            PID = _PID
            MM = _MM
            iDriver = ENUM_WDI_DRIVERTYPE.LIBUSB
        End Sub

        Public Sub New(_Name As String, _Manufacture As String, _VID As String, _PID As String, _MM As String, _Driver As ENUM_WDI_DRIVERTYPE)
            sName = _Name
            sManufacture = _Manufacture
            VID = _VID
            PID = _PID
            MM = _MM
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

            Return String.Join(" ", sCmd.ToArray)
        End Function
    End Structure

    Public Sub New()
    End Sub

    Public Sub InstallPlaystationEyeDriver64()
        For Each mConfig As STRUC_DEVICE_DRIVER_INFO In DRV_PSEYE_LIBUSB_CONFIGS
            InternalInstallDriver64(mConfig)
        Next
    End Sub

    Public Sub InstallPlaystationVrDrvier64()
        For Each mConfig As STRUC_DEVICE_DRIVER_INFO In DRV_PSVR_LIBUSB_CONFIGS
            InternalInstallDriver64(mConfig)
        Next
    End Sub

    Public Function VerifyPlaystationEyeDriver64() As Boolean
        Dim bDriversInstalled As Boolean = True

        For Each mInfo In DRV_PSEYE_LIBUSB_CONFIGS
            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                If (mUsbInfo.iConfigFlags <> 0) Then
                    Continue For
                End If

                If (String.IsNullOrEmpty(mUsbInfo.sService)) Then
                    ' No driver
                    bDriversInstalled = False
                Else
                    If (mUsbInfo.sService = LIBUSB_SERVICE_NAME) Then
                        Continue For
                    End If

                    'Wrong driver
                    bDriversInstalled = False
                End If
            Next
        Next

        Return bDriversInstalled
    End Function

    Public Function VerifyPlaystationVrDriver64() As Boolean
        Dim bDriversInstalled As Boolean = True
        Dim bHidInstalled As Boolean = True

        For Each mInfo In DRV_PSVR_LIBUSB_CONFIGS
            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                If (mUsbInfo.iConfigFlags <> 0) Then
                    Continue For
                End If

                If (String.IsNullOrEmpty(mUsbInfo.sService)) Then
                    ' No driver
                    bDriversInstalled = False
                Else
                    If (mUsbInfo.sService = LIBUSB_SERVICE_NAME) Then
                        Continue For
                    End If

                    'Wrong driver
                    bDriversInstalled = False
                End If
            Next
        Next

        ' Check if the device is using HID and no other weird driver.
        For Each mInfo In DRV_PSVR_HID_CONFIGS
            For Each mUsbInfo In GetDeviceProviderUSB(mInfo)
                If (mUsbInfo.iConfigFlags <> 0) Then
                    Continue For
                End If

                If (String.IsNullOrEmpty(mUsbInfo.sService)) Then
                    ' No driver
                    bHidInstalled = False
                Else
                    If (mUsbInfo.sService = HID_SERVICE_NAME) Then
                        Continue For
                    End If

                    'Wrong driver
                    bHidInstalled = False
                End If
            Next
        Next
        Return (bDriversInstalled AndAlso bHidInstalled)
    End Function

    Private Sub InternalInstallDriver64(mInfo As STRUC_DEVICE_DRIVER_INFO)
        Dim sRootFolder As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), DRV_ROOT_NAME)
        Dim sInstallerPath As String = IO.Path.Combine(sRootFolder, DRV_INSTALLER_NAME)

        Using mProcess As New Process
            mProcess.StartInfo.FileName = sInstallerPath
            mProcess.StartInfo.Arguments = mInfo.CreateCommandLine()
            mProcess.StartInfo.WorkingDirectory = sRootFolder
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()

            mProcess.WaitForExit()

            If (mProcess.ExitCode <> ENUM_WDI_ERROR.WDI_SUCCESS) Then
                Throw New ArgumentException(String.Format("Driver installation failed with error: {0} - {1}", mProcess.ExitCode, CType(mProcess.ExitCode, ENUM_WDI_ERROR).ToString))
            End If
        End Using
    End Sub

    Public Function GetDeviceProviderUSB(mInfo As STRUC_DEVICE_DRIVER_INFO) As STRUC_DEVICE_PROVIDER()
        Return InternalGetDeviceProvider(mInfo.VID, mInfo.PID, mInfo.MM, "USB")
    End Function

    Public Function GetDeviceProviderUSB(sVID As String, sPID As String, sMI As String) As STRUC_DEVICE_PROVIDER()
        Return InternalGetDeviceProvider(sVID, sPID, sMI, "USB")
    End Function

    Public Function GetDeviceProviderHID(mInfo As STRUC_DEVICE_DRIVER_INFO) As STRUC_DEVICE_PROVIDER()
        Return InternalGetDeviceProvider(mInfo.VID, mInfo.PID, mInfo.MM, "HID")
    End Function

    Public Function GetDeviceProviderHID(sVID As String, sPID As String, sMI As String) As STRUC_DEVICE_PROVIDER()
        Return InternalGetDeviceProvider(sVID, sPID, sMI, "HID")
    End Function

    Private Function InternalGetDeviceProvider(sVID As String, sPID As String, sMI As String, sInterface As String) As STRUC_DEVICE_PROVIDER()
        Dim mProviderList As New List(Of STRUC_DEVICE_PROVIDER)

        Dim sUseDevice As String = Nothing
        If (String.IsNullOrEmpty(sMI)) Then
            sUseDevice = String.Format("{0}\VID_{1}&PID_{2}", sInterface, sVID, sPID)
        Else
            If (sMI.Length = 1) Then
                sMI = "0" & sMI
            End If

            sUseDevice = String.Format("{0}\VID_{1}&PID_{2}&MI_{3}", sInterface, sVID, sPID, sMI)
        End If

        Dim mUsbDeviceKey As RegistryKey = Registry.LocalMachine.OpenSubKey(String.Format("SYSTEM\CurrentControlSet\Enum\{0}", sUseDevice))
        If (mUsbDeviceKey Is Nothing) Then
            Return mProviderList.ToArray
        End If

        For Each mDeviceSubKey As String In mUsbDeviceKey.GetSubKeyNames()
            Dim mDeviceKey As RegistryKey = mUsbDeviceKey.OpenSubKey(mDeviceSubKey)
            If (mDeviceKey Is Nothing) Then
                Continue For
            End If

            Dim sDeviceID As String = String.Format("{0}\{1}", sUseDevice, mDeviceSubKey)

            Dim sDriverLocation As String = TryCast(mDeviceKey.GetValue("Driver"), String)
            Dim sService As String = TryCast(mDeviceKey.GetValue("Service"), String)
            Dim sConfigFlags As DEVICE_CONFIG_FLAGS = CType(mDeviceKey.GetValue("ConfigFlags", 0), DEVICE_CONFIG_FLAGS)

            If (sDriverLocation IsNot Nothing) Then
                Dim mDriverKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Class\" & sDriverLocation)
                If (mDriverKey Is Nothing) Then
                    Continue For
                End If

                Dim sProviderName As String = TryCast(mDriverKey.GetValue("ProviderName"), String)
                Dim sProviderVersion As String = TryCast(mDriverKey.GetValue("DriverVersion"), String)
                Dim sDriverInfPath As String = TryCast(mDriverKey.GetValue("InfPath"), String)
                If (sProviderVersion IsNot Nothing AndAlso sProviderName IsNot Nothing) Then
                    Dim mInfo As New STRUC_DEVICE_PROVIDER
                    mInfo.sDeviceID = sDeviceID
                    mInfo.iConfigFlags = sConfigFlags
                    mInfo.sService = sService
                    mInfo.sProviderName = sProviderName
                    mInfo.sProviderVersion = sProviderVersion
                    mInfo.sDriverInfPath = sDriverInfPath

                    mProviderList.Add(mInfo)
                End If
            Else
                ' No driver installed
                Dim mInfo As New STRUC_DEVICE_PROVIDER
                mInfo.sDeviceID = sDeviceID
                mInfo.iConfigFlags = sConfigFlags
                mInfo.sService = sService
                mInfo.sProviderName = Nothing
                mInfo.sProviderVersion = Nothing
                mInfo.sDriverInfPath = Nothing

                mProviderList.Add(mInfo)
            End If
        Next

        Return mProviderList.ToArray
    End Function

    Public Sub ScanDevices()
        Using mProcess As New Process
            mProcess.StartInfo.FileName = "pnputil.exe"
            mProcess.StartInfo.Arguments = "/scan-devices"
            mProcess.StartInfo.WorkingDirectory = Environment.SystemDirectory
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()

            mProcess.WaitForExit()

            If (mProcess.ExitCode <> 0) Then
                Throw New ArgumentException(String.Format("Driver installation failed with error: {0}", mProcess.ExitCode))
            End If
        End Using
    End Sub

    Public Sub RemoveDevice(sInstanceID As String)
        Using mProcess As New Process
            mProcess.StartInfo.FileName = "pnputil.exe"
            mProcess.StartInfo.Arguments = String.Format("/remove-device ""{0}""", sInstanceID)
            mProcess.StartInfo.WorkingDirectory = Environment.SystemDirectory
            mProcess.StartInfo.CreateNoWindow = True
            mProcess.StartInfo.UseShellExecute = True

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()

            mProcess.WaitForExit()

            If (mProcess.ExitCode <> 0) Then
                Throw New ArgumentException(String.Format("Driver installation failed with error: {0}", mProcess.ExitCode))
            End If
        End Using
    End Sub

    Public Sub RemoveDriver(sInfFile As String)
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

            If (Environment.OSVersion.Version.Major > 5) Then
                mProcess.StartInfo.Verb = "runas"
            End If

            mProcess.Start()

            mProcess.WaitForExit()

            If (mProcess.ExitCode <> 0) Then
                Throw New ArgumentException(String.Format("Driver installation failed with error: {0}", mProcess.ExitCode))
            End If
        End Using
    End Sub
End Class
