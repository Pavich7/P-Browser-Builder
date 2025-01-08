Public Class fsstate
    Dim stage As Integer = 0
    Private Sub fsstate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        welcome.Enabled = False
        Label2.Visible = False
        Label4.Visible = False
        CheckBox1.Visible = False
        CheckBox2.Visible = False
        TabPage2.Enabled = False
        TabPage3.Enabled = False
        Dim apppath As String = Application.StartupPath()
        TabPage1.BackgroundImage = Image.FromFile(apppath + "\imgassets\setup.png")
    End Sub

    Private Sub Label29_Click(sender As Object, e As EventArgs) Handles Label29.Click
        Dim result As DialogResult = MessageBox.Show("Accept EULA?", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            stage = 2
            TabPage3.Enabled = True
            TabControl1.SelectedTab = TabPage3
            TabPage2.Enabled = False
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
        TabPage1.Enabled = True
        TabControl1.SelectedTab = TabPage1
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        MessageBox.Show("About data collection..." + vbNewLine + "Mandatory: Only collect data when first startup." + vbNewLine + "Optional: Collect data when every startup." + vbNewLine + "We are collecting your Builder Version, Hostname, IP Address.", "About data")
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Dim result As DialogResult = MessageBox.Show("Do you wish to exit P Browser Builder?", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        stage = 1
        TabPage2.Enabled = True
        TabControl1.SelectedTab = TabPage2
        TabPage1.Enabled = False
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
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
        Dim pbcfg As String = apppath + "\statedata\setting.builder.infsstate.pbcfg"
        Dim objWriter As New System.IO.StreamWriter(pbcfg)
        objWriter.Write("False")
        objWriter.Close()
        welcome.Enabled = True
        Me.Close()
    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Enter
        If stage = 0 Then
            TabControl1.SelectedTab = TabPage1
        End If
    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Enter
        If stage = 0 Then
            TabControl1.SelectedTab = TabPage1
        ElseIf stage = 1 Then
            TabControl1.SelectedTab = TabPage2
        End If
    End Sub
End Class