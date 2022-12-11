Public Class ClassLibusbDriver
    Public Const DRV_ROOT_NAME As String = "libusb_driver"
    Public Const DRV_INSTALLER_NAME As String = "amd64\install-filter.exe"
    Public ReadOnly DRV_INSTALLER_CONFIGS As String() = {
        "USB_Playstation_Eye_Camera.inf", ' Cameras that do not have any drivers installed. Shown as "Unknown Device".
        "USB_Playstation_Eye_Camera_(Interface_0).inf" ' Cameras that have another driver installed, such as WinUSB. Will be replaced with this driver instead.
    }

    Public Sub New()
    End Sub

    Public Sub InstallDriver64()
        Dim sRootFolder As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), DRV_ROOT_NAME)
        Dim sInstallerPath As String = IO.Path.Combine(sRootFolder, DRV_INSTALLER_NAME)

        For Each sDrvConfig As String In DRV_INSTALLER_CONFIGS
            Dim sDrvCOnfigPath As String = IO.Path.Combine(sRootFolder, sDrvConfig)

            Using mProcess As New Process
                mProcess.StartInfo.FileName = sInstallerPath
                mProcess.StartInfo.Arguments = String.Format("install -f=""{0}""", sDrvCOnfigPath)
                mProcess.StartInfo.WorkingDirectory = sRootFolder
                mProcess.StartInfo.CreateNoWindow = True
                mProcess.StartInfo.UseShellExecute = True

                If (Environment.OSVersion.Version.Major > 5) Then
                    mProcess.StartInfo.Verb = "runas"
                End If

                mProcess.Start()

                mProcess.WaitForExit()
                If (mProcess.ExitCode <> 0) Then
                    Throw New ArgumentException("Driver installation failed")
                End If
            End Using
        Next
    End Sub

    Public Sub UninstallDriver64()
        Dim sRootFolder As String = IO.Path.Combine(IO.Path.GetDirectoryName(Application.ExecutablePath), DRV_ROOT_NAME)
        Dim sInstallerPath As String = IO.Path.Combine(sRootFolder, DRV_INSTALLER_NAME)

        ' $TODO The first driver config seems to uninstall all drivers already? More tested required.
        For Each sDrvConfig As String In DRV_INSTALLER_CONFIGS
            Dim sDrvCOnfigPath As String = IO.Path.Combine(sRootFolder, sDrvConfig)

            Using mProcess As New Process
                mProcess.StartInfo.FileName = sInstallerPath
                mProcess.StartInfo.Arguments = String.Format("uninstall -f=""{0}""", sDrvCOnfigPath)
                mProcess.StartInfo.WorkingDirectory = sRootFolder
                mProcess.StartInfo.CreateNoWindow = True
                mProcess.StartInfo.UseShellExecute = True

                If (Environment.OSVersion.Version.Major > 5) Then
                    mProcess.StartInfo.Verb = "runas"
                End If

                mProcess.Start()

                mProcess.WaitForExit()
                If (mProcess.ExitCode <> 0) Then
                    Throw New ArgumentException("Driver uninstallation failed")
                End If
            End Using
        Next
    End Sub
End Class
