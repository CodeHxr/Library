using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace jCrypto
{
    public static class AES
    {
        private static readonly byte[] _salt = Encoding.UTF8.GetBytes("super secret salt");

        private static byte[] ReadByteArray(Stream s)
        {
            var rawLength = new byte[sizeof (int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
                throw new SystemException("Stream did not contain properly formatted byte array");

            var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
                throw new SystemException("Did not read byte array properly");

            return buffer;
        }

        public static string Encrypt(string plainText, string sharedSecret)
        {
            string outStr;

            var key = new Rfc2898DeriveBytes(sharedSecret, _salt);
            var aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (var msEncrypt = new MemoryStream())
            {
                msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }
                outStr = Convert.ToBase64String(msEncrypt.ToArray());
            }

            aesAlg.Clear();

            return outStr;
        }

        public static string Decrypt(string cipherText, string sharedSecret)
        {
            RijndaelManaged aesAlg;
            string plainText;

            var key = new Rfc2898DeriveBytes(sharedSecret, _salt);
            var bytes = Convert.FromBase64String(cipherText);

            using (var msDecrypt = new MemoryStream(bytes))
            {
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = ReadByteArray(msDecrypt);

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        plainText = srDecrypt.ReadToEnd();
                    }
                }
            }
            aesAlg.Clear();

            return plainText;
        }
    }

    public static class AESExtentions
    {
        public static string EncryptStringAES(this string plainText, string sharedSecret)
        {
            return AES.Encrypt(plainText, sharedSecret);
        }

        public static string DecryptStringAES(this string cipherText, string sharedSecret)
        {
            return AES.Decrypt(cipherText, sharedSecret);
        }
    }
}
