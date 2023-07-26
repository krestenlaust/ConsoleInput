namespace ConsoleInput.Logic
{
    /// <summary>
    /// Outputs the rising- and falling edge of a binary signal, based on a separate clock signal.
    /// </summary>
    /// TODO: Make SoA equivelent of this class.
    public class DataFlipFlop
    {
        /// <summary>
        /// Gets a value indicating the current signal value.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:Property summary documentation should match accessors", Justification = "Template text doesn't fit the function of this member.")]
        public bool Signal { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the current signal is rising edge.
        /// </summary>
        public bool RisingEdge { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the current signal is falling edge.
        /// </summary>
        public bool FallingEdge { get; private set; }

        /// <summary>
        /// Returns whether the signal is in rising edge state.
        /// </summary>
        /// <param name="previousSignal">The previous signal value.</param>
        /// <param name="newSignal">The current signal value.</param>
        /// <returns>Whether the signal is rising edge.</returns>
        public static bool IsRisingEdge(bool previousSignal, bool newSignal) => newSignal != previousSignal && newSignal;

        /// <summary>
        /// Returns whether the signal is in falling edge state.
        /// </summary>
        /// <param name="previousSignal">The previous signal value.</param>
        /// <param name="newSignal">The current signal value.</param>
        /// <returns>Whether the signal is falling edge.</returns>
        public static bool IsFallingEdge(bool previousSignal, bool newSignal) => newSignal != previousSignal && !newSignal;

        /// <summary>
        /// The CLK signal, that updates the state of the signal.
        /// </summary>
        /// <param name="newSignal">The new signal value accompanying the CLK signal.</param>
        public void ClockSignal(bool newSignal)
        {
            RisingEdge = IsRisingEdge(Signal, newSignal);
            FallingEdge = IsFallingEdge(Signal, newSignal);

            Signal = newSignal;
        }
    }
}
