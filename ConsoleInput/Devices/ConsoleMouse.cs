using ConsoleInput.Logic;
using ConsoleInput.WinAPI;
using static ConsoleInput.WinAPI.InputEventHandling;

namespace ConsoleInput.Devices
{
    /// <summary>
    /// A device representing the physical mouse. Handles all mouse events forwarded through the console input-event system.
    /// </summary>
    public class ConsoleMouse : IDevice, IInputRecordObserver<MOUSE_EVENT_RECORD>, IRequireConsoleMode, IButtonDevice<MouseButton>, ICursorDevice
    {
        /// <summary>
        /// The amount of buttons available.
        /// </summary>
        public const int MouseButtonCount = 5;

        readonly bool quickSelect;
        readonly DataFlipFlopArray dff;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMouse"/> class.
        /// </summary>
        /// <param name="quickSelectEnabled">If enabled, clicking results in normal console selection behavior.</param>
        public ConsoleMouse(bool quickSelectEnabled)
        {
            dff = new DataFlipFlopArray(MouseButtonCount);

            this.quickSelect = quickSelectEnabled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMouse"/> class. Disables quick select mode.
        /// </summary>
        public ConsoleMouse()
            : this(false)
        {
        }

        /// <inheritdoc/>
        public short X { get; set; }

        /// <inheritdoc/>
        public short Y { get; set; }

        /// <inheritdoc/>
        public uint GetConsoleMode() => (quickSelect ? 0 : ENABLE_EXTENDED_FLAGS) | ENABLE_MOUSE_INPUT;

        /// <summary>
        /// Modifies mouse state based on mouse event.
        /// </summary>
        /// <param name="mouseEvent">Event returned by ReadConsoleInput.</param>
        /// <returns><inheritdoc/></returns>
        public bool HandleEvent(MOUSE_EVENT_RECORD mouseEvent)
        {
            switch (mouseEvent.dwEventFlags)
            {
                case MouseEventFlags.DOUBLE_CLICK:
                    break;
                case MouseEventFlags.MOUSE_MOVED:
                    X = mouseEvent.dwMousePosition.X;
                    Y = mouseEvent.dwMousePosition.Y;
                    break;
                case MouseEventFlags.MOUSE_HWHEELED:
                    break;
                case MouseEventFlags.MOUSE_WHEELED:
                    break;
                case 0:
                    // mousebutton pressed or up
                    for (int n = 0; n < MouseButtonCount; n++)
                    {
                        dff.Signals[n] = (mouseEvent.dwButtonState & (1 << n)) != 0;
                    }

                    break;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool IsButtonDown(MouseButton button) => dff.Signals[(int)button];

        /// <inheritdoc/>
        public bool IsButtonPressed(MouseButton button) => dff.IsRisingEdge((int)button);

        /// <inheritdoc/>
        public bool IsButtonReleased(MouseButton button) => dff.IsFallingEdge((int)button);

        /// <inheritdoc/>
        public void Update()
        {
            dff.ClockSignal();
        }
    }
}
