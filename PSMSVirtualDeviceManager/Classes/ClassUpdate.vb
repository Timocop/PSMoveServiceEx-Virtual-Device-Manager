Imports System.Net
Imports System.Security.Authentication
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions

Public Class ClassUpdate
    Public Class ClassPsms
        Class STRUC_UPDATE_LOCATIONS
            Enum ENUM_VERSION_TYPE
                HASH
                VERSION
            End Enum

            Public sLocationInfo As String
            Public sVersionUrl As String
            Public sDataUrl As String
            Public sUserAgent As String
            Public iVerysionType As ENUM_VERSION_TYPE

            Public Sub New(_LocationInfo As String, _VersionUrl As String, _DataUrl As String, _UserAgent As String, _VersionType As ENUM_VERSION_TYPE)
                sLocationInfo = _LocationInfo
                sVersionUrl = _VersionUrl
                sDataUrl = _DataUrl
                sUserAgent = _UserAgent
                iVerysionType = _VersionType
            End Sub
        End Class

        Public Shared g_mUpdateLocations As STRUC_UPDATE_LOCATIONS() = {
            New STRUC_UPDATE_LOCATIONS("github.com",
                                       "https://raw.githubusercontent.com/Timocop/PSMoveServiceEx/master-experimental/updater/app_version.txt",
                                       "",
                                       String.Format("PSMS-EX Virtual Device Manager/{0} (compatible; Windows NT)", Application.ProductVersion),
                                       STRUC_UPDATE_LOCATIONS.ENUM_VERSION_TYPE.VERSION)
        }

        Public Shared Sub InstallUpdate(sPath As String, sProcessNamesKill As String())
            SetTLS12()

            If (String.IsNullOrEmpty(sPath) OrElse Not IO.Directory.Exists(sPath)) Then
                Throw New IO.DirectoryNotFoundException(String.Format("Path '{0}' does not exist", sPath))
            End If

#If Not DEBUG Then
            If (Not CheckUpdateAvailable(Application.ExecutablePath, Nothing)) Then
                Return
            End If
#End If

#If DEBUG Then
            IO.Directory.CreateDirectory(IO.Path.Combine(sPath, "UpdateTest"))
            Dim sDataPath As String = IO.Path.Combine(sPath, "UpdateTest\PsmsxUpdaterSFX.exe")
#Else
            Dim sDataPath As String = IO.Path.Combine(sPath, "VdmUpdaterSFX.exe")
#End If

            IO.File.Delete(sDataPath)

            Dim bSuccess As Boolean = False

            For Each mItem In g_mUpdateLocations
                Try
                    'Test if server files are available
                    Using mWC As New ClassWebClientEx
                        If (Not String.IsNullOrEmpty(mItem.sUserAgent)) Then
                            mWC.Headers("User-Agent") = mItem.sUserAgent
                        End If

                        IO.File.Delete(sDataPath)

                        mWC.DownloadFile(mItem.sDataUrl, sDataPath)

                        If (Not IO.File.Exists(sDataPath)) Then
                            Throw New ArgumentException("Files does not exist")
                        End If
                    End Using

                    bSuccess = True
                    Exit For
                Catch ex As Exception
                End Try
            Next

            If (Not bSuccess) Then
                Throw New ArgumentException("Unable to find update files")
            End If

#If Not DEBUG Then
            For Each pProcess As Process In Process.GetProcessesByName(IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath))
                Try
                    If (pProcess.HasExited OrElse pProcess.Id = Process.GetCurrentProcess.Id) Then
                        Continue For
                    End If

                    If (IO.Path.GetFullPath(pProcess.MainModule.FileName).ToLower <> IO.Path.GetFullPath(Application.ExecutablePath).ToLower) Then
                        Continue For
                    End If

                    pProcess.Kill()
                    pProcess.WaitForExit()
                Catch ex As Exception
                End Try
            Next
