using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Birth
{
    class Program
    {
        static void Main(string[] args)
        {
            Human Kim;
            Kim = new Human("김상형", 1980, 1, 2);
            Kim.Intro();
        }
    }
    class Human
    {
        private class Date
        {
            int year, mon, day;
            public Date(int year, int mon, int day)
            {
                this.year = year;
                this.mon = mon;
                this.day = day;
            }
            public void OutDate()
            {
                Console.WriteLine("{0}년 {1}월 {2}일", year, mon, day);
            }
        }
        private string Name;
        private Date Birth;
        public Human(string Name, int year, int mon, int day)
        {
            this.Name = Name;
            this.Birth = new Date(year, mon, day);
        }
        public void Intro()
        {
            Console.WriteLine("이름:" + Name);
            Console.Write("생년월일:");
            Birth.OutDate();
        }
    }
}
