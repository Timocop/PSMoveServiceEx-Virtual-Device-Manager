Imports System.Numerics
Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerRemoteDevices
    Implements ILogAction

    Structure STRUC_DEVICE_ITEM
        Dim iId As Integer

        Dim sNickName As String
        Dim sTrackerName As String
        Dim bHasError As Boolean
        Dim iFpsCounter As Integer
    End Structure

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate() Implements ILogAction.Generate
        If (g_mFormMain.g_mUCVirtualControllers Is Nothing OrElse g_mFormMain.g_mUCVirtualControllers.g_mUCRemoteDevices Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        ' Not thread-safe
        ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                               Dim mRemoteDevices = g_mFormMain.g_mUCVirtualControllers.g_mUCRemoteDevices.GetRemoteDevices()
                                               For Each mItem In mRemoteDevices
                                                   Dim mAng As Vector3 = ClassQuaternionTools.FromQ(mItem.g_mClassIO.m_Orientation)
                                                   Dim mReset As Vector3 = ClassQuaternionTools.FromQ(mItem.g_mClassIO.m_ResetOrientation)

                                                   sTrackersList.AppendFormat("[Controller_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                                   sTrackersList.AppendFormat("ID={0}", mItem.g_mClassIO.m_Index).AppendLine()
                                                   sTrackersList.AppendFormat("NickName={0}", mItem.m_Nickname).AppendLine()
                                                   sTrackersList.AppendFormat("TrackerName={0}", mItem.m_TrackerName).AppendLine()
                                                   sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                                   sTrackersList.AppendFormat("FpsPipeCounter={0}", mItem.g_mClassIO.m_FpsPipeCounter).AppendLine()
                                                   sTrackersList.AppendFormat("Orientation={0}", String.Format("{0}, {1}, {2}",
                                                                                                                mAng.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                                                                mAng.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                                                                mAng.Z.ToString(Globalization.CultureInfo.InvariantCulture))).AppendLine()
                                                   sTrackersList.AppendFormat("ResetOrientation={0}", String.Format("{0}, {1}, {2}",
                                                                                                                mReset.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                                                                mReset.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                                                                mReset.Z.ToString(Globalization.CultureInfo.InvariantCulture))).AppendLine()
                                                   sTrackersList.AppendFormat("YawOrientationOffset={0}", mItem.g_mClassIO.m_YawOrientationOffset).AppendLine()

                                                   sTrackersList.AppendLine()
                                               Next
                                           End Sub)

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_REMOTE_DEVICES
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Throw New NotImplementedException()
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
    End Function

    Public Function GetDevices() As STRUC_DEVICE_ITEM()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mDeviceList As New List(Of STRUC_DEVICE_ITEM)
        Dim mDevoceProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("[") AndAlso sLine.EndsWith("]"c)) Then
                Dim sDeviceKey As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewDevice As New STRUC_DEVICE_ITEM

                ' Required
                While True
                    If (mDevoceProp.ContainsKey("ID")) Then
                        mNewDevice.iId = CInt(mDevoceProp("ID"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("NickName")) Then
                        mNewDevice.sNickName = mDevoceProp("NickName")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("TrackerName")) Then
                        mNewDevice.sTrackerName = mDevoceProp("TrackerName")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("HasStatusError")) Then
                        mNewDevice.bHasError = (mDevoceProp("HasStatusError").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("FpsPipeCounter")) Then
                        mNewDevice.iFpsCounter = CInt(mDevoceProp("FpsPipeCounter"))
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
