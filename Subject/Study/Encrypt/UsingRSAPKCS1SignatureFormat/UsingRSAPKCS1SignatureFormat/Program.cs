using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace UsingRSAPKCS1SignatureFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            // The Hash value to sign
            byte[] hashValue = { 59, 4, 248, 102, 77, 97, 142, 201, 210, 12, 224, 93, 25, 41, 100, 197, 213, 134, 130, 135 };

            // The value to hold the signed value.
            byte[] signedHashValue;

            //Generate a public/private key pair
            RSA rsa = RSA.Create();

            // Create an RSAPKCS1SignatureFormatter object and pass it the RSA instance to transfer the private key.
            // RSAPKCS1SignatureFormatter 개체를 생성하고 이를 RSA 인스턴스에 전달하여 개인 키를 전송하십시오.
            RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);

            // Set the hash algorithm to SHA1.
            // 해시 알고리즘을 SHA1로 설정한다.
            rsaFormatter.SetHashAlgorithm("SHA1");

            //Create a signature for hashValue and assign it to signedHashValue.
            //hashValue의 서명을 만들어 signedHashValue에 할당하십시오.
            signedHashValue = rsaFormatter.CreateSignature(hashValue);

            /*
            RSAParameters rsaKeyInfo;
            rsaKeyInfo.Modulus = modulusData;
            rsaKeyInfo.Exponent = exponentData;
            rsa.ImportParameters(rsaKeyInfo);
            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm("SHA1");
            if (rsaDeformatter.VerifySignature(hashValue, signedHashValue))
            {
                Console.WriteLine("The signature is valid.");
            }
            else
            {
                Console.WriteLine("The signature is not valid.");
            }
             * */
        }
    }
}
