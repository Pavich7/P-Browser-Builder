<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class about
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label30
        '
        Me.Label30.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label30.Font = New System.Drawing.Font("Segoe UI Semilight", 10.0!, System.Drawing.FontStyle.Underline)
        Me.Label30.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label30.Location = New System.Drawing.Point(238, 316)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(124, 19)
        Me.Label30.TabIndex = 21
        Me.Label30.Text = "License Agreement"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semilight", 10.0!, System.Drawing.FontStyle.Underline)
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(238, 339)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 19)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Source Code License"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semilight", 10.0!, System.Drawing.FontStyle.Underline)
        Me.Label3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label3.Location = New System.Drawing.Point(238, 362)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 19)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "GitHub Repository"
        '
        'about
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(670, 414)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label30)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "about"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "About P Browser Builder"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label30 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
End Class
