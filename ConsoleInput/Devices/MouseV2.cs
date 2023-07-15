using ConsoleInput.Logic;
using ConsoleInput.WinAPI;

namespace ConsoleInput.Devices
{
    public class MouseV2 : IDevice, IButtonDevice<MouseButton>, ICursorDevice
    {
        /// <summary>
        /// The amount of buttons available.
        /// </summary>
        public const int MouseButtonCount = 5;

        DataFlipFlopArray Dff;
        short cursorX;
        short cursorY;

        public MouseV2()
        {
            Dff = new DataFlipFlopArray(MouseButtonCount);
        }

        public short GetX() => cursorX;

        public short GetY() => cursorY;

        public bool IsButtonDown(MouseButton button) => Dff.Signals[(int)button];

        public bool IsButtonPressed(MouseButton button) => Dff.IsRisingEdge((int)button);

        public bool IsButtonReleased(MouseButton button) => Dff.IsFallingEdge((int)button);

        public void Update()
        {
            Dff.ClockSignal();
        }

        /// <summary>
        /// Modifies mouse state based on mouse event.
        /// </summary>
        /// <param name="mouseEvent">Event returned by ReadConsoleInput.</param>
        internal void HandleMouseEvent(MOUSE_EVENT_RECORD mouseEvent)
        {
            switch (mouseEvent.dwEventFlags)
            {
                case MouseEventFlags.DOUBLE_CLICK:
                    break;
                case MouseEventFlags.MOUSE_MOVED:
                    cursorX = mouseEvent.dwMousePosition.X;
                    cursorY = mouseEvent.dwMousePosition.Y;
                    break;
                case MouseEventFlags.MOUSE_HWHEELED:
                    break;
                case MouseEventFlags.MOUSE_WHEELED:
                    break;
                case 0:
                    // mousebutton pressed or up
                    for (int n = 0; n < MouseButtonCount; n++)
                    {
                        Dff.Signals[n] = (mouseEvent.dwButtonState & (1 << n)) != 0;
                    }

                    break;
            }
        }
    }
}
