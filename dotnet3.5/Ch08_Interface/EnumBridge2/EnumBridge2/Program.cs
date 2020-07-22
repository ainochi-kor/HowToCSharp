using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace EnumBridge2
{
    class HanBridge
    {
        static string[] Bridge = {"팔당","강동","광진","천호","올림픽","잠실",
                                     "청담","영동","성수","동호","한남","반포","동작","한강","원효",
                                     "마포","서강","양화","성산","가양","방화","행주","김포","일산"};

        public IEnumerable EastToWest()
        {
            for (int i = 0; i < Bridge.Length; i++)
            {
                yield return Bridge[i];
            }
        }
        public IEnumerable WestToEast()
        {
            for(int i = Bridge.Length-1 ; i >= 0 ; i--)
            {
                yield return Bridge[i];
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            HanBridge hb = new HanBridge();
            foreach (string b in hb.EastToWest())
            {
                Console.WriteLine(b);
            }
            foreach(string b in hb.WestToEast())
            {
                Console.WriteLine(b);
            }
        }
    }
}
