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
