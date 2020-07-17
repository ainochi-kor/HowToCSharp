using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeEqual
{
    class Time
    {
        private int hour, min, sec;
        public Time() { }
        public Time(int h, int m, int s) { hour = h; min = m; sec = s; }
        public void OutTime()
        {
            Console.WriteLine("현재 시간은 {0}시 {1}분 {2}초이다.", hour, min, sec);
        }
        public static bool operator == (Time A, Time B)
        {
            return (A.hour == B.hour && A.min == B.min && A.sec == B.sec);
        }
        public static bool operator != (Time A, Time B)
        {
            return !(A == B);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Time A = new Time(5, 5, 5);
            Time B = new Time(5, 5, 5);
            Time C = new Time(6, 6, 6);
            Console.WriteLine("A와 B는 {0}", (A==B ? "같다":"다르다"));
            Console.WriteLine("A와 C는 {0}",(A==C ? "같다":"다르다"));
        }
    }
}
