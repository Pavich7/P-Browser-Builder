Public Class customscript
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Process.Start("https://www.thoughtco.com/how-to-use-processstart-in-vbnet-3424455")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.tempScript = TextBox1.Text
        Me.Close()
    End Sub

    Private Sub customscript_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.tempScript
    End Sub
End Class