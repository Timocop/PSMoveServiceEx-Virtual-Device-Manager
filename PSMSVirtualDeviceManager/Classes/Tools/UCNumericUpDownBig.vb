Public Class UCNumericUpDownBig
    Private g_mNumericUpDown As NumericUpDown = Nothing
    Private g_bResetVisible As Boolean = False
    Private g_iResetValue As Decimal = 0
    Private g_bDockOnControl As Boolean = False

    Private g_iTimerInterval As Integer = 250

    Private g_bInit As Boolean = False
    Private g_bIgnoreEvents As Boolean = False

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.  
        g_bInit = True
    End Sub

    Public Property m_bDockOnControl As Boolean
        Get
            Return g_bDockOnControl
        End Get
        Set(value As Boolean)
            If (g_bDockOnControl = value) Then
                Return
            End If

            g_bDockOnControl = value

            UpdateDocking()
        End Set
    End Property

    Public Property m_NumericUpDown As NumericUpDown
        Get
            Return g_mNumericUpDown
        End Get
        Set(value As NumericUpDown)
            If (g_mNumericUpDown Is value) Then
                Return
            End If

            g_mNumericUpDown = value

            UpdateDocking()
        End Set
    End Property

    Public Property m_ResetVisible As Boolean
        Get
            Return g_bResetVisible
        End Get
        Set(value As Boolean)
            If (g_bResetVisible = value) Then
                Return
            End If

            g_bResetVisible = value

            UpdateResetButton()
        End Set
    End Property

    Public Property m_ResetValue As Decimal
        Get
            Return g_iResetValue
        End Get
        Set(value As Decimal)
            g_iResetValue = value
        End Set
    End Property

    Private Sub Button_NumUp_Click(sender As Object, e As EventArgs) Handles Button_NumUp.Click
        If (g_mNumericUpDown Is Nothing) Then
            Return
        End If

        g_mNumericUpDown.Value = Math.Min(Math.Max(g_mNumericUpDown.Value + g_mNumericUpDown.Increment, g_mNumericUpDown.Minimum), g_mNumericUpDown.Maximum)
    End Sub

    Private Sub Button_NumDown_Click(sender As Object, e As EventArgs) Handles Button_NumDown.Click
        If (g_mNumericUpDown Is Nothing) Then
            Return
        End If

        g_mNumericUpDown.Value = Math.Min(Math.Max(g_mNumericUpDown.Value - g_mNumericUpDown.Increment, g_mNumericUpDown.Minimum), g_mNumericUpDown.Maximum)
    End Sub

    Private Sub Button_NumUp_MouseDown(sender As Object, e As MouseEventArgs) Handles Button_NumUp.MouseDown
        Timer_Up.Interval = g_iTimerInterval
        Timer_Up.Start()
    End Sub

    Private Sub Button_NumUp_MouseUp(sender As Object, e As MouseEventArgs) Handles Button_NumUp.MouseUp
        Timer_Up.Stop()
    End Sub

    Private Sub Button_NumDown_MouseDown(sender As Object, e As MouseEventArgs) Handles Button_NumDown.MouseDown
        Timer_Down.Interval = g_iTimerInterval
        Timer_Down.Start()
    End Sub

    Private Sub Button_NumDown_MouseUp(sender As Object, e As MouseEventArgs) Handles Button_NumDown.MouseUp
        Timer_Down.Stop()
    End Sub

    Private Sub Timer_Up_Tick(sender As Object, e As EventArgs) Handles Timer_Up.Tick
        Timer_Up.Interval = CInt(Math.Max(Timer_Up.Interval * 0.75, 1))

        If (g_mNumericUpDown Is Nothing) Then
            Return
        End If

        g_mNumericUpDown.Value = Math.Min(Math.Max(g_mNumericUpDown.Value + g_mNumericUpDown.Increment, g_mNumericUpDown.Minimum), g_mNumericUpDown.Maximum)
    End Sub

    Private Sub Timer_Down_Tick(sender As Object, e As EventArgs) Handles Timer_Down.Tick
        Timer_Down.Interval = CInt(Math.Max(Timer_Down.Interval * 0.75, 1))

        If (g_mNumericUpDown Is Nothing) Then
            Return
        End If

        g_mNumericUpDown.Value = Math.Min(Math.Max(g_mNumericUpDown.Value - g_mNumericUpDown.Increment, g_mNumericUpDown.Minimum), g_mNumericUpDown.Maximum)
    End Sub

    Private Sub Button_Reset_Click(sender As Object, e As EventArgs) Handles Button_Reset.Click
        If (g_mNumericUpDown Is Nothing) Then
            Return
        End If

        g_mNumericUpDown.Value = Math.Min(Math.Max(g_iResetValue, g_mNumericUpDown.Minimum), g_mNumericUpDown.Maximum)
    End Sub

    Private Sub UCNumericUpDownBig_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If (Me.Disposing OrElse Me.IsDisposed) Then
            Return
        End If

        UpdateResetButton()
        UpdateDocking()
    End Sub

    Private Sub UpdateResetButton()
        If (Not g_bInit) Then
            Return
        End If

        Button_Reset.Visible = g_bResetVisible
    End Sub

    Private Sub UpdateDocking()
        If (g_bIgnoreEvents) Then
            Return
        End If

        Try
            g_bIgnoreEvents = True

            If (Not g_bInit) Then
                Return
            End If

            If (g_mNumericUpDown Is Nothing) Then
                Return
            End If

            If (Not g_bDockOnControl) Then
                Return
            End If

            Me.Parent = g_mNumericUpDown
            Me.Dock = DockStyle.Right
            Me.BringToFront()
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub
End Class
