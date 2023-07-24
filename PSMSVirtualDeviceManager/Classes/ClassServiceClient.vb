Imports System.Numerics
Imports PSMoveServiceExCAPI.PSMoveServiceExCAPI
Imports PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants
Imports PSMSVirtualDeviceManager

Public Class ClassServiceClient
    Implements IDisposable

    Private Shared __ClientLock As New Object
    Private Shared __DataLock As New Object

    Private g_PSMoveServiceServer As Service
    Private g_ProcessingThread As Threading.Thread

    Private g_mPostStreamRequest As New List(Of String)

    Private g_bEnableSelectRecenter As Boolean = True

    Private g_bIsConnected As Boolean = False

    Public Interface IControllerData
        Property m_IsValid As Boolean
        Property m_IsConnected As Boolean
        Property m_IsTracking As Boolean
        Property m_Id As Integer
        Property m_Serial As String

        Property m_Position As Vector3
        Property m_Orientation As Quaternion

        Property m_OutputSeqNum As Integer
        Property m_BatteryLevel As Single

        Property m_LastTimeStamp As Date

        Function GetOrientationEuler() As Vector3
    End Interface

    Public Interface ITrackerData
        Property m_Path As String

        Property m_Position As Vector3
        Property m_Orientation As Quaternion

        Function GetOrientationEuler() As Vector3
    End Interface

    Structure STRUC_PSMOVE_CONTROLLER_DATA
        Implements IControllerData

        Public m_TriggerValue As Byte
        Public m_TriggerButton As Boolean
        Public m_MoveButton As Boolean
        Public m_PSButton As Boolean
        Public m_StartButton As Boolean
        Public m_SelectButton As Boolean
        Public m_SquareButton As Boolean
        Public m_CrossButton As Boolean
        Public m_CircleButton As Boolean
        Public m_TriangleButton As Boolean

        Public Property m_IsValid As Boolean Implements IControllerData.m_IsValid
        Public Property m_IsConnected As Boolean Implements IControllerData.m_IsConnected
        Public Property m_IsTracking As Boolean Implements IControllerData.m_IsTracking
        Public Property m_Id As Integer Implements IControllerData.m_Id
        Public Property m_Serial As String Implements IControllerData.m_Serial

        Public Property m_Position As Vector3 Implements IControllerData.m_Position
        Public Property m_Orientation As Quaternion Implements IControllerData.m_Orientation

        Public Property m_OutputSeqNum As Integer Implements IControllerData.m_OutputSeqNum
        Public Property m_BatteryLevel As Single Implements IControllerData.m_BatteryLevel

        Public Property m_LastTimeStamp As Date Implements IControllerData.m_LastTimeStamp

        Private Function IControllerData_GetOrientationEuler() As Vector3 Implements IControllerData.GetOrientationEuler
            Return ClassQuaternionTools.FromQ2(m_Orientation)
        End Function
    End Structure

    Structure STRUC_TRACKER_DATA
        Implements ITrackerData

        Public Property m_Path As String Implements ITrackerData.m_Path

        Public Property m_Position As Vector3 Implements ITrackerData.m_Position
        Public Property m_Orientation As Quaternion Implements ITrackerData.m_Orientation

        Public Function GetOrientationEuler() As Vector3 Implements ITrackerData.GetOrientationEuler
            Return ClassQuaternionTools.FromQ2(m_Orientation)
        End Function
    End Structure

    Private g_ControllerPool As New Dictionary(Of Integer, IControllerData)
    Private g_TrackerPool As New Dictionary(Of Integer, STRUC_TRACKER_DATA)

    Public Sub New()
    End Sub

    Public Sub ServiceStart()
        SyncLock __ClientLock
            ServiceStop()

            g_PSMoveServiceServer = New Service()
        End SyncLock
    End Sub

    Public Sub ServiceStop()
        SyncLock __ClientLock
            If (g_PSMoveServiceServer IsNot Nothing) Then
                g_PSMoveServiceServer.Dispose()
                g_PSMoveServiceServer = Nothing
            End If

            SyncLock __DataLock
                g_ControllerPool.Clear()
                g_TrackerPool.Clear()
            End SyncLock
        End SyncLock
    End Sub

    Public Sub StartProcessing()
        If (g_ProcessingThread IsNot Nothing AndAlso g_ProcessingThread.IsAlive) Then
            Return
        End If

        g_ProcessingThread = New Threading.Thread(AddressOf ProcessingThread)
        g_ProcessingThread.IsBackground = True
        g_ProcessingThread.Start()
    End Sub

    Public Sub RegisterPoseStream(sId As String)
        If (g_mPostStreamRequest.Contains(sId)) Then
            Return
        End If

        g_mPostStreamRequest.Add(sId)
    End Sub

    Public Sub UnregisterPoseStream(sId As String)
        If (Not g_mPostStreamRequest.Contains(sId)) Then
            Return
        End If

        g_mPostStreamRequest.Remove(sId)
    End Sub

    Property m_EnableSelectRecenter As Boolean
        Get
            SyncLock __ClientLock
                Return g_bEnableSelectRecenter
            End SyncLock
        End Get
        Set(value As Boolean)
            SyncLock __ClientLock
                g_bEnableSelectRecenter = value
            End SyncLock
        End Set
    End Property

    ReadOnly Property m_IsServiceConnected As Boolean
        Get
            SyncLock __ClientLock
                Return g_bIsConnected
            End SyncLock
        End Get
    End Property

    Private Sub ProcessingThread()
        Try
            Dim mControllers As New List(Of Controllers)
            Dim mTrackers As New List(Of Trackers)
            Dim bRefreshControllerList As Boolean = True
            Dim bRefreshTrackerList As Boolean = True

            Dim mControllerRecenterTime As New Dictionary(Of Integer, Stopwatch)

            Try
                While True
                    Dim bExceptionSleep As Boolean = False

                    Try
                        SyncLock __ClientLock
                            g_bIsConnected = g_PSMoveServiceServer.IsConnected

                            If (Not g_bIsConnected) Then
                                g_PSMoveServiceServer.Disconnect()
                                g_PSMoveServiceServer.Connect()
                            End If

                            g_PSMoveServiceServer.Update()

                            If (g_PSMoveServiceServer.HasControllerListChanged) Then
                                bRefreshControllerList = True
                            End If

                            If (g_PSMoveServiceServer.HasTrackerListChanged) Then
                                bRefreshTrackerList = True
                            End If

                            If (g_PSMoveServiceServer.HasPlayspaceOffsetChanged) Then
                                bRefreshTrackerList = True
                            End If

                            If (g_PSMoveServiceServer.HasConnectionStatusChanged) Then
                                bRefreshControllerList = True
                                bRefreshTrackerList = True
                            End If

                            If (bRefreshControllerList) Then
                                bRefreshControllerList = False

                                For i = 0 To mControllers.Count - 1
                                    mControllers(i).Dispose()
                                Next
                                mControllers.Clear()

                                mControllers.AddRange(Controllers.GetControllerList())

                                SyncLock __DataLock
                                    g_ControllerPool.Clear()
                                End SyncLock
                            End If

                            If (bRefreshTrackerList) Then
                                bRefreshTrackerList = False

                                For i = 0 To mTrackers.Count - 1
                                    mTrackers(i).Dispose()
                                Next
                                mTrackers.Clear()

                                mTrackers.AddRange(Trackers.GetTrackerList())

                                SyncLock __DataLock
                                    g_TrackerPool.Clear()
                                End SyncLock

                                For Each mTracker As Trackers In mTrackers
                                    Dim mData As New STRUC_TRACKER_DATA
                                    mData.m_Path = mTracker.m_Info.m_DevicePath

                                    If (mTracker.m_Info.IsPoseValid) Then
                                        mData.m_Position = New Vector3(
                                            mTracker.m_Info.m_Pose.m_Position.x,
                                            mTracker.m_Info.m_Pose.m_Position.y,
                                            mTracker.m_Info.m_Pose.m_Position.z)

                                        mData.m_Orientation = New Quaternion(
                                            mTracker.m_Info.m_Pose.m_Orientation.x,
                                            mTracker.m_Info.m_Pose.m_Orientation.y,
                                            mTracker.m_Info.m_Pose.m_Orientation.z,
                                            mTracker.m_Info.m_Pose.m_Orientation.w)
                                    End If

                                    SyncLock __DataLock
                                        g_TrackerPool(mTracker.m_Info.m_TrackerId) = mData
                                    End SyncLock
                                Next
                            End If
                        End SyncLock

                        For Each mController As Controllers In mControllers
                            Try
                                SyncLock __ClientLock
                                    If (Not mControllerRecenterTime.ContainsKey(mController.m_Info.m_ControllerId)) Then
                                        mControllerRecenterTime(mController.m_Info.m_ControllerId) = New Stopwatch
                                    End If

                                    If (g_mPostStreamRequest.Count > 0) Then
                                        If ((mController.m_DataStreamFlags And PSMStreamFlags.PSMStreamFlags_includePositionData) = 0) Then
                                            mController.m_DataStreamFlags = (mController.m_DataStreamFlags Or PSMStreamFlags.PSMStreamFlags_includePositionData)
                                        End If
                                    Else
                                        If ((mController.m_DataStreamFlags And PSMStreamFlags.PSMStreamFlags_includePositionData) > 0) Then
                                            mController.m_DataStreamFlags = (mController.m_DataStreamFlags And Not PSMStreamFlags.PSMStreamFlags_includePositionData)
                                        End If
                                    End If

                                    If (Not mController.m_Listening) Then
                                        mController.m_Listening = True
                                    End If

                                    If (Not mController.m_DataStreamEnabled) Then
                                        mController.m_DataStreamEnabled = True
                                    End If

                                    mController.Refresh(Controllers.Info.RefreshFlags.RefreshType_All)

                                    Dim bNewData As Boolean = False

                                    SyncLock __DataLock
                                        If (g_ControllerPool.ContainsKey(mController.m_Info.m_ControllerId)) Then
                                            If (mController.m_Info.m_OutputSequenceNum <> g_ControllerPool(mController.m_Info.m_ControllerId).m_OutputSeqNum) Then
                                                bNewData = True
                                            End If
                                        Else
                                            bNewData = True
                                        End If
                                    End SyncLock

                                    If (bNewData) Then
                                        Select Case (mController.m_Info.m_ControllerType)
                                            Case PSMControllerType.PSMController_Move
                                                Dim mData As New STRUC_PSMOVE_CONTROLLER_DATA
                                                mData.m_IsConnected = mController.m_Info.m_IsConnected
                                                mData.m_OutputSeqNum = mController.m_Info.m_OutputSequenceNum
                                                mData.m_Id = mController.m_Info.m_ControllerId
                                                mData.m_Serial = mController.m_Info.m_ControllerSerial
                                                mData.m_LastTimeStamp = Now

                                                If (mController.m_Info.IsStateValid) Then
                                                    Select Case (mController.m_Info.m_PSMoveState.m_BatteryValue)
                                                        Case PSMBatteryState.PSMBattery_0,
                                                             PSMBatteryState.PSMBattery_20,
                                                             PSMBatteryState.PSMBattery_40,
                                                             PSMBatteryState.PSMBattery_60,
                                                             PSMBatteryState.PSMBattery_80,
                                                             PSMBatteryState.PSMBattery_100
                                                            mData.m_BatteryLevel = (0.2F * mController.m_Info.m_PSMoveState.m_BatteryValue)
                                                        Case PSMBatteryState.PSMBattery_Charged,
                                                             PSMBatteryState.PSMBattery_Charging
                                                            mData.m_BatteryLevel = 1.0F
                                                    End Select

                                                    mData.m_IsTracking = mController.m_Info.m_PSMoveState.m_bIsCurrentlyTracking

                                                    mData.m_TriggerValue = mController.m_Info.m_PSMoveState.m_TriggerValue
                                                    mData.m_TriggerButton = (mController.m_Info.m_PSMoveState.m_TriggerButton > 0)
                                                    mData.m_MoveButton = (mController.m_Info.m_PSMoveState.m_MoveButton > 0)
                                                    mData.m_PSButton = (mController.m_Info.m_PSMoveState.m_PSButton > 0)
                                                    mData.m_StartButton = (mController.m_Info.m_PSMoveState.m_StartButton > 0)
                                                    mData.m_SelectButton = (mController.m_Info.m_PSMoveState.m_SelectButton > 0)
                                                    mData.m_SquareButton = (mController.m_Info.m_PSMoveState.m_SquareButton > 0)
                                                    mData.m_CrossButton = (mController.m_Info.m_PSMoveState.m_CrossButton > 0)
                                                    mData.m_CircleButton = (mController.m_Info.m_PSMoveState.m_CircleButton > 0)
                                                    mData.m_TriangleButton = (mController.m_Info.m_PSMoveState.m_TriangleButton > 0)

                                                    ' Do recenter
                                                    ' #TODO Probably want to use HMD orientation instead
                                                    Dim mSelectRecenterTime = mControllerRecenterTime(mController.m_Info.m_ControllerId)
                                                    Select Case (mController.m_Info.m_PSMoveState.m_SelectButton)
                                                        Case PSMButtonState.PSMButtonState_PRESSED
                                                            mSelectRecenterTime.Restart()

                                                        Case PSMButtonState.PSMButtonState_DOWN
                                                            If (mSelectRecenterTime.ElapsedMilliseconds > 250) Then
                                                                Dim mIdentity = New PSMQuatf With {
                                                                    .x = 0,
                                                                    .y = 0,
                                                                    .z = 0,
                                                                    .w = 1
                                                                }
                                                                mController.ResetControlerOrientation(mIdentity)

                                                                mSelectRecenterTime.Reset()
                                                            End If
                                                    End Select
                                                End If

                                                If (mController.m_Info.IsPoseValid) Then
                                                    mData.m_Position = New Vector3(
                                                        mController.m_Info.m_Pose.m_Position.x,
                                                        mController.m_Info.m_Pose.m_Position.y,
                                                        mController.m_Info.m_Pose.m_Position.z)

                                                    mData.m_Orientation = New Quaternion(
                                                        mController.m_Info.m_Pose.m_Orientation.x,
                                                        mController.m_Info.m_Pose.m_Orientation.y,
                                                        mController.m_Info.m_Pose.m_Orientation.z,
                                                        mController.m_Info.m_Pose.m_Orientation.w)
                                                End If

                                                SyncLock __DataLock
                                                    g_ControllerPool(mController.m_Info.m_ControllerId) = mData
                                                End SyncLock
                                        End Select
                                    End If

                                End SyncLock
                            Catch ex As Exception
                                'Whatever
                            End Try
                        Next

                        ClassPrecisionSleep.Sleep(1)
                    Catch ex As Threading.ThreadAbortException
                        Throw
                    Catch ex As Exception
                        SyncLock __ClientLock
                            If (g_PSMoveServiceServer IsNot Nothing) Then
                                If (Not g_PSMoveServiceServer.IsInitialized OrElse Not g_PSMoveServiceServer.IsConnected) Then
                                    bRefreshControllerList = True
                                    bRefreshTrackerList = True
                                End If
                            End If
                        End SyncLock

                        bExceptionSleep = True
                    End Try

                    ' Thread.Abort will not trigger inside a Try/Catch
                    If (bExceptionSleep) Then
                        bExceptionSleep = False
                        Threading.Thread.Sleep(5000)
                    End If
                End While
            Finally
                For i = 0 To mControllers.Count - 1
                    mControllers(i).Dispose()
                Next
                mControllers.Clear()

                For i = 0 To mTrackers.Count - 1
                    mTrackers(i).Dispose()
                Next
                mTrackers.Clear()
            End Try

        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetControllerRumble(iIndex As Integer, fRumble As Single)
        SyncLock __ClientLock
            Dim mController As New Controllers(iIndex)
            mController.SetControllerRumble(PSMControllerRumbleChannel.PSMControllerRumbleChannel_All, fRumble)
        End SyncLock
    End Sub

    Public Function GetControllersData() As IControllerData()
        SyncLock __DataLock
            Dim mControllerList As New List(Of IControllerData)

            For Each mItem In g_ControllerPool
                mControllerList.Add(mItem.Value)
            Next

            Return mControllerList.ToArray
        End SyncLock
    End Function

    Public Function GetTrackersData() As ITrackerData()
        SyncLock __DataLock
            Dim mTrackersList As New List(Of ITrackerData)

            For Each mItem In g_TrackerPool
                mTrackersList.Add(mItem.Value)
            Next

            Return mTrackersList.ToArray
        End SyncLock
    End Function

    ReadOnly Property m_ControllerData(i As Integer) As IControllerData
        Get
            SyncLock __DataLock
                If (Not g_ControllerPool.ContainsKey(i)) Then
                    Return Nothing
                End If

                Return g_ControllerPool(i)
            End SyncLock
        End Get
    End Property

    ReadOnly Property m_TrackerData(i As Integer) As ITrackerData
        Get
            SyncLock __DataLock
                If (Not g_TrackerPool.ContainsKey(i)) Then
                    Return Nothing
                End If

                Return g_TrackerPool(i)
            End SyncLock
        End Get
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If (g_ProcessingThread IsNot Nothing AndAlso g_ProcessingThread.IsAlive) Then
                    g_ProcessingThread.Abort()
                    g_ProcessingThread.Join()
                    g_ProcessingThread = Nothing
                End If

                If (g_PSMoveServiceServer IsNot Nothing) Then
                    g_PSMoveServiceServer.Dispose()
                    g_PSMoveServiceServer = Nothing
                End If
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
