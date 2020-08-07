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
        //Socket _connectSocket;
        TcpClient _clientSocket;
        NetworkStream _stream = default(NetworkStream);
        string _message = string.Empty;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

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

                for (int i = 65; i < 91; i++)
                {
                    cbxChannel.Items.Add(Convert.ToChar(i));
                }
            }
            catch { }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //_connectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {   
                _clientSocket = new TcpClient();
                tbxLocalIpAddress.Text = 
                    Regex.Replace(tbxLocalIpAddress.Text, @"[^0-9].[^0-9].[^0-9].[^0-9]", "");
                tbxPort.Text = Regex.Replace(tbxPort.Text, @"[^0-9]", "");

                if (tbxLocalIpAddress.Text.Replace(".","") == "" || tbxPort.Text == "")
                {
                    throw new Exception();
                }

                _clientSocket.Connect(tbxLocalIpAddress.Text, Int32.Parse(tbxPort.Text));
                _stream = _clientSocket.GetStream();
                Message = "Connected to Chat Server";
                DisplayText(Message);
                //rtbxReceivedData.Text = "서버와 연결되었습니다.\r\n";
                ButtonStatusChange();

                byte[] channelBuffer = Encoding.Unicode.GetBytes(this.cbxChannel.Text + "$");
                _stream.Write(channelBuffer, 0, channelBuffer.Length);
                _stream.Flush();
                MessageBox.Show("채널 전송완료");
                Thread threadHander = new Thread(GetMessage);
                threadHander.IsBackground = true;
                threadHander.Start();
                
            }
            catch (SocketException se)
            {
                MessageBox.Show("Local IP Address 또는 Port 번호가 올바르지 않습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("문제의 내용은 아래와 같습니다.\r\n" + ex.ToString());
            }
        }

        private void GetMessage()
        {
            try
            {
                while (true)
                {
                    _stream = _clientSocket.GetStream();
                    int bufferSize = _clientSocket.ReceiveBufferSize;
                    byte[] buffer = new byte[bufferSize];
                    int bytes = _stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.Unicode.GetString(buffer, 0, bytes);
                    if(message.Trim() != "")
                        DisplayText(message);
                }
            }
            catch 
            {
                this.Invoke(new DeligateButtonChange(ButtonStatusChange));
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] buffer = Encoding.Unicode.GetBytes(this.tbxSendData.Text + "$");
                _stream.Write(buffer, 0, buffer.Length);
                _stream.Flush();
                tbxSendData.Text = "";
            }
            catch { }
            
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _clientSocket.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DisplayText(string text)
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
    }
}
