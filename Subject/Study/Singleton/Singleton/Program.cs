using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton s1 = Singleton.Instance();
            Singleton s2 = Singleton.Instance();

            if(s1 == s2)
            {
                Console.WriteLine("Objects are the same instance");
            }

            //Wiat for user
            Console.ReadKey();

        }
    }

    //싱글톤 클래스
    class Singleton
    {
        private static Singleton _instance;

        // 'protected'로 생성자를 만듦
        protected Singleton()
        { }

        // 'static'으로 메서드 생성 
        public static Singleton Instance()
        {
            //다중쓰레드에서는 정상적으로 동작안하는 코드.
            //다중 쓰레드의 경우 동기화가 필요.
            if(_instance == null)
            {
                _instance = new Singleton();
            }

            ////다중 쓰레드 환경일경우 Lock 필요
            //if (_instance == null)
            //{
            //    lock(_instance)
            //    {
            //        _instance = new Singleton();
            //    }
            //}

            return _instance;
        }
    }
}
