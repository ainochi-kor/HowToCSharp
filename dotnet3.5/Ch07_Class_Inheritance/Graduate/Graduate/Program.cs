using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graduate
{
    class Human
    {
        protected string Name;
        protected int Age;
        public Human (string Name,int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }
    }

    class Student : Human
    {
        protected int StNum;
        public Student(string Name, int Age, int StNum)
        :base(Name, Age)
        {
            this.StNum = StNum;
        }
        public void Study()
        {
            Console.WriteLine("23은 6, 24 8, 25 10, 26은 12...");
        }
    }

    class Graduate : Student
    {
        protected string Major;
        public Graduate(string Name, int Age, int StNum, string Major) 
            : base(Name, Age, StNum)
        {
            this.Major = Major;
        }
        public void WriteThesis()
        {
            Console.WriteLine("서론, 본론, 결론 어쩌고 저쩌고");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Graduate Park = new Graduate("박미영", 32, 9101223, "영문학");
            Park.WriteThesis();
        }
    }
}
