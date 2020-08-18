using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;


namespace EncryptXml
{
    class Program
    {
        static void Main(string[] args)
        {
            Aes key = null;

            try
            {
                // AES키를 새로 생성.
                key = Aes.Create();

                // xml 문서를 불러와라
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load("XMLFile1.xml");

                // creditcard 요소를 암호화하라.
                Encrypt(xmlDoc, "creditcard", key);

                Console.WriteLine("The element was encrypted");

                Console.WriteLine(xmlDoc.InnerXml);

                Decrypt(xmlDoc, key);

                Console.WriteLine("The element was decrypted");

                Console.WriteLine(xmlDoc.InnerXml);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if(key != null)
                {
                    key.Clear();
                }
            }
        }

        public static void Encrypt(XmlDocument Doc, string ElementName, SymmetricAlgorithm Key)
        {
            // 요소를 체크해라
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (ElementName == null)
                throw new ArgumentNullException("ElementToEncrypt");
            if (Key == null)
                throw new ArgumentNullException("Alg");

            // 찾아라 / 특별한 요소를 / Xml문서 객체 안에서/ 그리고 창조해라/ 새로운 Xml요소 객체를.
            XmlElement elementToEncrypt = Doc.GetElementsByTagName(ElementName)[0] as XmlElement;
            // 요소가 없다면 Xml예외를 던져라.
            if(elementToEncrypt == null)
            {
                throw new XmlException("The specified element was not found");
            }

            //암호화된Xml 클래스의 새 인스턴스를 만들고 이를 사용하여 대칭 키로 XmlElement를 암호화하십시오.
            EncryptedXml eXml = new EncryptedXml();
            byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, Key, false);

            //Construct an EncryptedData object and populate it with the desired encryption information.
            //암호화된 데이터 개체를 구성하고 원하는 암호화 정보로 채우십시오.

            EncryptedData edElement = new EncryptedData();
            edElement.Type = EncryptedXml.XmlEncElementUrl;

            // Create an EncryptionMethod element so that the receiver knows which algorithm to use for decryption.
            // Determine what kind of algorithm is being used and supply the appropriate URL to the EncryptionMethod element.
            // 수신기가 암호 해독에 사용할 알고리즘을 알 수 있도록 EncryptionMethod 요소를 생성하십시오.
            // 사용 중인 알고리즘의 종류를 결정하고 적절한 URL을 EncryptionMethod 요소에 제공하십시오.

            string encryptionMethod = null;

            if(Key is Aes)
            {
                encryptionMethod = EncryptedXml.XmlEncAES256Url;
            }
            else
            {
                // AES로 변환이 안되면 예외처리 하라.
                throw new CryptographicException("The specified algorithm is not supported or not recommended for XML Encryption.");
            }

            edElement.EncryptionMethod = new EncryptionMethod(encryptionMethod);

            // EncryptedData 객체에 암호화된 요소 데이터를 추가해라.
            edElement.CipherData.CipherValue = encryptedElement;

            // Replace the element from the original XmlDocument object with the EncryptedData element.
            // 원래 XmlDocument 개체의 요소를 암호화된 데이터 요소로 교체하십시오.
            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
        }

        public static void Decrypt(XmlDocument Doc, SymmetricAlgorithm Alg)
        {
            // 요소 체크해라
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (Alg == null)
                throw new ArgumentNullException("Alg");

            XmlElement encryptedElement = Doc.GetElementsByTagName("EncryptedData")[0] as XmlElement;

            //만약 EncryptedData 요소를 찾지 못했다면 예외를 던져라.
            if(encryptedElement == null)
            {
                throw new XmlException("The EncryptedData element was not found.");
            }

            // 암호화된 데이터 개체를 만들고 채우십시오.
            EncryptedData edElement = new EncryptedData();
            edElement.LoadXml(encryptedElement);

            // 새 EncryptedXml 객체를 창조하라.
            EncryptedXml exml = new EncryptedXml();

            // 대칭 키를 사용하여 요소의 암호를 해독하십시오.
            byte[] rgbOutput = exml.DecryptData(edElement, Alg);

            // 암호화된 데이터 요소를 일반 텍스트 XML 요소로 교체하십시오.
            exml.ReplaceData(encryptedElement, rgbOutput);

        }
    }
}
