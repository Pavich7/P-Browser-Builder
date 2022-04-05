Imports System.IO.Compression
Imports CefSharp.WinForms
Imports System.Net
Public Class Form1
    Private WithEvents Browser As ChromiumWebBrowser
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter your websites URL.", "Build Failed!")
        Else
            Label7.Text = "Building in progress..."
            Dim apppath As String = Application.StartupPath()
            Dim pbcfg As String = apppath + "\resource\testspace\builderdata.pbcfg"
            Dim pbprogcfg As String = apppath + "\resource\testspace\progdata.pbcfg"
            Dim testapp As String = apppath + "\resource\testspace\P Browser App.exe"
            ProgressBar1.Value = 20
            If System.IO.File.Exists(pbcfg) = True Then
                ProgressBar1.Value = 50
                Dim objWriter As New System.IO.StreamWriter(pbcfg)
                Dim objWriter2 As New System.IO.StreamWriter(pbprogcfg)
                objWriter.Write(TextBox1.Text)
                objWriter.Close()
                objWriter2.Write(TextBox2.Text)
                objWriter2.Close()
                Dim icnexist As String = apppath + "\resource\testspace\appicns.ico"
                If System.IO.File.Exists(icnexist) Then
                    My.Computer.FileSystem.DeleteFile(apppath + "\resource\testspace\appicns.ico")
                Else
                    Dim icnshave As String = apppath + "\buildcache\appicns\appicns.ico"
                    If System.IO.File.Exists(icnshave) Then
                        My.Computer.FileSystem.CopyFile(apppath + "\buildcache\appicns\appicns.ico", apppath + "\resource\testspace\appicns.ico")
                    End If
                End If
                ProgressBar1.Value = 100
                MessageBox.Show("Build Completed! Click continue to test app.", "Build Completed!")
                Process.Start(testapp)
                Label7.Text = "Build completed!"
            Else
                MessageBox.Show("Build Failed! Incomplete or corrupted Data please reinstall builder.", "Build Failed!")
                Label7.Text = "Build failed!"
            End If
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.Click
        Button1.Enabled = False
        CheckBox1.Text = "Start your app after build"
        CheckBox2.Text = "Show your app in explorer after build"
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.Click
        Button1.Enabled = False
        CheckBox1.Text = "Start your app after build"
        CheckBox2.Text = "Show your app in explorer after build"
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        CheckBox1.Text = "Start your app after build"
        CheckBox2.Text = "Show your app in explorer after build"
        Button1.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter your websites URL.", "Build Failed!")
        ElseIf TextBox2.Text = "" Then
            MessageBox.Show("Please enter your application name.", "Build Failed!")
        Else
            If RadioButton1.Checked = True Then
                Label7.Text = "Building in progress..."
                Dim apppath As String = Application.StartupPath()
                Dim pbcfg As String = apppath + "\resource\buildspace\quickmode\builderdata.pbcfg"
                Dim pbprogcfg As String = apppath + "\resource\buildspace\quickmode\progdata.pbcfg"
                System.IO.Directory.Delete(apppath + "\binary", True)
                System.IO.Directory.CreateDirectory(apppath + "\binary")
                ProgressBar1.Value = 20
                If System.IO.File.Exists(pbcfg) = True Then
                    Dim objWriter As New System.IO.StreamWriter(pbcfg)
                    objWriter.Write(TextBox1.Text)
                    objWriter.Close()
                    Dim objWriter2 As New System.IO.StreamWriter(pbprogcfg)
                    objWriter2.Write(TextBox2.Text)
                    objWriter2.Close()
                    ProgressBar1.Value = 50
                    My.Computer.FileSystem.CopyDirectory(apppath + "\resource\buildspace\quickmode", apppath + "\binary", True)
                    My.Computer.FileSystem.RenameFile(apppath + "\binary\P Browser App.exe", TextBox2.Text + ".exe")
                    Dim icnshave As String = apppath + "\buildcache\appicns\appicns.ico"
                    If System.IO.File.Exists(icnshave) Then
                        My.Computer.FileSystem.CopyFile(apppath + "\buildcache\appicns\appicns.ico", apppath + "\binary\appicns.ico")
                    End If
                    ProgressBar1.Value = 100
                    MessageBox.Show("Build Completed! Click OK to continue.", "Build Completed!")
                    If CheckBox1.Checked = True Then
                        Process.Start(apppath + "\binary\" + TextBox2.Text + ".exe")
                    End If
                    If CheckBox2.Checked Then
                        Process.Start(apppath + "\binary\")
                    End If
                    Label7.Text = "Build completed!"
                Else
                    MessageBox.Show("Build Failed! Incomplete or corrupted Data please reinstall builder.", "Build Failed!")
                    Label7.Text = "Build failed!"
                End If
            ElseIf RadioButton2.Checked = True Then
                Label7.Text = "Building in progress..."
                Dim apppath As String = Application.StartupPath()
                System.IO.Directory.Delete(apppath + "\resource\buildspace\cleanmode", True)
                System.IO.Directory.CreateDirectory(apppath + "\resource\buildspace\cleanmode")
                Dim zipPath As String = apppath + "\resource\resourcepack\freshapp.zip"
                Dim extractPath As String = apppath + "\resource\buildspace\cleanmode"
                ZipFile.ExtractToDirectory(zipPath, extractPath)
                Dim pbcfg As String = apppath + "\resource\buildspace\cleanmode\builderdata.pbcfg"
                Dim pbprogcfg As String = apppath + "\resource\buildspace\cleanmode\progdata.pbcfg"
                System.IO.Directory.Delete(apppath + "\binary", True)
                System.IO.Directory.CreateDirectory(apppath + "\binary")
                ProgressBar1.Value = 20
                If System.IO.File.Exists(pbcfg) = True Then
                    Dim objWriter As New System.IO.StreamWriter(pbcfg)
                    objWriter.Write(TextBox1.Text)
                    objWriter.Close()
                    Dim objWriter2 As New System.IO.StreamWriter(pbprogcfg)
                    objWriter2.Write(TextBox2.Text)
                    objWriter2.Close()
                    ProgressBar1.Value = 50
                    My.Computer.FileSystem.CopyDirectory(apppath + "\resource\buildspace\cleanmode", apppath + "\binary", True)
                    My.Computer.FileSystem.RenameFile(apppath + "\binary\P Browser App.exe", TextBox2.Text + ".exe")
                    Dim icnshave As String = apppath + "\buildcache\appicns\appicns.ico"
                    If System.IO.File.Exists(icnshave) Then
                        My.Computer.FileSystem.CopyFile(apppath + "\buildcache\appicns\appicns.ico", apppath + "\binary\appicns.ico")
                    End If
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
                    Label7.Text = "Build completed!"
                Else
                    MessageBox.Show("Build Failed! Incomplete or corrupted Data please reinstall builder.", "Build Failed!")
                    Label7.Text = "Build failed!"
                End If
            Else
                MessageBox.Show("Please select Build Mode!", "Build Failed!")
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Label7.Text = "Cleaning in progress..."
            ProgressBar1.Value = 20
            Dim apppath As String = Application.StartupPath()
            System.IO.Directory.Delete(apppath + "\binary", True)
            ProgressBar1.Value = 50
            System.IO.Directory.CreateDirectory(apppath + "\binary")
            ProgressBar1.Value = 100
            Label7.Text = "Cleanup completed!"
        Catch ex As Exception
            MessageBox.Show("Please close built app first before perform this action.", "Failed!")
            Label7.Text = "Cleanup failed!"
            ProgressBar1.Value = 100
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Dim setting As New CefSettings With {
            .RemoteDebuggingPort = 8088
        }
        CefSharp.Cef.EnableHighDPISupport()
        CefSharp.Cef.Initialize(setting)
        Browser = New ChromiumWebBrowser("")
        Panel2.Controls.Add(Browser)
        Dim rscheck As String = apppath + "\resource"
        If Not System.IO.Directory.Exists(rscheck) Then
            Button1.Enabled = False
            Button2.Enabled = False
            RadioButton1.Enabled = False
            RadioButton2.Enabled = False
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            Label6.Enabled = False
            Label9.Enabled = False
            Label4.Enabled = False
            Label8.Enabled = False
            UninstallToolStripMenuItem.Enabled = False
            AboutToolStripMenuItem1.Enabled = False
        Else
            Label19.Visible = False
            Label20.Visible = False
            Label18.Visible = False
            Dim headercheck As String = apppath + "\resource\metadata\checkpoint\header.chkp"
            Dim resvcheck As String = apppath + "\resource\metadata\checkpoint\r100.chkp"
            If Not System.IO.File.Exists(headercheck) Then
                MessageBox.Show("Resource not compatible! You might encounter errors." + vbNewLine + "Please reinstall builder resource via resource menu.", "Resource not compatible!")
            End If
            If Not System.IO.File.Exists(resvcheck) Then
                MessageBox.Show("Resource not compatible! You might encounter errors." + vbNewLine + "Please reinstall builder resource via resource menu.", "Resource not compatible!")
            End If
        End If
            Dim cachecheck As String = apppath + "\updatedata\pbb-resource.zip"
        If Not System.IO.File.Exists(cachecheck) Then
            DeleteCacheToolStripMenuItem.Enabled = False
        End If
        ShowRightPanelToolStripMenuItem.Enabled = False
        ExtensionsNotFoundToolStripMenuItem.Enabled = False
        Button7.Enabled = False
        ProgressBar2.Visible = False
        SaveProjectToolStripMenuItem.Visible = False
        LoadProjectToolStripMenuItem.Visible = False
        SaveProjectToolStripMenuItem.Enabled = False
        LoadProjectToolStripMenuItem.Enabled = False
        RadioButton3.Visible = False
        Label21.Visible = False
        DevToolStripMenuItem.Visible = False
        Timer2.Start()
        TextBox3.Enabled = False
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
            Label17.Text = "example"
        Else
            Label2.Text = "Application name (" + TextBox2.Text + ".exe)"
            Label17.Text = TextBox2.Text
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Try
            ProgressBar1.Value = 10
            Dim apppath As String = Application.StartupPath()
            My.Computer.Network.DownloadFile("https://pavichdev.ddns.net/service/app.pavichdev.pbrowserbuilder/v1/newsfeed/nf1_title.txt", apppath + "\nfcache\nf1_title.txt")
            My.Computer.Network.DownloadFile("https://pavichdev.ddns.net/service/app.pavichdev.pbrowserbuilder/v1/newsfeed/nf1_desc.txt", apppath + "\nfcache\nf1_desc.txt")
            My.Computer.Network.DownloadFile("https://pavichdev.ddns.net/service/app.pavichdev.pbrowserbuilder/v1/newsfeed/nf1_date.txt", apppath + "\nfcache\nf1_date.txt")
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
            Label7.Text = "Ready to build"
            ProgressBar1.Value = 0
        Catch ex As Exception
            Label7.Text = "Ready to build"
            ProgressBar1.Value = 0
            Label15.Visible = True
            Label14.Visible = False
            Label12.Text = "Running in offline mode"
            Label13.Text = "Can't establish connection and fetch news feed with PavichDev Server."
        End Try
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Dim result As DialogResult = MessageBox.Show("P Browser Builder need to restart to refetch with server." + vbNewLine + "Do you wish to continue?", "Refetch Warning!", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Application.Restart()
        End If
    End Sub

    Private Sub ClearAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearAllToolStripMenuItem.Click
        Dim apppath As String = Application.StartupPath()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        CheckBox1.Text = "Start your app after build"
        CheckBox2.Text = "Show your app in explorer after build"
        Button1.Enabled = True
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        System.IO.Directory.Delete(apppath + "\buildcache\appicns", True)
        System.IO.Directory.CreateDirectory(apppath + "\buildcache\appicns")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim apppath As String = Application.StartupPath()
        System.IO.Directory.Delete(apppath + "\buildcache\appicns", True)
        System.IO.Directory.CreateDirectory(apppath + "\buildcache\appicns")
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.Title = "Choose your icons file"
        OpenFileDialog1.Filter = "Icons Files|*.ico"
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
            TextBox3.Text = OpenFileDialog1.FileName
            My.Computer.FileSystem.CopyFile(OpenFileDialog1.FileName, apppath + "\buildcache\appicns\appicns.ico")
        End If
    End Sub

    Private Sub InstallationGuideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InstallationGuideToolStripMenuItem.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/wiki/P-Browser-Builder-Guild#install-p-browser-builder-beta-030-and-later")
    End Sub

    Private Sub SubmitBugsReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubmitBugsReportToolStripMenuItem.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/issues/new/choose")
    End Sub

    Private Sub ReloadPreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReloadPreviewToolStripMenuItem.Click
        Browser.Load(TextBox1.Text)
        Browser.Refresh()
    End Sub

    Private Sub OpenPreviewLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenPreviewLogToolStripMenuItem.Click
        Try
            Dim apppath As String = Application.StartupPath()
            Process.Start(apppath + "\debug.log")
        Catch ex As Exception
            MessageBox.Show("Log not found! Maybe you can load some web and try again.", "Error!")
        End Try
    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        Label18.Enabled = False
        Label18.Text = "Waiting for confirmation..."
        Dim result As DialogResult = MessageBox.Show("Do you wish to install builder resource?" + vbNewLine + "Builder will not responding while installing resource." + vbNewLine + "ATTEMPTING TO EXIT THE BUILDER MAY INCOMPLETE RESOURCE", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Label21.Visible = True
            ProgressBar2.Visible = True
            Label18.Visible = False
            Try
                Dim apppath As String = Application.StartupPath()
                ProgressBar2.Value = 0
                System.IO.Directory.Delete(apppath + "\updatedata", True)
                System.IO.Directory.CreateDirectory(apppath + "\updatedata")
                ProgressBar2.Value = 25
                Dim strURL As String = "https://github.com/Pavich7/P-Browser-Builder-Resource/releases/latest/download/pbb-resource.zip"
                Using webcli As WebClient = New WebClient()
                    webcli.DownloadFile(strURL, apppath + "\updatedata\pbb-resource.zip")
                End Using
                ProgressBar2.Value = 50
                Dim zipPath As String = apppath + "\updatedata\pbb-resource.zip"
                Dim extractPath As String = apppath
                ProgressBar2.Value = 75
                ZipFile.ExtractToDirectory(zipPath, extractPath)
                ProgressBar2.Value = 100
                Dim result1 As DialogResult = MessageBox.Show("Installation completed but restart required!" + vbNewLine + "Do you want to restart P Browser Builder now?", "Installation completed!", MessageBoxButtons.YesNo)
                If (result1 = DialogResult.Yes) Then
                    Application.Restart()
                Else
                    Label18.Text = "Already installed! Please restart app."
                    Label18.Visible = True
                    Label18.Enabled = False
                    Label21.Visible = False
                    ProgressBar2.Visible = False
                End If
            Catch ex As Exception
                Dim apppath As String = Application.StartupPath()
                MessageBox.Show("Could not attempt to install resource!" + vbNewLine + ex.Message, "Error!")
                Label21.Visible = False
                ProgressBar2.Visible = False
                Label18.Visible = True
                System.IO.Directory.Delete(apppath + "\buildcache\appicns", True)
                System.IO.Directory.CreateDirectory(apppath + "\buildcache\appicns")
                Label18.Enabled = True
                Label18.Text = "Try again"
            End Try
        Else
            Label18.Enabled = True
            Label18.Text = "Try again"
        End If
    End Sub

    Private Sub UninstallBuilderResourceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UninstallToolStripMenuItem.Click
        Try
            Dim result As DialogResult = MessageBox.Show("Do you wish to completely uninstall builder resource?" + vbNewLine + "You can reinstall resource later via notification box", "You sure about this?", MessageBoxButtons.YesNo)
            If (result = DialogResult.Yes) Then
                Dim apppath As String = Application.StartupPath()
                System.IO.Directory.Delete(apppath + "\resource", True)
                MessageBox.Show("P Browser Builder need to restart app", "Uninstall Completed!")
                Application.Restart()
            End If
        Catch ex As Exception
            MessageBox.Show("Could not attempt to uninstall resource!" + vbNewLine + ex.Message + vbNewLine + "You may need to restart builder and try again.", "Error!")
        End Try
    End Sub

    Private Sub AboutBuilderResourceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        Try
            Dim apppath As String = Application.StartupPath()
            Dim metacheck As String = apppath + "\resource\metadata\version.txt"
            If Not System.IO.File.Exists(metacheck) Then
                MessageBox.Show("Resource not compatible with this version of P Browser Builder!" + vbNewLine + "Please update resource by uninstall and reinstall via resource menu.", "Error!")
            Else
                System.IO.Directory.Delete(apppath + "\resource\getcache", True)
                System.IO.Directory.CreateDirectory(apppath + "\resource\getcache")
                Dim fileReader As System.IO.StreamReader
                My.Computer.Network.DownloadFile("https://pavichdev.ddns.net/service/app.pavichdev.pbrowserbuilder/v1/cfuversion/onlineresver.txt", apppath + "\resource\getcache\onlineresver.txt")
                Dim fileReader1 As System.IO.StreamReader
                fileReader1 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\getcache\onlineresver.txt")
                Dim stringReader1 As String
                stringReader1 = fileReader1.ReadLine()
                fileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\metadata\version.txt")
                Dim stringReader As String
                stringReader = fileReader.ReadLine()
                MessageBox.Show("Builder Resource version: " + stringReader + vbNewLine + "Latest Online version: " + stringReader1 + vbNewLine + "To update version please uninstall and reinstall builder resource", "About Builder Resource")
            End If
        Catch ex As Exception
            Dim apppath As String = Application.StartupPath()
            Dim fileReader As System.IO.StreamReader
            fileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\metadata\version.txt")
            Dim stringReader As String
            stringReader = fileReader.ReadLine()
            MessageBox.Show("Could not fetch latest online version info!" + vbNewLine + "You are using resource version: " + stringReader, "Error!")
        End Try
    End Sub

    Private Sub DeleteInstallerCacheToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteCacheToolStripMenuItem.Click
        Try
            Dim apppath As String = Application.StartupPath()
            System.IO.Directory.Delete(apppath + "\updatedata", True)
            System.IO.Directory.CreateDirectory(apppath + "\updatedata")
            DeleteCacheToolStripMenuItem.Enabled = False
        Catch ex As Exception
            MessageBox.Show("Could not attempt to delete installer cache!" + vbNewLine + ex.Message + vbNewLine + "You may need to restart builder and try again.", "Error!")
        End Try
    End Sub

    Private Sub OpenRemoteDebuggingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenRemoteDebuggingToolStripMenuItem.Click
        Process.Start("http://127.0.0.1:8088/")
    End Sub

    Private Sub CustomizingGuildToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomizingGuildToolStripMenuItem.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/wiki/P-Browser-Builder-Guild#customizing-your-p-browser-app")
    End Sub

    Private Sub BuildingGuideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuildingGuideToolStripMenuItem.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/wiki/P-Browser-Builder-Guild#building-a-p-browser-app-from-p-browser-builder")
    End Sub

    <Obsolete>
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Dim pros As Process = Process.GetCurrentProcess()
        Dim demround1 As Double = pros.WorkingSet / 1024 / 1024
        demround1 = Math.Round(demround1, 2)
        Dim demround2 As Double = pros.PagedMemorySize / 1024 / 1024
        demround2 = Math.Round(demround2, 2)
        Dim demround3 As Double = ((My.Computer.Info.TotalPhysicalMemory - My.Computer.Info.AvailablePhysicalMemory) / My.Computer.Info.TotalPhysicalMemory) * 100
        demround3 = Math.Round(demround3, 2)
        Dim demround4 As Double = (My.Computer.Info.TotalPhysicalMemory - My.Computer.Info.AvailablePhysicalMemory) / 1024 / 1024 / 1024
        demround4 = Math.Round(demround4, 2)
        Label23.Text = "Memory Usage: " & demround1 & " MB"
        Label24.Text = "Paged Memory Usage: " & demround2 & " MB"
        ProgressBar3.Value = ((My.Computer.Info.TotalPhysicalMemory - My.Computer.Info.AvailablePhysicalMemory) / My.Computer.Info.TotalPhysicalMemory) * 100
        Label25.Text = "Overall Usage: " & demround3 & " % (" & demround4 & " GB)"
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Timer2.Start()
        Button7.Enabled = False
        Button8.Enabled = True
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Timer2.Stop()
        Button8.Enabled = False
        Button7.Enabled = True
        ProgressBar3.Value = 0
        Label23.Text = "Memory Usage: Paused"
        Label24.Text = "Paged Memory Usage: Paused"
        Label25.Text = "Paused"
    End Sub

    Private Sub OpenBuilderInExplorerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenBuilderInExplorerToolStripMenuItem.Click
        Dim apppath As String = Application.StartupPath()
        Process.Start(apppath)
    End Sub

    Private Sub OpenBuildDirectoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenBuildDirectoryToolStripMenuItem.Click
        Dim apppath As String = Application.StartupPath()
        Process.Start(apppath + "\binary")
    End Sub

    Private Sub MaximizedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaximizedToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub NormalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NormalToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub MinimizedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MinimizedToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub HideRightPanelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideRightPanelToolStripMenuItem.Click
        Panel6.Hide()
        Me.WindowState = FormWindowState.Normal
        Me.Size = New Size(1232, 646)
        Panel1.Width = 872
        HideRightPanelToolStripMenuItem.Enabled = False
        ShowRightPanelToolStripMenuItem.Enabled = True
        Timer2.Stop()
    End Sub

    Private Sub ShowRightPanelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowRightPanelToolStripMenuItem.Click
        Panel6.Show()
        Me.WindowState = FormWindowState.Normal
        Me.Size = New Size(1232, 646)
        Panel1.Width = 560
        ShowRightPanelToolStripMenuItem.Enabled = False
        HideRightPanelToolStripMenuItem.Enabled = True
        Timer2.Start()
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.Click
        Button1.Enabled = False
        CheckBox1.Text = "Start your installer after build"
        CheckBox2.Text = "Show your installer in explorer after build"
    End Sub

    Private Sub UnlockDeveloperMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnlockDeveloperMenuToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("Unlocking the Dev Menu is dangerous." + vbNewLine + "It is used to test incomplete features at runtime." + vbNewLine + "Some incomplete or faulty features can damage your Builder!" + vbNewLine + "For developers, you can go check the code in the repository." + vbNewLine + "Do you want to process it?", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            DevToolStripMenuItem.Visible = True
            UnlockDeveloperMenuToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub UnlockIncompleteFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnlockIncompleteFeatureToolStripMenuItem.Click
        RadioButton3.Visible = True
        SaveProjectToolStripMenuItem.Visible = True
        LoadProjectToolStripMenuItem.Visible = True
        UnlockIncompleteFeatureToolStripMenuItem.Enabled = False
    End Sub

    Private Sub ForceUnlockDisableButtonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForceUnlockDisableButtonToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("Force Unlocking the button is extremely dangerous." + vbNewLine + "YOUR MAY NEED TO REINSTALL YOUR BUILDER IN CASE OF DAMAGED!" + vbNewLine + "Do you want to process it?", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Button1.Enabled = True
            Button2.Enabled = True
            Label4.Enabled = True
            TextBox3.Enabled = True
            Label8.Enabled = True
            Label6.Enabled = True
            Label9.Enabled = True
            SaveProjectToolStripMenuItem.Enabled = True
            LoadProjectToolStripMenuItem.Enabled = True
            RadioButton1.Enabled = True
            RadioButton2.Enabled = True
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
            ShowRightPanelToolStripMenuItem.Enabled = True
            HideRightPanelToolStripMenuItem.Enabled = True
            UninstallToolStripMenuItem.Enabled = True
            DeleteCacheToolStripMenuItem.Enabled = True
            AboutToolStripMenuItem1.Enabled = True
            ExtensionsNotFoundToolStripMenuItem.Enabled = True
            ForceUnlockDisableButtonToolStripMenuItem.Enabled = True
            UnlockIncompleteFeatureToolStripMenuItem.Enabled = True
            UnlockDeveloperMenuToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub ResetExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub ReToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReToolStripMenuItem.Click
        Application.Restart()
    End Sub

    Private Sub Panel7_Paint(sender As Object, e As EventArgs) Handles Panel7.Click
        Panel6.Hide()
        Me.WindowState = FormWindowState.Normal
        Me.Size = New Size(1232, 646)
        Panel1.Width = 872
        HideRightPanelToolStripMenuItem.Enabled = False
        ShowRightPanelToolStripMenuItem.Enabled = True
        Timer2.Stop()
        MessageBox.Show("You can unhide right panel by click on" + vbNewLine + "Menu Strip: Window > Show right panel", "Notification")
    End Sub

    Private Sub SaveProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveProjectToolStripMenuItem.Click
        Dim apppath As String = Application.StartupPath()
        SaveFileDialog1.Filter = "P Browser Builder Project Files|*.pbproj"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            System.IO.Directory.Delete(apppath + "\savecache", True)
            System.IO.Directory.CreateDirectory(apppath + "\savecache")
            System.IO.Directory.CreateDirectory(apppath + "\savecache\" + TextBox2.Text)
        End If
    End Sub

    Private Sub LoadProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadProjectToolStripMenuItem.Click
        OpenFileDialog2.Multiselect = False
        OpenFileDialog2.Title = "Choose your project file"
        OpenFileDialog2.Filter = "P Browser Builder Project Files|*.pbproj"
        If OpenFileDialog2.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            MessageBox.Show("Project data files corrupt or not valid!", "Load failure!")
        End If
    End Sub
End Class