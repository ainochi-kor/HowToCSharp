using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickEvent
{
    public delegate void deleClick();
    class Button
    {
        public event deleClick Click;
        public void Draw()
        {
            Console.WriteLine("나는 버튼입니다.");
        }
        public void OnClick()
        {
            if (Click != null) Click();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("게임을 시작합니다.");
            for(int i = 0 ; i < 50; i++)
            {
                Console.Write('.');
                System.Threading.Thread.Sleep(50);
            }
            Console.WriteLine("\n게임 끝났다 치고.");
        }
        static void Main()
        {
            Button BtnStart = new Button();
            BtnStart.Draw();
            BthStart.Click += GameStart;
            Console.WriteLine("S:게임 시작, E:끝");
            for(;;)
            {
                if(Console.KeyAvailable)
                {
                    Console.WrtieLine
                }
            }
        }
    }
}
