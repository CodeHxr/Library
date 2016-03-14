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
    }
}
