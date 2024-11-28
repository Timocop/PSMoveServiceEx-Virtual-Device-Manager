Imports PSMSVirtualDeviceManager.ClassLogDiagnostics

Public Class ClassLogManagerSteamVrOverrides
    Implements ILogAction

    Public Shared ReadOnly SECTION_STEAMVR_OVERRIDES As String = "SteamVR Overrides"

    Public Shared ReadOnly LOG_ISSUE_BAD_OVERRIDES As String = "Bad SteamVR tracker overrides"

    Enum ENUM_OVERRIDE_TYPE
        HEAD
        LEFT_CONTROLLER
        RIGHT_CONTROLLER
        CUSTOM
    End Enum

    Structure STRUC_OVERRIDE_ITEM
        Dim iVmtId As Integer
        Dim sDevice As String

        Dim iType As ENUM_OVERRIDE_TYPE
    End Structure

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
        Dim sTrackersList As New Text.StringBuilder

        Dim mConfig As New ClassSteamVRConfig
        If (mConfig.LoadConfig()) Then
            For Each sOverrides In mConfig.m_ClassOverrides.GetOverrides
                sTrackersList.AppendFormat("[{0}]", sOverrides.Key).AppendLine()
                sTrackersList.AppendFormat("Override={0}", sOverrides.Value).AppendLine()
            Next
        End If

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_STEAMVR_OVERRIDES
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckBadOverrides())
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
    End Function

    Public Function CheckBadOverrides() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_BAD_OVERRIDES,
            "Virtual motion tracker id {0} ({1}) collides with the SteamVR tracker overrides ({2}). This can cause tracking issues or tracking not working at all.",
            "Remove the SteamVR override that is associated with the virtual motion tracker id.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        For Each mDevice In GetOverrides()
            Dim bExist As Boolean = False
            Dim mVmtLog As New ClassLogManagerVmtTrackers(g_mFormMain, g_ClassLogContent)

            Select Case (mDevice.iType)
                Case ENUM_OVERRIDE_TYPE.LEFT_CONTROLLER, ENUM_OVERRIDE_TYPE.RIGHT_CONTROLLER
                    ' Check if its a service override
                    If (mDevice.iVmtId < 0) Then
                        Continue For
                    End If

                    For Each mVmtDevice In mVmtLog.GetDevices()
                        If (mVmtDevice.iType <> ClassLogManageServiceDevices.ENUM_DEVICE_TYPE.CONTROLLER) Then
                            Continue For
                        End If

                        If (mDevice.iVmtId <> mVmtDevice.iVmtId) Then
                            Continue For
                        End If

                        bExist = True
                        Exit For
                    Next

                    If (bExist) Then
                        Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                        mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iVmtId, ClassVmtConst.VMT_DEVICE_NAME & mDevice.iVmtId, mDevice.iType.ToString)
                        mIssues.Add(mIssue)
                    End If

                Case ENUM_OVERRIDE_TYPE.HEAD
                    For Each mVmtDevice In mVmtLog.GetDevices()
                        If (mVmtDevice.iType <> ClassLogManageServiceDevices.ENUM_DEVICE_TYPE.HMD) Then
                            Continue For
                        End If

                        bExist = True
                        Exit For
                    Next

                    If (bExist) Then
                        Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
                        mIssue.sDescription = String.Format(mIssue.sDescription, mDevice.iVmtId, ClassVmtConst.VMT_DEVICE_NAME & mDevice.iVmtId, mDevice.iType.ToString)
                        mIssues.Add(mIssue)
                    End If
            End Select
        Next

        Return mIssues.ToArray
    End Function

    Public Function GetOverrides() As STRUC_OVERRIDE_ITEM()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mDeviceList As New List(Of STRUC_OVERRIDE_ITEM)
        Dim mDevoceProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("[") AndAlso sLine.EndsWith("]"c)) Then
                Dim sDeviceKey As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewDevice As New STRUC_OVERRIDE_ITEM

                ' Required
                While True
                    mNewDevice.sDevice = sDeviceKey

                    If (sDeviceKey.ToUpperInvariant.StartsWith(ClassVmtConst.VMT_DEVICE_SERIAL.ToUpperInvariant)) Then
                        mNewDevice.iVmtId = CInt(sDeviceKey.Remove(0, ClassVmtConst.VMT_DEVICE_SERIAL.Length))
                    Else
                        mNewDevice.iVmtId = -1
                    End If

                    If (mDevoceProp.ContainsKey("Override")) Then
                        Select Case (mDevoceProp("Override"))
                            Case "/user/head"
                                mNewDevice.iType = ENUM_OVERRIDE_TYPE.HEAD
                            Case "/user/hand/left"
                                mNewDevice.iType = ENUM_OVERRIDE_TYPE.LEFT_CONTROLLER
                            Case "/user/hand/right"
                                mNewDevice.iType = ENUM_OVERRIDE_TYPE.RIGHT_CONTROLLER
                            Case Else
                                mNewDevice.iType = ENUM_OVERRIDE_TYPE.CUSTOM
                        End Select
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
