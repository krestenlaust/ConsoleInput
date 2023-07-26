using System;

namespace ConsoleInput.Logic
{
    /// <summary>
    /// An array of data flip-flop signals.
    /// </summary>
    public class DataFlipFlopArray
    {
        readonly bool[] previousSignals;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFlipFlopArray"/> class.
        /// </summary>
        /// <param name="size">The size of the array.</param>
        public DataFlipFlopArray(int size)
        {
            Signals = new bool[size];
            previousSignals = new bool[size];
        }

        /// <summary>
        /// Gets the current signals.
        /// </summary>
        public bool[] Signals { get; private set; }

        /// <summary>
        /// Makes an internal copy of all values of <see cref="Signals"/>, and overrides its previous values.
        /// NOTE: Doesn't clear <see cref="Signals"/>, only copies its values.
        /// </summary>
        public void ClockSignal()
        {
            Array.Copy(Signals, previousSignals, Signals.Length);
        }

        /// <summary>
        /// Gets whether a signal (by-index) is rising edge.
        /// </summary>
        /// <param name="index">The index of a given signal.</param>
        /// <returns>Whether a signal is rising edge.</returns>
        public bool IsRisingEdge(int index) => DataFlipFlop.IsRisingEdge(previousSignals[index], Signals[index]);

        /// <summary>
        /// Gets whether a signal (by-index) is falling edge.
        /// </summary>
        /// <param name="index">The index of a given signal.</param>
        /// <returns>Whether a signal is falling edege.</returns>
        public bool IsFallingEdge(int index) => DataFlipFlop.IsFallingEdge(previousSignals[index], Signals[index]);
    }
}
