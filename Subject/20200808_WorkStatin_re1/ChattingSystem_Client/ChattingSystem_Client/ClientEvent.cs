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
    partial class Client_Form
    {

        public void ButtonStatusChange()
        {
            try
            {
                btnConnect.Enabled = !(btnConnect.Enabled);
                btnDisconnect.Enabled = !(btnDisconnect.Enabled);

                if(btnConnect.Enabled)
                    rtbxReceivedData.Text += tbxLocalIpAddress.Text + " 와의 연결이 끊어졌습니다.";
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
                
            }
            catch
            {
                //MessageBox.Show("연결 해제 메시지 단계 중 오류가 발생하였습니다.");
            }
        }
    }
}
