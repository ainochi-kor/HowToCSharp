using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ChattingSystem_Client
{
    partial class Client_Form
    {
        Socket SetupSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void ButtonStatusChange()
        {
            ConnectButton.Enabled = !(ConnectButton.Enabled);
            DisconnectButton.Enabled = !(DisconnectButton.Enabled);
        }

        private void DisconnectMessgae()
        {
            ReceivedData_TextBox.Text += LocalIpAddress_textBox.Text + " 와의 연결이 끊어졌습니다.";
        }

    }
}
