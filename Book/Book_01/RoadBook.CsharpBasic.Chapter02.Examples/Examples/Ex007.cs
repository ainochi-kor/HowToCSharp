using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadBook.CsharpBasic.Chapter02.Examples
{
    public class Ex007
    {
        public void Run()
        {
            int korean = 100;
            int english = 100;
            int mth = 98;
            int sport = 97;

            int totalScore = korean + english + mth + sport;

            Console.WriteLine("성적 총점{0}", totalScore);
            Console.WriteLine("평균{0}", totalScore / 4);
        }
    }
}
