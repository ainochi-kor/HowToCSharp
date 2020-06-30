using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baekjoon_1008
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();
            double output = ((double)str[0]-48) / ((double)str[2]-48);
            Console.WriteLine(output);
        }
    }
}
