using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadBook.CsharpBasic.Chapter02.Examples
{
    class Ex008
    {
        public void Run()
        {
            int korean = 100;
            int english = 100;
            int math = 98;
            int sport = 97;

            int totalScore = korean + english + math + sport;

            Console.WriteLine("성적 총점 {0}", totalScore);
            Console.WriteLine("평균{0}",(double)totalScore / 4);
        }
    }
}
