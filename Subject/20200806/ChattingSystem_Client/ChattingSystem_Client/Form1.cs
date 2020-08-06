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

namespace ChattingSystem_Client
{
    public partial class Client_Form : Form
    {
        Socket _connectSocket;



        delegate void DeligateButtonChange();
        delegate void DeligateDisconnectMessgae();

        public Client_Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
            }
            catch { }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if(_connectSocket.Connected)
                {
                    _connectSocket.Close();
                }
            }
            catch { }//접속 안하고 끄면 오류 생김.
            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _connectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {   
                tbxLocalIpAddress.Text = 
                    Regex.Replace(tbxLocalIpAddress.Text, @"[^0-9].[^0-9].[^0-9].[^0-9]", "");
                tbxPort.Text = Regex.Replace(tbxPort.Text, @"[^0-9]", "");
                if (tbxLocalIpAddress.Text.Replace(".","") == "" || tbxPort.Text == "")
                {
                    throw new Exception();
                }
                _connectSocket.Connect(new IPEndPoint(
                    IPAddress.Parse(tbxLocalIpAddress.Text), Int32.Parse(tbxPort.Text)));
                tbxReceivedData.Text = "서버와 연결되었습니다.\r\n";
                ButtonStatusChange();

                ThreadStart threadDelegate = new ThreadStart(ReceiveDataThread);
                Thread playThread = new Thread(threadDelegate);
                playThread.Start();
                
            }
            catch (Exception except)
            {
                MessageBox.Show("Local IP Address 또는 Port 번호가 올바르지 않습니다.");
            }         
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] sendData = Encoding.UTF8.GetBytes(tbxSendData.Text);
                _connectSocket.Send(BitConverter.GetBytes(sendData.Length), 0, 4, 0);
                _connectSocket.Send(sendData);
                tbxSendData.Text = "";
            }
            catch { }
            
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_connectSocket != null)
                {
                    _connectSocket.Close();
                    _connectSocket.Dispose();
                }
            }
            catch { }
        }

        public void ReceiveDataThread()
        {
            while (true)
            {
                try
                {
                    byte[] bufferSize = new byte[4];
                    _connectSocket.Receive(bufferSize, 0, bufferSize.Length, 0);
                    int remainSize = BitConverter.ToInt32(bufferSize, 0);

                    MemoryStream recordMemory = new MemoryStream();
                    while (remainSize > 0)
                    {
                        byte[] buffer;
                        if (remainSize < _connectSocket.ReceiveBufferSize)
                            buffer = new byte[remainSize];
                        else
                            buffer = new byte[_connectSocket.ReceiveBufferSize];

                        int usedSize = _connectSocket.Receive(buffer, 0, buffer.Length, 0);

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
                catch (Exception e)
                {
                    MessageBox.Show("서버와의 연결이 종료되었습니다.\r\n");
                    _connectSocket.Close();
                    try //사용 중 Windows Form을 닫을 경우, 바꿀 버튼이 없어서 에러가 발생
                    {
                        this.Invoke(new DeligateButtonChange(ButtonStatusChange));
                        //this.Invoke(new DeligateDisconnectMessgae(DisconnectMessgae));
                    }
                    catch { }
                    break;
                }
            }
        }
    }
}
