using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeleToMember
{
    delegate void dele(string s);

    class Program
    {
        public static void m1(string s) { Console.WriteLine(s); }
        public void m2(string s) { Console.WriteLine(s); }
        static void Main(string[] args)
        {
            dele d = m1;
            d("정적 메서드");
            Program CS = new Program();
            d = CS.m2;
            d("인스턴스 메서드");

            d = Outer.m3;
            d("외부 정적 메서드");
            Outer O = new Outer();
            d = O.m4;
            d("외부 인스턴스 메소드");

        }
    }
    class Outer
    {
        public static void m3(string s) { Console.WriteLine(s); }
        public void m4(string s) { Console.WriteLine(s); }
    }
}
