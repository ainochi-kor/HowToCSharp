using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

namespace StoreKey
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 키를 생성하고 컨테이너에 그것을 저장하십시오.
                GenKey_SaveInContainer("MyKeyContainer");

                // 컨테이너에서 키를 검색합니다.
                GetKeyFromContainer("MyKeyContainer");

                // 컨테이터네엇 키를 삭제합니다.
                DeleteKeyFromContainer("MyKeyContainer");

                GenKey_SaveInContainer("MyKeyContainer");

                DeleteKeyFromContainer("MyKeyContainer");
            }
            catch(CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void GenKey_SaveInContainer(string containerName)
    {
        // Create the CspParameters object and set the key container
        // name used to store the RSA key pair.
        var parameters = new CspParameters
        {
            KeyContainerName = containerName
        };

        // Create a new instance of RSACryptoServiceProvider that accesses
        // the key container MyKeyContainerName.
        using (var rsa = new RSACryptoServiceProvider(parameters))

        // Display the key information to the console.
        Console.WriteLine("Key added to container: \n {0}",rsa.ToXmlString(true));
    }

    
        private static void GenKey_SaveInContainer(string containerName)
        {
            // Create the CspParameters object and set the key container name used to store the RSA key pair.
            // CspParameter 개체를 생성하고 RSA 키 쌍을 저장하는 데 사용되는 키 컨테이너 이름을 설정하십시오.
            var parameters = new CspParameters { KeyContainerName = containerName };

            // Create a new instance of RSACryptoServiceProvider that accesses the key container MyKeyContainerName.
            // 키 컨테이너 MyKeyContainerName에 액세스하는 RSACryptoServiceProvider의 새 인스턴스를 생성하십시오.
            using (var rsa = new RSACryptoServiceProvider(parameters))

            Console.WriteLine("Key added to container : \n {0}", rsa.ToXmlString(true));
        }

        private static void GetKeyFromContainer(string containerName)
        {
            // Create the CspParameters object and set the key container name used to store the RSA key pair.
            // CspParameter 개체를 생성하고 RSA 키 쌍을 저장하는 데 사용되는 키 컨테이너 이름을 설정하십시오.
            var parameters = new CspParameters { KeyContainerName = containerName };

            // Create a new instance of RSACryptoServiceProvider that accesses the key container MyKeyContainerName.
            // 키 컨테이너 MyKeyContainerName에 액세스하는 RSACryptoServiceProvider의 새 인스턴스를 생성하십시오.
            using (var rsa = new RSACryptoServiceProvider(parameters))

            // 키 정보를 콘솔에 표시해라
            Console.WriteLine("Key retrieved from container : \n {0}", rsa.ToXmlString(true));
        }

        private static void DeleteKeyFromContainer(string containerName)
        {
            // Create the CspParameters object and set the key container name used to store the RSA key pair.
            // CspParameter 개체를 생성하고 RSA 키 쌍을 저장하는 데 사용되는 키 컨테이너 이름을 설정하십시오.
            var parameters = new CspParameters { KeyContainerName = containerName };

            // Create a new instance of RSACryptoServiceProvider that accesses the key container MyKeyContainerName.
            // 키 컨테이너 MyKeyContainerName에 액세스하는 RSACryptoServiceProvider의 새 인스턴스를 생성하십시오.
            using (var rsa = new RSACryptoServiceProvider(parameters){ PersistKeyInCsp = false })

            // Call Clear to release resources and delete the key from the container.
            // Clear를 호출하여 리소스를 해제하고 컨테이너에서 키를 삭제하십시오.
            rsa.Clear();

            Console.WriteLine("Key deleted.");
        }
         
    }
}