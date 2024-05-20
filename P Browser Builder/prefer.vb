Imports System.IO
Imports System.IO.Compression

Public Class prefer
    Private Sub prefer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Dim rescheck As String = apppath + "\resource"
        Dim cachecheck As String = apppath + "\statecache\updatecache\pbb-resource.zip"
        If Not System.IO.File.Exists(cachecheck) Then
            Label9.Enabled = False
        End If
        If Not System.IO.Directory.Exists(rescheck) Then
            Label2.Text = "Chromium : Not installed"
            Label5.Text = "CefSharp : Not installed"
            Label23.Text = "Version : Resource not installed"
            Label2.Enabled = False
            Label5.Enabled = False
            Label23.Enabled = False
            Label7.Text = "Delete curerently insatlled resource. (Resource not installed)"
            Label8.Enabled = False
            Label4.Text = "Resource not installed"
        Else
            Try
                Dim metacheck As String = apppath + "\resource\metadata\version.txt"
                If Not System.IO.File.Exists(metacheck) Then
                    MessageBox.Show("Resource not compatible with this version of P Browser Builder!" + vbNewLine + "Please update resource by uninstall and reinstall via build menu.", "Check for update error!")
                Else
                    System.IO.Directory.Delete(apppath + "\resource\getcache", True)
                    System.IO.Directory.CreateDirectory(apppath + "\resource\getcache")
                    Dim fileReader As System.IO.StreamReader
                    My.Computer.Network.DownloadFile("http://pavichdev.ddns.net/api/v2-pbb/cfuversion/onlineresver.txt", apppath + "\resource\getcache\onlineresver.txt")
                    Dim fileReader1 As System.IO.StreamReader
                    fileReader1 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\getcache\onlineresver.txt")
                    Dim stringReader1 As String
                    stringReader1 = fileReader1.ReadLine()
                    fileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\metadata\version.txt")
                    Dim stringReader As String
                    stringReader = fileReader.ReadLine()
                    If stringReader1.Contains(stringReader) Then
                        Label4.Text = "up-to-date!"
                    Else
                        Label4.Text = "Update available! (" + stringReader1 + ")"
                    End If
                    fileReader1.Close()
                End If
            Catch ex As Exception
                Label4.Text = "Error checking for update!"
            End Try
            Try
                Dim fileReader2 As System.IO.StreamReader
                Dim fileReader3 As System.IO.StreamReader
                Dim fileReader4 As System.IO.StreamReader
                fileReader2 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\buildspace\quickmode\pkgData\pkgchrome.pbad")
                fileReader3 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\buildspace\quickmode\pkgData\pkgcef.pbad")
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
        'Disable reset features (incompleted)
        Label13.Visible = False
        Label14.Visible = False
        Label15.Visible = False
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
                    MessageBox.Show("Initialization Failed! dlresCH is not updated!" + vbNewLine + "Please contact PavichDev Support! Click OK to restart.", "Error!")
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
            MessageBox.Show("Successed!", "OK!")
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

    Private Sub Label29_Click(sender As Object, e As EventArgs) Handles Label29.Click
        Dim result As DialogResult = MessageBox.Show("Unlocking the Advanced Menu is dangerous." + vbNewLine + "This menu is used to install or modify builder resouces." + vbNewLine + "" + vbNewLine + "Do you want to process it?", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Label29.Enabled = False
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

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        MessageBox.Show("To update resource, please uninstall and reinstall resource!" + vbNewLine + "Relaunch preference to recheck for update.", "Info")
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim result As DialogResult = MessageBox.Show("Do you wish to reset all setting?" + vbNewLine + "This cannot be undone!", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Dim result1 As DialogResult = MessageBox.Show("Do you wish to delete resource also?" + vbNewLine + "You can reinstall anytime via news feed.", "Resource setting", MessageBoxButtons.YesNo)
            Dim apppath As String = Application.StartupPath()
            System.IO.Directory.Delete(apppath + "\statecache\updatecache", True)
            System.IO.Directory.CreateDirectory(apppath + "\statecache\updatecache")
            If (result1 = DialogResult.Yes) Then
                System.IO.Directory.Delete(apppath + "\resource", True)
            End If
        End If
    End Sub
End Class