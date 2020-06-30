using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baekjoon_14681
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = Convert.ToInt32(Console.ReadLine());
            int y = Convert.ToInt32(Console.ReadLine());

            if (x * y > 0)
            {
                if (x > 0)
                    Console.WriteLine("1");
                else
                    Console.WriteLine("3");
            }
            else
            {
                if (x > 0)
                    Console.WriteLine("4");
                else
                    Console.WriteLine("2");
            }
        }
    }
}
