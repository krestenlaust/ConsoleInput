using System.Collections.Generic;

namespace ConsoleInput.Tests
{
    internal class MockInputManager : IInputManager
    {
        ICollection<IDevice> devices = new List<IDevice>();

        public void AddDevice(IDevice device) => devices.Add(device);

        public void RemoveDevice(IDevice device) => devices.Remove(device);

        public void Update()
        {
            foreach (var device in devices)
            {
                device.Update();
            }
        }
    }
}
