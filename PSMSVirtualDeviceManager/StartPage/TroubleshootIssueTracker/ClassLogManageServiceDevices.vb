Imports System.Numerics
Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManageServiceDevices
    Implements ILogAction

    Private g_mFormMain As FormMain

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain
    End Sub

    Public Sub DoWork(mData As Dictionary(Of String, String)) Implements ILogAction.DoWork
        If (g_mFormMain.g_mPSMoveServiceCAPI Is Nothing) Then
            Return
        End If

        If (Not g_mFormMain.g_mPSMoveServiceCAPI.m_IsServiceConnected) Then
            Throw New ArgumentException("Service not connected")
        End If

        Dim sTrackersList As New Text.StringBuilder

        Dim mControllers = g_mFormMain.g_mPSMoveServiceCAPI.GetControllersData
        For Each mItem In mControllers
            Dim mPos As Vector3 = mItem.m_Position
            Dim mAng As Vector3 = mItem.GetOrientationEuler()

            sTrackersList.AppendFormat("[Controller_{0}]", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("ID={0}", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("IsConnected={0}", mItem.m_IsConnected).AppendLine()
            sTrackersList.AppendFormat("IsTracking={0}", mItem.m_IsTracking).AppendLine()
            sTrackersList.AppendFormat("IsValid={0}", mItem.m_IsValid).AppendLine()
            sTrackersList.AppendFormat("Serial={0}", mItem.m_Serial).AppendLine()
            sTrackersList.AppendFormat("TrackingColor={0}", mItem.m_TrackingColor).AppendLine()
            sTrackersList.AppendFormat("BatteryLevel={0}", mItem.m_BatteryLevel).AppendLine()
            sTrackersList.AppendFormat("OutputSeqNum={0}", mItem.m_OutputSeqNum).AppendLine()
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

        Dim mHmds = g_mFormMain.g_mPSMoveServiceCAPI.GetHmdsData
        For Each mItem In mHmds
            Dim mPos As Vector3 = mItem.m_Position
            Dim mAng As Vector3 = mItem.GetOrientationEuler()

            sTrackersList.AppendFormat("[Hmd_{0}]", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("ID={0}", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("IsConnected={0}", mItem.m_IsConnected).AppendLine()
            sTrackersList.AppendFormat("IsTracking={0}", mItem.m_IsTracking).AppendLine()
            sTrackersList.AppendFormat("IsValid={0}", mItem.m_IsValid).AppendLine()
            sTrackersList.AppendFormat("Serial={0}", mItem.m_Serial).AppendLine()
            sTrackersList.AppendFormat("OutputSeqNum={0}", mItem.m_OutputSeqNum).AppendLine()
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

        Dim mTrakcers = g_mFormMain.g_mPSMoveServiceCAPI.GetTrackersData
        For Each mItem In mTrakcers
            Dim mPos As Vector3 = mItem.m_Position
            Dim mAng As Vector3 = mItem.GetOrientationEuler()

            sTrackersList.AppendFormat("[Tracker_{0}]", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("ID={0}", mItem.m_Id).AppendLine()
            sTrackersList.AppendFormat("Path={0}", mItem.m_Path).AppendLine()
            sTrackersList.AppendFormat("OutputSeqNum={0}", mItem.m_OutputSeqNum).AppendLine()
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
        Return SECTION_VDM_SERVICE_DEVICES
    End Function

    Public Function GetIssues(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Throw New NotImplementedException()
    End Function

    Public Function GetSectionContent(mData As Dictionary(Of String, String)) As String Implements ILogAction.GetSectionContent
        Throw New NotImplementedException()
    End Function
End Class
