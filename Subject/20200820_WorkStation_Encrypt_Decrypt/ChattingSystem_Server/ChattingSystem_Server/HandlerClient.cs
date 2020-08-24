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
using System.Windows.Forms;

namespace ChattingSystem_Server
{
    class HandleClient : ServerForm
    {
        #region CONST & FIELD AREA ********************************************

        TcpClient _tcpSocket = null;
        Thread _threadHandler = null;
        IPEndPoint _ipEndPoint = null;//(IPEndPoint)TcpSocket.Client.RemoteEndPoint;
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

        public IPEndPoint IpEndPoint
        {
            get { return _ipEndPoint; }
            set { _ipEndPoint = value; }
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
                IpEndPoint = (IPEndPoint)TcpSocket.Client.RemoteEndPoint;

                ThreadHandler = new Thread(DoChat);
                ThreadHandler.IsBackground = true;
                ThreadHandler.Start();
            }
            catch (Exception ex)
            {
                serverEvent.ErrorLog("StartClient", ex.Message);
            }
            
        }

        private void DoChat()
        {
            NetworkStream stream = null;
            try
            {
                string message = string.Empty;

                while (true)
                {
                    stream = TcpSocket.GetStream();

                    byte[] sizeBuf = new byte[TcpSocket.ReceiveBufferSize];
                    stream.Read(sizeBuf, 0, (int)TcpSocket.ReceiveBufferSize);
                    int size = BitConverter.ToInt32(sizeBuf, 0);

                    MemoryStream memoryStream = new MemoryStream();


                    while (size > 0)
                    {
                        byte[] buffer;
                        if (size < TcpSocket.ReceiveBufferSize) 
                            buffer = new byte[size];
                        else
                            buffer = new byte[TcpSocket.ReceiveBufferSize];

                        int rec = stream.Read(buffer, 0, buffer.Length);

                        size -= rec;
                        memoryStream.Write(buffer, 0, buffer.Length);
                    }
                    memoryStream.Close();

                    byte[] data = memoryStream.ToArray();

                    memoryStream.Dispose();

                    message = Encoding.UTF8.GetString(data);
                    if (message == "")
                        throw new Exception("공백 출력 == 상대방 소켓 끊어짐");
                    serverEvent.ReceiveServerLog(message);
                    OnReceived(message, ClientList[TcpSocket].ToString(), false);
                }
            }
            catch (Exception ex)
            {
                serverEvent.ErrorLog("DoChat", ex.Message);
                if (TcpSocket != null)
                {
                    OnDisconnected(TcpSocket);
                    TcpSocket.Close();
                    stream.Close();
                    if (ThreadHandler.IsAlive == true)
                    {
                        ThreadHandler.Interrupt();
                        ThreadHandler.Abort();
                    }
                }
            }
        }

        #endregion METHOD AREA ***************************************************
    }
}
