Imports System.Threading
Imports System.Text
Imports System.IO.Ports
Imports System.Net
Imports System.Net.WebSockets
Imports System.IO

Public Class Form1
    Public g_cts As New CancellationTokenSource()
    Public g_socket As New ClientWebSocket()
    Dim g_strUri As String
    Dim g_SerialPort As SerialPort
    Dim g_strComPort As String
    Dim g_IsOctoPrintRunning = False
    Dim g_IsYawCamRunning = False
    Dim g_strPrintersIP As String
    Dim g_IsUploadingFile = False
    Private WithEvents UploadWebClient As New WebClient

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'tbComPort.Text = My.Settings.ComPort
            cbComPort.SelectedText = My.Settings.ComPort
            tbPrinterIP.Text = My.Settings.PrinterIP
            g_strPrintersIP = tbPrinterIP.Text
            Timer1.Start()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub DataReceivedHandler(sender As Object, e As SerialDataReceivedEventArgs)
        Try
            If g_IsUploadingFile Then
                Exit Sub
            End If

            Dim sp As SerialPort = CType(sender, SerialPort)
            Dim data As String = sp.ReadLine
            SendSocketCommand(data)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub WorkerThread()
        Try
            Start().Wait()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Async Function Start() As Task
        Try
            g_socket = New ClientWebSocket()
            AddLog("Connecting to printer...")

            'socket.Options.KeepAliveInterval = New TimeSpan(90000)
            Await g_socket.ConnectAsync(New Uri(g_strUri), g_cts.Token)

            Dim strState As String = WebSocketState.Closed
            If (g_socket.State = WebSocketState.Open) Then
                strState = "Connected"
            ElseIf (g_socket.State = WebSocketState.Connecting) Then
                strState = "Connecting"
            End If

            Do While Not g_socket.State = WebSocketState.Open
                AddLog("Connecting...")
                Threading.Thread.Sleep(1000)
                Application.DoEvents()
            Loop

            AddLog(strState)
            AddLog("Waiting for connection from OctoPrint...")

            Await Task.Factory.StartNew(
                Async Function()
                    Dim rcvBytes = New Byte(127) {}
                    Dim rcvBuffer = New ArraySegment(Of Byte)(rcvBytes)
                    While True
                        Dim rcvResult As WebSocketReceiveResult = Await g_socket.ReceiveAsync(rcvBuffer, g_cts.Token)
                        Dim msgBytes As Byte() = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray()
                        Dim rcvMsg As String = Encoding.UTF8.GetString(msgBytes)
                        AddLog("Received: " & rcvMsg)
                        g_SerialPort.Write(rcvMsg)
                    End While
                End Function, g_cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.[Default])
        Catch ex As Exception
            If g_socket IsNot Nothing Then
                g_socket.Abort()
                g_socket.Dispose()
            End If

            If g_SerialPort IsNot Nothing Then
                g_SerialPort.Close()
                g_SerialPort.Dispose()
            End If
            MsgBox("Connection Error: Is printer turned on and connected to wifi?" & vbNewLine & ex.ToString)
        End Try
    End Function

    Public Sub SendSocketCommand(ByVal message As String)
        Try
            AddLog("Sent: " & message)
            Dim sendBytes As Byte() = Encoding.ASCII.GetBytes(message)
            Dim sendBuffer = New ArraySegment(Of Byte)(sendBytes)
            g_socket.SendAsync(sendBuffer, WebSocketMessageType.Text, endOfMessage:=True, cancellationToken:=g_cts.Token).Wait()
        Catch ex As Exception
            AddLog("SendSocketCommand Error: " & ex.ToString)
        End Try
    End Sub

    Private Sub AddLog(message As String)
        Try
            If tbLog.InvokeRequired Then
                tbLog.Invoke(DirectCast(AddressOf AddLog, Action(Of String)), message)
                Return
            End If
            tbLog.AppendText(message & vbNewLine)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnUpload_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Try
            Dim result As Integer = MessageBox.Show("Is the printer connected and octoprint disconnected?", "Warning!", MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Cancel Then
                Exit Sub
            ElseIf result = DialogResult.No Then
                Exit Sub
            ElseIf result = DialogResult.Yes Then
                Try
                    g_IsUploadingFile = True

                    SendWebCommand("http://" & g_strPrintersIP & "/set?code=M563 S6")

                    OpenFileDialog1.InitialDirectory = "c:\"
                    OpenFileDialog1.Filter = "GCODE Files (*.gcode)|*.gc|All files (*.*)|*.*"
                    OpenFileDialog1.FilterIndex = 2
                    OpenFileDialog1.RestoreDirectory = True

                    If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Dim myUri As String = String.Format("http://" & g_strPrintersIP & "/upload")
                        UploadWebClient.UploadFileTaskAsync(New Uri(myUri), OpenFileDialog1.FileName)
                        'UploadWebClient.UploadFileAsync(New Uri(myUri), OpenFileDialog1.FileName)
                    End If
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub myFtpUploadWebClient_UploadFileCompleted(sender As Object, e As System.Net.UploadFileCompletedEventArgs) Handles UploadWebClient.UploadFileCompleted
        Try
            If e.Error IsNot Nothing Then
                MsgBox("Error uploading file: " & e.Result.ToString & "-" & e.Error.Message)
                pbrFileUploadProgress.Value = 0
                g_IsUploadingFile = False
                Exit Sub
            End If

            Dim result As Integer = MessageBox.Show("File upload complete!, Would you like to rename the file or leave it as cache.gc?", "Notice", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                'SendSocketCommand("M566 " & OpenFileDialog1.SafeFileName)
                SendWebCommand("http://" & g_strPrintersIP & "/set?code=M566 " & OpenFileDialog1.SafeFileName)
                MsgBox("Ok, file was renamed to " & OpenFileDialog1.SafeFileName)
            ElseIf result = DialogResult.No Then
                MsgBox("Ok, filename left as cache.gc")
            End If

            pbrFileUploadProgress.Value = 0
            g_IsUploadingFile = False
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub UploadWebClient_UploadProgressChanged(sender As Object, e As System.Net.UploadProgressChangedEventArgs) Handles UploadWebClient.UploadProgressChanged
        Try
            Dim total As Integer = e.TotalBytesToSend
            Dim sent As Integer = e.BytesSent
            Dim left As Integer = total - sent
            pbrFileUploadProgress.Value = sent / total * 100
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Try

            If g_SerialPort IsNot Nothing Then
                g_SerialPort.Close()
                g_SerialPort.Dispose()
            End If

            My.Settings.PrinterIP = tbPrinterIP.Text
            My.Settings.ComPort = cbComPort.Text

            g_strPrintersIP = tbPrinterIP.Text
            g_strComPort = cbComPort.Text

            g_strUri = String.Format("ws://" & g_strPrintersIP & ":81")
            g_SerialPort = New SerialPort(g_strComPort)

            Dim t As New Thread(AddressOf WorkerThread)
            t.Start()

            g_SerialPort.BaudRate = 115200
            g_SerialPort.Parity = Parity.None
            g_SerialPort.StopBits = StopBits.One
            g_SerialPort.DataBits = 8
            g_SerialPort.Handshake = Handshake.None
            g_SerialPort.RtsEnable = True

            AddHandler g_SerialPort.DataReceived, AddressOf DataReceivedHandler

            AddLog("Opening Port")
            g_SerialPort.Open()
            AddLog("Port Open")

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnOctoPrint_Click(sender As Object, e As EventArgs) Handles btnOctoPrint.Click
        Try
            If g_IsOctoPrintRunning Then
                For Each p As Process In Process.GetProcesses()
                    If p.ProcessName.ToLower = "octoprint" Then
                        p.Kill()
                    End If
                Next
            Else
                Process.Start("C:\OctoPrint\venv\Scripts\octoprint.exe", "serve")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub btnYawam_Click(sender As Object, e As EventArgs) Handles btnYawam.Click
        Try
            If g_IsYawCamRunning Then
                For Each p As Process In Process.GetProcesses
                    If p.ProcessName.ToLower = "javaw" Then
                        p.Kill()
                    End If
                Next
            Else
                Dim p As New Process()
                p.StartInfo.WorkingDirectory = "C:\Program Files (x86)\Yawcam"
                p.StartInfo.FileName = "C:\Program Files (x86)\Yawcam\Yawcam.exe"
                p.Start()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            g_IsYawCamRunning = False
            g_IsOctoPrintRunning = False
            For Each p As Process In Process.GetProcesses
                If p.ProcessName.ToLower = "javaw" Then
                    g_IsYawCamRunning = True
                ElseIf p.ProcessName.ToLower = "octoprint" Then
                    g_IsOctoPrintRunning = True
                End If
            Next

            If g_IsYawCamRunning Then
                btnYawam.Text = "Stop Yawcam"
            Else
                btnYawam.Text = "Start Yawcam"
            End If

            If g_IsOctoPrintRunning Then
                btnOctoPrint.Text = "Stop Octoprint"
            Else
                btnOctoPrint.Text = "Start Octoprint"
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Function GetTime() As String
        Try
            Dim timeStamp As String = DateTime.Now.ToString("hhmmss")
            timeStamp = timeStamp.Insert(2, ":")
            timeStamp = timeStamp.Insert(5, ":")
            Return timeStamp
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Function

    Public Function SendWebCommand(ByVal cmd As String) As String
        Try
            Dim request As WebRequest = WebRequest.Create(cmd)
            Dim response As WebResponse = request.GetResponse()
            Dim strStatus As String = CType(response, HttpWebResponse).StatusDescription

            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()

            AddLog(responseFromServer)
            reader.Close()
            response.Close()
            Return responseFromServer
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return "Error!"
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Process.Start("http://127.0.0.1:5000")
        Catch ex As Exception
            MsgBox(ex, ToString)
        End Try
    End Sub

    Private Sub ComboBox1_DropDown(sender As Object, e As EventArgs) Handles cbComPort.DropDown
        cbComPort.Items.Clear()

        For Each sp As String In My.Computer.Ports.SerialPortNames
            cbComPort.Items.Add(sp)
        Next
    End Sub
End Class






