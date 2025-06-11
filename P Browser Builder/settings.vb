Imports System.IO
Imports CefSharp.DevTools.CSS

Module settings

    'settingver
    'hidesp
    'infsstate
    'inrsstate
    'resdlserver
    'usageinterv
    'winstateh
    'winstatew

    Dim apppath = Application.StartupPath
    Dim filePath As String = apppath + "\settings.pbcfg"

    Sub save(ByVal slotname As String, value As String)
        Dim lines As String() = File.ReadAllLines(filePath)
        If slotname = "hidesp" Then
            lines(1) = value
        ElseIf slotname = "infsstate" Then
            lines(2) = value
        ElseIf slotname = "inrsstate" Then
            lines(3) = value
        ElseIf slotname = "resdlserver" Then
            lines(4) = value
        ElseIf slotname = "usageinterv" Then
            lines(5) = value
        ElseIf slotname = "winstateh" Then
            lines(6) = value
        ElseIf slotname = "winstatew" Then
            lines(7) = value
        Else
            MessageBox.Show("Invalid saveslot!")
        End If
        File.WriteAllLines(filePath, lines)
    End Sub

    Function load(ByVal slotname As String)
        Dim lines As String() = File.ReadAllLines(filePath)
        Dim value As String
        If slotname = "settingver" Then
            value = lines(0)
        ElseIf slotname = "hidesp" Then
            value = lines(1)
        ElseIf slotname = "infsstate" Then
            value = lines(2)
        ElseIf slotname = "inrsstate" Then
            value = lines(3)
        ElseIf slotname = "resdlserver" Then
            value = lines(4)
        ElseIf slotname = "usageinterv" Then
            value = lines(5)
        ElseIf slotname = "winstateh" Then
            value = lines(6)
        ElseIf slotname = "winstatew" Then
            value = lines(7)
        Else
            MessageBox.Show("Invalid loadslot!")
        End If
        Return value
    End Function
End Module
