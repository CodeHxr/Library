using System;
using System.Security.Cryptography;
using jCrypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.jCrypto
{
    [TestClass]
    public class PublicKeyTests
    {
        private KeyPair _alice;
        private KeyPair _bob;
        private KeyPair _eve;
        private string _messageFromAliceToBob;

        [TestInitialize]
        public void Setup()
        {
            _alice = PublicKey.GenerateKeyPair(); // Message party A
            _bob = PublicKey.GenerateKeyPair();   // Message party B
            _eve = PublicKey.GenerateKeyPair();   // Message party interloper
            _messageFromAliceToBob = "Hi, Bob!";

            // Notes:  Alice and Bob have exchanged public keys.  Eve is trying to intercept/forge messages from them.
        }

        [TestMethod]
        public void BobCanDecryptMessageFromAlice()
        {
            // Arrange - Alice's message to Bob is encrypted with his public key
            var encryptedText = PublicKey.Encrypt(_messageFromAliceToBob, _bob.Public);

            // Act - Bob decrypts the message with his private key
            var decryptedText = PublicKey.Decrypt(encryptedText, _bob.Private);

            // Assert - Message is successfully decrypted
            Assert.AreEqual(_messageFromAliceToBob, decryptedText);
        }

        [TestMethod]
        [ExpectedException(typeof(CryptographicException))]
        public void EveCanNotDecryptMessageFromAliceToBob()
        {
            // Arrange - Alice's message to Bob; encrypted with his public key
            var encryptedText = PublicKey.Encrypt(_messageFromAliceToBob, _bob.Public);

            // Act - Eve intercepts message and attempts to decrypt with her own private key
            var decryptedText = PublicKey.Decrypt(encryptedText, _eve.Private);

            // Assert - Message is not successfully decrypted; exception thrown
        }

        [TestMethod]
        public void AliceCanDigitallySignMessageToBobAndHeCanVerifySignature()
        {   // Note: this is so Eve can not send a message to Bob posing as Alice    
            // Arrange - Alice's message to bob and digitally signs it
            var encryptedText = PublicKey.Encrypt(_messageFromAliceToBob, _bob.Public);
            var digitalSignature = PublicKey.Sign(encryptedText, _alice.Private);

            // Act - Bob verifies the digital signature
            var result = PublicKey.Verify(encryptedText, digitalSignature, _alice.Public);

            // Assert - signature verification passes
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EveAttemptsToImpersonateAlice_BobCanNotVerifyMessageCameFromAlice()
        {
            // Arrange - Alice's message to bob is signed by Eve
            var encryptedText = PublicKey.Encrypt(_messageFromAliceToBob, _bob.Public);
            var digitalSignature = PublicKey.Sign(encryptedText, _eve.Private);

            // Act - Bob verifies the digital signature is from Alice
            var result = PublicKey.Verify(encryptedText, digitalSignature, _alice.Public);

            // Assert - signature verification fails
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Encrypt_Decrypt_Work()
        {
            // Arrange
            var keys = PublicKey.GenerateKeyPair();
            const string originalText = "test string";

            // Act
            var encryptedText = PublicKey.Encrypt(originalText, keys.Public);
            var decryptedText = PublicKey.Decrypt(encryptedText, keys.Private);

            // Assert
            Assert.AreNotEqual(originalText, encryptedText);
            Assert.AreEqual(originalText, decryptedText);
        }
    }
}
