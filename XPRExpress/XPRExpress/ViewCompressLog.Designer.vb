<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewCompressLog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewCompressLog))
        Me.Extract = New System.Windows.Forms.Button
        Me.CompressLog = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Extract
        '
        Me.Extract.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Extract.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Extract.Location = New System.Drawing.Point(537, 309)
        Me.Extract.Name = "Extract"
        Me.Extract.Size = New System.Drawing.Size(75, 23)
        Me.Extract.TabIndex = 4
        Me.Extract.Text = "OK"
        Me.Extract.UseVisualStyleBackColor = True
        '
        'CompressLog
        '
        Me.CompressLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CompressLog.Location = New System.Drawing.Point(12, 12)
        Me.CompressLog.Multiline = True
        Me.CompressLog.Name = "CompressLog"
        Me.CompressLog.ReadOnly = True
        Me.CompressLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.CompressLog.Size = New System.Drawing.Size(600, 291)
        Me.CompressLog.TabIndex = 5
        Me.CompressLog.WordWrap = False
        '
        'ViewCompressLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 344)
        Me.Controls.Add(Me.CompressLog)
        Me.Controls.Add(Me.Extract)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ViewCompressLog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "View Compress Log"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Extract As System.Windows.Forms.Button
    Friend WithEvents CompressLog As System.Windows.Forms.TextBox
End Class
