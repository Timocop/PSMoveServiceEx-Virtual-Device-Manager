Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Text

Public Class ClassMonitor
    Protected Class ClassWin32
        <DllImport("user32.dll")>
        Public Shared Function ChangeDisplaySettingsEx(lpszDeviceName As String, ByRef lpDevMode As DEVMODE, hwnd As IntPtr, dwflags As ENUM_CHANGE_DISPLAY_SETTINGS_FLAGS, lParam As IntPtr) As ENUM_DISPLAY_SETTINGS_ERROR
        End Function

        <DllImport("user32.dll")>
        Public Shared Function EnumDisplayDevices(lpDevice As String, iDevNum As Integer, ByRef lpDisplayDevice As DISPLAY_DEVICE, dwFlags As Integer) As Boolean
        End Function

        <DllImport("user32.dll")>
        Public Shared Function EnumDisplayDevices(lpDevice As String, iDevNum As Integer, ByRef lpDisplayDevice As MONITOR_DEVICE, dwFlags As Integer) As Boolean
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Ansi)>
        Public Shared Function EnumDisplaySettings(lpszDeviceName As String, iModeNum As Integer, ByRef lpDevMode As DEVMODE) As Boolean
        End Function

        Public Const ENUM_CURRENT_SETTINGS As Integer = -1
        Public Const ENUM_REGISTRY_SETTINGS As Integer = -2

        Public Const DISP_CHANGE_SUCCESSFUL As Integer = 0
        Public Const DISP_CHANGE_RESTART As Integer = 1
        Public Const DISP_CHANGE_FAILED As Integer = -1
        Public Const DISP_CHANGE_BADMODE As Integer = -2
        Public Const DISP_CHANGE_NOTUPDATED As Integer = -3

        Public Const DM_BITSPERPEL As Integer = &H40000
        Public Const DM_PELSWIDTH As Integer = &H80000
        Public Const DM_PELSHEIGHT As Integer = &H100000
        Public Const DM_DISPLAYFREQUENCY As Integer = &H400000

        Public Const CCHFORMNAME As Integer = 32
    End Class

    Private Interface INullable
    End Interface

    <Flags>
    Public Enum DisplayDeviceStateFlags As Integer
        AttachedToDesktop = &H1
        PrimaryDevice = &H4
        MirroringDriver = &H8
        VGACompatible = &H10
        Removable = &H20
        VGA = &H40
    End Enum

    <StructLayout(LayoutKind.Sequential)>
    Public Structure DEVMODE
        Implements INullable

        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=ClassWin32.CCHFORMNAME)>
        Public dmDeviceName As String
        Public dmSpecVersion As Short
        Public dmDriverVersion As Short
        Public dmSize As Short
        Public dmDriverExtra As Short
        Public dmFields As Integer
        Public dmPositionX As Integer
        Public dmPositionY As Integer
        Public dmDisplayOrientation As ScreenOrientation
        Public dmDisplayFixedOutput As Integer
        Public dmColor As Short
        Public dmDuplex As Short
        Public dmYResolution As Short
        Public dmTTOption As Short
        Public dmCollate As Short
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=ClassWin32.CCHFORMNAME)>
        Public dmFormName As String
        Public dmLogPixels As Short
        Public dmBitsPerPel As Integer
        Public dmPelsWidth As Integer
        Public dmPelsHeight As Integer
        Public dmDisplayFlags As Integer
        Public dmDisplayFrequency As Integer
        Public dmICMMethod As Integer
        Public dmICMIntent As Integer
        Public dmMediaType As Integer
        Public dmDitherType As Integer
        Public dmReserved1 As Integer
        Public dmReserved2 As Integer
        Public dmPanningWidth As Integer
        Public dmPanningHeight As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure DISPLAY_DEVICE
        Public cb As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
        Public DeviceName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)>
        Public DeviceString As String
        Public StateFlags As DisplayDeviceStateFlags
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)>
        Public DeviceID As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)>
        Public DeviceKey As String
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure MONITOR_DEVICE
        Public cb As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
        Public DeviceName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)>
        Public DeviceString As String
        Public StateFlags As DisplayDeviceStateFlags
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)>
        Public DeviceID As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)>
        Public DeviceKey As String
    End Structure

    <Flags>
    Public Enum ENUM_CHANGE_DISPLAY_SETTINGS_FLAGS
        CDS_NONE = 0
        CDS_UPDATEREGISTRY = &H1
        CDS_TEST = &H2
        CDS_FULLSCREEN = &H4
        CDS_GLOBAL = &H8
        CDS_SET_PRIMARY = &H10
        CDS_VIDEOPARAMETERS = &H20
        CDS_ENABLE_UNSAFE_MODES = &H100
        CDS_DISABLE_UNSAFE_MODES = &H200
        CDS_RESET = &H40000000
        CDS_RESET_EX = &H20000000
        CDS_NORESET = &H10000000
    End Enum

    Public Enum ENUM_DISPLAY_SETTINGS_ERROR
        DISP_CHANGE_SUCCESSFUL = 0
        DISP_CHANGE_RESTART = 1
        DISP_CHANGE_FAILED = -1
        DISP_CHANGE_BADMODE = -2
        DISP_CHANGE_NOTUPDATED = -3
    End Enum

    Public Function GetMonitorList() As KeyValuePair(Of DISPLAY_DEVICE, MONITOR_DEVICE)()
        Dim mMonitors As New List(Of KeyValuePair(Of DISPLAY_DEVICE, MONITOR_DEVICE))

        Dim mDisplayDevice As New DISPLAY_DEVICE()
        mDisplayDevice.cb = Marshal.SizeOf(mDisplayDevice)

        Dim i As Integer = 0
        While (ClassWin32.EnumDisplayDevices(Nothing, i, mDisplayDevice, 0))

            Dim mMonitorDevice As New MONITOR_DEVICE()
            mMonitorDevice.cb = Marshal.SizeOf(mMonitorDevice)

            If (ClassWin32.EnumDisplayDevices(mDisplayDevice.DeviceName, 0, mMonitorDevice, 0)) Then
                mMonitors.Add(New KeyValuePair(Of DISPLAY_DEVICE, MONITOR_DEVICE)(mDisplayDevice, mMonitorDevice))
            End If

            i += 1
        End While

        Return mMonitors.ToArray
    End Function

    Public Function GetMonitorInfo(mDevice As DISPLAY_DEVICE) As DEVMODE
        Return GetMonitorInfo(mDevice.DeviceName)
    End Function

    Public Function GetMonitorInfo(sDeviceName As String) As DEVMODE
        Dim mDevMode As New DEVMODE()
        mDevMode.dmSize = CType(Marshal.SizeOf(mDevMode), Short)

        If (ClassWin32.EnumDisplaySettings(sDeviceName, ClassWin32.ENUM_CURRENT_SETTINGS, mDevMode)) Then
            Return Nothing
        End If

        Return mDevMode
    End Function

    Public Function ChangeRefreshRateForMonitor(mDevice As DISPLAY_DEVICE, iDisplayFrequency As Integer) As ENUM_DISPLAY_SETTINGS_ERROR
        Return ChangeRefreshRateForMonitor(mDevice.DeviceName, iDisplayFrequency)
    End Function

    Public Function ChangeRefreshRateForMonitor(sDeviceName As String, iDisplayFrequency As Integer) As ENUM_DISPLAY_SETTINGS_ERROR
        Dim mDevMode As New DEVMODE()
        mDevMode.dmSize = CType(Marshal.SizeOf(mDevMode), Short)

        If (Not ClassWin32.EnumDisplaySettings(sDeviceName, ClassWin32.ENUM_CURRENT_SETTINGS, mDevMode)) Then
            Return ENUM_DISPLAY_SETTINGS_ERROR.DISP_CHANGE_FAILED
        End If

        mDevMode.dmDisplayFrequency = iDisplayFrequency
        Dim result As ENUM_DISPLAY_SETTINGS_ERROR = ClassWin32.ChangeDisplaySettingsEx(sDeviceName, mDevMode, IntPtr.Zero, ENUM_CHANGE_DISPLAY_SETTINGS_FLAGS.CDS_TEST, IntPtr.Zero)

        If (result = ClassWin32.DISP_CHANGE_SUCCESSFUL) Then
            Return ClassWin32.ChangeDisplaySettingsEx(sDeviceName, mDevMode, IntPtr.Zero, ENUM_CHANGE_DISPLAY_SETTINGS_FLAGS.CDS_UPDATEREGISTRY, IntPtr.Zero)
        Else
            Return result
        End If
    End Function
End Class
