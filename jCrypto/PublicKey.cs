using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace jCrypto
{
    public static class PublicKey
    {
        private static readonly UnicodeEncoding _encoder = new UnicodeEncoding();
        private const bool USE_FOAEP = false;
        private const int KEY_SIZE = 512;

        public static KeyPair GenerateKeyPair()
        {
            var rsa = new RSACryptoServiceProvider(KEY_SIZE);
            var publicKey = rsa.ToXmlString(false);
            var privateKey = rsa.ToXmlString(true);

            var pair = new KeyPair { Private = privateKey, Public = publicKey };
            return pair;
        }

        public static string Encrypt(string plainText, string recipientPublicKey)
        {
            var rsa = new RSACryptoServiceProvider(KEY_SIZE);
            var rsaPrivate = new RSACryptoServiceProvider(KEY_SIZE);

            rsa.FromXmlString(recipientPublicKey);

            var bytesToEncrypt = _encoder.GetBytes(plainText);
            
            var encryptedBytes = rsa.Encrypt(bytesToEncrypt, USE_FOAEP);
            var encryptedStringArray = Array.ConvertAll(encryptedBytes, byt => byt.ToString());
            var encryptedString = string.Join(",", encryptedStringArray);
            
            return encryptedString;
        }

        public static string Decrypt(string encryptedString, string privateKey)
        {
            var rsa = new RSACryptoServiceProvider(KEY_SIZE);
            rsa.FromXmlString(privateKey);

            var dataArray = encryptedString.Split(',');
            var dataBytes = Array.ConvertAll(dataArray, byte.Parse);

            var decryptedBytes = rsa.Decrypt(dataBytes, USE_FOAEP);
            var decryptedString = _encoder.GetString(decryptedBytes);

            return decryptedString;
        }

        public static byte[] Sign(string encryptedText, string senderPrivateKey, bool base64Encoded = false)
        {
            if (base64Encoded)
                encryptedText = Base64.Decode(encryptedText);
            var bytes = Encoding.UTF8.GetBytes(encryptedText);

            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(senderPrivateKey);

            
            var digitalSignature = rsa.SignData(bytes, "SHA1");

            return digitalSignature;
        }

        public static bool Verify(string encryptedText, byte[] digitalSignature, string senderPublicKey, bool base64Encoded = false)
        {
            if (base64Encoded)
                encryptedText = Base64.Decode(encryptedText);

            var bytes = Encoding.UTF8.GetBytes(encryptedText);

            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(senderPublicKey);

            var verified = rsa.VerifyData(bytes, "SHA1", digitalSignature);

            return verified;
        }
    }
}
