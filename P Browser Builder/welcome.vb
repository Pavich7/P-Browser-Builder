Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class welcome
    Private Sub Panel1_Paint(sender As Object, e As EventArgs) Handles Panel1.Click
        Form1.Enabled = True
        Me.Close()
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property
    Private Sub welcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Me.BackgroundImage = Image.FromFile(apppath + "\imgassets\start.png")
        Dim rscheck As String = apppath + "\resource"
        Label7.Visible = False
        If Not System.IO.Directory.Exists(rscheck) Then
            Label7.Visible = True
        End If
        Form1.Enabled = False
    End Sub
    Private Sub loadsave()
        Dim apppath As String = Application.StartupPath()
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.Title = "Open P Browser Builder Project"
        OpenFileDialog1.Filter = "P Browser Builder Project|*.pbproj"
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Dim fileReader As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(OpenFileDialog1.FileName)
            Form1.TextBox2.Text = fileReader.ReadLine()
            Form1.TextBox1.Text = fileReader.ReadLine()
            Form1.TextBox3.Text = fileReader.ReadLine()
            Form1.TextBox4.Text = fileReader.ReadLine()
            Form1.CheckBox5.Checked = fileReader.ReadLine()
            Form1.CheckBox6.Checked = fileReader.ReadLine()
            'aname
            'url
            'wwin
            'hwin
            'fixwin
            'aotwin
            Form1.Enabled = True
            Form1.ProjnameToolStripMenuItem.Text = Form1.TextBox2.Text
            Me.Close()
        End If
    End Sub
    Private Sub Panel2_Paint(sender As Object, e As EventArgs) Handles Panel2.Click
        loadsave()
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As EventArgs) Handles Panel3.Click
        Application.Exit()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Application.Exit()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Application.Exit()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        loadsave()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        loadsave()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        loadsave()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form1.Enabled = True
        Me.Close()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Form1.Enabled = True
        Me.Close()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Form1.Enabled = True
        Me.Close()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        MessageBox.Show("After fresh install you will need to install resource to build. (Download size: approx. 140 MB)", "Info...")
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Dim result As DialogResult = MessageBox.Show("Do you wish to reset all setting?" + vbNewLine + "This cannot be undone!", "You sure about this?", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Dim apppath As String = Application.StartupPath()
            Dim objWriter As New System.IO.StreamWriter(apppath + "\statedata\setting.builder.inrsstate.pbcfg")
            objWriter.Write("True")
            objWriter.Close()
            Application.Restart()
        End If
    End Sub
End Class