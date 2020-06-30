using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array2_03
{
    class Program
    {
        static void TransArray(string[] arr)
        {
            string[] HangulDays = { "일", "월", "화", "수", "목", "금", "토" };
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = HangulDays[i];
            }

        }
        static void Main(string[] args)
        {
            //일차원 배열을 함수로 전달.
            string[] Days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            TransArray(Days);
            foreach (string str in Days)
                Console.Write(str);
        }
    }
}
