using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array1_01
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nArray = { 1, 2, 3, 4 };
            for (int i = 0; i < 4; i++)
                Console.Write("{0} ", nArray[i]);
            Console.Write('\n');

            for (int i = 0; i < nArray.Length; i++)
                Console.Write("{0} ", nArray[i]);
            Console.Write("\n");

            foreach (int m in nArray)
                Console.Write("{0} ", m);
            Console.Write('\n');

            string[] Days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            foreach (string str in Days)
                Console.Write(str + " ");
        }
    }
}
