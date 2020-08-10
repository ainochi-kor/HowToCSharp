using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ChattingSystem_Server
{
    class HandleClient
    {
        TcpClient _tcpSocket = null;
        Thread _threadHandler = null;
        public Dictionary<TcpClient, string> _clientList = null;

        public void StartClient(TcpClient clientSocket, Dictionary<TcpClient, string> clientList)
        {
            try
            {
                _tcpSocket = clientSocket;
                this._clientList = clientList;

                _threadHandler = new Thread(DoChat);
                _threadHandler.IsBackground = true;
                _threadHandler.Start();
            }
            catch { }
            
        }

        public delegate void MessageDisplayHandler(string message, string channel);
        public event MessageDisplayHandler OnReceived;

        public delegate void DisconnectedHandler(TcpClient clientSocket);
        public event DisconnectedHandler OnDisconnected;

        private void DoChat()
        {
            NetworkStream stream = null;
            try
            {
                byte[] buffer = new byte[1024];
                string message = string.Empty;
                int bytes = 0;

                while (true)
                {
                    stream = _tcpSocket.GetStream();
                    bytes = stream.Read(buffer, 0, buffer.Length);
                    message = Encoding.Unicode.GetString(buffer, 0, bytes);
                    message = message.Substring(0, message.LastIndexOf("$"));
                    if (OnReceived != null)
                        OnReceived(message, _clientList[_tcpSocket].ToString());
                }
            }
            catch (Exception ex)
            {
                if (_tcpSocket != null)
                {
                    if (OnDisconnected != null)
                        OnDisconnected(_tcpSocket);

                    if (_threadHandler.IsAlive == true)
                    {
                        _threadHandler.Interrupt();
                        _threadHandler.Abort();
                    }

                    _tcpSocket.Close();
                    stream.Close();
                }
            }
        }
    }
}
