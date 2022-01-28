Public Class Form1
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Application.Exit()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Process.Start("https://pavichdev.ddns.net/Home.html#feedbackintro")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath = Application.StartupPath
        WebBrowser1.Navigate(apppath + "/documents/p-browser-builder-eula.pdf")
    End Sub
End Class
