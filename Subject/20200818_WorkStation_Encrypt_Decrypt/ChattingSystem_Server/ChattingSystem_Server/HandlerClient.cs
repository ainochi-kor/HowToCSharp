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
                serverEvent.ErrorLog("StartClient", ex.Message);
            }
            
        }

        private void DoChat()
        {
            NetworkStream stream = null;
            try
            {
                //byte[] buffer = new byte[1024];
                string message = string.Empty;
                int bytes = 0;

                while (true)
                {
                    byte[] sizeBuf = new byte[4];

                    stream = TcpSocket.GetStream();//acc.Receive(sizeBuf, 0, sizeBuf.Length, 0);

                    int size = BitConverter.ToInt32(sizeBuf, 0);

                    //bytes = stream.Read(stream, 0, stream.Length);

                    
                    MemoryStream ms = new MemoryStream();

                     while (size > 0)
                    {
                        //MessageBox.Show(TcpSocket.ReceiveBufferSize.ToString());
                        byte[] buffer;
                        if (size < TcpSocket.ReceiveBufferSize)//acc.ReceiveBufferSize)
                            buffer = new byte[size];
                        else
                            buffer = new byte[TcpSocket.ReceiveBufferSize];

                        int rec = stream.Read(buffer, 0, buffer.Length);
 
                        size -= rec;
 
                        ms.Write(buffer, 0, buffer.Length);
                    }
 
                    ms.Close();
 
                    byte[] data = ms.ToArray();
 
                    ms.Dispose();


                    message = Encoding.UTF8.GetString(data);
                    serverEvent.ReceiveServerLog(message); //받은 메시지 원문 기록
                    OnReceived(message, ClientList[TcpSocket].ToString(), false);
                    /*
                    if (OnReceived != null)
                    {
                        if (message.Substring(message.LastIndexOf("$") + 1) == "@")
                            OnReceived(message.Substring(0, message.LastIndexOf("$")), ClientList[TcpSocket].ToString(), true);
                        else
                            ;
                    }
                     * */
                }
            }
            catch (Exception ex)
            {
                serverEvent.ErrorLog("DoChat", ex.Message);
                if (TcpSocket != null)
                {
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
