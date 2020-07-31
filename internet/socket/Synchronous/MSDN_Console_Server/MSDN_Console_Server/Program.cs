using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MSDN_Console_Server
{
    class Program
    {
        public static string data = null;

        public static string TruncateLeft(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) 
                return value;
            
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string LocalIPAddress()
        {
            IPHostEntry host; //인터넷 호스트 주소 정보에 컨테이너 클래스를 제공.
            string localIP = "";
            //Dns.GetHostName() : 로컬컴퓨터의 호스트 이름을 가져옴.
            //Dns.GetHostEntry(Dns.GetHostName()) 
            // : 호스트 이름 또는 IP주소를 IPHostEntry 인스턴스로 확인.
            host = Dns.GetHostEntry(Dns.GetHostName());
            Console.WriteLine("host : " + host);

            //host 변수와 견결된 IP주소 목록을 가져오거나 설정합니다.
            foreach(IPAddress ip in host.AddressList)
            {
                //가져온 IP주소가 v4일경우 if문을 실행합니다.
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    //localIP에 IPAddress를 저장합니다. 
                    localIP = ip.ToString();
                    break;
                }
            }
            //IPAddress를 반환합니다.
            return localIP;
        }


        public static void StartListening()
        {
            //리스너할 소켓과 조작한 소켓을 변수로 저장
            Socket listener = null;
            Socket handler = null;

            // 데이터 버퍼가 들어올 때 저장할 바이트 배열.
            byte[] bytes = new byte[1024];

            
            // LocalIPAddress 함수를 통해 로컬 호스트 IPAddress를 추출하여 변수에 저장.
            IPAddress CompanyAddress = IPAddress.Parse(LocalIPAddress());
            // CompanyAddress 변수의 IP주소와 11000포트 번호로 된 클래스를 생성
            IPEndPoint CompanyEndPoint = new IPEndPoint(CompanyAddress, 11000);

            //v4, stream, tcp 방식의 소켓을 생성.
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // CompanyEndPoint 클래스에 저장된 값을 통하여 listener 소켓을 Bind
                listener.Bind(CompanyEndPoint);
                // listener 소켓 최대 대기를 10개 지정
                listener.Listen(10);

                while(true)
                {
                    Console.WriteLine("Waiting for connections...");
                    // hander 소켓에 listener에 연결된 통신을 handler로 넘김.
                    handler = listener.Accept();
                    // data배열을 리셋.
                    data = null;

                    while(true)
                    {
                        //bytes 배열은 통신받은 데이터를 저장할 공간.
                        bytes = new byte[1024];
                        // handler 소켓을 통해 들어온 데이터는 bytes에 들어와서
                        // 그 결과 값을 byteRec에 저장합니다.
                        int byteRec = handler.Receive(bytes);
                        // 디코딩할 바이트 배열, 디코딩할 첫 다이트의 인덱스, 디코딩할 바이트 수.
                        data += Encoding.UTF8.GetString(bytes,0, byteRec);
                        //Console.WriteLine("eof if문 전");
                        //Console.WriteLine(data);
                        if(data.IndexOf("<eof>") > -1)
                        {
                            //Console.WriteLine("eof if문 내부");
                            break;
                        }
                    }
                    //Console.WriteLine("문자 처리 while문 밖 ");
                    data = TruncateLeft(data, data.Length - 5);
                    
                    Console.WriteLine("Text received : {0}", data);

                    data = "[Server Echo 메시지]" + data;
                    byte[] msg = Encoding.UTF8.GetBytes(data);

                    // msg를 클라이언트로 전송.
                    handler.Send(msg);
                    // 보내기 및 받기를 사용할 수 없도록 설정한다.
                    handler.Shutdown(SocketShutdown.Both);
                    // 소켓을 닫음.
                    handler.Close();
                }
            }
            catch(SocketException se)
            {
                Console.WriteLine("Socket 에러 : {0}", se.ToString());
                switch(se.SocketErrorCode)
                {
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionReset:
                        handler.Close();
                        break;
                    
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static void Main(string[] args)
        {
            StartListening();
            Console.ReadLine();
        }
    }
}
