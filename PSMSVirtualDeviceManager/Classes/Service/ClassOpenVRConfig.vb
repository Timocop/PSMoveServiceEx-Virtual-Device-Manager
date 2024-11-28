Imports System.Web.Script.Serialization

Public Class ClassOpenVRConfig
    Const STEAM_INSTALL_PATH_REGISTRY As String = "SOFTWARE\WOW6432Node\Valve\Steam"

    Private g_bConfigLoaded As Boolean = False
    Private g_mConfig As New Dictionary(Of String, Object)

    Structure STRUC_DRIVER_ITEM
        Dim sDriverName As String
        Dim sDriverPath As String

        Public Sub New(_DriverName As String, _DriverPath As String)
            sDriverName = _DriverName
            sDriverPath = _DriverPath
        End Sub
    End Structure

    Public Sub New()
    End Sub

    ReadOnly Property m_OpenVRPath As String
        Get
            Return IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "openvr")
        End Get
    End Property

    Public Sub AddPath(sPath As String)
        ValidateExternalDrivers()

        Dim mExternalDrivers = TryCast(g_mConfig("external_drivers"), ArrayList)
        If (mExternalDrivers Is Nothing) Then
            Throw New ArgumentException("Could not cast external_drivers as ArrayList")
        End If

        For Each sDriverPath As String In mExternalDrivers.ToArray
            If (sDriverPath Is Nothing) Then
                Continue For
            End If

            If (sDriverPath.ToLowerInvariant = sPath.ToLowerInvariant) Then
                Return
            End If
        Next

        mExternalDrivers.Add(sPath)
    End Sub

    Public Sub RemovePath(sPath As String)
        ValidateExternalDrivers()

        Dim mExternalDrivers = TryCast(g_mConfig("external_drivers"), ArrayList)
        If (mExternalDrivers Is Nothing) Then
            Throw New ArgumentException("Could not cast external_drivers as ArrayList")
        End If

        For i = mExternalDrivers.Count - 1 To 0 Step -1
            If (mExternalDrivers(i) Is Nothing) Then
                Continue For
            End If

            If (CStr(mExternalDrivers(i)).ToLowerInvariant = sPath.ToLowerInvariant) Then
                mExternalDrivers.RemoveAt(i)
            End If
        Next
    End Sub

    Public Function GetDrivers(Optional bSafeRead As Boolean = True) As STRUC_DRIVER_ITEM()
        ValidateExternalDrivers()

        Dim mExternalDrivers = TryCast(g_mConfig("external_drivers"), ArrayList)
        If (mExternalDrivers Is Nothing) Then
            Throw New ArgumentException("Could not cast external_drivers as ArrayList")
        End If

        Dim mDrivers As New List(Of STRUC_DRIVER_ITEM)
        For i = 0 To mExternalDrivers.Count - 1
            If (mExternalDrivers(i) Is Nothing) Then
                Continue For
            End If

            Dim sDriverName As String = Nothing
            Dim sDriverPath As String = CStr(mExternalDrivers(i))
            If (True) Then
                Dim sManifest As String = IO.Path.GetFullPath(IO.Path.Combine(sDriverPath, "driver.vrdrivermanifest"))
                If (IO.File.Exists(sManifest)) Then
                    Dim sContent As String
                    If (bSafeRead) Then
                        sContent = ClassUtils.FileReadAllTextSafe(sManifest)
                    Else
                        sContent = IO.File.ReadAllText(sManifest)
                    End If

                    Dim mManifest = (New JavaScriptSerializer).Deserialize(Of Dictionary(Of String, Object))(sContent)
                    If (mManifest IsNot Nothing) Then
                        If (mManifest.ContainsKey("name")) Then
                            sDriverName = CStr(mManifest("name"))
                        End If
                    End If
                End If
            End If

            mDrivers.Add(New STRUC_DRIVER_ITEM(sDriverName, sDriverPath))
        Next

        Return mDrivers.ToArray
    End Function

    Public Function LoadConfig(Optional bSafeRead As Boolean = True) As Boolean
        Dim sOpenVRPath As String = m_OpenVRPath
        If (sOpenVRPath Is Nothing) Then
            Return False
        End If

        Dim sConfigPath As String = IO.Path.Combine(sOpenVRPath, "openvrpaths.vrpath")
        If (Not IO.File.Exists(sConfigPath)) Then
            Return False
        End If

        Dim sContent As String
        If (bSafeRead) Then
            sContent = ClassUtils.FileReadAllTextSafe(sConfigPath)
        Else
            sContent = IO.File.ReadAllText(sConfigPath)
        End If

        g_mConfig = (New JavaScriptSerializer).Deserialize(Of Dictionary(Of String, Object))(sContent)
        If (g_mConfig Is Nothing) Then
            g_mConfig = New Dictionary(Of String, Object)
        End If

        g_bConfigLoaded = True
        Return True
    End Function

    Public Sub SaveConfig()
        If (Not g_bConfigLoaded) Then
            Return
        End If

        Dim sOpenVRPath As String = m_OpenVRPath
        If (sOpenVRPath Is Nothing) Then
            Return
        End If

        Dim sConfigPath As String = IO.Path.Combine(sOpenVRPath, "openvrpaths.vrpath")
        If (Not IO.File.Exists(sConfigPath)) Then
            Return
        End If

        ' Remove any invalid entries from the drivers list.
        RemoveInvalid()

        Dim sContent = ClassUtils.FormatJsonOutput((New JavaScriptSerializer).Serialize(g_mConfig))

        IO.File.WriteAllText(sConfigPath, sContent)
    End Sub

    Private Sub RemoveInvalid()
        ValidateExternalDrivers()

        Dim mExternalDrivers = TryCast(g_mConfig("external_drivers"), ArrayList)
        If (mExternalDrivers Is Nothing) Then
            Throw New ArgumentException("Could not cast external_drivers as ArrayList")
        End If

        ' Remove NULL drivers.
        For i = mExternalDrivers.Count - 1 To 0 Step -1
            If (mExternalDrivers(i) Is Nothing) Then
                mExternalDrivers.RemoveAt(i)
            End If
        Next

        ' Just remove the whole section if its empty.
        If (mExternalDrivers.Count = 0) Then
            g_mConfig.Remove("external_drivers")
        End If
    End Sub

    Private Sub ValidateExternalDrivers()
        If (Not g_mConfig.ContainsKey("external_drivers")) Then
            g_mConfig("external_drivers") = New ArrayList()
        End If

        If (TypeOf g_mConfig("external_drivers") IsNot ArrayList) Then
            g_mConfig("external_drivers") = New ArrayList()
        End If
    End Sub
End Class
