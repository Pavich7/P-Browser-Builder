Public Class usagesetting
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Please Enter value!", "FAILED!")
        Else
            Dim apppath As String = Application.StartupPath()
            Dim pbcfg As String = apppath + "\statedata\setting.builder.usageinterv.pbcfg"
            Dim objWriter As New System.IO.StreamWriter(pbcfg)
            objWriter.Write(TextBox1.Text)
            objWriter.Close()
            MessageBox.Show("Successed! You may need to restart to apply the change.", "OK!")
        End If
    End Sub

    Private Sub usagesetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Dim fileReader1 As System.IO.StreamReader
        fileReader1 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.usageinterv.pbcfg")
        Dim stringReader1 As String
        stringReader1 = fileReader1.ReadLine()
        TextBox1.Text = stringReader1
        fileReader1.Close()
    End Sub
End Class