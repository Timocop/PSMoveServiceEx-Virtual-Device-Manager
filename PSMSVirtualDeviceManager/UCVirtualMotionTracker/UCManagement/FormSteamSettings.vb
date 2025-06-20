﻿Public Class FormSteamSettings
    Private g_bSettingsLoaded As Boolean = False
    Private g_sForcedDriver As String = ""

    Const MANIFEST_PSMSX_VDM_APIKEY As String = "PSMoveServiceEx.VirtualDeviceManager"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            LoadSettings()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try

        If (g_sForcedDriver IsNot Nothing) Then
            If (g_sForcedDriver.Length > 0 AndAlso g_sForcedDriver <> "null") Then
                'Its another driver
                CheckBox_ForceNullDriver.Enabled = False
                LinkLabel_ResetForcedDrvier.Visible = True
            End If
        End If

        UcInformation1.m_ReadMoreAction = AddressOf InfoChangeSettings
    End Sub

    Private Sub InfoChangeSettings()
        CheckBox_RequireHmd.Checked = False
        CheckBox_EnableMultipleDrivers.Checked = True
    End Sub

    Private Sub FormSteamSettings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (Me.DialogResult <> DialogResult.OK) Then
            Return
        End If

        Try
            If (Process.GetProcessesByName("vrserver").Count > 0) Then
                Throw New ArgumentException("SteamVR is running! Close SteamVR and try again.")
            End If

            SaveSettings()
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLogMessageBox(ex)
        End Try
    End Sub

    Private Sub LinkLabel_ResetForcedDrvier_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_ResetForcedDrvier.LinkClicked
        CheckBox_ForceNullDriver.Enabled = True
        LinkLabel_ResetForcedDrvier.Visible = False
    End Sub

    Private Sub LoadSettings()
        Dim mSteamConfig As New ClassSteamVRConfig
        mSteamConfig.LoadConfig()

        g_sForcedDriver = mSteamConfig.m_ClassSettings.m_ForcedDriver

        CheckBox_EnableNullDriver.Checked = mSteamConfig.m_ClassSettings.m_NullHmdEnabled
        CheckBox_ForceNullDriver.Checked = (mSteamConfig.m_ClassSettings.m_ForcedDriver = "null")
        CheckBox_RequireHmd.Checked = mSteamConfig.m_ClassSettings.m_RequireHmd
        CheckBox_EnableMultipleDrivers.Checked = mSteamConfig.m_ClassSettings.m_ActivateMultipleDrivers
        CheckBox_EnableHome.Checked = mSteamConfig.m_ClassSettings.m_EnableHomeApp
        CheckBox_EnableMirror.Checked = mSteamConfig.m_ClassSettings.m_EnableMirrorView
        CheckBox_EnablePerfGraph.Checked = mSteamConfig.m_ClassSettings.m_EnablePerformanceGraph

        CheckBox_Autostart.Checked = False
        CheckBox_AutostartService.Checked = False

        ' Manage SteamVR manifests
        mSteamConfig.m_ClassManifests.LoadConfig()
        For Each sFile In mSteamConfig.m_ClassManifests.GetManifests()
            If (Not IO.File.Exists(sFile)) Then
                Continue For
            End If

            Dim mManifest As New ClassSteamVRConfig.ClassManifests.STRUC_BUILDIN_MANIFEST_CONTENT(sFile)
            If (Not mManifest.m_IsValid) Then
                Continue For
            End If

            If (Not mManifest.m_IsCurrentApplicationManifest()) Then
                Continue For
            End If

            CheckBox_Autostart.Checked = True

            Dim sArguments As String = mManifest.m_Arguments
            If (sArguments IsNot Nothing) Then
                For Each sArg As String In sArguments.Split(" "c)
                    Select Case (sArg)
                        Case FormMain.COMMANDLINE_START_SERVICE
                            CheckBox_AutostartService.Checked = True

                        Case FormMain.COMMANDLINE_START_REMOTEDEVICES
                            CheckBox_AutostartRemoteDevices.Checked = True

                        Case FormMain.COMMANDLINE_START_OSCSERVER
                            CheckBox_AutostartOscServer.Checked = True
                    End Select
                Next
            End If
        Next

        g_bSettingsLoaded = True
    End Sub

    Private Sub SaveSettings()
        If (Not g_bSettingsLoaded) Then
            Return
        End If

        Dim mSteamConfig As New ClassSteamVRConfig
        mSteamConfig.LoadConfig()

        mSteamConfig.m_ClassSettings.m_NullHmdEnabled = CheckBox_EnableNullDriver.Checked

        If (CheckBox_ForceNullDriver.Enabled) Then
            mSteamConfig.m_ClassSettings.m_ForcedDriver = If(CheckBox_ForceNullDriver.Checked, "null", "")
        End If

        mSteamConfig.m_ClassSettings.m_RequireHmd = CheckBox_RequireHmd.Checked
        mSteamConfig.m_ClassSettings.m_ActivateMultipleDrivers = CheckBox_EnableMultipleDrivers.Checked
        mSteamConfig.m_ClassSettings.m_EnableHomeApp = CheckBox_EnableHome.Checked
        mSteamConfig.m_ClassSettings.m_EnableMirrorView = CheckBox_EnableMirror.Checked
        mSteamConfig.m_ClassSettings.m_EnablePerformanceGraph = CheckBox_EnablePerfGraph.Checked

        mSteamConfig.SaveConfig()


        mSteamConfig.m_ClassManifests.LoadConfig()

        ' Add or remove current manifest for autostart
        If (CheckBox_Autostart.Checked) Then
            ' Remove multiple VDM autostart manifests
            If (True) Then
                For Each sFile In mSteamConfig.m_ClassManifests.GetManifests()
                    If (Not IO.File.Exists(sFile)) Then
                        Continue For
                    End If

                    Dim mManifest As New ClassSteamVRConfig.ClassManifests.STRUC_BUILDIN_MANIFEST_CONTENT(sFile)
                    If (Not mManifest.m_IsValid) Then
                        Continue For
                    End If

                    If (mManifest.m_IsCurrentApplicationManifest()) Then
                        Continue For
                    End If

                    If (String.IsNullOrEmpty(mManifest.m_AppKey) OrElse mManifest.m_AppKey <> MANIFEST_PSMSX_VDM_APIKEY) Then
                        Continue For
                    End If

                    mSteamConfig.m_ClassManifests.RemoveManifest(mManifest.m_File)

                    With New Text.StringBuilder
                        .AppendLine("Duplicated Virtual Device Manager manifest has been unregistered:")
                        .AppendLine()
                        .AppendLine(mManifest.m_File)
                        MessageBox.Show(.ToString, "Manifest Unregistered", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End With
                Next
            End If

            ' Manage SteamVR manifests for autostart
            If (True) Then
                Dim sManifestFile As String = mSteamConfig.m_ClassManifests.GetLocalApplicationManifestPath()

                mSteamConfig.m_ClassManifests.AddManifest(sManifestFile)

                Dim sArguments As New List(Of String)

                sArguments.Add(FormMain.COMMANDLINE_START_STEAMVR)

                If (CheckBox_AutostartService.Checked) Then
                    sArguments.Add(FormMain.COMMANDLINE_START_SERVICE)
                End If

                If (CheckBox_AutostartRemoteDevices.Checked) Then
                    sArguments.Add(FormMain.COMMANDLINE_START_REMOTEDEVICES)
                End If

                If (CheckBox_AutostartOscServer.Checked) Then
                    sArguments.Add(FormMain.COMMANDLINE_START_OSCSERVER)
                End If

                Dim mManifest As New ClassSteamVRConfig.ClassManifests.STRUC_BUILDIN_MANIFEST_CONTENT With {
                        .m_AppKey = MANIFEST_PSMSX_VDM_APIKEY,
                        .m_LaunchType = ClassSteamVRConfig.ClassManifests.STRUC_BUILDIN_MANIFEST_CONTENT.ENUM_LAUNCH_TYPE.BINARY,
                        .m_BinaryPathWindows = IO.Path.GetFileName(Application.ExecutablePath),
                        .m_Arguments = String.Join(" "c, sArguments.ToArray),
                        .m_IsDashboardOverlay = True,
                        .m_NameByLang = "Virtual Device Manager",
                        .m_DescriptionByLang = "PSMoveServiceEx - Virtual Device Manager"
                    }

                mManifest.SaveToFile(sManifestFile)

                mSteamConfig.m_ClassManifests.SetAutostartManifest(mManifest, True)
            End If
        Else
            mSteamConfig.m_ClassManifests.RemoveManifest(mSteamConfig.m_ClassManifests.GetLocalApplicationManifestPath())
            End If
            mSteamConfig.m_ClassManifests.SaveConfig()
    End Sub
End Class