using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType_nullable
{
    class Program
    {
        // null을 허용하지 않는 데이터형이 null값을 허용.
        // 데이터형? 변수명;
        // int? Var1;
        // bool? Var2 = null; // true, false, null

        /*
         * 속성
         * .HasValue // true, false // 값이 저장되어 있는지 확인할 수 있음.
         * .Value // 읽기 전용 // 쓸일이 거의 없음.
         */

        static void Main(string[] args)
        {
            int? Num1 = null;

            if (Num1.HasValue)
                Console.WriteLine("올바른 값");
            else
                Console.WriteLine("null 값");

            Console.WriteLine("null : {0}", Num1);
        }
    }
}
