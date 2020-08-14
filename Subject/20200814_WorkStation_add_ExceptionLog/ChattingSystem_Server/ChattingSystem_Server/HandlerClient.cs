using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using log4net;
using log4net.Config;

namespace ChattingSystem_Server
{
    class HandleClient
    {
        #region CONST & FIELD AREA ********************************************

        TcpClient _tcpSocket = null;
        Thread _threadHandler = null;
        private Dictionary<TcpClient, string> _clientList = null;
        ServerEvent serverEvent = new ServerEvent();

        #endregion CONST & FIELD AREA ********************************************

        #region PROPERTY AREA *************************************************

        public TcpClient TcpSocket
        {
            get { return _tcpSocket; }
            set { _tcpSocket = value; }
        }

        public Thread ThreadHandler
        {
            get { return _threadHandler; }
            set { _threadHandler = value; }
        }

        public Dictionary<TcpClient, string> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; }
        }

        #endregion PROPERTY AREA *************************************************

        #region DELEGATE & EVENT AREA *****************************************

        public delegate void MessageDisplayHandler(string message, string channel, bool isClientClose);
        public event MessageDisplayHandler OnReceived;

        public delegate void DisconnectedHandler(TcpClient clientSocket);
        public event DisconnectedHandler OnDisconnected;

        #endregion DELEGATE & EVENT AREA *****************************************

        #region METHOD AREA ***************************************************

        public void StartClient(TcpClient clientSocket, Dictionary<TcpClient, string> clientList)
        {
            try
            {
                TcpSocket = clientSocket;
                this.ClientList = clientList;

                ThreadHandler = new Thread(DoChat);
                ThreadHandler.IsBackground = true;
                ThreadHandler.Start();
            }
            catch (Exception ex)
            {
                serverEvent.ErrorLog("StartClient", ex.ToString());
            }
            
        }

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
                    stream = TcpSocket.GetStream();
                    bytes = stream.Read(buffer, 0, buffer.Length);
                    message = Encoding.Unicode.GetString(buffer, 0, bytes);
                    serverEvent.ReceiveServerLog(message); //받은 메시지 원문 기록
                    if (OnReceived != null)
                    {
                        if (message.Substring(message.LastIndexOf("$") + 1) == "@")
                            OnReceived(message.Substring(0, message.LastIndexOf("$")), ClientList[TcpSocket].ToString(), true);
                        else
                            OnReceived(message.Substring(0, message.LastIndexOf("$")), ClientList[TcpSocket].ToString(), false);
                    }
                }
            }
            catch (Exception ex)
            {
                serverEvent.ErrorLog("DoChat", ex.ToString());
                if (TcpSocket != null)
                {
                    IPEndPoint clientInfo = (IPEndPoint)TcpSocket.Client.RemoteEndPoint;
                    OnReceived(clientInfo.Address + ":" + clientInfo.Port + "/" + " 연결을 해제합니다.", ClientList[TcpSocket].ToString(), true);

                    TcpSocket.Close();
                    stream.Close();
                    if (ThreadHandler.IsAlive == true)
                    {
                        ThreadHandler.Interrupt();
                        ThreadHandler.Abort();
                    }
                    if (OnDisconnected != null)
                        OnDisconnected(TcpSocket);
                }
            }
        }

        #endregion METHOD AREA ***************************************************
    }
}
