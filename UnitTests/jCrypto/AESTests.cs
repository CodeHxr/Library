using System;
using jCrypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.jCrypto
{
    [TestClass]
    public class AESTests
    {
        [TestMethod]
        public void Encrypt_Decrypt_Extentions_Work()
        {
            // Arrange
            const string originalText = "this is a test";
            var key = Guid.NewGuid().ToString();

            // Act 
            var cipherText = originalText.EncryptStringAES(key);
            var plainText = cipherText.DecryptStringAES(key);

            // Assert
            Assert.AreNotEqual(originalText, cipherText);
            Assert.AreEqual(originalText, plainText);
        }

        [TestMethod]
        public void Encrypt_Decrypt_Work()
        {
            // Arrange
            const string originalText = "this is a different test";
            var key = Guid.NewGuid().ToString();

            // Act
            var cipherText = AES.Encrypt(originalText, key);
            var plainText = AES.Decrypt(cipherText, key);

            // Assert
            Assert.AreNotEqual(originalText, cipherText);
            Assert.AreEqual(originalText, plainText);
        }
    }
}
