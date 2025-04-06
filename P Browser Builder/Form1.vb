Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Reflection.Emit
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports CefSharp
Imports CefSharp.DevTools
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
                    Dim icnexist As String = apppath + "\resource\testspace\appicns.ico"
                    If System.IO.File.Exists(icnexist) Then
                        My.Computer.FileSystem.DeleteFile(apppath + "\resource\testspace\appicns.ico")
                    Else
                        Dim icnshave As String = apppath + "\statecache\buildcache\appicns\appicns.ico"
                        If System.IO.File.Exists(icnshave) Then
                            My.Computer.FileSystem.CopyFile(apppath + "\statecache\buildcache\appicns\appicns.ico", apppath + "\resource\testspace\appicns.ico")
                        End If
                    End If
                    ProgressBar1.Value = 100
                    MessageBox.Show("Build Completed! Click continue to test app." + vbNewLine + "Some features will not available in Testing.", "Build Completed!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Process.Start(apppath + "\resource\testspace\P Browser App.exe")
                    Label7.Text = "Build completed!"
                    Snooze(2)
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
                    Timer2.Start()
                    Label42.Enabled = True
                    Button7.Enabled = False
                    Button8.Enabled = True
                Catch ex As Exception
                    MessageBox.Show("Please close previous test app first before perform this action.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Label7.Text = "Test failed!"
                    Button2.Enabled = True
                    Button1.Enabled = True
                    ProgressBar1.Value = 0
                End Try
            Else
                MessageBox.Show("Build Failed! Unable to find manifest data! Please reinstall resource.", "Build Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Label7.Text = "Build failed!"
            End If
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.Click
        CheckBox1.Text = "Start your app after build"
        CheckBox2.Text = "Show your app in explorer after build"
    End Sub

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
                Label7.Text = "Building in progress..."
                System.IO.Directory.Delete(apppath + "\binary", True)
                System.IO.Directory.CreateDirectory(apppath + "\binary")
                Dim zipPath As String = apppath + "\resource\resourcepack\freshapp.zip"
                Dim extractPath As String = apppath + "\binary"
                ZipFile.ExtractToDirectory(zipPath, extractPath)
                ProgressBar1.Value = 20
                Using writer As New StreamWriter(apppath + "\binary\manifest.pbcfg", False)
                    'builderdata
                    'progdata
                    'contextmenu
                    'fsmsg.title
                    'fsmsg.desc
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
                    writer.WriteLine(welcomemessage.TextBox1.Text)
                    writer.WriteLine(welcomemessage.TextBox2.Text)
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
                ProgressBar1.Value = 50
                My.Computer.FileSystem.RenameFile(apppath + "\binary\P Browser App.exe", TextBox2.Text + ".exe")
                Dim icnshave As String = apppath + "\statecache\buildcache\appicns\appicns.ico"
                If System.IO.File.Exists(icnshave) Then
                    My.Computer.FileSystem.CopyFile(apppath + "\statecache\buildcache\appicns\appicns.ico", apppath + "\binary\appicns.ico", True)
                End If
                ProgressBar1.Value = 70
                ProgressBar1.Value = 80
                ProgressBar1.Value = 100
                If RadioButton3.Checked = True Then
                    System.IO.Directory.Delete(apppath + "\binarypkg", True)
                    System.IO.Directory.CreateDirectory(apppath + "\binarypkg")
                    Dim zipsource As String = apppath + "\binary\"
                    Dim zipbin As String = apppath + "\binarypkg\" + TextBox2.Text + ".zip"
                    ZipFile.CreateFromDirectory(zipsource, zipbin, CompressionLevel.Optimal, False)
                End If
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
                        Process.Start(My.Settings.tempScript)
                    Catch ex As Exception
                        MessageBox.Show("Cannot run script! Your app is build and ready!", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                Label7.Text = "Build completed!"
            Catch ex As Exception
                MessageBox.Show("Build Failed! Incomplete or corrupted Data please reinstall builder.", "Build Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Label7.Text = "Build failed!"
            End Try
        End If
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
        Dim objWriterw As New System.IO.StreamWriter(apppath + "\statedata\state.builder.winstatew.pbcfg")
        Dim objWriterh As New System.IO.StreamWriter(apppath + "\statedata\state.builder.winstateh.pbcfg")
        objWriterw.Write(Me.Width)
        objWriterh.Write(Me.Height)
        objWriterw.Close()
        objWriterh.Close()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        apppath = Application.StartupPath()
        'Init structure check
        If Not System.IO.Directory.Exists(apppath + "\imgassets") Then
            Me.Enabled = False
            errorencounter.RichTextBox1.Text = "Bad data structure! Reinstall might fixed this problem."
            errorencounter.Show()
        End If
        If Not System.IO.Directory.Exists(apppath + "\binary") Then System.IO.Directory.CreateDirectory(apppath + "\binary")
        If Not System.IO.Directory.Exists(apppath + "\binarypkg") Then System.IO.Directory.CreateDirectory(apppath + "\binarypkg")
        If Not System.IO.Directory.Exists(apppath + "\metadata") Then
            Me.Enabled = False
            errorencounter.RichTextBox1.Text = "Bad data structure! Reinstall might fixed this problem."
            errorencounter.Show()
        End If
        If Not System.IO.Directory.Exists(apppath + "\statecache") Then System.IO.Directory.CreateDirectory(apppath + "\statecache")
        If Not System.IO.Directory.Exists(apppath + "\statecache\buildcache") Then System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache")
        If Not System.IO.Directory.Exists(apppath + "\statecache\buildcache\appicns") Then System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\appicns")
        If Not System.IO.Directory.Exists(apppath + "\statecache\updatecache") Then System.IO.Directory.CreateDirectory(apppath + "\statecache\updatecache")
        If Not System.IO.Directory.Exists(apppath + "\statedata") Then
            Dim zipPath As String = apppath + "\packages\datatemplate.zip"
            Dim extractPath As String = apppath + "\"
            ZipFile.ExtractToDirectory(zipPath, extractPath)
        End If
        'Reset state Check
        Dim fileReader19 As System.IO.StreamReader
        fileReader19 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.inrsstate.pbcfg")
        Dim stringReader19 As String
        stringReader19 = fileReader19.ReadLine()
        fileReader19.Close()
        Button6.Visible = False
        CheckBox4.Visible = False
        PictureBox14.Enabled = False
        Label39.Visible = False
        Button6.Enabled = False
        RadioButton2.Checked = True
        CheckBox1.Checked = True
        Label42.Enabled = False
        TextBox6.Visible = False
        'Reset
        If stringReader19 = "True" Then
            Me.Enabled = False
            Try
                splash.Hide()
                MessageBox.Show("P Browser Builder is in Restore Mode!" + vbNewLine + "This mode will guide you to reset all Builder Settings.", "Warning!")
                Dim result As DialogResult = MessageBox.Show("Do you wish to reset all setting?" + vbNewLine + "This cannot be undone!", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If (result = DialogResult.Yes) Then
                    Dim result1 As DialogResult = MessageBox.Show("Do you wish to delete resource also?" + vbNewLine + "You can reinstall anytime via news feed.", "Resource setting", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    splash.Show()
                    'Delete Res if prompted
                    If (result1 = DialogResult.Yes) Then
                        Dim rscheck1 As String = apppath + "\resource"
                        If System.IO.Directory.Exists(rscheck1) Then
                            System.IO.Directory.Delete(apppath + "\resource", True)
                        End If
                    End If
                    'Delete config files
                    System.IO.Directory.Delete(apppath + "\statedata", True)
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
                    MessageBox.Show("Operation Completed!", "Success!")
                    Dim result11 As DialogResult = MessageBox.Show("Do you wish to restart?" + vbNewLine + "YES to restart, NO to shutdown.", "Restart?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If (result11 = DialogResult.Yes) Then
                        Application.Restart()
                    Else
                        Application.Exit()
                    End If
                Else
                    Dim objWriter11 As New System.IO.StreamWriter(apppath + "\statedata\setting.builder.inrsstate.pbcfg")
                    objWriter11.Write("False")
                    objWriter11.Close()
                    MessageBox.Show("Operation Aborted, Nothing Happened! Builder needs restart.", "Aborted!")
                    Application.Restart()
                End If
            Catch ex As Exception
                Dim objWriter111 As New System.IO.StreamWriter(apppath + "\statedata\setting.builder.inrsstate.pbcfg")
                objWriter111.Write("False")
                objWriter111.Close()
                MessageBox.Show("Failed to reset. Builder needs restart." + vbNewLine + ex.Message, "Fatal Error!")
                Application.Restart()
            End Try
        Else
            'Init Cef
            Dim setting As New CefSettings With {
                .RemoteDebuggingPort = 8088
            }
            setting.CachePath = apppath + "\statecache"
            'setting.UserAgent = "P Browser (x64, Builder Kit, Chromium 126.0.6478.115)"
            CefSharp.Cef.Initialize(setting)
            Browser = New ChromiumWebBrowser("")
            'BrowserDev = New ChromiumWebBrowser("")
            Panel2.Controls.Add(Browser)
            'Panel1.Controls.Add(BrowserDev)
            'BrowserDev.Load("http://127.0.0.1:8088/")
            Dim fileReader1 As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.usageinterv.pbcfg")
            Dim stringReader1 As String = fileReader1.ReadLine()
            Timer2.Interval = stringReader1
            fileReader1.Close()
            Button4.Enabled = False
            Button5.Enabled = False
            Dim rscheck As String = apppath + "\resource"
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
                'reschk
                Try
                    Dim client As WebClient = New WebClient()
                    Dim oresver As String = client.DownloadString("https://github.com/Pavich7/P-Browser-Builder-Resource/releases/latest/download/release_manifest.txt")
                    Dim fileReader As System.IO.StreamReader
                    fileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\version.txt")
                    Dim stringReader As String = fileReader.ReadLine()
                    If oresver.Contains(stringReader) Then
                        Label20.Visible = False
                        Label18.Visible = False
                        Panel4.Size = New Size(265, 264)
                    Else
                        Label20.Text = "Resource update available! (" + oresver + ")"
                        Label18.Text = "    Update"
                    End If
                    fileReader.Close()
                Catch ex As Exception
                    Label20.Visible = False
                    Label18.Visible = False
                    Panel4.Size = New Size(265, 264)
                End Try
                'chkpoint
                If Not System.IO.File.Exists(apppath + "\resource\cpd700") Then
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
                End If
            End If

            Dim fileReader111 As System.IO.StreamReader
            fileReader111 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.hidesp.pbcfg")
            Dim stringReader111 As String = fileReader111.ReadLine()

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
            Label15.Visible = False
            Label7.Visible = True
            Label7.Text = "Fetching in progress..."
            ProgressBar1.Visible = True
            If stringReader111 = "True" Then
                SidePanelToolStripMenuItem.Checked = False
                Me.MinimumSize = New Size(580, 630)
                Panel6.Hide()
                TabControl1.Width = TabControl1.Width + 313
            Else
                SidePanelToolStripMenuItem.Checked = True
                TabControl1.Width = TabControl1.Width + 313
            End If
            fileReader111.Close()
            Dim fileReaderw As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\state.builder.winstatew.pbcfg")
            Dim stringReaderw As String = fileReaderw.ReadLine()
            Dim fileReaderh As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\state.builder.winstateh.pbcfg")
            Dim stringReaderh As String = fileReaderh.ReadLine()
            Me.Size = New Size(stringReaderw, stringReaderh)
            fileReaderh.Close()
            fileReaderw.Close()
            Me.WindowState = FormWindowState.Minimized
            Timer3.Start()
            Timer1.Start()
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
        'Dim fileReader01 As System.IO.StreamReader
        'fileReader01 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.datacol.pbcfg")
        'Dim stringReader01 As String
        'stringReader01 = fileReader01.ReadLine()
        'fileReader01.Close()
        If My.Application.CommandLineArgs.Count > 0 Then
            Dim FileNameOpenWith As String = My.Application.CommandLineArgs(0)
            savemanager.loadsave(FileNameOpenWith)
            Me.Enabled = True
            Me.WindowState = FormWindowState.Normal
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
        Dim fileReader0 As System.IO.StreamReader
        fileReader0 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.infsstate.pbcfg")
        Dim stringReader0 As String
        stringReader0 = fileReader0.ReadLine()
        fileReader0.Close()
        If stringReader0 = "True" Then
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
        'nfstart
        Dim fileReader999 As System.IO.StreamReader
        fileReader999 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.nfstartfetch.pbcfg")
        Dim stringReader999 As String
        stringReader999 = fileReader999.ReadLine()
        If stringReader999 = "True" Then
            Try
                ProgressBar1.Value = 10
                Dim client As WebClient = New WebClient()
                Dim nf1cont As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v4/feedcontent.txt")
                Dim lines As String() = nf1cont.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                ProgressBar1.Value = 50
                If lines.Length > 0 Then Label12.Text = lines(0)
                If lines.Length > 1 Then Label13.Text = lines(1)
                If lines.Length > 2 Then Label14.Text = lines(2)
                ProgressBar1.Value = 100
                Label7.Text = "Ready to build"
                ProgressBar1.Value = 0
            Catch ex As Exception
                Label7.Text = "Ready to build"
                ProgressBar1.Value = 0
                Label15.Visible = True
                Label14.Visible = False
                Label12.Text = "Running in offline mode"
                Label13.Text = "Can't establish connection and fetch news feed with server."
            End Try
        Else
            Label7.Text = "Ready to build"
            ProgressBar1.Value = 0
            Label15.Visible = False
            Label14.Visible = False
            Label12.Text = "News Feed disabled"
            Label13.Text = "News Feed has been disabled. You can re-enable in feed setting."
        End If
        Dim rscheck As String = apppath + "\resource"
        If Not System.IO.Directory.Exists(rscheck) Then
            Label7.Text = "Not ready, resources required."
        End If
        fileReader999.Close()
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Try
            Label15.Text = "Please wait..."
            Label15.Enabled = False
            Dim client As WebClient = New WebClient()
            Dim nf1cont As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v4/feedcontent.txt")
            Dim lines As String() = nf1cont.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            If lines.Length > 0 Then Label12.Text = lines(0)
            If lines.Length > 1 Then Label13.Text = lines(1)
            If lines.Length > 2 Then Label14.Text = lines(2)
            Label15.Visible = False
            Label14.Visible = True
        Catch ex As Exception
            MessageBox.Show("Cannot connect!", "Error!")
            Label15.Text = "Try again"
            Label15.Enabled = True
        End Try
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
        Label23.Text = "Memory Usage: Paused"
        Label25.Text = "Paused"
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
            Dim fileReader As System.IO.StreamReader
            fileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.resdlserver.pbcfg")
            Dim stringReader As String
            stringReader = fileReader.ReadLine()
            Dim strURL As String = stringReader
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
    Private Sub OpenRemoteDebuggingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenRemoteDebuggingToolStripMenuItem.Click
        Process.Start("http://127.0.0.1:8088/")
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
        Dim result As DialogResult = MessageBox.Show("Force Unlocking the button is extremely dangerous." + vbNewLine + "YOUR MAY NEED TO REINSTALL YOUR BUILDER IN CASE OF DAMAGED!" + vbNewLine + "Do you want to process it?", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
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
        End If
    End Sub

    Private Sub ResetExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub ReToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReToolStripMenuItem.Click
        Application.Restart()
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

    Private Sub ReleaseNoteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseNoteToolStripMenuItem.Click
        Browser.Load("https://github.com/Pavich7/P-Browser-Builder/releases/")
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = False Then
            Button4.Enabled = False
            Button5.Enabled = False
            welcomemessage.Close()
        Else
            Button4.Enabled = True
            Button5.Enabled = True
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        welcomemessage.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        MessageBox.Show(welcomemessage.TextBox2.Text, welcomemessage.TextBox1.Text)
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
        Dim pbcfg As String = apppath + "\statedata\setting.builder.infsstate.pbcfg"
        Dim objWriter As New System.IO.StreamWriter(pbcfg)
        objWriter.Write("True")
        objWriter.Close()
    End Sub

    Private Sub RestartInRestoreModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartInRestoreModeToolStripMenuItem.Click
        Dim objWriter As New System.IO.StreamWriter(apppath + "\statedata\setting.builder.inrsstate.pbcfg")
        objWriter.Write("True")
        objWriter.Close()
        Application.Restart()
    End Sub

    Private Sub ShowSplashScreenToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ShowSplashScreenToolStripMenuItem.Click
        splash.Show()
    End Sub


    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click
        Browser.Forward
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Browser.Back
    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click
        Process.Start("http://127.0.0.1:8088/")
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
        customscript.Show()
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            Button6.Enabled = True
        Else
            Button6.Enabled = False
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
        Dim result As DialogResult = MessageBox.Show("You will lose all data! Please make sure your data is saved.", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            savemanager.loadsave("")
            welcome.Show()
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
        whatsnew.Show()
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
        Dim result As DialogResult = MessageBox.Show("You will lose all data! Please make sure your data is saved.", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            OpenFileDialog1.Multiselect = False
            OpenFileDialog1.Title = "Open P Browser Builder Project"
            OpenFileDialog1.Filter = "P Browser Builder Project|*.pbproj"
            If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                savemanager.loadsave(OpenFileDialog1.FileName)
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
            Me.MinimumSize = New Size(900, 630)
            TabControl1.Width = TabControl1.Width - 313
        Else
            Panel6.Hide()
            Me.MinimumSize = New Size(580, 630)
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
        Catch ex As Exception
            MessageBox.Show("Please close built app first before perform this action.", "Failed!")
            Label7.Text = "Cleanup failed!"
            ProgressBar1.Value = 0
        End Try
    End Sub

    Private Sub GettingStartedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GettingStartedToolStripMenuItem.Click
        getstart.Show()
    End Sub

    Private Sub LoadSampleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadSampleToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("You will lose all data! Please make sure your data is saved.", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            savemanager.loadsave(apppath + "\sample\sample1.pbproj")
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
            Label9.Visible = False
            Label21.Visible = False
            Label22.Visible = False
            PictureBox11.Visible = False
        Else
            Label9.Visible = True
            Label21.Visible = True
            Label22.Visible = True
            PictureBox11.Visible = True
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

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click
        Dim client As WebClient = New WebClient()
        Dim obuiver As String = client.DownloadString("https://github.com/Pavich7/P-Browser-Builder/releases/latest/download/release_manifest.txt")
        Dim bfileReader As System.IO.StreamReader
        bfileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\metadata\version.txt")
        Dim bstringReader As String = bfileReader.ReadLine()
        If obuiver.Contains(bstringReader) Then
            MessageBox.Show("Latest version installed!", "Update Utility", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim result As DialogResult = MessageBox.Show("Update available! Do you wish to download an update to the latest version?", "Confirmation?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (result = DialogResult.Yes) Then
                Dim patchr As DialogResult = MessageBox.Show("Would you Like to download the patch? It's smaller, as it only replaces changed files. A full download provides a fresh install. Both keeps your settings. " + vbNewLine + "Note: Patch updates are only supported for the latest release before the new one. " + vbNewLine + "Select YES for the patch or NO for the full download.", "Update options...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If (patchr = DialogResult.Yes) Then
                    Process.Start("https://github.com/Pavich7/P-Browser-Builder/releases/latest/download/Update.P.Browser.Builder.exe")
                ElseIf (patchr = DialogResult.No) Then
                    Process.Start("https://github.com/Pavich7/P-Browser-Builder/releases/latest/download/Install.P.Browser.Builder.exe")
                End If
            End If
        End If
        bfileReader.Close()
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
End Class