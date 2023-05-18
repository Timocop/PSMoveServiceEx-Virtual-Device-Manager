Imports System.Web.Script.Serialization

Public Class ClassOpenVRConfig
    Const STEAM_INSTALL_PATH_REGISTRY As String = "SOFTWARE\WOW6432Node\Valve\Steam"

    Private g_bConfigLoaded As Boolean = False
    Private g_mConfig As New Dictionary(Of String, Object)

    Public Sub New()
    End Sub

    ReadOnly Property m_OpenVRPath As String
        Get
            Return IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "openvr")
        End Get
    End Property

    Public Sub AddPath(sPath As String)
        If (Not g_mConfig.ContainsKey("external_drivers")) Then
            g_mConfig("external_drivers") = New ArrayList()
        End If

        Dim mExternalDrivers = TryCast(g_mConfig("external_drivers"), ArrayList)
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
        If (Not g_mConfig.ContainsKey("external_drivers")) Then
            g_mConfig("external_drivers") = New ArrayList()
        End If

        Dim mExternalDrivers = TryCast(g_mConfig("external_drivers"), ArrayList)
        If (mExternalDrivers Is Nothing) Then
            Throw New ArgumentException("Invalid cast")
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

    Public Function GetDrivers() As String()
        If (Not g_mConfig.ContainsKey("external_drivers")) Then
            Return Nothing
        End If

        Dim mExternalDrivers = TryCast(g_mConfig("external_drivers"), ArrayList)
        If (mExternalDrivers Is Nothing) Then
            Throw New ArgumentException("Invalid cast")
        End If

        Dim mDrivers As New List(Of String)
        For i = 0 To mExternalDrivers.Count - 1
            If (mExternalDrivers(i) Is Nothing) Then
                Continue For
            End If

            mDrivers.Add(CStr(mExternalDrivers(i)))
        Next

        Return mDrivers.ToArray
    End Function

    Public Function LoadConfig() As Boolean
        Dim sOpenVRPath As String = m_OpenVRPath
        If (sOpenVRPath Is Nothing) Then
            Return False
        End If

        Dim sConfigPath As String = IO.Path.Combine(sOpenVRPath, "openvrpaths.vrpath")
        If (Not IO.File.Exists(sConfigPath)) Then
            Return False
        End If

        Dim sContent As String = IO.File.ReadAllText(sConfigPath)

        g_mConfig = (New JavaScriptSerializer).Deserialize(Of Dictionary(Of String, Object))(sContent)

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

        Dim sContent = ClassUtils.FormatOutput((New JavaScriptSerializer).Serialize(g_mConfig))

        IO.File.WriteAllText(sConfigPath, sContent)
    End Sub

    Private Sub RemoveInvalid()
        If (Not g_mConfig.ContainsKey("external_drivers")) Then
            g_mConfig("external_drivers") = New ArrayList()
        End If

        Dim mExternalDrivers = TryCast(g_mConfig("external_drivers"), ArrayList)
        If (mExternalDrivers Is Nothing) Then
            Throw New ArgumentException("Invalid cast")
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

End Class
