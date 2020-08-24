using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singleton2
{
    class ClassB
    {
        Singleton singleton;

        public ClassB()
        {
            this.singleton = Singleton.GetInstance();
        }

        internal void PrintA()
        {
            Console.WriteLine(singleton.GetA());
        }
    }
}
