using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jBytes
{
    public static class ByteOperations
    {
        public static byte[] HexToBytes(string hex)
        {
            var bytes = new List<byte>();

            for (var i = 0; i < hex.Length/2; i++)
            {
                bytes.Add(Convert.ToByte(hex.Substring(i*2,2), 16));
            }

            return bytes.ToArray();
        }

        public static string BytesToHex(byte[] bytes)
        {
            var hex = bytes.Select(b => $"{b:x2}");
            var hexString = string.Join("", hex);

            return hexString;
        }

        public static byte[] XOR(byte[] source, byte[] xorSource)
        {
            var xorBytes = source.Select((t, i) => t ^ xorSource[i]).Select(Convert.ToByte).ToArray();

            return xorBytes;
        }

        public static string XOR(string source, string xorSource)
        {
            if(source.Length != xorSource.Length)
                throw new ArgumentException("Buffers not the same length");

            var sourceBytes = HexToBytes(source);
            var xorSourceBytes = HexToBytes(xorSource);
            var xorBytes = sourceBytes.Select((t, i) => t ^ xorSourceBytes[i]).Select(Convert.ToByte).ToArray();
            var xorString = BytesToHex(xorBytes);

            return xorString;
        }
    }
}
