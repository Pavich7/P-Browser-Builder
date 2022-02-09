Imports System.IO.Compression
Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Dim zipPath As String = apppath + "\installresource\installresource.zip"
        Dim extractPath As String = apppath
        ProgressBar1.Value = 10
        Try
            ZipFile.ExtractToDirectory(zipPath, extractPath)
            ProgressBar1.Value = 100
            If CheckBox1.Checked = False Then
                System.IO.Directory.Delete(apppath + "\resource", True)
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
            Process.Start(apppath + "\P Browser Builder.exe")
        End If
        Application.Exit()
    End Sub
End Class
