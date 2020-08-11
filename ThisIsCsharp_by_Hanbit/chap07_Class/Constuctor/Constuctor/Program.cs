using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Constuctor
{
    class Cat
    {
        public string _name;
        public string _color;

        public Cat()
        {
            _name = "";
            _color = "";
        }

        public Cat(string name, string color)
        {
            _name = name;
            _color = color;
        }

        ~Cat()
        {
            Console.WriteLine("{0} : 잘가", _name);
        }

        public void Meow()
        {
            Console.WriteLine("{0} : 야옹", _name);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Cat kitty = new Cat("키티", "하얀색");
            kitty.Meow();
            Console.WriteLine("{0} : {1}", kitty._name, kitty._color);

            Cat nero = new Cat("네로", "검은색");
            nero.Meow();
            Console.WriteLine("{0} : {1}", nero._name, nero._color);
        }
    }
}
