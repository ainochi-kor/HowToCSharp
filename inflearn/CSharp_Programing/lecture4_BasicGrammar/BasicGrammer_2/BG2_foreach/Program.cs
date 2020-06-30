using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG2_foreach
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] Array1 = { 1, 2, 3, 4 };
            foreach(int nValue in Array1)
            {
                Console.WriteLine(nValue);
            }

            ArrayList List = new ArrayList();
            List.Add(1);
            List.Add(2);
            List.Add(3);

            foreach(int nValue in Array1)
            {
                Console.WriteLine(nValue);
            }
        }
    }
}
