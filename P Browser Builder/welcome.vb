﻿Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class welcome
    Public realclose As Boolean
    Public Sub MyForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If realclose = True Then
            Application.Exit()
        End If
    End Sub

    Private Sub welcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Me.BackgroundImage = Image.FromFile(apppath + "\imgassets\start.png")
        Dim rscheck As String = apppath + "\resource"
        Label7.Visible = False
        If Not System.IO.Directory.Exists(rscheck) Then
            Label7.Visible = True
        End If
        Form1.Enabled = False
        realclose = True
    End Sub

    Private Sub getstarted(sender As Object, e As EventArgs) Handles Panel3.Click, PictureBox3.Click, Label6.Click, Label5.Click
        getstart.Show()
    End Sub

    Private Sub loadsavebtn(sender As Object, e As EventArgs) Handles PictureBox2.Click, Label2.Click, Label1.Click, Panel2.Click
        Dim apppath As String = Application.StartupPath()
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.Title = "Open P Browser Builder Project"
        OpenFileDialog1.Filter = "P Browser Builder Project|*.pbproj"
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            savemanager.loadsave(OpenFileDialog1.FileName)
            Form1.Enabled = True
            Form1.WindowState = FormWindowState.Normal
            realclose = False
            Me.Close()
        End If
    End Sub

    Private Sub newproj(sender As Object, e As EventArgs) Handles PictureBox1.Click, Label3.Click, Label4.Click, Panel1.Click
        Form1.Enabled = True
        Form1.WindowState = FormWindowState.Normal
        realclose = False
        Me.Close()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        MessageBox.Show("After fresh install you will need to install resource to build. (Download size: approx. 150 MB)", "Info...")
    End Sub

    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click
        prefer.Show()
    End Sub
End Class