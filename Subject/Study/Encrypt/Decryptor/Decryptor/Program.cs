using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.IO;
using System.Security.Cryptography;

namespace Decryptor
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // The key and IV must be the same values that were used to encrypt the stream.
            // 키와 IV는 스트림을 암호화하는 데 사용한 값과 동일해야 한다.
            byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
            byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

            try
            {
                // 파일 스트림을 생성하라
                FileStream myStream = new FileStream("TestData.txt", FileMode.Open);

                //Create a new instance of the default Aes implementation class
                //기본 Aes 구현 클래스의 새 인스턴스 생성
                Aes aes = Aes.Create();
                aes.Padding = PaddingMode.None;

                //Create a CryptoStream, pass it the file stream, and decrypt it with the Aes class using the key and IV.
                //CryptoStream을 생성하여 파일 스트림을 전달하고 키와 IV를 사용하여 Aes 클래스로 암호를 해독하십시오.
                CryptoStream cryptoStream = new CryptoStream(myStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);

                // 스트림을 읽어라
                StreamReader sReader = new StreamReader(cryptoStream);

                // 화면에 메시지를 출력하라.
                Console.WriteLine("The decrypted original message: {0}", sReader.ReadToEnd());

                // 스트림을 닫아라.
                sReader.Close();
                myStream.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("The decryption failed.\n" + e.ToString());
                throw;
            }
            
        }
    }
}
