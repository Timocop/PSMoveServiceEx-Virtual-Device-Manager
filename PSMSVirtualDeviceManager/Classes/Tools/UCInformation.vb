
Imports System.ComponentModel
Imports System.Drawing.Design

Public Class UCInformation
    Enum ENUM_INFO_TYPE
        INFORMATION
        WARNING
        CRITICAL
    End Enum

    Private g_iInfoType As ENUM_INFO_TYPE = ENUM_INFO_TYPE.INFORMATION
    Private g_mReadMoreAction As Action = Nothing

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
    End Sub

    <Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design", GetType(UITypeEditor))>
    Public Property m_Text As String
        Get
            Return Label_Text.Text
        End Get
        Set(value As String)
            Label_Text.Text = value
        End Set
    End Property

    Public Property m_ReadMoreText As String
        Get
            Return LinkLabel_MoreInfo.Text
        End Get
        Set(value As String)
            LinkLabel_MoreInfo.Text = value

            If (LinkLabel_MoreInfo.Text.Length > 0) Then
                LinkLabel_MoreInfo.Visible = True
            Else
                LinkLabel_MoreInfo.Visible = True
            End If
        End Set
    End Property

    Public Property m_ReadMoreAction As Action
        Get
            Return g_mReadMoreAction
        End Get
        Set(value As Action)
            g_mReadMoreAction = value
        End Set
    End Property

    Public Property m_InfoType As ENUM_INFO_TYPE
        Get
            Return g_iInfoType
        End Get
        Set(value As ENUM_INFO_TYPE)
            g_iInfoType = value

            Select Case (value)
                Case ENUM_INFO_TYPE.INFORMATION
                    ClassPictureBox_Icon.Image = My.Resources.user32_104_16x16_32
                    Panel_Color.BackColor = Color.RoyalBlue

                Case ENUM_INFO_TYPE.WARNING
                    ClassPictureBox_Icon.Image = My.Resources.user32_101_16x16_32
                    Panel_Color.BackColor = Color.Orange

                Case ENUM_INFO_TYPE.CRITICAL
                    ClassPictureBox_Icon.Image = My.Resources.netshell_1608_16x16_32
                    Panel_Color.BackColor = Color.Red
            End Select
        End Set
    End Property

    Private Sub LinkLabel_MoreInfo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_MoreInfo.LinkClicked
        If (g_mReadMoreAction Is Nothing) Then
            Return
        End If

        g_mReadMoreAction.DynamicInvoke()
    End Sub
End Class
