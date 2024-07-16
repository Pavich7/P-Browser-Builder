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
End Class