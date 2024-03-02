Imports System.Numerics

Public Class ClassQuaternionTools
    Public Shared Function ToQ(yaw As Single, pitch As Single, roll As Single) As Quaternion
        ToQ(New Vector3(pitch, yaw, roll))
    End Function

    Public Shared Function ToQ(v As Vector3) As Quaternion
        ' Convert Euler angles to quaternion
        Dim yaw As Single = v.Y * CSng(Math.PI / 180.0) ' Yaw (rotation around Y-axis) in radians
        Dim pitch As Single = v.X * CSng(Math.PI / 180.0) ' Pitch (rotation around X-axis) in radians
        Dim roll As Single = v.Z * CSng(Math.PI / 180.0) ' Roll (rotation around Z-axis) in radians

        Dim cy As Single = CSng(Math.Cos(yaw * 0.5))
        Dim sy As Single = CSng(Math.Sin(yaw * 0.5))
        Dim cp As Single = CSng(Math.Cos(pitch * 0.5))
        Dim sp As Single = CSng(Math.Sin(pitch * 0.5))
        Dim cr As Single = CSng(Math.Cos(roll * 0.5))
        Dim sr As Single = CSng(Math.Sin(roll * 0.5))

        Dim qw As Single = cy * cp * cr + sy * sp * sr
        Dim qx As Single = cy * cp * sr - sy * sp * cr
        Dim qy As Single = sy * cp * sr + cy * sp * cr
        Dim qz As Single = sy * cp * cr - cy * sp * sr

        Return New Quaternion(qx, qy, qz, qw)
    End Function

    Public Shared Function FromQ(q As Quaternion) As Vector3
        ' Convert quaternion to Euler angles
        Dim euler As New Vector3()

        ' Roll (X-axis rotation)
        Dim sinr As Double = 2.0 * (q.W * q.X + q.Y * q.Z)
        Dim cosr As Double = 1.0 - 2.0 * (q.X * q.X + q.Y * q.Y)
        euler.X = CSng(Math.Atan2(sinr, cosr))

        ' Pitch (Y-axis rotation)
        Dim sinp As Double = 2.0 * (q.W * q.Y - q.Z * q.X)
        If Math.Abs(sinp) >= 1 Then
            euler.Y = CSng(If(sinp < 0, -Math.PI / 2, Math.PI / 2)) ' Use -90 or 90 degrees if out of range
        Else
            euler.Y = CSng(Math.Asin(sinp))
        End If

        ' Yaw (Z-axis rotation)
        Dim siny As Double = 2.0 * (q.W * q.Z + q.X * q.Y)
        Dim cosy As Double = 1.0 - 2.0 * (q.Y * q.Y + q.Z * q.Z)
        euler.Z = CSng(Math.Atan2(siny, cosy))

        ' Convert Euler angles from radians to degrees
        euler.X *= CSng(180.0 / Math.PI)
        euler.Y *= CSng(180.0 / Math.PI)
        euler.Z *= CSng(180.0 / Math.PI)

        Return NormalizeAngles(euler)
    End Function

    Public Shared Function NormalizeAngles(angles As Vector3) As Vector3
        angles.X = NormalizeAngle(angles.X)
        angles.Y = NormalizeAngle(angles.Y)
        angles.Z = NormalizeAngle(angles.Z)
        Return angles
    End Function

    Public Shared Function NormalizeAngle(i As Single) As Single
        While (i > 360.0F)
            i -= 360.0F
        End While

        While (i < 0.0F)
            i += 360.0F
        End While

        Return i
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

    Public Shared Function CalculateAngleDegreesDifference(quaternion1 As Quaternion, quaternion2 As Quaternion) As Single
        ' Convert both quaternions to unit quaternions
        quaternion1 = Quaternion.Normalize(quaternion1)
        quaternion2 = Quaternion.Normalize(quaternion2)

        ' Calculate the dot product between the two quaternions
        Dim dotProduct As Single = Quaternion.Dot(quaternion1, quaternion2)

        ' Clamp the dot product to the range [-1, 1] to avoid numerical errors
        If (dotProduct < -1.0F) Then
            dotProduct = -1.0F
        End If
        If (dotProduct > 1.0F) Then
            dotProduct = 1.0F
        End If

        ' Calculate the angle between the two quaternions
        Dim angle As Single = CSng(Math.Acos(dotProduct))

        Return CSng(angle * (180.0F / Math.PI))
    End Function
End Class
