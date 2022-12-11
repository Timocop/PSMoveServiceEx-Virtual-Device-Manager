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
            g_mConfig("external_drivers") = New Dictionary(Of String, Object)
        End If

        Dim mExternalDrivers = TryCast(g_mConfig("external_drivers"), ArrayList)
        For Each sDriverPath As String In mExternalDrivers.ToArray
            If (sDriverPath.ToLowerInvariant = sPath.ToLowerInvariant) Then
                Return
            End If
        Next

        mExternalDrivers.Add(sPath)
    End Sub

    Public Sub RemovePath(sPath As String)
        If (Not g_mConfig.ContainsKey("external_drivers")) Then
            g_mConfig("external_drivers") = New Dictionary(Of String, Object)
        End If

        Dim mExternalDrivers = TryCast(g_mConfig("external_drivers"), ArrayList)

        For i = mExternalDrivers.Count - 1 To 0 Step -1
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
            Return Nothing
        End If

        Dim mDrivers As New List(Of String)
        For i = 0 To mExternalDrivers.Count - 1
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

        Dim mTmp As Object = Nothing
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

        Dim sContent = ClassUtils.FormatOutput((New JavaScriptSerializer).Serialize(g_mConfig))

        IO.File.WriteAllText(sConfigPath, sContent)
    End Sub

End Class
