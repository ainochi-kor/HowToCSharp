using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex005_Constructor
{
    class Program
    {
        class MyClass
        {
            string Message;

            public MyClass(string InputMessage)
            {
                Message = InputMessage;
            }

            public void PrintMessage()
            {
                Console.WriteLine(Message);
            }
        }
        static void Main(string[] args)
        {
            MyClass test = new MyClass("Happy!");
            test.PrintMessage();
        }
    }
}
