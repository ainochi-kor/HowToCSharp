using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLambda
{
    class Program
    {
        delegate int Calculate(int a, int b);

        static void Main(string[] args)
        {
            Calculate calc = (a, b) => a + b;
            Console.WriteLine("{0} + {1} : {2}", 3, 4, calc(3, 4));
        }
    }
}
