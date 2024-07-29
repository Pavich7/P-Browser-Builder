Public Class splash
    Private Sub splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Label1.Text = "Initializing Core Library..."
    End Sub

    Private Sub Label36_Click(sender As Object, e As EventArgs) Handles Label36.Click
        Application.Exit()
    End Sub
End Class