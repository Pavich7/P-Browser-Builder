﻿Public Class splash
    Private Sub splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Randomize()
        Dim sprand As Integer = CInt(Math.Ceiling(Rnd() * 3))
        Dim apppath As String = Application.StartupPath()
        Me.BackgroundImage = Image.FromFile(apppath + "\assets\splash_collection\splash" & sprand & ".png")
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()
    End Sub
End Class