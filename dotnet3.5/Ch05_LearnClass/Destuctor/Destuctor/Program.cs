using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Destuctor
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket S = new Socket(1234);
            Console.WriteLine("주거니 받거니 통신했다 친다고 한다...");
        }
    }
    class Socket
    {
        private int SocketPort;
        public Socket(int port)
        {
            SocketPort = port;
            Console.WriteLine("{0} 포트로 소켓을 연결한다.", port);
        }
        ~Socket()
        {
            SocketPort = 0; 
            Console.WriteLine("소켓 연결을 해제한다");
        }
    }
}
