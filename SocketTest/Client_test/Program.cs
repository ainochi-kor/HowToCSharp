using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client_test
{
    class Program
    {
        Socket trensferSocket;
        static void Main(string[] args)
        {
            Socket trensferSocket = new Socket(
                AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //trensferSocket.Connect(new IPEndPoint(IPAddress.Parse("10.177.55.73"), 1234));
            IPEndPoint companyEndPoint = new IPEndPoint(IPAddress.Parse("10.177.55.39"), 8080);
            try
            {
                trensferSocket.Connect(companyEndPoint);
            }
            catch
            {
                Console.WriteLine("Unable to connect to remote and point! \r\n");
                Main(args);
            }

            Console.Write("Enter Text: ");
            string text = Console.ReadLine();
            byte[] data = Encoding.UTF8.GetBytes(text);

            trensferSocket.Send(data);
            Console.Write("Data Sent! \r\n");
            Console.Write("Press any key To continue... ");
            Console.Read();
            trensferSocket.Close();
        }
    }
}
