Public Class fsstate
    Private Sub fsstate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label29_Click(sender As Object, e As EventArgs) Handles Label29.Click
        Dim result As DialogResult = MessageBox.Show("Accept EULA?", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Dim apppath As String = Application.StartupPath()
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
End Class