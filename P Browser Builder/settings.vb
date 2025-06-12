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
    'autocfu
    'designview
    'startlog
    'actiontoolbox
    'navigationbar

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
        ElseIf slotname = "autocfu" Then
            lines(8) = value
        ElseIf slotname = "designview" Then
            lines(9) = value
        ElseIf slotname = "startlog" Then
            lines(10) = value
        ElseIf slotname = "actiontoolbox" Then
            lines(11) = value
        ElseIf slotname = "navigationbar" Then
            lines(12) = value
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
        ElseIf slotname = "autocfu" Then
            value = lines(8)
        ElseIf slotname = "designview" Then
            value = lines(9)
        ElseIf slotname = "startlog" Then
            value = lines(10)
        ElseIf slotname = "actiontoolbox" Then
            value = lines(11)
        ElseIf slotname = "navigationbar" Then
            value = lines(12)
        End If
        Return value
    End Function
End Module
