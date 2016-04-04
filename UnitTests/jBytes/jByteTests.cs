using System;
using System.Linq;
using jBytes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.jBytes
{
    [TestClass]
    public class jByteTests
    {
        [TestMethod]
        public void CanConvertHexStringToBytes()
        {
            // Arrange
            const string source = "1c0111001f010100061a024b53535009181c";
            var expected = new byte[] {28, 1, 17, 0, 31, 1, 1, 0, 6, 26, 2, 75, 83, 83, 80, 9, 24, 28};

            // Act
            var actual = ByteOperations.HexToBytes(source);
            

            // Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void CanConvertBytesToHexString()
        {
            // Arrange
            var source = new byte[] { 28, 1, 17, 0, 31, 1, 1, 0, 6, 26, 2, 75, 83, 83, 80, 9, 24, 28 };
            const string expected = "1c0111001f010100061a024b53535009181c";

            // Act
            var actual = ByteOperations.BytesToHex(source);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanXorHexStringsAsBytes()
        {
            // Arrange
            const string source = "1c0111001f010100061a024b53535009181c";
            const string xorSource = "686974207468652062756c6c277320657965";
            const string expected = "746865206b696420646f6e277420706c6179";

            // Act
            var actual = ByteOperations.XOR(source, xorSource);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanXorByteArrays()
        {
            // Arrange
            var source = new byte[] {};
            var xorSource = new byte[] {};
            var expected = new byte[] {};

            // Act
            var actual = ByteOperations.XOR(source, xorSource);

            // Assert
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void XorFailsIfStringsNotSameLength()
        {
            // Arrange
            const string source = "1c0111001f010100061a024b53535009181cx";
            const string xorSource = "686974207468652062756c6c277320657965";

            // Act
            var actual = ByteOperations.XOR(source, xorSource);

            // Assert: NOP - exception is expected
        }

    }
}
