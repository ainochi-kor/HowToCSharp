using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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

                if (btnStart.Enabled == true && ClientIP != null)
                    rtbxReceivedData.Text += ClientIP + " 와의 연결이 끊어졌습니다...";
            }
            catch (Exception ex)
            {
                MessageBox.Show("버튼스테이터스체인지 에러.\r\n" + ex.ToString());
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

                        HandleClient handle = new HandleClient();
                        handle.OnReceived += new HandleClient.MessageDisplayHandler(OnReceived);
                        handle.OnDisconnected += new HandleClient.DisconnectedHandler(HandlerClientsOnDisconnected);
                        handle.StartClient(TcpClient, clientList);

                    }
                    catch (Exception ex)
                    {
                        TcpClient.Close();
                        TcpListner.Stop();
                        MessageBox.Show(ex.ToString());
                        throw new Exception();
                    }
                }
                
            }
            catch
            {
                MessageBox.Show("Local IP Address 또는 Port 번호가 올바르지 않습니다.");
                this.Invoke(new DeligateDisconnect(Disconnect));
                this.Invoke(new DeligateButtonChange(ButtonStatusChange));
            }
        }

        private void OnReceived(string message, string user_name)
        {
            try
            {
                string sendClient = message.Split('/')[0];
                string sendmessage = message.Split('/')[1];
                string displayMessage = sendClient + "/" + user_name + ">" + sendmessage;
                DisplayText(displayMessage);
                SendMessageAll(message, user_name, true);
            }
            catch(Exception ex)
            {
                MessageBox.Show("OnReceived : \r\n" + ex.ToString());
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
                MessageBox.Show("HandlerClientsOnDisconnected : \r\n" + ex.ToString());
            }
        }



        public void SendMessageAll(string message, string user_name, bool flag)
        {
            try
            {
                foreach (var pair in clientList)
                {
                    Trace.WriteLine(string.Format("tcpclient : {0} user_name : {1}", pair.Key, pair.Value));

                    TcpClient client = pair.Key as TcpClient;
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = null;

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
                MessageBox.Show("DisplayText : \r\n" + ex.ToString());
            }
        }
    }
}
