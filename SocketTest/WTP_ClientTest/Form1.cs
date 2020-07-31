using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WTP_ClientTest
{
    public partial class Form1 : Form
    {
        Socket sock;
        public Form1()
        {
            InitializeComponent();
            sock = socket();
            FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        void read()
        {
            while(true)
            {
                try
                {
                    byte[] buffer = new byte[255];
                    int rec = sock.Receive(buffer, 0, buffer.Length, SocketFlags.None);

                    if(rec <= 0)
                    {
                        throw new SocketException();
                    }

                    Array.Resize(ref buffer, rec);

                    Invoke((MethodInvoker)delegate
                    {
                        listBox1.Items.Add(Encoding.UTF8.GetString(buffer));
                    });
                }
                catch
                {
                    MessageBox.Show("클라이언트 : DISCONNECTION");
                    sock.Close();
                    break;
                }
            }
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sock.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sock.Connect(new IPEndPoint(IPAddress.Parse(textBox1.Text.Trim()), 1234));
                new Thread(() =>
                {
                    read();
                }).Start();
            }
            catch
            {
                MessageBox.Show("클라이언트 : CONNECTION FAILED");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] data = Encoding.UTF8.GetBytes(textBox2.Text);
            sock.Send(data, 0, data.Length, SocketFlags.None);
        }
    }
}
