<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tbLog = New System.Windows.Forms.TextBox()
        Me.tbPrinterIP = New System.Windows.Forms.TextBox()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.pbrFileUploadProgress = New System.Windows.Forms.ProgressBar()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnOctoPrint = New System.Windows.Forms.Button()
        Me.btnYawam = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cbComPort = New System.Windows.Forms.ComboBox()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbLog
        '
        Me.tbLog.BackColor = System.Drawing.SystemColors.Window
        Me.tbLog.ForeColor = System.Drawing.SystemColors.InfoText
        Me.tbLog.Location = New System.Drawing.Point(12, 109)
        Me.tbLog.Multiline = True
        Me.tbLog.Name = "tbLog"
        Me.tbLog.ReadOnly = True
        Me.tbLog.Size = New System.Drawing.Size(353, 99)
        Me.tbLog.TabIndex = 0
        '
        'tbPrinterIP
        '
        Me.tbPrinterIP.BackColor = System.Drawing.SystemColors.Window
        Me.tbPrinterIP.Location = New System.Drawing.Point(12, 54)
        Me.tbPrinterIP.Name = "tbPrinterIP"
        Me.tbPrinterIP.Size = New System.Drawing.Size(96, 20)
        Me.tbPrinterIP.TabIndex = 2
        Me.tbPrinterIP.Text = "0.0.0.0"
        Me.tbPrinterIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(198, 51)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(60, 23)
        Me.btnConnect.TabIndex = 4
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(264, 51)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(101, 23)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "Upload GCODE"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'pbrFileUploadProgress
        '
        Me.pbrFileUploadProgress.Location = New System.Drawing.Point(264, 80)
        Me.pbrFileUploadProgress.Name = "pbrFileUploadProgress"
        Me.pbrFileUploadProgress.Size = New System.Drawing.Size(101, 23)
        Me.pbrFileUploadProgress.TabIndex = 9
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Printer IP:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(114, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "COM Port:"
        '
        'btnOctoPrint
        '
        Me.btnOctoPrint.Location = New System.Drawing.Point(12, 80)
        Me.btnOctoPrint.Name = "btnOctoPrint"
        Me.btnOctoPrint.Size = New System.Drawing.Size(85, 23)
        Me.btnOctoPrint.TabIndex = 15
        Me.btnOctoPrint.Text = "Start OctoPrint"
        Me.btnOctoPrint.UseVisualStyleBackColor = True
        '
        'btnYawam
        '
        Me.btnYawam.Location = New System.Drawing.Point(103, 80)
        Me.btnYawam.Name = "btnYawam"
        Me.btnYawam.Size = New System.Drawing.Size(87, 23)
        Me.btnYawam.TabIndex = 16
        Me.btnYawam.Text = "Start Yawcam"
        Me.btnYawam.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(198, 80)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(60, 23)
        Me.Button1.TabIndex = 17
        Me.Button1.Text = "UI"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(385, 24)
        Me.MenuStrip1.TabIndex = 18
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'cbComPort
        '
        Me.cbComPort.FormattingEnabled = True
        Me.cbComPort.Location = New System.Drawing.Point(117, 54)
        Me.cbComPort.Name = "cbComPort"
        Me.cbComPort.Size = New System.Drawing.Size(73, 21)
        Me.cbComPort.TabIndex = 19
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(385, 217)
        Me.Controls.Add(Me.cbComPort)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnYawam)
        Me.Controls.Add(Me.btnOctoPrint)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pbrFileUploadProgress)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.tbPrinterIP)
        Me.Controls.Add(Me.tbLog)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.ShowIcon = False
        Me.Text = "MPSM WIFI TUNNEL v0.1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbLog As TextBox
    Friend WithEvents tbPrinterIP As TextBox
    Friend WithEvents btnConnect As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents pbrFileUploadProgress As ProgressBar
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnOctoPrint As Button
    Friend WithEvents btnYawam As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Button1 As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cbComPort As ComboBox
End Class
