using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Student_evaluation.Test
{
    [TestClass]
    public class SimpleTests
    {
        [TestMethod]
        public void TestAddition()
        {
            // Arrange
            int a = 1;
            int b = 1;

            // Act
            int result = a + b;

            // Assert
            Assert.AreEqual(2, result, "1 + 1 should equal 2.");
        }

        [TestMethod]
        public void TestSubtraction()
        {
            // Arrange
            int a = 5;
            int b = 3;

            // Act
            int result = a - b;

            // Assert
            Assert.AreEqual(2, result, "5 - 3 should equal 2.");
        }

        [TestMethod]
        public void TestMultiplication()
        {
            // Arrange
            int a = 3;
            int b = 4;

            // Act
            int result = a * b;

            // Assert
            Assert.AreEqual(12, result, "3 * 4 should equal 12.");
        }

        [TestMethod]
        public void TestDivision()
        {
            // Arrange
            int a = 10;
            int b = 2;

            // Act
            int result = a / b;

            // Assert
            Assert.AreEqual(5, result, "10 / 2 should equal 5.");
        }

        [TestMethod]
        public void TestEquality()
        {
            // Arrange
            int a = 5;
            int b = 5;

            // Act
            bool areEqual = (a == b);

            // Assert
            Assert.IsTrue(areEqual, "5 should be equal to 5.");
        }
    }
}
