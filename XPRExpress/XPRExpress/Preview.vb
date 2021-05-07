Public Class Preview

    Dim intFrame As Integer
    Dim objImageFrames As ImageFrames = Nothing

    Public Sub New(ByVal ImageFrames As ImageFrames)
        InitializeComponent()
        objImageFrames = ImageFrames
    End Sub

    Public Sub RefreshPage(ByVal ImageFrames As ImageFrames)
        Timer.Enabled = False
        objImageFrames = ImageFrames
        PageLoad()
    End Sub

    Private Sub Preview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PageLoad()
    End Sub

    Private Sub PageLoad()
        intFrame = 0
        lblFrames.Text = objImageFrames.Frames.Count
        lblWidth.Text = objImageFrames.Frames(intFrame).Frame.Width
        lblHeight.Text = objImageFrames.Frames(intFrame).Frame.Height
        PictureBox.Image = objImageFrames.Frames(intFrame).Frame
        If objImageFrames.Frames.Count > 1 Then
            Timer.Interval = IIf(objImageFrames.Frames(intFrame).Delay <= 0, 20, objImageFrames.Frames(intFrame).Delay)
            intFrame += 1
            Timer.Enabled = True
        End If
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        Timer.Interval = IIf(objImageFrames.Frames(intFrame).Delay <= 0, 20, objImageFrames.Frames(intFrame).Delay)
        PictureBox.Image = objImageFrames.Frames(intFrame).Frame
        intFrame += 1
        If intFrame >= objImageFrames.Frames.Count Then intFrame = 0
    End Sub

End Class