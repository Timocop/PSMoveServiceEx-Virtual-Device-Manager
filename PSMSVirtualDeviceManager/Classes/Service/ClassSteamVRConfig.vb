Imports System.Web.Script.Serialization

Public Class ClassSteamVRConfig
    Const STEAM_INSTALL_PATH_REGISTRY As String = "SOFTWARE\WOW6432Node\Valve\Steam"

    Private g_ClassOverrides As ClassOverrides
    Private g_ClassTrackerRoles As ClassTrackerRoles
    Private g_ClassSettings As ClassSettings
    Private g_ClassManifests As ClassManifests

    Private g_bConfigLoaded As Boolean = False
    Private g_mConfig As New Dictionary(Of String, Object)

    Public Sub New()
        g_ClassOverrides = New ClassOverrides(Me)
        g_ClassTrackerRoles = New ClassTrackerRoles(Me)
        g_ClassSettings = New ClassSettings(Me)
        g_ClassManifests = New ClassManifests(Me)
    End Sub

    ReadOnly Property m_SteamPath As String
        Get
            Dim mSubKey = My.Computer.Registry.LocalMachine.OpenSubKey(STEAM_INSTALL_PATH_REGISTRY, False)
            If (mSubKey Is Nothing) Then
                Return Nothing
            End If

            Return CStr(mSubKey.GetValue("InstallPath", Nothing))
        End Get
    End Property

    ReadOnly Property m_ClassOverrides As ClassOverrides
        Get
            Return g_ClassOverrides
        End Get
    End Property

    ReadOnly Property m_ClassTrackerRoles As ClassTrackerRoles
        Get
            Return g_ClassTrackerRoles
        End Get
    End Property

    ReadOnly Property m_ClassSettings As ClassSettings
        Get
            Return g_ClassSettings
        End Get
    End Property

    ReadOnly Property m_ClassManifests As ClassManifests
        Get
            Return g_ClassManifests
        End Get
    End Property

    Class ClassOverrides
        Private g_ClassSteamVRConfig As ClassSteamVRConfig

        Enum ENUM_OVERRIDE_TYPE
            INVALID = -1
            HEAD
            LEFT_HAND
            RIGHT_HAND

            __MAX
        End Enum
        Private g_sOverrideTypeNames(ENUM_OVERRIDE_TYPE.__MAX) As String
        Private g_sOverrideTypeNamesReadable(ENUM_OVERRIDE_TYPE.__MAX) As String

        Public Sub New(_ClassSteamVRConfig As ClassSteamVRConfig)
            g_ClassSteamVRConfig = _ClassSteamVRConfig

            g_sOverrideTypeNames(ENUM_OVERRIDE_TYPE.HEAD) = "/user/head"
            g_sOverrideTypeNames(ENUM_OVERRIDE_TYPE.LEFT_HAND) = "/user/hand/left"
            g_sOverrideTypeNames(ENUM_OVERRIDE_TYPE.RIGHT_HAND) = "/user/hand/right"

            g_sOverrideTypeNamesReadable(ENUM_OVERRIDE_TYPE.HEAD) = "Head-Mounted Device"
            g_sOverrideTypeNamesReadable(ENUM_OVERRIDE_TYPE.LEFT_HAND) = "Left Controller"
            g_sOverrideTypeNamesReadable(ENUM_OVERRIDE_TYPE.RIGHT_HAND) = "Right Controller"
        End Sub

        Public Function GetOverrideTypeReadableName(iType As ENUM_OVERRIDE_TYPE) As String
            If (iType = ENUM_OVERRIDE_TYPE.INVALID) Then
                Return "Invalid"
            End If

            Return g_sOverrideTypeNamesReadable(iType)
        End Function

        Public Function GetOverrideTypeName(iType As ENUM_OVERRIDE_TYPE) As String
            If (iType = ENUM_OVERRIDE_TYPE.INVALID) Then
                Return Nothing
            End If

            Return g_sOverrideTypeNames(iType)
        End Function

        Public Function GetOverrideTypeFromName(sName As String) As ENUM_OVERRIDE_TYPE
            For i = 0 To ENUM_OVERRIDE_TYPE.__MAX - 1
                If (sName.StartsWith(g_sOverrideTypeNames(i))) Then
                    Return CType(i, ENUM_OVERRIDE_TYPE)
                End If
            Next

            Return ENUM_OVERRIDE_TYPE.INVALID
        End Function

        Public Sub SetOverride(sTrackerName As String, sTrackerToOverride As String)
            If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("TrackingOverrides")) Then
                g_ClassSteamVRConfig.g_mConfig("TrackingOverrides") = New Dictionary(Of String, Object)
            End If

            Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("TrackingOverrides"), Dictionary(Of String, Object))

            mScansDic(sTrackerName) = sTrackerToOverride
        End Sub

        Public Sub RemoveOverride(sTrackerName As String)
            If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("TrackingOverrides")) Then
                g_ClassSteamVRConfig.g_mConfig("TrackingOverrides") = New Dictionary(Of String, Object)
            End If

            Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("TrackingOverrides"), Dictionary(Of String, Object))
            If (Not mScansDic.ContainsKey(sTrackerName)) Then
                Return
            End If

            mScansDic.Remove(sTrackerName)
        End Sub

        Public Function GetOverride(sTrackerName As String) As String
            Dim mOverrides = GetOverrides()

            For Each mItem In mOverrides
                If (mItem.Key = sTrackerName) Then
                    Return mItem.Value
                End If
            Next

            Return Nothing
        End Function

        Public Function GetOverrides() As KeyValuePair(Of String, String)()
            Dim mOverides As New List(Of KeyValuePair(Of String, String))

            If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("TrackingOverrides")) Then
                Return mOverides.ToArray
            End If

            Dim mOverrideDic = TryCast(g_ClassSteamVRConfig.g_mConfig("TrackingOverrides"), Dictionary(Of String, Object))
            If (mOverrideDic Is Nothing) Then
                Return mOverides.ToArray
            End If

            For Each mItem In mOverrideDic
                mOverides.Add(New KeyValuePair(Of String, String)(mItem.Key, CStr(mItem.Value)))
            Next

            Return mOverides.ToArray
        End Function

    End Class

    Class ClassTrackerRoles
        Private g_ClassSteamVRConfig As ClassSteamVRConfig

        Enum ENUM_TRACKER_ROLE_TYPE
            INVALID = -1
            HANDED_L
            HANDED_R
            HANDED_BOTH
            LEFT_FOOT
            RIGHT_FOOT
            LEFT_SHOULDER
            RIGHT_SHOULDER
            LEFT_ELBOW
            RIGHT_ELBOW
            LEFT_KNEE
            RIGHT_KNEE
            WAIST
            CHEST
            CAMERA
            KEYBOARD

            __MAX
        End Enum
        Private g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.__MAX) As String
        Private g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.__MAX) As String

        Public Sub New(_ClassSteamVRConfig As ClassSteamVRConfig)
            g_ClassSteamVRConfig = _ClassSteamVRConfig

            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.HANDED_L) = "TrackerRole_Handed,TrackedControllerRole_LeftHand"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.HANDED_R) = "TrackerRole_Handed,TrackedControllerRole_RightHand"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.HANDED_BOTH) = "TrackerRole_Handed"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.LEFT_FOOT) = "TrackerRole_LeftFoot"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.RIGHT_FOOT) = "TrackerRole_RightFoot"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.LEFT_SHOULDER) = "TrackerRole_LeftShoulder"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.RIGHT_SHOULDER) = "TrackerRole_RightShoulder"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.LEFT_ELBOW) = "TrackerRole_LeftElbow"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.RIGHT_ELBOW) = "TrackerRole_RightElbow"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.LEFT_KNEE) = "TrackerRole_LeftKnee"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.RIGHT_KNEE) = "TrackerRole_RightKnee"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.WAIST) = "TrackerRole_Waist"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.CHEST) = "TrackerRole_Chest"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.CAMERA) = "TrackerRole_Camera"
            g_sTrackerRoleNames(ENUM_TRACKER_ROLE_TYPE.KEYBOARD) = "TrackerRole_Keyboard"

            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.HANDED_BOTH) = "Handed"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.HANDED_L) = "Handed Left"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.HANDED_R) = "Handed Right"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.LEFT_FOOT) = "Left Foot"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.RIGHT_FOOT) = "Right Foot"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.LEFT_SHOULDER) = "Left Shoulder"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.RIGHT_SHOULDER) = "Right Shoulder"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.LEFT_ELBOW) = "Left Elbow"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.RIGHT_ELBOW) = "Right Elbow"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.LEFT_KNEE) = "Left Knee"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.RIGHT_KNEE) = "Right Knee"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.WAIST) = "Waist"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.CHEST) = "Chest"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.CAMERA) = "Camera"
            g_sTrackerRoleNamesReadable(ENUM_TRACKER_ROLE_TYPE.KEYBOARD) = "Keyboard"
        End Sub

        Public Function GetTrackerRoleReadableName(iType As ENUM_TRACKER_ROLE_TYPE) As String
            If (iType = ENUM_TRACKER_ROLE_TYPE.INVALID) Then
                Return "Invalid"
            End If

            Return g_sTrackerRoleNamesReadable(iType)
        End Function

        Public Function GetTrackerRoleName(iType As ENUM_TRACKER_ROLE_TYPE) As String
            If (iType = ENUM_TRACKER_ROLE_TYPE.INVALID) Then
                Return Nothing
            End If

            Return g_sTrackerRoleNames(iType)
        End Function

        Public Function GetTrackerRoleFromName(sName As String) As ENUM_TRACKER_ROLE_TYPE
            For i = 0 To ENUM_TRACKER_ROLE_TYPE.__MAX - 1
                If (sName = g_sTrackerRoleNames(i) OrElse sName.StartsWith(g_sTrackerRoleNames(i) & ",")) Then
                    Return CType(i, ENUM_TRACKER_ROLE_TYPE)
                End If
            Next

            Return ENUM_TRACKER_ROLE_TYPE.INVALID
        End Function

        Public Sub SetTrackerRole(sTrackerName As String, iType As ENUM_TRACKER_ROLE_TYPE)
            If (iType = ENUM_TRACKER_ROLE_TYPE.INVALID) Then
                Return
            End If

            If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("trackers")) Then
                g_ClassSteamVRConfig.g_mConfig("trackers") = New Dictionary(Of String, Object)
            End If

            Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("trackers"), Dictionary(Of String, Object))

            If (iType = ENUM_TRACKER_ROLE_TYPE.HANDED_BOTH) Then
                mScansDic(sTrackerName) = g_sTrackerRoleNames(iType) & ",TrackedControllerRole_Invalid"
            Else
                mScansDic(sTrackerName) = g_sTrackerRoleNames(iType)
            End If
        End Sub

        Public Sub RemoveTrackerRole(sTrackerName As String)
            If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("trackers")) Then
                g_ClassSteamVRConfig.g_mConfig("trackers") = New Dictionary(Of String, Object)
            End If

            Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("trackers"), Dictionary(Of String, Object))
            If (Not mScansDic.ContainsKey(sTrackerName)) Then
                Return
            End If

            mScansDic.Remove(sTrackerName)
        End Sub

        Public Function GetTrackerRole(sTrackerName As String) As String
            Dim mOverrides = GetKnownTrackersWithRoles()

            For Each mItem In mOverrides
                If (mItem.Key = sTrackerName) Then
                    Return mItem.Value
                End If
            Next

            Return Nothing
        End Function

        Public Function GetKnownTrackers() As String()
            Dim mOverides As New List(Of String)

            If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("trackers")) Then
                Return mOverides.ToArray
            End If

            Dim mOverrideDic = TryCast(g_ClassSteamVRConfig.g_mConfig("trackers"), Dictionary(Of String, Object))
            If (mOverrideDic Is Nothing) Then
                Return mOverides.ToArray
            End If

            For Each mItem In mOverrideDic
                mOverides.Add(mItem.Key)
            Next

            Return mOverides.ToArray
        End Function

        Public Function GetKnownTrackersWithRoles() As KeyValuePair(Of String, String)()
            Dim mOverides As New List(Of KeyValuePair(Of String, String))

            If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("trackers")) Then
                Return mOverides.ToArray
            End If

            Dim mOverrideDic = TryCast(g_ClassSteamVRConfig.g_mConfig("trackers"), Dictionary(Of String, Object))
            If (mOverrideDic Is Nothing) Then
                Return mOverides.ToArray
            End If

            For Each mItem In mOverrideDic
                mOverides.Add(New KeyValuePair(Of String, String)(mItem.Key, CStr(mItem.Value)))
            Next

            Return mOverides.ToArray
        End Function
    End Class

    Class ClassSettings
        ''' <summary>
        ''' Default Settings
        ''' C:\Program Files (x86)\Steam\steamapps\common\SteamVR\resources\settings\default.vrsettings
        ''' </summary>

        Private g_ClassSteamVRConfig As ClassSteamVRConfig


        Public Sub New(_ClassSteamVRConfig As ClassSteamVRConfig)
            g_ClassSteamVRConfig = _ClassSteamVRConfig
        End Sub

        Property m_NullHmdEnabled As Boolean
            Get
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("driver_null")) Then
                    Return False
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("driver_null"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("enable")) Then
                    Return False
                End If

                Return CBool(mScansDic("enable"))
            End Get
            Set(value As Boolean)
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("driver_null")) Then
                    g_ClassSteamVRConfig.g_mConfig("driver_null") = New Dictionary(Of String, Object)
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("driver_null"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("enable")) Then
                    mScansDic("enable") = False
                End If

                mScansDic("enable") = value
            End Set
        End Property

        Property m_ActivateMultipleDrivers As Boolean
            Get
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    Return False
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("activateMultipleDrivers")) Then
                    Return False
                End If

                Return CBool(mScansDic("activateMultipleDrivers"))
            End Get
            Set(value As Boolean)
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    g_ClassSteamVRConfig.g_mConfig("steamvr") = New Dictionary(Of String, Object)
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("activateMultipleDrivers")) Then
                    mScansDic("activateMultipleDrivers") = False
                End If

                mScansDic("activateMultipleDrivers") = value
            End Set
        End Property

        Property m_ForcedDriver As String
            Get
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    Return ""
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("forcedDriver")) Then
                    Return ""
                End If

                Return CStr(mScansDic("forcedDriver"))
            End Get
            Set(value As String)
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    g_ClassSteamVRConfig.g_mConfig("steamvr") = New Dictionary(Of String, Object)
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("forcedDriver")) Then
                    mScansDic("forcedDriver") = ""
                End If

                mScansDic("forcedDriver") = value
            End Set
        End Property

        Property m_EnableHomeApp As Boolean
            Get
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    Return True
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("enableHomeApp")) Then
                    Return True
                End If

                Return CBool(mScansDic("enableHomeApp"))
            End Get
            Set(value As Boolean)
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    g_ClassSteamVRConfig.g_mConfig("steamvr") = New Dictionary(Of String, Object)
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("enableHomeApp")) Then
                    mScansDic("enableHomeApp") = True
                End If

                mScansDic("enableHomeApp") = value
            End Set
        End Property

        Property m_EnableMirrorView As Boolean
            Get
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    Return False
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("showMirrorView")) Then
                    Return False
                End If

                Return CBool(mScansDic("showMirrorView"))
            End Get
            Set(value As Boolean)
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    g_ClassSteamVRConfig.g_mConfig("steamvr") = New Dictionary(Of String, Object)
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("showMirrorView")) Then
                    mScansDic("showMirrorView") = False
                End If

                mScansDic("showMirrorView") = value
            End Set
        End Property

        Property m_EnablePerformanceGraph As Boolean
            Get
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    Return False
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("showPerfGraph")) Then
                    Return False
                End If

                Return CBool(mScansDic("showPerfGraph"))
            End Get
            Set(value As Boolean)
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    g_ClassSteamVRConfig.g_mConfig("steamvr") = New Dictionary(Of String, Object)
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("showPerfGraph")) Then
                    mScansDic("showPerfGraph") = False
                End If

                mScansDic("showPerfGraph") = value
            End Set
        End Property

        Property m_RequireHmd As Boolean
            Get
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    Return True
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("requireHmd")) Then
                    Return True
                End If

                Return CBool(mScansDic("requireHmd"))
            End Get
            Set(value As Boolean)
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("steamvr")) Then
                    g_ClassSteamVRConfig.g_mConfig("steamvr") = New Dictionary(Of String, Object)
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("steamvr"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("requireHmd")) Then
                    mScansDic("requireHmd") = True
                End If

                mScansDic("requireHmd") = value
            End Set
        End Property

    End Class

    Class ClassManifests
        Const MANIFEST_FILE_NAME As String = "manifest.vrmanifest"

        Private g_ClassSteamVRConfig As ClassSteamVRConfig

        Private g_bConfigLoaded As Boolean = False
        Private g_mConfig As New Dictionary(Of String, Object)

        Class STRUC_BUILDIN_MANIFEST_CONTENT
            Private g_mManifest As New Dictionary(Of String, Object)

            Private g_sManifestPath As String = ""

            Enum ENUM_LAUNCH_TYPE
                UNKNOWN = -1
                BINARY
                URL
            End Enum

            Public Sub New()
                Clear()
            End Sub

            Public Sub New(sFile As String)
                Clear()
                ParseFromFile(sFile)
            End Sub

            ReadOnly Property m_RawManifest As Dictionary(Of String, Object)
                Get
                    Return g_mManifest
                End Get
            End Property

            ReadOnly Property m_IsValid As Boolean
                Get
                    If (Not g_mManifest.ContainsKey("source")) Then
                        Return False
                    End If

                    Return (CStr(g_mManifest("source")) = "builtin")
                End Get
            End Property

            Public Sub Clear()
                g_mManifest = New Dictionary(Of String, Object)
                g_mManifest("source") = "builtin"

                g_sManifestPath = ""
            End Sub

            ReadOnly Property m_IsCurrentApplicationManifest As Boolean
                Get
                    ' Needs to be build-in manifest
                    If (Not m_IsValid()) Then
                        Return False
                    End If

                    If (String.IsNullOrEmpty(g_sManifestPath)) Then
                        Return False
                    End If

                    Dim sApplicationPath As String = IO.Path.Combine(Application.StartupPath, MANIFEST_FILE_NAME)

                    Return (g_sManifestPath.ToLowerInvariant = sApplicationPath.ToLowerInvariant)
                End Get
            End Property

            ReadOnly Property m_File As String
                Get
                    Return g_sManifestPath
                End Get
            End Property

            Property m_AppKey As String
                Get
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (Not mApplicationsFirst.ContainsKey("app_key")) Then
                        Return Nothing
                    End If

                    Return CStr(mApplicationsFirst("app_key"))
                End Get
                Set(value As String)
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (value Is Nothing) Then
                        mApplicationsFirst.Remove("app_key")
                    Else
                        mApplicationsFirst("app_key") = value
                    End If
                End Set
            End Property

            Property m_LaunchType As ENUM_LAUNCH_TYPE
                Get
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (Not mApplicationsFirst.ContainsKey("launch_type")) Then
                        Return Nothing
                    End If

                    Select Case (CStr(mApplicationsFirst("launch_type")))
                        Case "binary"
                            Return ENUM_LAUNCH_TYPE.BINARY

                        Case "url"
                            Return ENUM_LAUNCH_TYPE.URL

                        Case Else
                            Return ENUM_LAUNCH_TYPE.UNKNOWN
                    End Select
                End Get
                Set(value As ENUM_LAUNCH_TYPE)
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    Select Case (value)
                        Case ENUM_LAUNCH_TYPE.BINARY
                            mApplicationsFirst("launch_type") = "binary"

                        Case ENUM_LAUNCH_TYPE.URL
                            mApplicationsFirst("launch_type") = "url"

                        Case Else
                            mApplicationsFirst.Remove("launch_type")
                    End Select
                End Set
            End Property

            Property m_BinaryPathWindows As String
                Get
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (Not mApplicationsFirst.ContainsKey("binary_path_windows")) Then
                        Return Nothing
                    End If

                    Return CStr(mApplicationsFirst("binary_path_windows"))
                End Get
                Set(value As String)
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (value Is Nothing) Then
                        mApplicationsFirst.Remove("binary_path_windows")
                    Else
                        mApplicationsFirst("binary_path_windows") = value
                    End If
                End Set
            End Property

            Property m_WorkingDirectory As String
                Get
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (Not mApplicationsFirst.ContainsKey("working_directory")) Then
                        Return Nothing
                    End If

                    Return CStr(mApplicationsFirst("working_directory"))
                End Get
                Set(value As String)
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (value Is Nothing) Then
                        mApplicationsFirst.Remove("working_directory")
                    Else
                        mApplicationsFirst("working_directory") = value
                    End If
                End Set
            End Property

            Property m_Arguments As String
                Get
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (Not mApplicationsFirst.ContainsKey("arguments")) Then
                        Return Nothing
                    End If

                    Return CStr(mApplicationsFirst("arguments"))
                End Get
                Set(value As String)
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (value Is Nothing) Then
                        mApplicationsFirst.Remove("arguments")
                    Else
                        mApplicationsFirst("arguments") = value
                    End If
                End Set
            End Property

            Property m_IsDashboardOverlay As Boolean
                Get
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (Not mApplicationsFirst.ContainsKey("is_dashboard_overlay")) Then
                        Return Nothing
                    End If

                    Return CBool(mApplicationsFirst("is_dashboard_overlay"))
                End Get
                Set(value As Boolean)
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    mApplicationsFirst("is_dashboard_overlay") = value
                End Set
            End Property

            Property m_NameByLang(Optional sLanguage As String = "en_us") As String
                Get
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (Not mApplicationsFirst.ContainsKey("strings")) Then
                        Return Nothing
                    End If

                    Dim mStrings = TryCast(mApplicationsFirst("strings"), Dictionary(Of String, Object))
                    If (Not mStrings.ContainsKey(sLanguage)) Then
                        Return Nothing
                    End If

                    Dim mLanguage = TryCast(mStrings(sLanguage), Dictionary(Of String, Object))
                    If (Not mLanguage.ContainsKey("name")) Then
                        Return Nothing
                    End If

                    Return CStr(mLanguage("name"))
                End Get
                Set(value As String)
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (Not mApplicationsFirst.ContainsKey("strings")) Then
                        mApplicationsFirst("strings") = New Dictionary(Of String, Object)
                    End If

                    Dim mStrings = TryCast(mApplicationsFirst("strings"), Dictionary(Of String, Object))
                    If (Not mStrings.ContainsKey(sLanguage)) Then
                        mStrings(sLanguage) = New Dictionary(Of String, Object)
                    End If

                    Dim mLanguage = TryCast(mStrings(sLanguage), Dictionary(Of String, Object))

                    mLanguage("name") = value
                End Set
            End Property

            Property m_DescriptionByLang(Optional sLanguage As String = "en_us") As String
                Get
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (Not mApplicationsFirst.ContainsKey("strings")) Then
                        Return Nothing
                    End If

                    Dim mStrings = TryCast(mApplicationsFirst("strings"), Dictionary(Of String, Object))
                    If (Not mStrings.ContainsKey(sLanguage)) Then
                        Return Nothing
                    End If

                    Dim mLanguage = TryCast(mStrings(sLanguage), Dictionary(Of String, Object))
                    If (Not mLanguage.ContainsKey("description")) Then
                        Return Nothing
                    End If

                    Return CStr(mLanguage("description"))
                End Get
                Set(value As String)
                    Dim mApplicationsFirst = InternalGetFirstApplication()

                    If (Not mApplicationsFirst.ContainsKey("strings")) Then
                        mApplicationsFirst("strings") = New Dictionary(Of String, Object)
                    End If

                    Dim mStrings = TryCast(mApplicationsFirst("strings"), Dictionary(Of String, Object))
                    If (Not mStrings.ContainsKey(sLanguage)) Then
                        mStrings(sLanguage) = New Dictionary(Of String, Object)
                    End If

                    Dim mLanguage = TryCast(mStrings(sLanguage), Dictionary(Of String, Object))

                    mLanguage("description") = value
                End Set
            End Property


            Private Function InternalGetFirstApplication() As Dictionary(Of String, Object)
                If (Not m_IsValid()) Then
                    Throw New ArgumentException("Only build-in manifests are supported")
                End If

                If (Not g_mManifest.ContainsKey("applications")) Then
                    g_mManifest("applications") = New ArrayList
                End If

                Dim mApplications = TryCast(g_mManifest("applications"), ArrayList)
                If (mApplications.Count = 0) Then
                    mApplications.Add(New Dictionary(Of String, Object))
                End If

                Return TryCast(mApplications(0), Dictionary(Of String, Object))
            End Function




            Public Sub ParseFromFile(sFile As String, Optional bSafeRead As Boolean = True)
                Dim sContent As String
                If (bSafeRead) Then
                    sContent = ClassUtils.ClassSafeFileRead.ReadFile(sFile)
                Else
                    sContent = IO.File.ReadAllText(sFile)
                End If

                g_mManifest = (New JavaScriptSerializer).Deserialize(Of Dictionary(Of String, Object))(sContent)

                g_sManifestPath = sFile
            End Sub

            Public Sub SaveToFile(sFile As String)
                Dim sContent = ClassUtils.FormatJsonOutput((New JavaScriptSerializer).Serialize(g_mManifest))

                IO.File.WriteAllText(sFile, sContent)

                g_sManifestPath = sFile
            End Sub
        End Class

        Public Sub New(_ClassSteamVRConfig As ClassSteamVRConfig)
            g_ClassSteamVRConfig = _ClassSteamVRConfig
        End Sub

        ReadOnly Property m_AppConfigFile As String
            Get
                If (g_ClassSteamVRConfig.m_SteamPath Is Nothing) Then
                    Return Nothing
                End If

                Return IO.Path.Combine(g_ClassSteamVRConfig.m_SteamPath, "config\appconfig.json")
            End Get
        End Property

        ReadOnly Property m_VrAppConfigPath As String
            Get
                If (g_ClassSteamVRConfig.m_SteamPath Is Nothing) Then
                    Return Nothing
                End If

                Return IO.Path.Combine(g_ClassSteamVRConfig.m_SteamPath, "config\vrappconfig")
            End Get
        End Property

        Public Function GetLocalApplicationManifestPath() As String
            Return IO.Path.Combine(Application.StartupPath, MANIFEST_FILE_NAME)
        End Function

        Public Sub AddManifest(sPath As String)
            ValidateManifestPaths()

            Dim mManifestPaths = TryCast(g_mConfig("manifest_paths"), ArrayList)
            If (mManifestPaths Is Nothing) Then
                Throw New ArgumentException("Could not cast manifest_paths as ArrayList")
            End If

            For Each sDriverPath As String In mManifestPaths.ToArray
                If (sDriverPath Is Nothing) Then
                    Continue For
                End If

                If (sDriverPath.ToLowerInvariant = sPath.ToLowerInvariant) Then
                    Return
                End If
            Next

            mManifestPaths.Add(sPath)
        End Sub

        Public Sub RemoveManifest(sPath As String)
            ValidateManifestPaths()

            Dim mManifestPaths = TryCast(g_mConfig("manifest_paths"), ArrayList)
            If (mManifestPaths Is Nothing) Then
                Throw New ArgumentException("Could not cast manifest_paths as ArrayList")
            End If

            For i = mManifestPaths.Count - 1 To 0 Step -1
                If (mManifestPaths(i) Is Nothing) Then
                    Continue For
                End If

                If (CStr(mManifestPaths(i)).ToLowerInvariant = sPath.ToLowerInvariant) Then
                    mManifestPaths.RemoveAt(i)
                End If
            Next
        End Sub

        Public Function GetManifests() As String()
            ValidateManifestPaths()

            Dim mManifestPaths = TryCast(g_mConfig("manifest_paths"), ArrayList)
            If (mManifestPaths Is Nothing) Then
                Throw New ArgumentException("Could not cast manifest_paths as ArrayList")
            End If

            Dim mManifests As New List(Of String)
            For i = 0 To mManifestPaths.Count - 1
                If (mManifestPaths(i) Is Nothing) Then
                    Continue For
                End If

                mManifests.Add(CStr(mManifestPaths(i)))
            Next

            Return mManifests.ToArray
        End Function

        Public Sub SetAutostartManifest(mManifest As STRUC_BUILDIN_MANIFEST_CONTENT, bEnabled As Boolean)
            If (String.IsNullOrEmpty(mManifest.m_AppKey) OrElse mManifest.m_AppKey.Trim.Length = 0) Then
                Throw New ArgumentException("App key can not be empty")
            End If

            If (m_VrAppConfigPath Is Nothing) Then
                Throw New ArgumentException("App config path not found")
            End If

            Dim sAppConfig As String = IO.Path.Combine(m_VrAppConfigPath, mManifest.m_AppKey & ".vrappconfig")

            Dim mAppConfig As New Dictionary(Of String, Object)
            mAppConfig("autolaunch") = bEnabled
            mAppConfig("last_launch_time") = 0

            Dim sContent As String = ClassUtils.FormatJsonOutput((New JavaScriptSerializer).Serialize(mAppConfig))

            IO.File.WriteAllText(sAppConfig, sContent)
        End Sub

        Public Function LoadConfig(Optional bSafeRead As Boolean = True) As Boolean
            Dim sConfigPath As String = m_AppConfigFile
            If (sConfigPath Is Nothing OrElse Not IO.File.Exists(sConfigPath)) Then
                Return False
            End If

            Dim sContent As String
            If (bSafeRead) Then
                sContent = ClassUtils.ClassSafeFileRead.ReadFile(sConfigPath)
            Else
                sContent = IO.File.ReadAllText(sConfigPath)
            End If

            g_mConfig = (New JavaScriptSerializer).Deserialize(Of Dictionary(Of String, Object))(sContent)

            g_bConfigLoaded = True
            Return True
        End Function

        Public Sub SaveConfig()
            If (Not g_bConfigLoaded) Then
                Return
            End If

            Dim sConfigPath As String = m_AppConfigFile
            If (sConfigPath Is Nothing OrElse Not IO.File.Exists(sConfigPath)) Then
                Return
            End If

            ' Remove any invalid entries from the drivers list.
            RemoveInvalid()

            Dim sContent = ClassUtils.FormatJsonOutput((New JavaScriptSerializer).Serialize(g_mConfig))

            IO.File.WriteAllText(sConfigPath, sContent)
        End Sub

        Private Sub RemoveInvalid()
            ValidateManifestPaths()

            Dim mManifestPaths = TryCast(g_mConfig("manifest_paths"), ArrayList)
            If (mManifestPaths Is Nothing) Then
                Throw New ArgumentException("Could not cast manifest_paths as ArrayList")
            End If

            ' Remove NULL drivers.
            For i = mManifestPaths.Count - 1 To 0 Step -1
                If (mManifestPaths(i) Is Nothing) Then
                    mManifestPaths.RemoveAt(i)
                End If
            Next

            ' Just remove the whole section if its empty.
            If (mManifestPaths.Count = 0) Then
                g_mConfig.Remove("manifest_paths")
            End If
        End Sub

        Private Sub ValidateManifestPaths()
            If (Not g_mConfig.ContainsKey("manifest_paths")) Then
                g_mConfig("manifest_paths") = New ArrayList()
            End If

            If (TypeOf g_mConfig("manifest_paths") IsNot ArrayList) Then
                g_mConfig("manifest_paths") = New ArrayList()
            End If
        End Sub
    End Class

    Public Function LoadConfig(Optional bSafeRead As Boolean = True) As Boolean
        Dim sSteamPath As String = m_SteamPath
        If (sSteamPath Is Nothing) Then
            Return False
        End If

        Dim sConfigPath As String = IO.Path.Combine(sSteamPath, "config\steamvr.vrsettings")
        If (Not IO.File.Exists(sConfigPath)) Then
            Return False
        End If

        Dim sContent As String
        If (bSafeRead) Then
            sContent = ClassUtils.ClassSafeFileRead.ReadFile(sConfigPath)
        Else
            sContent = IO.File.ReadAllText(sConfigPath)
        End If

        g_mConfig = (New JavaScriptSerializer).Deserialize(Of Dictionary(Of String, Object))(sContent)

        g_bConfigLoaded = True
        Return True
    End Function

    Public Sub SaveConfig()
        If (Not g_bConfigLoaded) Then
            Return
        End If

        Dim sSteamPath As String = m_SteamPath
        If (sSteamPath Is Nothing) Then
            Return
        End If

        Dim sConfigPath As String = IO.Path.Combine(sSteamPath, "config\steamvr.vrsettings")
        If (Not IO.File.Exists(sConfigPath)) Then
            Return
        End If

        Dim sContent = ClassUtils.FormatJsonOutput((New JavaScriptSerializer).Serialize(g_mConfig))

        IO.File.WriteAllText(sConfigPath, sContent)
    End Sub

End Class
