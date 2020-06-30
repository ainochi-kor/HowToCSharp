using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO01_02_StreamWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            int vlaue = 12;
            float value2 = 3.14f;
            string str1 = "Hello World";
            StreamWriter sw = new StreamWriter("Test.txt");
            sw.WriteLine(value);
            sw.WriteLine(value2);
            sw.WriteLine(str1);
            sw.Close();
        }
    }
}
