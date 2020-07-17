using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NestedType
{
    class Program
    {
        static void Main(string[] args)
        {
            Time Now = new Time(12, 34, 56);
            Now.OutTime();
        }
    }
    class Time
    {
        private class Int60
        {
            private int Integer;
            public Int60(int Value)
            {
                this.Value = Value;
            }
            public int Value
            {
                get { return Integer; }
                set { if (value >= 0 && value < 60) Integer = value; }
            }
        }
        private int hour;
        private Int60 min, sec;
        public Time(int hour, int min, int sec)
        {
            this.hour = hour;
            this.min = new Int60(min);
            this.sec = new Int60(sec);
        }
        public void OutTime()
        {
            Console.WriteLine("{0}시 {1}분 {2}초", hour, min.Value, sec.Value);
        }
    }
}
