Imports System.Reflection.Emit
Imports System.IO.Compression

Public Class Form1
    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Timer1.Stop()
            Await Task.Delay(1000)
            Label1.Text = "Setting up build resources..."
            Await Task.Delay(1000)
            ProgressBar1.Value = 10
            Dim apppath As String = Application.StartupPath()
            Dim pbcfg As String = apppath + "\testspace\builderdata.pbcfg"
            If System.IO.File.Exists(pbcfg) = True Then
                Label1.Text = "Resource already setup!"
                ProgressBar1.Value = 100
                Await Task.Delay(3000)
                Application.Exit()
            Else
                System.IO.Directory.Delete(apppath + "\buildspace\quickmode", True)
                System.IO.Directory.CreateDirectory(apppath + "\buildspace\quickmode")
                ProgressBar1.Value = 20
                Dim zipPath As String = apppath + "\resourcepack\freshapp.zip"
                Dim extractPath As String = apppath + "\buildspace\quickmode"
                ZipFile.ExtractToDirectory(zipPath, extractPath)
                ProgressBar1.Value = 50
                System.IO.Directory.Delete(apppath + "\testspace", True)
                System.IO.Directory.CreateDirectory(apppath + "\testspace")
                ProgressBar1.Value = 70
                Dim zipPath1 As String = apppath + "\resourcepack\freshapp.zip"
                Dim extractPath1 As String = apppath + "\testspace"
                ZipFile.ExtractToDirectory(zipPath1, extractPath1)
                ProgressBar1.Value = 100
                Label1.Text = "Completed!"
                Await Task.Delay(3000)
                Application.Exit()
            End If
        Catch ex As Exception
            MessageBox.Show("Fatal Error! Cannot setup resource!", "Fatal Error!")
            Application.Exit()
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub
End Class