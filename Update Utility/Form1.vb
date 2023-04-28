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
            Dim fileReader3 As System.IO.StreamReader
            Dim fileReader4 As System.IO.StreamReader
            fileReader1 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\buildspace\quickmode\pkgData\cefre.pbad")
            fileReader2 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\buildspace\quickmode\pkgData\cefwinf.pbad")
            fileReader3 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\buildspace\quickmode\pkgData\cefcomn.pbad")
            fileReader4 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\metadata\version.txt")
            Dim stringReader1 As String
            Dim stringReader2 As String
            Dim stringReader3 As String
            Dim stringReader4 As String
            stringReader1 = fileReader1.ReadLine()
            stringReader2 = fileReader2.ReadLine()
            stringReader3 = fileReader3.ReadLine()
            stringReader4 = fileReader4.ReadLine()
            Label4.Text = "Redist : " + stringReader1
            Label5.Text = "WinForms : " + stringReader2
            Label8.Text = "Common : " + stringReader3
            Label9.Text = "Version : " + stringReader4
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim apppath As String = Application.StartupPath()
        System.IO.Directory.Delete(apppath + "\UUbinary", True)
        System.IO.Directory.CreateDirectory(apppath + "\UUbinary")
        System.IO.Directory.Delete(apppath + "\buildcache\appicns", True)
        System.IO.Directory.CreateDirectory(apppath + "\buildcache\appicns")
        If FolderBrowserDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            TextBox2.Text = FolderBrowserDialog1.SelectedPath
            My.Computer.FileSystem.CopyDirectory(FolderBrowserDialog1.SelectedPath, apppath + "\UUbinary", True)
            MessageBox.Show("Copy complete")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim apppath As String = Application.StartupPath()
        System.IO.Directory.Delete(apppath + "\UUbinary", True)
        System.IO.Directory.CreateDirectory(apppath + "\UUbinary")
    End Sub
End Class
