Public Class about
    Private Sub about_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Process.Start("https://pavichdev.ddns.net/download/documents/p-browser-builder-eula.pdf")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/blob/master/LICENSE.txt")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Process.Start("https://pavichdev.ddns.net/Home.html#feedbackintro")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/")
    End Sub
End Class