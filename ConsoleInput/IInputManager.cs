namespace ConsoleInput
{
    public interface IInputManager
    {
        /// <summary>
        /// Pulls console event queues and calls <see cref="IDevice.Update"/> on ...
        /// </summary>
        void Update();

        /// <summary>
        /// Adds device. Updates Console Mode if device implements <see cref="IRequireConsoleMode"/>.
        /// </summary>
        /// <param name="device"></param>
        void AddDevice(IDevice device);

        /// <summary>
        /// Removes device. Updates Console Mode if device implements <see cref="IRequireConsoleMode"/>.
        /// </summary>
        /// <param name="device"></param>
        void RemoveDevice(IDevice device);
    }
}