#End If

            Dim sBatchFile As String = IO.Path.Combine(sPath, "InstallUpdate.bat")

            Dim sUpdateBatch As New Text.StringBuilder
            sUpdateBatch.AppendLine("@echo off")

            For Each sProcessName As String In sProcessNamesKill
                sUpdateBatch.AppendFormat("taskkill /F /T /IM ""{0}""", sProcessName).AppendLine() 'Terminate processes
            Next

            sUpdateBatch.AppendFormat("start /w """" ""{0}"" -y", sDataPath).AppendLine() 'Run 7zip SFX and wait
            sUpdateBatch.AppendFormat("start """" ""{0}""", Application.ExecutablePath).AppendLine() 'Run setup but do not wait
            sUpdateBatch.AppendFormat("del ""{0}""", sBatchFile).AppendLine() 'KMS 

            IO.File.WriteAllText(sBatchFile, sUpdateBatch.ToString)

            Using i As New Process
                i.StartInfo.FileName = sBatchFile
                i.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(sBatchFile)

                i.StartInfo.UseShellExecute = False
                i.StartInfo.CreateNoWindow = True

                i.Start()
            End Using

#If Not DEBUG Then
            Process.GetCurrentProcess.Kill()
            End
#End If
        End Sub

        Public Shared Function CheckUpdateHash(sFile As String, ByRef r_sLocationInfo As String) As Boolean
            Dim sNextHash As String = GetNextHash(r_sLocationInfo)
            Dim sFileHash As String = HashFileSHA256(sFile)

            If (String.IsNullOrEmpty(sNextHash) OrElse String.IsNullOrEmpty(sFileHash)) Then
                Return False
            End If

            Return (sNextHash.Trim.ToUpperInvariant <> sFileHash.Trim.ToUpperInvariant)
        End Function

        Public Shared Function CheckUpdateAvailable(sFile As String, ByRef r_sLocationInfo As String) As Boolean
            Dim sNextVersion = ""
            Dim sCurrentVersion = ""
            Return CheckUpdateAvailable(sFile, r_sLocationInfo, sNextVersion, sCurrentVersion)
        End Function

        Public Shared Function CheckUpdateAvailable(sFile As String, ByRef r_sLocationInfo As String, ByRef r_sNextVersion As String, ByRef r_sCurrentVersion As String) As Boolean
            Dim sNextVersion As String = GetNextVersion(r_sLocationInfo)
            Dim sCurrentVersion As String = FileVersionInfo.GetVersionInfo(sFile).ProductVersion
            If (String.IsNullOrEmpty(sCurrentVersion)) Then
                sCurrentVersion = New Version().ToString
            End If

            If (String.IsNullOrEmpty(sNextVersion)) Then
                Return False
            End If

            sNextVersion = Regex.Match(sNextVersion, "[0-9\.]+").Value
            sCurrentVersion = Regex.Match(sCurrentVersion, "[0-9\.]+").Value

            r_sNextVersion = sNextVersion
            r_sCurrentVersion = sCurrentVersion

            Return (New Version(sNextVersion) > New Version(sCurrentVersion))
        End Function

        Public Shared Function GetNextHash(ByRef r_sLocationInfo As String) As String
            r_sLocationInfo = Nothing

            SetTLS12()

            Dim sNextHash As String = Nothing
            Dim sLocationInfo As String = Nothing

            For Each mItem In g_mUpdateLocations
                Try
                    If (mItem.iVerysionType <> STRUC_UPDATE_LOCATIONS.ENUM_VERSION_TYPE.HASH) Then
                        Continue For
                    End If

                    Using mWC As New ClassWebClientEx
                        If (Not String.IsNullOrEmpty(mItem.sUserAgent)) Then
                            mWC.Headers("User-Agent") = mItem.sUserAgent
                        End If

                        Dim sHash = mWC.DownloadString(mItem.sVersionUrl)
                        If (String.IsNullOrEmpty(sHash)) Then
                            Continue For
                        End If

                        sNextHash = sHash
                        sLocationInfo = mItem.sLocationInfo
                    End Using
                Catch ex As Exception
                End Try
            Next

            If (String.IsNullOrEmpty(sNextHash)) Then
                Throw New ArgumentException("Unable to find update files")
            End If

            r_sLocationInfo = sLocationInfo
            Return sNextHash
        End Function

        Public Shared Function GetNextVersion(ByRef r_sLocationInfo As String) As String
            r_sLocationInfo = Nothing

            SetTLS12()

            Dim sNextVersion As String = Nothing
            Dim sLocationInfo As String = Nothing

            For Each mItem In g_mUpdateLocations
                Try
                    If (mItem.iVerysionType <> STRUC_UPDATE_LOCATIONS.ENUM_VERSION_TYPE.VERSION) Then
                        Continue For
                    End If

                    Using mWC As New ClassWebClientEx
                        If (Not String.IsNullOrEmpty(mItem.sUserAgent)) Then
                            mWC.Headers("User-Agent") = mItem.sUserAgent
                        End If

                        Dim sVersion = mWC.DownloadString(mItem.sVersionUrl)
                        If (String.IsNullOrEmpty(sVersion)) Then
                            Continue For
                        End If

                        If (Not String.IsNullOrEmpty(sNextVersion)) Then
                            If (New Version(sNextVersion) > New Version(sVersion)) Then
                                Continue For
                            End If
                        End If

                        sNextVersion = sVersion
                        sLocationInfo = mItem.sLocationInfo
                    End Using
                Catch ex As Exception
                End Try
            Next

            If (String.IsNullOrEmpty(sNextVersion)) Then
                Throw New ArgumentException("Unable to find update files")
            End If

            r_sLocationInfo = sLocationInfo
            Return sNextVersion
        End Function

        Private Shared Sub SetTLS12()
            'https://stackoverflow.com/questions/43240611/net-framework-3-5-and-tls-1-2
            Const _Tls12 As SslProtocols = DirectCast(&HC00, SslProtocols)
            Const Tls12 As SecurityProtocolType = DirectCast(_Tls12, SecurityProtocolType)

            ServicePointManager.SecurityProtocol = Tls12
        End Sub
    End Class

    Public Class ClassVdm
        Class STRUC_UPDATE_LOCATIONS
            Enum ENUM_VERSION_TYPE
                HASH
                VERSION
            End Enum

            Public sLocationInfo As String
            Public sVersionUrl As String
            Public sDataUrl As String
            Public sUserAgent As String
            Public iVerysionType As ENUM_VERSION_TYPE

            Public Sub New(_LocationInfo As String, _VersionUrl As String, _DataUrl As String, _UserAgent As String, _VersionType As ENUM_VERSION_TYPE)
                sLocationInfo = _LocationInfo
                sVersionUrl = _VersionUrl
                sDataUrl = _DataUrl
                sUserAgent = _UserAgent
                iVerysionType = _VersionType
            End Sub
        End Class

        Public Shared g_mUpdateLocations As STRUC_UPDATE_LOCATIONS() = {
            New STRUC_UPDATE_LOCATIONS("github.com",
                                       "https://raw.githubusercontent.com/Timocop/PSMoveServiceEx-Virtual-Device-Manager/master/PSMSVirtualDeviceManager/updater/app_version.txt",
                                       "https://raw.githubusercontent.com/Timocop/PSMoveServiceEx-Virtual-Device-Manager/master/PSMSVirtualDeviceManager/updater/PSMSVirtualDeviceManagerUpdateSFX.dat",
                                       String.Format("PSMS-EX Virtual Device Manager/{0} (compatible; Windows NT)", Application.ProductVersion),
                                       STRUC_UPDATE_LOCATIONS.ENUM_VERSION_TYPE.VERSION)
        }

        Public Shared Sub InstallUpdate(sPath As String, sProcessNamesKill As String())
            SetTLS12()

            If (String.IsNullOrEmpty(sPath) OrElse Not IO.Directory.Exists(sPath)) Then
                Throw New IO.DirectoryNotFoundException(String.Format("Path '{0}' does not exist", sPath))
            End If

