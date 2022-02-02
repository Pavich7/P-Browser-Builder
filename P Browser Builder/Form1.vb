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
        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter your websites URL.", "Build Failed!")
        Else
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
        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter your websites URL.", "Build Failed!")
        ElseIf TextBox2.Text = "" Then
            MessageBox.Show("Please enter your application name.", "Build Failed!")
        Else
            If RadioButton1.Checked = True Then
                Label7.Visible = True
                ProgressBar1.Visible = True
                Dim apppath As String = Application.StartupPath()
                Dim pbcfg As String = apppath + "\resource\buildspace\quickmode\builderdata.pbcfg"
                System.IO.Directory.Delete(apppath + "\binary", True)
                System.IO.Directory.CreateDirectory(apppath + "\binary")
                ProgressBar1.Value = 20
                If System.IO.File.Exists(pbcfg) = True Then
                    Dim objWriter As New System.IO.StreamWriter(pbcfg)
                    objWriter.Write(TextBox1.Text)
                    objWriter.Close()
                    ProgressBar1.Value = 50
                    My.Computer.FileSystem.CopyDirectory(apppath + "\resource\buildspace\quickmode", apppath + "\binary", True)
                    My.Computer.FileSystem.RenameFile(apppath + "\binary\P Browser App.exe", TextBox2.Text + ".exe")
                    ProgressBar1.Value = 100
                    MessageBox.Show("Build Completed! Click OK to continue.", "Build Completed!")
                    If CheckBox1.Checked = True Then
                        Process.Start(apppath + "\binary\" + TextBox2.Text + ".exe")
                    End If
                    If CheckBox2.Checked Then
                        Process.Start(apppath + "\binary\")
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
                System.IO.Directory.Delete(apppath + "\resource\buildspace\cleanmode", True)
                System.IO.Directory.CreateDirectory(apppath + "\resource\buildspace\cleanmode")
                Dim zipPath As String = apppath + "\resource\resourcepack\freshapp.zip"
                Dim extractPath As String = apppath + "\resource\buildspace\cleanmode"
                ZipFile.ExtractToDirectory(zipPath, extractPath)
                Label7.Visible = True
                ProgressBar1.Visible = True
                Dim pbcfg As String = apppath + "\resource\buildspace\cleanmode\builderdata.pbcfg"
                System.IO.Directory.Delete(apppath + "\binary", True)
                System.IO.Directory.CreateDirectory(apppath + "\binary")
                ProgressBar1.Value = 20
                If System.IO.File.Exists(pbcfg) = True Then
                    Dim objWriter As New System.IO.StreamWriter(pbcfg)
                    objWriter.Write(TextBox1.Text)
                    objWriter.Close()
                    ProgressBar1.Value = 50
                    My.Computer.FileSystem.CopyDirectory(apppath + "\resource\buildspace\cleanmode", apppath + "\binary", True)
                    My.Computer.FileSystem.RenameFile(apppath + "\binary\P Browser App.exe", TextBox2.Text + ".exe")
                    ProgressBar1.Value = 70
                    System.IO.Directory.Delete(apppath + "\resource\buildspace\cleanmode", True)
                    System.IO.Directory.CreateDirectory(apppath + "\resource\buildspace\cleanmode")
                    ProgressBar1.Value = 100
                    MessageBox.Show("Build Completed! Click OK to continue.", "Build Completed!")
                    If CheckBox1.Checked = True Then
                        Process.Start(apppath + "\binary\" + TextBox2.Text + ".exe")
                    End If
                    If CheckBox2.Checked Then
                        Process.Start(apppath + "\binary\")
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
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Label7.Visible = True
            Label7.Text = "Cleaning in progress..."
            ProgressBar1.Visible = True
            ProgressBar1.Value = 20
            Dim apppath As String = Application.StartupPath()
            System.IO.Directory.Delete(apppath + "\binary", True)
            ProgressBar1.Value = 50
            System.IO.Directory.CreateDirectory(apppath + "\binary")
            ProgressBar1.Value = 100
            MessageBox.Show("Cleanup completed!", "Completed!")
            Label7.Visible = False
            ProgressBar1.Visible = False
        Catch ex As Exception
            MessageBox.Show("Please close builded app first before perform this action.", "Failed!")
            Label7.Visible = False
            Label7.Text = "Building in progress..."
            ProgressBar1.Visible = False
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Dim setting As New CefSettings
        setting.RemoteDebuggingPort = 8088
        CefSharp.Cef.Initialize(setting)
        Browser = New ChromiumWebBrowser("")
        Panel2.Controls.Add(Browser)
        Button4.Enabled = False
        Button5.Enabled = False
        EditToolStripMenuItem.Enabled = False
        GuideToolStripMenuItem.Enabled = False
        Label15.Visible = False
        Label7.Visible = True
        Label7.Text = "Fetching in progress..."
        ProgressBar1.Visible = True
        System.IO.Directory.Delete(apppath + "\nfcache", True)
        System.IO.Directory.CreateDirectory(apppath + "\nfcache")
        Timer1.Start()
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

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text = "" Then
            Label2.Text = "Application name (example.exe)"
        Else
            Label2.Text = "Application name (" + TextBox2.Text + ".exe)"
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Try
            ProgressBar1.Value = 10
            Dim apppath As String = Application.StartupPath()
            My.Computer.Network.DownloadFile("https://pavichdev.ddns.net/service/app.pavichdev.pbrowserbuilder/newsfeed/nf1_title.txt", apppath + "\nfcache\nf1_title.txt")
            My.Computer.Network.DownloadFile("https://pavichdev.ddns.net/service/app.pavichdev.pbrowserbuilder/newsfeed/nf1_desc.txt", apppath + "\nfcache\nf1_desc.txt")
            My.Computer.Network.DownloadFile("https://pavichdev.ddns.net/service/app.pavichdev.pbrowserbuilder/newsfeed/nf1_date.txt", apppath + "\nfcache\nf1_date.txt")
            ProgressBar1.Value = 50
            Dim fileReader1 As System.IO.StreamReader
            Dim fileReader2 As System.IO.StreamReader
            Dim fileReader3 As System.IO.StreamReader
            fileReader1 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\nfcache\nf1_title.txt")
            fileReader2 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\nfcache\nf1_desc.txt")
            fileReader3 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\nfcache\nf1_date.txt")
            Dim stringReader1 As String
            Dim stringReader2 As String
            Dim stringReader3 As String
            stringReader1 = fileReader1.ReadLine()
            stringReader2 = fileReader2.ReadLine()
            stringReader3 = fileReader3.ReadLine()
            Label12.Text = stringReader1
            Label13.Text = stringReader2
            Label14.Text = stringReader3
            ProgressBar1.Value = 100
            Label7.Visible = False
            ProgressBar1.Visible = False
            Label7.Text = "Building in progress..."
        Catch ex As Exception
            Label7.Visible = False
            ProgressBar1.Visible = False
            Label15.Visible = True
            Label14.Visible = False
            Label12.Text = "Fetching failure!"
            Label13.Text = "Can't fetch news feed with server."
        End Try
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Dim result As DialogResult = MessageBox.Show("P Browser Builder need to restart to refetch with server." + vbNewLine + "Do you wish to continue?", "Refetch Warning!", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Application.Restart()
        End If
    End Sub
End Class
