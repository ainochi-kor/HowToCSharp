using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baekjoon_1330
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] str = Console.ReadLine().Split(' ');
            int[] num = new num[];
            for (int i = 0; i < str.Length; i++)
            { 
                str[i] = str[i].Trim();
                num[i] = Convert.ToInt32(str[i]);
            }
            Console.WriteLine(str[0]);
            Console.WriteLine(str[1]);
        }
    }
}
