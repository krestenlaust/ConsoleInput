using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInput.Tests
{
    internal class MockButtonDevice : IDevice, IButtonDevice<MouseButton>
    {
        public bool IsButtonDown(MouseButton button) => Dff.Signals[(int)button];

        public bool IsButtonPressed(MouseButton button) => Dff.IsRisingEdge((int)button);

        public bool IsButtonReleased(MouseButton button) => Dff.IsFallingEdge((int)button);

        public void Update()
        {

        }
    }
}
