Imports System.IO.Compression
Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class prefer
    Dim TotalSize As Long = 0
    Public Function GetDirSize(RootFolder As String) As Long
        Dim FolderInfo = New IO.DirectoryInfo(RootFolder)
        For Each File In FolderInfo.GetFiles : TotalSize += File.Length
        Next
        For Each SubFolderInfo In FolderInfo.GetDirectories : GetDirSize(SubFolderInfo.FullName)
        Next
        Return TotalSize
    End Function

    Private Sub prefer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Dim rescheck As String = apppath + "\resource"
        Dim cachecheck As String = apppath + "\statecache\updatecache\pbb-resource.zip"
        If Not System.IO.File.Exists(cachecheck) Then
            Label9.Enabled = False
        End If
        If Form1.DevToolStripMenuItem.Visible = True Then
            Label60.Enabled = False
        End If
        TotalSize = 0
        Dim TheSize2 As Long = GetDirSize(apppath + "\statecache\updatecache\")
        Label52.Text = FormatNumber(TheSize2 / 1024 / 1024, 1) & " MB"
        Try
            'Build Ver Check
            Dim bfileReader As System.IO.StreamReader
            bfileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\metadata\version.txt")
            Dim bstringReader As String
            bstringReader = bfileReader.ReadLine()
            Dim client As WebClient = New WebClient()
            Dim obuiver As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v1/ver/onlinebuiver.txt")
            If obuiver.Contains(bstringReader) Then
                Label30.Text = "up-to-date!"
                Label30.Enabled = False
            Else
                Label30.Text = "Update available! (" + obuiver + ")"
            End If
            bfileReader.Close()
        Catch ex As Exception
            Label30.Text = "Error checking for update!"
        End Try
        If Not System.IO.Directory.Exists(rescheck) Then
            Label2.Text = "Chromium : N/A"
            Label5.Text = "CefSharp : N/A"
            Label23.Text = "Version : N/A"
            Label51.Text = "0 MB"
            Label52.Text = "0 MB"
            Label2.Enabled = False
            Label5.Enabled = False
            Label23.Enabled = False
            Label7.Text = "Delete currently installed resource. (Resource not installed)"
            Label8.Enabled = False
            Label4.Text = "Resource not installed"
        Else
            Try
                Dim metacheck As String = apppath + "\resource\metadata\version.txt"
                If Not System.IO.File.Exists(metacheck) Then
                    MessageBox.Show("Resource not compatible with this version of P Browser Builder!" + vbNewLine + "Please update resource by uninstall and reinstall via build menu.", "Check for update error!")
                Else
                    'Res Ver Check
                    Dim fileReader As System.IO.StreamReader
                    fileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\metadata\version.txt")
                    Dim stringReader As String
                    stringReader = fileReader.ReadLine()
                    Dim client As WebClient = New WebClient()
                    Dim oresver As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v1/ver/onlineresver.txt")
                    If oresver.Contains(stringReader) Then
                        Label4.Text = "up-to-date!"
                        Label4.Enabled = False
                    Else
                        Label4.Text = "Update available! (" + oresver + ")"
                    End If
                    fileReader.Close()
                End If
            Catch ex As Exception
                Label4.Text = "Error checking for update!"
                Label4.Enabled = False
            End Try
            Try
                Dim fileReader2 As System.IO.StreamReader
                Dim fileReader3 As System.IO.StreamReader
                Dim fileReader4 As System.IO.StreamReader
                fileReader2 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\testspace\pkgData\pkgchrome.pbad")
                fileReader3 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\testspace\pkgData\pkgcef.pbad")
                fileReader4 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\metadata\version.txt")
                Dim stringReader2 As String
                Dim stringReader3 As String
                Dim stringReader4 As String
                stringReader2 = fileReader2.ReadLine()
                stringReader3 = fileReader3.ReadLine()
                stringReader4 = fileReader4.ReadLine()
                Label2.Text = "Chromium : " + stringReader2
                Label5.Text = "CefSharp : " + stringReader3
                Label23.Text = "Version : " + stringReader4
                fileReader2.Close()
                fileReader3.Close()
                fileReader4.Close()
                TotalSize = 0
                Dim TheSize1 As Long = GetDirSize(rescheck)
                Label51.Text = FormatNumber(TheSize1 / 1024 / 1024, 1) & " MB"
            Catch ex As Exception
                MessageBox.Show("Data gather failure!" + vbNewLine + "Error : " + ex.Message, "Fatal Error!")
                Me.Close()
            End Try
        End If
        Label22.Enabled = False
        Label24.Enabled = False
        Label25.Enabled = False
        Label28.Enabled = False
        TextBox1.Enabled = False
        Label19.Enabled = False
        Label20.Enabled = False
        Label21.Enabled = False
        Label45.Text = "Diagnostic Status: Stopped"
        Label42.Text = "Process ID: -"
        Dim fileReader11 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.nfstartfetch.pbcfg")
        Dim stringReader11 As String
        stringReader11 = fileReader11.ReadLine()
        If stringReader11 = "False" Then
            CheckBox1.Checked = True
        End If
        fileReader11.Close()

        Dim fileReader111 As System.IO.StreamReader
        fileReader111 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.hidesp.pbcfg")
        Dim stringReader111 As String
        stringReader111 = fileReader111.ReadLine()
        If stringReader111 = "True" Then
            CheckBox2.Checked = True
        End If
        fileReader111.Close()

        Dim fileReader1 As System.IO.StreamReader
        fileReader1 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.usageinterv.pbcfg")
        Dim stringReader1 As String
        stringReader1 = fileReader1.ReadLine()
        TextBox2.Text = stringReader1
        fileReader1.Close()

        Dim pros As Process = Process.GetCurrentProcess()
        Dim demround1 As Double = pros.WorkingSet / 1024 / 1024
        demround1 = Math.Round(demround1, 2)
        Label48.Text = "Process ID: " & pros.Id
        Label47.Text = "Memory Usage: " & demround1 & " MB"

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
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

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Try
            Dim apppath As String = Application.StartupPath()
            System.IO.Directory.Delete(apppath + "\statecache\updatecache", True)
            System.IO.Directory.CreateDirectory(apppath + "\statecache\updatecache")
            Label9.Enabled = False
            TotalSize = 0
            Dim TheSize3 As Long = GetDirSize(apppath + "\statecache\updatecache\")
            Label52.Text = FormatNumber(TheSize3 / 1024 / 1024, 1) & " MB"
        Catch ex As Exception
            MessageBox.Show("Could not attempt to delete installer cache!" + vbNewLine + ex.Message + vbNewLine + "You may need to restart builder and try again.", "Error!")
        End Try
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        about.Show()
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        Try
            MessageBox.Show("Step 1 of 3 : Welcome" + vbNewLine + "Welcome to sideload utility!" + vbNewLine + "Next step utility will let you select the sideload archive." + vbNewLine + "Select OK to continue.", "Sideload utility")
            Dim apppath As String = Application.StartupPath()
            OpenFileDialog1.Multiselect = False
            OpenFileDialog1.Title = "Choose your sideload file"
            OpenFileDialog1.Filter = "Resource sideload file|*.zip"
            If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                MessageBox.Show("Step 2 of 3 : Installation" + vbNewLine + "Click OK to start installation", "Sideload utility")
                Dim pbcfg = apppath + "\resource"
                If System.IO.Directory.Exists(pbcfg) = True Then
                    System.IO.Directory.Delete(apppath + "\resource", True)
                End If
                Dim zipPath As String = OpenFileDialog1.FileName
                Dim extractPath As String = apppath
                ZipFile.ExtractToDirectory(zipPath, extractPath)
                Try
                    Process.Start(apppath + "\resource\resinit.exe")
                Catch ex As Exception
                    MessageBox.Show("Initialization Failed!", "Error!")
                    Application.Restart()
                End Try
                Label7.Text = "First initializing Resource..."
                Dim result1 As DialogResult = MessageBox.Show("Installation completed but restart required!" + vbNewLine + "Do you want to restart P Browser Builder now?", "Installation completed!", MessageBoxButtons.YesNo)
                If (result1 = DialogResult.Yes) Then
                    Application.Restart()
                End If
            End If
        Catch ex As Exception
            Dim apppath As String = Application.StartupPath()
            MessageBox.Show("Could not attempt to install resource!" + vbNewLine + ex.Message, "Error!")
            Application.Restart()
        End Try
    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Please Enter URL!", "FAILED!")
        Else
            Dim apppath As String = Application.StartupPath()
            Dim pbcfg As String = apppath + "\statedata\setting.builder.resdlserver.pbcfg"
            Dim objWriter As New System.IO.StreamWriter(pbcfg)
            objWriter.Write(TextBox1.Text)
            objWriter.Close()
            MessageBox.Show("Success!", "OK!")
        End If
    End Sub

    Private Sub Label28_Click(sender As Object, e As EventArgs) Handles Label28.Click
        Dim apppath As String = Application.StartupPath()
        Dim pbcfg As String = apppath + "\statedata\setting.builder.resdlserver.pbcfg"
        Dim objWriter As New System.IO.StreamWriter(pbcfg)
        objWriter.Write("https://github.com/Pavich7/P-Browser-Builder-Resource/releases/latest/download/pbb-resource.zip")
        objWriter.Close()
        MessageBox.Show("Reset!", "OK!")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            Label22.Enabled = False
        Else
            Label22.Enabled = True
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        MessageBox.Show("To update resource, please uninstall and reinstall resource!" + vbNewLine + "Relaunch preference to recheck for update.", "Info")
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim result As DialogResult = MessageBox.Show("Do you wish to reset all setting?" + vbNewLine + "This cannot be undone!", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Dim apppath As String = Application.StartupPath()
            Dim objWriter As New System.IO.StreamWriter(apppath + "\statedata\setting.builder.inrsstate.pbcfg")
            objWriter.Write("True")
            objWriter.Close()
            Dim result1 As DialogResult = MessageBox.Show("Builder will enter restore mode on next launch." + vbNewLine + "Do you wish to restart now?", "You sure about this?", MessageBoxButtons.YesNo)
            If (result1 = DialogResult.Yes) Then
                Application.Restart()
            End If
        End If
    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click
        MessageBox.Show("Click OK to start download latest version...", "Informations")
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/releases/latest/download/Install.P.Browser.Builder.exe")
    End Sub

    Private Sub Label31_Click(sender As Object, e As EventArgs) Handles Label31.Click
        MessageBox.Show("In this textbox you can set where to download resources. If you want to host please make sure that resource server is directly pointed to your pbb-resource.zip file (like https://example.com/pbb-resource.zip)", "More informations...")
    End Sub

    Private Sub Label32_Click(sender As Object, e As EventArgs) Handles Label32.Click
        MessageBox.Show("If you already have pbb-resource.zip file or your custom one, using this button to install from file rather than download new ones. Please note that custom resource can be risky.", "More informations...")
    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Enter
        Dim result As DialogResult = MessageBox.Show("Unlocking the Advanced Menu is dangerous." + vbNewLine + "This menu is used to install or modify builder resouces." + vbNewLine + "" + vbNewLine + "Do you want to process it?", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Label22.Enabled = True
            Label24.Enabled = True
            Label25.Enabled = True
            Label28.Enabled = True
            TextBox1.Enabled = True
            Label19.Enabled = True
            Label20.Enabled = True
            Label21.Enabled = True
        Else
            TabControl1.SelectedTab = TabPage1
        End If
    End Sub

    Private Sub Label37_Click(sender As Object, e As EventArgs) Handles Label37.Click
        Try
            Dim client As WebClient = New WebClient()
            Dim nf1desc As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v1/nf/nf1_desc.txt")
            Dim nf1titl As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v1/nf/nf1_title.txt")
            Dim nf1date As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v1/nf/nf1_date.txt")
            Form1.Label12.Text = nf1titl
            Form1.Label13.Text = nf1desc
            Form1.Label14.Text = nf1date
            MessageBox.Show("News Feed refreshed!", "OK!")
        Catch ex As Exception
            MessageBox.Show("Error while refreshing News Feed", "Error!")
        End Try
    End Sub

    Private Sub Label33_Click(sender As Object, e As EventArgs) Handles Label33.Click
        Dim apppath As String = Application.StartupPath()
        Dim pbcfg As String = apppath + "\statedata\setting.builder.nfstartfetch.pbcfg"
        Dim objWriter As New System.IO.StreamWriter(pbcfg)
        If CheckBox1.Checked = True Then
            objWriter.Write("False")
            MessageBox.Show("Success! News Feed will not fetch on startup. You may need to restart to make change.", "OK!")
        Else
            objWriter.Write("True")
            MessageBox.Show("Success! News Feed will fetch on startup. You may need to restart to make change.", "OK!")
        End If
        objWriter.Close()
    End Sub

    Private Sub Label39_Click(sender As Object, e As EventArgs) Handles Label39.Click
        If TextBox2.Text = "" Then
            MessageBox.Show("Please Enter value!", "FAILED!")
        Else
            Dim apppath As String = Application.StartupPath()
            Dim pbcfg As String = apppath + "\statedata\setting.builder.usageinterv.pbcfg"
            Dim objWriter As New System.IO.StreamWriter(pbcfg)
            objWriter.Write(TextBox2.Text)
            objWriter.Close()
            MessageBox.Show("Success! You may need to restart to apply the change.", "OK!")
        End If
    End Sub

    Private Sub Label53_Click(sender As Object, e As EventArgs) Handles Label53.Click
        Dim apppath As String = Application.StartupPath()
        Dim pbcfg As String = apppath + "\statedata\setting.builder.hidesp.pbcfg"
        Dim objWriter As New System.IO.StreamWriter(pbcfg)
        If CheckBox2.Checked = True Then
            objWriter.Write("True")
            MessageBox.Show("Success! Side panel will hide on startup. You may need to restart to make change.", "OK!")
        Else
            objWriter.Write("False")
            MessageBox.Show("Success! Side panel will show on startup. You may need to restart to make change.", "OK!")
        End If
        objWriter.Close()
    End Sub

    Private Sub Label56_Click(sender As Object, e As EventArgs) Handles Label56.Click
        Dim apppath As String = Application.StartupPath()
        Process.Start(apppath + "/statedata")
    End Sub

    Private Sub Label60_Click(sender As Object, e As EventArgs) Handles Label60.Click
        Dim result As DialogResult = MessageBox.Show("Unlocking the Dev Menu is dangerous." + vbNewLine + "It is used to test incomplete features at runtime." + vbNewLine + "Some incomplete or faulty features can damage your Builder!" + vbNewLine + "For developers, you can go check the code in the repository." + vbNewLine + "Do you want to process it?", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Form1.DevToolStripMenuItem.Visible = True
            Label60.Enabled = False
        End If
    End Sub
End Class