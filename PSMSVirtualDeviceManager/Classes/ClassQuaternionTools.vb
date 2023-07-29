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

    Public Shared Function RotateVector(q As Quaternion, v As Vector3) As Vector3
        Return New Vector3(q.W * q.W * v.X + 2 * q.Y * q.W * v.Z - 2 * q.Z * q.W * v.Y + q.X * q.X * v.X + 2 * q.Y * q.X * v.Y + 2 * q.Z * q.X * v.Z - q.Z * q.Z * v.X - q.Y * q.Y * v.X,
                            2 * q.X * q.Y * v.X + q.Y * q.Y * v.Y + 2 * q.Z * q.Y * v.Z + 2 * q.W * q.Z * v.X - q.Z * q.Z * v.Y + q.W * q.W * v.Y - 2 * q.X * q.W * v.Z - q.X * q.X * v.Y,
                            2 * q.X * q.Z * v.X + 2 * q.Y * q.Z * v.Y + q.Z * q.Z * v.Z - 2 * q.W * q.Y * v.X - q.Y * q.Y * v.Z + 2 * q.W * q.X * v.Y - q.X * q.X * v.Z + q.W * q.W * v.Z)
    End Function

    Public Shared Function GetPositionInRotationSpace(mRotation As Quaternion, mPosition As Vector3) As Vector3
        Return RotateVector(Quaternion.Conjugate(mRotation), mPosition)
    End Function

    Public Shared Function FromVectorToVector(mFrom As Vector3, mTo As Vector3) As Quaternion
        Return LookRotation(mTo - mFrom, Vector3.UnitY)
    End Function

    Public Shared Function LookRotation(ByRef forward As Vector3, ByRef up As Vector3) As Quaternion
        forward = Vector3.Normalize(forward)
        Dim right As Vector3 = Vector3.Normalize(Vector3.Cross(up, forward))
        up = Vector3.Cross(forward, right)
        Dim m00 = right.X
        Dim m01 = right.Y
        Dim m02 = right.Z
        Dim m10 = up.X
        Dim m11 = up.Y
        Dim m12 = up.Z
        Dim m20 = forward.X
        Dim m21 = forward.Y
        Dim m22 = forward.Z


        Dim num8 As Single = m00 + m11 + m22
        Dim quaternion = New Quaternion()
        If num8 > 0F Then
            Dim num = CSng(Math.Sqrt(num8 + 1.0F))
            quaternion.W = num * 0.5F
            num = 0.5F / num
            quaternion.X = (m12 - m21) * num
            quaternion.Y = (m20 - m02) * num
            quaternion.Z = (m01 - m10) * num
            Return quaternion
        End If
        If m00 >= m11 AndAlso m00 >= m22 Then
            Dim num7 = CSng(Math.Sqrt(1.0F + m00 - m11 - m22))
            Dim num4 = 0.5F / num7
            quaternion.X = 0.5F * num7
            quaternion.Y = (m01 + m10) * num4
            quaternion.Z = (m02 + m20) * num4
            quaternion.W = (m12 - m21) * num4
            Return quaternion
        End If
        If m11 > m22 Then
            Dim num6 = CSng(Math.Sqrt(1.0F + m11 - m00 - m22))
            Dim num3 = 0.5F / num6
            quaternion.X = (m10 + m01) * num3
            quaternion.Y = 0.5F * num6
            quaternion.Z = (m21 + m12) * num3
            quaternion.W = (m20 - m02) * num3
            Return quaternion
        End If
        Dim num5 = CSng(Math.Sqrt(1.0F + m22 - m00 - m11))
        Dim num2 = 0.5F / num5
        quaternion.X = (m20 + m02) * num2
        quaternion.Y = (m21 + m12) * num2
        quaternion.Z = 0.5F * num5
        quaternion.W = (m01 - m10) * num2
        Return quaternion
    End Function

    Public Shared Function ExtractYawQuaternion(q As Quaternion, global_forward As Vector3) As Quaternion
        ' Extract the forward (z-axis) vector from the quaternion
        Dim forward As Vector3 = RotateVector(q, Vector3.UnitZ)

        ' Project the forward vector onto the xz-plane (horizontal plane) 
        Dim forward2d = Vector3.Normalize(New Vector3(forward.X, 0F, forward.Z))

        ' Compute the angle between the forward vector and the global forward vector
        Dim angle As Single = CSng(Math.Atan2(forward2d.X * global_forward.Z - forward2d.Z * global_forward.X,
                                              forward2d.X * global_forward.X + forward2d.Z * global_forward.Z))

        Return New Quaternion(0F, CSng(Math.Sin(angle / 2)), 0F, CSng(Math.Cos(angle / 2)))
    End Function
End Class
