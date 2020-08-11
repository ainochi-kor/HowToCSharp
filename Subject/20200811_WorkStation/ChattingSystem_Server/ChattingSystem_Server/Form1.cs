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
        private ArrayList _ClientInfoList = new ArrayList();
        private Dictionary<TcpClient, string> _clientList = new Dictionary<TcpClient, string>();

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

        public ArrayList ClientInfoList
        {
            get { return _ClientInfoList; }
            set { _ClientInfoList = value; }
        }

        public Dictionary<TcpClient, string> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; }
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
            }
            catch (Exception ex){ }
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
            catch (Exception ex){ }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessageAll(tbxSendData.Text, "Server", false);
                tbxSendData.Text = "";
            }
            catch (Exception ex){ }
        }
        #endregion FORM CONTROL AREA *********************************************

        #region METHOD AREA ***************************************************

        private void GetClientIP(string channel)
        {
            try
            {
                IpPoint = (IPEndPoint)TcpClient.Client.RemoteEndPoint;
                ClientIP = IpPoint.Address + ":" + IpPoint.Port + "/";
                rtbxReceivedData.Text += IpPoint.Address + ":" + IpPoint.Port + "/" +channel + " 님이 연결되었습니다.\r\n";
            }
            catch (Exception ex){ }
        }

        public void ButtonStatusChange()
        {
            try
            {
                btnStart.Enabled = !(btnStart.Enabled);
                btnStop.Enabled = !(btnStop.Enabled);


                if (btnStart.Enabled == true && ClientIP != null)
                {
                    foreach (string clientInfo in ClientInfoList)
                        rtbxReceivedData.Text += clientInfo + " 와의 연결이 끊어졌습니다.\r\n";
                    ClientInfoList.Clear();
                }
            }
            catch (Exception ex){ }
        }

        public void Disconnect()
        {
            try
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
            catch (Exception ex){ }
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
                        byte[] buffer = new byte[1024];
                        int bytes = stream.Read(buffer, 0, buffer.Length);
                        string channel = Encoding.Unicode.GetString(buffer, 0, bytes);
                        channel = channel.Split('$')[0];

                        this.Invoke(new DeligateGetClientIP(GetClientIP),channel);

                        ClientList.Add(TcpClient, channel);

                        IpPoint = (IPEndPoint)_tcpClient.Client.RemoteEndPoint;
                        ClientIP = IpPoint.Address + ":" + IpPoint.Port;
                        ClientInfoList.Add(ClientIP.ToString());
                        SendMessageAll(ClientIP + "/ 님이 연결되었습니다.", channel, false);

                        HandleClient handle = new HandleClient();
                        handle.OnReceived += new HandleClient.MessageDisplayHandler(OnReceived);
                        handle.OnDisconnected += new HandleClient.DisconnectedHandler(HandlerClientsOnDisconnected);
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
            }
            catch (Exception ex)
            { }
            finally
            {
                this.Invoke(new DeligateButtonChange(ButtonStatusChange));
            }
        }

        private void OnReceived(string message, string channel)
        {
            try
            {
                ClientIP = message.Split('/')[0];
                Sendmessage = message.Substring(message.IndexOf("/") + 1);
                string displayMessage = ClientIP + "/" + channel + ">" + Sendmessage;
                DisplayText(displayMessage);
                SendMessageAll(message, channel, true);

                if (Sendmessage == channel + " 연결을 해제합니다.")
                    ClientInfoList.Remove(ClientIP);
            }
            catch (Exception ex){ }
        }

        void HandlerClientsOnDisconnected(TcpClient clientScoket)
        {
            try
            {
                if (ClientList.ContainsKey(clientScoket))
                    ClientList.Remove(clientScoket);
            }
            catch (Exception ex){ }
        }



        public void SendMessageAll(string message, string channel, bool flag)
        {
            try
            {
                foreach (var pair in ClientList)
                {

                    TcpClient client = pair.Key as TcpClient;
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = null;

                    if (channel == pair.Value)
                    {
                        
                        ClientIP = message.Split('/')[0];
                        Sendmessage = message.Substring(message.IndexOf("/") + 1);
                        buffer = Encoding.Unicode.GetBytes(ClientIP + "/" + channel + ">" + Sendmessage);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();
                    }
                    else if (channel == "Server")
                    {
                        buffer = Encoding.Unicode.GetBytes(channel + ">" + message);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();
                        DisplayText(channel + ">" + message);
                    }
                }
            }
            catch (Exception ex) { }
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
            catch (Exception ex){ }
        }
        #endregion METHOD AREA ************************************************
    }
}
