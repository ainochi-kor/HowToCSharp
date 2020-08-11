using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inheritance
{
    class Base
    {
        protected string name;
        public Base(string name)
        {
            this.name = name;
            Console.WriteLine("{0}.Base()", this.name);
        }

         ~Base()
        {
            Console.WriteLine("{0}.~Base()", this.name);
        }

        public void BaseMethod()
        {
            Console.WriteLine("{0}.BaseMethod()", name);
        }
    }

    class Derived : Base
    {
        public Derived(string name) :base(name)
        {
            Console.WriteLine("{0}.Derived()",this.name);
        }

        ~Derived()
        {
            Console.WriteLine("{0}.~Derived()", this.name);
        }

        public void DeriveMethod()
        {
            Console.WriteLine("{0}.DerivedMethod()", name);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Base a = new Base("a");
            a.BaseMethod();

            Derived b = new Derived("b");
            b.BaseMethod();
            b.DeriveMethod();
        }
    }
}
