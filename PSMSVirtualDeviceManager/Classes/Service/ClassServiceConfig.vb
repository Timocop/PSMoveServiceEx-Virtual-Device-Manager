Imports System.Web.Script.Serialization

Public Class ClassServiceConfig

    Private g_mConfig As New Dictionary(Of String, Object)

    Private g_sPath As String = Nothing
    Private g_bConfigLoaded As Boolean = False

    Public Sub New(_Path As String)
        g_sPath = _Path
    End Sub

    Public Sub SetValue(Of T)(sPath As String, sKey As String, mValue As T)
        Dim sKeys As String() = sPath.Split("\"c, "/"c)

        Dim mScansDic As Dictionary(Of String, Object) = g_mConfig

        For Each sPathKey In sKeys
            If (String.IsNullOrEmpty(sPathKey)) Then
                Exit For
            End If

            If (Not mScansDic.ContainsKey(sPathKey)) Then
                mScansDic(sPathKey) = New Dictionary(Of String, Object)
            End If

            mScansDic = TryCast(mScansDic(sPathKey), Dictionary(Of String, Object))
        Next

        mScansDic(sKey) = mValue
    End Sub

    Public Function GetValue(Of T)(sPath As String, sKey As String, Optional mDefaultValue As T = Nothing) As T
        Dim sKeys As String() = sPath.Split("\"c, "/"c)

        Dim mScansDic As Dictionary(Of String, Object) = g_mConfig

        For Each sPathKey In sKeys
            If (String.IsNullOrEmpty(sPathKey)) Then
                Exit For
            End If

            If (Not mScansDic.ContainsKey(sPathKey)) Then
                Return Nothing
            End If

            mScansDic = TryCast(mScansDic(sPathKey), Dictionary(Of String, Object))
        Next

        If (Not mScansDic.ContainsKey(sKey)) Then
            If (mDefaultValue IsNot Nothing) Then
                Return mDefaultValue
            Else
                Throw New ArgumentException(String.Format("Key '{0}' does not exist!", sKey))
            End If
        End If

        Return CType(mScansDic(sKey), T)
    End Function

    Public Function LoadConfig() As Boolean
        If (g_sPath Is Nothing) Then
            g_bConfigLoaded = True
            Return False
        End If

        If (Not IO.File.Exists(g_sPath)) Then
            g_bConfigLoaded = True
            Return False
        End If

        Dim sContent As String = IO.File.ReadAllText(g_sPath)

        Dim mTmp As Object = Nothing
        g_mConfig = (New JavaScriptSerializer).Deserialize(Of Dictionary(Of String, Object))(sContent)

        g_bConfigLoaded = True
        Return True
    End Function

    Public Sub SaveConfig()
        If (g_sPath Is Nothing) Then
            Return
        End If

        If (Not g_bConfigLoaded) Then
            Return
        End If

        Dim sContent = ClassUtils.FormatJsonOutput((New JavaScriptSerializer).Serialize(g_mConfig))

        IO.File.WriteAllText(g_sPath, sContent)
    End Sub

    Public Shared Function GetConfigPath() As String
        Dim sConfigPath As String = Environment.ExpandEnvironmentVariables("%AppData%\PSMoveService")
        If (Not IO.Directory.Exists(sConfigPath)) Then
            Return Nothing
        End If

        Return sConfigPath
    End Function
End Class
