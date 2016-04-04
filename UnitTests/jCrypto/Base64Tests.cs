using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using jBytes;
using jCrypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.jCrypto
{
    [TestClass]
    public class Base64Tests
    {
        [TestMethod]
        public void Encode_Decode_Extentions_Work()
        {
            // Arrange
            const string originalText = "test string";

            // Act
            var encodedString = originalText.Base64Encode();
            var decodedString = encodedString.Base64Decode();

            // Assert
            Assert.AreNotEqual(originalText, encodedString);
            Assert.AreEqual(originalText, decodedString);
        }

        [TestMethod]
        public void Encode_Decode_Work()
        {
            // Arrange
            const string originalText = "test string";

            // Act
            var encodedString = Base64.Encode(originalText);
            var decodedString = Base64.Decode(encodedString);

            // Assert
            Assert.AreNotEqual(originalText, encodedString);
            Assert.AreEqual(originalText, decodedString);
        }

        [TestMethod]
        public void CanConvertHexToBase64String()
        {
            // Arrange
            const string source = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            const string expected = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";

            // Act
            var actual = Base64.FromHexString(source);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void killme()
        {
            const string source = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
            var sourceBytes = ByteOperations.HexToBytes(source);
            var metric = new Dictionary<byte, string>();

            for (var i = byte.MinValue; i < byte.MaxValue; i++)
            {
                var xorByte = Convert.ToByte(i);
                var cypherBytes = ByteOperations.XorCypher(sourceBytes, xorByte);
                var cypherText = ByteOperations.BytesToHex(cypherBytes).Base64Decode();
                Console.WriteLine($"{xorByte:int}: {cypherText}");
                metric.Add(xorByte, cypherText);
            }

            var bestHeuristic = metric.OrderBy(m => m.Value.Count(c=>c=='e' || c=='E')).First().Key;
            var bestResult = metric[bestHeuristic];

            Assert.AreEqual("", bestResult);
        }
    }
}
