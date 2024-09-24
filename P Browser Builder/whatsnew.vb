Public Class whatsnew
    Private Sub whatsnew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Me.BackgroundImage = Image.FromFile(apppath + "\imgassets\whatsnew.png")
    End Sub
End Class