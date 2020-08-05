using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ChattingSystem_Server
{
    public class ServerForm 
    {

        Socket SetupSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private static string LocalIPAddress()
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

        private static void Disconnect(Socket AcceptSocket,Socket Listener)
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

        private void ButtonStatusChange()
        {
            StartButton.Enabled = !(StartButton.Enabled);
            StopButton.Enabled = !(StopButton.Enabled);
        }

        private void GetClientIP(string ClientEndPoint)
        {
            _getClientIP = _acceptSocket.RemoteEndPoint.ToString();
            ReceivedData_TextBox.Text += _getClientIP + "와 연결되었습니다.\r\n";
        }

        private void DisconnectMessgae()
        {
            ReceivedData_TextBox.Text += _getClientIP + " 와의 연결이 끊어졌습니다...";
        }

        private void ShutdownThread()
        {
            try
            {   //Form_Closing 시, 바꿀 데이터가 없어서 오류가 발생함.
                this.Invoke(new DeligateButtonChange(ButtonStatusChange));
                this.Invoke(new DeligateDisconnectMessgae(DisconnectMessgae));
                _acceptSocket.Close();
                _acceptSocket.Dispose();
            }
            catch { 

            }

        }
    }
}
