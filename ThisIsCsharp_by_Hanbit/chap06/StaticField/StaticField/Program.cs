using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticField
{
    class Global
    {
        public static int Count = 0;
    }

    class ClassA
    {
        public ClassA()
        {
            Global.Count++;
        }
    }

    class ClassB
    {
        public ClassB()
        {
            Global.Count++;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Global.Count : {0}", Global.Count);

            new ClassA();
            new ClassA();
            new ClassB();
            new ClassB();

            Console.WriteLine("Global.Count : {0}", Global.Count);
        }
    }
}
