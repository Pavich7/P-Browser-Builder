Public Class splash
    Private Sub splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Randomize()
        Dim sprand As Integer = CInt(Math.Ceiling(Rnd() * 3))
        Dim apppath As String = Application.StartupPath()
        Me.BackgroundImage = Image.FromFile(apppath + "\imgassets\splash_collection\splash" & sprand & ".png")
        Timer1.Start()
    End Sub

    Private Sub Label36_Click(sender As Object, e As EventArgs) Handles Label36.Click
        Application.Exit()
    End Sub
End Class