namespace ConsoleInput.Logic
{
    /// <summary>
    /// Outputs the rising- and falling edge of a binary signal, based on a separate clock signal.
    /// </summary>
    /// TODO: Make SoA equivelent of this class.
    public class DataFlipFlop
    {
        public bool Signal { get; private set; }

        public bool RisingEdge { get; private set; }

        public bool FallingEdge { get; private set; }

        public void ClockSignal(bool newSignal)
        {
            RisingEdge = IsRisingEdge(Signal, newSignal);
            FallingEdge = IsFallingEdge(Signal, newSignal);

            Signal = newSignal;
        }

        public static bool IsRisingEdge(bool previousSignal, bool newSignal) => newSignal != previousSignal && newSignal;
        
        public static bool IsFallingEdge(bool previousSignal, bool newSignal) => newSignal != previousSignal && !newSignal;
    }
}
