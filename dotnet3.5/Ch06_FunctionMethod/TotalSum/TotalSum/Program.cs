using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TotalSum
{
    class Program
    {
        static public int TotalSum(int from, int to)
        {
            int sum = 0;
            for(int i = from; i<= to ; i++)
            {
                sum += i;
            }
            return sum;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("10~20의 합계 = {0}", TotalSum(10, 20));
        }
    }
}
