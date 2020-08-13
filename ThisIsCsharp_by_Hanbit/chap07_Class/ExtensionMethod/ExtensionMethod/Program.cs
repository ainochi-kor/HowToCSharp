using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyExtension; 

namespace MyExtension
{
    public static class IntegerExtension
    {
        public static int Square(this int myInt)
        {
            return myInt * myInt;
        }

        public static int Power(this int myint, int exponent)
        {
            int result = myint;
            for (int i = 1; i < exponent; i++)
                result = result * myint;

            return result;
        }
    }
}

namespace ExtensionMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("3^2 : {0}", 3.Square());
            Console.WriteLine("3^4 : {0}", 3.Power(4));
            Console.WriteLine("2^10 : {0}", 2.Power(10));
        }
    }
}
