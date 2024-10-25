using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Authorization.Crypto
{
    public class Crypto
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("jhutgugGMJHu57RT"); // 16 bytes
        private static readonly byte[] Iv = Encoding.UTF8.GetBytes("jhutgugGnnjU&T5!"); // 16 bytes

        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = Iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key,aes.IV);
                using (MemoryStream ms = new MemoryStream()) 
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
           
        }

        public static string Decrypt(string encryptedText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = Iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
