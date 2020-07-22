using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiliInch
{
    class Inch
    {
        public double len;
        public Inch(double len) { this.len = len; }
        public void OutValue() { Console.WriteLine("{0:F2} 인치", len); }
        public static implicit operator Mili(Inch i)
        {
            return new Mili((uint)(i.len * 25.4));
        }
        public static implicit operator Inch(Mili m)
        {
            return new Inch(m.len / 25.4);
        }
    }
    class Mili
    {
        public uint len;
        public Mili(uint len) { this.len = len; }
        public void OutValue() { Console.WriteLine("{0}밀리", len); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Inch disk = new Inch(0);
            Mili shoes = new Mili(0);

            disk.len = 3.5;
            shoes = disk;
            shoes.OutValue();

            shoes.len = 275;
            disk = shoes;
            disk.OutValue();
        }
    }
}
