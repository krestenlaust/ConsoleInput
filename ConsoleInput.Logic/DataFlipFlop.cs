namespace ConsoleInput.Logic
{
    /// <summary>
    /// Outputs the rising- and falling edge of a binary signal, based on a separate clock signal.
    /// </summary>
    public class DataFlipFlop
    {
        public bool Signal { get; private set; }

        public bool RisingEdge { get; private set; }

        public bool FallingEdge { get; private set; }

        public void ClockSignal(bool newSignal)
        {
            RisingEdge = newSignal != Signal && newSignal;
            FallingEdge = newSignal != Signal && !newSignal;

            Signal = newSignal;
        }
    }
}
