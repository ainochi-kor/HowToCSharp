using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sealed
{
    class Base
    {
        public virtual void Message() { Console.WriteLine("Base Message"); }
    }
    class Derived : Base
    {
        public sealed override void Message() { Console.WriteLine("Derived Message"); } 
    }
    class Third : Derived
    {
        public new void Message() { Console.WriteLine("Third Message"); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Base B = new Base();
            Derived D = new Derived();
            Third T = new Third();
            B.Message();
            D.Message();
            T.Message();


        }
    }
}
