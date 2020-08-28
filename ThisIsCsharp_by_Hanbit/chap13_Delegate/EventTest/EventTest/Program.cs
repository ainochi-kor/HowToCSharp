using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventTest
{
    delegate void EventHandler(string message);

    class MyNotifer
    {
        public event EventHandler SomethingHappened;
        public void DoSomeThing(int number)
        {
            int temp = number % 10;
 
            if(temp != 0 && temp % 3 == 0)
            {
                SomethingHappened(String.Format("{0} : 짝", number)); 
            }
        }
    }
    class Program
    {
        static public void MyHandler(string message)
        {
            Console.WriteLine(message);
        }
        static void Main(string[] args)
        {
            MyNotifer notifier = new MyNotifer();
            notifier.SomethingHappened += new EventHandler(MyHandler);

            for(int i = 1 ; i < 30 ; i++)
            {
                notifier.DoSomeThing(i);
            }
        }
    }
}
