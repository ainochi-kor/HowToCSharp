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

        public ServerForm()
        {
            InitializeComponent();
        }

        private void server_form_Load(object sender, EventArgs e)
        {
            //폼 로드시, 현재 컴퓨터의 LocalIPAddress를 TextBox에 출력합니다.
            LocalIpAddress_textBox.Text = LocalIPAddress();
        }

        private void server_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisConnect(Accept, Listener);
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            //byte[] bytes = new byte[1024];
            IPAddress ServerIPAddress = IPAddress.Parse(LocalIpAddress_textBox.Text.Trim());
            IPEndPoint ServerEndPoint = new IPEndPoint(ServerIPAddress, Int32.Parse(Port_textBox.Text));
            Listener = socket();
            Listener.Bind(ServerEndPoint);
            Listener.Listen(10);

            new Thread(() =>
            {
                while (true)
                {
                    MessageBox.Show("while");
                    Accept = Listener.Accept();
                    Listener.Close();

                    Received_data = null;
                    try
                    {
                        MessageBox.Show("try");
                        byte[] BufferSize = new byte[4];
                        Accept.Receive(BufferSize, 0, BufferSize.Length, 0);
                        int RemainSize = BitConverter.ToInt32(BufferSize, 0);

                        MemoryStream RecordMemory = new MemoryStream();
                        while (RemainSize > 0)
                        {
                            MessageBox.Show("while2");
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
                        MessageBox.Show("Invoke");
                        Invoke((MethodInvoker)delegate
                        {
                            ReceivedData_TextBox.Text = Encoding.UTF8.GetString(RecodeData);
                        });
                    }
                    catch (SocketException se)
                    {
                        MessageBox.Show("서버 : DISCONNECTION!\r\n" + se.ToString());
                        Accept.Close();
                        Accept.Dispose();
                        break;
                    }
                }
            }).Start();        
        }

        private void Stop_button_Click(object sender, EventArgs e)
        {
            DisConnect(Accept, Listener);
            ReceivedData_TextBox.Text += ClientIP + " 와의 연결이 끊어졌습니다.";
        }

        private void send_button_Click(object sender, EventArgs e)
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
