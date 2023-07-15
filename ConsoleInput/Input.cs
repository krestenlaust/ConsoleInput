namespace ConsoleInput
{
    using System;
    using ConsoleInput.WinAPI;
    using static ConsoleInput.WinAPI.InputEventHandling;

    /// <summary>
    /// Handles console input.
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// Whether to ignore keyboard events when reading ConsoleInput. If ignored then returns keyboard events to the ConsoleInput buffer.
        /// </summary>
        public static bool IgnoreKeyboard;

        /// <summary>
        /// Whether to ignore the mouse when polling events. If ignored then returns mouse events to the ConsoleInput buffer.
        /// </summary>
        public static bool IgnoreMouse;
        
        static IntPtr stdInHandle;

        /// <summary>
        /// Changes ConsoleMode according to input.
        /// </summary>
        /// <param name="enableMouse">Enables input polling for mouse.</param>
        /// <param name="enableKeyboard">Enables input polling for keyboard (disable this if using Console.ReadLine or similar).</param>
        /// <param name="quickSelect">If true, then clicking results in default selection mode.</param>
        public static void Setup(bool enableMouse, bool enableKeyboard, bool quickSelect = false)
        {
            IgnoreKeyboard = !enableKeyboard;
            IgnoreMouse = !enableMouse;

            stdInHandle = GetStdHandle(STD_INPUT_HANDLE);

            SetConsoleMode(stdInHandle, (quickSelect ? 0 : ENABLE_EXTENDED_FLAGS) | ENABLE_MOUSE_INPUT);
        }

        /// <summary>
        /// Call to update keystates for mouse and keyboard, and mouse position.
        /// </summary>
        public static void Update()
        {
            GetNumberOfConsoleInputEvents(stdInHandle, out uint numberOfEvents);
            INPUT_RECORD[] inputBuffer = new INPUT_RECORD[numberOfEvents];

            if (numberOfEvents > 0)
            {
                ReadConsoleInput(stdInHandle, inputBuffer, numberOfEvents, out numberOfEvents);
            }

            INPUT_RECORD[] otherInputBuffer = new INPUT_RECORD[numberOfEvents];
            uint otherInputCount = 0;

            for (int i = 0; i < numberOfEvents; i++)
            {
                switch (inputBuffer[i].EventType)
                {
                    case InputEventType.KEY_EVENT when !IgnoreKeyboard:
                        Keyboard.HandleKeyboardEvent(inputBuffer[i].Event.KeyEvent);
                        break;
                    case InputEventType.MOUSE_EVENT when !IgnoreMouse:
                        Mouse.HandleMouseEvent(inputBuffer[i].Event.MouseEvent);
                        break;
                    default:
                        otherInputBuffer[otherInputCount++] = inputBuffer[i];
                        break;
                }
            }

            // Rewrite unused ConsoleInput.
            WriteConsoleInput(stdInHandle, otherInputBuffer, otherInputCount, out uint _);

            Mouse.Update();
            Keyboard.Update();
        }
    }
}
