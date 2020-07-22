using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contravariance
{
    class Base { }
    class Dervied : Base { }

    delegate void dBase(Base a);
    delegate void dDervied(Dervied a);

    class Program
    {
        public static void GetBase(Base a) { }
        public static void GetDerived(Dervied a) { }
        static void Main(string[] args)
        {
            dBase db;
            db = GetBase; 
            // db = GetDerived;

            dDervied dd;
            dd = GetBase;
            dd = GetDerived;
        }
    }
}
