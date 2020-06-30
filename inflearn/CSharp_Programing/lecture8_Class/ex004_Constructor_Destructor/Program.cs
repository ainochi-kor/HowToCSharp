using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex004_Constructor_Destructor
{
    class Program
    {
        class MyClass
        {
            string Message;
            
            public MyClass()
            {
                Message = "Hello World";
            }

            public void PrintMessage()
            {
                Console.WriteLine(Message);
            }
        }
        static void Main(string[] args)
        {
            MyClass MyTest = new MyClass();
            MyTest.PrintMessage();
        }
    }
}
