using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baekjoon_1001
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();
            int output = (int)str[0] - (int)str[2];
            Console.WriteLine(output);
        }
    }
}
