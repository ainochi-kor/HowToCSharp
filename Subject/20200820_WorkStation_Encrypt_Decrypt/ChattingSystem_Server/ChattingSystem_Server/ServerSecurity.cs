using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using System.Windows.Forms;

namespace ChattingSystem_Server
{
    class ServerSecurity 
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
                return "";
            }
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }
    }
}
