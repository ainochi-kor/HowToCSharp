using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex009_Reference
{
    class Program
    {
        class MyClass
        {
            public int number;
            public MyClass()
            {
                number = 12;
            }
        }
        static void Main(string[] args)
        {
            MyClass MyTest = new MyClass();
            MyClass RefClass = MyTest;
            RefClass.number = 7;
            Console.WriteLine(MyTest.number);
        }
    }
}
