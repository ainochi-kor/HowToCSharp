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

        private TcpListener _tcpListner = null;
        private TcpClient _tcpClient = null;
        private static string _clientIP = null;
        static int counter = 0;

        public string ClientIP
        {
            get { return _clientIP; }
            set { _clientIP = value; }
        }
        public TcpListener TcpListner
        {
            get { return _tcpListner; }
            set { _tcpListner = value; }
        }
        public TcpClient TcpClient
        {
            get { return _tcpClient; }
            set { _tcpClient = value; }
        }
        
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
                TcpListner = new TcpListener(IPAddress.Parse(tbxLocalIpAddress.Text.Trim()), Int32.Parse(tbxPort.Text));
                TcpClient = default(TcpClient);
                TcpListner.Start();
                DisplayText(">> server Started");

                while(true)
                {
                    try
                    {
                        counter++;
                        TcpClient = TcpListner.AcceptTcpClient();
                        //DisplayText(">> Accept connection from client");
                        this.Invoke(new DeligateGetClientIP(GetClientIP));

                        NetworkStream stream = TcpClient.GetStream();
                        byte[] buffer = new byte[1024];
                        int bytes = stream.Read(buffer, 0, buffer.Length);
                        string channel = Encoding.Unicode.GetString(buffer, 0, bytes);
                        channel = channel.Substring(0, channel.IndexOf("$"));

                        clientList.Add(TcpClient, channel);

                        SendMessageAll(channel + "Joined ", "", false);


                        handleClient h_client = new handleClient();
                        h_client.OnReceived += new handleClient.MessageDisplayHandler(OnReceived);
                        h_client.OnDisconnected += new handleClient.DisconnectedHandler(HandlerClientsOnDisconnected);
                        h_client.startClient(TcpClient, clientList);
                        
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
                TcpClient.Close();
                TcpListner.Stop();
            }
            catch
            {
                MessageBox.Show("Local IP Address 또는 Port 번호가 올바르지 않습니다.");
            }
        }

        void HandlerClientsOnDisconnected(TcpClient clientScoket)
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
            Disconnect();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            bool isConnect = true;
            rtbxReceivedData.Text = "";

            Thread ServerThread = new Thread(InitSocket);
            ServerThread.IsBackground = true;
            ServerThread.Start();
            
            if (isConnect == true)
               ButtonStatusChange();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
           //서버쪽에서 서버쪽으로 보내는 것으로 바꿔야 함.
        }

        private void GetClientIP()
        {
            IPEndPoint ipPoint = (IPEndPoint)TcpClient.Client.RemoteEndPoint;
            ClientIP = ipPoint.Address.ToString();
            rtbxReceivedData.Text += ClientIP + "와 연결되었습니다.\r\n";
        }

        public void ShutdownThread()
        {
            try
            {   //Form_Closing 시, 바꿀 데이터가 없어서 오류가 발생함.
                Thread thread = Thread.CurrentThread;
                thread.Interrupt();
                TcpClient.Close();
                Thread.Sleep(1000); //Dispose하는 시간이 필요하여 1초정도 메인 쓰레드를 쉬어줌.
            }
            catch
            { }
        }

        public void ButtonStatusChange()
        {
            try
            {
                btnStart.Enabled = !(btnStart.Enabled);
                btnStop.Enabled = !(btnStop.Enabled);

                if(btnStart.Enabled == true)
                    rtbxReceivedData.Text += ClientIP + " 와의 연결이 끊어졌습니다...";
            }
            catch { }
        }

        public void Disconnect()
        {
            if (TcpClient != null)
            {
                TcpClient.Close();
            }
            if (TcpListner != null)
            {
                TcpListner.Stop();
            }
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
