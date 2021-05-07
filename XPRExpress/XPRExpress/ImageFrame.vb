Public Class ImageFrame

    Private _Frame As Drawing.Image
    Private _RawFrame As Byte()
    Private _Palette As Byte()
    Private _Delay As Integer

    Public Property Frame() As Drawing.Image
        Get
            Return _Frame
        End Get
        Set(ByVal Value As Drawing.Image)
            _Frame = Value
        End Set
    End Property

    Public Property RawFrame() As Byte()
        Get
            Return _RawFrame
        End Get
        Set(ByVal Value As Byte())
            _RawFrame = Value
        End Set
    End Property

    Public Property Palette() As Byte()
        Get
            Return _Palette
        End Get
        Set(ByVal Value As Byte())
            _Palette = Value
        End Set
    End Property

    Public Property Delay() As Integer
        Get
            Return _Delay
        End Get
        Set(ByVal Value As Integer)
            _Delay = Value
        End Set
    End Property

End Class
