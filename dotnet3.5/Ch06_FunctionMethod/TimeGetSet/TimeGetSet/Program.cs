using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeGetSet
{
    class Time
    {
        private int hour, min, sec;
        public Time(int h, int m, int s) { SetHour(h); SetMin(m); SetSec(s); }
        public int GetHour(){return hour;}
        public void SetHour(int hour){ if (hour < 24) this.hour = hour; }
        public int GetMin(){ return min; }
        public void SetMin(int min) { if (min < 60) this.min = min; }
        public int GetSec() { return sec; }
        public void SetSec(int sec) { if (sec < 60) this.sec = sec; }
        public void OutTime()
        {
            Console.WriteLine("현재 시간은 {0}시 {1}분 {2}초이다,", hour, min, sec);
        }
 
    }
    class Program
    {
        static void Main(string[] args)
        {
            Time Now;
            Now = new Time(12, 30, 45);
            Now.OutTime();
            Now.SetHour(55);
            Now.OutTime();
        }
    }
}
