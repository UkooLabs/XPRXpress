Public Class ImageFrames

    Private _Loops As Integer
    Private _Frames As New List(Of ImageFrame)

    Private _Frame As Integer = 0
    Private _Loop As Integer = 1

    Public Property Loops() As Integer
        Get
            Return _Loops
        End Get
        Set(ByVal Value As Integer)
            _Loops = Value
        End Set
    End Property

    Public Property Frames() As List(Of ImageFrame)
        Get
            Return _Frames
        End Get
        Set(ByVal Value As List(Of ImageFrame))
            _Frames = Value
        End Set
    End Property

End Class
