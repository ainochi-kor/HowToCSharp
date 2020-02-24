using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadBook.CsharpBasic.Chapter02.Examples
{
    class Ex011
    {
        int globalValue = 20;
        public void Run()
        {
            int localValue = 10;
            Sum();
            Multiple();

            Console.WriteLine("local : {0} / global : {1}", localValue, globalValue);
        }
        private void Sum()
        {
            globalValue += 10;
        }
        private void Multiple()
        {
            globalValue *= 2; 
        }
    }
}
