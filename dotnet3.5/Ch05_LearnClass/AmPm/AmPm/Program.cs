using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmPm
{
    class Time
    {
        private int hour, min, sec;
        private static bool UseAmPm;
        public Time(int h, int m, int s)
        {
            hour = h; min = m; sec = s;
        }
        static Time() 
        {
            UseAmPm = false;
        }
        public static void SetAmPm(bool bAmPm)
        {
            UseAmPm = bAmPm;
        }
        public void OutTime()
        {
            int h;
            string AmPm;
            if(UseAmPm == true)
            {
                if(hour < 12) 
                {
                    AmPm = "오전"; h = hour; 
                }
                else
                {
                    AmPm = "오후"; h = hour - 12;
                }
                Console.WriteLine("현재 시간은 {3} {0}시 {1}분 {2}초이다.",h, min, sec, AmPm);
            }
            else
            {
                Console.WriteLine("현재 시간은 {0}시 {1}분 {2}초이다.", hour, min, sec);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Time Now = new Time(18, 25, 55);
            Time Then = new Time(20, 30, 10);
            Now.OutTime();
            Then.OutTime();
            Time.SetAmPm(true);
            Now.OutTime();
            Then.OutTime();
        }
    }
}
