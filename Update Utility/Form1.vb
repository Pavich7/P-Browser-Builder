Imports System.Net
Imports System.Reflection.Emit

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Dim rescheck As String = apppath + "\resource"
        If Not System.IO.Directory.Exists(rescheck) Then
            MessageBox.Show("Resource not found!" + vbNewLine + "Please install builder resource via resource menu.", "Resource not found!")
        End If
        Try
            Dim fileReader1 As System.IO.StreamReader
            Dim fileReader2 As System.IO.StreamReader
            Dim fileReader4 As System.IO.StreamReader
            fileReader1 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\testspace\pkgData\pkgcef.pbad")
            fileReader2 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\testspace\pkgData\pkgchrome.pbad")
            fileReader4 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\metadata\version.txt")
            Dim stringReader1 As String
            Dim stringReader2 As String
            Dim stringReader4 As String
            stringReader1 = fileReader1.ReadLine()
            stringReader2 = fileReader2.ReadLine()
            stringReader4 = fileReader4.ReadLine()
            Label4.Text = "CefSharp : " + stringReader1
            Label5.Text = "Chromium : " + stringReader2
            Label9.Text = "Version : " + stringReader4
        Catch ex As Exception
            MessageBox.Show("Cannot load data from resource!" + vbNewLine + "Please reinstall builder resource via resource menu.", "Resource Error!")
        End Try
        Button6.Enabled = False
        TextBox2.Enabled = False
        Label1.Enabled = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim apppath As String = Application.StartupPath()
        System.IO.Directory.Delete(apppath + "\UUbinary", True)
        System.IO.Directory.CreateDirectory(apppath + "\UUbinary")
        If FolderBrowserDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            TextBox2.Text = FolderBrowserDialog1.SelectedPath
            Label7.Text = "Copying Content..."
            ProgressBar1.Style = ProgressBarStyle.Marquee
            cpworker.RunWorkerAsync()
        End If
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles cpworker.RunWorkerCompleted
        ProgressBar1.Style = ProgressBarStyle.Blocks
        Label7.Text = "Copy Completed!"
    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles cpworker.DoWork
        Try
            Dim apppath As String = Application.StartupPath()
            My.Computer.FileSystem.CopyDirectory(FolderBrowserDialog1.SelectedPath, apppath + "\UUbinary", True)
        Catch ex As Exception
            MessageBox.Show("Could not attempt to copy build!" + vbNewLine + ex.Message, "Error!")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim apppath As String = Application.StartupPath()
        System.IO.Directory.Delete(apppath + "\UUbinary", True)
        System.IO.Directory.CreateDirectory(apppath + "\UUbinary")
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        TextBox2.Text = ""
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            Button6.Enabled = True
            TextBox2.Enabled = True
            Label1.Enabled = True
        Else
            Button6.Enabled = False
            TextBox2.Enabled = False
            Label1.Enabled = False
        End If
    End Sub
End Class
