using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUnitEnergy
{
    interface IUnit
    {
        int Energy{ get; }
    }

    class Marine : IUnit{
        private int Life = 100;
        public int Energy
        {
            get { return Life; }
        }
    }

    class Zealot : IUnit{
        private int Shield = 50;
        private int Life = 50;
        public int Energy
        {
            get { return Shield + Life; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Marine M = new Marine();
        }
    }
}
