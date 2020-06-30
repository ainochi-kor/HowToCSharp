using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType_StandardInput_KeyChar
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo KeyInfo;
            do
            {
                KeyInfo = Console.ReadKey(true);
                if (KeyInfo.KeyChar == 'a')
                    Console.WriteLine("a가 눌렸어");
                    //Console.Write(KeyInfo.KeyChar);
            } while (KeyInfo.Key != ConsoleKey.Escape);
        }
    }
}
