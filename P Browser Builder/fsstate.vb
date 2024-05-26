Public Class fsstate
    Private Sub fsstate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label29_Click(sender As Object, e As EventArgs) Handles Label29.Click
        Dim result As DialogResult = MessageBox.Show("Accept EULA?", "You sure about this?", MessageBoxButtons.YesNo)
        Dim apppath As String = Application.StartupPath()
        If CheckBox1.Checked = False Then
            Dim pbcfg1 As String = apppath + "\statedata\setting.builder.datacol.pbcfg"
            Dim objWriter1 As New System.IO.StreamWriter(pbcfg1)
            objWriter1.Write("False")
            objWriter1.Close()
        Else
            Dim pbcfg11 As String = apppath + "\statedata\setting.builder.datacol.pbcfg"
            Dim objWriter11 As New System.IO.StreamWriter(pbcfg11)
            objWriter11.Write("True")
            objWriter11.Close()
        End If
        If (result = DialogResult.Yes) Then
            Dim pbcfg As String = apppath + "\statedata\setting.builder.infsstate.pbcfg"
            Dim objWriter As New System.IO.StreamWriter(pbcfg)
            objWriter.Write("False")
            objWriter.Close()
            Form1.Enabled = True
            Me.Close()
        End If
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim result As DialogResult = MessageBox.Show("Do you wish to exit P Browser Builder?", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        MessageBox.Show("About data collection..." + vbNewLine + "Mandatory: Only collect data when first startup." + vbNewLine + "Optional: Collect data when every startup." + vbNewLine + "We are collecting your Builder Version, Hostname, IP Address.", "About data")
    End Sub
End Class