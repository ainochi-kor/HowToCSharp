using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICreature
{
    interface ICreature
    {
        void Propagate(); //생물은 번식한다.
    }
    interface IPlant : ICreature
    {
        void Flower(); //식물은 꽃을 피운다.
    }
    interface IAnimal : ICreature
    {
        void Move(); //동물은 이동한다.
    }
    class Lion : IAnimal
    {
        public void Move() { Console.WriteLine("왔다리 갔다리"); }
        public void Propagate() { Console.WriteLine("이걸 ?"); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Lion Simba = new Lion();
            Simba.Move();
        }
    }
}
