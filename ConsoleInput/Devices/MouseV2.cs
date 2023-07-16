using ConsoleInput.Logic;
using ConsoleInput.WinAPI;
using static ConsoleInput.WinAPI.InputEventHandling;

namespace ConsoleInput.Devices
{
    public class MouseV2 : IDevice, IInputRecordObserver<MOUSE_EVENT_RECORD>, IRequireConsoleMode, IButtonDevice<MouseButton>, ICursorDevice
    {
        /// <summary>
        /// The amount of buttons available.
        /// </summary>
        public const int MouseButtonCount = 5;

        readonly bool quickSelect;
        DataFlipFlopArray Dff;
        short cursorX;
        short cursorY;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseV2"/> class.
        /// </summary>
        /// <param name="quickSelectEnabled">If enabled, clicking results in normal console selection behavior.</param>
        public MouseV2(bool quickSelectEnabled=false)
        {
            Dff = new DataFlipFlopArray(MouseButtonCount);

            this.quickSelect = quickSelectEnabled;
        }

        public uint GetConsoleMode() => (quickSelect ? 0 : ENABLE_EXTENDED_FLAGS) | ENABLE_MOUSE_INPUT;

        public short GetX() => cursorX;

        public short GetY() => cursorY;

        /// <summary>
        /// Modifies mouse state based on mouse event.
        /// </summary>
        /// <param name="mouseEvent">Event returned by ReadConsoleInput.</param>
        public bool HandleEvent(MOUSE_EVENT_RECORD mouseEvent)
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

            return false;
        }

        public bool IsButtonDown(MouseButton button) => Dff.Signals[(int)button];

        public bool IsButtonPressed(MouseButton button) => Dff.IsRisingEdge((int)button);

        public bool IsButtonReleased(MouseButton button) => Dff.IsFallingEdge((int)button);

        public void Update()
        {
            Dff.ClockSignal();
        }
    }
}
