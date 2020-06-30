using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType_Struct03
{
    class Program
    {
        // 구조체를 같은 구조체에 대입하게 되면 값이 복사
        // 구조체는 값 형식이고 클래스는 참조 형식이다.
        public struct MyStruct
        {
            public int Age;
        }
        static void Main(string[] args)
        {
           MyStruct TestStruct1, TestStruct2;
            TestStruct1.Age = 0;
            TestStruct2.Age = 10;
            Console.WriteLine(TestStruct1.Age);
            Console.WriteLine(TestStruct2.Age);
        }
    }
}
