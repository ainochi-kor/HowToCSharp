using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace problem05
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable ht = new Hashtable();

            ht["회사"] = "Microsoft";
            ht["URL"] = "www.microsoft.com";

            Console.WriteLine("회사 : {0} ", ht["회사"]);
            Console.WriteLine("URL : {0} ", ht["URL"]);
        }
    }
}
