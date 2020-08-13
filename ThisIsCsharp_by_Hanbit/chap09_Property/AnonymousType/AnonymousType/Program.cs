using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnonymousType
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new { Name = "박상현", Age = 123 };
            Console.WriteLine("Name : {0}, Age : {1}", a.Name, a.Age);

            var b = new {Subject = "수학", Score = new int[] {90,80,70,60}};
            Console.Write("Subject : {0}, Score: ", b.Subject);
            foreach (var score in b.Score)
                Console.Write("{0} ", score);

            Console.WriteLine();
        }
    }
}
