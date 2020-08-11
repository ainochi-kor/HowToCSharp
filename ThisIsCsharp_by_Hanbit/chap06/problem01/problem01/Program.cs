using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace problem01
{
    class Program
    {
        static double Square(int num)
        {
            return num * num;
        }

        static double Square(double num)
        {
            return num * num;
        }

        static void Main(string[] args)
        {
            Console.Write("수를 입력하세요: ");
            double input = Double.Parse(Console.ReadLine());

            if(input > (int)input)
                Console.WriteLine("결과 : {0}", Square(input));
            else
                Console.WriteLine("결과 : {0}", Square((int)input));
        }
    }
}
