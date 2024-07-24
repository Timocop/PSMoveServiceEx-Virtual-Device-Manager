Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerVirtualTrackers
    Implements ILogAction

    Private g_mFormMain As FormMain

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain
    End Sub

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
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
        Throw New NotImplementedException()
    End Function
End Class
