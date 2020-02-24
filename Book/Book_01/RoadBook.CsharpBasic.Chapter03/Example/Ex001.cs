using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadBook.CsharpBasic.Chapter03.Example
{
    class Ex001
    {
        public void Run()
        {
            const int zero = 0;

            if (zero == 0)
                Console.WriteLine("첫번째 if문 입니다.");
            if (zero == 0)
            {
                Console.WriteLine("두번째 if문입니다.");
                Console.WriteLine("zero는 0이기 때문에 출력을 합니다.");
            }

            if(zero == 1)
            {
                Console.WriteLine("세번째 if문입니다");
                Console.WriteLine("zero는 1이 아니기 때문에 출력에서 제외됩니다.");
            }
        }
    }
}
