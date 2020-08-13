using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace problem02
{
    class Program
    {
        static void Main(string[] args)
        {
            var nameCard = new { Name = "박성현", Age = 17 };
            Console.WriteLine("이름 : " + nameCard.Name + ", 나이 : " + nameCard.Age);

            var complex = new { Real = 3, Imaginary = -12 };
            Console.WriteLine("Read : " + complex.Real + ", Imaginary" + complex.Imaginary);
        }
    }
}
