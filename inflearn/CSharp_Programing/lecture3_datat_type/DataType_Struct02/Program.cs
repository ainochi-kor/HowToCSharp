using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType_Struct02
{
    class Program
    {
        public struct MyStruct
        {
            public int Age;
            public MyStruct(int InAge)
            {
                Age = InAge;
            }
        }
        static void Main(string[] args)
        {
            MyStruct TestStruct1;
            TestStruct1.Age = 12;
            Console.WriteLine(TestStruct1.Age);

            MyStruct TestStruct2 = new MyStruct();
            Console.WriteLine(TestStruct2.Age);

            MyStruct TestStruct3 = new MyStruct(12);
            Console.WriteLine("{0}", TestStruct3.Age);

        }
    }
}
