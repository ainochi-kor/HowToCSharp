using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace @override
{
    class Base
    {
        public virtual void Message() { Console.WriteLine("Base Message"); }
    }
    class Derived : Base
    { 
            public override void Message() { Console.WriteLine("Derived Message"); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Base B = new Base();
            Derived D = new Derived();
            B.Message();
            D.Message();
            Base B2 = D;
            B2.Message();
        }
    }
}
