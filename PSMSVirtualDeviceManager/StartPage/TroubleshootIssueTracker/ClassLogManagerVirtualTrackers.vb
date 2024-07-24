Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs
Imports PSMSVirtualDeviceManager.UCVirtualTrackerItem.ClassCaptureLogic

Public Class ClassLogManagerVirtualTrackers
    Implements ILogAction

    Structure STRUC_DEVICE_ITEM
        Dim sPath As String
        Dim iDeviceIndex As Integer
        Dim iPipePrimaryIndex As Integer
        Dim iPipeSecondaryIndex As Integer

        Dim iCameraFramerate As Integer
        Dim iCameraResolution As ENUM_RESOLUTION
        Dim bFlipImage As Boolean
        Dim iImageInterpolation As ENUM_INTERPOLATION
        Dim bInitialized As Boolean
        Dim bIsPlayStationCamera As Boolean
        Dim bPipeConnected As Boolean
        Dim bSupersampling As Boolean
        Dim bUseMJPG As Boolean
        Dim bHasStatusError As Boolean
    End Structure

    Private g_mFormMain As FormMain

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain
    End Sub

    Public Sub Generate(mData As Dictionary(Of String, String)) Implements ILogAction.Generate
        If (g_mFormMain.g_mUCVirtualTrackers Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        ' Not thread-safe
        ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                               Dim mTrackers = g_mFormMain.g_mUCVirtualTrackers.GetAllDevices()
                                               For Each mItem In mTrackers
                                                   sTrackersList.AppendFormat("[{0}]", mItem.m_DevicePath).AppendLine()
                                                   sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                                   sTrackersList.AppendFormat("CameraFramerate={0}", mItem.g_mClassCaptureLogic.m_CameraFramerate).AppendLine()
                                                   sTrackersList.AppendFormat("CameraResolution={0}", mItem.g_mClassCaptureLogic.m_CameraResolution).AppendLine()
                                                   sTrackersList.AppendFormat("DeviceIndex={0}", mItem.g_mClassCaptureLogic.m_DeviceIndex).AppendLine()
                                                   sTrackersList.AppendFormat("FlipImage={0}", mItem.g_mClassCaptureLogic.m_FlipImage).AppendLine()
                                                   sTrackersList.AppendFormat("ImageInterpolation={0}", mItem.g_mClassCaptureLogic.m_ImageInterpolation).AppendLine()
                                                   sTrackersList.AppendFormat("Initialized={0}", mItem.g_mClassCaptureLogic.m_Initialized).AppendLine()
                                                   sTrackersList.AppendFormat("IsPlayStationCamera={0}", mItem.g_mClassCaptureLogic.m_IsPlayStationCamera).AppendLine()
                                                   sTrackersList.AppendFormat("PipeConnected={0}", mItem.g_mClassCaptureLogic.m_PipeConnected).AppendLine()
                                                   sTrackersList.AppendFormat("PipePrimaryIndex={0}", mItem.g_mClassCaptureLogic.m_PipePrimaryIndex).AppendLine()
                                                   sTrackersList.AppendFormat("PipeSecondaryIndex={0}", mItem.g_mClassCaptureLogic.m_PipeSecondaryIndex).AppendLine()
                                                   sTrackersList.AppendFormat("ShowCaptureImage={0}", mItem.g_mClassCaptureLogic.m_ShowCaptureImage).AppendLine()
                                                   sTrackersList.AppendFormat("Supersampling={0}", mItem.g_mClassCaptureLogic.m_Supersampling).AppendLine()
                                                   sTrackersList.AppendFormat("UseMJPG={0}", mItem.g_mClassCaptureLogic.m_UseMJPG).AppendLine()

                                                   sTrackersList.AppendLine()
                                               Next
                                           End Sub)

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_VIRTUAL_TRACKERS
    End Function

    Public Function GetIssues(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Throw New NotImplementedException()
    End Function

    Public Function GetSectionContent(mData As Dictionary(Of String, String)) As String Implements ILogAction.GetSectionContent
        If (Not mData.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return mData(GetActionTitle())
    End Function

    Public Function GetDevices(mData As Dictionary(Of String, String)) As STRUC_DEVICE_ITEM()
        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mDeviceList As New List(Of STRUC_DEVICE_ITEM)
        Dim mDevoceProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("[") AndAlso sLine.EndsWith("]"c)) Then
                Dim sDevicePath As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewDevice As New STRUC_DEVICE_ITEM

                mNewDevice.sPath = sDevicePath

                ' Required
                While True
                    If (mDevoceProp.ContainsKey("DeviceIndex")) Then
                        mNewDevice.iDeviceIndex = CInt(mDevoceProp("DeviceIndex"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("PipePrimaryIndex")) Then
                        mNewDevice.iPipePrimaryIndex = CInt(mDevoceProp("PipePrimaryIndex"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("PipeSecondaryIndex")) Then
                        mNewDevice.iPipeSecondaryIndex = CInt(mDevoceProp("PipeSecondaryIndex"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("CameraFramerate")) Then
                        mNewDevice.iCameraFramerate = CInt(mDevoceProp("CameraFramerate"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("CameraResolution")) Then
                        mNewDevice.iCameraResolution = CType(CInt(mDevoceProp("CameraResolution")), ENUM_RESOLUTION)
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("FlipImage")) Then
                        mNewDevice.bFlipImage = (mDevoceProp("FlipImage").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("ImageInterpolation")) Then
                        mNewDevice.iImageInterpolation = CType(CInt(mDevoceProp("ImageInterpolation")), ENUM_INTERPOLATION)
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("Initialized")) Then
                        mNewDevice.bInitialized = (mDevoceProp("Initialized").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("IsPlayStationCamera")) Then
                        mNewDevice.bIsPlayStationCamera = (mDevoceProp("IsPlayStationCamera").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("PipeConnected")) Then
                        mNewDevice.bPipeConnected = (mDevoceProp("PipeConnected").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("Supersampling")) Then
                        mNewDevice.bSupersampling = (mDevoceProp("Supersampling").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("UseMJPG")) Then
                        mNewDevice.bUseMJPG = (mDevoceProp("UseMJPG").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("HasStatusError")) Then
                        mNewDevice.bHasStatusError = (mDevoceProp("HasStatusError").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    mDeviceList.Add(mNewDevice)
                    Exit While
                End While

                mDevoceProp.Clear()
            End If

            If (sLine.Contains("="c)) Then
                Dim sKey As String = sLine.Substring(0, sLine.IndexOf("="c))
                Dim sValue As String = sLine.Remove(0, sLine.IndexOf("="c) + 1)

                mDevoceProp(sKey) = sValue
            End If
        Next

        Return mDeviceList.ToArray
    End Function
End Class