#If Not DEBUG Then
            If (Not CheckUpdateAvailable(Application.ExecutablePath, Nothing)) Then
                Return
            End If
#End If

#If DEBUG Then
            IO.Directory.CreateDirectory(IO.Path.Combine(sPath, "UpdateTest"))
            Dim sDataPath As String = IO.Path.Combine(sPath, "UpdateTest\VdmUpdaterSFX.exe")
#Else
            Dim sDataPath As String = IO.Path.Combine(sPath, "VdmUpdaterSFX.exe")
#End If

            IO.File.Delete(sDataPath)

            Dim bSuccess As Boolean = False

            For Each mItem In g_mUpdateLocations
                Try
                    'Test if server files are available
                    Using mWC As New ClassWebClientEx
                        If (Not String.IsNullOrEmpty(mItem.sUserAgent)) Then
                            mWC.Headers("User-Agent") = mItem.sUserAgent
                        End If

                        IO.File.Delete(sDataPath)

                        mWC.DownloadFile(mItem.sDataUrl, sDataPath)

                        If (Not IO.File.Exists(sDataPath)) Then
                            Throw New ArgumentException("Files does not exist")
                        End If
                    End Using

                    bSuccess = True
                    Exit For
                Catch ex As Exception
                End Try
            Next

            If (Not bSuccess) Then
                Throw New ArgumentException("Unable to find update files")
            End If

