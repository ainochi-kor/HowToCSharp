using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

namespace ChattingSystem_Client
{
    class SecurityClient
    {
        public string EncryptedMessage(string textToEncrypt, string key)
        {
            RijndaelManaged rijndaelCipher;
            byte[] plainText;
            ICryptoTransform transform;

            try
            {
                rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = 128;
                rijndaelCipher.BlockSize = 128;

                byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
                byte[] keyBytes = new byte[16];
                int len = pwdBytes.Length;
                if (len > keyBytes.Length)
                {
                    len = keyBytes.Length;
                }
                Array.Copy(pwdBytes, keyBytes, len);
                rijndaelCipher.Key = keyBytes;
                rijndaelCipher.IV = keyBytes;

                transform = rijndaelCipher.CreateEncryptor();
                plainText = Encoding.UTF8.GetBytes(textToEncrypt);

            }
            catch
            {
                return "에러가 발생하였습니다"; ;
            }
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }

        public string DecryptedMessage(string textToDecrypt, string key)
        {
            byte[] plainText;
            try
            {
                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = 128;
                rijndaelCipher.BlockSize = 128;

                byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
                byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
                byte[] keyBytes = new byte[16];
                int len = pwdBytes.Length;
                if (len > keyBytes.Length)
                {
                    len = keyBytes.Length;
                }
                Array.Copy(pwdBytes, keyBytes, len);
                rijndaelCipher.Key = keyBytes;
                rijndaelCipher.IV = keyBytes;
                plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                return Encoding.UTF8.GetString(plainText);
            }
            catch(Exception e)
            { return ""; }
        }
    }
}
