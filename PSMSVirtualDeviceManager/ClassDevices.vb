Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes

Public Class ClassDevices
    Private Class FilterCategory     ' uuids.h  :  CLSID_* 
        ''' <summary> CLSID_VideoInputDeviceCategory, video capture category </summary>
        Public Shared ReadOnly VideoInputDevice As Guid = New Guid(&H860BB310UI, &H5D01, &H11D0, &HBD, &H3B, &H0, &HA0, &HC9, &H11, &HCE, &H86)
    End Class

    Private Class Clsid      ' uuids.h  :  CLSID_*
        ''' <summary> CLSID_SystemDeviceEnum for ICreateDevEnum </summary>
        Public Shared ReadOnly SystemDeviceEnum As Guid = New Guid(&H62BE5D10, &H60EB, &H11D0, &HBD, &H3B, &H0, &HA0, &HC9, &H11, &HCE, &H86)
    End Class


    Public Shared Function GetDevicesOfVideoInput(ByRef mDeviceList As List(Of ClassDeviceInfo)) As Boolean
        Return GetDevicesOfVideoInputInternal(FilterCategory.VideoInputDevice, mDeviceList)
    End Function

    Private Shared Function GetDevicesOfVideoInputInternal(mCategory As Guid, ByRef mDeviceList As List(Of ClassDeviceInfo)) As Boolean
        If (mDeviceList Is Nothing) Then
            mDeviceList = New List(Of ClassDeviceInfo)
        End If
        mDeviceList.Clear()

        Dim iResult As Integer
        Dim mDeviceCom As Object = Nothing
        Dim mDeviceEnum As ICreateDevEnum = Nothing
        Dim mEnumMoniker As IEnumMoniker = Nothing
        Dim mMoniker As IMoniker() = New IMoniker(0) {}

        Try
            Dim mDeviceEnumType = Type.GetTypeFromCLSID(Clsid.SystemDeviceEnum)
            If (mDeviceEnumType Is Nothing) Then
                Throw New NotImplementedException("System Device Enumerator")
            End If

            mDeviceCom = Activator.CreateInstance(mDeviceEnumType)
            mDeviceEnum = CType(mDeviceCom, ICreateDevEnum)
            iResult = mDeviceEnum.CreateClassEnumerator(mCategory, mEnumMoniker, 0)

            If (iResult <> 0) Then
                Throw New NotSupportedException("No devices of the category")
            End If

            Dim pFetch As IntPtr
            Dim iCount As Integer = 0

            While True
                iResult = mEnumMoniker.[Next](1, mMoniker, pFetch)
                If (iResult <> 0 OrElse mMoniker(0) Is Nothing) Then
                    Exit While
                End If

                Dim sDeviceName As String = GetProperty(mMoniker(0), "Description")
                If (sDeviceName Is Nothing) Then
                    sDeviceName = GetProperty(mMoniker(0), "FriendlyName")
                End If

                If (sDeviceName IsNot Nothing) Then
                    Dim sDeviceCLSID As String = GetProperty(mMoniker(0), "CLSID")

                    Dim sDevicePath As String = GetProperty(mMoniker(0), "DevicePath")
                    If (sDevicePath IsNot Nothing) Then
                        mDeviceList.Add(
                            New ClassDeviceInfo(
                                iCount,
                                sDeviceName,
                                sDevicePath,
                                sDeviceCLSID
                            )
                        )

                        iCount += 1
                    End If
                End If

                If (mMoniker(0) IsNot Nothing) Then
                    Marshal.ReleaseComObject(mMoniker(0))
                    mMoniker(0) = Nothing
                End If
            End While

            Return iCount > 0
        Catch ex As Exception
            Return False
        Finally
            mDeviceEnum = Nothing

            If (mMoniker(0) IsNot Nothing) Then
                Marshal.ReleaseComObject(mMoniker(0))
                mMoniker(0) = Nothing
            End If

            If (mEnumMoniker IsNot Nothing) Then
                Marshal.ReleaseComObject(mEnumMoniker)
                mEnumMoniker = Nothing
            End If

            If (mDeviceCom IsNot Nothing) Then
                Marshal.ReleaseComObject(mDeviceCom)
                mDeviceCom = Nothing
            End If
        End Try
    End Function

    Private Shared Function GetProperty(mMon As IMoniker, sProperty As String) As String
        Dim mBagCom As Object = Nothing
        Dim mBagProp As IPropertyBag = Nothing

        Try
            Dim mBagGUID As Guid = GetType(IPropertyBag).GUID
            mMon.BindToStorage(Nothing, Nothing, mBagGUID, mBagCom)
            mBagProp = CType(mBagCom, IPropertyBag)

            Dim mValue As Object = Nothing
            Dim iResult As Integer = mBagProp.Read(sProperty, mValue, IntPtr.Zero)
            If (iResult <> 0) Then
                Marshal.ThrowExceptionForHR(iResult)
            End If

            Dim sRet As String = TryCast(mValue, String)
            If (sRet Is Nothing OrElse sRet.Length < 1) Then
                Throw New NotImplementedException("Device FriendlyName")
            End If

            Return sRet
        Catch ex As Exception
            Return Nothing
        Finally
            mBagProp = Nothing

            If (mBagCom IsNot Nothing) Then
                Marshal.ReleaseComObject(mBagCom)
                mBagCom = Nothing
            End If
        End Try
    End Function

    Public Class ClassDeviceInfo
        Public m_Index As Integer
        Public m_Name As String
        Public m_CLSID As String
        Public m_Path As String

        Public Sub New(_Index As Integer, _Name As String, _Path As String, _CLSID As String)
            m_Index = _Index
            m_Name = _Name
            m_Path = _Path
            m_CLSID = _CLSID
        End Sub

        Public Function GetIndexByPath() As Integer
            Dim mDeviceList As New List(Of ClassDeviceInfo)
            If (ClassDevices.GetDevicesOfVideoInput(mDeviceList)) Then
                For i = 0 To mDeviceList.Count - 1
                    If (mDeviceList(i).m_Path <> m_Path) Then
                        Continue For
                    End If

                    Return mDeviceList(i).m_Index
                Next
            End If

            Return -1
        End Function
    End Class

    <ComVisible(True), ComImport, Guid("29840822-5B84-11D0-BD3B-00A0C911CE86"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface ICreateDevEnum
        <PreserveSig>
        Function CreateClassEnumerator(
            <[In]> ByRef pType As Guid,
            <Out> ByRef ppEnumMoniker As IEnumMoniker,
            <[In]> ByVal dwFlags As Integer
        ) As Integer
    End Interface

    <ComVisible(True), ComImport, Guid("55272A00-42CB-11CE-8135-00AA004BB851"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IPropertyBag
        <PreserveSig>
        Function Read(
            <[In], MarshalAs(UnmanagedType.LPWStr)> ByVal pszPropName As String,
            <[In], Out, MarshalAs(UnmanagedType.Struct)> ByRef pVar As Object, ByVal pErrorLog As IntPtr
        ) As Integer

        <PreserveSig>
        Function Write(
            <[In], MarshalAs(UnmanagedType.LPWStr)> ByVal pszPropName As String,
            <[In], MarshalAs(UnmanagedType.Struct)> ByRef pVar As Object
        ) As Integer
    End Interface
End Class
