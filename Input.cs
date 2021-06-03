namespace ConsoleInput
{
    using System;
    using System.Runtime.InteropServices;

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
        private const int STD_INPUT_HANDLE = -10;
        private const uint ENABLE_EXTENDED_FLAGS = 0x0080;
        private const uint ENABLE_MOUSE_INPUT = 0x0010;
        private static IntPtr stdInHandle;

        internal enum MouseEventFlags // https://docs.microsoft.com/en-us/windows/console/mouse-event-record-str
        {
            DOUBLE_CLICK = 0x0002,
            MOUSE_MOVED = 0x0001,
            MOUSE_HWHEELED = 0x0008,
            MOUSE_WHEELED = 0x0004,
        }

        internal enum InputEventType
        {
            FOCUS_EVENT = 0x0010,
            KEY_EVENT = 0x0001,
            MENU_EVENT = 0x0008,
            MOUSE_EVENT = 0x0002,
            WINDOW_BUFFER_SIZE_EVENT = 0x0004,
        }

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

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetNumberOfConsoleInputEvents(IntPtr hConsoleInput, out uint lpcNumberOfEvents);

        [DllImport("kernel32.dll", EntryPoint = "ReadConsoleInputW", CharSet = CharSet.Unicode)]
        private static extern bool ReadConsoleInput(IntPtr hConsoleInput, [Out] INPUT_RECORD[] lpBuffer, uint nLength, out uint lpNumberOfEventsRead);

        [DllImport("kernel32.dll", EntryPoint = "WriteConsoleInputW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool WriteConsoleInput(IntPtr hConsoleInput, INPUT_RECORD[] lpBuffer, uint nLength, out uint lpNumberOfEventsWritten);

        [StructLayout(LayoutKind.Sequential)]
        internal struct COORD
        {
            public short X;
            public short Y;

            public COORD(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct INPUT_RECORD_UNION
        {
            [FieldOffset(0)]
            public KEY_EVENT_RECORD KeyEvent;
            [FieldOffset(0)]
            public MOUSE_EVENT_RECORD MouseEvent;
            [FieldOffset(0)]
            public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;
            [FieldOffset(0)]
            public MENU_EVENT_RECORD MenuEvent;
            [FieldOffset(0)]
            public FOCUS_EVENT_RECORD FocusEvent;
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        internal struct KEY_EVENT_RECORD
        {
            [FieldOffset(0), MarshalAs(UnmanagedType.Bool)]
            public bool bKeyDown;
            [FieldOffset(4), MarshalAs(UnmanagedType.U2)]
            public ushort wRepeatCount;
            [FieldOffset(6), MarshalAs(UnmanagedType.U2)]
            public Keyboard.VirtualKey wVirtualKeyCode;
            [FieldOffset(8), MarshalAs(UnmanagedType.U2)]
            public ushort wVirtualScanCode;
            [FieldOffset(10)]
            public char UnicodeChar;
            [FieldOffset(12), MarshalAs(UnmanagedType.U4)]
            public uint dwControlKeyState;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSE_EVENT_RECORD
        {
            [FieldOffset(0)]
            public COORD dwMousePosition;
            [FieldOffset(4)]
            public uint dwButtonState;
            [FieldOffset(8)]
            public uint dwControlKeyState;
            [FieldOffset(12)]
            public MouseEventFlags dwEventFlags;
        }

        internal struct WINDOW_BUFFER_SIZE_RECORD
        {
            public COORD dwSize;

            public WINDOW_BUFFER_SIZE_RECORD(short x, short y)
            {
                dwSize = new COORD(x, y);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MENU_EVENT_RECORD
        {
            public uint dwCommandId;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct FOCUS_EVENT_RECORD
        {
            public uint bSetFocus;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT_RECORD
        {
            public InputEventType EventType;
            public INPUT_RECORD_UNION Event;
        }
    }
}
