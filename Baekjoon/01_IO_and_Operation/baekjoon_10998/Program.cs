using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baekjoon_10998
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();
            int output = ((int)str[0]-48) * ((int)str[2]-48);
            Console.WriteLine(output);
        }
    }
}
