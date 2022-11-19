Public Class FormTrackerOverrideSetup
    Const MAX_VMT_TRACKER As Integer = 20

    Enum ENUM_OVERRIDE_TYPE
        HEAD
        LEFT_HAND
        RIGHT_HAND
    End Enum

    Structure STRUC_DIALOG_RESULT
        Public bCustomTracker As Boolean
        Public iVMTTracker As Integer
        Public sCustomTrackerName As String
        Public iOverrideType As ENUM_OVERRIDE_TYPE
    End Structure

    Structure STRUC_OVERRIDE_TYPE_ITEM
        Sub New(_OverrideType As ENUM_OVERRIDE_TYPE)
            iOverrideType = _OverrideType
        End Sub

        Public iOverrideType As ENUM_OVERRIDE_TYPE

        Public Overrides Function ToString() As String
            Select Case (iOverrideType)
                Case ENUM_OVERRIDE_TYPE.HEAD
                    Return "Head Mount Device"

                Case ENUM_OVERRIDE_TYPE.LEFT_HAND
                    Return "Left Controller"

                Case ENUM_OVERRIDE_TYPE.RIGHT_HAND
                    Return "Right Controller"

            End Select

            Return "Unknown"
        End Function
    End Structure

    Public Sub New()
        Me.New(New String() {})
    End Sub

    Public Sub New(sCustomTrackers As String())

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Add VMT ids
        ComboBox_VMTTracker.Items.Clear()
        For i = 0 To MAX_VMT_TRACKER
            ComboBox_VMTTracker.Items.Add(i)
        Next
        ComboBox_VMTTracker.SelectedIndex = 0

        ' Add custom trackers
        ComboBox_CustomTracker.Items.Clear()
        For i = 0 To sCustomTrackers.Length - 1
            ComboBox_CustomTracker.Items.Add(sCustomTrackers(i))
        Next
        ComboBox_CustomTracker.SelectedText = ""

        ' Add override types
        ComboBox_OverrideType.Items.Clear()
        ComboBox_OverrideType.Items.Add(New STRUC_OVERRIDE_TYPE_ITEM(ENUM_OVERRIDE_TYPE.HEAD))
        ComboBox_OverrideType.Items.Add(New STRUC_OVERRIDE_TYPE_ITEM(ENUM_OVERRIDE_TYPE.LEFT_HAND))
        ComboBox_OverrideType.Items.Add(New STRUC_OVERRIDE_TYPE_ITEM(ENUM_OVERRIDE_TYPE.RIGHT_HAND))
        ComboBox_OverrideType.SelectedIndex = 0

        UpdateRadioButtonSelection()
    End Sub

    ReadOnly Property m_DialogResult As STRUC_DIALOG_RESULT
        Get
            Dim mResult As New STRUC_DIALOG_RESULT
            Select Case (True)
                Case RadioButton_VMT.Checked
                    mResult.bCustomTracker = False

                    mResult.iVMTTracker = CInt(ComboBox_VMTTracker.SelectedItem)

                Case RadioButton_Custom.Checked
                    mResult.bCustomTracker = True

                    mResult.sCustomTrackerName = ComboBox_CustomTracker.SelectedText

            End Select

            mResult.iOverrideType = DirectCast(ComboBox_OverrideType.SelectedItem, STRUC_OVERRIDE_TYPE_ITEM).iOverrideType
            Return mResult
        End Get
    End Property

    Private Sub Button_Add_Click(sender As Object, e As EventArgs) Handles Button_Add.Click
        If (m_DialogResult.bCustomTracker) Then
            If (Not CheckCustomTrackerValid()) Then
                MessageBox.Show("Custom tracker as illegal name and could not be added!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function CheckCustomTrackerValid() As Boolean
        Dim sText As String = m_DialogResult.sCustomTrackerName

        If (String.IsNullOrWhiteSpace(sText)) Then
            Return False
        End If

        If (sText.Length > Byte.MaxValue) Then
            Return False
        End If

        ' Check ASCII-only
        For i = 0 To sText.Length - 1
            If (AscW(sText(i)) < Byte.MinValue OrElse AscW(sText(i)) > Byte.MaxValue) Then
                Return False
            End If
        Next

        'Check for "
        For i = sText.Length - 1 To 0 Step -1
            If (sText(i) = """"c) Then
                Return False
            End If
        Next

        'Has been escaped?
        Dim iEscapeCount As Integer = 0
        For i = sText.Length - 1 To 0 Step -1
            If (sText(i) = "\"c) Then
                iEscapeCount += 1
            Else
                Exit For
            End If
        Next

        If ((iEscapeCount Mod 2) = 1) Then
            Return False
        End If

        Return True
    End Function

    Private Sub RadioButton_VMT_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton_VMT.CheckedChanged
        UpdateRadioButtonSelection()
    End Sub

    Private Sub RadioButton_Custom_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton_Custom.CheckedChanged
        UpdateRadioButtonSelection()
    End Sub

    Private Sub UpdateRadioButtonSelection()
        Label_VMTTracker.Enabled = False
        ComboBox_VMTTracker.Enabled = False
        Label_CustomTracker.Enabled = False
        ComboBox_CustomTracker.Enabled = False

        Select Case (True)
            Case RadioButton_VMT.Checked
                Label_VMTTracker.Enabled = True
                ComboBox_VMTTracker.Enabled = True

            Case RadioButton_Custom.Checked
                Label_CustomTracker.Enabled = True
                ComboBox_CustomTracker.Enabled = True
        End Select

    End Sub
End Class