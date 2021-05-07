<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.ContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PreviewMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ExtractAllMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExtractSelectedMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.Extract = New System.Windows.Forms.Button
        Me.Link2 = New System.Windows.Forms.LinkLabel
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog
        Me.ExtractProgressBar = New System.Windows.Forms.ProgressBar
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.ExtractDestinationFolder = New System.Windows.Forms.ComboBox
        Me.ExtractSourceFile = New System.Windows.Forms.ComboBox
        Me.ExtractLoadXPR = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.ExtractSelectDestinationFolder = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.ExtractSelectSourceFile = New System.Windows.Forms.Button
        Me.XPRTreeView = New System.Windows.Forms.TreeView
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.CompressDestinationFile = New System.Windows.Forms.ComboBox
        Me.CompressSourceFolder = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Compressing = New System.Windows.Forms.Label
        Me.CompressViewLog = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Compress = New System.Windows.Forms.Button
        Me.CompressSelectDestinationFile = New System.Windows.Forms.Button
        Me.CompressSelectSourceFolder = New System.Windows.Forms.Button
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.ShowImageSizes = New System.Windows.Forms.CheckBox
        Me.InstantPreview = New System.Windows.Forms.CheckBox
        Me.ShellIntegration = New System.Windows.Forms.CheckBox
        Me.DisableSplash = New System.Windows.Forms.CheckBox
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.HelpWebBrowser = New System.Windows.Forms.WebBrowser
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.AboutWebBrowser = New System.Windows.Forms.WebBrowser
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.ContextMenuStrip.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip
        '
        Me.ContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PreviewMenuItem, Me.ToolStripSeparator1, Me.ExtractAllMenuItem, Me.ExtractSelectedMenuItem})
        Me.ContextMenuStrip.Name = "ContextMenuStrip"
        Me.ContextMenuStrip.Size = New System.Drawing.Size(178, 76)
        '
        'PreviewMenuItem
        '
        Me.PreviewMenuItem.Name = "PreviewMenuItem"
        Me.PreviewMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.PreviewMenuItem.Text = "Preview"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(174, 6)
        '
        'ExtractAllMenuItem
        '
        Me.ExtractAllMenuItem.Name = "ExtractAllMenuItem"
        Me.ExtractAllMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ExtractAllMenuItem.Text = "Extract All"
        '
        'ExtractSelectedMenuItem
        '
        Me.ExtractSelectedMenuItem.Name = "ExtractSelectedMenuItem"
        Me.ExtractSelectedMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ExtractSelectedMenuItem.Text = "Extract Selected File"
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "folder_closed.png")
        Me.ImageList.Images.SetKeyName(1, "photo_scenery.png")
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.Filter = "XPR File (*.xpr)|*.xpr"
        '
        'Extract
        '
        Me.Extract.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Extract.Enabled = False
        Me.Extract.Location = New System.Drawing.Point(331, 186)
        Me.Extract.Name = "Extract"
        Me.Extract.Size = New System.Drawing.Size(75, 23)
        Me.Extract.TabIndex = 3
        Me.Extract.Text = "Extract"
        Me.Extract.UseVisualStyleBackColor = True
        '
        'Link2
        '
        Me.Link2.ActiveLinkColor = System.Drawing.Color.Blue
        Me.Link2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Link2.AutoSize = True
        Me.Link2.LinkColor = System.Drawing.Color.Blue
        Me.Link2.Location = New System.Drawing.Point(143, 260)
        Me.Link2.Name = "Link2"
        Me.Link2.Size = New System.Drawing.Size(163, 13)
        Me.Link2.TabIndex = 6
        Me.Link2.TabStop = True
        Me.Link2.Text = "http://www.teamblackbolt.co.uk"
        Me.Link2.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'FolderBrowserDialog
        '
        Me.FolderBrowserDialog.Description = "Please select desired folder..."
        '
        'ExtractProgressBar
        '
        Me.ExtractProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExtractProgressBar.Location = New System.Drawing.Point(9, 186)
        Me.ExtractProgressBar.Name = "ExtractProgressBar"
        Me.ExtractProgressBar.Size = New System.Drawing.Size(316, 23)
        Me.ExtractProgressBar.TabIndex = 7
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(421, 245)
        Me.TabControl1.TabIndex = 10
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ExtractDestinationFolder)
        Me.TabPage1.Controls.Add(Me.ExtractSourceFile)
        Me.TabPage1.Controls.Add(Me.ExtractLoadXPR)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.ExtractSelectDestinationFolder)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.ExtractSelectSourceFile)
        Me.TabPage1.Controls.Add(Me.XPRTreeView)
        Me.TabPage1.Controls.Add(Me.ExtractProgressBar)
        Me.TabPage1.Controls.Add(Me.Extract)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(413, 219)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Extract"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ExtractDestinationFolder
        '
        Me.ExtractDestinationFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExtractDestinationFolder.FormattingEnabled = True
        Me.ExtractDestinationFolder.Location = New System.Drawing.Point(9, 159)
        Me.ExtractDestinationFolder.Name = "ExtractDestinationFolder"
        Me.ExtractDestinationFolder.Size = New System.Drawing.Size(362, 21)
        Me.ExtractDestinationFolder.TabIndex = 21
        '
        'ExtractSourceFile
        '
        Me.ExtractSourceFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExtractSourceFile.FormattingEnabled = True
        Me.ExtractSourceFile.Location = New System.Drawing.Point(9, 22)
        Me.ExtractSourceFile.Name = "ExtractSourceFile"
        Me.ExtractSourceFile.Size = New System.Drawing.Size(282, 21)
        Me.ExtractSourceFile.TabIndex = 20
        '
        'ExtractLoadXPR
        '
        Me.ExtractLoadXPR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExtractLoadXPR.Enabled = False
        Me.ExtractLoadXPR.Location = New System.Drawing.Point(331, 21)
        Me.ExtractLoadXPR.Name = "ExtractLoadXPR"
        Me.ExtractLoadXPR.Size = New System.Drawing.Size(75, 23)
        Me.ExtractLoadXPR.TabIndex = 19
        Me.ExtractLoadXPR.Text = "Load XPR"
        Me.ExtractLoadXPR.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Source File"
        '
        'ExtractSelectDestinationFolder
        '
        Me.ExtractSelectDestinationFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExtractSelectDestinationFolder.Enabled = False
        Me.ExtractSelectDestinationFolder.Image = CType(resources.GetObject("ExtractSelectDestinationFolder.Image"), System.Drawing.Image)
        Me.ExtractSelectDestinationFolder.Location = New System.Drawing.Point(377, 158)
        Me.ExtractSelectDestinationFolder.Name = "ExtractSelectDestinationFolder"
        Me.ExtractSelectDestinationFolder.Size = New System.Drawing.Size(29, 23)
        Me.ExtractSelectDestinationFolder.TabIndex = 17
        Me.ExtractSelectDestinationFolder.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 143)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Destination Folder"
        '
        'ExtractSelectSourceFile
        '
        Me.ExtractSelectSourceFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExtractSelectSourceFile.Image = CType(resources.GetObject("ExtractSelectSourceFile.Image"), System.Drawing.Image)
        Me.ExtractSelectSourceFile.Location = New System.Drawing.Point(297, 21)
        Me.ExtractSelectSourceFile.Name = "ExtractSelectSourceFile"
        Me.ExtractSelectSourceFile.Size = New System.Drawing.Size(28, 23)
        Me.ExtractSelectSourceFile.TabIndex = 8
        Me.ExtractSelectSourceFile.UseVisualStyleBackColor = True
        '
        'XPRTreeView
        '
        Me.XPRTreeView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XPRTreeView.BackColor = System.Drawing.SystemColors.Window
        Me.XPRTreeView.ContextMenuStrip = Me.ContextMenuStrip
        Me.XPRTreeView.HotTracking = True
        Me.XPRTreeView.ImageIndex = 0
        Me.XPRTreeView.ImageList = Me.ImageList
        Me.XPRTreeView.Location = New System.Drawing.Point(9, 50)
        Me.XPRTreeView.Name = "XPRTreeView"
        Me.XPRTreeView.SelectedImageIndex = 0
        Me.XPRTreeView.Size = New System.Drawing.Size(397, 90)
        Me.XPRTreeView.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.CompressDestinationFile)
        Me.TabPage2.Controls.Add(Me.CompressSourceFolder)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.Compressing)
        Me.TabPage2.Controls.Add(Me.CompressViewLog)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Compress)
        Me.TabPage2.Controls.Add(Me.CompressSelectDestinationFile)
        Me.TabPage2.Controls.Add(Me.CompressSelectSourceFolder)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(413, 219)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Compress"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'CompressDestinationFile
        '
        Me.CompressDestinationFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CompressDestinationFile.FormattingEnabled = True
        Me.CompressDestinationFile.Location = New System.Drawing.Point(9, 62)
        Me.CompressDestinationFile.Name = "CompressDestinationFile"
        Me.CompressDestinationFile.Size = New System.Drawing.Size(362, 21)
        Me.CompressDestinationFile.TabIndex = 23
        '
        'CompressSourceFolder
        '
        Me.CompressSourceFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CompressSourceFolder.FormattingEnabled = True
        Me.CompressSourceFolder.Location = New System.Drawing.Point(9, 22)
        Me.CompressSourceFolder.Name = "CompressSourceFolder"
        Me.CompressSourceFolder.Size = New System.Drawing.Size(362, 21)
        Me.CompressSourceFolder.TabIndex = 22
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 86)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 23)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Compressing:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Compressing
        '
        Me.Compressing.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Compressing.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Compressing.Location = New System.Drawing.Point(88, 86)
        Me.Compressing.Name = "Compressing"
        Me.Compressing.Size = New System.Drawing.Size(318, 23)
        Me.Compressing.TabIndex = 20
        Me.Compressing.Text = "N/A"
        Me.Compressing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CompressViewLog
        '
        Me.CompressViewLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CompressViewLog.Enabled = False
        Me.CompressViewLog.Location = New System.Drawing.Point(250, 186)
        Me.CompressViewLog.Name = "CompressViewLog"
        Me.CompressViewLog.Size = New System.Drawing.Size(75, 23)
        Me.CompressViewLog.TabIndex = 19
        Me.CompressViewLog.Text = "View Log"
        Me.CompressViewLog.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Destination File"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Source Folder"
        '
        'Compress
        '
        Me.Compress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Compress.Enabled = False
        Me.Compress.Location = New System.Drawing.Point(331, 186)
        Me.Compress.Name = "Compress"
        Me.Compress.Size = New System.Drawing.Size(75, 23)
        Me.Compress.TabIndex = 8
        Me.Compress.Text = "Compress"
        Me.Compress.UseVisualStyleBackColor = True
        '
        'CompressSelectDestinationFile
        '
        Me.CompressSelectDestinationFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CompressSelectDestinationFile.Image = CType(resources.GetObject("CompressSelectDestinationFile.Image"), System.Drawing.Image)
        Me.CompressSelectDestinationFile.Location = New System.Drawing.Point(377, 61)
        Me.CompressSelectDestinationFile.Name = "CompressSelectDestinationFile"
        Me.CompressSelectDestinationFile.Size = New System.Drawing.Size(29, 23)
        Me.CompressSelectDestinationFile.TabIndex = 15
        Me.CompressSelectDestinationFile.UseVisualStyleBackColor = True
        '
        'CompressSelectSourceFolder
        '
        Me.CompressSelectSourceFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CompressSelectSourceFolder.Image = CType(resources.GetObject("CompressSelectSourceFolder.Image"), System.Drawing.Image)
        Me.CompressSelectSourceFolder.Location = New System.Drawing.Point(377, 21)
        Me.CompressSelectSourceFolder.Name = "CompressSelectSourceFolder"
        Me.CompressSelectSourceFolder.Size = New System.Drawing.Size(29, 23)
        Me.CompressSelectSourceFolder.TabIndex = 14
        Me.CompressSelectSourceFolder.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.ShowImageSizes)
        Me.TabPage5.Controls.Add(Me.InstantPreview)
        Me.TabPage5.Controls.Add(Me.ShellIntegration)
        Me.TabPage5.Controls.Add(Me.DisableSplash)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(413, 219)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Settings"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'ShowImageSizes
        '
        Me.ShowImageSizes.AutoSize = True
        Me.ShowImageSizes.Location = New System.Drawing.Point(6, 75)
        Me.ShowImageSizes.Name = "ShowImageSizes"
        Me.ShowImageSizes.Size = New System.Drawing.Size(150, 17)
        Me.ShowImageSizes.TabIndex = 13
        Me.ShowImageSizes.Text = "Show Image Sizes In Tree"
        Me.ShowImageSizes.UseVisualStyleBackColor = True
        '
        'InstantPreview
        '
        Me.InstantPreview.AutoSize = True
        Me.InstantPreview.Location = New System.Drawing.Point(6, 52)
        Me.InstantPreview.Name = "InstantPreview"
        Me.InstantPreview.Size = New System.Drawing.Size(102, 17)
        Me.InstantPreview.TabIndex = 5
        Me.InstantPreview.Text = "Instant Preview"
        Me.InstantPreview.UseVisualStyleBackColor = True
        '
        'ShellIntegration
        '
        Me.ShellIntegration.AutoSize = True
        Me.ShellIntegration.Location = New System.Drawing.Point(6, 29)
        Me.ShellIntegration.Name = "ShellIntegration"
        Me.ShellIntegration.Size = New System.Drawing.Size(105, 17)
        Me.ShellIntegration.TabIndex = 4
        Me.ShellIntegration.Text = "Shell Integration"
        Me.ShellIntegration.UseVisualStyleBackColor = True
        '
        'DisableSplash
        '
        Me.DisableSplash.AutoSize = True
        Me.DisableSplash.Location = New System.Drawing.Point(6, 6)
        Me.DisableSplash.Name = "DisableSplash"
        Me.DisableSplash.Size = New System.Drawing.Size(130, 17)
        Me.DisableSplash.TabIndex = 3
        Me.DisableSplash.Text = "Disable Splash Screen"
        Me.DisableSplash.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.HelpWebBrowser)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TabPage4.Size = New System.Drawing.Size(413, 219)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Help"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'HelpWebBrowser
        '
        Me.HelpWebBrowser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HelpWebBrowser.IsWebBrowserContextMenuEnabled = False
        Me.HelpWebBrowser.Location = New System.Drawing.Point(6, 6)
        Me.HelpWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.HelpWebBrowser.Name = "HelpWebBrowser"
        Me.HelpWebBrowser.Size = New System.Drawing.Size(401, 207)
        Me.HelpWebBrowser.TabIndex = 2
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.AboutWebBrowser)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(413, 219)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "About"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'AboutWebBrowser
        '
        Me.AboutWebBrowser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AboutWebBrowser.IsWebBrowserContextMenuEnabled = False
        Me.AboutWebBrowser.Location = New System.Drawing.Point(6, 6)
        Me.AboutWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.AboutWebBrowser.Name = "AboutWebBrowser"
        Me.AboutWebBrowser.Size = New System.Drawing.Size(401, 207)
        Me.AboutWebBrowser.TabIndex = 3
        '
        'SaveFileDialog
        '
        Me.SaveFileDialog.FileName = "Textures.xpr"
        Me.SaveFileDialog.Filter = "XPR File (*.xpr)|*.xpr"
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.XPRExpress.My.Resources.Resources.AppHeaderMain
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 287)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(452, 25)
        Me.Panel1.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 25)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Written by EqUiNoX"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 312)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Link2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(460, 346)
        Me.Name = "Main"
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "XPR Express - V1"
        Me.ContextMenuStrip.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Extract As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PreviewMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents Link2 As System.Windows.Forms.LinkLabel
    Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ExtractProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents XPRTreeView As System.Windows.Forms.TreeView
    Friend WithEvents ExtractSelectSourceFile As System.Windows.Forms.Button
    Friend WithEvents Compress As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CompressSelectDestinationFile As System.Windows.Forms.Button
    Friend WithEvents CompressSelectSourceFolder As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ExtractSelectDestinationFolder As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ExtractLoadXPR As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExtractSelectedMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExtractAllMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents CompressViewLog As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Compressing As System.Windows.Forms.Label
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents DisableSplash As System.Windows.Forms.CheckBox
    Friend WithEvents ShellIntegration As System.Windows.Forms.CheckBox
    Friend WithEvents HelpWebBrowser As System.Windows.Forms.WebBrowser
    Friend WithEvents ExtractSourceFile As System.Windows.Forms.ComboBox
    Friend WithEvents ExtractDestinationFolder As System.Windows.Forms.ComboBox
    Friend WithEvents CompressDestinationFile As System.Windows.Forms.ComboBox
    Friend WithEvents CompressSourceFolder As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents AboutWebBrowser As System.Windows.Forms.WebBrowser
    Friend WithEvents InstantPreview As System.Windows.Forms.CheckBox
    Friend WithEvents ShowImageSizes As System.Windows.Forms.CheckBox

End Class
