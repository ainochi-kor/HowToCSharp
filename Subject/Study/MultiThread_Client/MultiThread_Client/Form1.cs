using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace MultiThread_Client
{
    public partial class Form1 : Form
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream stream = default(NetworkStream);
        string message = string.Empty;

        string clientIP = "";
        string clientPort = "";

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for(int i = 65 ; i < 91 ;i++)
            {
                cbxChannel.Items.Add(Convert.ToChar(i));
            }
            
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(clientIP + ":" + clientPort + "/" + this.tbxMessage.Text + "$");
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            clientSocket.Connect("192.168.0.193", 1234);
            stream = clientSocket.GetStream();

            message = "Connected to Chat Server";
            DisplayText(message);

            byte[] buffer = Encoding.Unicode.GetBytes(this.cbxChannel.Text + "$");
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();

            Thread t_hander = new Thread(GetMessage);
            t_hander.IsBackground = true;
            t_hander.Start();
        }

        bool _isReceivedInfo = false;
        private void GetMessage()
        {
            while (true)
            {
                stream = clientSocket.GetStream();
                int BUFFERSIZE = clientSocket.ReceiveBufferSize;
                byte[] buffer = new byte[BUFFERSIZE];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.Unicode.GetString(buffer, 0, bytes);

                if (!_isReceivedInfo)
                {
                    clientIP = message.Split(':')[0];
                    clientPort = message.Split(':')[1].Split('/')[0];
                    _isReceivedInfo = !_isReceivedInfo;
                }
                DisplayText(message);
            }
        }

        private void DisplayText(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.BeginInvoke(new MethodInvoker(delegate
                {
                    richTextBox1.AppendText(text + Environment.NewLine);
                }));
            }
            else
                richTextBox1.AppendText(text + Environment.NewLine);
        }   
    }
}
