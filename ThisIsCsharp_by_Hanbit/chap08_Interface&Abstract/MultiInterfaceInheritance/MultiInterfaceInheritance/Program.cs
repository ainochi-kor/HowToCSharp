using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiInterfaceInheritance
{
    interface IRunnable
    {
        void Run();

    }

    interface IFlyable
    {
        void Fly();
    }

    class FlyingCar : IRunnable, IFlyable
    {
        public void Run()
        {
            Console.WriteLine("Run! Run!");
        }

        public void Fly()
        {
            Console.WriteLine("Fly! Fly!");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            FlyingCar car = new FlyingCar();
            car.Run();
            car.Fly();

            IRunnable runable = car as IRunnable;
            runable.Run();

            IFlyable flyable = car as IFlyable;
            flyable.Fly();

        }
    }
}
