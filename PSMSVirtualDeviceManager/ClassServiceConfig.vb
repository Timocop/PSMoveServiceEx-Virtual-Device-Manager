Imports System.Text.RegularExpressions

Public Class ClassServiceConfig
    Class ClassSettingsKey
        Enum ENUM_TYPE
            BOOL
            NUM
        End Enum

        Private g_iType As ENUM_TYPE = ENUM_TYPE.BOOL
        Private g_mValue As Object = 0

        Private g_sPath As String = ""
        Private g_sSettingKey As String = ""

        Private g_bLoaded As Boolean = False

        Public Sub New(sPath As String, sSettingKey As String, iType As ENUM_TYPE)
            g_sPath = sPath
            g_sSettingKey = sSettingKey
            g_iType = iType
        End Sub

        ReadOnly Property m_Type As ENUM_TYPE
            Get
                Return g_iType
            End Get
        End Property

        ReadOnly Property m_SettingsKey As String
            Get
                Return g_sSettingKey
            End Get
        End Property

        Property m_Value As Object
            Get
                If (Not g_bLoaded) Then
                    Return Nothing
                End If

                Return g_mValue
            End Get
            Set(value As Object)
                g_mValue = value
                g_bLoaded = True
            End Set
        End Property

        Property m_ValueB As Boolean
            Get
                If (Not g_bLoaded) Then
                    Return False
                End If

                Return CBool(g_mValue)
            End Get
            Set(value As Boolean)
                g_mValue = value
                g_bLoaded = True
            End Set
        End Property

        Property m_ValueF As Double
            Get
                If (Not g_bLoaded) Then
                    Return 0.0
                End If

                Return CDbl(g_mValue)
            End Get
            Set(value As Double)
                g_mValue = value
                g_bLoaded = True
            End Set
        End Property

        ReadOnly Property m_Loaded As Boolean
            Get
                Return g_bLoaded
            End Get
        End Property

        Public Sub Load()
            Dim sText As String = IO.File.ReadAllText(g_sPath)

            Dim mMatchBool = Regex.Match(sText, "^\s*""" & g_sSettingKey & """:\s*""(?<Value>true|false)"",{0,1}\s*$")
            If (mMatchBool.Success) Then
                g_iType = ENUM_TYPE.BOOL
                g_mValue = (mMatchBool.Groups("Value").Value = "true")

                g_bLoaded = True
            Else
                Dim mMatchNum = Regex.Match(sText, "^\s*""" & g_sSettingKey & """:\s*""(?<Value>[-0-9.]+)"",{0,1}\s*$", RegexOptions.Multiline)
                If (mMatchNum.Success) Then
                    Dim sNum As String = sText.Substring(mMatchNum.Groups("Value").Index, mMatchNum.Groups("Value").Length)
                    Dim iNum As Double = Double.Parse(sNum, Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

                    g_iType = ENUM_TYPE.NUM
                    g_mValue = iNum

                    g_bLoaded = True
                Else
                    Throw New ArgumentException("Unknown value type")
                End If
            End If
        End Sub

        Public Sub Save()
            If (Not g_bLoaded) Then
                Throw New ArgumentException("Value has not been loaded yet")
            End If

            Dim sText As String = IO.File.ReadAllText(g_sPath)
            Dim mMatchNum = Regex.Match(sText, "^\s*""" & g_sSettingKey & """:\s*""(?<Value>[-a-zA-Z0-9.]+)"",{0,1}\s*$", RegexOptions.Multiline)
            If (Not mMatchNum.Success) Then
                Throw New ArgumentException(String.Format("Could not find key '{0}'", g_sSettingKey))
            End If

            sText = sText.Remove(mMatchNum.Groups("Value").Index, mMatchNum.Groups("Value").Length)

            Select Case (g_iType)
                Case ENUM_TYPE.BOOL
                    sText = sText.Insert(mMatchNum.Groups("Value").Index, If(CBool(g_mValue), "true", "false"))
                Case Else
                    sText = sText.Insert(mMatchNum.Groups("Value").Index, CDbl(g_mValue).ToString(Globalization.CultureInfo.InvariantCulture))
            End Select

            IO.File.WriteAllText(g_sPath, sText)
        End Sub
    End Class
End Class
