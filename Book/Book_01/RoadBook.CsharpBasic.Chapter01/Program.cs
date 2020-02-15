using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadBook.CsharpBasic.Chapter01
{
    class Program
    {
        static void Main(string[] args)
        {
            //Examples 폴더 안에서 Hello 클래스를 불러들여 Run 기능을 수행하라.
            Examples.Hello hello = new Examples.Hello();
            hello.Run();
        }
    }
}
