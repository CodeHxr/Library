using System;
using System.Net.Security;
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
    }
}
