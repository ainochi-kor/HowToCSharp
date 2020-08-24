using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singleton2
{
    class Singleton
    {
        private int a;
        private static Singleton singleton;

        public Singleton()
        {
            a = 0;
        }

        public static Singleton GetInstance()
        {

            if (singleton == null) 
                singleton = new Singleton();
            return singleton;
        }
        
        internal void SetA(int val)
        {
            this.a = val;
        }

        internal int GetA()
        {
            return a;
        }
    }
}
