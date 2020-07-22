using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multilnheritance
{
    interface A1
    {
        void a();
    }
    interface A2
    {
        void b();
    }
    interface B : A1, A2
    {
        void b();
    }

    class C : B
    {
        public void a() { Console.WriteLine("a"); }
        public void b() { Console.WriteLine("b"); }
    }
    class Program
    {
        static void Method (B b)
        {
            b.a();
        }
        static void Main(string[] args)
        {
            C c = new C();
            c.a();  
            Method(c);
        }
    }
}
