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
        Form1.Enabled = False
        Dim apppath As String = Application.StartupPath()
        Dim fileReader11 As System.IO.StreamReader
        Dim fileReader21 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.urlsave.pbsf")
        fileReader21 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.anamesave.pbsf")
        Dim stringReader11 As String
        Dim stringReader21 As String
        stringReader11 = fileReader11.ReadLine()
        stringReader21 = fileReader21.ReadLine()
        If stringReader11 = "" Then
            If stringReader21 = "" Then
                Panel2.Enabled = False
                Label1.Text = "Load last saved project. (Not Found)"
            End If
        End If
        fileReader11.Close()
        fileReader21.Close()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As EventArgs) Handles Panel2.Click
        Dim apppath As String = Application.StartupPath()
        Dim fileReader11 As System.IO.StreamReader
        Dim fileReader21 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.urlsave.pbsf")
        fileReader21 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.anamesave.pbsf")
        Dim stringReader11 As String
        Dim stringReader21 As String
        stringReader11 = fileReader11.ReadLine()
        stringReader21 = fileReader21.ReadLine()
        Form1.TextBox1.Text = stringReader11
        Form1.TextBox2.Text = stringReader21
        Form1.Enabled = True
        fileReader11.Close()
        fileReader21.Close()
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
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.urlsave.pbsf")
        fileReader21 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.anamesave.pbsf")
        Dim stringReader11 As String
        Dim stringReader21 As String
        stringReader11 = fileReader11.ReadLine()
        stringReader21 = fileReader21.ReadLine()
        Form1.TextBox1.Text = stringReader11
        Form1.TextBox2.Text = stringReader21
        Form1.Enabled = True
        fileReader11.Close()
        fileReader21.Close()
        Me.Close()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim apppath As String = Application.StartupPath()
        Dim fileReader11 As System.IO.StreamReader
        Dim fileReader21 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.urlsave.pbsf")
        fileReader21 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.anamesave.pbsf")
        Dim stringReader11 As String
        Dim stringReader21 As String
        stringReader11 = fileReader11.ReadLine()
        stringReader21 = fileReader21.ReadLine()
        Form1.TextBox1.Text = stringReader11
        Form1.TextBox2.Text = stringReader21
        Form1.Enabled = True
        fileReader11.Close()
        fileReader21.Close()
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim apppath As String = Application.StartupPath()
        Dim fileReader11 As System.IO.StreamReader
        Dim fileReader21 As System.IO.StreamReader
        fileReader11 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.urlsave.pbsf")
        fileReader21 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\statedata\usersave.builder.anamesave.pbsf")
        Dim stringReader11 As String
        Dim stringReader21 As String
        stringReader11 = fileReader11.ReadLine()
        stringReader21 = fileReader21.ReadLine()
        Form1.TextBox1.Text = stringReader11
        Form1.TextBox2.Text = stringReader21
        Form1.Enabled = True
        fileReader11.Close()
        fileReader21.Close()
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
End Class