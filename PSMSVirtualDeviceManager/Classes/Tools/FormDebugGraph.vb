Imports System.Numerics

Public Class FormDebugGraph
    Private g_bIgnoreEvents As Boolean = False

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            g_bIgnoreEvents = True

            ToolStripComboBox_Scale.Items.Clear()
            ToolStripComboBox_Scale.Items.Add("1")
            ToolStripComboBox_Scale.Items.Add("10")
            ToolStripComboBox_Scale.Items.Add("100")
            ToolStripComboBox_Scale.Items.Add("1000")
            ToolStripComboBox_Scale.Items.Add("10000")
            ToolStripComboBox_Scale.SelectedIndex = 0
        Finally
            g_bIgnoreEvents = False
        End Try
    End Sub

    Public Property m_Index As Integer = 0
    Public Property m_MaxIndex As Integer = 1000
    Public Property m_Scale As Integer = 1

    Public Sub AddChartValues(sSeriesName As String, iValue As Quaternion)
        AddChartValues(sSeriesName & ".x", iValue.X)
        AddChartValues(sSeriesName & ".y", iValue.Y)
        AddChartValues(sSeriesName & ".z", iValue.Z)
        AddChartValues(sSeriesName & ".w", iValue.W)
    End Sub

    Public Sub AddChartValues(sSeriesName As String, iValue As Vector3)
        AddChartValues(sSeriesName & ".x", iValue.X)
        AddChartValues(sSeriesName & ".y", iValue.Y)
        AddChartValues(sSeriesName & ".z", iValue.Z)
    End Sub

    Public Sub AddChartValues(sSeriesName As String, iValue As Double)
        If (Not Chart_DebugGraph.Enabled) Then
            Return
        End If

        If (Chart_DebugGraph.ChartAreas.Count < 1) Then
            Return
        End If

        If (m_Scale < 1) Then
            m_Scale = 1
        End If

        iValue *= m_Scale

        If (Chart_DebugGraph.Series.IndexOf(sSeriesName) = -1) Then
            Dim mSeries = Chart_DebugGraph.Series.Add(sSeriesName)

            mSeries.ChartArea = Chart_DebugGraph.ChartAreas(0).Name
            mSeries.ChartType = DataVisualization.Charting.SeriesChartType.FastLine
            mSeries.BorderWidth = 2
        End If

        Chart_DebugGraph.Series(sSeriesName).Points.AddXY(m_Index, iValue)

        While (Chart_DebugGraph.Series(sSeriesName).Points.Count > 1 AndAlso
            Chart_DebugGraph.Series(sSeriesName).Points(1).XValue < (m_Index - m_MaxIndex))
            Chart_DebugGraph.Series(sSeriesName).Points.RemoveAt(0)
        End While

        ' Adjust bounds
        Dim mAxisX = Chart_DebugGraph.ChartAreas(0).AxisX()
        If (mAxisX.Maximum < m_Index) Then
            mAxisX.Maximum = m_Index
        End If
        If (mAxisX.Minimum < (m_Index - m_MaxIndex)) Then
            mAxisX.Minimum = m_Index - m_MaxIndex
        End If

        Dim mAxisY = Chart_DebugGraph.ChartAreas(0).AxisY()
        Dim iValueMax = Math.Ceiling(iValue * 0.1) * 10
        Dim iValueMin = Math.Floor(iValue * 0.1) * 10

        If (mAxisY.Maximum < iValueMax) Then
            mAxisY.Maximum = iValueMax
        End If
        If (mAxisY.Minimum > iValueMin) Then
            mAxisY.Minimum = iValueMin
        End If
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ClearGraphToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearGraphToolStripMenuItem.Click
        Chart_DebugGraph.Series.Clear()
        Chart_DebugGraph.ChartAreas(0).AxisX.Maximum = 1
        Chart_DebugGraph.ChartAreas(0).AxisX.Minimum = 0
        Chart_DebugGraph.ChartAreas(0).AxisY.Maximum = 1
        Chart_DebugGraph.ChartAreas(0).AxisY.Minimum = 0
    End Sub

    Private Sub ToolStripComboBox_Scale_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox_Scale.SelectedIndexChanged
        If (g_bIgnoreEvents) Then
            Return
        End If

        If (ToolStripComboBox_Scale.SelectedItem Is Nothing) Then
            Return
        End If

        m_Scale = CInt(ToolStripComboBox_Scale.SelectedItem)
    End Sub
End Class