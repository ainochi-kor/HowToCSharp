using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Security.Cryptography;

namespace usingCryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //파일 스트림 생성
                FileStream myStream = new FileStream("TestData.txt", FileMode.OpenOrCreate);

                // Create a new instance of the default Aes implementation class  and encrypt the stream.  
                // 기본 Aes 구현 클래스의 새 인스턴스를 만들고 스트림을 암호화하십시오.
                Aes aes = Aes.Create();
                //aes.Padding = PaddingMode.None;

                byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
                byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

                // Create a CryptoStream, pass it the FileStream, and encrypt it with the Aes class.  
                // CryptoStream을 생성하고 FileStream을 전달하고 Aes 클래스로 암호화하십시오.
                CryptoStream cryptStream = new CryptoStream(myStream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                // Create a StreamWriter for easy writing to the file stream.  
                // 파일 스트림에 쉽게 쓸 수 있도록 StreamWriter를 만드십시오.
                StreamWriter sWriter = new StreamWriter(cryptStream);

                // 스트림을 이용하여 쓰기
                sWriter.WriteLine("Hello World");

                // 모든 연결을 닫아라.
                sWriter.Close();
                cryptStream.Close();
                myStream.Close();

                // Inform the user that the message was written  to the stream.  
                // 스트림에 메시지가 작성되었음을 사용자에게 알리십시오.
                Console.WriteLine("The file was encrypted.(이 파일은 암호화되었습니다.)");
            }
            catch
            {
                // 예외가 발생했다고 사용자에게 알리십시오.
                Console.WriteLine("The Excryption failed.");
                throw;
            }
        }
    }
}
