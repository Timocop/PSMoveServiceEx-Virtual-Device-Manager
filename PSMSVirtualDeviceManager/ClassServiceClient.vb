Imports System.Numerics
Imports PSMoveServiceExCAPI.PSMoveServiceExCAPI

Public Class ClassServiceClient
    Implements IDisposable

    Private Shared __ClientLock As New Object
    Private Shared __DataLock As New Object

    Private g_PSMoveServiceServer As Service
    Private g_ProcessingThread As Threading.Thread

    Private g_bPoseStreamEnabled As Boolean = False
    Private g_bProcessingEnabled As Boolean = False

    Private g_bEnableSelectRecenter As Boolean = True

    Class STRUC_CONTROLLER_DATA
        Public bIsValid As Boolean
        Public bIsConnected As Boolean
        Public bIsTracking As Boolean

        Public mPosition As Vector3
        Public mOrientation As Quaternion

        Public iOutputSeqNum As Integer
    End Class

    Private Shared g_ControllerPool As New Dictionary(Of Integer, STRUC_CONTROLLER_DATA)

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
            End SyncLock
        End SyncLock
    End Sub

    Public Sub TheadStart()
        If (g_ProcessingThread IsNot Nothing AndAlso g_ProcessingThread.IsAlive) Then
            Return
        End If

        g_ProcessingThread = New Threading.Thread(AddressOf ProcessingThread)
        g_ProcessingThread.IsBackground = True
        g_ProcessingThread.Start()
    End Sub

    Public Sub StartPoseStream()
        SyncLock __ClientLock
            g_bPoseStreamEnabled = True
        End SyncLock
    End Sub

    Public Sub StartProcessing()
        SyncLock __ClientLock
            g_bProcessingEnabled = True
        End SyncLock
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

    Private Sub ProcessingThread()
        Dim mControllers As New List(Of Controllers)
        Dim bRefreshControllerList As Boolean = True

        Dim mControllerRecenterTime As New Dictionary(Of Integer, Stopwatch)

        Try
            While True
                Try
                    If (Not g_bProcessingEnabled) Then
                        Threading.Thread.Sleep(1000)
                        Continue While
                    End If

                    SyncLock __ClientLock
                        If (Not g_PSMoveServiceServer.IsConnected) Then
                            g_PSMoveServiceServer.Disconnect()
                            g_PSMoveServiceServer.Connect()
                        End If

                        g_PSMoveServiceServer.Update()

                        If (g_PSMoveServiceServer.HasControllerListChanged) Then
                            bRefreshControllerList = True
                        End If

                        If (g_PSMoveServiceServer.HasConnectionStatusChanged) Then
                            bRefreshControllerList = True
                        End If

                        If (bRefreshControllerList) Then
                            bRefreshControllerList = False

                            For i = 0 To mControllers.Count - 1
                                mControllers(i).Dispose()
                            Next
                            mControllers.Clear()

                            mControllers.AddRange(Controllers.GetControllerList())
                        End If
                    End SyncLock

                    For Each mController As Controllers In mControllers
                        Try
                            SyncLock __ClientLock
                                If (Not mControllerRecenterTime.ContainsKey(mController.m_Info.m_ControllerId)) Then
                                    mControllerRecenterTime(mController.m_Info.m_ControllerId) = New Stopwatch
                                End If

                                If (g_bPoseStreamEnabled) Then
                                    If ((mController.m_DataStreamFlags And Constants.PSMStreamFlags.PSMStreamFlags_includePositionData) = 0) Then
                                        mController.m_DataStreamFlags = (mController.m_DataStreamFlags Or Constants.PSMStreamFlags.PSMStreamFlags_includePositionData)
                                    End If
                                Else
                                    If ((mController.m_DataStreamFlags And Constants.PSMStreamFlags.PSMStreamFlags_includePositionData) > 0) Then
                                        mController.m_DataStreamFlags = (mController.m_DataStreamFlags And Not Constants.PSMStreamFlags.PSMStreamFlags_includePositionData)
                                    End If
                                End If

                                If (Not mController.m_Listening) Then
                                    mController.m_Listening = True
                                End If

                                If (Not mController.m_DataStreamEnabled) Then
                                    mController.m_DataStreamEnabled = True
                                End If

                                mController.Refresh(Controllers.Info.RefreshFlags.RefreshType_All)

                                Dim mData As New STRUC_CONTROLLER_DATA
                                mData.bIsConnected = mController.m_Info.m_IsConnected
                                mData.iOutputSeqNum = mController.m_Info.m_OutputSequenceNum

                                Select Case (mController.m_Info.m_ControllerType)
                                    Case Constants.PSMControllerType.PSMController_Move
                                        mData.bIsTracking = mController.m_Info.m_PSMoveState.m_bIsCurrentlyTracking

                                        ' Do recenter
                                        ' #TODO Probably want to use HMD orientation instead
                                        Dim mSelectRecenterTime = mControllerRecenterTime(mController.m_Info.m_ControllerId)
                                        Select Case (mController.m_Info.m_PSMoveState.m_SelectButton)
                                            Case Constants.PSMButtonState.PSMButtonState_PRESSED
                                                mSelectRecenterTime.Restart()

                                            Case Constants.PSMButtonState.PSMButtonState_RELEASED, Constants.PSMButtonState.PSMButtonState_UP
                                                If (mSelectRecenterTime.ElapsedMilliseconds > 250) Then
                                                    Dim mIdentity = New Constants.PSMQuatf With {
                                                        .x = 0,
                                                        .y = 0,
                                                        .z = 0,
                                                        .w = 1
                                                    }
                                                    mController.ResetControlerOrientation(mIdentity)
                                                End If

                                                mSelectRecenterTime.Reset()
                                        End Select

                                    Case Else
                                        mData.bIsTracking = False
                                End Select

                                If (mController.m_Info.IsPoseValid) Then
                                    mData.mPosition = New Vector3(
                                        mController.m_Info.m_Pose.m_Position.x,
                                        mController.m_Info.m_Pose.m_Position.y,
                                        mController.m_Info.m_Pose.m_Position.z)

                                    mData.mOrientation = New Quaternion(
                                        mController.m_Info.m_Pose.m_Orientation.x,
                                        mController.m_Info.m_Pose.m_Orientation.y,
                                        mController.m_Info.m_Pose.m_Orientation.z,
                                        mController.m_Info.m_Pose.m_Orientation.w)
                                End If

                                SyncLock __DataLock
                                    g_ControllerPool(mController.m_Info.m_ControllerId) = mData
                                End SyncLock
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
                            If (Not g_PSMoveServiceServer.IsConnected OrElse Not g_PSMoveServiceServer.IsInitialized) Then
                                bRefreshControllerList = True
                            End If
                        End If
                    End SyncLock
                    Threading.Thread.Sleep(5000)
                End Try
            End While
        Finally
            For i = 0 To mControllers.Count - 1
                mControllers(i).Dispose()
            Next
            mControllers.Clear()
        End Try
    End Sub

    Shared ReadOnly Property m_ControllerData(i As Integer) As STRUC_CONTROLLER_DATA
        Get
            SyncLock __DataLock
                If (Not g_ControllerPool.ContainsKey(i)) Then
                    Return Nothing
                End If

                Return g_ControllerPool(i)
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
