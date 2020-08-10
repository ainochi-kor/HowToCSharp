using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace problem03
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            while (i <  5)
            {
                int j = 0;
                while (j <= i)
                {
                    Console.Write("*");
                    j++;
                }
                Console.WriteLine();
                i++;
            }

            while( i > 0)
            {
                int j = 0;
                while(j < i)
                {
                    Console.Write("*");
                    j++;
                }
                Console.WriteLine();
                i--;
            }
        }
    }
}
