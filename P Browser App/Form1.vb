Imports CefSharp
Imports CefSharp.WinForms
Imports System.IO.StreamReader
Public Class Form1
    Private WithEvents Browser As ChromiumWebBrowser
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim apppath As String = Application.StartupPath()
            Dim setting As New CefSettings
            Dim filereader As System.IO.StreamReader
            filereader = My.Computer.FileSystem.OpenTextFileReader(apppath + "\builderdata.pbcfg")
            Dim loadweb As String
            loadweb = filereader.ReadLine()
            setting.RemoteDebuggingPort = 8088
            CefSharp.Cef.Initialize(setting)
            Browser = New ChromiumWebBrowser(loadweb)
            Panel1.Controls.Add(Browser)
        Catch ex As Exception
            MessageBox.Show("Failed to start application!", "Fatal Error!")
            Application.Exit()
        End Try
    End Sub
End Class