using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeakJoon_2908
{
    class Program
    {
        string num1 { get; }
        static void Main(string[] args)
        {
            string[] str = Console.ReadLine().Split();
            string num1 ="";
            string num2 ="";

            char temp;
            for(int i=0; i<str[0].Length;i++)
            {
                num1 += str[0][str[0].Length-i-1];
            }

            for (int i = 0; i < str[1].Length; i++)
            {
                num2 += str[1][str[1].Length - i - 1];
            }

            if (Convert.ToInt32(num1) > Convert.ToInt32(num2))
                Console.WriteLine(num1);
            else
                Console.WriteLine(num2);
        }
    }
}
