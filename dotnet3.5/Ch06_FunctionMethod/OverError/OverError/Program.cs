using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OverError
{
    class Time
    {
        public int hour, min, sec;
        public Time() { }
        public Time(int h, int m, int s) { hour = h; min = m; sec = s; }
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
        static void Method(int a, Time b) { Console.WriteLine("int, Time"); }
        static void Method(Time a, int b) { Console.WriteLine("Time, int"); }
        static void Main(string[] args)
        {
            Time Now = new Time(12, 34, 56);
            int i = 1234;

            Method(i, Now);
            Method(Now, i);
            //Method(Now, Now);
            //Method(i, i);
        }
    }
}
