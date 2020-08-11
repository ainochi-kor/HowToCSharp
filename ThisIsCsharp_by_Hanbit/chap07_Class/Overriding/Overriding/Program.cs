using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Overriding
{
    class ArmorSuite
    {
        public virtual void Initalize()
        {
            Console.WriteLine("Armored");
        }
    }

    class IronMan : ArmorSuite
    {
        public override void Initalize()
        {
            base.Initalize();
            Console.WriteLine("Repulsor Rays Armed");
        }
    }

    class WarMachine : ArmorSuite
    {
        public override void Initalize()
        {
            base.Initalize();
            Console.WriteLine("Double-Barrel Connons Armed");
            Console.WriteLine("Micro-Rocket Launcher Armed");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating ArmorSuite...");
            ArmorSuite armorsuite = new ArmorSuite();
            armorsuite.Initalize();

            Console.WriteLine("\nCreating IronMan...");
            ArmorSuite ironman = new IronMan();
            ironman.Initalize();

            Console.WriteLine("\nCreating WarMachine...");
            ArmorSuite warmachine = new WarMachine();
            warmachine.Initalize();
        }
    }
}
