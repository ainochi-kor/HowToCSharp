using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG2_Switch_Case
{
    class Program
    {
        static void Main(string[] args)
        {
            //정수, 문자상수, 문자열
            //반드시 break를 쓰도록 해야함.

            int nNum = 1;
            switch(nNum)
            {
                case 1:
                    Console.WriteLine("1 입니다.");
                    break;
                case 2:
                    Console.WriteLine("2 입니다.");
                    break;
            }
            string str = "yes";
            switch (str)
            {
                case "no":
                    Console.WriteLine("no");
                    break;
                case "yes":
                    Console.WriteLine("yes");
                    break;
            }

            char value = 'a';
            switch (value)
            {
                case 'a':
                    Console.WriteLine("a");
                    break;
                case 'b':
                    Console.WriteLine("b");
                    break;
            }
        }
    }
}
