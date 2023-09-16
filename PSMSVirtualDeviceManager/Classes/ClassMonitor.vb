Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Text

Public Class ClassMonitor
    <DllImport("user32.dll")>
    Public Shared Function ChangeDisplaySettingsEx(lpszDeviceName As String, ByRef lpDevMode As DEVMODE, hwnd As IntPtr, dwflags As ENUM_CHANGE_DISPLAY_SETTINGS_FLAGS, lParam As IntPtr) As ENUM_DISPLAY_SETTINGS_ERROR
    End Function

    <DllImport("user32.dll")>
    Public Shared Function EnumDisplayDevices(lpDevice As String, iDevNum As Integer, ByRef lpDisplayDevice As DISPLAY_DEVICE, dwFlags As Integer) As Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Ansi)>
    Public Shared Function EnumDisplaySettings(lpszDeviceName As String, iModeNum As Integer, ByRef lpDevMode As DEVMODE) As Boolean
    End Function

    Const ENUM_CURRENT_SETTINGS As Integer = -1
    Const ENUM_REGISTRY_SETTINGS As Integer = -2
    Const DISP_CHANGE_SUCCESSFUL As Integer = 0
    Const DISP_CHANGE_RESTART As Integer = 1
    Const DISP_CHANGE_FAILED As Integer = -1
    Const DISP_CHANGE_BADMODE As Integer = -2
    Const DISP_CHANGE_NOTUPDATED As Integer = -3
    Const DM_BITSPERPEL As Integer = &H40000
    Const DM_PELSWIDTH As Integer = &H80000
    Const DM_PELSHEIGHT As Integer = &H100000
    Const DM_DISPLAYFREQUENCY As Integer = &H400000
    Const CCHFORMNAME As Integer = 32

    Public Interface INullable
    End Interface

    <StructLayout(LayoutKind.Sequential)>
    Public Structure DEVMODE
        Implements INullable

        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CCHFORMNAME)>
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
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CCHFORMNAME)>
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
        Public StateFlags As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)>
        Public DeviceID As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)>
        Public DeviceKey As String
    End Structure

    Public Enum ENUM_DISPLAY_SETTINGS_ERROR
        DISP_CHANGE_SUCCESSFUL = 0
        DISP_CHANGE_RESTART = 1
        DISP_CHANGE_FAILED = -1
        DISP_CHANGE_BADMODE = -2
        DISP_CHANGE_NOTUPDATED = -3
    End Enum

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

    Public Function GetMonitorList() As DISPLAY_DEVICE()
        Dim mMonitors As New List(Of DISPLAY_DEVICE)

        Dim mDisplayDevice As New DISPLAY_DEVICE()
        mDisplayDevice.cb = Marshal.SizeOf(mDisplayDevice)

        Dim i As Integer = 0
        While (EnumDisplayDevices(Nothing, i, mDisplayDevice, 0))

            Dim mMonitorDevice As New DISPLAY_DEVICE()
            mMonitorDevice.cb = Marshal.SizeOf(mMonitorDevice)

            If (EnumDisplayDevices(mDisplayDevice.DeviceName, 0, mMonitorDevice, 0)) Then
                mMonitorDevice.DeviceName = mDisplayDevice.DeviceName
                mMonitors.Add(mMonitorDevice)
            End If

            i += 1
        End While

        Return mMonitors.ToArray
    End Function

    Public Shared Function ChangeRefreshRateForMonitor(sDeviceName As String, iDisplayFrequency As Integer) As ENUM_DISPLAY_SETTINGS_ERROR
        Dim mDevMode As New DEVMODE()
        mDevMode.dmSize = CType(Marshal.SizeOf(GetType(DEVMODE)), Short)

        If (Not EnumDisplaySettings(sDeviceName, ENUM_CURRENT_SETTINGS, mDevMode)) Then
            Return ENUM_DISPLAY_SETTINGS_ERROR.DISP_CHANGE_FAILED
        End If

        mDevMode.dmDisplayFrequency = iDisplayFrequency
        Dim result As ENUM_DISPLAY_SETTINGS_ERROR = ChangeDisplaySettingsEx(sDeviceName, mDevMode, IntPtr.Zero, ENUM_CHANGE_DISPLAY_SETTINGS_FLAGS.CDS_TEST, IntPtr.Zero)

        If (result = DISP_CHANGE_SUCCESSFUL) Then
            result = ChangeDisplaySettingsEx(sDeviceName, mDevMode, IntPtr.Zero, ENUM_CHANGE_DISPLAY_SETTINGS_FLAGS.CDS_UPDATEREGISTRY, IntPtr.Zero)

            If (result = DISP_CHANGE_SUCCESSFUL) Then
                Return result
            Else
                Return result
            End If
        Else
            Return result
        End If
    End Function

    Public Function GetMonitorInfo(sDeviceName As String) As DEVMODE
        Dim mDevMode As New DEVMODE()
        mDevMode.dmSize = CType(Marshal.SizeOf(GetType(DEVMODE)), Short)

        If (EnumDisplaySettings(sDeviceName, ENUM_CURRENT_SETTINGS, mDevMode)) Then
            Return Nothing
        End If

        Return mDevMode
    End Function
End Class
