using System;
using System.Collections;
using System.Collections.Generic;
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
    public partial class ServerForm
    {

        string _sendClient = "";
        string _sendmessage = "";
        ArrayList _ClientInfoList = new ArrayList();

        public string SendClient
        {
            get { return _sendClient; }
            set { _sendClient = value; }
        }
        public string Sendmessage
        {
            get { return _sendmessage; }
            set { _sendmessage = value; }
        }

        private void GetClientIP()
        {
            try
            {
                IPEndPoint ipPoint = (IPEndPoint)TcpClient.Client.RemoteEndPoint;
                ClientIP = ipPoint.Address.ToString();
                rtbxReceivedData.Text += ipPoint.Address + ":" + ipPoint.Port + "님이 연결되었습니다.\r\n";
            }
            catch (Exception ex)
            {
                //MessageBox.Show("GetClientIP : \r\n" + ex.ToString());
            }
        }

        public void ButtonStatusChange()
        {
            try
            {
                btnStart.Enabled = !(btnStart.Enabled);
                btnStop.Enabled = !(btnStop.Enabled);
                
                        
                if (btnStart.Enabled == true && ClientIP != null)
                {
                    foreach(string clientInfo in _ClientInfoList )
                        rtbxReceivedData.Text += clientInfo + " 와의 연결이 끊어졌습니다.\r\n";
                    _ClientInfoList.Clear();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("버튼스테이터스체인지 에러.\r\n" + ex.ToString());
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
                //MessageBox.Show("Disconnect : \r\n" + ex.ToString());
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
                    MessageBox.Show("Local IP Address 또는 Port 번호가 올바르지 않습니다.");
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
                        TcpClient = TcpListner.AcceptTcpClient();

                        this.Invoke(new DeligateGetClientIP(GetClientIP));

                        NetworkStream stream = TcpClient.GetStream();
                        byte[] buffer = new byte[1024];
                        int bytes = stream.Read(buffer, 0, buffer.Length);
                        string channel = Encoding.Unicode.GetString(buffer, 0, bytes);
                        channel = channel.Split('$')[0];

                        clientList.Add(TcpClient, channel);

                        ipPoint = (IPEndPoint)_tcpClient.Client.RemoteEndPoint;
                        ClientIP = ipPoint.Address.ToString();
                        _ClientInfoList.Add(ClientIP + ":" + ipPoint.Port.ToString());
                        SendMessageAll(ClientIP + ":" + ipPoint.Port + "/", channel, false);

                        HandleClient handle = new HandleClient();
                        handle.OnReceived += new HandleClient.MessageDisplayHandler(OnReceived);
                        handle.OnDisconnected += new HandleClient.DisconnectedHandler(HandlerClientsOnDisconnected);
                        handle.StartClient(TcpClient, clientList);

                    }
                    catch
                    {
                        throw new SocketException();
                    }
                }
                
            }
            catch(SocketException ex)
            {
                this.Invoke(new DeligateDisconnect(Disconnect));
            }
            catch(Exception ex)
            { }
            finally
            {
                this.Invoke(new DeligateButtonChange(ButtonStatusChange));
            }
        }

        private void OnReceived(string message, string channel)
        {
            try
            {
                SendClient = message.Split('/')[0];
                Sendmessage = message.Substring(message.IndexOf("/") + 1);
                string displayMessage = SendClient + "/" + channel + ">" + Sendmessage;
                DisplayText(displayMessage);
                SendMessageAll(message, channel, true);

                if (Sendmessage == " 연결을 해제합니다.")
                    _ClientInfoList.Remove(SendClient);
            }
            catch(Exception ex)
            {
                //MessageBox.Show("OnReceived : \r\n" + ex.ToString());
            }
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
               //MessageBox.Show("HandlerClientsOnDisconnected : \r\n" + ex.ToString());
            }
        }



        public void SendMessageAll(string message, string channel, bool flag)
        {
            try
            {
                foreach (var pair in clientList)
                {

                    TcpClient client = pair.Key as TcpClient;
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = null;

                    if (channel == pair.Value)
                    {
                        SendClient = message.Split('/')[0];
                        Sendmessage = message.Substring(message.IndexOf("/") + 1);
                        buffer = Encoding.Unicode.GetBytes(SendClient + "/" + channel + ">" + Sendmessage);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();
                    }
                    else if (channel == "Server")
                    {
                        buffer = Encoding.Unicode.GetBytes(channel + ">" + message);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();
                        DisplayText(channel + ">" + message);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("[서버]에서 송신 할 수 없습니다. \r\n" + ex.ToString());
            }
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
                    }));
                }
                else
                    rtbxReceivedData.AppendText(text + Environment.NewLine);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("DisplayText : \r\n" + ex.ToString());
            }
        }
    }
}
