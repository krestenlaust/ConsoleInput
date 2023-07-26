namespace ConsoleInput
{
    /// <summary>
    /// Represents an object that keeps track of <see cref="IDevice"/>s and forwards Update-signals to them, for them to poll their inputs.
    /// </summary>
    public interface IInputManager
    {
        /// <summary>
        /// Polls console event queues and calls <see cref="IDevice.Update"/> on ...
        /// </summary>
        void Update();

        /// <summary>
        /// Adds device. Updates Console Mode if device implements <see cref="IRequireConsoleMode"/>.
        /// </summary>
        /// <param name="device">The input device to add.</param>
        void AddDevice(IDevice device);

        /// <summary>
        /// Removes device. Updates Console Mode if device implements <see cref="IRequireConsoleMode"/>.
        /// </summary>
        /// <param name="device">The previously added input device, to remove.</param>
        void RemoveDevice(IDevice device);
    }
}
