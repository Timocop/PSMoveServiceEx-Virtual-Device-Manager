Public Class ClassVmtConst
    Public Const VMT_TRACKER_MAX As Integer = 16 'Max trackers we can have, reserve 10 for tracking references
    Public Const VMT_DRIVER_VERSION_EXPECT As String = "VMTX_018"

    Public Const VMT_IP As String = "127.0.0.1"
    Public Const VMT_PORT_RECEIVE As Integer = 34571
    Public Const VMT_PORT_SEND As Integer = 34570

    Public Const VMT_DEVICE_NAME As String = "PSMS_EX_"
    Public Const VMT_DEVICE_SERIAL As String = "/devices/psmsex/" & VMT_DEVICE_NAME
    Public Const VMT_DRIVER_NAME As String = "psmsex"
    Public Const VMT_DRIVER_ROOT_PATH As String = "driver\psmsex"
    Public Const VMT_DRIVER_FILE As String = "driver_psmsex.dll"
End Class
