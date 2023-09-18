Imports System.Runtime.InteropServices

Public Class ClassDevicesNotify
    Inherits NativeWindow
    Implements IDisposable

    Private g_mDeviceNotificationHandle As IntPtr = IntPtr.Zero
    Private g_mReceiveForm As ClassReceiverForm
    Private g_iDeviceType As ENUM_DEVICE_TYPE

    Protected Class ClassWin32
        Public Const WM_DEVICECHANGE As Integer = &H219
        Public Const DBT_DEVICEARRIVAL As Integer = &H8000
        Public Const DBT_DEVICEREMOVECOMPLETE As Integer = &H8004
        Public Const DBT_DEVTYP_DEVICEINTERFACE As Integer = &H5

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
        Public Class DEV_BROADCAST_DEVICEINTERFACE
            Public dbcc_size As Integer
            Public dbcc_devicetype As Integer
            Public dbcc_reserved As Integer
            Public dbcc_classguid As Guid

            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)>
            Public dbcc_name As String
        End Class

        <StructLayout(LayoutKind.Sequential)>
        Public Class DEV_BROADCAST_HDR
            Public dbch_size As Integer
            Public dbch_devicetype As Integer
            Public dbch_reserved As Integer
        End Class

        <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function RegisterDeviceNotification(hRecipient As IntPtr, <MarshalAs(UnmanagedType.LPStruct)> NotificationFilter As DEV_BROADCAST_DEVICEINTERFACE, Flags As Integer) As IntPtr
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function UnregisterDeviceNotification(hHandle As IntPtr) As Boolean
        End Function

    End Class

    Public Event OnDeviceConnectionChanged(bConnected As Boolean, sDeviceName As String)

    'https://learn.microsoft.com/en-us/windows-hardware/drivers/install/guid-devinterface-display-adapter
    Enum ENUM_DEVICE_TYPE
        GUID_DEVINTERFACE_USB_DEVICE
        GUID_DEVINTERFACE_VIDEO_OUTPUT_ARRIVAL
        GUID_DEVINTERFACE_USB_HOST_CONTROLLER
        GUID_DEVINTERFACE_USB_HUB
        GUID_DEVINTERFACE_MONITOR
        GUID_DEVINTERFACE_HID
        GUID_DEVINTERFACE_DISPLAY_ADAPTER

        __MAX
    End Enum
    Private g_sDeviceGuid As String() = {
            "A5DCBF10-6530-11D2-901F-00C04FB951ED", ' GUID_DEVINTERFACE_USB_DEVICE
            "1AD9E4F0-F88D-4360-BAB9-4C2D55E564CD", ' GUID_DEVINTERFACE_VIDEO_OUTPUT_ARRIVAL
            "3ABF6F2D-71C4-462A-8A92-1E6861E6AF27", ' GUID_DEVINTERFACE_USB_HOST_CONTROLLER
            "F18A0E88-C30C-11D0-8815-00A0C906BED8", ' GUID_DEVINTERFACE_USB_HUB
            "E6F07B5F-EE97-4a90-B076-33F57BF4EAA7", ' GUID_DEVINTERFACE_MONITOR
            "4D1E55B2-F16F-11CF-88CB-001111000030", ' GUID_DEVINTERFACE_HID
            "5B45201D-F2F2-4F3B-85BB-30FF1F953599" ' GUID_DEVINTERFACE_DISPLAY_ADAPTER
    }

    Public Sub New()
        g_mReceiveForm = New ClassReceiverForm

        Me.AssignHandle(g_mReceiveForm.Handle)
    End Sub

    Public Sub RegisterDeviceChange(iDeviceType As ENUM_DEVICE_TYPE)
        If (g_mDeviceNotificationHandle <> IntPtr.Zero) Then
            Return
        End If

        g_iDeviceType = iDeviceType

        Dim mDBI As New ClassWin32.DEV_BROADCAST_DEVICEINTERFACE()
        mDBI.dbcc_size = Marshal.SizeOf(mDBI)
        mDBI.dbcc_name = Nothing
        mDBI.dbcc_reserved = 0
        mDBI.dbcc_classguid = New Guid(g_sDeviceGuid(iDeviceType))
        mDBI.dbcc_devicetype = ClassWin32.DBT_DEVTYP_DEVICEINTERFACE

        ' Register for device change notifications 
        g_mDeviceNotificationHandle = ClassWin32.RegisterDeviceNotification(Me.Handle, mDBI, 0)

        If (g_mDeviceNotificationHandle = IntPtr.Zero) Then
            Throw New ArgumentException("RegisterDeviceNotification failed")
        End If
    End Sub

    Public Sub UnregisterDeviceChange()
        If (g_mDeviceNotificationHandle = IntPtr.Zero) Then
            Return
        End If

        ClassWin32.UnregisterDeviceNotification(g_mDeviceNotificationHandle)
        g_mDeviceNotificationHandle = IntPtr.Zero
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        Try
            If (m.Msg = ClassWin32.WM_DEVICECHANGE AndAlso m.LParam <> IntPtr.Zero) Then
                Select Case (CInt(m.WParam))
                    Case ClassWin32.DBT_DEVICEARRIVAL
                        Dim mDBH As ClassWin32.DEV_BROADCAST_HDR = CType(Marshal.PtrToStructure(m.LParam, GetType(ClassWin32.DEV_BROADCAST_HDR)), ClassWin32.DEV_BROADCAST_HDR)

                        If (mDBH.dbch_devicetype = ClassWin32.DBT_DEVTYP_DEVICEINTERFACE) Then
                            Dim mDBI As ClassWin32.DEV_BROADCAST_DEVICEINTERFACE = CType(Marshal.PtrToStructure(m.LParam, GetType(ClassWin32.DEV_BROADCAST_DEVICEINTERFACE)), ClassWin32.DEV_BROADCAST_DEVICEINTERFACE)

                            If (mDBI.dbcc_classguid = New Guid(g_sDeviceGuid(g_iDeviceType))) Then
                                RaiseEvent OnDeviceConnectionChanged(True, mDBI.dbcc_name)
                            End If
                        End If
                    Case ClassWin32.DBT_DEVICEREMOVECOMPLETE
                        Dim mDBH As ClassWin32.DEV_BROADCAST_HDR = CType(Marshal.PtrToStructure(m.LParam, GetType(ClassWin32.DEV_BROADCAST_HDR)), ClassWin32.DEV_BROADCAST_HDR)

                        If (mDBH.dbch_devicetype = ClassWin32.DBT_DEVTYP_DEVICEINTERFACE) Then
                            Dim mDBI As ClassWin32.DEV_BROADCAST_DEVICEINTERFACE = CType(Marshal.PtrToStructure(m.LParam, GetType(ClassWin32.DEV_BROADCAST_DEVICEINTERFACE)), ClassWin32.DEV_BROADCAST_DEVICEINTERFACE)

                            If (mDBI.dbcc_classguid = New Guid(g_sDeviceGuid(g_iDeviceType))) Then
                                RaiseEvent OnDeviceConnectionChanged(False, mDBI.dbcc_name)
                            End If
                        End If
                End Select
            End If
        Catch ex As Exception
        End Try

        MyBase.WndProc(m)
    End Sub

    Class ClassReceiverForm
        Inherits Form
    End Class

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                UnregisterDeviceChange()

                If (g_mReceiveForm IsNot Nothing AndAlso Not g_mReceiveForm.IsDisposed) Then
                    g_mReceiveForm.Dispose()
                    g_mReceiveForm = Nothing
                End If

                Me.ReleaseHandle()
                Me.DestroyHandle()
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
