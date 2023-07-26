using System;
using ConsoleInput.WinAPI;

namespace ConsoleInput
{
    /// <summary>
    /// Contains logic for handling mouse button states and position-data based on events retrieved by Windows Console API.
    /// </summary>
    [Obsolete("This API isn't supported any longer. Please use new InputManager and MouseV2 API.")]
    public static class Mouse
    {
        /// <summary>
        /// Describes whether a given mouse button (by index) is currently registered as 'down'.
        /// A given button in <c>MouseDown</c> is true as soon as <c>MousePress</c> is and stays true until <c>MouseUp</c> is.
        /// In short: True the whole duration of a mouse press.
        /// </summary>
        [Obsolete("Use new mouse API instead.")]
        public static readonly bool[] MouseDown = new bool[MouseButtonCount];

        /// <summary>
        /// Describes whether a given mouse button (by index) is just released.
        /// A given button in <c>MouseUp</c> is true the only the first update after it was released.
        /// </summary>
        [Obsolete("Use new mouse API instead.")]
        public static readonly bool[] MouseUp = new bool[MouseButtonCount];

        /// <summary>
        /// Describes whether a given mouse button (by index) is just pressed.
        /// A given button in <c>MousePress</c> is true only the first update after it was pressed.
        /// </summary>
        [Obsolete("Use new mouse API instead.")]
        public static readonly bool[] MousePress = new bool[MouseButtonCount];

        /// <summary>
        /// The amount of buttons available.
        /// </summary>
        internal const int MouseButtonCount = 5;

        static readonly bool[] MouseDownPrevious = new bool[MouseButtonCount];
        static readonly bool[] MouseDownCurrent = new bool[MouseButtonCount];

        /// <summary>
        /// Gets the column position of mouse (based on console window).
        /// </summary>
        public static short X { get; private set; } = 0;

        /// <summary>
        /// Gets the row position of mouse (based on console window).
        /// </summary>
        public static short Y { get; private set; } = 0;

        /// <summary>
        /// Gets whether the mouse button is currently held down.
        /// </summary>
        /// <param name="button">The mouse button to check.</param>
        /// <returns>The mouse button down state.</returns>
        public static bool GetMouseDown(MouseButton button) => MouseDown[(int)button];

        /// <summary>
        /// Gets whether the mouse button has just been pressed down (only true for the first clock-cycle after).
        /// </summary>
        /// <param name="button">The mouse button to check.</param>
        /// <returns>The mouse button just-pressed state.</returns>
        public static bool GetMousePressed(MouseButton button) => MousePress[(int)button];

        /// <summary>
        /// Called internally by Input class to update mouse input.
        /// </summary>
        internal static void Update()
        {
            // mousebutton held down
            for (int i = 0; i < MouseButtonCount; i++)
            {
                MouseUp[i] = false;
                MousePress[i] = false;

                if (MouseDownCurrent[i] != MouseDownPrevious[i])
                {
                    if (MouseDownCurrent[i])
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

                MouseDownPrevious[i] = MouseDownCurrent[i];
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
                        MouseDownCurrent[n] = (mouseEvent.dwButtonState & (1 << n)) != 0;
                    }

                    break;
            }
        }
    }
}
