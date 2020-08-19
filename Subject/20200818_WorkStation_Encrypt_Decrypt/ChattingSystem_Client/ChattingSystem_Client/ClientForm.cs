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
    public partial class ClientForm : Form
    {
        #region CONST & FIELD AREA ********************************************

        private TcpClient _clientSocket;
        private NetworkStream _stream = default(NetworkStream);
        private string _message = string.Empty;
        private string _clientIP = "";
        private string _clientPort = "";
        private bool _isReceivedInfo = false;
        SecurityClient _securityClient = new SecurityClient();

        #endregion CONST & FIELD AREA *****************************************
        
        #region PROPERTY AREA *************************************************

        public TcpClient ClientSocket
        {
            get { return _clientSocket; }
            set { _clientSocket = value; }
        }

        public NetworkStream Stream
        {
            get { return _stream; }
            set { _stream = value; }
        }

        public string ClientIP
        {
            get { return _clientIP; }
            set { _clientIP = value; }
        }
        public string ClientPort
        {
            get { return _clientPort; }
            set { _clientPort = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public bool IsReceivedInfo
        {
            get { return _isReceivedInfo; }
            set { _isReceivedInfo = value; }
        }

        #endregion PROPERTY AREA *************************************************

        #region DELEGATE AREA *************************************************

        delegate void DelegateButtonChange();
        delegate void DelegateDisconnect();
        delegate void DelegateSecurity(string message);

        #endregion DELEGATE AREA **********************************************

        #region FORM CONTROL AREA *********************************************

        public ClientForm()
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
                cbxChannel.Text = "A";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectSocket();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                ClientSocket = new TcpClient();
                tbxLocalIpAddress.Text = 
                    Regex.Replace(tbxLocalIpAddress.Text, @"[^0-9].[^0-9].[^0-9].[^0-9]", "");
                tbxPort.Text = Regex.Replace(tbxPort.Text, @"[^0-9]", "");

                if (tbxLocalIpAddress.Text.Replace(".","") == "" || tbxPort.Text == "")
                {
                    throw new Exception();
                }

                ClientSocket.Connect(tbxLocalIpAddress.Text, Int32.Parse(tbxPort.Text));
                Stream = ClientSocket.GetStream();

                Message = "서버와 연결되었습니다.";
                DisplayText(Message);
                ButtonStatusChange();

                byte[] data = Encoding.UTF8.GetBytes(this.cbxChannel.Text);

                Stream.Write(BitConverter.GetBytes(data.Length), 0, 4);
                Stream.Flush();

                //Thread.Sleep(1000);
                
                Stream.Write(data, 0, data.Length);
                Stream.Flush();
                
                Thread threadHander = new Thread(GetMessage);
                threadHander.IsBackground = true;
                threadHander.Start();
                //MessageBox.Show("다보냄");
            }
            catch (SocketException se)
            {
                MessageBox.Show("Local IP Address 또는 Port 번호가 올바르지 않습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {

                string encryptedMessage = _securityClient.EncryptedMessage(tbxLocalIpAddress.Text + ":" + tbxPort.Text + "/" + cbxChannel.Text,
                    ClientIP + ":" + ClientPort + "/" + this.tbxSendData.Text);

                byte[] message = Encoding.UTF8.GetBytes(encryptedMessage);

                Stream.Write(BitConverter.GetBytes(message.Length), 0, 4);
                Stream.Flush();

                Stream.Write(message, 0, message.Length);
                Stream.Flush();
                tbxSendData.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DisconnectSocket();
        }

        #endregion FORM CONTROL AREA ******************************************

        #region METHOD AREA ***************************************************

        public void ButtonStatusChange()
        {
            try
            {
                btnConnect.Enabled = !(btnConnect.Enabled);
                btnDisconnect.Enabled = !(btnDisconnect.Enabled);

                if (btnConnect.Enabled)
                    DisplayText(tbxLocalIpAddress.Text + " 와의 연결이 끊어졌습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetMessage()
        {
            try
            {
                while (true)
                {
                    Stream = ClientSocket.GetStream();
                    int bufferSize = ClientSocket.ReceiveBufferSize;
                    byte[] buffer = new byte[bufferSize];
                    int bytes = Stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.Unicode.GetString(buffer, 0, bytes);
                    
                    if (message.IndexOf('@') == 0)
                    {
                        MessageBox.Show(message.Substring(1));
                        message = _securityClient.DecryptedMessage(message.Substring(1), LocalIPAddress());
                        MessageBox.Show(message);
                        if (!IsReceivedInfo)
                        {
                            ClientIP = message.Split(':')[0];
                            ClientPort = message.Split(':')[1].Split('/')[0];
                            IsReceivedInfo = !IsReceivedInfo;
                            DisplayText(message);
                        }
                    }
                    else
                    {
                        this.Invoke(new DelegateSecurity(DecryptedMessage), message);
                    }
                        
                    if(message == "")
                    {
                        ClientSocket.Close();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                try
                {
                    this.Invoke(new DelegateDisconnect(DisconnectSocket));
                    this.Invoke(new DelegateButtonChange(ButtonStatusChange));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                MessageBox.Show(ex.ToString());
                IsReceivedInfo = false;
            }
        }

        private void DecryptedMessage(string message)
        {
            message = _securityClient.DecryptedMessage(tbxLocalIpAddress.Text + ":" + tbxPort.Text + "/" + cbxChannel.Text, message);
            DisplayText(message);
        }

        private void DisconnectSocket()
        {
            ClientSocket.Close();
            /*
             * 생각해야하는 부분.
            byte[] data = Encoding.Unicode.GetBytes(ClientIP + ":" + ClientPort + "/" + " 연결을 해제합니다." + "$@");
            
            try
            {
                byte[] buffer = Encoding.Unicode.GetBytes(ClientIP + ":" + ClientPort + "/" + " 연결을 해제합니다." + "$@");
                Stream.Write(buffer, 0, buffer.Length);
                Stream.Flush();
                ClientSocket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          * */
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
                        rtbxReceivedData.Select(rtbxReceivedData.Text.Length, 0);
                    }));
                }
                else
                    rtbxReceivedData.AppendText(text + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public string LocalIPAddress()
        {
            IPHostEntry host;
            string LocalIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {

                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    LocalIP = ip.ToString();
                    break;
                }
            }
            return LocalIP;
        }
        #endregion METHOD AREA ************************************************
    }
}
