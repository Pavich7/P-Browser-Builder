<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class errorencounter
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(errorencounter))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.ErrorImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(234, 10)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(95, 86)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Light", 25.25!)
        Me.Label1.Location = New System.Drawing.Point(124, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(322, 46)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Fatal Error Encounted"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Light", 15.25!)
        Me.Label2.Location = New System.Drawing.Point(136, 153)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(298, 30)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "An unknown error were occured"
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Segoe UI Light", 12.25!)
        Me.Button3.Location = New System.Drawing.Point(385, 276)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(166, 42)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Report to PavichDev"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Segoe UI Light", 12.25!)
        Me.Button1.Location = New System.Drawing.Point(198, 276)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(166, 42)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Exit Builder"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Segoe UI Light", 12.25!)
        Me.Button4.Location = New System.Drawing.Point(12, 276)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(166, 42)
        Me.Button4.TabIndex = 8
        Me.Button4.Text = "Restart Builder"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.Label3.Location = New System.Drawing.Point(109, 198)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(358, 57)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Restart Builer                     Restart P Browser Builer" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Exit Builer         " &
    "                 Exit P Browser Builer" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Report to PavichDev          Restart and" &
    " Open Report Forms"
        '
        'errorencounter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(563, 330)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "errorencounter"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "errorencounter"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Label3 As Label
End Class
