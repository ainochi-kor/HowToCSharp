using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nojo
{
    class Staff { }
    class Manager : Staff { }
    class Personnel : Staff { }
    class Account : Staff { }
    class Finance : Account { }

    class Program
    {
        static void Main(string[] args)
        {
            Staff[] nojo = new Staff[100];
            nojo[0] = new Manager();
            nojo[1] = new Personnel();
            nojo[2] = new Account();
            nojo[3] = new Manager();
            nojo[4] = new Finance();
        }
    }
}
