Public Class ClassListViewEx
    Inherits ListView

    Public Sub New()
        'Enable double buffering
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint, True)
    End Sub
End Class
