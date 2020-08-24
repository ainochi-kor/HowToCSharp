using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingleTon_LazyTest
{
    class Program
    {
        // Lazy 초기화는 여기에서 불리지만,
        // LazyObject는 ThreadProc이 수행되기 전까지 생성되지 않는다.

        static Lazy<LargeObject> lazyLargeObject = new Lazy<LargeObject>
        
        static void Main(string[] args)
        {
        }
    }
}
