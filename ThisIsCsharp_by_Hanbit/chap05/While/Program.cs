using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace While
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 10;

            while(i > 0)
            {
                Console.WriteLine($"i : {i--}");
            }
        }
    }
}
