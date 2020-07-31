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
using System.IO;

namespace WTP_ManyText_ServerTest
{
    public partial class Form1 : Form
    {
        Socket conn;
        Socket accept;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //소켓을 여는 행위
            conn.Bind(new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1234));
            conn.Listen(100);

            
            
            new Thread(() =>
                {
                    accept = conn.Accept();
                    conn.Close();
                    MessageBox.Show("연결되었습니다");
                    while (true)
                    {
                        try
                        {
                            //바이트의 크기를 먼저 받기 위한 변수를 생성.
                            byte[] sizeBuf = new byte[4];

                            accept.Receive(sizeBuf, 0, sizeBuf.Length, 0);

                            //sizeBuf크기를 size에 대입.
                            int size = BitConverter.ToInt32(sizeBuf, 0);
                            //스트림의 현재위치에서 읽기 또는 쓰기 작업 위치를 수행
                            MemoryStream ms = new MemoryStream();

                            while (size > 0)
                            {
                                byte[] buffer;

                                if (size < accept.ReceiveBufferSize)
                                    buffer = new byte[size];
                                else
                                    buffer = new byte[accept.ReceiveBufferSize];

                                int rec = accept.Receive(buffer, 0, buffer.Length, 0);

                                size -= rec;

                                ms.Write(buffer, 0, buffer.Length);
                            }

                            ms.Close();

                            byte[] data = ms.ToArray();

                            ms.Dispose();

                            Invoke((MethodInvoker)delegate
                            {
                                textBox1.Text = Encoding.UTF8.GetString(data);
                            });
                        }
                        catch
                        {
                            MessageBox.Show("서버 : DISCONNECTION!");
                            accept.Close();
                            accept.Dispose();
                            break;
                        }
                    }
                    Application.Exit();
                }).Start();
        }
    }
}
