using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndexOF
{
    class Program
    {
        static void Main(string[] args)
        {
            String str = "aimalnnnnnnnnn";
            String toFind = "n";
            int index = str.IndexOf("n");
            Console.WriteLine("Found '{0}' in '{1}' at position {2}",
                            toFind, str, index);
            str = str.Substring(0, str.IndexOf("n"));
            Console.WriteLine("'{0}'", str);
        }
    }
}
