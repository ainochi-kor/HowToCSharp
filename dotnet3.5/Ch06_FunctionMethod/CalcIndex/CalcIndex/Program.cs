using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcIndex
{
    class GuGu
    {
        public int this[int r, int c]
        {
            get
            {
                return (r * c);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GuGu G = new GuGu();
            Console.WriteLine("3 * 8 = {0}", G[3, 8]);
        }
    }
}
