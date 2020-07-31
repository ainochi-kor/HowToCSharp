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

namespace ChattingSystem_Server
{
    public partial class ServerForm : Form
    {
        private Socket Listener;
        private Socket Accept;
        private static string Received_data = null;
        private static string ClientIP = null;

        delegate void DeligateGetClientIP(string IPAddress);
        delegate void DeligateButtonChange();

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
            DisConnect(Accept, Listener);
        }

        private void GetClientIP(string ClientEndPoint)
        {
            ClientIP = Accept.RemoteEndPoint.ToString();
            ReceivedData_TextBox.Text += ClientIP + "\r\n";
            ButtonStatusChange();
        }

        
        protected void isTextNull(string LocalIPAddree, string Port)
        {
            if (LocalIPAddree == null || Port == null)
                
        }
        
        
        private void StartButton_Click(object sender, EventArgs e)
        {
            
            //byte[] bytes = new byte[1024];
            isTextNull(LocalIpAddress_textBox.Text, Port_textBox.Text);
          
            IPAddress ServerIPAddress = IPAddress.Parse(LocalIpAddress_textBox.Text.Trim());
            IPEndPoint ServerEndPoint = new IPEndPoint(ServerIPAddress, Int32.Parse(Port_textBox.Text));
            Listener = socket();
            Listener.Bind(ServerEndPoint);
            Listener.Listen(10);

            Received_data = null;

            new Thread(() =>
            {
                Accept = Listener.Accept();
                Listener.Close();
                this.Invoke(new DeligateGetClientIP(GetClientIP), Accept.RemoteEndPoint.ToString());
                while (true)
                {
                    try
                    {
                        byte[] BufferSize = new byte[4];
                        Accept.Receive(BufferSize, 0, BufferSize.Length, 0);
                        int RemainSize = BitConverter.ToInt32(BufferSize, 0);

                        MemoryStream RecordMemory = new MemoryStream();
                        while (RemainSize > 0)
                        {
                            byte[] buffer;
                            if (RemainSize < Accept.ReceiveBufferSize)
                                buffer = new byte[RemainSize];
                            else
                                buffer = new byte[Accept.ReceiveBufferSize];

                            int usedSize = Accept.Receive(buffer, 0, buffer.Length, 0);

                            RemainSize -= usedSize;
                            RecordMemory.Write(buffer, 0, buffer.Length);
                        }
                        RecordMemory.Close();
                        byte[] RecodeData = RecordMemory.ToArray();
                        RecordMemory.Dispose();
                        Invoke((MethodInvoker)delegate
                        {
                            ReceivedData_TextBox.Text += Encoding.UTF8.GetString(RecodeData) +"\r\n";
                        });
                    }
                    catch (SocketException se)
                    {
                        MessageBox.Show("서버 : DISCONNECTION!\r\n" + se.ToString());
                        this.Invoke(new DeligateButtonChange(ButtonStatusChange));
                        Accept.Close();
                        Accept.Dispose();
                        break;
                    }
                }
            }).Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            DisConnect(Accept, Listener);
            ReceivedData_TextBox.Text += ClientIP + " 와의 연결이 끊어졌습니다.";
            StartButton.Enabled = true;
            StopButton.Enabled = false;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] sendData = Encoding.UTF8.GetBytes(sendData_textBox.Text);
                Accept.Send(sendData, 0, sendData.Length, 0);
            }
            catch(SocketException socketExcept)
            {
                MessageBox.Show("텍스트 전송이 불가능합니다 \r\n" + socketExcept.ToString());
            }
            sendData_textBox.Text = "";
        }
        /*
        private Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public static string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
         */
    }
}
