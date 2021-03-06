﻿using System;
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
        private ArrayList _clientInfoList = new ArrayList();
        private Dictionary<TcpClient, string> _clientList = new Dictionary<TcpClient, string>();
        ServerEvent serverEvent = new ServerEvent();

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
            get { return _clientInfoList; }
            set { _clientInfoList = value; }
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

                cbxChannel.Items.Add("All");
                for (int i = 65; i < 91; i++)
                {
                    cbxChannel.Items.Add(Convert.ToChar(i));
                }
                cbxChannel.Text = "All";
            }
            catch (Exception ex)
            {
                serverEvent.ErrorLog("ServerForm_Load", ex.Message);
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
                serverEvent.ErrorLog("btnStart_Click", ex.Message);
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
                
                serverEvent.SendServerLog(tbxSendData.Text);
                SendMessageAll("Server/"+tbxSendData.Text, cbxChannel.Text);
                DisplayText("Server /"+ cbxChannel.Text + ">" + tbxSendData.Text);
                tbxSendData.Text = "";
            }
            catch (Exception ex) 
            {
                serverEvent.ErrorLog("btnStart_Click", ex.Message);
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
                serverEvent.ErrorLog("GetClientIP", ex.Message);
            }
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
                        DisplayText(clientInfo + " 와의 연결이 끊어졌습니다.");
                    ClientInfoList.Clear();
                }
            }
            catch (Exception ex)
            {
                serverEvent.ErrorLog("ButtonStatusChange", ex.Message);
            }
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
            catch (Exception ex)
            {
                serverEvent.ErrorLog("Disconnect", ex.Message);
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
                        byte[] buffer = new byte[1024];
                        int bytes = stream.Read(buffer, 0, buffer.Length);
                        string channel = Encoding.Unicode.GetString(buffer, 0, bytes);
                        channel = channel.Split('$')[0];

                        this.Invoke(new DeligateGetClientIP(GetClientIP), channel);

                        ClientList.Add(TcpClient, channel);

                        IpPoint = (IPEndPoint)TcpClient.Client.RemoteEndPoint;
                        ClientIP = IpPoint.Address + ":" + IpPoint.Port;
                        ClientInfoList.Add(ClientIP.ToString());
                        SendMessageAll(ClientIP + "/ 님이 연결되었습니다.", channel);

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
                serverEvent.ErrorLog("InitSocket", ex.Message);
            }
            catch (Exception ex)
            {
                serverEvent.ErrorLog("InitSocket", ex.Message);
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
                ClientIP = message.Split('/')[0];
                Sendmessage = message.Substring(message.IndexOf("/") + 1);
                string displayMessage = ClientIP + "/" + channel + ">" + Sendmessage;
                DisplayText(displayMessage);
                SendMessageAll(message, channel);

                if (isClientClose)
                    ClientInfoList.Remove(ClientIP);
            }
            catch (Exception ex) 
            {
                serverEvent.ErrorLog("OnReceived", ex.Message);
            }
        }

        void HandlerClientsOnDisconnected(TcpClient clientScoket)
        {
            try
            {
                if (ClientList.ContainsKey(clientScoket))
                    ClientList.Remove(clientScoket);
            }
            catch (Exception ex)
            {
                serverEvent.ErrorLog("HandlerClientsOnDisconnected", ex.Message);
            }
        }



        public void SendMessageAll(string message, string channel)
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
                    else if (channel == "All")
                    {
                        ClientIP = message.Split('/')[0];
                        Sendmessage = message.Substring(message.IndexOf("/") + 1);
                        buffer = Encoding.Unicode.GetBytes(ClientIP + "/" + channel + ">" + Sendmessage);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();

                    }
                }
            }
            catch (Exception ex)
            {
                serverEvent.ErrorLog("SendMessageAll", ex.Message);
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
                serverEvent.ErrorLog("DisplayText", ex.Message);
            }
        }
        #endregion METHOD AREA ************************************************
    }
}
