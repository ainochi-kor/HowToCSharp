using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindBook
{
    class Program
    {
        static string FindBook(string name)
        {
            return "윈도우즈 API 정복";
        }
        static string FindBook(int isbn)
        {
            return "지 혼자 연구하는 C/C++";
        }
        static string FindBook(string author, int year, int month)
        {
            return "추락하는 것은 날개가 있을까?";
        }
        static void Main(string[] args)
        {
            Console.WriteLine(FindBook("정복"));
            Console.WriteLine(FindBook(899569291));
            Console.WriteLine(FindBook("임운열", 1980, 1));
        }
    }
}
