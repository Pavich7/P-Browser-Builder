﻿Public Class about
    Private Sub about_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Process.Start("http://github.com/Pavich7/P-Browser-Builder/")
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Process.Start("http://pavichdev.ddns.net/Home.html#feedbackintro")
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Process.Start("http://github.com/Pavich7/P-Browser-Builder/blob/master/LICENSE.txt")
    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click
        Process.Start("http://pavichdev.ddns.net/download/documents/p-browser-builder-eula.pdf")
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim apppath As String = Application.StartupPath()
        Dim pbcfg1 As String = apppath + "\statedata\setting.builder.datacol.pbcfg"
        Dim objWriter1 As New System.IO.StreamWriter(pbcfg1)
        objWriter1.Write("False")
        objWriter1.Close()
        MessageBox.Show("Optional data collection is completely disabled.", "Success!")
    End Sub
End Class