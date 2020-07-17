using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _002_Partial
{
    class Program
    {
        partial class Human
        {
            public string Name;
            public int Age;
            
        }

        partial class Human
        {
            public void Intro()
            {
                Console.WriteLine("이름:" + Name);
                Console.WriteLine("나이:" + Age);
            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                Human Kim;
                Kim = new Human();
                Kim.Name = "김상형";
                Kim.Age = 25;
                Kim.Intro();
            }
        }
    }
}
