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

namespace ChattingSystem_Server
{
    public partial class ServerForm : Form
    {
        private Socket _connectSocket;
        private Socket _acceptSocket;
        private static string _getClientIP = null;

        ServerEvent serverEvent = new ServerEvent();

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
        public string GetClientIP
        {
            get { return _getClientIP; }
            set { _getClientIP = value; }
        }

        delegate void DeligateGetClientIP(string IPAddress);
        delegate void DeligateButtonChange();
        delegate void DeligateDisconnectMessgae();

        public ServerForm()
        { 
            InitializeComponent();
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
            serverEvent.Disconnect(_acceptSocket, _connectSocket);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            bool isConnect = true;

            tbxReceivedData.Text = "";
            try
            {   
                tbxLocalIpAddress.Text =
                    Regex.Replace(tbxLocalIpAddress.Text, @"[^0-9].[^0-9].[^0-9].[^0-9]", "");
                tbxPort.Text = Regex.Replace(tbxPort.Text, @"[^0-9]", "");
                if (tbxLocalIpAddress.Text == "" || tbxPort.Text == "")
                {
                    throw new Exception();
                }

                if (isConnect)
                {
                    IPAddress serverIPAddress = IPAddress.Parse(tbxLocalIpAddress.Text.Trim());
                    IPEndPoint serverEndPoint = new IPEndPoint(serverIPAddress, Int32.Parse(tbxPort.Text));
                    _connectSocket = serverEvent.SetupSocket();
                    _connectSocket.Bind(serverEndPoint);
                    _connectSocket.Listen(1);

                    ThreadStart threadDelegate = new ThreadStart(ReceiveDataThread);
                    Thread playThread = new Thread(threadDelegate);
                    playThread.Start();
                }
            }
            catch
            {
                MessageBox.Show("Local IP Address 또는 Port 번호가 올바르지 않습니다.");
                isConnect = false;
            }
            if (isConnect)
                serverEvent.ButtonStatusChange();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            serverEvent.Disconnect(_acceptSocket, _connectSocket);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
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
        }


        public void ReceiveDataThread()
        {
            try
            {
                _acceptSocket = _connectSocket.Accept();
                GetClientIP = _acceptSocket.RemoteEndPoint.ToString();
            }
            catch 
            { 
                ShutdownThread();
                return; 
            }
            _connectSocket.Close();
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
                catch (SocketException se)
                {
                    ShutdownThread();
                    tbxReceivedData.Text += ReceiveClientIP(_acceptSocket.RemoteEndPoint.ToString() + " 와의 연결이 끊어졌습니다...";
                    break;
                }
            }
        }

        public void ShutdownThread()
        {
            
            try
            {   //Form_Closing 시, 바꿀 데이터가 없어서 오류가 발생함.
                this.Invoke(new DeligateButtonChange(serverEvent.ButtonStatusChange));
                this.Invoke(new DeligateDisconnectMessgae(serverEvent.DisconnectMessgae));
                _acceptSocket.Close();
                _acceptSocket.Dispose();
            }
            catch
            {

            }
        }

    }
}
