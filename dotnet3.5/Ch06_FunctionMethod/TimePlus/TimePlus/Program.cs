using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimePlus
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
        public static Time operator+(Time A, Time B)
        {
            Time T = new Time();
            T.hour = A.hour + B.hour;
            T.min = A.min + B.min;
            T.sec = A.sec + B.sec;

            T.min += T.sec / 60;
            T.sec %= 60;
            T.hour += T.min / 60;
            T.min %= 60;
            return T;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Time A = new Time(1, 1, 1);
            Time B = new Time(2, 2, 2);
            Time C;
            C = A + B;
            C.OutTime();
        }
    }
}
