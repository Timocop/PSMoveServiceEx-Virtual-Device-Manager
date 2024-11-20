Imports System.Numerics
Imports PSMoveServiceExCAPI
Imports PSMoveServiceExCAPI.PSMoveServiceExCAPI
Imports PSMoveServiceExCAPI.PSMoveServiceExCAPI.Constants
Imports PSMSVirtualDeviceManager

Public Class ClassServiceClient
    Implements IDisposable

    Private Shared g_mClientLock As New Object
    Private Shared g_mDataLock As New Object

    Private g_PSMoveServiceServer As Service
    Private g_ProcessingThread As Threading.Thread

    Private g_mPostStreamRequest As New List(Of String)

    Private g_bEnableSelectRecenter As Boolean = True

    Private g_bIsConnected As Boolean = False

    Event OnControllerUpdate(iControllerId As Integer)
    Event OnHmdUpdate(iHmdId As Integer)
    Event OnControllerListChanged()
    Event OnHmdListChanged()
    Event OnTrackerListChanged()
    Event OnPlayspaceOffsetChanged()
    Event OnConnectionStatusChanged()

    Public Interface IControllerData
        Property m_IsValid As Boolean
        Property m_IsConnected As Boolean
        Property m_IsTracking As Boolean
        Property m_Id As Integer
        Property m_Serial As String

        Property m_Position As Vector3
        Property m_Orientation As Quaternion
        Property m_PositionVelocity As Vector3
        Property m_OrientationVelocity As Vector3

        Property m_OutputSeqNum As Integer
        Property m_BatteryLevel As Single
        Property m_TrackingColor As PSMTrackingColorType

        Property m_LastTimeStamp As Date

        Function GetOrientationEuler() As Vector3
    End Interface

    Public Interface IHmdData
        Property m_IsValid As Boolean
        Property m_IsConnected As Boolean
        Property m_IsTracking As Boolean
        Property m_Id As Integer
        Property m_Serial As String

        Property m_Position As Vector3
        Property m_Orientation As Quaternion
        Property m_PositionVelocity As Vector3
        Property m_OrientationVelocity As Vector3

        Property m_OutputSeqNum As Integer

        Property m_LastTimeStamp As Date

        Function GetOrientationEuler() As Vector3
    End Interface

    Public Interface ITrackerData
        Property m_Id As Integer
        Property m_Path As String

        Property m_Position As Vector3
        Property m_Orientation As Quaternion

        Property m_Exposure As Integer
        Property m_Gain As Integer
        Property m_Width As Integer

        Property m_OutputSeqNum As Integer

        Property m_LastTimeStamp As Date

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
        Public Property m_PositionVelocity As Vector3 Implements IControllerData.m_PositionVelocity
        Public Property m_OrientationVelocity As Vector3 Implements IControllerData.m_OrientationVelocity

        Public Property m_OutputSeqNum As Integer Implements IControllerData.m_OutputSeqNum
        Public Property m_BatteryLevel As Single Implements IControllerData.m_BatteryLevel
        Public Property m_TrackingColor As PSMTrackingColorType Implements IControllerData.m_TrackingColor

        Public Property m_LastTimeStamp As Date Implements IControllerData.m_LastTimeStamp

        Private Function IControllerData_GetOrientationEuler() As Vector3 Implements IControllerData.GetOrientationEuler
            Return ClassMathUtils.FromQ(m_Orientation)
        End Function
    End Structure

    Structure STRUC_TRACKER_DATA
        Implements ITrackerData

        Public Property m_Id As Integer Implements ITrackerData.m_Id
        Public Property m_Path As String Implements ITrackerData.m_Path

        Public Property m_Position As Vector3 Implements ITrackerData.m_Position
        Public Property m_Orientation As Quaternion Implements ITrackerData.m_Orientation

        Public Property m_OutputSeqNum As Integer Implements ITrackerData.m_OutputSeqNum

        Public Property m_LastTimeStamp As Date Implements ITrackerData.m_LastTimeStamp

        Public Property m_Exposure As Integer Implements ITrackerData.m_Exposure
        Public Property m_Gain As Integer Implements ITrackerData.m_Gain
        Public Property m_Width As Integer Implements ITrackerData.m_Width

        Public Function GetOrientationEuler() As Vector3 Implements ITrackerData.GetOrientationEuler
            Return ClassMathUtils.FromQ(m_Orientation)
        End Function
    End Structure

    Structure STRUC_MORPHEUS_HMD_DATA
        Implements IHmdData

        Public Property m_IsValid As Boolean Implements IHmdData.m_IsValid
        Public Property m_IsConnected As Boolean Implements IHmdData.m_IsConnected
        Public Property m_IsTracking As Boolean Implements IHmdData.m_IsTracking
        Public Property m_Id As Integer Implements IHmdData.m_Id
        Public Property m_Serial As String Implements IHmdData.m_Serial

        Public Property m_Position As Vector3 Implements IHmdData.m_Position
        Public Property m_Orientation As Quaternion Implements IHmdData.m_Orientation
        Public Property m_PositionVelocity As Vector3 Implements IHmdData.m_PositionVelocity
        Public Property m_OrientationVelocity As Vector3 Implements IHmdData.m_OrientationVelocity

        Public Property m_OutputSeqNum As Integer Implements IHmdData.m_OutputSeqNum
        Public Property m_LastTimeStamp As Date Implements IHmdData.m_LastTimeStamp

        Public Function GetOrientationEuler() As Vector3 Implements IHmdData.GetOrientationEuler
            Return ClassMathUtils.FromQ(m_Orientation)
        End Function
    End Structure

    Structure STRUC_VIRTUAL_HMD_DATA
        Implements IHmdData

        Public Property m_IsValid As Boolean Implements IHmdData.m_IsValid
        Public Property m_IsConnected As Boolean Implements IHmdData.m_IsConnected
        Public Property m_IsTracking As Boolean Implements IHmdData.m_IsTracking
        Public Property m_Id As Integer Implements IHmdData.m_Id
        Public Property m_Serial As String Implements IHmdData.m_Serial

        Public Property m_Position As Vector3 Implements IHmdData.m_Position
        Public Property m_Orientation As Quaternion Implements IHmdData.m_Orientation
        Public Property m_PositionVelocity As Vector3 Implements IHmdData.m_PositionVelocity
        Public Property m_OrientationVelocity As Vector3 Implements IHmdData.m_OrientationVelocity

        Public Property m_OutputSeqNum As Integer Implements IHmdData.m_OutputSeqNum
        Public Property m_LastTimeStamp As Date Implements IHmdData.m_LastTimeStamp

        Public Function GetOrientationEuler() As Vector3 Implements IHmdData.GetOrientationEuler
            Return ClassMathUtils.FromQ(m_Orientation)
        End Function
    End Structure

    Private g_mControllerPool As New Dictionary(Of Integer, IControllerData)
    Private g_mHmdPool As New Dictionary(Of Integer, IHmdData)
    Private g_mTrackerPool As New Dictionary(Of Integer, STRUC_TRACKER_DATA)

    Public Sub New()
    End Sub

    Public Sub ServiceStart()
        SyncLock g_mClientLock
            If (g_PSMoveServiceServer IsNot Nothing) Then
                Return
            End If

            g_PSMoveServiceServer = New Service()
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
            SyncLock g_mClientLock
                Return g_bEnableSelectRecenter
            End SyncLock
        End Get
        Set(value As Boolean)
            SyncLock g_mClientLock
                g_bEnableSelectRecenter = value
            End SyncLock
        End Set
    End Property

    ReadOnly Property m_IsServiceConnected As Boolean
        Get
            SyncLock g_mClientLock
                Return g_bIsConnected
            End SyncLock
        End Get
    End Property

    Private Sub ProcessingThread()
        Try
            Dim mControllers As New List(Of Controllers)
            Dim mHmds As New List(Of HeadMountedDevices)
            Dim mTrackers As New List(Of Trackers)
            Dim bRefreshControllerList As Boolean = True
            Dim bRefreshHmdList As Boolean = True
            Dim bRefreshTrackerList As Boolean = True

            Dim bConnected As Boolean = False
            Dim bDisconnected As Boolean = False

            Try
                While True
                    Dim bExceptionSleep As Boolean = False

                    Try
                        SyncLock g_mClientLock
                            g_bIsConnected = g_PSMoveServiceServer.IsConnected
                            If (bConnected <> g_bIsConnected OrElse bDisconnected) Then
                                bConnected = g_bIsConnected
                                bDisconnected = False

                                bRefreshControllerList = True
                                bRefreshHmdList = True
                                bRefreshTrackerList = True

                                RaiseEvent OnConnectionStatusChanged()
                            End If

                            If (Not g_bIsConnected) Then
                                g_PSMoveServiceServer.Disconnect()
                                g_PSMoveServiceServer.Connect()

                                g_bIsConnected = g_PSMoveServiceServer.IsConnected
                            End If

                            g_PSMoveServiceServer.Update()

                            If (bConnected <> g_bIsConnected OrElse bDisconnected) Then
                                bConnected = g_bIsConnected
                                bDisconnected = False

                                bRefreshControllerList = True
                                bRefreshHmdList = True
                                bRefreshTrackerList = True

                                RaiseEvent OnConnectionStatusChanged()
                            End If

                            If (g_PSMoveServiceServer.HasControllerListChanged) Then
                                bRefreshControllerList = True

                                RaiseEvent OnControllerListChanged()
                            End If

                            If (g_PSMoveServiceServer.HasHMDListChanged) Then
                                bRefreshHmdList = True

                                RaiseEvent OnHmdListChanged()
                            End If

                            If (g_PSMoveServiceServer.HasTrackerListChanged) Then
                                bRefreshTrackerList = True

                                RaiseEvent OnTrackerListChanged()
                            End If

                            If (g_PSMoveServiceServer.HasPlayspaceOffsetChanged) Then
                                bRefreshTrackerList = True

                                RaiseEvent OnPlayspaceOffsetChanged()
                            End If

                            If (bRefreshControllerList) Then
                                bRefreshControllerList = False

                                For i = 0 To mControllers.Count - 1
                                    mControllers(i).Dispose()
                                Next
                                mControllers.Clear()

                                mControllers.AddRange(Controllers.GetControllerList())

                                SyncLock g_mDataLock
                                    g_mControllerPool.Clear()
                                End SyncLock
                            End If

                            If (bRefreshHmdList) Then
                                bRefreshHmdList = False

                                For i = 0 To mHmds.Count - 1
                                    mHmds(i).Dispose()
                                Next
                                mHmds.Clear()

                                mHmds.AddRange(HeadMountedDevices.GetHmdList())

                                SyncLock g_mDataLock
                                    g_mHmdPool.Clear()
                                End SyncLock
                            End If

                            If (bRefreshTrackerList) Then
                                bRefreshTrackerList = False

                                For i = 0 To mTrackers.Count - 1
                                    mTrackers(i).Dispose()
                                Next
                                mTrackers.Clear()

                                mTrackers.AddRange(Trackers.GetTrackerList())

                                SyncLock g_mDataLock
                                    g_mTrackerPool.Clear()
                                End SyncLock
                            End If
                        End SyncLock

                        For Each mController As Controllers In mControllers
                            Try
                                SyncLock g_mClientLock
                                    If (g_mPostStreamRequest.Count > 0) Then
                                        Dim flags = mController.m_DataStreamFlags
                                        If ((flags And PSMStreamFlags.PSMStreamFlags_includePositionData) = 0) Then
                                            flags = (flags Or PSMStreamFlags.PSMStreamFlags_includePositionData)
                                        End If
                                        If ((flags And PSMStreamFlags.PSMStreamFlags_includePhysicsData) = 0) Then
                                            flags = (flags Or PSMStreamFlags.PSMStreamFlags_includePhysicsData)
                                        End If
                                        mController.m_DataStreamFlags = flags
                                    Else
                                        Dim flags = mController.m_DataStreamFlags
                                        If ((flags And PSMStreamFlags.PSMStreamFlags_includePositionData) > 0) Then
                                            flags = (flags And Not PSMStreamFlags.PSMStreamFlags_includePositionData)
                                        End If
                                        If ((flags And PSMStreamFlags.PSMStreamFlags_includePhysicsData) > 0) Then
                                            flags = (flags And Not PSMStreamFlags.PSMStreamFlags_includePhysicsData)
                                        End If
                                        mController.m_DataStreamFlags = flags
                                    End If

                                    If (Not mController.m_Listening) Then
                                        mController.m_Listening = True
                                    End If

                                    If (Not mController.m_DataStreamEnabled) Then
                                        mController.m_DataStreamEnabled = True
                                    End If

                                    mController.Refresh(Controllers.Info.RefreshFlags.RefreshType_All)

                                    Dim bNewData As Boolean = False

                                    SyncLock g_mDataLock
                                        If (g_mControllerPool.ContainsKey(mController.m_Info.m_ControllerId)) Then
                                            If (mController.m_Info.m_OutputSequenceNum <> g_mControllerPool(mController.m_Info.m_ControllerId).m_OutputSeqNum) Then
                                                bNewData = True
                                            End If
                                        Else
                                            bNewData = True
                                        End If
                                    End SyncLock

                                    If (bNewData) Then
                                        Select Case (mController.m_Info.m_ControllerType)
                                            Case PSMControllerType.PSMController_Move
                                                Dim bIsVirtual As Boolean = False

                                                Dim mData As New STRUC_PSMOVE_CONTROLLER_DATA
                                                mData.m_IsConnected = mController.m_Info.m_IsConnected
                                                mData.m_OutputSeqNum = mController.m_Info.m_OutputSequenceNum
                                                mData.m_Id = mController.m_Info.m_ControllerId
                                                mData.m_Serial = mController.m_Info.m_ControllerSerial
                                                mData.m_LastTimeStamp = Now

                                                If (mData.m_Serial.StartsWith("VirtualController")) Then
                                                    mData.m_Serial &= String.Format("_{0}", mController.m_Info.m_ControllerId)

                                                    bIsVirtual = True
                                                End If

                                                If (mController.m_Info.IsStateValid) Then
                                                    If (bIsVirtual) Then
                                                        mData.m_BatteryLevel = 1.0F
                                                    Else
                                                        Select Case (mController.m_Info.m_PSMoveState.m_BatteryValue)
                                                            Case PSMBatteryState.PSMBattery_0,
                                                                 PSMBatteryState.PSMBattery_20,
                                                                 PSMBatteryState.PSMBattery_40,
                                                                 PSMBatteryState.PSMBattery_60,
                                                                 PSMBatteryState.PSMBattery_80,
                                                                 PSMBatteryState.PSMBattery_100
                                                                mData.m_BatteryLevel = (0.2F * mController.m_Info.m_PSMoveState.m_BatteryValue)
                                                            Case Else
                                                                mData.m_BatteryLevel = 1.0F
                                                        End Select
                                                    End If

                                                    mData.m_TrackingColor = mController.m_Info.m_PSMoveState.m_TrackingColorType

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

                                                If (mController.m_Info.IsPhysicsValid) Then
                                                    mData.m_PositionVelocity = New Vector3(
                                                        mController.m_Info.m_Physics.m_LinearVelocityCmPerSec.x,
                                                        mController.m_Info.m_Physics.m_LinearVelocityCmPerSec.y,
                                                        mController.m_Info.m_Physics.m_LinearVelocityCmPerSec.z)

                                                    mData.m_OrientationVelocity = New Vector3(
                                                        mController.m_Info.m_Physics.m_AngularVelocityRadPerSec.x,
                                                        mController.m_Info.m_Physics.m_AngularVelocityRadPerSec.y,
                                                        mController.m_Info.m_Physics.m_AngularVelocityRadPerSec.z)
                                                End If

                                                'Santiy check, for some reason it can sometimes prodcuce NaN?
                                                If (IsNaN(mData.m_Position)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mControllerPool.ContainsKey(mController.m_Info.m_ControllerId)) Then
                                                            mData.m_Position = g_mControllerPool(mController.m_Info.m_ControllerId).m_Position
                                                        Else
                                                            mData.m_Position = Vector3.Zero
                                                        End If
                                                    End SyncLock
                                                End If

                                                If (IsNaN(mData.m_Orientation)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mControllerPool.ContainsKey(mController.m_Info.m_ControllerId)) Then
                                                            mData.m_Orientation = g_mControllerPool(mController.m_Info.m_ControllerId).m_Orientation
                                                        Else
                                                            mData.m_Orientation = Quaternion.Identity
                                                        End If
                                                    End SyncLock
                                                End If

                                                If (IsNaN(mData.m_PositionVelocity)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mControllerPool.ContainsKey(mController.m_Info.m_ControllerId)) Then
                                                            mData.m_PositionVelocity = g_mControllerPool(mController.m_Info.m_ControllerId).m_PositionVelocity
                                                        Else
                                                            mData.m_PositionVelocity = Vector3.Zero
                                                        End If
                                                    End SyncLock
                                                End If

                                                If (IsNaN(mData.m_OrientationVelocity)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mControllerPool.ContainsKey(mController.m_Info.m_ControllerId)) Then
                                                            mData.m_OrientationVelocity = g_mControllerPool(mController.m_Info.m_ControllerId).m_OrientationVelocity
                                                        Else
                                                            mData.m_OrientationVelocity = Vector3.Zero
                                                        End If
                                                    End SyncLock
                                                End If

                                                SyncLock g_mDataLock
                                                    g_mControllerPool(mController.m_Info.m_ControllerId) = mData
                                                End SyncLock
                                        End Select

                                        RaiseEvent OnControllerUpdate(mController.m_Info.m_ControllerId)
                                    End If

                                End SyncLock
                            Catch ex As Exception
                                ClassAdvancedExceptionLogging.WriteToLog(ex)
                            End Try
                        Next

                        For Each mHmd As HeadMountedDevices In mHmds
                            Try
                                SyncLock g_mClientLock
                                    If (g_mPostStreamRequest.Count > 0) Then
                                        Dim flags = mHmd.m_DataStreamFlags
                                        If ((flags And PSMStreamFlags.PSMStreamFlags_includePositionData) = 0) Then
                                            flags = (flags Or PSMStreamFlags.PSMStreamFlags_includePositionData)
                                        End If
                                        If ((flags And PSMStreamFlags.PSMStreamFlags_includePhysicsData) = 0) Then
                                            flags = (flags Or PSMStreamFlags.PSMStreamFlags_includePhysicsData)
                                        End If
                                        mHmd.m_DataStreamFlags = flags
                                    Else
                                        Dim flags = mHmd.m_DataStreamFlags
                                        If ((flags And PSMStreamFlags.PSMStreamFlags_includePositionData) > 0) Then
                                            flags = (flags And Not PSMStreamFlags.PSMStreamFlags_includePositionData)
                                        End If
                                        If ((flags And PSMStreamFlags.PSMStreamFlags_includePhysicsData) > 0) Then
                                            flags = (flags And Not PSMStreamFlags.PSMStreamFlags_includePhysicsData)
                                        End If
                                        mHmd.m_DataStreamFlags = flags
                                    End If

                                    If (Not mHmd.m_Listening) Then
                                        mHmd.m_Listening = True
                                    End If

                                    If (Not mHmd.m_DataStreamEnabled) Then
                                        mHmd.m_DataStreamEnabled = True
                                    End If

                                    mHmd.Refresh(HeadMountedDevices.Info.RefreshFlags.RefreshType_All)

                                    Dim bNewData As Boolean = False

                                    SyncLock g_mDataLock
                                        If (g_mHmdPool.ContainsKey(mHmd.m_Info.m_HmdId)) Then
                                            If (mHmd.m_Info.m_OutputSequenceNum <> g_mHmdPool(mHmd.m_Info.m_HmdId).m_OutputSeqNum) Then
                                                bNewData = True
                                            End If
                                        Else
                                            bNewData = True
                                        End If
                                    End SyncLock

                                    If (bNewData) Then
                                        Select Case (mHmd.m_Info.m_HmdType)
                                            Case PSMHmdType.PSMHmd_Morpheus
                                                Dim mData As New STRUC_MORPHEUS_HMD_DATA
                                                mData.m_IsConnected = mHmd.m_Info.m_IsConnected
                                                mData.m_OutputSeqNum = mHmd.m_Info.m_OutputSequenceNum
                                                mData.m_Id = mHmd.m_Info.m_HmdId
                                                mData.m_Serial = mHmd.m_Info.m_HmdSerial
                                                mData.m_LastTimeStamp = Now

                                                If (mHmd.m_Info.IsStateValid) Then
                                                    mData.m_IsTracking = mHmd.m_Info.m_PSMorpheusState.m_IsCurrentlyTracking
                                                End If

                                                If (mHmd.m_Info.IsPoseValid) Then
                                                    mData.m_Position = New Vector3(
                                                        mHmd.m_Info.m_Pose.m_Position.x,
                                                        mHmd.m_Info.m_Pose.m_Position.y,
                                                        mHmd.m_Info.m_Pose.m_Position.z)

                                                    mData.m_Orientation = New Quaternion(
                                                        mHmd.m_Info.m_Pose.m_Orientation.x,
                                                        mHmd.m_Info.m_Pose.m_Orientation.y,
                                                        mHmd.m_Info.m_Pose.m_Orientation.z,
                                                        mHmd.m_Info.m_Pose.m_Orientation.w)
                                                End If

                                                If (mHmd.m_Info.IsPhysicsValid) Then
                                                    mData.m_PositionVelocity = New Vector3(
                                                        mHmd.m_Info.m_Physics.m_LinearVelocityCmPerSec.x,
                                                        mHmd.m_Info.m_Physics.m_LinearVelocityCmPerSec.y,
                                                        mHmd.m_Info.m_Physics.m_LinearVelocityCmPerSec.z)

                                                    mData.m_OrientationVelocity = New Vector3(
                                                        mHmd.m_Info.m_Physics.m_AngularVelocityRadPerSec.x,
                                                        mHmd.m_Info.m_Physics.m_AngularVelocityRadPerSec.y,
                                                        mHmd.m_Info.m_Physics.m_AngularVelocityRadPerSec.z)
                                                End If

                                                'Santiy check, for some reason it can sometimes prodcuce NaN?
                                                If (IsNaN(mData.m_Position)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mHmdPool.ContainsKey(mHmd.m_Info.m_HmdId)) Then
                                                            mData.m_Position = g_mHmdPool(mHmd.m_Info.m_HmdId).m_Position
                                                        Else
                                                            mData.m_Position = Vector3.Zero
                                                        End If
                                                    End SyncLock
                                                End If

                                                If (IsNaN(mData.m_Orientation)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mHmdPool.ContainsKey(mHmd.m_Info.m_HmdId)) Then
                                                            mData.m_Orientation = g_mHmdPool(mHmd.m_Info.m_HmdId).m_Orientation
                                                        Else
                                                            mData.m_Orientation = Quaternion.Identity
                                                        End If
                                                    End SyncLock
                                                End If

                                                If (IsNaN(mData.m_PositionVelocity)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mHmdPool.ContainsKey(mHmd.m_Info.m_HmdId)) Then
                                                            mData.m_PositionVelocity = g_mHmdPool(mHmd.m_Info.m_HmdId).m_PositionVelocity
                                                        Else
                                                            mData.m_PositionVelocity = Vector3.Zero
                                                        End If
                                                    End SyncLock
                                                End If

                                                If (IsNaN(mData.m_OrientationVelocity)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mHmdPool.ContainsKey(mHmd.m_Info.m_HmdId)) Then
                                                            mData.m_OrientationVelocity = g_mHmdPool(mHmd.m_Info.m_HmdId).m_OrientationVelocity
                                                        Else
                                                            mData.m_OrientationVelocity = Vector3.Zero
                                                        End If
                                                    End SyncLock
                                                End If

                                                SyncLock g_mDataLock
                                                    g_mHmdPool(mHmd.m_Info.m_HmdId) = mData
                                                End SyncLock

                                            Case PSMHmdType.PSMHmd_Virtual
                                                Dim mData As New STRUC_VIRTUAL_HMD_DATA
                                                mData.m_IsConnected = mHmd.m_Info.m_IsConnected
                                                mData.m_OutputSeqNum = mHmd.m_Info.m_OutputSequenceNum
                                                mData.m_Id = mHmd.m_Info.m_HmdId
                                                mData.m_Serial = mHmd.m_Info.m_HmdSerial
                                                mData.m_LastTimeStamp = Now

                                                If (mHmd.m_Info.IsStateValid) Then
                                                    mData.m_IsTracking = mHmd.m_Info.m_PSMorpheusState.m_IsCurrentlyTracking
                                                End If

                                                If (mHmd.m_Info.IsPoseValid) Then
                                                    mData.m_Position = New Vector3(
                                                        mHmd.m_Info.m_Pose.m_Position.x,
                                                        mHmd.m_Info.m_Pose.m_Position.y,
                                                        mHmd.m_Info.m_Pose.m_Position.z)

                                                    mData.m_Orientation = New Quaternion(
                                                        mHmd.m_Info.m_Pose.m_Orientation.x,
                                                        mHmd.m_Info.m_Pose.m_Orientation.y,
                                                        mHmd.m_Info.m_Pose.m_Orientation.z,
                                                        mHmd.m_Info.m_Pose.m_Orientation.w)
                                                End If

                                                If (mHmd.m_Info.IsPhysicsValid) Then
                                                    mData.m_PositionVelocity = New Vector3(
                                                        mHmd.m_Info.m_Physics.m_LinearVelocityCmPerSec.x,
                                                        mHmd.m_Info.m_Physics.m_LinearVelocityCmPerSec.y,
                                                        mHmd.m_Info.m_Physics.m_LinearVelocityCmPerSec.z)

                                                    mData.m_OrientationVelocity = New Vector3(
                                                        mHmd.m_Info.m_Physics.m_AngularVelocityRadPerSec.x,
                                                        mHmd.m_Info.m_Physics.m_AngularVelocityRadPerSec.y,
                                                        mHmd.m_Info.m_Physics.m_AngularVelocityRadPerSec.z)
                                                End If

                                                'Santiy check, for some reason it can sometimes prodcuce NaN?
                                                If (IsNaN(mData.m_Position)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mHmdPool.ContainsKey(mHmd.m_Info.m_HmdId)) Then
                                                            mData.m_Position = g_mHmdPool(mHmd.m_Info.m_HmdId).m_Position
                                                        Else
                                                            mData.m_Position = Vector3.Zero
                                                        End If
                                                    End SyncLock
                                                End If

                                                If (IsNaN(mData.m_Orientation)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mHmdPool.ContainsKey(mHmd.m_Info.m_HmdId)) Then
                                                            mData.m_Orientation = g_mHmdPool(mHmd.m_Info.m_HmdId).m_Orientation
                                                        Else
                                                            mData.m_Orientation = Quaternion.Identity
                                                        End If
                                                    End SyncLock
                                                End If

                                                If (IsNaN(mData.m_PositionVelocity)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mHmdPool.ContainsKey(mHmd.m_Info.m_HmdId)) Then
                                                            mData.m_PositionVelocity = g_mHmdPool(mHmd.m_Info.m_HmdId).m_PositionVelocity
                                                        Else
                                                            mData.m_PositionVelocity = Vector3.Zero
                                                        End If
                                                    End SyncLock
                                                End If

                                                If (IsNaN(mData.m_OrientationVelocity)) Then
                                                    SyncLock g_mDataLock
                                                        If (g_mHmdPool.ContainsKey(mHmd.m_Info.m_HmdId)) Then
                                                            mData.m_OrientationVelocity = g_mHmdPool(mHmd.m_Info.m_HmdId).m_OrientationVelocity
                                                        Else
                                                            mData.m_OrientationVelocity = Vector3.Zero
                                                        End If
                                                    End SyncLock
                                                End If

                                                SyncLock g_mDataLock
                                                    g_mHmdPool(mHmd.m_Info.m_HmdId) = mData
                                                End SyncLock
                                        End Select

                                        RaiseEvent OnHmdUpdate(mHmd.m_Info.m_HmdId)
                                    End If

                                End SyncLock
                            Catch ex As Exception
                                ClassAdvancedExceptionLogging.WriteToLog(ex)
                            End Try
                        Next

                        For Each mTracker As Trackers In mTrackers
                            Try
                                SyncLock g_mClientLock
                                    If (Not mTracker.m_Listening) Then
                                        mTracker.m_Listening = True
                                    End If

                                    If (Not mTracker.m_DataStreamEnabled) Then
                                        mTracker.m_DataStreamEnabled = True
                                    End If

                                    mTracker.Refresh(Trackers.Info.RefreshFlags.RefreshType_Stats)

                                    Dim bNewData As Boolean = False

                                    SyncLock g_mDataLock
                                        If (g_mTrackerPool.ContainsKey(mTracker.m_Info.m_TrackerId)) Then
                                            If (mTracker.m_Info.m_Stats.m_SequenceNum <> g_mTrackerPool(mTracker.m_Info.m_TrackerId).m_OutputSeqNum) Then
                                                bNewData = True
                                            End If
                                        Else
                                            bNewData = True
                                        End If
                                    End SyncLock

                                    If (bNewData) Then
                                        Dim mData As New STRUC_TRACKER_DATA
                                        mData.m_Id = mTracker.m_Info.m_TrackerId
                                        mData.m_Path = mTracker.m_Info.m_DevicePath

                                        mData.m_OutputSeqNum = mTracker.m_Info.m_Stats.m_SequenceNum
                                        mData.m_LastTimeStamp = Now

                                        mData.m_Exposure = mTracker.m_Info.m_Stats.m_TrackerExposure
                                        mData.m_Gain = mTracker.m_Info.m_Stats.m_TrackerGain
                                        mData.m_Width = mTracker.m_Info.m_Stats.m_TrackerWidth

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

                                        SyncLock g_mDataLock
                                            g_mTrackerPool(mTracker.m_Info.m_TrackerId) = mData
                                        End SyncLock
                                    End If
                                End SyncLock

                            Catch ex As Exception
                                ClassAdvancedExceptionLogging.WriteToLog(ex)
                            End Try
                        Next

                    Catch ex As Threading.ThreadAbortException
                        Throw
                    Catch ex As ServiceExceptions.ServiceDisconnectedException
                        bDisconnected = True

                    Catch ex As Exception
                        bExceptionSleep = True
                        ClassAdvancedExceptionLogging.WriteToLog(ex)
                    End Try

                    ' Thread.Abort will not trigger inside a Try/Catch
                    If (bExceptionSleep) Then
                        bExceptionSleep = False
                        Threading.Thread.Sleep(5000)
                    End If

                    ClassPrecisionSleep.Sleep(1)
                End While
            Finally
                For i = 0 To mControllers.Count - 1
                    mControllers(i).Dispose()
                Next
                mControllers.Clear()

                For i = 0 To mHmds.Count - 1
                    mHmds(i).Dispose()
                Next
                mHmds.Clear()

                For i = 0 To mTrackers.Count - 1
                    mTrackers(i).Dispose()
                Next
                mTrackers.Clear()
            End Try

        Catch ex As Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            ClassAdvancedExceptionLogging.WriteToLog(ex)
        End Try
    End Sub

    Private Function IsNaN(mValue As Vector3) As Boolean
        Return (Single.IsNaN(mValue.X) OrElse Single.IsNaN(mValue.Y) OrElse Single.IsNaN(mValue.Z))
    End Function

    Private Function IsNaN(mValue As Quaternion) As Boolean
        Return (Single.IsNaN(mValue.X) OrElse Single.IsNaN(mValue.Y) OrElse Single.IsNaN(mValue.Z) OrElse Single.IsNaN(mValue.W))
    End Function

    Public Sub SetControllerRecenter(iIndex As Integer, mOrientation As Quaternion)
        SyncLock g_mClientLock
            Dim mController As New Controllers(iIndex)
            mController.ResetControlerOrientation(New PSMQuatf With {
                .x = mOrientation.X,
                .y = mOrientation.Y,
                .z = mOrientation.Z,
                .w = mOrientation.W
            })
        End SyncLock
    End Sub

    Public Sub SetControllerRumble(iIndex As Integer, fRumble As Single)
        SyncLock g_mClientLock
            Dim mController As New Controllers(iIndex)
            mController.SetControllerRumble(PSMControllerRumbleChannel.PSMControllerRumbleChannel_All, fRumble)
        End SyncLock
    End Sub

    Public Function GetControllersData() As IControllerData()
        SyncLock g_mDataLock
            Dim mControllerList As New List(Of IControllerData)

            For Each mItem In g_mControllerPool
                mControllerList.Add(mItem.Value)
            Next

            Return mControllerList.ToArray
        End SyncLock
    End Function

    Public Function GetHmdsData() As IHmdData()
        SyncLock g_mDataLock
            Dim mHmsdList As New List(Of IHmdData)

            For Each mItem In g_mHmdPool
                mHmsdList.Add(mItem.Value)
            Next

            Return mHmsdList.ToArray
        End SyncLock
    End Function

    Public Function GetTrackersData() As ITrackerData()
        SyncLock g_mDataLock
            Dim mTrackersList As New List(Of ITrackerData)

            For Each mItem In g_mTrackerPool
                mTrackersList.Add(mItem.Value)
            Next

            Return mTrackersList.ToArray
        End SyncLock
    End Function

    ReadOnly Property m_ControllerData(i As Integer) As IControllerData
        Get
            SyncLock g_mDataLock
                If (Not g_mControllerPool.ContainsKey(i)) Then
                    Return Nothing
                End If

                Return g_mControllerPool(i)
            End SyncLock
        End Get
    End Property

    ReadOnly Property m_HmdData(i As Integer) As IHmdData
        Get
            SyncLock g_mDataLock
                If (Not g_mHmdPool.ContainsKey(i)) Then
                    Return Nothing
                End If

                Return g_mHmdPool(i)
            End SyncLock
        End Get
    End Property

    ReadOnly Property m_TrackerData(i As Integer) As ITrackerData
        Get
            SyncLock g_mDataLock
                If (Not g_mTrackerPool.ContainsKey(i)) Then
                    Return Nothing
                End If

                Return g_mTrackerPool(i)
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
