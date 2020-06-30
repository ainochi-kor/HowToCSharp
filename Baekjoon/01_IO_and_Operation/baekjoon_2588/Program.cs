using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baekjoon_2588
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] token;
            int first_num, sum=0;
            string scond_num;
            int[] num;
            
            first_num = Convert.ToInt32(Console.ReadLine());
            scond_num = Console.ReadLine();

            num = new int[scond_num.Length];
            for (int i = 0; i < scond_num.Length; i++)
            {
                num[i] = first_num * (Convert.ToInt32(scond_num[scond_num.Length - i - 1])-48);
                //Console.WriteLine(Convert.ToInt32(scond_num[scond_num.Length - i - 1]));
                Console.WriteLine(num[i]);
                sum += num[i] * ((int)Math.Pow(10, i));
                //Console.WriteLine(sum);
            }
            Console.WriteLine(sum);
        }
    }
}
