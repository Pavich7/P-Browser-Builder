Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text
Imports CefSharp
Imports CefSharp.SchemeHandler
Imports CefSharp.WinForms
Public Class Form1
    Public WithEvents Browser As ChromiumWebBrowser
    'Private WithEvents BrowserDev As ChromiumWebBrowser
    Public Sub New()
        InitializeComponent()
    End Sub

    Public logpath
    Public apppath As String
    Dim pros As Process

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, TestProjectToolStripMenuItem.Click
        Dim spechk As Boolean = False
        Dim text As String = TextBox2.Text
        Dim specialCharacters As String = "\/:*?""<>|"
        For Each ch As Char In specialCharacters
            If text.Contains(ch) Then
                spechk = True
                Exit For
            End If
        Next
        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter your websites URL.", "Build Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf spechk = True Then
            MessageBox.Show("App name cannot contain any of these characters: \ / : * ? "" < > |", "Build Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Label7.Text = "Building in progress..."
            Dim pbcfg As String = apppath + "\resource\testspace\manifest.pbcfg"
            ProgressBar1.Value = 20
            If System.IO.File.Exists(pbcfg) = True Then
                Try
                    Button2.Enabled = False
                    Button1.Enabled = False
                    ProgressBar1.Value = 50
                    Using writer As New StreamWriter(apppath + "\resource\testspace\manifest.pbcfg", False)
                        'builderdata
                        'progdata
                        'contextmenu
                        'fsmsg.title
                        'fsmsg.desc
                        'fsmsg.icon
                        'fsmsg.enab
                        'appsizew
                        'appsizeh
                        'appfixbs
                        'appopval
                        'appaot
                        'apphico
                        'useragent
                        writer.WriteLine(TextBox1.Text)
                        writer.WriteLine(TextBox2.Text)
                        If CheckBox8.Checked = True Then
                            writer.WriteLine("True")
                        Else
                            writer.WriteLine("False")
                        End If
                        writer.WriteLine("")
                        writer.WriteLine("")
                        writer.WriteLine("Information")
                        writer.WriteLine("False")
                        writer.WriteLine(TextBox3.Text)
                        writer.WriteLine(TextBox4.Text)
                        If CheckBox5.Checked = True Then
                            writer.WriteLine("True")
                        Else
                            writer.WriteLine("False")
                        End If
                        writer.WriteLine(TextBox5.Text)
                        If CheckBox6.Checked = True Then
                            writer.WriteLine("True")
                        Else
                            writer.WriteLine("False")
                        End If
                        If CheckBox7.Checked = True Then
                            writer.WriteLine("True")
                        Else
                            writer.WriteLine("False")
                        End If
                        If CheckBox9.Checked = True Then
                            writer.WriteLine(TextBox6.Text)
                        End If
                    End Using
                    'create hash
                    Dim lines As List(Of String) = File.ReadAllLines(apppath + "\resource\testspace\manifest.pbcfg").ToList()
                    If lines.Count >= 7 Then
                        lines.RemoveAt(6)
                    End If
                    Dim combinedString As String = String.Join(Environment.NewLine, lines)
                    Dim md5 As MD5 = MD5.Create()
                    Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(combinedString)
                    Dim hashBytes As Byte() = md5.ComputeHash(inputBytes)
                    Dim hash As New StringBuilder()
                    For Each b As Byte In hashBytes
                        hash.Append(b.ToString("X2"))
                    Next
                    Dim calchash As String = hash.ToString()
                    Dim hashWriter As New System.IO.StreamWriter(apppath + "\resource\testspace\checksum.pbcfg")
                    hashWriter.Write(calchash)
                    hashWriter.Close()
                    'end create hash
                    If System.IO.File.Exists(apppath + "\resource\testspace\appicns.ico") Then
                        My.Computer.FileSystem.DeleteFile(apppath + "\resource\testspace\appicns.ico")
                    Else
                        If System.IO.File.Exists(apppath + "\statecache\buildcache\appicns\appicns.ico") Then
                            My.Computer.FileSystem.CopyFile(apppath + "\statecache\buildcache\appicns\appicns.ico", apppath + "\resource\testspace\appicns.ico")
                        End If
                    End If
                    My.Computer.FileSystem.CopyDirectory(apppath + "\statecache\buildcache\offlineweb\", apppath + "\resource\testspace\assets\localfiles\", True)
                    ProgressBar1.Value = 100
                    MessageBox.Show("Build Completed! Click continue to test app." + vbNewLine + "Some features will not available in Testing.", "Build Completed!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'bootbit
                    Using bitwriter As New StreamWriter(apppath + "\resource\testspace\bootbit.pbcfg", False)
                        'offweb
                        'devtools
                        'nologs
                        If CheckBox10.Checked = True Then
                            bitwriter.WriteLine("True")
                        Else
                            bitwriter.WriteLine("False")
                        End If
                        If CheckBox11.Checked = True Then
                            bitwriter.WriteLine("True")
                        Else
                            bitwriter.WriteLine("False")
                        End If
                        If CheckBox12.Checked = True Then
                            bitwriter.WriteLine("True")
                        Else
                            bitwriter.WriteLine("False")
                        End If
                    End Using
                    Process.Start(apppath + "\resource\testspace\P Browser App.exe")
                    Label7.Text = "Build completed!"
                    Snooze(2)
                    If CheckBox12.Checked = False Then
                        Dim dir = New System.IO.DirectoryInfo(apppath + "\resource\testspace\startlog")
                        Dim logf = dir.EnumerateFiles("*.txt").
                            OrderByDescending(Function(f) f.LastWriteTime).
                            FirstOrDefault()
                        If logf IsNot Nothing Then
                            logpath = logf.FullName
                            RichTextBox1.Text = File.ReadAllText(logpath)
                            Dim logname = logf.Name
                            Label39.Text = logname
                            Label39.Visible = True
                            PictureBox14.Enabled = True
                        End If
                    Else
                        RichTextBox1.Text = "Logging has been disabled. Please enable it to view log."
                    End If
                    Timer2.Start()
                    Label42.Enabled = True
                    Button7.Enabled = False
                    Button8.Enabled = True
                    Snooze(5)
                    ProgressBar1.Value = 0
                    Label7.Text = "Ready to build"
                Catch ex As Exception
                    MessageBox.Show("Please close previous test app first before perform this action.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Label7.Text = "Test failed!"
                    Button2.Enabled = True
                    Button1.Enabled = True
                    ProgressBar1.Value = 0
                    Snooze(5)
                    Label7.Text = "Ready to build"
                End Try
            Else
                MessageBox.Show("Build Failed! Unable to find manifest data! Please reinstall resource.", "Build Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Label7.Text = "Build failed!"
                Snooze(5)
                ProgressBar1.Value = 0
                Label7.Text = "Ready to build"
            End If
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.Click
        CheckBox1.Text = "Start your app after build"
        CheckBox2.Text = "Show your app in explorer after build"
    End Sub

    Dim stage1finish As Boolean
    Dim stage2finish As Boolean

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click, BuildProjectToolStripMenuItem.Click
        Dim spechk As Boolean = False
        Dim text As String = TextBox2.Text
        Dim specialCharacters As String = "\/:*?""<>|"
        For Each ch As Char In specialCharacters
            If text.Contains(ch) Then
                spechk = True
                Exit For
            End If
        Next
        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter your websites URL.", "Build Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf TextBox2.Text = "" Then
            MessageBox.Show("Please enter your application name.", "Build Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf spechk = True Then
            MessageBox.Show("App name cannot contain any of these characters: \ / : * ? "" < > |", "Build Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Try
                Panel10.Enabled = False
                Panel1.Enabled = False
                BuildToolStripMenuItem.Enabled = False
                stage1finish = False
                stage2finish = False
                Label7.Text = "Building in progress..."
                System.IO.Directory.Delete(apppath + "\binary", True)
                System.IO.Directory.CreateDirectory(apppath + "\binary")
                ProgressBar1.Value = 20
                Label7.Text = "Building in progress... (Preparing resource...)"
                buildworker.RunWorkerAsync()
                While stage1finish = False
                    Application.DoEvents()
                End While
                ProgressBar1.Value = 40
                Label7.Text = "Building in progress... (Applying configurations...)"
                Using writer As New StreamWriter(apppath + "\binary\manifest.pbcfg", False)
                    'builderdata
                    'progdata
                    'contextmenu
                    'fsmsg.title
                    'fsmsg.desc
                    'fsmsg.icon
                    'fsmsg.enab
                    'appsizew
                    'appsizeh
                    'appfixbs
                    'appopval
                    'appaot
                    'apphico
                    'useragent
                    writer.WriteLine(TextBox1.Text)
                    writer.WriteLine(TextBox2.Text)
                    If CheckBox8.Checked = True Then
                        writer.WriteLine("True")
                    Else
                        writer.WriteLine("False")
                    End If
                    writer.WriteLine(TextBox8.Text)
                    writer.WriteLine(TextBox7.Text)
                    writer.WriteLine(ComboBox1.Text)
                    If CheckBox3.Checked = True Then
                        writer.WriteLine("True")
                    Else
                        writer.WriteLine("False")
                    End If
                    writer.WriteLine(TextBox3.Text)
                    writer.WriteLine(TextBox4.Text)
                    If CheckBox5.Checked = True Then
                        writer.WriteLine("True")
                    Else
                        writer.WriteLine("False")
                    End If
                    writer.WriteLine(TextBox5.Text)
                    If CheckBox6.Checked = True Then
                        writer.WriteLine("True")
                    Else
                        writer.WriteLine("False")
                    End If
                    If CheckBox7.Checked = True Then
                        writer.WriteLine("True")
                    Else
                        writer.WriteLine("False")
                    End If
                    If CheckBox9.Checked = True Then
                        writer.WriteLine(TextBox6.Text)
                    End If
                End Using
                'bootbit
                Using bitwriter As New StreamWriter(apppath + "\binary\bootbit.pbcfg", False)
                    'offweb
                    'devtools
                    'nologs
                    bitwriter.WriteLine("False")
                    bitwriter.WriteLine("False")
                    If CheckBox12.Checked = True Then
                        bitwriter.WriteLine("True")
                    Else
                        bitwriter.WriteLine("False")
                    End If
                End Using
                'create hash
                Dim lines As List(Of String) = File.ReadAllLines(apppath + "\binary\manifest.pbcfg").ToList()
                If lines.Count >= 7 Then
                    lines.RemoveAt(6)
                End If
                Dim combinedString As String = String.Join(Environment.NewLine, lines)
                Dim md5 As MD5 = MD5.Create()
                Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(combinedString)
                Dim hashBytes As Byte() = md5.ComputeHash(inputBytes)
                Dim hash As New StringBuilder()
                For Each b As Byte In hashBytes
                    hash.Append(b.ToString("X2"))
                Next
                Dim calchash As String = hash.ToString()
                Dim hashWriter As New System.IO.StreamWriter(apppath + "\binary\checksum.pbcfg")
                hashWriter.Write(calchash)
                hashWriter.Close()
                'end create hash
                ProgressBar1.Value = 50
                My.Computer.FileSystem.RenameFile(apppath + "\binary\P Browser App.exe", TextBox2.Text + ".exe")
                If System.IO.File.Exists(apppath + "\statecache\buildcache\appicns\appicns.ico") Then
                    My.Computer.FileSystem.CopyFile(apppath + "\statecache\buildcache\appicns\appicns.ico", apppath + "\binary\appicns.ico", True)
                End If
                My.Computer.FileSystem.CopyDirectory(apppath + "\statecache\buildcache\offlineweb\", apppath + "\binary\assets\localfiles\", True)
                ProgressBar1.Value = 70
                If RadioButton3.Checked = True Then
                    System.IO.Directory.Delete(apppath + "\binarypkg", True)
                    System.IO.Directory.CreateDirectory(apppath + "\binarypkg")
                    Label7.Text = "Building in progress... (Compressing App...)"
                    buildfinalworker.RunWorkerAsync()
                    While stage2finish = False
                        Application.DoEvents()
                    End While
                End If
                ProgressBar1.Value = 100
                Label7.Text = "Build completed!"
                MessageBox.Show("Build Completed! Click OK to continue.", "Build Completed!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If CheckBox1.Checked = True Then
                    Process.Start(apppath + "\binary\" + TextBox2.Text + ".exe")
                End If
                If CheckBox2.Checked Then
                    If RadioButton3.Checked = True Then
                        Process.Start(apppath + "\binarypkg\")
                    Else
                        Process.Start(apppath + "\binary\")
                    End If
                End If
                If CheckBox4.Checked = True Then
                    Try
                        If TextBox9.Text IsNot "" Then
                            Process.Start(TextBox9.Text)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Cannot run script! Your app is build and ready!", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                Panel10.Enabled = True
                Panel1.Enabled = True
                BuildToolStripMenuItem.Enabled = True
                Snooze(5)
                ProgressBar1.Value = 0
                Label7.Text = "Ready to build"
            Catch ex As Exception
                MessageBox.Show("Build Failed! Incomplete or corrupted Data please reinstall builder." + vbNewLine + ex.Message, "Build Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Label7.Text = "Build failed!"
                Panel10.Enabled = True
                Panel1.Enabled = True
                BuildToolStripMenuItem.Enabled = True
                Snooze(5)
                ProgressBar1.Value = 0
                Label7.Text = "Ready to build"
            End Try
        End If
    End Sub
    Private Sub buildworker_DoWork(sender As Object, e As DoWorkEventArgs) Handles buildworker.DoWork
        Dim zipPath As String = apppath + "\resource\resourcepack\freshapp.zip"
        Dim extractPath As String = apppath + "\binary"
        ZipFile.ExtractToDirectory(zipPath, extractPath)
        stage1finish = True
    End Sub
    Private Sub buildfinalworker_DoWork(sender As Object, e As DoWorkEventArgs) Handles buildfinalworker.DoWork
        Dim zipsource As String = apppath + "\binary\"
        Dim zipbin As String = apppath + "\binarypkg\" + TextBox2.Text + ".zip"
        ZipFile.CreateFromDirectory(zipsource, zipbin, CompressionLevel.Optimal, False)
        stage2finish = True
    End Sub
    Private Sub Snooze(ByVal seconds As Integer)
        For i As Integer = 0 To seconds * 100
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click, BuildManagerToolStripMenuItem.Click
        buildmanage.Show()
    End Sub
    Public Sub MyForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Not TextBox1.Text = "" Then
            If MessageBox.Show("Are you sure you want to exit? Make sure you saved your work!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
        settings.save("winstatew", Me.Width)
        settings.save("winstateh", Me.Height)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        apppath = Application.StartupPath()
        If Process.GetProcessesByName(Process.GetCurrentProcess.ProcessName).Length > 1 Then
            splash.Hide()
            MessageBox.Show("P Browser Builder is already running!", "Already running", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Environment.Exit(0)
        End If
        splash.Label1.Text = "Checking app structure..."
        'Init structure check
        If Not System.IO.Directory.Exists(apppath + "\assets") Then
            Throw New Exception("Bad data structure! Reinstall might fixed this problem.")
        End If
        If Not System.IO.Directory.Exists(apppath + "\binary") Then System.IO.Directory.CreateDirectory(apppath + "\binary")
        If Not System.IO.Directory.Exists(apppath + "\binarypkg") Then System.IO.Directory.CreateDirectory(apppath + "\binarypkg")
        If Not System.IO.Directory.Exists(apppath + "\metadata") Then
            Throw New Exception("Bad data structure! Reinstall might fixed this problem.")
        End If
        If Not System.IO.Directory.Exists(apppath + "\statecache") Then System.IO.Directory.CreateDirectory(apppath + "\statecache")
        If Not System.IO.Directory.Exists(apppath + "\statecache\buildcache") Then System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache")
        If Not System.IO.Directory.Exists(apppath + "\statecache\buildcache\appicns") Then System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\appicns")
        If Not System.IO.Directory.Exists(apppath + "\statecache\buildcache\offlineweb") Then System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\offlineweb")
        If Not System.IO.Directory.Exists(apppath + "\statecache\updatecache") Then System.IO.Directory.CreateDirectory(apppath + "\statecache\updatecache")
        If Not File.Exists(apppath + "\settings.pbcfg") Then
            File.Copy(apppath + "\assets\settings_origin.pbcfg", apppath + "\settings.pbcfg")
        End If

        splash.Label1.Text = "Loading app state..."
        'Reset state Check
        Button6.Visible = False
        CheckBox4.Visible = False
        PictureBox14.Enabled = False
        Label39.Visible = False
        Button6.Enabled = False
        RadioButton2.Checked = True
        CheckBox1.Checked = True
        Label42.Enabled = False
        TextBox6.Visible = False
        TabControl1.TabPages.Remove(TabPage3)
        TabControl1.TabPages.Remove(TabPage4)
        Panel4.Visible = False
        TabControl1.TabPages.Remove(TabPage5)
        TabControl1.TabPages.Remove(TabPage6)
        TabControl1.TabPages.Remove(TabPage7)
        'Reset
        Dim inrsstate = settings.load("inrsstate")
        If inrsstate = "True" Then
            Me.Enabled = False
            Try
                splash.Hide()
                MessageBox.Show("P Browser Builder is in Restore Mode!" + vbNewLine + "This mode will guide you to reset all Builder Settings.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Dim result As DialogResult = MessageBox.Show("Do you wish to reset all setting?" + vbNewLine + "This cannot be undone!", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If (result = DialogResult.Yes) Then
                    Dim result1 As DialogResult = MessageBox.Show("Do you wish to delete resource also?" + vbNewLine + "You can reinstall anytime via news feed.", "Resource setting", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    'Delete Res if prompted
                    If (result1 = DialogResult.Yes) Then
                        Dim rscheck1 As String = apppath + "\resource"
                        If System.IO.Directory.Exists(rscheck1) Then
                            System.IO.Directory.Delete(apppath + "\resource", True)
                        End If
                    End If
                    'Delete config files
                    File.Delete(apppath + "\settings.pbcfg")
                    'Delete Main Cache Dir
                    System.IO.Directory.Delete(apppath + "\statecache", True)
                    'Delete Build Dir
                    System.IO.Directory.Delete(apppath + "\binary", True)
                    System.IO.Directory.Delete(apppath + "\binarypkg", True)
                    'Flush debug logs
                    Dim objWriter As New System.IO.StreamWriter(apppath + "\debug.log")
                    objWriter.Write("")
                    objWriter.Close()
                    'Write default whats news state
                    Dim objWriter1 As New System.IO.StreamWriter(apppath + "\wnannounce.pbstate")
                    objWriter1.Write("True")
                    objWriter1.Close()
                    'Write default autocfu state
                    Dim objWriter11 As New System.IO.StreamWriter(apppath + "\autocfu.pbstate")
                    objWriter11.Write("")
                    objWriter11.Close()
                    MessageBox.Show("Operation Completed!", "Success!")
                    Dim result11 As DialogResult = MessageBox.Show("Do you wish to restart?" + vbNewLine + "YES to restart, NO to shutdown.", "Restart?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If (result11 = DialogResult.Yes) Then
                        Application.Restart()
                    Else
                        Application.Exit()
                    End If
                Else
                    settings.save("inrsstate", "False")
                    MessageBox.Show("Operation Aborted, Nothing Happened! Builder needs restart.", "Aborted!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Application.Restart()
                End If
            Catch ex As Exception
                settings.save("inrsstate", "False")
                MessageBox.Show("Failed to reset. Builder needs restart." + vbNewLine + ex.Message, "Fatal Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Restart()
            End Try
        Else
            'Check autocfu
            Dim autocfu As Boolean = False
            Dim filePath As String = apppath + "\autocfu.pbstate"
            Dim currentDate As String = DateTime.Now.ToString("yyyy-MM-dd")
            Dim lastCheckedDate As String = If(File.Exists(filePath), File.ReadAllText(filePath).Trim(), "")
            If lastCheckedDate <> currentDate Then
                autocfu = True
                File.WriteAllText(filePath, currentDate)
            End If
            splash.Label1.Text = "Initializing core..."
            'Init Cef
            Dim setting As New CefSettings With {
                .RemoteDebuggingPort = 8088
            }
            setting.CachePath = apppath + "\statecache"
            setting.RegisterScheme(New CefCustomScheme() With {
                .SchemeName = "offline",
                .DomainName = "pbrowserapp",
                .SchemeHandlerFactory = New FolderSchemeHandlerFactory(
                    rootFolder:=apppath + "\statecache\buildcache\offlineweb",
                    hostName:="pbrowserapp",
                    defaultPage:="index.html"
                 )
            })
            'setting.UserAgent = "P Browser (x64, Builder Kit, Chromium 126.0.6478.115)"
            CefSharp.Cef.Initialize(setting)
            Browser = New ChromiumWebBrowser("")
            'BrowserDev = New ChromiumWebBrowser("")
            Panel2.Controls.Add(Browser)
            'Panel1.Controls.Add(BrowserDev)
            'BrowserDev.Load("http://127.0.0.1:8088/")
            System.IO.Directory.Delete(apppath + "\statecache\buildcache\appicns", True)
            System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\appicns")
            System.IO.Directory.Delete(apppath + "\statecache\buildcache\offlineweb", True)
            System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\offlineweb")
            Timer2.Interval = settings.load("usageinterv")
            Button4.Enabled = False
            Button5.Enabled = False
            Dim rscheck As String = apppath + "\resource"
            splash.Label1.Text = "Checking for resource updates..."
            If Not System.IO.Directory.Exists(rscheck) Then
                Button1.Enabled = False
                Button2.Enabled = False
                CheckBox1.Enabled = False
                CheckBox2.Enabled = False
                Label8.Enabled = False
                Label24.Enabled = False
                Button9.Enabled = False
                BuildProjectToolStripMenuItem.Enabled = False
                TestProjectToolStripMenuItem.Enabled = False
            Else
                'chkpoint
                If Not System.IO.File.Exists(apppath + "\resource\cpd740") Then
                    MessageBox.Show("Resource not compatible to this version of Builder!" + vbNewLine + "Please update Builder and Resource to ensure compatibility.", "Resource not compatible!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Button1.Enabled = False
                    Button2.Enabled = False
                    CheckBox1.Enabled = False
                    CheckBox2.Enabled = False
                    Label8.Enabled = False
                    Label24.Enabled = False
                    Button9.Enabled = False
                    BuildProjectToolStripMenuItem.Enabled = False
                    TestProjectToolStripMenuItem.Enabled = False
                    autocfu = True
                End If
                'reschk
                If autocfu = True Then
                    Try
                        Dim client As WebClient = New WebClient()
                        Dim oresver As String = client.DownloadString("https://github.com/Pavich7/P-Browser-Builder-Resource/releases/latest/download/release_manifest.txt")
                        Dim fileReader As System.IO.StreamReader
                        fileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\version.txt")
                        Dim stringReader As String = fileReader.ReadLine()
                        If oresver.Contains(stringReader) Then
                            Label20.Visible = False
                            Label18.Visible = False
                        Else
                            Label20.Text = "Resource update available! (" + oresver + ")"
                            Label18.Text = "    Update"
                        End If
                        fileReader.Close()
                    Catch ex As Exception
                        Label20.Visible = False
                        Label18.Visible = False
                    End Try
                Else
                    Label20.Visible = False
                    Label18.Visible = False
                End If
            End If
            'buicheck
            If autocfu = True Then
                splash.Label1.Text = "Checking for builder updates..."
                Try
                    Dim client1 As WebClient = New WebClient()
                    Dim obuiver As String = client1.DownloadString("https://github.com/Pavich7/P-Browser-Builder/releases/latest/download/release_manifest.txt")
                    Dim fileReader11 As System.IO.StreamReader
                    fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\metadata\version.txt")
                    Dim stringReader11 As String = fileReader11.ReadLine()
                    If Not obuiver.Contains(stringReader11) Then
                        If Not TabControl1.TabPages.Contains(TabPage5) Then
                            TabControl1.TabPages.Add(TabPage5)
                        End If
                        TabControl1.SelectedTab = TabPage5
                        Label13.Text = "New updates available!"
                        PictureBox12.Image = My.Resources.information
                        Panel4.Visible = True
                        Label14.Text = "New updates available! (" + obuiver + ")"
                    End If
                    fileReader11.Close()
                Catch ex As Exception
                End Try
            End If
            splash.Label1.Text = "Loading user preference..."
            Dim hidesp As String = settings.load("hidesp")
            Dim cachecheck As String = apppath + "\statecache\updatecache\pbb-resource.zip"
            Button7.Enabled = False
            DevToolStripMenuItem.Visible = False
            Label23.Text = "Begin Test to start diagnostic..."
            Label25.Text = "Begin Test to start diagnostic..."
            prefer.Label42.Text = "Process ID: -"
            prefer.Label45.Text = "Diagnostic Status: Stopped"
            Button8.Enabled = False
            Button7.Enabled = False
            ProgressBar3.Value = 0
            Label7.Visible = True
            ProgressBar1.Visible = True
            If hidesp = "True" Then
                SidePanelToolStripMenuItem.Checked = False
                Me.MinimumSize = New Size(580, 560)
                Panel6.Hide()
                TabControl1.Width = TabControl1.Width + 313
            Else
                SidePanelToolStripMenuItem.Checked = True
                TabControl1.Width = TabControl1.Width + 313
            End If
            Dim sizew As String = settings.load("winstatew")
            Dim sizeh As String = settings.load("winstateh")
            Me.Size = New Size(sizew, sizeh)
            Me.WindowState = FormWindowState.Minimized
            Timer3.Start()
            Timer1.Start()
            splash.Label1.Text = "Loading completed!"
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub SupportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupportToolStripMenuItem.Click
        Browser.Load("https://github.com/Pavich7/P-Browser-Builder/issues")
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        about.Show()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Browser.Load(TextBox1.Text)
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Browser.Load(TextBox1.Text)
        End If
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
        If My.Application.CommandLineArgs.Count > 0 Then
            Dim FileNameOpenWith As String = My.Application.CommandLineArgs(0)
            If FileNameOpenWith = "--devmode" Then
                Dim fecha = IO.File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location)
                DevToolStripMenuItem.Visible = True
                Label70.Visible = True
                Label70.Text = "DEV_ENV BUILT: " + fecha + " CEFPROD:" + AssemblyInfo.AssemblyVersion
                fsstate.Label13.Visible = True
                welcome.Show()
            Else
                savemanager.loadsave(FileNameOpenWith)
                Me.Enabled = True
                Me.WindowState = FormWindowState.Normal
            End If
        Else
            welcome.Show()
        End If
        'Sent analytics
        'Dim regDate As Date = Date.Now()
        'Dim strDate As String = regDate.ToString("ddMMMyyyy")
        'Dim strHostName As String
        'Dim strIPAddress As String
        'strHostName = System.Net.Dns.GetHostName()
        'strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()
        'If stringReader01 = "True" Then
        'Browser.Load("http://pavichdev.ddns.net/api/v1-act/activate.php?ver=" + "PBrowserBuilder+startup+" + strDate + "+" + Application.ProductVersion + "+" + strHostName + "+" + strIPAddress)
        'Snooze(1)
        'Browser.Load("about:blank")
        'End If
        'fsstate
        Dim infsstate As String = settings.load("infsstate")
        If infsstate = "True" Then
            Me.Enabled = False
            fsstate.Show()
        End If
        'wnstate
        'Dim fileReader110 As System.IO.StreamReader
        'fileReader110 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\wnannounce.pbstate")
        'Dim stringReader110 As String
        'stringReader110 = fileReader110.ReadLine()
        'fileReader110.Close()
        'If stringReader110 = "True" Then
        'whatsnew.Show()
        'Dim pbcfg111 As String = apppath + "\wnannounce.pbstate"
        'Dim objWriter111 As New System.IO.StreamWriter(pbcfg111)
        'objWriter111.Write("False")
        'objWriter111.Close()
        'End If
        Dim rscheck As String = apppath + "\resource"
        If Not System.IO.Directory.Exists(rscheck) Then
            Label7.Text = "Not ready, resources required."
        End If
    End Sub

    Private Sub ClearAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearAllToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("You will lose all data! Please make sure your data is saved.", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            savemanager.loadsave("")
        End If
    End Sub

    Private Sub SubmitBugsReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubmitBugsReportToolStripMenuItem.Click
        Browser.Load("https://github.com/Pavich7/P-Browser-Builder/issues/new/choose")
    End Sub

    Private Sub ReloadPreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReloadPreviewToolStripMenuItem.Click, PictureBox11.Click
        Browser.Refresh()
    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        Label7.Text = "Installing Resource (Waiting for confirmation)"
        ProgressBar1.Value = 0
        Dim result As DialogResult = MessageBox.Show("Do you wish to install builder resource?" + vbNewLine + "Attempting to exit the builder may incomplete resources!", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            dlworker.RunWorkerAsync()
            Label7.Text = "Installing Resource..."
            Label18.Enabled = False
            Label20.Text = "Resource is being installed..."
            ProgressBar1.Style = ProgressBarStyle.Marquee
            ProgressBar1.MarqueeAnimationSpeed = 40
        Else
            Label7.Text = "Ready to build"
        End If
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles dlworker.RunWorkerCompleted
        ProgressBar1.Style = ProgressBarStyle.Blocks
        Dim result0 As DialogResult = MessageBox.Show("Do you want to delete installation cache?" + vbNewLine + "Cache can be used to reinstall using advanced sideload. Delete if you wanted to save space. You can delete it later in preference.", "Delete cache?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If (result0 = DialogResult.Yes) Then
            System.IO.Directory.Delete(apppath + "\statecache\updatecache", True)
            System.IO.Directory.CreateDirectory(apppath + "\statecache\updatecache")
        End If
        Dim result1 As DialogResult = MessageBox.Show("Installation completed but restart required for fully functional!" + vbNewLine + "Do you want to restart P Browser Builder now?", "Installation completed!", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If (result1 = DialogResult.Yes) Then
            Application.Restart()
        Else
            Label18.Visible = False
            Label20.Visible = False
            Label7.Text = "Ready to build"
        End If
    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles dlworker.DoWork
        Try
            Dim resc As String = apppath + "\resource"
            If System.IO.Directory.Exists(resc) Then
                System.IO.Directory.Delete(apppath + "\resource", True)
            End If
            System.IO.Directory.Delete(apppath + "\statecache\updatecache", True)
            System.IO.Directory.CreateDirectory(apppath + "\statecache\updatecache")
            Dim strURL As String = settings.load("resdlserver")
            Using webcli As WebClient = New WebClient()
                webcli.DownloadFile(strURL, apppath + "\statecache\updatecache\pbb-resource.zip")
            End Using
            Dim zipPath As String = apppath + "\statecache\updatecache\pbb-resource.zip"
            Dim extractPath As String = apppath
            ZipFile.ExtractToDirectory(zipPath, extractPath)
            Dim zipPath1 As String = apppath + "\resource\resourcepack\freshapp.zip"
            Dim extractPath1 As String = apppath + "\resource\testspace"
            ZipFile.ExtractToDirectory(zipPath1, extractPath1)
        Catch ex As Exception
            MessageBox.Show("Could not attempt to install resource! Builder need to restart!" + vbNewLine + ex.Message, "Error!")
            Application.Restart()
        End Try
    End Sub
    <Obsolete>
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            Dim fileReader211 As System.IO.StreamReader
            fileReader211 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\testspace\pidlock.pbs")
            Dim stringReader211 As String = fileReader211.ReadLine()
            fileReader211.Close()
            pros = Process.GetProcessById(stringReader211)
            Dim demround1 As Double = pros.WorkingSet / 1024 / 1024
            demround1 = Math.Round(demround1, 2)
            Label23.Text = "Memory Usage: " & demround1 & " MB"
            Dim demround3 As Double = ((My.Computer.Info.TotalPhysicalMemory - My.Computer.Info.AvailablePhysicalMemory) / My.Computer.Info.TotalPhysicalMemory) * 100
            demround3 = Math.Round(demround3, 2)
            Dim demround4 As Double = (My.Computer.Info.TotalPhysicalMemory - My.Computer.Info.AvailablePhysicalMemory) / 1024 / 1024 / 1024
            demround4 = Math.Round(demround4, 2)
            ProgressBar3.Value = ((My.Computer.Info.TotalPhysicalMemory - My.Computer.Info.AvailablePhysicalMemory) / My.Computer.Info.TotalPhysicalMemory) * 100
            Label25.Text = "Overall Usage: " & demround3 & " % (" & demround4 & " GB)"
            prefer.Label45.Text = "Diagnostic Status: Running"
            prefer.Label42.Text = "Process ID: " & stringReader211
        Catch ex As Exception
            Label23.Text = "Begin Test to start diagnostic..."
            Label25.Text = "Begin Test to start diagnostic..."
            Label42.Enabled = False
            Button2.Enabled = True
            Button1.Enabled = True
            Timer2.Stop()
            Button8.Enabled = False
            Button7.Enabled = False
            ProgressBar3.Value = 0
            prefer.Label45.Text = "Diagnostic Status: Stopped"
            prefer.Label42.Text = "Process ID: -"
        End Try
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
        Label23.Text = "Diagnostic Paused"
        Label25.Text = "Paused"
        prefer.Label45.Text = "Diagnostic Status: Paused"
    End Sub

    Private Sub OpenBuilderInExplorerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenBuilderInExplorerToolStripMenuItem.Click
        Process.Start(apppath)
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.Click
        CheckBox1.Text = "Start your app after build (Dedicated)"
        CheckBox2.Text = "Show your ZIP file in explorer after build"
    End Sub

    Private Sub UnlockIncompleteFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnlockIncompleteFeatureToolStripMenuItem.Click
        UnlockIncompleteFeatureToolStripMenuItem.Enabled = False
    End Sub

    Private Sub ForceUnlockDisableButtonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForceUnlockDisableButtonToolStripMenuItem.Click
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
        Button6.Enabled = True
        Button7.Enabled = True
        Button8.Enabled = True
        Label4.Enabled = True
        Label8.Enabled = True
        RadioButton2.Enabled = True
        RadioButton3.Enabled = True
        CheckBox1.Enabled = True
        CheckBox2.Enabled = True
        ForceUnlockDisableButtonToolStripMenuItem.Enabled = True
        UnlockIncompleteFeatureToolStripMenuItem.Enabled = True
    End Sub

    Private Sub ShowSplashScreenToolStripMenuItem_Click(sender As Object, e As EventArgs)
        splash.Show()
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If ProgressBar1.Value = 0 Then
            Label28.Visible = False
            Label29.Visible = False
        Else
            Label28.Visible = True
            Label29.Visible = True
            Label28.Text = ProgressBar1.Value
        End If
    End Sub

    Private Sub ReleaseNoteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseNoteToolStripMenuItem.Click, Label48.Click, Label57.Click
        Browser.Load("https://github.com/Pavich7/P-Browser-Builder/releases/")
        TabControl1.SelectedTab = TabPage1
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = False Then
            Button4.Enabled = False
            Button5.Enabled = False
            TabControl1.TabPages.Remove(TabPage4)
        Else
            Button4.Enabled = True
            Button5.Enabled = True
            TabControl1.TabPages.Add(TabPage4)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TabControl1.SelectedTab = TabPage4
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter URL before getting page title.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            If My.Settings.tempWebTitle = "" Then
                MessageBox.Show("Current websites doesn't have title!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                TextBox2.Text = My.Settings.tempWebTitle
            End If
        End If
    End Sub
    Public Sub Browser_TitleChanged(sender As Object, e As CefSharp.TitleChangedEventArgs) Handles Browser.TitleChanged
        Dim currentTitle As String = e.Title
        My.Settings.tempWebTitle = currentTitle
    End Sub

    Private Sub FToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FToolStripMenuItem.Click
        settings.save("infsstate", "True")
    End Sub

    Private Sub RestartInRestoreModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartInRestoreModeToolStripMenuItem.Click
        settings.save("inrsstate", "True")
        Application.Restart()
    End Sub

    Private Sub ShowSplashScreenToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ShowSplashScreenToolStripMenuItem.Click
        splash.Show()
    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click, OpenRemoteDebuggingToolStripMenuItem.Click
        Browser.ShowDevTools
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        System.IO.Directory.Delete(apppath + "\statecache\buildcache\appicns", True)
        System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\appicns")
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.Title = "Choose your icons file"
        OpenFileDialog1.Filter = "Icons Files|*.ico"
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
            My.Settings.tempIcoLoc = OpenFileDialog1.FileName
            My.Computer.FileSystem.CopyFile(OpenFileDialog1.FileName, apppath + "\statecache\buildcache\appicns\appicns.ico")
            Label16.Text = "Application icons (" + Path.GetFileName(OpenFileDialog1.FileName) + ")"
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TabControl1.SelectedTab = TabPage7
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = False Then
            Button6.Enabled = False
            TabControl1.TabPages.Remove(TabPage7)
        Else
            Button6.Enabled = True
            TabControl1.TabPages.Add(TabPage7)
        End If
    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click
        Button6.Visible = True
        CheckBox4.Visible = True
        Label24.Visible = False
    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click
        MessageBox.Show("After fresh install you will need to install resource to build. (Download size: approx. 150 MB)", "Info...", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub StartWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartWindowToolStripMenuItem.Click
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            savemanager.loadsave("")
            welcome.Show()
        Else
            Dim result As DialogResult = MessageBox.Show("You will lose all data! Please make sure your data is saved.", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If (result = DialogResult.Yes) Then
                savemanager.loadsave("")
                welcome.Show()
            End If
        End If
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        TextBox3.Text = "944"
        TextBox4.Text = "573"
        TextBox5.Text = "100"
        CheckBox5.Checked = False
        CheckBox6.Checked = False
        CheckBox7.Checked = False
    End Sub

    Private Sub ResetWhatsNewStateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetWhatsNewStateToolStripMenuItem.Click
        Dim pbcfg1 As String = apppath + "\wnannounce.pbstate"
        Dim objWriter1 As New System.IO.StreamWriter(pbcfg1)
        objWriter1.Write("False")
        objWriter1.Close()
    End Sub

    Private Sub Label37_Click(sender As Object, e As EventArgs) Handles Label37.Click
        Process.Start(apppath + "\binary")
    End Sub

    Private Sub Label38_Click(sender As Object, e As EventArgs) Handles Label38.Click
        Process.Start(apppath + "\binarypkg")
    End Sub

    Private Sub WhatsNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WhatsNewToolStripMenuItem.Click
        If Not TabControl1.TabPages.Contains(TabPage3) Then
            TabControl1.TabPages.Add(TabPage3)
        End If
        TabControl1.SelectedTab = TabPage3
    End Sub

    Private Sub CefSharpLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CefSharpLogToolStripMenuItem.Click
        Try
            Process.Start(apppath + "\debug.log")
        Catch ex As Exception
            MessageBox.Show("Log not found! Maybe you can load some web and try again.", "Error!")
        End Try
    End Sub

    Private Sub ChromiumLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChromiumLogToolStripMenuItem.Click
        Try
            Process.Start(apppath + "\statecache\chrome_debug.log")
        Catch ex As Exception
            MessageBox.Show("Log not found! Maybe you can load some web and try again.", "Error!")
        End Try
    End Sub

    Private Sub Label39_Click(sender As Object, e As EventArgs) Handles Label39.Click
        Process.Start(logpath)
    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click
        Clipboard.SetText(RichTextBox1.Text)
    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click
        RichTextBox1.Text = File.ReadAllText(logpath)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            Label7.Text = "Flushing in progress..."
            ProgressBar1.Value = 20
            System.IO.Directory.Delete(apppath + "\resource\testspace\startlog", True)
            System.IO.Directory.CreateDirectory(apppath + "\resource\testspace\startlog")
            ProgressBar1.Value = 100
            Label39.Visible = False
            RichTextBox1.Text = "Logs flushed! Start a test again to view app start log..."
            Label7.Text = "Log flush completed!"
            Snooze(3)
            ProgressBar1.Value = 0
            Label7.Text = "Ready to build"
        Catch ex As Exception
            MessageBox.Show("Please close built app first before perform this action.", "Failed!")
            Label7.Text = "Log flush failed!"
            ProgressBar1.Value = 0
        End Try
    End Sub

    Private Sub SaveToFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToFileToolStripMenuItem.Click
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            MessageBox.Show("To save project, please enter Website URL and App Name.", "Error!")
        Else
            savemanager.savetofile()
        End If
    End Sub

    Private Sub OpenProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenProjectToolStripMenuItem.Click
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            OpenFileDialog1.Multiselect = False
            OpenFileDialog1.Title = "Open P Browser Builder Project"
            OpenFileDialog1.Filter = "P Browser Builder Project|*.pbproj"
            If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                savemanager.loadsave(OpenFileDialog1.FileName)
            End If
        Else
            Dim result As DialogResult = MessageBox.Show("You will lose all data! Please make sure your data is saved.", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If (result = DialogResult.Yes) Then
                OpenFileDialog1.Multiselect = False
                OpenFileDialog1.Title = "Open P Browser Builder Project"
                OpenFileDialog1.Filter = "P Browser Builder Project|*.pbproj"
                If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                    savemanager.loadsave(OpenFileDialog1.FileName)
                End If
            End If
        End If
    End Sub

    Dim hidenoti As Boolean = True
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        SidePanelToolStripMenuItem.Checked = False
        If hidenoti = True Then
            MessageBox.Show("You can unhide side panel by click on View > Side Panel" + vbNewLine + "or using Ctrl + R Shortcut", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
            hidenoti = False
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked = False Then
            PictureBox1.Visible = True
            Dim x As Integer = Label17.Location.X
            Dim y As Integer = Label17.Location.Y
            Label17.Location = New Point(x + 22, y)
        Else
            PictureBox1.Visible = False
            Dim x As Integer = Label17.Location.X
            Dim y As Integer = Label17.Location.Y
            Label17.Location = New Point(x - 22, y)
        End If
    End Sub

    Private Sub SidePanelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SidePanelToolStripMenuItem.CheckedChanged
        If SidePanelToolStripMenuItem.Checked = True Then
            Panel6.Show()
            Me.MinimumSize = New Size(900, 560)
            TabControl1.Width = TabControl1.Width - 313
        Else
            Panel6.Hide()
            Me.MinimumSize = New Size(580, 560)
            TabControl1.Width = TabControl1.Width + 313
        End If
    End Sub

    Private Sub DesignViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesignViewToolStripMenuItem.Click
        If DesignViewToolStripMenuItem.Checked = False Then
            TabControl1.TabPages.Remove(TabPage1)
        Else
            TabControl1.TabPages.Add(TabPage1)
        End If
    End Sub

    Private Sub StartLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartLogToolStripMenuItem.Click
        If StartLogToolStripMenuItem.Checked = False Then
            TabControl1.TabPages.Remove(TabPage2)
        Else
            TabControl1.TabPages.Add(TabPage2)
        End If
    End Sub

    Private Sub NormalToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NormalToolStripMenuItem1.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub MaximizeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaximizeToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub MinimizedToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles MinimizedToolStripMenuItem1.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub CleanBinaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CleanBinaryToolStripMenuItem.Click
        Try
            Label7.Text = "Cleaning in progress..."
            ProgressBar1.Value = 20
            System.IO.Directory.Delete(apppath + "\binary", True)
            ProgressBar1.Value = 50
            System.IO.Directory.CreateDirectory(apppath + "\binary")
            System.IO.Directory.Delete(apppath + "\binarypkg", True)
            System.IO.Directory.CreateDirectory(apppath + "\binarypkg")
            ProgressBar1.Value = 100
            Label7.Text = "Cleanup completed!"
            Snooze(3)
            ProgressBar1.Value = 0
            Label7.Text = "Ready to build"
        Catch ex As Exception
            MessageBox.Show("Please close built app first before perform this action.", "Failed!")
            Label7.Text = "Cleanup failed!"
            ProgressBar1.Value = 0
            Snooze(3)
            Label7.Text = "Ready to build"
        End Try
    End Sub

    Private Sub GettingStartedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GettingStartedToolStripMenuItem.Click
        If Not TabControl1.TabPages.Contains(TabPage6) Then
            TabControl1.TabPages.Add(TabPage6)
        End If
        TabControl1.SelectedTab = TabPage6
    End Sub

    Private Sub LoadSampleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadSampleToolStripMenuItem.Click, Label64.Click
        Dim result As DialogResult = MessageBox.Show("You will lose all data! Please make sure your data is saved.", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            savemanager.loadsave(apppath + "\assets\sample\sample1.pbproj")
            TabControl1.TabPages.Add(TabPage6)
        End If
    End Sub

    Private Sub Label42_Click(sender As Object, e As EventArgs) Handles Label42.Click
        Try
            pros.Kill()
        Catch ex As Exception
            MessageBox.Show("Process require Administrator to kill" & vbNewLine & "Please run as admin for full functional", "Error!")
        End Try
    End Sub

    Private Sub ActionToolboxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActionToolboxToolStripMenuItem.Click
        If ActionToolboxToolStripMenuItem.Checked = False Then
            Panel7.Visible = False
            Me.WindowState = FormWindowState.Normal
            Me.Size = New Size(1232, 646)
            Panel10.Height = 500
        Else
            Panel7.Visible = True
            Me.WindowState = FormWindowState.Normal
            Me.Size = New Size(1232, 646)
            Panel10.Height = 450
        End If
    End Sub

    Private Sub NavigationBarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NavigationBarToolStripMenuItem.Click
        If NavigationBarToolStripMenuItem.Checked = False Then
            PictureBox17.Visible = False
            PictureBox18.Visible = False
            Label22.Visible = False
            PictureBox11.Visible = False
            PictureBox16.Visible = False
        Else
            PictureBox17.Visible = True
            PictureBox18.Visible = True
            Label22.Visible = True
            PictureBox11.Visible = True
            PictureBox16.Visible = True
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click, PreferenceToolStripMenuItem.Click
        prefer.Show()
    End Sub

    Private Sub TextBox_KeyPress_Check(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click, Label49.Click
        ProgressBar1.Style = ProgressBarStyle.Marquee
        ProgressBar1.MarqueeAnimationSpeed = 40
        Label7.Text = "Checking for Updates..."
        Label13.Text = "Checking for Updates..."
        PictureBox12.Image = My.Resources.more
        Panel4.Visible = False
        If Not TabControl1.TabPages.Contains(TabPage5) Then
            TabControl1.TabPages.Add(TabPage5)
        End If
        TabControl1.SelectedTab = TabPage5
        cfuworker.RunWorkerAsync()
    End Sub
    Dim obuiver As String = ""
    Dim oresver As String = ""
    Private Sub cfuworker_DoWork(sender As Object, e As DoWorkEventArgs) Handles cfuworker.DoWork
        Try
            Dim client As WebClient = New WebClient()
            obuiver = client.DownloadString("https://github.com/Pavich7/P-Browser-Builder/releases/latest/download/release_manifest.txt")
            oresver = client.DownloadString("https://github.com/Pavich7/P-Browser-Builder-Resource/releases/latest/download/release_manifest.txt")
        Catch ex As Exception
            MessageBox.Show("Unable to check for update! Please try again later.", "Update Utility", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub cfuworker_DoWorkComplete(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles cfuworker.RunWorkerCompleted
        'buivercheck
        Dim bfileReader As System.IO.StreamReader
        bfileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\metadata\version.txt")
        Dim bstringReader As String = bfileReader.ReadLine()
        If obuiver.Contains(bstringReader) Then
            Label13.Text = "Latest version installed!"
            PictureBox12.Image = My.Resources.check
        ElseIf obuiver = "" Then
            Label13.Text = "Unable to check for update!"
            PictureBox12.Image = My.Resources.remove
        Else
            Label13.Text = "New updates available!"
            PictureBox12.Image = My.Resources.information
            Panel4.Visible = True
            Label14.Text = "New updates available! (" + obuiver + ")"
        End If
        bfileReader.Close()
        'resvercheck
        Dim rfileReader As System.IO.StreamReader
        rfileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\version.txt")
        Dim rstringReader As String = rfileReader.ReadLine()
        If oresver.Contains(rstringReader) Then
            Label20.Visible = False
            Label18.Visible = False
        Else
            Label20.Visible = True
            Label18.Visible = True
            Label20.Text = "Resource update available! (" + oresver + ")"
            Label18.Text = "    Update"
        End If
        rfileReader.Close()

        ProgressBar1.Style = ProgressBarStyle.Blocks
        Label7.Text = "Ready to build"
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        If My.Settings.tempIcoLoc = "" Then
            MessageBox.Show("Please choose icon first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Process.Start(My.Settings.tempIcoLoc)
        End If
    End Sub

    Private Sub CheckBox9_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox9.CheckedChanged
        If CheckBox9.Checked = True Then
            TextBox6.Visible = True
        Else
            TextBox6.Visible = False
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            Label43.Enabled = False
        Else
            Label43.Enabled = True
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        System.IO.Directory.Delete(apppath + "\statecache\buildcache\offlineweb", True)
        System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\offlineweb")
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.tempOffWebLoc = FolderBrowserDialog1.SelectedPath
            My.Computer.FileSystem.CopyDirectory(FolderBrowserDialog1.SelectedPath, apppath + "\statecache\buildcache\offlineweb\")
            Label46.Text = "Offline websites (Ready)"
            PictureBox23.Visible = True
        End If
    End Sub

    Private Sub Label46_Click(sender As Object, e As EventArgs) Handles Label46.Click
        If My.Settings.tempOffWebLoc = "" Then
            MessageBox.Show("Please choose your offline web folder first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Process.Start(My.Settings.tempOffWebLoc)
        End If
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        TabControl1.TabPages.Remove(TabPage3)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click, Button5.Click
        If ComboBox1.Text = "Warning" Then
            MessageBox.Show(TextBox7.Text, TextBox8.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            MessageBox.Show(TextBox7.Text, TextBox8.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        MessageBox.Show("Patch Download is smaller, as it only replaces changed files. A full download provides a fresh install. Both keeps your settings. " + vbNewLine + "Note: Patch updates are only supported for the latest release before the new one.", "Updates Help", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/releases/latest/download/Update.P.Browser.Builder.exe")
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/releases/latest/download/Install.P.Browser.Builder.exe")
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        TabControl1.TabPages.Remove(TabPage5)
    End Sub

    Private Sub EnlargeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnlargeToolStripMenuItem.CheckedChanged
        If EnlargeToolStripMenuItem.Checked = False Then
            Me.WindowState = FormWindowState.Normal
            Me.Size = New Size(1232, 798)
            TabControl1.Dock = DockStyle.None
            TabControl1.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom
            PictureBox16.Image = My.Resources.maximize
            SidePanelToolStripMenuItem.Enabled = True
        Else
            TabControl1.Dock = DockStyle.Fill
            PictureBox16.Image = My.Resources.minimize
            SidePanelToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub PictureBox16_Click(sender As Object, e As EventArgs) Handles PictureBox16.Click
        If EnlargeToolStripMenuItem.Checked = False Then
            EnlargeToolStripMenuItem.Checked = True
        Else
            EnlargeToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub PictureBox17_Click(sender As Object, e As EventArgs) Handles PictureBox17.Click
        Browser.Back
    End Sub

    Private Sub PictureBox18_Click(sender As Object, e As EventArgs) Handles PictureBox18.Click
        Browser.Forward
    End Sub

    Private Sub PictureBox19_Click(sender As Object, e As EventArgs) Handles PictureBox19.Click
        TabControl1.TabPages.Remove(TabPage6)
    End Sub

    Private Sub Label58_Click(sender As Object, e As EventArgs) Handles Label58.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/issues")
    End Sub

    Private Sub Label61_Click(sender As Object, e As EventArgs) Handles Label61.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/wiki")
    End Sub

    Private Sub GoBackToDefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoBackToDefaultToolStripMenuItem.Click
        Application.Restart()
    End Sub

    Private Sub KillSplashToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KillSplashToolStripMenuItem.Click
        splash.Close()
    End Sub

    Private Sub PictureBox23_Click(sender As Object, e As EventArgs) Handles PictureBox23.Click
        If My.Settings.tempOffWebLoc = "" Then
            MessageBox.Show("Please choose your offline web folder first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Browser.Load("offline://pbrowserapp/")
        End If
    End Sub

    Private Sub Label71_Click(sender As Object, e As EventArgs) Handles Label71.Click
        Process.Start("https://www.thoughtco.com/how-to-use-processstart-in-vbnet-3424455")
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Try
            If TextBox9.Text IsNot "" Then
                Process.Start(TextBox9.Text)
            Else
                MessageBox.Show("Script is empty!", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Failed to run script!", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetCfustateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetCfustateToolStripMenuItem.Click
        Dim filePath As String = apppath + "\autocfu.pbstate"
        File.WriteAllText(filePath, "")
    End Sub
End Class