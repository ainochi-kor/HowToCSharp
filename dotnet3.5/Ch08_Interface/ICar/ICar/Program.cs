using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICar
{
    interface ICar
    {
        void Run();
        void Back();
    }

    class Sedan : ICar
    {
        public virtual void Run() { Console.WriteLine("부웅 부웅"); }
        public virtual void Back() { Console.WriteLine("띠리리리리띠리리리리ㅣㅣ리"); }
    }

    abstract class Suv : ICar
    {
        public virtual void Run() { Console.WriteLine("덜덜덜덜"); }
        public abstract void Back();
        public abstract void Run4Wd();
    }
    class Grandeur : Sedan
    {
        public override void Run() { Console.WriteLine("달려도 소리가 안들림");}
        public void Booking() { Console.WriteLine("야 타");}
    }
    class Matiz : Sedan
    {
        public void FrogParking() { Console.WriteLine("아무리 좁아도 주차 가능");}
    }
    class Korando : Suv
    {
        public override void Back() { Console.WriteLine("뒤에 전부 비켜"); }
        public override sealed void Run4Wd() { Console.WriteLine("4륜 구동");}
    }
    class Program
    {
        public static void CarTest(ICar Car)
        {
            Car.Run();
            Car.Back();
        }
        static void Main(string[] args)
        {
            ICar MyCar = new Grandeur();
            ICar YourCar = new Matiz();
            CarTest(MyCar);
            CarTest(YourCar);
        }
    }
}
