using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeInt
{
    class Time
    {
        public int hour, min, sec;
        public Time() { }
        public Time(int h, int m, int s) { hour = h; min = m; sec = s; }
        public void OutTime()
        {
            Console.WriteLine("현재 시간은 {0}시 {1}분 {2}초이다.", hour, min, sec);
        }
        public static implicit operator int(Time T)
        {
            return T.hour * 3600 + T.min * 60 + T.sec;
        }
        public static explicit operator Time(int abs)
        {
            Time T = new Time();
            T.hour = abs / 3600;
            T.min = (abs / 60) % 60;
            T.sec = abs % 60;
            return T;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Time Now = new Time(12, 30, 40);
            Now.OutTime();
            int absec = Now;
            Console.WriteLine("절대초 = {0}", absec);
            Time Then = new Time();
            Then = (Time)12345;
            Then.OutTime();
        }
    }
}
