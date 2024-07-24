Imports System.Numerics
Imports PSMSVirtualDeviceManager
Imports PSMSVirtualDeviceManager.FormTroubleshootLogs

Public Class ClassLogManageOscDevices
    Implements ILogAction

    Enum ENUM_DEVICE_TYPE
        INVALID = 0
        CONTROLLER
        HMD
        TRACKER
    End Enum

    Structure STRUC_DEVICE_ITEM
        Dim iType As ENUM_DEVICE_TYPE
        Dim iId As Integer
        Dim sSerial As String

        Dim bPositionValid As Boolean
        Dim mPosition As Vector3

        Dim bOrientationValid As Boolean
        Dim mOrientation As Vector3
    End Structure

    Private g_mFormMain As FormMain

    Public Sub New(_FormMain As FormMain)
        g_mFormMain = _FormMain
    End Sub

    Public Sub Generate(mData As Dictionary(Of String, String)) Implements ILogAction.Generate
        If (g_mFormMain.g_mUCVirtualMotionTracker Is Nothing OrElse g_mFormMain.g_mUCVirtualMotionTracker.g_ClassOscDevices Is Nothing) Then
            Return
        End If

        Dim sTrackersList As New Text.StringBuilder

        Dim mVmtTrackers = g_mFormMain.g_mUCVirtualMotionTracker.g_ClassOscDevices.GetDevices
        For Each mItem In mVmtTrackers
            Dim mPos As Vector3 = mItem.mPos
            Dim mAng As Vector3 = mItem.GetOrientationEuler()

            sTrackersList.AppendFormat("[Device_{0}]", mItem.iIndex).AppendLine()
            sTrackersList.AppendFormat("ID={0}", mItem.iIndex).AppendLine()
            sTrackersList.AppendFormat("Type={0}", mItem.iType).AppendLine()
            sTrackersList.AppendFormat("Serial={0}", mItem.sSerial).AppendLine()
            sTrackersList.AppendFormat("Position={0}", String.Format("{0}, {1}, {2}",
                                                                            mPos.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mPos.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mPos.Z.ToString(Globalization.CultureInfo.InvariantCulture)
                                                                        )).AppendLine()
            sTrackersList.AppendFormat("Orientation={0}", String.Format("{0}, {1}, {2}",
                                                                            mAng.X.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mAng.Y.ToString(Globalization.CultureInfo.InvariantCulture),
                                                                            mAng.Z.ToString(Globalization.CultureInfo.InvariantCulture)
                                                                        )).AppendLine()

            sTrackersList.AppendLine()
        Next

        mData(GetActionTitle()) = sTrackersList.ToString
    End Sub

    Public Function GetActionTitle() As String Implements ILogAction.GetActionTitle
        Return SECTION_VDM_OSC_DEVICES
    End Function

    Public Function GetIssues(mData As Dictionary(Of String, String)) As STRUC_LOG_ISSUE() Implements ILogAction.GetIssues
        Throw New NotImplementedException()
    End Function

    Public Function GetSectionContent(mData As Dictionary(Of String, String)) As String Implements ILogAction.GetSectionContent
        If (Not mData.ContainsKey(GetActionTitle())) Then
            Return Nothing
        End If

        Return mData(GetActionTitle())
    End Function

    Public Function GetDevices(mData As Dictionary(Of String, String)) As STRUC_DEVICE_ITEM()
        Dim sContent As String = GetSectionContent(mData)
        If (sContent Is Nothing) Then
            Return {}
        End If

        Dim mDeviceList As New List(Of STRUC_DEVICE_ITEM)
        Dim mDevoceProp As New Dictionary(Of String, String)

        Dim sLines As String() = sContent.Split(New String() {vbNewLine, vbLf}, 0)
        For i = sLines.Length - 1 To 0 Step -1
            Dim sLine As String = sLines(i).Trim

            If (sLine.StartsWith("[") AndAlso sLine.EndsWith("]"c)) Then
                Dim sDeviceKey As String = sLine.Substring(1, sLine.Length - 2)

                Dim mNewDevice As New STRUC_DEVICE_ITEM

                ' Optional
                If (mDevoceProp.ContainsKey("Position") AndAlso mDevoceProp("Position").Split(","c).Count = 3) Then
                    Dim mPos = mDevoceProp("Position").Split(","c)
                    Dim iPos(3) As Single

                    If (Single.TryParse(mPos(0), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iPos(0)) AndAlso
                            Single.TryParse(mPos(1), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iPos(1)) AndAlso
                            Single.TryParse(mPos(2), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iPos(2))) Then

                        mNewDevice.mPosition = New Vector3(iPos(0), iPos(1), iPos(2))
                        mNewDevice.bPositionValid = True
                    End If
                End If

                If (mDevoceProp.ContainsKey("Orientation") AndAlso mDevoceProp("Orientation").Split(","c).Count = 3) Then
                    Dim mAng = mDevoceProp("Orientation").Split(","c)
                    Dim iAng(3) As Single

                    If (Single.TryParse(mAng(0), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iAng(0)) AndAlso
                            Single.TryParse(mAng(1), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iAng(1)) AndAlso
                            Single.TryParse(mAng(2), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, iAng(2))) Then

                        mNewDevice.mOrientation = New Vector3(iAng(0), iAng(1), iAng(2))
                        mNewDevice.bOrientationValid = True
                    End If
                End If

                ' Required
                While True
                    If (mDevoceProp.ContainsKey("Type")) Then
                        Select Case (mDevoceProp("Type"))
                            Case "CONTROLLER"
                                mNewDevice.iType = ENUM_DEVICE_TYPE.CONTROLLER
                            Case "HMD"
                                mNewDevice.iType = ENUM_DEVICE_TYPE.HMD
                            Case "TRACKER"
                                mNewDevice.iType = ENUM_DEVICE_TYPE.TRACKER
                            Case Else
                                Exit While
                        End Select
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("ID")) Then
                        mNewDevice.iId = CInt(mDevoceProp("ID"))
                    Else
                        Exit While
                    End If

                    If (mDevoceProp.ContainsKey("Serial")) Then
                        mNewDevice.sSerial = mDevoceProp("Serial")
                    Else
                        Exit While
                    End If

                    mDeviceList.Add(mNewDevice)
                    Exit While
                End While

                mDevoceProp.Clear()
            End If

            If (sLine.Contains("="c)) Then
                Dim sKey As String = sLine.Substring(0, sLine.IndexOf("="c))
                Dim sValue As String = sLine.Remove(0, sLine.IndexOf("="c) + 1)

                mDevoceProp(sKey) = sValue
            End If
        Next

        Return mDeviceList.ToArray
    End Function
End Class
