Imports System.IO
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class buildmanage
    Dim TotalSize As Long = 0
    Public Function GetDirSize(RootFolder As String) As Long
        Dim FolderInfo = New IO.DirectoryInfo(RootFolder)
        For Each File In FolderInfo.GetFiles : TotalSize += File.Length
        Next
        For Each SubFolderInfo In FolderInfo.GetDirectories : GetDirSize(SubFolderInfo.FullName)
        Next
        Return TotalSize
    End Function

    Private Sub Snooze(ByVal seconds As Integer)
        For i As Integer = 0 To seconds * 100
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim apppath As String = Application.StartupPath()
        Try
            Form1.Label7.Text = "Cleaning in progress..."
            Form1.ProgressBar1.Value = 20
            System.IO.Directory.Delete(apppath + "\binary", True)
            Form1.ProgressBar1.Value = 50
            System.IO.Directory.CreateDirectory(apppath + "\binary")
            System.IO.Directory.Delete(apppath + "\binarypkg", True)
            System.IO.Directory.CreateDirectory(apppath + "\binarypkg")
            Form1.ProgressBar1.Value = 100
            Label51.Text = "0.0 MB"
            Label52.Text = "0.0 MB"
            Label4.Text = "Application name: N/A"
            Label4.Enabled = False
            Form1.Label7.Text = "Cleanup completed!"
            Snooze(3)
            Form1.ProgressBar1.Value = 0
            Form1.Label7.Text = "Ready to build"
        Catch ex As Exception
            MessageBox.Show("Please close built app first before perform this action.", "Failed!")
            Form1.Label7.Text = "Cleanup failed!"
            Form1.ProgressBar1.Value = 0
            Snooze(3)
            Form1.Label7.Text = "Ready to build"
        End Try
    End Sub

    Private Sub buildmanage_Load(sender As Object, e As EventArgs) Handles MyBase.Load, PictureBox14.Click
        Dim apppath As String = Application.StartupPath()
        TotalSize = 0
        Dim TheSize2 As Long = GetDirSize(apppath + "\binary")
        Label51.Text = FormatNumber(TheSize2 / 1024 / 1024, 1) & " MB"
        TotalSize = 0
        Dim TheSize1 As Long = GetDirSize(apppath + "\binarypkg")
        Label52.Text = FormatNumber(TheSize1 / 1024 / 1024, 1) & " MB"
        If TheSize2 / 1024 / 1024 > 100 Then
            Try
                Dim filePath As String = Path.Combine(apppath, "binary\manifest.pbcfg")
                Dim lines As String() = File.ReadAllLines(filePath)
                If lines.Length >= 2 Then
                    Label4.Text = "Application name: " + lines(1)
                End If
            Catch ex As Exception
                Label4.Text = "Failed to load application name."
            End Try
        Else
            Label4.Text = "Application name: N/A"
            Label4.Enabled = False
        End If
        If System.IO.Directory.Exists(apppath + "\binary\statecache") Then
            Label5.Visible = True
        Else
            Label5.Visible = False
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim apppath As String = Application.StartupPath()
        Process.Start(apppath + "\binary")
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim apppath As String = Application.StartupPath()
        Process.Start(apppath + "\binarypkg")
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Dim apppath As String = Application.StartupPath()
        Try
            System.IO.Directory.Delete(apppath + "\binary\statecache", True)
            System.IO.Directory.Delete(apppath + "\binary\startlog", True)
            Dim result As DialogResult = MessageBox.Show("Did your App has Welcome Message enabled?", "Reset App", MessageBoxButtons.YesNo)
            If (result = DialogResult.Yes) Then
                Dim filePath As String = apppath + "\binary\manifest.pbcfg"
                Dim lines As String() = File.ReadAllLines(filePath)
                If lines.Length >= 7 Then
                    lines(6) = "True"
                End If
                File.WriteAllLines(filePath, lines)
            End If
            Label5.Visible = False
            MessageBox.Show("Successfully reset app!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Failed to reset app!", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class