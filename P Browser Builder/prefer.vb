﻿Imports System.IO
Imports System.Reflection.Emit

Public Class prefer
    Private Sub prefer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        Dim rescheck As String = apppath + "\resource"
        If Not System.IO.Directory.Exists(rescheck) Then
            Label4.Text = "Redist : Resource not installed"
            Label2.Text = "WinForms : Resource not installed"
            Label5.Text = "Common : Resource not installed"
            Label23.Text = "Version : Resource not installed"
            Label4.Enabled = False
            Label2.Enabled = False
            Label5.Enabled = False
            Label23.Enabled = False
            Label7.Text = "Delete curerently insatlled resource. (Resource not installed)"
            Label14.Text = "Online check for resource update. (Resource not installed)"
            Label8.Enabled = False
            Label13.Enabled = False
        Else
            Try
                Dim fileReader1 As System.IO.StreamReader
                Dim fileReader2 As System.IO.StreamReader
                Dim fileReader3 As System.IO.StreamReader
                Dim fileReader4 As System.IO.StreamReader
                fileReader1 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\buildspace\quickmode\pkgData\cefre.pbad")
                fileReader2 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\buildspace\quickmode\pkgData\cefwinf.pbad")
                fileReader3 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\buildspace\quickmode\pkgData\cefcomn.pbad")
                fileReader4 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\metadata\version.txt")
                Dim stringReader1 As String
                Dim stringReader2 As String
                Dim stringReader3 As String
                Dim stringReader4 As String
                stringReader1 = fileReader1.ReadLine()
                stringReader2 = fileReader2.ReadLine()
                stringReader3 = fileReader3.ReadLine()
                stringReader4 = fileReader4.ReadLine()
                Label4.Text = "Redist : " + stringReader1
                Label2.Text = "WinForms : " + stringReader2
                Label5.Text = "Common : " + stringReader3
                Label23.Text = "Version : " + stringReader4
            Catch ex As Exception
                MessageBox.Show("Data gather failure!" + vbNewLine + "Error : " + ex.Message, "Fatal Error!")
                Me.Close()
            End Try
        End If
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Try
            Dim result As DialogResult = MessageBox.Show("Do you wish to completely uninstall builder resource?" + vbNewLine + "You can reinstall resource later via notification box", "You sure about this?", MessageBoxButtons.YesNo)
            If (result = DialogResult.Yes) Then
                Dim apppath As String = Application.StartupPath()
                System.IO.Directory.Delete(apppath + "\resource", True)
                MessageBox.Show("P Browser Builder need to restart app", "Uninstall Completed!")
                Application.Restart()
            End If
        Catch ex As Exception
            MessageBox.Show("Could not attempt to uninstall resource!" + vbNewLine + ex.Message + vbNewLine + "You may need to restart builder and try again.", "Error!")
        End Try
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Try
            Dim apppath As String = Application.StartupPath()
            System.IO.Directory.Delete(apppath + "\updatedata", True)
            System.IO.Directory.CreateDirectory(apppath + "\updatedata")
            Label9.Enabled = False
        Catch ex As Exception
            MessageBox.Show("Could not attempt to delete installer cache!" + vbNewLine + ex.Message + vbNewLine + "You may need to restart builder and try again.", "Error!")
        End Try
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Try
            Dim apppath As String = Application.StartupPath()
            Dim metacheck As String = apppath + "\resource\metadata\version.txt"
            If Not System.IO.File.Exists(metacheck) Then
                MessageBox.Show("Resource not compatible with this version of P Browser Builder!" + vbNewLine + "Please update resource by uninstall and reinstall via build menu.", "Error!")
            Else
                System.IO.Directory.Delete(apppath + "\resource\getcache", True)
                System.IO.Directory.CreateDirectory(apppath + "\resource\getcache")
                Dim fileReader As System.IO.StreamReader
                My.Computer.Network.DownloadFile("http://pavichdev.ddns.net/service/app.pavichdev.pbrowserbuilder/v1/cfuversion/onlineresver.txt", apppath + "\resource\getcache\onlineresver.txt")
                Dim fileReader1 As System.IO.StreamReader
                fileReader1 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\getcache\onlineresver.txt")
                Dim stringReader1 As String
                stringReader1 = fileReader1.ReadLine()
                fileReader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\resource\metadata\version.txt")
                Dim stringReader As String
                stringReader = fileReader.ReadLine()
                If stringReader1.Contains(stringReader) Then
                    MessageBox.Show("Resource is up-to-date!", "Check for update")
                Else
                    MessageBox.Show("New version detected! (" + stringReader1 + ")" + vbNewLine + "Please update by uninstall and reinstall resource via build menu.", "Check for update")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Could not fetch latest online version info!" + vbNewLine + "Please try again later.", "Error!")
        End Try
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        about.Show()
    End Sub
End Class