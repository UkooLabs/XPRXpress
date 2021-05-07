Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging
Imports System.Collections.Generic

Public Class Decoder

    Private Const XPR_MAGIC_HEADER_VALUE As Integer = &H525058

    Private Const XPRFLAG_PALETTE As Integer = &H1
    Private Const XPRFLAG_ANIM As Integer = &H2

    Private Const D3DCOMMON_TYPE_MASK As Integer = &H70000
    Private Const D3DCOMMON_TYPE_TEXTURE As Integer = &H40000

    Private Const D3DFORMAT_A1R5G5B5 As Integer = &H2
    Private Const D3DFORMAT_A8R8G8B8 As Integer = &H6
    Private Const D3DFORMAT_P8 As Integer = &HB
    Private Const D3DFORMAT_DXT1 As Integer = &HC
    Private Const D3DFORMAT_DXT3 As Integer = &HE
    Private Const D3DFORMAT_DXT5 As Integer = &HF
    Private Const D3DFORMAT_LIN_A8R8G8B8 As Integer = &H12

    Private Structure D3DTexture
        Dim Common As UInt32
        Dim Data As UInt32
        Dim Lock As UInt32
        Dim Format As UInt32
        Dim Size As UInt32
        Dim Delay As UInt32
    End Structure

    Private Structure XPRFileInfo
        Dim Name As String
        Dim Offset As Integer
        Dim UnpackedSize As Integer
        Dim PackedSize As Integer
    End Structure

    Private dicXPRFileInfo As New List(Of XPRFileInfo)
    Private objFileStream As FileStream = Nothing
    Private objBinaryReader As BinaryReader = Nothing
    Private blnProtectionEnabled As Boolean = False

    Public ReadOnly Property FileCount() As Integer
        Get
            Return dicXPRFileInfo.Count
        End Get
    End Property

    Public ReadOnly Property FileName(ByVal Index As Integer) As String
        Get
            Return dicXPRFileInfo.Item(Index).Name
        End Get
    End Property

    Public Sub SaveImage(ByVal Index As Integer, ByVal FileName As String)
        If blnProtectionEnabled = False Then
            Dim objImageFrames As ImageFrames = GetImage(Index)
            If objImageFrames.Frames.Count = 1 Then
                objImageFrames.Frames(0).Frame.Save(FileName)
            Else
                If Path.GetExtension(FileName).ToUpper = ".GIF" Then
                    Dim objGif As New GifWriter
                    objGif.SaveGIF(FileName, objImageFrames)
                Else
                    objImageFrames.Frames(0).Frame.Save(FileName)
                End If
            End If
        Else
            Throw New ApplicationException("Image saving for protected XPR's is not allowed.")
        End If
    End Sub

    Public ReadOnly Property GetImage(ByVal Index As Integer) As ImageFrames
        Get
            Dim objXPRFileInfo As XPRFileInfo = dicXPRFileInfo.Item(Index)
            objBinaryReader.BaseStream.Seek(objXPRFileInfo.Offset, SeekOrigin.Begin)
            Dim bytPackedFile() As Byte = objBinaryReader.ReadBytes(objXPRFileInfo.PackedSize)
            Dim bytUnpackedFile() As Byte = Decompress(objXPRFileInfo.UnpackedSize, bytPackedFile)
            Return ConvertTextures(bytUnpackedFile)
        End Get
    End Property

    Public ReadOnly Property ProtectionEnabled() As Boolean
        Get
            Return blnProtectionEnabled
        End Get
    End Property

    Public Sub OpenXPR(ByVal FileName As String)

        objFileStream = New FileStream(FileName, FileMode.Open)
        objBinaryReader = New BinaryReader(objFileStream)
        If objFileStream.Length < 12 Then Throw New ApplicationException("Invalid XPR length in file '" & FileName & "'.")

        Dim intMagic As Integer = objBinaryReader.ReadInt32
        Dim intVersion = ((intMagic >> 24) And &H7F) - Asc("0")
        blnProtectionEnabled = ((intMagic >> 24) And &H80) = 0
        blnProtectionEnabled = False 'Force protection disabled

        If (intMagic And &HFFFFFF) <> XPR_MAGIC_HEADER_VALUE Or intVersion < 2 Then
            blnProtectionEnabled = False
            Exit Sub
            Throw New ApplicationException("Unsupported XPR file format '" & FileName & "'.")
        End If

        Dim intTotalSize As Integer = objBinaryReader.ReadInt32
        If intTotalSize <> objFileStream.Length Then Throw New ApplicationException("Team Blackbolt protected file '" & FileName & "'.")

        Dim intHeaderSize As Integer = objBinaryReader.ReadInt32
        If (intHeaderSize - 12) Mod 128 > 0 Then Throw New ApplicationException("Invalid XPR header in file '" & FileName & "'.")

        dicXPRFileInfo.Clear()
        For intloop As Integer = 1 To (intHeaderSize - 12) >> 7
            Dim objXPRFileInfo As New XPRFileInfo
            Dim arrBytes() As Byte = objBinaryReader.ReadBytes(116)
            Dim objEncoder As New System.Text.ASCIIEncoding
            objXPRFileInfo.Name = objEncoder.GetString(arrBytes).Split(Chr(0))(0)
            objXPRFileInfo.Offset = objBinaryReader.ReadInt32
            objXPRFileInfo.UnpackedSize = objBinaryReader.ReadInt32
            objXPRFileInfo.PackedSize = objBinaryReader.ReadInt32
            dicXPRFileInfo.Add(objXPRFileInfo)
        Next intloop

    End Sub

    Public Sub CloseXPR()
        blnProtectionEnabled = False
        If objBinaryReader IsNot Nothing Then objBinaryReader.Close()
        If objFileStream IsNot Nothing Then objFileStream.Close()
    End Sub

    <DllImport("XPRTool.dll")> _
    Private Shared Sub Unswizzle(ByVal Source As Byte(), ByVal Depth As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal Dest As Byte())
    End Sub

    <DllImport("XPRTool.dll")> _
    Private Shared Sub ConvertA1R5G5B5(ByVal Source As Byte(), ByVal Width As Integer, ByVal Height As Integer, ByVal Dest As Byte())
    End Sub

    <DllImport("XPRTool.dll")> _
    Private Shared Sub ConvertDXT1(ByVal Source As Byte(), ByVal Width As Integer, ByVal Height As Integer, ByVal Dest As Byte())
    End Sub

    <DllImport("XPRTool.dll")> _
    Private Shared Sub ConvertDXT3(ByVal Source As Byte(), ByVal Width As Integer, ByVal Height As Integer, ByVal Dest As Byte())
    End Sub

    <DllImport("XPRTool.dll")> _
    Private Shared Sub ConvertDXT5(ByVal Source As Byte(), ByVal Width As Integer, ByVal Height As Integer, ByVal Dest As Byte())
    End Sub

    <DllImport("XPRTool.dll")> _
    Private Shared Sub MapPalette(ByVal Pal As Byte(), ByVal Source As Byte(), ByVal Width As Integer, ByVal Height As Integer, ByVal Dest As Byte())
    End Sub

    <DllImport("lzo.dll")> _
    Private Shared Function lzo1x_decompress_safe(ByVal Source As Byte(), ByVal SourceLength As Integer, ByVal Dest As Byte(), ByRef DestLength As Integer, ByVal WorkMem As Byte()) As Integer
    End Function

    Private Function Decompress(ByVal OrigLength As Integer, ByVal Source As Byte()) As Byte()
        Dim bytBuffer As Byte() = New Byte(OrigLength - 1) {}
        Dim intOrigLength As Integer = OrigLength
        lzo1x_decompress_safe(Source, Source.Length, bytBuffer, intOrigLength, Nothing)
        Return bytBuffer
    End Function

    Private Function FormatIsSwizzled(ByVal intFormat As Integer) As Boolean
        Select Case intFormat
            Case D3DFORMAT_A1R5G5B5
                Return True
            Case D3DFORMAT_A8R8G8B8
                Return True
            Case D3DFORMAT_P8
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Function FormatIsSupported(ByVal intFormat As Integer) As Boolean
        Select Case intFormat
            Case D3DFORMAT_A1R5G5B5
                Return True
            Case D3DFORMAT_A8R8G8B8
                Return True
            Case D3DFORMAT_DXT1
                Return True
            Case D3DFORMAT_DXT3
                Return True
            Case D3DFORMAT_DXT5
                Return True
            Case D3DFORMAT_P8
                Return True
            Case D3DFORMAT_LIN_A8R8G8B8
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Function GetDepth(ByVal intFormat As Integer) As Integer
        Select Case intFormat
            Case D3DFORMAT_A1R5G5B5
                Return 4
            Case D3DFORMAT_A8R8G8B8
                Return 4
            Case D3DFORMAT_DXT1
                Return 4
            Case D3DFORMAT_DXT3
                Return 4
            Case D3DFORMAT_DXT5
                Return 4
            Case D3DFORMAT_P8
                Return 1
            Case D3DFORMAT_LIN_A8R8G8B8
                Return 4
            Case Else
                Return 0
        End Select
    End Function

    Private Function GetWord(ByVal bytData() As Byte, ByRef Offset As Integer) As UInt32
        Dim intValue As UInt32
        intValue = CType(bytData(Offset + 1), UInt32) << 8
        intValue += CType(bytData(Offset), UInt32)
        Offset += 2
        Return intValue
    End Function

    Private Function GetDWord(ByVal bytData() As Byte, ByRef Offset As Integer) As UInt32
        Dim intValue As UInt32
        intValue = CType(bytData(Offset), UInt32)
        intValue += CType(bytData(Offset + 1), UInt32) << 8
        intValue += CType(bytData(Offset + 2), UInt32) << 16
        intValue += CType(bytData(Offset + 3), UInt32) << 24
        Offset += 4
        Return intValue
    End Function

    Private Function ConvertTextures(ByVal Buffer() As Byte) As ImageFrames

        Dim objImageFrames As New ImageFrames
        Dim blnHasPalette As Boolean = False
        Dim objTex As New D3DTexture
        Dim lstTextures As New List(Of D3DTexture)
        Dim intRealSizeWidth As Integer = 0
        Dim intRealSizeHeight As Integer = 0
        Dim intNext As Integer = 0
        Dim intFlags As Integer = GetDWord(Buffer, intNext)
        Dim intLoops As Integer = 0

        If (intFlags And XPRFLAG_ANIM Or (intFlags >> 16) > 1) Then
            intLoops = GetDWord(Buffer, intNext)
            intRealSizeWidth = GetWord(Buffer, intNext)
            intRealSizeHeight = GetWord(Buffer, intNext)
            If intFlags And XPRFLAG_PALETTE Then
                blnHasPalette = True
                intNext += 12
            End If
            Dim intTextures As Integer = intFlags >> 16
            For intLoop As Integer = 1 To intTextures
                objTex = New D3DTexture
                objTex.Common = GetDWord(Buffer, intNext)
                objTex.Data = GetDWord(Buffer, intNext)
                objTex.Lock = GetDWord(Buffer, intNext)
                objTex.Format = GetDWord(Buffer, intNext)
                objTex.Size = GetDWord(Buffer, intNext)
                objTex.Delay = GetDWord(Buffer, intNext)
                lstTextures.Add(objTex)
            Next intLoop
        Else
            If intFlags And XPRFLAG_PALETTE Then
                blnHasPalette = True
                intNext += 12
            End If
            objTex = New D3DTexture
            objTex.Common = GetDWord(Buffer, intNext)
            objTex.Data = GetDWord(Buffer, intNext)
            objTex.Lock = GetDWord(Buffer, intNext)
            objTex.Format = GetDWord(Buffer, intNext)
            objTex.Size = GetDWord(Buffer, intNext)
            objTex.Delay = 0
            lstTextures.Add(objTex)
            intRealSizeWidth = GetWord(Buffer, intNext)
            intRealSizeHeight = GetWord(Buffer, intNext)
        End If

        objImageFrames.Loops = intLoops

        Dim intResDataOffset As Integer = (intNext + 127) And (Not 127)
        Return ConvertTextures(objImageFrames, blnHasPalette, intRealSizeWidth, intRealSizeHeight, lstTextures, intResDataOffset, Buffer)

    End Function

    Private Function ConvertTextures(ByVal ImageFrames As ImageFrames, ByVal HasPalette As Boolean, ByVal RealSizeWidth As Integer, ByVal RealSizeHeight As Integer, ByVal Textures As List(Of D3DTexture), ByVal ResDataOffset As Integer, ByVal Buffer() As Byte) As ImageFrames

        For intLoop As Integer = 1 To Textures.Count

            Dim objTex As D3DTexture = Textures.Item(intLoop - 1)

            If (objTex.Common And D3DCOMMON_TYPE_MASK) <> D3DCOMMON_TYPE_TEXTURE Then Throw New ApplicationException("Unsupported texture format.")

            Dim intTexWidth As Integer
            Dim intTexHeight As Integer
            Dim intTexFormat As Integer = (objTex.Format And &HFF00) >> 8
            If intTexFormat = D3DFORMAT_LIN_A8R8G8B8 Then
                intTexWidth = (RealSizeWidth + 63) And (Not 63)
                intTexHeight = RealSizeHeight
            Else
                intTexWidth = 1 << ((objTex.Format And &HF00000) >> 20)
                intTexHeight = 1 << ((objTex.Format And &HF000000) >> 24)
            End If
            Dim intTexDepth As Integer = GetDepth(intTexFormat)

            Dim intTexSize As Integer = (intTexWidth * intTexHeight * intTexDepth)
            Dim intTextureOffset As Integer = ResDataOffset
            Dim intPaletteOffset As Integer = 0
            If HasPalette = True Then
                intTextureOffset = ResDataOffset + objTex.Data
                intPaletteOffset = ResDataOffset
            End If

            If Not FormatIsSupported(intTexFormat) Then Throw New ApplicationException("Unsupported texture format.")

            Dim bytSource((Buffer.Length - intTextureOffset) - 1) As Byte
            Dim bytDest((intTexWidth * intTexHeight * intTexDepth) - 1) As Byte
            Array.Copy(Buffer, intTextureOffset, bytSource, 0, Buffer.Length - intTextureOffset)

            If intTexFormat = D3DFORMAT_A1R5G5B5 Then
                Dim bytDestTemp((intTexWidth * intTexHeight * intTexDepth) - 1) As Byte
                ConvertA1R5G5B5(bytSource, intTexWidth, intTexHeight, bytDestTemp)
                Unswizzle(bytDestTemp, intTexDepth, intTexWidth, intTexHeight, bytDest)
            ElseIf intTexFormat = D3DFORMAT_DXT1 Then
                ConvertDXT1(bytSource, intTexWidth, intTexHeight, bytDest)
            ElseIf intTexFormat = D3DFORMAT_DXT3 Then
                ConvertDXT3(bytSource, intTexWidth, intTexHeight, bytDest)
            ElseIf intTexFormat = D3DFORMAT_DXT5 Then
                ConvertDXT5(bytSource, intTexWidth, intTexHeight, bytDest)
            ElseIf intTexFormat = D3DFORMAT_LIN_A8R8G8B8 Then
                Array.Copy(bytSource, 0, bytDest, 0, bytSource.Length)
            ElseIf FormatIsSwizzled(intTexFormat) = True Then
                Unswizzle(bytSource, intTexDepth, intTexWidth, intTexHeight, bytDest)
            End If

            If HasPalette = False Then

                Dim objImageBack As New Drawing.Bitmap(CInt(intTexWidth), CInt(RealSizeHeight), PixelFormat.Format32bppArgb)
                Dim objGraphics As Drawing.Graphics = Drawing.Graphics.FromImage(objImageBack)
                objGraphics.Clear(Drawing.Color.Transparent)

                Dim m_BitmapData As BitmapData
                Dim objBitmap As New Drawing.Bitmap(objImageBack)
                Dim bounds As Drawing.Rectangle = New Drawing.Rectangle(0, 0, objBitmap.Width, objBitmap.Height)
                m_BitmapData = objBitmap.LockBits(bounds, Drawing.Imaging.ImageLockMode.ReadWrite, Drawing.Imaging.PixelFormat.Format32bppArgb)
                Dim RowSizeBytes As Integer = m_BitmapData.Stride
                Dim total_size As Integer = m_BitmapData.Stride * m_BitmapData.Height
                Marshal.Copy(bytDest, 0, m_BitmapData.Scan0, total_size)
                objBitmap.UnlockBits(m_BitmapData)

                objBitmap = objBitmap.Clone(New Drawing.Rectangle(0, 0, RealSizeWidth, RealSizeHeight), PixelFormat.Format32bppArgb)

                Dim objImageFrame As New ImageFrame
                objImageFrame.Frame = objBitmap
                Dim bytRawFrame((RealSizeWidth * RealSizeHeight * 4) - 1) As Byte
                For intLoopY As Integer = 0 To RealSizeHeight - 1
                    Array.Copy(bytDest, intLoopY * (intTexWidth * 4), bytRawFrame, intLoopY * (RealSizeWidth * 4), RealSizeWidth)
                Next intLoopY
                objImageFrame.RawFrame = bytRawFrame
                objImageFrame.Palette = Nothing
                objImageFrame.Delay = objTex.Delay
                ImageFrames.Frames.Add(objImageFrame)

            Else

                Dim bytPalette(1023) As Byte
                Dim bytMappedDest(((intTexWidth * intTexHeight) << 2) - 1) As Byte
                Array.Copy(Buffer, intPaletteOffset, bytPalette, 0, 1024)

                MapPalette(bytPalette, bytDest, intTexWidth, intTexHeight, bytMappedDest)

                Dim objImageBack As New Drawing.Bitmap(CInt(intTexWidth), CInt(RealSizeHeight), PixelFormat.Format32bppArgb)
                Dim objGraphics As Drawing.Graphics = Drawing.Graphics.FromImage(objImageBack)
                objGraphics.Clear(Drawing.Color.Transparent)

                Dim m_BitmapData As BitmapData
                Dim objBitmap As New Drawing.Bitmap(objImageBack)
                Dim bounds As Drawing.Rectangle = New Drawing.Rectangle(0, 0, objBitmap.Width, objBitmap.Height)
                m_BitmapData = objBitmap.LockBits(bounds, Drawing.Imaging.ImageLockMode.ReadWrite, Drawing.Imaging.PixelFormat.Format32bppArgb)
                Dim RowSizeBytes As Integer = m_BitmapData.Stride
                Dim total_size As Integer = m_BitmapData.Stride * m_BitmapData.Height
                Marshal.Copy(bytMappedDest, 0, m_BitmapData.Scan0, total_size)
                objBitmap.UnlockBits(m_BitmapData)
                objBitmap = objBitmap.Clone(New Drawing.Rectangle(0, 0, RealSizeWidth, RealSizeHeight), PixelFormat.Format32bppArgb)

                Dim objImageFrame As New ImageFrame
                objImageFrame.Frame = objBitmap
                Dim bytRawFrame((RealSizeWidth * RealSizeHeight) - 1) As Byte
                For intLoopY As Integer = 0 To RealSizeHeight - 1
                    Array.Copy(bytDest, intLoopY * intTexWidth, bytRawFrame, intLoopY * RealSizeWidth, RealSizeWidth)
                Next intLoopY
                objImageFrame.RawFrame = bytRawFrame
                objImageFrame.Palette = bytPalette
                objImageFrame.Delay = objTex.Delay
                ImageFrames.Frames.Add(objImageFrame)

            End If

        Next intLoop

        Return ImageFrames

    End Function

End Class
