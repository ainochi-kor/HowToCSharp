using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shallow
{
    class C
    {
        public int v;
        public C(int v) { this.v = v; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            C a = new C(1234);
            C b = a;
            b.v = 5678; 
            Console.WriteLine("a = {0}, b = {1}", a.v,b.v);
        }
    }
}
