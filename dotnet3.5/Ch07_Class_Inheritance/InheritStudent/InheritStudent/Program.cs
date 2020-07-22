using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InheritStudent
{
    class Human
    {
        protected string Name;
        protected int Age;
        public Human (string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }
        public virtual void Intro()
        {
            Console.WriteLine("이름:" + Name);
            Console.WriteLine("나이:" + Age);
        }
    }
    class Student : Human
    {
        protected int StNum;
        public Student(string Name, int Age, int StNum) : base(Name, Age)
        {
            this.StNum = StNum;
        }
        public override void Intro()
        {
            base.Intro();
            Console.WriteLine("학번: " + StNum);
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
