namespace ConsoleInput.Logic.Tests
{
    [TestClass]
    public class DataFlipFlopTests
    {
        [TestMethod]
        public void TestDefaultState()
        {
            // Arrange
            var dff = new DataFlipFlop();

            // Act

            // Assert
            Assert.AreEqual(dff.RisingEdge, false);
            Assert.AreEqual(dff.FallingEdge, false);
            Assert.AreEqual(dff.Signal, false);
        }

        [TestMethod]
        public void TestNeutralStateUpdate()
        {
            // Arrange
            var dff = new DataFlipFlop();

            // Act
            dff.ClockSignal(false);

            // Assert
            Assert.AreEqual(dff.RisingEdge, false);
            Assert.AreEqual(dff.FallingEdge, false);
            Assert.AreEqual(dff.Signal, false);
        }

        [TestMethod]
        public void TestEdgeStatesTrue()
        {
            // Arrange
            var dff = new DataFlipFlop();

            // Act
            dff.ClockSignal(true);

            // Assert
            Assert.AreEqual(dff.RisingEdge, true);
            Assert.AreEqual(dff.FallingEdge, false);
        }

        [TestMethod]
        public void TestEdgeStatesTrueFalseTrue()
        {
            // Arrange
            var dff = new DataFlipFlop();

            // Act
            dff.ClockSignal(true);
            dff.ClockSignal(false);
            dff.ClockSignal(true);

            // Assert
            Assert.AreEqual(dff.RisingEdge, true);
            Assert.AreEqual(dff.FallingEdge, false);
        }

        [TestMethod]
        public void TestEdgeStatesTrueFalse()
        {
            // Arrange
            var dff = new DataFlipFlop();

            // Act
            dff.ClockSignal(true);
            dff.ClockSignal(false);

            // Assert
            Assert.AreEqual(dff.RisingEdge, false);
            Assert.AreEqual(dff.FallingEdge, true);
        }

        [TestMethod]
        public void TestEdgeStatesTrueFalseFalse()
        {
            // Arrange
            var dff = new DataFlipFlop();

            // Act
            dff.ClockSignal(true);
            dff.ClockSignal(false);
            dff.ClockSignal(false);

            // Assert
            Assert.AreEqual(dff.RisingEdge, false);
            Assert.AreEqual(dff.FallingEdge, false);
            Assert.AreEqual(dff.Signal, false);
        }

        [TestMethod]
        public void TestEdgeStatesTrueTrue()
        {
            // Arrange
            var dff = new DataFlipFlop();

            // Act
            dff.ClockSignal(true);
            dff.ClockSignal(true);

            // Assert
            Assert.AreEqual(dff.RisingEdge, false);
            Assert.AreEqual(dff.FallingEdge, false);
        }

        [TestMethod]
        public void TestEdgeStatesTrueTrueFalse()
        {
            // Arrange
            var dff = new DataFlipFlop();

            // Act
            dff.ClockSignal(true);
            dff.ClockSignal(true);
            dff.ClockSignal(false);

            // Assert
            Assert.AreEqual(dff.RisingEdge, false);
            Assert.AreEqual(dff.FallingEdge, true);
        }
    }
}