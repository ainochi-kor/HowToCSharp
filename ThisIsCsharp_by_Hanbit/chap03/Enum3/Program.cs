using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum3
{
    class Program
    {
        enum DialogResult { YES = 10, NO, CANSEL, CONFIRM = 50, OK };
        static void Main(string[] args)
        {

                Console.WriteLine((int)DialogResult.YES);
                Console.WriteLine((int)DialogResult.NO);
                Console.WriteLine((int)DialogResult.CANSEL);
                Console.WriteLine((int)DialogResult.CONFIRM);
                Console.WriteLine((int)DialogResult.OK);
        }
    }
}
