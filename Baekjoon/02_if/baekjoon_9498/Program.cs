using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baekjoon_9498
{
    class Program
    {
        static void Main(string[] args)
        {
            int score = Convert.ToInt32(Console.ReadLine());

            if (score <= 100 && score > 89)
                Console.WriteLine("A");
            else if (score > 79)
                Console.WriteLine("B");
            else if (score > 69)
                Console.WriteLine("C");
            else if (score > 59)
                Console.WriteLine("D");
            else
                Console.WriteLine("F");
        }
    }
}
