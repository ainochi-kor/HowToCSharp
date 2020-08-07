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
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace MultiThread_Server
{
    public partial class ServerForm : Form
    {
        TcpListener server = null;
        TcpClient clientSocket = null;
        static int counter = 0;

        public Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();

        public ServerForm()
        {
            InitializeComponent();

            Thread t = new Thread(InitSocket);
            t.IsBackground = true;
            t.Start();
        }

        private void InitSocket()
        {
            server = new TcpListener(IPAddress.Any, 9999);
            clientSocket = default(TcpClient);
            server.Start();
            DisplayText(">> server Started");

            while(true)
            {
                try
                {
                    counter++;
                    clientSocket = server.AcceptTcpClient();
                    DisplayText(">> Accept connection from client");

                    NetworkStream stream = clientSocket.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string user_name = Encoding.Unicode.GetString(buffer, 0, bytes);
                    user_name = user_name.Substring(0, user_name.IndexOf("$"));

                    clientList.Add(clientSocket, user_name);

                    SendMessageAll(user_name + "Joined " , "", false);

                    handleClientcs h_client = new handleClientcs();
                    h_client.OnReceived += new handleClientcs.MessageDisplayHandler(OnReceived);
                    h_client.OnDisconnected += new handleClientcs.DisconnectedHandler(h_client_OnDisconnected);
                    h_client.startClient(clientSocket, clientList);
                }
                catch(SocketException se)
                {
                    Trace.WriteLine(string.Format("InitSocket - SocketException : {0}", se.Message));
                    break;
                }
                catch(Exception ex)
                {
                    Trace.WriteLine(string.Format("InitSocket - Exception : {0}", ex.Message));
                    break;
                }
            }

            clientSocket.Close();
            server.Stop();
        }

        void h_client_OnDisconnected(TcpClient clientScoket)
        {
            if (clientList.ContainsKey(clientScoket))
                clientList.Remove(clientScoket);
        }

        private void OnReceived(string message, string user_name)
        {
            string displayMessage = "From client: " + user_name + " : " + message;
            DisplayText(displayMessage);
            SendMessageAll(message, user_name, true);
        }

        public void SendMessageAll(string message, string user_name, bool flag)
        {
            foreach(var pair in clientList)
            {
                Trace.WriteLine(string.Format("tcpclient : {0} user_name : {1}", pair.Key, pair.Value));

                TcpClient client = pair.Key as TcpClient;
                NetworkStream stream = client.GetStream();
                byte[] buffer = null;

                IPEndPoint ipPoint = (IPEndPoint)clientSocket.Client.RemoteEndPoint;
                string clientIP = ipPoint.Address.ToString();

                if(flag)
                {
                    buffer = Encoding.Unicode.GetBytes( clientIP + ipPoint.Port.ToString()+"/"+ user_name + " says: " + message);
                }
                else
                {
                    buffer = Encoding.Unicode.GetBytes(message);
                }

                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
        }

        private void DisplayText(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.BeginInvoke(new MethodInvoker(delegate
                    {
                        richTextBox1.AppendText(text + Environment.NewLine);
                    }));
            }
            else
                richTextBox1.AppendText(text + Environment.NewLine);
        }
    }
}
