using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Threading;

namespace UsingGenericPrincipal
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generic Identity를 생성합니다.
            GenericIdentity myIdentity = new GenericIdentity("MyIndentity");

            // Generic Principal을 생성합니다.
            String[] myStringArray = { "Manager", "Teller" };
            GenericPrincipal myPrincipal = new GenericPrincipal(myIdentity, myStringArray);

            // 현재의 쓰레드에 Principal을 첨부해라
            // 반복적은 유효성 검사가 수행되지 않는 한 이 작업은 필요하지 않다.
            // 응용 프로그램의 다른 코드가 유효성을 검사해야 함 또는
            // Principle Permission 객체가 사용된다.
            Thread.CurrentPrincipal = myPrincipal;

            //Console에 값을 출력한다.
            String name = myPrincipal.Identity.Name;
            bool auth = myPrincipal.Identity.IsAuthenticated; //인증되었는가?
            bool isInRole = myPrincipal.IsInRole("Manager"); //역할이 있는가?

            Console.WriteLine("The name is : {0}", name);
            Console.WriteLine("The isAuthenticated is : {0}", auth);
            Console.WriteLine("Is this a Manager? {0}", isInRole);
        }
    }
}