#If Not DEBUG Then
            For Each pProcess As Process In Process.GetProcessesByName(IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath))
                Try
                    If (pProcess.HasExited OrElse pProcess.Id = Process.GetCurrentProcess.Id) Then
                        Continue For
                    End If

                    If (IO.Path.GetFullPath(pProcess.MainModule.FileName).ToLower <> IO.Path.GetFullPath(Application.ExecutablePath).ToLower) Then
                        Continue For
                    End If

                    pProcess.Kill()
                    pProcess.WaitForExit()
                Catch ex As Exception
                End Try
            Next
#End If

            Dim sBatchFile As String = IO.Path.Combine(sPath, "InstallUpdate.bat")

            Dim sUpdateBatch As New Text.StringBuilder
            sUpdateBatch.AppendLine("@echo off")

            For Each sProcessName As String In sProcessNamesKill
                sUpdateBatch.AppendFormat("taskkill /F /T /IM ""{0}""", sProcessName).AppendLine() 'Terminate processes
            Next

            sUpdateBatch.AppendFormat("start /w """" ""{0}"" -y", sDataPath).AppendLine() 'Run 7zip SFX and wait
            sUpdateBatch.AppendFormat("start """" ""{0}""", Application.ExecutablePath).AppendLine() 'Run setup but do not wait
            sUpdateBatch.AppendFormat("del ""{0}""", sBatchFile).AppendLine() 'KMS 

            IO.File.WriteAllText(sBatchFile, sUpdateBatch.ToString)

            Using i As New Process
                i.StartInfo.FileName = sBatchFile
                i.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(sBatchFile)

                i.StartInfo.UseShellExecute = False
                i.StartInfo.CreateNoWindow = True

                i.Start()
            End Using

#If Not DEBUG Then
            Process.GetCurrentProcess.Kill()
            End
