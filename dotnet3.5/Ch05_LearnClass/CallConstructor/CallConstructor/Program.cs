using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CallConstructor
{
    class Program
    {
        static void Main(string[] args)
        {
            Human Kim = new Human("김상형", 25);
            Kim.Intro();
            Human Lee = new Human("이순신");
            Lee.Intro();
        }
    }

    class Human
    {
        private string Name;
        private int Age;
        public Human(string Name, int Age) : this(Name)
        {
            this.Age = Age;
        }
        public Human(string Name)
        {
            this.Name = Name;
            Age = 1;
        }
        public void Intro()
        {
            Console.WriteLine("이름:" + Name);
            Console.WriteLine("나이:" + Age);
        }
    }
}
