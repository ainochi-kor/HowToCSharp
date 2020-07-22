using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelectMethod
{
    delegate int IntOpt(int a, int b);

    class Program
    {
        public static int Add(int a, int b) { return a + b; }
        public static int Mul(int a, int b) { return a * b; }
        static void Main(string[] args)
        {
            IntOpt[] arOP = { Add, Mul };
            int a = 3, b = 5;
            int o;
            Console.Write("어떤 연산을 하고 싶습니까? (1:덧셈, 2:곱셉)");
            o = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("결과는 {0} 입니다.", arOP[ o - 1 ](a,b));
        }
    }
}
