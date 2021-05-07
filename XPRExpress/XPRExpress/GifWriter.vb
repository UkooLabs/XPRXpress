Imports System.IO

Friend Class GifWriter

    Private Sub WriteString(ByVal FileStream As FileStream, ByVal Text As String)
        Dim objEncoding As New System.Text.ASCIIEncoding()
        Dim bytBuffer() As Byte = objEncoding.GetBytes(Text)
        FileStream.Write(bytBuffer, 0, bytBuffer.Length)
    End Sub

    Private Sub WriteShort(ByVal FileStream As FileStream, ByVal Value As Integer)
        FileStream.WriteByte(Value And &HFF)
        FileStream.WriteByte((Value >> 8) And &HFF)
    End Sub

    Private Sub WritePalette(ByVal FileStream As FileStream, ByVal Palette As Byte())
        For intLoop As Integer = 0 To 255
            FileStream.WriteByte(Palette((intLoop * 4) + 2))
            FileStream.WriteByte(Palette((intLoop * 4) + 1))
            FileStream.WriteByte(Palette(intLoop * 4))
        Next intLoop
    End Sub

    Private Sub WriteNetscapeExt(ByVal FileStream As FileStream, ByVal Repeat As Integer)
        FileStream.WriteByte(&H21)
        FileStream.WriteByte(&HFF)
        FileStream.WriteByte(11)
        WriteString(FileStream, "NETSCAPE2.0")
        FileStream.WriteByte(3)
        FileStream.WriteByte(1)
        WriteShort(FileStream, Repeat)
        FileStream.WriteByte(0)
    End Sub

    Private Sub WriteGraphicCtrlExt(ByVal FileStream As FileStream, ByVal Delay As Integer, ByVal TransparencyIndex As Integer)
        FileStream.WriteByte(&H21)
        FileStream.WriteByte(&HF9)
        FileStream.WriteByte(4)
        If TransparencyIndex > 0 Then
            FileStream.WriteByte(9)
            WriteShort(FileStream, Delay)
            FileStream.WriteByte(TransparencyIndex)
        Else
            FileStream.WriteByte(0)
            WriteShort(FileStream, Delay)
            FileStream.WriteByte(0)
        End If
        FileStream.WriteByte(0)
    End Sub

    Private Sub WriteImageDesc(ByVal FileStream As FileStream, ByVal Width As Integer, ByVal Height As Integer, ByVal FirstFrame As Boolean)
        FileStream.WriteByte(&H2C)
        WriteShort(FileStream, 0)
        WriteShort(FileStream, 0)
        WriteShort(FileStream, Width)
        WriteShort(FileStream, Height)
        If FirstFrame Then
            FileStream.WriteByte(0)
        Else
            FileStream.WriteByte(&H87)
        End If
    End Sub

    Public Function SaveGIF(ByVal FileName As String, ByVal ImageFrames As ImageFrames) As Boolean

        Dim intWidth As Integer = ImageFrames.Frames(0).Frame.Width
        Dim intHeight As Integer = ImageFrames.Frames(0).Frame.Height

        Dim objFileStream As New FileStream(FileName, FileMode.Create, FileAccess.Write)
        WriteString(objFileStream, "GIF89a")

        WriteShort(objFileStream, intWidth)
        WriteShort(objFileStream, intHeight)
        objFileStream.WriteByte(&HF7)
        objFileStream.WriteByte(0)
        objFileStream.WriteByte(0)

        WritePalette(objFileStream, ImageFrames.Frames(0).Palette)
        WriteNetscapeExt(objFileStream, 0)

        For intLoop1 As Integer = 0 To ImageFrames.Frames.Count - 1

            Dim bytRawFrame() As Byte = ImageFrames.Frames(intLoop1).RawFrame

            Dim intTransparencyCount As Integer = 0
            Dim intTransparencyIndex As Integer = -1
            For intLoop2 As Integer = 0 To 255
                If ImageFrames.Frames(intLoop1).Palette((intLoop2 * 4) + 3) = 0 Then
                    intTransparencyCount += 1
                    If intTransparencyCount = 1 Then
                        intTransparencyIndex = intLoop2
                    Else
                        For intLoop3 As Integer = LBound(bytRawFrame) To UBound(bytRawFrame)
                            If bytRawFrame(intLoop3) = intLoop2 Then bytRawFrame(intLoop3) = intTransparencyIndex
                        Next intLoop3
                    End If
                End If
            Next intLoop2

            WriteGraphicCtrlExt(objFileStream, ImageFrames.Frames(intLoop1).Delay \ 10, intTransparencyIndex)
            WriteImageDesc(objFileStream, intWidth, intHeight, intLoop1 = 0)
            If intLoop1 > 0 Then WritePalette(objFileStream, ImageFrames.Frames(intLoop1).Palette)

            Dim objLZWEncoder As New LZWEncoder(intWidth, intHeight, bytRawFrame, 8)
            objLZWEncoder.Encode(objFileStream)

        Next intLoop1

        objFileStream.WriteByte(&H3B)
        objFileStream.Flush()
        objFileStream.Close()

    End Function

End Class
