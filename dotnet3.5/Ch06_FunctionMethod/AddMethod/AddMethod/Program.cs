using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddMethod
{
    class Program
    {
        public static int Add(int a, int b) 
        { 
            return a + b; 
        }
        public static double Add( double a, double b)
        {
            return a + b;
        }
        public static string Add(string a, string b)
        {
            return a + b;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("1+2는 {0}입니다.",Add(1,2));
            Console.WriteLine("1.2+3.4는 {0}입니다.",Add(1.2,3.4));
            string a = "우리나라";
            string b = "대한민국";
            Console.WriteLine("{0} + {1}는 {2}입니다.",a,b,Add(a,b));

        }
    }
}
