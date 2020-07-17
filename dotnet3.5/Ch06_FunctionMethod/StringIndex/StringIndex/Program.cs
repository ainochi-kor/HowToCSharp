using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringIndex
{
    class Time
    {
        private int hour, min, sec;
        public Time(int h, int m, int s) { hour = h; min = m; sec = s; }
        public void OutTime()
        {
            Console.WriteLine("{0}시 {1}분 {2}초", hour, min, sec);
        }
        public int this[string what]
        {
            get
            {
                switch (what)
                {
                    case "시": return hour;
                    case "분": return min;
                    case "초": return sec;
                    default: return -1; //예외처리 해야함.
                }
            }
            set
            {
                switch (what)
                {
                    case "시": hour = value; break;
                    case "분": min = value; break;
                    case "초": sec = value; break;
                    default: break;
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Time Now = new Time(12, 34, 56);
            Now.OutTime();
            Now["분"] = 19;
            Console.WriteLine("분은 {0}입니다.", Now["분"]);
        }
    }
}
