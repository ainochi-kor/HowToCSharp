using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakjoon_10869
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] token;
            int[] num = new int[2];
            int sum, minus, multi, div, remain;

            string str = Console.ReadLine();

            token = str.Split(' ');
            for (int i = 0; i < token.Length; i++)
                token[i].Trim();
            for (int i = 0; i < token.Length; i++)
                num[i] = Convert.ToInt32(token[i]);

            Console.WriteLine(num[0] + num[1]);
            Console.WriteLine(num[0] - num[1]);
            Console.WriteLine(num[0] * num[1]);
            Console.WriteLine(num[0] / num[1]);
            Console.WriteLine(num[0] % num[1]);
        }
    }
}
