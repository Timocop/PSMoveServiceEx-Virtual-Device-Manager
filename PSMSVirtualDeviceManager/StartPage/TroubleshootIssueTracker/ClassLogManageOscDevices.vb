Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManageOscDevices
    Implements ILogAction

    Private g_mFormMain As FormMain

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain
    End Sub

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
        If (g_mFormMain.g_mUCVirtualMotionTracker Is Nothing OrElse g_mFormMain.g_mUCVirtualMotionTracker.g_ClassOscDevices Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        Dim mVmtTrackers = g_mFormMain.g_mUCVirtualMotionTracker.g_ClassOscDevices.GetDevices
        For Each mItem In mVmtTrackers
            sTrackersList.AppendFormat("[Device_{0}]", mItem.iIndex).AppendLine()
            sTrackersList.AppendFormat("ID={0}", mItem.iIndex).AppendLine()
            sTrackersList.AppendFormat("Type={0}", mItem.iType).AppendLine()
            sTrackersList.AppendFormat("Serial={0}", mItem.sSerial).AppendLine()
            sTrackersList.AppendFormat("Position={0}", mItem.mPos.ToString).AppendLine()
            sTrackersList.AppendFormat("Orientation={0}", mItem.GetOrientationEuler().ToString).AppendLine()

            sTrackersList.AppendLine()
        Next

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_OSC_DEVICES
    End Function

    Public Function GetIssues(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Throw New NotImplementedException()
    End Function
End Class
