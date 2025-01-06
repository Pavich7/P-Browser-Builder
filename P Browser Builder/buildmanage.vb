﻿Imports System.IO
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class buildmanage
    Dim TotalSize As Long = 0
    Public Function GetDirSize(RootFolder As String) As Long
        Dim FolderInfo = New IO.DirectoryInfo(RootFolder)
        For Each File In FolderInfo.GetFiles : TotalSize += File.Length
        Next
        For Each SubFolderInfo In FolderInfo.GetDirectories : GetDirSize(SubFolderInfo.FullName)
        Next
        Return TotalSize
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim apppath As String = Application.StartupPath()
        Try
            Form1.Label7.Text = "Cleaning in progress..."
            Form1.ProgressBar1.Value = 20
            System.IO.Directory.Delete(apppath + "\binary", True)
            Form1.ProgressBar1.Value = 50
            System.IO.Directory.CreateDirectory(apppath + "\binary")
            System.IO.Directory.Delete(apppath + "\binarypkg", True)
            System.IO.Directory.CreateDirectory(apppath + "\binarypkg")
            Form1.ProgressBar1.Value = 100
            Label51.Text = "0.0 MB"
            Label52.Text = "0.0 MB"
            Label4.Text = "Application name: N/A"
            Label4.Enabled = False
            Form1.Label7.Text = "Cleanup completed!"
        Catch ex As Exception
            MessageBox.Show("Please close built app first before perform this action.", "Failed!")
            Form1.Label7.Text = "Cleanup failed!"
            Form1.ProgressBar1.Value = 0
        End Try
    End Sub

    Private Sub buildmanage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim apppath As String = Application.StartupPath()
        TotalSize = 0
        Dim TheSize2 As Long = GetDirSize(apppath + "\binary")
        Label51.Text = FormatNumber(TheSize2 / 1024 / 1024, 1) & " MB"
        TotalSize = 0
        Dim TheSize1 As Long = GetDirSize(apppath + "\binarypkg")
        Label52.Text = FormatNumber(TheSize1 / 1024 / 1024, 1) & " MB"
        If TheSize2 / 1024 / 1024 > 100 Then
            Dim fileReader19 As System.IO.StreamReader
            fileReader19 = My.Computer.FileSystem.OpenTextFileReader(apppath + "\binary\progdata.pbcfg")
            Dim stringReader19 As String = fileReader19.ReadLine()
            Label4.Text = "Application name: " + stringReader19
            fileReader19.Close()
        Else
            Label4.Text = "Application name: N/A"
            Label4.Enabled = False
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim apppath As String = Application.StartupPath()
        Process.Start(apppath + "\binary")
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim apppath As String = Application.StartupPath()
        Process.Start(apppath + "\binarypkg")
    End Sub
End Class