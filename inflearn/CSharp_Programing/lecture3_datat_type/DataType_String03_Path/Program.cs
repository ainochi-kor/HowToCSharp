using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType_String03_Path
{
    class Program
    {
        static void Main(string[] args)
        {
            string str1 = "C:\\temp\\test.txt";
            string str2 = @"C:\temp\test.txt";
            Console.WriteLine("{0} {1}", str1, str2);
        }
    }
}
