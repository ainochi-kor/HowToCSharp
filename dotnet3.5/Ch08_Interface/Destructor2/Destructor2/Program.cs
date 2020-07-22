using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Destructor2
{
    class Sokcet : IDisposable
    {
        private int SocketPort;
        public Sokcet(int port)
        {
            SocketPort = port;
            Console.WriteLine("{0} 포트로 소켓을 연결한다.",port);
        }

        public void Dispose()
        {
            SocketPort = 0;
            Console.WriteLine("소켓 연결을 해제한다.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Sokcet S = new Sokcet(1234);
            Console.WriteLine("주거니 받거니 통신했다 치고...");

            S.Dispose();
        }
    }
}
