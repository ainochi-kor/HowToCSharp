using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileIO01_05_OnlyStreamReader
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("test.txt");
            int value = int.Parse(sr.ReadLine());
            float value2 = float.Parse(sr.ReadLine());
            string str1 = sr.ReadLine();
            Console.WriteLine("{0} {1} {2}", value, value2, str1);
        }
    }
}
