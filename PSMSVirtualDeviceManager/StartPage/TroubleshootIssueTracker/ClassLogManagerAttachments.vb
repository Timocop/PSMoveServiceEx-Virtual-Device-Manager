Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerAttachments
    Implements ILogAction

    Structure STRUC_DEVICE_ITEM
        Dim iId As Integer
        Dim iParentId As Integer

        Dim sNickName As String
        Dim bHasError As Boolean
        Dim iFpsCounter As Integer
    End Structure

    Private g_mFormMain As FormMain

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain
    End Sub

    Public Sub Generate(mData As Dictionary(Of String, String)) Implements ILogAction.Generate
        If (g_mFormMain.g_mUCVirtualControllers Is Nothing OrElse g_mFormMain.g_mUCVirtualControllers.g_mUCControllerAttachments Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        ' Not thread-safe
        ClassUtils.SyncInvoke(g_mFormMain, Sub()
                                               Dim mAttachments = g_mFormMain.g_mUCVirtualControllers.g_mUCControllerAttachments.GetAttachments()
                                               For Each mItem In mAttachments
                                                   sTrackersList.AppendFormat("[Controller_{0}]", mItem.g_mClassIO.m_Index).AppendLine()
                                                   sTrackersList.AppendFormat("ID={0}", mItem.g_mClassIO.m_Index).AppendLine()
                                                   sTrackersList.AppendFormat("NickName={0}", mItem.m_Nickname).AppendLine()
                                                   sTrackersList.AppendFormat("HasStatusError={0}", mItem.m_HasStatusError).AppendLine()
                                                   sTrackersList.AppendFormat("ParentControllerID={0}", mItem.g_mClassIO.m_ParentController).AppendLine()
                                                   sTrackersList.AppendFormat("FpsPipeCounter={0}", mItem.g_mClassIO.m_FpsPipeCounter).AppendLine()
                                                   sTrackersList.AppendFormat("ControllerOffset={0}", mItem.g_mClassIO.m_ControllerOffset.ToString).AppendLine()
                                                   sTrackersList.AppendFormat("ControllerYawCorrection={0}", mItem.g_mClassIO.m_ControllerYawCorrection).AppendLine()
                                                   sTrackersList.AppendFormat("JointOffset={0}", mItem.g_mClassIO.m_JointOffset.ToString).AppendLine()
                                                   sTrackersList.AppendFormat("JointYawCorrection={0}", mItem.g_mClassIO.m_JointYawCorrection).AppendLine()
                                                   sTrackersList.AppendFormat("OnlyJointOffset={0}", mItem.g_mClassIO.m_OnlyJointOffset).AppendLine()

                                                   sTrackersList.AppendLine()
                                               Next
                                           End Sub)

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_CONTROLLER_ATTACHMENTS
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
                Dim sDeviceKey As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewDevice As New STRUC_DEVICE_ITEM

                ' Optional
                If (mDevoceProp.ContainsKey("NickName")) Then
                    mNewDevice.sNickName = mDevoceProp("NickName")
                End If

                If (mDevoceProp.ContainsKey("HasStatusError")) Then
                    mNewDevice.bHasError = (mDevoceProp("HasStatusError").ToLowerInvariant = "true")
                End If

                If (mDevoceProp.ContainsKey("FpsPipeCounter")) Then
                    mNewDevice.iFpsCounter = CInt(mDevoceProp("FpsPipeCounter"))
                End If

                ' Required
                If (mDevoceProp.ContainsKey("ID") AndAlso mDevoceProp.ContainsKey("ParentControllerID")) Then
                    mNewDevice.iId = CInt(mDevoceProp("ID"))
                    mNewDevice.iParentId = CInt(mDevoceProp("ParentControllerID"))

                    mDeviceList.Add(mNewDevice)
                End If

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
