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

        delegate void DeligateGetClientIP(string IPAddress);
        delegate void DeligateButtonChange();
        delegate void DeligateDisconnectMessgae();

        public ServerForm()
        { 
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            //폼 로드시, 현재 컴퓨터의 LocalIPAddress를 TextBox에 출력합니다.
            LocalIpAddress_textBox.Text = LocalIPAddress();
            StartButton.Enabled = true;
            StopButton.Enabled = false;
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect(_acceptSocket, _connectSocket);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            //ButtonStatusChange();
            ReceivedData_TextBox.Text = "";
            try
            {   
                LocalIpAddress_textBox.Text =
                    Regex.Replace(LocalIpAddress_textBox.Text, @"[^0-9].[^0-9].[^0-9].[^0-9]", "");
                Port_textBox.Text = Regex.Replace(Port_textBox.Text, @"[^0-9]", "");
                if (LocalIpAddress_textBox.Text == "" || Port_textBox.Text == "")
                {
                    MessageBox.Show("Local IP Address가 올바르지 않습니다.");
                    //ButtonStatusChange();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Local IP Address 또는 Port 번호가 올바르지 않습니다.");
                return;
            }
            IPAddress serverIPAddress = IPAddress.Parse(LocalIpAddress_textBox.Text.Trim());
            IPEndPoint serverEndPoint = new IPEndPoint(serverIPAddress, Int32.Parse(Port_textBox.Text));
            _connectSocket = SetupSocket();
            _connectSocket.Bind(serverEndPoint);
            _connectSocket.Listen(1);
            
            PlayThread();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Disconnect(_acceptSocket, _connectSocket);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (sendData_textBox.Text == "") return;

            try
            {
                byte[] sendData = Encoding.UTF8.GetBytes(sendData_textBox.Text);
                _acceptSocket.Send(BitConverter.GetBytes(sendData.Length), 0, 4, 0);
                _acceptSocket.Send(sendData);
            }
            catch(SocketException socketExcept)
            {
                MessageBox.Show("텍스트 전송이 불가능합니다 \r\n" + socketExcept.ToString());
            }
            sendData_textBox.Text = "";
        }

        
        private void PlayThread()
        {
            new Thread(() =>
            {
                try
                {
                    _acceptSocket = _connectSocket.Accept();
                }
                catch 
                { 
                    ShutdownThread();
                    return; 
                }
                _connectSocket.Close();
                this.Invoke(new DeligateGetClientIP(GetClientIP), _acceptSocket.RemoteEndPoint.ToString());
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
                            ReceivedData_TextBox.Text += Encoding.UTF8.GetString(recodeData) + "\r\n";
                        });
                    }
                    catch (SocketException se)
                    {
                        ShutdownThread();
                        break;
                    }
                }
            }).Start();
        }

    }
}
