using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net;
using System.Net.Sockets;

namespace server_test
{
    class Program
    {
        static byte[] Buff { get; set; }
        static Socket serverSock;
        static void Main(string[] args)
        {
            ///
            /// 1. new Socket : 서버 소켓을 생성
            /// 2. AddressFamily.InterNetwork : 사용할 주소 체계
            /// 3. ProtocolType.IP : 프로토콜 타입을 IP로 설정
            serverSock = new Socket(
                AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            /// 4. new IPEndPoint : 서버의 주소를 받음
            /// 5. IPAddress.Any : 1234 포트로 들어오는 아이피 주소
            serverSock.Bind(new IPEndPoint(IPAddress.Any,1234));
            ///최대 대기자 수 설정.
            serverSock.Listen(100);

            Socket accepted = serverSock.Accept();

            Buff = new byte[accepted.SendBufferSize];
            int bytesRead = accepted.Receive(Buff);
            byte[] formatted = new byte[bytesRead];
            for(int i = 0 ; i < bytesRead ; i++)
            {
                formatted[i] = Buff[i];
            }

            string strdata = Encoding.UTF8.GetString(formatted);
            Console.Write(strdata + "\r\n");
            Console.Read();

            accepted.Close();
            serverSock.Close();
        }
    }
}
