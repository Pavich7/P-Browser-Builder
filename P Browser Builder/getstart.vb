Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class getstart
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/wiki")
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Process.Start("https://github.com/Pavich7/P-Browser-Builder/issues")
    End Sub

    Private Sub Label56_Click(sender As Object, e As EventArgs) Handles Label56.Click
        Dim apppath As String = Application.StartupPath()
        Dim rscheck As String = apppath + "\resource"
        If Not System.IO.Directory.Exists(rscheck) Then
            MessageBox.Show("Download resource for full experience." & vbNewLine & "You can download by click Download button below News Feed.", "Recommendation")
        End If
        savemanager.loadsave(apppath + "\assets\sample\sample1.pbproj")
        Form1.Enabled = True
        Form1.WindowState = FormWindowState.Normal
        welcome.realclose = False
        welcome.Close()
        Me.Close()
    End Sub
End Class