using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuPaLibrRary.Models;
using SuPaLibrRary.Services;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        readonly StartService SUT;

        public UnitTest1()
        {
            SUT = new StartService();
        }
        [TestMethod]
        public void DeductFuel_DecreasesFuelByOne()
        {
            // Arrange
            Status status = new Status { Fuel = 5 };
            // Act
            SUT.DeduktFuel(status);

            // Assert
            Assert.AreEqual(4, status.Fuel);
        }

        [TestMethod]
        public void Reverse_ChangesDirectionToSouth()
        {
            // Arrange
            Status status = new Status ();

            // Act
            SUT.Reverse(status);

            // Assert
            Assert.AreEqual(Direction.Suoth, status.Direction);
        }

        // You can write similar tests for other methods in the StartService class.

        [TestMethod]
        public void ReturnInt_WithValidInput_ReturnsInputValue()
        {
            // Arrange
            var inputValues = new string[] { "3" };
            var inputIndex = 0;
            Console.SetIn(new System.IO.StringReader(inputValues[inputIndex]));

            // Act
            int result = SUT.ReturnInt();

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void ReturnInt_WithInvalidInput_RetriesUntilValidInput()
        {
            // Arrange
            var inputValues = new string[] { "0", "8", "3" };
            var inputIndex = 0;
            Console.SetIn(new System.IO.StringReader(string.Join(Environment.NewLine, inputValues)));
            StartService startService = new StartService();

            // Act
            int result = startService.ReturnInt();

            // Assert
            Assert.AreEqual(3, result);
        }
    }
}