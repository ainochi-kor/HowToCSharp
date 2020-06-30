using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType_StandardInput
{
    /*
     * Console.ReadKey()
     * 사용자가 눌린 키 한 문자 정보를 리턴하는 메서드
     * 함수 원형
     * public static ConsoleKeyInfo ReadKey()
     * public static ConsoleKeyInfo ReadKey
     * (bool intercept) true : 화면 출력 안 함 / false : 화면 출력
     * 
     * ConsoleKeyInfo
     * 키의 문자와 Shift,Alt,Ctrl 보조키 상태 포함
     */
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * ConsoleKeyInfo 속성
             * ConsoleKeyInfo.Key
             * ConsoleKey 열거형 값
             * ConsoleKey.A, COnsoleKey.Escape 등..
             */
            ConsoleKeyInfo KeyInfo;
            do
            {
                KeyInfo = Console.ReadKey();
                if (KeyInfo.Key == ConsoleKey.A)
                    Console.WriteLine("a가 눌렸다.");
                //대소문자 구분이 없음
            } while (KeyInfo.Key != ConsoleKey.Escape);
        }
    }
}
