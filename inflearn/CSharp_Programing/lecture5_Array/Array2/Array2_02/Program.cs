using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array2_02
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][][] array3 = new int[2][][];
            array3[0] = new int[2][];
            array3[1] = new int[3][];

            array3[0][0] = new int[3] { 1, 2, 3 };
            array3[0][1] = new int[2] { 4, 5 };

            array3[1][0] = new int[3] { 6, 7, 8 };
            array3[1][1] = new int[2] { 9, 10 };
            array3[1][2] = new int[2] { 11, 12 };

            for(int i = 0; i < array3.Length; i++)
            {
                for(int j = 0; j < array3[i].Length; j++)
                {
                    for (int k = 0; k < array3[i][j].Length; k++)
                    {
                        Console.Write("{0} ", array3[i][j][k]);
                    }
                }
            }
        }
    }
}
