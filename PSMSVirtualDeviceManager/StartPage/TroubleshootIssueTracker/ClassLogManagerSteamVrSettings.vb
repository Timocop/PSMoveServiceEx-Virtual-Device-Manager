Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManagerSteamVrSettings
    Implements ILogAction

    Public Shared ReadOnly LOG_ISSUE_STEAMVR_DRIVER_LOAD_ISSUE As String = "SteamVR driver may not load properly"

    Private g_mFormMain As FormMain
    Private g_ClassLogContent As ClassLogContent

    Structure STRUC_CONFIG_ITEM
        Dim bIsValid As Boolean
        Dim sSteamPath As String

        Dim bActivateMultipleDrivers As Boolean
        Dim bEnableHomeApp As Boolean
        Dim bEnableMirrorView As Boolean
        Dim bEnablePerformanceGraph As Boolean
        Dim sForcedDriver As String
        Dim bNullHmdEnabled As Boolean
        Dim bRequireHmd As Boolean
    End Structure

    Public Sub New(_FormMain As FormMain, _ClassLogContent As ClassLogContent)
        g_mFormMain = _FormMain
        g_ClassLogContent = _ClassLogContent
    End Sub

    Public Sub Generate(bSilent As Boolean) Implements ILogAction.Generate
        Dim sTrackersList As New Text.StringBuilder

        Dim mConfig As New ClassSteamVRConfig
        mConfig.LoadConfig()

        sTrackersList.AppendFormat("[{0}]", mConfig.m_SteamPath).AppendLine()
        sTrackersList.AppendFormat("ActivateMultipleDrivers={0}", mConfig.m_ClassSettings.m_ActivateMultipleDrivers).AppendLine()
        sTrackersList.AppendFormat("EnableHomeApp={0}", mConfig.m_ClassSettings.m_EnableHomeApp).AppendLine()
        sTrackersList.AppendFormat("EnableMirrorView={0}", mConfig.m_ClassSettings.m_EnableMirrorView).AppendLine()
        sTrackersList.AppendFormat("EnablePerformanceGraph={0}", mConfig.m_ClassSettings.m_EnablePerformanceGraph).AppendLine()
        sTrackersList.AppendFormat("ForcedDriver={0}", mConfig.m_ClassSettings.m_ForcedDriver).AppendLine()
        sTrackersList.AppendFormat("NullHmdEnabled={0}", mConfig.m_ClassSettings.m_NullHmdEnabled).AppendLine()
        sTrackersList.AppendFormat("RequireHmd={0}", mConfig.m_ClassSettings.m_RequireHmd).AppendLine()

        g_ClassLogContent.m_Content(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_STEAMVR_SETTINGS
    End Function

    Public Function GetIssues() As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Dim mIssues As New List(Of STRUC_LOG_ISSUE)
        mIssues.AddRange(CheckRequiredHmd())
        mIssues.AddRange(CheckMultiDrivers())
        Return mIssues.ToArray
    End Function

    Public Function GetSectionContent() As String Implements ILogAction.GetSectionContent
        If (Not g_ClassLogContent.m_Content.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return g_ClassLogContent.m_Content(GetActionTitle())
    End Function

    Public Function CheckRequiredHmd() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_STEAMVR_DRIVER_LOAD_ISSUE,
            "SteamVR is configured to load drivers when a Head-mounted Display is present, but the virtual motion tracker SteamVR driver loads its devices after it has loaded and initialized, causing the SteamVR driver to never activate.",
            "In Virtual Device Manager go to 'Virtual Motion Tracker > Managment > SteamVR Support > Advanced Settings...' and uncheck 'Require Head-mounted Display' to properly load the SteamVR driver even if no Head-mounted Display is available.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim mSettings = GetSettings()
        If (Not mSettings.bIsValid) Then
            Return mIssues.ToArray
        End If

        Dim bHmdExist As Boolean = False
        Dim mVmtLog As New ClassLogManagerVmtTrackers(g_mFormMain, g_ClassLogContent)
        For Each mDevice In mVmtLog.GetDevices()
            If (mDevice.iType <> ClassLogManageServiceDevices.ENUM_DEVICE_TYPE.HMD) Then
                Continue For
            End If

            bHmdExist = True
            Exit For
        Next

        If (Not bHmdExist) Then
            Return mIssues.ToArray
        End If

        If (mSettings.bRequireHmd) Then
            Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
            mIssues.Add(mIssue)
        End If

        Return mIssues.ToArray
    End Function

    Public Function CheckMultiDrivers() As STRUC_LOG_ISSUE()
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mTemplate As New STRUC_LOG_ISSUE(
            LOG_ISSUE_STEAMVR_DRIVER_LOAD_ISSUE,
            "SteamVR is configured to only load one driver at the time. The virtual motion tracker SteamVR driver might not load when other third-party drivers are activated.",
            "In Virtual Device Manager go to 'Virtual Motion Tracker > Managment > SteamVR Support > Advanced Settings...' and check 'Activate multiple drivers' to properly load the SteamVR driver even if there are other drivers loaded and running.",
            ENUM_LOG_ISSUE_TYPE.ERROR
        )

        Dim mIssues As New List(Of STRUC_LOG_ISSUE)

        Dim mSettings = GetSettings()
        If (Not mSettings.bIsValid) Then
            Return mIssues.ToArray
        End If

        Dim bHmdExist As Boolean = False
        Dim mVmtLog As New ClassLogManagerVmtTrackers(g_mFormMain, g_ClassLogContent)
        If (mVmtLog.GetDevices().Length = 0) Then
            Return mIssues.ToArray
        End If

        Dim bDriverExist As Boolean = False
        Dim mDrviers As New ClassLogManagerSteamVrDrivers(g_mFormMain, g_ClassLogContent)
        For Each sDriver In mDrviers.GetDrivers()
            If (sDriver.ToLowerInvariant.EndsWith(ClassVmtConst.VMT_DRIVER_NAME.ToLowerInvariant)) Then
                Continue For
            End If

            bDriverExist = True
            Exit For
        Next

        If (Not bDriverExist) Then
            Return mIssues.ToArray
        End If

        If (Not mSettings.bActivateMultipleDrivers) Then
            Dim mIssue As New STRUC_LOG_ISSUE(mTemplate)
            mIssues.Add(mIssue)
        End If

        Return mIssues.ToArray
    End Function

    Public Function GetSettings() As STRUC_CONFIG_ITEM
        Dim sContent As String = GetSectionContent()
        If (sContent Is Nothing) Then
            Return New STRUC_CONFIG_ITEM()
        End If

        Dim mDevoceProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("[") AndAlso sLine.EndsWith("]"c)) Then
                Dim sSteamPath As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewConfig As New STRUC_CONFIG_ITEM

                ' Required
                While True
                    mNewConfig.sSteamPath = sSteamPath

                    If (mDevoceProp.ContainsKey("ActivateMultipleDrivers")) Then
                        mNewConfig.bActivateMultipleDrivers = (mDevoceProp("ActivateMultipleDrivers").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("EnableHomeApp")) Then
                        mNewConfig.bEnableHomeApp = (mDevoceProp("EnableHomeApp").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("EnableMirrorView")) Then
                        mNewConfig.bEnableMirrorView = (mDevoceProp("EnableMirrorView").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("EnablePerformanceGraph")) Then
                        mNewConfig.bEnablePerformanceGraph = (mDevoceProp("EnablePerformanceGraph").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("ForcedDriver")) Then
                        mNewConfig.sForcedDriver = mDevoceProp("ForcedDriver")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("NullHmdEnabled")) Then
                        mNewConfig.bNullHmdEnabled = (mDevoceProp("NullHmdEnabled").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("RequireHmd")) Then
                        mNewConfig.bRequireHmd = (mDevoceProp("RequireHmd").ToLowerInvariant = "true")
                    Else
                        Exit While
                    End If

                    mNewConfig.bIsValid = True

                    Return mNewConfig
                End While

                mDevoceProp.Clear()
            End If

            If (sLine.Contains("="c)) Then
                Dim sKey As String = sLine.Substring(0, sLine.IndexOf("="c))
                Dim sValue As String = sLine.Remove(0, sLine.IndexOf("="c) + 1)

                mDevoceProp(sKey) = sValue
            End If
        Next

        Return New STRUC_CONFIG_ITEM()
    End Function
End Class
