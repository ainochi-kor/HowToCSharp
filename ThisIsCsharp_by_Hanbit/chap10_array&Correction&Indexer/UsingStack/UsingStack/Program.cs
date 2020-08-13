using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace UsingStack
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack stack = new Stack();

            for (int i = 1; i < 6; i++)
                stack.Push(i);

            while (stack.Count > 0)
                Console.WriteLine(stack.Pop());
        }
    }
}
