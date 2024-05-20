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
        Dim fileReader11 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\setting.builder.alwpdiag.pbcfg")
        Dim stringReader11 As String
        stringReader11 = fileReader11.ReadLine()
        If stringReader11 = "True" Then
            CheckBox1.Checked = True
        End If
        fileReader11.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim apppath As String = Application.StartupPath()
        Dim pbcfg As String = apppath + "\statedata\setting.builder.alwpdiag.pbcfg"
        Dim objWriter As New System.IO.StreamWriter(pbcfg)
        If CheckBox1.Checked = True Then
            objWriter.Write("True")
            MessageBox.Show("Successed! Diagnostic will be paused when startup.", "OK!")
        Else
            objWriter.Write("False")
            MessageBox.Show("Successed! Diagnostic will be running when startup.", "OK!")
        End If
        objWriter.Close()
    End Sub
End Class