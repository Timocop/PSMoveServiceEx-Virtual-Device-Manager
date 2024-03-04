Imports Microsoft.Win32

Public Class FormDisplayModeSelection
    Private g_bIgnoreEvents As Boolean = False

    Enum ENUM_GPU_VENDOR_FLAGS
        UNKNOWN = 0
        INTEL
        AMD
        ATI
        NVIDIA
    End Enum

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Try
            ' Disable Direct-Mode for Nvidia GPUs
            ' $TODO Try to enable Direct-Mode for Nvidia using OSVR.
            Dim iGpuVendors As ENUM_GPU_VENDOR_FLAGS = GetGraphicsCardVendor()
            If ((iGpuVendors And ENUM_GPU_VENDOR_FLAGS.NVIDIA) <> 0) Then
                RadioButton_ModeDirect.Enabled = False
                RadioButton_ModeVirtual.Checked = True
            End If
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub RadioButton_ModeDirect_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton_ModeDirect.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_bIgnoreEvents = True
        RadioButton_ModeVirtual.Checked = Not RadioButton_ModeDirect.Checked
        g_bIgnoreEvents = False
    End Sub

    Private Sub RadioButton_ModeVirtual_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton_ModeVirtual.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_bIgnoreEvents = True
        RadioButton_ModeDirect.Checked = Not RadioButton_ModeVirtual.Checked
        g_bIgnoreEvents = False
    End Sub

    Public ReadOnly Property m_ResultDirectMode As Boolean
        Get
            Return RadioButton_ModeDirect.Checked
        End Get
    End Property

    Private Function GetGraphicsCardVendor() As ENUM_GPU_VENDOR_FLAGS
        Dim iVendors As ENUM_GPU_VENDOR_FLAGS = ENUM_GPU_VENDOR_FLAGS.UNKNOWN

        Using mDisplaySubKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}", False)
            For Each mSubKey In mDisplaySubKey.GetSubKeyNames
                Try
                    Using mGpuSubKey = mDisplaySubKey.OpenSubKey(mSubKey, False)
                        Dim sVendorName As String = CStr(mGpuSubKey.GetValue("ProviderName", Nothing))

                        If (String.IsNullOrEmpty(sVendorName)) Then
                            Continue For
                        End If

                        Select Case (True)
                            Case sVendorName.ToLowerInvariant.Contains("Intel".ToLowerInvariant)
                                iVendors = iVendors Or ENUM_GPU_VENDOR_FLAGS.INTEL
                            Case sVendorName.ToLowerInvariant.Contains("Advanced Micro Devices".ToLowerInvariant)
                                iVendors = iVendors Or ENUM_GPU_VENDOR_FLAGS.AMD
                            Case sVendorName.ToLowerInvariant.Contains("ATI Technologies".ToLowerInvariant)
                                iVendors = iVendors Or ENUM_GPU_VENDOR_FLAGS.ATI
                            Case sVendorName.ToLowerInvariant.Contains("NVIDIA".ToLowerInvariant)
                                iVendors = iVendors Or ENUM_GPU_VENDOR_FLAGS.NVIDIA
                        End Select
                    End Using
                Catch ex As Exception
                    ' Just ignore the "Properties" sub key access denied exception.
                    ' We could just enumerate all PCI devices and check their driver paths... but i cant be fucked.
                End Try
            Next
        End Using

        Return iVendors
    End Function
End Class