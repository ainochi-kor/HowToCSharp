using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace problem02_Matrix_Multiplication
{   
    class Program
    {
        static void Main(string[] args)
        {
            int[,] A2x2 = { { 3, 2 }, { 1, 4 } };
            int[,] B2x2 = { { 9, 2 }, { 1, 7 } };

            int a = 0, c = 0;
            int b, d;

            for(int i = 0 ; i < A2x2.GetLength(0) ; i++)
            {
                b = 0; d = 0;
                for(int j = 0 ; j < A2x2.GetLength(1) ; j++)
                {   
                   Console.Write(A2x2[0, a] * B2x2[b, 0] + A2x2[1, c] * B2x2[d, 1] +" ");
                    b = 1; d = 1;
                }
                Console.WriteLine();
                a = 1; c = 1;
            }
        }
    }
}
