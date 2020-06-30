using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex003_Access
{
    class Program
    {
        class MyClass
        {
            public int Year, Month, Day;
            public void Print()
            {
                Console.WriteLine("{0}년 {1}월 {2}일", Year, Month, Day);
            }
        }
        static void Main(string[] args)
        {
            MyClass MyTest = new MyClass();
            MyTest.Year = 2020;
            MyTest.Month = 6;
            MyTest.Day = 30;
            MyTest.Print();
        }
    }
}
