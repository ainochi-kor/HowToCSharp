using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Covariance
{
    class Base { }
    class Derived : Base { }

    delegate Base dBase();
    delegate Derived dDerived();

    class Program
    {
        public static Base GetBase() { return new Base(); }
        public static Derived GetDerived() { return new Derived(); }
        static void Main(string[] args)
        {
            dBase db;
            db = GetBase;
            db = GetDerived;

            dDerived dd;
            // dd = GetBase;
            dd = GetDerived;
        }
    }
}
