using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalFunction
{
    class Program
    {
        //Don't use localFunction in visual studio 2010
        static string ToLowerString(string input)
        {
            var arr = input.ToCharArray();
            for(int i = 0 ; i < arr.Length;i++)
            {
                arr[i] = ToLowerChar(i);
            }

            char ToLowerChar(int i)
            {
                if (arr[i] < 65 || arr[i] > 90)
                    return arr[i];
                else
                    return (char)(arr[i] + 32);
            }

            return new string(arr);
        }
        static void Main(string[] args)
        {
            Console.WriteLine(ToLowerString("Hello!"));
            Console.WriteLine(ToLowerString("Good Morning"));
            Console.WriteLine(ToLowerString("This is C#"));

        }
    }
}
