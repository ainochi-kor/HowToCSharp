using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datatype_reference
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] Array1 = { 1, 2, 3 };
            int[] RefArray;
            RefArray = Array1;
            RefArray[1] = 20;
            Console.WriteLine("{0} {1} {2}", Array1[0], Array1[1], Array1[2]);
        }
    }
}
