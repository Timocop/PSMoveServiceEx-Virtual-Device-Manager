Imports System.Net

Public Class ClassWebClientEx
    Inherits WebClient

    Private g_iTimeout As Integer

    ''' <summary>
    ''' Connection timeout in milliseconds
    ''' </summary>
    Public Property m_Timeout As Integer
        Get
            Return g_iTimeout
        End Get
        Set(value As Integer)
            g_iTimeout = value
        End Set
    End Property

    Public Sub New()
        Me.New(60000)
    End Sub

    Public Sub New(iTimeout As Integer)
        Me.m_Timeout = iTimeout
    End Sub

    Protected Overrides Function GetWebRequest(mAddress As Uri) As WebRequest
        Dim mRequest = MyBase.GetWebRequest(mAddress)

        If mRequest IsNot Nothing Then
            mRequest.Timeout = Me.m_Timeout
        End If

        Return mRequest
    End Function
End Class
