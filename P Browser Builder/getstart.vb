Public Class getstart
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/wiki/P-Browser-Builder-Guild")
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/issues")
    End Sub

    Private Sub Label56_Click(sender As Object, e As EventArgs) Handles Label56.Click
        Dim apppath As String = Application.StartupPath()
        Dim rscheck As String = apppath + "\resource"
        If Not System.IO.Directory.Exists(rscheck) Then
            MessageBox.Show("Download resource for full experience." & vbNewLine & "You can download by click Download button below News Feed.", "Recommendation")
        End If
        Try
            Dim fileReader As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\sample\sample1.pbproj")
            Form1.TextBox2.Text = fileReader.ReadLine()
            Form1.TextBox1.Text = fileReader.ReadLine()
            Form1.TextBox3.Text = fileReader.ReadLine()
            Form1.TextBox4.Text = fileReader.ReadLine()
            Form1.CheckBox5.Checked = fileReader.ReadLine()
            Form1.CheckBox6.Checked = fileReader.ReadLine()
            Form1.CheckBox7.Checked = fileReader.ReadLine()
            Form1.TextBox5.Text = fileReader.ReadLine()
            Form1.CheckBox3.Checked = fileReader.ReadLine()
            welcomemessage.TextBox1.Text = fileReader.ReadLine()
            welcomemessage.TextBox2.Text = fileReader.ReadLine()
            Form1.CheckBox8.Checked = fileReader.ReadLine()
            Form1.RadioButton2.Checked = fileReader.ReadLine()
            Form1.RadioButton3.Checked = fileReader.ReadLine()
            'aname
            'url
            'wwin
            'hwin
            'fixwin
            'aotwin
            'hwico
            'opaval
            'welena
            'msgti
            'msgde
            'conxme
            'm1chk
            'm2chk
            Form1.Enabled = True
            Form1.ProjnameToolStripMenuItem.Text = Form1.TextBox2.Text
            Form1.WindowState = FormWindowState.Normal
            fileReader.Close()
            welcome.realclose = False
            welcome.Close()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Load Failed!" & vbNewLine & "Project is not compatiable or corrupt!" & vbNewLine & ex.Message, "Error!")
        End Try
    End Sub
End Class