Public Class FormPlaysStationVRDisplayFrequency
    Private KNONW_DISPLAY_FREQUENCYS As Integer() = New Integer() {60, 90, 120}

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ComboBox_Frequency.BeginUpdate()
        Try
            ComboBox_Frequency.Items.Clear()

            For Each iFreq In KNONW_DISPLAY_FREQUENCYS
                ComboBox_Frequency.Items.Add(CStr(iFreq))
            Next

            ComboBox_Frequency.SelectedIndex = 0

            Dim iCurrentFreq As Integer = GetCurrentFrequency()
            If (iCurrentFreq > 0) Then
                ComboBox_Frequency.SelectedItem = CStr(iCurrentFreq)
            End If
        Finally
            ComboBox_Frequency.EndUpdate()
        End Try
    End Sub

    Private Function GetCurrentFrequency() As Integer
        Dim mMonitor As New ClassMonitor

        Dim mMonitorInfo As ClassMonitor.DEVMODE = Nothing
        If (mMonitor.FindPlaystationVrMonitor(mMonitorInfo, Nothing) = ClassMonitor.ENUM_PSVR_MONITOR_STATUS.SUCCESS) Then
            Return mMonitorInfo.dmDisplayFrequency
        End If

        Return 0
    End Function


    Private Sub Button_Apply_Click(sender As Object, e As EventArgs) Handles Button_Apply.Click
        Try
            Dim iNewFrequency As Integer = CInt(ComboBox_Frequency.SelectedItem)

            Dim mMonitor As New ClassMonitor

            Dim mMonitorInfo As ClassMonitor.DEVMODE = Nothing
            Dim mDisplayInfo As KeyValuePair(Of ClassMonitor.DISPLAY_DEVICE, ClassMonitor.MONITOR_DEVICE) = Nothing
            If (mMonitor.FindPlaystationVrMonitor(mMonitorInfo, mDisplayInfo) = ClassMonitor.ENUM_PSVR_MONITOR_STATUS.SUCCESS) Then
                Dim iResult = mMonitor.ChangeRefreshRateForMonitor(mDisplayInfo.Key, iNewFrequency)

                If (iResult <> ClassMonitor.ENUM_DISPLAY_SETTINGS_ERROR.DISP_CHANGE_SUCCESSFUL) Then
                    Throw New ArgumentException(String.Format("Changing display frequency failed with error: {0} - {1}", CInt(iResult), iResult.ToString))
                End If
            Else
                Throw New ArgumentException("Unable to find PlayStation VR display")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class