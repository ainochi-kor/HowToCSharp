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
        //;

        private TcpListener _tcpListner = null;
        private TcpClient _tcpClient = null;
        private static string _clientIP = null;
        //static int counter = 0;
        IPEndPoint ipPoint;
        public Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();

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

        delegate void DeligateGetClientIP();
        delegate void DeligateButtonChange();
        delegate void DeligateDisconnect();

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
            catch (Exception ex)
            {
                MessageBox.Show("btnStart_Click : \r\n" + ex.ToString());
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
                MessageBox.Show("btnStart_Click : \r\n" + ex.ToString());
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
                SendMessageAll(tbxSendData.Text, "Server", false);
                tbxSendData.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnSend_Click : \r\n" + ex.ToString());
            }
        }
    }
}
