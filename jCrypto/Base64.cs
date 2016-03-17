using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCrypto
{
    public static class Base64
    {
        public static string Encode(string plainText)
        {
            var bytes = Encoding.UTF8.GetBytes(plainText);
            var encodedString = Convert.ToBase64String(bytes);

            return encodedString;
        }

        public static string Decode(string encodedText)
        {
            var bytes = Convert.FromBase64String(encodedText);
            var plainText = Encoding.UTF8.GetString(bytes);

            return plainText;
        }

        public static string FromHexString(string hexString)
        {
            var ints = new List<int>();

            for (var i = 0; i < hexString.Length/2; i++)
            {
                var current = hexString.Substring(i*2, 2);
                ints.Add(Convert.ToInt32(current, 16));
            }

            var bytes = ints.Select(Convert.ToByte).ToArray();
            var actual = Convert.ToBase64String(bytes);

            return actual;
        }
    }

    public static class Base64Extentions
    {
        public static string Base64Encode(this string plainText)
        {
            return Base64.Encode(plainText);
        }

        public static string Base64Decode(this string encodedText)
        {
            return Base64.Decode(encodedText);
        }
    }
}
