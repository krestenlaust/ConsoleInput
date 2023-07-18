namespace ConsoleInput
{
    /// <summary>
    /// Implemented by input devices that need a clock signal to update their button states.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Called whenever an input device needs to update its button state.
        /// </summary>
        void Update();
    }
}
