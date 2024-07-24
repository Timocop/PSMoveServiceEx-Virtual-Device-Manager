Imports System.Numerics
Imports PSMSVirtualDeviceManager
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
            Dim mPos As Vector3 = mItem.mPos
            Dim mAng As Vector3 = mItem.GetOrientationEuler()

            sTrackersList.AppendFormat("[Device_{0}]", mItem.iIndex).AppendLine()
            sTrackersList.AppendFormat("ID={0}", mItem.iIndex).AppendLine()
            sTrackersList.AppendFormat("Type={0}", mItem.iType).AppendLine()
            sTrackersList.AppendFormat("Serial={0}", mItem.sSerial).AppendLine()
            sTrackersList.AppendFormat("Position={0}", String.Format("{0}, {1}, {2}",
                                                                            mPos.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mPos.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mPos.Z.ToString(Globalization.CultureInfo.InvariantCulture)
                                                                        )).AppendLine()
            sTrackersList.AppendFormat("Orientation={0}", String.Format("{0}, {1}, {2}",
                                                                            mAng.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mAng.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mAng.Z.ToString(Globalization.CultureInfo.InvariantCulture)
                                                                        )).AppendLine()

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

    Public Function GetSectionContent(mData As Dictionary(Of String, String)) As String Implements ILogAction.GetSectionContent
        If (Not mData.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return mData(GetActionTitle())
    End Function
End Class
