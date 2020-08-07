using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ChattingSystem_Server
{
    partial class ServerForm
    {
        public void Disconnect()
        {
            if (TcpClient != null)
            {
                TcpClient.Close();
            }
            if (TcpListner != null)
            {
                TcpListner.Stop();
            }
        }

        private void DisplayText(string text)
        {
            if (rtbxReceivedData.InvokeRequired)
            {
                rtbxReceivedData.BeginInvoke(new MethodInvoker(delegate
                {
                    rtbxReceivedData.AppendText(text + Environment.NewLine);
                }));
            }
            else
                rtbxReceivedData.AppendText(text + Environment.NewLine);
        }

        public void ButtonStatusChange()
        {
            try
            {
                btnStart.Enabled = !(btnStart.Enabled);
                btnStop.Enabled = !(btnStop.Enabled);

                if (btnStart.Enabled == true)
                    rtbxReceivedData.Text += clientInfo.ClientIP + " 와의 연결이 끊어졌습니다...";
            }
            catch { }
        }

        public void ShutdownThread()
        {
            try
            {   //Form_Closing 시, 바꿀 데이터가 없어서 오류가 발생함.
                Thread thread = Thread.CurrentThread;
                thread.Interrupt();
                TcpClient.Close();
                Thread.Sleep(1000); //Dispose하는 시간이 필요하여 1초정도 메인 쓰레드를 쉬어줌.
            }
            catch
            { }
        }

        public void ServerForm_Load(object sender, EventArgs e)
        {
            //폼 로드시, 현재 컴퓨터의 LocalIPAddress를 TextBox에 출력합니다.
            tbxLocalIpAddress.Text = serverEvent.LocalIPAddress();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }
    }
}
