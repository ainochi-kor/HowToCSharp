using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG1_asOperater
{
    class Program
    {
        //as 연산자
        /*
         * 형변환과 변환 조사
         * 캐스트 연산자의 역할과 불변환은 null 리턴
         * 참조, 박싱, 언박싱, null형에 사용
         * 결과형 = 참조형, 언박싱, 박싱 as 변환형 
         */
        class A
        {

        }
        class B
        {

        }
        static void Main(string[] args)
        {
            string str1 = "123";
            object obj = str1;
            string str2 = obj as string;
            Console.WriteLine(str2);

            A test1 = new A(); //객체 생성
            object obj1 = test1; 
            B test2 = obj1 as B; //A 를 B로 형변환 할 수 있는가?
            if (test2 == null) 
                Console.WriteLine("형변환 실패");
            else
                Console.WriteLine("형변환 성공");
        }
    }
}
