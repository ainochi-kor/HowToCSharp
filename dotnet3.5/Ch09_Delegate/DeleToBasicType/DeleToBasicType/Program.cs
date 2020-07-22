using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeleToBasicType
{
    delegate void dLong(long a);
    delegate void dInt(int a);
    class Program
    {
        public static void GetLong(long a) { }
        public static void GetInt(int a) { }
        static void Main(string[] args)
        {
            dLong dl;
            dl = GetLong;
            // dl = GetInt;

            dInt di;
            //di = GetLong;
            di = GetInt;
        }
    }
}
