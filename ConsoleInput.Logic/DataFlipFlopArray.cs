using System;

namespace ConsoleInput.Logic
{
    public class DataFlipFlopArray
    {
        public bool[] Signals { get; private set; }
        readonly bool[] previousSignals;

        public DataFlipFlopArray(int size)
        {
            Signals = new bool[size];
            previousSignals = new bool[size];
        }

        /// <summary>
        /// Makes an internal copy of all values of <see cref="Signals"/>, and overrides its previous values.
        /// NOTE: Doesn't clear <see cref="Signals"/>, only copies its values.
        /// </summary>
        public void ClockSignal()
        {
            Array.Copy(Signals, previousSignals, Signals.Length);
        }

        public bool IsRisingEdge(int index) => DataFlipFlop.IsRisingEdge(previousSignals[index], Signals[index]);

        public bool IsFallingEdge(int index) => DataFlipFlop.IsFallingEdge(previousSignals[index], Signals[index]);
    }
}
