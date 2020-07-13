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
            Console.WriteLine("사각형의 너비를 입력하세요");
            string width = Console.ReadLine();

            Console.WriteLine("사각형의 높이를 입력하세요");
            string height = Console.ReadLine();

            int area = int.Parse(width) * int.Parse(height);
            Console.WriteLine($"사각형의 넓이 : {area}");
        }
    }
}
