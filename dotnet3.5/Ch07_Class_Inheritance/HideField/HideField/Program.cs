using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HideField
{
    class Base
    {
        public int a;
    }
    class Derived : Base
    {
        public new double a;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Base B = new Base();
            Derived D = new Derived();
            B.a = 1234;
            D.a = 5.678;
            Console.WriteLine(B.a);
            Console.WriteLine(D.a);
        }
    }
}
