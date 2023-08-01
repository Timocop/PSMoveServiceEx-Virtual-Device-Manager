Imports System.Web.Script.Serialization

Public Class ClassSteamVRConfig
    Const STEAM_INSTALL_PATH_REGISTRY As String = "SOFTWARE\WOW6432Node\Valve\Steam"

    Private g_ClassOverrides As ClassOverrides
    Private g_ClassTrackerRoles As ClassTrackerRoles
    Private g_ClassSettings As ClassSettings

    Private g_bConfigLoaded As Boolean = False
    Private g_mConfig As New Dictionary(Of String, Object)

    Public Sub New()
        g_ClassOverrides = New ClassOverrides(Me)
        g_ClassTrackerRoles = New ClassTrackerRoles(Me)
        g_ClassSettings = New ClassSettings(Me)
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

            g_sOverrideTypeNamesReadable(ENUM_OVERRIDE_TYPE.HEAD) = "Head Mount Device"
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
                If (Not mScansDic.ContainsKey("enabled")) Then
                    Return False
                End If

                Return CBool(mScansDic("enabled"))
            End Get
            Set(value As Boolean)
                If (Not g_ClassSteamVRConfig.g_mConfig.ContainsKey("driver_null")) Then
                    g_ClassSteamVRConfig.g_mConfig("driver_null") = New Dictionary(Of String, Object)
                End If

                Dim mScansDic = TryCast(g_ClassSteamVRConfig.g_mConfig("driver_null"), Dictionary(Of String, Object))
                If (Not mScansDic.ContainsKey("enabled")) Then
                    mScansDic("enabled") = False
                End If

                mScansDic("enabled") = value
            End Set
        End Property
    End Class

    Public Function LoadConfig() As Boolean
        Dim sSteamPath As String = m_SteamPath
        If (sSteamPath Is Nothing) Then
            Return False
        End If

        Dim sConfigPath As String = IO.Path.Combine(sSteamPath, "config\steamvr.vrsettings")
        If (Not IO.File.Exists(sConfigPath)) Then
            Return False
        End If

        Dim sContent As String = IO.File.ReadAllText(sConfigPath)

        Dim mTmp As Object = Nothing
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

        Dim sContent = ClassUtils.FormatOutput((New JavaScriptSerializer).Serialize(g_mConfig))

        IO.File.WriteAllText(sConfigPath, sContent)
    End Sub

End Class
