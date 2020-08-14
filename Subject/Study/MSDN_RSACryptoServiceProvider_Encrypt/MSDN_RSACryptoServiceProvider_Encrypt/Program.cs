using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MSDN_RSACryptoServiceProvider_Encrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                byte[] PublicKey = {
                                    214,46,220,83,160,73,40,39,201,155,19,202,3,11,191,178,56,
                                   74,90,36,248,103,18,144,170,163,145,87,54,61,34,220,222,
                                   207,137,149,173,14,92,120,206,222,158,28,40,24,30,16,175,
                                   108,128,35,230,118,40,121,113,125,216,130,11,24,90,48,194,
                                   240,105,44,76,34,57,249,228,125,80,38,9,136,29,117,207,139,
                                   168,181,85,137,126,10,126,242,120,247,121,8,100,12,201,171,
                                   38,226,193,180,190,117,177,87,143,242,213,11,44,180,113,93,
                                   106,99,179,68,175,211,164,116,64,148,226,254,172,147
                                   };
                byte[] Exponent = { 1, 0, 1 };

                byte[] EncryptedSymmetricKey;
                byte[] EncryptedSymmetricIV;

                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                RSAParameters RSAKeyInfo = new RSAParameters();

                RSAKeyInfo.Modulus = PublicKey;
                RSAKeyInfo.Exponent = Exponent;

                RSA.ImportParameters(RSAKeyInfo);

                var aes = Aes.Create();

                EncryptedSymmetricKey = RSA.Encrypt(aes.Key, false);
                EncryptedSymmetricIV = RSA.Encrypt(aes.IV, false);

                Console.WriteLine("Aes Key and IV have been encrypted with RSACryptoServiceProvider.");
            }
            catch(CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
