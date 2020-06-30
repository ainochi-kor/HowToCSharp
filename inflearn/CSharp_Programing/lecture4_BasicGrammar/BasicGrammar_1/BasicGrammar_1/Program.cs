using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG1_Operater
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool 연산자
            bool bFlag = false;
            Console.WriteLine("{0} {1} {2}\n", !bFlag, !true, !false);

            //산술연산자
            string str;
            str = "3" + ".14";
            Console.WriteLine(+5);
            Console.WriteLine(5+5);
            Console.WriteLine(5+.5);
            Console.WriteLine("5"+"5");
            Console.WriteLine(5.01f+"5");
            Console.WriteLine(3.14f+"5");
            Console.WriteLine(str);
            str = 1 + "test" + 3.14f + "abcd";
            Console.WriteLine(str+"\n");

            //is 연산자
            int nValue = 10;
            if (nValue is float)
                Console.WriteLine("호환됨");
            else
                Console.WriteLine("호환 안됨");

            if (nValue is object) // boxing 호환
                Console.WriteLine("호환됨");
            else
                Console.WriteLine("호환 안됨");

            object obj = nValue;
            if (obj is int)
            {
                Console.WriteLine("호환됨");
            }
            else
                Console.WriteLine("호환 안됨");

            

        }
    }
}
