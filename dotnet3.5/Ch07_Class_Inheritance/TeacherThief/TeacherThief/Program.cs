using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeacherThief
{
    class Human
    {
        protected string Name;
        protected int Age;
        public Human(String Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }
    }

    class Teacher : Human
    {
        protected string Subject;
        public Teacher(string Name, int Age, string Subject):base(Name, Age)
        {
            this.Subject = Subject;
        }
        public void Teach()
        {
            Console.WriteLine("얘들아 공부 열심히 해라");
        }
    }
    class Thief : Human
    {
        protected int Career;
        public Thief(string Name, int Age, int Career)
            :base(Name, Age)
        {
            this.Career = Career;
        }
        public void Steal()
        {
            Console.WriteLine("오늘은 어디를 털어 볼까?");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Teacher Eng = new Teacher("박미영", 35, "영어");
            Thief KangDo = new Thief("야월담", 25, 3);
            Eng.Teach();
            KangDo.Steal();
        }
    }
}
