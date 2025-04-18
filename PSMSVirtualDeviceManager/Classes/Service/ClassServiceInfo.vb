﻿Public Class ClassServiceInfo
    Private g_sFileName As String = ""

    Private g_bConfigsLoaded As Boolean = False
    Private Shared g_mThreadLock As New Object

    Enum ENUM_SERVICE_PROCESS_TYPE
        NONE
        NORMAL
        ADMIN
    End Enum

    Public Sub New()
    End Sub

    Property m_FileName As String
        Get
            Return g_sFileName
        End Get
        Set(value As String)
            g_sFileName = value
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
            Try
                m_FileName = mProcess.MainModule.FileName
                Return True
            Catch ex As Exception
                ' Ignore any access denied
            End Try
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

    Public Function IsServiceRunning() As ENUM_SERVICE_PROCESS_TYPE
        If (Process.GetProcessesByName("PSMoveService").Count > 0) Then
            Return ENUM_SERVICE_PROCESS_TYPE.NORMAL

        ElseIf (Process.GetProcessesByName("PSMoveServiceAdmin").Count > 0) Then
            Return ENUM_SERVICE_PROCESS_TYPE.ADMIN

        Else
            Return ENUM_SERVICE_PROCESS_TYPE.NONE
        End If
    End Function

    Public Function IsConfigToolRunning() As Boolean
        Return (Process.GetProcessesByName("PSMoveConfigTool").Count > 0)
    End Function


    Public Sub SaveConfig()
        SyncLock g_mThreadLock
            If (Not g_bConfigsLoaded) Then
                Return
            End If

            Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_SETTINGS, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                    mIniContent.Add(New ClassIni.STRUC_INI_CONTENT("Settings", "PSMoveServiceLocation", m_FileName))

                    mIni.WriteKeyValue(mIniContent.ToArray)
                End Using
            End Using
        End SyncLock
    End Sub

    Public Sub LoadConfig()
        SyncLock g_mThreadLock
            Using mStream As New IO.FileStream(ClassConfigConst.PATH_CONFIG_SETTINGS, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Using mIni As New ClassIni(mStream)
                    m_FileName = mIni.ReadKeyValue("Settings", "PSMoveServiceLocation", "")
                End Using
            End Using

            g_bConfigsLoaded = True
        End SyncLock
    End Sub
End Class
