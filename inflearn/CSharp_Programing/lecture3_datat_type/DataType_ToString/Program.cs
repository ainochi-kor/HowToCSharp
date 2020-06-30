using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType_ToString
{
    class Program
    {
        /*
         * ToString()
         * 
         * 기본데이터형.Parse()
         * Convert.ToInt32()
         * Convert.ToSingle() // float형임.
         * Convert.ToXXXXX()
         */
        static void Main(string[] args)
        {
            int value1 = 127; //int형 생성
            string str1 = value1.ToString(); //문자열로 바꿈
            Console.WriteLine(str1); // 출력

            int value2 = Convert.ToInt32(str1); //문자열을 정수형으로 바꿈
            Console.WriteLine(value2);

            string str2 = "3.14"; //문자열 생성
            float value3 = float.Parse(str2); //float형으로 바꿈.
            Console.WriteLine(value3);
        }
    }
}
