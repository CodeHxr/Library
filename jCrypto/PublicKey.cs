using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace jCrypto
{
    public class PublicKey
    {
        public static KeyPair GenerateKeyPair()
        {
            var rsa = new RSACryptoServiceProvider();
            var publicKey = rsa.ToXmlString(false);
            var privateKey = rsa.ToXmlString(true);

            var pair = new KeyPair {Private = privateKey, Public = publicKey};
            return pair;
        }
    }
}
