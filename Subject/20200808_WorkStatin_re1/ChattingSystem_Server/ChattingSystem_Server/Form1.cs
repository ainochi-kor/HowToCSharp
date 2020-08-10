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
        ClientInfo clientInfo;
        ClientHandler[] Room;

        private TcpListener _tcpListner = null;
        private TcpClient _tcpClient = null;
        static int counter = 0;
        

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
        
        
        public Dictionary<TcpClient, string> _clientList = new Dictionary<TcpClient, string>();

        delegate void DeligateGetClientIP();
        delegate void DeligateButtonChange();
        delegate void DeligateDisconnectMessgae();
        delegate void DeligateEndThread();
        delegate void DeligateRoom();
        

        public ServerForm()
        { 
            InitializeComponent();
        }

        
        private void ChattingChannel()
        {
            Room = new ClientHandler[26];
            for (int i = 0; i < Room.Length; i++)
                Room[i] = new ClientHandler();
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
                //this.Invoke(new DeligateRoom());
                
                while(true)
                {
                    try
                    {
                        counter++;
                        TcpClient = TcpListner.AcceptTcpClient();

                        IPEndPoint ipPoint = (IPEndPoint)TcpClient.Client.RemoteEndPoint;
                        string clientIP = ipPoint.Address.ToString();
                        int clientPort = Int32.Parse(ipPoint.Port.ToString());
                         
                        NetworkStream stream = TcpClient.GetStream();
                        byte[] buffer = new byte[1024];
                        int bytes = stream.Read(buffer, 0, buffer.Length);
                        string clientChannel = Encoding.Unicode.GetString(buffer, 0, bytes);
                        clientChannel = clientChannel.Substring(0, clientChannel.IndexOf("$"));
                     
                        clientInfo = new ClientInfo(clientIP, clientPort, clientChannel);
                        this.Invoke(new DeligateGetClientIP(GetClientIP));


                        _clientList.Add(TcpClient, clientInfo.Channel);
                        

                        //SendMessageAll(clientInfo.Channel + "Joined ", "", false);

                        handleClient hadlerClient = new handleClient();
                        hadlerClient.OnReceived += new handleClient.MessageDisplayHandler(OnReceived);
                        hadlerClient.OnDisconnected += new handleClient.DisconnectedHandler(HandlerClientsOnDisconnected);
                        hadlerClient.startClient(TcpClient, _clientList);
                    }
                    catch (SocketException se)
                    {
                        Trace.WriteLine(string.Format("InitSocket - SocketException : {0}", se.Message));
                        ChattingChannel();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(string.Format("InitSocket - Exception : {0}", ex.Message));
                        ChattingChannel();
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
            if (_clientList.ContainsKey(clientScoket))
                _clientList.Remove(clientScoket);
        }

        private void OnReceived(string message, string channel)
        {
            clientInfo.Message = message;
            string displayMessage = clientInfo.ClientIP + ":" + clientInfo.Port + "/" + clientInfo.Channel + "> " + clientInfo.Message;
            DisplayText(displayMessage);
            SendMessageAll(message, channel, true);
        }

        public void SendMessageAll(string message, string user_name, bool flag)
        {
            foreach (var pair in _clientList)
            {
                //MessageBox.Show(string.Format("tcpclient : {0} user_name : {1}", pair.Key, pair.Value));

                TcpClient client = pair.Key as TcpClient;
                NetworkStream stream = client.GetStream();
                byte[] buffer = null;

                if (pair.Value == user_name)
                {
                    buffer = Encoding.Unicode.GetBytes(user_name + " says: " + message);
                }
                else
                {
                    buffer = Encoding.Unicode.GetBytes(message);
                    buffer = null;
                }

                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ChattingChannel();
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
            rtbxReceivedData.Text += clientInfo.ClientIP + "와 연결되었습니다.\r\n";
            Monitor observer = new Monitor("제발 되어주세요");
            char channel = clientInfo.Channel[0];
            observer.Subscribe(Room[Convert.ToInt32(channel) - 65]);
        }

        public void StartObserver()
        {
            
        }

        
    }
}
