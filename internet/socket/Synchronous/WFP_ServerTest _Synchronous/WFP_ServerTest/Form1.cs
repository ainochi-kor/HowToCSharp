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
using System.Threading;

namespace WFP_ServerTest
{
    public partial class Form1 : Form
    {

        public static string data = null;

        private Socket listener = null;
        private Socket handler = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(handler != null)
            {
                handler.Close();
                handler.Dispose();
            }
            if(listener != null)
            {
                listener.Close();
                listener.Dispose();
            }
            Application.Exit();
        }

        public static string TruncateLeft(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string LocalIPAddresss()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress ip in host.AddressList)
            {
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    return localIP;
                }
            }
            return localIP;
        }

        //IP4, Stream형식, TCP통신
        

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] bytes = new byte[1024];

            IPAddress localIPAddress = IPAddress.Parse(LocalIPAddresss());
            IPEndPoint localEndPoint = new IPEndPoint(localIPAddress, 12000);

            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                new Thread(delegate(){
                    while(true)
                    {

                        Invoke((MethodInvoker)delegate
                        {
                            listBox1.Items.Add("Wating for connections...");
                        });

                        handler = listener.Accept();
                        Invoke((MethodInvoker) delegate
                        {
                            listBox1.Items.Add("Connecting!!!");
                        });
                        try
                        {
                            data = null;
                            while(true)
                            {
                                bytes = new byte[1024];
                                //받은 바이트의 수
                                int bytesRec = handler.Receive(bytes);
                                //바이트를 위치 0 부터 받은 바이트 수까지 출력.
                                data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                                if(data.IndexOf("<eof>") > -1)
                                    break;
                        
                            }

                            data = TruncateLeft(data, data.Length-5);

                            Invoke((MethodInvoker)delegate
                            {
                                listBox1.Items.Add(string.Format("Text Received : {0}",data));
                            });

                            data = "[Server Echo 메시지]" + data;
                            byte[] msg = Encoding.UTF8.GetBytes(data);

                            handler.Send(msg);
                        }
                        catch 
                        {
                            MessageBox.Show("서버 : DISCONNECTION!");
                            handler.Close();
                            handler.Dispose();
                            break;
                        }
                    }
                }).Start();
            }
            catch(SocketException se)
            {
                MessageBox.Show("SocketException Error : " + se.ToString());
                switch(se.SocketErrorCode)
                {
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionReset:
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception 에러 : " + ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
