using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex001_AccessModifier
{
    class Program
    {
        class Date
        {
            public int Year;
            protected int Month;
            int Day;
            

            public void Print()
            {
                Console.WriteLine("{0} {1} {2}", Year, Month, Day);
            }
        }
        static void Main(string[] args)
        {
            Date Test = new Date();
            Test.Print(); 
        }
    }
}
