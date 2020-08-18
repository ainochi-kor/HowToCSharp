using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Security.Cryptography;

namespace HashValue
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] hashValue;

            string messageString = "This is the original message!";

            // Create a new instance of the UnicodeEncoding class to convert the string into an array of Unicode bytes.
            // UnicodeEncoding 클래스의 새 인스턴스를 만들어 문자열을 유니코드 바이트 배열로 변환하십시오.
            UnicodeEncoding ue = new UnicodeEncoding();

            // stringdmf 바이트 배열에 변환해서 넣어라.
            byte[] messageBytes = ue.GetBytes(messageString);

            //SHA256클래스의 해쉬 값을 생성하여 새 인스턴스를 생성해라.
            SHA256 shHash = SHA256.Create(); 
            
            // 바이트 배열로부터 해쉬값을 생성해라.
            hashValue = shHash.ComputeHash(messageBytes);

            foreach(byte b in hashValue)
            {
                Console.Write("{0} ", b);
            }
        }
    }
}
