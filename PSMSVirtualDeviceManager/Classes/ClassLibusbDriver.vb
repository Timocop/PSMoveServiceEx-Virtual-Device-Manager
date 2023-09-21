Imports System.Runtime.InteropServices
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
        Public Shared Function SetupDiGetDeviceRegistryProperty(DeviceInfoSet As IntPtr, <MarshalAs(UnmanagedType.LPStruct)> DeviceInfoData As SP_DEVINFO_DATA, Property_ As Integer, ByRef PropertyRegDataType As Integer, PropertyBuffer As IntPtr, PropertyBufferSize As Integer, ByRef RequiredSize As Integer) As Boolean
        End Function
    End Class

    Public Const LIBUSB_SERVICE_NAME As String = "libusb0"
    Public Const HID_SERVICE_NAME As String = "HidUsb"

    Public Const DRV_ROOT_NAME As String = "libusb_driver"
    Public Const DRV_INSTALLER_NAME As String = "wdi-simple.exe"
    Public ReadOnly DRV_PSEYE_KNOWN_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation Eye Camera (Composite Device)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", Nothing),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation Eye Camera (Interface 0)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", "00"),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation Eye Camera (Interface 1)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", "01")
    }
    Public ReadOnly DRV_PSEYE_LIBUSB_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation Eye Camera", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", Nothing)
    }

    Public ReadOnly DRV_PSVR_KNOWN_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR (Composite Device)", "Sony Corp.", "054C", "09AF", Nothing),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR 3D Audio", "Sony Corp.", "054C", "09AF", "00"),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Audio Device", "Sony Corp.", "054C", "09AF", "01"),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Sensor (Interface 4)", "Sony Corp.", "054C", "09AF", "04"),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Control (Interface 5)", "Sony Corp.", "054C", "09AF", "05"),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR H.264", "Sony Corp.", "054C", "09AF", "06"),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR BulkIn", "Sony Corp.", "054C", "09AF", "07"),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Input Device", "Sony Corp.", "054C", "09AF", "08")
    }
    Public ReadOnly DRV_PSVR_LIBUSB_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR 3D Audio", "Sony Corp.", "054C", "09AF", "00"), ' Just add a driver so it does not show as hardware issue.
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Control", "Sony Corp.", "054C", "09AF", "05"),
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR H.264", "Sony Corp.", "054C", "09AF", "06"), ' Just add a driver so it does not show as hardware issue.
        New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR BulkIn", "Sony Corp.", "054C", "09AF", "07") ' Just add a driver so it does not show as hardware issue.
    }
    Public ReadOnly DRV_PSVR_HID_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
      New STRUC_DEVICE_DRIVER_INFO("USB PlayStation VR Sensor", "Sony Corp.", "054C", "09AF", "04")
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

    Public Function InstallPlaystationEyeDriver64() As ENUM_WDI_ERROR
        For Each mConfig As STRUC_DEVICE_DRIVER_INFO In DRV_PSEYE_LIBUSB_CONFIGS
            Dim iExitCode As ENUM_WDI_ERROR = InternalInstallDriver64(mConfig)
            If (iExitCode <> ENUM_WDI_ERROR.WDI_SUCCESS) Then
                Return iExitCode
            End If
        Next

        Return ENUM_WDI_ERROR.WDI_SUCCESS
    End Function

    Public Function InstallPlaystationVrDrvier64() As ENUM_WDI_ERROR
        For Each mConfig As STRUC_DEVICE_DRIVER_INFO In DRV_PSVR_LIBUSB_CONFIGS
            Dim iExitCode As ENUM_WDI_ERROR = InternalInstallDriver64(mConfig)
            If (iExitCode <> ENUM_WDI_ERROR.WDI_SUCCESS) Then
                Return iExitCode
            End If
        Next

        Return ENUM_WDI_ERROR.WDI_SUCCESS
    End Function

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

        ' Remove devices after everything is done.
        For Each mDeviceID As String In sDevicesToRemove
            RemoveDevice(mDeviceID, True)
            bScanNewDevices = True
        Next

        If (bScanNewDevices) Then
            ScanDevices()
        End If
    End Sub

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
                    If (mUsbInfo.sService = LIBUSB_SERVICE_NAME) Then
                        Continue For
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
                    If (mUsbInfo.sService = LIBUSB_SERVICE_NAME) Then
                        Continue For
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
                    If (mUsbInfo.sService = HID_SERVICE_NAME) Then
                        Continue For
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
        Dim sRootFolder As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), DRV_ROOT_NAME)
        Dim sInstallerPath As String = IO.Path.Combine(sRootFolder, DRV_INSTALLER_NAME)

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

    Public Function GetDeviceProviderDISPLAY(sSerial As String) As STRUC_DEVICE_PROVIDER()
        Return InternalGetDeviceProvider(sSerial, Nothing, Nothing, "DISPLAY")
    End Function

    Private Function InternalGetDeviceProvider(sVID As String, sPID As String, sMI As String, sInterface As String) As STRUC_DEVICE_PROVIDER()
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

                Dim mInfo As New STRUC_DEVICE_PROVIDER
                mInfo.sDeviceID = sDeviceID
                mInfo.iConfigFlags = sConfigFlags
                mInfo.sService = sService
                mInfo.sProviderName = sProviderName
                mInfo.sProviderVersion = sProviderVersion
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
                mInfo.sDriverInfPath = Nothing

                mProviderList.Add(mInfo)
            End If
        Next

        Return mProviderList.ToArray
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
        Return IsUsbDeviceConnected(mInfo.VID, mInfo.PID)
    End Function

    Public Function IsUsbDeviceConnected(sVID As String, sPID As String) As Boolean
        Dim mDevInfo As IntPtr = ClassWin32.SetupDiGetClassDevs(Guid.Empty, IntPtr.Zero, IntPtr.Zero, ClassWin32.DIGCF_PRESENT Or ClassWin32.DIGCF_ALLCLASSES)
        If (mDevInfo = IntPtr.Zero) Then
            Throw New ArgumentException("SetupDiGetClassDevs failed")
        End If

        Try
            Dim iDevIndex As Integer = 0
            Dim mDevInfoData As New ClassWin32.SP_DEVINFO_DATA
            mDevInfoData.cbSize = Marshal.SizeOf(mDevInfoData)

            While (ClassWin32.SetupDiEnumDeviceInfo(mDevInfo, iDevIndex, mDevInfoData))
                Dim iRegDataType As Integer = 0
                Dim iBufferSize As Integer = 0

                ClassWin32.SetupDiGetDeviceRegistryProperty(mDevInfo, mDevInfoData, ClassWin32.SPDRP_HARDWAREID, iRegDataType, IntPtr.Zero, 0, iBufferSize)

                If (iBufferSize > 0) Then
                    Dim iBuffer As IntPtr = Marshal.AllocHGlobal(iBufferSize)
                    If (iBuffer = IntPtr.Zero) Then
                        Throw New ArgumentException("AllocHGlobal failed")
                    End If

                    Try
                        If (ClassWin32.SetupDiGetDeviceRegistryProperty(mDevInfo, mDevInfoData, ClassWin32.SPDRP_HARDWAREID, iRegDataType, iBuffer, iBufferSize, iBufferSize)) Then
                            Dim sDeviceHardwareID As String = Marshal.PtrToStringAuto(iBuffer)

                            If (sDeviceHardwareID.StartsWith(String.Format("USB\VID_{0}&PID_{1}", sVID, sPID))) Then
                                Return True
                            End If
                        End If
                    Finally
                        If (iBuffer <> IntPtr.Zero) Then
                            Marshal.FreeHGlobal(iBuffer)
                        End If
                    End Try
                End If

                iDevIndex += 1
            End While
        Finally
            If (mDevInfo <> IntPtr.Zero) Then
                ClassWin32.SetupDiDestroyDeviceInfoList(mDevInfo)
            End If
        End Try

        Return False ' Device is not connected
    End Function
End Class
