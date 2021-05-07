Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class Splash

    Private Class Win32

        Public Enum Bool
            [False] = 0
            [True]
        End Enum

        '<StructLayout(LayoutKind.Sequential)> _
        Public Structure Point
            Public x As Int32
            Public y As Int32

            Public Sub New(ByVal x As Int32, ByVal y As Int32)
                Me.x = x
                Me.y = y
            End Sub
        End Structure

        '<StructLayout(LayoutKind.Sequential)> _
        Public Structure Size
            Public cx As Int32
            Public cy As Int32

            Public Sub New(ByVal cx As Int32, ByVal cy As Int32)
                Me.cx = cx
                Me.cy = cy
            End Sub
        End Structure

        '<StructLayout(LayoutKind.SequentialPack = 1)> _
        Structure ARGB
            Public Blue As Byte
            Public Green As Byte
            Public Red As Byte
            Public Alpha As Byte
        End Structure

        '<StructLayout(LayoutKind.Sequential, , 1)> _
        Public Structure BLENDFUNCTION
            Public BlendOp As Byte
            Public BlendFlags As Byte
            Public SourceConstantAlpha As Byte
            Public AlphaFormat As Byte
        End Structure

        Public Const ULW_COLORKEY As Int32 = 1
        Public Const ULW_ALPHA As Int32 = 2
        Public Const ULW_OPAQUE As Int32 = 4
        Public Const AC_SRC_OVER As Byte = 0
        Public Const AC_SRC_ALPHA As Byte = 1

        <DllImport("user32.dll")> _
        Public Shared Function UpdateLayeredWindow(ByVal hwnd As IntPtr, ByVal hdcDst As IntPtr, ByRef pptDst As Point, ByRef psize As Size, ByVal hdcSrc As IntPtr, ByRef pprSrc As Point, ByVal crKey As Int32, ByRef pblend As BLENDFUNCTION, ByVal dwFlags As Int32) As Bool
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function GetDC(ByVal hWnd As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Integer
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function CreateCompatibleDC(ByVal hDC As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function DeleteDC(ByVal hdc As IntPtr) As Bool
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function SelectObject(ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function DeleteObject(ByVal hObject As IntPtr) As Bool
        End Function

    End Class

    Private Sub Splash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strVersion As String = System.String.Format("Version: {0}.{1}.{2}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build)
        Dim strDeveloped As String = "Developed By: |EqUiNoX|"
        Dim strGFX As String = "GFX By: Blackbolt"

        Dim objBitmap As Bitmap = New Bitmap(My.Resources.splashscreen)
        Dim objGraphics As Graphics = Graphics.FromImage(objBitmap)
        Dim objFont As New Font("Arial", 10, FontStyle.Bold)

        objGraphics.DrawString(strDeveloped, objFont, Brushes.Black, 12, 340)
        objGraphics.DrawString(strGFX, objFont, Brushes.Black, 12, 360)

        objGraphics.DrawString(strVersion, objFont, Brushes.Black, 480 - objGraphics.MeasureString(strVersion, objFont).Width, 360)

        SetBitmap(objBitmap)

        Me.Show()
        Threading.Thread.Sleep(4000)
        Me.Hide()
        Me.Close()

    End Sub

    Public Sub SetBitmap(ByVal bitmap As Bitmap)
        Dim screenDc As IntPtr = Win32.GetDC(IntPtr.Zero)
        Dim memDc As IntPtr = Win32.CreateCompatibleDC(screenDc)
        Dim hBitmap As IntPtr = IntPtr.Zero
        Dim oldBitmap As IntPtr = IntPtr.Zero
        Try
            hBitmap = Bitmap.GetHbitmap(Color.FromArgb(0))
            oldBitmap = Win32.SelectObject(memDc, hBitmap)
            Dim size As Win32.Size = New Win32.Size(Bitmap.Width, Bitmap.Height)
            Dim pointSource As Win32.Point = New Win32.Point(0, 0)
            Dim topPos As Win32.Point = New Win32.Point(Left, Top)
            Dim blend As Win32.BLENDFUNCTION = New Win32.BLENDFUNCTION
            blend.BlendOp = Win32.AC_SRC_OVER
            blend.BlendFlags = 0
            blend.SourceConstantAlpha = 255
            blend.AlphaFormat = Win32.AC_SRC_ALPHA
            Win32.UpdateLayeredWindow(Handle, screenDc, topPos, size, memDc, pointSource, 0, blend, Win32.ULW_ALPHA)
        Finally
            Win32.ReleaseDC(IntPtr.Zero, screenDc)
            If Not (hBitmap = IntPtr.Zero) Then
                Win32.SelectObject(memDc, oldBitmap)
                Win32.DeleteObject(hBitmap)
            End If
            Win32.DeleteDC(memDc)
        End Try
    End Sub

    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim objCreateParams As CreateParams = MyBase.CreateParams
            objCreateParams.ExStyle = objCreateParams.ExStyle Or &H80000
            Return objCreateParams
        End Get
    End Property

End Class