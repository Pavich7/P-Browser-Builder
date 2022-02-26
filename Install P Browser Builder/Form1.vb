Imports System.IO.Compression
Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Dim appcheck As String = apppath + "\app0.3.0"
        If System.IO.Directory.Exists(appcheck) Then
            Dim result As DialogResult = MessageBox.Show("Installer detected current installed P Browser Builder with the same version as installer content." + vbNewLine + "You are about to launch installer while P Browser Build already installed! If you are about to install only resource please click YES." + vbNewLine + "Do you wish to proceed anyway?", "Installed content detected!", MessageBoxButtons.YesNo)
            If (result = DialogResult.No) Then
                Process.Start(apppath + "/app0.3.0/P Browser Builder.exe")
                Application.Exit()
            End If
        End If
        Dim space As Int64 = My.Computer.FileSystem.Drives.Item(0).AvailableFreeSpace
        Dim spacecal As Int64 = space / 1000000000
        Dim spacecalcom As String = spacecal.ToString
        Label8.Text = "Available disk space " + spacecalcom + " GB"
        TabPage2.Enabled = False
        TabPage3.Enabled = False
        TabPage4.Enabled = False
        TabPage5.Enabled = False
        RichTextBox1.ReadOnly = True
        Button3.Enabled = False
        CheckBox3.Checked = True
        CheckBox3.Enabled = False
        CheckBox1.Checked = True
        CheckBox4.Checked = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TabControl1.SelectedTab = TabPage4
        TabPage1.Enabled = False
        TabPage2.Enabled = False
        TabPage3.Enabled = False
        TabPage4.Enabled = True
        TabPage5.Enabled = False
        Dim apppath As String = Application.StartupPath()
        Dim zipPath As String = apppath + "\updateresource\updateresource.zip"
        Dim extractPath As String = apppath + "\app0.3.0"
        ProgressBar1.Value = 10
        Try
            ZipFile.ExtractToDirectory(zipPath, extractPath)
            ProgressBar1.Value = 100
            If CheckBox1.Checked = False Then
                System.IO.Directory.Delete(apppath + "\app0.3.0\resource", True)
            End If
        Catch ex As Exception
            MessageBox.Show("Error! Please uninstall older version first!", "Installation Exist Error!")
            Process.Start(apppath)
            Application.Exit()
        End Try
        ProgressBar1.Value = 100
        TabControl1.SelectedTab = TabPage5
        TabPage1.Enabled = False
        TabPage2.Enabled = False
        TabPage3.Enabled = False
        TabPage4.Enabled = False
        TabPage5.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TabControl1.SelectedTab = TabPage2
        TabPage1.Enabled = False
        TabPage2.Enabled = True
        TabPage3.Enabled = False
        TabPage4.Enabled = False
        TabPage5.Enabled = False
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Process.Start("https://pavichdev.ddns.net/download/documents/p-browser-builder-eula.pdf")
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            Button3.Enabled = True
        End If
        If CheckBox2.Checked = False Then
            Button3.Enabled = False
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim apppath As String = Application.StartupPath()
        Dim filecheck As String = apppath + "\app0.3.0\P Browser Builder.exe"
        If System.IO.File.Exists(filecheck) Then
            Dim result As DialogResult = MessageBox.Show("Another P Browser Builder Detected!" + vbNewLine + "Do you want to install only Builder Resource?" + vbNewLine + "(Do not click yes if you want to update version)", "Already Install Detected!", MessageBoxButtons.YesNo)
            If (result = DialogResult.Yes) Then
                TabControl1.SelectedTab = TabPage4
                TabPage1.Enabled = False
                TabPage2.Enabled = False
                TabPage3.Enabled = False
                TabPage4.Enabled = True
                TabPage5.Enabled = False
                Dim zipPath As String = apppath + "\updateresource\updateresource.zip"
                Dim extractPath As String = apppath + "\updatetemp"
                ProgressBar1.Value = 10
                Try
                    Dim fcheck As String = apppath + "\updatetemp"
                    If System.IO.Directory.Exists(fcheck) Then
                        System.IO.Directory.Delete(apppath + "\updatetemp", True)
                    End If
                    System.IO.Directory.CreateDirectory(apppath + "\updatetemp")
                    ZipFile.ExtractToDirectory(zipPath, extractPath)
                    My.Computer.FileSystem.CopyDirectory(apppath + "\updatetemp", apppath + "\app0.3.0", True)
                    ProgressBar1.Value = 100
                Catch ex As Exception
                    MessageBox.Show("Error! Builder Resource already installed", "Installation Exist Error!")
                    Application.Exit()
                End Try
                ProgressBar1.Value = 100
                MessageBox.Show("Patching complete! Click OK to launch P Browser Builder", "Installation Completed!")
                Process.Start(apppath + "\app0.3.0\P Browser Builder.exe")
                Application.Exit()
            Else
                MessageBox.Show("In case to reinstall, please uninstall older one first then running downloaded installer again.", "Already Install Detected!")
                Application.Exit()
            End If
        End If
        TabControl1.SelectedTab = TabPage3
        TabPage1.Enabled = False
        TabPage2.Enabled = False
        TabPage3.Enabled = True
        TabPage4.Enabled = False
        TabPage5.Enabled = False
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Label7.Text = "Disk space required 1.28 GB"
        End If
        If CheckBox1.Checked = False Then
            Label7.Text = "Disk space required 680 MB"
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim apppath As String = Application.StartupPath()
        If CheckBox4.Checked Then
            Process.Start(apppath + "\app0.3.0\P Browser Builder.exe")
        End If
        Application.Exit()
    End Sub
End Class
