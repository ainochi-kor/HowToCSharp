using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Constuctor
{
    class Program
    {
        static void Main(string[] args)
        {
            Human Kim;
            Kim = new Human("김상형", 25);
            Kim.Intro();
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

}
