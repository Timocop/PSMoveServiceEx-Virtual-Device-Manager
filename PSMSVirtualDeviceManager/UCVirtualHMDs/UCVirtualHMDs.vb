Public Class UCVirtualHMDs
    Private g_bIgnoreEvents As Boolean = False

    Public g_mFormMain As FormMain
    Private g_bInit As Boolean = False

    Public Sub New(_mFormMain As FormMain)
        g_mFormMain = _mFormMain

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        Try
            g_bIgnoreEvents = True

            ComboBox_VirtualHMDCount.Items.Clear()
            For i = 0 To ClassSerivceConst.PSMOVESERVICE_MAX_HMD_COUNT
                ComboBox_VirtualHMDCount.Items.Add(CStr(i))
            Next
        Finally
            g_bIgnoreEvents = False
        End Try

        CreateControl()
    End Sub

    Public Sub Init()
        If (g_bInit) Then
            Return
        End If

        g_bInit = True

        Try
            g_bIgnoreEvents = True

            LoadSettings()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub ComboBox_VirtualHMDCount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_VirtualHMDCount.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Try
            Dim mTrackerConfig As New ClassServiceConfig(GetConfig())
            mTrackerConfig.LoadConfig()
            mTrackerConfig.SetValue("", "virtual_hmd_count", ComboBox_VirtualHMDCount.SelectedIndex)
            mTrackerConfig.SaveConfig()

            g_mFormMain.PromptRestartPSMoveService()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Function GetConfig() As String
        Dim sConfigPath As String = ClassServiceConfig.GetConfigPath()
        If (sConfigPath Is Nothing) Then
            Return Nothing
        End If

        Return IO.Path.Combine(sConfigPath, "HMDManagerConfig.json")
    End Function

    Private Sub LoadSettings()
        Try
            g_bIgnoreEvents = True

            Dim mTrackerConfig As New ClassServiceConfig(GetConfig())
            mTrackerConfig.LoadConfig()

            SetComboBoxValueClamp(ComboBox_VirtualHMDCount, mTrackerConfig.GetValue(Of Integer)("", "virtual_hmd_count", 0))
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Private Sub SetComboBoxValueClamp(mControl As ComboBox, iValue As Integer)
        If (mControl.Items.Count = 0) Then
            Return
        End If

        mControl.SelectedIndex = Math.Max(0, Math.Min(mControl.Items.Count - 1, iValue))
    End Sub
End Class
