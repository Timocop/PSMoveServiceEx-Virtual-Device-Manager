Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerHardware
    Implements ILogAction

    Structure STRUC_DEVICE_ITEM
        Dim sPath As String

        Dim sName As String
        Dim sManufacture As String
        Dim sExpectedService As String
        Dim sDriverInfPath As String
        Dim sProviderDescription As String
        Dim sProviderName As String
        Dim sProviderVersion As String
        Dim sService As String
        Dim bHasDriverInstalled As Boolean
        Dim bIsEnabled As Boolean
        Dim bIsRemoved As Boolean
        Dim iConfigFlags As ClassLibusbDriver.DEVICE_CONFIG_FLAGS
        Dim bIsCorrectDrvierInstalled As Boolean
    End Structure

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate() Implements ILogAction.Generate
        Dim sTrackersList As New Text.StringBuilder

        Dim mLibusbDriver As New ClassLibusbDriver

        Dim mUsbDevices As New Dictionary(Of String, ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO)
        For Each mDevice In mLibusbDriver.GetAllDevices("USB")
            If (String.IsNullOrEmpty(mDevice.sService)) Then
                Continue For
            End If

            Dim bSuccess As Boolean = False

            Select Case (mDevice.sService.ToUpperInvariant)
                Case ClassLibusbDriver.USBVIDEO_SERVICE_NAME.ToUpperInvariant,
                                ClassLibusbDriver.BTHUSB_SERVICE_NAME.ToUpperInvariant

                    bSuccess = True
            End Select

            If (bSuccess) Then
                Dim sVID As String = Nothing
                Dim sPID As String = Nothing
                Dim sMM As String = Nothing
                Dim sSerial As String = Nothing
                If (Not mLibusbDriver.ResolveHardwareID(mDevice.sDeviceID, sVID, sPID, sMM, sSerial)) Then
                    Continue For
                End If

                If (String.IsNullOrEmpty(mDevice.sProviderDescription)) Then
                    Continue For
                End If

                Dim mKnownConfig As New ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO(mDevice.sProviderDescription, "", sVID, sPID, sMM, mDevice.sService)

                mUsbDevices(String.Format("{0}/{1}/{2}", sVID, sPID, If(sMM, "XX"))) = mKnownConfig
            End If
        Next

        Dim mTmpList As New List(Of ClassLibusbDriver.STRUC_DEVICE_DRIVER_INFO)
        mTmpList.AddRange(ClassLibusbDriver.DRV_PS4CAM_KNOWN_CONFIGS)
        mTmpList.AddRange(ClassLibusbDriver.DRV_PSEYE_KNOWN_CONFIGS)
        mTmpList.AddRange(ClassLibusbDriver.DRV_PSVR_KNOWN_CONFIGS)
        mTmpList.AddRange(ClassLibusbDriver.DRV_PSMOVE_KNOWN_CONFIGS)
        mTmpList.AddRange(ClassLibusbDriver.DRV_CONTROLLER_KNOWN_CONFIGS)
        mTmpList.AddRange(ClassLibusbDriver.DRV_DUALSHOCK_KNOWN_CONFIGS)

        For Each mDevice In mTmpList
            mUsbDevices(String.Format("{0}/{1}/{2}", mDevice.VID, mDevice.PID, If(mDevice.MM, "XX"))) = mDevice
        Next

        For Each mDevice In mUsbDevices
            For Each mProvider In mLibusbDriver.GetDeviceProvider(mDevice.Value.VID, mDevice.Value.PID, mDevice.Value.MM, "USB", "")
                Dim sVID As String = Nothing
                Dim sPID As String = Nothing
                Dim sMM As String = Nothing
                Dim sSerial As String = Nothing
                If (mLibusbDriver.ResolveHardwareID(mProvider.sDeviceID, sVID, sPID, sMM, sSerial)) Then
                    If (Not mLibusbDriver.IsUsbDeviceConnected(mDevice.Value.VID, mDevice.Value.PID, "USB", sSerial)) Then
                        Continue For
                    End If
                End If

                sTrackersList.AppendFormat("[{0}]", mProvider.sDeviceID).AppendLine()
                sTrackersList.AppendFormat("Name={0}", mDevice.Value.sName).AppendLine()
                sTrackersList.AppendFormat("Manufacture={0}", mDevice.Value.sManufacture).AppendLine()
                sTrackersList.AppendFormat("ExpectedService={0}", mDevice.Value.sService).AppendLine()

                sTrackersList.AppendFormat("DriverInfPath={0}", mProvider.sDriverInfPath).AppendLine()
                sTrackersList.AppendFormat("ProviderDescription={0}", mProvider.sProviderDescription).AppendLine()
                sTrackersList.AppendFormat("ProviderName={0}", mProvider.sProviderName).AppendLine()
                sTrackersList.AppendFormat("ProviderVersion={0}", mProvider.sProviderVersion).AppendLine()
                sTrackersList.AppendFormat("Service={0}", mProvider.sService).AppendLine()
                sTrackersList.AppendFormat("HasDriverInstalled={0}", mProvider.HasDriverInstalled).AppendLine()
                sTrackersList.AppendFormat("IsEnabled={0}", mProvider.IsEnabled).AppendLine()
                sTrackersList.AppendFormat("IsRemoved={0}", mProvider.IsRemoved).AppendLine()
                sTrackersList.AppendFormat("ConfigFlags={0}", CInt(mProvider.iConfigFlags)).AppendLine()
                sTrackersList.AppendFormat("ConfigFlagsName={0}", mProvider.iConfigFlags.ToString).AppendLine()

                If (Not mProvider.HasDriverInstalled()) Then
                    sTrackersList.AppendFormat("IsCorrectDrvierInstalled={0}", "false").AppendLine()
                Else
                    If (String.IsNullOrEmpty(mDevice.Value.sService) AndAlso String.IsNullOrEmpty(mProvider.sService)) Then
                        sTrackersList.AppendFormat("IsCorrectDrvierInstalled={0}", "true").AppendLine()
                    Else
                        If (Not String.IsNullOrEmpty(mDevice.Value.sService) AndAlso Not String.IsNullOrEmpty(mProvider.sService)) Then
                            sTrackersList.AppendFormat("IsCorrectDrvierInstalled={0}", mProvider.sService.ToUpperInvariant = mDevice.Value.sService.ToUpperInvariant).AppendLine()
                        Else
                            sTrackersList.AppendFormat("IsCorrectDrvierInstalled={0}", "false").AppendLine()
                        End If
                    End If
                End If

                sTrackersList.AppendLine()
            Next
        Next

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_CONNECTED_HARDWARE
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckBadDrivers())
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
    End Function

    Public Function CheckBadDrivers() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            "Required drivers not installed",
            "Drivers for device {0} ({1}) are not installed and might not work correctly.",
            "Install all nessecary drivers.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        ' Check if ID even point to existing devices
        For Each mDevice In GetDevices()
            If (mDevice.bIsCorrectDrvierInstalled) Then
                Continue For
            End If

            Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
            mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.sPath, mDevice.sName)
            mIssues.Add(mIssue)
        Next

        Return mIssues.ToArray
    End Function

    Public Function GetDevices() As STRUC_DEVICE_ITEM()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mDeviceList As New List(Of STRUC_DEVICE_ITEM)
        Dim mDeviceProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("[") AndAlso sLine.EndsWith("]"c)) Then
                Dim sDeviceKey As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewDevice As New STRUC_DEVICE_ITEM

                ' Required
                While True
                    mNewDevice.sPath = sDeviceKey

                    If (mDeviceProp.ContainsKey("Name")) Then
                        mNewDevice.sName = mDeviceProp("Name")
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("Manufacture")) Then
                        mNewDevice.sManufacture = mDeviceProp("Manufacture")
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("ExpectedService")) Then
                        mNewDevice.sExpectedService = mDeviceProp("ExpectedService")
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("DriverInfPath")) Then
                        mNewDevice.sDriverInfPath = mDeviceProp("DriverInfPath")
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("ProviderDescription")) Then
                        mNewDevice.sProviderDescription = mDeviceProp("ProviderDescription")
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("ProviderName")) Then
                        mNewDevice.sProviderName = mDeviceProp("ProviderName")
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("ProviderVersion")) Then
                        mNewDevice.sProviderVersion = mDeviceProp("ProviderVersion")
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("Service")) Then
                        mNewDevice.sService = mDeviceProp("Service")
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("HasDriverInstalled")) Then
                        mNewDevice.bHasDriverInstalled = (mDeviceProp("HasDriverInstalled").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("IsEnabled")) Then
                        mNewDevice.bIsEnabled = (mDeviceProp("IsEnabled").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("IsRemoved")) Then
                        mNewDevice.bIsRemoved = (mDeviceProp("IsRemoved").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("ConfigFlags")) Then
                        mNewDevice.iConfigFlags = CType(CInt(mDeviceProp("ConfigFlags")), ClassLibusbDriver.DEVICE_CONFIG_FLAGS)
                    Else
                        Exit While
                    End If

                    If (mDeviceProp.ContainsKey("IsCorrectDrvierInstalled")) Then
                        mNewDevice.bIsCorrectDrvierInstalled = (mDeviceProp("IsCorrectDrvierInstalled").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    mDeviceList.Add(mNewDevice)
                    Exit While
                End While

                mDeviceProp.Clear()
            End If

            If (sLine.Contains("="c)) Then
                Dim sKey As String = sLine.Substring(0, sLine.IndexOf("="c))
                Dim sValue As String = sLine.Remove(0, sLine.IndexOf("="c) + 1)

                mDeviceProp(sKey) = sValue
            End If
        Next

        Return mDeviceList.ToArray
    End Function
End Class
