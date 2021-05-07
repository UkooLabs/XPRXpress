Imports System.IO

Friend Class LZWEncoder

    Private Const EOF As Integer = -1
    Private Const BITS As Integer = 12
    Private Const HASHSIZE As Integer = 5003
    Private Const MAXMAXCODE As Integer = 1 << BITS

    Private intWidth As Integer
    Private intHeight As Integer
    Private bytRawFrame As Byte()
    Private intPixelIndex As Integer
    Private intAccumCount As Integer
    Private intInitCodeSize As Integer
    Private intFreeEnt As Integer = 0
    Private intBits As Integer
    Private blnClearFlag As Boolean = False
    Private intCurrentAccum As Integer = 0
    Private intCurrentBits As Integer = 0
    Private intMaxCode As Integer
    Private intGlobalInitBits As Integer
    Private intClearCode As Integer
    Private intEOFCode As Integer

    Private intMasks() As Integer = {&H0, &H1, &H3, &H7, &HF, &H1F, &H3F, &H7F, &HFF, &H1FF, &H3FF, &H7FF, &HFFF, &H1FFF, &H3FFF, &H7FFF, &HFFFF}
    Private bytAccum As Byte() = New Byte(256) {}
    Private intHashTable As Integer() = New Integer(HASHSIZE) {}
    Private intCodeTable As Integer() = New Integer(HASHSIZE) {}

    Public Sub New(ByVal Width As Integer, ByVal Height As Integer, ByVal RawFrame As Byte(), ByVal ColorDepth As Integer)
        intWidth = Width
        intHeight = Height
        bytRawFrame = RawFrame
        intInitCodeSize = Math.Max(2, ColorDepth)
    End Sub

    Private Sub AddValue(ByVal Value As Byte, ByVal FileStream As FileStream)
        bytAccum(intAccumCount) = Value
        intAccumCount += 1
        If intAccumCount >= 254 Then Flush(FileStream)
    End Sub

    Private Sub ClearTable(ByVal FileStream As FileStream)
        ResetCodeTable()
        intFreeEnt = intClearCode + 2
        blnClearFlag = True
        Output(intClearCode, FileStream)
    End Sub

    Private Sub ResetCodeTable()
        For intLoop As Integer = LBound(intHashTable) To UBound(intHashTable)
            intHashTable(intLoop) = -1
        Next intLoop
    End Sub

    Private Sub Compress(ByVal intInitBits As Integer, ByVal FileStream As FileStream)

        Dim intFCode As Integer = HASHSIZE
        Dim intHShift As Integer = 0
        Dim blnExit As Boolean = False
        Dim intEnt As Integer = NextPixel()
        Dim intIndex As Integer
        Dim intValue As Integer
        Dim intDisp As Integer

        intGlobalInitBits = intInitBits
        blnClearFlag = False
        intBits = intGlobalInitBits
        intMaxCode = MaxCode(intBits)
        intClearCode = 1 << (intInitBits - 1)
        intEOFCode = intClearCode + 1
        intFreeEnt = intClearCode + 2
        intAccumCount = 0

        While intFCode < 65536
            intHShift += 1
            intFCode *= 2
        End While

        intHShift = 8 - intHShift
        ResetCodeTable()
        Output(intClearCode, FileStream)

OuterLoop:

        intValue = NextPixel()
        If intValue = EOF Then
            Output(intEnt, FileStream)
            Output(intEOFCode, FileStream)
        Else
            intFCode = (intValue << BITS) + intEnt
            intIndex = (intValue << intHShift) Xor intEnt
            If intHashTable(intIndex) = intFCode Then
                intEnt = intCodeTable(intIndex)
            Else
                If intHashTable(intIndex) >= 0 Then
                    intDisp = HASHSIZE - intIndex
                    If intIndex = 0 Then intDisp = 1
                    Do
                        intIndex = intIndex - intDisp
                        If intIndex < 0 Then intIndex += HASHSIZE
                        If intHashTable(intIndex) = intFCode Then
                            intEnt = intCodeTable(intIndex)
                            GoTo OuterLoop
                        End If
                    Loop While intHashTable(intIndex) >= 0
                End If
                Output(intEnt, FileStream)
                intEnt = intValue
                If intFreeEnt < MAXMAXCODE Then
                    intCodeTable(intIndex) = intFreeEnt
                    intFreeEnt += 1
                    intHashTable(intIndex) = intFCode
                Else
                    ClearTable(FileStream)
                End If
            End If
            GoTo OuterLoop
        End If

    End Sub

    Public Sub Encode(ByVal FileStream As FileStream)
        FileStream.WriteByte(intInitCodeSize)
        intPixelIndex = 0
        Compress(intInitCodeSize + 1, FileStream)
        FileStream.WriteByte(0)
    End Sub

    Private Sub Flush(ByVal FileStream As FileStream)
        If intAccumCount > 0 Then
            FileStream.WriteByte(intAccumCount)
            FileStream.Write(bytAccum, 0, intAccumCount)
            intAccumCount = 0
        End If
    End Sub

    Private Function MaxCode(ByVal BitCount As Integer) As Integer
        Return (1 << BitCount) - 1
    End Function

    Private Function NextPixel() As Integer
        If intPixelIndex = bytRawFrame.Length Then Return EOF
        Dim bytPixel As Byte = bytRawFrame(intPixelIndex)
        intPixelIndex += 1
        Return bytPixel
    End Function

    Private Sub Output(ByVal intCode As Integer, ByVal FileStream As FileStream)

        intCurrentAccum = intCurrentAccum And intMasks(intCurrentBits)

        If (intCurrentBits > 0) Then
            intCurrentAccum = intCurrentAccum Or (intCode << intCurrentBits)
        Else
            intCurrentAccum = intCode
        End If

        intCurrentBits += intBits

        While (intCurrentBits >= 8)
            AddValue((intCurrentAccum And &HFF), FileStream)
            intCurrentAccum >>= 8
            intCurrentBits -= 8
        End While

        If intFreeEnt > intMaxCode OrElse blnClearFlag Then
            If blnClearFlag Then
                intBits = intGlobalInitBits
                intMaxCode = MaxCode(intBits)
                blnClearFlag = False
            Else
                intBits += 1
                If intBits = BITS Then
                    intMaxCode = MAXMAXCODE
                Else
                    intMaxCode = MaxCode(intBits)
                End If
            End If
        End If

        If intCode = intEOFCode Then
            While (intCurrentBits > 0)
                AddValue((intCurrentAccum And &HFF), FileStream)
                intCurrentAccum >>= 8
                intCurrentBits -= 8
            End While
            Flush(FileStream)
        End If

    End Sub

End Class
