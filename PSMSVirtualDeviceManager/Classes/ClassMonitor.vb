Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Text
Imports Microsoft.Win32

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

    Public Const PSVR_MONITOR_GEN1_NAME As String = "MONITOR\SNYB403"
    Public Const PSVR_MONITOR_GEN2_NAME As String = "MONITOR\SNY6A04"

    Public Const PSVR_MONITOR_GEN1_VID As Integer = &HD94D
    Public Const PSVR_MONITOR_GEN1_PID As Integer = &HB403
    Public Const PSVR_MONITOR_GEN2_VID As Integer = &HD94D
    Public Const PSVR_MONITOR_GEN2_PID As Integer = &H6A04

    <Flags>
    Public Enum DISPLAY_DEVICE_STATE As Integer
        DISPLAY_DEVICE_ATTACHED_TO_DESKTOP = &H1
        DISPLAY_DEVICE_PRIMARY_DEVICE = &H2
        DISPLAY_DEVICE_MIRRORING_DRIVER = &H4
        DISPLAY_DEVICE_VGA_COMPATIBLE = &H8
        DISPLAY_DEVICE_REMOVABLE = &H10
        DISPLAY_DEVICE_DISCONNECT = &H2000000
        DISPLAY_DEVICE_REMOTE = &H4000000
        DISPLAY_DEVICE_MODESPRUNED = &H8000000
        DISPLAY_DEVICE_MULTI_DRIVER = &H20000000
        DISPLAY_DEVICE_HARDWARE = &H80
        DISPLAY_DEVICE_ACTIVE = &H1
    End Enum

    <StructLayout(LayoutKind.Sequential)>
    Public Structure DEVMODE
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
        Public StateFlags As DISPLAY_DEVICE_STATE
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
        Public StateFlags As DISPLAY_DEVICE_STATE
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

    Public Enum ENUM_PATCHED_RESGITRY_STATE
        NOT_PATCHED
        WAITING_FOR_RELOAD
        PATCHED_DIRECT
        PATCHED_MULTI
    End Enum

    Public Enum ENUM_PSVR_MONITOR_STATUS
        SUCCESS
        ERROR_MIRRROED
        ERROR_NOT_ACTIVE
        ERROR_NOT_FOUND
    End Enum

    Public Function FindPlaystationVrMonitor(ByRef mResult As DEVMODE, ByRef mDisplayInfo As KeyValuePair(Of DISPLAY_DEVICE, MONITOR_DEVICE)) As ENUM_PSVR_MONITOR_STATUS
        For Each mInfo In GetMonitorList()
            Dim mMonitorInfo As DEVMODE = GetMonitorInfo(mInfo.Key)
            If (mInfo.Value.Length < 1) Then
                Continue For
            End If

            Dim mPrimMonitor = mInfo.Value(0)

            If (mPrimMonitor.DeviceID.StartsWith(PSVR_MONITOR_GEN1_NAME) OrElse mPrimMonitor.DeviceID.StartsWith(PSVR_MONITOR_GEN2_NAME)) Then
                mResult = mMonitorInfo
                mDisplayInfo = New KeyValuePair(Of DISPLAY_DEVICE, MONITOR_DEVICE)(mInfo.Key, mPrimMonitor)

                ' Is monitor mirrored?
                If (mInfo.Value.Length > 1) Then
                    Return ENUM_PSVR_MONITOR_STATUS.ERROR_MIRRROED
                End If

                ' Is monitor not active?
                If ((mPrimMonitor.StateFlags And DISPLAY_DEVICE_STATE.DISPLAY_DEVICE_ACTIVE) = 0) Then
                    Return ENUM_PSVR_MONITOR_STATUS.ERROR_NOT_ACTIVE
                End If

                Return ENUM_PSVR_MONITOR_STATUS.SUCCESS
            End If
        Next

        Return ENUM_PSVR_MONITOR_STATUS.ERROR_NOT_FOUND
    End Function

    Public Function GetMonitorList() As KeyValuePair(Of DISPLAY_DEVICE, MONITOR_DEVICE())()
        Dim mDisplays As New List(Of KeyValuePair(Of DISPLAY_DEVICE, MONITOR_DEVICE()))

        Dim mDisplayDevice As New DISPLAY_DEVICE()
        mDisplayDevice.cb = Marshal.SizeOf(mDisplayDevice)

        Dim i As Integer = 0
        While (ClassWin32.EnumDisplayDevices(Nothing, i, mDisplayDevice, 0))

            Dim mMonitorDevice As New MONITOR_DEVICE()
            mMonitorDevice.cb = Marshal.SizeOf(mMonitorDevice)


            Dim mMonitors As New List(Of MONITOR_DEVICE)

            ' If we get multiple monitors on one display, that means we got mirrored monitors.
            Dim j As Integer = 0
            While (ClassWin32.EnumDisplayDevices(mDisplayDevice.DeviceName, j, mMonitorDevice, 0))
                mMonitors.Add(mMonitorDevice)
                j += 1
            End While

            If (j > 0) Then
                mDisplays.Add(New KeyValuePair(Of DISPLAY_DEVICE, MONITOR_DEVICE())(mDisplayDevice, mMonitors.ToArray))
            End If

            i += 1
        End While

        Return mDisplays.ToArray
    End Function

    Public Function GetMonitorInfo(mDevice As DISPLAY_DEVICE) As DEVMODE
        Return GetMonitorInfo(mDevice.DeviceName)
    End Function

    Public Function GetMonitorInfo(sDeviceName As String) As DEVMODE
        Dim mDevMode As New DEVMODE()
        mDevMode.dmSize = CType(Marshal.SizeOf(mDevMode), Short)

        If (Not ClassWin32.EnumDisplaySettings(sDeviceName, ClassWin32.ENUM_CURRENT_SETTINGS, mDevMode)) Then
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

    Public Sub PatchPlaystationVrMonitor(bDirectMode As Boolean)
        Dim mClassMonitor As New ClassMonitor

        'Find the monitors registry key.
        Dim mMonitorsKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Enum\DISPLAY", True)
        If (mMonitorsKey Is Nothing) Then
            Throw New ArgumentException("Unable to open registry 'SYSTEM\CurrentControlSet\Enum\DISPLAY'")
        End If

        For Each sMonitorName As String In mMonitorsKey.GetSubKeyNames()
            Dim mMonitorKey As RegistryKey = mMonitorsKey.OpenSubKey(sMonitorName, True)
            If (mMonitorKey Is Nothing) Then
                Continue For
            End If

            For Each sId As String In mMonitorKey.GetSubKeyNames()
                Dim mIdKey As RegistryKey = mMonitorKey.OpenSubKey(sId, True)
                If (mIdKey Is Nothing) Then
                    Continue For
                End If

                If (Not PSVR_MONITOR_GEN1_NAME.EndsWith(sMonitorName) AndAlso Not PSVR_MONITOR_GEN2_NAME.EndsWith(sMonitorName)) Then
                    Continue For
                End If

                Dim mParametersKey As RegistryKey = mIdKey.OpenSubKey("Device Parameters", True)
                If (mParametersKey Is Nothing) Then
                    Continue For
                End If

                Dim mEdidOverride As RegistryKey = mParametersKey.CreateSubKey("EDID_OVERRIDE")
                If (mEdidOverride Is Nothing) Then
                    Continue For
                End If

                ' Give it a sexy friendly name for the device manager.
                mIdKey.SetValue("FriendlyName", "PSVR", RegistryValueKind.String)

                Dim iFullEDID As Byte() = New Byte() {}

                Select Case (True)
                    Case (PSVR_MONITOR_GEN1_NAME.EndsWith(sMonitorName))
                        If (bDirectMode) Then
                            iFullEDID = My.Resources.EDID_PSVR1_DIRECT
                        Else
                            iFullEDID = My.Resources.EDID_PSVR1_MULTI
                        End If

                    Case (PSVR_MONITOR_GEN2_NAME.EndsWith(sMonitorName))
                        If (bDirectMode) Then
                            iFullEDID = My.Resources.EDID_PSVR2_DIRECT
                        Else
                            iFullEDID = My.Resources.EDID_PSVR2_MULTI
                        End If

                    Case Else
                        Throw New ArgumentException("Unknown PSVR monitor hardware id")
                End Select

                Dim iSplitBase As Byte() = New Byte() {}
                Dim iSplitExt As Byte() = New Byte() {}

                If (True) Then
                    Dim iBase As New List(Of Byte)
                    Dim iExt As New List(Of Byte)

                    For i = 0 To iFullEDID.Length - 1
                        If (i < 128) Then
                            iBase.Add(iFullEDID(i))
                        Else
                            iExt.Add(iFullEDID(i))
                        End If
                    Next

                    iSplitBase = iBase.ToArray
                    iSplitExt = iExt.ToArray
                End If

                mEdidOverride.SetValue("0", iSplitBase, RegistryValueKind.Binary)
                mEdidOverride.SetValue("1", iSplitExt, RegistryValueKind.Binary)

                Exit For
            Next
        Next
    End Sub

    Public Function IsPlaystationVrMonitorPatched() As ENUM_PATCHED_RESGITRY_STATE
        Dim mClassMonitor As New ClassMonitor

        'Find the monitors registry key.
        Dim mMonitorsKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Enum\DISPLAY", False)
        If (mMonitorsKey Is Nothing) Then
            Throw New ArgumentException("Unable to open registry 'SYSTEM\CurrentControlSet\Enum\DISPLAY'")
        End If

        For Each sMonitorName As String In mMonitorsKey.GetSubKeyNames()
            Dim mMonitorKey As RegistryKey = mMonitorsKey.OpenSubKey(sMonitorName, False)
            If (mMonitorKey Is Nothing) Then
                Continue For
            End If

            For Each sId As String In mMonitorKey.GetSubKeyNames()
                Dim mIdKey As RegistryKey = mMonitorKey.OpenSubKey(sId, False)
                If (mIdKey Is Nothing) Then
                    Continue For
                End If

                If (Not PSVR_MONITOR_GEN1_NAME.EndsWith(sMonitorName) AndAlso Not PSVR_MONITOR_GEN2_NAME.EndsWith(sMonitorName)) Then
                    Continue For
                End If

                Dim mParametersKey As RegistryKey = mIdKey.OpenSubKey("Device Parameters", False)
                If (mParametersKey Is Nothing) Then
                    Continue For
                End If

                Dim mEdidOverride As RegistryKey = mParametersKey.OpenSubKey("EDID_OVERRIDE")
                If (mEdidOverride Is Nothing) Then
                    Continue For
                End If

                Dim iFullEDID_Multi = New Byte() {}
                Dim iFullEDID_Direct = New Byte() {}

                Select Case (True)
                    Case (PSVR_MONITOR_GEN1_NAME.EndsWith(sMonitorName))
                        iFullEDID_Multi = My.Resources.EDID_PSVR1_MULTI
                        iFullEDID_Direct = My.Resources.EDID_PSVR1_DIRECT

                    Case (PSVR_MONITOR_GEN2_NAME.EndsWith(sMonitorName))
                        iFullEDID_Multi = My.Resources.EDID_PSVR2_MULTI
                        iFullEDID_Direct = My.Resources.EDID_PSVR2_DIRECT
                    Case Else
                        Throw New ArgumentException("Unknown PSVR monitor hardware id")
                End Select

                Dim iSplitBase_Multi = New Byte() {}
                Dim iSplitExt_Multi = New Byte() {}
                Dim iSplitBase_Direct = New Byte() {}
                Dim iSplitExt_Direct = New Byte() {}

                ' Multi
                If (True) Then
                    Dim iBase As New List(Of Byte)
                    Dim iExt As New List(Of Byte)

                    For i = 0 To iFullEDID_Multi.Length - 1
                        If (i < 128) Then
                            iBase.Add(iFullEDID_Multi(i))
                        Else
                            iExt.Add(iFullEDID_Multi(i))
                        End If
                    Next

                    iSplitBase_Multi = iBase.ToArray
                    iSplitExt_Multi = iExt.ToArray
                End If

                ' Direct
                If (True) Then
                    Dim iBase As New List(Of Byte)
                    Dim iExt As New List(Of Byte)

                    For i = 0 To iFullEDID_Direct.Length - 1
                        If (i < 128) Then
                            iBase.Add(iFullEDID_Direct(i))
                        Else
                            iExt.Add(iFullEDID_Direct(i))
                        End If
                    Next

                    iSplitBase_Direct = iBase.ToArray
                    iSplitExt_Direct = iExt.ToArray
                End If

                ' Check if overrides are the same as VDMs EDID overrides. 
                Dim iOverrideBase As Byte() = TryCast(mEdidOverride.GetValue("0", Nothing), Byte())
                If (iOverrideBase IsNot Nothing) Then
                    If (Not IsBytesEqual(iOverrideBase, iSplitBase_Multi) AndAlso Not IsBytesEqual(iOverrideBase, iSplitBase_Direct)) Then
                        Return ENUM_PATCHED_RESGITRY_STATE.NOT_PATCHED
                    End If
                Else
                    Return ENUM_PATCHED_RESGITRY_STATE.NOT_PATCHED
                End If

                Dim iOverrideExt As Byte() = TryCast(mEdidOverride.GetValue("1", Nothing), Byte())
                If (iOverrideExt IsNot Nothing) Then
                    If (Not IsBytesEqual(iOverrideExt, iSplitExt_Multi) AndAlso Not IsBytesEqual(iOverrideExt, iSplitExt_Direct)) Then
                        Return ENUM_PATCHED_RESGITRY_STATE.NOT_PATCHED
                    End If
                Else
                    Return ENUM_PATCHED_RESGITRY_STATE.NOT_PATCHED
                End If

                ' Check if current EDID is equal to patched. If not, then reboot or replug device to update.
                Dim iCurrentEDID As Byte() = TryCast(mParametersKey.GetValue("EDID", Nothing), Byte())
                If (iCurrentEDID IsNot Nothing) Then
                    If (IsBytesEqual(iCurrentEDID, iSplitBase_Multi)) Then
                        Return ENUM_PATCHED_RESGITRY_STATE.PATCHED_MULTI
                    End If

                    If (IsBytesEqual(iCurrentEDID, iSplitBase_Direct)) Then
                        Return ENUM_PATCHED_RESGITRY_STATE.PATCHED_DIRECT
                    End If

                    Return ENUM_PATCHED_RESGITRY_STATE.WAITING_FOR_RELOAD
                Else
                    Return ENUM_PATCHED_RESGITRY_STATE.WAITING_FOR_RELOAD
                End If
            Next
        Next

        Return ENUM_PATCHED_RESGITRY_STATE.NOT_PATCHED
    End Function

    Private Function IsBytesEqual(x As Byte(), y As Byte()) As Boolean
        If (x.Length = y.Length) Then
            For i = 0 To x.Length - 1
                If (x(i) = y(i)) Then
                    Continue For
                End If

                Return False
            Next
        Else
            Return False
        End If

        Return True
    End Function
End Class
