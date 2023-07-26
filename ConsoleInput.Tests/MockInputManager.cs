using System.Collections.Generic;

namespace ConsoleInput.Tests
{
    internal class MockInputManager : IInputManager
    {
        ICollection<IDevice> devices = new List<IDevice>();

        /// <inheritdoc/>
        public void AddDevice(IDevice device) => devices.Add(device);

        /// <inheritdoc/>
        public void RemoveDevice(IDevice device) => devices.Remove(device);

        /// <inheritdoc/>
        public void Update()
        {
            foreach (var device in devices)
            {
                device.Update();
            }
        }
    }
}
