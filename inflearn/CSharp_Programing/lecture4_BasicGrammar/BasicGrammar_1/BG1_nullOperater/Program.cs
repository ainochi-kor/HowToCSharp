using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG1_nullOperater
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * null 병합 연산자
             * ?? (null 조사)
             * C = A ?? B
             * A 가 null이 아니면 A를 C에 대입.
             * A 가 null이면 B를 C에 대입. 
             */

            int? x = null; //nullable형식, null저장가능
            int y = x ?? -1;
            Console.WriteLine(y);
            x = 10;
            y = x ?? -1;
            Console.WriteLine(y);
        }
    }
}
