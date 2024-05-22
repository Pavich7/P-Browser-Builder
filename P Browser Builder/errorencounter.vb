Public Class errorencounter
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Application.Restart()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/issues/new/choose")
        Application.Restart()
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property
End Class