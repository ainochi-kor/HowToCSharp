using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Security.Permissions;
using System.Security.Principal;
namespace SecurityPrincipalDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Retrieve a GenericPrincipal that is based on the current user's WindowsIdentity.
            //현재 사용자의 Windows ID를 기반으로 하는 일반 사용자 검색
            GenericPrincipal genericPrincipal = GetGenericPrincipal();

            // Retrieve the generic identity of the GenericPrincipal object.
            //GenericPrincipal 개체의 일반 ID를 검색하십시오.
            GenericIdentity principalIdentity = (GenericIdentity)genericPrincipal.Identity;

            // Display the identity name and authentication type.
            //Identity 이름과 검증 타입을 화면에 띄우시오.
            if(principalIdentity.IsAuthenticated)
            {
                Console.WriteLine(principalIdentity.Name);
                Console.WriteLine("Type : " + principalIdentity.AuthenticationType);
            }

            // Verify that the generic principal has been assigned the NetworkUser role.
            // 일반 주체에 NetworkUser 역할이 할당되었는지 확인하십시오.

            //스레드의 현재 보안 주체(역할 기반 보안용)를 가져오거나 설정합니다.
            Thread.CurrentPrincipal = genericPrincipal; 
        }

        // Create a generic principal based on values from the current WindowsIdentity.
        // 현재 윈도우즈 ID의 값을 기반으로 일반 주체를 생성하십시오.
        private static GenericPrincipal GetGenericPrincipal()
        {
            // Use values from the current WindowsIdentity to construct a set of GenericPrincipal roles.
            //현재 윈도우즈 ID의 값을 사용하여 GenericPrincipal 역할 집합을 구성하십시오.
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            string[] roles = new string[10];
            if(windowsIdentity.IsAuthenticated)
            {
                // Add custom NetworkUser role.
                // 사용자 지정 NetworkUser 역할 추가.
                roles[0] = "NetworkUser";
            }

            if(windowsIdentity.IsGuest)
            {
                // Add custom GuestUser role.
                // 사용자 지정 GuestUser 역할 추가.
                roles[1] = "GuestUser";
            }

            if(windowsIdentity.IsSystem)
            {
                // Add custom SystemUser role.
                // 사용자 지정 SystemUser 역할 추가.
                roles[2] = "SystemUser";
            }

            // Construct a GenericIdentity object based on the current Windows identity name and authentication type.
            // 현재 윈도우즈 ID 이름 및 인증 유형을 기반으로 GenericIdentity 개체를 생성하십시오.
            string authenticationType = windowsIdentity.AuthenticationType;
            string userName = windowsIdentity.Name;
            GenericIdentity genericIdentity = new GenericIdentity(userName, authenticationType);

            // 일반 ID를 기반으로 GenericPrincipal 개체 구성 및 사용자에 대한 사용자 지정 역할.
            GenericPrincipal genericPrinciapl = new GenericPrincipal(genericIdentity, roles);

            return genericPrinciapl;
        }

    }
}
