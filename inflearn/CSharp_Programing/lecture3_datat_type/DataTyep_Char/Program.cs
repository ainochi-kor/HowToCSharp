using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTyep_Char
{
    class Program
    {
        static void Main(string[] args)
        {
            char Mun = '7'; //문자 상수값 = 55
            int Num = Mun;
            Num = Num + 1;
            Console.WriteLine("Mun = {0} 문자상수값 = {1} 유니코드문자 = {2}", (int)Mun, Num, (char)Num);
            Mun = (char)Num;
            Console.WriteLine(Mun);
        }
    }
}
