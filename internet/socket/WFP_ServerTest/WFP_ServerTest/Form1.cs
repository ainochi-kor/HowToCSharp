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
        Socket sock; 
        Socket acc;
        public Form1()
        {
            InitializeComponent();
        }

        //IP4, Stream형식, TCP통신
        Socket socket() 
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //소켓 생성
            sock = socket();

            //소켓을 IP엔드포인트(파싱한 데이터)에 연결한다. 포트는 1234
            sock.Bind(new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1234));
            //최대 대기 수
            sock.Listen(0);

            new Thread(delegate()
                {
                    acc = sock.Accept(); //발신을 수신한다.
                    MessageBox.Show("서버 : CONNECTION ACCEPTED!");
                    sock.Close();//연결하는 소켓을 닫음.

                    while (true)
                    {
                        try
                        {
                            byte[] buffer = new byte[255]; 
                            // 수신 버퍼에 바인딩된 SocketFlags의 데이터를 받음.
                            // Receive한 바이트를 buffer이름의 배열에 저장하는데 
                            // 0번 인덱스부터 저장하며, buffer.Lenth
                            // (클라이언트에서 넘어온 버퍼의 크기)만큼 크기를 지정하여 
                            // rec에 저장한다.
                            int rec = acc.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                            

                            // 받은 데이터가 없으면 에러
                            if (rec <= 0)
                            {
                                throw new SocketException();
                            }

                            // 배열의 크기를 rec(buffer.Lenth)만큼 재조정한다.
                            Array.Resize(ref buffer, rec);
                            // 
                            Invoke((MethodInvoker)delegate
                            {
                                listBox1.Items.Add(Encoding.UTF8.GetString(buffer));
                            });
                        }
                        catch
                        {
                            MessageBox.Show("서버 : DISCONNECTION!");
                            acc.Close();
                            acc.Dispose();
                            break;
                        }
                    }
                    Application.Exit();
                }).Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //data라는 변수를 byte형식으로 textBox1의 text를 저장.
                byte[] data = Encoding.UTF8.GetBytes(textBox1.Text);
                //연결된 개체에 보내기
                acc.Send(data, 0, data.Length, 0);
            }
            catch(SocketException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
