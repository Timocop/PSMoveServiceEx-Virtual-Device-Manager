Imports System.Runtime.InteropServices

Public Class UCVirtualTrackerItem
    Const MAX_PSMOVESERIVCE_TRACKERS = 8
    Const PROBE_MULTIPLY = 64

    Public g_mUCVirtualTrackers As UCVirtualTrackers

    Private g_mMessageLabel As Label

    Private g_mClassCaptureLogic As ClassCaptureLogic
    Private g_iPreviousTrackerIdSelectedIndex As Integer = -1

    Public g_bIgnoreEvents As Boolean = False
    Public g_bIgnoreUnsaved As Boolean = False

    Private g_iCaptureFps As Integer = 0
    Private g_iPipeFps As Integer = 0

    Public Sub New(_UCVirtualTrackers As UCVirtualTrackers, mDeviceInfo As ClassDevices.ClassDeviceInfo)
        g_mUCVirtualTrackers = _UCVirtualTrackers

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        g_mClassCaptureLogic = New ClassCaptureLogic(Me, mDeviceInfo.m_Index, mDeviceInfo.m_Path)
        Label_FriendlyName.Text = mDeviceInfo.m_Name

        Try
            g_bIgnoreEvents = True

            ' Add all possible trackers. Where as -1 means disabled.
            ComboBox_DeviceTrackerId.Items.Clear()
            For i = -1 To ClassSerivceConst.PSMOVESERVICE_MAX_TRACKER_COUNT - 1
                ComboBox_DeviceTrackerId.Items.Add(CStr(i))
            Next
            ComboBox_DeviceTrackerId.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try

        ' Keep the UI disabled until we are finished
        Me.Enabled = False

        ComboBox_ImageInterpolation.Items.Clear()
        ComboBox_ImageInterpolation.Items.Add("Nearest Neighbot (fastest/worest)")
        ComboBox_ImageInterpolation.Items.Add("Bilinear (default)")
        ComboBox_ImageInterpolation.Items.Add("Bicubic")
        ComboBox_ImageInterpolation.Items.Add("Lanczos (slowest/best)")
        If ([Enum].GetNames(GetType(ClassCaptureLogic.ENUM_INTERPOLATION)).Count <> ComboBox_ImageInterpolation.Items.Count) Then
            Throw New ArgumentException("Not equal")
        End If

        ' Add a "Please wait" UI message while initalizing the video input device
        g_mMessageLabel = New Label()
        g_mMessageLabel.Parent = Me
        g_mMessageLabel.TextAlign = ContentAlignment.MiddleCenter
        g_mMessageLabel.AutoSize = False
        g_mMessageLabel.Dock = DockStyle.Fill
        g_mMessageLabel.Text = "Please wait..."
        g_mMessageLabel.Font = New Font(g_mMessageLabel.Font.FontFamily, 24, FontStyle.Bold)
        g_mMessageLabel.BringToFront()
        g_mMessageLabel.Show()

        SetFpsText(0, 0)
        SetUnsavedState(False)

        CreateControl()
    End Sub

    Private Sub SetUnsavedState(bIsUnsaved As Boolean)
        If (g_bIgnoreUnsaved) Then
            Return
        End If

        If (bIsUnsaved) Then
            Button_ConfigSave.Text = String.Format("Save Settings*")
            Button_ConfigSave.Font = New Font(Button_ConfigSave.Font, FontStyle.Bold)
        Else
            Button_ConfigSave.Text = String.Format("Save Settings")
            Button_ConfigSave.Font = New Font(Button_ConfigSave.Font, FontStyle.Regular)
        End If
    End Sub

    Private Sub UCVirtualTrackerItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        g_mClassCaptureLogic.StartInitThread(False)
    End Sub

    ReadOnly Property m_DevicePath As String
        Get
            If (g_mClassCaptureLogic Is Nothing) Then
                Return Nothing
            End If

            Return g_mClassCaptureLogic.m_DevicePath
        End Get
    End Property

    Private Sub TrackBar_DeviceExposure_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_DeviceExposure.ValueChanged
        If (g_bIgnoreEvents OrElse Not TrackBar_DeviceExposure.Enabled) Then
            Return
        End If

        If (g_mClassCaptureLogic.m_Capture Is Nothing) Then
            Return
        End If

        g_mClassCaptureLogic.m_Capture.Exposure = TrackBar_DeviceExposure.Value
        SetUnsavedState(True)
    End Sub

    Private Sub TrackBar_DeviceGain_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_DeviceGain.ValueChanged
        If (g_bIgnoreEvents OrElse Not TrackBar_DeviceGain.Enabled) Then
            Return
        End If

        If (g_mClassCaptureLogic.m_Capture Is Nothing) Then
            Return
        End If

        g_mClassCaptureLogic.m_Capture.Gain = TrackBar_DeviceGain.Value
        SetUnsavedState(True)
    End Sub

    Private Sub TrackBar_DeviceGamma_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_DeviceGamma.ValueChanged
        If (g_bIgnoreEvents OrElse Not TrackBar_DeviceGamma.Enabled) Then
            Return
        End If

        If (g_mClassCaptureLogic.m_Capture Is Nothing) Then
            Return
        End If

        g_mClassCaptureLogic.m_Capture.Gamma = TrackBar_DeviceGamma.Value
        SetUnsavedState(True)
    End Sub

    Private Sub TrackBar_DeviceConstrast_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar_DeviceConstrast.ValueChanged
        If (g_bIgnoreEvents OrElse Not TrackBar_DeviceConstrast.Enabled) Then
            Return
        End If

        If (g_mClassCaptureLogic.m_Capture Is Nothing) Then
            Return
        End If

        g_mClassCaptureLogic.m_Capture.Contrast = TrackBar_DeviceConstrast.Value
        SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_ShowCaptureImage_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ShowCaptureImage.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        ' Disable others capture previews if they are enabled. We only need one esp. for performance.
        If (CheckBox_ShowCaptureImage.Checked) Then
            For Each mDeviceItem In g_mUCVirtualTrackers.GetAllDevices()
                If (mDeviceItem Is Me) Then
                    Continue For
                End If

                mDeviceItem.CheckBox_ShowCaptureImage.Checked = False
            Next
        End If

        g_mClassCaptureLogic.m_ShowCaptureImage = CheckBox_ShowCaptureImage.Checked
    End Sub

    Private Sub ComboBox_DeviceTrackerId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_DeviceTrackerId.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        Dim iSelectedTrackerId As Integer = CInt(ComboBox_DeviceTrackerId.SelectedItem)

        ' Check if the pipe index already has been used so we dont write to the same pipe.
        If (iSelectedTrackerId > -1) Then
            For Each mDeviceItem In g_mUCVirtualTrackers.GetAllDevices()
                If (mDeviceItem Is Me) Then
                    Continue For
                End If

                If (mDeviceItem.g_mClassCaptureLogic Is Nothing) Then
                    Continue For
                End If

                If (mDeviceItem.g_mClassCaptureLogic.m_PipeIndex < 0) Then
                    Continue For
                End If

                If (mDeviceItem.g_mClassCaptureLogic.m_PipeIndex = iSelectedTrackerId) Then
                    MessageBox.Show("This tracker id is already being in use!", "Unable to set tracker id", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    ComboBox_DeviceTrackerId.SelectedIndex = 0
                    Return
                End If
            Next
        End If

        If (iSelectedTrackerId > -1 AndAlso g_iPreviousTrackerIdSelectedIndex > 0 AndAlso ComboBox_DeviceTrackerId.SelectedIndex <> g_iPreviousTrackerIdSelectedIndex) Then
            Dim sMessage As New Text.StringBuilder
            sMessage.AppendLine("You are about to change the tracker id associated with this video input device.")
            sMessage.AppendLine("PSMoveService saves its virtual tracker settings using their tracker id and you will lose all settings configured for this device if you change the tracker id!")
            sMessage.AppendLine("You will have to re-configure this virtual tracker in PSMoveService again. (e.g. distortion calibration, color calibration etc.)")
            sMessage.AppendLine()
            sMessage.AppendLine("Click OK to continue or CANCEL to abort.")

            If (MessageBox.Show(sMessage.ToString, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                ComboBox_DeviceTrackerId.SelectedIndex = g_iPreviousTrackerIdSelectedIndex
                Return
            End If
        End If

        g_mClassCaptureLogic.m_PipeIndex = iSelectedTrackerId
        g_iPreviousTrackerIdSelectedIndex = ComboBox_DeviceTrackerId.SelectedIndex
        SetUnsavedState(True)
    End Sub

    Private Sub CheckBox_FlipHorizontal_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_FlipHorizontal.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassCaptureLogic.m_FlipImage = CheckBox_FlipHorizontal.Checked
        SetUnsavedState(True)
    End Sub

    Private Sub ComboBox_ImageInterpolation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_ImageInterpolation.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        g_mClassCaptureLogic.m_ImageInterpolation = CType(ComboBox_ImageInterpolation.SelectedIndex, ClassCaptureLogic.ENUM_INTERPOLATION)
        SetUnsavedState(True)
    End Sub

    Private Sub Button_RestartDevice_Click(sender As Object, e As EventArgs) Handles Button_RestartDevice.Click
        Try
            Me.Enabled = False

            ' Restart the init thread to get changed information about the device.
            g_mClassCaptureLogic.StartInitThread(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.Dispose()
        End Try
    End Sub

    Private Sub Button_ConfigSave_Click(sender As Object, e As EventArgs) Handles Button_ConfigSave.Click
        Try
            g_mClassCaptureLogic.g_mClassConfig.SaveConfig()
            SetUnsavedState(False)

            MessageBox.Show("Device settings saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CheckBox_Autostart_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Autostart.CheckedChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        SetUnsavedState(True)
    End Sub

    Private Sub Button_Close_Click(sender As Object, e As EventArgs) Handles Button_Close.Click
        Me.Dispose()
    End Sub

    Public Sub SetFpsText(iCaptureFps As Integer, iPipeFps As Integer)
        If (iCaptureFps > -1) Then
            g_iCaptureFps = iCaptureFps
        End If

        If (iPipeFps > -1) Then
            g_iPipeFps = iPipeFps
        End If

        If (g_iCaptureFps < 20 OrElse g_iPipeFps < 20) Then
            TextBox_Fps.ForeColor = Color.Red
        ElseIf (g_iCaptureFps < 27 OrElse g_iPipeFps < 27) Then
            TextBox_Fps.ForeColor = Color.DarkOrange
        Else
            TextBox_Fps.ForeColor = Color.Black
        End If

        TextBox_Fps.Text = String.Format("FPS: {0}, I/O FPS: {1}", g_iCaptureFps, g_iPipeFps)
    End Sub

    Private Sub CleanUp()
        If (g_mClassCaptureLogic IsNot Nothing) Then
            g_mClassCaptureLogic.Dispose()
            g_mClassCaptureLogic = Nothing
        End If
    End Sub

    Class ClassCaptureLogic
        Implements IDisposable

        Private g_bInitalized As Boolean = False

        Private g_mInitThread As Threading.Thread
        Private g_mCaptureThread As Threading.Thread
        Private g_mPipeThread As Threading.Thread
        Private g_mDeviceWatchdogThread As Threading.Thread

        Private g_mCapture As OpenCvSharp.VideoCapture
        Private g_mCaptureFrame As OpenCvSharp.Mat
        Private g_mPipEvent As New Threading.AutoResetEvent(False)

        Private g_mThreadLock As New Object

        Private g_mUCVirtualTrackerItem As UCVirtualTrackerItem

        Private g_bShowCaptureImage As Boolean = False
        Private g_iPipeIndex As Integer = -1

        Private g_iDeviceIndex As Integer = -1
        Private g_sDevicePath As String = ""
        Private g_bFlipImage As Boolean = False

        Enum ENUM_INTERPOLATION
            NEARST_NEIGHBOR
            BILINEAR
            BICUBIC
            LANCZOS
        End Enum
        Private g_iImageInterpolation As ENUM_INTERPOLATION = ENUM_INTERPOLATION.NEARST_NEIGHBOR

        Public g_mClassConfig As ClassConfig

        Public Sub New(_UCVirtualTrackerItem As UCVirtualTrackerItem, _DeviceIndex As Integer, _DevicePath As String)
            g_mUCVirtualTrackerItem = _UCVirtualTrackerItem
            g_iDeviceIndex = _DeviceIndex
            g_sDevicePath = _DevicePath

            g_mClassConfig = New ClassConfig(Me)

            g_mCaptureFrame = New OpenCvSharp.Mat(480, 640, OpenCvSharp.MatType.CV_8UC3)
        End Sub

        ''' <summary>
        ''' Starts or restarts the init thread.
        ''' The init thread prepares this class for future use. This is required to run before anything else.
        ''' </summary>
        ''' <param name="bRestart"></param>
        Public Sub StartInitThread(bRestart As Boolean)
            If (g_mInitThread IsNot Nothing AndAlso g_mInitThread.IsAlive) Then
                If (bRestart) Then
                    g_mInitThread.Abort()
                    g_mInitThread.Join()
                    g_mInitThread = Nothing
                Else
                    Return
                End If
            End If

            If (g_mCaptureThread IsNot Nothing AndAlso g_mCaptureThread.IsAlive) Then
                g_mCaptureThread.Abort()
                g_mCaptureThread.Join()
                g_mCaptureThread = Nothing
            End If

            If (g_mPipeThread IsNot Nothing AndAlso g_mPipeThread.IsAlive) Then
                g_mPipeThread.Abort()
                g_mPipeThread.Join()
                g_mPipeThread = Nothing
            End If

            If (g_mDeviceWatchdogThread IsNot Nothing AndAlso g_mDeviceWatchdogThread.IsAlive) Then
                g_mDeviceWatchdogThread.Abort()
                g_mDeviceWatchdogThread.Join()
                g_mDeviceWatchdogThread = Nothing
            End If

            g_mUCVirtualTrackerItem.Enabled = False

            g_mInitThread = New Threading.Thread(AddressOf InitThread)
            g_mInitThread.IsBackground = True
            g_mInitThread.Start()
        End Sub

        ''' <summary>
        ''' Start or restart the capture thread.
        ''' The capture thread will read and cache the devices frames.
        ''' </summary>
        ''' <param name="bRestart"></param>
        Private Sub StartCaptureThread(bRestart As Boolean)
            If (Not g_bInitalized) Then
                Return
            End If

            If (g_mCaptureThread IsNot Nothing AndAlso g_mCaptureThread.IsAlive) Then
                If (bRestart) Then
                    g_mCaptureThread.Abort()
                    g_mCaptureThread.Join()
                    g_mCaptureThread = Nothing
                Else
                    Return
                End If
            End If

            g_mCaptureThread = New Threading.Thread(AddressOf CaptureThread)
            g_mCaptureThread.IsBackground = True
            g_mCaptureThread.Start()
        End Sub

        ''' <summary>
        ''' Start or restart the named pipe thread.
        ''' The pipe thread reads the cached frame and sends it to a named pipe to PSMoveSerivce.
        ''' </summary>
        ''' <param name="bRestart"></param>
        Private Sub StartPipeThread(bRestart As Boolean)
            If (Not g_bInitalized) Then
                Return
            End If

            If (g_mPipeThread IsNot Nothing AndAlso g_mPipeThread.IsAlive) Then
                If (bRestart) Then
                    g_mPipeThread.Abort()
                    g_mPipeThread.Join()
                    g_mPipeThread = Nothing
                Else
                    Return
                End If
            End If

            g_mPipeThread = New Threading.Thread(AddressOf PipeThread)
            g_mPipeThread.IsBackground = True
            g_mPipeThread.Start()
        End Sub

        ''' <summary>
        ''' Starts or restarts the watchdog thread.
        ''' This thread will re-run the init thread if video input devices change.
        ''' 
        ''' $TODO Use WMI Plug-and-Play watcher instead.
        ''' </summary>
        ''' <param name="bRestart"></param>
        Private Sub StartDeviceWatchodogThread(bRestart As Boolean)
            If (Not g_bInitalized) Then
                Return
            End If

            If (g_mDeviceWatchdogThread IsNot Nothing AndAlso g_mDeviceWatchdogThread.IsAlive) Then
                If (bRestart) Then
                    g_mDeviceWatchdogThread.Abort()
                    g_mDeviceWatchdogThread.Join()
                    g_mDeviceWatchdogThread = Nothing
                Else
                    Return
                End If
            End If

            g_mDeviceWatchdogThread = New Threading.Thread(AddressOf DeviceWatchdogThread)
            g_mDeviceWatchdogThread.IsBackground = True
            g_mDeviceWatchdogThread.Start()
        End Sub

        Public Property m_Capture As OpenCvSharp.VideoCapture
            Get
                SyncLock g_mThreadLock
                    Return g_mCapture
                End SyncLock
            End Get
            Set(value As OpenCvSharp.VideoCapture)
                SyncLock g_mThreadLock
                    g_mCapture = value
                End SyncLock
            End Set
        End Property

        Public Property m_CaptureFrame As OpenCvSharp.Mat
            Get
                SyncLock g_mThreadLock
                    Return g_mCaptureFrame
                End SyncLock
            End Get
            Set(value As OpenCvSharp.Mat)
                SyncLock g_mThreadLock
                    g_mCaptureFrame = value
                End SyncLock
            End Set
        End Property

        Public Property m_ShowCaptureImage As Boolean
            Get
                SyncLock g_mThreadLock
                    Return g_bShowCaptureImage
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock g_mThreadLock
                    g_bShowCaptureImage = value
                End SyncLock
            End Set
        End Property

        Public Property m_PipeIndex As Integer
            Get
                SyncLock g_mThreadLock
                    Return g_iPipeIndex
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock g_mThreadLock
                    If (value > ClassSerivceConst.PSMOVESERVICE_MAX_TRACKER_COUNT - 1) Then
                        Return
                    End If

                    If (g_iPipeIndex <> value) Then
                        g_iPipeIndex = value

                        StartPipeThread(True)
                    End If
                End SyncLock
            End Set
        End Property

        Public Property m_DeviceIndex As Integer
            Get
                SyncLock g_mThreadLock
                    Return g_iDeviceIndex
                End SyncLock
            End Get
            Set(value As Integer)
                SyncLock g_mThreadLock
                    g_iDeviceIndex = value
                End SyncLock
            End Set
        End Property

        Public Property m_DevicePath As String
            Get
                SyncLock g_mThreadLock
                    Return g_sDevicePath
                End SyncLock
            End Get
            Set(value As String)
                SyncLock g_mThreadLock
                    g_sDevicePath = value
                End SyncLock
            End Set
        End Property

        Public Property m_FlipImage As Boolean
            Get
                SyncLock g_mThreadLock
                    Return g_bFlipImage
                End SyncLock
            End Get
            Set(value As Boolean)
                SyncLock g_mThreadLock
                    g_bFlipImage = value
                End SyncLock
            End Set
        End Property

        Public Property m_ImageInterpolation As ENUM_INTERPOLATION
            Get
                SyncLock g_mThreadLock
                    Return g_iImageInterpolation
                End SyncLock
            End Get
            Set(value As ENUM_INTERPOLATION)
                If (value < ENUM_INTERPOLATION.NEARST_NEIGHBOR) Then
                    value = ENUM_INTERPOLATION.NEARST_NEIGHBOR
                End If

                If (value > [Enum].GetNames(GetType(ENUM_INTERPOLATION)).Count - 1) Then
                    value = ENUM_INTERPOLATION.LANCZOS
                End If

                SyncLock g_mThreadLock
                    g_iImageInterpolation = value
                End SyncLock
            End Set
        End Property

        Public Function GetImageInterpolation() As OpenCvSharp.InterpolationFlags
            Select Case (m_ImageInterpolation)
                Case ENUM_INTERPOLATION.NEARST_NEIGHBOR
                    Return OpenCvSharp.InterpolationFlags.Nearest
                Case ENUM_INTERPOLATION.BILINEAR
                    Return OpenCvSharp.InterpolationFlags.Linear
                Case ENUM_INTERPOLATION.BICUBIC
                    Return OpenCvSharp.InterpolationFlags.Cubic
                Case ENUM_INTERPOLATION.LANCZOS
                    Return OpenCvSharp.InterpolationFlags.Lanczos4
            End Select

            Return OpenCvSharp.InterpolationFlags.Nearest
        End Function

        Public Function IsDeviceValid() As Boolean
            Return (GetDeviceIndexByPath() = m_DeviceIndex)
        End Function

        Public Function GetDeviceIndexByPath() As Integer
            Dim mDeviceList As New List(Of ClassDevices.ClassDeviceInfo)
            If (ClassDevices.GetDevicesOfVideoInput(mDeviceList)) Then
                For i = 0 To mDeviceList.Count - 1
                    If (mDeviceList(i).m_Path = m_DevicePath) Then
                        Return mDeviceList(i).m_Index
                    End If
                Next
            End If

            Return -1
        End Function

        Private Sub InitThread()
            Try
                g_mUCVirtualTrackerItem.BeginInvoke(Sub() g_mUCVirtualTrackerItem.g_mMessageLabel.Text = "Initializing device...")
                g_mUCVirtualTrackerItem.BeginInvoke(Sub() g_mUCVirtualTrackerItem.g_mMessageLabel.Visible = True)

                SyncLock g_mThreadLock
                    'Remove old capture before we create a new one
                    If (m_Capture IsNot Nothing AndAlso Not m_Capture.IsDisposed) Then
                        m_Capture.Dispose()
                        m_Capture = Nothing
                    End If

                    Dim iRealIndex As Integer = GetDeviceIndexByPath()
                    If (iRealIndex < 0) Then
                        Throw New ArgumentException("Unable to open video input device. Device can not be found.")
                    End If

                    ' Replace the old index with the new one if it changed by any way.
                    m_DeviceIndex = iRealIndex
                    m_Capture = New OpenCvSharp.VideoCapture(iRealIndex, OpenCvSharp.VideoCaptureAPIs.DSHOW)
                    If (Not m_Capture.IsOpened) Then
                        Throw New ArgumentException("Unable to open video input device.")
                    End If

                    ' Try to read the first frame before we change anything. Just in case properties wont apply instandly.
                    Using mMat As New OpenCvSharp.Mat
                        m_Capture.Read(mMat)
                    End Using

                    g_mUCVirtualTrackerItem.BeginInvoke(Sub() g_mUCVirtualTrackerItem.g_mMessageLabel.Text = "Setting default device properties...")

                    ' Disable any unneeded stuff. Tho, some things wont work still...
                    m_Capture.AutoExposure = -1
                    m_Capture.AutoFocus = False
                    m_Capture.WhiteBalanceBlueU = -1
                    m_Capture.WhiteBalanceRedV = -1
                    m_Capture.FrameHeight = 480
                    m_Capture.FrameWidth = 640
                    m_Capture.Fps = 30 ' Probably does not work

                    g_mUCVirtualTrackerItem.BeginInvoke(Sub() g_mUCVirtualTrackerItem.g_mMessageLabel.Text = "Probing device properties...")
                End SyncLock

                ' $TODO: Minimize copy paste code. This looks like ass.
                ' Probing exposure
                Dim iExposureDefault As Double = m_Capture.Exposure
                Dim iExposureMin As Double = 0
                Dim iExposureMax As Double = 0

                If (True) Then
                    For j = 0 To 1
                        For i = 1 To 255
                            If (j = 0) Then
                                m_Capture.Exposure = (i * PROBE_MULTIPLY)
                                If (m_Capture.Exposure = iExposureMax) Then
                                    iExposureMax = m_Capture.Exposure
                                    Exit For
                                End If

                                iExposureMax = m_Capture.Exposure
                            Else
                                m_Capture.Exposure = -(i * PROBE_MULTIPLY)
                                If (m_Capture.Exposure = iExposureMin) Then
                                    iExposureMin = m_Capture.Exposure
                                    Exit For
                                End If

                                iExposureMin = m_Capture.Exposure
                            End If
                        Next
                    Next

                    m_Capture.Exposure = iExposureDefault
                End If

                ' Probing gain
                Dim iGainDefault As Double = m_Capture.Gain
                Dim iGainMin As Double = 0
                Dim iGainMax As Double = 0

                If (True) Then
                    For j = 0 To 1
                        For i = 1 To 255
                            If (j = 0) Then
                                m_Capture.Gain = (i * PROBE_MULTIPLY)
                                If (m_Capture.Gain = iGainMax) Then
                                    iGainMax = m_Capture.Gain
                                    Exit For
                                End If

                                iGainMax = m_Capture.Gain
                            Else
                                m_Capture.Gain = -(i * PROBE_MULTIPLY)
                                If (m_Capture.Gain = iGainMin) Then
                                    iGainMin = m_Capture.Gain
                                    Exit For
                                End If

                                iGainMin = m_Capture.Gain
                            End If
                        Next
                    Next

                    m_Capture.Gain = iGainDefault
                End If

                ' Probing gamma
                Dim iGammaDefault As Double = m_Capture.Gamma
                Dim iGammaMin As Double = 0
                Dim iGammaMax As Double = 0

                If (True) Then
                    For j = 0 To 1
                        For i = 1 To 255
                            If (j = 0) Then
                                m_Capture.Gamma = (i * PROBE_MULTIPLY)
                                If (m_Capture.Gamma = iGammaMax) Then
                                    iGammaMax = m_Capture.Gamma
                                    Exit For
                                End If

                                iGammaMax = m_Capture.Gamma
                            Else
                                m_Capture.Gamma = -(i * PROBE_MULTIPLY)
                                If (m_Capture.Gamma = iGammaMin) Then
                                    iGammaMin = m_Capture.Gamma
                                    Exit For
                                End If

                                iGammaMin = m_Capture.Gamma
                            End If
                        Next
                    Next

                    m_Capture.Gamma = iGammaDefault
                End If

                ' Probing constrast
                Dim iContrastDefault As Double = m_Capture.Contrast
                Dim iContrastMin As Double = 0
                Dim iContrastMax As Double = 0

                If (True) Then
                    For j = 0 To 1
                        For i = 1 To 255
                            If (j = 0) Then
                                m_Capture.Contrast = (i * PROBE_MULTIPLY)
                                If (m_Capture.Contrast = iContrastMax) Then
                                    iContrastMax = m_Capture.Contrast
                                    Exit For
                                End If

                                iContrastMax = m_Capture.Contrast
                            Else
                                m_Capture.Contrast = -(i * PROBE_MULTIPLY)
                                If (m_Capture.Contrast = iContrastMin) Then
                                    iContrastMin = m_Capture.Contrast
                                    Exit For
                                End If

                                iContrastMin = m_Capture.Contrast
                            End If
                        Next
                    Next

                    m_Capture.Contrast = iContrastDefault
                End If

                g_mUCVirtualTrackerItem.Invoke(Sub()
                                                   Try
                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = True
                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True

                                                       Dim bDisabled As Boolean = (iExposureMin = iExposureMax)
                                                       If (bDisabled) Then
                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Enabled = False
                                                           Return
                                                       End If

                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Minimum = CInt(iExposureMin)
                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Maximum = CInt(iExposureMax)
                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Value = CInt(Math.Max(iExposureMin, Math.Min(iExposureMax, iExposureDefault)))
                                                   Finally
                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = False
                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                   End Try
                                               End Sub)

                g_mUCVirtualTrackerItem.Invoke(Sub()
                                                   Try
                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = True
                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True

                                                       Dim bDisabled As Boolean = (iGainMin = iGainMax)
                                                       If (bDisabled) Then
                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Enabled = False
                                                           Return
                                                       End If

                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Minimum = CInt(iGainMin)
                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Maximum = CInt(iGainMax)
                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Value = CInt(Math.Max(iGainMin, Math.Min(iGainMax, iGainDefault)))
                                                   Finally
                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = False
                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                   End Try
                                               End Sub)

                g_mUCVirtualTrackerItem.Invoke(Sub()
                                                   Try
                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = True
                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True

                                                       Dim bDisabled As Boolean = (iGammaMin = iGammaMax)
                                                       If (bDisabled) Then
                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Enabled = False
                                                           Return
                                                       End If

                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Minimum = CInt(iGammaMin)
                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Maximum = CInt(iGammaMax)
                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Value = CInt(Math.Max(iGammaMin, Math.Min(iGammaMax, iGammaDefault)))
                                                   Finally
                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = False
                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                   End Try
                                               End Sub)

                g_mUCVirtualTrackerItem.Invoke(Sub()
                                                   Try
                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = True
                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True

                                                       Dim bDisabled As Boolean = (iContrastMin = iContrastMax)
                                                       If (bDisabled) Then
                                                           g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Enabled = False
                                                           Return
                                                       End If

                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Minimum = CInt(iContrastMin)
                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Maximum = CInt(iContrastMax)
                                                       g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Value = CInt(Math.Max(iContrastMin, Math.Min(iContrastMax, iContrastDefault)))
                                                   Finally
                                                       g_mUCVirtualTrackerItem.g_bIgnoreEvents = False
                                                       g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                   End Try
                                               End Sub)

                g_mUCVirtualTrackerItem.Invoke(Sub()
                                                   m_ShowCaptureImage = g_mUCVirtualTrackerItem.CheckBox_ShowCaptureImage.Checked
                                                   m_PipeIndex = CInt(g_mUCVirtualTrackerItem.ComboBox_DeviceTrackerId.SelectedItem)
                                                   m_FlipImage = g_mUCVirtualTrackerItem.CheckBox_FlipHorizontal.Checked
                                                   m_ImageInterpolation = CType(g_mUCVirtualTrackerItem.ComboBox_ImageInterpolation.SelectedIndex, ENUM_INTERPOLATION)
                                               End Sub)

                g_mUCVirtualTrackerItem.BeginInvoke(Sub() g_mUCVirtualTrackerItem.g_mMessageLabel.Visible = False)
                g_mUCVirtualTrackerItem.BeginInvoke(Sub() g_mUCVirtualTrackerItem.Enabled = True)

                g_bInitalized = True

                ' Start all needed threads
                StartCaptureThread(False)
                StartPipeThread(False)
                StartDeviceWatchodogThread(False)

                ' Load saved config for this device
                g_mUCVirtualTrackerItem.BeginInvoke(Sub()
                                                        Try
                                                            g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = True

                                                            g_mClassConfig.LoadConfig()
                                                        Finally
                                                            g_mUCVirtualTrackerItem.g_bIgnoreUnsaved = False
                                                        End Try

                                                        g_mUCVirtualTrackerItem.SetUnsavedState(False)
                                                    End Sub)

            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Unable to initalize video device", MessageBoxButtons.OK, MessageBoxIcon.Error)

                g_mUCVirtualTrackerItem.BeginInvoke(Sub() g_mUCVirtualTrackerItem.Dispose())
            End Try
        End Sub

        Private Sub CaptureThread()
            Dim mFrameDelay As New Stopwatch
            Dim mFrameImage As New Stopwatch
            Dim mFramePrint As New Stopwatch
            mFrameDelay.Start()
            mFrameImage.Start()
            mFramePrint.Start()

            Dim iFPS As Integer = 0
            Dim iFpsSec As Integer = 1
            Dim iFpsCount As Integer = 0

            While True
                Try
                    Using mFrame As New OpenCvSharp.Mat(480, 640, OpenCvSharp.MatType.CV_8UC3)
                        If (Not m_Capture.IsOpened) Then
                            Throw New ArgumentException("Video capture not open.")
                        End If

                        If (Not m_Capture.Read(mFrame)) Then
                            Throw New ArgumentException("Unable to read video capture.")
                        End If

                        If (mFrame.Empty) Then
                            Throw New ArgumentException("Frame empty.")
                        End If

                        ' Calculate FPS
                        If (True) Then
                            iFPS += CInt(New TimeSpan(0, 0, 1).Ticks / Math.Max(1, mFrameDelay.ElapsedTicks))
                            iFpsCount += 1
                            mFrameDelay.Restart()

                            If ((mFramePrint.ElapsedMilliseconds / 1000) > iFpsSec) Then
                                Dim iPrintFPS As Integer = CInt(iFPS / iFpsCount)
                                g_mUCVirtualTrackerItem.BeginInvoke(Sub() g_mUCVirtualTrackerItem.SetFpsText(iPrintFPS, -1))

                                mFramePrint.Restart()
                                iFPS = 0
                                iFpsCount = 0
                            End If
                        End If

                        ' Sometimes setting resolutions on devices wont work. (e.g. Kinect One)
                        ' We NEED to resize the image to 640x480 because our buffer is only that size!
                        Using mNewFrame = mFrame.Resize(New OpenCvSharp.Size(640, 480), 0, 0, GetImageInterpolation())
                            mNewFrame.CopyTo(mFrame)
                        End Using

                        ' PSEyes have their Y flipped.
                        ' But some video input devices do Not. So flip them here instead.
                        If (m_FlipImage) Then
                            Using mNewMat = mFrame.Flip(OpenCvSharp.FlipMode.Y)
                                mNewMat.CopyTo(mFrame)
                            End Using
                        End If

                        ' Copy to global frame
                        mFrame.CopyTo(m_CaptureFrame)

                        g_mPipEvent.Set()

                        ' Preview captured image
                        ' $TODO: Quite performance heavy, find a better way.
                        If (mFrameImage.ElapsedMilliseconds > 100) Then
                            mFrameImage.Restart()

                            If (g_bShowCaptureImage) Then
                                Dim mBitmap As Bitmap = Nothing

                                Using mStream As New IO.MemoryStream
                                    Dim iByte As Byte() = mFrame.ImEncode()

                                    mStream.Write(iByte, 0, iByte.Length)

                                    mBitmap = New Bitmap(mStream)
                                End Using

                                g_mUCVirtualTrackerItem.BeginInvoke(Sub()
                                                                        If (g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image IsNot Nothing) Then
                                                                            g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image.Dispose()
                                                                            g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image = Nothing
                                                                        End If

                                                                        g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image = mBitmap
                                                                    End Sub)
                            Else
                                g_mUCVirtualTrackerItem.BeginInvoke(Sub()
                                                                        If (g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image IsNot Nothing) Then
                                                                            g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image.Dispose()
                                                                            g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image = Nothing
                                                                        End If

                                                                        g_mUCVirtualTrackerItem.PictureBox_CaptureImage.Image = Nothing
                                                                    End Sub)
                            End If
                        End If
                    End Using
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    If (mFramePrint.ElapsedMilliseconds > 1000) Then
                        g_mUCVirtualTrackerItem.BeginInvoke(Sub() g_mUCVirtualTrackerItem.SetFpsText(0, -1))

                        mFramePrint.Restart()
                        iFPS = 0
                        iFpsCount = 0
                    End If

                    Threading.Thread.Sleep(1000)
                End Try
            End While
        End Sub

        Private Sub PipeThread()
            Dim VIRT_BUFFER_SIZE As Integer = (m_CaptureFrame.Height * m_CaptureFrame.Width * 3)

            Dim mFrameDelay As New Stopwatch
            Dim mFrameImage As New Stopwatch
            Dim mFramePrint As New Stopwatch
            mFrameDelay.Start()
            mFrameImage.Start()
            mFramePrint.Start()

            Dim iFPS As Integer = 0
            Dim iFpsSec As Integer = 1
            Dim iFpsCount As Integer = 0

            While True
                Try
                    If (m_PipeIndex < 0) Then
                        Throw New ArgumentException("Invalid pipe index.")
                    End If

                    Using mPipe As New IO.Pipes.NamedPipeClientStream(".", "PSMoveSerivceEx\VirtPSeyeStream_" & m_PipeIndex, IO.Pipes.PipeDirection.Out)
                        ' The thread when aborting will hang if we dont put a timeout.
                        mPipe.Connect(5000)

                        While True
                            ' Calculate FPS
                            If (True) Then
                                iFPS += CInt(New TimeSpan(0, 0, 1).Ticks / Math.Max(1, mFrameDelay.ElapsedTicks))
                                iFpsCount += 1
                                mFrameDelay.Restart()

                                If ((mFramePrint.ElapsedMilliseconds / 1000) > iFpsSec) Then
                                    Dim iPrintFPS As Integer = CInt(iFPS / iFpsCount)
                                    g_mUCVirtualTrackerItem.BeginInvoke(Sub() g_mUCVirtualTrackerItem.SetFpsText(-1, iPrintFPS))

                                    mFramePrint.Restart()
                                    iFPS = 0
                                    iFpsCount = 0
                                End If
                            End If

                            g_mPipEvent.WaitOne(New TimeSpan(0, 0, 5))

                            Dim iBytes = New Byte(VIRT_BUFFER_SIZE) {}
                            Marshal.Copy(m_CaptureFrame.Data, iBytes, 0, iBytes.Length)

                            ' Write to pipe and wait for response.
                            ' $TODO Latency is quite ok but somewhat noticeable
                            mPipe.Write(iBytes, 0, iBytes.Length)
                            mPipe.WaitForPipeDrain()
                        End While
                    End Using
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    If (mFramePrint.ElapsedMilliseconds > 1000) Then
                        g_mUCVirtualTrackerItem.BeginInvoke(Sub() g_mUCVirtualTrackerItem.SetFpsText(-1, 0))

                        mFramePrint.Restart()
                        iFPS = 0
                        iFpsCount = 0
                    End If

                    Threading.Thread.Sleep(1000)
                End Try
            End While
        End Sub

        Private Sub DeviceWatchdogThread()
            While True
                Try
                    Threading.Thread.Sleep(5000)

                    If (Not IsDeviceValid()) Then
                        ' The device has changed somehow! Re-initiate!
                        g_mUCVirtualTrackerItem.BeginInvoke(Sub()
                                                                StartInitThread(True)
                                                            End Sub)

                        Return
                    End If
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    Threading.Thread.Sleep(1000)
                End Try
            End While
        End Sub

        Class ClassConfig
            Private Shared ReadOnly g_sConfigPath As String = IO.Path.Combine(Application.StartupPath, "devices.ini")

            Private g_mClassCaptureLogic As ClassCaptureLogic

            Public Sub New(_ClassCaptureLogic As ClassCaptureLogic)
                g_mClassCaptureLogic = _ClassCaptureLogic
            End Sub

            Public Shared Function CanDeviceAutostart(sDevicePath As String) As Boolean
                Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.Read)
                    Using mIni As New ClassIni(mStream)
                        Return (mIni.ReadKeyValue(sDevicePath, "Autostart", "False") = "True")
                    End Using
                End Using

                Return False
            End Function

            Public Sub SaveConfig()
                Dim sDevicePath As String = g_mClassCaptureLogic.m_DevicePath

                Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)
                        Dim mIniContent As New List(Of ClassIni.STRUC_INI_CONTENT)

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "FriendlyName", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.Label_FriendlyName.Text)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "DeviceExposure", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceExposure.Value)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "DeviceGain", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceGain.Value)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "DeviceGamma", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceGamma.Value)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "DeviceContrast", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast.Value)))

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "TrackerId", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.ComboBox_DeviceTrackerId.SelectedIndex)))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "FlipImageHorizontal", If(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.CheckBox_FlipHorizontal.Checked, "True", "False")))
                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "ImageInterpolation", CStr(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.ComboBox_ImageInterpolation.SelectedIndex)))

                        mIniContent.Add(New ClassIni.STRUC_INI_CONTENT(sDevicePath, "Autostart", If(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.CheckBox_Autostart.Checked, "True", "False")))

                        mIni.WriteKeyValue(mIniContent.ToArray)
                    End Using
                End Using
            End Sub

            Public Sub LoadConfig()
                Dim sDevicePath As String = g_mClassCaptureLogic.m_DevicePath

                Using mStream As New IO.FileStream(g_sConfigPath, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                    Using mIni As New ClassIni(mStream)
                        SetTrackBarClamp(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceExposure, CInt(mIni.ReadKeyValue(sDevicePath, "DeviceExposure", "0")))
                        SetTrackBarClamp(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceGain, CInt(mIni.ReadKeyValue(sDevicePath, "DeviceGain", "0")))
                        SetTrackBarClamp(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceGamma, CInt(mIni.ReadKeyValue(sDevicePath, "DeviceGamma", "0")))
                        SetTrackBarClamp(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.TrackBar_DeviceConstrast, CInt(mIni.ReadKeyValue(sDevicePath, "DeviceContrast", "0")))

                        SetComboBoxClamp(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.ComboBox_DeviceTrackerId, CInt(mIni.ReadKeyValue(sDevicePath, "TrackerId", "0")))
                        g_mClassCaptureLogic.g_mUCVirtualTrackerItem.CheckBox_FlipHorizontal.Checked = (mIni.ReadKeyValue(sDevicePath, "FlipImageHorizontal", "False") = "True")
                        SetComboBoxClamp(g_mClassCaptureLogic.g_mUCVirtualTrackerItem.ComboBox_ImageInterpolation, CInt(mIni.ReadKeyValue(sDevicePath, "ImageInterpolation", CStr(ClassCaptureLogic.ENUM_INTERPOLATION.BILINEAR))))

                        g_mClassCaptureLogic.g_mUCVirtualTrackerItem.CheckBox_Autostart.Checked = (mIni.ReadKeyValue(sDevicePath, "Autostart", "False") = "True")
                    End Using
                End Using
            End Sub

            Private Sub SetTrackBarClamp(mControl As TrackBar, iValue As Integer)
                mControl.Value = Math.Max(mControl.Minimum, Math.Min(mControl.Maximum, iValue))
            End Sub

            Private Sub SetComboBoxClamp(mControl As ComboBox, iIndex As Integer)
                If (mControl.Items.Count = 0) Then
                    Return
                End If

                mControl.SelectedIndex = Math.Max(0, Math.Min(mControl.Items.Count - 1, iIndex))
            End Sub
        End Class

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects). 
                    If (g_mInitThread IsNot Nothing AndAlso g_mInitThread.IsAlive) Then
                        g_mInitThread.Abort()
                        g_mInitThread.Join()
                        g_mInitThread = Nothing
                    End If

                    If (g_mCaptureThread IsNot Nothing AndAlso g_mCaptureThread.IsAlive) Then
                        g_mCaptureThread.Abort()
                        g_mCaptureThread.Join()
                        g_mCaptureThread = Nothing
                    End If

                    If (g_mPipeThread IsNot Nothing AndAlso g_mPipeThread.IsAlive) Then
                        g_mPipeThread.Abort()
                        g_mPipeThread.Join()
                        g_mPipeThread = Nothing
                    End If

                    If (g_mDeviceWatchdogThread IsNot Nothing AndAlso g_mDeviceWatchdogThread.IsAlive) Then
                        g_mDeviceWatchdogThread.Abort()
                        g_mDeviceWatchdogThread.Join()
                        g_mDeviceWatchdogThread = Nothing
                    End If

                    If (g_mCapture IsNot Nothing AndAlso Not g_mCapture.IsDisposed) Then
                        g_mCapture.Dispose()
                        g_mCapture = Nothing
                    End If

                    If (g_mCaptureFrame IsNot Nothing AndAlso Not g_mCaptureFrame.IsDisposed) Then
                        g_mCaptureFrame.Dispose()
                        g_mCaptureFrame = Nothing
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim mHelp As New FormVirtualTrackersHelp
        mHelp.ShowDialog(Me)
    End Sub
End Class
