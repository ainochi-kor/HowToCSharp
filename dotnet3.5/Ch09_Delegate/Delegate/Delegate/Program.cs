using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delegate
{
    delegate void dele(int a);
    class Program
    {
        public static void Method1(int a) { Console.WriteLine("Method1 " + a); }
        public static void Method2(int a) { Console.WriteLine("Method2 " + a); }
        static void Main(string[] args)
        {
            dele d;
            d = new dele(Method1);
            d(12);
            d = new dele(Method2);
            d(34);
        }
    }
}
