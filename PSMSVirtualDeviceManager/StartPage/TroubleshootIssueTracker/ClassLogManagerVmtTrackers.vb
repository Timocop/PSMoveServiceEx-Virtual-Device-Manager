Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs
Imports PSMSVirtualDeviceManager.UCVirtualMotionTrackerItem.ClassIO

Public Class ClassLogManagerVmtTrackers
    Implements ILogAction

    Enum ENUM_DEVICE_TYPE
        INVALID = 0
        CONTROLLER
        HMD
    End Enum

    Structure STRUC_DEVICE_ITEM
        Dim iId As Integer
        Dim iVmtId As Integer
        Dim iType As ENUM_DEVICE_TYPE

        Dim iVmtTrackerRole As ENUM_TRACKER_ROLE
        Dim bHasStatusError As Boolean
        Dim iFpsOscCounter As Integer
    End Structure

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate() Implements ILogAction.Generate
        If (g_mFormMain.g_mUCVirtualMotionTracker Is Nothing OrElse g_mFormMain.g_mUCVirtualMotionTracker.g_UCVmtTrackers Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        ' Not thread-safe
        ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                               Dim mVmtTrackers = g_mFormMain.g_mUCVirtualMotionTracker.g_UCVmtTrackers.GetVmtTrackers()
                                               For Each mItem In mVmtTrackers
                                                   If (mItem.g_mClassIO.m_IsHMD) Then
                                                       sTrackersList.AppendFormat("[Hmd_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                                   Else
                                                       sTrackersList.AppendFormat("[Controller_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                                   End If
                                                   sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                                   sTrackersList.AppendFormat("ID={0}", mItem.g_mClassIO.m_Index).AppendLine()
                                                   sTrackersList.AppendFormat("VmtID={0}", mItem.g_mClassIO.m_VmtTracker).AppendLine()
                                                   sTrackersList.AppendFormat("VmtTrackerRole={0}", CInt(mItem.g_mClassIO.m_VmtTrackerRole)).AppendLine()
                                                   sTrackersList.AppendFormat("VmtTrackerRoleName={0}", mItem.g_mClassIO.m_VmtTrackerRole.ToString).AppendLine()
                                                   sTrackersList.AppendFormat("FpsOscCounter={0}", mItem.g_mClassIO.m_FpsOscCounter).AppendLine()

                                                   sTrackersList.AppendLine()
                                               Next
                                           End Sub)

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_VMT_TRACKERS
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
                Dim sDevicePath As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewDevice As New STRUC_DEVICE_ITEM

                ' Required
                While True
                    Select Case (True)
                        Case sDevicePath.StartsWith("Controller_")
                            mNewDevice.iType = ENUM_DEVICE_TYPE.CONTROLLER

                        Case sDevicePath.StartsWith("Hmd_")
                            mNewDevice.iType = ENUM_DEVICE_TYPE.HMD

                        Case Else
                            Exit While
                    End Select

                    If (mDevoceProp.ContainsKey("ID")) Then
                        mNewDevice.iId = CInt(mDevoceProp("ID"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("VmtID")) Then
                        mNewDevice.iVmtId = CInt(mDevoceProp("VmtID"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("VmtTrackerRole")) Then
                        mNewDevice.iVmtTrackerRole = CType(CInt(mDevoceProp("VmtTrackerRole")), ENUM_TRACKER_ROLE)
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("FpsOscCounter")) Then
                        mNewDevice.iFpsOscCounter = CInt(mDevoceProp("FpsOscCounter"))
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
