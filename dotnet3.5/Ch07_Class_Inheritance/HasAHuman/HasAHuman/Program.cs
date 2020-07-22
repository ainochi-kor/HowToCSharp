using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HasAHuman
{
    class Human
    {
        private string Name;
        private int Age;
        public Human(string Name,int Age)
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
    class Student
    {
        public Human H;
        protected int StNum;
        public Student(string Name, int Age, int StNum)
        {
            this.StNum = StNum;
            H = new Human(Name, Age);
        }

        public void Intro()
        {
            H.Intro();
            Console.WriteLine("학번:" + StNum);
        }
        public void Study()
        {
            Console.WriteLine("하늘 천 따지 검을 현 누를 황");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Student Kim;
            Kim = new Student("김상형", 25, 8906299);
            Kim.Intro();
            Kim.Study();
        }
    }
}
