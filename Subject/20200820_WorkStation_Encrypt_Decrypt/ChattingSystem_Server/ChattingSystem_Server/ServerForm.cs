using System;
using System.Collections.Generic;
using System.Collections;
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
using log4net;
using log4net.Config;



namespace ChattingSystem_Server
{
    public partial class ServerForm : Form
    {
        #region CONST & FIELD AREA ********************************************

        private TcpListener _tcpListner = null;
        private TcpClient _tcpClient = null;
        private static string _clientIP = null;
        private static string _sendmessage = null;
        private IPEndPoint _ipPoint;
        private Dictionary<TcpClient, string> _clientList = new Dictionary<TcpClient, string>();
        ServerEvent _serverEvent = new ServerEvent();
        ServerSecurity _encrypted = new ServerSecurity();

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

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

        public string ClientIP
        {
            get { return _clientIP; }
            set { _clientIP = value; }
        }

        public string Sendmessage
        {
            get { return _sendmessage; }
            set { _sendmessage = value; }
        }

        public IPEndPoint IpPoint
        {
            get { return _ipPoint; }
            set { _ipPoint = value; }
        }

        public Dictionary<TcpClient, string> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; }
        }

        public string PortNum()
        {
            return tbxPort.Text;
        }

        public ServerEvent ServerEvent
        {
            get { return _serverEvent; }
            set { _serverEvent = value; }
        }

        internal ServerSecurity Encrypted
        {
            get { return _encrypted; }
            set { _encrypted = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region DELEGATE AREA *************************************************

        delegate void DeligateGetClientIP(string channel);
        delegate void DeligateButtonChange();
        delegate void DeligateDisconnect();

        #endregion DELEGATE AREA **********************************************

        #region FORM CONTROL AREA *********************************************

        public ServerForm()
        {
            InitializeComponent();
        }

        public void ServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                ServerEvent serverEvent = new ServerEvent();
                tbxLocalIpAddress.Text = serverEvent.LocalIPAddress();
                btnStart.Enabled = true;
                btnStop.Enabled = false;

                cbxChannel.Items.Add("All");
                for (int i = 65; i < 91; i++)
                {
                    cbxChannel.Items.Add(Convert.ToChar(i));
                }
                cbxChannel.Text = "All";
            }
            catch (Exception ex)
            {
                ServerEvent.ErrorLog("ServerForm_Load", ex.Message);
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                bool isConnect = true;
                rtbxReceivedData.Text = "";

                Thread ServerThread = new Thread(InitSocket);
                ServerThread.IsBackground = true;
                ServerThread.Start();

                if (isConnect == true)
                    ButtonStatusChange();
            }
            catch (Exception ex)
            {
                ServerEvent.ErrorLog("btnStart_Click", ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessageAll(tbxSendData.Text, cbxChannel.Text, true);
                DisplayText("Server /"+ cbxChannel.Text + ">" + tbxSendData.Text);
                tbxSendData.Text = "";
            }
            catch (Exception ex) 
            {
                ServerEvent.ErrorLog("btnStart_Click", ex.Message);
            }
        }
        #endregion FORM CONTROL AREA *********************************************

        #region METHOD AREA ***************************************************

        private void GetClientIP(string channel)
        {
            try
            {
                IpPoint = (IPEndPoint)TcpClient.Client.RemoteEndPoint;
                ClientIP = IpPoint.Address + ":" + IpPoint.Port + "/";
                rtbxReceivedData.Text += IpPoint.Address + ":" + IpPoint.Port + "/" + channel + " 님이 연결되었습니다.\r\n";
            }
            catch (Exception ex) 
            {
                ServerEvent.ErrorLog("GetClientIP", ex.Message);
            }
        }

        public void ButtonStatusChange()
        {
            try
            {
                btnStart.Enabled = !(btnStart.Enabled);
                btnStop.Enabled = !(btnStop.Enabled);
            }
            catch (Exception ex)
            {
                ServerEvent.ErrorLog("ButtonStatusChange", ex.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                foreach (var pair in ClientList)
                {
                    TcpClient client = pair.Key as TcpClient;
                    client.Close();
                }
                ClientList.Clear();

                if (TcpClient != null)
                {
                    TcpClient.Close();
                }
                if (TcpListner != null)
                {
                    TcpListner.Stop();
                }
            }
            catch (Exception ex)
            {
                ServerEvent.ErrorLog("Disconnect", ex.Message);
            }
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
                    MessageBox.Show("Local IP Address 또는 Port 번호가 올바르지 않습니다.");
                    throw new Exception();
                }
                TcpListner = new TcpListener(IPAddress.Parse(tbxLocalIpAddress.Text.Trim()), Int32.Parse(tbxPort.Text));
                TcpClient = default(TcpClient);
                TcpListner.Start();
                DisplayText(">> server Started");
                while (true)
                {
                    try
                    {
                        TcpClient = TcpListner.AcceptTcpClient();
                        NetworkStream stream = TcpClient.GetStream();                      
                        stream = TcpClient.GetStream();

                        byte[] sizeBuf = new byte[TcpClient.ReceiveBufferSize];
                        stream.Read(sizeBuf, 0, (int)TcpClient.ReceiveBufferSize);
                        int size = BitConverter.ToInt32(sizeBuf, 0);
                        
                        MemoryStream memoryStream = new MemoryStream();

                        while (size > 0)
                        {
                            byte[] buffer;
                            if (size < TcpClient.ReceiveBufferSize)
                                buffer = new byte[size];
                            else
                                buffer = new byte[TcpClient.ReceiveBufferSize];
                            
                            int rec = stream.Read(buffer, 0, buffer.Length);

                            size -= rec;
                            memoryStream.Write(buffer, 0, buffer.Length);
                        }
                        memoryStream.Close();
                        byte[] data = memoryStream.ToArray();
                        memoryStream.Dispose();

                        string channel = Encoding.UTF8.GetString(data);
                        this.Invoke(new DeligateGetClientIP(GetClientIP), channel);
                        ClientList.Add(TcpClient, channel);
                        SendMessageAll(ClientIP + "/ 님이 연결되었습니다.", channel, true);

                        HandleClient handle = new HandleClient();
                        handle.OnReceived += new HandleClient.MessageDisplayHandler(OnReceived);
                        handle.OnDisconnected += new HandleClient.DisconnectedHandler(OnDisconnected);
                        handle.StartClient(TcpClient, ClientList);

                    }
                    catch
                    {
                        throw new SocketException();
                    }
                }
            }
            catch (SocketException ex)
            {
                this.Invoke(new DeligateDisconnect(Disconnect));
                ServerEvent.ErrorLog("InitSocket", ex.Message);
            }
            catch (Exception ex)
            {
                ServerEvent.ErrorLog("InitSocket", ex.Message);
            }
            finally
            {
                this.Invoke(new DeligateButtonChange(ButtonStatusChange));
            }
        }

        private void OnReceived(string message, string channel, bool isClientClose)
        {
            try
            {
                DisplayText(message);
                SendMessageAll(message, channel, false);
            }
            catch (Exception ex) 
            {
                ServerEvent.ErrorLog("OnReceived", ex.Message);
            }
        }

        void OnDisconnected(TcpClient clientScoket)
        {   
            try
            {
                IpPoint = (IPEndPoint)clientScoket.Client.RemoteEndPoint;
                ClientIP = ClientIP = IpPoint.Address + ":" + IpPoint.Port;
                DisplayText(ClientIP + "/ 님의 연결이 해제되었습니다.");
                SendMessageAll(ClientIP + "/ 님의 연결이 해제되었습니다.", ClientList[clientScoket].ToString(), true);
                if (ClientList.ContainsKey(clientScoket))
                    ClientList.Remove(clientScoket);
            }
            catch (Exception ex)
            {
                ServerEvent.ErrorLog("OnDisconnected", ex.Message);
            }
        }

        public void SendMessageAll(string message, string channel, bool isServer)
        {
            try
            {
                foreach (var pair in ClientList)
                {
                    TcpClient client = pair.Key as TcpClient;
                    IpPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = null;

                    if(isServer)
                    {
                        string sendmessage = Encrypted.EncryptedMessage(message, IpPoint.Address.ToString());
                        ServerEvent.SendServerLog(sendmessage);
                        if (channel == pair.Value)
                        {
                            buffer = Encoding.Unicode.GetBytes("ⓐ" + sendmessage);
                            stream.Write(buffer, 0, buffer.Length);
                            stream.Flush();
                        }
                        else if (channel == "All")
                        {
                            buffer = Encoding.Unicode.GetBytes("ⓐ" + sendmessage);
                            stream.Write(buffer, 0, buffer.Length);
                            stream.Flush();
                        }
                    }
                    else
                    {
                        if (channel == pair.Value)
                        {
                            buffer = Encoding.Unicode.GetBytes(message);
                            stream.Write(buffer, 0, buffer.Length);
                            stream.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ServerEvent.ErrorLog("SendMessageAll", ex.Message);
            }
        }

        private void DisplayText(string text)
        {
            try
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
            catch (Exception ex)
            {
                ServerEvent.ErrorLog("DisplayText", ex.Message);
            }
        }
        #endregion METHOD AREA ************************************************
    }
}
