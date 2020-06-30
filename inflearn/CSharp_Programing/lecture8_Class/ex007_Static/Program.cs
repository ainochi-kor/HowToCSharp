using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex007_Static
{
    class Program
    {
        class MyClass
        {
            public static int number = 12;
            public static void Print() { Console.WriteLine("Hello World!"); }
        }
        static void Main(string[] args)
        {
            MyClass.Print();
            Console.WriteLine(MyClass.number);
            MyClass.number = 200;
            MyClass test = new MyClass();
            Console.WriteLine(MyClass.number);
        }
    }
}
