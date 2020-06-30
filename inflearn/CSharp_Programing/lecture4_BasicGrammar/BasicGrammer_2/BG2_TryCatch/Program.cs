using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG2_TryCatch
{
    class Program
    {
        static void Main(string[] args)
        {
            //IndexOutOfRangeException
            int[] array = { 1, 2, 3 };
            try
            {
                array[3] = 10;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("배열 인덱스 에러 발생");
                Console.WriteLine(e.ToString());
                array[2] = 10;
            }

            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }
    }
}
