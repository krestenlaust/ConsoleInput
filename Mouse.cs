namespace ConsoleInput
{
    using static ConsoleInput.Input;

    /// <summary>
    /// Contains logic for handling mouse button states and position-data based on events retrieved by Windows Console API.
    /// </summary>
    public static class Mouse
    {
        /// <summary>
        /// Describes whether a given mouse button (by index) is currently registered as 'down'.
        /// A given button in <c>MouseDown</c> is true as soon as <c>MousePress</c> is and stays true until <c>MouseUp</c> is.
        /// In short: True the whole duration of a mouse press.
        /// </summary>
        public static readonly bool[] MouseDown = new bool[MOUSE_BUTTON_COUNT];

        /// <summary>
        /// Describes whether a given mouse button (by index) is just released.
        /// A given button in <c>MouseUp</c> is true the only the first update after it was released.
        /// </summary>
        public static readonly bool[] MouseUp = new bool[MOUSE_BUTTON_COUNT];

        /// <summary>
        /// Describes whether a given mouse button (by index) is just pressed.
        /// A given button in <c>MousePress</c> is true only the first update after it was pressed.
        /// </summary>
        public static readonly bool[] MousePress = new bool[MOUSE_BUTTON_COUNT];

        /// <summary>
        /// The amount of buttons available.
        /// </summary>
        internal const int MOUSE_BUTTON_COUNT = 5;

        private static readonly bool[] mouseDownPrevious = new bool[MOUSE_BUTTON_COUNT];
        private static readonly bool[] mouseDownCurrent = new bool[MOUSE_BUTTON_COUNT];

        /// <summary>
        /// Column position of mouse (based on console window).
        /// </summary>
        public static short x { get; private set; } = 0;

        /// <summary>
        /// Row position of mouse (based on console window).
        /// </summary>
        public static short y { get; private set; } = 0;

        /// <summary>
        /// Called internally by Input class to update mouse input.
        /// </summary>
        internal static void Update()
        {
            // mousebutton held down
            for (int i = 0; i < MOUSE_BUTTON_COUNT; i++)
            {
                MouseUp[i] = false;
                MousePress[i] = false;

                if (mouseDownCurrent[i] != mouseDownPrevious[i])
                {
                    if (mouseDownCurrent[i])
                    {
                        MousePress[i] = true;
                        MouseDown[i] = true;
                    }
                    else
                    {
                        MouseUp[i] = true;
                        MouseDown[i] = false;
                    }
                }

                mouseDownPrevious[i] = mouseDownCurrent[i];
            }
        }

        /// <summary>
        /// Modifies mouse state based on mouse event.
        /// </summary>
        /// <param name="mouseEvent">Event returned by ReadConsoleInput.</param>
        internal static void HandleMouseEvent(MOUSE_EVENT_RECORD mouseEvent)
        {
            switch (mouseEvent.dwEventFlags)
            {
                case MouseEventFlags.DOUBLE_CLICK:
                    break;
                case MouseEventFlags.MOUSE_MOVED:
                    x = mouseEvent.dwMousePosition.X;
                    y = mouseEvent.dwMousePosition.Y;
                    break;
                case MouseEventFlags.MOUSE_HWHEELED:
                    break;
                case MouseEventFlags.MOUSE_WHEELED:
                    break;
                case 0:
                    // mousebutton pressed or up
                    for (int n = 0; n < MOUSE_BUTTON_COUNT; n++)
                    {
                        mouseDownCurrent[n] = (mouseEvent.dwButtonState & (1 << n)) != 0;
                    }

                    break;
            }
        }
    }
}
