Public Class saveopt
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Dim apppath As String = Application.StartupPath()
            Dim pbcfg1 As String = apppath + "\statedata\usersave.builder.anamesave.pbsf"
            Dim objWriter1 As New System.IO.StreamWriter(pbcfg1)
            objWriter1.Write("")
            objWriter1.Close()
            Dim pbcfg2 As String = apppath + "\statedata\usersave.builder.urlsave.pbsf"
            Dim objWriter2 As New System.IO.StreamWriter(pbcfg2)
            objWriter2.Write(Form1.TextBox1.Text)
            objWriter2.Close()
            MessageBox.Show("Saved!", "Completed!")
        Catch ex As Exception
            MessageBox.Show("Save Failed!", "Error!")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim apppath As String = Application.StartupPath()
            Dim pbcfg1 As String = apppath + "\statedata\usersave.builder.anamesave.pbsf"
            Dim objWriter1 As New System.IO.StreamWriter(pbcfg1)
            objWriter1.Write(Form1.TextBox2.Text)
            objWriter1.Close()
            Dim pbcfg2 As String = apppath + "\statedata\usersave.builder.urlsave.pbsf"
            Dim objWriter2 As New System.IO.StreamWriter(pbcfg2)
            objWriter2.Write("")
            objWriter2.Close()
            MessageBox.Show("Saved!", "Completed!")
        Catch ex As Exception
            MessageBox.Show("Save Failed!", "Error!")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim apppath As String = Application.StartupPath()
            Dim pbcfg1 As String = apppath + "\statedata\usersave.builder.anamesave.pbsf"
            Dim objWriter1 As New System.IO.StreamWriter(pbcfg1)
            objWriter1.Write(Form1.TextBox2.Text)
            objWriter1.Close()
            Dim pbcfg2 As String = apppath + "\statedata\usersave.builder.urlsave.pbsf"
            Dim objWriter2 As New System.IO.StreamWriter(pbcfg2)
            objWriter2.Write(Form1.TextBox1.Text)
            objWriter2.Close()
            MessageBox.Show("Saved!", "Completed!")
        Catch ex As Exception
            MessageBox.Show("Save Failed!", "Error!")
        End Try
    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        Dim apppath As String = Application.StartupPath()
        Dim pbcfg1 As String = apppath + "\statedata\usersave.builder.anamesave.pbsf"
        Dim objWriter1 As New System.IO.StreamWriter(pbcfg1)
        objWriter1.Write("")
        objWriter1.Close()
        Dim pbcfg2 As String = apppath + "\statedata\usersave.builder.urlsave.pbsf"
        Dim objWriter2 As New System.IO.StreamWriter(pbcfg2)
        objWriter2.Write("")
        objWriter2.Close()
        MessageBox.Show("Cleared!", "Completed!")
    End Sub
End Class