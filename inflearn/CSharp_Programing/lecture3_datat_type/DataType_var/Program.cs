using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType_var
{
    class Program
    {
        static void Main(string[] args)
        {
            var value1 = 3.14f;
            float value2 = 10.12f;
            float sum = value1 + value2;
            Console.WriteLine("{0} {1:f2} {2}", value1, value2, sum);
        }
    }
}
