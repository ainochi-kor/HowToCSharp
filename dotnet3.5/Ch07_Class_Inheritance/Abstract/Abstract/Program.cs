using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abstract
{
    abstract class Animal
    {
        public abstract void Sound();
    }

    class Dog : Animal
    {
        public override void Sound() { Console.WriteLine("멍멍"); }
    }
    class Cow : Animal
    {
        public override void Sound() { Console.WriteLine("음메"); }
    }
    class Cat : Animal
    {
        public override void Sound() { Console.WriteLine("야옹"); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Animal A; 
            // A = new Animal();
            A = new Dog(); A.Sound();
            A = new Cow(); A.Sound();
            A = new Cat(); A.Sound();
        }
    }
}