#End If
        End Sub

        Public Shared Function CheckUpdateHash(sFile As String, ByRef r_sLocationInfo As String) As Boolean
            Dim sNextHash As String = GetNextHash(r_sLocationInfo)
            Dim sFileHash As String = HashFileSHA256(sFile)

            If (String.IsNullOrEmpty(sNextHash) OrElse String.IsNullOrEmpty(sFileHash)) Then
                Return False
            End If

            Return (sNextHash.Trim.ToUpperInvariant <> sFileHash.Trim.ToUpperInvariant)
        End Function

        Public Shared Function CheckUpdateAvailable(sFile As String, ByRef r_sLocationInfo As String) As Boolean
            Dim sNextVersion = ""
            Dim sCurrentVersion = ""
            Return CheckUpdateAvailable(sFile, r_sLocationInfo, sNextVersion, sCurrentVersion)
        End Function

        Public Shared Function CheckUpdateAvailable(sFile As String, ByRef r_sLocationInfo As String, ByRef r_sNextVersion As String, ByRef r_sCurrentVersion As String) As Boolean
            Dim sNextVersion As String = GetNextVersion(r_sLocationInfo)
            Dim sCurrentVersion As String = FileVersionInfo.GetVersionInfo(sFile).ProductVersion
            If (String.IsNullOrEmpty(sCurrentVersion)) Then
                sCurrentVersion = New Version().ToString
            End If

            If (String.IsNullOrEmpty(sNextVersion)) Then
                Return False
            End If

            sNextVersion = Regex.Match(sNextVersion, "[0-9\.]+").Value
            sCurrentVersion = Regex.Match(sCurrentVersion, "[0-9\.]+").Value

            r_sNextVersion = sNextVersion
            r_sCurrentVersion = sCurrentVersion

            Return (New Version(sNextVersion) > New Version(sCurrentVersion))
        End Function

        Public Shared Function GetNextHash(ByRef r_sLocationInfo As String) As String
            r_sLocationInfo = Nothing

            SetTLS12()

            Dim sNextHash As String = Nothing
            Dim sLocationInfo As String = Nothing

            For Each mItem In g_mUpdateLocations
                Try
                    If (mItem.iVerysionType <> STRUC_UPDATE_LOCATIONS.ENUM_VERSION_TYPE.HASH) Then
                        Continue For
                    End If

                    Using mWC As New ClassWebClientEx
                        If (Not String.IsNullOrEmpty(mItem.sUserAgent)) Then
                            mWC.Headers("User-Agent") = mItem.sUserAgent
                        End If

                        Dim sHash = mWC.DownloadString(mItem.sVersionUrl)
                        If (String.IsNullOrEmpty(sHash)) Then
                            Continue For
                        End If

                        sNextHash = sHash
                        sLocationInfo = mItem.sLocationInfo
                    End Using
                Catch ex As Exception
                End Try
            Next

            If (String.IsNullOrEmpty(sNextHash)) Then
                Throw New ArgumentException("Unable to find update files")
            End If

            r_sLocationInfo = sLocationInfo
            Return sNextHash
        End Function

        Public Shared Function GetNextVersion(ByRef r_sLocationInfo As String) As String
            r_sLocationInfo = Nothing

            SetTLS12()

            Dim sNextVersion As String = Nothing
            Dim sLocationInfo As String = Nothing

            For Each mItem In g_mUpdateLocations
                Try
                    If (mItem.iVerysionType <> STRUC_UPDATE_LOCATIONS.ENUM_VERSION_TYPE.VERSION) Then
                        Continue For
                    End If

                    Using mWC As New ClassWebClientEx
                        If (Not String.IsNullOrEmpty(mItem.sUserAgent)) Then
                            mWC.Headers("User-Agent") = mItem.sUserAgent
                        End If

                        Dim sVersion = mWC.DownloadString(mItem.sVersionUrl)
                        If (String.IsNullOrEmpty(sVersion)) Then
                            Continue For
                        End If

                        If (Not String.IsNullOrEmpty(sNextVersion)) Then
                            If (New Version(sNextVersion) > New Version(sVersion)) Then
                                Continue For
                            End If
                        End If
                        sNextVersion = sVersion
                        sLocationInfo = mItem.sLocationInfo
                    End Using
                Catch ex As Exception
                End Try
            Next

            If (String.IsNullOrEmpty(sNextVersion)) Then
                Throw New ArgumentException("Unable to find update files")
            End If

            r_sLocationInfo = sLocationInfo
            Return sNextVersion
        End Function
    End Class

    Private Shared Sub SetTLS12()
        'https://stackoverflow.com/questions/43240611/net-framework-3-5-and-tls-1-2
        Const _Tls12 As SslProtocols = DirectCast(&HC00, SslProtocols)
        Const Tls12 As SecurityProtocolType = DirectCast(_Tls12, SecurityProtocolType)

        ServicePointManager.SecurityProtocol = Tls12
    End Sub

    Public Shared Function HashFileSHA256(sFile As String) As String
        Dim sHash As New StringBuilder

        Dim sTemp As String = ""

        Using mHash As New SHA256Managed()
            Using mFS As New IO.FileStream(sFile, IO.FileMode.Open, IO.FileAccess.Read)
                mHash.ComputeHash(mFS)
            End Using

            Dim iHash = mHash.Hash

            For ii As Integer = 0 To iHash.Length - 1
                sTemp = Convert.ToString(iHash(ii), 16)
                If (sTemp.Length = 1) Then
                    sTemp = "0" & sTemp
                End If
                sHash.Append(sTemp)
            Next

            mHash.Clear()
        End Using

        Return sHash.ToString
    End Function
End Class
