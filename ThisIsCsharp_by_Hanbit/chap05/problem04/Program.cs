using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace problem04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("반혹 횟수를 입력하세요 : ");
            int num = Int32.Parse(Console.ReadLine());

            if (num <= 0)
                Console.WriteLine("0보다 작거나 같은 수는 입력할 수 없습니다.");

            
            for(int i = 0; i <= num; i++ )
            {
                for(int j = 0; j < i;j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
    }
}
