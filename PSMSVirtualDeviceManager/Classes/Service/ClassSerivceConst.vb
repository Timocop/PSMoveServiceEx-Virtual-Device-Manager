﻿Imports System.Numerics

Public Class ClassSerivceConst
    Public Const PSMOVESERVICE_MAX_TRACKER_COUNT As Integer = 8
    Public Const PSMOVESERVICE_MAX_CONTROLLER_COUNT As Integer = 10
    Public Const PSMOVESERVICE_MAX_HMD_COUNT As Integer = 4

    Class ClassCameraPose
        Structure STRUC_CAMERA_POSE_ITEM
            Dim sName As String
            Dim mOrientation As Quaternion
            Dim mPosition As Vector3

            Public Sub New(_Name As String)
                Me.New(_Name, Quaternion.Identity, Vector3.Zero)
            End Sub

            Public Sub New(_Name As String, _Orientation As Quaternion, _Position As Vector3)
                sName = _Name
            End Sub

            Public Sub SaveToConfig(sFile As String)
                Dim mConfig As New ClassServiceConfig(sFile)
                mConfig.LoadConfig()

                mConfig.SetValue(Of String)("pose\orientation", "x", mOrientation.X.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("pose\orientation", "y", mOrientation.Y.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("pose\orientation", "z", mOrientation.Z.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("pose\orientation", "w", mOrientation.W.ToString(Globalization.CultureInfo.InvariantCulture))

                mConfig.SetValue(Of String)("pose\position", "x", mPosition.X.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("pose\position", "y", mPosition.Y.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("pose\position", "z", mPosition.Z.ToString(Globalization.CultureInfo.InvariantCulture))

                mConfig.SaveConfig()
            End Sub
            Public Sub LoadFromConfig(sFile As String)
                Dim mConfig As New ClassServiceConfig(sFile)
                mConfig.LoadConfig()

                mOrientation.X = Single.Parse(mConfig.GetValue(Of String)("pose\orientation", "x", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                mOrientation.Y = Single.Parse(mConfig.GetValue(Of String)("pose\orientation", "y", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                mOrientation.Z = Single.Parse(mConfig.GetValue(Of String)("pose\orientation", "z", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                mOrientation.W = Single.Parse(mConfig.GetValue(Of String)("pose\orientation", "w", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)

                mPosition.X = Single.Parse(mConfig.GetValue(Of String)("pose\position", "x", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                mPosition.Y = Single.Parse(mConfig.GetValue(Of String)("pose\position", "y", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                mPosition.Z = Single.Parse(mConfig.GetValue(Of String)("pose\position", "z", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
            End Sub
        End Structure
    End Class

    Class ClassCameraDistortion
        Enum ENUM_CAMERA_DISTORTION_TYPE
            PSEYE
            PS4CAM
            GENERIC
        End Enum

        Public Shared ReadOnly PSMOVESERVICE_KNOWN_DISTORTION As STRUC_CAMERA_DISTORTION_ITEM() = {
            New STRUC_CAMERA_DISTORTION_ITEM("PlayStation Eye", ENUM_CAMERA_DISTORTION_TYPE.PSEYE, 554.2563, 554.2563, 320, 240, -0.10771770030260086, 0.1213262677192688, 0.04875476285815239, 0.000917330733500421, 0.00010589254816295579), 'Error: Unknown
            New STRUC_CAMERA_DISTORTION_ITEM("PlayStation 4 Stereo Camera", ENUM_CAMERA_DISTORTION_TYPE.PS4CAM, 424.84967041015625, 515.53033447265625, 308.64697265625, 248.89617919921875, -0.021186288446187973, 0.049334883689880371, -0.062413521111011505, -0.00082370272139087319, 0.0016732711810618639) 'Error: 0.152170
        }

        Structure STRUC_CAMERA_DISTORTION_ITEM
            Dim sName As String
            Dim iType As ENUM_CAMERA_DISTORTION_TYPE
            Dim iFocalLengthX As Double
            Dim iFocalLengthY As Double
            Dim iPrincipalX As Double
            Dim iPrincipalY As Double
            Dim iDistortionK1 As Double
            Dim iDistortionK2 As Double
            Dim iDistortionK3 As Double
            Dim iDistortionP1 As Double
            Dim iDistortionP2 As Double

            Public Sub New(_Name As String, _Type As ENUM_CAMERA_DISTORTION_TYPE, _FocalLengthX As Double, _FocalLengthY As Double, _PrincipalX As Double, _PrincipalY As Double, _DistortionK1 As Double, _DistortionK2 As Double, _DistortionK3 As Double, _DistortionP1 As Double, _DistortionP2 As Double)
                sName = _Name
                iType = _Type
                iFocalLengthX = _FocalLengthX
                iFocalLengthY = _FocalLengthY
                iPrincipalX = _PrincipalX
                iPrincipalY = _PrincipalY
                iDistortionK1 = _DistortionK1
                iDistortionK2 = _DistortionK2
                iDistortionK3 = _DistortionK3
                iDistortionP1 = _DistortionP1
                iDistortionP2 = _DistortionP2
            End Sub

            Public Sub SaveToConfig(sFile As String)
                Dim mConfig As New ClassServiceConfig(sFile)
                mConfig.LoadConfig()

                mConfig.SetValue(Of String)("", "focalLengthX", iFocalLengthX.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("", "focalLengthY", iFocalLengthY.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("", "principalX", iPrincipalX.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("", "principalY", iPrincipalY.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("", "distortionK1", iDistortionK1.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("", "distortionK2", iDistortionK2.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("", "distortionK3", iDistortionK3.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("", "distortionP1", iDistortionP1.ToString(Globalization.CultureInfo.InvariantCulture))
                mConfig.SetValue(Of String)("", "distortionP2", iDistortionP2.ToString(Globalization.CultureInfo.InvariantCulture))

                mConfig.SaveConfig()
            End Sub
            Public Sub LoadFromConfig(sFile As String)
                Dim mConfig As New ClassServiceConfig(sFile)
                mConfig.LoadConfig()

                iFocalLengthX = Double.Parse(mConfig.GetValue(Of String)("", "focalLengthX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                iFocalLengthY = Double.Parse(mConfig.GetValue(Of String)("", "focalLengthY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                iPrincipalX = Double.Parse(mConfig.GetValue(Of String)("", "principalX", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                iPrincipalY = Double.Parse(mConfig.GetValue(Of String)("", "principalY", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                iDistortionK1 = Double.Parse(mConfig.GetValue(Of String)("", "distortionK1", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                iDistortionK2 = Double.Parse(mConfig.GetValue(Of String)("", "distortionK2", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                iDistortionK3 = Double.Parse(mConfig.GetValue(Of String)("", "distortionK3", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                iDistortionP1 = Double.Parse(mConfig.GetValue(Of String)("", "distortionP1", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
                iDistortionP2 = Double.Parse(mConfig.GetValue(Of String)("", "distortionP2", "0.0"), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture)
            End Sub
        End Structure

        Public Shared Function GetKnownDistortionByType(i As ENUM_CAMERA_DISTORTION_TYPE, ByRef mReturnDistortion As STRUC_CAMERA_DISTORTION_ITEM) As Boolean
            For Each mDistortion In PSMOVESERVICE_KNOWN_DISTORTION
                If (mDistortion.iType = i) Then
                    mReturnDistortion = mDistortion
                    Return True
                End If
            Next

            Return False
        End Function
    End Class
End Class
