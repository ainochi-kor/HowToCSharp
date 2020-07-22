using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarUnit
{
    class Unit
    {
        public virtual void Move() { Console.WriteLine("이동하다"); }
        public virtual void Attack() { Console.WriteLine("공격하다"); }
        public virtual void Die() { Console.WriteLine("죽는다"); }
    }
    class Marine : Unit
    {
        public override void Move() { Console.WriteLine("아장 아장"); }
        public override void Attack() { Console.WriteLine("두두두두"); }
        public override void Die() { Console.WriteLine("으아악"); }
    }
    class Tank : Unit
    {
        public override void Move() { Console.WriteLine("끼릭 끼릭"); }
        public override void Attack() { Console.WriteLine("빠방~쾅"); }
        public override void Die() { Console.WriteLine("슈우우(파란연기)"); } 
    }
    class Zealot : Unit
    {
        public override void Move() { Console.WriteLine("뒤뚱 뒤뚱"); }
        public override void Attack() { Console.WriteLine("퍽퍽퍽. 나 질럿이야"); }
        public override void Die() { Console.WriteLine("슈우우(파란연기)"); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Unit[] arUnit = { new Marine(), new Tank(), new Zealot() };
            for (int i = 0 ; i < arUnit.Length ; i++)
            {
                arUnit[i].Move();
            }
        }
    }
}
