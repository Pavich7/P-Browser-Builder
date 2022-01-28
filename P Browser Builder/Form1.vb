Imports System.IO.StreamWriter
Imports System.IO.Compression
Imports System.IO
Imports CefSharp
Imports CefSharp.WinForms
Public Class Form1
    Private WithEvents Browser As ChromiumWebBrowser
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label7.Visible = True
        ProgressBar1.Visible = True
        Dim apppath As String = Application.StartupPath()
        Dim pbcfg As String = apppath + "\resource\testspace\builderdata.pbcfg"
        Dim testapp As String = apppath + "\resource\testspace\P Browser App.exe"
        ProgressBar1.Value = 20
        If System.IO.File.Exists(pbcfg) = True Then
            ProgressBar1.Value = 50
            Dim objWriter As New System.IO.StreamWriter(pbcfg)
            objWriter.Write(TextBox1.Text)
            objWriter.Close()
            ProgressBar1.Value = 100
            MessageBox.Show("Build Completed! Click continue to test app.", "Build Completed!")
            Process.Start(testapp)
            Label7.Visible = False
            ProgressBar1.Visible = False
        Else
            MessageBox.Show("Build Failed! Incomplete or corrupted Data please reinstall builder.", "Build Failed!")
            Label7.Visible = False
            ProgressBar1.Visible = False
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.Click
        Button1.Enabled = False
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.Click
        Button1.Enabled = False
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        Button1.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If RadioButton1.Checked = True Then
            Label7.Visible = True
            ProgressBar1.Visible = True
            Dim apppath As String = Application.StartupPath()
            Dim pbcfg As String = apppath + "\resource\buildspace\quickmode\builderdata.pbcfg"
            Dim buildedapp As String = apppath + "\binary\P Browser App.exe"
            System.IO.Directory.Delete(apppath + "\binary", True)
            System.IO.Directory.CreateDirectory(apppath + "\binary")
            ProgressBar1.Value = 20
            If System.IO.File.Exists(pbcfg) = True Then
                Dim objWriter As New System.IO.StreamWriter(pbcfg)
                objWriter.Write(TextBox1.Text)
                objWriter.Close()
                ProgressBar1.Value = 50
                My.Computer.FileSystem.CopyDirectory(apppath + "\resource\buildspace\quickmode", apppath + "\binary", True)
                ProgressBar1.Value = 100
                MessageBox.Show("Build Completed! Click OK to continue.", "Build Completed!")
                If CheckBox1.Checked = True Then
                    Process.Start(buildedapp)
                End If
                If CheckBox2.Checked Then
                    Process.Start(apppath + "\binary")
                End If
                Label7.Visible = False
                ProgressBar1.Visible = False
            Else
                MessageBox.Show("Build Failed! Incomplete or corrupted Data please reinstall builder.", "Build Failed!")
                Label7.Visible = False
                ProgressBar1.Visible = False
            End If
        ElseIf RadioButton2.Checked = True Then
            Dim apppath As String = Application.StartupPath()
            Dim zipPath As String = apppath + "\resource\resourcepack\freshapp.zip"
            Dim extractPath As String = apppath + "\resource\buildspace\cleanmode"
            ZipFile.ExtractToDirectory(zipPath, extractPath)
            Label7.Visible = True
            ProgressBar1.Visible = True
            Dim pbcfg As String = apppath + "\resource\buildspace\cleanmode\builderdata.pbcfg"
            Dim buildedapp As String = apppath + "\binary\P Browser App.exe"
            System.IO.Directory.Delete(apppath + "\binary", True)
            System.IO.Directory.CreateDirectory(apppath + "\binary")
            ProgressBar1.Value = 20
            If System.IO.File.Exists(pbcfg) = True Then
                Dim objWriter As New System.IO.StreamWriter(pbcfg)
                objWriter.Write(TextBox1.Text)
                objWriter.Close()
                ProgressBar1.Value = 50
                My.Computer.FileSystem.CopyDirectory(apppath + "\resource\buildspace\cleanmode", apppath + "\binary", True)
                ProgressBar1.Value = 70
                System.IO.Directory.Delete(apppath + "\resource\buildspace\cleanmode", True)
                System.IO.Directory.CreateDirectory(apppath + "\resource\buildspace\cleanmode")
                ProgressBar1.Value = 100
                MessageBox.Show("Build Completed! Click OK to continue.", "Build Completed!")
                If CheckBox1.Checked = True Then
                    Process.Start(buildedapp)
                End If
                If CheckBox2.Checked Then
                    Process.Start(apppath + "\binary")
                End If
                Label7.Visible = False
                ProgressBar1.Visible = False
            Else
                MessageBox.Show("Build Failed! Incomplete or corrupted Data please reinstall builder.", "Build Failed!")
                Label7.Visible = False
                ProgressBar1.Visible = False
            End If
        Else
            MessageBox.Show("Please select Build Mode!", "Build Failed!")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim apppath As String = Application.StartupPath()
            System.IO.Directory.Delete(apppath + "\binary", True)
            System.IO.Directory.CreateDirectory(apppath + "\binary")
            MessageBox.Show("Cleanup completed!", "Completed!")
        Catch ex As Exception
            MessageBox.Show("Please close builded app first before perform this action.", "Failed!")
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim setting As New CefSettings
        setting.RemoteDebuggingPort = 8088
        CefSharp.Cef.Initialize(setting)
        Browser = New ChromiumWebBrowser("")
        Panel2.Controls.Add(Browser)
        Label7.Visible = False
        ProgressBar1.Visible = False
        TextBox2.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = False
        EditToolStripMenuItem.Enabled = False
        GuideToolStripMenuItem.Enabled = False
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub SupportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupportToolStripMenuItem.Click
        Process.Start("https://pavichdev.ddns.net/Home.html#feedbackintro")
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        about.Show()
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        CheckBox1.Checked = False
        CheckBox2.Checked = False
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Browser.Load(TextBox1.Text)
    End Sub
End Class
