using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimePlusInt
{
    class Time
    {
        private int hour, min, sec;
        public Time() { }
        public Time(int h, int m, int s) { hour = h; min = m; sec = s; }
        public void OutTime()
        {
            Console.WriteLine("현재 시간은  {0}시 {1}분 {2}초이다.", hour, min, sec);
        }

        public static Time operator+(Time A, int s)
        {
            Time T = new Time();
            T.hour = A.hour;
            T.min = A.min;
            T.sec = A.sec;
            T.sec += s;

            T.min += T.sec / 60;
            T.sec %= 60;
            T.hour += T.min / 60;
            T.min %= 60;
            return T;
        }
        public static Time operator +(int s, Time A)
        {
            return A + s;
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            Time A = new Time(1, 1, 1);
            A = A + 1;
            A.OutTime();
            A = 1 + A;
            A.OutTime();
            A += 2;
            A.OutTime();
        }
    }
}
