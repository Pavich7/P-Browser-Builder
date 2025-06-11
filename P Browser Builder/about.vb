Public Class about
    Private Sub about_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Me.BackgroundImage = Image.FromFile(apppath + "\assets\about.png")
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Process.Start("http://github.com/Pavich7/P-Browser-Builder/")
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Process.Start("http://github.com/Pavich7/")
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Process.Start("http://github.com/Pavich7/P-Browser-Builder/blob/master/LICENSE.md")
    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/blob/master/EULA.md")
    End Sub
End Class