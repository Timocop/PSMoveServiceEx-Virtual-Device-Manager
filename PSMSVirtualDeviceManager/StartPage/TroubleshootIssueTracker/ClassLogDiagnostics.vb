Public Class ClassLogDiagnostics
    Public Enum ENUM_LOG_ISSUE_TYPE
        INFO
        WARNING
        [ERROR]
    End Enum

    Public Structure STRUC_LOG_ISSUE
        Dim bValid As Boolean

        Dim sMessage As String
        Dim sDescription As String
        Dim sSolution As String
        Dim iType As ENUM_LOG_ISSUE_TYPE

        Public Sub New(_Issue As STRUC_LOG_ISSUE)
            Me.New(_Issue.sMessage, _Issue.sDescription, _Issue.sSolution, _Issue.iType)
        End Sub

        Public Sub New(_Message As String, _Description As String, _Solution As String, _Type As ENUM_LOG_ISSUE_TYPE)
            bValid = True
            sMessage = _Message
            sDescription = _Description
            sSolution = _Solution
            iType = _Type
        End Sub
    End Structure

    Public Interface ILogAction
        Function GetActionTitle() As String
        Sub Generate(bSilent As Boolean)
        Function GetIssues() As STRUC_LOG_ISSUE()
        Function GetSectionContent() As String
    End Interface

    Public Class ClassLogContent
        Private g_mThreadLock As New Object
        Private g_mLogContent As New Dictionary(Of String, String)

        ReadOnly Property m_Content As Dictionary(Of String, String)
            Get
                SyncLock g_mThreadLock
                    Return g_mLogContent
                End SyncLock
            End Get
        End Property

        ReadOnly Property m_Lock As Object
            Get
                Return g_mThreadLock
            End Get
        End Property
    End Class

    Public Shared Function GetAllJobs(mFormMain As FormMain, mLogContent As ClassLogContent) As ILogAction()
        Dim mLogJobs As New List(Of ILogAction)

        mLogJobs.Add(New ClassLogDxdiag(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogService(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogProcesses(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManager(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManagerVmtTrackers(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManageOscDevices(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManageServiceDevices(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManagerAttachments(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManagerRemoteDevices(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManagerPSVR(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManagerVirtualTrackers(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManagerSteamVrDrivers(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManagerSteamVrManifests(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManagerSteamVrOverrides(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManagerSteamVrSettings(mFormMain, mLogContent))
        mLogJobs.Add(New ClassLogManagerHardware(mFormMain, mLogContent))

        Return mLogJobs.ToArray()
    End Function
End Class
