using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ex003_Split_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string str;
            Console.Write("파일명을 입력해주세요 : ");
            string filename = Console.ReadLine();
            StreamReader sr = new StreamReader(filename);
            str = sr.ReadLine();
            string[] strData1 = str.Split(new char[] { ':' });
            int nCount = int.Parse(strData1[1]);
            Console.WriteLine("------------------------------");
            for(int i = 0; i < nCount; i++)
            {
                str = sr.ReadLine();
                string[] strData2 = str.Split(new char[] { ',' });
                Console.WriteLine("{0} {1} {2} {3} {4}",
                    strData2[0], strData2[1], strData2[2],strData2[3], strData2[4]);
            }
            Console.WriteLine("------------------------------");
            sr.Close();
        }
    }
}
