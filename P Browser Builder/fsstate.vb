Public Class fsstate
    Dim stage As Integer = 0
    Private Sub fsstate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        welcome.Enabled = False
        Label2.Visible = False
        Label4.Visible = False
        CheckBox1.Visible = False
        CheckBox2.Visible = False
        TabPage2.Enabled = False
        TabPage3.Enabled = False
        Dim apppath As String = Application.StartupPath()
        TabPage1.BackgroundImage = Image.FromFile(apppath + "\assets\setup.png")
    End Sub

    Private Sub Label29_Click(sender As Object, e As EventArgs) Handles Label29.Click
        If CheckBox3.Checked = True And CheckBox4.Checked = True Then
            Dim result As DialogResult = MessageBox.Show("I have read and agree with these agreement.", "Agreement", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If (result = DialogResult.Yes) Then
                stage = 2
                TabPage3.Enabled = True
                TabControl1.SelectedTab = TabPage3
                TabPage2.Enabled = False
            End If
        Else
            MessageBox.Show("You need to agree to both agreement!", "Agreement")
        End If
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        TabPage1.Enabled = True
        TabControl1.SelectedTab = TabPage1
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Dim result As DialogResult = MessageBox.Show("Do you wish to exit P Browser Builder?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If (result = DialogResult.Yes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        stage = 1
        TabPage2.Enabled = True
        TabControl1.SelectedTab = TabPage2
        TabPage1.Enabled = False
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click, Label13.Click
        Dim apppath As String = Application.StartupPath()
        settings.save("infsstate", "False")
        welcome.Enabled = True
        Me.Close()
    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Enter
        If stage = 0 Then
            TabControl1.SelectedTab = TabPage1
        End If
    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Enter
        If stage = 0 Then
            TabControl1.SelectedTab = TabPage1
        ElseIf stage = 1 Then
            TabControl1.SelectedTab = TabPage2
        End If
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/blob/master/EULA.md")
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/blob/master/LICENSE.md")
    End Sub
End Class