Imports System.IO.Compression
Public Class Form1
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Process.Start("https://pavichdev.ddns.net/download/documents/p-browser-builder-eula.pdf")
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Button1.Enabled = True
        End If
        If CheckBox1.Checked = False Then
            Button1.Enabled = False
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Enabled = False
        Label4.Visible = False
        ProgressBar1.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CheckBox1.Visible = False
        Label3.Visible = False
        Button1.Visible = False
        Label4.Visible = True
        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        Dim apppath As String = Application.StartupPath()
        Dim zipPath As String = apppath + "\installresource\installresource.zip"
        Dim extractPath As String = apppath
        Try
            ZipFile.ExtractToDirectory(zipPath, extractPath)
        Catch ex As Exception
            MessageBox.Show("Error! Please uninstall older version first!", "Installation Exist Error!")
            Process.Start(apppath)
            Application.Exit()
        End Try
        MessageBox.Show("Installation Complete!", "Complete!")
        Process.Start(apppath + "\P Browser Builder.exe")
        Application.Exit()
    End Sub
End Class
