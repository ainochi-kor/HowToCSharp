using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewVideo
{
    delegate void Notice(string Movie);

    class Mania
    {
        private int id;
        public Mania(int id)
        {
            this.id = id;
        }
        public void RendMe(string Movie)
        {
            Console.WriteLine("{0}번 고객이 {1}을 빌려간다.", id, Movie);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Mania[] arMania = new Mania[10];
            for (int i = 0; i < 10; i++)
            {
                arMania[i] = new Mania(i);
            }
            Notice RentCall = null;
            RentCall += arMania[1].RendMe;
            RentCall += arMania[4].RendMe;
            RentCall += arMania[6].RendMe;
            RentCall += arMania[9].RendMe;

            RentCall("염소 부인");
        }
    }
}
