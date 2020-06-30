using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Smart_RiceMaker01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 40);
            Random rand = new Random();
            ConsoleColor[] Color =
            {
                ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Red, ConsoleColor.Yellow,
                ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Green
            };

            while(true)
            {
                Console.Clear();
                {
                    Console.ForegroundColor = Color[rand.Next(7)];
                    Console.SetCursorPosition(rand.Next(100), rand.Next(40));
                    Console.Write("Hello World");
                }
                Thread.Sleep(200);
            }
        }
    }
}
