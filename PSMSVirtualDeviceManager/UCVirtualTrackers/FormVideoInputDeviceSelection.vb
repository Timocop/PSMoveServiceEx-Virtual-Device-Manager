Public Class FormVideoInputDeviceSelection

    Class ClassComboBoxDeviceInfoItem
        Inherits ClassVideoInputDevices.ClassDeviceInfo

        Public Sub New(mDeviceItem As ClassVideoInputDevices.ClassDeviceInfo)
            MyBase.New(mDeviceItem.m_Index, mDeviceItem.m_Name, mDeviceItem.m_Path, mDeviceItem.m_CLSID)

            Dim iIndex As Integer = mDeviceItem.m_Index
            Dim sName As String = mDeviceItem.m_Name
            Dim sPath As String = mDeviceItem.m_Path
            Dim sCLSID As String = mDeviceItem.m_CLSID

            Dim sNameOverride As String = ""
            For Each mDevice In ClassLibusbDriver.DRV_PS4CAM_KNOWN_CONFIGS
                Dim sHardwareId As String = String.Format("\USB#VID_{0}&PID_{1}", mDevice.VID, mDevice.PID)

                If (mDeviceItem.m_Path.ToLowerInvariant.Contains(sHardwareId.ToLowerInvariant)) Then
                    sNameOverride = mDevice.sName
                    Exit For
                End If
            Next

            If (Not String.IsNullOrEmpty(sNameOverride)) Then
                sName = sNameOverride
            End If

            m_Name = sName
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("ID: {0}, Name: {1}", m_Index, m_Name)
        End Function
    End Class

    Structure STRUC_DIALOG_RESULT
        Public bIsValid As Boolean
        Public mVideoInputDeviceItem As ClassComboBoxDeviceInfoItem
    End Structure

    ReadOnly Property m_DialogResult As STRUC_DIALOG_RESULT
        Get
            Dim mResult As New STRUC_DIALOG_RESULT

            If (ComboBox_Devices.SelectedItem IsNot Nothing) Then
                If (TypeOf ComboBox_Devices.SelectedItem Is ClassComboBoxDeviceInfoItem) Then
                    mResult.mVideoInputDeviceItem = DirectCast(ComboBox_Devices.SelectedItem, ClassComboBoxDeviceInfoItem)
                    mResult.bIsValid = True
                End If
            End If

            Return mResult
        End Get
    End Property

    Private Sub FormVideoInputDeviceSelection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ComboBox_Devices.Items.Clear()

            ' Fill the list with video input devices.
            Dim mDeviceList As New List(Of ClassVideoInputDevices.ClassDeviceInfo)
            If (ClassVideoInputDevices.GetDevicesOfVideoInput(mDeviceList)) Then
                For i = 0 To mDeviceList.Count - 1
                    ComboBox_Devices.Items.Add(New ClassComboBoxDeviceInfoItem(mDeviceList(i)))
                Next
            End If

            ' Nothing found? We better not set the index otherwise we error.
            If (ComboBox_Devices.Items.Count > 0) Then
                ComboBox_Devices.SelectedIndex = 0
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub Button_Add_Click(sender As Object, e As EventArgs) Handles Button_Add.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class