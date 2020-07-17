using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeIndexer
{
    class Time
    {
        private int hour, min, sec;
        public Time(int h, int m, int s) { hour = h; min = m; sec = s; }
        public void OutTime()
        {
            Console.WriteLine("{0}시 {1}분 {2}초", hour, min, sec);
        }
        public int this[int what]
        {
            get
            {
                switch(what)
                {
                    case 0: return hour;
                    case 1: return min;
                    case 2: return sec;
                    default: return -1; //예외처리하는 것이 바람직하다.
                }
            }
            set
            {
                switch(what)
                {
                    case 0: hour = value; break;
                    case 1: min = value; break;
                    case 2: sec = value; break;
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
            Now[1] = 19;
            Console.WriteLine("분은 {0}입니다.", Now[1]);
        }
    }
}
