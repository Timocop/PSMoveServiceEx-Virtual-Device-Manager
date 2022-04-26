Imports System.Runtime.InteropServices

Public Class FormMain
    Public g_mUCVirtualControllers As UCVirtualControllers
    Public g_mUCVirtualHMDs As UCVirtualHMDs
    Public g_mUCVirtualTrackers As UCVirtualTrackers

    Enum ENUM_PAGE
        VIRTUAL_CONTROLLERS
        VIRTUAL_HMDS
        VIRTUAL_TRACKERS
    End Enum

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        g_mUCVirtualControllers = New UCVirtualControllers()
        g_mUCVirtualControllers.SuspendLayout()
        g_mUCVirtualControllers.Parent = Panel_Pages
        g_mUCVirtualControllers.Dock = DockStyle.Fill
        g_mUCVirtualControllers.Visible = False
        g_mUCVirtualControllers.ResumeLayout()

        g_mUCVirtualHMDs = New UCVirtualHMDs()
        g_mUCVirtualHMDs.SuspendLayout()
        g_mUCVirtualHMDs.Parent = Panel_Pages
        g_mUCVirtualHMDs.Dock = DockStyle.Fill
        g_mUCVirtualHMDs.Visible = False
        g_mUCVirtualHMDs.ResumeLayout()

        g_mUCVirtualTrackers = New UCVirtualTrackers()
        g_mUCVirtualTrackers.SuspendLayout()
        g_mUCVirtualTrackers.Parent = Panel_Pages
        g_mUCVirtualTrackers.Dock = DockStyle.Fill
        g_mUCVirtualTrackers.Visible = False
        g_mUCVirtualTrackers.ResumeLayout()

        Label_Version.Text = String.Format("Version: {0}", Application.ProductVersion.ToString)
    End Sub

    Public Sub SelectPage(iPage As ENUM_PAGE)
        g_mUCVirtualControllers.Visible = False
        g_mUCVirtualHMDs.Visible = False
        g_mUCVirtualTrackers.Visible = False

        Select Case (iPage)
            Case ENUM_PAGE.VIRTUAL_CONTROLLERS
                g_mUCVirtualControllers.Visible = True
            Case ENUM_PAGE.VIRTUAL_HMDS
                g_mUCVirtualHMDs.Visible = True
            Case ENUM_PAGE.VIRTUAL_TRACKERS
                g_mUCVirtualTrackers.Visible = True
        End Select
    End Sub

    Private Sub ToolStripMenuItem_VDControllers_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_VDControllers.Click
        SelectPage(ENUM_PAGE.VIRTUAL_CONTROLLERS)
    End Sub

    Private Sub ToolStripMenuItem_VDHeadMountDevices_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_VDHeadMountDevices.Click
        SelectPage(ENUM_PAGE.VIRTUAL_HMDS)
    End Sub

    Private Sub ToolStripMenuItem_VDTrackers_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_VDTrackers.Click
        SelectPage(ENUM_PAGE.VIRTUAL_TRACKERS)
    End Sub

    Private Sub ToolStripMenuItem_FileExit_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_FileExit.Click
        Environment.Exit(0)
    End Sub

    Private Sub CleanUp()
        If (g_mUCVirtualControllers IsNot Nothing AndAlso Not g_mUCVirtualControllers.IsDisposed) Then
            g_mUCVirtualControllers.Dispose()
            g_mUCVirtualControllers = Nothing
        End If

        If (g_mUCVirtualHMDs IsNot Nothing AndAlso Not g_mUCVirtualHMDs.IsDisposed) Then
            g_mUCVirtualHMDs.Dispose()
            g_mUCVirtualHMDs = Nothing
        End If

        If (g_mUCVirtualTrackers IsNot Nothing AndAlso Not g_mUCVirtualTrackers.IsDisposed) Then
            g_mUCVirtualTrackers.Dispose()
            g_mUCVirtualTrackers = Nothing
        End If
    End Sub

    Private Function GetCameraNameById(iIndex As Integer) As String
        Dim sCameras As New List(Of String)
        Dim iCount As Integer = 0

        Dim sClasses = New String() {
            "Image",
            "Camera",
            "KinectSensor"
        }

        For i = 0 To sClasses.Length - 1
            sClasses(i) = String.Format("PNPClass = '{0}'", sClasses(i))
        Next

        Using searcher As New Management.ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE (" & String.Join(" OR ", sClasses) & ")")
            For Each device In searcher.[Get]()
                If (iCount = iIndex) Then
                    Return device("Caption").ToString()
                End If

                iCount += 1
            Next
        End Using

        Return Nothing
    End Function

    Private Sub Button_RestartPSMS_Click(sender As Object, e As EventArgs) Handles Button_RestartPSMS.Click
        Try
            Dim sPSMSPath As String = Nothing

            Dim pProcesses As Process() = Process.GetProcessesByName("PSMoveService")
            If (pProcesses Is Nothing OrElse pProcesses.Length < 1) Then
                Throw New ArgumentException("PSMoveService not running.")
            End If

            For Each mProcess In pProcesses
                If (sPSMSPath Is Nothing) Then
                    sPSMSPath = mProcess.MainModule.FileName
                End If

                If (mProcess.CloseMainWindow()) Then
                    mProcess.WaitForExit(10000)
                Else
                    mProcess.Kill()
                End If
            Next

            If (sPSMSPath Is Nothing OrElse Not IO.File.Exists(sPSMSPath)) Then
                Throw New ArgumentException("PSMoveService executable not found. Please start manualy.")
            End If

            Dim mNewProcess As New Process()
            mNewProcess.StartInfo.FileName = sPSMSPath
            mNewProcess.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(sPSMSPath)
            mNewProcess.StartInfo.UseShellExecute = False
            mNewProcess.Start()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Class
