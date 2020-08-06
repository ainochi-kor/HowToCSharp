using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ChattingSystem_Server
{
    public partial class ServerForm : Form
    {
        ServerEvent serverEvent = new ServerEvent();
        
        
        private static string _clientIP = null;

        public string ClientIP
        {
            get { return _clientIP; }
            set { _clientIP = value; }
        }
        /*
        private Socket _connectSocket;
        private Socket _acceptSocket;

        public Socket ConnectSocket
        {
            get { return _connectSocket; }
            set { _connectSocket = value; }
        }
        public Socket AcceptSocket
        {
            get { return _acceptSocket; }
            set { _acceptSocket = value; }
        }
        
        */

        TcpListener _tcpListner = null;
        TcpClient _tcpClient = null;
        static int counter = 0;

        public Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();

        delegate void DeligateGetClientIP();
        delegate void DeligateButtonChange();
        delegate void DeligateDisconnectMessgae();
        delegate void DeligateEndThread();
        

        public ServerForm()
        { 
            InitializeComponent();

            
        }

        private void InitSocket()
        {
            try
            {


                tbxLocalIpAddress.Text =
                    Regex.Replace(tbxLocalIpAddress.Text, @"[^0-9].[^0-9].[^0-9].[^0-9]", "");
                tbxPort.Text = Regex.Replace(tbxPort.Text, @"[^0-9]", "");
                if (tbxLocalIpAddress.Text == "" || tbxPort.Text == "")
                {
                    throw new Exception();
                }

                
                //IPAddress serverIPAddress = IPAddress.Parse(tbxLocalIpAddress.Text.Trim());
                //IPEndPoint serverEndPoint = new IPEndPoint(serverIPAddress, Int32.Parse(tbxPort.Text));

                _tcpListner = new TcpListener(IPAddress.Parse(tbxLocalIpAddress.Text.Trim()), Int32.Parse(tbxPort.Text));
                _tcpClient = default(TcpClient);
                //_tcpListner = new TcpListener(IPAddress.Any, Int32.Parse(tbxPort.Text));
                _tcpListner.Start();
                DisplayText(">> server Started");

                /*
                _connectSocket = serverEvent.SetupSocket();
                _connectSocket.Bind(serverEndPoint);
                _connectSocket.Listen(1000);

                ThreadStart threadDelegate = new ThreadStart(ReceiveDataThread);
                Thread playThread = new Thread(threadDelegate);
                playThread.Start();
                */
                while(true)
                {
                    try
                    {
                        counter++;
                        _tcpClient = _tcpListner.AcceptTcpClient();
                        DisplayText(">> Accept connection from client");
                        //ClientIP = _tcpClient.RemoteEndPoint.ToString();
                        this.Invoke(new DeligateGetClientIP(GetClientIP));

                        NetworkStream stream = _tcpClient.GetStream();
                        byte[] buffer = new byte[1024];
                        int bytes = stream.Read(buffer, 0, buffer.Length);
                        string channel = Encoding.Unicode.GetString(buffer, 0, bytes);
                        channel = channel.Substring(0, channel.IndexOf("$"));

                        clientList.Add(_tcpClient, channel);

                        SendMessageAll(channel + "Joined ", "", false);


                        handleClient h_client = new handleClient();
                        h_client.OnReceived += new handleClient.MessageDisplayHandler(OnReceived);
                        h_client.OnDisconnected += new handleClient.DisconnectedHandler(h_client_OnDisconnected);
                        h_client.startClient(_tcpClient, clientList);
                        
                    }
                    catch (SocketException se)
                    {
                        //Trace.WriteLine(string.Format("InitSocket - SocketException : {0}", se.Message));
                        break;
                    }
                    catch (Exception ex)
                    {
                        //Trace.WriteLine(string.Format("InitSocket - Exception : {0}", ex.Message));
                        break;
                    }
                }
                _tcpClient.Close();
                _tcpListner.Stop();
            }
            catch
            {
                MessageBox.Show("Local IP Address 또는 Port 번호가 올바르지 않습니다.");
                //isConnect = false;
            }
        }

        void h_client_OnDisconnected(TcpClient clientScoket)
        {
            if (clientList.ContainsKey(clientScoket))
                clientList.Remove(clientScoket);
        }

        private void OnReceived(string message, string user_name)
        {
            string displayMessage = ClientIP +"/" + user_name + "> " + message;
            DisplayText(displayMessage);
            SendMessageAll(message, user_name, true);
        }

        public void SendMessageAll(string message, string user_name, bool flag)
        {
            foreach (var pair in clientList)
            {
                Trace.WriteLine(string.Format("tcpclient : {0} user_name : {1}", pair.Key, pair.Value));

                TcpClient client = pair.Key as TcpClient;
                NetworkStream stream = client.GetStream();
                byte[] buffer = null;

                if (flag)
                {
                    buffer = Encoding.Unicode.GetBytes(user_name + " says: " + message);
                }
                else
                {
                    buffer = Encoding.Unicode.GetBytes(message);
                }

                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
        }

        public void ServerForm_Load(object sender, EventArgs e)
        {
            //폼 로드시, 현재 컴퓨터의 LocalIPAddress를 TextBox에 출력합니다.
            tbxLocalIpAddress.Text = serverEvent.LocalIPAddress();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //serverEvent.Disconnect();
            Disconnect();
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            bool isConnect = true;
            rtbxReceivedData.Text = "";

            Thread t = new Thread(InitSocket);
            t.IsBackground = true;
            t.Start();

            
            if (isConnect == true)
               ButtonStatusChange();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //serverEvent.Disconnect();
            //ButtonStatusChange();
            Disconnect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            /*
            if (tbxSendData.Text == "") return;

            try
            {
                byte[] sendData = Encoding.UTF8.GetBytes(tbxSendData.Text);
                _acceptSocket.Send(BitConverter.GetBytes(sendData.Length), 0, 4, 0);
                _acceptSocket.Send(sendData);
            }
            catch(SocketException socketExcept)
            {
                MessageBox.Show("텍스트 전송이 불가능합니다 \r\n" + socketExcept.ToString());
            }
            tbxSendData.Text = "";
             * */
        }

        /*
        public void ReceiveDataThread()
        {
            try
            {
                _acceptSocket = _connectSocket.Accept();
                ClientIP = _acceptSocket.RemoteEndPoint.ToString();

                this.Invoke(new DeligateGetClientIP(GetClientIP));


                while (true)
                {   //Client로 부터 수신 받은 메시지의 크기를 받고, 메시지 출력.
                    try
                    {
                        byte[] bufferSize = new byte[4];
                        _acceptSocket.Receive(bufferSize, 0, bufferSize.Length, 0);
                        int remainSize = BitConverter.ToInt32(bufferSize, 0);

                        MemoryStream recordMemory = new MemoryStream();
                        while (remainSize > 0)
                        {
                            byte[] buffer;
                            if (remainSize < _acceptSocket.ReceiveBufferSize)
                                buffer = new byte[remainSize];
                            else
                                buffer = new byte[_acceptSocket.ReceiveBufferSize];

                            int usedSize = _acceptSocket.Receive(buffer, 0, buffer.Length, 0);

                            remainSize -= usedSize;
                            recordMemory.Write(buffer, 0, buffer.Length);
                        }
                        recordMemory.Close();
                        byte[] recodeData = recordMemory.ToArray();
                        recordMemory.Dispose();
                        Invoke((MethodInvoker)delegate
                        {
                            tbxReceivedData.Text += Encoding.UTF8.GetString(recodeData) + "\r\n";
                        });
                    }
                    catch
                    {
                        throw new Exception();
                    }
                }
            }
            catch
            {
                MessageBox.Show("쓰레드 에러 발생");
                this.Invoke(new DeligateButtonChange(ButtonStatusChange));
                this.Invoke(new DeligateEndThread(ShutdownThread));
            }
        }
        */

        private void GetClientIP()
        {
            IPEndPoint ipPoint = (IPEndPoint)_tcpClient.Client.RemoteEndPoint;
            ClientIP = ipPoint.Address.ToString();
            rtbxReceivedData.Text += ClientIP + "와 연결되었습니다.\r\n";
        }

        public void ShutdownThread()
        {
            try
            {   //Form_Closing 시, 바꿀 데이터가 없어서 오류가 발생함.
                
                Thread thread = Thread.CurrentThread;
                thread.Interrupt();
                _tcpClient.Close();
                //_tcpClient.Dispose();
                Thread.Sleep(1000); 
            }
            catch
            {
                
            }
        }

        public void ButtonStatusChange()
        {
            //ServerForm server = new ServerForm();
            try
            {
                //MessageBox.Show(server.Controls.Find("btnStart", true).FirstOrDefault().Enabled.ToString());
                btnStart.Enabled = !(btnStart.Enabled);
                btnStop.Enabled = !(btnStop.Enabled);

                if(btnStart.Enabled == true)
                    rtbxReceivedData.Text += ClientIP + " 와의 연결이 끊어졌습니다...";
            }
            catch { }
        }

        public void Disconnect()
        {

            //ServerForm server = new ServerForm();

            if (_tcpClient != null)
            {
                _tcpClient.Close();
                //_tcpClient.Dispose();
            }
            if (_tcpListner != null)
            {
                _tcpListner.Stop();
                //_tcpListner.Dispose();
            }
            /*
             ServerForm server = new ServerForm();

            if (server.AcceptSocket != null)
            {
                server.AcceptSocket.Close();
                server.AcceptSocket.Dispose();
            }
            if (server.ConnectSocket != null)
            {
                server.ConnectSocket.Close();
                server.ConnectSocket.Dispose();
            }
             */
        }

        private void DisplayText(string text)
        {
            if (rtbxReceivedData.InvokeRequired)
            {
                rtbxReceivedData.BeginInvoke(new MethodInvoker(delegate
                {
                    rtbxReceivedData.AppendText(text + Environment.NewLine);
                }));
            }
            else
                rtbxReceivedData.AppendText(text + Environment.NewLine);
        }
    }
}
