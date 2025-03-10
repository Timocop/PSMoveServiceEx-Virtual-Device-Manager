﻿Public Class FormDisplayDistortMonitor
    Private g_mUpdateThread As Threading.Thread

    Public g_iDistortFov As Single = 0.0F
    Public g_iDistortK0 As Single = 0.0F
    Public g_iDistortK1 As Single = 0.0F
    Public g_iDistortScale As Single = 1.0F
    Public g_iDistortRScale As Single = 0.0F
    Public g_iDistortGScale As Single = 0.0F
    Public g_iDistortBScale As Single = 0.0F

    Public g_iPatternSize As Integer = 64

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 

        CreateControl()
        CreateHandle()

        g_mUpdateThread = New Threading.Thread(AddressOf UpdaterThread)
        g_mUpdateThread.Priority = Threading.ThreadPriority.Lowest
        g_mUpdateThread.IsBackground = True
        g_mUpdateThread.Start()
    End Sub

    Private Sub UpdaterThread()
        Dim mEyeBitmap(2) As Bitmap
        Dim iFrameCount As Integer = 0

        Try
            While True
                Try
                    Dim mMonitor As New ClassMonitor
                    Dim mMonitorInfo As ClassMonitor.DEVMODE = Nothing
                    If (mMonitor.FindPlaystationVrMonitor(mMonitorInfo, Nothing) <> ClassMonitor.ENUM_PSVR_MONITOR_STATUS.SUCCESS) Then
                        ClassUtils.AsyncInvoke(Sub()
                                                   If (Me.Visible) Then
                                                       Me.Visible = False
                                                   End If
                                               End Sub)

                        Exit Try
                    End If

                    iFrameCount += 1

                    Dim iWindowX As Integer = mMonitorInfo.dmPositionX
                    Dim iWindowY As Integer = mMonitorInfo.dmPositionY
                    Dim iWindowW As Integer = mMonitorInfo.dmPelsWidth
                    Dim iWindowH As Integer = mMonitorInfo.dmPelsHeight

                    Dim iDistortFov As Single = ClassUtils.SyncInvokeEx(Of Single)(Function() g_iDistortFov)
                    Dim iDistortK0 As Single = ClassUtils.SyncInvokeEx(Of Single)(Function() g_iDistortK0)
                    Dim iDistortK1 As Single = ClassUtils.SyncInvokeEx(Of Single)(Function() g_iDistortK1)
                    Dim iDistortScale As Single = ClassUtils.SyncInvokeEx(Of Single)(Function() g_iDistortScale)
                    Dim iDistortRScale As Single = ClassUtils.SyncInvokeEx(Of Single)(Function() g_iDistortRScale)
                    Dim iDistortGScale As Single = ClassUtils.SyncInvokeEx(Of Single)(Function() g_iDistortGScale)
                    Dim iDistortBScale As Single = ClassUtils.SyncInvokeEx(Of Single)(Function() g_iDistortBScale)
                    Dim iPatternSize As Integer = ClassUtils.SyncInvokeEx(Of Integer)(Function() g_iPatternSize)

                    ClassUtils.AsyncInvoke(Sub()
                                               Me.Location = New Point(iWindowX, iWindowY)
                                               Me.Size = New Size(iWindowW, iWindowH)

                                               If (Not Me.Visible) Then
                                                   Me.Visible = True
                                               End If
                                           End Sub)

                    Const EYE_LEFT = 0
                    Const EYE_RIGHT = 1

                    Dim iRenderW As Integer = ClassUtils.SyncInvokeEx(Of Integer)(Function() Me.Width)
                    Dim iRenderH As Integer = ClassUtils.SyncInvokeEx(Of Integer)(Function() Me.Height)

                    Dim mPatternThreads As New List(Of Threading.Thread)
                    Try
                        For i = 0 To mEyeBitmap.Length - 1
                            Dim _i = i

                            If (mEyeBitmap(_i) IsNot Nothing) Then
                                mEyeBitmap(_i).Dispose()
                                mEyeBitmap(_i) = Nothing
                            End If

                            Dim mPatternThread As New Threading.Thread(
                                Sub()
                                    Using mPattern = CreateCheckerboard(iRenderH, CInt(iRenderW / 2), iPatternSize)
                                        Using mFov = ApplyFOV(mPattern, iDistortFov)
                                            Dim mRgbChannels = SeparateRGBChannels(mFov)

                                            Try
                                                Dim mDistortThreads As New List(Of Threading.Thread)

                                                Try
                                                    For j = 0 To mRgbChannels.Length - 1
                                                        Dim _j = j
                                                        Dim mDistortThread As New Threading.Thread(
                                                            Sub()
                                                                Try
                                                                    Select Case (_j)
                                                                        Case 0
                                                                            Dim tmp = ApplyDistortionToBitmap(mRgbChannels(_j), iDistortK0, iDistortK1, iDistortScale + iDistortRScale)
                                                                            mRgbChannels(_j).Dispose()
                                                                            mRgbChannels(_j) = tmp
                                                                        Case 1
                                                                            Dim tmp = ApplyDistortionToBitmap(mRgbChannels(_j), iDistortK0, iDistortK1, iDistortScale + iDistortGScale)
                                                                            mRgbChannels(_j).Dispose()
                                                                            mRgbChannels(_j) = tmp
                                                                        Case 2
                                                                            Dim tmp = ApplyDistortionToBitmap(mRgbChannels(_j), iDistortK0, iDistortK1, iDistortScale + iDistortBScale)
                                                                            mRgbChannels(_j).Dispose()
                                                                            mRgbChannels(_j) = tmp
                                                                    End Select
                                                                Catch ex As Threading.ThreadAbortException
                                                                    Throw
                                                                Catch ex As Exception
                                                                    ClassAdvancedExceptionLogging.WriteToLog(ex)
                                                                End Try
                                                            End Sub)

                                                        mDistortThread.Priority = Threading.ThreadPriority.Lowest
                                                        mDistortThread.IsBackground = True
                                                        mDistortThread.Start()
                                                        mDistortThreads.Add(mDistortThread)
                                                    Next
                                                Catch ex As Threading.ThreadAbortException
                                                    For Each mThread As Threading.Thread In mPatternThreads
                                                        mThread.Abort()
                                                    Next

                                                    Throw
                                                Catch ex As Exception
                                                    ClassAdvancedExceptionLogging.WriteToLog(ex)
                                                Finally
                                                    For Each mThread As Threading.Thread In mDistortThreads
                                                        mThread.Join()
                                                    Next
                                                End Try

                                                mEyeBitmap(_i) = CombineRGBBitmaps(mRgbChannels)
                                            Finally
                                                For j = 0 To mRgbChannels.Length - 1
                                                    mRgbChannels(j).Dispose()
                                                Next
                                            End Try
                                        End Using
                                    End Using

                                    Using g As Graphics = Graphics.FromImage(mEyeBitmap(_i))
                                        ' Define the font and brush for drawing the number
                                        ' Define the font and brush for drawing the number
                                        Dim font As New Font("Arial", 24)
                                        Dim brush As New SolidBrush(Color.Red)

                                        ' Calculate the position to center the text
                                        Dim text As String = iFrameCount.ToString()
                                        Dim textSize As SizeF = g.MeasureString(text, font)
                                        Dim x As Single = (mEyeBitmap(_i).Width - textSize.Width) / 2
                                        Dim y As Single = (mEyeBitmap(_i).Height - textSize.Height) / 2

                                        ' Draw the number on the Bitmap
                                        g.DrawString(text, font, brush, x, y)
                                    End Using
                                End Sub)
                            mPatternThread.Priority = Threading.ThreadPriority.Lowest
                            mPatternThread.IsBackground = True
                            mPatternThread.Start()
                            mPatternThreads.Add(mPatternThread)
                        Next
                    Catch ex As Threading.ThreadAbortException
                        For Each mThread As Threading.Thread In mPatternThreads
                            mThread.Abort()
                        Next

                        Throw
                    Catch ex As Exception
                        ClassAdvancedExceptionLogging.WriteToLog(ex)
                    Finally
                        For Each mThread As Threading.Thread In mPatternThreads
                            mThread.Join()
                        Next
                    End Try

                    ClassUtils.AsyncInvoke(Sub()
                                               PictureBox_EyeL.Image = mEyeBitmap(EYE_LEFT)
                                               PictureBox_EyeR.Image = mEyeBitmap(EYE_RIGHT)
                                           End Sub)

                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As Exception
                    ClassAdvancedExceptionLogging.WriteToLog(ex)
                End Try

                Threading.Thread.Sleep(100)
            End While
        Finally
            For i = 0 To mEyeBitmap.Length - 1
                If (mEyeBitmap(i) IsNot Nothing) Then
                    mEyeBitmap(i).Dispose()
                    mEyeBitmap(i) = Nothing
                End If
            Next
        End Try
    End Sub

    Private Function CreateCheckerboard(width As Integer, height As Integer, squareSize As Integer) As Bitmap
        Dim checkerboard As Bitmap = New Bitmap(width, height)

        Using graphics As Graphics = Graphics.FromImage(checkerboard)
            Dim whiteBrush As Brush = Brushes.White
            Dim blackBrush As Brush = Brushes.Black

            Dim numRows As Integer = CInt(height / squareSize)
            Dim numCols As Integer = CInt(width / squareSize)

            For row = 0 To numRows - 1
                For col = 0 To numCols - 1
                    Dim currentBrush As Brush = If((row + col) Mod 2 = 0, whiteBrush, blackBrush)
                    graphics.FillRectangle(currentBrush, col * squareSize, row * squareSize, squareSize, squareSize)
                Next
            Next
        End Using

        Return checkerboard
    End Function

    Private Function ApplyFOV(inputImage As Bitmap, fovAngle As Single) As Bitmap
        Dim width As Integer = inputImage.Width
        Dim height As Integer = inputImage.Height
        Dim fovImage As Bitmap = New Bitmap(width, height)

        Dim centerX = width / 2.0
        Dim centerY = height / 2.0
        Dim fovRadians = fovAngle * Math.PI / 180.0

        For y = 0 To height - 1
            For x = 0 To width - 1
                ' Calculate polar coordinates relative to the image center
                Dim dx = x - centerX
                Dim dy = y - centerY
                Dim radius = Math.Sqrt(dx * dx + dy * dy)
                Dim theta = Math.Atan2(dy, dx)

                ' Apply FOV distortion to polar coordinates
                Dim newRadius = radius / Math.Cos(fovRadians / 2.0)

                ' Map distorted polar coordinates back to Cartesian coordinates
                Dim newX = centerX + newRadius * Math.Cos(theta)
                Dim newY = centerY + newRadius * Math.Sin(theta)

                ' Ensure that the new coordinates are within bounds
                newX = ClassMathUtils.ClampValue(newX, 0, width - 1)
                newY = ClassMathUtils.ClampValue(newY, 0, height - 1)

                ' Get the color from the original image
                Dim pixelColor As Color = inputImage.GetPixel(CInt(newX), CInt(newY))

                ' Set the color in the FOV-distorted image
                fovImage.SetPixel(x, y, pixelColor)
            Next
        Next

        Return fovImage
    End Function

    Private Function SeparateRGBChannels(inputBitmap As Bitmap) As Bitmap()
        Dim redChannelBitmap As New Bitmap(inputBitmap)
        Dim greenChannelBitmap As New Bitmap(inputBitmap)
        Dim blueChannelBitmap As New Bitmap(inputBitmap)

        For y As Integer = 0 To inputBitmap.Height - 1
            For x As Integer = 0 To inputBitmap.Width - 1
                Dim pixelColor As Color = inputBitmap.GetPixel(x, y)
                redChannelBitmap.SetPixel(x, y, Color.FromArgb(pixelColor.R, 0, 0))
                greenChannelBitmap.SetPixel(x, y, Color.FromArgb(0, pixelColor.G, 0))
                blueChannelBitmap.SetPixel(x, y, Color.FromArgb(0, 0, pixelColor.B))
            Next
        Next

        Return {redChannelBitmap, greenChannelBitmap, blueChannelBitmap}
    End Function

    Private Function ApplyDistortionToBitmap(inputImage As Bitmap, k0 As Single, k1 As Single, scale As Single) As Bitmap
        Dim width As Integer = inputImage.Width
        Dim height As Integer = inputImage.Height
        Dim distortedImage As New Bitmap(width, height)

        Dim centerX As Double = width / 2.0
        Dim centerY As Double = height / 2.0

        For y As Integer = 0 To height - 1
            For x As Integer = 0 To width - 1
                ' Normalize coordinates to the range [0, 1]
                Dim u As Double = x / CDbl(width)
                Dim v As Double = y / CDbl(height)

                ' Apply the distortion formula
                Dim fU As Single = CSng(u)
                Dim fV As Single = CSng(v)

                Dim r2 As Single = (fU - 0.5F) * (fU - 0.5F) + (fV - 0.5F) * (fV - 0.5F)
                Dim r4 As Single = r2 * r2
                Dim dist As Single = (1.0F + k0 * r2 + k1 * r4)
                fU = (((fU * 2.0F - 1.0F) * dist) * scale + 1.0F) * 0.5F
                fV = (((fV * 2.0F - 1.0F) * dist) * scale + 1.0F) * 0.5F

                ' Map distorted coordinates back to image space
                Dim xDistorted As Integer = CInt(fU * width)
                Dim yDistorted As Integer = CInt(fV * height)

                ' Ensure that the distorted coordinates are within bounds
                xDistorted = ClassMathUtils.ClampValue(xDistorted, 0, width - 1)
                yDistorted = ClassMathUtils.ClampValue(yDistorted, 0, height - 1)

                ' Get the color from the original image
                Dim pixelColor As Color = inputImage.GetPixel(xDistorted, yDistorted)

                ' Set the color in the distorted image
                distortedImage.SetPixel(x, y, pixelColor)
            Next
        Next

        Return distortedImage
    End Function

    Private Function CombineRGBBitmaps(rgbChannels As Bitmap()) As Bitmap
        Dim width As Integer = rgbChannels(0).Width
        Dim height As Integer = rgbChannels(0).Height
        Dim combinedBitmap As New Bitmap(width, height)

        For y As Integer = 0 To height - 1
            For x As Integer = 0 To width - 1
                Dim redValue As Integer = rgbChannels(0).GetPixel(x, y).R
                Dim greenValue As Integer = rgbChannels(1).GetPixel(x, y).G
                Dim blueValue As Integer = rgbChannels(2).GetPixel(x, y).B

                combinedBitmap.SetPixel(x, y, Color.FromArgb(redValue, greenValue, blueValue))
            Next
        Next

        Return combinedBitmap
    End Function

    Private Sub FormDisplayDistortMonitor_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CleanUp()
    End Sub

    Private Sub CleanUp()
        If (g_mUpdateThread IsNot Nothing AndAlso g_mUpdateThread.IsAlive) Then
            g_mUpdateThread.Abort()
            g_mUpdateThread.Join()
            g_mUpdateThread = Nothing
        End If
    End Sub
End Class