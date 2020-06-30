using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baekjoon_2884
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] str = Console.ReadLine().Split(' ');
            int[] num = new int[str.Length];
            for(int i = 0; i<str.Length; i++ )
            {
                str[i] = str[i].Trim();
                num[i] = Convert.ToInt32(str[i]);
            }
            num[1] -= 45;

            if (num[1] < 0)
            {
                num[0] -= 1;
                num[1] += 60;
            }
            if (num[0] < 0)
                num[0] += 24;

            Console.WriteLine("{0} {1}",num[0], num[1]);
            
        }
    }
}
