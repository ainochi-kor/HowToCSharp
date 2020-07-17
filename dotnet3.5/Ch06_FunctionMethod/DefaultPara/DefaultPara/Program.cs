using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultPara
{
    class Program
    {
        public static void PrintMessage(string m, int n, bool bBeep)
        {
            for(int i = 0; i < n ; i++)
            {
                Console.WriteLine(m);
            }
            if(bBeep)
            {
                Console.Beep();
            }
        }

        public static void PrintMessage()
        {
            PrintMessage("디폴트 메시지", 1, false);
        }
        public static void PrintMessage(string m)
        {
            PrintMessage(m, 1, false);
        }
        public static void PrintMessage(string m, int n)
        {
            PrintMessage(m, n, false);
        }
        public static void PrintMessage(string m, bool bBeep)
        {
            PrintMessage(m, 1, bBeep);
        }
        static void Main(string[] args)
        {
            PrintMessage("메시지입니다.", 1, false);
            PrintMessage("중요한 메시지.", 3, true);
            PrintMessage("소리와 함께 출력되는 메시지", true);
            PrintMessage();
        }
    }
}
