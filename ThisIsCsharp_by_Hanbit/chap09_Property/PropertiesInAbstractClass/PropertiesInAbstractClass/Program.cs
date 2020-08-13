using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PropertiesInAbstractClass
{
    abstract class Product
    {
        private static int serial = 0;
        public string SerialID
        {
            get { return String.Format("{0:d5}", serial++); }
        }

        abstract public DateTime ProductDate
        {
            get;
            set;
        }
    }

    class MyProduct : Product
    {
        public override DateTime ProductDate
        {
            get;
            set;
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            Product product_1 = new MyProduct()
            {
                ProductDate = new DateTime(2018, 1, 10)
            };

            Console.WriteLine("Product:{0}, Product Date :{1}", product_1.SerialID, product_1.ProductDate);

            Product product_2 = new MyProduct()
            {
                ProductDate = new DateTime(2018, 2, 3)
            };
            Console.WriteLine("Product:{0}, Product Date :{1}", product_2.SerialID, product_2.ProductDate);
        }
    }
}
