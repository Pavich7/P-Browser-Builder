Imports System.IO
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
        TotalSize = 0
        Dim TheSize2 As Long = GetDirSize(apppath + "\statecache\updatecache\")
        Label52.Text = FormatNumber(TheSize2 / 1024 / 1024, 1) & " MB"
        If Not System.IO.Directory.Exists(rescheck) Then
            Label2.Text = "Chromium : N/A"
            Label5.Text = "CefSharp : N/A"
            Label23.Text = "Version : N/A"
            Label51.Text = "0.0 MB"
            Label2.Enabled = False
            Label5.Enabled = False
            Label23.Enabled = False
            Label30.Enabled = False
            Label7.Text = "Delete currently installed resource. (Resource not installed)"
            Label8.Enabled = False
        Else
            Try
                Dim fileReader2 As System.IO.StreamReader
                Dim fileReader3 As System.IO.StreamReader
                Dim fileReader4 As System.IO.StreamReader
                fileReader2 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\testspace\pkgData\pkgchrome.pbad")
                fileReader3 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\testspace\pkgData\pkgcef.pbad")
                fileReader4 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\version.txt")
                Dim stringReader2 As String = fileReader2.ReadLine()
                Dim stringReader3 As String = fileReader3.ReadLine()
                Dim stringReader4 As String = fileReader4.ReadLine()
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
        Dim hidesp As String = settings.load("hidesp")
        If hidesp = "True" Then
            CheckBox2.Checked = True
        End If
        TextBox2.Text = settings.load("usageinterv")
        Dim pros As Process = Process.GetCurrentProcess()
        Dim demround1 As Double = pros.WorkingSet / 1024 / 1024
        demround1 = Math.Round(demround1, 2)
        Label48.Text = "Process ID: " & pros.Id
        Label47.Text = "Memory Usage: " & demround1 & " MB"

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Try
            Dim result As DialogResult = MessageBox.Show("Do you wish to completely uninstall builder resource?" + vbNewLine + "You can reinstall resource later via notification box", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If (result = DialogResult.Yes) Then
                Dim apppath As String = Application.StartupPath()
                System.IO.Directory.Delete(apppath + "\resource", True)
                MessageBox.Show("P Browser Builder need to restart app", "Uninstall Completed!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Application.Restart()
            End If
        Catch ex As Exception
            MessageBox.Show("Could not attempt to uninstall resource!" + vbNewLine + ex.Message + vbNewLine + "You may need to restart builder and try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show("Could not attempt to delete installer cache!" + vbNewLine + ex.Message + vbNewLine + "You may need to restart builder and try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                Dim zipPath1 As String = apppath + "\resource\resourcepack\freshapp.zip"
                Dim extractPath1 As String = apppath + "\resource\testspace"
                ZipFile.ExtractToDirectory(zipPath1, extractPath1)
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
            MessageBox.Show("Please Enter URL!", "FAILED!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            settings.save("resdlserver", TextBox1.Text)
            MessageBox.Show("Success!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Label28_Click(sender As Object, e As EventArgs) Handles Label28.Click
        settings.save("resdlserver", "https://github.com/Pavich7/P-Browser-Builder-Resource/releases/latest/download/pbb-resource.zip")
        MessageBox.Show("Reset!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            Label22.Enabled = False
        Else
            Label22.Enabled = True
        End If
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim result As DialogResult = MessageBox.Show("Do you wish to reset all setting?" + vbNewLine + "This cannot be undone!", "You sure about this?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If (result = DialogResult.Yes) Then
            Dim apppath As String = Application.StartupPath()
            settings.save("inrsstate", "True")
            Application.Restart()
        End If
    End Sub

    Private Sub Label31_Click(sender As Object, e As EventArgs) Handles Label31.Click
        MessageBox.Show("In this textbox you can set where to download resources. If you want to host please make sure that resource server is directly pointed to your pbb-resource.zip file (like https://example.com/pbb-resource.zip)", "More informations...", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Label32_Click(sender As Object, e As EventArgs) Handles Label32.Click
        MessageBox.Show("If you already have pbb-resource.zip file or your custom one, using this button to install from file rather than download new ones. Please note that custom resource can be risky.", "More informations...", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Enter
        If My.Settings.advWarnState = False Then
            Dim result As DialogResult = MessageBox.Show("Unlocking the Advanced Menu is dangerous." + vbNewLine + "This menu is used to install or modify builder resources." + vbNewLine + "" + vbNewLine + "Do you want to process it?", "Confirmation?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If (result = DialogResult.Yes) Then
                Label22.Enabled = True
                Label24.Enabled = True
                Label25.Enabled = True
                Label28.Enabled = True
                TextBox1.Enabled = True
                Label19.Enabled = True
                Label20.Enabled = True
                Label21.Enabled = True
                My.Settings.advWarnState = True
            Else
                TabControl1.SelectedTab = TabPage1
            End If
        Else
            Label22.Enabled = True
            Label24.Enabled = True
            Label25.Enabled = True
            Label28.Enabled = True
            TextBox1.Enabled = True
            Label19.Enabled = True
            Label20.Enabled = True
            Label21.Enabled = True
        End If
    End Sub

    Private Sub Label39_Click(sender As Object, e As EventArgs) Handles Label39.Click
        If TextBox2.Text = "" Then
            MessageBox.Show("Please Enter value!", "FAILED!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            settings.save("usageinterv", TextBox2.Text)
            MessageBox.Show("Success! You may need to restart to apply the change.", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Label53_Click(sender As Object, e As EventArgs) Handles Label53.Click
        If CheckBox2.Checked = True Then
            settings.save("hidesp", "True")
            MessageBox.Show("Success! Side panel will hide on startup. You may need to restart to make change.", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            settings.save("hidesp", "False")
            MessageBox.Show("Success! Side panel will show on startup. You may need to restart to make change.", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Label56_Click(sender As Object, e As EventArgs) Handles Label56.Click
        Dim apppath As String = Application.StartupPath()
        Dim filePath As String = apppath + "\settings.pbcfg"
        Using folderDialog As New FolderBrowserDialog()
            folderDialog.Description = "Select a folder to export the settings file"
            If folderDialog.ShowDialog() = DialogResult.OK Then
                Dim selectedPath As String = folderDialog.SelectedPath
                Dim destinationPath As String = Path.Combine(selectedPath, "settings.pbcfg")
                File.Copy(filePath, destinationPath, True)
                MessageBox.Show("Settings exported!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using
    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click
        MessageBox.Show("Builder may freeze temporarily. Please wait until the operation is complete.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Dim apppath As String = Application.StartupPath()
        Try
            System.IO.Directory.Delete(apppath + "\resource\testspace", True)
            Dim zipPath1 As String = apppath + "\resource\resourcepack\freshapp.zip"
            Dim extractPath1 As String = apppath + "\resource\testspace"
            ZipFile.ExtractToDirectory(zipPath1, extractPath1)
        Catch ex As Exception
            MessageBox.Show("Operation failed! Please consider reinstall resource.", "FAILED!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        MessageBox.Show("Operation completed!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Label33_Click(sender As Object, e As EventArgs) Handles Label33.Click
        Dim apppath As String = Application.StartupPath()
        MessageBox.Show("Please make sure that settings are from same Builder version.", "Import Assistance", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Dim destinationPath As String = apppath + "\settings.pbcfg"
        Using openDialog As New OpenFileDialog()
            openDialog.Title = "Select settings.pbcfg to import"
            openDialog.Filter = "Settings File (*.pbcfg)|*.pbcfg"
            If openDialog.ShowDialog() = DialogResult.OK Then
                File.Copy(openDialog.FileName, destinationPath, True)
                MessageBox.Show("Settings imported! Click OK to restart.", "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Application.Restart()
            End If
        End Using
    End Sub
End Class