using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clone
{
    class C : ICloneable
    {
        public int v;
        public C(int v) { this.v = v; }
        public object Clone()
        {
            return new C(v);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            C a = new C(1234);
            C b = (C)a.Clone();
            b.v = 5678;
            Console.WriteLine("a = {0}, b = {1}", a.v, b.v);
        }
    }
}
