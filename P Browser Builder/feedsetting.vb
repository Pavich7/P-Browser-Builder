Imports System.Net
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class feedsetting
    Private Sub feedsetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Dim fileReader11 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.nfstartfetch.pbcfg")
        Dim stringReader11 As String
        stringReader11 = fileReader11.ReadLine()
        If stringReader11 = "False" Then
            CheckBox1.Checked = True
        End If
        fileReader11.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim apppath As String = Application.StartupPath()
        Dim pbcfg As String = apppath + "\statedata\setting.builder.nfstartfetch.pbcfg"
        Dim objWriter As New System.IO.StreamWriter(pbcfg)
        If CheckBox1.Checked = True Then
            objWriter.Write("False")
            MessageBox.Show("Successed! News Feed will fetch on startup. You may need to restart to make change.", "OK!")
        Else
            objWriter.Write("True")
            MessageBox.Show("Successed! News Feed will fetch on startup. You may need to restart to make change.", "OK!")
        End If
        Me.Close()
        objWriter.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim client As WebClient = New WebClient()
            Dim nf1desc As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v1/nf/nf1_desc.txt")
            Dim nf1titl As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v1/nf/nf1_title.txt")
            Dim nf1date As String = client.DownloadString("https://pavich7.github.io/MBP-Services/pbb-v1/nf/nf1_date.txt")
            Form1.Label12.Text = nf1titl
            Form1.Label13.Text = nf1desc
            Form1.Label14.Text = nf1date
            MessageBox.Show("News Feed refreshed!", "OK!")
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error while refreshing News Feed", "Error!")
        End Try
    End Sub
End Class