using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUnit
{
    interface IUnit
    {
        void Move(int x, int y);
        void Attack(int x, int y);
        void Die();
    }

    class Marine : IUnit
    {
        public void Move(int x, int y) { Console.WriteLine("아장 아장"); }
        public void Attack(int x, int y) { Console.WriteLine("두두두두"); }
        public void Die() { Console.WriteLine("으아악"); } 
    }
    class SCV : IUnit
    {

        public void Move(int x, int y) { Console.WriteLine("데굴 데굴"); }
        public void Attack(int x, int y) { Console.WriteLine("찌직 찌직"); }
        public void Die() { Console.WriteLine("펑~꽥꼬닥"); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Marine M = new Marine();
            M.Move(5, 5);
            M.Attack(10, 10);
            M.Die();
        }
    }
}
