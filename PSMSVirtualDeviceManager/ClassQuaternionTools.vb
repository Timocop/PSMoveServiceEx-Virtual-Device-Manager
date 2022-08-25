Imports System.Numerics

Public Class ClassQuaternionTools
    Public Shared Function ToQ(v As Vector3) As Quaternion
        Return ToQ(v.Y, v.X, v.Z)
    End Function

    Public Shared Function ToQ(yaw As Single, pitch As Single, roll As Single) As Quaternion
        yaw *= CSng(Math.PI / 180)
        pitch *= CSng(Math.PI / 180)
        roll *= CSng(Math.PI / 180)
        Dim rollOver2 = roll * 0.5F
        Dim sinRollOver2 As Single = CSng(Math.Sin(rollOver2))
        Dim cosRollOver2 As Single = CSng(Math.Cos(rollOver2))
        Dim pitchOver2 = pitch * 0.5F
        Dim sinPitchOver2 As Single = CSng(Math.Sin(pitchOver2))
        Dim cosPitchOver2 As Single = CSng(Math.Cos(pitchOver2))
        Dim yawOver2 = yaw * 0.5F
        Dim sinYawOver2 As Single = CSng(Math.Sin(yawOver2))
        Dim cosYawOver2 As Single = CSng(Math.Cos(yawOver2))
        Dim result As Quaternion
        result.W = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2
        result.X = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2
        result.Y = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2
        result.Z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2
        Return result
    End Function

    Public Shared Function FromQ2(q1 As Quaternion) As Vector3
        Dim sqw As Single = q1.W * q1.W
        Dim sqx As Single = q1.X * q1.X
        Dim sqy As Single = q1.Y * q1.Y
        Dim sqz As Single = q1.Z * q1.Z
        Dim unit = sqx + sqy + sqz + sqw ' if normalised is one, otherwise is correction factor
        Dim test As Single = q1.X * q1.W - q1.Y * q1.Z
        Dim v As Vector3

        If test > 0.4995F * unit Then ' singularity at north pole
            v.Y = CSng(2.0F * Math.Atan2(q1.Y, q1.X))
            v.X = Math.PI / 2
            v.Z = 0
            Return NormalizeAngles(v * (180 / Math.PI))
        End If

        If test < -0.4995F * unit Then ' singularity at south pole
            v.Y = CSng(-2.0F * Math.Atan2(q1.Y, q1.X))
            v.X = -Math.PI / 2
            v.Z = 0
            Return NormalizeAngles(v * (180 / Math.PI))
        End If

        Dim q As Quaternion = New Quaternion(q1.W, q1.Z, q1.X, q1.Y)
        v.Y = CSng(Math.Atan2(2.0F * q.X * q.W + 2.0F * q.Y * q.Z, 1 - 2.0F * (q.Z * q.Z + q.W * q.W)))     ' Yaw
        v.X = CSng(Math.Asin(2.0F * (q.X * q.Z - q.W * q.Y)))                                               ' Pitch
        v.Z = CSng(Math.Atan2(2.0F * q.X * q.Y + 2.0F * q.Z * q.W, 1 - 2.0F * (q.Y * q.Y + q.Z * q.Z)))     ' Roll
        Return NormalizeAngles(v * (180 / Math.PI))
    End Function

    Public Shared Function NormalizeAngles(angles As Vector3) As Vector3
        angles.X = NormalizeAngle(angles.X)
        angles.Y = NormalizeAngle(angles.Y)
        angles.Z = NormalizeAngle(angles.Z)
        Return angles
    End Function

    Public Shared Function NormalizeAngle(angle As Single) As Single
        While (angle > 360)
            angle -= 360
        End While

        While (angle < 0)
            angle += 360
        End While

        Return angle
    End Function
End Class
