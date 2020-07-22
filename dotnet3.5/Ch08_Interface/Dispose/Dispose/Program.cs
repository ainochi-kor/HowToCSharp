using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispose
{
    class Socket : IDisposable
    {
        private int SocketPort;
        private bool Disposed = false;
        public Socket(int port)
        {
            SocketPort = port;
            Console.WriteLine("{0} 포트로 소켓을 연결한다.", port);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool bManage)
        {
            if (Disposed) return;
            Disposed = true;
            if(bManage)
            {
                //여기서 관리 자원을 해제한다.
            }
            SocketPort = 0;
            Console.WriteLine("소켓 연결을 해제한다.");
        }
        ~Socket()
        {
            Dispose(false);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Socket S = new Socket(1234);
            Console.WriteLine("주거니 받거니 통신했다 치고");
            S.Dispose();
        }
    }
}
