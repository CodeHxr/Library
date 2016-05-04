using jMath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.jMath
{
    [TestClass]
    public class Vector2DTests
    {
        [TestMethod]
        public void Vector2D_Can_UseEquality()
        {
            // Arrange
            var v1 = new Vector2D(1, 1);
            var v2 = new Vector2D(1, 1);

            // Act
            // [nop]

            // Assert
            Assert.IsTrue(v1 == v2);
        }

        [TestMethod]
        public void Vector2D_Can_Add()
        {
            // Arrange
            var v1 = new Vector2D(2, 3);
            var v2 = new Vector2D(3, 4);
            var vExpected = new Vector2D(5, 7);

            // Act
            var vActual = v1 + v2;

            // Assert
            Assert.AreEqual(vExpected, vActual);
        }

        [TestMethod]
        public void Vector2D_Can_Subtract()
        {
            // Arrange
            var v1 = new Vector2D(4, 5);
            var v2 = new Vector2D(2, 3);
            var vExpected = new Vector2D(2, 2);

            // Act
            var vActual = v1 - v2;

            // Assert
            Assert.AreEqual(vExpected, vActual);
        }

        [TestMethod]
        public void Vector2D_Can_Scale()
        {
            // Arrange
            var v = new Vector2D(2, 3);
            const double scalar = 1.5;
            var vExpected = new Vector2D(3, 4.5);

            // Act
            var vActual = v * scalar;

            // Assert
            Assert.AreEqual(vExpected, vActual);
        }

        [TestMethod]
        public void Vector2D_Can_DotProduct()
        {
            // Arrange
            var v1 = new Vector2D(2, 3);
            var v2 = new Vector2D(4, 5);
            const double dpExpected = 23;

            // Act
            var dpActual = v1.DotProduct(v2);

            // Assert
            Assert.AreEqual(dpExpected, dpActual);
        }

        [TestMethod]
        public void Vector2D_Can_CanNormalize()
        {
            // Arrange
            var v = new Vector2D(2, 3);

            // Act
            var vNormalized = v.Normalize();

            // Assert
            Assert.AreEqual(1, vNormalized.Length);
        }

        [TestMethod]
        public void Vector2D_IsUnitVector_TrueIfTrue()
        {
            // Arrange
            var v = new Vector2D(0, 1);

            // Act
            var result = Vector2D.IsUnitVector(v);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Vector2D_IsUnitVector_FalseIfFalse()
        {
            // Arrange
            var v = new Vector2D(1, 1);

            // Act
            var result = Vector2D.IsUnitVector(v);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
