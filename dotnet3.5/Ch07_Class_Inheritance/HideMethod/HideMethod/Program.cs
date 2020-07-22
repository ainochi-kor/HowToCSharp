using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HideMethod
{
    class Base
    {
        public void Message() { Console.WriteLine("Base Message"); }
    }
    class Derived : Base
    {
        public void Message() { Console.WriteLine("Derived Message");  }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Base B = new Base();
            Derived D = new Derived();
            B.Message();
            D.Message();
        }
    }
}
