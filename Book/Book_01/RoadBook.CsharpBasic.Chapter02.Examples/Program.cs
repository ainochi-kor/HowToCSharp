using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadBook.CsharpBasic.Chapter02
{
    class Program
    {
        static void Main(string[] args)
        {
            Examples.Ex001 ex001 = new Examples.Ex001();
            ex001.Run();
            Console.WriteLine("\n");
            Examples.Ex002 ex002 = new Examples.Ex002();
            ex002.Run();
            Console.WriteLine("\n");
            Examples.Ex003 ex003 = new Examples.Ex003();
            ex003.Run();
            Console.WriteLine("\n");
            Examples.Ex004 ex004 = new Examples.Ex004();
            ex004.Run(); 
            Console.WriteLine("\n");
            Examples.Ex005 ex005 = new Examples.Ex005();
            ex005.Run();
        }
    }
}
