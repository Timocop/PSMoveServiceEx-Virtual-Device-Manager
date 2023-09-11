Imports System.Resources

Public Class ClassServiceInfo
    Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "settings.ini")
    Private g_sFileName As String = ""
    Private g_bConfigsLoaded As Boolean = False
    Private g_iLastSelectedLanguage As Integer = 0
    Property m_FileName As String
        Get
            Return g_sFileName
        End Get
        Set(value As String)
            g_sFileName = value
        End Set
    End Property

    Enum ENUM_SERVICE_PROCESS_TYPE
        NONE
        NORMAL
        ADMIN
    End Enum


    Public Sub ChangeLanguage()
        Dim rm As ResourceManager = New ResourceManager(GetType(FormMain))
        Dim mUCStartPage As UCStartPage
        Select Case FormMain.ComboBox1.SelectedIndex
            Case 1
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("pl-PL")
                LastSelectedLanguage = 1
                SaveConfig()
            Case 2
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("es")
                LastSelectedLanguage = 2
                SaveConfig()
            Case 3
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("de")
                LastSelectedLanguage = 3
                SaveConfig()
            Case Else ' default
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US")
                LastSelectedLanguage = 0
                SaveConfig()
        End Select

        ' Navigation bar
        FormMain.Label2.Text = rm.GetString("Navigation")
        FormMain.LinkLabel_StartPage.Text = rm.GetString("ServiceManagement")
        FormMain.LinkLabel_RunPSMS.Text = rm.GetString("RunPSMS")
        FormMain.LinkLabel_StopPSMS.Text = rm.GetString("StopPSMS")
        FormMain.LinkLabel_RestartPSMS.Text = rm.GetString("RestartPSMS")
        FormMain.LinkLabel_RunPSMSTool.Text = rm.GetString("RunPSMS")
        FormMain.LinkLabel_Controllers.Text = rm.GetString("Controllers")
        FormMain.LinkLabel_ControllersGeneral.Text = rm.GetString("ControllersGeneral")
        FormMain.LinkLabel_ControllersRemote.Text = rm.GetString("ControllersRemote")
        FormMain.LinkLabel_RemoteStartSocket.Text = rm.GetString("RemoteStartSocket")
        FormMain.LinkLabel_ControllersAttachments.Text = rm.GetString("ControllerAttachements")
        FormMain.LinkLabel_ControllersVMT.Text = rm.GetString("ControllersVMT")
        FormMain.LinkLabel_VMTStartOscServer.Text = rm.GetString("VMTStartOscServer")
        FormMain.LinkLabel1LinkLabel_VMTPauseOscServer.Text = rm.GetString("VMTPauseOscServer")
        FormMain.LinkLabel_HMDs.Text = rm.GetString("HMDs")
        FormMain.LinkLabel_Trackers.Text = rm.GetString("Trackers")
        FormMain.Label4.Text = rm.GetString("Troubleshooting")
        FormMain.LinkLabel_InstallCameraDrivers.Text = rm.GetString("InstallCameraDrivers")
        FormMain.LinkLabel_FactoryResetService.Text = rm.GetString("FactoryResetService")
        FormMain.Label1.Text = rm.GetString("Language")
        FormMain.ComboBox1.Items.Insert(0, rm.GetString("English")) ' i have to do it like this because if you do ComboBox1.Items(1) = (rm.GetString("English") its gonna detect a selectedindex
        FormMain.ComboBox1.Items.Insert(1, rm.GetString("Polish"))  ' change and make an infinite loop so now if you select an language its gonna work now but your selection will disappear.
        FormMain.ComboBox1.Items.Insert(2, rm.GetString("Spanish"))
        FormMain.ComboBox1.Items.Insert(3, rm.GetString("German"))
        For i As Integer = FormMain.ComboBox1.Items.Count - 1 To 4 Step -1 ' removing the additional languages created from the insert thing
            FormMain.ComboBox1.Items.RemoveAt(i)
        Next

        FormMain.ToolTip1.SetToolTip(FormMain.LanguageT, rm.GetString("LanguageT"))
        FormMain.LinkLabel_RunSteamVR.Text = rm.GetString("RunSteamVR")
        FormMain.LinkLabel_Github.Text = rm.GetString("GitHub")
        FormMain.LinkLabel_Updates.Text = rm.GetString("Updates")
        FormMain.Label_Version.Text = String.Format("{0}: {1}", rm.GetString("Version"), Application.ProductVersion.ToString())
        ' UCStartPage
        mUCStartPage.Label3.Text = rm.GetString("ServiceManagement")
        mUCStartPage.Label4.Text = rm.GetString("Label3")
        mUCStartPage.Label8.Text = rm.GetString("PSMSUpdateAvailable")
        mUCStartPage.Label10.Text = rm.GetString("PSMSUpdateText")
        mUCStartPage.Button_PsmsxUpdateDownload.Text = rm.GetString("DownloadNow")
        mUCStartPage.Button_PsmsUpdateIgnore.Text = rm.GetString("Ignore")
        mUCStartPage.Label9.Text = rm.GetString("VDMUpdateAvailable")
        mUCStartPage.Label11.Text = rm.GetString("VDMUpdateText")
        mUCStartPage.Button_VdmUpdateDownload.Text = rm.GetString("DownloadNow")
        mUCStartPage.Button_VdmUpdateIgnore.Text = rm.GetString("DownloadNow")
        mUCStartPage.Label1.Text = rm.GetString("ServiceControl")
        mUCStartPage.LinkLabel_ServiceRun.Text = rm.GetString("RunPSMS")
        mUCStartPage.LinkLabel_ServiceRunCmd.Text = rm.GetString("DebugService")
        mUCStartPage.LinkLabel_ServiceRestart.Text = rm.GetString("RestartPSMS")
        mUCStartPage.LinkLabel_ServiceStop.Text = rm.GetString("StopPSMS")
        mUCStartPage.LinkLabel_ServicePath.Text = rm.GetString("SetServicePath")
        mUCStartPage.Label6.Text = rm.GetString("Troubleshooting")
        mUCStartPage.LinkLabel_InstallDrivers.Text = rm.GetString("InstallCameraDrivers")
        mUCStartPage.LinkLabel_ServiceFactory.Text = rm.GetString("FactoryResetService")
        mUCStartPage.Label2.Text = rm.GetString("Configuration")
        mUCStartPage.LinkLabel_ConfigToolRun.Text = rm.GetString("RunPSMSTool")
        mUCStartPage.LinkLabel_ConfigToolRunCmd.Text = rm.GetString("RunPSMSToolCmd")
        mUCStartPage.LinkLabel_ConfigToolClose.Text = rm.GetString("ClosePSMSTool")
        mUCStartPage.Label7.Text = rm.GetString("SupportAndUpdates")
        mUCStartPage.LinkLabel_Github.Text = rm.GetString("VisitGithub")
        mUCStartPage.LinkLabel_Updates.Text = rm.GetString("Updates")
        mUCStartPage.Label12.Text = rm.GetString("AvailableServiceDevices")
        mUCStartPage.ColumnHeader_Type.Text = rm.GetString("Type")
        mUCStartPage.ColumnHeader_Color.Text = rm.GetString("Color")
        mUCStartPage.ColumnHeader_ID.Text = rm.GetString("ID")
        mUCStartPage.ColumnHeader_Serial.Text = rm.GetString("Serial")
        mUCStartPage.ColumnHeader_Pos.Text = rm.GetString("Position")
        mUCStartPage.ColumnHeader_Orientation.Text = rm.GetString("Orientation")
        mUCStartPage.ColumnHeader_Battery.Text = rm.GetString("Battery")

    End Sub



    Property LastSelectedLanguage() As Integer
        Get
            Return g_iLastSelectedLanguage
        End Get
        Set(value As Integer)
            g_iLastSelectedLanguage = value
        End Set
    End Property

    Public Function FileExist() As Boolean
        If (String.IsNullOrEmpty(m_FileName) OrElse Not IO.File.Exists(m_FileName)) Then
            Return False
        End If

        Return True
    End Function

    Public Function FindByProcess() As Boolean
        Dim pProcesses As Process() = Process.GetProcessesByName("PSMoveService")
        If (pProcesses Is Nothing OrElse pProcesses.Length < 1) Then
            Return False
        End If

        For Each mProcess In pProcesses
            m_FileName = mProcess.MainModule.FileName
            Return True
        Next

        Return False
    End Function

    Public Function SearchForService() As Boolean
        Using mFileSearch As New OpenFileDialog()
            mFileSearch.Title = "Find PSMoveServiceEx..."
            mFileSearch.Filter = "PSMoveService|PSMoveService.exe"
            mFileSearch.Multiselect = False
            mFileSearch.CheckFileExists = True

            If (mFileSearch.ShowDialog() = DialogResult.OK) Then
                m_FileName = mFileSearch.FileName
                Return True
            End If

            Return False
        End Using
    End Function

    Public Sub SaveConfig()
        If (Not g_bConfigsLoaded) Then
            Return
        End If

        Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            Using mIni As New ClassIni(mStream)
                Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("Settings", "PSMoveServiceLocation", m_FileName))
                mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("Settings", "LastSelectedLanguage", g_iLastSelectedLanguage.ToString()))

                mIni.WriteKeyValue(mIniContent.ToArray)
            End Using
        End Using
    End Sub

    Public Sub LoadConfig()
        Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
            Using mIni As New ClassIni(mStream)
                m_FileName = mIni.ReadKeyValue("Settings", "PSMoveServiceLocation", "")
                Integer.TryParse(mIni.ReadKeyValue("Settings", "LastSelectedLanguage", "0"), g_iLastSelectedLanguage)
            End Using
        End Using

        g_bConfigsLoaded = True
    End Sub
    Public Function CheckIfServiceRunning() As ENUM_SERVICE_PROCESS_TYPE

        ublic Function() CheckIfServiceRunning() As ENUM_SERVICE_PROCESS_TYPE

        If Process.GetProcessesByName("PSMoveService").Count > 0 Then
            Return ENUM_SERVICE_PROCESS_TYPE.NORMAL
        ElseIf Process.GetProcessesByName("PSMoveServiceAdmin").Count > 0 Then
            Return ENUM_SERVICE_PROCESS_TYPE.ADMIN
        Else
            Return ENUM_SERVICE_PROCESS_TYPE.NONE
        End If
    End Function



    Public Function CheckIfConfigToolRunning() As Boolean
        Return (Process.GetProcessesByName("PSMoveConfigTool").Count > 0)
    End Function
End Class
