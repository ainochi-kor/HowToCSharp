using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            a = 100;
            Console.WriteLine($"a = 100 : {a}");
            a += 90;
            Console.WriteLine($"a += 90 : {a}");
            a -= 80;
            Console.WriteLine($"a -= 80 : {a}");
            a *= 70;
            Console.WriteLine($"a *= 70 : {a}");
            a /= 60;
            Console.WriteLine($"a /= 60 : {a}");
            a %= 50;
            Console.WriteLine($"a %= 50 : {a}");
            a &= 40;
            Console.WriteLine($"a &= 40 : {a}");
            a |= 30;
            Console.WriteLine($"a |= 30 : {a}");
            a ^= 20;
            Console.WriteLine($"a ^= 20 : {a}");
            a <<= 10;
            Console.WriteLine($"a <<= 10 : {a}");
            a >>= 1;
            Console.WriteLine($"a >>= 1 : {a}");
        }
    }
}
