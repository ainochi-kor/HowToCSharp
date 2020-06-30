using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baekjoon_10430
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] token;
            int[] num = new int[3];
            int A, B, C;
            string str = Console.ReadLine();
            
            token = str.Split(' ');
            
            for (int i = 0; i < token.Length; i++)
                token[i].Trim();
            for (int i = 0; i < token.Length; i++)
                num[i] = Convert.ToInt32(token[i]);
            
            A = num[0];
            B = num[1]; 
            C = num[2];

            Console.WriteLine((A + B) % C);
            Console.WriteLine(((A % C) + (B % C)) % C);
            Console.WriteLine((A * B) % C);
            Console.WriteLine(((A % C) * (B % C)) % C);
        }
    }
}
