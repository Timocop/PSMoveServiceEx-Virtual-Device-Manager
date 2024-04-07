Public Class ClassConfigConst
    Public Shared ReadOnly PATH_LOG_APPLICATION_ERROR As String = IO.Path.Combine(Application.StartupPath, "application_error.ini")
    Public Shared ReadOnly PATH_CONFIG_SETTINGS As String = IO.Path.Combine(Application.StartupPath, "settings.ini")
    Public Shared ReadOnly PATH_CONFIG_ATTACHMENT As String = IO.Path.Combine(Application.StartupPath, "attach_devices.ini")
    Public Shared ReadOnly PATH_CONFIG_REMOTE As String = IO.Path.Combine(Application.StartupPath, "remote_devices.ini")
    Public Shared ReadOnly PATH_CONFIG_VMT As String = IO.Path.Combine(Application.StartupPath, "vmt_devices.ini")
    Public Shared ReadOnly PATH_CONFIG_DEVICES As String = IO.Path.Combine(Application.StartupPath, "devices.ini")
End Class
