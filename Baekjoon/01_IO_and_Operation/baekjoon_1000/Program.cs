using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baekjoon_1000
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string str = Console.ReadLine();
            int sum = (int)str[0] - 48 + (int)str[2] - 48;
            Console.WriteLine(sum);
        }
    }
}
