using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MSDN_Console_Client
{
    class Program
    {
        // 로컬 호스트의 IP를 추출하는 함수.
        public static string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private static void StartClient()
        {
            
            byte[] bytes = new byte[1024];

            try
            {
                IPAddress remoteIPAddress = IPAddress.Parse(LocalIPAddress());
                IPEndPoint remoteEndPoint = new IPEndPoint(remoteIPAddress, 11000);

                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    //로컬 호스트에 연결.
                    sender.Connect(remoteEndPoint);
                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                    //메시지 입력한 것을 바이트화 시킴.
                    Console.Write("Client 메시지 : ");
                    byte[] msg = Encoding.UTF8.GetBytes(Console.ReadLine());

                    //메시지를 보내고 결과값을 in로 받음.
                    int bytesSent = sender.Send(msg);

                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}", Encoding.UTF8.GetString(bytes, 0, bytesRec));

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                catch(ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch(SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch(Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void Main(string[] args)
        {
            int i = 0;
            while (i < 3)
            {
                StartClient();
                i++;
            } 
            Console.ReadLine();
        }
    }
}
