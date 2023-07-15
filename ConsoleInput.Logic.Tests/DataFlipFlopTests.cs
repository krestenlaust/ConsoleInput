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
            Assert.AreEqual(false, dff.RisingEdge);
            Assert.AreEqual(false, dff.FallingEdge);
            Assert.AreEqual(false, dff.Signal);
        }

        [TestMethod]
        public void TestNeutralStateUpdate()
        {
            // Arrange
            var dff = new DataFlipFlop();

            // Act
            dff.ClockSignal(false);

            // Assert
            Assert.AreEqual(false, dff.RisingEdge);
            Assert.AreEqual(false, dff.FallingEdge);
            Assert.AreEqual(false, dff.Signal);
        }

        [TestMethod]
        public void TestEdgeStatesTrue()
        {
            // Arrange
            var dff = new DataFlipFlop();

            // Act
            dff.ClockSignal(true);

            // Assert
            Assert.AreEqual(true, dff.RisingEdge);
            Assert.AreEqual(false, dff.FallingEdge);
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
            Assert.AreEqual(true, dff.RisingEdge);
            Assert.AreEqual(false, dff.FallingEdge);
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
            Assert.AreEqual(false, dff.RisingEdge);
            Assert.AreEqual(true, dff.FallingEdge);
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
            Assert.AreEqual(false, dff.RisingEdge);
            Assert.AreEqual(false, dff.FallingEdge);
            Assert.AreEqual(false, dff.Signal);
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
            Assert.AreEqual(false, dff.RisingEdge);
            Assert.AreEqual(false, dff.FallingEdge);
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
            Assert.AreEqual(false, dff.RisingEdge);
            Assert.AreEqual(true, dff.FallingEdge);
        }
    }
}