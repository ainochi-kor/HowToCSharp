using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singleton2
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton singleton = new Singleton();

            ClassA a = new ClassA();
            a.SetA(10);
            a.PirntA();

            ClassB b = new ClassB();
            b.PrintA();
        }
    }
}
