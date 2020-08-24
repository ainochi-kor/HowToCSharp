using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingleTon_LazyTest
{
    class LargeObject
    {
        private int initBy = 0;
        public int InitializedBy { get { return initBy; } }

        public long[] Data = new long[1024 * 1024 * 10];

        public LargeObject(int initializedBy)
        {
            initBy = initializedBy;
            Console.WriteLine("LargeObject was created on thread id {0}.", initBy);
        }
    }
}
