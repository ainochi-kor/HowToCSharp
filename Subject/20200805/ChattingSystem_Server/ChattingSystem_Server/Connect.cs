using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ChattingSystem_Server
{
    public class ServerEvent 
    {
        

        public Socket SetupSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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

        public void Disconnect(Socket AcceptSocket, Socket Listener)
        {
            if (AcceptSocket != null)
            {
                AcceptSocket.Close();
                AcceptSocket.Dispose();
            }
            if (Listener != null)
            {
                Listener.Close();
                Listener.Dispose();
            }
        }

        public void ButtonStatusChange()
        {
            ServerForm server = new ServerForm();
            try
            {
                server.Controls.Find("btnStart", true)[0].Enabled =
                    !(server.Controls.Find("btnStart", true)[0].Enabled);
                server.Controls.Find("btnStop", true)[0].Enabled =
                    !(server.Controls.Find("btnStop", true)[0].Enabled);
            }
            catch { }
        }

        
        public void DisconnectMessgae()
        {
            ServerForm server = new ServerForm();
            //server.Controls.Find("tbxReceivedData", true)[0].Text += server.GetClientIP() + " 와의 연결이 끊어졌습니다...";
        }

        
    }
}
