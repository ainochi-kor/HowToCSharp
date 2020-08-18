using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

namespace HashValue2
{
    class Program
    {
        static void Main(string[] args)
        {
            // 해쉬 값은 SHA256을 이용하여 "This is the original message!"에서 생상되어집니다.
            byte[] sentHashValue = { 185, 203, 236, 22, 3, 228, 27, 130, 87, 23, 244, 15, 87, 88, 14, 43, 37, 61, 106, 224, 81, 172, 224, 211, 104, 85, 194, 197, 194, 25, 120, 217 };

            // 이 문자열은 이전의 값에 해당한다.
            string messageString = "This is the original message!";

            byte[] compareHashValue;

            // 유니코드인코딩 클래스를 이용하여 변환하여 문자열을 유니코드 바이트의 배열에 넣는 새 인스턴스를 생성해라 
            UnicodeEncoding ue = new UnicodeEncoding();

            // 바이트 배열에 문자열을 변환해서 넣어라
            byte[] messageBytes = ue.GetBytes(messageString);

            // SHA256클래스로 해시 값을 생성하는 새 인스턴스를 생성하라.
            SHA256 shHash = SHA256.Create();

            // 바이트 배열로부터 해시 값을 생성하라.
            compareHashValue = shHash.ComputeHash(messageBytes);

            bool same = true;

            //두 바이트 배열의 값을 비교하라.
            for(int x = 0 ; x < sentHashValue.Length; x++)
            {
                if(sentHashValue[x] != compareHashValue[x])
                {
                    same = false;
                }
            }
            //Display whether or not the hash values are the same.
            if(same)
            {
                Console.WriteLine("The hash codes match.");
            }
            else
            {
                Console.WriteLine("The hsah codes do not match.");
            }
        }
    }
}
