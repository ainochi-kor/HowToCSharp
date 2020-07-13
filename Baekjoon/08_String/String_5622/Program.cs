using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String_5622
{
    class Program
    {
        static void Main(string[] args)
        {
            char[][] arr_str = new char[10][];
            for (int i = 0; i< arr_str.Length; i++)
            {
                if (i == 6 || i == 8)
                    arr_str[i] = new char[4];
                else
                    arr_str[i] = new char[3];
            }

            arr_str[0].' ';

                ['A','B','C'],['D','E','F'],['G','H','I'],['J','K','L'],
       ['M','N','O'],['P','Q','R','S'],['T','U','V'],['W','X','Y','Z']]
str = input()

a_time = 0
for k in range(len(str)) :
    d_time = 0
    for i in range(len(num)) :
        d_time += 1
        if str[k] in num[i]:
            a_time += d_time+1

print(a_time)
        }
    }
}
