Imports System.IO
Imports System.Diagnostics
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Collections.Specialized
Imports Microsoft.VisualBasic
Imports System
Imports System.Security.Permissions
Imports Microsoft.Win32

Public Class Main

    Private Enum Mode
        Extract = 0
        Compress = 1
        Abort = 2
    End Enum

    Public FileInfo As Preview

    Private strCompressLog As String = ""
    Private objDecoder As New Decoder

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strVersion As String = System.String.Format("XPR Express - V{0}.{1}.{2}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build)
        Me.Text = strVersion

        ApplySettings()

        Dim strArguments() As String = ParseCommandLine(System.Environment.CommandLine)

        HelpWebBrowser.DocumentText = My.Resources.Help
        AboutWebBrowser.DocumentText = My.Resources.About

        If strArguments.Length = 2 AndAlso (strArguments(1).ToLower = "-help" Or strArguments(1).ToLower = "-h") Then
            TabControl1.SelectedIndex = 2
        ElseIf strArguments.Length > 2 Then
            ExecuteCommandLine(strArguments)
        End If

    End Sub

    Private Sub ApplySettings()
        DisableSplash.Checked = My.Settings.DisableSplash
        InstantPreview.Checked = My.Settings.InstantPreview
        ShowImageSizes.Checked = My.Settings.ShowImageSizes
        If DisableSplash.Checked = False Then
            Dim SplashScreen As New Splash
            SplashScreen.ShowDialog()
        End If
        ShellIntegration.Checked = ShellIntegrated()
        PopulateMRUCombo("ExtractSourceFile", ExtractSourceFile)
        PopulateMRUCombo("ExtractDestinationFolder", ExtractDestinationFolder)
        PopulateMRUCombo("CompressSourceFolder", CompressSourceFolder)
        PopulateMRUCombo("CompressDestinationFile", CompressDestinationFile)
    End Sub

    Private Sub PopulateMRUCombo(ByVal MRUListName As String, ByVal MRUCombo As ComboBox)
        MRUCombo.Items.Clear()
        Dim objStringCollection As StringCollection = My.Settings.Item(MRUListName)
        For intLoop As Integer = 0 To objStringCollection.Count - 1
            MRUCombo.Items.Add(objStringCollection(intLoop))
        Next intLoop
        If objStringCollection.Count > 0 Then MRUCombo.Text = objStringCollection(0)
    End Sub

    Private Sub AddToMRU(ByVal MRUListName As String, ByVal Entry As String)

        Dim objStringCollection As StringCollection = My.Settings.Item(MRUListName)

        Dim intPos As Integer = SearchStringCollection(objStringCollection, Entry)
        If intPos > -1 Then objStringCollection.RemoveAt(intPos)

        objStringCollection.Insert(0, Entry)

        If objStringCollection.Count > 5 Then objStringCollection.RemoveAt(5)

        My.Settings.Item(MRUListName) = objStringCollection
        My.Settings.Save()

    End Sub

    Private Function SearchStringCollection(ByVal StringCollection As StringCollection, ByVal SearchString As String) As Integer
        For intLoop As Integer = 0 To StringCollection.Count - 1
            If StringCollection(intLoop).ToLower = SearchString.ToLower Then Return intLoop
        Next intLoop
        Return -1
    End Function

    Private Function ParseCommandLine(ByVal CommandLine As String) As String()

        Dim ArgumentCount As Integer = 0
        Dim Arguments() As String
        Dim CharCounter As Integer = 0
        Dim InQuotes As Boolean = False

        ReDim Preserve Arguments(ArgumentCount)
        Arguments(ArgumentCount) = ""

        For intLoop1 As Integer = 0 To CommandLine.Length - 1
            If InQuotes = False And CommandLine.Chars(intLoop1) = Chr(34) Then
                InQuotes = True
            Else
                If InQuotes = False And CommandLine.Chars(intLoop1) = " " Then
                    If Arguments(ArgumentCount) <> "" Then
                        ArgumentCount += 1
                        ReDim Preserve Arguments(ArgumentCount)
                        Arguments(ArgumentCount) = ""
                    End If
                ElseIf InQuotes = True And CommandLine.Chars(intLoop1) = Chr(34) Then
                    InQuotes = False
                Else
                    Arguments(ArgumentCount) &= CommandLine.Chars(intLoop1)
                End If
            End If
        Next intLoop1
        Return Arguments

    End Function

    Private Sub ExecuteCommandLine(ByVal Arguments() As String)

        Dim intMode As Mode = Mode.Compress
        Dim strInput As String = ""
        Dim strOutput As String = ""
        Dim strErrors As String = ""

        For intLoop As Integer = 1 To Arguments.Length - 1
            Select Case Arguments(intLoop).ToLower
                Case "-input", "-i"
                    intLoop += 1
                    If intLoop <= Arguments.Length Then
                        strInput = Arguments(intLoop)
                        If strOutput = "" Then
                            If strInput.ToLower.EndsWith(".xpr") Then
                                strOutput = FixPath(Directory.GetCurrentDirectory)
                            Else
                                strOutput = FixPath(Directory.GetCurrentDirectory) & "Textures.xpr"
                            End If
                        End If
                    Else
                        strErrors &= "Missing param for input." & ControlChars.CrLf
                    End If
                Case "-output", "-o"
                    intLoop += 1
                    If intLoop <= Arguments.Length Then
                        strOutput = Arguments(intLoop)
                    Else
                        strErrors &= "Missing param for output." & ControlChars.CrLf
                    End If
                Case "-reqoutputpath", "-rp"
                    If FolderBrowserDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                        strOutput = FolderBrowserDialog.SelectedPath
                    Else
                        intMode = Mode.Abort
                    End If
                Case "-reqoutputfile", "-rf"
                    If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                        strOutput = SaveFileDialog.FileName
                    Else
                        intMode = Mode.Abort
                    End If
                Case Else
                    strErrors &= "Unknow param " & Chr(34) & Arguments(intLoop) & Chr(34) & "." & ControlChars.CrLf
            End Select
        Next intLoop

        If strErrors = "" And intMode <> Mode.Abort Then
            If File.Exists(strInput) Then
                intMode = Mode.Extract
                If Directory.Exists(strOutput) = False Then strErrors &= "Invalid output folder provided." & strOutput & ControlChars.CrLf
            ElseIf Directory.Exists(strInput) Then
                intMode = Mode.Compress
                If Directory.Exists(Path.GetDirectoryName(Path.GetFullPath(strOutput))) = False Then strErrors &= "Invalid output folder provided." & ControlChars.CrLf
            Else
                strErrors &= "Invalid file/folder provided for input." & ControlChars.CrLf
            End If
        End If

        If strErrors = "" Then
            Select Case intMode
                Case Mode.Compress
                    Show()
                    TabControl1.SelectedIndex = 1
                    CompressSourceFolder.Text = FixPath(strInput)
                    CompressDestinationFile.Text = strOutput
                    Application.DoEvents()
                    CompressAll(True)
                Case Mode.Extract
                    Show()
                    TabControl1.SelectedIndex = 0
                    ExtractSourceFile.Text = strInput
                    Application.DoEvents()
                    OpenXPR(True)
                    If Extract.Enabled = True Then
                        ExtractDestinationFolder.Text = FixPath(strOutput)
                        Application.DoEvents()
                        ExtractAll(True)
                    End If
                Case Mode.Abort
                    Me.Close()
            End Select
        Else
            MsgBox(strErrors, MsgBoxStyle.Critical)
        End If

    End Sub

    Private Sub Main_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        objDecoder.CloseXPR()
    End Sub

    Private Sub Link2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Link2.LinkClicked
        System.Diagnostics.Process.Start(Link2.Text)
    End Sub

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function GetShortPathName(ByVal lpszLongPath As String, ByVal lpszShortPath As StringBuilder, ByVal cchBuffer As Integer) As Integer
    End Function

    Private Function ConvertLongPathToShort(ByVal longPathName As String) As String
        Dim shortNameBuffer = New StringBuilder
        Dim size As Integer = GetShortPathName(longPathName, shortNameBuffer, shortNameBuffer.Capacity)
        If (size >= shortNameBuffer.Capacity) Then
            shortNameBuffer.Capacity = size + 1
            GetShortPathName(longPathName, shortNameBuffer, shortNameBuffer.Capacity)
        End If
        Return shortNameBuffer.ToString()
    End Function

    Private Function FixPath(ByVal Path As String) As String
        If Not Path.EndsWith("\") Then Return Path & "\"
        Return Path
    End Function

#Region "Extract Page"

    Private Sub ExtractSourceFile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExtractSourceFile.TextChanged
        ExtractLoadXPR.Enabled = ExtractSourceFile.Text <> ""
    End Sub

    Private Sub ExtractSelectSourceFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExtractSelectSourceFile.Click
        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            ExtractSourceFile.Text = OpenFileDialog.FileName
            OpenXPR(False)
        End If
    End Sub

    Private Sub ExtractLoadXPR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExtractLoadXPR.Click
        OpenXPR(False)
    End Sub

    Private Sub OpenXPR(ByVal FromCommandLine As Boolean)

        XPRTreeView.Nodes.Clear()
        Extract.Enabled = False
        ExtractSourceFile.Enabled = True
        ExtractSelectSourceFile.Enabled = True
        ExtractDestinationFolder.Enabled = False
        ExtractSelectDestinationFolder.Enabled = False

        If ExtractLoadXPR.Text = "Close XPR" Then
            If FileInfo IsNot Nothing Then
                FileInfo.Close()
                FileInfo.Dispose()
            End If
            objDecoder.CloseXPR()
            ExtractLoadXPR.Text = "Load XPR"
            Exit Sub
        End If

        If File.Exists(Path.GetFullPath(ExtractSourceFile.Text)) Then
            If Path.GetExtension(Path.GetFullPath(ExtractSourceFile.Text)).ToUpper <> ".XPR" Then
                MsgBox("Source file selected must be an XPR file.", MsgBoxStyle.Information)
                Exit Sub
            End If
        Else
            MsgBox("Source file selected could not be found.", MsgBoxStyle.Information)
        End If

        Try

            objDecoder.OpenXPR(Path.GetFullPath(ExtractSourceFile.Text))

            Dim objTreeNode As TreeNode = XPRTreeView.Nodes.Add(Path.GetFileName(ExtractSourceFile.Text))
            Dim objXPRNode As New XPRNode(XPRNode.NodeType.Folder, "", 0)
            objTreeNode.Tag = objXPRNode
            BuildXPRTreee(objTreeNode)

            ExtractDestinationFolder.Enabled = Not objDecoder.ProtectionEnabled
            ExtractSelectDestinationFolder.Enabled = Not objDecoder.ProtectionEnabled
            ExtractSourceFile.Enabled = False
            ExtractSelectSourceFile.Enabled = False
            ExtractLoadXPR.Text = "Close XPR"

            AddToMRU("ExtractSourceFile", ExtractSourceFile.Text)
            PopulateMRUCombo("ExtractSourceFile", ExtractSourceFile)

            If objDecoder.ProtectionEnabled = True Then
                If FromCommandLine = False Then
                    MsgBox("Sorry extraction is not allowed with protected XPR's, however you will be able to view images.", MsgBoxStyle.Information)
                Else
                    MsgBox("Sorry extraction is not allowed with protected XPR's", MsgBoxStyle.Information)
                    Me.Close()
                End If
            Else
                Extract.Enabled = ExtractDestinationFolder.Text <> ""
            End If

        Catch ex As Exception
            Extract.Enabled = False
            ExtractSourceFile.Enabled = True
            ExtractSelectSourceFile.Enabled = True
            ExtractDestinationFolder.Enabled = False
            ExtractSelectDestinationFolder.Enabled = False
            ExtractLoadXPR.Text = "Open XPR"
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub BuildXPRTreee(ByVal Node As TreeNode)

        For intLoop1 As Integer = 0 To objDecoder.FileCount - 1

            Dim strPaths As String() = objDecoder.FileName(intLoop1).Split("\")
            Dim objCurrentNode As TreeNode = Node
            Dim objXPRNode As XPRNode

            For intLoop2 As Integer = 0 To strPaths.Length - 2

                Dim strPath As String = strPaths(intLoop2)

                Dim blnPathFound As Boolean = False
                For Each objTreeNode As TreeNode In Node.Nodes
                    objXPRNode = objTreeNode.Tag
                    If objXPRNode.Type = XPRNode.NodeType.Folder AndAlso objXPRNode.Name = strPath Then
                        objCurrentNode = objTreeNode
                        blnPathFound = True
                    End If
                Next objTreeNode

                If blnPathFound = False Then
                    objXPRNode = New XPRNode(XPRNode.NodeType.Folder, strPath, 0)
                    objCurrentNode = objCurrentNode.Nodes.Add(strPath)
                    objCurrentNode.Tag = objXPRNode
                End If

            Next intLoop2

            objXPRNode = New XPRNode(XPRNode.NodeType.File, objDecoder.FileName(intLoop1), intLoop1)
            Dim strTemp As String = strPaths(strPaths.Length - 1)
            If ShowImageSizes.Checked Then
                Dim objImageFrame As ImageFrame = objDecoder.GetImage(objXPRNode.ImageID).Frames(0)
                strTemp &= String.Format(" ({0},{1})", objImageFrame.Frame.Width, objImageFrame.Frame.Height)
            End If

            objCurrentNode = objCurrentNode.Nodes.Add(strTemp)
            objCurrentNode.ImageIndex = 1
            objCurrentNode.SelectedImageIndex = 1
            objCurrentNode.Tag = objXPRNode

        Next intLoop1

        XPRTreeView.Sort()

    End Sub

    Private Sub ExtractSelectDestinationFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExtractSelectDestinationFolder.Click
        If FolderBrowserDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            ExtractDestinationFolder.Text = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub ExtractDestinationFolder_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExtractDestinationFolder.TextChanged
        Extract.Enabled = ExtractDestinationFolder.Text <> "" And ExtractLoadXPR.Text = "Close XPR"
    End Sub

    Private Sub Extract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Extract.Click
        ExtractAll(False)
    End Sub

    Private Sub ExtractAll(ByVal ExitOnExtract As Boolean)
        Try
            Extract.Enabled = False
            ExtractProgressBar.Value = 0
            ExtractProgressBar.Maximum = objDecoder.FileCount
            For intLoop1 As Integer = 0 To objDecoder.FileCount - 1
                ExtractProgressBar.Value = intLoop1 + 1
                Dim strPath As String = FixPath(Path.GetFullPath(ExtractDestinationFolder.Text))
                Dim strFile As String = objDecoder.FileName(intLoop1)
                Directory.CreateDirectory(Path.GetDirectoryName(strPath & strFile))
                objDecoder.SaveImage(intLoop1, strPath & strFile)
            Next intLoop1
            Extract.Enabled = True

            AddToMRU("ExtractSourceFile", ExtractSourceFile.Text)
            AddToMRU("ExtractDestinationFolder", ExtractDestinationFolder.Text)
            PopulateMRUCombo("ExtractSourceFile", ExtractSourceFile)
            PopulateMRUCombo("ExtractDestinationFolder", ExtractDestinationFolder)

            If ExitOnExtract Then
                Close()
            Else
                MsgBox("Extraction complete.")
            End If
        Catch ex As Exception
            Extract.Enabled = True
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub XPRTreeView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles XPRTreeView.DoubleClick
        If XPRTreeView.SelectedNode IsNot Nothing Then
            Dim objXPRNode As XPRNode = XPRTreeView.SelectedNode.Tag
            If objXPRNode.Type = XPRNode.NodeType.File Then
                Preview(objXPRNode)
            End If
        End If
    End Sub

    Private Sub XPRTreeView_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles XPRTreeView.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            XPRTreeView.SelectedNode = XPRTreeView.GetNodeAt(e.X, e.Y)
        End If
    End Sub

    Private Sub ContextMenuStrip_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip.Opening
        PreviewMenuItem.Enabled = False
        ExtractSelectedMenuItem.Enabled = False
        ExtractAllMenuItem.Enabled = Extract.Enabled
        If XPRTreeView.SelectedNode IsNot Nothing Then
            Dim objXPRNode As XPRNode = XPRTreeView.SelectedNode.Tag
            If objXPRNode.Type = XPRNode.NodeType.File Then
                PreviewMenuItem.Enabled = True
                ExtractSelectedMenuItem.Enabled = Extract.Enabled
            End If
        End If
    End Sub

    Private Sub PreviewMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviewMenuItem.Click
        Dim objXPRNode As XPRNode = XPRTreeView.SelectedNode.Tag
        Preview(objXPRNode)
    End Sub

    Private Sub XPRTreeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles XPRTreeView.AfterSelect
        If InstantPreview.Checked Then
            If XPRTreeView.SelectedNode IsNot Nothing Then
                Dim objXPRNode As XPRNode = XPRTreeView.SelectedNode.Tag
                If objXPRNode.Type = XPRNode.NodeType.File Then
                    Preview(objXPRNode)
                End If
            End If
        End If
    End Sub

    Private Sub Preview(ByVal XPRNode As XPRNode)
        If FileInfo IsNot Nothing Then
            If FileInfo.IsDisposed Then FileInfo = New Preview(objDecoder.GetImage(XPRNode.ImageID))
            FileInfo.RefreshPage(objDecoder.GetImage(XPRNode.ImageID))
        Else
            FileInfo = New Preview(objDecoder.GetImage(XPRNode.ImageID))
        End If
        If FileInfo.Visible = False Then FileInfo.Show(Me)
    End Sub

    Private Sub ExtractAllMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExtractAllMenuItem.Click
        ExtractAll(False)
    End Sub

    Private Sub ExtractSelectedMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExtractSelectedMenuItem.Click
        Extract.Enabled = False
        Dim objXPRNode As XPRNode = XPRTreeView.SelectedNode.Tag
        Dim strPath As String = ExtractDestinationFolder.Text
        If Not strPath.EndsWith("\") Then strPath &= "\"
        Dim strFile As String = objDecoder.FileName(objXPRNode.ImageID)
        Directory.CreateDirectory(Path.GetDirectoryName(strPath & strFile))
        objDecoder.SaveImage(objXPRNode.ImageID, strPath & strFile)
        Extract.Enabled = True
        MsgBox("Extraction complete.")
    End Sub

#End Region

#Region "Compress Page"

    Private Sub CompressSelectSourceFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompressSelectSourceFolder.Click
        If FolderBrowserDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            CompressSourceFolder.Text = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub CompressSourceFolder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompressSourceFolder.TextChanged
        Compress.Enabled = CompressSourceFolder.Text <> "" And CompressDestinationFile.Text <> ""
        Compressing.Text = "N/A"
    End Sub

    Private Sub CompressSelectDestinationFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompressSelectDestinationFile.Click
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            CompressDestinationFile.Text = SaveFileDialog.FileName
        End If
    End Sub

    Private Sub CompressDestinationFile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompressDestinationFile.TextChanged
        Compress.Enabled = CompressSourceFolder.Text <> "" And CompressDestinationFile.Text <> ""
        Compressing.Text = "N/A"
    End Sub

    Private Sub Compress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Compress.Click
        CompressAll(False)
    End Sub

    Private Sub CompressAll(ByVal ExitOnCompress As Boolean)

        Try
            strCompressLog = ""
            Compress.Enabled = False
            CompressViewLog.Enabled = False
            Compressing.Text = "Processing, please wait..."
            Application.DoEvents()

            Dim intExitCode As Integer
            Dim intErrors As Integer = 0
            Dim strTempFile As String = Path.GetTempFileName()
            Dim objFileStream As New FileStream(strTempFile, FileMode.Create)
            objFileStream.Write(My.Resources.XBMCTex, 0, My.Resources.XBMCTex.Length)
            objFileStream.Close()

            Dim objProcess As New Process()
            objProcess.StartInfo.CreateNoWindow = False
            objProcess.StartInfo.UseShellExecute = False
            objProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            objProcess.StartInfo.RedirectStandardOutput = True
            objProcess.StartInfo.RedirectStandardError = True
            objProcess.StartInfo.FileName = strTempFile

            Dim strArguments As String = "-i " & ConvertLongPathToShort(FixPath(CompressSourceFolder.Text))
            strArguments &= " -o " & ConvertLongPathToShort(FixPath(Path.GetDirectoryName(CompressDestinationFile.Text))) & Path.GetFileName(CompressDestinationFile.Text)

            objProcess.StartInfo.Arguments = strArguments
            objProcess.Start()

            Dim strOutput As String = ""
            While Not objProcess.StandardOutput.EndOfStream
                strOutput = objProcess.StandardOutput.ReadLine
                If InStr(strOutput, Chr(8)) > 0 Then
                    Dim strTemp As String = ""
                    For intLoop As Integer = 0 To strOutput.Length - 1
                        If strOutput.Chars(intLoop) = Chr(8) Then
                            strTemp = strTemp.Substring(0, strTemp.Length - 1)
                        Else
                            strTemp &= strOutput.Chars(intLoop)
                        End If
                    Next intLoop
                    strOutput = strTemp
                End If
                strCompressLog &= strOutput & ControlChars.CrLf
                If strOutput.StartsWith("ERROR:") Then intErrors += 1
                If strOutput.EndsWith("waste)") Then
                    Dim intPos As Integer = strOutput.IndexOf(":")
                    If intPos > -1 Then Compressing.Text = strOutput.Substring(0, intPos)
                End If
                Application.DoEvents()
            End While

            intExitCode = objProcess.ExitCode

            objProcess.WaitForExit()
            objProcess.Close()

            Compressing.Text = "N/A"
            CompressViewLog.Enabled = True
            Compress.Enabled = True

            AddToMRU("CompressSourceFolder", CompressSourceFolder.Text)
            AddToMRU("CompressDestinationFile", CompressDestinationFile.Text)
            PopulateMRUCombo("CompressSourceFolder", CompressSourceFolder)
            PopulateMRUCombo("CompressDestinationFile", CompressDestinationFile)

            If intErrors = 0 Then
                If ExitOnCompress Then
                    Close()
                Else
                    MsgBox("Compression complete.")
                End If
            Else
                If intExitCode = 0 Then
                    MsgBox("Compression complete with " & intErrors & " error(s). Please view log.", MsgBoxStyle.Exclamation)
                Else
                    MsgBox("Compression failed due to unexpected error.", MsgBoxStyle.Exclamation)
                End If
            End If

        Catch ex As Exception
            Compressing.Text = "N/A"
            Compress.Enabled = True
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub CompressViewLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompressViewLog.Click
        Dim objViewCompressLog As New ViewCompressLog
        objViewCompressLog.CompressLog.Text = strCompressLog
        objViewCompressLog.ShowDialog(Me)
    End Sub

#End Region

#Region "Shell Integration"

    Private Function ShellIntegrated() As Boolean
        Dim RegistryKey1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Classes\Directory\shell\XPRExpress.Compress")
        ShellIntegrated = RegistryKey1 IsNot Nothing
        If RegistryKey1 IsNot Nothing Then RegistryKey1.Close()
    End Function

    Private Sub ShellIntegration_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShellIntegration.Click

        If ShellIntegration.Checked Then
            Try

                Dim RegistryKey1 As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\Classes\Directory\shell\XPRExpress.CompressHere")
                RegistryKey1.SetValue(Nothing, "Compress folder to XPR Here")
                RegistryKey1.CreateSubKey("command").SetValue(Nothing, Chr(34) & Application.ExecutablePath & Chr(34) & " -input " & Chr(34) & "%1" & Chr(34))

                Dim RegistryKey1a As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\Classes\Directory\shell\XPRExpress.CompressTo")
                RegistryKey1a.SetValue(Nothing, "Compress folder to XPR To...")
                RegistryKey1a.CreateSubKey("command").SetValue(Nothing, Chr(34) & Application.ExecutablePath & Chr(34) & " -input " & Chr(34) & "%1" & Chr(34) & " -reqoutputfile")

                Dim RegistryKey2 As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\Classes\.xpr")
                RegistryKey2.SetValue(Nothing, "XPRExpress")

                Dim RegistryKey3 As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\Classes\XPRExpress")
                RegistryKey3.SetValue(Nothing, "XPRExpress")
                Dim RegistryKey4 As RegistryKey = RegistryKey3.CreateSubKey("shell")

                Dim RegistryKey5 As RegistryKey = RegistryKey4.CreateSubKey("XPRExpress.ExtractHere")
                RegistryKey5.SetValue(Nothing, "&Extract XPR Here")
                Dim RegistryKey6 As RegistryKey = RegistryKey5.CreateSubKey("command")
                RegistryKey6.SetValue(Nothing, Chr(34) & Application.ExecutablePath & Chr(34) & " -input " & Chr(34) & "%1" & Chr(34))

                Dim RegistryKey7 As RegistryKey = RegistryKey4.CreateSubKey("XPRExpress.ExtractTo")
                RegistryKey7.SetValue(Nothing, "&Extract XPR To...")
                Dim RegistryKey8 As RegistryKey = RegistryKey7.CreateSubKey("command")
                RegistryKey8.SetValue(Nothing, Chr(34) & Application.ExecutablePath & Chr(34) & " -input " & Chr(34) & "%1" & Chr(34) & " -reqoutputpath")

                RegistryKey1.Close()
                RegistryKey1a.Close()
                RegistryKey2.Close()
                RegistryKey3.Close()
                RegistryKey4.Close()
                RegistryKey5.Close()
                RegistryKey6.Close()
                RegistryKey7.Close()
                RegistryKey8.Close()

            Catch ex As Exception
                ShellIntegration.Checked = False
                MsgBox("Error: Could not set up shell integration.")
            End Try

        Else

            Try
                Registry.CurrentUser.DeleteSubKeyTree("Software\Classes\Directory\shell\XPRExpress.Compress")
                Registry.CurrentUser.DeleteSubKeyTree("Software\Classes\.xpr")
                Registry.CurrentUser.DeleteSubKeyTree("Software\Classes\XPRExpress")
            Catch ex As Exception
                ShellIntegration.Checked = True
                MsgBox("Error: Could not remove shell integration.")
            End Try

        End If
    End Sub

#End Region

    Private Sub DisableSplash_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisableSplash.CheckedChanged
        My.Settings.DisableSplash = DisableSplash.Checked
        My.Settings.Save()
    End Sub

    Private Sub InstantPreview_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstantPreview.CheckedChanged
        My.Settings.InstantPreview = InstantPreview.Checked
        My.Settings.Save()
    End Sub

    Private Sub ShowImageSizes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowImageSizes.CheckedChanged
        My.Settings.ShowImageSizes = ShowImageSizes.Checked
        My.Settings.Save()
    End Sub

End Class
