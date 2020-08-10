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
        ServerEvent serverEvent = new ServerEvent();

        private TcpListener _tcpListner = null;
        private TcpClient _tcpClient = null;
        private static string _clientIP = null;
        static int counter = 0;
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
        //delegate void DeligateDisconnectMessgae();
        //delegate void DeligateEndThread();
        

        public ServerForm()
        { 
            InitializeComponent();
        }

        public void ServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                //폼 로드시, 현재 컴퓨터의 LocalIPAddress를 TextBox에 출력합니다.
                tbxLocalIpAddress.Text = serverEvent.LocalIPAddress();
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                MessageBox.Show("btnSend_Click : \r\n" + ex.ToString());  
            }
        }

        public void GetClientIP()
        {
            try
            {
                IPEndPoint ipPoint = (IPEndPoint)TcpClient.Client.RemoteEndPoint;
                ClientIP = ipPoint.Address.ToString();
                rtbxReceivedData.Text += ipPoint.Address + ":" + ipPoint.Port + "님이 연결되었습니다.\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetClientIP : \r\n" + ex.ToString());  
            }
        }

        public void ShutdownThread()
        {
            try
            {   //Form_Closing 시, 바꿀 데이터가 없어서 오류가 발생함.
                Thread thread = Thread.CurrentThread;
                thread.Interrupt();
                TcpClient.Close();
                Thread.Sleep(1000); //Dispose하는 시간이 필요하여 1초정도 메인 쓰레드를 쉬어줌.
            }
            catch (Exception ex)
            {
                MessageBox.Show("ShutdownThread : \r\n" + ex.ToString());  
            }
        }

        public void ButtonStatusChange()
        {
            try
            {
                btnStart.Enabled = !(btnStart.Enabled);
                btnStop.Enabled = !(btnStop.Enabled);

                if(btnStart.Enabled == true && ClientIP != null)
                    rtbxReceivedData.Text += ClientIP + " 와의 연결이 끊어졌습니다...";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ButtonStatusChange : \r\n" + ex.ToString());  
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
                MessageBox.Show("Disconnect : \r\n" + ex.ToString()); 
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
                        counter++;//무슨 기능이 있는지 잘 모르겠음.
                        TcpClient = TcpListner.AcceptTcpClient();

                        this.Invoke(new DeligateGetClientIP(GetClientIP));

                        NetworkStream stream = TcpClient.GetStream();
                        byte[] buffer = new byte[1024];
                        int bytes = stream.Read(buffer, 0, buffer.Length);
                        string channel = Encoding.Unicode.GetString(buffer, 0, bytes);
                        channel = channel.Substring(0, channel.IndexOf("$"));

                        clientList.Add(TcpClient, channel);

                        ipPoint = (IPEndPoint)_tcpClient.Client.RemoteEndPoint;
                        ClientIP = ipPoint.Address.ToString();
                        SendMessageAll(ClientIP + ":" + ipPoint.Port + "/", channel, false);

                        HandleClient h_client = new HandleClient();
                        h_client.OnReceived += new HandleClient.MessageDisplayHandler(OnReceived);
                        h_client.OnDisconnected += new HandleClient.DisconnectedHandler(HandlerClientsOnDisconnected);
                        h_client.startClient(TcpClient, clientList);

                    }
                    catch (SocketException se)
                    {
                        MessageBox.Show(se.ToString());
                        throw new Exception();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw new Exception("");
                    }
                }
                TcpClient.Close();
                TcpListner.Stop();
            }
            catch
            {
                MessageBox.Show("Local IP Address 또는 Port 번호가 올바르지 않습니다.");
                this.Invoke(new DeligateButtonChange(ButtonStatusChange));
            }
        }

        public void OnReceived(string message, string user_name)
        {
            ServerForm server = new ServerForm();

            string sendClient = message.Split('/')[0];
            string sendmessage = message.Split('/')[1];
            string displayMessage = sendClient + "/" + user_name + ">" + sendmessage;
            server.DisplayText(displayMessage);
            server.SendMessageAll(message, user_name, true);
        }

        void HandlerClientsOnDisconnected(TcpClient clientScoket)
        {
            try
            {
                if (clientList.ContainsKey(clientScoket))
                    clientList.Remove(clientScoket);
            }
            catch (Exception ex)
            {
                MessageBox.Show("HandlerClientsOnDisconnected : \r\n" + ex.ToString());
            }
        }

        public void SendMessageAll(string message, string user_name, bool flag)
        {
            MessageBox.Show("Lsasd.");
            try
            {
                foreach (var pair in clientList)
                {
                    Trace.WriteLine(string.Format("tcpclient : {0} user_name : {1}", pair.Key, pair.Value));

                    TcpClient client = pair.Key as TcpClient;
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = null;

                    //MessageBox.Show("message \r\n" + message);
                    if (user_name == pair.Value)
                    {
                        string sendClient = message.Split('/')[0];
                        string sendmessage = message.Split('/')[1];
                        buffer = Encoding.Unicode.GetBytes(sendClient + "/" + user_name + ">" + sendmessage);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();
                    }
                    else if (user_name == "Server")
                    {
                        buffer = Encoding.Unicode.GetBytes(user_name + ">" + message);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();
                        DisplayText(user_name + ">" + message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[서버]에서 송신 할 수 없습니다. \r\n" + ex.ToString());
            }
        }

        public void DisplayText(string text)
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
            catch(Exception ex)
            {
                MessageBox.Show("DisplayText : \r\n" + ex.ToString());
            }          
        }
            
    }
}
