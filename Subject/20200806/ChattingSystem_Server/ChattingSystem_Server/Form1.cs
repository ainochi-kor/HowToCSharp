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
        private static string _clientIP = null;

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
        public string ClientIP
        {
            get { return _clientIP; }
            set { _clientIP = value; }
        }

        delegate void DeligateGetClientIP();
        delegate void DeligateButtonChange();
        delegate void DeligateDisconnectMessgae();
        delegate void DeligateEndThread();
        

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
            //serverEvent.Disconnect();
            Disconnect();
            
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
                    _connectSocket.Listen(1000);

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

        private void GetClientIP()
        {
            ClientIP = _acceptSocket.RemoteEndPoint.ToString();
            tbxReceivedData.Text += ClientIP + "와 연결되었습니다.\r\n";
        }

        public void ShutdownThread()
        {
            try
            {   //Form_Closing 시, 바꿀 데이터가 없어서 오류가 발생함.
                
                Thread thread = Thread.CurrentThread;
                thread.Interrupt();
                _acceptSocket.Close();
                _acceptSocket.Dispose();
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
                    tbxReceivedData.Text += ClientIP + " 와의 연결이 끊어졌습니다...";
            }
            catch { }
        }

        public void Disconnect()
        {

            //ServerForm server = new ServerForm();

            if (AcceptSocket != null)
            {
                AcceptSocket.Close();
                AcceptSocket.Dispose();
            }
            if (ConnectSocket != null)
            {
                ConnectSocket.Close();
                ConnectSocket.Dispose();
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

    }
}
