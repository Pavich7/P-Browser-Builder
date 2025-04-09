﻿Imports System.IO

'aname
'url
'wwin
'hwin
'fixwin
'aotwin
'hwico
'opaval
'welena
'msgti
'msgde
'conxme
'm1chk
'm2chk
'userag

Module savemanager
    Sub loadsave(ByVal savloc As String)
        Dim apppath = Application.StartupPath
        Form1.TextBox1.Text = ""
        Form1.TextBox2.Text = ""
        Form1.TextBox3.Text = "944"
        Form1.TextBox4.Text = "573"
        Form1.TextBox5.Text = "100"
        Form1.TextBox6.Text = ""
        Form1.RadioButton2.Checked = True
        Form1.RadioButton3.Checked = False
        Form1.CheckBox1.Text = "Start your app after build"
        Form1.CheckBox2.Text = "Show your app in explorer after build"
        Form1.CheckBox1.Checked = True
        Form1.CheckBox2.Checked = False
        Form1.CheckBox3.Checked = False
        Form1.CheckBox4.Checked = False
        Form1.CheckBox5.Checked = False
        Form1.CheckBox6.Checked = False
        Form1.CheckBox7.Checked = False
        Form1.CheckBox8.Checked = False
        Form1.CheckBox9.Checked = False
        Form1.CheckBox10.Checked = False
        Form1.Button6.Visible = False
        Form1.CheckBox4.Visible = False
        Form1.Label24.Visible = True
        Form1.TabControl1.SelectedTab = Form1.TabPage1
        Form1.PictureBox1.Image = My.Resources.p_browser_icon_001_rq2_icon
        Form1.Browser.Load("about:blank")
        Form1.Label16.Text = "Application icons (*.ico)"
        Form1.Label46.Text = "Offline websites (Not set)"
        My.Settings.tempIcoLoc = ""
        System.IO.Directory.Delete(apppath + "\statecache\buildcache\appicns", True)
        System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\appicns")
        System.IO.Directory.Delete(apppath + "\statecache\buildcache\offlineweb", True)
        System.IO.Directory.CreateDirectory(apppath + "\statecache\buildcache\offlineweb")
        Form1.ProjnameToolStripMenuItem.Text = "Untitled Project"
        If savloc IsNot "" Then
            Try
                Dim fileReader As System.IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(savloc)
                Form1.TextBox2.Text = fileReader.ReadLine()
                Form1.TextBox1.Text = fileReader.ReadLine()
                Form1.TextBox3.Text = fileReader.ReadLine()
                Form1.TextBox4.Text = fileReader.ReadLine()
                Form1.CheckBox5.Checked = fileReader.ReadLine()
                Form1.CheckBox6.Checked = fileReader.ReadLine()
                Form1.CheckBox7.Checked = fileReader.ReadLine()
                Form1.TextBox5.Text = fileReader.ReadLine()
                Form1.CheckBox3.Checked = fileReader.ReadLine()
                welcomemessage.TextBox1.Text = fileReader.ReadLine()
                welcomemessage.TextBox2.Text = fileReader.ReadLine()
                Form1.CheckBox8.Checked = fileReader.ReadLine()
                Form1.RadioButton2.Checked = fileReader.ReadLine()
                Form1.RadioButton3.Checked = fileReader.ReadLine()
                Form1.TextBox6.Text = fileReader.ReadLine()
                If Form1.TextBox6.Text IsNot "" Then
                    Form1.CheckBox9.Checked = True
                End If
                Form1.ProjnameToolStripMenuItem.Text = Form1.TextBox2.Text
                fileReader.Close()
            Catch ex As Exception
                MessageBox.Show("Load Failed!" & vbNewLine & "Project is not compatiable or corrupt!" & vbNewLine & ex.Message, "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Sub savetofile()
        Try
            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Filter = "P Browser Builder Project (*.pbproj)|*.pbproj"
            saveFileDialog1.RestoreDirectory = True
            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    Using writer As New StreamWriter(myStream)
                        writer.WriteLine(Form1.TextBox2.Text)
                        writer.WriteLine(Form1.TextBox1.Text)
                        writer.WriteLine(Form1.TextBox3.Text)
                        writer.WriteLine(Form1.TextBox4.Text)
                        writer.WriteLine(Form1.CheckBox5.CheckState)
                        writer.WriteLine(Form1.CheckBox6.CheckState)
                        writer.WriteLine(Form1.CheckBox7.CheckState)
                        writer.WriteLine(Form1.TextBox5.Text)
                        writer.WriteLine(Form1.CheckBox3.CheckState)
                        writer.WriteLine(welcomemessage.TextBox1.Text)
                        writer.WriteLine(welcomemessage.TextBox2.Text)
                        writer.WriteLine(Form1.CheckBox8.CheckState)
                        writer.WriteLine(Form1.RadioButton2.Checked)
                        writer.WriteLine(Form1.RadioButton3.Checked)
                        If Form1.CheckBox9.Checked = True Then
                            writer.WriteLine(Form1.TextBox6.Text)
                        Else
                            writer.WriteLine("")
                        End If
                    End Using
                    myStream.Close()
                    MessageBox.Show("Saved to file!", "Completed!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Form1.ProjnameToolStripMenuItem.Text = Form1.TextBox2.Text
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Save Failed!" & vbNewLine & ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Module
