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
        Dim fileReader11 As System.IO.StreamReader
        Dim fileReader21 As System.IO.StreamReader
        Dim fileReader31 As System.IO.StreamReader
        Dim fileReader41 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.urlsave.pbsf")
        fileReader21 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.anamesave.pbsf")
        fileReader31 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.wwindow.pbsf")
        fileReader41 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.hwindow.pbsf")
        Dim stringReader11 As String
        Dim stringReader21 As String
        Dim stringReader31 As String
        Dim stringReader41 As String
        stringReader11 = fileReader11.ReadLine()
        stringReader21 = fileReader21.ReadLine()
        stringReader31 = fileReader31.ReadLine()
        stringReader41 = fileReader41.ReadLine()
        If stringReader11 = "" Then
            If stringReader21 = "" Then
                If stringReader31 = "" Then
                    If stringReader41 = "" Then
                        Panel2.Enabled = False
                        Label1.Text = "Load last saved project. (Not Found)"
                    End If
                End If
            End If
        End If
        fileReader11.Close()
        fileReader21.Close()
        fileReader31.Close()
        fileReader41.Close()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As EventArgs) Handles Panel2.Click
        Dim apppath As String = Application.StartupPath()
        Dim fileReader11 As System.IO.StreamReader
        Dim fileReader21 As System.IO.StreamReader
        Dim fileReader31 As System.IO.StreamReader
        Dim fileReader41 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.urlsave.pbsf")
        fileReader21 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.anamesave.pbsf")
        fileReader31 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.wwindow.pbsf")
        fileReader41 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.hwindow.pbsf")
        Dim stringReader11 As String
        Dim stringReader21 As String
        Dim stringReader31 As String
        Dim stringReader41 As String
        stringReader11 = fileReader11.ReadLine()
        stringReader21 = fileReader21.ReadLine()
        stringReader31 = fileReader31.ReadLine()
        stringReader41 = fileReader41.ReadLine()
        Form1.TextBox1.Text = stringReader11
        Form1.TextBox2.Text = stringReader21
        Form1.TextBox3.Text = stringReader31
        Form1.TextBox4.Text = stringReader41
        Form1.Enabled = True
        fileReader11.Close()
        fileReader21.Close()
        fileReader31.Close()
        fileReader41.Close()
        Me.Close()
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
        Dim apppath As String = Application.StartupPath()
        Dim fileReader11 As System.IO.StreamReader
        Dim fileReader21 As System.IO.StreamReader
        Dim fileReader31 As System.IO.StreamReader
        Dim fileReader41 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.urlsave.pbsf")
        fileReader21 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.anamesave.pbsf")
        fileReader31 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.wwindow.pbsf")
        fileReader41 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.hwindow.pbsf")
        Dim stringReader11 As String
        Dim stringReader21 As String
        Dim stringReader31 As String
        Dim stringReader41 As String
        stringReader11 = fileReader11.ReadLine()
        stringReader21 = fileReader21.ReadLine()
        stringReader31 = fileReader31.ReadLine()
        stringReader41 = fileReader41.ReadLine()
        Form1.TextBox1.Text = stringReader11
        Form1.TextBox2.Text = stringReader21
        Form1.TextBox3.Text = stringReader31
        Form1.TextBox4.Text = stringReader41
        Form1.Enabled = True
        fileReader11.Close()
        fileReader21.Close()
        fileReader31.Close()
        fileReader41.Close()
        Me.Close()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim apppath As String = Application.StartupPath()
        Dim fileReader11 As System.IO.StreamReader
        Dim fileReader21 As System.IO.StreamReader
        Dim fileReader31 As System.IO.StreamReader
        Dim fileReader41 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.urlsave.pbsf")
        fileReader21 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.anamesave.pbsf")
        fileReader31 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.wwindow.pbsf")
        fileReader41 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.hwindow.pbsf")
        Dim stringReader11 As String
        Dim stringReader21 As String
        Dim stringReader31 As String
        Dim stringReader41 As String
        stringReader11 = fileReader11.ReadLine()
        stringReader21 = fileReader21.ReadLine()
        stringReader31 = fileReader31.ReadLine()
        stringReader41 = fileReader41.ReadLine()
        Form1.TextBox1.Text = stringReader11
        Form1.TextBox2.Text = stringReader21
        Form1.TextBox3.Text = stringReader31
        Form1.TextBox4.Text = stringReader41
        Form1.Enabled = True
        fileReader11.Close()
        fileReader21.Close()
        fileReader31.Close()
        fileReader41.Close()
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim apppath As String = Application.StartupPath()
        Dim fileReader11 As System.IO.StreamReader
        Dim fileReader21 As System.IO.StreamReader
        Dim fileReader31 As System.IO.StreamReader
        Dim fileReader41 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.urlsave.pbsf")
        fileReader21 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.anamesave.pbsf")
        fileReader31 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.wwindow.pbsf")
        fileReader41 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.hwindow.pbsf")
        Dim stringReader11 As String
        Dim stringReader21 As String
        Dim stringReader31 As String
        Dim stringReader41 As String
        stringReader11 = fileReader11.ReadLine()
        stringReader21 = fileReader21.ReadLine()
        stringReader31 = fileReader31.ReadLine()
        stringReader41 = fileReader41.ReadLine()
        Form1.TextBox1.Text = stringReader11
        Form1.TextBox2.Text = stringReader21
        Form1.TextBox3.Text = stringReader31
        Form1.TextBox4.Text = stringReader41
        Form1.Enabled = True
        fileReader11.Close()
        fileReader21.Close()
        fileReader31.Close()
        fileReader41.Close()
        Me.Close()
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