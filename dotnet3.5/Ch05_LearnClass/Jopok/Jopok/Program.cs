using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jopok
{
    class Program
    {
        static void Main(string[] args)
        {
            Jopok NalaliPa;
            NalaliPa = new Jopok("김상형", 25, 500);
            NalaliPa.Intro();
        }
    }

    class Human
    {
        private string Name;
        private int Age;
        public Human(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }
        public void Intro()
        {
            Console.WriteLine("이름:" + Name);
            Console.WriteLine("나이:" + Age);
        }
    }
    class Jopok
    {
        readonly private Human Dumok;
        private int Jolgae;
        public Jopok(string Name, int Age, int Jolgae)
        {
            Dumok = new Human(Name, Age);
            this.Jolgae = Jolgae;
        }
        public void Intro()
        {
            Console.WriteLine("대빵 신상 명세");
            Dumok.Intro();
            Console.WriteLine("조직원:{0}명", Jolgae);
        }
    }
}
