using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO02
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "국어: 90 영어: 100 수학: 70";
            string[] str_Element = str.Split(new char[] { ' ' });
            int kor = int.Parse(str_Element[1]);
            int eng = int.Parse(str_Element[3]);
            int math = int.Parse(str_Element[5]);
            int total = kor + eng + math;
            float average = total / 3.0f;
            Console.WriteLine("{0} {1} {2} {3} {4}", kor, eng, math, total, Math.Round(average));
        }
    }
}
