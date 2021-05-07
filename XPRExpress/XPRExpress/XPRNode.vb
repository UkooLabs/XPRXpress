Public Class XPRNode

    Private strName As String
    Private objType As NodeType
    Private intImageID As Integer

    Public Enum NodeType
        Folder = 0
        File = 1
    End Enum

    Public Sub New(ByVal Type As NodeType, ByVal Name As String, ByVal ImageID As Integer)
        objType = Type
        strName = Name
        intImageID = ImageID
    End Sub

    Public ReadOnly Property Type() As NodeType
        Get
            Return objType
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return strName
        End Get
    End Property

    Public ReadOnly Property ImageID() As Integer
        Get
            Return intImageID
        End Get
    End Property

End Class
