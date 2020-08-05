using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace ChattingSystem_Client
{
    public class ClientEvent
    {

        public void ButtonStatusChange()
        {
            Client_Form client = new Client_Form();
            try
            {
                client.Controls.Find("btnConnect", true)[0].Enabled = !(client.Controls.Find("btnConnect", true)[0].Enabled);
                client.Controls.Find("btnDisconnect", true)[0].Enabled = !(client.Controls.Find("btnDisconnect", true)[0].Enabled);
            }
            catch 
            {
                //MessageBox.Show("버튼을 활성/비활성 단계에서 오류가 발생하였습니다.");
            }
         }

        public void DisconnectMessgae()
        {
            try
            {
                Client_Form client = new Client_Form();
                client.Controls.Find("tbxReceivedData", true)[0].Text += client.Controls.Find("tbxLocalIpAddress", true)[0].Text + " 와의 연결이 끊어졌습니다.";
            }
            catch 
            {
                //MessageBox.Show("연결 해제 메시지 단계 중 오류가 발생하였습니다.");
            }
        }
    }
}
