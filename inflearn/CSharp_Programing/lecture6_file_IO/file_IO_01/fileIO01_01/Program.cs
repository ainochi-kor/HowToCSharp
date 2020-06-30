using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileIO01_01
{
    class Program
    {
        static void Main(string[] args)
        {
            int value = 12;
            float value2 = 3.14f;
            string str1 = "Hello World";
            //FileStream fs = new FileStream("test.txt", FileMode.Create);
            //StreamWriter sw = new StreamWriter(fs);
            //sw.WriteLine(value);
            //sw.WriteLine(value2);
            //sw.WriteLine(str1);
            //sw.Close();

            using (StreamWriter sw = new StreamWriter(new FileStream("Test1.txt",FileMode.Create)))
            {
                sw.WriteLine(value);
                sw.WriteLine(value2);
                sw.WriteLine(str1);
            }

        }
    }
}
