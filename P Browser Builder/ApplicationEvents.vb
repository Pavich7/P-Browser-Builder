Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            MessageBox.Show("Unhandled Error!" + vbNewLine + "We apologize, but an unexpected error occurred while running P Browser Builder." + vbNewLine + vbNewLine + "Error Details:" + vbNewLine + vbNewLine + e.Exception.Message + vbNewLine + vbNewLine + "Technical Details:" + vbNewLine + vbNewLine + e.Exception.ToString + vbNewLine + vbNewLine + "If the problem persists, please reinstall P Browser Builder.", "Fatal Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim result As DialogResult = MessageBox.Show("Would you like to report this problem on GitHub Issue?" + vbNewLine + "Error details will be copied to your clipboard.", "Report Bugs?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If (result = DialogResult.Yes) Then
                Clipboard.SetText(e.Exception.ToString)
                Process.Start("https://github.com/Pavich7/P-Browser-Builder/issues/new")
            End If
        End Sub
    End Class
End Namespace