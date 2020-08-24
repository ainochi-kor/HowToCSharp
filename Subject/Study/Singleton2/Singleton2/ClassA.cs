using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singleton2
{
    class ClassA
    {
        Singleton signleton;

        public ClassA()
        {
            this.signleton = Singleton.GetInstance();
        }

        internal void SetA(int val)
        {
            signleton.SetA(val);
        }

        internal void PirntA()
        {
            Console.WriteLine(signleton.GetA());
        }
    }
}
