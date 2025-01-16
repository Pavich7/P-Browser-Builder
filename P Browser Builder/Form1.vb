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
    Private WithEvents Browser As ChromiumWebBrowser
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
            MessageBox.Show("Please enter your websites URL.", "Build Failed!")
        ElseIf spechk = True Then
            MessageBox.Show("App name cannot contain any of these characters: \ / : * ? "" < > |", "Build Failed!")
        Else
            Label7.Text = "Building in progress..."
            Dim pbcfg As String = apppath + "\resource\testspace\builderdata.pbcfg"
            Dim pbprogcfg As String = apppath + "\resource\testspace\progdata.pbcfg"
            Dim pbprogw As String = apppath + "\resource\testspace\appsizew.pbcfg"
            Dim pbprogh As String = apppath + "\resource\testspace\appsizeh.pbcfg"
            Dim pbprogfw As String = apppath + "\resource\testspace\appfixbs.pbcfg"
            Dim pbprogaot As String = apppath + "\resource\testspace\appaot.pbcfg"
            Dim pbopval As String = apppath + "\resource\testspace\appopval.pbcfg"
            Dim pbhico As String = apppath + "\resource\testspace\apphico.pbcfg"
            Dim testapp As String = apppath + "\resource\testspace\P Browser App.exe"
            ProgressBar1.Value = 20
            If System.IO.File.Exists(pbcfg) = True Then
                Try
                    Button2.Enabled = False
                    Button1.Enabled = False
                    ProgressBar1.Value = 50
                    Dim objWriter As New System.IO.StreamWriter(pbcfg)
                    Dim objWriter2 As New System.IO.StreamWriter(pbprogcfg)
                    Dim objWriter3 As New System.IO.StreamWriter(pbprogh)
                    Dim objWriter4 As New System.IO.StreamWriter(pbprogw)
                    Dim objWriter5 As New System.IO.StreamWriter(pbprogfw)
                    Dim objWriter6 As New System.IO.StreamWriter(pbprogaot)
                    Dim objWriter7 As New System.IO.StreamWriter(pbopval)
                    Dim objWriter8 As New System.IO.StreamWriter(pbhico)
                    objWriter.Write(TextBox1.Text)
                    objWriter.Close()
                    objWriter2.Write(TextBox2.Text)
                    objWriter2.Close()
                    objWriter3.Write(TextBox4.Text)
                    objWriter3.Close()
                    objWriter4.Write(TextBox3.Text)
                    objWriter4.Close()
                    If CheckBox5.Checked = True Then
                        objWriter5.Write("True")
                    End If
                    objWriter5.Close()
                    If CheckBox6.Checked = True Then
                        objWriter6.Write("True")
                    End If
                    objWriter6.Close()
                    Dim icnexist As String = apppath + "\resource\testspace\appicns.ico"
                    If System.IO.File.Exists(icnexist) Then
                        My.Computer.FileSystem.DeleteFile(apppath + "\resource\testspace\appicns.ico")
                    Else
                        Dim icnshave As String = apppath + "\statecache\buildcache\appicns\appicns.ico"
                        If System.IO.File.Exists(icnshave) Then
                            My.Computer.FileSystem.CopyFile(apppath + "\statecache\buildcache\appicns\appicns.ico", apppath + "\resource\testspace\appicns.ico")
                        End If
                    End If
                    objWriter7.Write(TextBox5.Text)
                    objWriter7.Close()
                    If CheckBox7.Checked = True Then
                        objWriter8.Write("True")
                    End If
                    objWriter8.Close()
                    ProgressBar1.Value = 100
                    MessageBox.Show("Build Completed! Click continue to test app." + vbNewLine + "Some features will not available in Testing.", "Build Completed!")
                    Process.Start(testapp)
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
                    MessageBox.Show("Please close previous test app first before perform this action.", "Failed!")
                    Label7.Text = "Test failed!"
                    Button2.Enabled = True
                    Button1.Enabled = True
                    ProgressBar1.Value = 0
                End Try
            Else
                MessageBox.Show("Build Failed! Incomplete or corrupted Data please reinstall builder.", "Build Failed!")
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
            MessageBox.Show("Please enter your websites URL.", "Build Failed!")
        ElseIf TextBox2.Text = "" Then
            MessageBox.Show("Please enter your application name.", "Build Failed!")
        ElseIf spechk = True Then
            MessageBox.Show("App name cannot contain any of these characters: \ / : * ? "" < > |", "Build Failed!")
        Else
            If RadioButton2.Checked = True Then
                Label7.Text = "Building in progress..."
                System.IO.Directory.Delete(apppath + "\resource\buildspace", True)
                System.IO.Directory.CreateDirectory(apppath + "\resource\buildspace")
                Dim zipPath As String = apppath + "\resource\resourcepack\freshapp.zip"
                Dim extractPath As String = apppath + "\resource\buildspace"
                ZipFile.ExtractToDirectory(zipPath, extractPath)
                Dim pbcfg As String = apppath + "\resource\buildspace\builderdata.pbcfg"
                Dim pbprogcfg As String = apppath + "\resource\buildspace\progdata.pbcfg"
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
                    Dim pbprogw As String = apppath + "\resource\buildspace\appsizew.pbcfg"
                    Dim pbprogh As String = apppath + "\resource\buildspace\appsizeh.pbcfg"
                    Dim objWriter13 As New System.IO.StreamWriter(pbprogh)
                    Dim objWriter14 As New System.IO.StreamWriter(pbprogw)
                    objWriter13.Write(TextBox4.Text)
                    objWriter13.Close()
                    objWriter14.Write(TextBox3.Text)
                    objWriter14.Close()
                    Dim pbprogfw As String = apppath + "\resource\buildspace\appfixbs.pbcfg"
                    Dim objWriter50 As New System.IO.StreamWriter(pbprogfw)
                    If CheckBox5.Checked = True Then
                        objWriter50.Write("True")
                    End If
                    objWriter50.Close()
                    Dim pbprogaot As String = apppath + "\resource\buildspace\appaot.pbcfg"
                    Dim objWriter51 As New System.IO.StreamWriter(pbprogaot)
                    If CheckBox6.Checked = True Then
                        objWriter51.Write("True")
                    End If
                    objWriter51.Close()
                    Dim pbhico As String = apppath + "\resource\buildspace\apphico.pbcfg"
                    Dim objWriter52 As New System.IO.StreamWriter(pbhico)
                    If CheckBox7.Checked = True Then
                        objWriter52.Write("True")
                    End If
                    objWriter52.Close()
                    Dim pbopval As String = apppath + "\resource\buildspace\appopval.pbcfg"
                    Dim objWriter511 As New System.IO.StreamWriter(pbopval)
                    objWriter511.Write(TextBox5.Text)
                    objWriter511.Close()
                    ProgressBar1.Value = 50
                    My.Computer.FileSystem.CopyDirectory(apppath + "\resource\buildspace", apppath + "\binary", True)
                    My.Computer.FileSystem.RenameFile(apppath + "\binary\P Browser App.exe", TextBox2.Text + ".exe")
                    Dim icnshave As String = apppath + "\statecache\buildcache\appicns\appicns.ico"
                    If System.IO.File.Exists(icnshave) Then
                        My.Computer.FileSystem.CopyFile(apppath + "\statecache\buildcache\appicns\appicns.ico", apppath + "\binary\appicns.ico", True)
                    End If
                    ProgressBar1.Value = 70
                    System.IO.Directory.Delete(apppath + "\resource\buildspace", True)
                    System.IO.Directory.CreateDirectory(apppath + "\resource\buildspace")
                    ProgressBar1.Value = 80
                    Dim pbfsd As String = apppath + "\binary\fsmsg.desc.pbcfg"
                    Dim pbfst As String = apppath + "\binary\fsmsg.title.pbcfg"
                    Dim pbfse As String = apppath + "\binary\fsmsg.enab.pbval"
                    Dim objWriter3 As New System.IO.StreamWriter(pbfsd)
                    objWriter3.Write(welcomemessage.TextBox2.Text)
                    objWriter3.Close()
                    Dim objWriter4 As New System.IO.StreamWriter(pbfst)
                    objWriter4.Write(welcomemessage.TextBox1.Text)
                    objWriter4.Close()
                    If CheckBox3.Checked = False Then
                        Dim objWriter5 As New System.IO.StreamWriter(pbfse)
                        objWriter5.Write("False")
                        objWriter5.Close()
                    Else
                        Dim objWriter5 As New System.IO.StreamWriter(pbfse)
                        objWriter5.Write("True")
                        objWriter5.Close()
                    End If
                    ProgressBar1.Value = 100
                    MessageBox.Show("Build Completed! Click OK to continue.", "Build Completed!")
                    If CheckBox1.Checked = True Then
                        Process.Start(apppath + "\binary\" + TextBox2.Text + ".exe")
                    End If
                    If CheckBox2.Checked Then
                        Process.Start(apppath + "\binary\")
                    End If
                    If CheckBox4.Checked = True Then
                        Try
                            Process.Start(My.Settings.tempScript)
                        Catch ex As Exception
                            MessageBox.Show("Cannot run script! Your app is build and ready!", "Failed!")
                        End Try
                    End If
                    Label7.Text = "Build completed!"
                Else
                    MessageBox.Show("Build Failed! Incomplete or corrupted Data please reinstall builder.", "Build Failed!")
                    Label7.Text = "Build failed!"
                End If
            ElseIf RadioButton3.Checked = True Then
                Label7.Text = "Building in progress..."
                System.IO.Directory.Delete(apppath + "\resource\buildspace", True)
                System.IO.Directory.CreateDirectory(apppath + "\resource\buildspace")
                Dim zipPath As String = apppath + "\resource\resourcepack\freshapp.zip"
                Dim extractPath As String = apppath + "\resource\buildspace"
                ZipFile.ExtractToDirectory(zipPath, extractPath)
                Dim pbcfg As String = apppath + "\resource\buildspace\builderdata.pbcfg"
                Dim pbprogcfg As String = apppath + "\resource\buildspace\progdata.pbcfg"
                System.IO.Directory.Delete(apppath + "\binary", True)
                System.IO.Directory.CreateDirectory(apppath + "\binary")
                System.IO.Directory.Delete(apppath + "\binarypkg", True)
                System.IO.Directory.CreateDirectory(apppath + "\binarypkg")
                ProgressBar1.Value = 20
                If System.IO.File.Exists(pbcfg) = True Then
                    Dim objWriter As New System.IO.StreamWriter(pbcfg)
                    objWriter.Write(TextBox1.Text)
                    objWriter.Close()
                    Dim objWriter2 As New System.IO.StreamWriter(pbprogcfg)
                    objWriter2.Write(TextBox2.Text)
                    objWriter2.Close()
                    Dim pbprogw As String = apppath + "\resource\buildspace\appsizew.pbcfg"
                    Dim pbprogh As String = apppath + "\resource\buildspace\appsizeh.pbcfg"
                    Dim objWriter13 As New System.IO.StreamWriter(pbprogh)
                    Dim objWriter14 As New System.IO.StreamWriter(pbprogw)
                    objWriter13.Write(TextBox4.Text)
                    objWriter13.Close()
                    objWriter14.Write(TextBox3.Text)
                    objWriter14.Close()
                    Dim pbprogfw As String = apppath + "\resource\buildspace\appfixbs.pbcfg"
                    Dim objWriter50 As New System.IO.StreamWriter(pbprogfw)
                    If CheckBox5.Checked = True Then
                        objWriter50.Write("True")
                    End If
                    objWriter50.Close()
                    Dim pbprogaot As String = apppath + "\resource\buildspace\appaot.pbcfg"
                    Dim objWriter51 As New System.IO.StreamWriter(pbprogaot)
                    If CheckBox6.Checked = True Then
                        objWriter51.Write("True")
                    End If
                    Dim pbhico As String = apppath + "\resource\buildspace\apphico.pbcfg"
                    Dim objWriter52 As New System.IO.StreamWriter(pbhico)
                    If CheckBox7.Checked = True Then
                        objWriter52.Write("True")
                    End If
                    objWriter52.Close()
                    Dim pbopval As String = apppath + "\resource\buildspace\appopval.pbcfg"
                    Dim objWriter511 As New System.IO.StreamWriter(pbopval)
                    objWriter511.Write(TextBox5.Text)
                    objWriter511.Close()
                    objWriter51.Close()
                    ProgressBar1.Value = 50
                    My.Computer.FileSystem.CopyDirectory(apppath + "\resource\buildspace", apppath + "\binary", True)
                    My.Computer.FileSystem.RenameFile(apppath + "\binary\P Browser App.exe", TextBox2.Text + ".exe")
                    Dim icnshave As String = apppath + "\statecache\buildcache\appicns\appicns.ico"
                    If System.IO.File.Exists(icnshave) Then
                        My.Computer.FileSystem.CopyFile(apppath + "\statecache\buildcache\appicns\appicns.ico", apppath + "\binary\appicns.ico", True)
                    End If
                    ProgressBar1.Value = 70
                    System.IO.Directory.Delete(apppath + "\resource\buildspace", True)
                    System.IO.Directory.CreateDirectory(apppath + "\resource\buildspace")
                    ProgressBar1.Value = 80
                    Dim pbfsd As String = apppath + "\binary\fsmsg.desc.pbcfg"
                    Dim pbfst As String = apppath + "\binary\fsmsg.title.pbcfg"
                    Dim pbfse As String = apppath + "\binary\fsmsg.enab.pbval"
                    Dim objWriter3 As New System.IO.StreamWriter(pbfsd)
                    objWriter3.Write(welcomemessage.TextBox2.Text)
                    objWriter3.Close()
                    Dim objWriter4 As New System.IO.StreamWriter(pbfst)
                    objWriter4.Write(welcomemessage.TextBox1.Text)
                    objWriter4.Close()
                    If CheckBox3.Checked = False Then
                        Dim objWriter5 As New System.IO.StreamWriter(pbfse)
                        objWriter5.Write("False")
                        objWriter5.Close()
                    Else
                        Dim objWriter5 As New System.IO.StreamWriter(pbfse)
                        objWriter5.Write("True")
                        objWriter5.Close()
                    End If
                    ProgressBar1.Value = 90
                    Dim zipsource As String = apppath + "\binary\"
                    Dim zipbin As String = apppath + "\binarypkg\" + TextBox2.Text + ".zip"
                    ZipFile.CreateFromDirectory(zipsource, zipbin, CompressionLevel.Optimal, False)
                    ProgressBar1.Value = 100
                    MessageBox.Show("Build Completed! Click OK to continue.", "Build Completed!")
                    If CheckBox1.Checked = True Then
                        Process.Start(apppath + "\binary\" + TextBox2.Text + ".exe")
                    End If
                    If CheckBox2.Checked Then
                        Process.Start(apppath + "\binarypkg\")
                    End If
                    If CheckBox4.Checked = True Then
                        Try
                            Process.Start(My.Settings.tempScript)
                        Catch ex As Exception
                            MessageBox.Show("Cannot run script! Your app is build and ready!", "Failed!")
                        End Try
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
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        apppath = Application.StartupPath()
        'Init structure check
        Dim flcheck1 As String = apppath + "\binary"
        Dim flcheck2 As String = apppath + "\binarypkg"
        Dim flcheck3 As String = apppath + "\metadata"
        Dim flcheck4 As String = apppath + "\statecache"
        Dim flcheck41 As String = apppath + "\statecache\buildcache"
        Dim flcheck44 As String = apppath + "\statecache\updatecache"
        Dim flcheck5 As String = apppath + "\statedata"
        Dim flcheck6 As String = apppath + "\imgassets"
        If Not System.IO.Directory.Exists(flcheck6) Then
            Me.Enabled = False
            errorencounter.RichTextBox1.Text = "Bad data structure! Reinstall might fixed this problem."
            errorencounter.Show()
        End If
        If Not System.IO.Directory.Exists(flcheck1) Then
            Me.Enabled = False
            errorencounter.RichTextBox1.Text = "Bad data structure! Reinstall might fixed this problem."
            errorencounter.Show()
        End If
        If Not System.IO.Directory.Exists(flcheck2) Then
            Me.Enabled = False
            errorencounter.RichTextBox1.Text = "Bad data structure! Reinstall might fixed this problem."
            errorencounter.Show()
        End If
        If Not System.IO.Directory.Exists(flcheck3) Then
            Me.Enabled = False
            errorencounter.RichTextBox1.Text = "Bad data structure! Reinstall might fixed this problem."
            errorencounter.Show()
        End If
        If Not System.IO.Directory.Exists(flcheck4) Then
            Me.Enabled = False
            errorencounter.RichTextBox1.Text = "Bad data structure! Reinstall might fixed this problem."
            errorencounter.Show()
        End If
        If Not System.IO.Directory.Exists(flcheck41) Then
            Me.Enabled = False
            errorencounter.RichTextBox1.Text = "Bad data structure! Reinstall might fixed this problem."
            errorencounter.Show()
        End If
        If Not System.IO.Directory.Exists(flcheck44) Then
            Me.Enabled = False
            errorencounter.RichTextBox1.Text = "Bad data structure! Reinstall might fixed this problem."
            errorencounter.Show()
        End If
        If Not System.IO.Directory.Exists(flcheck5) Then
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
                    'Flush Main Cache Dir
                    System.IO.Directory.Delete(apppath + "\statecache", True)
                    System.IO.Directory.CreateDirectory(apppath + "\statecache")
                    'Regen Sub-Cache Dir
                    System.IO.Directory.CreateDirectory(apppath + "\statecache\updatecache")
                    System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache")
                    System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\appicns")
                    'Flush Build Dir
                    System.IO.Directory.Delete(apppath + "\binary", True)
                    System.IO.Directory.CreateDirectory(apppath + "\binary")
                    System.IO.Directory.Delete(apppath + "\binarypkg", True)
                    System.IO.Directory.CreateDirectory(apppath + "\binarypkg")
                    'Flush debug logs
                    Dim objWriter As New System.IO.StreamWriter(apppath + "\debug.log")
                    objWriter.Write("")
                    objWriter.Close()
                    'Write default whats news state
                    Dim objWriter1 As New System.IO.StreamWriter(apppath + "\wnannounce.pbstate")
                    objWriter1.Write("True")
                    objWriter1.Close()
                    'Reset Temp setting
                    My.Settings.tempWebTitle = ""
                    My.Settings.tempIcoLoc = ""
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
            Dim fileReader1 As System.IO.StreamReader
            fileReader1 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.usageinterv.pbcfg")
            Dim stringReader1 As String
            stringReader1 = fileReader1.ReadLine()
            Timer2.Interval = stringReader1
            fileReader1.Close()
            Button4.Enabled = False
            Button5.Enabled = False
            Dim rscheck As String = apppath + "\resource"
            If Not System.IO.Directory.Exists(rscheck) Then
                Button1.Enabled = False
                Button2.Enabled = False
                RadioButton2.Enabled = False
                RadioButton3.Enabled = False
                CheckBox1.Enabled = False
                CheckBox2.Enabled = False
                Label4.Enabled = False
                Label8.Enabled = False
                Label24.Enabled = False
                Button9.Enabled = False
                BuildProjectToolStripMenuItem.Enabled = False
                TestProjectToolStripMenuItem.Enabled = False
            Else
                'reschk
                Try
                    Dim oresver As String
                    Dim client As WebClient = New WebClient()
                    Dim nf1cont As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v3/cfuver.txt")
                    Dim lines As String() = nf1cont.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                    If lines.Length > 1 Then oresver = lines(1)
                    Dim fileReader As System.IO.StreamReader
                    fileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\metadata\version.txt")
                    Dim stringReader As String = fileReader.ReadLine()
                    If oresver.Contains(stringReader) Then
                        Label20.Visible = False
                        Label18.Visible = False
                        Panel4.Size = New Size(265, 148)
                    Else
                        Label20.Text = "Resource update available! (" + oresver + ")"
                        Label18.Text = "    Update"
                    End If
                    fileReader.Close()
                Catch ex As Exception
                    Label20.Visible = False
                    Label18.Visible = False
                    Panel4.Size = New Size(265, 148)
                End Try
                'chkpoint
                Dim resvcheck4 As String = apppath + "\resource\metadata\checkpoint\r640.chkp"
                If Not System.IO.File.Exists(resvcheck4) Then
                    MessageBox.Show("Unload required! Resource not compatible!" + vbNewLine + "Please reinstall builder resource via preference menu.", "Resource not compatible!")
                    Button1.Enabled = False
                    Button2.Enabled = False
                    RadioButton2.Enabled = False
                    RadioButton3.Enabled = False
                    CheckBox1.Enabled = False
                    CheckBox2.Enabled = False
                    Label4.Enabled = False
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
                Panel6.Hide()
                Me.WindowState = FormWindowState.Normal
                Me.Size = New Size(1232, 646)
                TabControl1.Width = 872
            Else
                SidePanelToolStripMenuItem.Checked = True
            End If
            fileReader111.Close()
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
            Try
                Dim fileReader As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(FileNameOpenWith)
                TextBox2.Text = fileReader.ReadLine()
                TextBox1.Text = fileReader.ReadLine()
                TextBox3.Text = fileReader.ReadLine()
                TextBox4.Text = fileReader.ReadLine()
                CheckBox5.Checked = fileReader.ReadLine()
                CheckBox6.Checked = fileReader.ReadLine()
                CheckBox7.Checked = fileReader.ReadLine()
                TextBox5.Text = fileReader.ReadLine()
                CheckBox3.Checked = fileReader.ReadLine()
                welcomemessage.TextBox1.Text = fileReader.ReadLine()
                welcomemessage.TextBox2.Text = fileReader.ReadLine()
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
                Me.Enabled = True
                ProjnameToolStripMenuItem.Text = TextBox2.Text
                Me.WindowState = FormWindowState.Normal
                fileReader.Close()
            Catch ex As Exception
                MessageBox.Show("Load Failed!" & vbNewLine & "Project is not compatiable or corrupt!" & vbNewLine & ex.Message, "Error!")
                Application.Restart()
            End Try
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
                Dim nf1cont As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v3/feedcontent.txt")
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
        fileReader999.Close()
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Try
            Label15.Text = "Please wait..."
            Label15.Enabled = False
            Dim client As WebClient = New WebClient()
            Dim nf1cont As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v3/feedcontent.txt")
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
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = "944"
            TextBox4.Text = "573"
            TextBox5.Text = "100"
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            CheckBox1.Text = "Start your app after build"
            CheckBox2.Text = "Show your app in explorer after build"
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            CheckBox4.Checked = False
            CheckBox5.Checked = False
            CheckBox6.Checked = False
            CheckBox7.Checked = False
            PictureBox1.Image = My.Resources.p_browser_icon_001_rq2_icon
            Label16.Text = "Application icons (*.ico)"
            My.Settings.tempIcoLoc = ""
            System.IO.Directory.Delete(apppath + "\statecache\buildcache\appicns", True)
            System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\appicns")
            ProjnameToolStripMenuItem.Text = "Untitled Project"
        End If
    End Sub

    Private Sub SubmitBugsReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubmitBugsReportToolStripMenuItem.Click
        Browser.Load("https://github.com/Pavich7/P-Browser-Builder/issues/new/choose")
    End Sub

    Private Sub ReloadPreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReloadPreviewToolStripMenuItem.Click
        Browser.Load(TextBox1.Text)
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
            Process.Start(apppath + "\resource\resinit.exe")
            System.Threading.Thread.Sleep(30000)
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

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click
        prefer.Show()
    End Sub

    Private Sub ReleaseNoteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseNoteToolStripMenuItem.Click
        Browser.Load("https://github.com/Pavich7/P-Browser-Builder/releases/")
    End Sub

    Private Sub ReinitializeResourceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReinitializeResourceToolStripMenuItem.Click
        Try
            Process.Start(apppath + "\resource\resinit.exe")
        Catch ex As Exception
            MessageBox.Show("Initialization Failed! dlresCH is not updated!" + vbNewLine + "Please contact PavichDev Support! Click OK to restart.", "Error!")
            Application.Restart()
        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        prefer.Show()
        prefer.TabControl1.SelectedIndex = 3
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

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
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

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        CheckBox3.Checked = False
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter URL before getting page title.", "Error!")
        Else
            If My.Settings.tempWebTitle = "" Then
                MessageBox.Show("Current websites doesn't have title!", "Error!")
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
        If My.Settings.tempIcoLoc = "" Then
            MessageBox.Show("Please choose icon first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Process.Start(My.Settings.tempIcoLoc)
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
        MessageBox.Show("After fresh install you will need to install resource to build. (Download size: approx. 140 MB)", "Info...")
    End Sub

    Private Sub StartWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartWindowToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("You will lose all data! Please make sure your data is saved.", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = "944"
            TextBox4.Text = "573"
            TextBox5.Text = "100"
            RadioButton2.Checked = True
            RadioButton3.Checked = False
            CheckBox1.Text = "Start your app after build"
            CheckBox2.Text = "Show your app in explorer after build"
            CheckBox1.Checked = True
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            CheckBox4.Checked = False
            CheckBox5.Checked = False
            CheckBox6.Checked = False
            CheckBox7.Checked = False
            TabControl1.SelectedTab = TabPage1
            PictureBox1.Image = My.Resources.p_browser_icon_001_rq2_icon
            Label16.Text = "Application icons (*.ico)"
            My.Settings.tempIcoLoc = ""
            System.IO.Directory.Delete(apppath + "\statecache\buildcache\appicns", True)
            System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\appicns")
            ProjnameToolStripMenuItem.Text = "Untitled Project"
            Browser.Load("about:blank")
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

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        Browser.Reload
    End Sub

    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click
        prefer.Show()
        prefer.TabControl1.SelectedIndex = 2
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
            Try
                Dim myStream As Stream
                Dim saveFileDialog1 As New SaveFileDialog()
                saveFileDialog1.Filter = "P Browser Builder Project (*.pbproj)|*.pbproj"
                saveFileDialog1.RestoreDirectory = True
                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    myStream = saveFileDialog1.OpenFile()
                    If (myStream IsNot Nothing) Then
                        Using writer As New StreamWriter(myStream)
                            writer.WriteLine(TextBox2.Text)
                            writer.WriteLine(TextBox1.Text)
                            writer.WriteLine(TextBox3.Text)
                            writer.WriteLine(TextBox4.Text)
                            writer.WriteLine(CheckBox5.CheckState)
                            writer.WriteLine(CheckBox6.CheckState)
                            writer.WriteLine(CheckBox7.CheckState)
                            writer.WriteLine(TextBox5.Text)
                            writer.WriteLine(CheckBox3.CheckState)
                            writer.WriteLine(welcomemessage.TextBox1.Text)
                            writer.WriteLine(welcomemessage.TextBox2.Text)
                        End Using
                        myStream.Close()
                        MessageBox.Show("Saved to file!", "Completed!")
                    End If
                End If
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
                ProjnameToolStripMenuItem.Text = TextBox2.Text
            Catch ex As Exception
                MessageBox.Show("Save Failed!" & vbNewLine & ex.Message, "Error!")
            End Try
        End If
    End Sub

    Private Sub OpenProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenProjectToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("You will lose all data! Please make sure your data is saved.", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            OpenFileDialog1.Multiselect = False
            OpenFileDialog1.Title = "Open P Browser Builder Project"
            OpenFileDialog1.Filter = "P Browser Builder Project|*.pbproj"
            If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                Try
                    Dim fileReader As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(OpenFileDialog1.FileName)
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = "944"
                    TextBox4.Text = "573"
                    TextBox5.Text = "100"
                    RadioButton2.Checked = True
                    RadioButton3.Checked = False
                    CheckBox1.Text = "Start your app after build"
                    CheckBox2.Text = "Show your app in explorer after build"
                    CheckBox1.Checked = True
                    CheckBox2.Checked = False
                    CheckBox3.Checked = False
                    CheckBox4.Checked = False
                    CheckBox5.Checked = False
                    CheckBox6.Checked = False
                    CheckBox7.Checked = False
                    TabControl1.SelectedTab = TabPage1
                    PictureBox1.Image = My.Resources.p_browser_icon_001_rq2_icon
                    Label16.Text = "Application icons (*.ico)"
                    My.Settings.tempIcoLoc = ""
                    System.IO.Directory.Delete(apppath + "\statecache\buildcache\appicns", True)
                    System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\appicns")
                    Browser.Load("about:blank")
                    TextBox2.Text = fileReader.ReadLine()
                    TextBox1.Text = fileReader.ReadLine()
                    TextBox3.Text = fileReader.ReadLine()
                    TextBox4.Text = fileReader.ReadLine()
                    CheckBox5.Checked = fileReader.ReadLine()
                    CheckBox6.Checked = fileReader.ReadLine()
                    CheckBox7.Checked = fileReader.ReadLine()
                    TextBox5.Text = fileReader.ReadLine()
                    CheckBox3.Checked = fileReader.ReadLine()
                    welcomemessage.TextBox1.Text = fileReader.ReadLine()
                    welcomemessage.TextBox2.Text = fileReader.ReadLine()
                    ProjnameToolStripMenuItem.Text = TextBox2.Text
                    fileReader.Close()
                Catch ex As Exception
                    MessageBox.Show("Load Failed!" & vbNewLine & "Project is not compatiable or corrupt!" & vbNewLine & ex.Message, "Error!")
                End Try
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
            End If
        End If
    End Sub

    Dim hidenoti As Boolean = True
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Panel6.Hide()
        SidePanelToolStripMenuItem.Checked = False
        Me.WindowState = FormWindowState.Normal
        Me.Size = New Size(1232, 646)
        TabControl1.Width = 872
        If hidenoti = True Then
            MessageBox.Show("You can unhide side panel by click on View > Side Panel" + vbNewLine + "or using Ctrl + R Shortcut", "Notification")
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
            Me.WindowState = FormWindowState.Normal
            Me.Size = New Size(1232, 646)
            TabControl1.Width = 559
        Else
            Panel6.Hide()
            Me.WindowState = FormWindowState.Normal
            Me.Size = New Size(1232, 646)
            TabControl1.Width = 872
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
        getstart.Label56.Enabled = False
        getstart.Label57.Text = "Please load from Help menu."
    End Sub

    Private Sub LoadSampleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadSampleToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("You will lose all data! Please make sure your data is saved.", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            Try
                Dim fileReader As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\sample\sample1.pbproj")
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = "944"
                TextBox4.Text = "573"
                TextBox5.Text = "100"
                RadioButton2.Checked = True
                RadioButton3.Checked = False
                CheckBox1.Text = "Start your app after build"
                CheckBox2.Text = "Show your app in explorer after build"
                CheckBox1.Checked = True
                CheckBox2.Checked = False
                CheckBox3.Checked = False
                CheckBox4.Checked = False
                CheckBox5.Checked = False
                CheckBox6.Checked = False
                CheckBox7.Checked = False
                TabControl1.SelectedTab = TabPage1
                PictureBox1.Image = My.Resources.p_browser_icon_001_rq2_icon
                Label16.Text = "Application icons (*.ico)"
                My.Settings.tempIcoLoc = ""
                System.IO.Directory.Delete(apppath + "\statecache\buildcache\appicns", True)
                System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\appicns")
                Browser.Load("about:blank")
                TextBox2.Text = fileReader.ReadLine()
                TextBox1.Text = fileReader.ReadLine()
                TextBox3.Text = fileReader.ReadLine()
                TextBox4.Text = fileReader.ReadLine()
                CheckBox5.Checked = fileReader.ReadLine()
                CheckBox6.Checked = fileReader.ReadLine()
                CheckBox7.Checked = fileReader.ReadLine()
                TextBox5.Text = fileReader.ReadLine()
                CheckBox3.Checked = fileReader.ReadLine()
                welcomemessage.TextBox1.Text = fileReader.ReadLine()
                welcomemessage.TextBox2.Text = fileReader.ReadLine()
                ProjnameToolStripMenuItem.Text = TextBox2.Text
                fileReader.Close()
            Catch ex As Exception
                MessageBox.Show("Load Failed!" & vbNewLine & "Project is not compatiable or corrupt!" & vbNewLine & ex.Message, "Error!")
            End Try
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
        Else
            Panel7.Visible = True
        End If
    End Sub
End Class