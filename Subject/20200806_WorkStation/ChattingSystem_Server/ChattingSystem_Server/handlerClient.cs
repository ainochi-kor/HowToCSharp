using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
//using System.Diagnostics;

namespace ChattingSystem_Server
{
    class handleClient
    {
        TcpClient _tcpSocket = null;
        public Dictionary<TcpClient, string> clientList = null;

        public void startClient(TcpClient clientSocket, Dictionary<TcpClient, string> clientList)
        {
            _tcpSocket = clientSocket;
            this.clientList = clientList;

            Thread threadHandler = new Thread(DoChat);
            threadHandler.IsBackground = true;
            threadHandler.Start();
        }

        public delegate void MessageDisplayHandler(string message, string user_name);
        public event MessageDisplayHandler OnReceived;

        public delegate void DisconnectedHandler(TcpClient clientSocket);
        public event DisconnectedHandler OnDisconnected;

        private void DoChat()
        {
            NetworkStream stream = null;
            try
            {
                byte[] buffer = new byte[1024];
                string msg = string.Empty;
                int bytes = 0;
                int MessageCount = 0;

                while (true)
                {
                    MessageCount++;
                    stream = _tcpSocket.GetStream();
                    bytes = stream.Read(buffer, 0, buffer.Length);
                    msg = Encoding.Unicode.GetString(buffer, 0, bytes);
                    msg = msg.Substring(0, msg.IndexOf("$"));

                    if (OnReceived != null)
                        OnReceived(msg, clientList[_tcpSocket].ToString());
                }
            }
            catch (SocketException se)
            {
                //Trace.WriteLine(string.Format("doChat - SocketException : {0}", se.Message));
                if (_tcpSocket != null)
                {
                    if (OnDisconnected != null)
                        OnDisconnected(_tcpSocket);

                    _tcpSocket.Close();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Trace.WriteLine(string.Format("doChat - Exception : {0}", ex.Message));
                if (_tcpSocket != null)
                {
                    if (OnDisconnected != null)
                        OnDisconnected(_tcpSocket);
                    _tcpSocket.Close();
                    stream.Close();
                }
            }
        }

    }
}
