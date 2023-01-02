Public Class ClassLibusbDriver
    Public Const DRV_ROOT_NAME As String = "libusb_driver"
    Public Const DRV_INSTALLER_NAME As String = "wdi-simple.exe"
    'Public ReadOnly DRV_INSTALLER_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
    '    New STRUC_DEVICE_DRIVER_INFO("USB Playstation Eye Camera", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", Nothing), ' Unknown device (Composite device). 
    '    New STRUC_DEVICE_DRIVER_INFO("USB Playstation Eye Camera (Interface 0)", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", "0") ' Device detected but no driver found. Audio works by default.
    '}
    Public ReadOnly DRV_INSTALLER_CONFIGS As STRUC_DEVICE_DRIVER_INFO() = {
        New STRUC_DEVICE_DRIVER_INFO("USB Playstation Eye Camera", "Nam Tai E&E Products Ltd. or OmniVision Technologies, Inc.", "1415", "2000", Nothing) ' Unknown device (Composite device).  
    }

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

    Structure STRUC_DEVICE_DRIVER_INFO
        Dim sName As String
        Dim sManufacture As String
        Dim VID As String
        Dim PID As String
        Dim MM As String

        Public Sub New(_Name As String, _Manufacture As String, _VID As String, _PID As String, _MM As String)
            sName = _Name
            sManufacture = _Manufacture
            VID = _VID
            PID = _PID
            MM = _MM
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

            sCmd.Add(String.Format("-t ""{0}""", 1)) 'libusb

            Return String.Join(" ", sCmd.ToArray)
        End Function
    End Structure

    Public Sub New()
    End Sub

    Public Sub InstallDriver64()
        Dim sRootFolder As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), DRV_ROOT_NAME)
        Dim sInstallerPath As String = IO.Path.Combine(sRootFolder, DRV_INSTALLER_NAME)

        For Each mDrvConfig As STRUC_DEVICE_DRIVER_INFO In DRV_INSTALLER_CONFIGS

            Using mProcess As New Process
                mProcess.StartInfo.FileName = sInstallerPath
                mProcess.StartInfo.Arguments = mDrvConfig.CreateCommandLine()
                mProcess.StartInfo.WorkingDirectory = sRootFolder
                mProcess.StartInfo.CreateNoWindow = True
                mProcess.StartInfo.UseShellExecute = True

                If (Environment.OSVersion.Version.Major > 5) Then
                    mProcess.StartInfo.Verb = "runas"
                End If

                mProcess.Start()

                mProcess.WaitForExit()

                If (mProcess.ExitCode <> ENUM_WDI_ERROR.WDI_SUCCESS) Then
                    Throw New ArgumentException(String.Format("Driver installation failed with error {0} - {1}", mProcess.ExitCode, CType(mProcess.ExitCode, ENUM_WDI_ERROR).ToString))
                End If
            End Using
        Next
    End Sub

End Class
